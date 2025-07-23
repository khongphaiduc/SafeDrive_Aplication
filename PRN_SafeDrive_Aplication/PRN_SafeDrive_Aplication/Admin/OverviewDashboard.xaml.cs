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
    /// Interaction logic for OverviewDashboard.xaml
    /// </summary>
    public partial class OverviewDashboard : UserControl
    {
        private readonly Prn1Context _context;

        public OverviewDashboard()
        {
            InitializeComponent();
            _context = new Prn1Context(); // Ensure it's injected or initialized correctly
            LoadDashboard();
        }

        private void LoadDashboard()
        {
            var totalUsers = _context.Users.Count();
            var adminCount = _context.Users.Count(u => u.Role == "Admin");
            var teacherCount = _context.Users.Count(u => u.Role == "Teacher");
            var studentCount = _context.Users.Count(u => u.Role == "Student");

            txtTotalUsers.Text = totalUsers.ToString();
            txtUserRoles.Text = $"Admin: {adminCount} | GV: {teacherCount} | HV: {studentCount}";

            var today = DateOnly.FromDateTime(DateTime.Now);
            var activeCourses = _context.Courses.Count(c => c.EndDate > today);
            var activeExams = _context.Exams.Count(e => e.Status == "Active" || e.Date >= today);
            txtActiveCourses.Text = activeCourses.ToString();
            txtActiveExams.Text = activeExams.ToString();

            var issuedCertificates = _context.Certificates.Count();
            txtCertificatesIssued.Text = issuedCertificates.ToString();
        }

    }
}
