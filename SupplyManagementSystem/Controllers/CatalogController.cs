using System.Linq;
using System.Web.Mvc;
using SCM.DataService.DataContext;
using SCM.ViewModels;

namespace SCM.Controllers
{
    public class CatalogController : Controller
    {
        private AppDataContext _context;
        public CatalogController()
        {
            _context = new AppDataContext();
        }
        // GET
        public ActionResult Index()
        {
            var categories = _context.SampleCategories.ToList();
            var vm = new CatalogViewModel();
            vm.SampleCategories = categories;
            return View(vm);
        }


        public ActionResult Category(int id)
        {
            var category = _context.SampleCategories.FirstOrDefault(sampleCategory => sampleCategory.Id == id);
            var compositions = _context.ChemicalCompositions.Where(c => c.SampleCategoryId == category.Id).ToList();

            ViewBag.CategoryId = id;
            ViewBag.CategoryName = category.Title;
            return View(compositions);
        }

        
    }
}