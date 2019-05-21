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
using SCM.ViewModels.Order;
using SCM.ViewModels.Samples;

namespace SCM.Controllers
{
    public class OrdersController : Controller
    {
        private AppDataContext db = new AppDataContext();

        // GET: Orders
        public ActionResult Index(int? clientId)
        {
            var dbOrders = db.Orders.AsQueryable();
            var client = db.Clients.FirstOrDefault(c => c.Id == clientId);
            var clientName =client == null? null : $"{client?.Name} - {client?.Organization}";
            if (clientId.HasValue) dbOrders = dbOrders.Where(o => o.ClientId == clientId);

            var orders = dbOrders.Select(s => new OrderGridModel()
            {
                Id = s.Id,
                Date = s.Date,
                Title = s.Title,
                IsFinished = s.IsFinished,
                ClientOrganization = s.Client.Organization
            }).ToList();

            ViewBag.ClientId = clientId;
            ViewBag.ClientName = clientName;
            return View(orders);
        }

        public ActionResult OrdersRead([DataSourceRequest] DataSourceRequest request, int? clientId)
        {
            ViewBag.ClientId = clientId;
            var dbOrders = db.Orders.AsQueryable();
            if (clientId.HasValue) dbOrders = dbOrders.Where(o => o.ClientId == clientId);
            
            var orders = dbOrders.Select(s => new OrderGridModel()
            {
                Id = s.Id,
                Date = s.Date,
                Title = s.Title,
                IsFinished = s.IsFinished,
                ClientOrganization = s.Client.Organization
            }).ToList();
            return Json(orders.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }


        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.ClientId =
                new SelectList(db.Clients.ToList().Select(s => new {Id = s.Id, Name = $"{s.Name} - {s.Organization}"}),
                    "Id", "Name");


            var accessableSamples = db.Samples.Where(s => s.Amount >= 1).Select(sample => new SelectListItem()
                {Text = sample.Title, Value = sample.Id.ToString()}).OrderBy(option => option.Text);
            var samples = new MultiSelectList(accessableSamples, "Value", "Text");

            ViewBag.SampleId = samples;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStageOne(SimpleOrderViewModel order)
        {
            ViewBag.ClientId =
                new SelectList(db.Clients.ToList().Select(s => new {Id = s.Id, Name = $"{s.Name} - {s.Organization}"}),
                    "Id", "Name");

            var accessableSamples = db.Samples.Where(s => s.Amount >= 1).Select(sample => new SelectListItem()
                {Text = sample.Title, Value = sample.Id.ToString()}).OrderBy(option => option.Text);
            var samplesId = new MultiSelectList(accessableSamples, "Value", "Text");

            ViewBag.SampleId = samplesId;
            if (!ModelState.IsValid)
                return PartialView("Partials/CreateFirstStep", order);

            var client = db.Clients.FirstOrDefault(c => c.Id == order.ClientId);
            var samples = db.Samples.Where(s => order.SampleId.Contains(s.Id)).Select(s => new SampleOrderModel
            {
                InStock = s.Amount,
                Amount = 1,
                Title = s.Title,
                SampleId = s.Id
            }).ToList();
            var fullOrderModel = new FullOrderViewModel();
            fullOrderModel.Samples = samples;
            fullOrderModel.Title = order.Title;
            fullOrderModel.CreationDateTime = DateTime.Now;
            fullOrderModel.ClientId = order.ClientId;
            fullOrderModel.ClientTitle = $"{client.Name} - {client.Organization}";

            return PartialView("Partials/CreateSecondStep", fullOrderModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStageTwo(FullOrderViewModel model)
        {
            var valid = true;
            List<string> errored = new List<string>();
            foreach (var sample in model.Samples)
            {
                var dbSample = db.Samples.FirstOrDefault(s => s.Id == sample.SampleId);
                if (dbSample.Amount < sample.Amount)
                {
                    errored.Add(sample.Title);
                    valid = false;
                }
            }

            var concatErrored = string.Join(", ", errored);
            if (!valid)
            {
                var insertion = concatErrored.Length == 1 ? "имеет" : "имеют";
                ViewBag.Message = $"Образцы {concatErrored} {insertion} неверное количество!";
                ModelState.AddModelError("Samples", "Какой-то из образцов имеет недопустимое количество");
            }

            if (!ModelState.IsValid)
                return PartialView("Partials/CreateSecondStep", model);

            var order = new Order();
            order.Title = model.Title;
            order.Client = db.Clients.FirstOrDefault(c => c.Id == model.ClientId);
            order.Date = DateTime.Now;
            db.Orders.Add(order);
            db.SaveChanges();

            foreach (var sample in model.Samples)
            {
                var dbSample = db.Samples.FirstOrDefault(s => s.Id == sample.SampleId);
                dbSample.Amount = dbSample.Amount - sample.Amount;
                var link = new OrdersSample();
                link.Order = order;
                link.OrderId = order.Id;
                link.Sample = dbSample;
                link.SampleId = dbSample.Id;
                link.Amount = sample.Amount;
                db.Add(link);
            }

            db.SaveChanges();

            var successMarkup = "<div class=\"container\"><h3>Успешно!</h3>" +
                                "<p>Заказ поступил в обработку.</p></div>";
            return Content(successMarkup);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }

            ViewBag.ClientId = new SelectList(db.Clients, "Id", "Name", order.ClientId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Date,IsFinished,ClientId")]
            Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(db.Clients, "Id", "Name", order.ClientId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);

            var links = db.OrdersSamples.Where( r=> r.OrderId == order.Id).ToList();
            foreach (var link in links) db.OrdersSamples.Remove(link);
            db.Orders.Remove(order);
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