using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kaliko.ImageLibrary;
using SCM.DataService.DataContext;
using SCM.Models;
using SCM.ViewModels.SampleCategory;

namespace SCM.Controllers
{
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
            var category = new SampleCategory();
            category.Title = model.Title;
            category.ChemicalElements = elements;
            var image = model.ImageFile;
            string uploadPath = Server.MapPath("~/Content/Images/SampleCategoryImages");
            string newFileOne = Path.Combine(uploadPath, image.FileName);  
            image.SaveAs(newFileOne);
            
            var processingImage = new KalikoImage(Path.Combine(uploadPath, image.FileName));
            processingImage.Resize(600, 400);
            processingImage.SaveImage(Path.Combine(uploadPath, category.Title+".jpeg"), ImageFormat.Jpeg);
            category.ImageName = category.Title + ".jpeg";
            context.Add(category);
            context.Commit();
            return View(new CreateSampleCategoryViewModel());
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