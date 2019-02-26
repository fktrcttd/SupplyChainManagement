using System.Data.Entity;
using Microsoft.AspNet.Identity;
using SCM;
using SCM.Models.IdentityModels;

namespace SCM.DataService.DataContext
{
    public class IdentityDbInit : DropCreateDatabaseIfModelChanges<AppDataContext>
    {
        protected override void Seed(AppDataContext context)
        {
            PerformInitialSetup(context);
            base.Seed(context);
        }

        private void PerformInitialSetup(AppDataContext context)
        {
            var userManager = new AppUserManager(new ApplicatonUserStore(context));
            var roleManager = new ApplicationRoleManager(new ApplicationRoleStore(context));

            // создаем роль администртора
            var adminRole = new ScmRole() {Name = "admin"};

            // добавляем ее в БД
            roleManager.Create<ScmRole, int>(adminRole);

            // создаем пользователей
            var admin = new ScmUser {Email = "georgij.al@yandex.ru", UserName = "admin"};
            string password = "admin_1ADMIN";
            var result = userManager.Create(admin, password);
            // если создание пользователя прошло успешно
            if (result.Succeeded)
                // добавляем для пользователя роль
                userManager.AddToRole(admin.Id, adminRole.Name);
        }
    }
}