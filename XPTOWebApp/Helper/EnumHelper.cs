using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace XPTOWebApp.Helper
{
    public class EnumHelper
    {
        public enum DefaultRoleTemplates : int
        {
            [Description("Administrator")]
            Administrator = 1,
            [Description("ManageDepartments")]
            ManageDepartments = 2,
            [Description("ManageEmployees")]
            ManageEmployees = 3
        }

        public static class DefaultRoles
        {
            public const string Administrator = "Administrator";
            public const string ManageDepartments = "ManageDepartments";
            public const string ManageEmployees = "ManageEmployees";
        }
    }
}