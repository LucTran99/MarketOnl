using Microsoft.AspNetCore.Mvc;

namespace MarketOnl.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
