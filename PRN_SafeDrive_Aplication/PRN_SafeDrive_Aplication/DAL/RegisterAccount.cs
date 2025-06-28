

using PRN_SafeDrive_Aplication.Models;

namespace PRN_SafeDrive_Aplication.DAL
{
    public class RegisterAccount
    {
        public Prn1Context dbcontext  = new Prn1Context();
        // đăng ký tài khoảng mới 
        public bool Register(string username, string password, string salt ,string email, string role)
        {
            var listUser = dbcontext.Users.ToList();

            dbcontext.Users.Add(new User
            {
                FullName = username,
                Password = password,
                Salt = salt,
                Email = email,
                Role = role
            });

            int row = dbcontext.SaveChanges();

            return row != 0 ? true : false;
        }


        // kiểm tra xem tên đăng nhập đã có người sử dụng hay chưa
        public bool IsUsernameAvailable(string email)
        {
            return dbcontext.Users.Any(s => s.FullName == email);
        }








    }
}
