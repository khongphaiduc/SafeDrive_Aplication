using PRN_SafeDrive_Aplication.BiLL;
using PRN_SafeDrive_Aplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN_SafeDrive_Aplication.DAL
{
    public class ResetPassword
    {
        //tạo mk ngẫu  nhiên 
        public string RandomPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            char[] password = new char[8];
            for (int i = 0; i < password.Length; i++)
            {
                password[i] = chars[random.Next(chars.Length)];
            }
            return new string(password);
        }

        // reset password của thằng user 
        public string ResetPasswords(string email)
        {
            string salt = HashingPasswork.GenerateSalt(); // tạo salt mới
            string password = RandomPassword(); // tạo mật khẩu ngẫu nhiên mới
            string HashedPassword = HashingPasswork.HashPassword(password, salt); // băm mật khẩu mới với salt
            Prn1Context dbcontext = new Prn1Context();
            var user = dbcontext.Users.FirstOrDefault(u => u.Email == email); // tìm người dùng theo email
            if (user != null)
            {
                user.Password = HashedPassword;
                user.Salt = salt; // cập nhật mật khẩu và salt mới
                dbcontext.Update(user); // cập nhật người dùng
                dbcontext.SaveChanges();
                return password;
            }
            else
            {
                Console.WriteLine("Tài Khoản không tồn tại ");
                return " ";
            }

        }

    }
}
