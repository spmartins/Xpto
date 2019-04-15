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
            try
            {
                return EmployeeBLL.Instance.CreateEmployee(employee);
            }
            catch (Exception ex)
            {
                Log.Error("CreateEmployee", ex);
                SendException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString());
            }

            return false;
        }


        public bool DeleteEmployee(int employeeId)
        {
            try
            {
                return EmployeeBLL.Instance.DeleteEmployee(employeeId);
            }
            catch (Exception ex)
            {
                Log.Error("CreateEmployee", ex);
                SendException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString());
            }
            return false;
        }

        public IQueryable<ServiceEmployee> GetAllEmployees()
        {
            var employees =  EmployeeBLL.Instance.GetAllEmployees();
            return (from e in employees
                    select new ServiceEmployee
                    {
                        EmployeeId = e.EmployeeId,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        HireDate = e.HireDate,
                        MobilePhoneNumber = e.MobilePhoneNumber,
                        OfficePhoneNumber = e.OfficePhoneNumber,
                        DepartmentId = e.DepartmentId,
                        DepartmentName = e.DepartmentName,
                        Email = e.Email,
                        ExitDate = e.ExitDate,
                        Deleted = e.Deleted,
                        ModifiedBy = e.ModifiedBy,
                        LastUpdate = e.LastUpdate
                    });
        }

        public ServiceEmployee GetEmployeeById(int employeeId)
        {
            var e = EmployeeBLL.Instance.GetEmployeeById(employeeId);
            return  new ServiceEmployee
            {
                EmployeeId = e.EmployeeId,
                FirstName = e.FirstName,
                LastName = e.LastName,
                HireDate = e.HireDate,
                MobilePhoneNumber = e.MobilePhoneNumber,
                OfficePhoneNumber = e.OfficePhoneNumber,
                DepartmentId = e.DepartmentId,
                DepartmentName = e.DepartmentName,
                Email = e.Email,
                ExitDate = e.ExitDate,
                Deleted = e.Deleted,
                ModifiedBy = e.ModifiedBy,
                LastUpdate = e.LastUpdate
            };
        }

        public bool UpdateEmployee(Employee employee)
        {
            try
            {
                if (employee != null)
                {
                    return EmployeeBLL.Instance.UpdateEmployee(employee);
                }
            }
            catch (Exception ex)
            {
                Log.Error("UpdateEmployee", ex);
                return false;
            }

            return false;
        }

        private XptoModel.Employee PopulateEmployeeModel(Employee employee, bool isUpdate)
        {
            var emp =  new XptoModel.Employee
            {
                 FirstName = employee.FirstName,
                 LastName = employee.LastName,
                 MobilePhoneNumber = employee.MobilePhoneNumber,
                 OfficePhoneNumber = employee.OfficePhoneNumber,
                 DepartmentId = employee.DepartmentId,
                 HireDate = employee.HireDate,
                 Email = employee.Email,
                 ExitDate = employee.ExitDate,
                 Deleted = employee.Deleted,
                 ModifiedBy = employee.ModifiedBy,
                 LastUpdate = employee.LastUpdate
            };

            if (isUpdate)
            {
                emp.EmployeeId = employee.EmployeeId;
            }

            return emp;
        }

        #endregion

        #region Department
        public bool CreateDepartment(Department department)
        {
            try
            {
                return DepartmentBLL.Instance.CreateDepartment(department);
            }
            catch (Exception ex)
            {
                Log.Error("CreateDepartment", ex);
                SendException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString());
            }
            return false;
        }

        public bool DeleteDepartment(int departmentId)
        {
            try
            {
                return DepartmentBLL.Instance.DeleteDepartment(departmentId);
            }
            catch (Exception ex)
            {
                Log.Error("CreateDepartment", ex);
                SendException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString());
            }

            return false;
        }

        public bool UpdateDepartment(Department department)
        {
            try
            {
                if (department != null)
                {
                    return DepartmentBLL.Instance.UpdateDepartment(department);
                }
            }
            catch (Exception ex)
            {
                Log.Error("UpdateEmployee", ex);
                SendException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString());
            }

            return false;
        }


        public IQueryable<Department> GetAllDepartments()
        {
            return DepartmentBLL.Instance.GetAllDepartments();
        }

        public Department GetDepartmentById(int departmentId)
        {
            return DepartmentBLL.Instance.GetDepartmentById(departmentId);
        }

        #endregion

        #region User
        public bool AuthenticateUser(Authenticate authenticate)
        {
            return UserBLL.Instance.ValidateUser(authenticate.Email, authenticate.Password);
        }

        public bool CreateUser(User user) {
            return UserBLL.Instance.CreateUser(user);
        }

        public bool DeleteUser(int userId)
        {
            return UserBLL.Instance.DeleteUser(userId);
        }

        public User GetUserById(int userId)
        {
            return UserBLL.Instance.GetUserById(userId);
        }
        public IQueryable<ServiceUserRoles> GetUserRoles(int userId)
        {
            var userRoles =  UserBLL.Instance.GetUserRoles(userId);
            return (from r in userRoles
                   select new ServiceUserRoles
                   {
                       UserId = r.UserId,
                       RoleId = r.RoleId,
                       RoleName = r.RoleName,
                       Deleted = r.Deleted,
                       ModifiedBy = r.ModifiedBy,
                       LastUpdate = r.LastUpdate,
                   });
        }

        public IQueryable<Role> GetAllRoles() {

            return RolesBLL.Instance.GetAllRoles();
           
        }
        public IQueryable<User> GetAllUsers()
        {
            return UserBLL.Instance.GetAllUsers();
        }

        private bool ValidateUserData(Authenticate authenticate)
        {
            return string.IsNullOrEmpty(authenticate.Email) ||
                   string.IsNullOrEmpty(authenticate.Password) ? false : true;
        }

        public bool EmailExists(string email)
        {

            return UserBLL.Instance.CheckIfEmailExists(email);
        }
        #endregion

        public void SendException(string operation, string message)
        {
            throw new FaultException<FaultContract>(new FaultContract
            {
                Error_Message = message,
                Operation = operation
            }, new FaultReason(message));
        }
    }
}

