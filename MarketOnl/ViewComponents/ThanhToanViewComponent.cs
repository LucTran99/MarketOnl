using MarketOnl.Extentions;
using MarketOnl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketOnl.ViewComponents
{
    public class ThanhToanViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            ShoppingCart cart = HttpContext.Session.Get<ShoppingCart>("Cart");

            if (cart != null && cart.Items.Any())
            {

                return View(cart.Items);
            }
            return View();
        }
    }
}
