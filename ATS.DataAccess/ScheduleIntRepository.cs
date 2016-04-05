using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;


namespace ATS.DataAccess
{
    public class ScheduleIntRepository : Repository<BEClient.ScheduleInt>
    {
        public ScheduleIntRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public Guid IsScheduled(BEClient.ScheduleInt ScheduleInt)
        {
            try
            {
                Guid reResult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("IsScheduled");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ScheduleInt.ApplicationId);
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, ScheduleInt.VacRndId);

                var result = base.FindScalarValue(command);
                if (result != null)
                {
                    Guid.TryParse(result.ToString(), out reResult);
                }
                return reResult;
            }
            catch
            {
                throw;
            }
        }

        public Guid CreateDummyInterview(BEClient.ScheduleInt ScheduleInt)
        {
            try
            {
                Guid reResult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("CreateDummyInterview");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ScheduleInt.ApplicationId);
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, ScheduleInt.VacRndId);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, false);
                Database.AddOutParameter(command, "ScheduleIntId", DbType.Guid, 16);
                var result = base.Add(command, "ScheduleIntId");
                if (result != null)
                {
                    Guid.TryParse(result.ToString(), out reResult);
                }
                return reResult;
            }
            catch
            {
                throw;
            }
        }

        public Guid AddSaveScheduleInt(BEClient.ScheduleInt ScheduleInt)
        {
            try
            {
                Guid reResult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertScheduleInt");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ScheduleInt.ApplicationId);
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, ScheduleInt.VacRndId);
                Database.AddInParameter(command, "@ScheduleDate", DbType.DateTime, ScheduleInt.ScheduleDate);
                Database.AddInParameter(command, "@StartTime", DbType.Time, ScheduleInt.StartTime);
                Database.AddInParameter(command, "@EndTime", DbType.Time, ScheduleInt.EndTime);
                Database.AddInParameter(command, "@Score", DbType.Double, ScheduleInt.Score);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, false);
                Database.AddOutParameter(command, "ScheduleIntId", DbType.Guid, 16);
                var result = base.Add(command, "ScheduleIntId");
                if (result != null)
                {
                    Guid.TryParse(result.ToString(), out reResult);
                }
                return reResult;
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateScheduleInt(BEClient.ScheduleInt ScheduleInt)
        {
            try
            {
                Guid reResult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("UpdateScheduleInt");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@ScheduleIntId", DbType.Guid, ScheduleInt.ScheduleIntId);
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, ScheduleInt.VacRndId);
                Database.AddInParameter(command, "@ScheduleDate", DbType.String, ScheduleInt.ScheduleDateStr);
                Database.AddInParameter(command, "@StartTime", DbType.Time, ScheduleInt.StartTime);
                Database.AddInParameter(command, "@EndTime", DbType.Time, ScheduleInt.EndTime);
                return base.Save(command);
            }
            catch
            {
                throw;
            }
        }

        public bool IsInterviewBegin(Guid ScheduleIntId)
        {
            try
            {
                bool reResult = false;
                DbCommand command = Database.GetStoredProcCommand("IsInterViewBegin");
                Database.AddInParameter(command, "@ScheduleIntId", DbType.Guid, ScheduleIntId);

                var result = base.FindScalarValue(command);
                if (result != null)
                {
                    Boolean.TryParse(result.ToString(), out reResult);
                }
                return reResult;
            }
            catch
            {
                throw;
            }
        }

        public bool DeleteScheduleInt(Guid ScheduleIntId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("DeleteScheduleInt");
                Database.AddInParameter(command, "@ScheduleIntId", DbType.Guid, ScheduleIntId);
                Database.AddInParameter(command, "UpdatedBy", DbType.Guid, CurrentUser.UserId);
                Database.AddInParameter(command, "UpdatedOn", DbType.DateTime, DateTime.UtcNow);
                var result = base.FindScalarValue(command);
                int val = 0;
                if (result != null)
                {

                    Int32.TryParse(result.ToString(), out val);
                }
                if (val == -100)
                    return false;
                else
                    return true;
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.ScheduleInt> GetVacRndDetailByApplicationId(Guid ApplicationId, String LstRndTypeId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetVacRndDetailByApplicationId");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                Database.AddInParameter(command, "@LstRndTypeId", DbType.String, LstRndTypeId);
                return base.Find(command, new GetVacRndDetailByAppIdFactory());
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.ScheduleInt> GetBeginInterviewsByAppIdAndUserId(Guid ApplicationId, Guid UserId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetBeginInterviewsByAppIdAndUserId");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);
                return base.Find(command, new GetBeginInterviewsFactory(), false);
            }
            catch
            {
                throw;
            }
        }

        internal class GetBeginInterviewsFactory : IDomainObjectFactory<BEClient.ScheduleInt>
        {
            public BEClient.ScheduleInt Construct(IDataReader reader)
            {
                BEClient.ScheduleInt ScheduleDetails = new BEClient.ScheduleInt();
                ScheduleDetails.ScheduleIntId = HelperMethods.GetGuid(reader, "ScheduleIntId");
                ScheduleDetails.VacRndId = HelperMethods.GetGuid(reader, "VacRndId");
                ScheduleDetails.Round = HelperMethods.GetString(reader, "Round");
                ScheduleDetails.InterviewStatus = HelperMethods.GetInt32(reader, "InterviewStatus");

                return ScheduleDetails;
            }
        }

        internal class GetVacRndDetailByAppIdFactory : IDomainObjectFactory<BEClient.ScheduleInt>
        {
            public BEClient.ScheduleInt Construct(IDataReader reader)
            {
                BEClient.ScheduleInt ScheduleDetails = new BEClient.ScheduleInt();
                ScheduleDetails.ScheduleDateStr = HelperMethods.GetString(reader, "ScheduleDate");
                ScheduleDetails.StartTime = HelperMethods.GetString(reader, "StartTime");
                ScheduleDetails.EndTime = HelperMethods.GetString(reader, "EndTime");
                ScheduleDetails.RoundWeight = HelperMethods.GetInt32(reader, "RoundWeight");
                ScheduleDetails.Score = HelperMethods.GetFloat(reader, "Score");
                ScheduleDetails.VacRndId = HelperMethods.GetGuid(reader, "VacRndId");
                ScheduleDetails.ScheduleIntId = HelperMethods.GetGuid(reader, "ScheduleIntId");
                ScheduleDetails.InterviewerList = HelperMethods.GetString(reader, "InterviewerList");
                ScheduleDetails.Round = HelperMethods.GetString(reader, "Round");
                ScheduleDetails.RndOrder = HelperMethods.GetInt32(reader, "RndOrder");
                return ScheduleDetails;
            }
        }



        public BEClient.ScheduleInt GetIntScheduleByScheduleIntId(Guid ScheduleIntId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetIntScheduleByScheduleIntId");
                Database.AddInParameter(command, "@ScheduleIntId", DbType.Guid, ScheduleIntId);
                return base.FindOne(command, new GetIntScheduleByScheduleIntIdFactory());
            }
            catch
            {
                throw;
            }
        }


        public Guid GetApplicationIdByScheduleIntId(Guid ScheduleIntId)
        {
            try
            {
                Guid Result = Guid.Empty;

                DbCommand command = Database.GetStoredProcCommand("GetApplicationIdByScheduleIntId");
                Database.AddInParameter(command, "@ScheduleIntId", DbType.Guid, ScheduleIntId);
                var rEsult = base.FindScalarValue(command);
                if (rEsult != null)
                {
                    Guid.TryParse(rEsult.ToString(), out Result);
                }
                return Result;
            }
            catch
            {
                throw;
            }
        }



        internal class GetIntScheduleByScheduleIntIdFactory : IDomainObjectFactory<BEClient.ScheduleInt>
        {
            public BEClient.ScheduleInt Construct(IDataReader reader)
            {
                BEClient.ScheduleInt ScheduleDetails = new BEClient.ScheduleInt();
                ScheduleDetails.InterviewerList = HelperMethods.GetString(reader, "InterviewerList");
                ScheduleDetails.ScheduleDateStr = HelperMethods.GetString(reader, "ScheduleDate");
                ScheduleDetails.StartTime = HelperMethods.GetString(reader, "StartTime");
                ScheduleDetails.EndTime = HelperMethods.GetString(reader, "EndTime");
                ScheduleDetails.RoundWeight = HelperMethods.GetInt32(reader, "RoundWeight");
                ScheduleDetails.Score = HelperMethods.GetFloat(reader, "Score");
                ScheduleDetails.VacRndId = HelperMethods.GetGuid(reader, "VacRndId");
                ScheduleDetails.ScheduleIntId = HelperMethods.GetGuid(reader, "ScheduleIntId");
                ScheduleDetails.Round = HelperMethods.GetString(reader, "Round");
                ScheduleDetails.RndOrder = HelperMethods.GetInt32(reader, "RndOrder");
                ScheduleDetails.IsBeginInterview = HelperMethods.GetBoolean(reader, "IsBeginInterview");

                return ScheduleDetails;
            }
        }
        internal class GetIntScheduleByAppIdAndRndTypeIdFactory : IDomainObjectFactory<BEClient.ScheduleInt>
        {
            public BEClient.ScheduleInt Construct(IDataReader reader)
            {
                BEClient.ScheduleInt ScheduleDetails = new BEClient.ScheduleInt();
                ScheduleDetails.VacRndId = HelperMethods.GetGuid(reader, "VacRndId");
                ScheduleDetails.ScheduleIntId = HelperMethods.GetGuid(reader, "ScheduleIntId");
                return ScheduleDetails;
            }
        }


        public Guid GetSchIdByAppAndVacQueAndRMId(Guid ApplicationId, Guid VacRndId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetSchIdByAppAndVacQueAndRMId");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                string ScheduleIntId = base.FindScalarValue(command);
                if (ScheduleIntId == "")
                {
                    return Guid.Empty;
                }
                else
                {
                    return new Guid(ScheduleIntId);
                }
            }
            catch
            {
                throw;
            }
        }
        public BEClient.ScheduleInt GetSchIdByAppAndRndTypeId(Guid ApplicationId, string RndTypeId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetSchIdByAppAndRndTypeId");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                Database.AddInParameter(command, "@RndTypeId", DbType.String, RndTypeId);
                return base.FindOne(command, new GetIntScheduleByScheduleIntIdFactory(), false);

            }

            catch
            {
                throw;
            }
        }


        public BEClient.ScheduleInt GetAppStatusByAppId(Guid ApplicationId, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAppStatusByAppId");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.FindOne(command, new GetAppStatusByAppIdFactory());

            }
            catch
            {
                throw;
            }
        }

        internal class GetAppStatusByAppIdFactory : IDomainObjectFactory<BEClient.ScheduleInt>
        {
            public BEClient.ScheduleInt Construct(IDataReader reader)
            {
                BEClient.ScheduleInt ScheduleDetails = new BEClient.ScheduleInt();
                ScheduleDetails.VacRndId = HelperMethods.GetGuid(reader, "VacRndId");
                ScheduleDetails.Round = HelperMethods.GetString(reader, "RoundName");
                return ScheduleDetails;
            }
        }
    }
}
