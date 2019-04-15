using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Security;
using XptoModel;

namespace XPTOWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService
    {

        [OperationContract]
        bool AuthenticateUser(Authenticate authenticate);
        [OperationContract]
        bool CreateUser(User user);
        [OperationContract]
        bool DeleteUser(int userId);
        [OperationContract]
        User GetUserById(int userId);
        [OperationContract]
        IQueryable<ServiceUserRoles> GetUserRoles(int userId);
        [OperationContract]
        IQueryable<Role> GetAllRoles();
        [OperationContract]
        IQueryable<User> GetAllUsers();
        [OperationContract]
        bool CreateEmployee(Employee employee);
        [OperationContract]
        bool EmailExists(string email);
        [OperationContract]
        ServiceEmployee GetEmployeeById(int employeeId);
        [OperationContract]
        bool DeleteEmployee(int employeeId);
        [OperationContract]
        bool UpdateEmployee(Employee employee);
        [OperationContract]
        IQueryable<ServiceEmployee> GetAllEmployees();
        [OperationContract]
        bool CreateDepartment(Department department);
        [OperationContract]
        bool UpdateDepartment(Department department);
        [OperationContract]
        bool DeleteDepartment(int departmentId);
        [OperationContract]
        IQueryable<Department> GetAllDepartments();
        [OperationContract]
        Department GetDepartmentById(int departmentId);

    }

    [DataContract]
    public class Authenticate
    {
        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Password { get; set; }
    }
    [DataContract]
    public class ServiceUserRoles
    {
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public int RoleId { get; set; }
        [DataMember]
        public string RoleName { get; set; }
        [DataMember]
        public bool Deleted { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> LastUpdate { get; set; }
    }

    [DataContract]
    public class ServiceEmployee
    {
        [DataMember]
        public int EmployeeId { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string MobilePhoneNumber { get; set; }
        [DataMember]
        public string OfficePhoneNumber { get; set; }
        [DataMember]
        public int DepartmentId { get; set; }
        [DataMember]
        public System.DateTime HireDate { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public Nullable<System.DateTime> ExitDate { get; set; }
        [DataMember]
        public bool Deleted { get; set; }
        [DataMember]
        public int ModifiedBy { get; set; }
        [DataMember]
        public Nullable<System.DateTime> LastUpdate { get; set; }
        [DataMember]
        public string DepartmentName{ get; set; }
    }


        [DataContract]
    public class FaultContract
    {
        [DataMember]
        public string Operation { get; set; }

        [DataMember]
        public string Error_Message { get; set; }
    }
}
