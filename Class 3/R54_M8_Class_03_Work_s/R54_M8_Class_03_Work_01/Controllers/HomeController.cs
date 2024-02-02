using R54_M8_Class_03_Work_01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace R54_M8_Class_03_Work_01.Controllers
{
    public class HomeController : Controller
    {
        TestDbContext db = new TestDbContext();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Test()
        {
            db.Employees.Add(new RegularEmployee { Name = "ABAC", Post = "AA" });
            db.Employees.Add(new Teacher { Name = "ABA", Subject = "AA" });
            db.SaveChanges();
            return View(db.Employees.ToList()); ;
        }
    }
}