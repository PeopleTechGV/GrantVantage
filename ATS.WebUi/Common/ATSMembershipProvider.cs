//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Security;
//using BLMaster = ATS.BusinessLogic.Master;
//using BEMaster = ATS.BusinessEntity.Master;

//namespace ATS.WebUi.Common
//{
//    public class ATSMembershipProvider : MembershipProvider
//    {
//        #region Properties

//        public override int MinRequiredPasswordLength
//        {
//            get { return 6; }

//        }

//        #endregion

//        #region Implemented Overrides

//        public override string ApplicationName
//        {
//            get
//            {
//                return Prompt.Shared.Utility.Library.Methods.GetAppSettingValue("ApplicationName");
//            }
//            set
//            {
//                throw new Exception("Not implemented");
//            }
//        }

//        public override bool ValidateUser(string username, string password)
//        {
//            string IpAddress = HttpContext.Current.Request.UserHostAddress;

//            BLMaster.UserAction userAction = new BLMaster.UserAction();
//            Guid systemUserId = userAction.ValidateUserByClient(username, password,CurrentClient.Instance.ClientId);

//            if (systemUserId != null && !systemUserId.Equals(Guid.Empty))
//            {
//                CurrentUser.Instance. CurrentUser currentUser = new CurrentUser(systemUserAction.GetSystemUserBySystemUserId(systemUserId));
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }

//        public override MembershipUser GetUser(string username, bool userIsOnline)
//        {
//            return CurrentUser.Instance;
//        }

//        public override bool ChangePassword(string username, string oldPassword, string newPassword)
//        {
//            try
//            {
//                return true;
//            }
//            catch { return false; }
//        }

//        #endregion

//        #region Method not yet implemented



//        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
//        {
//            return false;
//        }

//        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
//        {
//            throw new NotImplementedException();
//        }

//        public override bool DeleteUser(string username, bool deleteAllRelatedData)
//        {
//            throw new NotImplementedException();
//        }

//        public override bool EnablePasswordReset
//        {
//            get { throw new NotImplementedException(); }
//        }

//        public override bool EnablePasswordRetrieval
//        {
//            get { throw new NotImplementedException(); }
//        }

//        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
//        {
//            throw new NotImplementedException();
//        }

//        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
//        {
//            throw new NotImplementedException();
//        }

//        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
//        {
//            throw new NotImplementedException();
//        }

//        public override int GetNumberOfUsersOnline()
//        {
//            throw new NotImplementedException();
//        }

//        public override string GetPassword(string username, string answer)
//        {
//            throw new NotImplementedException();
//        }


//        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
//        {
//            throw new NotImplementedException();
//        }

//        public override string GetUserNameByEmail(string email)
//        {
//            throw new NotImplementedException();
//        }

//        public override int MaxInvalidPasswordAttempts
//        {
//            get { throw new NotImplementedException(); }
//        }

//        public override int MinRequiredNonAlphanumericCharacters
//        {
//            get { return 0; }
//        }

//        public override int PasswordAttemptWindow
//        {
//            get { throw new NotImplementedException(); }
//        }

//        public override MembershipPasswordFormat PasswordFormat
//        {
//            get { throw new NotImplementedException(); }
//        }

//        public override string PasswordStrengthRegularExpression
//        {
//            get { throw new NotImplementedException(); }
//        }

//        public override bool RequiresQuestionAndAnswer
//        {
//            get { throw new NotImplementedException(); }
//        }

//        public override bool RequiresUniqueEmail
//        {
//            get { throw new NotImplementedException(); }
//        }

//        public override string ResetPassword(string username, string answer)
//        {
//            throw new NotImplementedException();
//        }

//        public override bool UnlockUser(string userName)
//        {
//            throw new NotImplementedException();
//        }

//        public override void UpdateUser(MembershipUser user)
//        {
//            throw new NotImplementedException();
//        }

//        #endregion
//    }
//}
