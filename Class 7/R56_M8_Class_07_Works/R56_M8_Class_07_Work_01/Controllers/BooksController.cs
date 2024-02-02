using Microsoft.AspNetCore.Mvc;
using R56_M8_Class_07_Work_01.Models;
using R56_M8_Class_07_Work_01.Repositories.Interfaces;
using R56_M8_Class_07_Work_01.ViewModels.Input;

namespace R56_M8_Class_07_Work_01.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookRepo repo;
        public BooksController(IBookRepo repo)
        {
            this.repo = repo;
        }
        public async Task<IActionResult> Index()
        {
            var data = await this.repo.GetAsync(true);
           
            return View(data);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Authors = await this.repo.GetAuthorOptionsAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(BookInputModel model)
        {
            if (ModelState.IsValid)
            {
                var book = new Book
                {
                    Title=model.Title,
                    PublishDate=model.PublishDate,
                    CoverPrice=model.CoverPrice
                };
                foreach(var i in model.Authors)
                {
                    book.BookAuthors.Add(new BookAuthor { AuthorId = i });
                }
                if(await this.repo.InsertAsync(book))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Failed to save.");
                }
            }
            ViewBag.Authors = await this.repo.GetAuthorOptionsAsync();
            return View(model);
        }
    }
}
