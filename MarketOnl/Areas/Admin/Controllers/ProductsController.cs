using AspNetCoreHero.ToastNotification.Abstractions;

using MarketOnl.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using WebBH.Extentions;

namespace MarketOnl.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly BanHangOnlContext _context;
        public INotyfService NotyfService { get; }
        public ProductsController(BanHangOnlContext context, INotyfService notyfService)
        { 
            _context = context;
            NotyfService = notyfService;
        }
        public  IActionResult Index(int? page)
        {
            var pgeNumber = page == null || page <=0 ? 1 : page.Value;
            var pageSize = 5;

            var listProducts = _context.Products.AsNoTracking().Include(x => x.Cat).OrderByDescending(x => x.ProductId);

            PagedList<Product> model = new PagedList<Product>(listProducts, pgeNumber, pageSize);
            ViewBag.CurrentPage = pgeNumber;

            return View(model);
        }


        public IActionResult Add()
        {
            ViewBag.ProductCategory = new SelectList(_context.CatProducts.ToList(), "CatId", "CatName");


            return View();
        }


        [HttpPost]
        public async  Task<IActionResult> Add(Product model,  Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (ModelState.IsValid)
            {
                // Lấy tên sản phẩm ra và chuyển hóa lại tên nó
                model.ProductName = Utilities.ToTitleCase(model.ProductName);


                // Nếu có chọn ảnh
                if (fThumb != null)
                {
                    string extention = Path.GetExtension(fThumb.FileName);
                    string image = Utilities.SEOUrl(model.ProductName) + extention;
                    model.Picture = await Utilities.UploadFile(fThumb, @"products", image.ToLower());
                }

                if (string.IsNullOrEmpty(model.ProductName)) model.Picture = "default.jpg";

                model.Alias = Utilities.SEOUrl(model.Title);

                NotyfService.Success("Thêm sản phẩm thành công");

                _context.Products.Add(model);
                _context.SaveChanges();



                return RedirectToAction("Index");
            }

            ViewBag.ProductCategory = new SelectList(_context.CatProducts.ToList(), "CatId", "CatName");

            return View();
        }






        public ActionResult Edit(int id)
        {
            ViewBag.ProductCategory = new SelectList(_context.CatProducts.ToList(), "CatId", "CatName");
            var item = _context.Products.Find(id);
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Product model , Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if(id != model.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Lấy tên sản phẩm ra và chuyển hóa lại tên nó
                model.ProductName = Utilities.ToTitleCase(model.ProductName);


                // Nếu có chọn ảnh
                if (fThumb != null)
                {
                    string extention = Path.GetExtension(fThumb.FileName);
                    string image = Utilities.SEOUrl(model.ProductName) + extention;
                    model.Picture = await Utilities.UploadFile(fThumb, @"products", image.ToLower());
                }

                if (string.IsNullOrEmpty(model.ProductName)) model.Picture = "default.jpg";

                model.Alias = Utilities.SEOUrl(model.ProductName);

                NotyfService.Success("Cập nhật sản phẩm thành công");
                _context.Products.Update(model);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.ProductCategory = new SelectList(_context.CatProducts.ToList(), "CatId", "CatName");

            return View();
        }




        [HttpGet]
        public async Task<IActionResult> Delete (int id)
        {
            var item = await _context.Products.FindAsync(id);
            _context.Entry(item).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return Ok(true);

        }


    }
}
