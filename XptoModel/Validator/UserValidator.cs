using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XptoModel.Validator
{
    public class UserValidator
    {
        [Key]
        [Required(ErrorMessage = "Required")]
        public int UserId { get; set; }


        [Required(ErrorMessage = "Required")]
        [StringLength(260, ErrorMessage = "Max Length 260 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(128, ErrorMessage = "Max Length 128 characters", MinimumLength = 10)]
        public string Password { get; set; }


        [Required(ErrorMessage = "Required")]
        public bool Deleted { get; set; }

        [Required(ErrorMessage = "Required")]
        public int ModifiedBy { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> LastUpdate { get; set; }
    }
}
