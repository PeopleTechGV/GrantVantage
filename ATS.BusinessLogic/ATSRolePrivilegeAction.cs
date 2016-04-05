using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic
{
    public class ATSRolePrivilegeAction : ActionBase
    {
        #region private data member
        private DAClient.ATSRolePrivilegeRepository _ATSRolePrivilegeRepository;
        private Guid _MyClientId { get; set; }
        #endregion
        #region Constructor

        public ATSRolePrivilegeAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _ATSRolePrivilegeRepository = new DAClient.ATSRolePrivilegeRepository(base.ConnectionString);
            _ATSRolePrivilegeRepository.CurrentUser = base.CurrentUser;
            _ATSRolePrivilegeRepository.CurrentClient = base.CurrentClient;

        }
        #endregion

        public List<BEClient.ATSRolePrivilege> GetSRPByRoleId(Guid AtsSecurityRoleId)
        {
            try
            {
                return _ATSRolePrivilegeRepository.GetSRPByRoleId(AtsSecurityRoleId);
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
                return _ATSRolePrivilegeRepository.GetAllRelativePrivilege();
            }
            catch
            {
                throw;
            }
        }
        public bool RemoveSRPByATSSecurityRoleId(Guid ATSSecurityRoleId)
        {
            try
            {
                return _ATSRolePrivilegeRepository.RemoveSRPByATSSecurityRoleId(ATSSecurityRoleId);
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
                return _ATSRolePrivilegeRepository.GetAllPrivilegeBySecurityRoles(ATSSecurityRoleId, ClientId);
            }
            catch
            {
                throw;
            }

        }


    }
}
