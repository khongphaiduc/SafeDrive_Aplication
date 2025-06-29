using Microsoft.EntityFrameworkCore;
using PRN_SafeDrive_Aplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PRN_SafeDrive_Aplication.BiLL
{


    public class HashingPasswork
    {


        // tạo salt
        public static string GenerateSalt()
        {
            byte[] saltBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }





        // Băm mật khẩu với salt
        public static string HashPassword(string password, string salt)
        {
            using (SHA256 sha = SHA256.Create())
            {
                var combinedBytes = Encoding.UTF8.GetBytes(password + salt);
                var hashBytes = sha.ComputeHash(combinedBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }



        // lấy salt từ email
        public static string getSalt(string email)
        {
            Prn1Context mydbcontext = new Prn1Context();
            return mydbcontext.Users
                .Where(s => s.Email.Equals(email))
                .Select(s => s.Salt)
                .FirstOrDefault() ?? string.Empty;

        }


        // lấy mật khẩu từ email
        public static string getPasswordHashing(string email)
        {
            Prn1Context mydbcontext = new Prn1Context();
            return mydbcontext.Users
                .Where(s => s.Email.Equals(email))
                .Select(s => s.Password)
                .FirstOrDefault() ?? string.Empty;
        }



    }
}

