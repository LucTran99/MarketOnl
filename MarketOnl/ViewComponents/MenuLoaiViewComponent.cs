using MarketOnl.Data;
using MarketOnl.Models;
using Microsoft.AspNetCore.Mvc;

namespace MarketOnl.ViewComponents
{

    public class MenuLoaiViewComponent : ViewComponent
    {
        private readonly BanHangOnlContext _context;

        public MenuLoaiViewComponent(BanHangOnlContext context)
        {
            _context = context;

        }
        public IViewComponentResult Invoke()
        {
            var data = _context.CatProducts.Select(i => new MenuLoai
            {
                CatId = i.CatId,
                CatName = i.CatName
            }).OrderBy(l => l.CatName);
            return View(data);
        }
    }
}
