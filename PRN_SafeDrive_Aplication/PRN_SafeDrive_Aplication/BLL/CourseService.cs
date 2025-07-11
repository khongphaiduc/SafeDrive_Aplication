using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PRN_SafeDrive_Aplication.Models;

namespace PRN_SafeDrive_Aplication.BLL
{
    public class CourseService
    {
        public List<Course> GetCoursesByTeacherId(int teacherId)
        {
            using (var db = new Prn1Context())
            {
                return db.Courses
                         .Where(c => c.TeacherId == teacherId)
                         .ToList();
            }
        }

        //public static List<dynamic> GetStudentsByCourseId(int courseId)
        //{
        //    using (var db = new Prn1Context())
        //    {
        //        return (from reg in db.Registrations
        //                join u in db.Users on reg.UserId equals u.UserId
        //                where reg.CourseId == courseId
        //                select new
        //                {
        //                    u.FullName,
        //                    u.Email,
        //                    reg.Status
        //                }).ToList<dynamic>();
        //    }

        //}

        //public List<Course> GetCoursesByEmail(string email)
        //{
        //    email = email.Trim().ToLower();

        //    using (var context = new Prn1Context())
        //    {
        //        var courses = (from r in context.Registrations
        //                       join u in context.Users on r.UserId equals u.UserId
        //                       join c in context.Courses on r.UserId equals c.TeacherId
        //                       where u.Email.ToLower().Trim() == email
        //                       select c).ToList();

        //        MessageBox.Show($"Tìm thấy {courses.Count} khóa học cho email: {email}");
        //        return courses;
        //    }
        //}


        public List<Course> GetCoursesByEmail(string email)
        {
            using (var context = new Prn1Context())
            {
                // Chuẩn hóa email
                string normalizedEmail = email.Trim().ToLower();

                // Tìm user theo email
                var user = context.Users
                    .FirstOrDefault(u => u.Email.Trim().ToLower() == normalizedEmail);

                if (user == null)
                    return new List<Course>();

                // Lấy các đăng ký của user
                var registrations = context.Registrations
                    .Where(r => r.UserId == user.UserId)
                    .Select(r => r.CourseId)
                    .ToList();

                if (registrations.Count == 0)
                    return new List<Course>();

                // Lấy danh sách khóa học từ CourseId
                var courses = context.Courses
                    .Where(c => registrations.Contains(c.CourseId))
                    .ToList();

                return courses;
            }
        }


    }
}
