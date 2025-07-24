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
    /// Interaction logic for AllUsersControl.xaml
    /// </summary>
    public partial class AllUsersControl : UserControl
    {
        private readonly Prn1Context _context;

        public AllUsersControl()
        {
            InitializeComponent();
            _context = new Prn1Context();
            LoadUsers();
        }

        private void LoadUsers(string keyword = "")
        {
            var users = _context.Users
                .Where(u => string.IsNullOrEmpty(keyword) ||
                            u.FullName.Contains(keyword) ||
                            u.Email.Contains(keyword))
                .Select(u => new
                {
                    u.FullName,
                    u.Email,
                    u.Role,
                    u.Class,
                    u.School,
                    u.Phone
                })
                .ToList();

            dgUsers.ItemsSource = users;
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            LoadUsers(keyword);
        }
    }
}
