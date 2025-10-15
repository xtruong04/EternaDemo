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
    public class CertificateController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Certificate
        public ActionResult Index()
        {
            var certificates = db.Certificates.Include(c => c.Gemstone);
            return View(certificates.ToList());
        }

        // GET: Admin/Certificate/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Certificate certificate = db.Certificates.Find(id);
            if (certificate == null)
            {
                return HttpNotFound();
            }
            return View(certificate);
        }

        // GET: Admin/Certificate/Create
        public ActionResult Create()
        {
            ViewBag.GemstoneId = new SelectList(db.Gemstones, "Id", "Name");
            return View();
        }

        // POST: Admin/Certificate/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Type,Number,IssueDate,FileUrl,GemstoneId")] Certificate certificate)
        {
            if (ModelState.IsValid)
            {
                db.Certificates.Add(certificate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GemstoneId = new SelectList(db.Gemstones, "Id", "Name", certificate.GemstoneId);
            return View(certificate);
        }

        // GET: Admin/Certificate/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Certificate certificate = db.Certificates.Find(id);
            if (certificate == null)
            {
                return HttpNotFound();
            }
            ViewBag.GemstoneId = new SelectList(db.Gemstones, "Id", "Name", certificate.GemstoneId);
            return View(certificate);
        }

        // POST: Admin/Certificate/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Type,Number,IssueDate,FileUrl,GemstoneId")] Certificate certificate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(certificate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GemstoneId = new SelectList(db.Gemstones, "Id", "Name", certificate.GemstoneId);
            return View(certificate);
        }

        // GET: Admin/Certificate/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Certificate certificate = db.Certificates.Find(id);
            if (certificate == null)
            {
                return HttpNotFound();
            }
            return View(certificate);
        }

        // POST: Admin/Certificate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Certificate certificate = db.Certificates.Find(id);
            db.Certificates.Remove(certificate);
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
    }
}
