using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PRN_SafeDrive_Aplication.BLL
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

        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            string hashOfInput = HashPassword(enteredPassword, storedSalt);
            return hashOfInput == storedHash;
        }


    }
}

