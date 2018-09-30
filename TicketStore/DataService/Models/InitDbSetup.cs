using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TicketStore.Models;

namespace TicketStore.DataService.Models
{
    public class IdentityDbInit : DropCreateDatabaseIfModelChanges<ApplicationDataContext>
    {
        protected override void Seed(ApplicationDataContext context)
        {
            PerformInitialSetup(context);
            base.Seed(context);
        }

        private void PerformInitialSetup(ApplicationDataContext context)
        {
            var userManager = new ApplicationUserManager(new ApplicatonUserStore(context));
            var roleManager = new ApplicationRoleManager(new ApplicationRoleStore(context));

            // создаем роль администртора
            var adminRole = new ApplicationRole() {Name = "admin"};

            // добавляем ее в БД
            roleManager.Create<ApplicationRole, int>(adminRole);

            // создаем пользователей
            var admin = new ApplicationUser {Email = "georgij.al@yandex.ru", UserName = "admin"};
            string password = "admin_1ADMIN";
            var result = userManager.Create(admin, password);
            // если создание пользователя прошло успешно
            if (result.Succeeded)
                // добавляем для пользователя роль
                userManager.AddToRole(admin.Id, adminRole.Name);
        }
    }
}