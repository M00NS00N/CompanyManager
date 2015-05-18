using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompanyManager.DatabaseAccessLayer;
using CompanyManager.DatabaseAccessLayer.Context;
using CompanyManager.DatabaseAccessLayer.Repositories;
using CompanyManager.DatabaseAccessLayer.Repositories.Interfaces;
using Attribute = CompanyManager.DatabaseAccessLayer.Context.Attribute;

namespace CompanyManager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //CompanyDatabaseContext context = new CompanyDatabaseContext();
            //IGenericRepository<Attribute> attrRepo = new GenericRepository<Attribute>(context);
            //attrRepo.Add(new Attribute
            //{
            //    AttributeName = "new Attribute"
            //});
            return View();
        }

        public ActionResult Catalog()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}