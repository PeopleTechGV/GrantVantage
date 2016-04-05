using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Prompt.Shared.Utility.Library;
using System.Web.Security;
using BEMaster = ATS.BusinessEntity.Master;
using BLMaster = ATS.BusinessLogic.Master;

using BEClient = ATS.BusinessEntity;
using BLClient = ATS.BusinessLogic;

using ATSHelper = ATS.HelperLibrary;

namespace ATS.WebUi.Common
{
    public class CurrentSession : MembershipUser
    {
        #region Fields

        private BEClient.User _VerifiedUser;
        private BEMaster.Client _VerifiedClient;
        private BEMaster.UserMaster _VerifiedUserMaster;

        #endregion

        #region singelton

        //the private instance of this class
        //private static CurrentUser _instance = null;

        //the public property exposing the private instance for this class
        public static CurrentSession Instance
        {
            get
            {
                try
                {
                CurrentSession myCurrentSession = null;
                Methods.GetVar<CurrentSession>("ATS_CurrentUser", ref myCurrentSession);
                return myCurrentSession;
                }
                catch 
                {

                    return null;
                }
            }
            private set //only this class can call this accessor.
            {
                CurrentSession myCurrentSession = null;
                Methods.SetVar<CurrentSession>("ATS_CurrentUser", ref myCurrentSession, value);
            }
        }

        //a public static constructor (
        public CurrentSession(BEClient.User pCurrentUser)
        {
            if (Instance == null)
                Instance = new CurrentSession();

            this.FullName = pCurrentUser.Username;
            VerifiedUser = pCurrentUser;
        }

        public CurrentSession(BEMaster.Client pCurrentclient)
        {
            if (Instance == null)
                Instance = new CurrentSession();


            VerifiedClient = pCurrentclient;
        }

        public CurrentSession(BEMaster.UserMaster pCurrentUserMaster)
        {
            if (Instance == null)
                Instance = new CurrentSession();


            VerifiedUserMaster = pCurrentUserMaster;
        }

        /// <summary>
        /// Returns the current NTB user credentials verified profile translated to a Business Entity Credentials Verified Profile.
        /// </summary>
        public BEClient.User VerifiedUser
        {
            get
            {
                Methods.GetVar<BEClient.User>(ATSHelper.Constant.SESSION_LOGGEDIN_USER, ref _VerifiedUser);
                return _VerifiedUser;
            }
            set
            {
                Methods.SetVar<BEClient.User>(ATSHelper.Constant.SESSION_LOGGEDIN_USER, ref _VerifiedUser, value);
            }//end set accessor

        }//end property

        public BEMaster.Client VerifiedClient
        {
            get
            {
                Methods.GetVar<BEMaster.Client>(ATSHelper.Constant.SESSION_LOGGEDIN_CLIENT, ref _VerifiedClient);
                return _VerifiedClient;
            }
            set
            {
                Methods.SetVar<BEMaster.Client>(ATSHelper.Constant.SESSION_LOGGEDIN_CLIENT, ref _VerifiedClient, value);
            }//end set accessor

        }//end property

        public BEMaster.UserMaster VerifiedUserMaster
        {
            get
            {
                Methods.GetVar<BEMaster.UserMaster>(ATSHelper.Constant.SESSION_LOGGEDIN_USERMASTER, ref _VerifiedUserMaster);
                return _VerifiedUserMaster;
            }
            set
            {
                Methods.SetVar<BEMaster.UserMaster>(ATSHelper.Constant.SESSION_LOGGEDIN_USERMASTER, ref _VerifiedUserMaster, value);
            }//end set accessor

        }//end property

        //a base private constructor
        private CurrentSession()
        {
            //flush the session
            Methods.Session.Clear();
        }

        public void CurrentUserLogOut()
        {
            //flush the session
            Methods.SetVar<BEClient.User>(ATSHelper.Constant.SESSION_LOGGEDIN_USER, ref _VerifiedUser, null);
        }
       
        public void CurrentClientLogOut()
        {
            //flush the session
            Methods.SetVar<BEMaster.Client>(ATSHelper.Constant.SESSION_LOGGEDIN_CLIENT, ref _VerifiedClient, null);
        }

        public void CurrentUserMasterLogOut()
        {
            //flush the session
            Methods.SetVar<BEMaster.UserMaster>(ATSHelper.Constant.SESSION_LOGGEDIN_USERMASTER, ref _VerifiedUserMaster, null);
        }

        #endregion singelton



        #region properties

        public String FullName { get; set; }

        #endregion

        #region Methods

        public void Logout()
        {
            Methods.Session.Clear();
            Instance = null;
            VerifiedUser = null;
            FormsAuthentication.SignOut();
        }
        public Guid UserId
        {
            get
            {
                return (this.VerifiedUser != null) ? this.VerifiedUser.UserId : Guid.Empty;
            }
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Returns the username of the current impersonated profile
        /// </summary>
        public override string UserName
        {
            get
            {
                return (this.VerifiedUser != null) ? this.VerifiedUser.Username : string.Empty;
            }
        }

        /// <summary>
        /// This is hard coded to the PromptMembershipProvider
        /// </summary>
        public override string ProviderName
        {
            get
            {
                return "ATSMembershipProvider";
            }
        }
        #endregion
    }
}
