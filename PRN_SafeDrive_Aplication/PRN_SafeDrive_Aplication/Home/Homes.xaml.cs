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
using PRN_SafeDrive_Aplication.Admin;
using PRN_SafeDrive_Aplication.Home;
using PRN_SafeDrive_Aplication.Log;
using PRN_SafeDrive_Aplication.Models;
using PRN_SafeDrive_Aplication.Police;
using PRN_SafeDrive_Aplication.Student;
using PRN_SafeDrive_Aplication.Teacher;


namespace PRN_SafeDrive_Aplication.BiLL
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Homes : Window
    {
        public Homes()
        {
            InitializeComponent();
            UIStudent.Visibility = Visibility.Collapsed;
            UITeachers.Visibility = Visibility.Collapsed;
            UIPolice.Visibility = Visibility.Collapsed;


            string roleTest = SessionUser.Role;
            string emailTest = SessionUser.Email;

            //Console.WriteLine($"Email của bạn là : {emailTest} và  role của bạn là {roleTest}");


            if (SessionUser.Role.Equals("Student"))
            {
                UIStudent.Visibility = Visibility.Visible;       // Hiển thị giao diện sinh viên
            }
            else if (SessionUser.Role == ("Teacher"))
            {
                UITeachers.Visibility = Visibility.Visible;      // Hiển thị giao diện giáo viên
            }
            else if (SessionUser.Role == ("TrafficPolice"))
            {
                UIPolice.Visibility = Visibility.Visible;       // Hiển thị giao diện cảnh sát
            } 
            else if (SessionUser.Email == "admin@gmail.com")
            {
                UIAdmin.Visibility = Visibility.Visible;
            }
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Your logic here
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StudentWindow s = new();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new StudentWindow();
        }


        // hiện  thị khóa học với view là công an
        private void DisplayCourseOfPolice(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new DisplayListAllCourse();
        }


        // hiện thị tất cả khóa học của teacher 
        private void DisplayCourseOfTeacher(object sender, RoutedEventArgs e)
        {
            //MainContent.Content = new DisplayListCourseOfTeacher();
            MainContent.Content = new ManageCoursesWindow();
        }


        // tạo khóa học dành cho teacher
        private void Button_Click_taokhoahoc(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new CreateCourse();
        }



        // tổ trức kỳ thi dành cho  cảnh sát 
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new DisplayListExams();
        }





        private void HomeStudent(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new HomeUserControl();
        }
        private void HomeTeacher(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new HomeUserControl();
        }
        private void HomePolice(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new HomeUserControl();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
        }


        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (SessionUser.Role.Equals("Student"))
            {
                // Hiển thị danh sách khóa học cho student
                var courseListControl = new CourseListUserControl();
                courseListControl.CourseSelected += CourseListControl_CourseSelected;
                MainContent.Content = courseListControl;
            }
        }

        private void CourseListControl_CourseSelected(object? sender, int courseId)
        {
            // Hiển thị chi tiết khóa học
            var courseDetailControl = new CourseDetailUserControl();
            courseDetailControl.BackRequested += CourseDetailControl_BackRequested;
            courseDetailControl.LoadCourseDetail(courseId);
            MainContent.Content = courseDetailControl;
        }

        private void CourseListControl_CourseSelected2(object? sender, int courseId)
        {
            // Hiển thị chi tiết khóa học khi bấm khóa học của tôi
            var courseDetailControl = new CourseDetailUserControl();
            courseDetailControl.BackRequested += CourseDetailControl_BackRequested2;
            courseDetailControl.LoadCourseDetail(courseId);
            MainContent.Content = courseDetailControl;
        }

        private void CourseDetailControl_BackRequested(object? sender, EventArgs e)
        {
            // Quay lại danh sách khóa học 
            var courseListControl = new CourseListUserControl();
            courseListControl.CourseSelected += CourseListControl_CourseSelected;
            MainContent.Content = courseListControl;
        }

        private void CourseDetailControl_BackRequested2(object? sender, EventArgs e)
        {
            // Quay lại danh sách khóa học khi bấm khóa học của tôi
            var courseListControl = new StudentViewCourses();
            courseListControl.CourseSelected += CourseListControl_CourseSelected2;
            MainContent.Content = courseListControl;
        }



        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (SessionUser.Role.Equals("Student"))
            {
                // Hiển thị danh sách khóa học cho student
                var courseListControl = new StudentViewCourses();
                courseListControl.CourseSelected += CourseListControl_CourseSelected2;
                MainContent.Content = courseListControl;
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ViewExamSchedule(SessionUser.Email);
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new StudentCertificates();
        }

  

        private void LogOutMethod(object sender, RoutedEventArgs e)
        {
            SessionUser.Email = string.Empty;
            SessionUser.Role = string.Empty;
            SessionUser.UserId = 0;
            Login loginWindow = new Login();
            this.Close(); // Đóng cửa sổ hiện tại
            loginWindow.ShowDialog();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ViewAllScores();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new CertificateListView();
        }

        private void btnClassStatistics(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ClassStatisticsView();
        }

        private void btnSystemStatistics(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new OverviewDashboard();

        }
    }
}
