using Microsoft.AspNetCore.Mvc;
using R56_M8_Class_07_Work_01.Models;
using R56_M8_Class_07_Work_01.Repositories.Interfaces;

namespace R56_M8_Class_07_Work_01.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IAuthorRepo repo;
        public AuthorsController(IAuthorRepo repo)
        {
            this.repo = repo;
        }
        public async Task<IActionResult> Index()
        {
            var data = await this.repo.GetAsync(true);
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Author author)
        {
            if(ModelState.IsValid)
            {
                if(await this.repo.InsertAsync(author))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Failed to save");
                }
            }
            return View(author);
        }
    }
}
