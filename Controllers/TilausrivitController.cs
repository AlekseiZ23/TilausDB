using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TilausDB.Models;

namespace TilausDB.Controllers
{
    public class TilausrivitController : Controller
    {
        private TilausDBEntities db = new TilausDBEntities();

        // GET: OrderDetails
        public ActionResult Index()
        {
            var tilausrivit = db.Tilausrivit.Include(o => o.Tuotteet).Include(o => o.Tilaukset);
            return View(tilausrivit.ToList());
        }

        // GET: OrderDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tilausrivit tilausrivit = db.Tilausrivit.Find(id);
            if (tilausrivit == null)
            {
                return HttpNotFound();
            }
            return View(tilausrivit);
        }

        // GET: OrderDetails/Create
        public ActionResult Create()
        {
            ViewBag.TuoteID = new SelectList(db.Tuotteet, "TuoteID", "Nimi");
            ViewBag.TilausID = new SelectList(db.Tilaukset, "TilausID", "AsiakasID");
            return View();
        }

        // POST: OrderDetails/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TilausID,TuoteID,Ahinta,Maara")] Tilausrivit tilausrivit)
        {
            if (ModelState.IsValid)
            {
                db.Tilausrivit.Add(tilausrivit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TilausID = new SelectList(db.Tuotteet, "TuoteID", "Nimi", tilausrivit.TuoteID);
            ViewBag.TuoteID = new SelectList(db.Tilaukset, "TilausID", "AsikasID", tilausrivit.TilausID);
            return View(tilausrivit);
        }

        // GET: OrderDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tilausrivit tilausrivit = db.Tilausrivit.Find(id);
            if (tilausrivit == null)
            {
                return HttpNotFound();
            }

            ViewBag.TilausID = new SelectList(db.Tuotteet, "TuoteID", "Nimi", tilausrivit.TuoteID);
            ViewBag.TuoteID = new SelectList(db.Tilaukset, "TilausID", "AsikasID", tilausrivit.TilausID);
            return View(tilausrivit);
        }

        // POST: OrderDetails/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TilausID,AsiakasID,Ahinta,Maara")] Tilausrivit tilausrivit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tilausrivit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TilausID = new SelectList(db.Tuotteet, "TuoteID", "Nimi", tilausrivit.TuoteID);
            ViewBag.TuoteID = new SelectList(db.Tilaukset, "TilausID", "AsikasID", tilausrivit.TilausID);
            return View(tilausrivit);
        }

        // GET: OrderDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tilausrivit tilausrivit = db.Tilausrivit.Find(id);
            if (tilausrivit == null)
            {
                return HttpNotFound();
            }
            return View(tilausrivit);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tilausrivit tilausrivit = db.Tilausrivit.Find(id);
            db.Tilausrivit.Remove(tilausrivit);
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
