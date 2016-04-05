using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAClient = ATS.DataAccess;
using BEClient = ATS.BusinessEntity;

namespace ATS.BusinessLogic
{
    public class RndTypeAction : ActionBase
    {
        #region private data member
        private DAClient.RndTypeRepository _RndTypeRepository;
        private Guid _MyClientId { get; set; }
        #endregion


        #region Constructor

        public RndTypeAction(Guid ClientId)
            : base(ClientId)
        {

            _MyClientId = ClientId;
            _RndTypeRepository = new DAClient.RndTypeRepository(base.ConnectionString);
            _RndTypeRepository.CurrentUser = base.CurrentUser;
            _RndTypeRepository.CurrentClient = base.CurrentClient;
        }
        #endregion




        public BEClient.RndType GetRecordByRecordId(Guid recordId, Guid languageId)
        {
            try
            {
                BEClient.RndType objRndType = _RndTypeRepository.GetRecordByRecordId(recordId, languageId);

                objRndType.RndTypeEntityLanguage = new List<BEClient.EntityLanguage>();
                DAClient.EntityLanguageRepository _ObjEntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                objRndType.RndTypeEntityLanguage = _ObjEntityLanguageRepository.GetEntityLanguageByEntityAndRecordId(objRndType.Privilage, objRndType.RndTypeId);

                return objRndType;
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.RndType> GetAllRndTypeByLanguage(Guid LanguageId)
        {
            try
            {
                return _RndTypeRepository.GetAllRndType(LanguageId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.RndType> GetRndTypeForRndConfig(Guid VacancyId, Guid LanguageId)
        {
            try
            {
                return _RndTypeRepository.GetRndTypeForRndConfig(VacancyId, LanguageId);
            }
            catch
            {
                throw;
            }
        }
        public List<BEClient.RndType> GetRndTypeForTRndConfig(Guid TVacId, Guid LanguageId)
        {
            try
            {
                return _RndTypeRepository.GetRndTypeForTRndConfig(TVacId, LanguageId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.RndType> GetAllRndTypeByVacancy(Guid VacId, Guid VacRndId, Guid LanguageId)
        {
            try
            {
                return _RndTypeRepository.GetAllRndTypeByVacancy(VacId, VacRndId, LanguageId);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.RndType GetRoundDetailsByVacRndId(Guid VacRndId, Guid LanguageId)
        {
            try
            {
                return _RndTypeRepository.GetRoundDetailsByVacRndId(VacRndId, LanguageId);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.RndType GetRoundDetailsByTVacRndId(Guid TVacRndId, Guid LanguageId)
        {
            try
            {
                return _RndTypeRepository.GetRoundDetailsByTVacRndId(TVacRndId, LanguageId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.RndType> GetAllRndTypeByTVac(Guid TVacId, Guid VacRndId, Guid LanguageId)
        {
            try
            {
                return _RndTypeRepository.GetAllRndTypeByTVac(TVacId, VacRndId, LanguageId);
            }
            catch
            {
                throw;
            }
        }

        public Guid AddRndType(BEClient.RndType pRndType)
        {
            try
            {
                _RndTypeRepository.BeginTransaction();

                DAClient.EntityLanguageRepository EntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                EntityLanguageRepository.Transaction = _RndTypeRepository.Transaction;
                EntityLanguageRepository.CurrentUser = _RndTypeRepository.CurrentUser;
                EntityLanguageRepository.CurrentClient = _RndTypeRepository.CurrentClient;
                pRndType.IsActive = true;
                string fieldValue = string.Empty;
                fieldValue = string.Format("{0} = '{1}' and {2} = {3}", BEClient.Common.CommonTblVal.DefaultName, pRndType.DefaultName, BEClient.Common.CommonTblVal.IsDelete, 0);

                Guid RndTypeId = _RndTypeRepository.AddRndType(pRndType, fieldValue);

                foreach (BEClient.EntityLanguage clientLanguage in pRndType.RndTypeEntityLanguage)
                {
                    clientLanguage.EntityName = pRndType.Privilage;
                    clientLanguage.RecordId = RndTypeId;
                    EntityLanguageRepository.AddEntityLanguage(clientLanguage);
                }
                _RndTypeRepository.CommitTransaction();
                return RndTypeId;
            }
            catch
            {
                _RndTypeRepository.RollbackTransaction();
                throw;
            }
        }
        public List<Guid> GetRndTypeIdByCandidateSelfEvalution()
        {
            try
            {
                int[] type = { (int)BEClient.QuestionType.QT_SELF };
                return _RndTypeRepository.GetRndTypeIdByQuestionType(type);
            }
            catch (Exception)
            {

                throw;
            }

        }
         public List<Guid> GetRestrictedRounds()
        {
            try
            {
        
                return _RndTypeRepository.GetRestrictedRounds();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<Guid> GetCandidateSurveyRndTypeId()
        {
            try
            {
                return _RndTypeRepository.GetCandidateSurveyRndType();
            }
            catch (Exception)
            {

                throw;
            }

        }


        public bool UpdateRndType(BEClient.RndType RndType)
        {
            try
            {
                _RndTypeRepository.BeginTransaction();

                DAClient.EntityLanguageRepository EntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                EntityLanguageRepository.Transaction = _RndTypeRepository.Transaction;
                EntityLanguageRepository.CurrentUser = _RndTypeRepository.CurrentUser;
                EntityLanguageRepository.CurrentClient = _RndTypeRepository.CurrentClient;
                string fieldValue = string.Empty;
                fieldValue = string.Format("{0} = '{1}' and {2} != '{3}' and {4} = {5}", BEClient.Common.CommonTblVal.DefaultName, RndType.DefaultName, BEClient.Common.RndTypeTbl.RndTypeId, RndType.RndTypeId, BEClient.Common.CommonTblVal.IsDelete, 0);
                bool recordUpdated = _RndTypeRepository.UpdateRndType(RndType, fieldValue);

                if (recordUpdated)
                {
                    foreach (BEClient.EntityLanguage clientLanguage in RndType.RndTypeEntityLanguage)
                    {
                        EntityLanguageRepository.UpdateEntityLanguage(new BEClient.EntityLanguage()
                        {
                            EntityName = RndType.Privilage,
                            LanguageId = clientLanguage.LanguageId,
                            LocalName = clientLanguage.LocalName,
                            ShowToCandidate = clientLanguage.ShowToCandidate,
                            RecordId = RndType.RndTypeId
                        }
                        );
                    }
                }

                _RndTypeRepository.CommitTransaction();
                return recordUpdated;
            }
            catch
            {
                throw;
            }
        }

        public bool Delete(string recordid)
        {
            try
            {
                return _RndTypeRepository.Delete(recordid);
            }
            catch
            {
                throw;
            }
        }

        public bool IsRndTypeConfigured(string RndTypeId)
        {
            try
            {
                return _RndTypeRepository.IsRndTypeConfigured(RndTypeId);
            }
            catch
            {
                throw;
            }

        }
        public bool CheckForApplicationRound(Guid VacancyId)
        {
            try
            {
                return _RndTypeRepository.CheckForApplicationRound(VacancyId);
            }
            catch
            {
                throw;
            }
        }

    }
}

