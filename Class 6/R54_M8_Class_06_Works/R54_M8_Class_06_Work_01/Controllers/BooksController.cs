using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using R54_M8_Class_06_Work_01.Models;
using R54_M8_Class_06_Work_01.Repositories.Interfaces;
using R54_M8_Class_06_Work_01.ViewModels;
using System.Runtime.InteropServices;

namespace R54_M8_Class_06_Work_01.Controllers
{
    public class BooksController : Controller
    {
        private IUnitOfWork _unitOWork;
        IGenericRepo<Book> _repo;
        public BooksController(IUnitOfWork _unitOWork)
        {
            this._unitOWork = _unitOWork;
            this._repo = this._unitOWork.GetRepo<Book>();
        }
        public async Task<IActionResult> Index()
        {
            
            var data = await this._repo.GetAsync(x=> x.Include(b => b.BookAuthors).ThenInclude(ba=> ba.Author));
            //var modelData = data.Select(b => new BookViewModel
            //{
            //    BookId = b.BookId,
            //    Title = b.Title,
            //    PublishDate = b.PublishDate,
            //    CoverPrice = b.CoverPrice,
            //    Authors = string.Join(", ",  b.BookAuthors.Select(ba => ba.Author.Name).ToList())
            //});
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
