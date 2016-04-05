using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAClient = ATS.DataAccess;
using BEClient = ATS.BusinessEntity;

namespace ATS.BusinessLogic
{    
    public class ApplicationHistoryAction : ActionBase
    {
        #region private data member
        private DAClient.ApplicationHostoryRepository _ApplicationHostoryRepository;
        private Guid _MyClientId { get; set; }
        #endregion


        #region Constructor

        public ApplicationHistoryAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _ApplicationHostoryRepository = new DAClient.ApplicationHostoryRepository(base.ConnectionString);
            _ApplicationHostoryRepository.CurrentUser = base.CurrentUser;
            _ApplicationHostoryRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public List<BEClient.ApplicationHistory> GetApplicationHistoryByApplicationId(Guid applicationId, Guid currentLanguageId)
        {
            try
            {
                return _ApplicationHostoryRepository.GetApplicationHistoryByApplicationId(applicationId, currentLanguageId);
            }
            catch
            {
                //throw;
                return null;
            }
        }

        public bool AddApplicationHistory(BEClient.ApplicationHistory objApplicationHistory)
        {
            try
            {
                return _ApplicationHostoryRepository.AddApplicationHistory(objApplicationHistory);
            }
            catch
            {
                //throw;
                return false;
            }
        }
    }
}
