using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Security;
using XPTOBLL;
using XptoModel;

namespace XPTOWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service : IService
    {

        private static readonly ILog Log = LogManager.GetLogger(typeof(Service));
        #region employee

        public bool CreateEmployee(Employee employee)
        {
            return EmployeeBLL.Instance.CreateEmployee(employee);
        }

        public bool DeleteEmployee(int employeeId)
        {
            return EmployeeBLL.Instance.DeleteEmployee(employeeId);
        }

        public IQueryable<Employee> GetAllEmployees()
        {
            return EmployeeBLL.Instance.GetAllEmployees();
        }

        public Employee GetEmployeeById(int employeeId)
        {
            return EmployeeBLL.Instance.GetEmployeeById(employeeId);
        }

        public bool UpdateEmployee(Employee employee)
        {
            return EmployeeBLL.Instance.UpdateEmployee(employee);
        }

        public bool EmailExists(string email) {

            return EmployeeBLL.Instance.CheckIfEmailExists(email);
        }

        #endregion

        #region Department
        public bool CreateDepartment(Department department)
        {
            throw new NotImplementedException();
        }

        public bool DeleteDepartment(int departmentId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateDepartment(Department department)
        {
            throw new NotImplementedException();
        }


        public IQueryable<Department> GetAllDepartments()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Authenticate
        public bool AuthenticateUser(Authenticate authenticate)
        {
            return AccountBLL.Instance.ValidateUser(authenticate.Email, authenticate.Password);
        }

        private bool ValidateUserData(Authenticate authenticate)
        {
            return string.IsNullOrEmpty(authenticate.Email) ||
                   string.IsNullOrEmpty(authenticate.Password) ? false : true;
        }
        #endregion

    }
}

