using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using SCM.DataService.DataContext;
using SCM.Models;

namespace SCM.Controllers
{
    [Authorize]
    public class SamplesController : Controller
    {
        private AppDataContext db = new AppDataContext();

        // GET: Samples
        public ActionResult Index()
        {
            var samples = db.Samples.Include(s => s.ChemicalComposition);
            return View(samples.ToList());
        }
        
        public ActionResult SamplesRead([DataSourceRequest]DataSourceRequest request)
        {
            var context = new AppDataContext();
            var samples = context.Samples.ToList();
            return Json(samples.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        // GET: Samples/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sample sample = db.Samples.Find(id);
            if (sample == null)
            {
                return HttpNotFound();
            }
            return View(sample);
        }

        // GET: Samples/Create
        public ActionResult Create(int? categoryId)
        {
            if (categoryId.HasValue)
            {
                ViewBag.ChemicalCompositionId = new SelectList(db.ChemicalCompositions.Where(c=> c.SampleCategoryId == categoryId), "Id", "Title"); 
            }
            else
            {
                ViewBag.ChemicalCompositionId = new SelectList(db.ChemicalCompositions, "Id", "Title");   
            }
            
            return View();
        }

        // POST: Samples/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,ChemicalCompositionId,Amount,ImageLink,Weight,ExpiryDate")] Sample sample)
        {
            if (ModelState.IsValid)
            {

                db.Samples.Add(sample);
                db.SaveChanges();
                return RedirectToAction("Index", "Catalog");
            }

            ViewBag.ChemicalCompositionId = new SelectList(db.ChemicalCompositions, "Id", "Title", sample.ChemicalCompositionId);
            return View(sample);
        }

        // GET: Samples/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sample sample = db.Samples.Find(id);
            if (sample == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChemicalCompositionId = new SelectList(db.ChemicalCompositions, "Id", "Title", sample.ChemicalCompositionId);
            return View(sample);
        }

        // POST: Samples/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,ChemicalCompositionId,Amount,ImageLink,Weight,ExpiryDate")] Sample sample)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sample).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ChemicalCompositionId = new SelectList(db.ChemicalCompositions, "Id", "Title", sample.ChemicalCompositionId);
            return View(sample);
        }

        // GET: Samples/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sample sample = db.Samples.Find(id);
            if (sample == null)
            {
                return HttpNotFound();
            }
            return View(sample);
        }

        // POST: Samples/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sample sample = db.Samples.Find(id);
            db.Samples.Remove(sample);
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
