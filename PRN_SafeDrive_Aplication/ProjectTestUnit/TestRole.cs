using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTestUnit
{
    public class TestRole
    {

        [Theory]
        [InlineData("t1@gmail.com","Teacher")]
        [InlineData("ptrungduc10112@gmail.com", "TrafficPolice")]
        [InlineData("ptrungduc10111@gmail.com", "Student")]
        public void TestRoleUser(string email, string expectvalue)
        {
            string role = PRN_SafeDrive_Aplication.Log.Login.GetRoleByEmail(email);
               
            Assert.Equal(expectvalue, role);
        }

    }
}
