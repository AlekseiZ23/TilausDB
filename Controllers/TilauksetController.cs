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
    public class TilauksetController : Controller
    {
        private TilausDBEntities db = new TilausDBEntities();

        // GET: tilaukset
        public ActionResult Index()
        {
            var tilaukset = db.Tilaukset.Include(o => o.Asiakkaat);
            return View(tilaukset.ToList());
        }

        // GET: tilaukset/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tilaukset tilaukset = db.Tilaukset.Find(id);
            if (tilaukset == null)
            {
                return HttpNotFound();
            }
            return View(tilaukset);
        }

        // GET: tilaukset/Create
        public ActionResult Create()
        {
            ViewBag.AsiakasID = new SelectList(db.Asiakkaat, "AsiakasID", "Nimi");
            ViewBag.Henkilo_id = new SelectList(db.Henkilot, "Henkilo_id", "Sukunimi");
            return View();
        }

        // POST: tilaukset/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TilausID,AsiakasID,Henkilo_id,Tilauspvm,Toimituspvmss,Toimitusosoite,ShipRegion,Postinumero")] Tilaukset tilaukset)
        {
            if (ModelState.IsValid)
            {
                Postitoimipaikat newPostitoimipaikat = new Postitoimipaikat();
                newPostitoimipaikat.Postinumero = tilaukset.Postinumero;
                db.Postitoimipaikat.Add(newPostitoimipaikat);
                db.Tilaukset.Add(tilaukset);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AsiakasID = new SelectList(db.Asiakkaat, "AsiakasID", "Nimi", tilaukset.AsiakasID);
            return View(tilaukset);
        }

        // GET: tilaukset/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tilaukset tilaukset = db.Tilaukset.Find(id);
            if (tilaukset == null)
            {
                return HttpNotFound();
            }
            ViewBag.AsiakasID = new SelectList(db.Asiakkaat, "AsiakasID", "Nimi", tilaukset.AsiakasID);         
            return View(tilaukset);
        }

        // POST: tilaukset/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TilausID,AsiakasID,Henkilo_id,Tilauspvm,Toimituspvmss,Toimitusosoite,ShipRegion,Postinumero")] Tilaukset tilaukset)
        {
            if (ModelState.IsValid)
            {
                if (tilaukset.Tilauspvm != null)
                {
                    
                    tilaukset.Toimituspvm = tilaukset.Tilauspvm.Value.AddDays(14);
                }
                db.Entry(tilaukset).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AsiakasID = new SelectList(db.Asiakkaat, "AsiakasID", "Nimi", tilaukset.AsiakasID);
            return View(tilaukset);
        }

        // GET: tilaukset/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tilaukset tilaukset = db.Tilaukset.Find(id);
            if (tilaukset == null)
            {
                return HttpNotFound();
            }
            return View(tilaukset);
        }

        // POST: tilaukset/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tilaukset tilaukset = db.Tilaukset.Find(id);
            db.Tilaukset.Remove(tilaukset);
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
