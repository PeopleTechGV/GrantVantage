using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic
{
    public class CandidateApplicationsAction : ActionBase
    {
        #region private data member
        private DAClient.CandidateApplicationsRepository _CandidateApplicationsRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor

        public CandidateApplicationsAction(Guid ClientId)
            : base(ClientId)
        {

            _MyClientId = ClientId;
            _CandidateApplicationsRepository = new DAClient.CandidateApplicationsRepository(base.ConnectionString);
            _CandidateApplicationsRepository.CurrentUser = base.CurrentUser;
            _CandidateApplicationsRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public List<BEClient.CandidateApplications> GetAllCandidateApplications(Guid UserId, Guid LanguageId, string LstRndTypeId, string status)
        {
            try
            {
                return _CandidateApplicationsRepository.GetCandidateApplicationByUserId(UserId, LanguageId, LstRndTypeId, status);
            }
            catch
            {
                throw;
            }
        }


        public List<BEClient.CandidateApplications> GetRequiredDocumentsCount(Guid UserId)
        {
            try
            {
                return _CandidateApplicationsRepository.GetRequiredDocumentsCount(UserId);
            }
            catch
            {
                throw;
            }
        }

        public Int32 GetCandidateSurveyQueCount(Guid ApplicationId)
        {
            try
            {
                return _CandidateApplicationsRepository.GetCandidateSurveyQueCount(ApplicationId);
            }
            catch
            {
                throw;
            }
                
        }

        public BEClient.CandidateApplications GetApplicationDetailByApplication(Guid ApplicationId, string url = null)
        {
            try
            {
                if (ApplicationId != Guid.Empty && url != null)
                {
                    Guid RecentlyViewId = Guid.Empty;
                    BEClient.RecentlyViewed objRecentlyview = new BEClient.RecentlyViewed();

                    objRecentlyview.Category = "Application";
                    objRecentlyview.URL = url;
                    objRecentlyview.ViewItemId = ApplicationId;
                    RecentlyViewedAction objRecentlyViewedAction = new RecentlyViewedAction(_MyClientId);
                    RecentlyViewId = objRecentlyViewedAction.InsertRecentlyViewed(objRecentlyview);
                }
                return _CandidateApplicationsRepository.GetCandidateApplicationByApplicationId(ApplicationId);
            }
            catch
            {
                throw;
            }
        }
    }
}
