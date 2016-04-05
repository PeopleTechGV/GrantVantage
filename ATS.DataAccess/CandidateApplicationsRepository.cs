using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Data.Common;
using System.Data;

namespace ATS.DataAccess
{
    public class CandidateApplicationsRepository : Repository<BEClient.CandidateApplications>
    {
        public CandidateApplicationsRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }


        public List<BEClient.CandidateApplications> GetCandidateApplicationByUserId(Guid UserId, Guid LanguageId, string LstRndTypeId, string Status)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllCandidateApplicationsByUser");
                Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                Database.AddInParameter(command, "LstRndTypeId", DbType.String, LstRndTypeId);
                Database.AddInParameter(command, "@Status", DbType.String, Status);

                return base.Find(command, new GetCandidateApplicationsByUserIdFactory());
            }
            catch
            {
                throw;
            }
        }
        public BEClient.CandidateApplications GetCandidateApplicationByApplicationId(Guid ApplicationId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetApplicationDetailByApplicationId");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                return base.FindOne(command, new GetCandidateApplicationsByApplicationIdFactory());
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
                DbCommand command = Database.GetStoredProcCommand("GetRequiredDocumentsCount");
                Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);
                return base.Find(command, new GetRequiredDocumentsCountFactory(), false);
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
                Int32 reResult = 0;
                DbCommand command = Database.GetStoredProcCommand("GetCandidateSurveyQueCount");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                var result = base.FindScalarValue(command);
                if (result != null)
                {
                    Int32.TryParse(result.ToString(), out reResult);
                }
                return reResult;
            }
            catch
            {
                throw;
            }
        }



        internal class GetRequiredDocumentsCountFactory : IDomainObjectFactory<BEClient.CandidateApplications>
        {
            public BEClient.CandidateApplications Construct(IDataReader reader)
            {
                BEClient.CandidateApplications candidateapplications = new BEClient.CandidateApplications();
                candidateapplications.objVacancy = new BEClient.Vacancy();
                candidateapplications.objVacancy.VacancyId = HelperMethods.GetGuid(reader, "VacancyId");
                candidateapplications.ReqDocCount = HelperMethods.GetInt32(reader, "ReqDocCount");
                return candidateapplications;
            }
        }

        internal class GetCandidateApplicationsByUserIdFactory : IDomainObjectFactory<BEClient.CandidateApplications>
        {
            public BEClient.CandidateApplications Construct(IDataReader reader)
            {
                BEClient.CandidateApplications candidateapplications = new BEClient.CandidateApplications();
                candidateapplications.objAtsResume = new BEClient.ATSResume();
                candidateapplications.objVacancy = new BEClient.Vacancy();

                candidateapplications.objVacancy.VacancyId = HelperMethods.GetGuid(reader, "VacancyId");
                candidateapplications.ApplicationId = HelperMethods.GetGuid(reader, "ApplicationId");


                candidateapplications.objVacancy.JobTitle = HelperMethods.GetString(reader, "JobTitle");
                candidateapplications.objVacancy.LocationText = HelperMethods.GetString(reader, "LocationText");
                candidateapplications.objVacancy.VacancyStatusText = HelperMethods.GetString(reader, "VacancyStatusText");
                candidateapplications.objAtsResume.ResumeName = HelperMethods.GetString(reader, "ResumeName");
                candidateapplications.objAtsResume.CoverLetterName = HelperMethods.GetString(reader, "CoverLetterName");
                candidateapplications.objAtsResume.ProfileName = HelperMethods.GetString(reader, "ProfileName");
                candidateapplications.objAtsResume.ProfileId = HelperMethods.GetGuid(reader, "ProfileId");
                candidateapplications.objAtsResume.ATSResumeId = HelperMethods.GetGuid(reader, "ATSResumeId");
                candidateapplications.objAtsResume.CoverLetterId = HelperMethods.GetGuid(reader, "CoverLetterId");
                candidateapplications.objAtsResume.Path = HelperMethods.GetString(reader, "Path");
                candidateapplications.ApplicationStatus = HelperMethods.GetString(reader, "ApplicationStatus");
                candidateapplications.AppliedOn = HelperMethods.GetDateTime(reader, "ApplicationStatusUpdate");
                candidateapplications.ShowToCandidate = HelperMethods.GetString(reader, "ShowToCandidate");
                candidateapplications.TotalOffers = HelperMethods.GetInt32(reader, "TotalOffers");
                candidateapplications.TotalInterviews = HelperMethods.GetInt32(reader, "TotalInterviews");
                candidateapplications.AppStatusAttrNo = HelperMethods.GetInt32(reader, "AppStatusAttrNo");

                candidateapplications.TotalAppQues = HelperMethods.GetInt32(reader, "TotalAppQues");
                return candidateapplications;
            }
        }


        internal class GetCandidateApplicationsByApplicationIdFactory : IDomainObjectFactory<BEClient.CandidateApplications>
        {
            public BEClient.CandidateApplications Construct(IDataReader reader)
            {
                BEClient.CandidateApplications candidateapplications = new BEClient.CandidateApplications();
                candidateapplications.objAtsResume = new BEClient.ATSResume();
                candidateapplications.objVacancy = new BEClient.Vacancy();

                candidateapplications.objVacancy.VacancyId = HelperMethods.GetGuid(reader, "VacancyId");
                candidateapplications.ApplicationId = HelperMethods.GetGuid(reader, "ApplicationId");


                candidateapplications.objVacancy.JobTitle = HelperMethods.GetString(reader, "JobTitle");
                candidateapplications.objVacancy.StartDate = HelperMethods.GetDateTime(reader, "StartDate");
                candidateapplications.objVacancy.LocationText = HelperMethods.GetString(reader, "LocationText");
                candidateapplications.objVacancy.EmploymentTypeText = HelperMethods.GetString(reader, "EmploymentTypeText");
                candidateapplications.objVacancy.JobTypeText = HelperMethods.GetString(reader, "JobTypeText");
                candidateapplications.objVacancy.PositionTypeName = HelperMethods.GetString(reader, "PositionTypeName");
                candidateapplications.objVacancy.PositionID = HelperMethods.GetDecimal(reader, "PositionID");
                candidateapplications.objVacancy.ActivityCode = HelperMethods.GetString(reader, "ActivityCode");
                candidateapplications.objVacancy.AnnouncementType = HelperMethods.GetString(reader, "AnnouncementType");
                candidateapplications.objVacancy.FundingOpptyNumber = HelperMethods.GetString(reader, "FundingOpptyNumber");
                candidateapplications.objVacancy.AnnouncementDate = HelperMethods.GetDateTime(reader, "AnnouncementDate");
                candidateapplications.objVacancy.OpenDate = HelperMethods.GetDateTime(reader, "OpenDate");
                candidateapplications.objVacancy.ApplicationDueDate = HelperMethods.GetDateTime(reader, "ApplicationDueDate");
                candidateapplications.objVacancy.ExpirationDate = HelperMethods.GetDateTime(reader, "ExpirationDate");

                candidateapplications.objAtsResume.ResumeName = HelperMethods.GetString(reader, "ResumeName");
                candidateapplications.objAtsResume.CoverLetterName = HelperMethods.GetString(reader, "CoverLetterName");
                candidateapplications.objAtsResume.ProfileName = HelperMethods.GetString(reader, "ProfileName");
                candidateapplications.objAtsResume.ProfileId = HelperMethods.GetGuid(reader, "ProfileId");
                candidateapplications.objAtsResume.ATSResumeId = HelperMethods.GetGuid(reader, "ATSResumeId");
                candidateapplications.objAtsResume.CoverLetterId = HelperMethods.GetGuid(reader, "CoverLetterId");
                candidateapplications.objAtsResume.Path = HelperMethods.GetString(reader, "Path");
                candidateapplications.ApplicationStatus = HelperMethods.GetString(reader, "ApplicationStatus");
                candidateapplications.AppliedOn = HelperMethods.GetDateTime(reader, "ApplicationStatusUpdate");
                candidateapplications.ShowToCandidate = HelperMethods.GetString(reader, "ShowToCandidate");



                return candidateapplications;
            }
        }
    }
}
