using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using SCM.DataService.DataContext;
using SCM.Models;

namespace SCM.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Init();

            return View();
        }

        private void Init()
        {
            var db = new AppDataContext();
            if (db.ChemicalElements.Count() < 4)
            {
                string path = Server.MapPath("~/Content/Parse/elements.txt");
                var elements = ParseJsonHelper.GetChemicalElements(path)
                    .Select(el => new ChemicalElement {Title = el.name, Symbol = el.symbol});

                foreach (var el in elements)
                {
                    db.ChemicalElements.Add(el);
                }

                db.SaveChanges();
            }

//            if (!db.ChemicalCompositions.Any())
//            {
//                var newChemicalComposition = new ChemicalComposition();
//                newChemicalComposition.Title = "тестовый состав";
//                newChemicalComposition.Index = "тс1";
//                newChemicalComposition.SampleCategoryId = 4;
//                db.ChemicalCompositions.Add(newChemicalComposition);
//                db.SaveChanges();
//
//                newChemicalComposition.CompositionsElements = db.ChemicalElements
//                    .Where(e => e.Id < 20).ToList()
//                    .Select(e => new CompositionsElement()
//                    {
//                        Percentage = 2,
//                        ChemicalElementId = e.Id,
//                        ChemicalCompositionId = newChemicalComposition.Id
//                    })
//                    .ToList();
//                db.SaveChanges();
//            }
        }

        public ActionResult About()
        {
            ViewBag.KendoRequired = true;
            ViewBag.Message = "Your application description page.";
            return View();
        }
        
        public ActionResult TestKendo([DataSourceRequest]DataSourceRequest request)
        {
            
            var context = new AppDataContext();
            var users = context.Users.ToList();
            return Json(users.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
        public ActionResult News()
        {
          
            return View();
        }
        
        /*public ActionResult Customers_Read([DataSourceRequest]DataSourceRequest request)
        {
            return Json(GetCustomers().ToDataSourceResult(request));
        }*/
    }
}