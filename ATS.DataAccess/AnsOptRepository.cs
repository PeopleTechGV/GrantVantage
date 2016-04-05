using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
namespace ATS.DataAccess
{
    public class AnsOptRepository:Repository<BEClient.AnsOpt>
    {
        public AnsOptRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }


        public Guid AddAnsOpt(BEClient.AnsOpt pAnsOpt)
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
                DbCommand command = Database.GetStoredProcCommand("InsertAnsOpt");

                command.CommandTimeout = 100;

                //Database.AddInParameter(command, "@DegreeTypeId", DbType.Guid, pDegreeType.DegreeTypeId);

                Database.AddInParameter(command, "@QueId", DbType.Guid, pAnsOpt.QueId);
                Database.AddInParameter(command, "@DefaultName", DbType.String, pAnsOpt.DefaultName);
                Database.AddInParameter(command, "@Weight", DbType.Int32, pAnsOpt.Weight);
                Database.AddInParameter(command, "@Order", DbType.Int32, pAnsOpt.Order);
                Database.AddInParameter(command, "@IsActive", DbType.Boolean, pAnsOpt.IsActive);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, pAnsOpt.IsDelete);
                Database.AddOutParameter(command, "AnsOptId", DbType.Guid, 16);
                var result = base.Add(command, "AnsOptId");

                if (result != null)
                {
                  
                    Guid.TryParse(result.ToString(), out reREsult);
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


        public bool UpdateAnsOpt(BEClient.AnsOpt pAnsOpt)
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
                DbCommand command = Database.GetStoredProcCommand("UpdateAnsOpt");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@QueId", DbType.Guid, pAnsOpt.QueId);
                Database.AddInParameter(command, "@Order", DbType.Int32, pAnsOpt.Order);

                Database.AddInParameter(command, "@DefaultName", DbType.String, pAnsOpt.DefaultName);

                Database.AddInParameter(command, "@Weight", DbType.Int32, pAnsOpt.Weight);

                Database.AddInParameter(command, "@IsActive", DbType.Boolean, pAnsOpt.IsActive);

                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, pAnsOpt.IsDelete);

                Database.AddInParameter(command, "AnsOptId", DbType.Guid, pAnsOpt.AnsOptId);



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


        public bool RemoveAnsOptByQueId(Guid QueId)
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
                DbCommand command = Database.GetStoredProcCommand("RemoveAnsOptByQueId");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@QueId", DbType.Guid, QueId);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, true);
                var result = base.Save(command, false);
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

        public List<BEClient.AnsOpt> GetAllAnsOptByQue(Guid QueId,Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAnsOptByQueId");

                Database.AddInParameter(command, "@QueId", DbType.Guid, QueId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);

                return base.Find(command, new GetAllAnsOptByQuesTypeFactory());

            }
            catch
            {
                throw;
            }
        }
        public bool UpdateAnsOptOrder( Guid AnsOptId, Guid QueId,string OrderDir)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("UpdateAnsOptOrder");

        
                Database.AddInParameter(command, "@AnsOptId", DbType.Guid, AnsOptId);
                Database.AddInParameter(command, "@QueId", DbType.Guid, QueId);
                Database.AddInParameter(command, "@Orderdir", DbType.String, OrderDir);
                return base.Save(command,false);

            }
            catch
            {
                throw;
            }
        }
        public BEClient.AnsOpt GetRecordByRecordId(Guid
            AnsOptId, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAnsOptById");

                Database.AddInParameter(command, "@AnsOptId", DbType.Guid, AnsOptId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);

                return base.FindOne(command, new GetAnsOptByIdFactory());

            }
            catch
            {
                throw;
            }
        }

        public object GetOrderAnsOptByQue(Guid QueId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetOrderAnsOpt");
                Database.AddInParameter(command, "@QueId", DbType.Guid, QueId);
                return Database.ExecuteScalar(command);

            }
            catch
            {
                throw;
            }
        }


        public bool InlineUpdateAnsOpt(BEClient.AnsOpt objAnsOpt, Guid LanguageId)
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
                DbCommand command = Database.GetStoredProcCommand("InlineUpdateAnsOpt");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@LocalName", DbType.String, objAnsOpt.LocalName);
                Database.AddInParameter(command, "@Weight", DbType.Int32, objAnsOpt.Weight);

                Database.AddInParameter(command, "@AnsOptId", DbType.Guid, objAnsOpt.AnsOptId);
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

        public bool UpdateOrder(Guid AnsOptId,Guid QueId,int order)
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
                DbCommand command = Database.GetStoredProcCommand("UpdateOrderAnsOpt");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@AnsOptId", DbType.String,AnsOptId);
                Database.AddInParameter(command, "@QueId", DbType.Int32, QueId);

                Database.AddInParameter(command, "@Order", DbType.Guid,order);
                
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

        public Int32 GetValueByAnsopt(string AnsoptId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetValueByAnsOpt");
                Database.AddInParameter(command, "@AnsOptId", DbType.String, AnsoptId);
                string value =base.FindScalarValue(command);
                return Convert.ToInt32(value);
            }
            catch
            {
                throw;
            }
        }

        public int GetNewAnsOrder(Guid QueId)
        {
            try
            {
                int reResult = 0;
                DbCommand command = Database.GetStoredProcCommand("GetNewAnsOrder");
                Database.AddInParameter(command, "@QueId", DbType.Guid, QueId);

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

        public bool DeleteAnsOpt(Guid AnsOptId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("DeleteAns");
                Database.AddInParameter(command, "@AnsOptId", DbType.Guid, AnsOptId);
                return base.Save(command, true);
            }
            catch
            {
                throw;
            }
        }

        internal class GetAllAnsOptByQuesTypeFactory : IDomainObjectFactory<BEClient.AnsOpt>
        {
            public BEClient.AnsOpt Construct(IDataReader reader)
            {
                BEClient.AnsOpt AnsOpt = new BEClient.AnsOpt();
                AnsOpt.AnsOptId = HelperMethods.GetGuid(reader, "AnsOptId");
                AnsOpt.QueId = HelperMethods.GetGuid(reader, "QueId");
                AnsOpt.DefaultName = HelperMethods.GetString(reader, "DefaultName");
                AnsOpt.Weight = HelperMethods.GetInt32(reader, "Weight");
                AnsOpt.LocalName = HelperMethods.GetString(reader, "LocalName");
                AnsOpt.Order = HelperMethods.GetInt32(reader, "Order");
                return AnsOpt;
            }
        }
        internal class GetAnsOptByIdFactory : IDomainObjectFactory<BEClient.AnsOpt>
        {
            public BEClient.AnsOpt Construct(IDataReader reader)
            {
                BEClient.AnsOpt AnsOpt = new BEClient.AnsOpt();
                AnsOpt.AnsOptId = HelperMethods.GetGuid(reader, "AnsOptId");
                AnsOpt.QueId = HelperMethods.GetGuid(reader, "QueId");
                AnsOpt.DefaultName = HelperMethods.GetString(reader, "DefaultName");
                AnsOpt.Weight = HelperMethods.GetInt32(reader, "Weight");
                AnsOpt.LocalName = HelperMethods.GetString(reader, "LocalName");
                AnsOpt.Order = HelperMethods.GetInt32(reader, "Order");
                AnsOpt.QueDataType = HelperMethods.GetInt32(reader, "QueDataType");
                AnsOpt.QueDivisionId =HelperMethods.GetGuid(reader, "DivisionId");

                return AnsOpt;
            }
        }



        public List<BEClient.AnsOpt> GetAnsListByQueId(Guid QueId, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAnsListByQueId");
                Database.AddInParameter(command, "@QueId", DbType.Guid, QueId);
                return base.Find(command, new GetAnsListByQueIdFactory());
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.AnsOpt> GetAnsListByAppAnsId(string Answer, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAppAnswerOpt");
                Database.AddInParameter(command, "@Answer", DbType.String, Answer);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.Find(command, new GetAnsListByAppAnsIdFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal class GetAnsListByAppAnsIdFactory:IDomainObjectFactory<BEClient.AnsOpt>
        {
            public BEClient.AnsOpt Construct(IDataReader reader)
            {
                BEClient.AnsOpt Answer = new BEClient.AnsOpt();
                Answer.LocalName = HelperMethods.GetString(reader, "LocalName");
                return Answer;
            }
        }

        internal class GetAnsListByQueIdFactory : IDomainObjectFactory<BEClient.AnsOpt>
        {
            public BEClient.AnsOpt Construct(IDataReader reader)
            {
                BEClient.AnsOpt Answer = new BEClient.AnsOpt();
                Answer.DefaultName = HelperMethods.GetString(reader, "DefaultlName");
                Answer.AnsOptId = HelperMethods.GetGuid(reader, "AnsOptId");
                Answer.QueDivisionId= HelperMethods.GetGuid(reader, "DivisionId");
                Answer.QueId = HelperMethods.GetGuid(reader, "QueId");
                Answer.Order= HelperMethods.GetInt32(reader, "Order");
                Answer.Weight = HelperMethods.GetInt32(reader, "Weight");
                Answer.QueDataType= HelperMethods.GetInt32(reader, "QueDataType");
                
                return Answer;
            }
        }

    }
}
