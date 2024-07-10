using Microsoft.AspNetCore.Mvc;
using MarketOnl.Data;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
namespace MarketOnl.Controllers
{
    public class ProductsController : Controller
    {
        private readonly BanHangOnlContext _context;

        public ProductsController(BanHangOnlContext context)
        {
            _context = context;

        }



        [Route("sanpham.html", Name = "ShopProducts")]
        public IActionResult Index(int? page)
        {
            var pageNumber  = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 6;


            var listPro = _context.Products.AsNoTracking().OrderByDescending(x => x.Title);

            PagedList<Product> pagedList = new PagedList<Product>(listPro, pageNumber, pageSize);

			//var item = _context.Products.Include(x => x.Cat).ToList();
			ViewBag.CurrentPage = pageNumber;


			return View(pagedList);
        }


        [Route("/{Alias}-{id}.html", Name = "ProductsDetails")]
        public IActionResult Details(int id)
        {
            var product = _context.Products.FirstOrDefault(m => m.ProductId == id);
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            // Khi click vào cộng thêm 1
            product.ViewCount = product.ViewCount + 1;

            // Đánh dấu rằng chỉ có thuộc tính ViewCount của đối tượng sản phẩm đã được thay đổi.
            _context.Entry(product).Property(x => x.ViewCount).IsModified = true;

            return View(product);
        }
      


    }
}
