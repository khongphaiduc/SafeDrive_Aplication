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
using System.Windows.Shapes;

namespace PRN_SafeDrive_Aplication.Teacher
{
    /// <summary>
    /// Interaction logic for DisplayListMemberClass.xaml
    /// </summary>
    public partial class DisplayListMemberClass : Window
    {
        private int _IDCourse;

        public List<Students> ListStudent { get; set; } = new List<Students>();


        public DisplayListMemberClass(int IDCourse)
        {
            InitializeComponent();
            _IDCourse = IDCourse;
            ListStudent = GetListSutdentOFCourse(_IDCourse);
            if (ListStudent.Count == 0)
            {
                MessageBox.Show("Không có học viên nào trong khóa học này.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
                return;
            }
            ListStudentOfCourse.ItemsSource = ListStudent;
        }



        public List<Students> GetListSutdentOFCourse(int IDCourse)
        {

            using (Prn1Context mydbcontext = new Prn1Context())
            {

                var listSutdent = from a in mydbcontext.Registrations
                                  join b in mydbcontext.Users on a.UserId equals b.UserId
                                  where a.CourseId == IDCourse &&
                                        a.Status == "Approved" 
                                  select new Students
                                  {
                                      Id = b.UserId,
                                      Name = b.FullName,
                                      Email = b.Email,
                                      Class = b.Class,
                                      School = b.School,
                                      Phone = b.Phone
                                  };
                return listSutdent.ToList();
            }

        }
    }
}
