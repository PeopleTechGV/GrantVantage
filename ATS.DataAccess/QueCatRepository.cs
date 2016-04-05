using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;


namespace ATS.DataAccess
{
    public class QueCatRepository : Repository<BEClient.QueCat>
    {
        public QueCatRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public Guid AddQueCat(BEClient.QueCat QueCat, string fieldValue)
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
                DbCommand command = Database.GetStoredProcCommand("InsertQueCat");

                command.CommandTimeout = 100;



                Database.AddInParameter(command, "@DefaultName", DbType.String, QueCat.DefaultName);
                Database.AddInParameter(command, "@Weight", DbType.Int32, QueCat.Weight);
                Database.AddInParameter(command, "@Order", DbType.Int32, QueCat.Order);
                Database.AddInParameter(command, "@IsActive", DbType.Boolean, QueCat.IsActive);
                Database.AddInParameter(command, "@FieldValue", DbType.String, fieldValue);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, QueCat.IsDelete);
                Database.AddOutParameter(command, "QueCatId", DbType.Guid, 16);
                Database.AddInParameter(command, "@RoundAttributeId", DbType.Guid, QueCat.RoundAttributeId);
                Database.AddOutParameter(command, "@IsError", DbType.Int32, 4);

                string[] outParams = new string[] { "QueCatId", "@IsError" };

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


        public bool DeleteQueCat(Guid QueCatId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("DeleteQueCat");
                Database.AddInParameter(command, "@QueCatId", DbType.Guid, QueCatId);
                return base.Save(command);
            }
            catch
            {
                throw;
            }
        }
       
        
        public bool UpdateQueCatOrder(Guid QueCatId ,string orderDir)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("UpdateCatOrder");

                //Database.AddInParameter(command, "@NewOrder", DbType.Int32, NewOrder);
                //Database.AddInParameter(command, "@CurOrder", DbType.Int32, CurOrder);
                Database.AddInParameter(command, "@QueCatId", DbType.Guid, QueCatId);
                Database.AddInParameter(command, "@Orderdir", DbType.String, orderDir);
                return base.Save(command, false);

            }
            catch
            {
                throw;
            }
        }
        public bool UpdateQueCat(BEClient.QueCat QueCat, string fieldValue)
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
                DbCommand command = Database.GetStoredProcCommand("UpdateQueCat");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@DefaultName", DbType.String, QueCat.DefaultName);
                Database.AddInParameter(command, "@Weight", DbType.Int32, QueCat.Weight);
                Database.AddInParameter(command, "@IsActive", DbType.Boolean, QueCat.IsActive);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, QueCat.IsDelete);
                Database.AddInParameter(command, "@FieldValue", DbType.String, fieldValue);
                Database.AddInParameter(command, "@QueCatId", DbType.Guid, QueCat.QueCatId);
                Database.AddInParameter(command, "@RoundAttributeId", DbType.Guid, QueCat.RoundAttributeId);
                Database.AddOutParameter(command, "IsError", DbType.Int32, 4);
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



        
        
        public List<BEClient.QueCat> GetAllQueCat(Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllQueCat");
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.Find(command, new GetAllQueCatFactory());
            }
            catch
            {
                throw;
            }
        }

        public BEClient.QueCat GetRecordByRecordId(Guid recordId,Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetQueCatById");
                Database.AddInParameter(command, "@QueCatId", DbType.Guid, recordId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.FindOne(command, new GetRecordByRecordIdFactory());
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.QueCat> GetQueByQueCatId(Guid VarRndId, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetQueCatForFillCombo");
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VarRndId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.Find(command, new GetQueByQueCatIdFactory());
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.QueCat> FillQueCat(Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("FillQueCat");
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.Find(command, new GetQueByQueCatIdFactory());
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.QueCat> GetQueByTVacRndId(Guid TVacRndId, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetTQueCatForFillCombo");
                Database.AddInParameter(command, "@TVacRndId", DbType.Guid, TVacRndId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.Find(command, new GetQueByQueCatIdFactory());
            }
            catch
            {
                throw;
            }
        }



        public int GetQueCatDetails(Guid QueCatId)
        {
            try
            {
                int reResult = 0;
                DbCommand command = Database.GetStoredProcCommand("GetQueCatDetails");
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

        public int GetNewQueCatOrder()
        {
            try
            {
                int reResult = 0;
                DbCommand command = Database.GetStoredProcCommand("GetNewCatOrder");
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


        internal class GetQueByQueCatIdFactory : IDomainObjectFactory<BEClient.QueCat>
        {
            public BEClient.QueCat Construct(IDataReader reader)
            {
                BEClient.QueCat quecat = new BEClient.QueCat();
                quecat.QueCatId = HelperMethods.GetGuid(reader, "QueCatId");
                quecat.LocalName = HelperMethods.GetString(reader, "LocalName");
                return quecat;
            }
        }




        internal class GetAllQueCatFactory : IDomainObjectFactory<BEClient.QueCat>
        {
            public BEClient.QueCat Construct(IDataReader reader)
            {
                BEClient.QueCat quecat = new BEClient.QueCat();
                quecat.QueCatId = HelperMethods.GetGuid(reader, "QueCatId");
                quecat.DefaultName = HelperMethods.GetString(reader, "DefaultName");
                quecat.Weight = HelperMethods.GetInt32(reader, "Weight");
                quecat.Order = HelperMethods.GetInt32(reader, "Order");
                quecat.IsActive = HelperMethods.GetBoolean(reader, "IsActive");
                quecat.IsDelete = HelperMethods.GetBoolean(reader, "IsDelete");
                quecat.LocalName = HelperMethods.GetString(reader, "LocalName");
                quecat.RoundAttributeName = HelperMethods.GetString(reader, "RoundAttributesName");
                quecat.RoundAttributeNo = HelperMethods.GetInt32(reader, "RoundAttributesNo");
                quecat.QueCount = HelperMethods.GetInt32(reader, "QueCount");

                return quecat;
            }
        }
        internal class GetRecordByRecordIdFactory : IDomainObjectFactory<BEClient.QueCat>
        {
            public BEClient.QueCat Construct(IDataReader reader)
            {
                BEClient.QueCat quecat = new BEClient.QueCat();
                quecat.QueCatId = HelperMethods.GetGuid(reader, "QueCatId");
                quecat.DefaultName = HelperMethods.GetString(reader, "DefaultName");
                quecat.Weight = HelperMethods.GetInt32(reader, "Weight");
                quecat.IsActive = HelperMethods.GetBoolean(reader, "IsActive");
                quecat.IsDelete = HelperMethods.GetBoolean(reader, "IsDelete");
                quecat.LocalName = HelperMethods.GetString(reader, "LocalName");
                quecat.RoundAttributeId = HelperMethods.GetGuid(reader, "RoundAttributeId");
                return quecat;
            }
        }


    }
}
