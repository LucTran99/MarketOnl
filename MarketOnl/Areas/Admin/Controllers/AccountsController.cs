using MarketOnl.Data;
using MarketOnl.Extentions;
using MarketOnl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Security.Cryptography;
using System.Text;

namespace MarketOnl.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountsController : Controller
    {
        private readonly BanHangOnlContext _context;

        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;


        public AccountsController(BanHangOnlContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> GetList(jDatatable model)
        {
            var items = (from i in _context.Accounts select i);
            int recordsTotal = 0;
            // Nếu cột order ko rỗng (là có nhấn sắp xếp). Và khi nhấn vào nó sẽ trả về cột order thứ mấy
            if (!string.IsNullOrEmpty(model.columns[model.order[0].column].name) && !string.IsNullOrEmpty(model.order[0].dir))
            {
                items = items.OrderBy(model.columns[model.order[0].column].name + ' ' + model.order[0].dir);
            }

            // Tìm kiếm
            if (!string.IsNullOrEmpty(model.search.value))
            {
                items = items.Where(x => x.Name.Contains(model.search.value));
            }
            recordsTotal = items.Count();


            // Phân trang
            var data = await items.Select(i => new
            {
                i.AccountId,

                i.Name,
                roleName = i.Role.RoleName,
                i.LastLogin,
                i.Picture, 
                i.PhoneNumber


            }).Skip(model.start).Take(model.length).ToListAsync();


            var jsonData = new { draw = model.draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
            return Ok(jsonData);
        }

        [HttpGet]
        public async Task<IActionResult> getItem(int id)
        {
            if (_context.Accounts == null)
            {

                return NotFound();
            }

            var item = await _context.Accounts.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }



        [HttpPost]
        public async Task<ActionResult> Save(AccountsVM model, IFormFile Picture)
        {
            MarketOnl.Data.Account items;

            if(model.AccountId == null)
            {
                // Tạo mới một tài khoản
                items = new MarketOnl.Data.Account();

                items.CreateDate = DateTime.Now;
                await _context.Accounts.AddAsync(items);

            }
            else
            {
                items = await _context.Accounts.FindAsync(model.AccountId);
                items.UpdateDate = DateTime.Now;
                
            }

            items.Name = model.Name;
            items.Email = model.Email;
            items.PhoneNumber = model.PhoneNumber;


            if (!string.IsNullOrEmpty(model.Password))
            {
                var passHash = HashPassword(model.Password);
                items.Password = passHash;
            }
            if (Picture != null)
            {
                var path = Path.Combine(this._environment.WebRootPath, "AdminAssets/images/user", Picture.FileName);
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await Picture.CopyToAsync(stream);
                    stream.Close();

                }
                items.Picture = "/AdminAssets/images/user/" + Picture.FileName;
            }


            items.RoleId = model.RoleId;
            await _context.SaveChangesAsync();

            return Ok(items);
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





        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginVM model)
        {
            var passHash = HashPassword(model.Password);

            var items = _context.Accounts.Where(x => x.Email == model.Email && x.Password == passHash).FirstOrDefault();
            if (items != null)
            {
                HttpContext.Session.Set("Member", items);
                return RedirectToAction("Index", "Home");
            }


            return RedirectToAction("Login", "Accounts");


        }


        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Member");
            return RedirectToAction("Login", "Accounts");
        }
    }
}
