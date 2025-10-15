using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EternaDemo.Models;

namespace EternaDemo.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            // Đếm dữ liệu từ database
            ViewBag.TotalProducts = db.Products.Count();
            ViewBag.TotalCategories = db.Categories.Count();
            ViewBag.TotalUsers = db.Users.Count();
            ViewBag.TotalOrders = db.Orders.Count();
            ViewBag.TotalGemstone =db.Gemstones.Count();

            // Ví dụ biểu đồ doanh thu (6 tháng gần nhất)
            var revenueData = db.Orders
                .Where(o => o.CreatedAt != null)
                .GroupBy(o => new { o.CreatedAt.Month })
                .Select(g => new
                {
                    Month = g.Key.Month,
                    Total = g.Sum(x => (decimal?)x.Total) ?? 0
                })
                .OrderBy(g => g.Month)
                .ToList();

            ViewBag.RevenueLabels = revenueData.Select(x => "Th" + x.Month).ToArray();
            ViewBag.RevenueValues = revenueData.Select(x => x.Total).ToArray();

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}