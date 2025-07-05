using PRN_SafeDrive_Aplication.BiLL;
using PRN_SafeDrive_Aplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
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
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        //lấy role xem là giáo viên, học sinh hay csgt
        public static string GetRoleByEmail(string email)
        {
            using (var db = new Prn1Context())
            {
                return db.Users
                    .Where(u => u.Email == email)
                    .Select(u => u.Role)
                    .FirstOrDefault() ?? "";
            }
        }

        // đăng nhập
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string account = UsernameBox.Text;

            string password = PasswordBox.Password;


            bool result = UserLog.LoginUser(account, password);

            if (result)
            {
             


                SessionUser.Email = account; // Lưu email vào SessionUser
                SessionUser.Role = GetRoleByEmail(account); //lưu role vào session
                SessionUser.UserId = UserLog.GetUserIdByEmail(account); // Lưu ID người dùng vào SessionUser

                Home home = new Home();   // brack point để xem thằng l email và role còn null hay không

                home.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không đúng!");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RegisterAccount registerAccount = new RegisterAccount();
            registerAccount.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ForgetPassword forgetPassword = new ForgetPassword();
            forgetPassword.Show();
        }
    }
}
