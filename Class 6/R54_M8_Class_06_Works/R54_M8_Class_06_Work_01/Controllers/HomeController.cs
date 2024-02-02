using Microsoft.AspNetCore.Mvc;

namespace R54_M8_Class_06_Work_01.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
