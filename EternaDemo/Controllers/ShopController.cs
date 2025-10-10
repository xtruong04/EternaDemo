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
        AppDbContext db = new AppDbContext();
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
            return View();
        }
    }
}