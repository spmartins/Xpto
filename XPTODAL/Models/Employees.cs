using System;
using System.Collections.Generic;
using System.Text;

namespace XPTOBLL.Models
{
    public class Employees
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string OfficePhoneNumber { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public System.DateTime HireDate { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> ExitDate { get; set; }
        public bool Deleted { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
