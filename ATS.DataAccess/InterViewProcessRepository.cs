using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
namespace ATS.DataAccess
{
    public class InterViewProcessRepository : Repository<BEClient.InterViewProcess>
    {
        public InterViewProcessRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }
        public List<BEClient.InterViewProcess> GetQueCatDetailsBtyOrder(Guid VacRndId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetQueCatDetailsBtyOrder");
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                return base.Find(command, new GetQueCatDetailsBtyOrderFactory(), false);
            }
            catch
            {
                throw;
            }
        }
        public BEClient.InterViewProcess GetQueCatDetailsBtyOrder(Guid VacRndId, int? order, Guid? ScheduleIntId)
        {
            try
            {
                int _Row = 0;
                DbCommand command = Database.GetStoredProcCommand("GetQueCatDetailsBtyOrder");
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                if (ScheduleIntId != null)
                {
                    Database.AddInParameter(command, "@ScheduleIntId", DbType.Guid, ScheduleIntId);
                }
                if (Int32.TryParse(order.ToString(), out _Row))
                {
                    Database.AddInParameter(command, "@Row", DbType.Int32, _Row);
                }
                return base.FindOne(command, new GetQueCatDetailsBtyOrderFactory(), false);
            }
            catch
            {
                throw;
            }
        }
        internal class GetQueCatDetailsBtyOrderFactory : IDomainObjectFactory<BEClient.InterViewProcess>
        {
            public BEClient.InterViewProcess Construct(IDataReader reader)
            {
                BEClient.InterViewProcess InterViewProcess = new BEClient.InterViewProcess();
                InterViewProcess.ObjQueCat = new BEClient.QueCat();
                InterViewProcess.ObjQueCat.QueCatId = HelperMethods.GetGuid(reader, "QueCatId");
                InterViewProcess.ObjQueCat.LocalName = HelperMethods.GetString(reader, "LocalName");
                InterViewProcess.TotalCat = HelperMethods.GetInt32(reader, "TotalCount");
                InterViewProcess.CurrentCat = HelperMethods.GetInt64(reader, "Row");
                InterViewProcess.VacRndId = HelperMethods.GetGuid(reader, "VacRndId");
                InterViewProcess.ObjQueCat.Weight = HelperMethods.GetInt32(reader, "Weight");
                InterViewProcess.ObjQueCat.Score = HelperMethods.GetFloat(reader, "Score");
                return InterViewProcess;
            }
        }

    }
}
