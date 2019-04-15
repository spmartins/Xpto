using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace XPTOWebApp.Models
{
    public class UserModel
    {
        public int UserId { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage ="Email format error")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public bool Deleted { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? LastUpdate { get; set; }

        public virtual UserRoleModel UserRole { get; set; }

    }
}