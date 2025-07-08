using PRN_SafeDrive_Aplication.Models;
using PRN_SafeDrive_Aplication.MyModels;
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

namespace PRN_SafeDrive_Aplication.Teacher
{
    /// <summary>
    /// Interaction logic for DisplayListCourseOfTeacher.xaml
    /// </summary>
    public partial class DisplayListCourseOfTeacher : UserControl
    {
        public List<MCourse> ListCourseOfTeacher { get; set; } = new List<MCourse>();
        public DisplayListCourseOfTeacher()
        {
            InitializeComponent();
            ListCourseOfTeacher = getlistCourse();
            CourseDataGridOfTeacher.ItemsSource = ListCourseOfTeacher;
        }

        public List<MCourse> getlistCourse()
        {

            using (Prn1Context mydbcontext = new Prn1Context())
            {
                var list = (
                    from c in mydbcontext.Courses
                    join u in mydbcontext.Users on c.TeacherId equals u.UserId
                    where c.TeacherId == SessionUser.UserId // Lọc khóa học theo ID giáo viên hiện tại
                    select new MCourse
                    {
                        CourseId = c.CourseId,
                        CourseName = c.CourseName,
                        NameTeacher = u.FullName,
                        Content = c.ContentCourse,
                        DateStart = c.StartDate.ToString("dd/MM/yyyy"),
                        DateEnd = c.EndDate.ToString("dd/MM/yyyy")
                    }).ToList();
                return list;
            }
        }







        //  gọi màn hình hiển thị khi thằng user ấn vào 1  lớp học 
        public void DisplayMemeberOFClass(object sender, MouseButtonEventArgs e)
        {

            var selectedItem = CourseDataGridOfTeacher.SelectedItem;  // lấy instance của dòng thằng user vừa chọn 

            if (selectedItem == null) return;


            // cách 1  ép trực tiếp về kiểu  để lấy thuôc tính 
            /*           MCourse GetIdCourse = (MCourse)CourseDataGridOfTeacher.SelectedItem;
                       string  Id = GetIdCourse.CourseId.ToString(); // lấy ID của khóa học mà thằng user vừa chọn
           */


            // cách 2 sử dụng is để kiểm tra rồi lấy thuộc tính 
            if (selectedItem is MCourse s)
            {
                try
                {
                    int IDCourse = s.CourseId; // lấy ID của khóa học mà thằng user vừa chọn
                    var k = new DisplayListMemberClass(IDCourse);
                    k.ShowDialog();
                }
                catch (Exception )
                {
                    
                    
                }


            }


        }
    }
}
