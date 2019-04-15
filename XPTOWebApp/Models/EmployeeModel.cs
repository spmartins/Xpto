using System;
using System.ComponentModel.DataAnnotations;

namespace XPTOWebApp.Models
{
    public class EmployeeModel
    {
        public int EmployeeId { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Mobile Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string MobilePhoneNumber { get; set; }
        [Display(Name = "Office Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string OfficePhoneNumber { get; set; }
        [Required]
        [Display(Name = "Department Name")]
        public int DepartmentId { get; set; }
        [Display(Name = "Department Name")]
        public string DepartmentName{ get; set; }
        [Required]
        [Display(Name = "Hire Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime HireDate { get; set; }
        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Email format error")]
        public string Email { get; set; }
        [Display(Name = "Exit Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? ExitDate { get; set; }
        [Display(Name = "Deleted")]
        public bool Deleted { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}