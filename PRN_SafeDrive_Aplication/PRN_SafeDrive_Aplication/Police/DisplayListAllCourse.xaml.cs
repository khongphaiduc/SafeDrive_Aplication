using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using PRN_SafeDrive_Aplication.Models;
using PRN_SafeDrive_Aplication.MyModels;
using PRN_SafeDrive_Aplication.Teacher;
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

namespace PRN_SafeDrive_Aplication.Police
{
    /// <summary>
    /// Interaction logic for DisplayListAllCourse.xaml
    /// </summary>
    public partial class DisplayListAllCourse : UserControl
    {

        public List<MCourse> ListAllCourses { get; set; } = new List<MCourse>();
        public DisplayListAllCourse()
        {
            InitializeComponent();
            ListAllCourses = getlistCourse();
            ListCourse.ItemsSource = ListAllCourses;
        }


        public List<MCourse> getlistCourse()
        {
            using (Prn1Context mydbcontext = new Prn1Context())
            {
                var list = (
                    from c in mydbcontext.Courses
                    join u in mydbcontext.Users on c.TeacherId equals u.UserId
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

        private void ViewDetailListStudent(object sender, MouseButtonEventArgs e)
        {

            try
            {
                var ItemSelected = (MCourse)ListCourse.SelectedItem;

                if (ItemSelected is MCourse s)
                {

                    int IDCourse = s.CourseId; // lấy ID của khóa học mà thằng user vừa chọn
                    var k = new DisplayListMemberClass(IDCourse);
                    k.ShowDialog();   // bắt thăng user phải tắt tab này mới cho mở tab khác 
                }
            }
            catch (Exception ex )
            {
               

            }




        }
    }
}
