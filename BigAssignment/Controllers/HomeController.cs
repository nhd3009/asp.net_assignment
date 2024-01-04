using BigAssignment.Helper;
using BigAssignment.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using X.PagedList;

namespace BigAssignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MovieWebContext _dbContext;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _dbContext = new MovieWebContext();
        }

        public IActionResult Index(int? page)
        {
            ViewBag.pageN = page;
            var listSlider = _dbContext.Sliders.ToList();
            return View(listSlider);
        }

        public IActionResult IndexHome()
        {
            return RedirectToAction("Index");
        }


        public IActionResult DetailProduct(int IdProduct)
        {
            var product = _dbContext.Products.FirstOrDefault(p => p.Id == IdProduct);    
            return View(product);

        }
        public IActionResult ProductByType(int CateId)
        {
           
            ViewBag.Category = _dbContext.Categories.FirstOrDefault(c => c.Id == CateId);
            var productByType = _dbContext.Products.Where(x => x.CateId == CateId).OrderBy(x => x.Name).ToList();
            
            return View(productByType);
        }
        public ActionResult Search(String key, int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            if (String.IsNullOrEmpty(key.Trim()))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                HttpContext.Session.SetString("keyword", key);
                var list = _dbContext.Products.Where(m => m.Status == 1);
                list = list.Where(m => m.Name.Contains(key)).OrderByDescending(m => m.CreatedAt);
                ViewBag.Count = list.Count();
                ViewBag.key = key;
                PagedList<Product> lst = new PagedList<Product>(list, pageNumber, pageSize);
                return View(lst);

            }
        }
        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}