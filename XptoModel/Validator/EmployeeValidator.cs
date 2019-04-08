using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace XPTOBLL.Validator
{
    public class EmployeeValidator
    {
        [Key]
        [Required(ErrorMessage ="Required")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(260, ErrorMessage = "Max Length 260 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(260, ErrorMessage = "Max Length 260 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(260, ErrorMessage = "Max Length 260 characters",MinimumLength = 10)]
        public string Password { get; set; }

        [StringLength(260, ErrorMessage = "Max Length 260 characters")]
        public string MobilePhoneNumber { get; set; }

        [StringLength(260, ErrorMessage = "Max Length 260 characters")]
        public string OfficePhoneNumber { get; set; }

        [Required(ErrorMessage = "Required")]
        public byte DepartmentId { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        [DataType(DataType.Date)]
        public System.DateTime HireDate { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(260, ErrorMessage = "Max Length 260 characters")]
        public string Email { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> ExitDate { get; set; }

        [Required(ErrorMessage = "Required")]
        public bool Deleted { get; set; }

        [Required(ErrorMessage = "Required")]
        public int ModifiedBy { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> LastUpdate { get; set; }

    }
}
