using PRN_SafeDrive_Aplication.BiLL;
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
    /// <summary>
    /// Interaction logic for RegisterAccount.xaml
    /// </summary>
    public partial class RegisterAccount : Window
    {
        public RegisterAccount()
        {
            InitializeComponent();
        }
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            // xử lý logic đăng ký tại đây
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {


            string username = txtFullName.Text;

            string password = txtPassword.Password;
            string confirmPassword = txtConfirmPassword.Password;
            if (!password.Equals(confirmPassword))
            {
                MessageBox.Show("Mật Khẩu Không Khớp ");
                return;
            }
            string email = txtEmail.Text;
            ComboBoxItem selectedItem = (ComboBoxItem)cbRole.SelectedItem;
            string role = selectedItem.Content.ToString();

            bool result = UserLog.RegisterUser(username, password, email, role);

            if (result)
            {
                MessageBox.Show("Đăng Ký Thành Công");
            }
            else
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại hoặc có lỗi xảy ra. Vui lòng thử lại.");
            }
        }



        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
