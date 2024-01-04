using BigAssignment.Areas.Admin.Models.AuthenAdmin;
using BigAssignment.Library;
using BigAssignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace BigAssignment.Areas.Admin.Controllers
{
    [Area("admin")]
    [AuthenticationAdmin]
    public class CategoryController : Controller
    {
        private MovieWebContext db;

        public CategoryController(MovieWebContext _db)
        {
            db = _db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("admin/categoryList")]
        public IActionResult CategoryList(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var categories = db.Categories.AsNoTracking().OrderBy(x => x.Name).Where(x => x.Status == 1);
            PagedList<Category> lst = new PagedList<Category>(categories, pageNumber, pageSize);
            return View(lst);
        }

        [Route("admin/categoryDetail")]
        public IActionResult CategoryDetail(int? categoryId)
        {
            if (categoryId == null)
            {
                TempData["Message"] = "Category ID is null";
                return RedirectToAction("CategoryList");
            }
            Category category = db.Categories.Find(categoryId);
            if(category == null){
                TempData["Message"] = "Don't have data for this category";
                return RedirectToAction("CategoryDetail");
            }

            return View(category);

        }

        [Route("admin/addCategory")]
        [HttpGet]
        public IActionResult AddCategory()
        {
            ViewBag.listCat = new SelectList(db.Categories.Where(x => x.Status == 1), "Id", "Name", 0);
            ViewBag.listOrder = new SelectList(db.Categories.Where(x => x.Status == 1), "Orders", "Name", 0);
            return View();
        }
        [Route("admin/addCategory")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                ViewBag.listCat = new SelectList(db.Categories.Where(x => x.Status == 1), "Id", "Name", 0);
                ViewBag.listOrder = new SelectList(db.Categories.Where(x => x.Status == 1), "Orders", "Name", 0);

                if (ModelState.IsValid)
                {
                    if(category.ParentId == null)
                    {
                        category.ParentId = 0;
                    }
                    String slug = XString.ToAscii(category.Name);
                    CheckSlug check = new CheckSlug();

                    if(!check.KiemTraSlug("Category", slug, null))
                    {
                        TempData["Message"] = "Category is exist, please try again";
                        return RedirectToAction("AddCategory", "Category");
                    }

                    category.Slug = slug;
                    category.CreatedAt = DateTime.Now;
                    category.CreatedBy = int.Parse(HttpContext.Session.GetString("Admin_Id"));
                    category.UpdatedAt = DateTime.Now;
                    category.UpdatedBy = int.Parse(HttpContext.Session.GetString("Admin_Id"));

                    db.Categories.Add(category);
                    db.SaveChanges();
                    TempData["Message"] = "Add Category Done!";
                    return RedirectToAction("CategoryList", "Category");
                }
            }
            TempData["Message"] = "Error when adding category";
            return View(category);
        }

        [Route("admin/editCategory")]
        [HttpGet]
        public IActionResult EditCategory(int categoryId)
        {
            ViewBag.listCat = new SelectList(db.Categories.Where(x => x.Status == 1), "Id", "Name", 0);
            ViewBag.listOrder = new SelectList(db.Categories.Where(x => x.Status == 1), "Orders", "Name", 0);

            Category category = db.Categories.Find(categoryId);
            if(category == null)
            {
                TempData["Message"] = "404! Warning";
                return RedirectToAction("CategoryList", "Category");
            }
            return View(category);
        }

        [Route("admin/editCategory")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCategory(Category category)
        {
            ViewBag.listCat = new SelectList(db.Categories.Where(x => x.Status == 1), "Id", "Name", 0);
            ViewBag.listOrder = new SelectList(db.Categories.Where(x => x.Status == 1), "Orders", "Name", 0);


            if (ModelState.IsValid)
            {
                String Slug = XString.ToAscii(category.Name);
                int Id = category.Id;
                if(db.Categories.Where(x => x.Slug == Slug && x.Id != Id).Count() > 0)
                {
                    TempData["Message"] = "Category has existed";
                    return RedirectToAction("EditCategory", "Category");
                }
                if(db.Topics.Where(x => x.Slug == Slug && x.Id != Id).Count() >0)
                {
                    TempData["Message"] = "Category has existed";
                    return RedirectToAction("EditCategory", "Category");
                }
                if(db.Posts.Where(x => x.Slug == Slug && x.Id != Id ).Count() > 0)
                {
                    TempData["Message"] = "Category has existed";
                    return RedirectToAction("EditCategory", "Category");
                }
                if(db.Products.Where(x => x.Slug == Slug && x.Id != Id).Count() > 0)
                {
                    TempData["Message"] = "Category has existed";
                    return RedirectToAction("EditCategory", "Category");
                }
                category.Slug = Slug;

                category.UpdatedAt = DateTime.Now;
                category.UpdatedBy = int.Parse(HttpContext.Session.GetString("Admin_Id"));

                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Update Success";
                return RedirectToAction("CategoryList", "Category");

            }
            return View(category);
        }

        [Route("admin/putCategoryToTrash")]
        [HttpPost]
        public IActionResult PutCategoryToTrash(int categoryId)
        {
            Category category = db.Categories.Find(categoryId);

            if(category == null)
            {
                TempData["Message"] = "Category doesn't exist";
                return RedirectToAction("CategoryList", "Category");
            }
            int count_child = db.Categories.Where(x => x.ParentId == categoryId).Count();

            if(count_child != 0)
            {
                TempData["Message"] = "Cannot Delete because this category is parent";
                return RedirectToAction("CategoryList", "Category");
            }

            category.Status = category.Status == 0 ? 1 : 0;

            category.CreatedAt = DateTime.Now;

            category.UpdatedAt = DateTime.Now;

            category.UpdatedBy = int.Parse(HttpContext.Session.GetString("Admin_Id"));

            category.CreatedBy = int.Parse(HttpContext.Session.GetString("Admin_Id"));

            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("CategoryList");
        }

        //[Route("admin/getCategoryOutOfTrash")]
        //[HttpPost]
        //public IActionResult GetCategoryOutOfTrash(int? categoryId)
        //{
        //    Category category = db.Categories.Find(categoryId);
        //    if (category == null)
        //    {
        //        TempData["Message"] = "Category doesn't existed";
        //        return RedirectToAction("CategoryTrashList", "Category");
        //    }
        //    category.Status = 1;

        //    category.UpdatedAt = DateTime.Now;
        //    category.UpdatedBy = 1;

        //    db.Entry(category).State = EntityState.Modified;
        //    return RedirectToAction("CategoryTrashList", "Category");
        //}



        [Route("admin/categoryTrashlist")]
        public IActionResult CategoryTrashList(int? page)
        {
            var lst = db.Categories.Where(x => x.Status == 0).ToList();
            return View(lst);
        }


        [Route("admin/deleteCategory")]
        [HttpGet]
        public IActionResult DeleteCategory(int categoryId)
        {
            TempData["Message"] = "";
            db.Remove(db.Categories.Find(categoryId));
            db.SaveChanges();
            TempData["Message"] = "Đã xóa sản phẩm";
            return RedirectToAction("CategoryList", "Category");
        }
    }
}
