using Microsoft.Identity.Client;
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

namespace PRN_SafeDrive_Aplication.Police
{
    /// <summary>
    /// Interaction logic for PoliceInputMark.xaml
    /// </summary>
    public partial class PoliceInputMark : Window
    {
        private int _IDCourse;
        private int _IDExam;

        public PoliceInputMark(int IDCourse, int IDExam)
        {
            InitializeComponent();
            _IDCourse = IDCourse;
            _IDExam = IDExam;
            StudentsDataGrid.ItemsSource = GetListStudentOfCourse(_IDCourse);
        }




        // lấy danh sách của 1 khóa học trong bài thi đấy 
        public List<Students> GetListStudentOfCourse(int courseId)
        {
            using (var dbcontext = new Prn1Context())
            {
                var students = (
                    from a in dbcontext.Courses
                    join b in dbcontext.Registrations on a.CourseId equals b.CourseId
                    join c in dbcontext.Users on b.UserId equals c.UserId
                    join d in dbcontext.Results on c.UserId equals d.UserId into gj
                    from d in gj.DefaultIfEmpty()
                    where a.CourseId == courseId
                    select new Students
                    {
                        Id = c.UserId,
                        Name = c.FullName,
                        Email = c.Email,
                        Class = c.Class,
                        School = c.School,
                        Phone = c.Phone,
                        Mark = d != null ? d.Score : 0  // nếu null thì cho thằng này 0 điểm  không nó nhiều  ok 
                    }
                ).ToList();

                return students;
            }
        }



        // khi sửa vào dataGrid thì sự kiện này tự động sẽ được gọi 
        private void myDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            // Lấy item vừa sửa
            var EditMark = e.Row.Item as Students;

            if (EditMark != null)
            {
                try
                {
                    using (var dbcontext = new Prn1Context())
                    {
                        dbcontext.Results.Add(new Result
                        {
                            UserId = EditMark.Id,  // id thằng học sinh 
                            ExamId = _IDExam,
                            Score = EditMark.Mark,
                            PassStatus = EditMark.Mark >= 5 // nếu điểm >= 5 thì pass ok 
                        });
                        dbcontext.SaveChanges();
                    }

                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private void EndExam(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var dbcontext = new Prn1Context())
                {

                    var s = (from a in dbcontext.Exams
                             where a.ExamId == _IDExam
                             select a).FirstOrDefault();


                    if (s != null)
                    {
                        s.Status = "Ended"; // cập nhật trạng thái kỳ thi là đã kết thúc 
                        dbcontext.SaveChanges();
                        MessageBox.Show("Kỳ thi đã kết thúc thành công");
                        this.Close(); 
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy kỳ thi với ID đã cho.");


                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
      
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); 
        }
    }
}
