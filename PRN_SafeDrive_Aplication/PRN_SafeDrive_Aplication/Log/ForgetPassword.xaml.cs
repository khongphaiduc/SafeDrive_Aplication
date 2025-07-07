using PRN_SafeDrive_Aplication.DAL;
using PRN_SafeDrive_Aplication.Service;
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

namespace PRN_SafeDrive_Aplication.Log
{
    public partial class ForgetPassword : Window
    {
        public ForgetPassword()
        {
            InitializeComponent();
        }

        private async void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string emailUser = txtEmail.Text;
            if (string.IsNullOrEmpty(emailUser))
            {
                MessageBox.Show("Vui lòng nhập email của bạn.");
                return;
            }
            else
            {

                Registers s = new Registers();
                if (!s.IsUsernameAvailable(emailUser))
                {
                    MessageBox.Show("Email không tồn tại ");
                    return;
                }
                else
                {
                    ResetPassword t = new ResetPassword();

                    string password = t.ResetPasswords(emailUser); // reset mật khẩu của thằng user

                    await MyResendEmail.SendGmailAsyncs(emailUser, "New Password", "Mật Khẩu Mới Của bạn là :" + password + "\n " + "Vui lòng đăng nhập và thay đổi lại mật khẩu ");
                }

            }
        }
    }
}
