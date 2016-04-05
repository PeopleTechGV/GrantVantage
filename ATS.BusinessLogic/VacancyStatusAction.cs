using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic
{
    public class VacancyStatusAction : ActionBase
    {
        #region private data member
        private DAClient.VacancyStatusRepository _VacancyStatusRepository;
        private Guid _MyClientId { get; set; }

        #endregion
        #region Constructor

        public VacancyStatusAction(Guid ClientId)
            : base(ClientId)
        {


            _MyClientId = ClientId;
            _VacancyStatusRepository = new DAClient.VacancyStatusRepository(base.ConnectionString);
            _VacancyStatusRepository.CurrentUser = base.CurrentUser;
            _VacancyStatusRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public List<BEClient.VacancyStatus> GetAllVacancyStatusByClientAndLanguage(Guid LanguageId)
        {
            try
            {
                return _VacancyStatusRepository.GetAllVacancyStatusByClientAndLanguage(LanguageId);
            }
            catch
            {
                throw;
            }
        }
        public List<BEClient.VacancyStatus> GetVacancyStatusByCategoryAndlanguage(Guid languageId, string Category)
        {
            try
            {
                return _VacancyStatusRepository.GetAllVacancyStatusByCategoryAndLanguage(languageId, Category);
            }
            catch
            {
                throw;
            }
        }
        public BEClient.VacancyStatus GetRecordByRecordId(Guid recordId)
        {
            try
            {
                BEClient.VacancyStatus objVacancyStatus = _VacancyStatusRepository.GetRecordByRecordId(recordId);
                objVacancyStatus.VacancyStatusEntityLanguage = new List<BEClient.EntityLanguage>();

                DAClient.EntityLanguageRepository _ObjEntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                objVacancyStatus.VacancyStatusEntityLanguage = _ObjEntityLanguageRepository.GetEntityLanguageByEntityAndRecordId(objVacancyStatus.Privilage, objVacancyStatus.VacancyStatusId);


                return objVacancyStatus;
            }
            catch
            {
                throw;
            }
        }

        public Guid AddVacancyStatus(BEClient.VacancyStatus pVacancyStatus)
        {
            try
            {
                _VacancyStatusRepository.BeginTransaction();
                DAClient.EntityLanguageRepository EntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                EntityLanguageRepository.Transaction = _VacancyStatusRepository.Transaction;
                EntityLanguageRepository.CurrentUser = _VacancyStatusRepository.CurrentUser;
                EntityLanguageRepository.CurrentClient = _VacancyStatusRepository.CurrentClient;
                string fieldValue = string.Empty;
                fieldValue = string.Format("{0} = '{1}' and {2} = {3}", BEClient.Common.CommonTblVal.VacancyStatusText, pVacancyStatus.VacancyStatusText, BEClient.Common.CommonTblVal.IsDelete, 0);
                Guid VacancyStatusId = _VacancyStatusRepository.AddVacancyStatus(pVacancyStatus, fieldValue);
                foreach (BEClient.EntityLanguage clientLanguage in pVacancyStatus.VacancyStatusEntityLanguage)
                {
                    clientLanguage.EntityName = pVacancyStatus.Privilage;
                    clientLanguage.RecordId = VacancyStatusId;
                    EntityLanguageRepository.AddEntityLanguage(clientLanguage);

                }
                _VacancyStatusRepository.CommitTransaction();
                return VacancyStatusId;
            }
            catch
            {
                _VacancyStatusRepository.RollbackTransaction();
                throw;
            }
        }

        public bool Delete(string recordid)
        {
            try
            {
                bool Result = false;
                _VacancyStatusRepository.BeginTransaction();
                Result = _VacancyStatusRepository.Delete(recordid);
                if (Result)
                {
                    _VacancyStatusRepository.CommitTransaction();
                    return Result;
                }
                return Result;

            }
            catch
            {
                _VacancyStatusRepository.RollbackTransaction();
                throw;
            }

        }

        public bool UpdateVacancyStatus(BEClient.VacancyStatus pVacancyStatus)
        {
            try
            {
                _VacancyStatusRepository.BeginTransaction();

                string fieldValue = string.Empty;
                fieldValue = string.Format("{0} = '{1}' and {2} != '{3}' and {4} = {5}", BEClient.Common.CommonTblVal.VacancyStatusText, pVacancyStatus.VacancyStatusText, BEClient.Common.VacancyStatusTbl.VacancyStatusId, pVacancyStatus.VacancyStatusId, BEClient.Common.CommonTblVal.IsDelete, 0);

                DAClient.EntityLanguageRepository EntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                EntityLanguageRepository.Transaction = _VacancyStatusRepository.Transaction;
                EntityLanguageRepository.CurrentUser = _VacancyStatusRepository.CurrentUser;
                EntityLanguageRepository.CurrentClient = _VacancyStatusRepository.CurrentClient;

                bool recordUpdated = _VacancyStatusRepository.Update(pVacancyStatus, fieldValue);

                if (recordUpdated)
                {
                    foreach (BEClient.EntityLanguage clientLanguage in pVacancyStatus.VacancyStatusEntityLanguage)
                    {
                        EntityLanguageRepository.UpdateEntityLanguage(new BEClient.EntityLanguage()
                        {
                            EntityName = pVacancyStatus.Privilage,
                            LanguageId = clientLanguage.LanguageId,
                            LocalName = clientLanguage.LocalName,
                            RecordId = pVacancyStatus.VacancyStatusId
                        }
                        );
                    }
                }
                _VacancyStatusRepository.CommitTransaction();
                return recordUpdated;
            }
            catch
            {
                _VacancyStatusRepository.RollbackTransaction();
                throw;
            }
        }
        public Guid GetVacancyReasonIdByVacancyStatus(string Category)
        {
            try
            {
                return _VacancyStatusRepository.GetVacancyReasonIdByVacancyStatus(Category);
            }
            catch
            {
                throw;
            }
        }
    }
}
