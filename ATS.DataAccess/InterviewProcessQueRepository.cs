using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
namespace ATS.DataAccess
{
    public class InterviewProcessQueRepository : Repository<BEClient.InterviewProcessQue>
    {
        public InterviewProcessQueRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public List<BEClient.InterviewProcessQue> GetQueDetailsBtyOrder(Guid VacRndId, Guid QueCatId, Guid AppId, Guid LanguageId, Guid? QueId = null)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetQueDetailsByOrder");
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                Database.AddInParameter(command, "@QueCatId", DbType.Guid, QueCatId);
                Database.AddInParameter(command, "@AppId", DbType.Guid, AppId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                if (QueId != null && (Guid)QueId != Guid.Empty)
                    Database.AddInParameter(command, "@QueId", DbType.Guid, QueId);

                return base.Find(command, new GetQueDetailsBtyOrderFactory(), false);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.InterviewProcessQue GetQueDetailsBtyOrder(Guid VacRndId, Guid QueCatId, int order, Guid LanguageId)
        {
            try
            {
                DbCommand command = null;
                if (order < 0)
                {
                    command = Database.GetStoredProcCommand("GetLastQueDetailsByQueCat");
                }
                else
                {
                    command = Database.GetStoredProcCommand("GetQueDetailsByOrder");
                    Database.AddInParameter(command, "@Row", DbType.Int32, order);

                }
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                Database.AddInParameter(command, "@QueCatId", DbType.Guid, QueCatId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.FindOne(command, new GetQueDetailsBtyOrderFactory(), false);
            }
            catch
            {
                throw;
            }
        }


        public List<BEClient.InterviewProcessQue> GetAllQueDetailsByOrder(Guid VacRndId, Guid LanguageId)
        {
            try
            {
                DbCommand command = null;
                command = Database.GetStoredProcCommand("GetAllQueDetails");
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.Find(command, new GetQueDetailsBtyOrderFactory(), false);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.InterviewProcessQue GetAnswerByAppIdAndQueId(Guid AppId, Guid QueId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAnswerByAppIdAndQueId");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, AppId);
                Database.AddInParameter(command, "@QueId", DbType.Guid, QueId);
                return base.FindOne(command, new GetAnswerByAppIdAndQueIdFactory(), false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal class GetAnswerByAppIdAndQueIdFactory : IDomainObjectFactory<BEClient.InterviewProcessQue>
        {
            public BEClient.InterviewProcessQue Construct(IDataReader reader)
            {
                BEClient.InterviewProcessQue InterViewProcess = new BEClient.InterviewProcessQue();
                InterViewProcess.ObjQue.QueCatLocalName = HelperMethods.GetString(reader, "QueCat");
                InterViewProcess.ObjQue.QueDataType = HelperMethods.GetInt32(reader, "QueDataType");
                return null;
            }
        }

        internal class GetQueDetailsBtyOrderFactory : IDomainObjectFactory<BEClient.InterviewProcessQue>
        {
            public BEClient.InterviewProcessQue Construct(IDataReader reader)
            {
                BEClient.InterviewProcessQue InterViewProcess = new BEClient.InterviewProcessQue();
                InterViewProcess.ObjQue = new BEClient.Question();
                InterViewProcess.ObjQue.QueId = HelperMethods.GetGuid(reader, "QueId");
                InterViewProcess.ObjQue.LocalName = HelperMethods.GetString(reader, "LocalName");
                InterViewProcess.TotalQue = HelperMethods.GetInt32(reader, "TotalCount");
                InterViewProcess.CurrentQue = HelperMethods.GetInt64(reader, "Row");
                InterViewProcess.VacRndId = HelperMethods.GetGuid(reader, "VacRndId");
                InterViewProcess.ObjQue.QueCatId = HelperMethods.GetGuid(reader, "QueCatId");
                InterViewProcess.ObjQue.QueType = HelperMethods.GetString(reader, "QueType");
                InterViewProcess.ObjQue.QueDataType = HelperMethods.GetInt32(reader, "QueDataType");
                InterViewProcess.ObjQue.Weight = HelperMethods.GetInt32(reader, "Weight");
                InterViewProcess.ObjQue.Score = HelperMethods.GetFloat(reader, "Score");
                InterViewProcess.VacRndWeight = HelperMethods.GetInt32(reader, "VacRndWeight");
                InterViewProcess.VacQueId = HelperMethods.GetGuid(reader, "VacQueId");
                InterViewProcess.VacQueWeight = HelperMethods.GetInt32(reader, "VacQueWeight");

                return InterViewProcess;
            }
        }
    }
}
