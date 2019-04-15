using System;
using System.Collections.Generic;
using System.Text;

namespace XPTOBLL.Models
{
    public class UserRoles
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool Deleted { get; set; }
        public int ModifiedBy { get; set; }
        public Nullable<System.DateTime> LastUpdate { get; set; }

    }
}
