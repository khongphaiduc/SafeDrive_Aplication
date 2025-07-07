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
            khoaHocStu.Visibility = Visibility.Collapsed;
            khoaHocTea.Visibility = Visibility.Collapsed;

            MessageBox.Show($"Role trong Home: {SessionUser.Role}");

            if (SessionUser.Role == "Teacher")
                khoaHocTea.Visibility = Visibility.Visible;
            else if (SessionUser.Role == "Student")
                khoaHocStu.Visibility = Visibility.Visible;

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

        private void khoaHocStu_Click(object sender, RoutedEventArgs e)
        {
           // ViewCoursesWindow v = new();
          //  v.ShowDialog();
        }
    }
}
