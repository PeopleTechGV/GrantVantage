using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAClient = ATS.DataAccess;
using BEClient = ATS.BusinessEntity;

namespace ATS.BusinessLogic
{
    public class ApplicationBasedStatusAction : ActionBase
    {
        #region private data member
        private DAClient.ApplicationBasedStatusRepository _ApplicationBasedStatusRepository;
        private Guid _MyClientId { get; set; }

        #endregion

        #region Constructor

        public ApplicationBasedStatusAction(Guid ClientId)
            : base(ClientId)
        {


            _MyClientId = ClientId;
            _ApplicationBasedStatusRepository = new DAClient.ApplicationBasedStatusRepository(base.ConnectionString);
            _ApplicationBasedStatusRepository.CurrentUser = base.CurrentUser;
            _ApplicationBasedStatusRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public Guid AddAppBasedStatus(BEClient.ApplicationBasedStatus pApplicationBasedStatus)
        {
            try
            {
                _ApplicationBasedStatusRepository.BeginTransaction();
                DAClient.EntityLanguageRepository EntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                EntityLanguageRepository.Transaction = _ApplicationBasedStatusRepository.Transaction;
                EntityLanguageRepository.CurrentUser = _ApplicationBasedStatusRepository.CurrentUser;
                EntityLanguageRepository.CurrentClient = _ApplicationBasedStatusRepository.CurrentClient;
                string fieldValue = string.Empty;
                fieldValue = string.Format("{0} = '{1}' and {2} = {3}", BEClient.Common.CommonTblVal.StatusText, pApplicationBasedStatus.StatusText, BEClient.Common.CommonTblVal.IsDelete, 0);
                Guid AppBasedStatusId = _ApplicationBasedStatusRepository.AddApplicationBasedStatusStatus(pApplicationBasedStatus, fieldValue);
                foreach (BEClient.EntityLanguage clientLanguage in pApplicationBasedStatus.ApplicationBasedStatusEntityLanguage)
                {
                    clientLanguage.EntityName = pApplicationBasedStatus.Privilage;
                    clientLanguage.RecordId = AppBasedStatusId;
                    EntityLanguageRepository.AddEntityLanguage(clientLanguage);

                }
                _ApplicationBasedStatusRepository.CommitTransaction();
                return AppBasedStatusId;
            }
            catch
            {
                _ApplicationBasedStatusRepository.RollbackTransaction();
                throw;
            }
        }


        public bool UpdateAppBasedStatus(BEClient.ApplicationBasedStatus pAppBasedStatus)
        {
            try
            {
                _ApplicationBasedStatusRepository.BeginTransaction();

                string fieldValue = string.Empty;
                fieldValue = string.Format("{0} = '{1}' and {2} != '{3}' and {4} = {5}", BEClient.Common.CommonTblVal.StatusText, pAppBasedStatus.StatusText, BEClient.Common.AppBasedStatusTbl.ApplicationBasedStatusId, pAppBasedStatus.ApplicationBasedStatusId, BEClient.Common.CommonTblVal.IsDelete, 0);

                DAClient.EntityLanguageRepository EntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                EntityLanguageRepository.Transaction = _ApplicationBasedStatusRepository.Transaction;
                EntityLanguageRepository.CurrentUser = _ApplicationBasedStatusRepository.CurrentUser;
                EntityLanguageRepository.CurrentClient = _ApplicationBasedStatusRepository.CurrentClient;

                bool recordUpdated = _ApplicationBasedStatusRepository.Update(pAppBasedStatus, fieldValue);

                if (recordUpdated)
                {
                    foreach (BEClient.EntityLanguage clientLanguage in pAppBasedStatus.ApplicationBasedStatusEntityLanguage)
                    {
                        EntityLanguageRepository.UpdateEntityLanguage(new BEClient.EntityLanguage()
                        {
                            EntityName = pAppBasedStatus.Privilage,
                            LanguageId = clientLanguage.LanguageId,
                            LocalName = clientLanguage.LocalName,
                            ShowToCandidate = clientLanguage.ShowToCandidate,
                            RecordId = pAppBasedStatus.ApplicationBasedStatusId
                        }
                        );
                    }
                }
                _ApplicationBasedStatusRepository.CommitTransaction();
                return recordUpdated;
            }
            catch
            {
                _ApplicationBasedStatusRepository.RollbackTransaction();
                throw;
            }
        }

        public bool Delete(string applicationBasedStatusId)
        {
            try
            {
                _ApplicationBasedStatusRepository.BeginTransaction();
                bool Result = _ApplicationBasedStatusRepository.Delete(applicationBasedStatusId);
                if (Result)
                {
                    _ApplicationBasedStatusRepository.CommitTransaction();
                    return Result;
                }
                else
                {
                    _ApplicationBasedStatusRepository.RollbackTransaction();
                    return Result;
                }


            }
            catch
            {
                _ApplicationBasedStatusRepository.RollbackTransaction();
                throw;
            }
        }

        public List<BEClient.ApplicationBasedStatus> GetAllAppBasedStatus(Guid LanguageId)
        {
            try
            {

                return _ApplicationBasedStatusRepository.GetAllApplicationBasedStatus(LanguageId);

            }
            catch
            { throw; }
        }

        public BEClient.ApplicationBasedStatus GetrecordByRecordId(Guid recordid)
        {
            try
            {
                BEClient.ApplicationBasedStatus objAppBasedStatus = _ApplicationBasedStatusRepository.GetRecordByRecordId(recordid);
                objAppBasedStatus.ApplicationBasedStatusEntityLanguage = new List<BEClient.EntityLanguage>();

                DAClient.EntityLanguageRepository _ObjEntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                objAppBasedStatus.ApplicationBasedStatusEntityLanguage = _ObjEntityLanguageRepository.GetEntityLanguageByEntityAndRecordId(objAppBasedStatus.Privilage, objAppBasedStatus.ApplicationBasedStatusId);

                return objAppBasedStatus;
            }
            catch
            {
                throw;
            }
        }

        public bool IsApplicationDecline(Guid ApplicationId)
        {
            try
            {
                return _ApplicationBasedStatusRepository.IsApplicationDecline(ApplicationId);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.ApplicationBasedStatus GetEmailContentByABSId(Guid ApplicationBasedStatusId, Guid ApplicationId)
        {
            try
            {
                return _ApplicationBasedStatusRepository.GetEmailContentByABSId(ApplicationBasedStatusId, ApplicationId);
            }
            catch
            {
                throw;
            }
        }
    }
}
