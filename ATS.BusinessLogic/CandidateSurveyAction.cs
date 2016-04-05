using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic
{

    public class CandidateSurveyAction : ActionBase
    {
        #region private data member
        private DAClient.CandidateSurveyRepository _CandidateSurveyRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor

        public CandidateSurveyAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _CandidateSurveyRepository = new DAClient.CandidateSurveyRepository(base.ConnectionString);
            _CandidateSurveyRepository.CurrentUser = base.CurrentUser;
            _CandidateSurveyRepository.CurrentClient = base.CurrentClient;

        }
        #endregion

        public List<BEClient.CandidateSurvey> CheckCandidateSurvey(Guid ApplicationId)
        {
            try
            {
                return _CandidateSurveyRepository.CheckCandidateSurvey(ApplicationId);
            }
            catch
            {
                throw;
            }
        }
        public List<BEClient.CandidateSurvey> GetCandidateSurveyRounds(Guid ApplicationId)
        {
            try
            {
                return _CandidateSurveyRepository.GetCandidateSurveyRnds(ApplicationId);
            }
            catch
            {
                throw;
            }
        }

    }
}
