using System;
using System.Collections.Generic;
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

            var product = db.Products.Find(productId);
            if (product == null)
                return HttpNotFound();

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
            return RedirectToAction("Index");
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
    }
}