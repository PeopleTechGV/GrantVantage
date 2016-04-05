using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;

namespace ATS.DataAccess
{
    public class AppAnswerRepository : Repository<BEClient.AppAnswer>
    {
        public AppAnswerRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public Guid InsertAppAnswer(BEClient.AppAnswer objAppAnswer)
        {
            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertAppAnswer");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@ScheduleIntId", DbType.Guid, objAppAnswer.ScheduleIntId);
                Database.AddInParameter(command, "@Answer", DbType.String, objAppAnswer.Answer);
                Database.AddInParameter(command, "@VacQueId", DbType.Guid, objAppAnswer.VacQueId);
                Database.AddInParameter(command, "@VacRMId", DbType.Guid, objAppAnswer.VacRMId);
                Database.AddInParameter(command, "@Comment", DbType.String, objAppAnswer.Comment);
                if (objAppAnswer.Value != null)
                {
                    Database.AddInParameter(command, "@Value", DbType.Int32, objAppAnswer.Value);
                }
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, objAppAnswer.IsDelete);
                Database.AddOutParameter(command, "AppAnsId", DbType.Guid, 16);
                var result = base.Add(command, "AppAnsId");
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

        public bool UpdateAppAnswer(BEClient.AppAnswer objAppAnswer)
        {
            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateAppAnswer");

                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@ScheduleIntId", DbType.Guid, objAppAnswer.ScheduleIntId);
                Database.AddInParameter(command, "@Answer", DbType.String, objAppAnswer.Answer);
                Database.AddInParameter(command, "@VacQueId", DbType.Guid, objAppAnswer.VacQueId);
                Database.AddInParameter(command, "@VacRMId", DbType.Guid, objAppAnswer.VacRMId);
                Database.AddInParameter(command, "@Comment", DbType.String, objAppAnswer.Comment);
                Database.AddInParameter(command, "@Value", DbType.Int32, objAppAnswer.Value);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, objAppAnswer.IsDelete);
                Database.AddInParameter(command, "@AppAnsId", DbType.Guid, objAppAnswer.AppAnsId);

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

        public BEClient.AppAnswer GetAppAnswer(Guid VacQueId, Guid VacRmId, Guid ScheduleIntId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAppAnsByVacQueAndVacRM");

                Database.AddInParameter(command, "@VacQueId", DbType.Guid, VacQueId);
                Database.AddInParameter(command, "@VacRMId", DbType.Guid, VacRmId);
                Database.AddInParameter(command, "@ScheduleIntId", DbType.Guid, ScheduleIntId);

                return base.FindOne(command, new GetAppAnswerFactory());
            }
            catch
            {
                throw;
            }
        }

        public Boolean RemoveOldAnswers(Guid ApplicationId, string RndTypeId)
        {
            try
            {
                bool ReResult = false;
                DbCommand command = Database.GetStoredProcCommand("RemoveOldAnswers");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                Database.AddInParameter(command, "@RndTypeId", DbType.String, RndTypeId);
                var result = base.Remove(command);
                if (result != null)
                {
                    ReResult = true;
                }
                return ReResult;
            }
            catch
            {
                throw;
            }
        }

        internal class GetAppAnswerFactory : IDomainObjectFactory<BEClient.AppAnswer>
        {
            public BEClient.AppAnswer Construct(IDataReader reader)
            {
                BEClient.AppAnswer objAppAnswer = new BEClient.AppAnswer();
                objAppAnswer.AppAnsId = HelperMethods.GetGuid(reader, "AppAnsId");
                objAppAnswer.ScheduleIntId = HelperMethods.GetGuid(reader, "ScheduleIntId");
                objAppAnswer.VacQueId = HelperMethods.GetGuid(reader, "VacQueId");
                objAppAnswer.VacRMId = HelperMethods.GetGuid(reader, "VacRMId");
                objAppAnswer.Answer = HelperMethods.GetString(reader, "Answer");
                objAppAnswer.Value = HelperMethods.GetInt32(reader, "Value");
                objAppAnswer.Comment = HelperMethods.GetString(reader, "Comment");
                return objAppAnswer;
            }
        }


        public BEClient.AppAnswer GetAppAnsByVacQueAndVacRMAndAppId(Guid VacQueId, Guid VacRmId, Guid ApplicationId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAppAnsByVacQueAndVacRMAndAppId");

                Database.AddInParameter(command, "@VacQueId", DbType.Guid, VacQueId);
                Database.AddInParameter(command, "@VacRMId", DbType.Guid, VacRmId);
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);

                return base.FindOne(command, new GetAppAnsByVacQueAndVacRMAndAppIdFactory());
            }
            catch
            {
                throw;
            }
        }

        internal class GetAppAnsByVacQueAndVacRMAndAppIdFactory : IDomainObjectFactory<BEClient.AppAnswer>
        {
            public BEClient.AppAnswer Construct(IDataReader reader)
            {
                BEClient.AppAnswer objAppAnswer = new BEClient.AppAnswer();
                objAppAnswer.AppAnsId = HelperMethods.GetGuid(reader, "AppAnsId");
                objAppAnswer.ScheduleIntId = HelperMethods.GetGuid(reader, "ScheduleIntId");
                objAppAnswer.VacQueId = HelperMethods.GetGuid(reader, "VacQueId");
                objAppAnswer.VacRMId = HelperMethods.GetGuid(reader, "VacRMId");
                objAppAnswer.Answer = HelperMethods.GetString(reader, "Answer");
                objAppAnswer.Value = HelperMethods.GetInt32(reader, "Value");
                objAppAnswer.Comment = HelperMethods.GetString(reader, "Comment");
                return objAppAnswer;
            }
        }


        public BEClient.AppAnswer GetAppAnswerByAppIdAndRMIdAndQueId(Guid VacQueId, Guid VacRmId, Guid ScheduleIntId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAppAnsByVacQueAndVacRM");
                Database.AddInParameter(command, "@VacQueId", DbType.Guid, VacQueId);
                Database.AddInParameter(command, "@VacRMId", DbType.Guid, VacRmId);
                Database.AddInParameter(command, "@ScheduleIntId", DbType.Guid, ScheduleIntId);
                return base.FindOne(command, new GetAppAnswerByAppAndRMAndQueFactory());
            }
            catch
            {
                throw;
            }
        }

        internal class GetAppAnswerByAppAndRMAndQueFactory : IDomainObjectFactory<BEClient.AppAnswer>
        {
            public BEClient.AppAnswer Construct(IDataReader reader)
            {
                BEClient.AppAnswer objAppAnswer = new BEClient.AppAnswer();
                objAppAnswer.AppAnsId = HelperMethods.GetGuid(reader, "AppAnsId");
                objAppAnswer.ScheduleIntId = HelperMethods.GetGuid(reader, "ScheduleIntId");
                objAppAnswer.VacQueId = HelperMethods.GetGuid(reader, "VacQueId");
                objAppAnswer.VacRMId = HelperMethods.GetGuid(reader, "VacRMId");
                objAppAnswer.Answer = HelperMethods.GetString(reader, "Answer");
                objAppAnswer.Value = HelperMethods.GetInt32(reader, "Value");
                objAppAnswer.Comment = HelperMethods.GetString(reader, "Comment");
                return objAppAnswer;
            }
        }

        public List<BEClient.AppAnswer> GetAnswerStatusByVacRndId(Guid ApplicationId, Guid VacRndId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAnswerStatusByVacRndId");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                return base.Find(command, new GetAnswerStatusFactory(), false);
            }
            catch
            {
                throw;
            }
        }

        internal class GetAnswerStatusFactory : IDomainObjectFactory<BEClient.AppAnswer>
        {
            public BEClient.AppAnswer Construct(IDataReader reader)
            {
                BEClient.AppAnswer objAppAnswer = new BEClient.AppAnswer();
                objAppAnswer.VacQueId = HelperMethods.GetGuid(reader, "AppAnsId");
                objAppAnswer.IsAnsPending = HelperMethods.GetBoolean(reader, "IsAnsPending");
                return objAppAnswer;
            }
        }
    }
}
