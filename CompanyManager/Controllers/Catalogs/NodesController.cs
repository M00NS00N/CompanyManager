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
    public class NodesController : Controller
    {
        private CompanyDatabaseContext db = new CompanyDatabaseContext();

        // GET: Nodes
        public ActionResult Index()
        {
            var nodes = db.Nodes.Include(n => n.DestinationProduct).Include(n => n.InitialProduct).Include(n => n.MainProduct);
            return View(nodes.ToList());
        }

        // GET: Nodes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Node node = db.Nodes.Find(id);
            node.InitialProduct = db.Products.Find(node.InitialProductId);
            node.DestinationProduct = db.Products.Find(node.DestinationProductId);
            node.MainProduct = db.Products.Find(node.MainProductId);
            if (node == null)
            {
                return HttpNotFound();
            }
            return View(node);
        }

        // GET: Nodes/Create
        public ActionResult Create()
        {
            ViewBag.DestinationProductId = new SelectList(db.Products.OrderBy(x=>x.Annotation), "Id", "Annotation");
            ViewBag.InitialProductId = new SelectList(db.Products.OrderBy(x => x.Annotation), "Id", "Annotation");
            ViewBag.MainProductId = new SelectList(db.Products.OrderBy(x => x.Annotation), "Id", "Annotation");
            return View();
        }

        // POST: Nodes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MainProductId,InitialProductId,DestinationProductId,Count")] Node node)
        {
            if (ModelState.IsValid)
            {
                db.Nodes.Add(node);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DestinationProductId = new SelectList(db.Products, "Id", "Annotation", node.DestinationProductId);
            ViewBag.InitialProductId = new SelectList(db.Products, "Id", "Annotation", node.InitialProductId);
            ViewBag.MainProductId = new SelectList(db.Products, "Id", "Annotation", node.MainProductId);
            return View(node);
        }

        // GET: Nodes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Node node = db.Nodes.Find(id);
            if (node == null)
            {
                return HttpNotFound();
            }
            ViewBag.DestinationProductId = new SelectList(db.Products, "Id", "Annotation", node.DestinationProductId);
            ViewBag.InitialProductId = new SelectList(db.Products, "Id", "Annotation", node.InitialProductId);
            ViewBag.MainProductId = new SelectList(db.Products, "Id", "Annotation", node.MainProductId);
            return View(node);
        }

        // POST: Nodes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MainProductId,InitialProductId,DestinationProductId,Count")] Node node)
        {
            if (ModelState.IsValid)
            {
                db.Entry(node).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DestinationProductId = new SelectList(db.Products, "Id", "Annotation", node.DestinationProductId);
            ViewBag.InitialProductId = new SelectList(db.Products, "Id", "Annotation", node.InitialProductId);
            ViewBag.MainProductId = new SelectList(db.Products, "Id", "Annotation", node.MainProductId);
            return View(node);
        }

        // GET: Nodes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Node node = db.Nodes.Find(id);
            node.InitialProduct = db.Products.Find(node.InitialProductId);
            node.DestinationProduct = db.Products.Find(node.DestinationProductId);
            node.MainProduct = db.Products.Find(node.MainProductId);
            if (node == null)
            {
                return HttpNotFound();
            }
            return View(node);
        }

        // POST: Nodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Node node = db.Nodes.Find(id);
            db.Nodes.Remove(node);
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
