using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic
{
    public class CandidateEmployeeAction : ActionBase
    {
        #region private data member
        private DAClient.CandidateEmployeeRepository _CandidateEmployeeRepository;
        private Guid _MyClientId { get; set; }
        #endregion
        #region Constructor

        public CandidateEmployeeAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _CandidateEmployeeRepository = new DAClient.CandidateEmployeeRepository(base.ConnectionString);
            _CandidateEmployeeRepository.CurrentUser = base.CurrentUser;
            _CandidateEmployeeRepository.CurrentClient = base.CurrentClient;

        }
        #endregion

        public List<BEClient.CandidateEmployee> GetReviewerUserNamesByVacRndId(Guid VacRndId, Guid ApplicationId)
        {
            try
            {
                return _CandidateEmployeeRepository.GetReviewerUserNamesByVacRndId(VacRndId, ApplicationId);
            }
            catch
            {
                throw;
            }

        }
    }
}
