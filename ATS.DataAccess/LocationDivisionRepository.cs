using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;

namespace ATS.DataAccess
{
    public class LocationDivisionRepository : Repository<BEClient.LocationDivision>
    {
        public LocationDivisionRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }
        public Guid AddLocationDivision(BEClient.LocationDivision locationdivision)
        {
            //bool LocalTrasaction = false;
            try
            {
                //if (base.Transaction == null)
                //{
                //    base.BeginTransaction();
                //    LocalTrasaction = true;
                //}
                //base.BeginTransaction();
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertLocationDivision");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@JobLocationId", DbType.Guid, locationdivision.JobLocationId);
                Database.AddInParameter(command, "@DivisionId", DbType.Guid, locationdivision.DivisionId);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, locationdivision.IsDelete);
                Database.AddOutParameter(command, "LocationDivisionId", DbType.Guid, 16); ;
                var result = base.Add(command, "LocationDivisionId");
                if (result != null)
                {
                    Guid.TryParse(result.ToString(), out reREsult);
                }
                //if (LocalTrasaction)
                //    base.CommitTransaction();
                return reREsult;
            }
            catch
            {
                //if (LocalTrasaction)
                //    this.RollbackTransaction();
                throw;
            }
        }

        public bool DeleteLocationDivision(Guid LocationDivisionId)
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
                DbCommand command = Database.GetStoredProcCommand("DeleteLocationDivision");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@LocationDivisionId", System.Data.DbType.Guid, LocationDivisionId);
                var result = base.Remove(command);
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

        public bool DeleteLocationDivisionByJobLocationId(Guid JobLocationId)
        {
            //bool LocalTrasaction = false;
            try
            {
                //if (base.Transaction == null)
                //{
                //    base.BeginTransaction();
                //    LocalTrasaction = true;
                //}
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("DeleteLocationDivisionByJobLocationId");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@JobLocationId", System.Data.DbType.Guid, JobLocationId);
                var result = base.Remove(command);
                if (result != null)
                {
                    bool.TryParse(result.ToString(), out reREsult);
                }
                //if (LocalTrasaction)
                //    base.CommitTransaction();
                return reREsult;
            }
            catch
            {
                //if (LocalTrasaction)
                //    this.RollbackTransaction();
                throw;
            }
        }



    }
}
