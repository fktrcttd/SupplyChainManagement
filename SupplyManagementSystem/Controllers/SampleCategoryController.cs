using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Kaliko.ImageLibrary;
using SCM.DataService.DataContext;
using SCM.Models;
using SCM.ViewModels.SampleCategory;
using WebApp.Infrastructure;

namespace SCM.Controllers
{
    [Authorize]
    public class SampleCategoryController : Controller
    {
        public ActionResult Create()
        {
            var vm = new CreateSampleCategoryViewModel();
            return View(vm);
        }
        
        
        [HttpPost]
        public ActionResult Create(CreateSampleCategoryViewModel model)
        {
            var context = new AppDataContext();
            var elements = model.BaseChemicalElementId != null && model.BaseChemicalElementId.Any()
                ? context.ChemicalElements.Where(e => model.BaseChemicalElementId.Contains(e.Id)).ToList() : new List<ChemicalElement>();
            var category = new SampleCategory
            {
                Title = model.Title, ChemicalElements = elements, ImageLink = model.ImageLink
            };
            context.Add(category);
            context.Commit();
            return RedirectToAction("Index", "Catalog");
        }

        private void ResolveViewBag()
        {
            var context = new AppDataContext();

            var accessableChemicalElements = context.ChemicalElements.Select(element => new SelectListItem()
                {Text = element.Symbol +"-" +element.Title, Value = element.Id.ToString()}).OrderBy(option => option.Text);
            var chemicalElementsMultiSelect = new MultiSelectList(accessableChemicalElements, "Value", "Text");
            ViewBag.ChemicalElementsMultiSelect = chemicalElementsMultiSelect;
        }


        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            ResolveViewBag();
            base.OnResultExecuting(filterContext);
        }
    }
}