using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAClient = ATS.DataAccess;
using BEClient = ATS.BusinessEntity;

namespace ATS.BusinessLogic
{
    public class AppAnswerAction : ActionBase
    {
        #region private data member
        private DAClient.AppAnswerRepository _AppAnswerRepository;
        private Guid _MyClientId { get; set; }
        #endregion


        #region Constructor

        public AppAnswerAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _AppAnswerRepository = new DAClient.AppAnswerRepository(base.ConnectionString);
            _AppAnswerRepository.CurrentUser = base.CurrentUser;
            _AppAnswerRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public Guid InsertAppAnswer(BEClient.AppAnswer objAppAnswer)
        {
            try
            {
                _AppAnswerRepository.BeginTransaction();
                Guid Result = _AppAnswerRepository.InsertAppAnswer(objAppAnswer);
                if (!Result.Equals(Guid.Empty))
                {
                    _AppAnswerRepository.CommitTransaction();
                }
                else
                {
                    _AppAnswerRepository.RollbackTransaction();
                }
                return Result;
            }
            catch (Exception)
            {
                _AppAnswerRepository.RollbackTransaction();
                throw;
            }
        }


        public bool UpdateAppAns(BEClient.AppAnswer objAppAns)
        {
            try
            {
                _AppAnswerRepository.BeginTransaction();
                bool result = _AppAnswerRepository.UpdateAppAnswer(objAppAns);
                if (result)
                {
                    _AppAnswerRepository.CommitTransaction();

                }
                else
                {
                    _AppAnswerRepository.RollbackTransaction();
                }
                return result;
            }
            catch
            {
                _AppAnswerRepository.RollbackTransaction();
                throw;
            }
        }

        public bool IsRecordExists(Guid recordid)
        {
            try
            {
                DAClient.CommonRepository _common = new DAClient.CommonRepository(base.ConnectionString);
                return Convert.ToBoolean(_common.IsRecordExists("AppAnswer", "AppAnsId", recordid));
            }
            catch (Exception)
            {

                throw;
            }
        }
        public BEClient.AppAnswer GetAppAnswer(Guid VacQueId, Guid VacRmId, Guid ScheduleIntId)
        {
            try
            {
                return _AppAnswerRepository.GetAppAnswer(VacQueId, VacRmId, ScheduleIntId);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.AppAnswer GetAppAnsByVacQueAndVacRMAndAppId(Guid VacQueId, Guid VacRmId, Guid ApplicationId)
        {
            try
            {
                return _AppAnswerRepository.GetAppAnsByVacQueAndVacRMAndAppId(VacQueId, VacRmId, ApplicationId);
            }
            catch
            {
                throw;
            }
        }
        public Boolean RemoveOldAnswers(Guid ApplicationId, string RndTypeId)
        {
            try
            {
                return _AppAnswerRepository.RemoveOldAnswers(ApplicationId, RndTypeId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.AppAnswer> GetAnswerStatusByVacRndId(Guid ApplicationId, Guid VacRndId)
        {
            try
            {
                return _AppAnswerRepository.GetAnswerStatusByVacRndId(ApplicationId, VacRndId);
            }
            catch
            {
                throw;
            }
        }
    }
}
