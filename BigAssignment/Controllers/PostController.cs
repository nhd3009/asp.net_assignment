
//using BigAssignment.Library;
//using BigAssignment.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.CodeAnalysis.CodeStyle;
//using System.Transactions;

//namespace BigAssignment.Controllers
//{
//    public class PostController : Controller
//    {
//        private MovieWebContext  db = new MovieWebContext();

//        public ActionResult Index()
//        {
//            ViewBag.countTrash = db.Posts.Where(m => m.Status == 0 && m.Type == "post").Count();
//            var list = from p in db.Posts
//                       join t in db.Topics
//                       on p.TopicId equals t.Id
//                       where p.Status != 0
//                       orderby p.CreatedAt descending
//                       select new PostTopics()
//                       {
//                           PostId = p.Id,
//                           PostImg = p.Image,
//                           PostName = p.Title,
//                           PostStatus = p.Status,
//                           TopicName = t.Name
//                       };
//            return View(list.ToList());
//        }
//        public ActionResult Trash()
//        {
//            var list = from p in db.Posts
//                       join t in db.Topics
//                       on p.TopicId equals t.Id
//                       where p.Status == 0
//                       orderby p.CreatedAt descending
//                       select new PostTopics()
//                       {
//                           PostId = p.Id,
//                           PostImg = p.Image,
//                           PostName = p.Title,
//                           PostStatus = p.Status,
//                           TopicName = t.Name
//                       };
//            return View(list.ToList());
//        }
//        // Create
//        public ActionResult Create()
//        {
//            Topic Topic = new Topic();
//            ViewBag.ListTopic = new SelectList(db.Topics.ToList(), "ID", "Name", 0);
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create(Post Post)
//        {
//            if (ModelState.IsValid)
//            {
//                String strSlug = XString.ToAscii(Post.Title);
//                Post.Slug = strSlug;
//                Post.Type = "post";
//                Post.Created_at = DateTime.Now;
//                Post.Created_by = int.Parse(Session["Admin_ID"].ToString());
//                Post.Updated_at = DateTime.Now;
//                Post.Updated_by = int.Parse(Session["Admin_ID"].ToString());
//                var file = Request.Files["Image"];
//                if (file != null && file.ContentLength > 0)
//                {
//                    String filename = strSlug + file.FileName.Substring(file.FileName.LastIndexOf("."));
//                    Post.Image = filename;
//                    String Strpath = Path.Combine(Server.MapPath("~/Public/images/post/"), filename);
//                    file.SaveAs(Strpath);
//                }
//                db.Posts.Add(Post);
//                db.SaveChanges();
//                Notification.set_flash("Đã thêm bài viết mới!", "success");
//                return RedirectToAction("Index");
//            }
//            return View(Post);
//        }
//        // Edit
//        public ActionResult Edit(int? id)
//        {
//            Topic Topic = new Topic();
//            ViewBag.ListTopic = new SelectList(db.Topics.ToList(), "ID", "Name", 0);
//            Post Post = db.Posts.Find(id);
//            if (Post == null)
//            {
//                .set_flash("Không tồn tại bài viết!", "warning");
//                return RedirectToAction("Index", "Post");
//            }
//            return View(Post);
//        }

//        [HttpPost, ValidateInput(false)]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit(Post Post)
//        {
//            Topic Topic = new Topic();
//            ViewBag.ListTopic = new SelectList(db.Topics.ToList(), "ID", "Name", 0);
//            if (ModelState.IsValid)
//            {
//                String strSlug = XString.ToAscii(Post.Title);
//                Post.Slug = strSlug;
//                Post.Type = "post";
//                Post.Updated_at = DateTime.Now;
//                Post.Updated_by = int.Parse(Session["Admin_ID"].ToString());
//                var file = Request.Files["Image"];
//                if (file != null && file.ContentLength > 0)
//                {
//                    String filename = strSlug + file.FileName.Substring(file.FileName.LastIndexOf("."));
//                    Post.Image = filename;
//                    String Strpath = Path.Combine(Server.MapPath("~/Public/images/post/"), filename);
//                    file.SaveAs(Strpath);
//                }

//                db.Entry(Post).State = EntityState.Modified;
//                db.SaveChanges();
//                Notification.set_flash("Đã cập nhật lại bài viết!", "success");
//                return RedirectToAction("Index");
//            }
//            return View(Post);
//        }
//        public ActionResult DelTrash(int? id)
//        {
//            Post Post = db.Posts.Find(id);
//            Post.Status = 0;

//            Post.Updated_at = DateTime.Now;
//            Post.Updated_by = int.Parse(Session["Admin_ID"].ToString());
//            db.Entry(Post).State = EntityState.Modified;
//            db.SaveChanges();
//            Notification.set_flash("Đã chuyển vào thùng rác!" + " ID = " + id, "success");
//            return RedirectToAction("Index");
//        }
//        public ActionResult Undo(int? id)
//        {
//            Post Post = db.Posts.Find(id);
//            Post.Status = 2;

//            Post.Updated_at = DateTime.Now;
//            Post.Updated_by = int.Parse(Session["Admin_ID"].ToString());
//            db.Entry(Post).State = EntityState.Modified;
//            db.SaveChanges();
//            Notification.set_flash("Khôi phục thành công!" + " ID = " + id, "success");
//            return RedirectToAction("Trash");
//        }
//        [HttpPost]
//        public JsonResult changeStatus(int id)
//        {
//            Post Post = db.Posts.Find(id);
//            Post.Status = (Post.Status == 1) ? 2 : 1;

//            Post.Updated_at = DateTime.Now;
//            Post.Updated_by = int.Parse(Session["Admin_ID"].ToString());
//            db.Entry(Post).State = EntityState.Modified;
//            db.SaveChanges();
//            return Json(new { Status = Post.Status });
//        }

//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                Notification.set_flash("Không tồn tại bài viết!", "warning");
//                return RedirectToAction("Index", "Post");
//            }
//            Post Post = db.Posts.Find(id);
//            if (Post == null)
//            {
//                Notification.set_flash("Không tồn tại bài viết!", "warning");
//                return RedirectToAction("Index", "Post");
//            }
//            return View(Post);
//        }

//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                Notification.set_flash("Không tồn tại bài viết!", "warning");
//                return RedirectToAction("Index", "Post");
//            }
//            Post Post = db.Posts.Find(id);
//            if (Post == null)
//            {
//                Notification.set_flash("Không tồn tại bài viết!", "warning");
//                return RedirectToAction("Index", "Post");
//            }
//            return View(Post);
//        }

//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            Post Post = db.Posts.Find(id);
//            db.Posts.Remove(Post);
//            db.SaveChanges();
//            Notification.set_flash("Đã xóa vĩnh viễn", "danger");
//            return RedirectToAction("Trash");
//        }
//    }
//}
