using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EternaDemo.Models;
using Microsoft.AspNet.Identity;

namespace EternaDemo.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: User
        public ActionResult UserProfile()
        {
            var userId = User.Identity.GetUserId();
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var user = db.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        // GET: /User/EditProfile
        public ActionResult EditProfile()
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId); 
            return View(user);
        }
        // POST: /User/EditProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(ApplicationUser model)
        {
            var user = db.Users.Find(model.Id);
            if (user != null)
            {
                user.FullName = model.FullName;
                user.PhoneNumber = model.PhoneNumber;
                user.SelectedAddressId = model.SelectedAddressId;
                db.SaveChanges();
                TempData["Message"] = "Profile updated successfully!";
                return RedirectToAction("UserProfile");
            }
            return View(model);

        }
    }
}
