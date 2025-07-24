using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN_SafeDrive_Aplication.Models
{
    internal class CourseDisplay
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string TeacherName { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string Description { get; set; }
    }
}
