using Microsoft.AspNet.Identity.EntityFramework;

namespace SCM.Models.IdentityModels
{
    public class ScmRole : IdentityRole<int, ScmUserRole>
    {
        public ScmRole()
        {
            
        }
        public ScmRole(string name) : this()
        {
            base.Name = name;
        }
    }
}