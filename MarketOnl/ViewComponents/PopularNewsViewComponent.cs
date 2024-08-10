using MarketOnl.Data;
using MarketOnl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketOnl.ViewComponents
{

    public class PopularNewsViewComponent : ViewComponent
    {
        private readonly BanHangOnlContext _context;

        public PopularNewsViewComponent(BanHangOnlContext context)
        {
            _context = context;

        }

        public IViewComponentResult Invoke()
        {
            var data = _context.News.Where( i => i.Published == true).ToList();
            return View(data);
        }
        
    }
}
