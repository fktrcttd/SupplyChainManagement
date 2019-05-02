using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCM.DataService.DataContext;
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
            var image = Request.Files[0] as HttpPostedFileBase;
            string uploadPath = Server.MapPath("~/Content/SampleCategoryImages");
            string newFileOne = Path.Combine(uploadPath, image.FileName);  
  
            image.SaveAs(newFileOne);  
  
            return View(model);
        }

        private void ResolveViewBag()
        {
            var context = new AppDataContext();

            var accessableChemicalElements = context.ChemicalElements.Select(element => new SelectListItem()
                {Text = element.Symbol +"-" +element.Title, Value = element.Id.ToString()});
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