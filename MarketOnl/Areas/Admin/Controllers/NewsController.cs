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
    public class NewsController : Controller
    {
        private readonly BanHangOnlContext _context;
        public INotyfService NotyfService { get; }
        public NewsController(BanHangOnlContext context, INotyfService notyfService)
        {
            _context = context;
            NotyfService = notyfService;
        }
        public IActionResult Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 5;

            var listNews = _context.News.AsNoTracking().Include(x => x.NewCat);
            PagedList<News> model = new PagedList<News>(listNews, pageNumber, pageSize);


            ViewBag.CurrentPage = pageNumber;

            return View(model);
        }



        public IActionResult Add()
        {
            ViewBag.CatNews = new SelectList(_context.NewsCats.ToList(), "NewCatId", "Title");

            return View();
        }

        [HttpPost]
       
        public async Task<IActionResult> Add(News model, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {
            if (ModelState.IsValid)
            {
                // Lấy tên sản phẩm ra và chuyển hóa lại tên nó
                model.Title = Utilities.ToTitleCase(model.Title);

                if (fThumb != null)
                {
                    string extention = Path.GetExtension(fThumb.FileName);
                    string image = Utilities.SEOUrl(model.Title) + extention;
                    model.Image = await Utilities.UploadFile(fThumb, @"news", image.ToLower());
                }

                if (string.IsNullOrEmpty(model.Title)) model.Image = "default.jpg";
                model.Alias = Utilities.SEOUrl(model.Title);
                model.CreatedOn = DateTime.Now;

                NotyfService.Success("Thêm tin tức thành công");

                _context.News.Add(model);
                _context.SaveChanges();



                return RedirectToAction("Index");
            }

            ViewBag.CatNews = new SelectList(_context.NewsCats.ToList(), "NewCatId", "Title");

            return View();

        }

        public IActionResult Edit (int id)
        {
            ViewBag.CatNews = new SelectList(_context.NewsCats.ToList(), "NewCatId", "Title");

            var items = _context.News.Find(id);


            return View(items);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, News model, Microsoft.AspNetCore.Http.IFormFile fThumb)
        {

            if (id != model.NewsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Lấy tên sản phẩm ra và chuyển hóa lại tên nó
                model.Title = Utilities.ToTitleCase(model.Title);

                if (fThumb != null)
                {
                    string extention = Path.GetExtension(fThumb.FileName);
                    string image = Utilities.SEOUrl(model.Title) + extention;
                    model.Image = await Utilities.UploadFile(fThumb, @"news", image.ToLower());
                }

               // if (string.IsNullOrEmpty(model.Title)) model.Image = "default.jpg";

                model.Alias = Utilities.SEOUrl(model.Title);
               

                model.UpdatedOn = DateTime.Now;

                NotyfService.Success("Cập nhật tin tức thành công");

                _context.News.Update(model);
                _context.SaveChanges();



                return RedirectToAction("Index");
            }

            ViewBag.CatNews = new SelectList(_context.NewsCats.ToList(), "NewCatId", "Title");

            return View();

        }



        [HttpGet]

        public async Task<IActionResult> Delete(int id)
        {
            var items = await _context.News.FindAsync(id);
            _context.Entry(items).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return Ok(true);


        }




    }
}
