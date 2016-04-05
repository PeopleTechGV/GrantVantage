using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;
using BESrp = ATS.BusinessEntity.SRPEntity;

namespace ATS.BusinessLogic
{
    public class VacancyRoundAction : ActionBase
    {
        #region private data member
        private DAClient.VacancyRoundRepository _VacancyRoundRepository;
        private Guid _MyClientId { get; set; }
        private Guid _CurrentLanguageId;
        //private BESrp.SRPJobLocation _SRPJobLocation { get; set; }
        //private BESrp.SRPJobLocation SRPJobLocation { get { return SRPJobLocation; } }
        #endregion

        public VacancyRoundAction(Guid ClientId, bool CreateSRPObject = false)
            : base(ClientId)
        {
            if (CreateSRPObject)
                base.CreateSRPObject(SRPCommon.CreateSRPEntityObjects.SRPCreateObj<BESrp.SRPVacancy>(ClientId));

                _MyClientId = ClientId;
            _VacancyRoundRepository = new DAClient.VacancyRoundRepository(base.ConnectionString);
            _VacancyRoundRepository.CurrentUser = base.CurrentUser;
            _VacancyRoundRepository.CurrentClient = base.CurrentClient;
        }

        public Guid AddVacancyRound(BEClient.VacancyRound objVacancyRound)
        {
            try
            {
                _VacancyRoundRepository.BeginTransaction();

                Guid VacancyRoundId = _VacancyRoundRepository.AddVacancyRound(objVacancyRound);

                _VacancyRoundRepository.CommitTransaction();

                return VacancyRoundId;

            }
            catch
            {
                _VacancyRoundRepository.RollbackTransaction();
                throw;
            }
        }

        public bool Delete(Guid VacRndId)
        {
            try
            {
                bool Result = false;
                _VacancyRoundRepository.BeginTransaction();
                Result = _VacancyRoundRepository.DeleteVacRound(VacRndId);
                if (Result)
                {
                    _VacancyRoundRepository.CommitTransaction();
                    return Result;
                }
                return Result;
            }
            catch
            {
                _VacancyRoundRepository.RollbackTransaction();
                throw;
            }

        }

        public bool Update(BEClient.VacancyRound objVacancyRound)
        {
            try
            {
                _VacancyRoundRepository.BeginTransaction();

                bool recordUpdated = _VacancyRoundRepository.Update(objVacancyRound);

                _VacancyRoundRepository.CommitTransaction();
                return recordUpdated;
            }
            catch
            {
                _VacancyRoundRepository.RollbackTransaction();
                throw;
            }
        }

        public List<BEClient.VacancyRound> GetAllRound(Guid VacancyId, Guid LanguageId, Guid ApplicationId, bool FillScore = true)
        {
            try
            {
                List<BEClient.VacancyRound> objVacRoundList = _VacancyRoundRepository.GetAllRound(VacancyId, LanguageId, FillScore, ApplicationId);
                foreach (BEClient.VacancyRound current in objVacRoundList)
                {
                    base.SetRoleRecordWise(current, current.DivisionId);
                }
                return objVacRoundList; 

                //return _VacancyRoundRepository.GetAllRound(VacancyId, LanguageId, FillScore, ApplicationId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.VacancyRound> GetAllRoundByApplicationId(Guid ApplicationId)
        {
            try
            {
                return _VacancyRoundRepository.GetAllRoundByApplicationId(ApplicationId);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.VacancyRound GetCountOfReviewersAndQuestion(Guid roundId)
        {
            try
            {
                BEClient.VacancyRound objVacancyRound = _VacancyRoundRepository.GetCountOfReviewersAndQuestion(roundId);

                return objVacancyRound;
            }
            catch
            {
                throw;
            }
        }
        public Boolean IsRndTypeSelfEval(Guid VacRndId)
        {
            try
            {
                int RndType = (int)BEClient.QuestionType.QT_SELF;
                return _VacancyRoundRepository.IsRndTypeSelfEval(VacRndId, RndType);
            }
            catch
            {
                throw;
            }
        }
        public BEClient.VacancyRound GetRoundConfigByRoundId(Guid roundId, Guid languageId)
        {
            try
            {
                BEClient.VacancyRound objVacancyRound = _VacancyRoundRepository.GetRoundConfigByRoundId(roundId, languageId);
                base.SetRoleRecordWise(objVacancyRound, objVacancyRound.DivisionId);
                return objVacancyRound;
            }
            catch
            {
                throw;
            }
        }

        public BEClient.VacancyRound GetVacRndByVacIdAndRndTypeId(Guid VacId, Guid RndTypeId)
        {
            try
            {
                return _VacancyRoundRepository.GetVacRndFromVacIdAndRndTypeId(VacId, RndTypeId);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.VacancyRound GetVacRndByAppIdAndRndTypeId(Guid ApplicationId, string RndTypeId)
        {
            try
            {
                return _VacancyRoundRepository.GetVacRndByAppIdAndRndTypeId(ApplicationId, RndTypeId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.VacancyRound> GetRndTypeByApplicationId(Guid ApplicationId, string LstRndTypeId, Guid LanguageId)
        {
            try
            {
                return _VacancyRoundRepository.GetRndTypeByApplicationId(ApplicationId, LstRndTypeId, LanguageId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.VacancyRound> GetAllRndTypeByApplicationId(Guid ApplicationId, string LstRndTypeId,Guid ScheduleIntId, Guid LanguageId)
        {
            try
            {
                return _VacancyRoundRepository.GetAllRndTypeByApplicationId(ApplicationId, LstRndTypeId, ScheduleIntId, LanguageId);
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateIsScreening(string RndtypeId, Guid VacRndId)
        {
            try
            {
                return _VacancyRoundRepository.UpdateIsScreening(RndtypeId, VacRndId);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.VacancyRound GetActiveRoundForApplication(Guid ApplicationId)
        {
            try
            {
                return _VacancyRoundRepository.GetActiveRoundForApplication(ApplicationId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.VacancyRound> GetAllVacRndByVacancyIdAndLanguage(Guid VacancyId ,Guid LanguageId)
        {
            try
            {
                return _VacancyRoundRepository.GetAllVacRndByVacancyIdAndLanguage(VacancyId,LanguageId);

            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.VacancyRound> GetVacRoundsFromCurrentVacRound(Guid VacRndId , Guid ApplicationId,Guid LanguageId)
        {
            try
            {
                return _VacancyRoundRepository.GetVacRoundsFromCurrentVacRound(VacRndId, ApplicationId, LanguageId);
            }
            catch
            {
                throw;
            }

        }
        public bool UpdateVacRndOrder(Guid VacancyId, Guid VacRndId, string OrderDir)
        {
            try
            {
                return _VacancyRoundRepository.UpdateVacRndOrder(VacancyId, VacRndId, OrderDir);
            }
            catch
            {
                throw;
            }
        }
        public int GetRoundCountByVancancyId(Guid VacancyId)
        {
            try
            {
                return _VacancyRoundRepository.GetRoundCountByVancancyId(VacancyId);
            }
            catch
            {
                throw;
            }
        }
        public Guid GetDivisionIdByVacRndId(Guid VacRndId)
        {
            try
            {
                return _VacancyRoundRepository.GetDivisionIdByVacRndId(VacRndId);
            }
            catch
            {
                throw;
            }
        }
     }
}
