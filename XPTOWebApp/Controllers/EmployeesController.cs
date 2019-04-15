using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using XPTOWebApp.Models;
using XPTOWebApp.ServiceReference1;

namespace XPTOWebApp.Controllers
{
    [AccessAuthorize(Roles = "Administrator,ManageEmployees")]

    public class EmployeesController : BaseController
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(EmployeesController));

         public ActionResult Index()
        {
            var employeeList = Client.GetAllEmployees();
            return View(ConvertToAppEmployeeList(employeeList));
        }

        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceEmployee employee = Client.GetEmployeeById(id.Value);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(ConvertToAppEmployee(employee));
        }

        public ActionResult Create()
        {

            ViewBag.DepartmentId = new SelectList(Client.GetAllDepartments(), "DepartmentId", "DepartmentName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeModel employee)
        {
            if (ModelState.IsValid)
            {
                var e = ConvertToServiceEmployee(employee);
                var created = Client.CreateEmployee(e);

                if (created) { return RedirectToAction("Index"); }
                else
                {
                    var errorModel = new ErrorModel { Title = "Error", Message = "Error creating Employee" };
                    return PartialView("_ErrorMessage", errorModel);
                }

            }
            return View(employee);
        }

        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceEmployee employee = Client.GetEmployeeById(id.Value);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(Client.GetAllDepartments(), "DepartmentId", "DepartmentName");
            return View(ConvertToAppEmployee(employee));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeModel employee)
        {
            if (ModelState.IsValid)
            {
                var e = ConvertToServiceEmployee(employee);
                var created = Client.UpdateEmployee(e);
                if (created) { return RedirectToAction("Index"); }
                else
                {
                    var errorModel = new ErrorModel { Title = "Error", Message = "Error Updating Employee" };
                    return PartialView("_ErrorMessage", errorModel);
                }
            }
            ViewBag.DepartmentId = new SelectList(Client.GetAllDepartments(), "DepartmentId", "DepartmentName");
            return View(employee);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceEmployee employee = Client.GetEmployeeById(id.Value);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(ConvertToAppEmployee(employee));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceEmployee employee = Client.GetEmployeeById(id);
            if (employee != null)
            {
                var deleted = Client.DeleteEmployee(id);
                if (deleted) { return RedirectToAction("Index"); }
                else
                {
                    var errorModel = new ErrorModel { Title = "Error", Message = "Error Deleting Employee" };
                    return PartialView("_ErrorMessage", errorModel);
                }
            }

            return RedirectToAction("Index");
        }

        #region Auxiliar
        private List<EmployeeModel> ConvertToAppEmployeeList(ServiceEmployee[] employeeList)
        {
            var model = (from e in employeeList
                         select new EmployeeModel
                         {
                             EmployeeId = e.EmployeeId,
                             FirstName = e.FirstName,
                             LastName = e.LastName,
                             MobilePhoneNumber = e.MobilePhoneNumber,
                             OfficePhoneNumber = e.OfficePhoneNumber,
                             DepartmentId = e.DepartmentId,
                             DepartmentName = e.DepartmentName,
                             Email = e.Email,
                             ExitDate = e.ExitDate,
                             Deleted = e.Deleted,
                             ModifiedBy = e.ModifiedBy,
                             LastUpdate = e.LastUpdate
                         }).ToList();
            return model;

        }

        private EmployeeModel ConvertToAppEmployee(ServiceEmployee employee)
        {
            EmployeeModel e = new EmployeeModel
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                MobilePhoneNumber = employee.MobilePhoneNumber,
                OfficePhoneNumber = employee.OfficePhoneNumber,
                DepartmentId = employee.DepartmentId,
                DepartmentName = employee.DepartmentName,
                HireDate = employee.HireDate,
                Email = employee.Email,
                ExitDate = employee.ExitDate,
                Deleted = employee.Deleted,
                ModifiedBy = employee.ModifiedBy,
                LastUpdate = employee.LastUpdate
            };

            return e;
        }

        private Employee ConvertToServiceEmployee(EmployeeModel model)
        {
            //TODO: validate client side data
            Employee d = new Employee
            {
                EmployeeId = model.EmployeeId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                HireDate = model.HireDate,
                MobilePhoneNumber = model.MobilePhoneNumber,
                OfficePhoneNumber = model.OfficePhoneNumber,
                DepartmentId = model.DepartmentId,
                Email = model.Email,
                ExitDate = model.ExitDate,
                Deleted = model.Deleted,
                ModifiedBy = User.UserId,
                LastUpdate = DateTime.Now
            };

            return d;
        }

        #endregion

    }
}
