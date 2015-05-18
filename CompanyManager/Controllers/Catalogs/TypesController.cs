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
using Type = CompanyManager.DatabaseAccessLayer.Context.Type;

namespace CompanyManager.Controllers.Catalogs
{
    public class TypesController : Controller
    {
        // GET: Types
        public ActionResult Index()
        {
            var typesList = UnitOfWork.Instance.TypeRepository.GetAll(null, null, String.Empty);
            return View(typesList);
        }

        // GET: Types/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Type type = UnitOfWork.Instance.TypeRepository.GetById(id);
            if (type == null)
            {
                return HttpNotFound();
            }
            return View(type);
        }

        // GET: Types/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Types/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TypeName")] Type type)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork.Instance.TypeRepository.Add(type);
                return RedirectToAction("Index");
            }

            return View(type);
        }

        // GET: Types/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Type type = UnitOfWork.Instance.TypeRepository.GetById(id);
            if (type == null)
            {
                return HttpNotFound();
            }
            return View(type);
        }

        // POST: Types/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TypeName")] Type type)
        {
            if (ModelState.IsValid)
            {
                Type typeToUpdate = UnitOfWork.Instance.TypeRepository.GetById(type.Id);
                typeToUpdate.TypeName = type.TypeName;

                UnitOfWork.Instance.TypeRepository.Update(typeToUpdate);
                return RedirectToAction("Index");
            }
            return View(type);
        }

        // GET: Types/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Type type = UnitOfWork.Instance.TypeRepository.GetById(id);
            if (type == null)
            {
                return HttpNotFound();
            }
            return View(type);
        }

        // POST: Types/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Type type = UnitOfWork.Instance.TypeRepository.GetById(id);
            UnitOfWork.Instance.TypeRepository.Remove(type);
            return RedirectToAction("Index");
        }
    }
}
