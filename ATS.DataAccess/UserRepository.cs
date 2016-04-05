using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using BEClient = ATS.BusinessEntity;

namespace ATS.DataAccess
{
    public class UserRepository : Repository<BEClient.User>
    {
        public UserRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }
        public Guid ValidateUserByClient(String Username, String Password, Guid ClientId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("ValidateUserByClient");

                Database.AddInParameter(command, "@Username", DbType.String, Username);
                Database.AddInParameter(command, "@Password", DbType.String, Password);
                Database.AddInParameter(command, "@ClientId", DbType.Guid, ClientId);

                BEClient.User validUser = base.FindOne(command, new GetValideLogin());
                if (validUser != null)
                {
                    return validUser.UserId;
                }
                return Guid.Empty;
            }
            catch
            {
                throw;
            }
        }

        public bool ResetPassword(Guid pUserId, string pPassword)
        {
            try
            {
                string strFieldValues = string.Empty;
                strFieldValues += string.Format("[{0}] = '{1}'", "Password", pPassword);

                return QuickUpdateContact(pUserId, strFieldValues);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Guid SignupUser(string pUsername, string pPassword, Guid pClientId, Guid divisionId, Guid languageId, Boolean active)
        {
            bool LocalTrasaction = false;
            try
            {
                if (base.Transaction == null)
                {
                    base.BeginTransaction();
                    LocalTrasaction = true;
                }
                Guid reUserId = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("SignupUser");

                Database.AddInParameter(command, "@Username", DbType.String, pUsername);

                Database.AddInParameter(command, "@LanguageId", DbType.Guid, languageId);

                Database.AddInParameter(command, "@ClientId", DbType.Guid, pClientId);

                Database.AddInParameter(command, "@Password", DbType.String, pPassword);

                if (!divisionId.Equals(Guid.Empty))
                    Database.AddInParameter(command, "@DivisionId", DbType.Guid, divisionId);

                Database.AddInParameter(command, "@Active", DbType.Boolean, active);

                Database.AddOutParameter(command, "@UserId", DbType.Guid, 16);

                Database.AddOutParameter(command, "@IsError", DbType.Int32, 4);

                string[] outParams = new string[] { "@UserId", "@IsError" };

                var result = base.Add(command, outParams, false);

                String errorCode = result[outParams[1]].ToString();

                if (result != null)
                {
                    switch (errorCode)
                    {
                        case "105":
                            throw new Exception("105");
                        case "102":
                            throw new Exception("102");
                        case "104":
                            throw new Exception("104");
                        case "106":
                            throw new Exception("106");
                    }
                    reUserId = new Guid(result[outParams[0]].ToString());
                }
                if (LocalTrasaction)
                    base.CommitTransaction();

                return reUserId;
            }
            catch
            {
                if (LocalTrasaction)
                    this.RollbackTransaction();
                throw;

            }
        }

        public void SignupUserOrg(string pUsername, Guid Userid, Guid pClientId)
        {
            bool LocalTrasaction = false;
            try
            {
                if (base.Transaction == null)
                {
                    base.BeginTransaction();
                    LocalTrasaction = true;
                }
                Guid reUserId = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertOrganization");

                Database.AddInParameter(command, "@OrganizationName", DbType.String, pUsername);

                Database.AddInParameter(command, "@userid", DbType.Guid, Userid);

              base.Execute(command);
  
                if (LocalTrasaction)
                    base.CommitTransaction();

                
            }
            catch
            {
                if (LocalTrasaction)
                    this.RollbackTransaction();
                throw;

            }
        }

        public bool CompleteRegistration(BEClient.User user, bool pIsActive)
        {
            try
            {
                string strFieldValues = string.Empty;

                strFieldValues += string.Format("[{0}] = {1}", "IsActive", pIsActive ? 1 : 0);

                return QuickUpdateContact(user.UserId, strFieldValues);


            }
            catch (Exception)
            {

                throw;
            }
        }

        public BEClient.User ValidateUserName(string pUserName, Guid pClientId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("ValidateUserName");
                Database.AddInParameter(command, "@UserName", DbType.String, pUserName);
                Database.AddInParameter(command, "@ClientId", DbType.Guid, pClientId);

                BEClient.User validUser = base.FindOne(command, new ValidateUsersFactory());
                if (validUser != null)
                {
                    return validUser;
                }
                return null;
            }
            catch
            {
                throw;
            }
        }

        public bool QuickUpdateContact(Guid pUserId, string pValues)
        {
            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("QuickUpdateContact");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@UserId", DbType.Guid, pUserId);

                Database.AddInParameter(command, "@FieldValues", DbType.String, pValues);

                var result = base.Save(command);

                if (result != null)
                {
                    bool.TryParse(result.ToString(), out reREsult);
                }

                return reREsult;
            }

            catch
            {
                throw;
            }
        }


        public bool DeactivateCandidate(string UserName)
        {
            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("DeactivateCandidate");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@UserName", DbType.String, UserName);
                var result = base.Save(command, false);
                if (result != null)
                {
                    bool.TryParse(result.ToString(), out reREsult);
                }
                return reREsult;
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
                DbCommand command = Database.GetStoredProcCommand("GetUserById");
                Database.AddInParameter(command, "@UserId", DbType.Guid, recordId);
                BEClient.User validUser = base.FindOne(command, new GetAllUsersData());
                if (validUser != null)
                {
                    return validUser;
                }
                return null;
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
                DbCommand command = Database.GetStoredProcCommand("GetUserByIdWithoutStatus");
                Database.AddInParameter(command, "@UserId", DbType.Guid, recordId);
                BEClient.User validUser = base.FindOne(command, new GetAllUsersData(), false);
                if (validUser != null)
                {
                    return validUser;
                }
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
                DbCommand command = Database.GetStoredProcCommand("GetUserFromUserName");
                Database.AddInParameter(command, "@UserName", DbType.String, UserName);
                BEClient.User validUser = base.FindOne(command, new GetUsersIdByUserNameFactory());

                return validUser;
            }
            catch
            {
                throw;
            }
        }




        public List<BEClient.User> GetAllUser(Guid clientId, String divisionId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllUser");
                if (!string.IsNullOrEmpty(divisionId))
                {
                    Database.AddInParameter(command, "@DivisionId", DbType.String, divisionId);
                }
                Database.AddInParameter(command, "@ClientId", DbType.Guid, clientId);
                return base.Find(command, new GetAllUserFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<BEClient.User> GetLocationManagers()
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetLocationManagers");
                return base.Find(command, new GetLocationManagersFactory(), false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<BEClient.User> GetAllReviewers(Guid VacRndId, String divisionId, Guid? UserId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllReviewers");
                if (!string.IsNullOrEmpty(divisionId))
                {
                    Database.AddInParameter(command, "@DivisionId", DbType.String, divisionId);
                }
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                if (UserId == null)
                { Database.AddInParameter(command, "@UserId", DbType.Guid, DBNull.Value); }
                else
                {
                    Database.AddInParameter(command, "@UserId", DbType.Guid, (Guid)UserId);
                }
                return base.Find(command, new GetAllReviewerFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<BEClient.User> GetAllReviewersForTemplate(Guid VacRndId, String divisionId, Guid? UserId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllReviewersForTemplate");
                if (!string.IsNullOrEmpty(divisionId))
                {
                    Database.AddInParameter(command, "@DivisionId", DbType.String, divisionId);
                }
                Database.AddInParameter(command, "@TVacRndId", DbType.Guid, VacRndId);
                if (UserId == null)
                { Database.AddInParameter(command, "@UserId", DbType.Guid, DBNull.Value); }
                else
                {
                    Database.AddInParameter(command, "@UserId", DbType.Guid, (Guid)UserId);
                }
                return base.Find(command, new GetAllReviewerFactory());
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
                DbCommand command = Database.GetStoredProcCommand("UpdateUser");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@UserId", DbType.Guid, objUser.UserId);

                Database.AddInParameter(command, "@UserName", DbType.String, objUser.Username);

                Database.AddInParameter(command, "@DivisionId", DbType.Guid, objUser.DivisionId);

                Database.AddInParameter(command, "@IsActive", DbType.Boolean, objUser.IsActive);

                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, objUser.IsDelete);

                var result = base.Save(command);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public bool Delete(string recordId)
        {
            try
            {

                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("Deleteuser");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@userId", DbType.String, recordId);

                var result = base.Save(command, false);

                if (result != null)
                {
                    Boolean.TryParse(result.ToString(), out reREsult);
                }

                return reREsult;
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

                DbCommand command = Database.GetStoredProcCommand("GetDivisionByUserId");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);

                var result = base.FindScalarValue(command);

                return result;
            }

            catch
            {
                throw;
            }

        }
        public BEClient.User GetSSOUser(Guid UserId, Guid SSOToken)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetSSOUser");


                Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);
                Database.AddInParameter(command, "@SSOToken", DbType.Guid, SSOToken);
                BEClient.User validUser = base.FindOne(command, new GetSSOUserData());
                if (validUser != null)
                {
                    return validUser;
                }
                return null;
            }
            catch
            {
                throw;
            }
        }

        public bool ResetSSOToken(Guid UserId)
        {
            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("ResetSSOToken");
                Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);
                var result = base.FindOne(command, new GetSSOUserData(), false);
                if (result != null)
                {
                    Boolean.TryParse(result.ToString(), out reREsult);
                }
                return reREsult;
            }
            catch
            {
                throw;
            }
        }


        public List<BEClient.User> GetAllUsersByDivisionId(Guid DivisionId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetUsersByDivisionId");
                Database.AddInParameter(command, "@DivisionId", DbType.Guid, DivisionId);
                return base.Find(command, new GetUsersByDivisionIdFactory(), false);

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
                DbCommand command = Database.GetStoredProcCommand("GetOnBoardingUsers");
                Database.AddInParameter(command, "@DivisionId", DbType.Guid, DivisionId);
                return base.Find(command, new GetUsersByDivisionIdFactory(), false);
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
                DbCommand command = Database.GetStoredProcCommand("GetAllEmployees");
                return base.Find(command, new GetUsersByDivisionIdFactory(), false);

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
                DbCommand command = Database.GetStoredProcCommand("GetUserByEmailId");
                Database.AddInParameter(command, "@EmailId", DbType.String, EmailId);
                BEClient.User validUser = base.FindOne(command, new GetUserDataByUserId());
                if (validUser != null)
                {
                    return validUser;
                }
                return null;
            }
            catch
            {
                throw;
            }
        }

        public bool ChangeUserPassword(Guid UserId, string Password, string newPassword)
        {
            try
            {

                bool result = false;
                string errorcodes = string.Empty;
                DbCommand command = Database.GetStoredProcCommand("ChangePasswordByUser");
                Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);
                Database.AddInParameter(command, "@Password", DbType.String, Password);
                Database.AddInParameter(command, "@newPassword", DbType.String, newPassword);
                Database.AddOutParameter(command, "@ErrorCode", DbType.Int32, 4);
                //string[] outParams = new string[] { "@ErrorCode" };
                var Result = base.Add(command, "ErrorCode", false);
                if (Result != null)
                {
                    String errorCode = Result.ToString();
                    switch (errorCode)
                    {
                        case "201":
                            result = true;
                            break;
                        case "202":
                            result = false;
                            throw new Exception("Old password doesnot match");
                    }

                }
                return result;


            }
            catch
            {
                throw;
            }
        }
        public bool UpdateProfileImage(string ImageName, Guid UserId)
        {
            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateProfileImage");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);
                Database.AddInParameter(command, "@ProfileImage", DbType.String, ImageName);

                var result = base.Save(command, false);
                if (result != null)
                {
                    reREsult = true;
                }
                return reREsult;
            }
            catch
            {
                throw;
            }
        }


        public bool ValidateNewEmployee(string UserName)
        {
            try
            {
                int result = 0;
                DbCommand command = Database.GetStoredProcCommand("ValidateNewEmployee");

                Database.AddInParameter(command, "@Username", DbType.String, UserName);
                //Database.AddOutParameter(command, "@IsError", DbType.Int32, 4);
                var Result = base.FindScalarValue(command);
                if (Result != null)
                {
                    int.TryParse(Result.ToString(), out result);
                    switch (result)
                    {
                        case 105:
                            throw new Exception("105");
                        case 102:
                            throw new Exception("102");
                        case 104:
                            throw new Exception("104");
                        case 106:
                            throw new Exception("106");
                    }
                }
                return true;
            }
            catch
            {
                throw;
            }

        }

      
    }


    internal class GetUsersByDivisionIdFactory : IDomainObjectFactory<BEClient.User>
    {
        BEClient.User IDomainObjectFactory<BEClient.User>.Construct(IDataReader reader)
        {
            try
            {
                BEClient.User objUser = new BEClient.User();
                objUser.ObjUserDetails = new BEClient.UserDetails();
                objUser.UserId = HelperMethods.GetGuid(reader, "UserId");
                objUser.ObjUserDetails.FirstName = HelperMethods.GetString(reader, "FirstName");
                objUser.ObjUserDetails.LastName = HelperMethods.GetString(reader, "LastName");
                return objUser;
            }
            catch
            {
                throw;
            }

        }
    }
    internal class GetOnboardingUsersFactory : IDomainObjectFactory<BEClient.User>
    {
        BEClient.User IDomainObjectFactory<BEClient.User>.Construct(IDataReader reader)
        {
            try
            {
                BEClient.User objUser = new BEClient.User();
                objUser.ObjUserDetails = new BEClient.UserDetails();
                objUser.UserId = HelperMethods.GetGuid(reader, "UserId");
                objUser.ObjUserDetails.FirstName = HelperMethods.GetString(reader, "FirstName");
                objUser.ObjUserDetails.LastName = HelperMethods.GetString(reader, "LastName");
                return objUser;
            }
            catch
            {
                throw;
            }

        }
    }
    internal class GetUserPasswordFactory : IDomainObjectFactory<BEClient.User>
    {
        BEClient.User IDomainObjectFactory<BEClient.User>.Construct(IDataReader reader)
        {
            try
            {
                BEClient.User objUser = new BEClient.User();
                objUser.Password = HelperMethods.GetString(reader, "password");
                return objUser;
            }
            catch
            {
                throw;
            }

        }
    }

    internal class GetAllUserFactory : IDomainObjectFactory<BEClient.User>
    {
        public BEClient.User Construct(IDataReader reader)
        {
            BEClient.User objUser = new BEClient.User();
            objUser.ObjUserDetails = new BEClient.UserDetails();
            objUser.UserId = HelperMethods.GetGuid(reader, "UserId");
            objUser.ObjUserDetails.FirstName = HelperMethods.GetString(reader, "FirstName");
            return objUser;
        }
    }

    internal class GetLocationManagersFactory : IDomainObjectFactory<BEClient.User>
    {
        public BEClient.User Construct(IDataReader reader)
        {
            BEClient.User objUser = new BEClient.User();
            objUser.ObjUserDetails = new BEClient.UserDetails();
            objUser.UserId = HelperMethods.GetGuid(reader, "UserId");
            objUser.ObjUserDetails.FirstName = HelperMethods.GetString(reader, "FirstName");
            objUser.ObjUserDetails.LastName = HelperMethods.GetString(reader, "LastName");
            return objUser;
        }
    }
    internal class GetAllReviewerFactory : IDomainObjectFactory<BEClient.User>
    {
        public BEClient.User Construct(IDataReader reader)
        {
            BEClient.User objUser = new BEClient.User();
            objUser.ObjUserDetails = new BEClient.UserDetails();
            objUser.UserId = HelperMethods.GetGuid(reader, "UserId");
            objUser.ObjUserDetails.FirstName = HelperMethods.GetString(reader, "FirstName");
            return objUser;
        }
    }

    internal class GetAllUsersData : IDomainObjectFactory<BEClient.User>
    {
        BEClient.User IDomainObjectFactory<BEClient.User>.Construct(IDataReader reader)
        {
            try
            {
                BEClient.User objUser = new BEClient.User();
                objUser.ObjUserDetails = new BEClient.UserDetails();
                objUser.ObjUserDetails.FirstName = HelperMethods.GetString(reader, "FirstName");
                objUser.ObjUserDetails.LastName = HelperMethods.GetString(reader, "LastName");
                objUser.UserId = HelperMethods.GetGuid(reader, "UserId");
                objUser.Password = HelperMethods.GetString(reader, "Password");
                objUser.Username = HelperMethods.GetString(reader, "Username");
                objUser.ClientId = HelperMethods.GetGuid(reader, "ClientId");
                objUser.DivisionId = HelperMethods.GetGuid(reader, "DivisionId");
                objUser.CreatedBy = HelperMethods.GetGuid(reader, "CreatedBy");
                objUser.IsActive = HelperMethods.GetBoolean(reader, "IsActive");
                objUser.ProfileImage = HelperMethods.GetString(reader, "ProfileImage");
                objUser.ManageCompanySetup = HelperMethods.GetBoolean(reader, "ManageCompanySetup");
                return objUser;
            }
            catch
            {
                throw;
            }

        }
        internal class GetReviewerUserNamesByVacRndIdFactory : IDomainObjectFactory<BEClient.User>
        {
            BEClient.User IDomainObjectFactory<BEClient.User>.Construct(IDataReader reader)
            {
                try
                {
                    BEClient.User objUser = new BEClient.User();
                    objUser.UserId = HelperMethods.GetGuid(reader, "UserId");
                    objUser.Username = HelperMethods.GetString(reader, "Username");
                    return objUser;
                }
                catch
                {
                    throw;
                }

            }
        }

    }

    internal class GetSSOUserData : IDomainObjectFactory<BEClient.User>
    {
        BEClient.User IDomainObjectFactory<BEClient.User>.Construct(IDataReader reader)
        {
            try
            {
                BEClient.User objUser = new BEClient.User();
                objUser.Password = HelperMethods.GetString(reader, "Password");
                objUser.Username = HelperMethods.GetString(reader, "Username");
                return objUser;
            }
            catch
            {
                throw;
            }

        }

    }
    internal class GetUserDataByUserId : IDomainObjectFactory<BEClient.User>
    {
        BEClient.User IDomainObjectFactory<BEClient.User>.Construct(IDataReader reader)
        {
            try
            {
                BEClient.User objUser = new BEClient.User();
                objUser.Password = HelperMethods.GetString(reader, "Password");
                objUser.UserId = HelperMethods.GetGuid(reader, "UserId");
                objUser.ClientId = HelperMethods.GetGuid(reader, "ClientId");
                return objUser;
            }
            catch
            {
                throw;
            }

        }

    }
    internal class GetUsersIdByUserNameFactory : IDomainObjectFactory<BEClient.User>
    {
        BEClient.User IDomainObjectFactory<BEClient.User>.Construct(IDataReader reader)
        {
            try
            {
                BEClient.User objUser = new BEClient.User();
                objUser.UserId = HelperMethods.GetGuid(reader, "UserId");
                return objUser;
            }
            catch
            {
                throw;
            }

        }
    }

    


    internal class ValidateUsersFactory : IDomainObjectFactory<BEClient.User>
    {
        BEClient.User IDomainObjectFactory<BEClient.User>.Construct(IDataReader reader)
        {
            try
            {
                BEClient.User objUser = new BEClient.User();
                objUser.UserId = HelperMethods.GetGuid(reader, "UserId");

                objUser.ClientId = HelperMethods.GetGuid(reader, "ClientId");



                return objUser;
            }
            catch
            {
                throw;
            }

        }
    }

    internal class GetValideLogin : IDomainObjectFactory<BEClient.User>
    {
        BEClient.User IDomainObjectFactory<BEClient.User>.Construct(IDataReader reader)
        {
            try
            {
                switch (HelperMethods.GetInt32(reader, "IsError"))
                {
                    case 0:
                        BEClient.User objUser = new BEClient.User();
                        objUser.UserId = HelperMethods.GetGuid(reader, "UserId");
                        return objUser;
                    case -101:
                        return null;
                    default:
                        return null;
                }


            }
            catch
            {
                throw;
            }
        }
    }

}


