
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XPTOWebApp.Models
{
    public class RolesModel
    {
      
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool Active { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime? LastUpdate { get; set; }

    }
}