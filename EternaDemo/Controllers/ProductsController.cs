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
        public ActionResult QuickView(int id)
        {
            try
            {
                var product = db.Products
                    .Include(p => p.ProductImages)
                    .FirstOrDefault(p => p.Id == id);

                if (product == null)
                    return HttpNotFound();

                return PartialView("_QuickView", product);
            }
            catch (Exception ex)
            {
                return Content("Error: " + ex.Message);
            }
        }
        public ActionResult GetDetail(int id)
        {
            Product item = new Product();
            item = db.Products.Find(id);
            return PartialView(item);
        }
    }
}