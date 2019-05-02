using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using SCM.DataService.DataContext;
using SCM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCM.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Products_Read([DataSourceRequest] DataSourceRequest request)
        //{
        //    return Json(productService.Read().ToDataSourceResult(request));
        //}

        public ActionResult UsersRead ([DataSourceRequest] DataSourceRequest request)
        {
            var context = new AppDataContext();
            var users = context.Users.Select(u => new CreateModel() {Email = u.Email, Name = u.Name, Password = u.PasswordHash }).AsEnumerable().ToDataSourceResult(request);
            return Json(users);

        }
    }
}