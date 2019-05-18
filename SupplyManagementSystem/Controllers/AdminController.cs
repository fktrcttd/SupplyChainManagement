using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using SCM.DataService.DataContext;
using SCM.Models.IdentityModels;
using SCM.ViewModels;

namespace SCM.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController: Controller
    {
        #region Users

        public ActionResult UsersList(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View("Users/Index",AppUserManager.Users.OrderBy(u => u.Id).ToPagedList(pageNumber, pageSize));
        }
        
        public ActionResult UsersRead([DataSourceRequest]DataSourceRequest request)
        {
            var context = new AppDataContext();
            var users = context.Users.ToList();
            return Json(users.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult CreateUser() => PartialView("Users/Partials/Create");

        [HttpPost]
        public async Task<ActionResult> CreateUser(CreateModel model)
        {
            if (ModelState.IsValid)
            {
                var ctx = AppDataContext.JoinOrOpen();
                var roleName = ctx.Roles.FirstOrDefault(r => r.Id == model.RoleId);
                ScmUser scmUser = new ScmUser { UserName = model.Email, Email = model.Email, Name = model.Name};
                IdentityResult result =
                    await AppUserManager.CreateAsync(scmUser, model.Password);

                if (result.Succeeded)
                    await AppUserManager.AddToRoleAsync(scmUser.Id, role: roleName?.Name);

                if (result.Succeeded)
                    return RedirectToAction("UsersList");
                else
                    AddErrorsFromResult(result);
            }
            return RedirectToAction("UsersList");
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteUser(int id)
        {
            ScmUser scmUser = await AppUserManager.FindByIdAsync(id);

            if (scmUser != null)
            {
                IdentityResult result = await AppUserManager.DeleteAsync(scmUser);
                if (result.Succeeded)
                    return RedirectToAction("UsersList");
                else
                    return View("Error", result.Errors);
            }
            else
                return View("Error", new string[] {"Пользователь не найден"});
        }
        
        public async Task<ActionResult> EditUser(int id)
        {
            ScmUser scmUser = await AppUserManager.FindByIdAsync(id);
            if (scmUser != null)
            {
                return PartialView("Users/Partials/Edit", new UpdateModel()
                {
                    Email = scmUser.Email,
                    Id = scmUser.Id,
                    RoleId = scmUser.Roles.FirstOrDefault(w => w.UserId == scmUser.Id)?.RoleId,
                    Name = scmUser.Name
                });
            }
            else
                return View("Error", new string[] {"Пользователь не найден"});
        }

        [HttpPost]
        public async Task<ActionResult> EditUser(UpdateModel model)
        {
            int id = model.Id;
            string password = model.Password;
            string email = model.Email;
            string name = model.Name;
            ScmUser scmUser = await AppUserManager.FindByIdAsync(id);
            if (scmUser != null)
            {
                int? roleId = model.RoleId;
                var ctx = AppDataContext.JoinOrOpen();
                var role = ctx.Roles.FirstOrDefault(r => r.Id == roleId);
                bool isInRole = AppUserManager.IsInRole(scmUser.Id, role?.Name);
                if (!isInRole)
                {
                    var userToRoles = scmUser.Roles;
                    List<string> roles = new List<string>();
                    foreach (var applicationUserRole in userToRoles)
                        roles.Add(ctx.Roles.FirstOrDefault(r => r.Id == applicationUserRole.RoleId)?.Name);

                    roles.RemoveAll(string.IsNullOrWhiteSpace);

                    await AppUserManager.RemoveFromRolesAsync(scmUser.Id, roles.ToArray());
                    AppUserManager.AddToRole(scmUser.Id, role?.Name);
                }
                    
                scmUser.Email = email;
                IdentityResult validEmail
                    = await AppUserManager.UserValidator.ValidateAsync(scmUser);

                if (!validEmail.Succeeded)
                    AddErrorsFromResult(validEmail);

                IdentityResult validPass = null;
                if (!string.IsNullOrEmpty(password))
                {
                    validPass
                        = await AppUserManager.PasswordValidator.ValidateAsync(password);

                    if (validPass.Succeeded)
                        scmUser.PasswordHash =
                            AppUserManager.PasswordHasher.HashPassword(password);
                    else
                        AddErrorsFromResult(validPass);
                }

                if (validEmail.Succeeded)
                {
                    scmUser.Name = name;
                    IdentityResult result = await AppUserManager.UpdateAsync(scmUser);
                    if (result.Succeeded)
                        return RedirectToAction("UsersList");
                    else
                        AddErrorsFromResult(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "Пользователь не найден");
            }
            return RedirectToAction("UsersList");
        }
        
        

        #endregion

        #region Roles

        private AppUserManager AppUserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }
        private ApplicationRoleManager RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
        }

        public ActionResult RoleList()
        {
            AppDataContext.Close();
            var manager = RoleManager;
            var roles = manager.Roles.ToList();
            
            return View("Roles/Index", roles);
        }

        public ActionResult CreateRole()
        {
            return View("Roles/Create");
        }

        [HttpPost]
        public async Task<ActionResult> CreateRole([Required]string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result
                    = await RoleManager.CreateAsync(new ScmRole(name));

                if (result.Succeeded)
                {
                    return RedirectToAction("RoleList");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View(name);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteRole(int id)
        {
            ScmRole role = await RoleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await RoleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("RoleList");
                }
                else
                {
                    return View("Error", result.Errors);
                }
            }
            else
            {
                return View("Error", new string[] { "Роль не найдена" });
            }
        }
        
        
        public async Task<ActionResult> EditRole(int id)
        {
            ScmRole role = await RoleManager.FindByIdAsync(id);
            int[] memberIDs = role.Users.Select(x => x.UserId).ToArray();

            IEnumerable<ScmUser> members
                = AppUserManager.Users.Where(x => memberIDs.Any(y => y == x.Id));

            IEnumerable<ScmUser> nonMembers = AppUserManager.Users.Except(members);

            return View("Roles/Edit", new RoleEditModel
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            });
        }

        [HttpPost]
        public async Task<ActionResult> EditRole(RoleModificationModel model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (int userId in model.IdsToAdd ?? new int[] { })
                {
                    result = await AppUserManager.AddToRoleAsync(userId, model.RoleName);

                    if (!result.Succeeded)
                    {
                        return View("Error", result.Errors);
                    }
                }
                foreach (int userId in model.IdsToDelete ?? new int[] { })
                {
                    result = await AppUserManager.RemoveFromRoleAsync(userId,
                        model.RoleName);

                    if (!result.Succeeded)
                    {
                        return View("Error", result.Errors);
                    }
                }
                return RedirectToAction("RoleList");

            }
            return View("Error", new string[] { "Роль не найдена" });
        }
        
        #endregion
        
    }
}