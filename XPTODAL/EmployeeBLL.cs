using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XPTOBLL.Validator;
using XPTODAL;
using XptoModel;

namespace XPTOBLL
{
    public class EmployeeBLL: BaseBusiness<EmployeeBLL>
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(EmployeeBLL));

        private XPTOEntities XptoEntities { get; set; }

        protected XPTOEntities Xpto
        {
            get { return XptoEntities ?? (new XPTOEntities()); }
        }

        public IQueryable<Employee> GetAllEmployees()
        {
            return from x in Xpto.Employees
                   orderby x.EmployeeId
                   select x;
        }

        public IQueryable<EmployeeRole> GetEmployeeRoles(int employeeId)
        {
            return from x in Xpto.EmployeeRoles
                   where x.EmployeeId == employeeId
                   select x;
        }

        public Employee GetEmployeeById(int employeeId)
        {
            var query = from x in Xpto.Employees
                   where x.EmployeeId == employeeId
                   select x;

            return query.FirstOrDefault();
        }

        public bool CreateEmployee(Employee employee)
        {
            try
            {
                if (!CheckIfEmailExists(employee.Email)){
                    employee.Password = MD5Crypt.Encrypt(employee.Password);
                    return AddEntity(Xpto, employee);
                }
                return false;
               
            }
            catch (Exception ex)
            {
                Log.Error("CreateEmployee", ex);
                return false;
            }

        }

        public bool UpdateEmployee(Employee employee)
        {
            try
            {
                employee.Password = MD5Crypt.Encrypt(employee.Password);
                return UpdateEntity(Xpto, employee, typeof(EmployeeValidator));
            }
            catch (Exception ex)
            {
                Log.Error("CreateEmployee", ex);
                return false;
            }

        }

        public bool DeleteEmployee(int employeeId)
        {
            try
            {
                var employee = Xpto.Employees.Where(e => e.EmployeeId == employeeId).FirstOrDefault();
                employee.Deleted = true;
                                
                return UpdateEntity(Xpto, employee, typeof(EmployeeValidator));
            }
            catch (Exception ex)
            {
                Log.Error("CreateEmployee", ex);
                return false;
            }

        }

        public bool CheckIfEmailExists(string email)
        {
            return Xpto.Employees.Any(e => e.Email == email);
        }
    }
}
