using log4net;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;
using XptoModel;
using System.Linq;
using XPTODAL;
using XptoModel.Validator;
using XPTOBLL.Models;

namespace XPTOBLL
{
    public class UserBLL : BaseBusiness<UserBLL>
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(UserBLL));
        private XPTOEntities _XptoEntities { get; set; }

        protected XPTOEntities Xpto
        {
            get { return _XptoEntities ?? (_XptoEntities = new XPTOEntities()); }
        }

        public bool CreateUser(User user)
        {
            try
            {
                if (!CheckIfEmailExists(user.Email))
                {
                    user.Password = MD5Crypt.Encrypt(user.Password);
                    return AddEntity(Xpto, user);
                }
                return false;

            }
            catch (Exception ex)
            {
                Log.Error("CreateUser", ex);
                return false;
            }

        }

        public bool UpdateUser(User user)
        {
            try
            {
                user.Password = MD5Crypt.Encrypt(user.Password);
                return UpdateEntity(Xpto, user, typeof(UserValidator));
            }
            catch (Exception ex)
            {
                Log.Error("UpdateUser", ex);
                return false;
            }

        }

        public bool DeleteUser(int userId)
        {
            try
            {
                var user = Xpto.Users.Where(e => e.UserId == userId).FirstOrDefault();
                user.Deleted = true;

                return UpdateEntity(Xpto, user, typeof(UserValidator));
            }
            catch (Exception ex)
            {
                Log.Error("DeleteUser", ex);
                return false;
            }

        }

        public IQueryable<User> GetAllUsers()
        {
            return from x in Xpto.Users
                   orderby x.UserId
                   select x;
        }

        public User GetUserById(int userId)
        {
            var query = from x in Xpto.Users
                        .Include("UserRole")
                        where x.UserId == userId
                        select x;

            return query.FirstOrDefault();
        }


        public IQueryable<UserRoles> GetUserRoles(int userId)
        {
                return (from x in Xpto.UserRoles
                        join r in Xpto.Roles on x.RoleId equals r.RoleId
                        where x.UserId == userId
                        select new UserRoles
                        {
                            UserId = x.UserId,
                            RoleId = x.RoleId,
                            RoleName = r.RoleName,
                            Deleted = x.Deleted,
                            LastUpdate = x.LastUpdate,
                            ModifiedBy = x.ModifiedBy
                        });
        }
        public bool ValidateUser(string email, string password)
        {
            var result = false;
            try
            {
                if (!string.IsNullOrEmpty(email))
                {
                    var tempPass = Xpto.Users.Where(u => u.Email == email)
                                   .Select(u => u.Password)
                                   .FirstOrDefault();
                    result = tempPass.Equals(MD5Crypt.Encrypt(password));
                }

                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return result;
            }
        }

        public bool CheckIfEmailExists(string email)
        {
            return Xpto.Users.Any(u => u.Email == email);
        }

    }
}
