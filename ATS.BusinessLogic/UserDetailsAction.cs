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
    public class UserDetailsAction : ActionBase
    {
        #region private data member
        private DAClient.UserDetailsRepository _UserDetailsRepository;
        private Guid _MyClientId { get; set; }
        #endregion
        #region Constructor

        public UserDetailsAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _UserDetailsRepository = new DAClient.UserDetailsRepository(base.ConnectionString);
            _UserDetailsRepository.CurrentUser = base.CurrentUser;
            _UserDetailsRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public BEClient.UserDetails GetUserDetailsByUserId(Guid UserId)
        {
            try
            {
                BEClient.UserDetails _objUserDetails = _UserDetailsRepository.GetUserByUserId(UserId);

                return _objUserDetails;

            }
            catch
            {
                throw;
            }
        }
      



        #region CRUD

        public Guid AddAnonymousUserDetails(BEClient.UserDetails pUserDetails)
        {
            try
            {
                _UserDetailsRepository.BeginTransaction();
                Guid AnonymousUserId = (Guid)pUserDetails.UserId;
                _UserDetailsRepository.CurrentClient = base.CurrentClient;
                if (_UserDetailsRepository.CurrentUser == null)
                {

                    _UserDetailsRepository.CurrentUser = new BEClient.User() { UserId = AnonymousUserId };
                }


                Guid AnonymousUserDetailsId = _UserDetailsRepository.AddUserDetails(pUserDetails);

                _UserDetailsRepository.CommitTransaction();
                return AnonymousUserDetailsId;
            }
            catch
            {
                _UserDetailsRepository.RollbackTransaction();
                throw;
            }


        }

        public bool UpdateUserDetails(BEClient.UserDetails objuserdetails)
        {
            try
            {

                _UserDetailsRepository.BeginTransaction();
                if (_UserDetailsRepository.CurrentUser == null)
                {

                    _UserDetailsRepository.CurrentUser = new BEClient.User() { UserId = (Guid)objuserdetails.UserId };
                }
                bool IsUpdated = _UserDetailsRepository.UpdateUserDetails(objuserdetails);
                if (IsUpdated)
                {
                    _UserDetailsRepository.CommitTransaction();
                    return IsUpdated;
                }
                else
                {
                    _UserDetailsRepository.RollbackTransaction();
                    return IsUpdated;

                }
            }
            catch
            {
                _UserDetailsRepository.RollbackTransaction();
                throw;
            }
        }

        public bool UpdateEmergencyContactDetails(BEClient.UserDetails objuserdetails)
        {
            try
            {

                _UserDetailsRepository.BeginTransaction();
                if (_UserDetailsRepository.CurrentUser == null)
                {

                    _UserDetailsRepository.CurrentUser = new BEClient.User() { UserId = (Guid)objuserdetails.UserId };
                }
                bool IsUpdated = _UserDetailsRepository.UpdateEmergencyDetails(objuserdetails);
                if (IsUpdated)
                {
                    _UserDetailsRepository.CommitTransaction();
                    return IsUpdated;
                }
                else
                {
                    _UserDetailsRepository.RollbackTransaction();
                    return IsUpdated;

                }
            }
            catch
            {
                _UserDetailsRepository.RollbackTransaction();
                throw;
            }
        }

        public List<BEClient.UserDetails> GetInterviewerByVacRndId(Guid VacRndId, Guid AppId)
        {
            try
            {
                return _UserDetailsRepository.GetInterviewerByVacRndId(VacRndId, AppId);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.UserDetails GetUserDetailsByAppId(Guid ApplicationId)
        {
            try
            {
                return _UserDetailsRepository.GetUserDetailsByAppId(ApplicationId);
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}