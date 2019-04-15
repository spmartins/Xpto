using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XPTOBLL.Models;
using XPTOBLL.Validator;
using XPTODAL;
using XptoModel;

namespace XPTOBLL
{
    public class EmployeeBLL: BaseBusiness<EmployeeBLL>
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(EmployeeBLL));

        private XPTOEntities _XptoEntities { get; set; }

        protected XPTOEntities Xpto
        {
            get { return _XptoEntities ?? (_XptoEntities = new XPTOEntities()); }
        }

        public IQueryable<Employees> GetAllEmployees()
        {
            return (from e in Xpto.Employees
                    join d in Xpto.Departments on e.DepartmentId equals d.DepartmentId
                    orderby e.EmployeeId
                    select new Employees
                    {
                        EmployeeId = e.EmployeeId,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        HireDate = e.HireDate,
                        MobilePhoneNumber = e.MobilePhoneNumber,
                        OfficePhoneNumber = e.OfficePhoneNumber,
                        DepartmentId = e.DepartmentId,
                        DepartmentName = d.DepartmentName,
                        Email = e.Email,
                        ExitDate = e.ExitDate,
                        Deleted = e.Deleted,
                        ModifiedBy = e.ModifiedBy,
                        LastUpdate = e.LastUpdate
                    });
        }


        public Employees GetEmployeeById(int employeeId)
        {
            var query = from e in Xpto.Employees
                        join d in Xpto.Departments on e.DepartmentId equals d.DepartmentId
                        where e.EmployeeId == employeeId
                        select new Employees
                        {
                            EmployeeId = e.EmployeeId,
                            FirstName = e.FirstName,
                            LastName = e.LastName,
                            HireDate = e.HireDate,
                            MobilePhoneNumber = e.MobilePhoneNumber,
                            OfficePhoneNumber = e.OfficePhoneNumber,
                            DepartmentId = e.DepartmentId,
                            DepartmentName = d.DepartmentName,
                            Email = e.Email,
                            ExitDate = e.ExitDate,
                            Deleted = e.Deleted,
                            ModifiedBy = e.ModifiedBy,
                            LastUpdate = e.LastUpdate
                        };

            return query.FirstOrDefault();
        }

        public bool CreateEmployee(Employee employee)
        {
            try
            {
                return AddEntity(Xpto, employee);

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
                return UpdateEntity(Xpto, employee, typeof(EmployeeValidator));
            }
            catch (Exception ex)
            {
                Log.Error("UpdateEmployee", ex);
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
                Log.Error("DeleteEmployee", ex);
                return false;
            }

        }
        
    }
}
