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

        bool CreateEmployee(Employee employee);

        bool EmailExists(string email);

        Employee GetEmployeeById(int employeeId);

        bool DeleteEmployee(int employeeId);

        bool UpdateEmployee(Employee employee);

        IQueryable<Employee> GetAllEmployees();

        bool CreateDepartment(Department department);

        bool UpdateDepartment(Department department);

        bool DeleteDepartment(int departmentId);

        IQueryable<Department> GetAllDepartments();

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
    public class FaultContract
    {
        [DataMember]
        public string Operation { get; set; }

        [DataMember]
        public string Error_Message { get; set; }
    }
}
