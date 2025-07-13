using PRN_SafeDrive_Aplication.Models;
using System;
using System.Linq;
using System.Windows.Controls;

namespace PRN_SafeDrive_Aplication.Student
{
    public partial class StudentCertificates : UserControl
    {
        public StudentCertificates()
        {
            InitializeComponent();
            LoadCertificates();
        }

        private void LoadCertificates()
        {
            if (string.IsNullOrEmpty(SessionUser.Email))
            {
                System.Windows.MessageBox.Show("Phiên đăng nhập đã hết hạn!");
                return;
            }

            using (var context = new Prn1Context())
            {
                var user = context.Users.FirstOrDefault(u => u.Email == SessionUser.Email);
                if (user == null)
                {
                    System.Windows.MessageBox.Show("Không tìm thấy thông tin người dùng!");
                    return;
                }

                var certificates = context.Certificates
                    .Where(c => c.UserId == user.UserId)
                    .Select(c => new
                    {
                        c.CertificateCode,
                        IssuedDate = c.IssuedDate.HasValue ? c.IssuedDate.Value.ToString("dd/MM/yyyy") : "N/A",
                        ExpirationDate = c.ExpirationDate.HasValue ? c.ExpirationDate.Value.ToString("dd/MM/yyyy") : "Không hết hạn"
                    })
                    .ToList();

                CertificatesList.ItemsSource = certificates;
            }
        }
    }
}
