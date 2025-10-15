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
    public class GemstoneController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Gemstone
        public ActionResult Index()
        {
            return View(db.Gemstones.ToList());
        }

        // GET: Admin/Gemstone/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gemstone gemstone = db.Gemstones.Find(id);
            if (gemstone == null)
            {
                return HttpNotFound();
            }
            return View(gemstone);
        }

        // GET: Admin/Gemstone/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Gemstone/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Cut,Clarity,Color,Carat,Origin,Treatment")] Gemstone gemstone)
        {
            if (ModelState.IsValid)
            {
                db.Gemstones.Add(gemstone);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gemstone);
        }

        // GET: Admin/Gemstone/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gemstone gemstone = db.Gemstones.Find(id);
            if (gemstone == null)
            {
                return HttpNotFound();
            }
            return View(gemstone);
        }

        // POST: Admin/Gemstone/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Cut,Clarity,Color,Carat,Origin,Treatment")] Gemstone gemstone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gemstone).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gemstone);
        }

        // GET: Admin/Gemstone/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gemstone gemstone = db.Gemstones.Find(id);
            if (gemstone == null)
            {
                return HttpNotFound();
            }
            return View(gemstone);
        }

        // POST: Admin/Gemstone/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gemstone gemstone = db.Gemstones.Find(id);
            db.Gemstones.Remove(gemstone);
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
