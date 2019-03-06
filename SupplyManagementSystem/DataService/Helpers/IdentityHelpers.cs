using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using SCM;

namespace SCM.DataService.Helpers
{
    public static class IdentityHelpers
    {
        public static MvcHtmlString GetUserName(this HtmlHelper html, int id)
        {
            AppUserManager mgr = HttpContext.Current
                .GetOwinContext().GetUserManager<AppUserManager>();

            return new MvcHtmlString(mgr.FindByIdAsync(id).Result.UserName);
        }
    }
}