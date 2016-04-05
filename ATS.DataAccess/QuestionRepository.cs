using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;

namespace ATS.DataAccess
{
    public class QuestionRepository : Repository<BEClient.Question>
    {
        public QuestionRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }
        public Guid AddQue(BEClient.Question Que, string fieldValue)
        {
            bool LocalTrasaction = false;
            try
            {
                if (base.Transaction == null)
                {
                    base.BeginTransaction();
                    LocalTrasaction = true;
                }
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertQue");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@DefaultName", DbType.String, Que.DefaultName);
                Database.AddInParameter(command, "@Weight", DbType.Int32, Que.Weight);
                Database.AddInParameter(command, "@Order", DbType.Int32, Que.Order);
                Database.AddInParameter(command, "@QueType", DbType.String, Que.QueType);
                Database.AddInParameter(command, "@QueDataType", DbType.Int32, Que.QueDataType);
                //Database.AddInParameter(command, "@DivisionId", DbType.Guid, Que.DivisionId);
                Database.AddInParameter(command, "@IsActive", DbType.Boolean, Que.IsActive);
                Database.AddInParameter(command, "@FieldValue", DbType.String, fieldValue);
                Database.AddInParameter(command, "@QueCatId", DbType.Guid, Que.QueCatId);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, Que.IsDelete);
                Database.AddOutParameter(command, "QueId", DbType.Guid, 16);
                Database.AddOutParameter(command, "@IsError", DbType.Int32, 4);
                //CR-9
                Database.AddInParameter(command, "@IntegrationKey", DbType.String, Que.IntegrationKey);
                string[] outParams = new string[] { "QueId", "@IsError" };

                var result = base.Add(command, outParams);
                String errorCode = result[outParams[1]].ToString();

                if (result != null)
                {
                    switch (errorCode)
                    {
                        case "101":
                            throw new Exception("Que Cat already exists.");
                    }
                    Guid.TryParse(result[outParams[0]].ToString(), out reREsult);

                }
                if (LocalTrasaction)
                    base.CommitTransaction();

                return reREsult;
            }

            catch
            {
                if (LocalTrasaction)
                    this.RollbackTransaction();
                throw;
            }
        }

        public bool UpdateQueOrder(Guid QueCatId, Guid QueId,string OrderDir)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("UpdateQueOrder");

                
                
                Database.AddInParameter(command, "@QueCatId", DbType.Guid, QueCatId);
                Database.AddInParameter(command, "@QueId", DbType.Guid, QueId);
                Database.AddInParameter(command, "@Orderdir", DbType.String, OrderDir);
                
                return base.Save(command, false);

            }
            catch
            {
                throw;
            }
        }


        public bool DeleteQue(Guid QueId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("DeleteQue");
                Database.AddInParameter(command, "@QueId", DbType.Guid, QueId);
                return base.Save(command);

            }
            catch
            {
                throw;
            }
        }


        public bool UpdateQueCat(BEClient.Question Que, string fieldValue)
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
                DbCommand command = Database.GetStoredProcCommand("UpdateQue");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@DefaultName", DbType.String, Que.DefaultName);

                Database.AddInParameter(command, "@Weight", DbType.Int32, Que.Weight);
                Database.AddInParameter(command, "@DivisionId", DbType.Guid, Que.DivisionId);
                Database.AddInParameter(command, "@IsActive", DbType.Boolean, Que.IsActive);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, Que.IsDelete);
                Database.AddInParameter(command, "@QueCatId", DbType.Guid, Que.QueCatId);
                Database.AddInParameter(command, "@QueType", DbType.String, Que.QueType);
                Database.AddInParameter(command, "@QueDataType", DbType.Int32, Que.QueDataType);
                Database.AddInParameter(command, "@FieldValue", DbType.String, fieldValue);
                Database.AddInParameter(command, "@QueId", DbType.Guid, Que.QueId);
                Database.AddOutParameter(command, "IsError", DbType.Int32, 4);
                //CR-9
                Database.AddInParameter(command, "@IntegrationKey", DbType.String, Que.IntegrationKey);
                var result = base.SaveWithoutDuplication(command, "IsError");

                if (result != null)
                {

                    switch (result.ToString())
                    {
                        case "101":
                            throw new Exception("Que Cat already exists.");
                    }
                    if (result.ToString() == "0")
                    {
                        reREsult = true;
                    }
                }
                if (LocalTrasaction)
                    base.CommitTransaction();

                return reREsult;
            }

            catch
            {
                if (LocalTrasaction)
                    this.RollbackTransaction();
                throw;
            }
        }


        public List<BEClient.Question> GetAllQue(Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllQue");
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.Find(command, new GetAllQueFactory());
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.Question> GetAllQueByQueCatId(Guid QueCatId, Guid LanguageId, String ListOfDivision)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllQueByQueCatId");
                Database.AddInParameter(command, "@QueCatId", DbType.Guid, QueCatId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                if (!string.IsNullOrEmpty(ListOfDivision))
                {
                    Database.AddInParameter(command, "@DivisionList", DbType.String, ListOfDivision);
                }

                return base.Find(command, new GetAllQueByQueCatFactory());
            }
            catch
            {
                throw;
            }
        }


        public BEClient.Question GetRecordByRecordId(Guid recordId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetQueById");
                Database.AddInParameter(command, "@QueId", DbType.Guid, recordId);
                return base.FindOne(command, new GetRecordByRecordIdFactory());
            }
            catch
            {
                throw;
            }
        }

        public bool InlineUpdateQuestion(BEClient.Question objQue, Guid LanguageId)
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
                DbCommand command = Database.GetStoredProcCommand("InlineUpdateQuestion");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@LocalName", DbType.String, objQue.LocalName);
                Database.AddInParameter(command, "@Weight", DbType.Int32, objQue.Weight);
                Database.AddInParameter(command, "@QueDataType", DbType.Int32, objQue.QueDataType);
                Database.AddInParameter(command, "@QueId", DbType.Guid, objQue.QueId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                var result = base.Save(command);

                if (result != null)
                {
                    bool.TryParse(result.ToString(), out reREsult);
                }
                if (LocalTrasaction)
                    base.CommitTransaction();

                return reREsult;
            }

            catch
            {
                if (LocalTrasaction)
                    this.RollbackTransaction();
                throw;
            }

        }

        public List<BEClient.Question> GetQueByVacRndAndVacQueCat(Guid VacRndId, Guid VacQueCatId, Guid LanguageId, String DivisionList, Guid? QueId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetQueByVacRndAndVacQueCat");
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                Database.AddInParameter(command, "@VacQueCatId", DbType.Guid, VacQueCatId);
                if (!String.IsNullOrEmpty(DivisionList))
                { Database.AddInParameter(command, "@DivisionList", DbType.String, DivisionList); }
                if (QueId != null)
                { Database.AddInParameter(command, "@QueId", DbType.Guid, QueId); }
                return base.Find(command, new GetAllQueByVacancyFactory(), false);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.Question> GetQueByTVacRndAndTVacQueCat(Guid TVacRndId, Guid TVacQueCatId, Guid LanguageId, String DivisionList, Guid? QueId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetQueByTVacRndAndTVacQueCat");
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                Database.AddInParameter(command, "@TVacRndId", DbType.Guid, TVacRndId);
                Database.AddInParameter(command, "@TVacQueCatId", DbType.Guid, TVacQueCatId);
                if (!String.IsNullOrEmpty(DivisionList))
                { Database.AddInParameter(command, "@DivisionList", DbType.String, DivisionList); }
                if (QueId!=null)
                { Database.AddInParameter(command, "@QueId", DbType.Guid , QueId); }
                return base.Find(command, new GetAllQueByVacancyFactory(),false);
            }
            catch
            {
                throw;
            }
        }


        public int GetNewQueOrder(Guid QueCatId)
        {
            try
            {
                int reResult = 0;
                DbCommand command = Database.GetStoredProcCommand("GetNewQueOrder");
                Database.AddInParameter(command, "@QueCatId", DbType.Guid, QueCatId);
                
                var result = base.FindScalarValue(command);
                if (result != null)
                {
                    int.TryParse(result.ToString(), out reResult);
                }
                return reResult;
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.Question> GetAllQueByVacRnd(Guid VacRndId,Guid? QueId)
        {
            try
            {

                DbCommand command = Database.GetStoredProcCommand("GetAllQueByVacancy");

                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                if (QueId == null)
                { Database.AddInParameter(command, "@QueId", DbType.Guid, DBNull.Value); }
                else
                {
                    Database.AddInParameter(command, "@QueId", DbType.Guid, (Guid)QueId);
                }
                
                return base.Find(command, new GetAllQueByVacancyFactory());

            }
            catch
            {
                throw;
            }
        }

        internal class GetAllQueFactory : IDomainObjectFactory<BEClient.Question>
        {
            public BEClient.Question Construct(IDataReader reader)
            {
                BEClient.Question que = new BEClient.Question();
                que.QueId = HelperMethods.GetGuid(reader, "QueId");
                que.DivisionId = HelperMethods.GetGuid(reader, "DivisionId");
                que.QueCatId = HelperMethods.GetGuid(reader, "QueCatId");
                que.DefaultName = HelperMethods.GetString(reader, "DefaultName");
                que.Weight = HelperMethods.GetInt32(reader, "Weight");
                que.IsActive = HelperMethods.GetBoolean(reader, "IsActive");
                que.IsDelete = HelperMethods.GetBoolean(reader, "IsDelete");
                que.LocalName = HelperMethods.GetString(reader, "LocalName");
                que.QueType = HelperMethods.GetString(reader, "QueType");
                que.QueDataType = HelperMethods.GetInt32(reader, "QueDataType");
                que.QueCatLocalName = HelperMethods.GetString(reader, "QueCatLocalName");
                //CR-9
                que.IntegrationKey = HelperMethods.GetString(reader, "IntegrationKey");
                return que;
            }
        }

        internal class GetRecordByRecordIdFactory : IDomainObjectFactory<BEClient.Question>
        {
            public BEClient.Question Construct(IDataReader reader)
            {
                BEClient.Question que = new BEClient.Question();
                que.DivisionId = HelperMethods.GetGuid(reader, "DivisionId");
                que.QueId = HelperMethods.GetGuid(reader, "QueId");
                que.QueCatId = HelperMethods.GetGuid(reader, "QueCatId");
                que.DefaultName = HelperMethods.GetString(reader, "DefaultName");
                que.Weight = HelperMethods.GetInt32(reader, "Weight");
                que.IsActive = HelperMethods.GetBoolean(reader, "IsActive");
                que.IsDelete = HelperMethods.GetBoolean(reader, "IsDelete");
                que.LocalName = HelperMethods.GetString(reader, "LocalName");
                que.QueType = HelperMethods.GetString(reader, "QueType");
                que.QueDataType = HelperMethods.GetInt32(reader, "QueDataType");
                que.QueCatLocalName = HelperMethods.GetString(reader, "QueCatLocalName");
                que.Order = HelperMethods.GetInt32(reader, "Order");
                //CR-9
                que.IntegrationKey = HelperMethods.GetString(reader, "IntegrationKey");

                return que;
            }
        }


        internal class GetAllQueByQueCatFactory : IDomainObjectFactory<BEClient.Question>
        {
            public BEClient.Question Construct(IDataReader reader)
            {
                BEClient.Question que = new BEClient.Question();
                que.QueId = HelperMethods.GetGuid(reader, "QueId");
                que.QueCatId = HelperMethods.GetGuid(reader, "QueCatId");
                que.DefaultName = HelperMethods.GetString(reader, "DefaultName");
                que.Weight = HelperMethods.GetInt32(reader, "Weight");
                que.DivisionId = HelperMethods.GetGuid(reader, "DivisionId");
                que.IsActive = HelperMethods.GetBoolean(reader, "IsActive");
                que.IsDelete = HelperMethods.GetBoolean(reader, "IsDelete");
                que.LocalName = HelperMethods.GetString(reader, "LocalName");
                que.QueType = HelperMethods.GetString(reader, "QueType");
                que.QueDataType = HelperMethods.GetInt32(reader, "QueDataType");
                que.Order = HelperMethods.GetInt32(reader, "Order");

                //CR-9
                que.IntegrationKey = HelperMethods.GetString(reader, "IntegrationKey");
                return que;
            }
        }

        internal class GetAllQueByVacancyFactory : IDomainObjectFactory<BEClient.Question>
        {
            public BEClient.Question Construct(IDataReader reader)
            {
                BEClient.Question objQuestion = new BEClient.Question();
                objQuestion.QueId = HelperMethods.GetGuid(reader, "QueId");
                objQuestion.LocalName = HelperMethods.GetString(reader, "LocalName");
                objQuestion.QueDataType = HelperMethods.GetInt32(reader, "QueDataType");
                //CR-9
                objQuestion.IntegrationKey = HelperMethods.GetString(reader, "IntegrationKey");
                return objQuestion;
            }
        }

     
    }
}