using MarketOnl.Data;
using MarketOnl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
namespace MarketOnl.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CatNews : Controller
    {
        private readonly BanHangOnlContext _context;

        public CatNews(BanHangOnlContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {


            return View();
        }


        [HttpPost]
        public async Task<IActionResult> GetList(jDatatable model)
        {
            var item = (from i in _context.NewsCats select i);
            int records = 0;

            if (!string.IsNullOrEmpty(model.columns[model.order[0].column].name) && !string.IsNullOrEmpty(model.order[0].dir))
            {
                item = item.OrderBy(model.columns[model.order[0].column].name + ' ' + model.order[0].dir);
            }


            if (!string.IsNullOrEmpty(model.search.value))
            {
                item = item.Where(x => x.Title.Contains(model.search.value));
            }

            records = item.Count();

            var data = await item.Select(i => new
            {
                i.NewCatId,
                i.Title

            }).Skip(model.start).Take(model.length).ToListAsync();

            var jsonData = new { draw = model.draw, recordsFiltered = records, recordsTotal = records, data = data };
            return Ok(jsonData);
        }



        [HttpPost]
        public async Task<ActionResult> Save(CatNewsVM model)
        {

            MarketOnl.Data.NewsCat items = new MarketOnl.Data.NewsCat();


            items.Title = model.Name;

            await _context.NewsCats.AddAsync(items);
            await _context.SaveChangesAsync();
            return Ok(items);


        }

    }
}
