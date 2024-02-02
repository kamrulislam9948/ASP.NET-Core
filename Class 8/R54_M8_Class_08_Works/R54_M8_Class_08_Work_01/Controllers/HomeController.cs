using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using R54_M8_Class_08_Work_01.Models;
using R54_M8_Class_08_Work_01.Repositories.Intefaces;

namespace R54_M8_Class_08_Work_01.Controllers
{
    public class HomeController : Controller
    {
        

        public  IActionResult Index()
        {
            return View();
        }
    }
}
