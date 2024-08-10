using MarketOnl.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;

namespace MarketOnl.Controllers
{
    [Route("News")]
    public class NewsController : Controller
    {
        private readonly BanHangOnlContext _context;

        public NewsController(BanHangOnlContext context)
        {
            _context = context;

        }


        [Route("tintuc.html", Name = "News")]
        public IActionResult Index(int? page)
        {
            var pageNumber = page == null || page <= 0 ? 1 : page.Value;
            var pageSize = 3;

            var listNews = _context.News.AsTracking().OrderByDescending(x => x.Title);

            PagedList<News> pagedList = new PagedList<News>(listNews, pageNumber, pageSize);

            return View(pagedList);
        }


        [Route("news/{Alias}-{id}.html", Name ="Posts")]
        public IActionResult DetailsNews(int id)
        {
            var news = _context.News.FirstOrDefault(m => m.NewsId == id);
            if (news != null)
            {
                // Đây là DbSet đại diện cho bảng sản phẩm trong cơ sở dữ liệu.
                _context.News.Attach(news);
                // Khi click vào cộng thêm 1
                news.Views = news.Views + 1;

                // Đánh dấu rằng chỉ có thuộc tính ViewCount của đối tượng sản phẩm đã được thay đổi.
                _context.Entry(news).Property(x => x.Views).IsModified = true;
                _context.SaveChanges();

            }


            return View(news);
        }


    }
}
