using Microsoft.AspNetCore.Mvc;

namespace BigAssignment.Controllers
{
    public class NewsController : Controller
    {
       
        public IActionResult Site()
        {
            return View();
        }
    }
}
