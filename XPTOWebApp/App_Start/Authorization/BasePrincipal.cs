using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using static XPTOWebApp.Helper.EnumHelper;

namespace XPTOWebApp
{
  
        public class BasePrincipal : GenericPrincipal, IPrincipal
        {
        public BasePrincipal(IIdentity baseIdentity, string[] roles, int userId)
        : base(baseIdentity, roles)
        {
            Roles = new HashSet<string>(roles);
            UserId = userId;
            IsAdministrator = roles != null && Roles.Contains(DefaultRoles.Administrator);
            CanManageDepartments = roles != null &&  Roles.Contains(DefaultRoles.ManageDepartments);
            CanManageEmployees = roles != null && Roles.Contains(DefaultRoles.ManageEmployees);
        }

        public int UserId { get; }

        public string Email { get; }

        public HashSet<string> Roles { get; private set; }

        public HashSet<int> RolesLst { get; private set; }

        public bool IsAdministrator { get; set; }
        public bool CanManageDepartments{ get; set; }
        public bool CanManageEmployees { get; set; }

        public bool HasRole(string role)
        {
            if (Roles != null) {
                if (role.Contains(","))
                {
                    string[] roles = role.Split(',');
                    foreach (var r in roles)
                    {
                        return Roles.Contains(r);
                    }
                }
                else
                {
                    return Roles.Contains(role);
                }
            }

            return false;         
        }
    }
}