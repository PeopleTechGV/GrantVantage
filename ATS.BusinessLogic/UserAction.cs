using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;
using BESrp = ATS.BusinessEntity.SRPEntity;

namespace ATS.BusinessLogic
{
    public class UserAction : ActionBase
    {
        #region private data member
        private DAClient.UserRepository _UserRepository;
        private DAClient.DivisionRepository _DivisionRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor
        public UserAction(Guid ClientId, bool CreateSRPObject = false)
            : base(ClientId)
        {
            if (CreateSRPObject)
                base.CreateSRPObject(SRPCommon.CreateSRPEntityObjects.SRPCreateObj<BESrp.SRPUser>(ClientId));
            _MyClientId = ClientId;
            _UserRepository = new DAClient.UserRepository(base.ConnectionString);
            _DivisionRepository = new DAClient.DivisionRepository(base.ConnectionString);
        }
        #endregion

        #region Validate User Method
        public Guid ValidateUserByClient(String Username, String Password)
        {
            try
            {
                return _UserRepository.ValidateUserByClient(Username, Password, _MyClientId);
            }
            catch
            {

                throw;
            }
        }
        #endregion

        #region SignUp Users
        public void SignupUserOrg(string orgname, Guid userid, Guid clientId)
        {
            DAClient.UserSecurityRoleRepository _UserSecurityRoleRepository = new DAClient.UserSecurityRoleRepository(base.ConnectionString);
            try
            {
                _UserRepository.BeginTransaction();

                _UserRepository.SignupUserOrg(orgname, userid, clientId);

                //_UserSecurityRoleRepository.Transaction = _UserRepository.Transaction;
                //_UserSecurityRoleRepository.CurrentUser = _UserRepository.CurrentUser;
                //_UserSecurityRoleRepository.CurrentClient = _UserRepository.CurrentClient;

                //foreach (BEClient.ATSSecurityRole SecurityRole in securityRoles)
                //{
                //    BEClient.UserSecurityRole UserSecurityrole = new BEClient.UserSecurityRole();
                //    UserSecurityrole.ClientId = clientId;
                //    UserSecurityrole.AtsSecurityRole = SecurityRole;
                //    UserSecurityrole.UserId = newUserId;
                //    UserSecurityrole.IsDelete = false;
                //    _UserSecurityRoleRepository.AddUserSecurityRole(UserSecurityrole);
                //}
                _UserRepository.CommitTransaction();
                
            }
            catch
            {
                _UserRepository.RollbackTransaction();
                throw;
            }
        }
        public Guid SignupUser(string username, string password, Guid clientId, Guid divisionId, BEClient.ATSSecurityRole[] securityRoles, Guid languageId, Boolean active)
        {
            DAClient.UserSecurityRoleRepository _UserSecurityRoleRepository = new DAClient.UserSecurityRoleRepository(base.ConnectionString);
            try
            {
                _UserRepository.BeginTransaction();


                Guid newUserId = _UserRepository.SignupUser(username, password, clientId, divisionId, languageId, active);

                _UserSecurityRoleRepository.Transaction = _UserRepository.Transaction;
                _UserSecurityRoleRepository.CurrentUser = _UserRepository.CurrentUser;
                _UserSecurityRoleRepository.CurrentClient = _UserRepository.CurrentClient;

                foreach (BEClient.ATSSecurityRole SecurityRole in securityRoles)
                {
                    BEClient.UserSecurityRole UserSecurityrole = new BEClient.UserSecurityRole();
                    UserSecurityrole.ClientId = clientId;
                    UserSecurityrole.AtsSecurityRole = SecurityRole;
                    UserSecurityrole.UserId = newUserId;
                    UserSecurityrole.IsDelete = false;
                    _UserSecurityRoleRepository.AddUserSecurityRole(UserSecurityrole);
                }
                _UserRepository.CommitTransaction();
                return newUserId;
            }
            catch
            {
                _UserRepository.RollbackTransaction();
                throw;
            }
        }
        #endregion
        #region CRUD
        public BEClient.User GetUserForSession(Guid UserId)
        {
            try
            {
                BEClient.User objUser = null;
                objUser = GetRecordById(UserId);
                if (objUser != null)
                {
                    DivisionAction objDivisionAction = new DivisionAction(_MyClientId);
                    UserSecurityRoleAction objUserSecurityRoleAction = new UserSecurityRoleAction(_MyClientId);
                    objUser.AddHocDivision = objDivisionAction.GetUserDivisionPermissionByUser(UserId);
                    if (objUser.AddHocDivision == null)
                        objUser.AddHocDivision = new List<BEClient.Division>();

                    //Add Current division in AddHoc
                    if (objUser.DivisionId != null && !objUser.DivisionId.Equals(Guid.Empty))
                    {
                        objUser.AddHocDivision.Add(objDivisionAction.GetRecordByRecordId(objUser.DivisionId));

                    }
                    //Add Current UserSecurityRole in session
                    objUser.SecurityRoles = objUserSecurityRoleAction.GetUserSecurityRoleByUserId(UserId);

                    objUser.SisterDivisionUserId = _DivisionRepository.GetSisterDivisionIdsByUserId(UserId);

                    if (objUser.SisterDivisionUserId != null && objUser.SisterDivisionUserId.Count > 0)
                    {
                        objUser.SisterDivisionUserId = objUser.SisterDivisionUserId.Distinct().ToList<Guid>();
                        foreach (var v in objUser.AddHocDivision)
                        {
                            objUser.SisterDivisionUserId.Remove(v.DivisionId);
                        }

                    }

                    objUser.ChildDivisionUserId = _DivisionRepository.GetChildDivisionIdsByUserId(UserId);
                    if (objUser.ChildDivisionUserId != null && objUser.ChildDivisionUserId.Count > 0)
                    {
                        objUser.ChildDivisionUserId = objUser.ChildDivisionUserId.Distinct().ToList<Guid>();
                        foreach (var v in objUser.AddHocDivision)
                        {
                            objUser.ChildDivisionUserId.Remove(v.DivisionId);
                        }
                    }
                }
                return objUser;

            }
            catch
            {

                throw;
            }
        }


        public BEClient.User GetRecordById(Guid recordId)
        {
            try
            {
                return _UserRepository.GetRecordById(recordId);
            }
            catch
            {
                throw;
            }

        }
        public BEClient.User GetRecordByIdWithoutStatus(Guid recordId)
        {
            try
            {
                return _UserRepository.GetRecordById(recordId);
            }
            catch
            {
                throw;
            }

        }
        #endregion

        public bool CompleteRegistration(BEClient.User user, bool pIsActive)
        {
            try
            {
                Guid _UserId = user.UserId;
                if (_UserRepository.CurrentUser == null)
                {

                    _UserRepository.CurrentUser = new BEClient.User() { UserId = _UserId };
                }

                return _UserRepository.CompleteRegistration(user, pIsActive);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public BEClient.CandidateProfile GetUserByUserId(Guid UserId)
        {
            try
            {
                return null;
            }
            catch
            {
                throw;
            }
        }

        public BEClient.User GetUserIdByUserName(string UserName)
        {
            try
            {
                return _UserRepository.GetUserIdByUserName(UserName);
            }
            catch
            {
                throw;

            }
        }

        public BEClient.User ValidateUserName(string pUserName, Guid pClientId)
        {
            try
            {
                return _UserRepository.ValidateUserName(pUserName, pClientId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ResetPassword(Guid pUserId, string pPassword)
        {
            try
            {
                if (_UserRepository.CurrentUser == null)
                {

                    _UserRepository.CurrentUser = new BEClient.User() { UserId = pUserId };
                }
                return _UserRepository.ResetPassword(pUserId, pPassword);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool UpdateUser(BEClient.User objUser)
        {
            try
            {
                return _UserRepository.UpdateUser(objUser);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public BEClient.User GetSSOUser(Guid UserId, Guid SSOToken)
        {
            try
            {
                return _UserRepository.GetSSOUser(UserId, SSOToken);
            }
            catch
            {
                throw;
            }

        }
        public bool ResetSSoToken(Guid UserId)
        {
            try
            {
                return _UserRepository.ResetSSOToken(UserId);
            }
            catch
            {
                throw;
            }

        }

        public bool ChangeUserPassword(Guid UserId, string Password, string newpassword)
        {
            try
            {
                return _UserRepository.ChangeUserPassword(UserId, Password,newpassword);
            }
            catch
            {
                throw;
            }
        }
           public List<BEClient.User> GetAllUOnboardingUser(Guid DivisionId)
        {
               try
               {
                   return _UserRepository.GetAllUOnboardingUser(DivisionId);
               }
               catch
               {
                   throw;
               }
        }
        //public bool Delete(Guid recordid, Guid languageid)
        //{
        //    try
        //    {
        //        _UserRepository.BeginTransaction();
        //        bool Result = _UserRepository.Delete(recordid, languageid);
        //        if (Result)
        //        {
        //            _UserRepository.CommitTransaction();
        //            return Result;
        //        }
        //        else
        //        {
        //            _UserRepository.RollbackTransaction();
        //            return Result;
        //        }


        //    }
        //    catch
        //    {
        //        _UserRepository.RollbackTransaction();
        //        throw;

        //    }
        //}

        public List<BEClient.User> GetAllUser()
        {
            try
            {
                string usersDivision = base.GetAllDivisionsByCurrentUser;
                return _UserRepository.GetAllUser(_MyClientId, usersDivision);


            }
            catch
            {
                throw;
            }
        }
        public List<BEClient.User> GetAllReviewer(Guid VacRndId, Guid? UserId)
        {
            try
            {
                string usersDivision = base.GetAllDivisionsByCurrentUser;
                if (string.IsNullOrEmpty(usersDivision))
                {
                    usersDivision = null;
                }
                return _UserRepository.GetAllReviewers(VacRndId, usersDivision, UserId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.User> GetAllReviewersForTemplate(Guid VacRndId, Guid? UserId)
        {
            try
            {
                string usersDivision = base.GetAllDivisionsByCurrentUser;
                if (string.IsNullOrEmpty(usersDivision))
                {
                    usersDivision = null;
                }
                return _UserRepository.GetAllReviewersForTemplate(VacRndId, usersDivision, UserId);
            }
            catch
            {
                throw;
            }
        }

        public string GetDivisionByUserId(Guid UserId)
        {
            try
            {
                return _UserRepository.GetDivisionByUserId(UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<BEClient.User> GetAllUserByDivisionId(Guid DivisionId)
        {
            try
            {
                return _UserRepository.GetAllUsersByDivisionId(DivisionId);
            }
            catch
            {
                throw;
            }

        }
        public List<BEClient.User> GetAllEmployees()
        {
            try
            {
                return _UserRepository.GetAllEmployees();
            }
            catch
            {
                throw;
            }
        }
        public List<BEClient.User> GetLocationManagers()
        {
            try
            {
                return _UserRepository.GetLocationManagers();
            }
            catch
            {
                throw;
            }
        }
        public BEClient.User GetUserByEmailId(string EmailId)
        {
            try
            {
                return _UserRepository.GetUserByEmailId(EmailId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateProfileImage(string ImageName, Guid UserId)
        {
            try
            {
                bool IsUpdated = _UserRepository.UpdateProfileImage(ImageName, UserId);
                return IsUpdated;
            }
            catch
            {
                return false;
            }
        }


        public bool DeactivateCandidate(String UserName)
        {
            try
            {
                return _UserRepository.DeactivateCandidate(UserName);
            }
            catch
            {
                throw;
            }
        }

        public bool ValidateNewEmployee(String UserName)
        {
            try
            {
                return _UserRepository.ValidateNewEmployee(UserName);
            }
            catch
            {
                throw;
            }
        }
       

    }
}
