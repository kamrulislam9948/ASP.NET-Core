using Microsoft.AspNetCore.Mvc;

namespace R56_M8_Class_07_Work_01.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
