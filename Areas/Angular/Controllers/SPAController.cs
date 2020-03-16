using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseManagemane_Project.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagemane_Project.Areas.Angular.Controllers
{
    [Area("Angular")]
    public class SPAController : Controller
    {
        IStudentRepository repo;
        public SPAController(IStudentRepository repo) { this.repo = repo; }
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult StudentList()
        {
            var data = repo.GetStudents().Select(x => new {
                x.StudentId,
                x.StudentName,
                x.Phone,
                x.Email,
                x.CourseId,
                CourseName = x.Course.CourseName
            }).ToList();
            return Json(data);
        }
    }
}