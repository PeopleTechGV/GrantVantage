using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAClient = ATS.DataAccess;
using BEClient = ATS.BusinessEntity;


namespace ATS.BusinessLogic
{
    public class NavCountAction : ActionBase
    {
        #region private data member
        private DAClient.NavCountRepository _NavCountRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor
        public NavCountAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _NavCountRepository = new DAClient.NavCountRepository(base.ConnectionString);
            _NavCountRepository.CurrentUser = base.CurrentUser;
            _NavCountRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public BEClient.NavCount CandidateNavigationCount(Guid UserId, Guid LanguageId)
        {
            try
            {
                return _NavCountRepository.CandidateNavigationCount(UserId, LanguageId);
            }
            catch
            {
                throw;
            }
        }
    }
}
