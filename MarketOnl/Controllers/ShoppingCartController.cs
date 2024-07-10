using MarketOnl.Data;
using MarketOnl.Extentions;
using MarketOnl.Models;
using Microsoft.AspNetCore.Mvc;


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


        public IActionResult ShowCount()
        {
            ShoppingCart cart = HttpContext.Session.Get<ShoppingCart>("Cart");
            if (cart != null)
            {
                return Json(new { Count = cart.Items.Count });
            }
            return Json(new { Count = 0 });
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
