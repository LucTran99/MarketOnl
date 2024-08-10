using MarketOnl.Models;
using Microsoft.AspNetCore.Mvc;
using MarketOnl.Data;
using System.Security.Cryptography;
using System.Text;
using MarketOnl.Session;
using Microsoft.AspNetCore.Http;
namespace MarketOnl.Controllers
{
    public class LoginController : Controller
    {

		private readonly BanHangOnlContext _context;

		

		public LoginController(BanHangOnlContext context)
		{
			_context = context;
		
		}

		public IActionResult Index()
        {

            return View();

        }


		[HttpPost]
		public IActionResult login(LoginClient model)
		{

			var password = HashPassword(model.Password);

			var data = _context.Customers.Where(x => x.CustomerEmail == model.Email && x.CustomerPassoword == password).FirstOrDefault();

			if (data != null)
			{
				HttpContext.Session.SetInt32("CustomerId", data.CustomerId);

				return RedirectToAction("Index", "Home");
			}

            return RedirectToAction("register", "Login");
        }


        public IActionResult Register()
        {
            return View();
        }

		public IActionResult logout()
		{

			TempData["LogoutMessage"] = "Bạn có muốn đăng xuất không?";

			HttpContext.Session.Remove("CustomerId");


			return RedirectToAction("Index", "Home");
		}


        [HttpPost]
        public IActionResult addCustomer(RegisterClientVM model)
        {

		var items = new  MarketOnl.Data.Customer();

            if(items.CreatedDate == null)
            {
				items.CreatedDate = DateTime.Now;

			}

            items.CustomerName = model.CustomerName;
            items.CustomerEmail = model.Email;
            items.CustomerPhone = model.Phone;

            if(!string.IsNullOrEmpty(model.Password))
            {
				var passHash = HashPassword(model.Password);
				items.CustomerPassoword = passHash;
			}


		
			_context.Add(items);
			_context.SaveChanges();

            HttpContext.Session.SetInt32("CustomerId", items.CustomerId);


			// Trang đăng nhập
            return RedirectToAction("index");
        }


		public static string HashPassword(string password)
		{
			using (MD5 md5 = MD5.Create())
			{
				byte[] inputBytes = Encoding.ASCII.GetBytes(password);
				byte[] hashBytes = md5.ComputeHash(inputBytes);

				StringBuilder sb = new StringBuilder();
				for (int i = 0; i < hashBytes.Length; i++)
				{
					sb.Append(hashBytes[i].ToString("X2"));
				}
				return sb.ToString();
			}
		}



	}
}
