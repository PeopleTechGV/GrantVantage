using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;

namespace ATS.DataAccess
{
    public class ATSRolePrivilegeRepository : Repository<BEClient.ATSRolePrivilege>
    {
        public ATSRolePrivilegeRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public Guid AddATSRolePrivilege(BEClient.ATSRolePrivilege atsroleprivilege)
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
                DbCommand command = Database.GetStoredProcCommand("InsertATSRolePrivilege");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@ClientId", DbType.Guid, atsroleprivilege.ClientId);
                Database.AddInParameter(command, "@ATSSecurityRoleId", DbType.Guid, atsroleprivilege.ATSSecurityRoleId);
                Database.AddInParameter(command, "@ATSRelativePrivilegeId", DbType.Guid, atsroleprivilege.ATSRelativePrivilegeId);
                Database.AddInParameter(command, "@ScopeAll", DbType.Boolean, atsroleprivilege.ScopeAll);
                Database.AddInParameter(command, "@ScopeOwn", DbType.Boolean, atsroleprivilege.ScopeOwn);
                Database.AddInParameter(command, "@ScopeChild", DbType.Boolean, atsroleprivilege.ScopeChild);
                Database.AddInParameter(command, "@ScopeSister", DbType.Boolean, atsroleprivilege.ScopeSister);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, atsroleprivilege.IsDelete);
                Database.AddOutParameter(command, "ATSRolePrivilegeId", DbType.Guid, 16);

                var result = base.Add(command, "ATSRolePrivilegeId");

                if (result != null)
                {
                    Guid.TryParse(result.ToString(), out reREsult);
                }
                if (LocalTrasaction)
                    this.CommitTransaction();

                return reREsult;
            }

            catch
            {
                if (LocalTrasaction)
                    this.RollbackTransaction();
                throw;
            }
        }

        public List<BEClient.ATSRolePrivilege> GetSRPByRoleId(Guid AtsSecurityRoleId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetSRPByRoleId");
                Database.AddInParameter(command, "@ATSSecurityRoleId", DbType.Guid, AtsSecurityRoleId);
                return base.Find(command, new GetSRPByRoleIdFactory());
            }
            catch
            {
                throw;
            }
        }
        public List<BEClient.ATSRolePrivilege> GetAllRelativePrivilege()
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllATSRelativePrivileges");
                return base.Find(command, new GetAllRelativePrivilegeFactory(), false);
            }

            catch
            {
                throw;
            }
        }
        public List<BEClient.ATSRolePrivilege> GetAllPrivilegeBySecurityRoles(string ATSSecurityRoleId, Guid ClientId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllPrivilegeBySecurityRoleId");
                Database.AddInParameter(command, "@SecurityRoleId", DbType.String, ATSSecurityRoleId);
                Database.AddInParameter(command, "@ClientId", DbType.Guid, ClientId);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, false);
                return base.Find(command, new GetAllPrivilegeBySecurityRolesFactory(), false);
            }

            catch
            {
                throw;
            }
        }

        public bool RemoveSRPByATSSecurityRoleId(Guid ATSSecurityRoleId)
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
                DbCommand command = Database.GetStoredProcCommand("DeleteSRPByAtsRoleId");
                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ATSSecurityRoleId", DbType.Guid, ATSSecurityRoleId);
                var result = base.Remove(command);

                if (result != null)
                {
                    bool.TryParse(result.ToString(), out reREsult);
                }
                if (LocalTrasaction)
                    this.CommitTransaction();

                return reREsult;
            }
            catch
            {
                throw;
            }
        }

        internal class GetAllPrivilegeBySecurityRolesFactory : IDomainObjectFactory<BEClient.ATSRolePrivilege>
        {
            public BEClient.ATSRolePrivilege Construct(IDataReader reader)
            {
                BEClient.ATSRolePrivilege objRelativePrivilages = new BEClient.ATSRolePrivilege();
                objRelativePrivilages.ATSRelativePrivilegeId = HelperMethods.GetGuid(reader, "ATSRelativePrivilegeId");
                objRelativePrivilages.DisplayName = HelperMethods.GetString(reader, "DisplayName");
                int a = HelperMethods.GetInt32(reader, "A");
                objRelativePrivilages.ScopeAll = Convert.ToBoolean(a);
                int c = HelperMethods.GetInt32(reader, "C");
                objRelativePrivilages.ScopeChild = Convert.ToBoolean(c);
                int o = HelperMethods.GetInt32(reader, "O");
                objRelativePrivilages.ScopeOwn = Convert.ToBoolean(o);
                int s = HelperMethods.GetInt32(reader, "S");
                objRelativePrivilages.ScopeSister = Convert.ToBoolean(s);

                //objRelativePrivilages.ScopeAll = HelperMethods.GetBoolean(reader, "A");
                //objRelativePrivilages.ScopeChild = HelperMethods.GetBoolean(reader, "C");
                //objRelativePrivilages.ScopeOwn = HelperMethods.GetBoolean(reader, "O");
                //objRelativePrivilages.ScopeSister = HelperMethods.GetBoolean(reader, "S");

                objRelativePrivilages.DisplayOrder = HelperMethods.GetInt32(reader, "DisplayOrder");
                objRelativePrivilages.RelativeDisplayOrder = HelperMethods.GetInt32(reader, "RelativeDisplayOrder");
                return objRelativePrivilages;
            }
        }
        internal class GetAllRelativePrivilegeFactory : IDomainObjectFactory<BEClient.ATSRolePrivilege>
        {
            public BEClient.ATSRolePrivilege Construct(IDataReader reader)
            {
                BEClient.ATSRolePrivilege objRelativePrivilages = new BEClient.ATSRolePrivilege();
                objRelativePrivilages.ATSRelativePrivilegeId = HelperMethods.GetGuid(reader, "ATSRelativePrivilegeId");
                objRelativePrivilages.DisplayName = HelperMethods.GetString(reader, "DisplayName");
                objRelativePrivilages.ScopeAll = HelperMethods.GetBoolean(reader, "ScopeAll");
                objRelativePrivilages.ScopeChild = HelperMethods.GetBoolean(reader, "ScopeChild");
                objRelativePrivilages.ScopeOwn = HelperMethods.GetBoolean(reader, "ScopeOwn");
                objRelativePrivilages.ScopeSister = HelperMethods.GetBoolean(reader, "ScopeSister");
                objRelativePrivilages.DisplayOrder = HelperMethods.GetInt32(reader, "DisplayOrder");
                objRelativePrivilages.RelativeDisplayOrder = HelperMethods.GetInt32(reader, "RelativeDisplayOrder");
                return objRelativePrivilages;
            }
        }

        internal class GetSRPByRoleIdFactory : IDomainObjectFactory<BEClient.ATSRolePrivilege>
        {
            public BEClient.ATSRolePrivilege Construct(IDataReader reader)
            {
                BEClient.ATSRolePrivilege objATSRolePrivilege = new BEClient.ATSRolePrivilege();
                objATSRolePrivilege.ATSRolePrivilegeId = HelperMethods.GetGuid(reader, "SecurityRolePrivilegeId");
                objATSRolePrivilege.ATSRelativePrivilegeId = HelperMethods.GetGuid(reader, "ATSRelativePrivilegeId");
                objATSRolePrivilege.ATSSecurityRoleId = HelperMethods.GetGuid(reader, "ATSSecurityRoleId");
                objATSRolePrivilege.ScopeAll = HelperMethods.GetBoolean(reader, "ScopeAll");
                objATSRolePrivilege.ScopeChild = HelperMethods.GetBoolean(reader, "ScopeChild");
                objATSRolePrivilege.ScopeOwn = HelperMethods.GetBoolean(reader, "ScopeOwn");
                objATSRolePrivilege.ScopeSister = HelperMethods.GetBoolean(reader, "ScopeSister");
                objATSRolePrivilege.DisplayName = HelperMethods.GetString(reader, "DisplayName");
                objATSRolePrivilege.DisplayOrder = HelperMethods.GetInt32(reader, "DisplayOrder");
                objATSRolePrivilege.RelativeDisplayOrder = HelperMethods.GetInt32(reader, "RelativeDisplayOrder");

                return objATSRolePrivilege;
            }
        }
    }
}
