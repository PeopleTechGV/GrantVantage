using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAClient = ATS.DataAccess;
using BEClient = ATS.BusinessEntity;


namespace ATS.BusinessLogic
{
    public class GetScoreAction:ActionBase
    {
        #region private data member
        private DAClient.GetScoreRepository _GetScoreRepository;
        private Guid _MyClientId { get; set; }
        #endregion


            #region Constructor

        public GetScoreAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _GetScoreRepository = new DAClient.GetScoreRepository(base.ConnectionString);
            _GetScoreRepository.CurrentUser = base.CurrentUser;
            _GetScoreRepository.CurrentClient = base.CurrentClient;

        }
        #endregion

        public List<BEClient.GetScore> GetAllScore(Guid AppId,Guid VacRndId,Guid VacQueId)
        {
            try
            {
                return _GetScoreRepository.GetAllScore(AppId,VacRndId,VacQueId);
            }
            catch
            {
                throw;
            }
        }
        public BEClient.GetScore GetApplicationScore(Guid AppId)
        {
            try
            {
                return _GetScoreRepository.GetApplicationScore(AppId);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.GetScore GetTotalScoreByScheduleAndVacRnd(Guid ScheduleIntId,Guid VacRndId)
        {
            try
            {
                return _GetScoreRepository.GetTotalScoreByScheduleAndVacRnd(ScheduleIntId,VacRndId);
            }
            catch
            {
                throw;
            }
        }
        


       
    }
}
