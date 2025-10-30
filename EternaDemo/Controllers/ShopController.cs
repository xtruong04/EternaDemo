using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EternaDemo.Models;
using Microsoft.AspNet.Identity;

namespace EternaDemo.Controllers
{
    public class ShopController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //GET: Shop
        public ActionResult Index(int? cateID)
        {
            try
            {
                List<Product> allProduct = new List<Product>();
                if (cateID != null)
                {
                    allProduct = db.Products.Where(s => s.CateId == cateID).ToList();
                }
                else
                {
                    allProduct = db.Products.ToList();
                }
                return View(allProduct);
            }
            catch
            {
                return Redirect("/not-found");
            }

        }
        public ActionResult Menu()
        {
            try
            {
                List<Category> categories = new List<Category>();
                categories = db.Categories.ToList();
                return PartialView("_Menu", categories);
            }
            catch
            {
                return Redirect("not-found");
            }
        }

        public ActionResult Details(int id)
        {
            try
            {
                Product item = new Product();
                item = db.Products.Find(id);
                return View(item);
            }
            catch
            {
                return Redirect("/not-found");
            }

        }

        public ActionResult RelatedProducts(int proId, int CateId)
        {
            List<Product> item = new List<Product>();
            try
            {
                item = db.Products.Where(s => s.CateId == CateId && s.Id != proId).Take(4).ToList();
                return PartialView(item);
            }
            catch
            {
                item = new List<Product>();
                return PartialView(item);
            }
        }
        public ActionResult ShoppingCart()
        {
            var userId = User.Identity.GetUserId();

            if (string.IsNullOrEmpty(userId))
                return RedirectToAction("Login", "Account");

            // Lấy user và SelectedAddress (include luôn vì nó là navigation property)
            var user = db.Users
                .Include("SelectedAddress")
                .FirstOrDefault(u => u.Id == userId);

            // Lấy giỏ hàng của user
            var order = db.Orders
                .Include("Items.Product.ProductImages")
                .FirstOrDefault(o => o.UserId == userId && o.Status == Order.OrderStatus.Pending);

            var items = order?.Items ?? new List<OrderItem>();

            // ✅ Truyền SelectedAddress qua ViewBag
            ViewBag.SelectedAddress = user?.SelectedAddress;
            // ✅ Tính subtotal (tổng các sản phẩm)
            decimal subtotal = items.Sum(i => i.Quantity * i.UnitPrice);

            // ✅ Lấy phí vận chuyển từ DB (nếu có)
            decimal shippingFee = order?.ShippingFee ?? 0;

            // ✅ Tính total
            decimal total = subtotal + shippingFee;

            // ✅ Truyền dữ liệu sang View
            ViewBag.Subtotal = subtotal;
            ViewBag.ShippingFee = shippingFee;
            ViewBag.Total = total;

            return View(items);
        }

        [HttpPost]
        public ActionResult UpdateCart(List<CartUpdateModel> items)
        {
            var userId = User.Identity.GetUserId();
            if (string.IsNullOrEmpty(userId))
                return Json(new { success = false, message = "Bạn cần đăng nhập để cập nhật giỏ hàng." });

            var order = db.Orders
                .Include("Items.Product")
                .FirstOrDefault(o => o.UserId == userId && o.Status == Order.OrderStatus.Pending);

            if (order == null)
                return Json(new { success = false, message = "Không tìm thấy giỏ hàng." });

            foreach (var item in items)
            {
                var existingItem = order.Items.FirstOrDefault(i => i.Id == item.ItemId);
                if (existingItem != null)
                {
                    existingItem.Quantity = item.Quantity;
                    existingItem.Subtotal = existingItem.Quantity * existingItem.UnitPrice;
                }
            }

            db.SaveChanges();

            var total = order.Items.Sum(i => i.Subtotal);
            return Json(new { success = true, total = total.ToString("#,##0") + " VND" });
        }
        public class CartUpdateModel
        {
            public int ItemId { get; set; }
            public int Quantity { get; set; }
        }
    }
}