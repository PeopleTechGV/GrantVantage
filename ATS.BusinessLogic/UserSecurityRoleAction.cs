using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;
using BESrp = ATS.BusinessEntity.SRPEntity;
namespace ATS.BusinessLogic
{
    public class UserSecurityRoleAction : ActionBase
    {
        #region private data member
        private DAClient.UserSecurityRoleRepository _UserSecurityRoleRepository;
        private DAClient.PermissionAndScopeRepository _PermissionAndScopeRepository;
        private DAClient.UserDetailsRepository _UserDetailsRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor

        public UserSecurityRoleAction(Guid ClientId, bool CreateSRPObject = false)
            : base(ClientId)
        {
            if (CreateSRPObject)
                base.CreateSRPObject(SRPCommon.CreateSRPEntityObjects.SRPCreateObj<BESrp.SRPUserSecurityRole>(ClientId));

            _MyClientId = ClientId;
            _UserSecurityRoleRepository = new DAClient.UserSecurityRoleRepository(base.ConnectionString);
            _PermissionAndScopeRepository = new DAClient.PermissionAndScopeRepository(base.ConnectionString);
            _UserDetailsRepository = new DAClient.UserDetailsRepository(base.ConnectionString);
            _PermissionAndScopeRepository.CurrentUser = base.CurrentUser;
            _PermissionAndScopeRepository.CurrentClient = base.CurrentClient;
            _UserSecurityRoleRepository.CurrentUser = base.CurrentUser;
            _UserSecurityRoleRepository.CurrentClient = base.CurrentClient;
            _UserDetailsRepository.CurrentUser = base.CurrentUser;
            _UserDetailsRepository.CurrentClient = base.CurrentClient;
        }
        public Guid AddUserSecurityRole(BEClient.UserSecurityRole userSecurityRole)
        {
            try
            {
                return _UserSecurityRoleRepository.AddUserSecurityRole(userSecurityRole);
            }
            catch
            {
                throw;
            }
        }
        public List<BEClient.UserSecurityRole> GetUserSecurityRoleByUserId(Guid UserId)
        {
            try
            {
                return _UserSecurityRoleRepository.GetUserSecurityRoleByUserId(UserId);
            }
            catch
            {
                throw;
            }
        }

        public Guid InsertSecurityRoleByUser(string[] objATSSecurityRolePrivilegesId, List<ATS.BusinessEntity.PrivilegeAndPermissionAndScope> objGroupListOfSameRemarkQty, Guid userId, BEClient.UserDetails objUserDetails)
        {
            try
            {
                _UserSecurityRoleRepository.BeginTransaction();

                Guid userSecurityRoleId = Guid.Empty;

                _UserDetailsRepository.Transaction = _UserSecurityRoleRepository.Transaction;
                bool result = _UserDetailsRepository.UpdateUserDetails(objUserDetails);

                if (result)
                {
                    var deleteRoleResult = _UserSecurityRoleRepository.DeleteRoleByUserAndClient(_MyClientId, userId);
                    if (deleteRoleResult)
                    {
                        foreach (var v in objATSSecurityRolePrivilegesId)
                        {
                            userSecurityRoleId = _UserSecurityRoleRepository.InsertSecurityRoleByUser(new Guid(v), _MyClientId, userId);
                            if (userSecurityRoleId == null && userSecurityRoleId.Equals(Guid.Empty))
                            {
                                _UserSecurityRoleRepository.RollbackTransaction();
                                return Guid.Empty;
                            }
                        }
                        Guid adHocPrivilegeId = Guid.Empty;
                        _PermissionAndScopeRepository.Transaction = _UserSecurityRoleRepository.Transaction;
                        foreach (var v in objGroupListOfSameRemarkQty)
                        {
                            Guid privilegeId = v.PrivilegeId;

                            foreach (var c in v.PermissionList)
                            {
                                String permission = c.Permission == "C" ? BEClient.Common.PermissionType.Create : c.Permission == "V" ? BEClient.Common.PermissionType.View : c.Permission == "M" ? BEClient.Common.PermissionType.Modify : c.Permission == "D" ? BEClient.Common.PermissionType.Delete : string.Empty;

                                foreach (var h in c.ScopeList)
                                {
                                    String scope = h.Scope;
                                    adHocPrivilegeId = _PermissionAndScopeRepository.InsertAdHocPrivilege(privilegeId, permission, scope, _MyClientId, userId);
                                    if (adHocPrivilegeId == null && adHocPrivilegeId.Equals(Guid.Empty))
                                    {
                                        _UserSecurityRoleRepository.RollbackTransaction();
                                        return Guid.Empty;
                                    }
                                }
                            }
                        }
                        _UserSecurityRoleRepository.CommitTransaction();
                    }
                    else
                    {
                        _UserSecurityRoleRepository.RollbackTransaction();
                    }
                }
                else
                {
                    _UserSecurityRoleRepository.RollbackTransaction();
                }
                return userSecurityRoleId;
            }
            catch
            {
                _UserSecurityRoleRepository.RollbackTransaction();
                throw;
            }
        }
        #endregion
    }
}
