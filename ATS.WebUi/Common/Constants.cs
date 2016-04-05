using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATS.WebUi.Common
{
    public class Constants
    {
        #region Javascript Error Appear
        public const Int32 JSCR_MSG_FADEOUT= 3000;
#endregion
        #region for Area

        public const string AREA_ROOT = "";
        
        public const string AREA_CANDIDATE = "Candidate";
        public const string AREA_EMPLOYEE = "Employee";
        public const string AREA_ADMIN = "Admin";

        #endregion

        #region Controller

        #endregion
        public const String STR_TEMP = "Temp";

        public const String SERVER_TEMP_DOC_PATH = @"~\Resume\Temp";
        public const String SERVER_TEMP_LOGO_PATH = @"~\Logo";

        #region EMPLOYEE DEFAULT ACTION AND CONTROLLE
        public const String DEFAULT_REDIRECT_EMPLOYEE_CONTROLLER = "MyVacancy";
        public const String DEFAULT_REDIRECT_EMPLOYEE_ACTION = "Index";
        #endregion
        #region CANDIDATE DEFAULT ACTION AND CONTROLLE
        public const String DEFAULT_REDIRECT_CANDIDATE_CONTROLLER = "Home";
        public const String DEFAULT_REDIRECT_CANDIDATE_ACTION = "Index";
        #endregion
        #region EMAIL
        public const int EMAIL_EXPIRY_LINK = 24;
        #endregion

        #region for Redirection Method 
        public const string STR_LIST_METHOD = "Index";
        public const string STR_CREATE_METHOD = "Create";
        public const string STR_EDIT_METHOD = "Edit";
        public const string STR_VIEW_METHOD = "View";
        #endregion

        #region for Default Language Culture
        public const string Default_Language_Culture = "en-US";
        #endregion

        #region General Language Tag
        public const string Save_Button = "Save_BN";
        #endregion
        #region Logo Constant
        public const string STR_LOGO_PATH = @"~/UploadImages/Logo/";
        public const string STR_RESUME_PATH = @"~/Resume/";
        public const string STR_RESUME_DIR_PATH = @"~/Resume";
        #endregion


        #region Cookie Constant
        public const string COOKIE_CURRENT_CLIENTNAME = "ATSClientName";
        public const string COOKIE_CURRENT_USER = "ATSLoginUser";
        
        public const int COOKIE_MIN = 1440;//mins
        public const string COOKIE_CURRENT_USER_REMEMBER_ME = "ATSLoginUserRememberMe";
        public const int COOKIE_MIN_REMEMBER_ME = 1440;//mins
        
        
        #endregion

        #region Vacancy Date Format
        public const String VACANCY_DATE_FORMAT = "MMM dd, yyyy";
        #endregion

 

     

#region Paging
        public const int PAGGING = 20;
#endregion

    }

}