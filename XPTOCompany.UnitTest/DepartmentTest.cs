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
    public class DepartmentTest
    {
        private Service _service;

        private readonly Department _department = new Department
        {
            Active = true,
            ModifiedBy = 1,
        };

        [TestInitialize]
        public void SetUp()
        {
            _service = new Service();
        }

        [TestMethod]
        [DataRow("DepartmentTest1")]
        [DataRow("DepartmentTest2")]
        public void TestCreateDepartment(string name)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                _department.DepartmentName = name;
                var dep = _service.CreateDepartment(_department);
                var deps = _service.GetAllDepartments();
                bool depCreated = deps.Where(e => e.DepartmentName == _department.DepartmentName).Any() ? true : false;

                Assert.AreEqual(true, depCreated, "Department created");
            }
        }

        [TestMethod]
        public void TestDeleteDepartment()
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                bool validate = false;
                var dep = _service.GetAllDepartments().FirstOrDefault();
                if (dep != null)
                {
                    validate = _service.DeleteDepartment(dep.DepartmentId);
                }
                Assert.AreEqual(true, validate, "User deleted");
            }
        }
    }
}
