using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XPTOWebApp.ServiceReference1;

namespace XPTOWebApp.Controllers
{
    [AccessAuthorize]
    public class BaseController : Controller
    {
        public BasePrincipal User { get { return base.User as BasePrincipal; } }

        private ServiceClient ServiceClient { get; set; }
        protected ServiceClient Client
        {
            get { return ServiceClient ?? new ServiceClient(); }
        }
    }
}