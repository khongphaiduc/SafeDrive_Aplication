using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using PRN_SafeDrive_Aplication.Models;

namespace PRN_SafeDrive_Aplication.Admin
{
    public partial class ManageCoursesWindow : UserControl
    {
        private readonly Prn1Context _context;
        private readonly int _teacherId;

        public ManageCoursesWindow()
        {
            InitializeComponent();
            _context = new Prn1Context();

            LoadCourses();
        }

        private void LoadCourses()
        {
            var courses = _context.Courses
                .Include(c => c.Teacher)
                .Select(c => new CourseDisplay
                {
                    CourseID = c.CourseId,
                    CourseName = c.CourseName,
                    TeacherName = c.Teacher != null ? c.Teacher.FullName : "(không có)",
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    Description = c.ContentCourse ?? ""
                })
                .ToList();

            dgCourses.ItemsSource = courses;

            if (courses.Count > 0)
                dgCourses.SelectedIndex = 0;
        }

        private void dgCourses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgCourses.SelectedItem is CourseDisplay course)
            {
                int courseId = course.CourseID;

                var students = _context.Registrations
                    .Where(r => r.CourseId == courseId && r.User.Role == "Student")
                    .Select(r => new
                    {
                        FullName = r.User.FullName,
                        Email = r.User.Email,
                        Status = r.Status
                    })
                    .ToList();

                dgStudents.ItemsSource = students;

                tbDetailName.Text = course.CourseName;
                tbDetailStart.Text = course.StartDate.ToString("dd/MM/yyyy");
                tbDetailEnd.Text = course.EndDate.ToString("dd/MM/yyyy");
                tbDetailDesc.Text = course.Description;
            }
            else
            {
                dgStudents.ItemsSource = null;
                tbDetailName.Text = "";
                tbDetailStart.Text = "";
                tbDetailEnd.Text = "";
                tbDetailDesc.Text = "";
            }
        }

        private void BtnDeleteCourse_Click(object sender, RoutedEventArgs e)
        {
            if (dgCourses.SelectedItem is not CourseDisplay selectedCourse)
            {
                MessageBox.Show("Vui lòng chọn một khóa học để xóa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa khóa học '{selectedCourse.CourseName}' không?",
                "Xác nhận xóa",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (confirm != MessageBoxResult.Yes)
                return;

            var courseToDelete = _context.Courses.FirstOrDefault(c => c.CourseId == selectedCourse.CourseID);
            if (courseToDelete == null)
            {
                MessageBox.Show("Không tìm thấy khóa học hoặc bạn không có quyền xóa!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var registrationIds = _context.Registrations
                    .Where(r => r.CourseId == selectedCourse.CourseID)
                    .Select(r => r.RegistrationId)
                    .ToList();

                var examIds = _context.Exams
                    .Where(e => e.CourseId == selectedCourse.CourseID)
                    .Select(e => e.ExamId)
                    .ToList();

                if (examIds.Any())
                    _context.Results.RemoveRange(_context.Results.Where(r => examIds.Contains(r.ExamId)));

                if (registrationIds.Any())
                    _context.Payments.RemoveRange(_context.Payments.Where(p => registrationIds.Contains(p.RegistrationId)));

                if (registrationIds.Any())
                    _context.Registrations.RemoveRange(_context.Registrations.Where(r => registrationIds.Contains(r.RegistrationId)));

                if (examIds.Any())
                    _context.Exams.RemoveRange(_context.Exams.Where(e => examIds.Contains(e.ExamId)));

                _context.Courses.Remove(courseToDelete);
                _context.SaveChanges();

                LoadCourses();
                dgStudents.ItemsSource = null;
                tbDetailName.Text = tbDetailStart.Text = tbDetailEnd.Text = tbDetailDesc.Text = "";

                MessageBox.Show("Đã xóa khóa học thành công.", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message, "Lỗi khi xóa khóa học", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    public class CourseDisplay
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public string TeacherName { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string Description { get; set; }
    }
}
