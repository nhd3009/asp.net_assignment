using BigAssignment.Library;
using BigAssignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using System.Web;
using System.IO;
using JetBrains.Annotations;
using System.Net;
using BigAssignment.Areas.Admin.Models.AuthenAdmin;

namespace BigAssignment.Areas.Admin.Controllers
{
    [Area("admin")]
    [AuthenticationAdmin]
    public class UserController : Controller
    {

        private MovieWebContext db;

        public UserController(MovieWebContext _db)
        {
            db = _db;
        }


        public IActionResult Index()
        {
            return View();
        }



        [Route("admin/userlist")]
        public IActionResult UserList(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var userlst = db.Users.AsNoTracking().OrderBy(x => x.Id).Where(x => x.Status == 1);
            PagedList<User> lst = new PagedList<User>(userlst, pageNumber, pageSize);
            return View(lst);
        }
        [Route("admin/AddUser")]
        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }
        [Route("admin/AddUser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(User user, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                String avatar = XString.ToAscii(user.Fullname);
                user.Password = XString.ToMD5(user.Password);
                user.CreatedAt = DateTime.Now;
                user.CreatedBy = int.Parse(HttpContext.Session.GetString("Admin_Id"));
                user.UpdatedAt = DateTime.Now;
                user.UpdatedBy = int.Parse(HttpContext.Session.GetString("Admin_Id"));

                // Xử lý tệp tin ở đây
                if (Image != null && Image.Length > 0)
                {
                    var fileName = Path.GetFileName(Image.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "template", "images", "user", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await Image.CopyToAsync(fileStream);
                    }
                    user.Image = fileName;
                }



                // Thêm user vào CSDL
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("UserList");
            }

            return View(user);
        }

        [Route("admin/UserDetail")]
        public IActionResult UserDetail(int? userId)
        {
            if(userId == null)
            {
                TempData["Message"] = "There's nothing id here";
                return RedirectToAction("UserList", "User");
            }
            User user = db.Users.Find(userId);
            if(user == null)
            {
                TempData["Message"] = "There's nothing user here";
                return RedirectToAction("UserList", "User");
            }
            return View(user);
        }


        [Route("admin/EditUser")]
        [HttpGet]
        public IActionResult EditUser(int userId)
        {
            if (userId == null)
            {
                TempData["Message"] = "There's nothing id here";
                return RedirectToAction("UserList", "User");
            }
            User user = db.Users.Find(userId);
            if (user == null)
            {
                TempData["Message"] = "There's nothing user here";
                return RedirectToAction("UserList", "User");
            }
            return View(user);
        }

        [Route("admin/EditUser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(User user, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                String avatar = XString.ToAscii(user.Fullname);
                user.Password = XString.ToMD5(user.Password);
                user.CreatedAt = DateTime.Now;
                user.UpdatedAt = DateTime.Now;
                user.CreatedBy = int.Parse(HttpContext.Session.GetString("Admin_Id"));
                user.UpdatedBy = int.Parse(HttpContext.Session.GetString("Admin_Id"));

                // Xử lý tệp tin ở đây
                if (Image != null && Image.Length > 0)
                {
                    var fileName = Path.GetFileName(Image.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "template", "images", "user", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await Image.CopyToAsync(fileStream);
                    }
                    user.Image = fileName;
                }
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UserList");
            }
            return View(user);
        }

        [Route("admin/DeleteUser")]
        [HttpGet]
        public IActionResult DeleteUser(int? userID)
        {
            TempData["Message"] = "";
            db.Remove(db.Users.Find(userID));
            db.SaveChanges();
            TempData["Message"] = "Đã xóa User";
            return RedirectToAction("UserList", "User");
        }


        [Route("admin/userTrashlist")]
        public IActionResult UserTrashList()
        {
            return View(db.Users.Where(m => m.Status == 0).ToList());
        }

        [Route("admin/putUserToTrash")]
        [HttpPost]
        public IActionResult PutUserToTrash(int userID)
        {
            var user = db.Users.FirstOrDefault(p => p.Id == userID);
            if (user != null)
            {
                user.UpdatedAt= DateTime.Now;
                user.Status = user.Status == 0 ? 1 : 0;
                db.SaveChanges();
            }

            return RedirectToAction("UserList");
        }
    }
}
