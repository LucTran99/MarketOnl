using MarketOnl.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketOnl.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly BanHangOnlContext _context;

        public CategoryController(BanHangOnlContext context)
        {
            _context = context;
           
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> GetChild(int parentId)
        {
            var items = from i in _context.Categories where i.ParentId == parentId select i;
            var data = await items.Select(i => new { i.Id, i.Name, }).ToListAsync();
            return Ok(data);

        }

    }
}
