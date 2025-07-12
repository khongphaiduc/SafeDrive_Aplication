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
    /// Interaction logic for CourseListUserControl.xaml
    /// </summary>
    public partial class CourseListUserControl : UserControl
    {
        private readonly string _courseNameHint = "Tìm kiếm với tên khóa học";
        private readonly string _teacherNameHint = "Tìm kiếm với tên giảng viên";

        public event EventHandler<int>? CourseSelected;
        public event EventHandler? BackRequested;

        private List<User> _allTeachers = new();

        public CourseListUserControl()
        {
            InitializeComponent();
            LoadCourses();
            // Set initial hint color if needed (in case not set in XAML)
            SearchCourseNameTextBox.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(136, 136, 136));
            SearchTeacherNameTextBox.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(136, 136, 136));
        }

        private void SearchCourseNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchCourseNameTextBox.Text == _courseNameHint)
            {
                SearchCourseNameTextBox.Text = "";
                SearchCourseNameTextBox.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Black);
            }
        }

        private void SearchCourseNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchCourseNameTextBox.Text))
            {
                SearchCourseNameTextBox.Text = _courseNameHint;
                SearchCourseNameTextBox.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(136, 136, 136));
            }
        }

        private void SearchTeacherNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchTeacherNameTextBox.Text == _teacherNameHint)
            {
                SearchTeacherNameTextBox.Text = "";
                SearchTeacherNameTextBox.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Black);
            }
        }

        private void SearchTeacherNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchTeacherNameTextBox.Text))
            {
                SearchTeacherNameTextBox.Text = _teacherNameHint;
                SearchTeacherNameTextBox.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(136, 136, 136));
            }
        }

        private void LoadCourses()
        {
            try
            {
                using (var context = new Prn1Context())
                {
                    var courses = context.Courses
                        .Include(c => c.Teacher)
                        .OrderBy(c => c.StartDate)
                        .ToList();

                    CoursesItemsControl.ItemsSource = courses;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách khóa học: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ViewDetailButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is int courseId)
            {
                CourseSelected?.Invoke(this, courseId);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            BackRequested?.Invoke(this, EventArgs.Empty);
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string courseName = SearchCourseNameTextBox.Text == _courseNameHint ? string.Empty : SearchCourseNameTextBox.Text.Trim().ToLower();
            string teacherName = SearchTeacherNameTextBox.Text == _teacherNameHint ? string.Empty : SearchTeacherNameTextBox.Text.Trim().ToLower();

            try
            {
                using (var context = new Prn1Context())
                {
                    var query = context.Courses.Include(c => c.Teacher).AsQueryable();

                    if (!string.IsNullOrEmpty(courseName))
                    {
                        query = query.Where(c => c.CourseName.ToLower().Contains(courseName));
                    }
                    if (!string.IsNullOrEmpty(teacherName))
                    {
                        query = query.Where(c => c.Teacher.FullName.ToLower().Contains(teacherName));
                    }

                    var filteredCourses = query.OrderBy(c => c.StartDate).ToList();
                    CoursesItemsControl.ItemsSource = filteredCourses;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm khóa học: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
