using PRN_SafeDrive_Aplication.Models;
using System.Linq;
using System.Windows;

namespace PRN_SafeDrive_Aplication.Student
{
    public partial class StudentWindow : Window
    {
        private readonly Prn1Context _context;
        private User currentUser;

        public StudentWindow()
        {
            InitializeComponent();
            _context = new Prn1Context();

            // Lấy email từ SessionUser
            string email = SessionUser.Email;
            currentUser = _context.Users.FirstOrDefault(u => u.Email == email);

            if (currentUser != null)
            {
                // Hiện lên bảng thông tin phía trên
                this.DataContext = new
                {
                    UserID = currentUser.UserId,
                    FullName = currentUser.FullName,
                    Email = currentUser.Email,
                    Class = currentUser.Class ?? "",
                    School = currentUser.School ?? "",
                    Phone = currentUser.Phone ?? ""
                };

                // Đổ vào form chỉnh sửa bên dưới
                txtHoTen.Text = currentUser.FullName;
                txtLop.Text = currentUser.Class ?? "";
                txtTruong.Text = currentUser.School ?? "";
                txtSoDienThoai.Text = currentUser.Phone ?? "";
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin sinh viên với email này!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            if (currentUser != null)
            {
                // Validate họ và tên
                string hoTen = txtHoTen.Text.Trim();
                if (string.IsNullOrWhiteSpace(hoTen) || hoTen.Any(char.IsDigit) || hoTen.Contains("  "))
                {
                    MessageBox.Show("Họ và tên không hợp lệ! Không được chứa số, không được để trống và không có khoảng trắng liên tiếp.",
                                    "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtHoTen.Focus();
                    return;
                }

                // Validate số điện thoại
                string soDT = txtSoDienThoai.Text.Trim();
                if (!System.Text.RegularExpressions.Regex.IsMatch(soDT, @"^0\d{9}$"))
                {
                    MessageBox.Show("Số điện thoại phải bắt đầu bằng số 0 và có đúng 10 chữ số.",
                                    "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtSoDienThoai.Focus();
                    return;
                }

                // Nếu hợp lệ mới cập nhật thông tin
                if (currentUser != null)
                {
                    currentUser.FullName = hoTen;
                    currentUser.Class = txtLop.Text.Trim();
                    currentUser.School = txtTruong.Text.Trim();
                    currentUser.Phone = soDT;

                    _context.SaveChanges();

                    // Lấy giá trị người dùng vừa sửa
                    currentUser.FullName = txtHoTen.Text;
                    currentUser.Class = txtLop.Text;
                    currentUser.School = txtTruong.Text;
                    currentUser.Phone = txtSoDienThoai.Text;

                    _context.SaveChanges();

                    // Cập nhật lại bảng thông tin phía trên
                    this.DataContext = new
                    {
                        UserID = currentUser.UserId,
                        FullName = currentUser.FullName,
                        Email = currentUser.Email,
                        Class = currentUser.Class ?? "",
                        School = currentUser.School ?? "",
                        Phone = currentUser.Phone ?? ""
                    };

                    MessageBox.Show("Cập nhật thông tin thành công!");
                }
            }
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}