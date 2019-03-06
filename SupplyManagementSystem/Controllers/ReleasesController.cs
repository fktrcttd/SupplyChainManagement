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
    public class ReleasesController : Controller
    {
        private AppDataContext db = new AppDataContext();

        // GET: Releases
        public ActionResult Index()
        {
            var releases = db.Releases.Include(r => r.Sample).Include(r => r.Worker);
            return View(releases.ToList());
        }

        // GET: Releases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Release release = db.Releases.Find(id);
            if (release == null)
            {
                return HttpNotFound();
            }
            return View(release);
        }

        // GET: Releases/Create
        public ActionResult Create()
        {
            ViewBag.SampleId = new SelectList(db.Samples, "Id", "Title");
            ViewBag.WorkerId = new SelectList(db.Workers, "Id", "Name");
            return View();
        }

        // POST: Releases/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SampleId,WorkerId,Date,Quantity,DeleteForbidden,EditForbidden,Title,IsPublish,LastModifed")] Release release)
        {
            if (ModelState.IsValid)
            {
                db.Releases.Add(release);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SampleId = new SelectList(db.Samples, "Id", "Title", release.SampleId);
            ViewBag.WorkerId = new SelectList(db.Workers, "Id", "Name", release.WorkerId);
            return View(release);
        }

        // GET: Releases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Release release = db.Releases.Find(id);
            if (release == null)
            {
                return HttpNotFound();
            }
            ViewBag.SampleId = new SelectList(db.Samples, "Id", "Title", release.SampleId);
            ViewBag.WorkerId = new SelectList(db.Workers, "Id", "Name", release.WorkerId);
            return View(release);
        }

        // POST: Releases/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SampleId,WorkerId,Date,Quantity,DeleteForbidden,EditForbidden,Title,IsPublish,LastModifed")] Release release)
        {
            if (ModelState.IsValid)
            {
                db.Entry(release).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SampleId = new SelectList(db.Samples, "Id", "Title", release.SampleId);
            ViewBag.WorkerId = new SelectList(db.Workers, "Id", "Name", release.WorkerId);
            return View(release);
        }

        // GET: Releases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Release release = db.Releases.Find(id);
            if (release == null)
            {
                return HttpNotFound();
            }
            return View(release);
        }

        // POST: Releases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Release release = db.Releases.Find(id);
            db.Releases.Remove(release);
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
