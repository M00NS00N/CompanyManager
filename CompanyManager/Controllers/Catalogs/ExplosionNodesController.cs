using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CompanyManager.Algorithms;
using CompanyManager.DatabaseAccessLayer.Context;
using CompanyManager.Models;

namespace CompanyManager.Controllers.Catalogs
{
    public class ExplosionNodesController : Controller
    {
        private CompanyDatabaseContext db = new CompanyDatabaseContext();

        // GET: ExplosionNodes
        public ActionResult Index()
        {
            var explosionNodes = db.ExplosionNodes.Include(e => e.MainProduct).Include(e => e.ProductComponent).ToList();
            foreach(var node in explosionNodes)
            {
                db.ExplosionNodes.Remove(node);
            }
            db.SaveChanges();
            ExplosionNodesViewModel model = new ExplosionNodesViewModel
            {
                Nodes = db.Nodes.Include(e => e.MainProduct).Include(e => e.InitialProduct).Include(e => e.DestinationProduct).ToList(),// new List<Node>(),
                ExplosionNodes = new List<ExplosionNode>()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string productCode)
        {
            if(productCode == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var explosionNode = db.Products.First(x => x.ProductCode == productCode);
            if(explosionNode == null)
            {
                return HttpNotFound();
            }
            Algorithms.Explosion alg = new Algorithms.Explosion();
            alg.Calculate(explosionNode);
            var explosionNodesResult = db.ExplosionNodes.Include(e => e.MainProduct).Include(e => e.ProductComponent).ToList();
            ExplosionNodesViewModel model = new ExplosionNodesViewModel
            {
                ExplosionNodes = explosionNodesResult,
                Nodes = db.Nodes.Include(e => e.MainProduct).Include(e => e.InitialProduct).Include(e => e.DestinationProduct).ToList()
            };
            return View(model);
        }

        // GET: ExplosionNodes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExplosionNode explosionNode = db.ExplosionNodes.Find(id);
            if (explosionNode == null)
            {
                return HttpNotFound();
            }
            return View(explosionNode);
        }

        // GET: ExplosionNodes/Create
        public ActionResult Create()
        {
            ViewBag.MainProductId = new SelectList(db.Products, "Id", "ProductName");
            ViewBag.ProductComponentId = new SelectList(db.Products, "Id", "ProductName");
            return View();
        }

        // POST: ExplosionNodes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MainProductId,ProductComponentId,Count")] ExplosionNode explosionNode)
        {
            if (ModelState.IsValid)
            {
                db.ExplosionNodes.Add(explosionNode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MainProductId = new SelectList(db.Products, "Id", "ProductName", explosionNode.MainProductId);
            ViewBag.ProductComponentId = new SelectList(db.Products, "Id", "ProductName", explosionNode.ProductComponentId);
            return View(explosionNode);
        }

        // GET: ExplosionNodes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExplosionNode explosionNode = db.ExplosionNodes.Find(id);
            if (explosionNode == null)
            {
                return HttpNotFound();
            }
            ViewBag.MainProductId = new SelectList(db.Products, "Id", "ProductName", explosionNode.MainProductId);
            ViewBag.ProductComponentId = new SelectList(db.Products, "Id", "ProductName", explosionNode.ProductComponentId);
            return View(explosionNode);
        }

        // POST: ExplosionNodes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MainProductId,ProductComponentId,Count")] ExplosionNode explosionNode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(explosionNode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MainProductId = new SelectList(db.Products, "Id", "ProductName", explosionNode.MainProductId);
            ViewBag.ProductComponentId = new SelectList(db.Products, "Id", "ProductName", explosionNode.ProductComponentId);
            return View(explosionNode);
        }

        // GET: ExplosionNodes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExplosionNode explosionNode = db.ExplosionNodes.Find(id);
            if (explosionNode == null)
            {
                return HttpNotFound();
            }
            return View(explosionNode);
        }

        // POST: ExplosionNodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ExplosionNode explosionNode = db.ExplosionNodes.Find(id);
            db.ExplosionNodes.Remove(explosionNode);
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
