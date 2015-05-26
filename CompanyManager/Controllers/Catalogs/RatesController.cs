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
            CreateRateViewModel createRate = new CreateRateViewModel();            
            createRate.RateViewModel = new RateViewModel();
            IEnumerable<Product> products = db.Products.OrderBy(x => x.ProductCode);
            List<ProductViewModel> productViewModels = new List<ProductViewModel>();
            foreach (Product product in products)
            {
                productViewModels.Add(new ProductViewModel()
                {
                    ProductId = product.Id,
                    ProductName = product.Annotation + " " + product.ProductName
                });
            }

            createRate.Materials = db.Materials.OrderBy(x => x.Code);
            createRate.Products = productViewModels;
            
            return View(createRate);
        }

        // POST: Rates/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateRateViewModel createRateViewModel)
        {            
            if (ModelState.IsValid)
            {
                RateViewModel rateViewModel = createRateViewModel.RateViewModel;
                Rate rate = new Rate()
                {
                    MaterialId = rateViewModel.MaterialId,
                    ProductId = rateViewModel.ProductId,
                    ConsumptionRate = rateViewModel.ConsumptionRate,
                    WasteRate = rateViewModel.WasteRate
                };

                db.Rates.Add(rate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.MaterialId = new SelectList(db.Materials.OrderBy(x=>x.Code), "Id", "Code", rate.MaterialId);
            //ViewBag.ProductId = new SelectList(db.Products.OrderBy(x=>x.ProductCode), "Id", "ProductCode", rate.ProductId);
            return View(createRateViewModel);
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

            CreateRateViewModel createRate = new CreateRateViewModel();
            createRate.RateViewModel = new RateViewModel()
            {
                Id = rate.Id,
                MaterialId = rate.MaterialId,
                ProductId = rate.ProductId,
                ConsumptionRate = rate.ConsumptionRate,
                WasteRate = rate.WasteRate
            };
            IEnumerable<Product> products = db.Products.OrderBy(x => x.ProductCode);
            List<ProductViewModel> productViewModels = new List<ProductViewModel>();
            foreach (Product product in products)
            {
                productViewModels.Add(new ProductViewModel()
                {
                    ProductId = product.Id,
                    ProductName = product.Annotation + " " + product.ProductName
                });
            }

            createRate.Materials = db.Materials.OrderBy(x => x.Code);
            createRate.Products = productViewModels;

            //ViewBag.MaterialId = new SelectList(db.Materials.OrderBy(x=>x.Code), "Id", "Code", rate.MaterialId);
            //ViewBag.ProductId = new SelectList(db.Products.OrderBy(x=>x.ProductCode), "Id", "ProductCode", rate.ProductId);
            return View(createRate);
        }

        // POST: Rates/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreateRateViewModel createRateViewModel)
        {
            if (ModelState.IsValid)
            {
                RateViewModel rateViewModel = createRateViewModel.RateViewModel;
                Rate rate = db.Rates.SingleOrDefault(r => r.Id == rateViewModel.Id);
                rate.ProductId = rateViewModel.ProductId;
                rate.MaterialId = rateViewModel.MaterialId;
                rate.ConsumptionRate = rateViewModel.ConsumptionRate;
                rate.WasteRate = rateViewModel.WasteRate;

                db.Entry(rate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.MaterialId = new SelectList(db.Materials.OrderBy(x=>x.Code), "Id", "Code", rate.MaterialId);
            //ViewBag.ProductId = new SelectList(db.Products.OrderBy(x=>x.ProductCode), "Id", "ProductCode", rate.ProductId);
            return View(createRateViewModel);
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
