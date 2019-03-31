using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SCM.DataService.DataContext;
using SCM.Models;

namespace SCM.Controllers
{
    public class ChemicalCompositionsController : Controller
    {
        private AppDataContext db = new AppDataContext();

        // GET: ChemicalCompositions
        public ActionResult Index()
        {
            if(db.ChemicalElements.Count() < 4)
            {
                var elements = ParseJsonHelper.GetChemicalElements()
                .Select(el => new ChemicalElement { Title = el.name, Symbol = el.symbol, IsPublish = true });

                foreach (var el in elements)
                {
                    db.ChemicalElements.Add(el);
                }
                db.SaveChanges();
            }
            
            
            return View(db.ChemicalCompositions.ToList());
        }

        // GET: ChemicalCompositions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChemicalComposition chemicalComposition = db.ChemicalCompositions.Find(id);
            if (chemicalComposition == null)
            {
                return HttpNotFound();
            }
            return View(chemicalComposition);
        }

        // GET: ChemicalCompositions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChemicalCompositions/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,DeleteForbidden,EditForbidden,IsPublish,LastModifed")] ChemicalComposition chemicalComposition)
        {
            if (ModelState.IsValid)
            {
                db.ChemicalCompositions.Add(chemicalComposition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(chemicalComposition);
        }

        // GET: ChemicalCompositions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChemicalComposition chemicalComposition = db.ChemicalCompositions.Find(id);
            if (chemicalComposition == null)
            {
                return HttpNotFound();
            }
            return View(chemicalComposition);
        }

        // POST: ChemicalCompositions/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,DeleteForbidden,EditForbidden,IsPublish,LastModifed")] ChemicalComposition chemicalComposition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chemicalComposition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chemicalComposition);
        }

        // GET: ChemicalCompositions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChemicalComposition chemicalComposition = db.ChemicalCompositions.Find(id);
            if (chemicalComposition == null)
            {
                return HttpNotFound();
            }
            return View(chemicalComposition);
        }

        // POST: ChemicalCompositions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChemicalComposition chemicalComposition = db.ChemicalCompositions.Find(id);
            db.ChemicalCompositions.Remove(chemicalComposition);
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
