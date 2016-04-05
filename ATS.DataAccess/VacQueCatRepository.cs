using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;

namespace ATS.DataAccess
{
    public class VacQueCatRepository : Repository<BEClient.VacQueCat>
    {
        public VacQueCatRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }
        //public Guid AddSaveVacQueCat(BEClient.VacQueCat VacQueCat,int imilli = 0)
        //{
        //    bool IsInnerTransaction = false;
        //    try
        //    {
        //        if (base.Transaction == null)
        //        {
        //            base.BeginTransaction();
        //            IsInnerTransaction = true;
        //        }
        //        Guid reResult = Guid.Empty;
        //        DbCommand command = Database.GetStoredProcCommand("InsertVacQueCat");
        //        command.CommandTimeout = 100;
        //        Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacQueCat.VacRndId);
        //        Database.AddInParameter(command, "@QueCatId", DbType.Guid, VacQueCat.QueCatId);
        //        Database.AddInParameter(command, "@Weight", DbType.Int32, VacQueCat.Weight);
        //        Database.AddInParameter(command, "@IsDelete", DbType.Boolean, false);
        //        Database.AddOutParameter(command, "@VacQueCatId", DbType.Guid, 16);
        //        var result = base.Add(command, "VacQueCatId",true,imilli);
        //        if (result != null)
        //        {
        //            Guid.TryParse(result.ToString(), out reResult);
        //        }
        //        if (IsInnerTransaction)
        //            base.CommitTransaction();
        //        return reResult;
        //    }
        //    catch
        //    {
        //        if (IsInnerTransaction)
        //            base.RollbackTransaction();

        //        throw;
        //    }
        //}


        public Guid AddSaveVacQueCat(BEClient.VacQueCat VacQueCat, int imilli = 0)
        {
            try
            {
                Guid reResult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertVacQueCat");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacQueCat.VacRndId);
                Database.AddInParameter(command, "@QueCatId", DbType.Guid, VacQueCat.QueCatId);
                //Database.AddInParameter(command, "@Weight", DbType.Int32, VacQueCat.Weight);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, false);
                Database.AddOutParameter(command, "@VacQueCatId", DbType.Guid, 16);
                var result = base.Add(command, "VacQueCatId", true, imilli);
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

        public Boolean UpdateVacQueCat(BEClient.VacQueCat VacQueCat)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("UpdateVacQueCat");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@VacQueCatId", DbType.Guid, VacQueCat.VacQueCatId);
                Database.AddInParameter(command, "@Weight", DbType.Int32, VacQueCat.Weight);
                var Result= base.Save(command, false);
                return HelperMethods.GetBoolean(Result);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.VacQueCat GetVacQueCatById(Guid VacQueCatId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetVacQueCatByVacQueCatId");
                Database.AddInParameter(command, "@VacQueCatId", DbType.Guid, VacQueCatId);
                return base.FindOne(command, new GetVacQueCatByIdFactory());
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.VacQueCat> GetVacQueCatByRoundId(Guid VacRndId, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllVacQueCatByRoundId");
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.Find(command, new GetVacQueCatByIdFactory());
            }
            catch
            {
                throw;
            }
        }

        public bool Delete(Guid VacQueCatId, Guid VacRndId)
        {
            try
            {
                bool reResult = false;
                DbCommand command = Database.GetStoredProcCommand("DeleteSingleVacQueCat");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                Database.AddInParameter(command, "@VacQueCatId", DbType.Guid, VacQueCatId);
                Database.AddInParameter(command, "@UpdatedBy", DbType.Guid, CurrentUser.UserId);
                Database.AddInParameter(command, "@UpdatedOn", DbType.DateTime, DateTime.UtcNow);
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

        public bool UpdateVacQueCatOrder(Guid VacRndId, Guid VacQueCatId, string OrderDir)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("UpdateVacQueCatOrder");
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                Database.AddInParameter(command, "@VacQueCatId", DbType.Guid, VacQueCatId);
                Database.AddInParameter(command, "@Orderdir", DbType.String, OrderDir);
                return base.Save(command, false);

            }
            catch
            {
                throw;
            }
        }

        internal class GetVacQueCatByIdFactory : IDomainObjectFactory<BEClient.VacQueCat>
        {
            public BEClient.VacQueCat Construct(IDataReader reader)
            {
                BEClient.VacQueCat VacQueCat = new BEClient.VacQueCat();
                VacQueCat.VacQueCatId = HelperMethods.GetGuid(reader, "VacQueCatId");
                VacQueCat.VacRndId = HelperMethods.GetGuid(reader, "VacRndId");
                VacQueCat.QueCatId = HelperMethods.GetGuid(reader, "QueCatId");
                VacQueCat.objQueCat = new BEClient.QueCat();
                VacQueCat.objQueCat.LocalName = HelperMethods.GetString(reader, "LocalName");
                VacQueCat.objQueCat.DefaultName = HelperMethods.GetString(reader, "DefaultName");
                VacQueCat.Weight = HelperMethods.GetInt32(reader, "Weight");
                VacQueCat.order = HelperMethods.GetInt32(reader, "Order");
                VacQueCat.DivisionId = HelperMethods.GetGuid(reader, "DivisionId");
                VacQueCat.VacQueCount = HelperMethods.GetInt32(reader, "VacQueCount");
                return VacQueCat;
            }
        }
    }
}