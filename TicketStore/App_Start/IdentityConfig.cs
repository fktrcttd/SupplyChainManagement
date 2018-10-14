using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Owin;
using TicketStore.DataService;
using TicketStore.Models;

namespace TicketStore
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Подключите здесь службу электронной почты для отправки сообщения электронной почты.
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Подключите здесь службу SMS, чтобы отправить текстовое сообщение.
            return Task.FromResult(0);
        }
    }
    
    public class ApplicationRoleManager : RoleManager<AppRole, int>
    {
        public ApplicationRoleManager(IRoleStore<AppRole,int> roleStore)
            : base(roleStore) { }

        public static ApplicationRoleManager Create(
            IdentityFactoryOptions<ApplicationRoleManager> options, 
            IOwinContext context)
        {
            var manager = new ApplicationRoleManager(
                new ApplicationRoleStore(context.Get<AppDataContext>()));
            return manager;
        }
    }
    // Настройка диспетчера пользователей приложения. UserManager определяется в ASP.NET Identity и используется приложением.
    public class AppUserManager : UserManager<AppUser, int>
    {
        public AppUserManager(IUserStore<AppUser, int> store)
            : base(store)
        {
            
        }

        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options, IOwinContext context) 
        {
            var manager =new AppUserManager(
                new ApplicatonUserStore(context.Get<AppDataContext>())); 
            // Настройка логики проверки имен пользователей
            manager.UserValidator = new UserValidator<AppUser, int>(manager) 
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Настройка логики проверки паролей
            manager.PasswordValidator = new PasswordValidator
            {/*
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
*/
            };

            // Настройка параметров блокировки по умолчанию
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Регистрация поставщиков двухфакторной проверки подлинности. Для получения кода проверки пользователя в данном приложении используется телефон и сообщения электронной почты
            // Здесь можно указать собственный поставщик и подключить его.
            manager.RegisterTwoFactorProvider("Код, полученный по телефону", new PhoneNumberTokenProvider<AppUser, int> 
            {
                MessageFormat = "Ваш код безопасности: {0}"
            });
            manager.RegisterTwoFactorProvider("Код из сообщения", new EmailTokenProvider<AppUser, int> 
            {
                Subject = "Код безопасности",
                BodyFormat = "Ваш код безопасности: {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = 
                    new DataProtectorTokenProvider<AppUser, int>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Настройка диспетчера входа для приложения.
    public class ApplicationSignInManager : SignInManager<AppUser, int>
    {
        public ApplicationSignInManager(AppUserManager appUserManager, IAuthenticationManager authenticationManager)
            : base(appUserManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(AppUser appUser)
        {
            return appUser.GenerateUserIdentityAsync((AppUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<AppUserManager>(), context.Authentication);
        }
    }
    
    
}
