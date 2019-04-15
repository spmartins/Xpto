using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XPTODAL;
using XptoModel;
using XptoModel.Validator;

namespace XPTOBLL
{
    public class DepartmentBLL : BaseBusiness<DepartmentBLL>
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(DepartmentBLL));

        private XPTOEntities _XptoEntities { get; set; }
        protected XPTOEntities Xpto
        {
            get { return _XptoEntities ?? (_XptoEntities = new XPTOEntities()); }
        }

        public IQueryable<Department> GetAllDepartments()
        {
            return from x in Xpto.Departments
                   where x.Active
                   orderby x.DepartmentName
                   select x;
        }


        public Department GetDepartmentById(int departmentId)
        {
            var query = from x in Xpto.Departments
                        where x.DepartmentId == departmentId
                        select x;

            return query.FirstOrDefault();
        }

        public bool CreateDepartment(Department department)
        {
            try
            {
                return AddEntity(Xpto, department);

            }
            catch (Exception ex)
            {
                Log.Error("CreateDepartment", ex);
                return false;
            }

        }

        public bool UpdateDepartment(Department department)
        {
            try
            {
                return UpdateEntity(Xpto, department, typeof(DepartmentValidator));
            }
            catch (Exception ex)
            {
                Log.Error("UpdateDepartment", ex);
                return false;
            }

        }

        public bool DeleteDepartment(int departmentId)
        {
            try
            {
                var department = Xpto.Departments.Where(e => e.DepartmentId == departmentId).FirstOrDefault();
                department.Active = false;

                return UpdateEntity(Xpto, department, typeof(DepartmentValidator));
            }
            catch (Exception ex)
            {
                Log.Error("DeleteDepartment", ex);
                return false;
            }

        }
    }
}
