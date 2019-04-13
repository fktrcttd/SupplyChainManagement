using Microsoft.AspNet.Identity.EntityFramework;
using SCM.DataService.DataContext;
using SCM.DataService;

namespace SCM.Models.IdentityModels
{
    public class ApplicationRoleStore : RoleStore<ScmRole, int, ScmUserRole>
    {
        public ApplicationRoleStore(AppDataContext context)
            : base(context)
        {
            
        }
    }
}