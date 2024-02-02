using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using R54_M8_Class_09_Work_01.Models;
using R54_M8_Class_09_Work_01.ViewModels.Input;
using System.Security.Claims;
using X.PagedList;
using X.PagedList.Mvc;

namespace R54_M8_Class_09_Work_01.Controllers
{
    public class JobSeekersController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly AppDbContext db;
        public JobSeekersController(UserManager<AppUser> userManager, AppDbContext db)
        {
            this.userManager = userManager;
            this.db = db;
        }
        public  ActionResult Index()
        {
          
            return View();
        }
        public async Task<IActionResult> Profile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var data = await db.JobSeekers.FirstOrDefaultAsync(x => x.Id == userId);
            if (data == null) { return NotFound(); }
            return View(new JobSeekerInputModel {  Id=data.Id, FullName=data.FullName, ContactNumber=data.ContactNumber, BirthDate=data.BirthDate});
        }
        [HttpPost]
        public async Task<IActionResult> Profile(JobSeekerInputModel model)
        {
            if (ModelState.IsValid)
            {
                var data = await db.Users.OfType<JobSeeker>().FirstOrDefaultAsync(x => x.Id == model.Id);
                if (data == null) { return NotFound(); }
                data.FullName = model.FullName;
                data.ContactNumber = model.ContactNumber;
                data.BirthDate = model.BirthDate;
                await db.SaveChangesAsync();
                return View("Index", "JobSeekers");
            }
            return View(model);
        }
    }
}
