using BigAssignment.Library;
using BigAssignment.Models;
using Microsoft.AspNetCore.Mvc;

namespace BigAssignment.Areas.Admin.Controllers
{
    public class AccessController : Controller
    {
        MovieWebContext db = new MovieWebContext();

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Admin_Name") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
        }

        [HttpPost]
        public IActionResult Login(string user, string pass)
        {
            int count_username = db.Users.Where(m => m.Status == 1 && ((m.Phone).ToString() == user || m.Email == user || m.Name == user) && m.Access != 0).Count();
            if (count_username == 0)
            {
                ViewBag.Error = "No User found";
                return View();
            }
            else
            {
                String password = XString.ToMD5(pass);
                var user_account = db.Users.Where(m => m.Status == 1 && ((m.Phone).ToString() == user || m.Email == user || m.Name == user) && m.Access != 0 && m.Password == password);
                if (user_account == null)
                {
                    ViewBag.Error = "Invalid username or password";
                    return View();
                }
                else
                {
                    var useracc = user_account.First();
                    HttpContext.Session.SetString("Admin_Name", useracc.Fullname);
                    HttpContext.Session.SetString("Admin_Id", useracc.Id.ToString());
                    HttpContext.Session.SetString("Admin_Images", useracc.Image);
                    HttpContext.Session.SetString("Admin_Address", useracc.Address);
                    HttpContext.Session.SetString("Admin_Email", useracc.Email);
                    HttpContext.Session.SetString("Admin_CreatedAt", useracc.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"));
                    return RedirectToAction("Index", "Admin");
                }
            }

        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            // Xóa thông tin của người dùng khỏi session
            HttpContext.Session.Remove("Admin_Id");
            HttpContext.Session.Remove("Admin_Name");
            HttpContext.Session.Remove("Admin_Email");
            HttpContext.Session.Remove("Admin_CreatedAt");
            HttpContext.Session.Remove("Admin_Address");
            HttpContext.Session.Remove("Admin_Images");

            // Chuyển hướng đến trang đăng nhập
            return RedirectToAction("Login", "AuthenAdmin");
        }
    }
}
