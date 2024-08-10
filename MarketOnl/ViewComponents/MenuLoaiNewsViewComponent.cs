using MarketOnl.Data;
using MarketOnl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketOnl.ViewComponents
{
    public class MenuLoaiNewsViewComponent : ViewComponent
    {
        private readonly BanHangOnlContext _context;

        public MenuLoaiNewsViewComponent(BanHangOnlContext context)
        {
            _context = context;

        }

        public IViewComponentResult Invoke()
        {
            var data = _context.NewsCats.Select(i => new MenuLoaiNews
            {
                NewCatId = i.NewCatId,
                Title = i.Title
            }).OrderBy(l => l.NewCatId);
            return View(data);
        }

    }
}
