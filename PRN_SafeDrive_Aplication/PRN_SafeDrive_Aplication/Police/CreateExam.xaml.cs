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
using System.Windows.Shapes;

namespace PRN_SafeDrive_Aplication.Police
{
    /// <summary>
    /// Interaction logic for CreateExam.xaml
    /// </summary>
    public partial class CreateExam : Window
    {
        private List<string> ListRoom = Enumerable.Range(1, 30)
                                           .Select(i => $"Room {i}")
                                           .ToList();



        public CreateExam()
        {
            InitializeComponent();

            cbRoom.ItemsSource = ListRoom;
            cbCertificateCode.ItemsSource = GetCertificateCode();
            cbExamer.ItemsSource = ListTeacher();
            cbCourseName.ItemsSource = ListCourse();
        }


        // lấy danh sách các CertificateCode
        public List<string> GetCertificateCode()
        {
            using (Prn1Context mydbcontext = new Prn1Context())
            {
                var List = (from  s in mydbcontext.Certificates
                           select s.CertificateCode).Distinct();
                return List.ToList();

            }
        }


        // lấy danh sách giáo viên 
        public List<string> ListTeacher()
        {
            using (Prn1Context mydbcontext = new Prn1Context())
            {
                var teachers = from s in mydbcontext.Users
                               where s.Role == "Teacher"
                               select s.FullName;
                return teachers.ToList();
            }
        }

        // lấy danh sách khóa học
        public List<string> ListCourse()
        {
            using (Prn1Context mydbcontext = new Prn1Context())
            {
                var courses = from s in mydbcontext.Courses
                              select s.CourseName;
                return courses.ToList();

            }
        }


        // lấy id khi biết giáo viên 
        public string GetIDExamer(string nameExamer)
        {
            using (Prn1Context mydbcontext = new Prn1Context())
            {
                var user = mydbcontext.Users.FirstOrDefault(u => u.FullName == nameExamer);
                return user != null ? user.UserId.ToString() : "Unknown";
            }
        }


        // lấy IdKhoas học 
        public string GetIDCourse(string nameCourse)
        {
            using (Prn1Context mydbcontext = new Prn1Context())
            {
                var course = mydbcontext.Courses.FirstOrDefault(c => c.CourseName == nameCourse);
                return course != null ? course.CourseId.ToString() : "Unknown";
            }
        }



        // lấy ID từ CertificateCode
        public int GetCodeIDCertificate(string certificateCode)
        {
            using (Prn1Context mydbcontext = new Prn1Context())
            {
                var certificate = mydbcontext.Certificates.FirstOrDefault(c => c.CertificateCode == certificateCode);
                return certificate != null ? certificate.CertificateCodeID : -1; // Trả về -1 nếu không tìm thấy
            }
        }


        // tạp lịch thi mới 
        public bool MethodCreateExams()
        {
            try
            {
                string IdExamer = GetIDExamer(cbExamer.SelectedItem.ToString());
                string IdCourse = GetIDCourse(cbCourseName.SelectedItem.ToString());
                string date = dpExamDate.SelectedDate.HasValue ? dpExamDate.SelectedDate.Value.ToString("yyyy-MM-dd") : string.Empty;
                string room = cbRoom.SelectedItem.ToString();
                string IDCertificates = cbCertificateCode.SelectedItem.ToString();
                using (Prn1Context mydbcontext = new Prn1Context())
                {
                    mydbcontext.Exams.Add(new Exam()
                    {
                        ExamerId = int.Parse(IdExamer),
                        CourseId = int.Parse(IdCourse),
                        Date = DateOnly.Parse(date),
                        Room = room,
                        Status = "Pending",
                        IDCertificates = GetCodeIDCertificate(IDCertificates)
                    });

                    mydbcontext.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi {ex.Message}", "Thông báo ", MessageBoxButton.OKCancel);

            }
            return false;
        }

        // tạo lịch thi 
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool result = MethodCreateExams();
            if (result)
            {
                MessageBox.Show("Thêm Lịch Thi Thành Công");
                this.DialogResult = true; // Đặt DialogResult để thông báo thành công
                this.Close();
            }
            else
            {
                MessageBox.Show("Thêm Lịch Thi Thất Bại");
            }
        }
    }
}
