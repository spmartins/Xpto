using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using XPTOWebApp.Models;

namespace XPTOWebApp.Controllers
{
    public class LoginController : BaseController
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult LoginValidation(UserModel model)
        {
            if (ModelState.IsValid)
            {
                ServiceReference1.Authenticate authenticate = ConvertToServiceAuthenticate(model);
                var validUser = Client.AuthenticateUser(authenticate);
                if (validUser)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, false);

                    return RedirectToAction("Index","Home");                 
                }
            }

            var errorModel = new ErrorModel { Title = "Error", Message = "Login error" };
            return PartialView("_ErrorMessage", errorModel);

        }

        [AllowAnonymous]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            AccessAuthorizeAttribute.ClearUserSession(HttpContext);
            return RedirectToAction("Login");
        }

        private ServiceReference1.Authenticate ConvertToServiceAuthenticate(UserModel user)
        {
            //TODO: validate client side data
            ServiceReference1.Authenticate authenticate = new ServiceReference1.Authenticate
            {
                Email = user.Email,
                Password = user.Password
            };

            return authenticate;
        }
    }
}