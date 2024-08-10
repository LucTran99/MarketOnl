using Azure;
using MarketOnl.Data;
using MarketOnl.Extentions;
using MarketOnl.Models;
using Microsoft.AspNetCore.Mvc;


using Order = MarketOnl.Data.Order;

namespace MarketOnl.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly BanHangOnlContext _context;

        public ShoppingCartController(BanHangOnlContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ShoppingCart cart = HttpContext.Session.Get<ShoppingCart>("Cart");
            if(cart != null && cart.Items.Any())
            {
                return View(cart.Items);
            }
            return View();
         
        }

        public IActionResult CheckOutSuccess()
        {
            ShoppingCart cart = HttpContext.Session.Get<ShoppingCart>("Cart");

            if(cart != null && cart.Items.Any())
            {
                return View(cart.Items);
            }

            return View();
        }

        public IActionResult ShowCount()
        {
            ShoppingCart cart = HttpContext.Session.Get<ShoppingCart>("Cart");
            if (cart != null)
            {
                return Json(new { Count = cart.Items.Count });
            }
            return Json(new { Count = 0 });
        }

        public IActionResult CheckOut()
        {
			ShoppingCart cart = HttpContext.Session.Get<ShoppingCart>("Cart");

            if(cart != null && cart.Items.Any())
            {
                ViewBag.CheckCart = cart;
                //return View(cart.Items);
            }
            return View();
		}
        public IActionResult Partial_View_ThanhToan()
        {
            ShoppingCart cart = HttpContext.Session.Get<ShoppingCart>("Cart");

            if (cart != null && cart.Items.Any())
            {
               
                return View(cart.Items);
            }
            return View();
        }

        public IActionResult PartialCheckOut()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(OrderViewModel orderView)
        {
            var code = new { Success = false, code = -1, message = "Unknown error" };
            if (ModelState.IsValid)
            {
				ShoppingCart cart = HttpContext.Session.Get<ShoppingCart>("Cart");
                if(cart != null)
                {
                    // Lấy ra các thông tin của KH từ Form
                    Order order = new Order();
                    order.CustomerName = orderView.CustomerName;
                    order.Phone = orderView.Phone;
                    order.Address = orderView.Address;
                    order.Email = orderView.Email;



                    // Chi Tiết Đơn Hàng
                    // Đối với mỗi sản phẩm trong giỏ hàng, tạo một đối tượng tb_OrderDetail mới và thêm vào đơn hàng.
                    cart.Items.ForEach(x => order.OrderDetails.Add(new OrderDetail
                    {
                        ProductId = x.productId,
                        Quatity = x.Quantity,
                        Price = x.Price
                    }));


                    // Tổng tiền lấy từ Session
                    order.TotalAmount = cart.Items.Sum(x => (x.Price * x.Quantity));


                    order.TypePayment = orderView.TypePayment;

                    order.CreatedDate = DateTime.Now;

                    // Tạo một mã đơn hàng

                    Random rd = new Random();
                    order.Code = "ĐH"+rd.Next(0,9)  +rd.Next(0, 9) +rd.Next(0, 9);

                    _context.Orders.Add(order);
                    _context.SaveChanges();

					return RedirectToAction("CheckOutSuccess");
				}
            }
            else
            {
                code = new { Success = false, code = -2, message = "Thông tin không hợp lệ" };
            }

            return Json(code);

		}


        [HttpPost]
        public IActionResult AddToCart(int id, int quantity)
        {
            var code = new { Success = false, msg = "", code = -1, Count = 0 };

            try
            {
                

                var checkProduct = _context.Products.FirstOrDefault(x => x.ProductId == id);
                if (checkProduct != null)
                {
                  
                    ShoppingCart shoppingCart = HttpContext.Session.Get<ShoppingCart>("Cart");
                    if (shoppingCart == null)
                    {
                        // Kiểm tra xem giỏ hàng đã tồn tại trong phiên làm việc (session) của người dùng chưa.
                        // Nếu chưa, tạo mới một giỏ hàng.
                        shoppingCart = new ShoppingCart();
                    }


                    // Tạo một đối tượng ShoppingCartItem mới với thông tin từ sản phẩm được truy vấn từ cơ sở dữ liệu.
                    ShoppingCartItem item = new ShoppingCartItem()
                    {
                        productId = checkProduct.ProductId,
                        ProductName = checkProduct.Title,

                        Quantity = quantity,

                    };

                    if (checkProduct.Picture != null)
                    {
                        item.ProductImg = checkProduct.Picture;
                    }

                    if (checkProduct.Price != null)
                    {
                        item.Price = (decimal)checkProduct.Price;
                    }



                    if (checkProduct.PriceSale > 0)
                    {
                        item.Price = (decimal)checkProduct.PriceSale;
                    }
                    item.TotalPrice = item.Quantity * item.Price;


                    shoppingCart.AddToCart(item, quantity);

                    HttpContext.Session.Set("Cart", shoppingCart);

                    code = new { Success = true, msg = "Thêm giỏ hàng thành công", code = 1, Count = shoppingCart.Items.Count() };


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Json(code);
        }



    }
}
