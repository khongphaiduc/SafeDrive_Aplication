using PRN_SafeDrive_Aplication.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN_SafeDrive_Aplication.BiLL
{
    public  class UserLog
    {

        // đăng nhâpk
        public static bool LoginUser(string email, string password)
        {

            Registers register = new Registers();

            if (!register.IsUsernameAvailable(email)) return false;  // kiểm tra xem tên đăng nhập đã tồn tại hay chưa


            string salt = HashingPasswork.getSalt(email);   // lấy salt từ email

            string inputPasswordEnter = HashingPasswork.getPasswordHashing(email);  // lấy mật khẩu đã băm từ email trong database

            string hashedPassword = HashingPasswork.HashPassword(password, salt);  // băm mật khẩu đã nhập với salt

            if (inputPasswordEnter.Equals(hashedPassword))
            {
                return true; 
            }
            else
            {
                return false; 
            }   

        }


        // đăng ký 
        public static bool RegisterUser(string username, string password, string email, string role)
        {
            Registers register = new Registers();
            if (register.IsUsernameAvailable(email)) return false;  // kiểm tra xem tên đăng nhập đã tồn tại hay chưa
            string salt = HashingPasswork.GenerateSalt();  // tạo salt mới
            string hashedPassword = HashingPasswork.HashPassword(password, salt);  // băm mật khẩu với salt
            return register.Register(username, hashedPassword, salt, email, role);  // đăng ký tài khoản mới
        }





    }
}
