using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace ATS.DataAccess
{
    public class CommonRepository:Repository<bool>
    {

        public CommonRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }
        public object IsRecordExists(String TableName, String FieldName, Guid FieldValue)
        {
            DbCommand command = Database.GetStoredProcCommand("PrimaryKeyExists");

            Database.AddInParameter(command, "@TableName", DbType.String, TableName);

            Database.AddInParameter(command, "@FieldName", DbType.String, FieldName);

            Database.AddInParameter(command, "@FieldValue", DbType.Guid, FieldValue);

            return Database.ExecuteScalar(command);
        }

    }
}
