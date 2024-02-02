using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using R54_M8_Class_09_Work_01.Models;
using R54_M8_Class_09_Work_01.ViewModels.Input;

namespace R54_M8_Class_09_Work_01.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AppUser> userManager;
        private readonly AppDbContext db;
        public AccountController(AppDbContext db, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            this.db = db;
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
            if (ModelState.IsValid)
            {
                if (model.IsJobProvider)
                {
                    var user = new JobProvider { UserName = model.UserName };
                    var result = await userManager.CreateAsync(user, model.Password ?? "");
                    if (result.Succeeded)
                    {
                        if (model.IsJobProvider) await userManager.AddToRoleAsync(user, "JobProvider");
                        else await userManager.AddToRoleAsync(user, "JobSeeker");
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Registration failed");
                    }
                }
                else
                {
                    var user = new JobSeeker { UserName = model.UserName };
                    var result = await userManager.CreateAsync(user, model.Password ?? "");
                    if (result.Succeeded)
                    {
                        
                        await userManager.AddToRoleAsync(user, "JobSeeker");
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Registration failed");
                    }
                }
                   
            }
            return View(model);
        }
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginInputModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.UserName ?? "");
                if (user == null)
                {
                    ModelState.AddModelError("", "User does not exist");

                }
                else
                {

                    var result = await signInManager.PasswordSignInAsync(model.UserName ?? "", model.Password ?? "", false, false);
                    if (result.Succeeded)
                    {
                        if (await userManager.IsInRoleAsync(user, "JobProvider"))
                        {
                            var jp = await db.Users.OfType<JobProvider>().FirstOrDefaultAsync(x => x.Id == user.Id);
                            if (jp == null || (string.IsNullOrEmpty(jp.CompanyName) || string.IsNullOrEmpty(jp.CompanyAddress) ))
                            {
                                return RedirectToAction("Profile", "JobProviders");
                            }
                            else
                            {
                                return RedirectToAction("Index", "JobProviders");
                            }
                           
                            

                        }
                        if (await userManager.IsInRoleAsync(user, "JobSeeker"))
                        {
                            var jb = await db.Users.OfType<JobSeeker>().FirstOrDefaultAsync(x => x.Id == user.Id);
                            if (jb != null && (string.IsNullOrEmpty(jb.FullName) || string.IsNullOrEmpty(jb.ContactNumber) || jb.BirthDate == null))
                            {
                                return RedirectToAction("Profile", "JobSeekers");
                            }
                            else
                            {
                                return RedirectToAction("Index", "Home");
                            }
                            
                        }
                    }
                }
            }

            return View(model);
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
