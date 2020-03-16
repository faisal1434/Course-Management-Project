using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CourseManagemane_Project.Models;
using CourseManagemane_Project.Data;

namespace CourseManagemane_Project.Controllers
{
    public class HomeController : Controller
    {
        StudentDbContext db;
        ApplicationDbContext appDb;
        public HomeController(StudentDbContext db, ApplicationDbContext appDb)
        {
            this.db = db;
            this.appDb = appDb;
            this.db.Database.EnsureCreated();
            this.appDb.Database.EnsureCreated();
            if (!db.Courses.Any()) Seed();
        }
        public IActionResult Index()
        {
            this.db.Database.EnsureCreated();
            this.appDb.Database.EnsureCreated();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private void Seed()
        {

            db.Courses.AddRange(new Course[]
            {
                    new Course{CourseName="C#",CourseFee=7500, Duration=6, StartDate=DateTime.Parse("2018-11-11")},
                    new Course{CourseName="C++",CourseFee=5000, Duration=7, StartDate=DateTime.Parse("2019-1-1")},
                    new Course{CourseName="NT",CourseFee=6000, Duration=5, StartDate=DateTime.Parse("2017-12-12")},
                    new Course{CourseName="JAVA",CourseFee=4000, Duration=8, StartDate=DateTime.Parse("2019-2-2")},
                    new Course{CourseName="GAVE",CourseFee=7000, Duration=9, StartDate=DateTime.Parse("2018-10-10")},
                    new Course{CourseName="DDD",CourseFee=6500, Duration=3, StartDate=DateTime.Parse("2019-5-7")}
            });
            db.SaveChanges();
            db.Students.AddRange(new Student[]
            {
                    new Student{ StudentName="S1", CourseId=1, Phone="014567321", Email="sna@gmail.com"},
                     new Student{ StudentName="S2", CourseId=1, Phone="0145457321", Email="fat@gmail.com"}
            });
            db.SaveChanges();

        }
    }
}
