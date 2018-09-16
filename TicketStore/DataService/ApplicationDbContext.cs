using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TicketStore.Models;

namespace TicketStore.DataService
{
    public class ApplicationDbContext
        : IdentityDbContext<ApplicationUser, ApplicationRole, int, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {

        public ApplicationDbContext()
            : base("DefaultConnection")
        {

        }

        static ApplicationDbContext()
        {
            Database.SetInitializer(new IdentityDbInit());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<ApplicationRole>().ToTable("Roles");
            modelBuilder.Entity<ApplicationUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<ApplicationUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<ApplicationUserLogin>().ToTable("UserLogin");
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }

    public class IdentityDbInit : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            PerformInitialSetup(context);
            base.Seed(context);
        }

        private void PerformInitialSetup(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new ApplicatonUserStore(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
 
            // создаем роль администртора
            var adminRole = new IdentityRole { Name = "admin" };
 
            // добавляем ее в БД
            roleManager.Create(adminRole);
 
            // создаем пользователей
            var admin = new ApplicationUser { Email = "georgij.al@yandex.ru", UserName = "admin" };
            string password = "admin_1ADMIN";
            var result = userManager.Create(admin, password);
            // если создание пользователя прошло успешно
            if (result.Succeeded)
                // добавляем для пользователя роль
                userManager.AddToRole(admin.Id, adminRole.Name);
        }
    }
}