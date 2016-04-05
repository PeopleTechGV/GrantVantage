using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEMaster = ATS.BusinessEntity.Master;
using System.Data.Common;
using System.Data;

namespace ATS.DataAccess.Master
{
    public class StoredProcedureRepository : Repository<BEMaster.StoredProcedure>
    {

        public BEMaster.StoredProcedure GetRoutineDefination(String RoutineName)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetRoutineDefination");
                Database.AddInParameter(command, "@RoutineName", DbType.String, RoutineName);
                return base.FindOne(command, new GetRoutine(), false);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateRoutineDefination(String RoutineDefination)
        {
            bool LocalTransaction = false;
            bool result = false;
            if (base.Transaction == null)
            {
                LocalTransaction = true;
                base.BeginTransaction();
            }
            try
            {
                DbCommand command = Database.GetSqlStringCommand(RoutineDefination);
                result = base.Save(command, false);

                if (LocalTransaction)
                    base.CommitTransaction();
            }
            catch (Exception)
            {
                if (LocalTransaction)
                    base.RollbackTransaction();
                throw;
            }
            return result;
        }
    }
    internal class GetRoutine : IDomainObjectFactory<BEMaster.StoredProcedure>
    {
        public BEMaster.StoredProcedure Construct(IDataReader reader)
        {
            BEMaster.StoredProcedure objStoredProcedure = new BEMaster.StoredProcedure();
            objStoredProcedure.ROUTINE_DEFINITION = HelperMethods.GetString(reader, "ROUTINE_DEFINITION");
            return objStoredProcedure;
        }
    }
}
