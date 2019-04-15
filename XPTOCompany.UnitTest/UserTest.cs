using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using XptoModel;
using XPTOWcfService;

namespace XPTOCompany.UnitTest
{
    [TestClass]
    public class UserTest
    {
        private readonly Authenticate _authenticate = new Authenticate();

        private readonly User _user = new User
        {
            Email = "TestUserEmail",
            Password = "TestPassword",
        };

        private Service _service;

        [TestInitialize]
        public void SetUp()
        {
            _service = new Service();
        }

        [TestMethod]
        [DataRow("TestEmail2@enear.co", "123456")]
        [DataRow("TestEmail3@enear.co", "789101")]
        public void TestAuthenticateUser(string email, string password)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                bool validate = false;
                _user.Email = email;
                _user.Password = password;
                if (_service.CreateUser(_user))
                {
                    _authenticate.Email = email;
                    _authenticate.Password = password;
                    validate = _service.AuthenticateUser(_authenticate);
                }
                Assert.AreEqual(true, validate, "User Validated");
            }
        }


        [TestMethod]
        [DataRow("TestEmailuser1@enear.co", "123456")]
        [DataRow("TestEmailuser2@enear.co", "789101")]
        public void TestCreateUser(string email, string password)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                bool validate = false;
                _user.Email = email;
                _user.Password = password;
                validate = _service.CreateUser(_user);

                Assert.AreEqual(true, validate, "User Validated");
            }
        }

        [TestMethod]
        public void TestDeleteUser()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                bool validate = false;
                var user = _service.GetAllUsers().FirstOrDefault();
                if (user != null)
                {
                    validate = _service.DeleteUser(user.UserId);
                }
                Assert.AreEqual(true, validate, "User deleted");
            }
        }

    }
}
