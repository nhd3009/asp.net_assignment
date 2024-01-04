using BigAssignment.Areas.Admin.Models.AuthenAdmin;
using BigAssignment.Library;
using BigAssignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;

namespace BigAssignment.Areas.Admin.Controllers
{
    [Area("admin")]
    [AuthenticationAdmin]
    public class OrderController : Controller
    {
        private MovieWebContext db;

        public OrderController(MovieWebContext _db)
        {
            db = _db;
        }


        public IActionResult Index()
        {
            return View();
        }

        [Route("admin/OrderList")]
        public IActionResult OrderList()
        {
            ViewBag.countTrash = db.Orders.Where(m => m.Trash == 1).Count();
            var results = (from od in db.OrderDetails
                           join o in db.Orders on od.OrderId equals o.Id
                           where o.Trash != 1
                           group od by new { od.OrderId, o.DeliveryName, o.DeliveryEmail, o.Status, o.CreateDate, o.ExportDate } into groupb
                           orderby groupb.Key.CreateDate descending
                           select new ListOrder
                           {
                               Id = groupb.Key.OrderId,
                               SAmount = groupb.Sum(m => m.Amount),
                               CustomerName = groupb.Key.DeliveryName,
                               Status = groupb.Key.Status,
                               CreateDate = groupb.Key.CreateDate,
                               ExportDate = groupb.Key.ExportDate
                           }).ToList();


            return View(results);
        }

        [Route("admin/OrderTrashList")]
        public IActionResult OrderTrashList()
        {
            ViewBag.countTrash = db.Orders.Where(m => m.Trash == 1).Count();
            var results = (from od in db.OrderDetails
                           join o in db.Orders on od.OrderId equals o.Id
                           where o.Trash == 1
                           group od by new { od.OrderId, o.DeliveryName, o.Status, o.CreateDate, o.ExportDate } into groupb
                           orderby groupb.Key.CreateDate descending
                           select new ListOrder
                           {
                               Id = groupb.Key.OrderId,
                               SAmount = groupb.Sum(m => m.Amount),
                               CustomerName = groupb.Key.DeliveryName,
                               Status = groupb.Key.Status,
                               CreateDate = groupb.Key.CreateDate,
                               ExportDate = groupb.Key.ExportDate
                           }).ToList();


            return View(results);
        }

        [Route("admin/DetailOrder")]
        [HttpGet]
        public IActionResult OrderDetail(int orderId)
        {
            if (orderId == null)
            {
                TempData["Message"] = "Order doesn't exist";
                return RedirectToAction("OrderList");
            }
            
            Order order = db.Orders.Find(orderId);

            if (order == null)
            {
                TempData["Message"] = "Order doesn't exist";
                return RedirectToAction("OrderList");
            }

            ViewBag.orderDetails = db.OrderDetails.Where(x => x.OrderId == orderId).ToList();

            ViewBag.productOrder = db.Products.ToList();
            return View(order);

        }

        [Route("admin/UpdateOrder")]
        [HttpGet]
        public IActionResult UpdateOrder(int orderId)
        {
            if (orderId== null)
            {
                TempData["Message"] = "There's nothing id here";
                return RedirectToAction("OrderList", "Order");
            }
            Order order = db.Orders.Find(orderId);
            if (order == null)
            {
                TempData["Message"] = "There's nothing user here";
                return RedirectToAction("OrderList", "Order");
            }
            return View(order);
        }

        [Route("admin/UpdateOrder")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateOrder(int id, int Status)
        {
            var order = db.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                TempData["Message"] = "There's no order with this ID";
                return RedirectToAction("OrderList");
            }
            order.Status = Status;
            order.UpdatedBy = int.Parse(HttpContext.Session.GetString("Admin_Id"));
            order.UpdatedAt = DateTime.Now;
            db.SaveChanges();
            TempData["Message"] = "Order status updated successfully!";
            return RedirectToAction("OrderList");
        }

        [Route("admin/cancelOrder")]
        [HttpPost]
        public IActionResult CancelOrder(int orderId)
        {
            var order = db.Orders.FirstOrDefault(p => p.Id == orderId);
            if (order != null)
            {
                order.UpdatedAt = DateTime.Now;
                order.UpdatedBy = int.Parse(HttpContext.Session.GetString("Admin_Id"));
                order.Trash = 1;
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("OrderList");
        }

        [Route("admin/reverseOrder")]
        [HttpPost]
        public IActionResult ReverseOrder(int orderId)
        {
            var order = db.Orders.FirstOrDefault(p => p.Id == orderId);
            if (order != null)
            {
                order.UpdatedAt = DateTime.Now;
                order.UpdatedBy = int.Parse(HttpContext.Session.GetString("Admin_Id"));
                db.Entry(order).State = EntityState.Modified;
                order.Trash = 0;
                db.SaveChanges();
            }

            return RedirectToAction("OrderTrashList");
        }

        [Route("admin/DeleteOrder")]
        [HttpGet]
        public IActionResult DeleteOrder(int orderId)
        {
            TempData["Message"] = "";
            var o = db.Orders.Where(x => x.Id == orderId).ToList();
            var od = db.OrderDetails.Where(x => x.OrderId == orderId).ToList();
            if(od.Any())
            {
                db.RemoveRange(od);
            }
            db.Remove(db.Orders.Find(orderId));
            db.SaveChanges();
            TempData["Message"] = "Đã xóa Đơn hàng";
            return RedirectToAction("OrderList", "Order");
        }

        [HttpPost]
        public JsonResult changeStatus(int id, int op)
        {
            Order order = db.Orders.Find(id);
            if (op == 1) { order.Status = 1; } else if (op == 2) { order.Status = 2; } else { order.Status = 3; }

            order.ExportDate = DateTime.Now;
            order.UpdatedAt = DateTime.Now;
            order.UpdatedBy = 1;
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new { s = order.Status, t = order.ExportDate.ToString() });
        }
    }
}
