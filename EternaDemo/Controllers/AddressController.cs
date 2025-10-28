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
    public class AddressController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Address/Addresses
        public ActionResult Addresses()
        {
            var userId = User.Identity.GetUserId();
            var addresses = db.Addresses.Where(a => a.UserId == userId).ToList();
            return View(addresses);
        }

        // GET: /Address/AddAddress
        [HttpGet]
        public ActionResult AddAddress()
        {
            return View();
        }

        // POST: /Address/AddAddress
        [HttpPost]
        public ActionResult AddAddress(Address address)
        {
            var userId = User.Identity.GetUserId();
            address.UserId = userId;

            db.Addresses.Add(address);
            db.SaveChanges();

            TempData["Message"] = "Address added successfully!";
            return RedirectToAction("EditProfile", "User");
        }
        // GET: /Address/EditAddress/{id}
        public ActionResult EditAddress(int id)
        {
            var address = db.Addresses.Find(id);
            if (address == null)
                return HttpNotFound();

            return View(address);
        }

        // POST: /Address/EditAddress
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAddress(Address model)
        {
            if (ModelState.IsValid)
            {
                var address = db.Addresses.Find(model.Id);
                if (address != null)
                {
                    address.Street = model.Street;
                    address.City = model.City;
                    address.Districts = model.Districts;
                    address.ZipCode = model.ZipCode;
                    address.Country = model.Country;

                    db.SaveChanges();
                    TempData["Message"] = "Address updated successfully!";
                    return RedirectToAction("EditProfile", "User");
                }
            }

            return View(model);
        }
        public ActionResult SelectAddress()
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Include("Addresses").FirstOrDefault(u => u.Id == userId);

            if (user == null)
                return RedirectToAction("Login", "Account");

            ViewBag.OrderId = TempData["OrderId"];
            return View(user.Addresses.ToList());
        }

        [HttpPost]
        public ActionResult SelectAddress(int addressId, int orderId)
        {
            var order = db.Orders.Find(orderId);
            var address = db.Addresses.Find(addressId);

            if (order == null || address == null)
                return RedirectToAction("Index", "Cart");

            //order.ShippingAddress = $"{address.Street}, {address.City}";
            order.Status = Order.OrderStatus.Processing;
            order.PaymentStatus = Order.PaymentState.Paid;
            order.PaidAt = DateTime.UtcNow;
            order.Subtotal = order.Items.Sum(i => i.Subtotal);
            order.Total = order.Subtotal;
            db.SaveChanges();

            return RedirectToAction("OrderSuccess", "Cart");
        }
    }
}