using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Data.Common;
using System.Data;

namespace ATS.DataAccess
{
    public class TVacQueCatRepository : Repository<BEClient.TVacQueCat>
    {
        public TVacQueCatRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public Guid AddTVacQueCat(BEClient.TVacQueCat TVacQueCat)
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
                DbCommand command = Database.GetStoredProcCommand("InsertTVacQueCat");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@TVacId", System.Data.DbType.Guid, TVacQueCat.TVacId);
                Database.AddInParameter(command, "@TVacRndId", DbType.Guid, TVacQueCat.TVacRndId);
                Database.AddInParameter(command, "@QueCatId", DbType.Guid, TVacQueCat.QueCatId);
                //Database.AddInParameter(command, "@Weight", DbType.Int32, TVacQueCat.Weight);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, TVacQueCat.IsDelete);
                Database.AddOutParameter(command, "TVacQueCatId", DbType.Guid, 16); ;
                var result = base.Add(command, "TVacQueCatId");
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


        public Boolean UpdateTVacQueCatWeight(BEClient.TVacQueCat TVacQueCat)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("UpdateTVacQueCatWeight");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@TVacQueCatId", DbType.Guid, TVacQueCat.TVacQueCatId);
                Database.AddInParameter(command, "@Weight", DbType.Int32, TVacQueCat.Weight);
                var Result = base.Save(command);
                return HelperMethods.GetBoolean(Result);
            }
            catch
            {
                throw;
            }
        }


        public bool UpdateTVacQueCat(BEClient.TVacQueCat TVacQueCat)
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
                DbCommand command = Database.GetStoredProcCommand("UpdateTVacQueCat");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@TVacId", System.Data.DbType.Guid, TVacQueCat.TVacId);

                Database.AddInParameter(command, "@TVacRndId", DbType.Guid, TVacQueCat.TVacRndId);
                Database.AddInParameter(command, "@QueCatId", DbType.Guid, TVacQueCat.QueCatId);

                Database.AddInParameter(command, "@Weight", DbType.Int32, TVacQueCat.Weight);

                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, TVacQueCat.IsDelete);





                Database.AddInParameter(command, "@TVacQueCatId", DbType.Guid, 16); ;

                var result = base.Add(command, "TVacQueCatId");

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

        public List<BEClient.TVacQueCat> GetAllTVacQueCat()
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllTVacQueCat");

                return base.Find(command, new GetAllTVacQueCatFactory());
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.TVacQueCat> GetTVacQueCatByTVacId(Guid TVacId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetTVacQueCatByTVacId");
                Database.AddInParameter(command, "@TVacId", DbType.Guid, TVacId);
                return base.Find(command, new GetTVacQueCatByTVacIdFactory());
            }
            catch
            {
                throw;
            }
        }

        public BEClient.TVacQueCat GetRecordByRecordId(Guid TVacQueCatId , Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetTVacQueCatById");
                Database.AddInParameter(command, "@TVacQueCatId", DbType.Guid, TVacQueCatId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.FindOne(command, new GetRecordByRecordIdFactory());
            }
            catch
            {
                throw;
            }
        }

        internal class GetAllTVacQueCatFactory : IDomainObjectFactory<BEClient.TVacQueCat>
        {
            public BEClient.TVacQueCat Construct(IDataReader reader)
            {
                BEClient.TVacQueCat TVacQueCat = new BEClient.TVacQueCat();
                TVacQueCat.TVacQueCatId = HelperMethods.GetGuid(reader, "TVacQueCatId");
                TVacQueCat.TVacRndId = HelperMethods.GetGuid(reader, "TVacRndId");
                TVacQueCat.TVacId = HelperMethods.GetGuid(reader, "TVacId");
                TVacQueCat.QueCatId = HelperMethods.GetGuid(reader, "QueCatId");
                TVacQueCat.Weight = HelperMethods.GetInt32(reader, "Weight");
                TVacQueCat.IsDelete = HelperMethods.GetBoolean(reader, "IsDelete");
                return TVacQueCat;
            }
        }

        internal class GetRecordByRecordIdFactory : IDomainObjectFactory<BEClient.TVacQueCat>
        {
            public BEClient.TVacQueCat Construct(IDataReader reader)
            {
                BEClient.TVacQueCat TVacQueCat = new BEClient.TVacQueCat();
                TVacQueCat.TVacQueCatId = HelperMethods.GetGuid(reader, "TVacQueCatId");
                TVacQueCat.TVacRndId = HelperMethods.GetGuid(reader, "TVacRndId");
                TVacQueCat.QueCatId = HelperMethods.GetGuid(reader, "QueCatId");
                TVacQueCat.objQueCat = new BEClient.QueCat();
                TVacQueCat.objQueCat.LocalName = HelperMethods.GetString(reader, "LocalName");
                TVacQueCat.objQueCat.DefaultName = HelperMethods.GetString(reader, "DefaultName");
                TVacQueCat.Weight = HelperMethods.GetInt32(reader, "Weight");
                return TVacQueCat;
            }
        }
        internal class GetTVacQueCatByTVacIdFactory : IDomainObjectFactory<BEClient.TVacQueCat>
        {
            public BEClient.TVacQueCat Construct(IDataReader reader)
            {
                BEClient.TVacQueCat TVacQueCat = new BEClient.TVacQueCat();
                TVacQueCat.TVacQueCatId = HelperMethods.GetGuid(reader, "TVacQueCatId");
                TVacQueCat.TVacRndId = HelperMethods.GetGuid(reader, "TVacRndId");
                TVacQueCat.TVacId = HelperMethods.GetGuid(reader, "TVacId");
                TVacQueCat.QueCatId = HelperMethods.GetGuid(reader, "QueCatId");
                TVacQueCat.Weight = HelperMethods.GetInt32(reader, "Weight");
                TVacQueCat.IsDelete = HelperMethods.GetBoolean(reader, "IsDelete");
                return TVacQueCat;
            }
        }

        public List<BEClient.TVacQueCat> GetTVacQueCatByRoundId(Guid TVacRndId, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllTVacQueCatByRoundId");
                Database.AddInParameter(command, "@TVacRndId", DbType.Guid, TVacRndId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.Find(command, new GetVacQueCatByIdFactory());
            }
            catch
            {
                throw;
            }
        }
        public bool UpdateTVacQueCatOrder(Guid TVacRndId, Guid TVacQueCatId, string OrderDir)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("UpdateTVacQueCatOrder");
                Database.AddInParameter(command, "@TVacRndId", DbType.Guid, TVacRndId);
                Database.AddInParameter(command, "@TVacQueCatId", DbType.Guid, TVacQueCatId);
                Database.AddInParameter(command, "@Orderdir", DbType.String, OrderDir);
                return base.Save(command, false);

            }
            catch
            {
                throw;
            }
        }

        internal class GetVacQueCatByIdFactory : IDomainObjectFactory<BEClient.TVacQueCat>
        {
            public BEClient.TVacQueCat Construct(IDataReader reader)
            {
                BEClient.TVacQueCat TVacQueCat = new BEClient.TVacQueCat();
                TVacQueCat.TVacQueCatId = HelperMethods.GetGuid(reader, "TVacQueCatId");
                TVacQueCat.TVacRndId = HelperMethods.GetGuid(reader, "TVacRndId");
                TVacQueCat.QueCatId = HelperMethods.GetGuid(reader, "QueCatId");
                TVacQueCat.objQueCat = new BEClient.QueCat();
                TVacQueCat.objQueCat.LocalName = HelperMethods.GetString(reader, "LocalName");
                TVacQueCat.objQueCat.DefaultName = HelperMethods.GetString(reader, "DefaultName");
                TVacQueCat.Weight = HelperMethods.GetInt32(reader, "Weight");
                TVacQueCat.TVacQueCount = HelperMethods.GetInt32(reader, "TVacQueCount");
                TVacQueCat.Order = HelperMethods.GetInt32(reader, "Order");

                return TVacQueCat;
            }
        }

        public bool Delete(Guid TVacQueCatId, Guid TVacRndId)
        {
            try
            {
                bool reResult = false;
                DbCommand command = Database.GetStoredProcCommand("DeleteSingleTVacQueCat");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@TVacRndId", DbType.Guid, TVacRndId);
                Database.AddInParameter(command, "@TVacQueCatId", DbType.Guid, TVacQueCatId);
                var result = base.Remove(command);
                if (result != null)
                {
                    bool.TryParse(result.ToString(), out reResult);
                }
                return reResult;

            }
            catch
            {
                throw;
            }
        }

      
    }
}
