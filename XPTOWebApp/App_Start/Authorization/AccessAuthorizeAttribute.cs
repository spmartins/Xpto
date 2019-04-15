using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using XPTOWebApp.ServiceReference1;

namespace XPTOWebApp
{
    public class AccessAuthorizeAttribute : AuthorizeAttribute
    {
        private ServiceClient ServiceClient { get; set; }
        protected ServiceClient Client
        {
            get { return ServiceClient ?? new ServiceClient(); }
        }

        public const string XptoPrincipalSessionKey = "XptoPrincipal";

        public string GetAuthenticationName(HttpContextBase httpContext)
        {
            string username = httpContext.User.Identity.Name;
            return ResolveUsernameFromDomainName(username);
        }

        private string ResolveUsernameFromDomainName(string domainName)
        {
            string[] username = Regex.Split(domainName, Regex.Escape("\\"));
            if (username.Length > 1)
            {
                return username[1];
            }
            return domainName;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // Returns HTTP 401 by default - see HttpUnauthorizedResult.cs.
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    {"action", "Index"},
                    {"controller", "Error"},
                    {"statusCode", "401"}
                });
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
                if (httpContext.Session != null)
                {
                    var basePrincipal = httpContext.Session[XptoPrincipalSessionKey] as BasePrincipal;
                    if (basePrincipal != null)
                    {
                        httpContext.User = basePrincipal;
                        return string.IsNullOrEmpty(Roles) || (httpContext.User as BasePrincipal).HasRole(Roles);
                    }               
                }
                if (AuthorizeUser(httpContext))
                {
                    return string.IsNullOrEmpty(Roles) || (httpContext.User as BasePrincipal).HasRole(Roles);
                }
                return false;
        }

        private bool AuthorizeUser(HttpContextBase httpContext)
        {
            var authorize = false;
            var principal = BuildBasePrincipal(httpContext);
            if (principal != null)
            {
                httpContext.User = principal;
                if (httpContext.Session != null)
                {
                    httpContext.Session[XptoPrincipalSessionKey] = principal;
                }
                authorize = true;
            }
            return authorize;
        }

        private BasePrincipal BuildBasePrincipal(HttpContextBase httpContext)
        {
                if (httpContext == null)
                {
                    throw new ArgumentNullException("httpContext");
                }

                var userName = GetAuthenticationName(httpContext);
                var user = Client.GetAllUsers().Where(u => u.Email == userName).FirstOrDefault();
                var roles = GetUserRoles(user.UserId);

                
                var principal = new BasePrincipal(httpContext.User.Identity, roles,
                                    user.UserId);

               return principal;
        }

        private string[] GetUserRoles(int userId)
        {
            var roles = Client.GetUserRoles(userId);
            List<string> rolesList = new List<string>();

            foreach (var r in roles)
            {
                rolesList.Add(r.RoleName);
            }
            return rolesList.ToArray();

        }

        public static void ClearUserSession(HttpContextBase httpContext)
        {
            if (httpContext.Session != null)
            {
                httpContext.Session.Clear();
            }
        }
    }
}