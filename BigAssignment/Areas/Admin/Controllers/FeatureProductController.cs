using BigAssignment.Areas.Admin.Models.AuthenAdmin;
using BigAssignment.Library;
using BigAssignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace BigAssignment.Areas.Admin.Controllers
{
    [Area("admin")]
    [AuthenticationAdmin]
    public class FeatureProductController : Controller
    {
        private MovieWebContext db;

        public FeatureProductController(MovieWebContext _db)
        {
            db = _db;
        }


        public IActionResult Index()
        {
            return View();
        }

        [Route("admin/productlist")]
        public IActionResult ProductList(int? page)
        {
            ViewBag.countTrash = db.Products.Where(m => m.Status == 0).Count();
            var list = from p in db.Products
                       join c in db.Categories
                       on p.CateId equals c.Id
                       where p.Status != 0
                       where p.CateId== c.Id
                       orderby p.CreatedAt descending
                       select new ProductCategory()
                       {
                           ProductId = p.Id,
                           ProductImg = p.Image,
                           ProductName = p.Name,
                           ProductStatus = p.Status,
                           ProductDiscount = p.Discount,
                           CategoryName = c.Name
                       };
            return View(list.ToList());
        }


        [Route("admin/AddProduct")]
        [HttpGet]
        public IActionResult AddProduct()
        {
            Category category = new Category();
            ViewBag.ListCat = new SelectList(db.Categories.Where(x => x.Status != 0), "Id", "Name", 0);
            return View();
        }

        [Route("admin/AddProduct")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(Product product, IFormFile Image)
        {
            ViewBag.ListCat = new SelectList(db.Categories.Where(x => x.Status != 0), "Id", "Name", 0);

            if(ModelState.IsValid)
            {
                product.Price = product.Price + 500000;
                product.ProPrice = product.ProPrice + 500000;

                String strSlug = XString.ToAscii(product.Name);
                product.Slug = strSlug;
                product.CreatedAt = DateTime.Now;
                product.UpdatedAt = DateTime.Now;
                product.CreatedBy = int.Parse(HttpContext.Session.GetString("Admin_Id"));
                product.CreatedBy = int.Parse(HttpContext.Session.GetString("Admin_Id"));

                if (Image != null && Image.Length > 0)
                {
                    var fileName = Path.GetFileName(Image.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "template", "images", "products", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await Image.CopyToAsync(fileStream);
                    }
                    product.Image = fileName;
                }

                db.Products.Add(product);
                db.SaveChanges();
                TempData["Message"] = "Adding Done";

                return RedirectToAction("ProductList");

            }

            return View(product);
        }

        [Route("admin/EditProduct")]
        [HttpGet]
        public IActionResult EditProduct(int productId)
        {
            ViewBag.ListCat = new SelectList(db.Categories.ToList(), "Id", "Name", 0);

            Product product = db.Products.Find(productId);

            if(product == null)
            {
                TempData["Message"] = "Null";
            }
            return View(product);
        }

        [Route("admin/putProductToTrash")]
        [HttpPost]
        public IActionResult PutProductToTrash(int productId)
        {
            var product = db.Products.FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                product.UpdatedAt = DateTime.Now;
                product.UpdatedBy = int.Parse(HttpContext.Session.GetString("Admin_Id"));
                product.Status = product.Status == 0 ? 1 : 0;
                db.SaveChanges();
            }

            return RedirectToAction("ProductList");
        }


        [Route("admin/EditProduct")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(Product product, IFormFile Image)
        {
            ViewBag.ListCat = new SelectList(db.Categories.ToList(), "Id", "Name", 0);

            if (ModelState.IsValid)
            {
                String strSlug = XString.ToAscii(product.Name);
                product.Slug = strSlug;

                product.UpdatedAt = DateTime.Now;
                product.UpdatedBy = int.Parse(HttpContext.Session.GetString("Admin_Id"));

                if (Image != null && Image.Length > 0)
                {
                    var fileName = Path.GetFileName(Image.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "template", "images", "products", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await Image.CopyToAsync(fileStream);
                    }
                    product.Image = fileName;
                }

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Done";
                return RedirectToAction("ProductList");

            }

            return View(product);
        }

        [Route("admin/DetailProduct")]
        [HttpGet]
        public IActionResult ProductDetail(int productId)
        {
            if(productId == null)
            {
                TempData["Message"] = "product doesn't exist";
                return RedirectToAction("ProductList");
            }
            Product product = db.Products.Find(productId);

            if(product == null)
            {
                TempData["Message"] = "Product doesn't exist";
                return RedirectToAction("ProductList");
            }
            return View(product);

        }

        [Route("admin/DeleteProduct")]
        [HttpGet]
        public IActionResult DeleteProduct(int productID)
        {
            TempData["Message"] = "";
            db.Remove(db.Products.Find(productID));
            db.SaveChanges();
            TempData["Message"] = "Đã xóa sản phẩm";
            return RedirectToAction("ProductList", "FeatureProduct");
        }

        [Route("admin/productTrashlist")]
        public IActionResult ProductTrashList(int? page)
        {
            var list = from p in db.Products
                       join c in db.Categories
                       on p.CateId equals c.Id
                       where p.Status == 0
                       where p.CateId == c.Id
                       orderby p.CreatedAt descending
                       select new ProductCategory()
                       {
                           ProductId = p.Id,
                           ProductImg = p.Image,
                           ProductName = p.Name,
                           ProductStatus = p.Status,
                           ProductDiscount = p.Discount,
                           CategoryName = c.Name
                       };
            return View(list.ToList());
        }


    }
}
