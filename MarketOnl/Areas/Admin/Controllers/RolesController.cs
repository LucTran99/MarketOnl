using MarketOnl.Data;
using MarketOnl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
namespace MarketOnl.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly BanHangOnlContext _context;

        public RolesController(BanHangOnlContext context)
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
            var item = (from i in _context.Roles select i);
            int records = 0;

            if (!string.IsNullOrEmpty(model.columns[model.order[0].column].name) && !string.IsNullOrEmpty(model.order[0].dir))
            {
                item = item.OrderBy(model.columns[model.order[0].column].name + ' ' + model.order[0].dir);
            }


            if (!string.IsNullOrEmpty(model.search.value))
            {
                item = item.Where(x => x.RoleName.Contains(model.search.value));
            }


            records = item.Count();


            var data = await item.Select(i => new
            {
                i.RoleId,
                i.RoleName
            }).Skip(model.start).Take(model.length).ToListAsync();



            var jsonData = new { draw = model.draw, recordsFiltered = records, recordsTotal = records, data = data };
            return Ok(jsonData);





        }



        [HttpGet]
        public async Task<IActionResult> getList()
        {
            var items = (from i in _context.Roles select i);
            var data = await items.Select(x => new { x.RoleId, x.RoleName }).ToListAsync();
            return Ok(data);
        }




        [HttpPost]
        public async Task<IActionResult> Save(RolesVM model)
        {
                
            MarketOnl.Data.Role item = new MarketOnl.Data.Role();

            item.RoleName = model.Name;

            await _context.Roles.AddAsync(item);
            await _context.SaveChangesAsync();
            return Ok(item);



        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {


            var item = await _context.Roles.FindAsync(id);
            _context.Entry(item).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return Ok(true);


        }





    }
}
