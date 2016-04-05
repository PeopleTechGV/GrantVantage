using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;
using BESrp = ATS.BusinessEntity.SRPEntity;

namespace ATS.BusinessLogic
{
    public class UserDivisionSecurityRoleAction : ActionBase
    {
        #region private data member
        private DAClient.UserDivisionSecurityRoleRepository _UserDivisionSecurityRoleRepository;
        private DAClient.PermissionAndScopeRepository _PermissionAndScopeRepository;

        private Guid _MyClientId { get; set; }
        private BESrp.SRPDivision _SRPDivision { get; set; }
        #endregion
        private BESrp.SRPDivision SRPDivision { get { return SRPDivision; } }
        
        #region Constructor

        public UserDivisionSecurityRoleAction(Guid ClientId, bool CreateSRPObject = false)
            : base(ClientId)
        {
            if (CreateSRPObject)
                base.CreateSRPObject(SRPCommon.CreateSRPEntityObjects.SRPCreateObj<BESrp.SRPUser>(ClientId));

            _MyClientId = ClientId;
            _UserDivisionSecurityRoleRepository = new DAClient.UserDivisionSecurityRoleRepository(base.ConnectionString);
            _UserDivisionSecurityRoleRepository.CurrentUser = base.CurrentUser;
            _UserDivisionSecurityRoleRepository.CurrentClient = base.CurrentClient;
            _PermissionAndScopeRepository = new DAClient.PermissionAndScopeRepository(base.ConnectionString);
       
            _PermissionAndScopeRepository.CurrentUser = base.CurrentUser;
            _PermissionAndScopeRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public List<BEClient.UserDivisionSecurityRole> GetAllUsersByDivision(Guid languageId)
        {
            try
            {
                string usersDivision = base.GetAllDivisionsByCurrentUser;
                List<BEClient.UserDivisionSecurityRole> UserDivisionSecurityRoleList = _UserDivisionSecurityRoleRepository.GetAllUsersByDivision(usersDivision, languageId);
                foreach (BEClient.UserDivisionSecurityRole _Division in UserDivisionSecurityRoleList)
                {
                    base.SetRoleRecordWise(_Division, _Division.DivisionId);
                }

                return UserDivisionSecurityRoleList;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Guid InsertUser(BEClient.UserDivisionSecurityRole objUserDivisionSecurityRole)
        {
            Guid userId = Guid.Empty;
            Guid userDetailId = Guid.Empty;
            Guid userDivisionPermissionId = Guid.Empty;
            Guid UserSecurityRoleId = Guid.Empty;

            DAClient.UserRepository objUserRepository = new DAClient.UserRepository(base.ConnectionString);

            try
            {
                //Add user
                objUserRepository.BeginTransaction();

                userId = objUserRepository.SignupUser(objUserDivisionSecurityRole.objUser.Username, objUserDivisionSecurityRole.objUser.Password, _MyClientId, objUserDivisionSecurityRole.DivisionId,base.CurrentClient.CurrentLanguageId, true);

                if (userId.Equals(Guid.Empty))
                {
                    objUserRepository.RollbackTransaction();
                    return userId;
                }
                //End user

                //Start User Detail
                DAClient.UserDetailsRepository objUserDetailsRepository = new DAClient.UserDetailsRepository(base.ConnectionString);
                objUserDetailsRepository.Transaction = objUserRepository.Transaction;

                objUserDivisionSecurityRole.objUserDetail.UserId = userId;
                objUserDetailsRepository.CurrentUser = new BEClient.User();
                objUserDetailsRepository.CurrentUser = base.CurrentUser;
                userDetailId = objUserDetailsRepository.AddUserDetails(objUserDivisionSecurityRole.objUserDetail);

                if (userDetailId.Equals(Guid.Empty))
                {
                    objUserRepository.RollbackTransaction();
                    return userId;
                }
                //End user Detail

                //Start Add Adhoc divisions
                //DAClient.UserDivisionPermissionRepository objUserDivisionPermissionRepository = new DAClient.UserDivisionPermissionRepository(base.ConnectionString);
                //if (objUserDivisionSecurityRole.objDivision != null)
                //{
                //    objUserDivisionPermissionRepository.Transaction = objUserRepository.Transaction;

                //    BEClient.UserDivisionPermission objUserDivisionPermission = new BEClient.UserDivisionPermission();
                //    objUserDivisionPermission.ClientId = _MyClientId;
                //    objUserDivisionPermission.UserId = userId;
                //    objUserDivisionPermissionRepository.CurrentUser = new BEClient.User();
                //    objUserDivisionPermissionRepository.CurrentUser = base.CurrentUser;
                //    objUserDivisionPermission.IsDelete = false;

                //    for (int v = 0; v < objUserDivisionSecurityRole.objDivision.DivisionId.Count(); v++)
                //    {
                //        objUserDivisionPermission.DivisionId = new Guid(objUserDivisionSecurityRole.objDivision.DivisionId[v]);
                //        userDivisionPermissionId = objUserDivisionPermissionRepository.Add(objUserDivisionPermission);
                //        if (userDivisionPermissionId.Equals(Guid.Empty))
                //        {
                //            objUserRepository.RollbackTransaction();
                //            return userId;
                //        }
                //    }
                //}
                //End Add Adhoc divisions 

                //Start User Security Role
                DAClient.UserSecurityRoleRepository objUserSecurityRoleRepository = new DAClient.UserSecurityRoleRepository(base.ConnectionString);
                if (objUserDivisionSecurityRole.objATSSecurityRolePrivilagesMaster != null)
                {
                    objUserSecurityRoleRepository.Transaction = objUserRepository.Transaction;

                    BEClient.UserSecurityRole objUserSecurityRole = new BEClient.UserSecurityRole();
                    objUserSecurityRole.ClientId = _MyClientId;
                    objUserSecurityRole.UserId = userId;
                    objUserSecurityRole.IsDelete = false;
                    objUserSecurityRoleRepository.CurrentUser = new BEClient.User();
                    objUserSecurityRoleRepository.CurrentUser = base.CurrentUser;

                    for (int v = 0; v < objUserDivisionSecurityRole.objATSSecurityRolePrivilagesMaster.SRP_Ids.Count(); v++)
                    {
                        objUserSecurityRole.AtsSecurityRoleId = new Guid(objUserDivisionSecurityRole.objATSSecurityRolePrivilagesMaster.SRP_Ids[v]);
                        UserSecurityRoleId = objUserSecurityRoleRepository.AddUserSecurityRole(objUserSecurityRole);

                        if (UserSecurityRoleId.Equals(Guid.Empty))
                        {
                            objUserRepository.RollbackTransaction();
                            return userId;
                        }
                    }
                }
                //End User Security role

                objUserRepository.CommitTransaction();

                return userId;
            }
            catch (Exception ex)
            {
                objUserRepository.RollbackTransaction();
                if(ex.Message == "104")
                {
                    throw new Exception("User Already Exists.");
                }
                else if (ex.Message == "105")
                {
                    throw new Exception("User is Registered but not active.");

                }
                else
                {
                    throw;
                }
            }
        }

        public bool UpdateUser(BEClient.UserDivisionSecurityRole objUserDivisionSecurityRole, List<ATS.BusinessEntity.PrivilegeAndPermissionAndScope> objGroupListOfSameRemarkQty)
        {
            bool userResult = false;
            bool userDetail = false;
            Guid userDivisionPermissionId = Guid.Empty;
            Guid UserSecurityRoleId = Guid.Empty;

            DAClient.UserRepository objUserRepository = new DAClient.UserRepository(base.ConnectionString);
            try
            {
                //Add user
                objUserRepository.BeginTransaction();
                objUserDivisionSecurityRole.objUser.UserId = (Guid)objUserDivisionSecurityRole.objUserDetail.UserId;
                objUserDivisionSecurityRole.objUser.DivisionId = objUserDivisionSecurityRole.DivisionId;
                objUserRepository.CurrentUser = new BEClient.User();
                objUserRepository.CurrentUser = base.CurrentUser;
                userResult = objUserRepository.UpdateUser(objUserDivisionSecurityRole.objUser);

                if (!userResult)
                {
                    objUserRepository.RollbackTransaction();
                    return userResult;
                }
                //End user

                //Start User Detail
                DAClient.UserDetailsRepository objUserDetailsRepository = new DAClient.UserDetailsRepository(base.ConnectionString);
                objUserDetailsRepository.Transaction = objUserRepository.Transaction;
                objUserDetailsRepository.CurrentUser = new BEClient.User();
                objUserDetailsRepository.CurrentUser = base.CurrentUser;
                userDetail = objUserDetailsRepository.UpdateUserDetails(objUserDivisionSecurityRole.objUserDetail);

                if (!userDetail)
                {
                    objUserRepository.RollbackTransaction();
                    return userResult;
                }
                //End user Detail

                //Start Add Adhoc divisions
                //DAClient.UserDivisionPermissionRepository objUserDivisionPermissionRepository = new DAClient.UserDivisionPermissionRepository(base.ConnectionString);
                //var deleteDivisionResult = objUserDivisionPermissionRepository.DeleteDivisionByUserAndClient(_MyClientId, objUserDivisionSecurityRole.objUser.UserId);
                //if (deleteDivisionResult)
                //{
                //    if (objUserDivisionSecurityRole.objDivision != null)
                //    {
                //        objUserDivisionPermissionRepository.Transaction = objUserRepository.Transaction;

                //        BEClient.UserDivisionPermission objUserDivisionPermission = new BEClient.UserDivisionPermission();
                //        objUserDivisionPermission.ClientId = _MyClientId;
                //        objUserDivisionPermission.UserId = (Guid)objUserDivisionSecurityRole.objUserDetail.UserId;
                //        objUserDivisionPermissionRepository.CurrentUser = new BEClient.User();
                //        objUserDivisionPermissionRepository.CurrentUser = base.CurrentUser;
                //        objUserDivisionPermission.IsDelete = false;

                //        for (int v = 0; v < objUserDivisionSecurityRole.objDivision.DivisionId.Count(); v++)
                //        {
                //            objUserDivisionPermission.DivisionId = new Guid(objUserDivisionSecurityRole.objDivision.DivisionId[v]);
                //            userDivisionPermissionId = objUserDivisionPermissionRepository.Add(objUserDivisionPermission);
                //            if (userDivisionPermissionId.Equals(Guid.Empty))
                //            {
                //                objUserRepository.RollbackTransaction();
                //                return userResult;
                //            }
                //        }
                //    }
                //}
                ////End Add Adhoc divisions 

                //Start User Security Role
                DAClient.UserSecurityRoleRepository objUserSecurityRoleRepository = new DAClient.UserSecurityRoleRepository(base.ConnectionString);
                var deleteRoleResult = objUserSecurityRoleRepository.DeleteRoleByUserAndClient(_MyClientId, objUserDivisionSecurityRole.objUser.UserId);
                if (deleteRoleResult)
                {
                    if (objUserDivisionSecurityRole.objATSSecurityRolePrivilagesMaster != null)
                    {
                        objUserSecurityRoleRepository.Transaction = objUserRepository.Transaction;

                        BEClient.UserSecurityRole objUserSecurityRole = new BEClient.UserSecurityRole();
                        objUserSecurityRole.ClientId = _MyClientId;
                        objUserSecurityRole.UserId = (Guid)objUserDivisionSecurityRole.objUserDetail.UserId;
                        objUserSecurityRole.IsDelete = false;
                        objUserSecurityRoleRepository.CurrentUser = new BEClient.User();
                        objUserSecurityRoleRepository.CurrentUser = base.CurrentUser;

                        for (int v = 0; v < objUserDivisionSecurityRole.objATSSecurityRolePrivilagesMaster.SRP_Ids.Count(); v++)
                        {
                            objUserSecurityRole.AtsSecurityRoleId = new Guid(objUserDivisionSecurityRole.objATSSecurityRolePrivilagesMaster.SRP_Ids[v]);
                            UserSecurityRoleId = objUserSecurityRoleRepository.AddUserSecurityRole(objUserSecurityRole);

                            if (UserSecurityRoleId.Equals(Guid.Empty))
                            {
                                objUserRepository.RollbackTransaction();
                                return userResult;
                            }
                        }
                    }
                }
                //Adhoc permision --start--
                Guid adHocPrivilegeId = Guid.Empty;
                _PermissionAndScopeRepository.Transaction = objUserRepository.Transaction;
                foreach (var v in objGroupListOfSameRemarkQty)
                {
                    Guid privilegeId = v.PrivilegeId;

                    foreach (var c in v.PermissionList)
                    {
                        String permission = c.Permission == "C" ? BEClient.Common.PermissionType.Create : c.Permission == "V" ? BEClient.Common.PermissionType.View : c.Permission == "M" ? BEClient.Common.PermissionType.Modify : c.Permission == "D" ? BEClient.Common.PermissionType.Delete : string.Empty;

                        foreach (var h in c.ScopeList)
                        {
                            String scope = h.Scope;
                            adHocPrivilegeId = _PermissionAndScopeRepository.InsertAdHocPrivilege(privilegeId, permission, scope, _MyClientId, objUserDivisionSecurityRole.objUser.UserId);
                            if (adHocPrivilegeId == null && adHocPrivilegeId.Equals(Guid.Empty))
                            {
                                objUserRepository.RollbackTransaction();
                                return false;
                            }
                        }
                    }
                }
                //EndAdhoc privilege
                //End User Security role

                objUserRepository.CommitTransaction();

                return userResult;
            }
            catch (Exception)
            {
                objUserRepository.RollbackTransaction();

                throw;
            }
        }

        public bool Delete(string recordid)
        {
            DAClient.UserRepository objUserRepository = new DAClient.UserRepository(base.ConnectionString);
            try
            {
                objUserRepository.BeginTransaction();
                bool Result = objUserRepository.Delete(recordid);
                if (Result)
                {
                    objUserRepository.CommitTransaction();
                    return Result;
                }
                else
                {
                    objUserRepository.RollbackTransaction();
                    return Result;
                }


            }
            catch
            {
                objUserRepository.RollbackTransaction();
                throw;

            }
        }

        public BEClient.UserDivisionSecurityRole GetRecordById(Guid RecordId)
        {
            BEClient.UserDivisionSecurityRole _objUserDivisionSecurityRole = new BEClient.UserDivisionSecurityRole();

            //Create action object for Edit only
            UserDetailsAction objUserDetailsAction = new UserDetailsAction(_MyClientId);
            UserAction objUserAction = new UserAction(_MyClientId);
            //END

            _objUserDivisionSecurityRole.objUserDetail = objUserDetailsAction.GetUserDetailsByUserId((Guid)RecordId);
            _objUserDivisionSecurityRole.objUser = objUserAction.GetRecordById((Guid)RecordId);
            base.SetRoleRecordWise(_objUserDivisionSecurityRole, _objUserDivisionSecurityRole.objUser.DivisionId);
            return _objUserDivisionSecurityRole;

        }
    }
}
