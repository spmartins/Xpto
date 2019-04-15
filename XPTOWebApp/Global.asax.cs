using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using XPTOWebApp.Controllers;

namespace XPTOWebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private readonly ILog log = LogManager.GetLogger(typeof(MvcApplication));
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();

            var objErr = ex.GetBaseException();
            var err = string.Format("Error caught in Application_Error event\n \nError Message:{0} \n \nStack Trace:{1}", objErr.Message,
                objErr.StackTrace);

            log.Error(err, ex);
            Server.ClearError();

            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", "Error");
            routeData.Values.Add("action", "Index");

            IController controller = new ErrorController();
            RequestContext requestContext = new RequestContext(new HttpContextWrapper(Context), routeData);

            controller.Execute(requestContext);
        }
    }
}
