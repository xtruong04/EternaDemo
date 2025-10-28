using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EternaDemo.Models;
using System.Data.Entity;


namespace EternaDemo.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Products
        public ActionResult GetDetail(int id)
        {
            Product item = new Product();
            item = db.Products.Find(id);
            return PartialView(item);
        }
    }
}