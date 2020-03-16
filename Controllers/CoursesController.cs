using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CourseManagemane_Project.Models;
using CourseManagemane_Project.Repositories;
using CourseManagemane_Project.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagemane_Project.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        IStudentRepository repo;
        public CoursesController(IStudentRepository repo) { this.repo = repo; }
        public IActionResult Index(int pg = 1)
        {
            var data = repo.GetCourses().OrderBy(x => x.CourseId);
            ViewBag.Pager = new PagerModel
            {
                TotalPages = (int)Math.Ceiling((double)data.Count() / 5),
                CurrentPage = pg
            };
            return View(data.Skip((pg - 1) * 5).Take(5).ToList());
        }
        public IActionResult Summary(int pg = 1, string sort = "", string search = "")
        {
            Thread.Sleep(2000);
            var data = repo.GetSummary();
            ViewBag.Pager = new PagerModel
            {
                TotalPages = (int)Math.Ceiling((double)data.Count() / 5),
                CurrentPage = pg
            };
            if (sort == "") { ViewBag.NameSort = "name"; }
            ViewBag.NameSort = sort == "name" ? "name_desc" : "name";
            ViewBag.CountSort = sort == "count" ? "count_desc" : "count";

            ViewBag.CurrentSort = sort == "" ? "name" : sort;
            ViewBag.Search = search;
            switch (sort)
            {
                case "name":
                    data = data.OrderBy(x => x.CourseName);
                    break;
                case "name_desc":
                    data = data.OrderByDescending(x => x.CourseName);
                    break;
                case "count":
                    data = data.OrderBy(x => x.StudentCount);
                    break;
                case "count_desc":
                    data = data.OrderByDescending(x => x.StudentCount);
                    break;
                default:
                    data = data.OrderBy(x => x.CourseId);
                    break;
            }
            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(x => x.CourseName.ToLower().StartsWith(search.ToLower()));
            }
            var modelData = data.Skip((pg - 1) * 5).Take(5).ToList();
            ///////////////////////////////////////////////////////////////////////
            if (HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_CourseSummary", modelData);
            }
            ///////////////////////////////////////////////////////////////////////
            return View(modelData);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Course c)
        {
            if (ModelState.IsValid)
            {
                repo.InsertCourse(c);
                return RedirectToAction("Index");
            }
            return View(c);
        }
        public IActionResult Edit(int id)
        {
            var c = repo.GetCourseById(id);
            if (c == null)
            {
                return NotFound();
            }
            return View(c);
        }
        [HttpPost]
        public IActionResult Edit(Course c)
        {
            if (ModelState.IsValid)
            {
                repo.EditCourse(c);
                return RedirectToAction("Index");
            }
            return View(c);
        }
        public IActionResult Delete(int id)
        {
            var c = repo.GetCourseById(id);
            if (c == null)
            {
                return NotFound();
            }
            return View(c);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ApplyDelete(int id)
        {

            repo.DeleteCourse(id);
            return RedirectToAction("Index");


        }
    }
}