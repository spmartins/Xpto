//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace XptoModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string OfficePhoneNumber { get; set; }
        public int DepartmentId { get; set; }
        public System.DateTime HireDate { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> ExitDate { get; set; }
        public bool Deleted { get; set; }
        public int ModifiedBy { get; set; }
        public Nullable<System.DateTime> LastUpdate { get; set; }
    
        public virtual Department Department { get; set; }
    }
}
