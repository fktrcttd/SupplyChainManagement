using System;
using System.Web.Mvc;

namespace SCM.Controllers
{
    public class ExcelController : Controller
    {
        
        [HttpPost]
        public ActionResult ExportExcel(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);
            return File(fileContents, contentType, fileName);
        }
    }
}