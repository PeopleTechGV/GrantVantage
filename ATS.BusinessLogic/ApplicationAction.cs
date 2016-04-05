using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using BECommon = ATS.BusinessEntity.Common;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic
{
    public class ApplicationAction : ActionBase
    {
        #region private data member
        private DAClient.ApplicationRepository _ApplicationRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor

        public ApplicationAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _ApplicationRepository = new DAClient.ApplicationRepository(base.ConnectionString);
            _ApplicationRepository.CurrentUser = base.CurrentUser;
            _ApplicationRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public Guid InsertApplication(BEClient.Application objApplication)
        {
            try
            {
                _ApplicationRepository.BeginTransaction();


                Guid Result = _ApplicationRepository.InsertApplication(objApplication);
                if (!Result.Equals(Guid.Empty))
                {
                    _ApplicationRepository.CommitTransaction();
                }
                else
                {
                    _ApplicationRepository.RollbackTransaction();
                }
                return Result;
            }
            catch (Exception)
            {
                _ApplicationRepository.RollbackTransaction();
                throw;
            }
        }

        public List<BEClient.Application> GetAllVacancyApplyByUser(Guid userId, Guid languageId)
        {
            try
            {
                return _ApplicationRepository.GetAllVacancyApplyByUser(userId, languageId, (int)BEClient.VacancyState.Active);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.Application GetApplicationByApplicationId(Guid ApplicationId)
        {
            try
            {
                return _ApplicationRepository.GetApplicationByApplicationId(ApplicationId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.Application> GetApplicationByAtsResume(Guid ATSResumeId)
        {
            try
            {
                return _ApplicationRepository.GetApplicationByATSResume(ATSResumeId);
            }
            catch
            {
                throw;
            }
        }

        public Int64 GetCountOfSubmittedApplicationByProfile(Guid ProfileId, Guid LanguageId)
        {
            try
            {
                return _ApplicationRepository.GetCountOfApplicationsByProfileAndStatus(ProfileId, LanguageId, ATS.BusinessEntity.Common.ApplicationStatusOptionsConstant.APP_STAT_SUBMIT);
            }
            catch
            {
                throw;
            }
        }

        public bool ReactiveApplication(Guid ApplicationId)
        {
            try
            {
                bool Result = false;
                Result = _ApplicationRepository.ReactiveApplication(ApplicationId);

                if (Result)
                {
                    Common.HistoryAction.CreateHistoryObject(ApplicationId, this.CurrentUser.UserId, 5, null, _MyClientId);
                }

                return Result;
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.Application> GetSubmittedApplicationByProfile(Guid ProfileId, Guid LanguageId)
        {
            try
            {
                return _ApplicationRepository.GetApplicationByProfileIdAndStatus(ProfileId, LanguageId, ATS.BusinessEntity.Common.ApplicationStatusOptionsConstant.APP_STAT_SUBMIT);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.Application> GetDraftApplicationByProfile(Guid ProfileId, Guid LanguageId)
        {
            try
            {
                return _ApplicationRepository.GetApplicationByProfileIdAndStatus(ProfileId, LanguageId, ATS.BusinessEntity.Common.ApplicationStatusOptionsConstant.APP_STAT_DRAFT);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.Application> GetAllApplicationByProfile(Guid ProfileId, Guid LanguageId)
        {
            try
            {
                return _ApplicationRepository.GetApplicationByProfileIdAndStatus(ProfileId, LanguageId, String.Empty);
            }
            catch
            {
                throw;
            }
        }

        public bool DeleteApplication(Guid recordid)
        {
            try
            {
                return _ApplicationRepository.DeleteApplication(recordid);
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateApplicationBasedStatus(Guid ApplicationId, Guid applicationBasedStatusId)
        {
            try
            {
                bool Result = false;
                Result = _ApplicationRepository.ChangeApplicationBasedStatus(ApplicationId, applicationBasedStatusId);
                if (Result)
                {
                    Common.HistoryAction.CreateHistoryObject(ApplicationId, this.CurrentUser.UserId, 4, null, _MyClientId);
                }
                return Result;
            }
            catch
            {
                throw;
            }
        }

        public bool ChangeApplicationStatus(Guid ApplicationId, string ApplicationStatus)
        {
            try
            {
                bool Result = false;
                Result = _ApplicationRepository.ChangeApplicationStatus(ApplicationId, ApplicationStatus);
                if (Result)
                {
                    if (ApplicationStatus != null && ApplicationStatus == BECommon.ApplicationStatusOptionsConstant.APP_STAT_SUBMIT)
                    {
                        Common.HistoryAction.CreateHistoryObject(ApplicationId, this.CurrentUser.UserId, 6, null, _MyClientId);
                    }
                }
                return Result;
            }
            catch
            {
                throw;
            }
        }

        public bool ChangeApplicationStatusWithoutLogin(Guid ApplicationId, string ApplicationStatus, Guid _UserId)
        {
            try
            {

                bool Result = false;

                if (_ApplicationRepository.CurrentUser == null)
                {
                    _ApplicationRepository.CurrentUser = new BEClient.User() { UserId = _UserId };

                }
                Result = _ApplicationRepository.ChangeApplicationStatus(ApplicationId, ApplicationStatus);

                if (Result)
                {
                    if (ApplicationStatus != null && ApplicationStatus == BECommon.ApplicationStatusOptionsConstant.APP_STAT_SUBMIT)
                    {
                        Common.HistoryAction.CreateHistoryObject(ApplicationId, this.CurrentUser.UserId, 6, null, _MyClientId);
                    }
                }
                return Result;
            }
            catch
            {
                throw;
            }
        }

        public Guid InsertApplicationWithoutLogin(BEClient.Application ObjApp, Guid newUserId)
        {
            try
            {
                if (_ApplicationRepository.CurrentUser == null)
                {
                    _ApplicationRepository.CurrentUser = new BEClient.User() { UserId = newUserId };
                }
                return _ApplicationRepository.InsertApplicationWithoutLogin(ObjApp);
            }
            catch
            {
                throw;
            }
        }

        public string GetApplyStatusByVacIdAndUserId(Guid VacancyId, Guid UserId)
        {
            return _ApplicationRepository.GetApplyStatusByVacIdAndUserId(VacancyId, UserId);
        }

        public string GetApplicationStatusByAppId(Guid ApplicationId)
        {
            return _ApplicationRepository.GetApplicationStatusByAppId(ApplicationId);
        }

        public string GetAppStatusByVacIdAndUserId(Guid VacancyId, Guid UserId)
        {
            return _ApplicationRepository.GetAppStatusByVacIdAndUserId(VacancyId, UserId);
        }

        public Guid GetApplicationIdByVacIdAndUserId(Guid VacancyId, Guid UserId, string url = null)
        {


            try
            {
                Guid ApplicationId = Guid.Empty;
                ApplicationId = _ApplicationRepository.GetApplicationIdByVacIdAndUserId(VacancyId, UserId);
                if (ApplicationId != Guid.Empty)
                {
                    Guid RecentlyViewId = Guid.Empty;
                    BEClient.RecentlyViewed objRecentlyview = new BEClient.RecentlyViewed();
                    objRecentlyview.Category = "Application";
                    objRecentlyview.URL = url;
                    objRecentlyview.ViewItemId = ApplicationId;
                    RecentlyViewedAction objRecentlyViewedAction = new RecentlyViewedAction(_MyClientId);
                    RecentlyViewId = objRecentlyViewedAction.InsertRecentlyViewed(objRecentlyview);

                }
                return ApplicationId;
            }
            catch
            {
                throw;
            }

        }

        public BEClient.Application GetResumeAndCLByVacIdAndUserId(Guid VacancyId, Guid UserId)
        {
            return _ApplicationRepository.GetResumeAndCLByVacIdAndUserId(VacancyId, UserId);
        }

        public bool UpdateAppResumeAndCLByVacIdAndUserId(BEClient.Application objApplication)
        {
            try
            {
                return _ApplicationRepository.UpdateAppResumeAndCLByVacIdAndUserId(objApplication);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.Application GetAppState(Guid ApplicationId)
        {
            return _ApplicationRepository.GetAppState(ApplicationId);
        }

        public BEClient.Application GetAppState(Guid VacancyId, Guid UserId)
        {
            return _ApplicationRepository.GetAppState(VacancyId, UserId);
        }

        public bool UpdateSaveForLater(Guid ApplicationId, bool Status)
        {
            try
            {
                return _ApplicationRepository.UpdateSaveForLater(ApplicationId, Status);
            }
            catch
            {
                throw;
            }
        }

        public Int32 GetAppAnswerTotalCount(Guid ApplicationId, string LstRndTypeId)
        {
            try
            {
                return _ApplicationRepository.GetAppAnswerTotalCount(ApplicationId, LstRndTypeId);
            }
            catch
            {
                throw;
            }
        }
        public List<BEClient.Application> GetAssignedCandidateByVacRoundId(Guid VacRoundId, Guid ReviewerId)
        {
            try
            {
                return _ApplicationRepository.GetAssignedCandidateByVacRoundId(VacRoundId, ReviewerId);
            }
            catch
            {
                throw;
            }
        }
        public bool UpdateATSResumeId(Guid _ApplicationId, Guid _ATSResumeId)
        {
            try
            {
                return _ApplicationRepository.UpdateATSResumeId(_ApplicationId, _ATSResumeId);
            }
            catch
            {
                throw;
            }
        }

        public Guid GetCandidateUserIdByApplicationId(Guid ApplicationId)
        {
            try
            {
                return _ApplicationRepository.GetCandidateUserIdByApplicationId(ApplicationId);
            }
            catch
            {
                throw;
            }
        }
    }
}
