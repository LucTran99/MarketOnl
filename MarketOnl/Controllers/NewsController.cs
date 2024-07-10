using MarketOnl.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;

namespace MarketOnl.Controllers
{
	public class NewsController : Controller
	{
		private readonly BanHangOnlContext _context;

		public NewsController(BanHangOnlContext context)
		{
			_context  = context;

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
	}
}
