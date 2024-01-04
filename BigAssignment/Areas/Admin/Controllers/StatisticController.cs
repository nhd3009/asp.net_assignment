using BigAssignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Linq;
using BigAssignment.Areas.Admin.Models.AuthenAdmin;

namespace BigAssignment.Areas.Admin.Controllers
{
    [Area("admin")]
    [AuthenticationAdmin]
    public class StatisticController : Controller
    {
        private MovieWebContext db;

        public StatisticController(MovieWebContext _db)
        {
            db = _db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("admin/Statistic")]
        public IActionResult Statistics()
        {
            var monthlyRevenue = (from order in db.Orders
                                  join orderDetail in db.OrderDetails on order.Id equals orderDetail.OrderId
                                  where order.ExportDate != null
                                  group orderDetail by new { Month = order.ExportDate.Value.Month, Year = order.ExportDate.Value.Year } into g
                                  select new { MonthYear = g.Key, TotalRevenue = g.Sum(od => od.Amount) })
                     .ToList();

            ViewBag.MonthlyRevenue = monthlyRevenue;


            var query = from od in db.OrderDetails
                        join p in db.Products on od.ProductId equals p.Id
                        group od by p.Name into g
                        select new { ProductName = g.Key, TotalQuantity = g.Sum(x => x.Quantity) };
            ViewBag.OrderDetails = query.ToList();
            return View();
        }
    }
}
