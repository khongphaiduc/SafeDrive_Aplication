using PRN_SafeDrive_Aplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace PRN_SafeDrive_Aplication.Teacher
{
    /// <summary>
    /// Interaction logic for CreateCourse.xaml
    /// </summary>
    public partial class CreateCourse : UserControl
    {
        public CreateCourse()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string courseName = Regex.Replace(CourseNameTextBox.Text.Trim(),@"\s+", " "); 
            string contentCourse = Regex.Replace(CourseContentTextBox.Text.Trim(), @"\s+", " "); 

            DateTime? startDate = StartDatePicker.SelectedDate;
            DateTime? endDate = EndDatePicker.SelectedDate;

            if (string.IsNullOrEmpty(courseName) || string.IsNullOrEmpty(contentCourse))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin khóa học.", "Thông báo đến bạn", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!startDate.HasValue || !endDate.HasValue) { 
             MessageBox.Show("Vui lòng chọn ngày bắt đầu và ngày kết thúc.", "Thông báo đến bạn ", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (startDate > endDate )
            {
                MessageBox.Show("Ngày bắt đầu không thể sau ngày kết thúc.", "Thông báo báo đến bạn ", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int row = 0;

            using (var dbcontext = new Prn1Context())
            {
                dbcontext.Courses.Add(new Course
                {
                    CourseName = courseName,
                    ContentCourse = contentCourse,
                    StartDate = DateOnly.FromDateTime((DateTime)startDate),
                    EndDate =DateOnly.FromDateTime((DateTime)endDate),
                    TeacherId = SessionUser.UserId // Lấy ID giáo viên từ SessionUser
                });
                row =  dbcontext.SaveChanges();
               
            }

            _ = row > 0 ? MessageBox.Show("Tạo khóa học thành công.", "Thông báo đến bạn", MessageBoxButton.OK, MessageBoxImage.Information)
                               : MessageBox.Show("Tạo khóa học thất bại.", "Thông báo đến bạn", MessageBoxButton.OK, MessageBoxImage.Error);


        }
    }
}
