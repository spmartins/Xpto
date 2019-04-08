using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Transactions;
using System.Linq;
using XPTOWcfService;
using XptoModel;
using System;

namespace XPTO.UnitTest
{
    [TestClass]
    public class EmployeeTest
    {
        private readonly Authenticate _authenticate = new Authenticate();
        private readonly Employee _emp = new Employee
        {
            FirstName = "TestName",
            LastName = "TestLastName",
            Password = "TestPassword",
            MobilePhoneNumber = "Test123456789",
            OfficePhoneNumber = "Test123456789",
            DepartmentId = 1,
            HireDate = DateTime.Now,
            ExitDate = DateTime.Now,
            Deleted = false,
            ModifiedBy = 1
        };

        private XPTOEntities _xptoEntities;

        [TestInitialize]
        public void SetUp()
        {
            _xptoEntities = new XPTOEntities();
        }


        [TestMethod]
        [DataRow("TestEmail1@enear.co")]
        [DataRow("TestEmail2@enear.co")]
        public void TestCreateEmployee(string email)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                XPTOWcfService.Service service = new XPTOWcfService.Service();
                _emp.Email = email;
                var employee = service.CreateEmployee(_emp);
                var employees = service.GetAllEmployees();
                bool employeeCreated = employees.Where(e=> e.Email == _emp.Email).Any() ? true : false;

                Assert.AreEqual(true, employeeCreated, "Employee created");
            }
        }

        [TestMethod]
        [DataRow("TestEmail2@enear.co","123456")]
        [DataRow("TestEmail3@enear.co","789101")]
        public void TestAuthenticateUser(string email, string password)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                bool validate = false;
                XPTOWcfService.Service service = new XPTOWcfService.Service();
                _emp.Email = email;
                _emp.Password = password;
                if (service.CreateEmployee(_emp))
                {
                    _authenticate.Email = email;
                    _authenticate.Password = password;
                    validate = service.AuthenticateUser(_authenticate);
                }
                Assert.AreEqual(true, validate, "User Validated");
            }
        }

        [TestMethod]
        public void TestDeleteEmployee()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                bool validate = false;
                XPTOWcfService.Service service = new XPTOWcfService.Service();
                var employee = service.GetAllEmployees().FirstOrDefault();
                if (employee != null)
                {
                    validate = service.DeleteEmployee(employee.EmployeeId);
                }
                Assert.AreEqual(true, validate, "Employee deleted");
            }
        }


    }
}
