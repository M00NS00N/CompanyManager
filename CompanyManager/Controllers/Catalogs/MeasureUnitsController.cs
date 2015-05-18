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
    public class MeasureUnitsController : Controller
    {
        // GET: MeasureUnits
        public ActionResult Index()
        {
            var measureUnitList = UnitOfWork.Instance.MeasureUnitRepository.GetAll(null, null, String.Empty);
            return View(measureUnitList);
        }

        // GET: MeasureUnits/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeasureUnit measureUnit = UnitOfWork.Instance.MeasureUnitRepository.GetById(id);
            if (measureUnit == null)
            {
                return HttpNotFound();
            }
            return View(measureUnit);
        }

        // GET: MeasureUnits/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MeasureUnits/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MeasureUnitFullName,MeasureUnitShortName")] MeasureUnit measureUnit)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork.Instance.MeasureUnitRepository.Add(measureUnit);
                return RedirectToAction("Index");
            }

            return View(measureUnit);
        }

        // GET: MeasureUnits/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeasureUnit measureUnit = UnitOfWork.Instance.MeasureUnitRepository.GetById(id);
            if (measureUnit == null)
            {
                return HttpNotFound();
            }
            return View(measureUnit);
        }

        // POST: MeasureUnits/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MeasureUnitFullName,MeasureUnitShortName")] MeasureUnit measureUnit)
        {
            if (ModelState.IsValid)
            {
                MeasureUnit measureUnitToUpdate = UnitOfWork.Instance.MeasureUnitRepository.GetById(measureUnit.Id);
                measureUnitToUpdate.MeasureUnitFullName = measureUnit.MeasureUnitFullName;
                measureUnitToUpdate.MeasureUnitShortName = measureUnit.MeasureUnitShortName;
                UnitOfWork.Instance.MeasureUnitRepository.Update(measureUnitToUpdate);
                return RedirectToAction("Index");
            }
            return View(measureUnit);
        }

        // GET: MeasureUnits/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MeasureUnit measureUnit = UnitOfWork.Instance.MeasureUnitRepository.GetById(id);
            if (measureUnit == null)
            {
                return HttpNotFound();
            }
            return View(measureUnit);
        }

        // POST: MeasureUnits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            MeasureUnit measureUnit = UnitOfWork.Instance.MeasureUnitRepository.GetById(id);
            UnitOfWork.Instance.MeasureUnitRepository.Remove(measureUnit);
            return RedirectToAction("Index");
        }
    }
}
