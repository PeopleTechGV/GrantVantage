using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic
{
    public class HireCandidateAction : ActionBase
    {
        #region private data member
        private DAClient.HireCandidatesRepository _HireCandidatesRepository;
        private Guid _MyClientId { get; set; }
        #endregion

          #region Constructor

        public HireCandidateAction(Guid ClientId, bool CreateSRPObject = false)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _HireCandidatesRepository = new DAClient.HireCandidatesRepository(base.ConnectionString);
            _HireCandidatesRepository.CurrentUser = base.CurrentUser;
            _HireCandidatesRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public BusinessEntity.HireCandidates GetHiredCandidate(Guid ApplicationId ,Guid LanguageId)
        {
            try
            {
                return _HireCandidatesRepository.GetHiredCandidates(ApplicationId, LanguageId);
            }
            catch
            {
                throw;
            }
        }

    }
}
