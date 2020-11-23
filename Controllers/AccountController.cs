using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AURA.Data;
using AURA.Models;
using AURA.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AURA.Controllers
{
    //[Authorize]
    public class AccountController : Controller
    {
        private readonly PostContext _context;

        private readonly UserManager<IdentityUser> UserManager;

        private readonly SignInManager<IdentityUser> SignInManager;
        private readonly RoleManager<ApplicationRole> RoleManager;

        public AccountController(RoleManager<ApplicationRole> roleManager, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }

        public async Task<IActionResult> Register()
        {
            RegisterViewModel newRegister = new RegisterViewModel();
            var allRoles = RoleManager.Roles.ToList();

            newRegister.RoleList = new List<SelectListItem>();
            foreach (var role in allRoles)
            {
                newRegister.RoleList.Add(new SelectListItem()
                {
                    Text = role.Name,
                    Value = role.Id
                });
            }
            return View(newRegister);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = registerViewModel.Email,
                    Email = registerViewModel.Email,
                    EmailConfirmed = true
                };

                var result = await UserManager.CreateAsync(user, registerViewModel.Password);

                //Update Role
                //var roleDetail = await RoleManager.FindByIdAsync(registerViewModel.Role);
                //await UserManager.RemoveFromRolesAsync(user, UserManager.GetRolesAsync(user).Result.ToArray());
                //await UserManager.AddToRoleAsync(user, roleDetail.NormalizedName);
                //await UserManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = registerViewModel.Email,
                    Email = registerViewModel.Email,
                };
                var result = await SignInManager.PasswordSignInAsync(registerViewModel.Email, registerViewModel.Password, true, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid Credentials!");
            }
            return View(registerViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        public void AddRole(string roleName)
        {
            if (!string.IsNullOrEmpty(roleName))
            {
                ApplicationRole newRole = new ApplicationRole()
                {
                    Name = roleName,
                    NormalizedName = roleName.ToUpper()
                };
                var roleCreated = RoleManager.CreateAsync(newRole);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword(ForgotPasswordViewModel model, bool success)
        {
            model = model ?? new ForgotPasswordViewModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    model.Message = "Please enter valid email to reset your password. ";
                    return View(model);
                }
                if (string.IsNullOrEmpty(model.Password))
                {
                    model.IsValidEmail = true;
                    return RedirectToAction(nameof(ForgotPassword), model);
                }

                var code = await UserManager.GeneratePasswordResetTokenAsync(user);

                var result = await UserManager.ResetPasswordAsync(user, code, model.Password);
                if (result.Succeeded)
                {
                    user.PasswordHash = UserManager.PasswordHasher.HashPassword(user, model.Password);
                    var success = UserManager.UpdateAsync(user);
                    if (success.Result.Succeeded)
                    {
                        model.Success = true;
                        model.Message = "Your password has been reset. Please go to login Page.";
                        return RedirectToAction(nameof(ForgotPassword), model);
                    }
                }
                AddErrors(result);

                return RedirectToAction(nameof(ForgotPassword), new { success = true });
            }
            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation(bool success, string message)
        {
            var viewModel = new ResetPasswordViewModel()
            {
                Success = success,
                Message = message
            };
            return View("ResetPassword", viewModel);
        }
    }
}
