using CourseManagemane_Project.Models;
using CourseManagemane_Project.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagemane_Project.Repositories
{
    public interface IStudentRepository
    {
        IQueryable<CourseVM> GetSummary();
        IQueryable<Course> GetCourses();
        Course GetCourseById(int id);
        void InsertCourse(Course Course);
        void EditCourse(Course dept);
        void DeleteCourse(int id);
        IEnumerable<Student> GetStudents();
    }
}
