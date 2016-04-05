using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAClient = ATS.DataAccess;
using BEClient = ATS.BusinessEntity;
using BESrp = ATS.BusinessEntity.SRPEntity;

namespace ATS.BusinessLogic
{
    public class VacancyApplicantAction : ActionBase
    {

        #region private data member
        private DAClient.VacancyApplicationRepository _VacancyApplicationRepository;
        private Guid _MyClientId { get; set; }
        #endregion


        #region Constructor

        public VacancyApplicantAction(Guid ClientId, bool CreateSRPObject = false)
            : base(ClientId)
        {
            if (CreateSRPObject)
                base.CreateSRPObject(SRPCommon.CreateSRPEntityObjects.SRPCreateObj<BESrp.SRPVacancy>(ClientId));
            _MyClientId = ClientId;
            _VacancyApplicationRepository = new DAClient.VacancyApplicationRepository(base.ConnectionString);
            _VacancyApplicationRepository.CurrentUser = base.CurrentUser;
            _VacancyApplicationRepository.CurrentClient = base.CurrentClient;
        }
        #endregion


        public List<BEClient.VacancyApplicant> GetSubmittedApplicantByVacancyId(Guid VacancyId)
        {
            try
            {
                return _VacancyApplicationRepository.GetApplicantByVacancyIdAndApplicationStatus(VacancyId, BEClient.Common.ApplicationStatusOptionsConstant.APP_STAT_SUBMIT);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.VacancyApplicant> GetApplicantsByVacIdAndAppStatus(Guid VacancyId, String AppStatus)
        {
            try
            {
                return _VacancyApplicationRepository.GetApplicantsByVacIdAndAppStatus(VacancyId, AppStatus);
            }
            catch
            {
                throw;
            }
        }

        
        public List<BEClient.VacancyApplicant> GetApplicantDetailsByVacancySearch(String FilterSearchString, String SortBySearchString, int CurretPage, int RecordPerPage, out int TotalCount, string applicationStatus, string searchtext = "")
        {
            try
            {
                if (applicationStatus.ToLower() == "draft")
                    return _VacancyApplicationRepository.GetApplicantDetailsByVacancySearch(FilterSearchString, SortBySearchString, CurretPage, RecordPerPage, BEClient.Common.ApplicationStatusOptionsConstant.APP_STAT_DRAFT, out TotalCount, applicationStatus, searchtext);
                else
                    return _VacancyApplicationRepository.GetApplicantDetailsByVacancySearch(FilterSearchString, SortBySearchString, CurretPage, RecordPerPage, BEClient.Common.ApplicationStatusOptionsConstant.APP_STAT_SUBMIT, out TotalCount, applicationStatus, searchtext);

            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.VacancyApplicant> GetApplicationStatusWithCount(Guid VacancyId = new Guid())
        {
            try
            {
                return _VacancyApplicationRepository.GetApplicationStatusWithCount(VacancyId);
            }
            catch
            {
                throw;
            }
        }
        public List<BEClient.VacancyApplicant> GetApplicationStatusCountAndList(Guid VacancyId)
        {
            try
            {
                return _VacancyApplicationRepository.GetApplicationStatusCountAndList(VacancyId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.VacancyApplicant> GetApplicationStausListAndCountForCandidate(Guid UserId)
        {
            try
            {
                return _VacancyApplicationRepository.GetApplicationStausListAndCountForCandidate(UserId);
            }
            catch
            {
                throw;
            }
        }

    
    }
}
