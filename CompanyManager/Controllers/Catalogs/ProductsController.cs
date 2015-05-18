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
    public class ProductsController : Controller
    {
        private CompanyDatabaseContext db = new CompanyDatabaseContext();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Attribute).Include(p => p.Kind).Include(p => p.Type).Include(p=>p.ProductMeasureUnit).ToList();
            foreach(var product in products)
            {
                product.ProductMeasureUnit = db.MeasureUnits.Find(product.ProductMeasureUnitId);
            }
            return View(products);
        }

        // GET: Products/Details/5
        public ActionResult Details(string id)
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

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.AttributeId = new SelectList(db.Attributes, "Id", "AttributeName");
            ViewBag.KindId = new SelectList(db.Kinds, "Id", "KindName");
            ViewBag.TypeId = new SelectList(db.Types, "Id", "TypeName");
            ViewBag.ProductMeasureUnitId = new SelectList(db.MeasureUnits, "Id", "MeasureUnitShortName");
            return View();
        }

        // POST: Products/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Annotation,Count,ProductName,ProductCode,KindId,TypeId,AttributeId,ProductMeasureUnitId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AttributeId = new SelectList(db.Attributes, "Id", "AttributeName", product.AttributeId);
            ViewBag.KindId = new SelectList(db.Kinds, "Id", "KindName", product.KindId);
            ViewBag.TypeId = new SelectList(db.Types, "Id", "TypeName", product.TypeId);
            ViewBag.ProductMeasureUnitId = new SelectList(db.MeasureUnits, "Id", "MeasureUnitShortName", product.ProductMeasureUnitId);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(string id)
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
            ViewBag.AttributeId = new SelectList(db.Attributes, "Id", "AttributeName", product.AttributeId);
            ViewBag.KindId = new SelectList(db.Kinds, "Id", "KindName", product.KindId);
            ViewBag.TypeId = new SelectList(db.Types, "Id", "TypeName", product.TypeId);
            ViewBag.ProductMeasureUnitId = new SelectList(db.MeasureUnits, "Id", "MeasureUnitShortName", product.ProductMeasureUnitId);
            return View(product);
        }

        // POST: Products/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Annotation,Count,ProductName,ProductCode,KindId,TypeId,AttributeId,ProductMeasureUnitId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AttributeId = new SelectList(db.Attributes, "Id", "AttributeName", product.AttributeId);
            ViewBag.KindId = new SelectList(db.Kinds, "Id", "KindName", product.KindId);
            ViewBag.TypeId = new SelectList(db.Types, "Id", "TypeName", product.TypeId);
            ViewBag.ProductMeasureUnitId = new SelectList(db.MeasureUnits, "Id", "MeasureUnitShortName", product.ProductMeasureUnitId);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(string id)
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

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public PartialViewResult Search(string keyword)
        {
            var list = db.Products.Where(x => x.ProductCode.Contains(keyword));
            if (list.Count() == 0)
            {
                list = db.Products.Where(x => x.ProductName.Contains(keyword));
            }
            return PartialView(list);
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
