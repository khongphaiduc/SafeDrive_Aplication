using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PRN_SafeDrive_Aplication.Models;
using PRN_SafeDrive_Aplication.MyModels;

namespace PRN_SafeDrive_Aplication.Student
{
    /// <summary>
    /// Interaction logic for ViewExamSchedule.xaml
    /// </summary>
    public partial class ViewExamSchedule : UserControl
    {
        private readonly string _email;
        private readonly Prn1Context _context = new();

        public ViewExamSchedule(string email)
        {
            InitializeComponent();
            _email = email;
            LoadData();
        }

        private void LoadData()
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == _email);
            if (user == null) return;

            // Lịch học
            var courses = _context.Registrations
            .Where(r => r.UserId == user.UserId)
            .Select(r => new
            {
                r.Course.CourseName,
                r.Course.StartDate,
                r.Course.EndDate,
                r.Course.ContentCourse
            })
            .ToList();

            dgCourses.ItemsSource = courses;

            // Lịch thi
            var exams = _context.Exams
                .Where(e => courses.Select(c => c.CourseName).Contains(e.Course.CourseName))
                .Select(e => new
                {
                    CourseName = e.Course.CourseName,
                    ExamDate = e.Date,
                    e.Room
                }).ToList();
            dgExams.ItemsSource = exams;

            // Kết quả thi
            var results = _context.Results
            .Where(r => r.UserId == user.UserId)
            .Select(r => new
            {
                CourseName = r.Exam.Course.CourseName,
                Score = r.Score,
                PassStatus = r.PassStatus ? "Đạt" : "Không đạt"
            })
            .ToList();

            dgResults.ItemsSource = results;
        }
    }
}
