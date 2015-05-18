using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CompanyManager.Algorithms;
using CompanyManager.DatabaseAccessLayer.Context;
using CompanyManager.Models;

namespace CompanyManager.Controllers.Catalogs
{
    public class ProductPlanResultsController : Controller
    {
        private CompanyDatabaseContext db = new CompanyDatabaseContext();

        // GET: ProductPlanResults
        //public ActionResult Index()
        //{

        //    ProductPlanResultViewModel model = new ProductPlanResultViewModel()
        //    {
        //        ProductPlanResults = new List<ProductPlanResult>(),
        //        Nodes =
        //            db.Nodes.Include(e => e.MainProduct)
        //                .Include(e => e.InitialProduct)
        //                .Include(e => e.DestinationProduct)
        //                .ToList()
        //    };
        //    return View(model);
        //}

        public ActionResult Index(string productCode, int count)
        {
            if (productCode == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var explosionNode = db.Products.First(x => x.ProductCode == productCode);
            if (explosionNode == null)
            {
                return HttpNotFound();
            }
            MaterialRate algMat = new MaterialRate();

            var result = algMat.Calculate(explosionNode.Id);
            result = db.ProductPlanResults.Include(p => p.Material).Include(p=>p.Material.MeasureUnit).ToList();
            foreach (var res in result)
            {
                res.Value *= count;
            }
            var model = new ProductPlanResultViewModel()
            {
                ProductPlanResults = result,
                Nodes =
                    db.Nodes.Include(e => e.MainProduct)
                        .Include(e => e.InitialProduct)
                        .Include(e => e.DestinationProduct)
                        .ToList()
            };
            return View(model);
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
