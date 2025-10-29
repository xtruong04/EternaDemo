using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EternaDemo.Models;
using Microsoft.AspNet.Identity;

namespace EternaDemo.Controllers
{
    [Authorize]
    public class WishlistController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Wishlist
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var items = db.WishlistItems
                .Include("Product")
                .Where(w => w.UserId == userId)
                .OrderByDescending(w => w.AddedAt)
                .ToList();

            return View(items);
        }

        // POST: Wishlist/Add
        [HttpPost]
        public ActionResult Add(int productId)
        {
            var userId = User.Identity.GetUserId();

            // Kiểm tra xem đã có trong wishlist chưa
            var existing = db.WishlistItems.FirstOrDefault(w => w.ProductId == productId && w.UserId == userId);
            if (existing == null)
            {
                var wishlistItem = new WishlistItem
                {
                    ProductId = productId,
                    UserId = userId,
                    AddedAt = DateTime.Now
                };
                db.WishlistItems.Add(wishlistItem);
                db.SaveChanges();
            }

            return Json(new { success = true });
        }

        // POST: Wishlist/Remove
        [HttpPost]
        public ActionResult Remove(int id)
        {
            var item = db.WishlistItems.Find(id);
            if (item != null)
            {
                db.WishlistItems.Remove(item);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}