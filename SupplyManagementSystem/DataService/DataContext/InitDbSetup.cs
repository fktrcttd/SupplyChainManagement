using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity;
using SCM;
using SCM.Domain.Enums;
using SCM.Models;
using SCM.Models.IdentityModels;

namespace SCM.DataService.DataContext
{
    public class IdentityDbInit : DropCreateDatabaseAlways<AppDataContext>
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
            var admin = new ScmUser {Email = "keygubinskaya@mail.ru", UserName = "keygubinskaya@mail.ru" };
            string password = "admin_1ADMIN";
            var result = userManager.Create(admin, password);
            // если создание пользователя прошло успешно
            if (result.Succeeded)
                // добавляем для пользователя роль
                userManager.AddToRole(admin.Id, adminRole.Name);

            InitSampleCategories(context);
        }


        public void InitSampleCategories(AppDataContext context)
        {
            if (context.SampleCategories.Any())
                return;
            var steel = new SampleCategory();
            steel.Title = "Стали";
            steel.SampleCategoryType = SampleCategoryType.ChemicalAnalysis;
            
            var castIron = new SampleCategory();
            castIron.Title = "Чугуны";
            castIron.SampleCategoryType = SampleCategoryType.ChemicalAnalysis;
            
            var nicelBased = new SampleCategory();
            nicelBased.Title = "Сплавы на никелевой основе";
            nicelBased.SampleCategoryType = SampleCategoryType.ChemicalAnalysis;
            context.SampleCategories.Add(steel);
            context.SampleCategories.Add(nicelBased);
            context.SampleCategories.Add(castIron);
            context.SaveChanges();
        }
    }
}