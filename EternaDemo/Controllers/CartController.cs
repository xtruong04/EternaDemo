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
    public class CartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cart
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var order = db.Orders
                .Include("Items.Product")
                .FirstOrDefault(o => o.UserId == userId && o.Status == Order.OrderStatus.Pending);

            if (order == null)
            {
                order = new Order
                {
                    UserId = userId,
                    Status = Order.OrderStatus.Pending,
                    PaymentStatus = Order.PaymentState.Unpaid,
                    CreatedAt = DateTime.UtcNow,
                    Items = new System.Collections.Generic.List<OrderItem>()
                };
                db.Orders.Add(order);
                db.SaveChanges();
            }

            return View(order);
        }

        // POST: Cart/AddToCart
        [HttpPost]
        public ActionResult AddToCart(int productId, int quantity = 1)
        {
            var userId = User.Identity.GetUserId();

            if (string.IsNullOrEmpty(userId))
                return Json(new { success = false, message = "Vui lòng đăng nhập để thêm sản phẩm vào giỏ hàng." });

            var order = db.Orders
                .Include("Items.Product")
                .FirstOrDefault(o => o.UserId == userId && o.Status == Order.OrderStatus.Pending);

            if (order == null)
            {
                order = new Order
                {
                    UserId = userId,
                    Status = Order.OrderStatus.Pending,
                    PaymentStatus = Order.PaymentState.Unpaid,
                    CreatedAt = DateTime.UtcNow,
                    Items = new List<OrderItem>()
                };
                db.Orders.Add(order);
                db.SaveChanges();
            }

            var product = db.Products.Find(productId);
            if (product == null)
                return Json(new { success = false, message = "Sản phẩm không tồn tại." });

            var existingItem = order.Items.FirstOrDefault(i => i.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                existingItem.Subtotal = existingItem.Quantity * existingItem.UnitPrice;
            }
            else
            {
                var newItem = new OrderItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    UnitPrice = product.Price,
                    Subtotal = product.Price * quantity,
                    OrderId = order.Id
                };
                db.OrderItems.Add(newItem);
            }

            db.SaveChanges();

            // Đếm lại tổng số lượng item
            var totalItems = db.OrderItems
                .Where(i => i.OrderId == order.Id)
                .Sum(i => i.Quantity);

            return Json(new { success = true, count = totalItems });
        }

        // POST: Cart/Remove
        [HttpPost]
        public ActionResult Remove(int itemId)
        {
            var item = db.OrderItems.Find(itemId);
            if (item != null)
            {
                db.OrderItems.Remove(item);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // POST: Cart/Checkout
        [HttpPost]
        public ActionResult Checkout()
        {
            var userId = User.Identity.GetUserId();
            var order = db.Orders
                .Include("Items.Product")
                .FirstOrDefault(o => o.UserId == userId && o.Status == Order.OrderStatus.Pending);

            if (order == null) return RedirectToAction("Index");

            order.Status = Order.OrderStatus.Processing;
            order.PaymentStatus = Order.PaymentState.Paid;
            order.PaidAt = DateTime.UtcNow;
            order.UpdatedAt = DateTime.UtcNow;

            order.Subtotal = order.Items.Sum(i => i.Subtotal);
            order.ShippingFee = 0;
            order.Tax = 0;
            order.Total = order.Subtotal;

            db.SaveChanges();
            return RedirectToAction("OrderSuccess");
        }

        public ActionResult OrderSuccess()
        {
            return View();
        }
        [ChildActionOnly]
        public ActionResult CartPartial()
        {
            var userId = User.Identity.IsAuthenticated ? User.Identity.GetUserId() : null;

            if (userId == null)
            {
                ViewBag.CartCount = 0;
                return PartialView("_CartPartial", new List<OrderItem>());
            }

            var order = db.Orders
                .Include("Items.Product")
                .FirstOrDefault(o => o.UserId == userId && o.Status == Order.OrderStatus.Pending);

            var items = order?.Items.ToList() ?? new List<OrderItem>();
            ViewBag.CartCount = items.Sum(i => i.Quantity);

            return PartialView("_CartPartial", items);
        }
        [HttpGet]
        public JsonResult GetCartCount()
        {
            if (!User.Identity.IsAuthenticated)
            {
                // Nếu chưa đăng nhập thì giỏ hàng = 0
                return Json(new { count = 0 }, JsonRequestBehavior.AllowGet);
            }

            var userId = User.Identity.GetUserId();
            var count = db.OrderItems
                .Where(i => i.Order.UserId == userId && i.Order.Status == Order.OrderStatus.Pending)
                .Sum(i => (int?)i.Quantity) ?? 0;

            return Json(new { count = count }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateCart(List<OrderItem> updatedItems)
        {
            var userId = User.Identity.GetUserId();

            if (string.IsNullOrEmpty(userId))
            {
                return Json(new { success = false, message = "Vui lòng đăng nhập." });
            }

            // Lấy đơn hàng đang chờ xử lý của người dùng từ database
            var order = db.Orders
                .Include("Items")
                .FirstOrDefault(o => o.UserId == userId && o.Status == Order.OrderStatus.Pending);

            if (order == null)
            {
                return Json(new { success = false, message = "Không tìm thấy giỏ hàng." });
            }

            try
            {
                // Duyệt qua danh sách các sản phẩm đã cập nhật từ client
                foreach (var updatedItem in updatedItems)
                {
                    // Tìm mục hàng tương ứng trong đơn hàng của người dùng
                    var existingItem = order.Items.FirstOrDefault(i => i.Id == updatedItem.Id);

                    if (existingItem != null)
                    {
                        // Nếu số lượng cập nhật là 0 hoặc nhỏ hơn
                        if (updatedItem.Quantity <= 0)
                        {
                            // Xóa mục hàng khỏi cơ sở dữ liệu
                            db.OrderItems.Remove(existingItem);
                        }
                        else
                        {
                            // Cập nhật số lượng và tính lại tổng phụ
                            existingItem.Quantity = updatedItem.Quantity;
                            existingItem.Subtotal = existingItem.Quantity * existingItem.UnitPrice;
                            db.Entry(existingItem).State = EntityState.Modified;
                        }
                    }
                }

                db.SaveChanges();

                // Đếm lại tổng số lượng item để cập nhật giao diện
                var totalItems = order.Items.Sum(i => (int?)i.Quantity) ?? 0;

                return Json(new { success = true, count = totalItems });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Đã xảy ra lỗi khi cập nhật giỏ hàng: " + ex.Message });
            }
        }
    }
}