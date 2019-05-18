using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SCM.DataService.DataContext;

namespace SCM.Models.IdentityModels
{
    public class ScmUser : IdentityUser<int, ApplicationUserLogin, ScmUserRole, ApplicationUserClaim>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ScmUser, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        
        [DisplayName("ФИО")]
        public String Name { get; set; }

        [NotMapped] public string RoleAsString => GetRole();

        private string GetRole()
        {
            var ctx = AppDataContext.JoinOrOpen();
            var roleId = this.Roles.FirstOrDefault()?.RoleId;
            var role = ctx.Roles.FirstOrDefault(r => r.Id == roleId);
            return role != null ? role.Name : "Не определена";
        }
    }
}