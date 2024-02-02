using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using R54_M8_Class_09_Work_01.Models;
using R54_M8_Class_09_Work_01.ViewModels.Input;

namespace R54_M8_Class_09_Work_01.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AppUser> userManager;
        public AccountController(SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterInputModel model)
        {
            if(ModelState.IsValid)
            {
                var user = new AppUser {  UserName=model.UserName };
                var result = await userManager.CreateAsync(user, model.Password ?? "");
                if(result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", "Registration failed");
                }
            }
            return View(model);
        }
        public IActionResult Login()
        {
            return View();
        }
        [Authorize]
        public IActionResult Logout()
        {
            return View();
        }
    }
}
