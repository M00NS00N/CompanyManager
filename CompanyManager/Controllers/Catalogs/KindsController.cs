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
    public class KindsController : Controller
    {

        // GET: Kinds
        public ActionResult Index()
        {
            var kindsList = UnitOfWork.Instance.KindRepository.GetAll(null, null, String.Empty);
            return View(kindsList);
        }

        // GET: Kinds/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kind kind = UnitOfWork.Instance.KindRepository.GetById(id);
            if (kind == null)
            {
                return HttpNotFound();
            }
            return View(kind);
        }

        // GET: Kinds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kinds/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,KindName")] Kind kind)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork.Instance.KindRepository.Add(kind);
                return RedirectToAction("Index");
            }

            return View(kind);
        }

        // GET: Kinds/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kind kind = UnitOfWork.Instance.KindRepository.GetById(id);
            if (kind == null)
            {
                return HttpNotFound();
            }
            return View(kind);
        }

        // POST: Kinds/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,KindName")] Kind kind)
        {
            if (ModelState.IsValid)
            {
                Kind kindToUpdate = UnitOfWork.Instance.KindRepository.GetById(kind.Id);

                kindToUpdate.KindName = kind.KindName;
                
                UnitOfWork.Instance.KindRepository.Update(kindToUpdate);
                return RedirectToAction("Index");
            }
            return View(kind);
        }

        // GET: Kinds/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kind kind = UnitOfWork.Instance.KindRepository.GetById(id);
            if (kind == null)
            {
                return HttpNotFound();
            }
            return View(kind);
        }

        // POST: Kinds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Kind kind = UnitOfWork.Instance.KindRepository.GetById(id);
            UnitOfWork.Instance.KindRepository.Remove(kind);
            return RedirectToAction("Index");
        }

    }
}
