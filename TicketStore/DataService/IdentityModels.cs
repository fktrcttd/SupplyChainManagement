using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TicketStore.Core;
using TicketStore.DataService;
using TicketStore.DataService.Models;

namespace TicketStore.Models
{
    
    // В профиль пользователя можно добавить дополнительные данные, если указать больше свойств для класса ApplicationUser. Подробности см. на странице https://go.microsoft.com/fwlink/?LinkID=317594.
  
    public class AppUser : IdentityUser<int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        
        [DisplayName("ФИО")]
        public String Name { get; set; }
    }

    public class ApplicationUserRole : IdentityUserRole<int>
    {

    }

    public class ApplicationUserLogin : IdentityUserLogin<int>
    {
    }

    public class ApplicationUserClaim : IdentityUserClaim<int>
    {
    }

    public class AppRole : IdentityRole<int, ApplicationUserRole>
    {
        public AppRole()
        {
            
        }
        public AppRole(string name) : this()
        {
            base.Name = name;
        }
    }

    public class ApplicatonUserStore :
        UserStore<AppUser, AppRole, int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicatonUserStore(AppDataContext context)
            : base(context)
        {
            
        }


    }
    
    public class ApplicationRoleStore : RoleStore<AppRole, int, ApplicationUserRole>
    {
        public ApplicationRoleStore(AppDataContext context)
            : base(context)
        {
            
        }
    }

}