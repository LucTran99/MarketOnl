using MarketOnl.Data;
using MarketOnl.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MarketOnl.Controllers
{
    public class HomeController : Controller
    {
        
		private BanHangOnlContext _context;

		public HomeController(BanHangOnlContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var item = _context.Products.ToList();
            return View(item);
        }

     
    }
}
