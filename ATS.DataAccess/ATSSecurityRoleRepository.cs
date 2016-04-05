using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Data.Common;
using System.Data;

namespace ATS.DataAccess
{
    public class ATSSecurityRoleRepository : Repository<BEClient.ATSSecurityRolePrivilages>
    {
        public ATSSecurityRoleRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public List<BEClient.ATSSecurityRolePrivilages> GetAllPrivilegeBySecurityRoleId(Guid clientId, string securityRoleId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllPrivilegeBySecurityRoleId");

                Database.AddInParameter(command, "@ClientId", DbType.Guid, clientId);
                Database.AddInParameter(command, "@SecurityRoleId", DbType.String, securityRoleId);

                List<BEClient.ATSSecurityRolePrivilages> ListATSSecurityRolePrivilages = base.Find(command, new GetAllPrivilegeBySecurityRoleIdFactory());
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

        public List<BEClient.ATSSecurityRolePrivilages> GetAllATSSecurityRoleByClientAndLanguage(Guid clientId, Guid languageId, int sequenceNo)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllATSSecurityRoleByClientAndLanguage");

                Database.AddInParameter(command, "@ClientId", DbType.Guid, clientId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, languageId);
                Database.AddInParameter(command, "@SequenceNo", DbType.Int32, sequenceNo);

                List<BEClient.ATSSecurityRolePrivilages> ListATSSecurityRolePrivilages = base.Find(command, new GetAllATSSecurityRoleByClientAndLanguageFactory());
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

        public List<BEClient.ATSSecurityRolePrivilages> GetAllATSPrevilegeByClientAndLanguage(Guid clientId, Guid languageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllATSPrevilegeByClientAndLanguage");

                Database.AddInParameter(command, "@ClientId", DbType.Guid, clientId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, languageId);

                List<BEClient.ATSSecurityRolePrivilages> ListATSSecurityRolePrivilages = base.Find(command, new GetAllATSPrevilegeByClientAndLanguageFactory());
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

        public List<BEClient.ATSSecurityRolePrivilages> GetUserSecurityRole(BEClient.ATSPrivilage privilage, string securityRole)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetSecurityRolePrivilageByPrivilageAndRole");

                Database.AddInParameter(command, "@ATSPrivilage", DbType.String, privilage.ToString());
                Database.AddInParameter(command, "@Userid", DbType.Guid, base.CurrentUser.UserId);
                Database.AddInParameter(command, "@ATSSecurityRole", DbType.String, securityRole.ToString());

                List<BEClient.ATSSecurityRolePrivilages> ListATSSecurityRolePrivilages = base.Find(command, new GetSecurityRoleFactory(), false);
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

        public List<BEClient.ATSSecurityRolePrivilages> GetSecurityRoleByUserAndClient(Guid clientId, Guid userId,Guid languageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetSecurityRoleByUserAndClient");

                Database.AddInParameter(command, "@ClientId", DbType.Guid, clientId);
                Database.AddInParameter(command, "@UserId", DbType.Guid, userId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, languageId);

                List<BEClient.ATSSecurityRolePrivilages> ListATSSecurityRolePrivilages = base.Find(command, new GetAllATSSecurityRoleByClientAndLanguageFactory());
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
        
        internal class GetSecurityRoleFactory : IDomainObjectFactory<BEClient.ATSSecurityRolePrivilages>
        {
            public BEClient.ATSSecurityRolePrivilages Construct(IDataReader reader)
            {
                BEClient.ATSSecurityRolePrivilages objATSSecurityRolePrivilages = new BEClient.ATSSecurityRolePrivilages();

                objATSSecurityRolePrivilages.SRP_Id = HelperMethods.GetGuid(reader, "SecurityRolePrivilegeId");

                String ATSPrivilage  = HelperMethods.GetString(reader, "ATSPrivilage");
                BEClient.ATSPrivilage currentATSPrivilage = BEClient.ATSPrivilage.Other;
                Enum.TryParse<BEClient.ATSPrivilage>(ATSPrivilage, true, out currentATSPrivilage);
                objATSSecurityRolePrivilages.Privilage = currentATSPrivilage;

                String ATSSecurityRole = HelperMethods.GetString(reader, "ATSSecurityRole");
                BEClient.ATSSecurityRole currentATSSecurityRole = BEClient.ATSSecurityRole.Other;
                Enum.TryParse<BEClient.ATSSecurityRole>(ATSSecurityRole, true, out currentATSSecurityRole);
                objATSSecurityRolePrivilages.SecurityRole = currentATSSecurityRole;


                String ATSPermissionType = HelperMethods.GetString(reader, "PermissionType");
                BEClient.ATSPermissionType currentATSPermissionType = BEClient.ATSPermissionType.Other;
                Enum.TryParse<BEClient.ATSPermissionType>(ATSPermissionType, true, out currentATSPermissionType);
                objATSSecurityRolePrivilages.PermissionType = currentATSPermissionType;


                String ATSScope = HelperMethods.GetString(reader, "Scope");
                BEClient.ATSScope currentATSScope  = BEClient.ATSScope.Other;
                Enum.TryParse<BEClient.ATSScope>(ATSScope, true, out currentATSScope);
                objATSSecurityRolePrivilages.Scope= currentATSScope;


                return objATSSecurityRolePrivilages;
            }
        }

        internal class GetAllATSSecurityRoleByClientAndLanguageFactory : IDomainObjectFactory<BEClient.ATSSecurityRolePrivilages>
        {
            public BEClient.ATSSecurityRolePrivilages Construct(IDataReader reader)
            {
                BEClient.ATSSecurityRolePrivilages objATSSecurityRolePrivilages = new BEClient.ATSSecurityRolePrivilages();

                objATSSecurityRolePrivilages.SRP_Id = HelperMethods.GetGuid(reader, "ATSSecurityRoleId");

                objATSSecurityRolePrivilages.RoleLocalName = HelperMethods.GetString(reader, "LocalName");

                objATSSecurityRolePrivilages.SqNo = HelperMethods.GetInt32(reader, "SequenceNo");

                return objATSSecurityRolePrivilages;
            }
        }

        internal class GetAllATSPrevilegeByClientAndLanguageFactory : IDomainObjectFactory<BEClient.ATSSecurityRolePrivilages>
        {
            public BEClient.ATSSecurityRolePrivilages Construct(IDataReader reader)
            {
                BEClient.ATSSecurityRolePrivilages objATSSecurityRolePrivilages = new BEClient.ATSSecurityRolePrivilages();

                objATSSecurityRolePrivilages.SRP_Id = HelperMethods.GetGuid(reader, "ATSPrivilegeId");

                objATSSecurityRolePrivilages.RoleLocalName = HelperMethods.GetString(reader, "LocalName");

                return objATSSecurityRolePrivilages;
            }
        }

        internal class GetAllPrivilegeBySecurityRoleIdFactory : IDomainObjectFactory<BEClient.ATSSecurityRolePrivilages>
        {
            public BEClient.ATSSecurityRolePrivilages Construct(IDataReader reader)
            {
                BEClient.ATSSecurityRolePrivilages objATSSecurityRolePrivilages = new BEClient.ATSSecurityRolePrivilages();

                objATSSecurityRolePrivilages.SRP_Id = HelperMethods.GetGuid(reader, "ATSPrivilegeId");

                String ATSPermissionType = HelperMethods.GetString(reader, "PermissionType");
                BEClient.ATSPermissionType currentATSPermissionType = BEClient.ATSPermissionType.Other;
                Enum.TryParse<BEClient.ATSPermissionType>(ATSPermissionType, true, out currentATSPermissionType);
                objATSSecurityRolePrivilages.PermissionType = currentATSPermissionType;

                String ATSScope = HelperMethods.GetString(reader, "Scope");
                BEClient.ATSScope currentATSScope = BEClient.ATSScope.Other;
                Enum.TryParse<BEClient.ATSScope>(ATSScope, true, out currentATSScope);
                objATSSecurityRolePrivilages.Scope = currentATSScope;

                objATSSecurityRolePrivilages.IsChecked = HelperMethods.GetBoolean(reader, "IsChecked");

                return objATSSecurityRolePrivilages;
            }
        }

    }
}
