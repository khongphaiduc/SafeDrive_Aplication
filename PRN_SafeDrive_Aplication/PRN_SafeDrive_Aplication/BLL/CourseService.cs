using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRN_SafeDrive_Aplication.Models;

namespace PRN_SafeDrive_Aplication.BLL
{
    public static class CourseService
    {
        public static List<Course> GetCoursesByTeacherId(int teacherId)
        {
            using (var db = new Prn1Context())
            {
                return db.Courses
                         .Where(c => c.TeacherId == teacherId)
                         .ToList();
            }
        }

        public static List<dynamic> GetStudentsByCourseId(int courseId)
        {
            using (var db = new Prn1Context())
            {
                return (from reg in db.Registrations
                        join u in db.Users on reg.UserId equals u.UserId
                        where reg.CourseId == courseId
                        select new
                        {
                            u.FullName,
                            u.Email,
                            reg.Status
                        }).ToList<dynamic>();
            }
        }
    }
}
