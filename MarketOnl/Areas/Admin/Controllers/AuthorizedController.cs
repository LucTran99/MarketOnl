using MarketOnl.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketOnl.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorizedController : Controller
    {
        private readonly BanHangOnlContext _context;

        public AuthorizedController(BanHangOnlContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Save(int GroupId, int RoleId)
        {
            var item = await _context.Authorizeds.Where(i => i.RolesId == GroupId && i.CrudId == RoleId).FirstOrDefaultAsync();
            if(item == null)
            {
                item = new Data.Authorized()
                {
                    RolesId = GroupId,
                    CrudId = RoleId
                };
                _context.Authorizeds.Add(item);
            }
            else
            {
                _context.Authorizeds.Remove(item);
            }
            await _context.SaveChangesAsync();
            return Ok();


        }
    }
}
