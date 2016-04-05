using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
namespace ATS.DataAccess
{
    public class BeginInterviewRepository : Repository<BEClient.BeginInterView>
    {
        public BeginInterviewRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }


        public bool IsReviewer(Guid VacRndId, Guid UserId)
        {
            try
            {
                bool reResult = false;
                DbCommand command = Database.GetStoredProcCommand("IsReviewer");
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);
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

        public bool UpdateInterviewStatus(Guid VacRndId, Guid UserId, bool IsComplete, Guid ScheduleIntId,Int32? IntStatus)
        {
            try
            {
                bool reResult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdatInterviewStatus");
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);
                Database.AddInParameter(command, "@ScheduleIntId", DbType.Guid, ScheduleIntId);
                if (IntStatus != null)
                {
                    Database.AddInParameter(command, "@InterviewStatus", DbType.Int32, IntStatus);
                }
                    if (IsComplete)
                {
                    Database.AddInParameter(command, "@IntEnd", DbType.DateTime, System.DateTime.UtcNow);
                }
                Database.AddInParameter(command, "@IsComplete", DbType.Boolean, IsComplete);
                var result = base.Save(command);
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

        public bool UpdateStatusOfInterview(Guid VacRndId, Guid UserId, bool IsComplete, Guid ScheduleIntId,int InterviewStatus)
        {
            try
            {
                bool reResult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateStatusOfInterview");
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);
                Database.AddInParameter(command, "@ScheduleIntId", DbType.Guid, ScheduleIntId);
                Database.AddInParameter(command, "@InterviewStatus", DbType.Int32, InterviewStatus);
                if (IsComplete)
                {
                    Database.AddInParameter(command, "@IntEnd", DbType.DateTime, System.DateTime.UtcNow);
                }
                Database.AddInParameter(command, "@IsComplete", DbType.Boolean, IsComplete);
                var result = base.Save(command);
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



        public BEClient.BeginInterView IsBeginInterviewExists(Guid VacRndId, Guid UserId, Guid ScheduleIntId)
        {
            try
            {
                Guid reResult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("IsBeginInterview");
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);
                Database.AddInParameter(command, "@ScheduleIntId", DbType.Guid, ScheduleIntId);
                return base.FindOne(command, new IsBeginInterViewExistFactory());
            }
            catch
            {
                throw;
            }
        }
        public Guid InsertBeginInterview(BEClient.BeginInterView objBeginInterView)
        {
            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertBeginInterview");

                command.CommandTimeout = 100;



                Database.AddInParameter(command, "@VacRndId", DbType.Guid, objBeginInterView.VacRndId);

                Database.AddInParameter(command, "@UserId", DbType.Guid, objBeginInterView.UserId);

                Database.AddInParameter(command, "@IsComplete", DbType.Boolean, objBeginInterView.IsComplete);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, objBeginInterView.IsDelete);
                Database.AddInParameter(command, "@ScheduleIntId", DbType.Guid, objBeginInterView.ScheduleIntId);

                Database.AddInParameter(command, "@IntStart", DbType.DateTime, objBeginInterView.IntStart);

                if (objBeginInterView.IntEnd == DateTime.MinValue)
                {
                    Database.AddInParameter(command, "@IntEnd", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    Database.AddInParameter(command, "@IntEnd", DbType.DateTime, objBeginInterView.IntEnd);
                }
                Database.AddOutParameter(command, "BeginIntId", DbType.Guid, 16);

                var result = base.Add(command, "BeginIntId");
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

        public bool IsInterviewComplete(Guid VacRndId, Guid UserId, Guid ScheduleIntId)
        {
            try
            {
                bool reResult = false;
                DbCommand command = Database.GetStoredProcCommand("IsInterviewComplete");
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);
                Database.AddInParameter(command, "@ScheduleIntId", DbType.Guid, ScheduleIntId);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, false);
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

        internal class IsBeginInterViewExistFactory : IDomainObjectFactory<BEClient.BeginInterView>
        {
            public BEClient.BeginInterView Construct(IDataReader reader)
            {
                BEClient.BeginInterView objBeginInterView = new BEClient.BeginInterView();
                objBeginInterView.BeginIntId = HelperMethods.GetGuid(reader, "BeginIntId");
                objBeginInterView.IsComplete = HelperMethods.GetBoolean(reader, "IsComplete");
                objBeginInterView.UpdatedOn = HelperMethods.GetDateTime(reader, "UpdatedOn");
                objBeginInterView.UserText = HelperMethods.GetString(reader, "UserText");
                objBeginInterView.IntEnd = HelperMethods.GetDateTime(reader, "IntEnd");
                objBeginInterView.Score = HelperMethods.GetFloat(reader, "Score");
                return objBeginInterView;
            }
        }
    }
}
