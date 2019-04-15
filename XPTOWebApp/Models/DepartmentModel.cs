using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace XPTOWebApp.Models
{
    public class DepartmentModel
    {
        public int DepartmentId { get; set; }
        [Required]
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
        [Required]
        [Display(Name = "Active")]
        public bool Active { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}