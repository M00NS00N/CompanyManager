using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CompanyManager.DatabaseAccessLayer.Context;

namespace CompanyManager.Controllers.Catalogs
{
    public class ProductPlansController : Controller
    {
        private CompanyDatabaseContext db = new CompanyDatabaseContext();

        // GET: ProductPlans
        public ActionResult Index()
        {
            var productPlans = db.ProductPlans.Include(p => p.Product);
            return View(productPlans.OrderBy(x=>x.Year).ThenBy(x=>x.Month).ToList());
        }

        // GET: ProductPlans/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductPlan productPlan = db.ProductPlans.Find(id);
            if (productPlan == null)
            {
                return HttpNotFound();
            }
            return View(productPlan);
        }

        // GET: ProductPlans/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products.Where(x => x.ProductCode == "92660").ToList(), "Id", "ProductName");
            return View();
        }

        // POST: ProductPlans/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductId,Month,Year,Count")] ProductPlan productPlan)
        {
            if (ModelState.IsValid)
            {
                db.ProductPlans.Add(productPlan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Products.Where(x => x.ProductCode == "92660").ToList(), "Id", "ProductName", productPlan.ProductId);
            return View(productPlan);
        }

        // GET: ProductPlans/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductPlan productPlan = db.ProductPlans.Find(id);
            if (productPlan == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "ProductName", productPlan.ProductId);
            return View(productPlan);
        }

        // POST: ProductPlans/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductId,Month,Year,Count")] ProductPlan productPlan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productPlan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "ProductName", productPlan.ProductId);
            return View(productPlan);
        }

        // GET: ProductPlans/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductPlan productPlan = db.ProductPlans.Find(id);
            if (productPlan == null)
            {
                return HttpNotFound();
            }
            return View(productPlan);
        }

        // POST: ProductPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ProductPlan productPlan = db.ProductPlans.Find(id);
            db.ProductPlans.Remove(productPlan);
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
