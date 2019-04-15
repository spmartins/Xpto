using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XPTODAL;
using XptoModel;
using XptoModel.Validator;

namespace XPTOBLL
{
    public class RolesBLL : BaseBusiness<RolesBLL>
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(RolesBLL));

        private XPTOEntities _XptoEntities { get; set; }
        protected XPTOEntities Xpto
        {
            get { return _XptoEntities ?? (_XptoEntities = new XPTOEntities()); }
        }

        public IQueryable<Role> GetAllRoles()
        {
            return from x in Xpto.Roles
                   orderby x.RoleName
                   select x;
        }

        public Role GetRoleById(int roleId)
        {
            var query = from x in Xpto.Roles
                        where x.RoleId == roleId
                        select x;

            return query.FirstOrDefault();
        }

        public bool CreateRole(Role role)
        {
            try
            {
                return AddEntity(Xpto, role);

            }
            catch (Exception ex)
            {
                Log.Error("CreateRole: ", ex);
                return false;
            }

        }

        public bool UpdateRole(Role role)
        {
            try
            {
                return UpdateEntity(Xpto, role, typeof(RoleValidator));
            }
            catch (Exception ex)
            {
                Log.Error("UpdateRole: ", ex);
                return false;
            }

        }

        public bool DeleteRole(int roleId)
        {
            try
            {
                var role = Xpto.Roles.Where(e => e.RoleId == roleId).FirstOrDefault();
                role.Active = false;

                return UpdateEntity(Xpto, role, typeof(RoleValidator));
            }
            catch (Exception ex)
            {
                Log.Error("DeleteRole: ", ex);
                return false;
            }

        }
    }
}
