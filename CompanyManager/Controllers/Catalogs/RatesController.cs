using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CompanyManager.DatabaseAccessLayer;
using CompanyManager.DatabaseAccessLayer.Context;

namespace CompanyManager.Controllers.Catalogs
{
    public class RatesController : Controller
    {
        private CompanyDatabaseContext db = new CompanyDatabaseContext();

        // GET: Rates
        public ActionResult Index()
        {
            var rates = db.Rates.Include(r => r.Material).Include(r => r.Product);
            return View(rates.ToList());
        }

        // GET: Rates/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rate rate = db.Rates.Find(id);
            if (rate == null)
            {
                return HttpNotFound();
            }
            return View(rate);
        }

        // GET: Rates/Create
        public ActionResult Create()
        {
            ViewBag.MaterialId = new SelectList(db.Materials.OrderBy(x=>x.Code), "Id", "Code");
            ViewBag.ProductId = new SelectList(db.Products.OrderBy(x=>x.ProductCode), "Id", "ProductCode");
            return View();
        }

        // POST: Rates/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductId,MaterialId,ConsumptionRate,WasteRate")] Rate rate)
        {
            if (ModelState.IsValid)
            {
                db.Rates.Add(rate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaterialId = new SelectList(db.Materials.OrderBy(x=>x.Code), "Id", "Code", rate.MaterialId);
            ViewBag.ProductId = new SelectList(db.Products.OrderBy(x=>x.ProductCode), "Id", "ProductCode", rate.ProductId);
            return View(rate);
        }

        // GET: Rates/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rate rate = db.Rates.Find(id);
            if (rate == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaterialId = new SelectList(db.Materials.OrderBy(x=>x.Code), "Id", "Code", rate.MaterialId);
            ViewBag.ProductId = new SelectList(db.Products.OrderBy(x=>x.ProductCode), "Id", "ProductCode", rate.ProductId);
            return View(rate);
        }

        // POST: Rates/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductId,MaterialId,ConsumptionRate,WasteRate")] Rate rate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaterialId = new SelectList(db.Materials.OrderBy(x=>x.Code), "Id", "Code", rate.MaterialId);
            ViewBag.ProductId = new SelectList(db.Products.OrderBy(x=>x.ProductCode), "Id", "ProductCode", rate.ProductId);
            return View(rate);
        }

        // GET: Rates/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rate rate = db.Rates.Find(id);
            if (rate == null)
            {
                return HttpNotFound();
            }
            return View(rate);
        }

        // POST: Rates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Rate rate = db.Rates.Find(id);
            db.Rates.Remove(rate);
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
