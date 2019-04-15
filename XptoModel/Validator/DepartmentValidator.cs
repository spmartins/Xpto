using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XptoModel.Validator
{
    public class DepartmentValidator
    {
        [Key]
        [Required(ErrorMessage = "Required")]
        public int DepartmentId { get; set; }


        [Required(ErrorMessage = "Required")]
        [StringLength(260, ErrorMessage = "Max Length 260 characters")]
        public string DepartmentName { get; set; }

        [Required(ErrorMessage = "Required")]
        public bool Active { get; set; }

        [Required(ErrorMessage = "Required")]
        public int ModifiedBy { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        [DataType(DataType.Date)]
        public DateTime? LastUpdate { get; set; }
    }
}
