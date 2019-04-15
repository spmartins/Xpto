using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XPTOWebApp.Models
{
    public class UserRoleModel
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public bool Deleted { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? LastUpdate { get; set; }

        public virtual RolesModel Role { get; set; }
        public virtual UserModel User { get; set; }
    }
}