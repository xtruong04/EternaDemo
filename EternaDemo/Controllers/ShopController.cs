using System;
using System.Collections.Generic;
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

            var order = db.Orders
                .Include("Items.Product.ProductImages")
                .FirstOrDefault(o => o.UserId == userId && o.Status == Order.OrderStatus.Pending);

            var items = order?.Items ?? new List<OrderItem>();

            return View(items); // ✅ truyền model sang view
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