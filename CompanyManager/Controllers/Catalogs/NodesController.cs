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
using CompanyManager.Models;

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
            var products = db.Products.OrderBy(x => x.Annotation);
            var productNames = products.Select(p => p.Annotation + " " + p.ProductName);

            List<ProductViewModel> productViewModels = new List<ProductViewModel>();
            foreach (Product product in products)
            {
                productViewModels.Add(new ProductViewModel()
                {
                    ProductId = product.Id,
                    ProductName = product.Annotation + " " + product.ProductName
                });
            }

            CreateNodeViewModel createNodeViewModel = new CreateNodeViewModel();
            createNodeViewModel.Products = productViewModels;
            //Tuple<NodeViewModel, IEnumerable<ProductViewModel>> tuple =
            //    new Tuple<NodeViewModel, IEnumerable<ProductViewModel>>(new NodeViewModel(), productViewModels);
            //ViewBag.DestinationProductId = new SelectList(products, "Id", "Annotation");
            //ViewBag.InitialProductId = new SelectList(products, "Id", "Annotation");
            //ViewBag.MainProductId = new SelectList(products, "Id", "Annotation");
            return View(createNodeViewModel);
        }

        // POST: Nodes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateNodeViewModel createNodeViewModel)
        {
            NodeViewModel nodeViewModel = createNodeViewModel.Node;
            if (ModelState.IsValid)
            {
                Node node = new Node()
                {
                    MainProductId = nodeViewModel.MainProductId,
                    InitialProductId = nodeViewModel.InitialProductId,
                    DestinationProductId = nodeViewModel.DestinationProductId,
                    Count = nodeViewModel.Count
                };

                db.Nodes.Add(node);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.DestinationProductId = new SelectList(db.Products, "Id", "Annotation", node.DestinationProductId);
            //ViewBag.InitialProductId = new SelectList(db.Products, "Id", "Annotation", node.InitialProductId);
            //ViewBag.MainProductId = new SelectList(db.Products, "Id", "Annotation", node.MainProductId);
            return View(createNodeViewModel);
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

            var products = db.Products.OrderBy(x => x.Annotation);
            var productNames = products.Select(p => p.Annotation + " " + p.ProductName);

            List<ProductViewModel> productViewModels = new List<ProductViewModel>();            
            foreach (Product product in products)
            {
                productViewModels.Add(new ProductViewModel()
                {
                    ProductId = product.Id,
                    ProductName = product.Annotation + " " + product.ProductName
                });
            }

            CreateNodeViewModel createNodeViewModel = new CreateNodeViewModel();
            createNodeViewModel.Node = new NodeViewModel()
            {
                Id = node.Id,
                MainProductId = node.MainProductId,
                InitialProductId = node.InitialProductId,
                DestinationProductId = node.DestinationProductId,
                Count = node.Count
            };
            createNodeViewModel.Products = productViewModels;

            //ViewBag.DestinationProductId = new SelectList(db.Products, "Id", "Annotation", node.DestinationProductId);
            //ViewBag.InitialProductId = new SelectList(db.Products, "Id", "Annotation", node.InitialProductId);
            //ViewBag.MainProductId = new SelectList(db.Products, "Id", "Annotation", node.MainProductId);
            return View(createNodeViewModel);
        }

        // POST: Nodes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreateNodeViewModel createNodeViewModel)
        {
            if (ModelState.IsValid)
            {
                NodeViewModel nodeViewModel = createNodeViewModel.Node;
                Node node = db.Nodes.SingleOrDefault(n => n.Id == createNodeViewModel.Node.Id);
                node.MainProductId = nodeViewModel.MainProductId;
                node.InitialProductId = nodeViewModel.InitialProductId;
                node.DestinationProductId = nodeViewModel.DestinationProductId;
                node.Count = nodeViewModel.Count;

                db.Entry(node).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.DestinationProductId = new SelectList(db.Products, "Id", "Annotation", node.DestinationProductId);
            //ViewBag.InitialProductId = new SelectList(db.Products, "Id", "Annotation", node.InitialProductId);
            //ViewBag.MainProductId = new SelectList(db.Products, "Id", "Annotation", node.MainProductId);
            return View(createNodeViewModel);
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
