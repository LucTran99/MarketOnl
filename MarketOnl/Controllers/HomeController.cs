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
            var item = _context.Products.Take(8).ToList();

            //var relatedPro = _context.Products.Take(9).ToList();
            //ViewBag.RelatedPro = relatedPro;

            var sales = _context.Products.Where(i => i.CatId == 3).Take(2).ToList();
            ViewBag.Sales = sales;

            return View(item);
        }


        
     
    }
}
