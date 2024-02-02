using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using R54_M8_Class_09_Work_01.Models;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
namespace R54_M8_Class_09_Work_01.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly AppDbContext db;
        public HomeController(AppDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index(int pg = 1, int perPage = 5)
        {
            var data = db.JobPosts
                .Where(x=> !x.IsClosed)
                .Include(x=> x.JobProvider)                
                .ToPagedList(pg, perPage);
            return View(data);
        }
    }
}
