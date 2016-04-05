using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Data.Common;
using System.Data;

namespace ATS.DataAccess
{
    public class PermissionAndScopeRepository : Repository<BEClient.UserSecurityRole>
    {
        public PermissionAndScopeRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public Guid InsertAdHocPrivilege(Guid privilegeId,string permission,string scope, Guid clientId, Guid userId)
        {
            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertAdHocPrivilege");

                Database.AddInParameter(command, "@UserId", System.Data.DbType.Guid, userId);

                Database.AddInParameter(command, "@ClientId", DbType.Guid, clientId);

                Database.AddInParameter(command, "@ATSPrivilegeId", DbType.Guid,privilegeId);

                Database.AddInParameter(command, "@PermissionType", DbType.String, permission);

                Database.AddInParameter(command, "@Scope", DbType.String, scope);

                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, false);

                Database.AddOutParameter(command, "@AdHocPrivilegeId", DbType.Guid, 32);

                var result = base.Add(command, "AdHocPrivilegeId");

                if (result != null)
                {
                    Guid.TryParse(result.ToString(), out reREsult);
                }

                return reREsult;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
