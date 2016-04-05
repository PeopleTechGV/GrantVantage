using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using BEMaster = ATS.BusinessEntity.Master;

using BLClient = ATS.BusinessLogic;
using BLMaster = ATS.BusinessLogic.Master;
using ATSHelper = ATS.HelperLibrary;
using Prompt.Shared.Utility.Library;

namespace ATS.BusinessLogic.Common
{
    public static class LoginOperation
    {
        static BLClient.UserAction _UserAction;
        static BLMaster.UserMasterAction _UserMasterAction;

        public static BEMaster.Client CreateClientSession(String clientName)
        {

            BLMaster.ClientAction objClientAction = new BLMaster.ClientAction();
            BEMaster.Client objClient = objClientAction.GetClientByName(clientName);
            if (objClient == null)
            {
                Methods.Session.Clear();
                return null;
            }
            else
            {
                BLClient.CompanyInfoAction objCompanyInfoAction = new BLClient.CompanyInfoAction(objClient.ClientId);
                objClient.objCompanyInfo = objCompanyInfoAction.GetCompanyInfoDetails();
            }
            objClient.ClientLanguageList = new List<BEMaster.ClientLanguage>();
            BLMaster.ClientLanguageAction objClientLanguageAction = new BLMaster.ClientLanguageAction();
            objClient.ClientLanguageList = objClientLanguageAction.GetLanguageByClientId(objClient.ClientId);

            return objClient;

        }

        public static BEClient.User ValidateLogin(string Username, string Password, Guid ClientId)
        {
            _UserAction = new BLClient.UserAction(ClientId);
            Guid CurrentUserId = _UserAction.ValidateUserByClient(Username, Password);
            if (!CurrentUserId.Equals(Guid.Empty))
                return _UserAction.GetUserForSession(CurrentUserId);
            else
                return null;

        }
        public static BEClient.User GetLogedInUserObj(Guid ClientId, Guid UserId)
        {
            _UserAction = new BLClient.UserAction(ClientId);
            BEClient.User _currentUser = _UserAction.GetUserForSession(UserId);
            if (_currentUser != null)
                return _currentUser;
            else
                return null;

        }

        public static BEMaster.UserMaster ValidateUserMaster(string Username, string Password)
        {
            _UserMasterAction = new BLMaster.UserMasterAction();
            BEMaster.UserMaster objUserMaster = _UserMasterAction.ValidateUser(Username, Password);
            if (objUserMaster != null)
                return objUserMaster;
            else
                return null;

        }

        public static Guid CandidateSignUp(string Username, string Password, Guid ClientId, out string oResult, Guid languageId, Boolean active)
        {
            Guid resConactid = Guid.Empty;
            oResult = string.Empty;
            try
            {
                BEClient.ATSSecurityRole[] SecurityRole = new BEClient.ATSSecurityRole[1];
                SecurityRole[0] = BEClient.ATSSecurityRole.Candidate;
                resConactid = SignUp(Username, Password, ClientId, Guid.Empty, SecurityRole, out oResult, languageId, active);
                return resConactid;
            }
            catch (Exception ex)
            {

                resConactid = Guid.Empty;
                oResult = ex.Message;
                return resConactid;
            }
        }


        public static void CandidateOrganization(string Orgname, Guid userid, Guid ClientId)
        {
            _UserAction = new UserAction(ClientId);
            Guid resConactid = Guid.Empty;

        
            try
            {

              _UserAction.SignupUserOrg(Orgname, userid, ClientId);
         
            }
            catch
            {
                throw;
            }
        }
        private static Guid SignUp(string Username, string Password, Guid ClientId, Guid DivisionId, BEClient.ATSSecurityRole[] SecurityRole, out string oResult, Guid languageId, Boolean active)
        {
            _UserAction = new UserAction(ClientId);
            Guid resConactid = Guid.Empty;

            oResult = string.Empty;
            try
            {

                resConactid = _UserAction.SignupUser(Username, Password, ClientId, DivisionId, SecurityRole, languageId, active);
                return resConactid;
            }
            catch
            {
                throw;
            }
        }

        public static BEClient.User GetUserById(Guid pUserId, Guid pClientId)
        {
            try
            {
                UserAction _UserAction = new UserAction(pClientId);
                return _UserAction.GetRecordById(pUserId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool CompleteRegister(Guid ContactId, Guid ClientId)
        {
            try
            {

                UserAction _UserAction = new UserAction(ClientId);
                return _UserAction.CompleteRegistration(new BEClient.User() { UserId = ContactId }, true);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static bool Login(Guid LogedinUserId, Guid ClientId)
        {
            bool LoggedIn = false;
            try
            {
                UserAction _UserAction = new UserAction(ClientId);
                BEClient.User user = _UserAction.GetUserForSession(LogedinUserId);

                if (user != null)
                {

                    LoggedIn = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return LoggedIn;
        }
        public static BEClient.User ValidateUserName(string pUserName, Guid pClientId)
        {
            try
            {
                UserAction _UserAction = new UserAction(pClientId);
                return _UserAction.ValidateUserName(pUserName, pClientId);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public static bool ResetPassword(Guid UserId, string Password, Guid pClientId)
        {
            try
            {
                UserAction _UserAction = new UserAction(pClientId);

                return _UserAction.ResetPassword(UserId, Password);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}
