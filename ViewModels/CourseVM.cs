using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagemane_Project.ViewModels
{
    public class CourseVM
    {
        public int CourseId { get; set; }
        [Required, StringLength(50), Display(Name = "Course Name")]
        public string CourseName { get; set; }
        [Display(Name = "Course Fee"), DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal CourseFee { get; set; }

        public int Duration { get; set; }
        [Required, Column(TypeName = "date"), Display(Name = "Start Date"),
          DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [Display(Name = "Student Count")]
        public int StudentCount { get; set; }
    }
}
