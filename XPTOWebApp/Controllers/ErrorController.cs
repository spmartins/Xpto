using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XPTOWebApp.Models;

namespace XPTOWebApp.Controllers
{
    public class ErrorController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Index(int? statusCode, Exception exception = null)
        {
            var errorModel = new ErrorModel { Title = "Error", Message = "Unauthorized" };
            return View("_ErrorMessage", errorModel);
        }
    }
}