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
    public class MetalController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Metal
        public ActionResult Index()
        {
            return View(db.Metals.ToList());
        }

        // GET: Admin/Metal/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Metal metal = db.Metals.Find(id);
            if (metal == null)
            {
                return HttpNotFound();
            }
            return View(metal);
        }

        // GET: Admin/Metal/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Metal/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Purity")] Metal metal)
        {
            if (ModelState.IsValid)
            {
                db.Metals.Add(metal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(metal);
        }

        // GET: Admin/Metal/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Metal metal = db.Metals.Find(id);
            if (metal == null)
            {
                return HttpNotFound();
            }
            return View(metal);
        }

        // POST: Admin/Metal/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Purity")] Metal metal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(metal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(metal);
        }

        // GET: Admin/Metal/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Metal metal = db.Metals.Find(id);
            if (metal == null)
            {
                return HttpNotFound();
            }
            return View(metal);
        }

        // POST: Admin/Metal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Metal metal = db.Metals.Find(id);
            db.Metals.Remove(metal);
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
