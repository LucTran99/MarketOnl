using MarketOnl.Data;
using MarketOnl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Dynamic.Core;
namespace MarketOnl.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CatProductsController : Controller
    {
        private readonly BanHangOnlContext _context;

        public CatProductsController(BanHangOnlContext context)
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
            var item = (from i in _context.CatProducts select i);
            int records = 0;

            if (!string.IsNullOrEmpty(model.columns[model.order[0].column].name) && !string.IsNullOrEmpty(model.order[0].dir))
            {
                item = item.OrderBy(model.columns[model.order[0].column].name + ' ' + model.order[0].dir);
            }


            if (!string.IsNullOrEmpty(model.search.value))
            {
                item = item.Where(x => x.CatName.Contains(model.search.value));
            }

            records = item.Count();


            var data = await item.Select(i => new
            {
                i.CatId,
                i.CatName
            }).Skip(model.start).Take(model.length).ToListAsync();

            var jsonData = new { draw = model.draw, recordsFiltered = records, recordsTotal = records, data = data };
            return Ok(jsonData);
        }



        [HttpPost]
        public async Task<ActionResult> Save(CatProductsVM model)
        {
         
            MarketOnl.Data.CatProduct items = new MarketOnl.Data.CatProduct();


            items.CatName = model.Name;

            await _context.CatProducts.AddAsync(items);
            await _context.SaveChangesAsync();
            return Ok(items);


        }



        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {


            var item = await _context.CatProducts.FindAsync(id);
            _context.Entry(item).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return Ok(true);


        }

    }
}
