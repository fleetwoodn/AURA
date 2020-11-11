using System.Threading.Tasks;
using AURA.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AURA.Controllers
{
    //[Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> UserManager;

        private readonly SignInManager<IdentityUser> SignInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
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
    }
}
