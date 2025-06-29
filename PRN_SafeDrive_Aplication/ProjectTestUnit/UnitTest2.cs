using PRN_SafeDrive_Aplication.BiLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTestUnit
{
    public class UnitTest2
    {
        [Theory]
        [InlineData("Phạm trung Đức","123","ptrungduc1011@gmail.com","Teacher")]
        [InlineData("Phạm Trung Long", "123", "ptrungduc10111@gmail.com", "Student")]
        [InlineData("Duong Bình Nhật Minh", "123", "ptrungduc10112@gmail.com", "TrafficPolice")]
        public void testRegisterUser(string username, string password, string email, string role)
        {
           
            bool result = UserLog.RegisterUser(username, password, email, role);

            Assert.True(result);

        }


        [Theory]
        [InlineData("ptrungduc1011@gmail.com","123")]
        public void TestLoginUser(string email, string password)
        {
            bool result = UserLog.LoginUser(email, password);
            Assert.True(result);
        }


    }
}
