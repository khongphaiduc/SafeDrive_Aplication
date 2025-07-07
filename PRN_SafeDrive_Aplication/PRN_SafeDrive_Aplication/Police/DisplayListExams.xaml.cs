using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Protocols;
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

namespace PRN_SafeDrive_Aplication.Police
{
    /// <summary>
    /// Interaction logic for DisplayListExams.xaml
    /// </summary>
    public partial class DisplayListExams : UserControl
    {
        public List<MExams> ListExams { get; set; } = new List<MExams>();

        public DisplayListExams()
        {
            InitializeComponent();

            ListExams = GetListExam();
            dgExams.ItemsSource = ListExams;
            Test();
        }

        public string getNameExamer(int userId)
        {
            using (Prn1Context mydbcontext = new Prn1Context())
            {
                var user = mydbcontext.Users.FirstOrDefault(u => u.UserId == userId);
                return user != null ? user.FullName : "Unknown";
            }
        }


        public List<string> GetCertificateCode()
        {
            using (Prn1Context mydbcontext = new Prn1Context())
            {
                var List = from s in mydbcontext.Certificates
                           select s.CertificateCode;
                return List.ToList();

            }
        }
        public void LoadExams()
        {
            ListExams = GetListExam();
            dgExams.ItemsSource = null;  // Reset source để refresh
            dgExams.ItemsSource = ListExams;
        }

        // lấy danh sách các kỳ thi 
        public List<MExams> GetListExam()
        {
            using (Prn1Context mydbcontext = new Prn1Context())
            {

                var list = from a in mydbcontext.Exams
                           join b in mydbcontext.Courses on a.CourseId equals b.CourseId
                           join c in mydbcontext.Users on a.ExamerId equals c.UserId
                           join d in mydbcontext.Certificates on a.IDCertificates equals d.CertificateId

                           select
                              new MExams
                              {
                                  NameCourse = b.CourseName,
                                  CodeCertificate = d.CertificateCode,
                                  Date = a.Date.ToString("yyyy-MM-dd"),
                                  Room = a.Room,
                                  Examer = c.FullName,
                                  Status = a.Status
                              };
                return list.ToList();
            }
        }



        public void Test()
        {

            var s = GetListExam();

            foreach (var item in s)
            {
                Console.WriteLine($"Course: {item.NameCourse}, Certificate: {item.CodeCertificate}, Date: {item.Date}, Room: {item.Room}, Examer: {item.Examer}, Status: {item.Status}");
            }
        }



        private void btnCreateExam_Click(object sender, RoutedEventArgs e)
        {
            var s = new CreateExam();
            if(s.ShowDialog() == true)
            {
                LoadExams(); // Tải lại danh sách kỳ thi sau khi tạo mới
            }
          
        }
    }

}
