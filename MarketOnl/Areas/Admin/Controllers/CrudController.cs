using MarketOnl.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketOnl.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CrudController : Controller
    {
        private readonly BanHangOnlContext _context;

        public CrudController(BanHangOnlContext context)
        {
            _context = context;

        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public  async Task<IActionResult> GetAllByCategory(int CategoryId, int GroupId)
        {
            var items = await (from r in _context.Cruds.Where( i => i.CategoryId ==  CategoryId )
                               join a in _context.Authorizeds.Where(i => i.RolesId== GroupId)
                               on r.CrudId equals a.CrudId into g

                               // Nếu không có bản ghi nào khớp trong Authorizeds, a sẽ là null.
                               // Điều này giúp thực hiện left join, lấy tất cả các bản ghi từ Cruds kể cả khi không có bản ghi khớp trong Authorizeds.
                               from a in g.DefaultIfEmpty()
                              
                               select new
                               {
                                   r.CrudId,
                                   r.Name,
                                   a.RolesId
                               }).ToListAsync();
            return Ok(items);
        }
    }
}
