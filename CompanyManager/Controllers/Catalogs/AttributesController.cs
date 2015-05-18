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
using Attribute = CompanyManager.DatabaseAccessLayer.Context.Attribute;

namespace CompanyManager.Controllers.Catalogs
{
    public class AttributesController : Controller
    {
        // GET: Attributes
        public ActionResult Index()
        {
            var attributesList = UnitOfWork.Instance.AttributeRepository.GetAll(null, null, String.Empty);
            return View(attributesList);
        }

        // GET: Attributes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attribute attribute = UnitOfWork.Instance.AttributeRepository.GetById(id);
            if (attribute == null)
            {
                return HttpNotFound();
            }
            return View(attribute);
        }

        // GET: Attributes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Attributes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AttributeName")] Attribute attribute)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork.Instance.AttributeRepository.Add(attribute);
                
                return RedirectToAction("Index");
            }

            return View(attribute);
        }

        // GET: Attributes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attribute attribute = UnitOfWork.Instance.AttributeRepository.GetById(id);
            if (attribute == null)
            {
                return HttpNotFound();
            }
            return View(attribute);
        }

        // POST: Attributes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AttributeName")] Attribute attribute)
        {
            if (ModelState.IsValid)
            {
                Attribute attributeToUpdate = UnitOfWork.Instance.AttributeRepository.GetById(attribute.Id);
                attributeToUpdate.AttributeName = attribute.AttributeName;

                UnitOfWork.Instance.AttributeRepository.Update(attributeToUpdate);
                
                return RedirectToAction("Index");
            }
            return View(attribute);
        }

        // GET: Attributes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attribute attribute = UnitOfWork.Instance.AttributeRepository.GetById(id);
            if (attribute == null)
            {
                return HttpNotFound();
            }
            return View(attribute);
        }

        // POST: Attributes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Attribute attribute = UnitOfWork.Instance.AttributeRepository.GetById(id);
            UnitOfWork.Instance.AttributeRepository.Remove(attribute);
            return RedirectToAction("Index");
        }
    }
}
