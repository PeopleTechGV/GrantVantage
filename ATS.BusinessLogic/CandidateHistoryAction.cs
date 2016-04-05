using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAClient = ATS.DataAccess;
using BEClient = ATS.BusinessEntity;

namespace ATS.BusinessLogic
{
    public class CandidateHistoryAction : ActionBase
    {

        #region private data member
        private DAClient.CandidateHistoryRespository _CandidateHistoryRespository;
        private Guid _MyClientId { get; set; }
        #endregion


        #region Constructor

        public CandidateHistoryAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _CandidateHistoryRespository = new DAClient.CandidateHistoryRespository(base.ConnectionString);
            _CandidateHistoryRespository.CurrentUser = base.CurrentUser;
            _CandidateHistoryRespository.CurrentClient = base.CurrentClient;
        }
        #endregion


        public Guid InsertCandidateHistory(BEClient.CandidateHistory objCandidateHistory)
        {
            try
            {
                return _CandidateHistoryRespository.InsertCandidateHistory(objCandidateHistory);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.CandidateHistory> GetCandidateHistoryByApplicationId(Guid ApplicationId)
        {
            try
            {
                return _CandidateHistoryRespository.GetCandidateHistoryByApplicationId(ApplicationId);
            }
            catch
            {
                throw;
            }
        }

    }
}
