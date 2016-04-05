using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Data.Common;
using System.Data;

namespace ATS.DataAccess
{
    public class UserSecurityRoleRepository : Repository<BEClient.UserSecurityRole>
    {
        public UserSecurityRoleRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }
        public Guid AddUserSecurityRole(BEClient.UserSecurityRole UserSecurityRole)
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
                DbCommand command = Database.GetStoredProcCommand("InsertUserSecurityRole");

                Database.AddInParameter(command, "@UserId", System.Data.DbType.Guid, UserSecurityRole.UserId);

                Database.AddInParameter(command, "@ClientId", DbType.Guid, UserSecurityRole.ClientId);

                if (UserSecurityRole.AtsSecurityRoleId.Equals(Guid.Empty))
                {
                    Database.AddInParameter(command, "@ATSSecurityRole", DbType.String, UserSecurityRole.AtsSecurityRole.ToString());
                }
                else
                { Database.AddInParameter(command, "@ATSSecurityRoleId", DbType.Guid, UserSecurityRole.AtsSecurityRoleId); }
                
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, UserSecurityRole.IsDelete);

                Database.AddOutParameter(command, "@UserSecurityRoleId", DbType.Guid, 16);

                var result = base.Add(command, "UserSecurityRoleId", false);

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
        public List<BEClient.UserSecurityRole> GetUserSecurityRoleByUserId(Guid UserId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetUserSecurityRoleByUserId");

                Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);


                List<BEClient.UserSecurityRole> ListUserSecurityRole = base.Find(command, new GetUSerSecurityRoleFactory(), false);
                if (ListUserSecurityRole != null)
                {
                    return ListUserSecurityRole;
                }
                return null;
            }
            catch
            {
                throw;
            }
        }
        public Guid InsertSecurityRoleByUser(Guid securityRoleId, Guid clientId, Guid userId)
        {
            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertSecurityRoleByUser");

                Database.AddInParameter(command, "@UserId", System.Data.DbType.Guid, userId);

                Database.AddInParameter(command, "@ClientId", DbType.Guid, clientId);

                Database.AddInParameter(command, "@ATSSecurityRoleId", DbType.Guid, securityRoleId);

                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, false);

                Database.AddOutParameter(command, "@UserSecurityRoleId", DbType.Guid, 32);

                var result = base.Add(command, "UserSecurityRoleId");

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

        public bool DeleteRoleByUserAndClient(Guid clientId, Guid userId)
        {
            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("DeleteRoleByUserAndClient");

                Database.AddInParameter(command, "@UserId", System.Data.DbType.Guid, userId);

                Database.AddInParameter(command, "@ClientId", DbType.Guid, clientId);

                var result = base.Remove(command);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        internal class GetUSerSecurityRoleFactory : IDomainObjectFactory<BEClient.UserSecurityRole>
        {
            public BEClient.UserSecurityRole Construct(IDataReader reader)
            {
                BEClient.UserSecurityRole objUserSecurityRole = new BEClient.UserSecurityRole();

                objUserSecurityRole.UserSecurityRoleId = HelperMethods.GetGuid(reader, "UserSecurityRoleId");
                objUserSecurityRole.UserId = HelperMethods.GetGuid(reader, "UserId");
                objUserSecurityRole.AtsSecurityRoleId = HelperMethods.GetGuid(reader, "ATSSecurityRoleId");
                String ATSSecurityRole = HelperMethods.GetString(reader, "DefaultName");
                BEClient.ATSSecurityRole currentATSSecurityRole = BEClient.ATSSecurityRole.Other;
                Enum.TryParse<BEClient.ATSSecurityRole>(ATSSecurityRole, true, out currentATSSecurityRole);
                objUserSecurityRole.AtsSecurityRole = currentATSSecurityRole;
                objUserSecurityRole.SecurityRoleName = ATSSecurityRole;
                return objUserSecurityRole;
            }
        }

    }
}
