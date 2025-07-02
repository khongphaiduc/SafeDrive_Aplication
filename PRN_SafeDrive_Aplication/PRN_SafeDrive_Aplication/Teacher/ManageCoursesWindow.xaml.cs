using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PRN_SafeDrive_Aplication.Models;

namespace PRN_SafeDrive_Aplication.Teacher
{
    public partial class ManageCoursesWindow : Window
    {
        private Prn1Context _context;
        private int _teacherId;

        public ManageCoursesWindow()
        {
            InitializeComponent();
            _context = new Prn1Context();

            // Lấy email giáo viên từ session (giả sử đã lưu trong SessionUser.Email)
            string teacherEmail = SessionUser.Email;

            // Tìm giáo viên theo email
            var teacher = _context.Users.FirstOrDefault(u => u.Email == teacherEmail && u.Role == "Teacher");
            if (teacher == null)
            {
                MessageBox.Show("Không tìm thấy thông tin giáo viên.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
                return;
            }
            _teacherId = teacher.UserId;

            // Lấy danh sách các khóa học mà giáo viên này phụ trách
            var courses = _context.Courses
                                  .Where(c => c.TeacherId == _teacherId)
                                  .Select(c => new
                                  {
                                      CourseID = c.CourseId,
                                      CourseName = c.CourseName,
                                      StartDate = c.StartDate,
                                      EndDate = c.EndDate,
                                      Description = c.ContentCourse
                                  })
                                  .ToList();

            dgCourses.ItemsSource = courses;

            if (dgCourses.Items.Count > 0)
                dgCourses.SelectedIndex = 0;
        }

        private void dgCourses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgCourses.SelectedItem != null)
            {
                dynamic course = dgCourses.SelectedItem;
                int courseId = course.CourseID;

                // Lấy danh sách học sinh đã đăng ký tham gia khóa học này
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

                // Hiển thị thông tin chi tiết khóa học
                tbDetailName.Text = course.CourseName;
                tbDetailStart.Text = course.StartDate.ToString("dd/MM/yyyy");
                tbDetailEnd.Text = course.EndDate.ToString("dd/MM/yyyy");
                tbDetailDesc.Text = course.Description ?? "";
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
    }
}