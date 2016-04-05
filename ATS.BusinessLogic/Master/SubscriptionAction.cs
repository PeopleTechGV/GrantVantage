using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAMaster = ATS.DataAccess.Master;
using BEMaster = ATS.BusinessEntity.Master;
using BE = ATS.BusinessEntity;

namespace ATS.BusinessLogic.Master
{
    public class SubscriptionAction : ActionBase
    {
        private DAMaster.SubscriptionRepository objSubscriptionRepository;

        public SubscriptionAction()
        {
            base.SetMasterDatabsaeConnectionString();
            objSubscriptionRepository = new DAMaster.SubscriptionRepository();
        }

        public List<BEMaster.Subscription> GetAllSubscription()
        {
            try
            {
                return objSubscriptionRepository.GetAllSubscription();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public BEMaster.Subscription GetSubscriptionById(Guid subscriptionId)
        {
            try
            {
                return objSubscriptionRepository.GetSubscriptionById(subscriptionId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Guid InsertSubscription(BEMaster.Subscription objSubscription)
        {
            try
            {
                objSubscriptionRepository.BeginTransaction();
                Guid subscriptionId = Guid.Empty;
                string fieldValue = string.Empty;
                fieldValue = string.Format("{0} = '{1}' and {2} = {3}", BE.Common.SubscriptionTbl.UserLimit, objSubscription.UserLimit, BE.Common.CommonTblVal.IsDelete, 0);
                subscriptionId = objSubscriptionRepository.InsertSubscription(objSubscription, fieldValue);
                if (subscriptionId == null)
                {
                    objSubscriptionRepository.RollbackTransaction();
                    return Guid.Empty;
                }
                objSubscriptionRepository.CommitTransaction();
                return subscriptionId;
            }
            catch
            {
                objSubscriptionRepository.RollbackTransaction();
                throw;
            }
        }

        public bool UpdateSubscriptionById(BEMaster.Subscription objSubscription)
        {
            try
            {
                objSubscriptionRepository.BeginTransaction();
                string fieldValue = string.Empty;
                fieldValue = string.Format("{0} = '{1}' and {2} != '{3}' and {4} = {5}", BE.Common.SubscriptionTbl.UserLimit, objSubscription.UserLimit, BE.Common.SubscriptionTbl.SubscriptionId, objSubscription.SubscriptionId, BE.Common.CommonTblVal.IsDelete, 0);

                bool updateLanguageResult = false;
                updateLanguageResult = objSubscriptionRepository.UpdateSubscriptionById(objSubscription, fieldValue);
                if (!updateLanguageResult)
                {
                    objSubscriptionRepository.RollbackTransaction();
                    return false;
                }
                objSubscriptionRepository.CommitTransaction();
                return updateLanguageResult;
            }
            catch
            {
                objSubscriptionRepository.RollbackTransaction();
                throw;
            }
        }
    }
}
