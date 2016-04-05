using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;
using BESrp = ATS.BusinessEntity.SRPEntity;
namespace ATS.BusinessLogic
{
    public class ATSSecurityRoleAction : ActionBase
    {
        #region private data member
        private DAClient.ATSSecurityRoleRepository _ATSSecurityRoleRepository;
        private Guid _MyClientId { get; set; }
        private BESrp.SRPVacancy _SRPVacancy { get; set; }
        #endregion

        #region Constructor

        public ATSSecurityRoleAction(Guid ClientId, bool CreateSRPObject = false)
            : base(ClientId)
        {
            if (CreateSRPObject)
                base.CreateSRPObject(SRPCommon.CreateSRPEntityObjects.SRPCreateObj<BESrp.SRPATSSecurityRole>(ClientId));

            _MyClientId = ClientId;
            _ATSSecurityRoleRepository = new DAClient.ATSSecurityRoleRepository(base.ConnectionString);
            _ATSSecurityRoleRepository.CurrentUser = base.CurrentUser;
            _ATSSecurityRoleRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public List<BEClient.ATSSecurityRolePrivilages> GetAllPrivilegeBySecurityRoleId(string securityRoleId)
        {
            try
            {
                return _ATSSecurityRoleRepository.GetAllPrivilegeBySecurityRoleId(_MyClientId, securityRoleId);
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
                return _ATSSecurityRoleRepository.GetUserSecurityRole(privilage, securityRole);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.ATSSecurityRolePrivilages> GetAllATSSecurityRoleByClientAndLanguage(Guid languageId, int sequenceNo)
        {
            try
            {
                return _ATSSecurityRoleRepository.GetAllATSSecurityRoleByClientAndLanguage(_MyClientId, languageId,sequenceNo);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.ATSSecurityRolePrivilages> GetSecurityRoleByUserAndClient(Guid userId,Guid languageId)
        {
            try
            {
                return _ATSSecurityRoleRepository.GetSecurityRoleByUserAndClient(_MyClientId, userId, languageId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.ATSSecurityRolePrivilages> GetAllATSPrevilegeByClientAndLanguage(Guid languageId)
        {
            try
            {
                return _ATSSecurityRoleRepository.GetAllATSPrevilegeByClientAndLanguage(_MyClientId, languageId);
            }
            catch
            {
                throw;
            }
        }
     
    }
}
