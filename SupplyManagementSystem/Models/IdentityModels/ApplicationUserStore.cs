using Microsoft.AspNet.Identity.EntityFramework;
using SCM.DataService.DataContext;
using SCM.DataService;

namespace SCM.Models.IdentityModels
{
    public class ApplicatonUserStore :
        UserStore<ScmUser, ScmRole, int, ApplicationUserLogin, ScmUserRole, ApplicationUserClaim>
    {
        public ApplicatonUserStore(AppDataContext context)
            : base(context)
        {
            
        }
    }
}