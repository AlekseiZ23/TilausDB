using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TilausDB.Models;

namespace TilausDB.Controllers
{
    public class PostitoimiPaikatController : Controller
    {
        // GET: postitoimipaikat
        TilausDBEntities db = new TilausDBEntities();
        public ActionResult Index()
        {
            var postitoimipaikat = db.Postitoimipaikat.Include(s => s.Asiakkaat).ToList();
            return View(postitoimipaikat);
        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Postitoimipaikat postitoimipaikat = db.Postitoimipaikat.Find(id);
            if (postitoimipaikat == null)
            {
                return HttpNotFound();
            }
            return View(postitoimipaikat);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Postitoimipaikat postitoimipaikat = db.Postitoimipaikat.Find(id);
            if (postitoimipaikat == null) return HttpNotFound();
            ViewBag.Postinumero = new SelectList(db.Postitoimipaikat, "Postinumero", "Postitoimipaikka", postitoimipaikat.Postinumero);
            return View(postitoimipaikat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Postinumero, Postitoimipaikka")] Postitoimipaikat postitoimipaikat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(postitoimipaikat).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.RegionID = new SelectList(db.Postitoimipaikat, "Postinumero", "Postitoimipaikka", postitoimipaikat.Postinumero);
                return RedirectToAction("Index");
            }
            return View(postitoimipaikat);
        }

        public ActionResult Create()
        {
            ViewBag.RegionID = new SelectList(db.Postitoimipaikat, "Postinumero", "Postitoimipaikka");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Postinumero, Postitoimipaikka")] Postitoimipaikat postitoimipaikat)
        {
            if (ModelState.IsValid)
            {
                db.Postitoimipaikat.Add(postitoimipaikat);
                db.SaveChanges();
                ViewBag.Postitoimipaikat = new SelectList(db.Postitoimipaikat, "Postinumero","Postitoimipaikka");
                return RedirectToAction("Index");
            }
            return View(postitoimipaikat);
        }

        public ActionResult Delete(string id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Postitoimipaikat postitoimipaikat = db.Postitoimipaikat.Find(id);
            if (postitoimipaikat == null) return HttpNotFound();
            return View(postitoimipaikat);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Postitoimipaikat postitoimipaikat = db.Postitoimipaikat.Find(id);
            db.Postitoimipaikat.Remove(postitoimipaikat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}