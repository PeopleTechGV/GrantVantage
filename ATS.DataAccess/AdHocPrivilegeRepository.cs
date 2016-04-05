using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Data.Common;
using System.Data;

namespace ATS.DataAccess
{
   public class AdHocPrivilegeRepository : Repository<BEClient.AdHocPrivilege>
    {
       public AdHocPrivilegeRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

       public List<BEClient.AdHocPrivilege> GetAdHocRolesByUserAndClient(Guid clientId, Guid userId)
       {
           try
           {
               DbCommand command = Database.GetStoredProcCommand("GetAdHocRolesByUserAndClient");

               Database.AddInParameter(command, "@ClientId", DbType.Guid, clientId);
               Database.AddInParameter(command, "@UserId", DbType.Guid, userId);

               List<BEClient.AdHocPrivilege> ListATSSecurityRolePrivilages = base.Find(command, new GetAdHocRolesByUserAndClientFactory());
               if (ListATSSecurityRolePrivilages != null)
               {
                   return ListATSSecurityRolePrivilages;
               }
               return null;
           }
           catch
           {
               throw;
           }
       }

       internal class GetAdHocRolesByUserAndClientFactory : IDomainObjectFactory<BEClient.AdHocPrivilege>
       {
           public BEClient.AdHocPrivilege Construct(IDataReader reader)
           {
               BEClient.AdHocPrivilege objAdHocPrivilege = new BEClient.AdHocPrivilege();

               objAdHocPrivilege.ATSPrivilegeId = HelperMethods.GetGuid(reader, "ATSPrivilegeId");

               objAdHocPrivilege.PermissionType = HelperMethods.GetString(reader, "PermissionType");

               objAdHocPrivilege.Scope = HelperMethods.GetString(reader, "Scope");

               return objAdHocPrivilege;
           }
       }
    }
}
