using BigAssignment.Models;
using Microsoft.AspNetCore.Mvc;

namespace BigAssignment.Controllers
{
    public class LogoutController : Controller
    {

        private readonly MovieWebContext _httpContextAccessor;

        public LogoutController(MovieWebContext httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }

    }
}
