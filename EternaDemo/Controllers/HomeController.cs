using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EternaDemo.Models;
using Microsoft.AspNet.Identity;

namespace EternaDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext db = new AppDbContext();
        public ActionResult Index()
        {
            try
            {
                List<Category> categories = new List<Category>();
                categories = db.Categories.Take(2)
                   .ToList();

                List<Product> allproducts = new List<Product>();
                allproducts = db.Products
                    .OrderByDescending(s => s.Id)
                    .Take(4)
                    .ToList();


                List<Product> featuredproducts = new List<Product>();
                featuredproducts = db.Products
                    .Where(p => p.IsFeatured == true)
                    .Take(4)
                    .OrderByDescending(s => s.Id).ToList();

                List<Product> newarrival = db.Products
                    .OrderByDescending(p => p.Id)
                    .Take(4)
                    .ToList();

                HomeViewModel item = new HomeViewModel();
                item.Categories = categories;
                item.AllProducts = allproducts;
                item.FeaturedProducts = featuredproducts;
                item.NewArrival = newarrival;
                return View(item);
            }
            catch
            {
                return Redirect("/not-found");
            }
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Blog()
        {
            return View();
        }
    }
}