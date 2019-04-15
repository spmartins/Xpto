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
       
        private readonly Employee _emp = new Employee
        {
            FirstName = "TestName",
            LastName = "TestLastName",
            MobilePhoneNumber = "Test123456789",
            OfficePhoneNumber = "Test123456789",
            DepartmentId = 1,
            HireDate = DateTime.Now,
            ExitDate = DateTime.Now,
            Deleted = false,
            ModifiedBy = 1
        };

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
        [DataRow("TestEmail1@enear.co")]
        [DataRow("TestEmail2@enear.co")]
        public void TestCreateEmployee(string email)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                _emp.Email = email;
                var employee = _service.CreateEmployee(_emp);
                var employees = _service.GetAllEmployees();
                bool employeeCreated = employees.Where(e=> e.Email == _emp.Email).Any() ? true : false;

                Assert.AreEqual(true, employeeCreated, "Employee created");
            }
        }

        [TestMethod]
        public void TestDeleteEmployee()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                bool validate = false;
                var employee = _service.GetAllEmployees().FirstOrDefault();
                if (employee != null)
                {
                    validate = _service.DeleteEmployee(employee.EmployeeId);
                }
                Assert.AreEqual(true, validate, "Employee deleted");
            }
        }


    }
}
