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
using PRN_SafeDrive_Aplication.Police;
using PRN_SafeDrive_Aplication.Student;
using PRN_SafeDrive_Aplication.Teacher;

namespace PRN_SafeDrive_Aplication.BiLL
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();
            UIStudent.Visibility = Visibility.Collapsed;
            UITeachers.Visibility = Visibility.Collapsed;
            UIPolice.Visibility = Visibility.Collapsed;

            
            string roleTest = SessionUser.Role;
            string emailTest = SessionUser.Email;

            Console.WriteLine($"Email của bạn là : {emailTest} và  role của bạn là {roleTest}");


            if (SessionUser.Role.Equals("Student"))
            {
                UIStudent.Visibility = Visibility.Visible;       // Hiển thị giao diện sinh viên
            }
            else if (SessionUser.Role == ("Teacher"))
            {
                Console.WriteLine(SessionUser.Role);
                UITeachers.Visibility = Visibility.Visible;      // Hiển thị giao diện giáo viên
            }
            else
            {
                UIPolice.Visibility = Visibility.Visible;       // Hiển thị giao diện cảnh sát
            }
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Your logic here
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StudentWindow s = new();
            s.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            StudentWindow s = new();
            s.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ManageCoursesWindow m = new();
            m.ShowDialog();
        }



        private void Button_Click_taokhoahoc(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new CreateCourse();
        }

        

        // tổ trức kỳ thi
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new DisplayListExams();
        }
    }
}
