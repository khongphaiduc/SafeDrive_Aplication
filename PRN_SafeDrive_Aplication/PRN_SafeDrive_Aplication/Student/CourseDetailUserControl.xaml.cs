using Microsoft.EntityFrameworkCore;
using PRN_SafeDrive_Aplication.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PRN_SafeDrive_Aplication.Student
{
    /// <summary>
    /// Interaction logic for CourseDetailUserControl.xaml
    /// </summary>
    public partial class CourseDetailUserControl : UserControl
    {
        public event EventHandler? BackRequested;
        private Course? _currentCourse;
        private int _currentCourseId;

        public CourseDetailUserControl()
        {
            InitializeComponent();
        }

        public void LoadCourseDetail(int courseId)
        {
            _currentCourseId = courseId;

            try
            {
                using (var context = new Prn1Context())
                {
                    _currentCourse = context.Courses
                        .Include(c => c.Teacher)
                        .FirstOrDefault(c => c.CourseId == courseId);

                    if (_currentCourse != null)
                    {
                        DisplayCourseInfo();
                        CheckRegistrationStatus();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin khóa học!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin khóa học: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DisplayCourseInfo()
        {
            if (_currentCourse == null) return;

            CourseNameTextBlock.Text = _currentCourse.CourseName;
            TeacherNameTextBlock.Text = _currentCourse.Teacher?.FullName ?? "Chưa có thông tin";

            // Xử lý StartDate và EndDate có thể null hoặc không có
            try
            {
                StartDateTextBlock.Text = _currentCourse.StartDate.ToString("dd/MM/yyyy");
                EndDateTextBlock.Text = _currentCourse.EndDate.ToString("dd/MM/yyyy");
            }
            catch
            {
                StartDateTextBlock.Text = "Chưa cập nhật";
                EndDateTextBlock.Text = "Chưa cập nhật";
            }

            ContentTextBlock.Text = !string.IsNullOrEmpty(_currentCourse.ContentCourse) ?
                _currentCourse.ContentCourse : "Nội dung khóa học sẽ được cập nhật sớm";

            // Hiển thị trạng thái khóa học
            try
            {
                var currentDate = DateOnly.FromDateTime(DateTime.Now);
                if (currentDate < _currentCourse.StartDate)
                {
                    StatusTextBlock.Text = "Sắp diễn ra";
                    StatusTextBlock.Foreground = new SolidColorBrush(Color.FromRgb(241, 196, 15)); // #f1c40f
                }
                else if (currentDate >= _currentCourse.StartDate && currentDate <= _currentCourse.EndDate)
                {
                    StatusTextBlock.Text = "Đang diễn ra";
                    StatusTextBlock.Foreground = new SolidColorBrush(Color.FromRgb(39, 174, 96)); // #27ae60
                }
                else
                {
                    StatusTextBlock.Text = "Đã kết thúc";
                    StatusTextBlock.Foreground = new SolidColorBrush(Color.FromRgb(231, 76, 60)); // #e74c3c
                }
            }
            catch
            {
                StatusTextBlock.Text = "Chưa cập nhật";
                StatusTextBlock.Foreground = new SolidColorBrush(Color.FromRgb(149, 165, 166)); // #95a5a6
            }
        }

        private void CheckRegistrationStatus()
        {
            if (_currentCourse == null) return;

            try
            {
                // Kiểm tra SessionUser trước
                if (string.IsNullOrEmpty(SessionUser.Email))
                {
                    RegisterButton.Content = "⚠️ Phiên hết hạn";
                    RegisterButton.IsEnabled = false;
                    RegisterButton.Background = new SolidColorBrush(Color.FromRgb(231, 76, 60)); // #e74c3c
                    return;
                }

                using (var context = new Prn1Context())
                {
                    // Lấy thông tin user hiện tại từ session
                    var currentUser = context.Users.FirstOrDefault(u => u.Email == SessionUser.Email);

                    if (currentUser != null)
                    {
                        // Kiểm tra xem đã đăng ký chưa
                        var existingRegistration = context.Registrations
                            .FirstOrDefault(r => r.UserId == currentUser.UserId && r.CourseId == _currentCourseId);

                        if (existingRegistration != null)
                        {
                            RegisterButton.Content = "✅ Đã Đăng Ký";
                            RegisterButton.IsEnabled = false;
                            RegisterButton.Background = new SolidColorBrush(Color.FromRgb(149, 165, 166)); // #95a5a6
                        }
                        else
                        {
                            // Kiểm tra xem khóa học đã kết thúc chưa
                            try
                            {
                                var currentDate = DateOnly.FromDateTime(DateTime.Now);
                                if (currentDate > _currentCourse.EndDate)
                                {
                                    RegisterButton.Content = "⏰ Đã Kết Thúc";
                                    RegisterButton.IsEnabled = false;
                                    RegisterButton.Background = new SolidColorBrush(Color.FromRgb(231, 76, 60)); // #e74c3c
                                }
                                else
                                {
                                    RegisterButton.Content = "🎓 Đăng Ký Khóa Học";
                                    RegisterButton.IsEnabled = true;
                                    RegisterButton.Background = new SolidColorBrush(Color.FromRgb(39, 174, 96)); // #27ae60
                                }
                            }
                            catch
                            {
                                // Nếu không có thông tin ngày tháng, cho phép đăng ký
                                RegisterButton.Content = "🎓 Đăng Ký Khóa Học";
                                RegisterButton.IsEnabled = true;
                                RegisterButton.Background = new SolidColorBrush(Color.FromRgb(39, 174, 96)); // #27ae60
                            }
                        }
                    }
                    else
                    {
                        RegisterButton.Content = "❌ Không tìm thấy user";
                        RegisterButton.IsEnabled = false;
                        RegisterButton.Background = new SolidColorBrush(Color.FromRgb(231, 76, 60)); // #e74c3c
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi kiểm tra trạng thái đăng ký: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                RegisterButton.Content = "⚠️ Lỗi kiểm tra";
                RegisterButton.IsEnabled = false;
                RegisterButton.Background = new SolidColorBrush(Color.FromRgb(231, 76, 60)); // #e74c3c
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentCourse == null) return;

            try
            {
                // Kiểm tra SessionUser trước
                if (string.IsNullOrEmpty(SessionUser.Email))
                {
                    MessageBox.Show("Phiên đăng nhập đã hết hạn. Vui lòng đăng nhập lại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                using (var context = new Prn1Context())
                {
                    // Lấy thông tin user hiện tại
                    var currentUser = context.Users.FirstOrDefault(u => u.Email == SessionUser.Email);

                    if (currentUser == null)
                    {
                        MessageBox.Show("Không tìm thấy thông tin người dùng! Vui lòng đăng nhập lại.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Kiểm tra xem đã đăng ký chưa
                    var existingRegistration = context.Registrations
                        .FirstOrDefault(r => r.UserId == currentUser.UserId && r.CourseId == _currentCourseId);

                    if (existingRegistration != null)
                    {
                        MessageBox.Show("Bạn đã đăng ký khóa học này rồi!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }

                    // Kiểm tra xem khóa học có tồn tại không
                    var courseExists = context.Courses.Any(c => c.CourseId == _currentCourseId);
                    if (!courseExists)
                    {
                        MessageBox.Show("Khóa học không tồn tại hoặc đã bị xóa!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Xác nhận đăng ký
                    var result = MessageBox.Show(
                        $"Bạn có chắc chắn muốn đăng ký khóa học \"{_currentCourse.CourseName}\"?\n\nSau khi đăng ký, bạn sẽ có thể truy cập vào tài liệu và bài giảng của khóa học.",
                        "Xác nhận đăng ký",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        // Thử các giá trị Status khác nhau để tìm giá trị được chấp nhận
                        string[] possibleStatuses = { "Pending", "Enrolled", "Registered", "Active", "1", "0", "Y", "N", "T", "F", "Complete", "Incomplete" };
                        bool registrationSuccess = false;
                        string workingStatus = "";

                        foreach (string statusToTry in possibleStatuses)
                        {
                            try
                            {
                                // Tạo context mới cho mỗi lần thử
                                using (var tryContext = new Prn1Context())
                                {
                                    var registration = new Registration
                                    {
                                        UserId = currentUser.UserId,
                                        CourseId = _currentCourseId,
                                        Status = statusToTry,
                                        Comments = $"Đăng ký ngày {DateTime.Now:dd/MM/yyyy HH:mm}"
                                    };

                                    tryContext.Registrations.Add(registration);
                                    int rowsAffected = tryContext.SaveChanges();

                                    if (rowsAffected > 0)
                                    {
                                        workingStatus = statusToTry;
                                        registrationSuccess = true;
                                        System.Diagnostics.Debug.WriteLine($"Registration successful with Status: '{statusToTry}'");
                                        break; // Thoát khỏi loop nếu thành công
                                    }
                                }
                            }
                            catch (DbUpdateException dbEx)
                            {
                                System.Diagnostics.Debug.WriteLine($"Status '{statusToTry}' failed: {dbEx.InnerException?.Message ?? dbEx.Message}");
                                continue; // Thử status tiếp theo
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($"Status '{statusToTry}' error: {ex.Message}");
                                continue;
                            }
                        }

                        if (registrationSuccess)
                        {
                            MessageBox.Show(
                                $"🎉 Đăng ký khóa học thành công!\n\nTrạng thái: {workingStatus}\nBạn có thể xem khóa học đã đăng ký trong phần \"Khóa học của tôi\".",
                                "Thành công",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);

                            // Cập nhật lại trạng thái nút
                            CheckRegistrationStatus();
                        }
                        else
                        {
                            MessageBox.Show(
                                "❌ Không thể đăng ký khóa học do constraint database.\n\nVui lòng liên hệ admin để kiểm tra cấu hình constraint cho trường Status trong bảng Registrations.",
                                "Lỗi Database Constraint",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (DbUpdateException dbEx)
            {
                MessageBox.Show($"Lỗi cơ sở dữ liệu: {dbEx.InnerException?.Message ?? dbEx.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đăng ký khóa học: {ex.Message}\n\nChi tiết: {ex.InnerException?.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            BackRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
