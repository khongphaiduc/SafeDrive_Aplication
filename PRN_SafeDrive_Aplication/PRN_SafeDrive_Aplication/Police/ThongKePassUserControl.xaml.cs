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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PRN_SafeDrive_Aplication.Police
{
    /// <summary>
    /// Interaction logic for ThongKePassUserControl.xaml
    /// </summary>
    public partial class ThongKePassUserControl : UserControl
    {
        public ThongKePassUserControl()
        {
            InitializeComponent();
            LoadPassRateData();
        }

        private void LoadPassRateData()
        {
            // Giả sử có lớp Prn1Context và bảng Result, Course
            using (var db = new Prn1Context())
            {
                var query = from c in db.Courses
                            join e in db.Exams on c.CourseId equals e.CourseId
                            join r in db.Results on e.ExamId equals r.ExamId
                            group r by new { c.CourseId, c.CourseName } into g
                            select new
                            {
                                CourseName = g.Key.CourseName,
                                Total = g.Count(),
                                PassCount = g.Count(x => x.PassStatus),
                                PassRate = g.Count() == 0 ? 0 : (int)((double)g.Count(x => x.PassStatus) / g.Count() * 100)
                            };
                PassRateDataGrid.ItemsSource = query.ToList();
            }
        }
    }
}
