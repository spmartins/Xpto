using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using XPTOWebApp.Models;

namespace XPTOWebApp.Controllers
{
    public class UsersController : BaseController
    {

        // GET: Users
        public ActionResult Index()
        {
            var users = Client.GetAllUsers();
            return View(users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceReference1.User user = Client.GetUserById(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(Client.GetAllUsers(), "UserId", "UserId");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserModel user)
        {
            if (ModelState.IsValid)
            {
                var u = ConvertToServiceUser(user);
                var created = Client.CreateUser(u);

                if (created) { return RedirectToAction("Index"); }
                else {
                    var errorModel = new ErrorModel {Title = "Error", Message= "Error creating user"};
                    return PartialView( "_ErrorMessage", errorModel);
                }
                
            }
            return View(user);
        }



        private ServiceReference1.User ConvertToServiceUser(UserModel user)
        {
            //TODO: validate client side data
            ServiceReference1.User u = new ServiceReference1.User
            {
                Email = user.Email,
                Password = user.Password,
                Deleted = false,
                ModifiedBy = User.UserId,
                LastUpdate = DateTime.Now
            };

            return u;
        }
    }
}
