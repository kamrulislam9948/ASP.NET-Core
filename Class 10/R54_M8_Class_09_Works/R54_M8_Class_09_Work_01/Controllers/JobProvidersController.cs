using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using R54_M8_Class_09_Work_01.Models;
using R54_M8_Class_09_Work_01.ViewModels.Input;
using System.Security.Claims;
using X.PagedList;

namespace R54_M8_Class_09_Work_01.Controllers
{

    public class JobProvidersController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly AppDbContext db;
        public JobProvidersController(UserManager<AppUser> userManager, AppDbContext db)
        {
            this.userManager = userManager;
            this.db = db;
        }

        public IActionResult Index(int pg = 1, int perPage = 5, string status = "open")
        {
            ViewBag.Status = status;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var data = db.JobPosts
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.LastDayOfSubmission)
               .Include(x => x.JobProvider)
               .Include(x => x.JobSeekerJobPosts).ThenInclude(x => x.JobSeeker);
            if (status == "open")
                return View(data.Where(x => !x.IsClosed).ToPagedList(pg, perPage));
            else if (status == "closed")
                return View(data.Where(x => x.IsClosed).ToPagedList(pg, perPage));
            else return View(data.ToPagedList(pg, perPage));
        }
        public async Task<IActionResult> Profile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var data = await db.JobProviders.FirstOrDefaultAsync(x => x.Id == userId);
            if (data == null) { return NotFound(); }
            return View(new JobProviderInputModel { Id = data.Id, CompanyName = data.CompanyName });
        }
        [HttpPost]
        public async Task<IActionResult> Profile(JobProviderInputModel model)
        {
            if (ModelState.IsValid)
            {
                var data = await db.JobProviders.FirstOrDefaultAsync(x => x.Id == model.Id);
                if (data == null) { return NotFound(); }
                data.CompanyName = model.CompanyName;
                data.CompanyAddress = model.CompanyAddress;
                await db.SaveChangesAsync();
                return View("Index", "JobProviders");
            }
            return View(model);
        }
        public async Task<IActionResult> UpdateStatus(int id, bool status)
        {
            var data = await db.JobPosts.FirstOrDefaultAsync(x => x.JobPostId == id);
            if (data == null) { return NotFound(); }
            data.IsClosed = status;
            await db.SaveChangesAsync();
            return Json(new { success = true });
        }
    }
}
