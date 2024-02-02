using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using R54_M8_Class_08_Work_01.Models;
using R54_M8_Class_08_Work_01.Repositories.Intefaces;
using R54_M8_Class_08_Work_01.ViewModels.Input;

namespace R54_M8_Class_08_Work_01.Controllers
{
    public class BooksController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IGenericRepo<Book> repo;
        public BooksController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.repo = this.unitOfWork.GetRepo<Book>();
        }
        public async Task<IActionResult> Index()
        {
            var data =await this.repo.GetAllAsync(x=> x.Include(b=> b.BookAuthors).ThenInclude(ba=> ba.Author));
            return View(data);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Authors = await this.repo.ExecuteSqlCollection<Author>("SELECT * FROM dbo.Authors");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(BookInputModel model)
        {
            if(ModelState.IsValid)
            {
                var book = new Book
                {
                    Title = model.Title,
                    PublishDate = model.PublishDate,
                    CoverPrice = model.CoverPrice
                };
                foreach (var i in model.Authors)
                {
                    book.BookAuthors.Add(new BookAuthor { AuthorId = i });
                }
                await this.repo.InsertAsync(book);
                if (await this.unitOfWork.SaveAsync())
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Failed to save.");
                }
            }
            ViewBag.Authors = await this.repo.ExecuteSqlCollection<Author>("SELECT * FROM dbo.Authors");
            return View(model);
        }
    }
}
