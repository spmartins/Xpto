using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Transactions;
using XptoModel;

namespace XPTODAL
{
    public abstract class BaseBusiness<T> where T : class, new()
    {
        #region SingleTon

        private static T _instance;

        public static T Instance
        {
            get { return _instance ?? (_instance = new T()); }
        }

        #endregion SingleTon

        private void Add<TEntity>(XPTOEntities xptoEntities, TEntity entity) where TEntity : class
        {
            xptoEntities.Set<TEntity>().Add(entity);
            xptoEntities.SaveChanges();
        }

        private void Update<TEntity>(XPTOEntities xptoEntities, TEntity entity) where TEntity : class
        {
           xptoEntities.SaveChanges();
        }

        protected virtual List<ValidationResult> IsValid<TEntity>(TransactionScope transaction, TEntity entity, Type entityValidator) where TEntity : class
        {
            ValidationContext context = new ValidationContext(entity, null, null);
            List<ValidationResult> results = new List<ValidationResult>();
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(entity.GetType(), entityValidator), entity.GetType());
            if (Validator.TryValidateObject(entity, context, results, true))
                return null;

            return results;
        }


        public bool UpdateEntity<TEntity>(XPTOEntities xptoEntities, TEntity entity, Type entityValidator, TransactionScope transaction = null) where TEntity : class
        {
            bool isValid = false;
            try
            {
                var listResult = IsValid(transaction, entity, entityValidator);
                if (listResult == null)
                {
                    xptoEntities.SaveChanges();
                    isValid = true;
                }

                return isValid;
            }
            catch (Exception ex)
            {
                return isValid;
            }
        }

        public bool AddEntity<TEntity>(XPTOEntities xptoEntities, TEntity entity, Type entityValidator = null, TransactionScope transaction = null) where TEntity : class
        {
            try
            {
                if (entityValidator == null) 
                {
                    Add(xptoEntities,entity);
                    return true;
                }

                var listResult = IsValid(transaction, entity, entityValidator);
                if (listResult == null)
                {
                    Add(xptoEntities,entity);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                
                return false;
            }

        }



    }
}
