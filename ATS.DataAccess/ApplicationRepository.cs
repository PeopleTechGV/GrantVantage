using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Data.Common;
using System.Data;

namespace ATS.DataAccess
{
    public class ApplicationRepository : Repository<BEClient.Application>
    {
        public ApplicationRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }


        public Guid InsertApplication(BEClient.Application objApplication)
        {
            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertApplication");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ATSResumeId", DbType.Guid, objApplication.ATSResumeId);

                Database.AddInParameter(command, "@ATSCoverLetterId", DbType.Guid, objApplication.ATSCoverLetterId);

                Database.AddInParameter(command, "@VacancyId", DbType.Guid, objApplication.VacancyId);

                Database.AddInParameter(command, "@ApplicationStatus", DbType.String, objApplication.ApplicationStatus);

                Database.AddInParameter(command, "@LanguageId", DbType.Guid, objApplication.LanguageId);

                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, objApplication.IsDelete);

                Database.AddInParameter(command, "@ApplicationStatusUpdate", DbType.DateTime, System.DateTime.UtcNow);

                Database.AddOutParameter(command, "ApplicationId", DbType.Guid, 32);

                var result = base.Add(command, "ApplicationId");

                if (result != null)
                {
                    Guid.TryParse(result.ToString(), out reREsult);
                }

                return reREsult;
            }

            catch
            {
                throw;
            }
        }


        public bool UpdateAppResumeAndCLByVacIdAndUserId(BEClient.Application objApplication)
        {
            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateAppResumeAndCLByVacIdAndUserId");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@VacancyId", DbType.Guid, objApplication.VacancyId);
                Database.AddInParameter(command, "@UserId", DbType.Guid, objApplication.CreatedBy);
                Database.AddInParameter(command, "@ATSResumeId", DbType.Guid, objApplication.ATSResumeId);
                Database.AddInParameter(command, "@ATSCoverLetterId", DbType.Guid, objApplication.ATSCoverLetterId);
                var result = base.Save(command);
                if (result != null)
                {
                    bool.TryParse(result.ToString(), out reREsult);
                }
                return reREsult;
            }

            catch
            {
                throw;
            }
        }

        public Guid InsertApplicationWithoutLogin(BEClient.Application objApplication)
        {
            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertApplication");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ATSResumeId", DbType.Guid, objApplication.ATSResumeId);

                Database.AddInParameter(command, "@ATSCoverLetterId", DbType.Guid, objApplication.ATSCoverLetterId);

                Database.AddInParameter(command, "@VacancyId", DbType.Guid, objApplication.VacancyId);

                Database.AddInParameter(command, "@ApplicationStatus", DbType.String, objApplication.ApplicationStatus);

                Database.AddInParameter(command, "@LanguageId", DbType.Guid, objApplication.LanguageId);

                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, objApplication.IsDelete);

                Database.AddInParameter(command, "@ApplicationStatusUpdate", DbType.DateTime, System.DateTime.UtcNow);

                Database.AddOutParameter(command, "ApplicationId", DbType.Guid, 32);

                var result = base.Add(command, "ApplicationId");

                if (result != null)
                {
                    Guid.TryParse(result.ToString(), out reREsult);
                }

                return reREsult;
            }

            catch
            {
                throw;
            }
        }

        public List<BEClient.Application> GetAllVacancyApplyByUser(Guid userId, Guid languageId, int vacancyStatus)
        {
            DbCommand command = Database.GetStoredProcCommand("GetAllVacancyApplyByUser");

            Database.AddInParameter(command, "@UserId", DbType.Guid, userId);

            Database.AddInParameter(command, "@LanguageId", DbType.Guid, languageId);

            Database.AddInParameter(command, "@VacancyState", DbType.Int32, vacancyStatus);

            return base.Find(command, new GetAllVacancyApplyByUserFactory());
        }

        public BEClient.Application GetApplicationByApplicationId(Guid ApplicationId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetApplicationByApplicationId");

                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);

                return base.FindOne(command, new GetApplicationById());
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
                Guid ReResult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("GetCandidateUserIdByApplicationId");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                var Result = base.FindScalarValue(command);
                if (Result != null)
                {
                    Guid.TryParse(Result.ToString(), out ReResult);
                }
                return ReResult;
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
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("ChangeApplicationStatus");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                Database.AddInParameter(command, "@ApplicationStatus", DbType.String, ApplicationStatus);
                Database.AddInParameter(command, "@ApplicationStatusUpdate", DbType.DateTime, System.DateTime.UtcNow);
                var result = base.Save(command);
                if (result != null)
                {
                    Boolean.TryParse(result.ToString(), out reREsult);
                }
                return reREsult;
            }
            catch
            {
                throw;
            }
        }

        public bool ChangeApplicationBasedStatus(Guid ApplicationId, Guid ApplicationBasedStatusId)
        {
            try
            {
                bool reREsult = false;

                DbCommand command = Database.GetStoredProcCommand("UpdateApplicationForStatus");

                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);

                Database.AddInParameter(command, "@ApplicationBasedStatusId", DbType.Guid, ApplicationBasedStatusId);


                var result = base.Save(command, false);

                if (result != null)
                {
                    Boolean.TryParse(result.ToString(), out reREsult);
                }

                return reREsult;
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
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("ReactiveApplication");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                var result = base.Save(command, false);
                if (result != null)
                {
                    bool.TryParse(result.ToString(), out reREsult);
                }
                return reREsult;
            }
            catch
            {
                throw;
            }
        }

        public Int64 GetCountOfApplicationsByProfileAndStatus(Guid ProfileId, Guid LanguageId, String Status)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetCountOfApplicationsByProfileAndStatus");

                Database.AddInParameter(command, "@ProfileId", DbType.Guid, ProfileId);

                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, false);

                if (!String.IsNullOrEmpty(Status))
                {
                    Database.AddInParameter(command, "@ApplicationStatus", DbType.String, Status);
                }
                return Convert.ToInt64(base.FindScalarValue(command));

            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.Application> GetApplicationByProfileIdAndStatus(Guid ProfileId, Guid LanguageId, String Status)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllVacanciesByProfileAndStatus");
                Database.AddInParameter(command, "@ProfileId", DbType.Guid, ProfileId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                if (!String.IsNullOrEmpty(Status))
                {
                    Database.AddInParameter(command, "@ApplicationStatus", DbType.String, Status);
                }
                return base.Find(command, new GetAllApplicationByProfileyId());
            }
            catch
            {
                throw;
            }
        }

        public BEClient.Application GetResumeAndCLByVacIdAndUserId(Guid VacancyId, Guid UserId)
        {
            DbCommand command = Database.GetStoredProcCommand("GetResumeAndCLByVacIdAndUserId");
            Database.AddInParameter(command, "@VacancyId", DbType.Guid, VacancyId);
            Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);
            return base.FindOne(command, new GetResumeAndCLByVacIdAndUserIdFactory());
        }

        public List<BEClient.Application> GetApplicationByATSResume(Guid ATSResumeId)
        {
            DbCommand command = Database.GetStoredProcCommand("GetApplicationByAtsResumeId");
            Database.AddInParameter(command, "@AtsResumeId", DbType.Guid, ATSResumeId);
            return base.Find(command, new GetAllVacancyApplyByUserFactory());
        }

        public string GetApplyStatusByVacIdAndUserId(Guid VacancyId, Guid UserId)
        {
            DbCommand command = Database.GetStoredProcCommand("GetApplyStatusByVacIdAndUserId");
            Database.AddInParameter(command, "@VacancyId", DbType.Guid, VacancyId);
            Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);
            return base.FindScalarValue(command);
        }

        public string GetApplicationStatusByAppId(Guid ApplicationId)
        {
            DbCommand command = Database.GetStoredProcCommand("GetApplicationStatusByAppId");
            Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
            return base.FindScalarValue(command);
        }

        public string GetAppStatusByVacIdAndUserId(Guid VacancyId, Guid UserId)
        {
            DbCommand command = Database.GetStoredProcCommand("GetApplicationStatusByUserIdVacId");
            Database.AddInParameter(command, "@VacancyId", DbType.Guid, VacancyId);
            Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);
            return base.FindScalarValue(command);
        }

        public Guid GetApplicationIdByVacIdAndUserId(Guid VacancyId, Guid UserId)
        {
            DbCommand command = Database.GetStoredProcCommand("GetApplicationIdByVacIdAndUserId");
            Database.AddInParameter(command, "@VacancyId", DbType.Guid, VacancyId);
            Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);

            var result = base.FindScalarValue(command);
            Guid ApplicationId = Guid.Empty;
            if (result != null)
            {
                Guid.TryParse(result.ToString(), out ApplicationId);
            }
            return ApplicationId;
        }

        public bool DeleteApplication(Guid recordid)
        {
            bool LocalTrasaction = false;
            try
            {
                if (base.Transaction == null)
                {
                    base.BeginTransaction();
                    LocalTrasaction = true;
                }
                bool reREsult = false;
                //parameterArray
                DbCommand command = Database.GetStoredProcCommand("DeleteApplication");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, recordid);
                var result = base.Remove(command);
                if (result != null)
                {
                    bool.TryParse(Convert.ToString(result), out reREsult);
                }
                if (LocalTrasaction)
                    base.CommitTransaction();

                return reREsult;
            }
            catch (Exception)
            {
                if (LocalTrasaction)
                    this.RollbackTransaction();
                throw;
            }
        }

        public BEClient.Application GetAppState(Guid ApplicationId)
        {
            DbCommand command = Database.GetStoredProcCommand("GetAppStateByAppId");
            Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
            return base.FindOne(command, new GetAppStatusFactory());
        }

        public BEClient.Application GetAppState(Guid VacancyId, Guid UserId)
        {
            DbCommand command = Database.GetStoredProcCommand("GetAppStateByVacIdAndUserId");
            Database.AddInParameter(command, "@VacancyId", DbType.Guid, VacancyId);
            Database.AddInParameter(command, "UserId", DbType.Guid, UserId);
            return base.FindOne(command, new GetAppStatusFactory());
        }

        public bool UpdateSaveForLater(Guid ApplicationId, bool Status)
        {
            try
            {
                bool ReResult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateSaveForLater");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                Database.AddInParameter(command, "@Status", DbType.Boolean, Status);
                var Result = base.Save(command);
                if (Result != null)
                {
                    Boolean.TryParse(Result.ToString(), out ReResult);
                }
                return ReResult;
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
                Int32 ReResult = 0;
                DbCommand command = Database.GetStoredProcCommand("GetAppAnswerTotalCount");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                Database.AddInParameter(command, "@LstRndTypeId", DbType.String, LstRndTypeId);
                var Result = base.FindScalarValue(command);
                if (Result != null)
                {
                    Int32.TryParse(Result.ToString(), out ReResult);
                }
                return ReResult;
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
                DbCommand command = Database.GetStoredProcCommand("GetAllAssignedCandidateByVacRoundId");
                Database.AddInParameter(command, "@VacRoundId", DbType.Guid, VacRoundId);
                Database.AddInParameter(command, "@ReviewerId", DbType.Guid, ReviewerId);

                return base.Find(command, new GetAssignedCandidate(), false);
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateATSResumeId(Guid ApplicationId, Guid ATSResumeId)
        {
            try
            {
                bool ReResult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateATSResumeIdForApplication");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                Database.AddInParameter(command, "@ATSResumeId", DbType.Guid, ATSResumeId);
                var Result = base.Save(command, false);
                if (Result != null)
                {
                    Boolean.TryParse(Result.ToString(), out ReResult);
                }
                return ReResult;
            }
            catch
            {
                throw;
            }
        }

        internal class GetAppStatusFactory : IDomainObjectFactory<BEClient.Application>
        {
            public BEClient.Application Construct(IDataReader reader)
            {
                BEClient.Application objApplication = new BEClient.Application();
                objApplication.ApplicationId = HelperMethods.GetGuid(reader, "ApplicationId");
                objApplication.VacancyId = HelperMethods.GetGuid(reader, "VacancyId");
                objApplication.ApplicationStatus = HelperMethods.GetString(reader, "ApplicationStatus");
                objApplication.SaveForLater = HelperMethods.GetBoolean(reader, "SaveForLater");
                return objApplication;
            }
        }

        internal class GetApplicationById : IDomainObjectFactory<BEClient.Application>
        {
            public BEClient.Application Construct(IDataReader reader)
            {
                BEClient.Application objApplication = new BEClient.Application();
                objApplication.ATSResumeId = HelperMethods.GetGuid(reader, "ATSResumeId");
                objApplication.ATSCoverLetterId = HelperMethods.GetGuid(reader, "ATSCoverLetterId");
                objApplication.UserId = HelperMethods.GetGuid(reader, "CreatedBy");
                objApplication.LanguageId = HelperMethods.GetGuid(reader, "LanguageId");
                objApplication.VacancyId = HelperMethods.GetGuid(reader, "VacancyId");
                return objApplication;
            }
        }

        internal class GetApplicationByAtsresumeFactory : IDomainObjectFactory<BEClient.Application>
        {
            public BEClient.Application Construct(IDataReader reader)
            {
                BEClient.Application objApplication = new BEClient.Application();
                objApplication.ATSResumeId = HelperMethods.GetGuid(reader, "ATSResumeId");
                return objApplication;
            }
        }

        internal class GetResumeAndCLByVacIdAndUserIdFactory : IDomainObjectFactory<BEClient.Application>
        {
            public BEClient.Application Construct(IDataReader reader)
            {
                BEClient.Application objApplication = new BEClient.Application();
                objApplication.ATSResumeId = HelperMethods.GetGuid(reader, "ATSResumeId");
                objApplication.ATSCoverLetterId = HelperMethods.GetGuid(reader, "ATSCoverLetterId");
                return objApplication;
            }
        }

        internal class GetAllVacancyApplyByUserFactory : IDomainObjectFactory<BEClient.Application>
        {
            public BEClient.Application Construct(IDataReader reader)
            {
                BEClient.Application objApplication = new BEClient.Application();
                objApplication.VacancyId = HelperMethods.GetGuid(reader, "VacancyId");
                return objApplication;
            }
        }

        internal class GetAllApplicationByProfileyId : IDomainObjectFactory<BEClient.Application>
        {
            public BEClient.Application Construct(IDataReader reader)
            {
                BEClient.Application objApplication = new BEClient.Application();
                objApplication.objCandidateApplications = new BEClient.CandidateApplications();
                objApplication.objCandidateApplications.objVacancy = new BEClient.Vacancy();
                objApplication.objCandidateApplications.objVacancy.JobTitle = HelperMethods.GetString(reader, "JobTitle");
                objApplication.objCandidateApplications.objVacancy.LocationText = HelperMethods.GetString(reader, "LocationText");
                objApplication.objCandidateApplications.objVacancy.Location = HelperMethods.GetGuid(reader, "Location");
                objApplication.ATSResumeId = HelperMethods.GetGuid(reader, "ATSResumeId");
                objApplication.ATSCoverLetterId = HelperMethods.GetGuid(reader, "ATSCoverLetterId");
                objApplication.LanguageId = HelperMethods.GetGuid(reader, "LanguageId");
                objApplication.VacancyId = HelperMethods.GetGuid(reader, "vacancyId");
                objApplication.ApplicationId = HelperMethods.GetGuid(reader, "ApplicationId");
                objApplication.ApplicationStatus = HelperMethods.GetString(reader, "ApplicationStatus");
                objApplication.AppState = HelperMethods.GetString(reader, "AppState");
                objApplication.Score = HelperMethods.GetFloat(reader, "Score");
                objApplication.CreatedOn = HelperMethods.GetDateTime(reader, "CreatedOn");
                objApplication.IsDeclined = HelperMethods.GetBoolean(reader, "Isdeclined");
                return objApplication;
            }
        }

        internal class GetAssignedCandidate : IDomainObjectFactory<BEClient.Application>
        {
            public BEClient.Application Construct(IDataReader reader)
            {
                BEClient.Application objApplication = new BEClient.Application();
                objApplication.ApplicationId = HelperMethods.GetGuid(reader, "ApplicationId");
                objApplication.VacancyId = HelperMethods.GetGuid(reader, "VacancyId");
                objApplication.CandidateName = HelperMethods.GetString(reader, "CandidateName");
                return objApplication;
            }
        }
    }
}
