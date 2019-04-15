using log4net;
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
    public class DepartmentsController : BaseController
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(DepartmentsController));

        public ActionResult Index()
        {
            var departmentList = Client.GetAllDepartments();          
            return View(ConvertToAppDepartmentList(departmentList));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceReference1.Department department = Client.GetDepartmentById(id.Value);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(ConvertToAppDepartment(department));
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DepartmentModel department)
        {
            if (ModelState.IsValid)
            {
                var d = ConvertToServiceDepartment(department);
                var created = Client.CreateDepartment(d);

                if (created) { return RedirectToAction("Index"); }
                else
                {
                    var errorModel = new ErrorModel { Title = "Error", Message = "Error creating Department" };
                    return PartialView("_ErrorMessage", errorModel);
                }

            }
            return View(department);
        }

        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceReference1.Department department = Client.GetDepartmentById(id.Value);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(ConvertToAppDepartment(department));
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DepartmentModel department)
        {
            if (ModelState.IsValid)
            {
                var d = ConvertToServiceDepartment(department);
                var created = Client.UpdateDepartment(d);
                if (created) { return RedirectToAction("Index"); }
                else
                {
                    var errorModel = new ErrorModel { Title = "Error", Message = "Error Updating Department" };
                    return PartialView("_ErrorMessage", errorModel);
                }
            }
            return View(department);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceReference1.Department department = Client.GetDepartmentById(id.Value);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(ConvertToAppDepartment(department));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceReference1.Department department = Client.GetDepartmentById(id);
            if(department != null)
            {
                var deleted = Client.DeleteDepartment(id);
                if (deleted) { return RedirectToAction("Index"); }
                else
                {
                    var errorModel = new ErrorModel { Title = "Error", Message = "Error Deleting Department" };
                    return PartialView("_ErrorMessage", errorModel);
                }
            }

            return RedirectToAction("Index");
        }

        #region Auxiliar

        private ServiceReference1.Department ConvertToServiceDepartment(DepartmentModel model)
        {
            //TODO: validate client side data
            ServiceReference1.Department d = new ServiceReference1.Department
            {
                DepartmentName = model.DepartmentName,
                Active = model.Active,
                ModifiedBy = User.UserId,
                LastUpdate = model.LastUpdate
            };

            return d;
        }

        private DepartmentModel ConvertToAppDepartment(ServiceReference1.Department department)
        {
            DepartmentModel d = new DepartmentModel
            {
                DepartmentName = department.DepartmentName,
                Active = department.Active,
                ModifiedBy = department.ModifiedBy,
                LastUpdate = department.LastUpdate
            };

            return d;
        }

        private List<DepartmentModel> ConvertToAppDepartmentList(Department[] departmentList)
        {
            var model = (from d in departmentList
                         select new DepartmentModel {
                             DepartmentId = d.DepartmentId,
                             DepartmentName = d.DepartmentName,
                             Active = d.Active,
                             ModifiedBy = d.ModifiedBy,
                             LastUpdate = d.LastUpdate
                         }).ToList();
            return model;
                     
        }

        #endregion
    }
}
