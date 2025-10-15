using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EternaDemo.Models;

namespace EternaDemo.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.Certificate).Include(p => p.Gemstone).Include(p => p.Metal);
            return View(products.ToList());
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.CateId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.CertificateId = new SelectList(db.Certificates, "Id", "Type");
            ViewBag.GemstoneId = new SelectList(db.Gemstones, "Id", "Name");
            ViewBag.MetalId = new SelectList(db.Metals, "Id", "Name");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Alias,Description,IsFeatured,Price,MetalId,CateId,GemstoneId,CertificateId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CateId = new SelectList(db.Categories, "Id", "Name", product.CateId);
            ViewBag.CertificateId = new SelectList(db.Certificates, "Id", "Type", product.CertificateId);
            ViewBag.GemstoneId = new SelectList(db.Gemstones, "Id", "Name", product.GemstoneId);
            ViewBag.MetalId = new SelectList(db.Metals, "Id", "Name", product.MetalId);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CateId = new SelectList(db.Categories, "Id", "Name", product.CateId);
            ViewBag.CertificateId = new SelectList(db.Certificates, "Id", "Type", product.CertificateId);
            ViewBag.GemstoneId = new SelectList(db.Gemstones, "Id", "Name", product.GemstoneId);
            ViewBag.MetalId = new SelectList(db.Metals, "Id", "Name", product.MetalId);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Alias,Description,IsFeatured,Price,MetalId,CateId,GemstoneId,CertificateId")] Product product)
        {
            System.Diagnostics.Debug.WriteLine("Alias nhận được: " + product.Alias);
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CateId = new SelectList(db.Categories, "Id", "Name", product.CateId);
            ViewBag.CertificateId = new SelectList(db.Certificates, "Id", "Type", product.CertificateId);
            ViewBag.GemstoneId = new SelectList(db.Gemstones, "Id", "Name", product.GemstoneId);
            ViewBag.MetalId = new SelectList(db.Metals, "Id", "Name", product.MetalId);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [Route("Admin/Products/GetCertificateByGemstone")]
        public JsonResult GetCertificateByGemstone(int gemstoneId)
        {
            var certificate = db.Certificates
                .FirstOrDefault(c => c.GemstoneId == gemstoneId);

            if (certificate != null)
            {
                return Json(new
                {
                    id = certificate.Id,
                    name = certificate.Type
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}
