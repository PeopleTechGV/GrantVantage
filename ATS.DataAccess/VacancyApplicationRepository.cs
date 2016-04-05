using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Data;
using System.Data.Common;
namespace ATS.DataAccess
{
    public class VacancyApplicationRepository : Repository<BEClient.VacancyApplicant>
    {
        public VacancyApplicationRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public List<BEClient.VacancyApplicant> GetApplicantByVacancyIdAndApplicationStatus(Guid VacancyId, String ApplicationStatus)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetApplicantDetailsByVacancyAndStatus");
                Database.AddInParameter(command, "@VacancyId", DbType.Guid, VacancyId);
                if (!String.IsNullOrEmpty(ApplicationStatus))
                {
                    Database.AddInParameter(command, "@ApplicationStatus", DbType.String, ApplicationStatus);
                }
                return base.Find(command, new GetAllApplicantDetailsByVacanyFactory());
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
                DbCommand command = Database.GetStoredProcCommand("GetApplicantsByVacIdAndAppStatus");
                Database.AddInParameter(command, "@VacancyId", DbType.Guid, VacancyId);
                Database.AddInParameter(command, "@AppStatus", DbType.String, AppStatus);
                return base.Find(command, new GetAllApplicantDetailsByVacanyFactory());
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
                DbCommand command = Database.GetStoredProcCommand("GetApplicationStausListAndCountForCandidate");
                Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, base.CurrentClient.CurrentLanguageId);

                return base.Find(command, new GetApplicationStausListAndCountForCandidateFactory(), false);
            }
            catch
            {
                throw;
            }
        }


        public List<BEClient.VacancyApplicant> GetApplicantDetailsByVacancySearch(String FilterSearchString, String SortBySearchString, int CurretPage, int RecordPerPage, string ApplicationStatus, out int TotalCount, string applicationStatus, string searchText = "")
        {
            try
            {


                if (string.IsNullOrEmpty(applicationStatus) || applicationStatus == "Active")
                {
                    FilterSearchString += " AND ApplicationStatus Not In ('Draft', 'Declined')";
                }
                else if (applicationStatus == "Assigned To Me")
                {
                    FilterSearchString += "AND ApplicationStatus <> 'Draft'";
                }
                else
                {
                    FilterSearchString += " AND ApplicationStatus='" + applicationStatus + "'";
                }

                TotalCount = 0;
                DbCommand command = Database.GetStoredProcCommand("GetApplicantDetailsByVacancySearch");
                Database.AddInParameter(command, "@GridFilterSearchString", DbType.String, FilterSearchString);
                Database.AddInParameter(command, "@GridSortBySearchString", DbType.String, SortBySearchString);
                Database.AddInParameter(command, "@PageNumber", DbType.Int32, CurretPage);
                Database.AddInParameter(command, "@PageSize", DbType.Int32, RecordPerPage);
                if (!string.IsNullOrEmpty(applicationStatus) && applicationStatus == "Assigned To Me")
                {
                    Database.AddInParameter(command, "@UserId", DbType.Guid, base.CurrentUser.UserId);
                }

                if (!String.IsNullOrEmpty(ApplicationStatus))
                {
                    Database.AddInParameter(command, "@ApplicationStatus", DbType.String, ApplicationStatus);
                }
                if (!String.IsNullOrEmpty(searchText))
                {
                    Database.AddInParameter(command, "@SearchText", DbType.String, searchText);
                }
                List<BEClient.VacancyApplicant> mylist = base.Find(command, new GetAllApplicantDetailsByVacanyFactory());
                if (mylist != null && mylist.Count > 0)
                {
                    TotalCount = mylist.FirstOrDefault().TotalCount;
                }
                return mylist;
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
                DbCommand command = Database.GetStoredProcCommand("GetApplicantStatusWithCount");
                Database.AddInParameter(command, "@VacancyId", DbType.Guid, VacancyId);
                Database.AddInParameter(command, "@UserId", DbType.Guid, base.CurrentUser.UserId);

                List<BEClient.VacancyApplicant> objApplicationStatusList = base.Find(command, new GetApplicationStatusWithCountFactory(), false);
                return objApplicationStatusList;
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
                DbCommand command = Database.GetStoredProcCommand("GetApplicationStatusCountAndList");
                Database.AddInParameter(command, "@VacancyId", DbType.Guid, VacancyId);
                Database.AddInParameter(command, "@UserId", DbType.Guid, base.CurrentUser.UserId);

                List<BEClient.VacancyApplicant> objApplicationStatusList = base.Find(command, new GetApplicationStatusCountWithListFactory(), false);
                return objApplicationStatusList;
            }
            catch
            {
                throw;
            }
        }


        internal class GetAllApplicantDetailsByVacanyFactory : IDomainObjectFactory<BEClient.VacancyApplicant>
        {
            public BEClient.VacancyApplicant Construct(IDataReader reader)
            {
                BEClient.VacancyApplicant objVacancyApplicant = new BEClient.VacancyApplicant();
                objVacancyApplicant.ApplicationId = HelperMethods.GetGuid(reader, "ApplicationId");
                objVacancyApplicant.ApplicantName = HelperMethods.GetString(reader, "ApplicantName");
                objVacancyApplicant.AppliedOn = HelperMethods.GetDateTime(reader, "AppliedOn");
                objVacancyApplicant.ProfileName = HelperMethods.GetString(reader, "ProfileName");
                objVacancyApplicant.CoverLetterName = HelperMethods.GetString(reader, "CoverLetterName");
                objVacancyApplicant.ResumeName = HelperMethods.GetString(reader, "ResumeName");
                objVacancyApplicant.ResumeId = HelperMethods.GetGuid(reader, "ResumeId");
                objVacancyApplicant.CoverLetterId = HelperMethods.GetGuid(reader, "CoverLetterId");
                objVacancyApplicant.NewFileName = HelperMethods.GetString(reader, "NewFileName");
                objVacancyApplicant.VacancyId = HelperMethods.GetGuid(reader, "VacancyId");
                objVacancyApplicant.JobTitle = HelperMethods.GetString(reader, "JobTitle");
                objVacancyApplicant.VacancyDivisionId = HelperMethods.GetGuid(reader, "DivisionId");
                objVacancyApplicant.ApplicantionStatus = HelperMethods.GetString(reader, "ApplicationStatus");
                objVacancyApplicant.TotalCount = HelperMethods.GetInt32(reader, "TotalCount");
                objVacancyApplicant.Score = HelperMethods.GetFloat(reader, "Score");
                objVacancyApplicant.NewCoverLetterName = HelperMethods.GetString(reader, "NewCoverLetterName");
                objVacancyApplicant.PublicCode = HelperMethods.GetInt64(reader, "PublicCode");
                objVacancyApplicant.ProfileImage = HelperMethods.GetString(reader, "ProfileImage");

                return objVacancyApplicant;
            }
        }


        internal class GetApplicationStatusWithCountFactory : IDomainObjectFactory<BEClient.VacancyApplicant>
        {
            public BEClient.VacancyApplicant Construct(IDataReader reader)
            {
                BEClient.VacancyApplicant objVacancyApplicant = new BEClient.VacancyApplicant();
                objVacancyApplicant.ApplicantionStatus = HelperMethods.GetString(reader, "ApplicationStatus");
                objVacancyApplicant.TotalCount = HelperMethods.GetInt32(reader, "TotalCount");
                return objVacancyApplicant;
            }
        }
        internal class GetApplicationStatusCountWithListFactory : IDomainObjectFactory<BEClient.VacancyApplicant>
        {
            public BEClient.VacancyApplicant Construct(IDataReader reader)
            {
                BEClient.VacancyApplicant objVacancyApplicant = new BEClient.VacancyApplicant();
                objVacancyApplicant.ApplicantionStatus = HelperMethods.GetString(reader, "ApplicationStatus");
                objVacancyApplicant.TotalCount = HelperMethods.GetInt32(reader, "TotalCount");
                objVacancyApplicant.VacApplicationLinkList = HelperMethods.GetString(reader, "VacApplicationLinkList");
                return objVacancyApplicant;
            }
        }
        internal class GetApplicationStausListAndCountForCandidateFactory : IDomainObjectFactory<BEClient.VacancyApplicant>
        {
            public BEClient.VacancyApplicant Construct(IDataReader reader)
            {
                BEClient.VacancyApplicant objVacancyApplicant = new BEClient.VacancyApplicant();
                objVacancyApplicant.ApplicantionStatus = HelperMethods.GetString(reader, "ShowToCandidate");
                objVacancyApplicant.TotalCount = HelperMethods.GetInt32(reader, "TotalCount");
                return objVacancyApplicant;
            }
        }
    }
}
