using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;

namespace ATS.DataAccess
{
    public class TVacQueRepository : Repository<BEClient.TVacQue>
    {
        public TVacQueRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }
        public Guid InsertTVacQue(BEClient.TVacQue objTVacQue)
        {
            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertTVacQue");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@TVacId", DbType.Guid, objTVacQue.TVacId);
                Database.AddInParameter(command, "@TVacQueCatId", DbType.Guid, objTVacQue.TVacQueCatId);
                Database.AddInParameter(command, "@TVacRndId", DbType.Guid, objTVacQue.TVacRndId);
                Database.AddInParameter(command, "@QueId", DbType.Guid, objTVacQue.QueId);
                //Database.AddInParameter(command, "@Weight", DbType.Int32, objTVacQue.Weight);
                Database.AddInParameter(command, "@IsActive", DbType.Boolean, true);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, false);
                Database.AddOutParameter(command, "TVacQueId", DbType.Guid, 32);
                var result = base.Add(command, "TVacQueId");
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

        public bool UpdateTVacQueWeightById(BEClient.TVacQue objTVacQue)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("UpdateTVacQueWeightById");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@TVacQueId", DbType.Guid, objTVacQue.TVacQueId);
                Database.AddInParameter(command, "@Weight", DbType.Int32, objTVacQue.Weight);
                var Result = base.Save(command);
                return HelperMethods.GetBoolean(Result);
            }
            catch
            {
                throw;
            }
        }


        public bool Update(BEClient.TVacQue objTVacQue)
        {
            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateTVacQue");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@TVacQueId", DbType.Guid, objTVacQue.TVacQueId);
                Database.AddInParameter(command, "@TVacId", DbType.Guid, objTVacQue.TVacId);
                Database.AddInParameter(command, "@TVacQueCatId", DbType.Guid, objTVacQue.TVacQueCatId);
                Database.AddInParameter(command, "@TVacRndId", DbType.Guid, objTVacQue.TVacRndId);
                Database.AddInParameter(command, "@QueId", DbType.Guid, objTVacQue.QueId);
                Database.AddInParameter(command, "@Weight", DbType.Int32, objTVacQue.Weight);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, objTVacQue.IsDelete);
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

        public bool Delete(Guid TVacQueId)
        {

            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("DeleteTVacQueByVacQueId");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@TVacQueId", DbType.Guid, TVacQueId);


                var result = base.Remove(command);

                if (result != null)
                {
                    reREsult = true;
                }

                return reREsult;
            }

            catch
            {
                throw;
            }
        }

        public BEClient.TVacQue GetTVacQueById(Guid TVacQueId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetTVacQueById");
                Database.AddInParameter(command, "@TVacQueId", DbType.Guid, TVacQueId);
                return base.FindOne(command, new GetTVacQueByIdFactory());
            }
            catch
            {
                throw;
            }
        }


        public List<BEClient.TVacQue> GetTVacQueIdByTVacId(Guid TVacId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetTVacQueIdByTVacId");
                Database.AddInParameter(command, "@TVacId", DbType.Guid, TVacId);
                return base.Find(command, new GetTVacQueIdByTVacIdFactory());
            }
            catch
            {
                throw;
            }
        }


        public List<BEClient.TVacQue> GetTVacQueByTVacQueCatId(Guid TVacQueCatId, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetTVacQueByTVacQueCatId");
                Database.AddInParameter(command, "@TVacQueCatId", DbType.Guid, TVacQueCatId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.Find(command, new GetTVacQueByTVacQueCatIdFactory());
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateTVacQueOrder(Guid TVacQueCatId, Guid TVacQueId, string OrderDir)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("UpdateTVacQueOrder");
                Database.AddInParameter(command, "@TVacQueCatId", DbType.Guid, TVacQueCatId);
                Database.AddInParameter(command, "@TVacQueId", DbType.Guid, TVacQueId);
                Database.AddInParameter(command, "@Orderdir", DbType.String, OrderDir);
                return base.Save(command, false);

            }
            catch
            {
                throw;
            }
        }

        internal class GetTVacQueByTVacQueCatIdFactory : IDomainObjectFactory<BEClient.TVacQue>
        {
            public BEClient.TVacQue Construct(IDataReader reader)
            {
                BEClient.TVacQue TVacQue = new BEClient.TVacQue();
                TVacQue.TVacQueId = HelperMethods.GetGuid(reader, "TVacQueId");
                TVacQue.QueType = HelperMethods.GetInt32(reader, "QueDataType");
                TVacQue.Order = HelperMethods.GetInt32(reader, "Order");
                TVacQue.TVacQueCatId = HelperMethods.GetGuid(reader, "TVacQueCatId");
                
                TVacQue.Weight = HelperMethods.GetInt32(reader, "Weight");
                TVacQue.TVacRndId = HelperMethods.GetGuid(reader, "TVacRndId");
                TVacQue.LocalName = HelperMethods.GetString(reader, "LocalName");
                TVacQue.DivisionId = HelperMethods.GetGuid(reader, "DivisionId");
                TVacQue.QueId = HelperMethods.GetGuid(reader, "QueId");
                return TVacQue;
            }
        }

        internal class GetTVacQueByIdFactory : IDomainObjectFactory<BEClient.TVacQue>
        {
            public BEClient.TVacQue Construct(IDataReader reader)
            {
                BEClient.TVacQue TVacQue = new BEClient.TVacQue();
                TVacQue.TVacQueId = HelperMethods.GetGuid(reader, "TVacQueId");
                TVacQue.TVacId = HelperMethods.GetGuid(reader, "TVacId");
                TVacQue.TVacQueCatId = HelperMethods.GetGuid(reader, "TVacQueCatId");
                TVacQue.TVacRndId = HelperMethods.GetGuid(reader, "TVacRndId");
                TVacQue.QueId = HelperMethods.GetGuid(reader, "QueId");
                TVacQue.Weight = HelperMethods.GetInt32(reader, "Weight");
                TVacQue.QueType = Convert.ToInt32(HelperMethods.GetString(reader, "QueType"));
                return TVacQue;
            }
        }
        internal class GetTVacQueIdByTVacIdFactory : IDomainObjectFactory<BEClient.TVacQue>
        {
            public BEClient.TVacQue Construct(IDataReader reader)
            {
                BEClient.TVacQue TVacQue = new BEClient.TVacQue();
                TVacQue.TVacQueId = HelperMethods.GetGuid(reader, "TVacQueId");
                TVacQue.TVacId = HelperMethods.GetGuid(reader, "TVacId");
                TVacQue.TVacQueCatId = HelperMethods.GetGuid(reader, "TVacQueCatId");
                TVacQue.TVacRndId = HelperMethods.GetGuid(reader, "TVacRndId");
                TVacQue.QueId = HelperMethods.GetGuid(reader, "QueId");
                TVacQue.Weight = HelperMethods.GetInt32(reader, "Weight");
                return TVacQue;
            }
        }

    }
}