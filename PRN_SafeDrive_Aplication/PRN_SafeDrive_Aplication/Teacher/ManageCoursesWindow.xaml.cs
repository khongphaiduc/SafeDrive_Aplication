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
            Console.WriteLine($"Bên đại ca đức anh {SessionUser.Email}");
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

        private void BtnDeleteCourse_Click(object sender, RoutedEventArgs e)
        {
            if (dgCourses.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn một khóa học để xóa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            dynamic selectedCourse = dgCourses.SelectedItem;
            int courseId = selectedCourse.CourseID;

            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa khóa học '{selectedCourse.CourseName}' không?",
                "Xác nhận xóa",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes) return;

            var courseToDelete = _context.Courses.FirstOrDefault(c => c.CourseId == courseId && c.TeacherId == _teacherId);
            if (courseToDelete == null)
            {
                MessageBox.Show("Không tìm thấy khóa học hoặc bạn không có quyền xóa!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // Lấy danh sách các liên kết một lần duy nhất
                var registrationIds = _context.Registrations
                    .Where(r => r.CourseId == courseId)
                    .Select(r => r.RegistrationId)
                    .ToList();

                var examIds = _context.Exams
                    .Where(e => e.CourseId == courseId)
                    .Select(e => e.ExamId)
                    .ToList();

                // Xóa các Results liên quan tới Exams
                if (examIds.Any())
                    _context.Results.RemoveRange(_context.Results.Where(r => examIds.Contains(r.ExamId)));

                // Xóa các Payments liên quan tới Registrations
                if (registrationIds.Any())
                    _context.Payments.RemoveRange(_context.Payments.Where(p => registrationIds.Contains(p.RegistrationId)));

                // Xóa các Registrations
                if (registrationIds.Any())
                    _context.Registrations.RemoveRange(_context.Registrations.Where(r => registrationIds.Contains(r.RegistrationId)));

                // Xóa các Exams
                if (examIds.Any())
                    _context.Exams.RemoveRange(_context.Exams.Where(e => examIds.Contains(e.ExamId)));

                // Xóa khóa học
                _context.Courses.Remove(courseToDelete);

                _context.SaveChanges();

                // Làm mới danh sách
                dgCourses.ItemsSource = _context.Courses
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
                dgStudents.ItemsSource = null;

                // Xóa thông tin chi tiết
                tbDetailName.Text = "";
                tbDetailStart.Text = "";
                tbDetailEnd.Text = "";
                tbDetailDesc.Text = "";

                MessageBox.Show("Đã xóa khóa học thành công.", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException?.Message ?? ex.Message, "Lỗi khi xóa khóa học", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}