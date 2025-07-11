using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using PRN_SafeDrive_Aplication.BLL;
using PRN_SafeDrive_Aplication.Models;

namespace PRN_SafeDrive_Aplication.Student
{
    /// <summary>
    /// Interaction logic for CourseListControl.xaml
    /// </summary>
    public partial class CourseListControl : UserControl, INotifyPropertyChanged
    {
        private ObservableCollection<Course> _courses;
        public ObservableCollection<Course> Courses
        {
            get => _courses;
            set { _courses = value; OnPropertyChanged(); }
        }

        private Course _selectedCourse;
        public Course SelectedCourse
        {
            get => _selectedCourse;
            set { _selectedCourse = value; OnPropertyChanged(); }
        }

        public CourseListControl()
        {
            InitializeComponent();
            DataContext = this;

            LoadCoursesForStudent();
        }

        private void LoadCoursesForStudent()
        {
            string email = SessionUser.Email;
            //MessageBox.Show("email hiện tại: " + email);
            var courseService = new CourseService();
            var courseList = courseService.GetCoursesByEmail(email);

            Courses = new ObservableCollection<Course>(courseList);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
