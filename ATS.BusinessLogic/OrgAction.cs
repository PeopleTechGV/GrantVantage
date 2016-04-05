using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;
using System.Data.Common;
using System.Data;
using BESrp = ATS.BusinessEntity.SRPEntity;
namespace ATS.BusinessLogic
{
    public class OrgAction:ActionBase
    {  //CR-2
        #region private data member
        private DAClient.OrgDetailsRepository _OrgDetailsRepository;
        private Guid _MyClientId { get; set; }
        #endregion
        #region Constructor

        public OrgAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _OrgDetailsRepository = new DAClient.OrgDetailsRepository(base.ConnectionString);
            _OrgDetailsRepository.CurrentUser = base.CurrentUser;
            _OrgDetailsRepository.CurrentClient = base.CurrentClient;
        }
        #endregion
        public BEClient.Organization GetUserOrgDetailsByUserId(Guid UserId)
        {
            try
            {
                BEClient.Organization _objorgUserDetails = _OrgDetailsRepository.GetUserOrgByUserId(UserId);

                return _objorgUserDetails;

            }
            catch
            {
                throw;
            }
        }
        public bool UpdateUserorgDetails(BEClient.Organization objuserdetails)
        {
            try
            {

                _OrgDetailsRepository.BeginTransaction();
                if (_OrgDetailsRepository.CurrentUser == null)
                {

                    _OrgDetailsRepository.CurrentUser = new BEClient.User() { UserId = (Guid)objuserdetails.UserId };
                }
                bool IsUpdated = _OrgDetailsRepository.UpdateUserorgDetails(objuserdetails);
                if (IsUpdated)
                {
                    _OrgDetailsRepository.CommitTransaction();
                    return IsUpdated;
                }
                else
                {
                    _OrgDetailsRepository.RollbackTransaction();
                    return IsUpdated;

                }
            }
            catch
            {
                _OrgDetailsRepository.RollbackTransaction();
                throw;
            }
        }
    }
}
