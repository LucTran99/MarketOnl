using MarketOnl.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace MarketOnl.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrdersController : Controller
    {
        private BanHangOnlContext _context;

        public OrdersController(BanHangOnlContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var item = _context.Orders.ToList();
            return View(item);
        }



        [HttpPost]
        public ActionResult UpdateTT(int id, int trangthai)
        {
            var item = _context.Orders.Find(id);
            if (item != null)
            {
                _context.Orders.Attach(item);


                
                item.TypePayment = trangthai;

                _context.Entry(item).Property(x => x.TypePayment).IsModified = true;  /// Đánh dấu thuộc tính 'TypePayment' là đã thay đổi 
                _context.SaveChanges();
                return Json(new { massage = "Success", Success = true });
            }
            return Json(new { massage = "UnSuccess", Success = false });
        }


        public IActionResult Details(int id)
        {
            var data = _context.Orders.Find(id);
            var orderDetails = _context.OrderDetails.Include(od => od.Product).Where(x => x.OrderId == id).ToList();
            ViewBag.OrderDetails = orderDetails;
            return View(data);

        }


    }
}
