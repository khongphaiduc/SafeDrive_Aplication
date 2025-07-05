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

            ListExams = getlistExam();
            dgExams.ItemsSource = ListExams;
        }

        public string getNameExamer(int userId)
        {
            using (Prn1Context mydbcontext = new Prn1Context())
            {
                var user = mydbcontext.Users.FirstOrDefault(u => u.UserId == userId);
                return user != null ? user.FullName : "Unknown";
            }
        }

        public List<MExams> getlistExam()
        {

            using (Prn1Context mydbcontext = new Prn1Context())
            {
                var list = (
    from s in mydbcontext.Exams
    join c in mydbcontext.Courses on s.CourseId equals c.CourseId
    join u in mydbcontext.Users on c.TeacherId equals u.UserId
    join k in mydbcontext.Certificates on u.UserId equals k.UserId
    select new
    {
        CourseName = c.CourseName,
        CertificateCode = k.CertificateCode,
        Date = s.Date,
        Room = s.Room,
        ExamerId = s.ExamerId,
        Status = s.Status
    }
                       ).ToList()
                      .Select(x => new MExams
                      {
                          NameCourse = x.CourseName,
                          CodeCertificate = x.CertificateCode,
                          Date = x.Date.ToString(),
                          Room = x.Room,
                          Examer = getNameExamer(x.ExamerId),
                          Status = x.Status
                      }).ToList();

                return list;

            }

        }





    }

}
