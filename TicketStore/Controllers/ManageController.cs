﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using TicketStore.Models;

namespace TicketStore.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private AppUserManager _appUserManager;

        public ManageController()
        {
        }

        public ManageController(AppUserManager appUserManager, ApplicationSignInManager signInManager)
        {
            AppUserManager = appUserManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public AppUserManager AppUserManager
        {
            get
            {
                return _appUserManager ?? HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
            private set
            {
                _appUserManager = value;
            }
        }

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Ваш пароль изменен."
                : message == ManageMessageId.SetPasswordSuccess ? "Пароль задан."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Настроен поставщик двухфакторной проверки подлинности."
                : message == ManageMessageId.Error ? "Произошла ошибка."
                : message == ManageMessageId.AddPhoneSuccess ? "Ваш номер телефона добавлен."
                : message == ManageMessageId.RemovePhoneSuccess ? "Ваш номер телефона удален."
                : "";

            var userId = User.Identity.GetUserId();
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await AppUserManager.GetPhoneNumberAsync(User.Identity.GetUserId<int>()),
                TwoFactor = await AppUserManager.GetTwoFactorEnabledAsync(User.Identity.GetUserId<int>()),
                Logins = await AppUserManager.GetLoginsAsync(User.Identity.GetUserId<int>()),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };
            return View(model);
        }

       
       
        
        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            IdentityResult result = await AppUserManager.ChangePasswordAsync(
                User.Identity.GetUserId<int>(),
                model.OldPassword, 
                model.NewPassword);
            if (result.Succeeded)
            {
                var user = await AppUserManager.FindByIdAsync(User.Identity.GetUserId<int>()); 
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await AppUserManager.AddPasswordAsync(User.Identity.GetUserId<int>(), model.NewPassword); 
                if (result.Succeeded)
                {
                    var user = await AppUserManager.FindByIdAsync(User.Identity.GetUserId<int>());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // Это сообщение означает наличие ошибки; повторное отображение формы
            return View(model);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing && _appUserManager != null)
            {
                _appUserManager.Dispose();
                _appUserManager = null;
            }

            base.Dispose(disposing);
        }

        #region Вспомогательные приложения
        // Используется для защиты от XSRF-атак при добавлении внешних имен входа
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = AppUserManager.FindById(User.Identity.GetUserId<int>());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = AppUserManager.FindById(User.Identity.GetUserId<int>());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

#endregion
    }
}