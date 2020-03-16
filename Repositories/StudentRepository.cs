using CourseManagemane_Project.Models;
using CourseManagemane_Project.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagemane_Project.Repositories
{
    public class StudentRepository:IStudentRepository
    {
        StudentDbContext db = null;
        public StudentRepository(StudentDbContext db)
        {
            this.db = db;
            if (!this.db.Courses.Any()) this.SeedDummy();
        }

        public void DeleteCourse(int id)
        {
            var c = new Course { CourseId = id };
            db.Entry(c).State = EntityState.Deleted;
            db.SaveChanges();
        }

        public void EditCourse(Course c)
        {
            db.Entry(c).State = EntityState.Modified;
            db.SaveChanges();
        }

        public Course GetCourseById(int id)
        {
            return db.Courses.FirstOrDefault(x => x.CourseId == id);
        }

        public IQueryable<Course> GetCourses()
        {
            return db.Courses;
        }

        public IEnumerable<object> GetStudents()
        {
            return db.Students.Include(x => x.Course);
        }

        public IQueryable<CourseVM> GetSummary()
        {
            return db.Courses.Include(x => x.Students)
                 .Select(x => new CourseVM
                 {
                     CourseId = x.CourseId,
                     CourseName = x.CourseName,
                     Duration=x.Duration,
                     CourseFee=x.CourseFee,
                     StartDate=x.StartDate,
                     StudentCount = x.Students.Count
                 }).AsQueryable();
        }

        public void InsertCourse(Course c)
        {
            db.Courses.Add(c);
            db.SaveChanges();
        }

        IEnumerable<Student> IStudentRepository.GetStudents()
        {
            return db.Students.Include(x => x.Course);
        }

        private void SeedDummy()
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
