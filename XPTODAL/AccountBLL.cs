using log4net;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;
using XptoModel;
using System.Linq;
using XPTODAL;

namespace XPTOBLL
{
    public class AccountBLL : BaseBusiness<AccountBLL>
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(AccountBLL));
        private XPTOEntities XptoEntities { get; set; }

        protected XPTOEntities Xpto
        {
            get { return XptoEntities ?? (new XPTOEntities()); }
        }


        public bool ValidateUser(string email, string password)
        {
            var result = false;
            try
            {
                if (!string.IsNullOrEmpty(email))
                {
                    var tempPass = Xpto.Employees.Where(e => e.Email == email)
                                   .Select(e => e.Password)
                                   .FirstOrDefault();
                    result = tempPass == MD5Crypt.Encrypt(password);
                }

                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return result;
            }
        }

    }
}
