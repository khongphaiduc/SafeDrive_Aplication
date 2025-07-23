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
using PRN_SafeDrive_Aplication.Models;

namespace PRN_SafeDrive_Aplication.Admin
{
    /// <summary>
    /// Interaction logic for ClassStatisticsView.xaml
    /// </summary>
    public partial class ClassStatisticsView : UserControl
    {
        private readonly Prn1Context _context = new();

        public ClassStatisticsView()
        {
            InitializeComponent();
            LoadStatistics();
        }

        private void LoadStatistics()
        {
            // Lấy tất cả kết quả
            var results = _context.Results
                .Select(r => new
                {
                    r.UserId,
                    r.Score,
                    FullName = r.User.FullName,
                    Email = r.User.Email
                })
                .ToList();

            int totalStudents = results.Count;
            double avgScore = results.Any() ? results.Average(r => (double)r.Score) : 0;
            int passCount = results.Count(r => r.Score >= 5);
            int failCount = results.Count(r => r.Score < 5);

            // Hiển thị thống kê
            lblTotalStudents.Text = totalStudents.ToString();
            lblAverageScore.Text = avgScore.ToString("F2");
            lblPassCount.Text = passCount.ToString();
            lblFailCount.Text = failCount.ToString();

            // Tạo danh sách hiển thị
            var resultView = results.Select(r => new
            {
                r.FullName,
                r.Email,
                Score = r.Score,
                Status = r.Score >= 5 ? "Đậu" : "Rớt"
            }).ToList();

            dgResults.ItemsSource = resultView;
        }
    }
}
