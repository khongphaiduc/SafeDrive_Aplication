using PRN_SafeDrive_Aplication.BiLL;
using PRN_SafeDrive_Aplication.DAL;
using System.Data;

namespace ProjectTestUnit
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

            // Arrange

            var register = new Registers();

            // Act

            bool resut = register.Register("ptrungduc111111", "123", "123", "ptrungduc101@gmail111111.com", "Teacher");


            // Assert
            Assert.True(resut);   //  kỳ vọng đúng  :  nếu kỳ vọng sai thì sử dung Assert.False(result);

        }



        [Theory]
        [InlineData("user151", "123", "123", "ptrungduc101@gmail212.com", "Teacher", true)]
        [InlineData("user116", "abc", "abc", "ptrungduc101@gmail1234.com", "Teacher", true)]
        [InlineData("user118", "xyz", "xyz", "ptrungduc101@gmail211.com", "TrafficPolice", true)]
        public void Register_ShouldReturnTrue_WhenValid(string username, string password, string salt, string email, string role, bool expected)
        {
            // Arrange
            var register = new Registers();

            // Act
            bool result = register.Register(username, password, salt, email, role);

            // Assert
            Assert.Equal(result, expected);
        }



        [Theory]
        [InlineData("ptrungduc", false)] // Tên đăng nhập đã tồn tại
        [InlineData("sontrungmpt", false)] // Tên đăng nhập đã tồn tại

        public void Test2(string account, bool expected)
        {
            // Arrange
            var register = new Registers();
            // Act
            bool result = register.IsUsernameAvailable(account);
            // Assert
            Assert.Equal(result, expected);  // kỳ vọng là tên đăng nhập đã tồn tại, nên kết quả là false
        }



        [Fact]

        public void Test3()
        {

            var result = HashingPasswork.getSalt("123");
            
            Assert.Equal(result, string.Empty); 
        }





    }
}