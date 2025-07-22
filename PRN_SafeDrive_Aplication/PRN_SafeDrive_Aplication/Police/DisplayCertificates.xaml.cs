using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PRN_SafeDrive_Aplication.Models;

namespace PRN_SafeDrive_Aplication.Police
{
    public partial class CertificateListView : UserControl
    {
        private readonly Prn1Context _context;

        public CertificateListView()
        {
            InitializeComponent();
            _context = new Prn1Context();
            LoadCertificates();
        }

        private void LoadCertificates()
        {
            var data = (from cert in _context.Certificates
                        join user in _context.Users on cert.UserId equals user.UserId
                        select new CertificateDto
                        {
                            StudentName = user.FullName,
                            CertificateCode = cert.CertificateCode,
                            IssuedDate = cert.IssuedDate.HasValue
                                ? cert.IssuedDate.Value.ToString("dd/MM/yyyy")
                                : "",
                            ExpirationDate = cert.ExpirationDate.HasValue
                                ? cert.ExpirationDate.Value.ToString("dd/MM/yyyy")
                                : "",
                            CertificateCodeID = cert.CertificateCodeID.ToString()
                        }).ToList();

            dgCertificates.ItemsSource = data;
        }

        private void ExportCertificates_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Chức năng xuất chưa được triển khai.");
        }
    }
    }
