using Microsoft.AspNetCore.Mvc;
using MarketOnl.Data;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
namespace MarketOnl.Controllers
{
    public class ProductsCatController : Controller
    {
        private readonly BanHangOnlContext _context;

        public ProductsCatController(BanHangOnlContext context)
        {
            _context = context;
        }


        // GET: ProductCattegory
        public IActionResult Index(int id, int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 6;


            var listPro = _context.Products.AsNoTracking().Where(x => x.CatId == id).OrderByDescending(x => x.ProductId);


            PagedList<Product> pagedList = new PagedList<Product>(listPro, pageNumber, pageSize);

            //var item = _context.Products.Where(x => x.CatId == id).ToList();


            return View(pagedList);
        }
    }
}
