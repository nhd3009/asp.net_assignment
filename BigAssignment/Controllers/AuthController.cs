
using BigAssignment.Helper;
using BigAssignment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BigAssignment.Controllers
{
    public class AuthController : Controller
    {
        MovieWebContext _dbContext = new MovieWebContext();

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                return View();
            } else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            var encryptionPassword = XString.ToMD5(user.Password);
            if (HttpContext.Session.GetString("Email") == null)
            {
                var userLogin = _dbContext.Users.Where(x => x.Name == user.Name && x.Password == encryptionPassword).FirstOrDefault();
                if (userLogin != null)
                {  
                    HttpContext.Session.SetString("Name", userLogin.Name.ToString());
                    HttpContext.Session.SetString("user_id", userLogin.Id.ToString());
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
    }
}
