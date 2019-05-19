using System.Linq;
using System.Web.Mvc;
using SCM.DataService.DataContext;

namespace SCM.Controllers
{
    public class ChemicalElementController : Controller
    {
        // GET
        public ActionResult Index()
        {
            var context = new AppDataContext();
            var elements = context.ChemicalElements.ToList();
            return View();
        }
    }
}