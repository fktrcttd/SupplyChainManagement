using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using SCM.DataService.DataContext;
using SCM.Models;
using SCM.ViewModels.Clients;
using SCM.ViewModels.Order;

namespace SCM.Controllers
{
    public class ClientsController : Controller
    {
        private AppDataContext db = new AppDataContext();

        // GET: Clients
        public ActionResult Index()
        {
            var clients = db.Clients.Select(c => new ClientGridModel()
            {
                Id = c.Id,
                Email = c.Email,
                Name = c.Name,
                Organization = c.Organization,
                Phone = c.Phone,
                Check = c.Check,
                Inn = c.Inn
            }).ToList();
            return View(clients);
        }

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Organization,Inn,Check,Email,Phone,DeleteForbidden,EditForbidden,Title,IsPublish,LastModifed")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(client);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Organization,Inn,Check,Email,Phone,DeleteForbidden,EditForbidden,Title,IsPublish,LastModifed")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ClientsRead([DataSourceRequest]DataSourceRequest request)
        {
            var clients = db.Clients.Select(c => new ClientGridModel()
            {
                Id = c.Id,
                Email = c.Email,
                Name = c.Name,
                Organization = c.Organization,
                Phone = c.Phone,
                Check = c.Check,
                Inn = c.Inn
            }).ToList();
            return Json(clients.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        
        public ActionResult Order()
        {
            return PartialView("Partials/Request");
        }
        
        [HttpPost]
        public ActionResult Order(OrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorMarkup = "<div class=\"container\"><h3>Ошибка!</h3>"+"<p>Проверьте введенные данные и повторите отправку</p></div>";
                return Content(errorMarkup);
            }
            
            
            //standartnye.obraztsy@yandex.ru
            //sormSORM

            var manager = db.Users.FirstOrDefault(u => u.Email == "standartnye.obraztsy@yandex.ru");
            
            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress("standartnye.obraztsy@yandex.ru", "Автоматическая рассылка ОСМ");
            // кому отправляем
            MailAddress to = new MailAddress(manager.Email);
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = "Поступила новая заявка";
            // текст письма
            m.Body = $"<h1>Поступила новая заявка:&nbsp;</h1>" +
                     "<ul>"+
                        $"<li>Город: {model.City}</li>"+
                        $"<li>Имя заказчика: {model.Name}</li>"+
                        $"<li>Номер телефона: {model.Phone}</li>"+
                        $"<li>Комментари к заказу: {model.Comment}</li>"+
                    "</ul>";
            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 587);
            // логин и пароль
            smtp.Credentials = new NetworkCredential("standartnye.obraztsy@yandex.ru", "sormSORM");
            smtp.EnableSsl = true;
            
            //закомментировать и письма не будут отправляться
            smtp.Send(m);
            
            var client = new Client();
            client.Name = model.Name;
            client.Phone = model.Phone;
            client.Organization = "Неизвестно";

            db.Clients.Add(client);
            db.SaveChanges();
            var markup = "<div class=\"container\"><h3>Спасибо за заявку!</h3>"+"<p>Менеджер скоро с вами свяжется!</p></div>";
            return Content(markup);
        }
        
    }
}
