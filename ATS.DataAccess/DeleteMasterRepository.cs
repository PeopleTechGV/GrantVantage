using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;

namespace ATS.DataAccess
{
    public class DeleteMasterRepository:Repository<BEClient.DeleteMaster>
    {
        public DeleteMasterRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }
        public bool Delete(BEClient.DeleteMaster deletemaster)
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
                DbCommand command = Database.GetStoredProcCommand("DeleteMasters");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@Id", DbType.Guid, deletemaster.Id);
                Database.AddInParameter(command, "@FieldName", DbType.String, deletemaster.FieldName);
                Database.AddInParameter(command, "@TableName", DbType.String, deletemaster.Tablename);

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
    }
}
