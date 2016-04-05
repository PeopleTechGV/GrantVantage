using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity.Common
{
    public class XMLResources
    {
        public const string rootNode_resource = Resources.Entities.XMLResources.rootNode_resource;
        public const string childNode_resource = Resources.Entities.XMLResources.childNode_resource;
        public const string childAttr_culture = Resources.Entities.XMLResources.childAttr_culture;
        public const string childAttr_name = Resources.Entities.XMLResources.childAttr_name;
        public const string childAttr_value = Resources.Entities.XMLResources.childAttr_value;
        public static string directory_Name = Resources.Entities.XMLResources.directory_Name;
        public static string file_Name = Resources.Entities.XMLResources.file_Name;
    }

    public class PermissionType
    {
        public const string Create = "Create";
        public const string View = "View";
        public const string Modify = "Modify";
        public const string Delete = "Delete";
    }

    public class SQLFile
    {
        public const string DefaultDBScript = "ATS_DefaultClientScript.sql";
        public const string DefaultDataScript = "ATS_DefaultClientDataScript.sql";
        public const string DefaultDropDBScript = "ATS_DropDatabase.sql";
        public const string DefaultFolder = "App_Data";
        public const string DefaultDBName = "ATS_Client";
        public const string ClientId = "ClientID";
    }
    public class RoutineName
    {
        public const string SOLR_DELETE_FORMAT = "{0}\n insert into @tbl EXEC [{1}].[dbo].[{1}SolrDelete] @UpdatedOn";
        public const string SOLR_DELTA_FORMAT = "{0}\n insert into @tbl EXEC [{1}].[dbo].[{1}SolrDelta] @UpdatedOn";
        public const string SOLR_SEARCH_FORMAT = "{0}\n insert into @tbl EXEC [{1}].[dbo].[{1}SolrSearch] @VacancyId ";

        public const string SOLR_ACHIEVEMENT = "{0}\n insert into @tbl EXEC [{1}].[dbo].[Solr_Achievement] @ProfileId,@LanguageId ";
        public const string SOLR_ASSOCIATIONS = "{0}\n insert into @tbl EXEC [{1}].[dbo].[Solr_Associations] @ProfileId,@LanguageId ";
        public const string SOLR_AVAILABILITY = "{0}\n insert into @tbl EXEC [{1}].[dbo].[Solr_Availability] @ProfileId,@LanguageId ";

        public const string SOLR_EDUHISTORY = "{0}\n insert into @tbl EXEC [{1}].[dbo].[Solr_EducationHistory] @ProfileId,@LanguageId ";
        public const string SOLR_EMPHISTORY = "{0}\n insert into @tbl EXEC [{1}].[dbo].[Solr_EmploymentHistory] @ProfileId,@LanguageId ";
        public const string SOLR_EXECUTIVESUMMARY = "{0}\n insert into @tbl EXEC [{1}].[dbo].[Solr_ExecutiveSummary] @ProfileId,@LanguageId ";
        public const string SOLR_LICENCEANDCERTIFICATION = "{0}\n insert into @tbl EXEC [{1}].[dbo].[Solr_LicenceAndCertifications] @ProfileId,@LanguageId ";
        public const string SOLR_OBJECTIVE = "{0}\n insert into @tbl EXEC [{1}].[dbo].[Solr_Objective] @ProfileId,@LanguageId ";
        public const string SOLR_PROFILE = "{0}\n insert into @tbl EXEC [{1}].[dbo].[Solr_Profile]";

        public const string SOLR_PROFILE_DELETE = "{0}\n insert into @tbl EXEC [{1}].[dbo].[SolrProfileDelete]";
        public const string SOLR_PROFILE_DELTA = "{0}\n insert into @tbl EXEC [{1}].[dbo].[SolrProfileDelta]";

        public const string SOLR_PUBLICATIONHISTORY = "{0}\n insert into @tbl EXEC [{1}].[dbo].[Solr_PublicationHistory] @ProfileId,@LanguageId ";
        public const string SOLR_REFERENCE = "{0}\n insert into @tbl EXEC [{1}].[dbo].[Solr_Reference] @ProfileId,@LanguageId ";
        public const string SOLR_SKILLS = "{0}\n insert into @tbl EXEC [{1}].[dbo].[Solr_Skills] @ProfileId,@LanguageId ";
        public const string SOLR_SPEAKINGEVENTHISTORY = "{0}\n insert into @tbl EXEC [{1}].[dbo].[Solr_SpeakingEventHistory] @ProfileId,@LanguageId ";
        public const string SOLR_USERDETAIL = "{0}\n insert into @tbl EXEC [{1}].[dbo].[Solr_UserDetail] @ProfileId,@LanguageId ";


        public const string SOLR_DELETE = "ATSSolrDelete";
        public const string SOLR_DELTA = "ATSSolrDelta";
        public const string SOLR_SEARCH = "ATSSolrSearch";

        public const string SOLR_CON_ACHIEVEMENT = "SolrAchievement";
        public const string SOLR_CON_ASSOCIATIONS = "SolrAssociations";
        public const string SOLR_CON_AVAILABILITY = "SolrAvailability";

        public const string SOLR_CON_EDUHISTORY = "SolrEducationHistory";
        public const string SOLR_CON_EMPHISTORY = "SolrEmploymentHistory";
        public const string SOLR_CON_EXECUTIVESUMMARY = "SolrExecutiveSummary";
        public const string SOLR_CON_LICENCEANDCERTIFICATION = "SolrLicenceAndCertifications";
        public const string SOLR_CON_OBJECTIVE = "SolrObjective";
        public const string SOLR_CON_PROFILE = "SolrProfile";
        public const string SOLR_CON_PROFILE_DELETE = "SolrProfileDelete";
        public const string SOLR_CON_PROFILE_DELTA = "SolrProfileDelta";
        public const string SOLR_CON_PUBLICATIONHISTORY = "SolrPublicationHistory";
        public const string SOLR_CON_REFERENCE = "SolrReference";
        public const string SOLR_CON_SKILLS = "SolrSkills";
        public const string SOLR_CON_SPEAKINGEVENTHISTORY = "SolrSpeakingEventHistory";
        public const string SOLR_CON_USERDETAIL = "SolrUserDetail";

        public const string SOLR_FIXED_TEXT = "--DBQUERY--";
    }

    public class SubscriptionTbl
    {
        public const string UserLimit = "UserLimit";
        public const string SubscriptionId = "SubscriptionId";
    }

    public class VacancyTbl
    {
        public const string PositionId = "PositionID";
        public const string VacancyId = "VacancyId";
    }

    public class DivisionTbl
    {
        public const string DivisionId = "DivisionId";
    }

    public class JobLocationTbl
    {
        public const string JobLocationId = "JobLocationId";
    }

    public class VacancyStatusTbl
    {
        public const string VacancyStatusId = "VacancyStatusId";
    }

    public class PositionTypeTbl
    {
        public const string PositionTypeId = "PositionTypeId";
    }

    public class AppBasedStatusTbl
    {
        public const string ApplicationBasedStatusId = "ApplicationBasedStatusId";
    }

    public class ClientTbl
    {
        public const string ClientName = "ClientName";
        public const string ClientId = "ClientId";
    }

    public class DegreeTypeTbl
    {
        public const string DefaultName = "DefaultName";
        public const string DegreeTypeId = "DegreeTypeId";

    }

    public class SkillTypeTbl
    {
        public const string DefaultName = "DefaultName";
        public const string SkillTypeId = "SkillTypeId";

    }
    public class RndTypeTbl
    {
        public const string DefaultName = "DefaultName";
        public const string RndTypeId = "RndTypeId";

    }
    public class VacQueTypeTbl
    {
        public const string DefaultName = "DefaultName";
        public const string VacQueTypeId = "VacQueTypeId";

    }
    public class QueCatTbl
    {
        public const string DefaultName = "DefaultName";
        public const string QueCatId = "QueCatId";

    }
    public class QueTbl
    {
        public const string DefaultName = "DefaultName";
        public const string QueId = "QueId";

    }

    public class LanguageTbl
    {
        public const string LangaugeName = "LanguageName";
        public const string LangaugeId = "LangaugeId";
    }
    public class OfferLetterTbl
    {
        public const string OfferLetterName = "OfferLetterName";
        public const string OfferletterId = "OfferletterId";
    }

    public class CommonTblVal
    {
        public const string IsDelete = "IsDelete";
        public const string DefaultName = "DefaultName";
        public const string DefaultValue = "DefaultValue";

        public const string VacancyStatusText = "VacancyStatusText";
        public const string StatusText = "StatusText";
        public const string ShowToCandiDate = "ShowToCandiDate";


        public const string OfferLetterName = "OfferLetterName";
    }
    public class VacancyRound
    {
        public const string ROUND_CONFIGURATION = "ROUND_CONFIGURATION";//Round Configuration

        public const string ROUND_WEIGHT = "ROUND_WEIGHT";//Weight(1-100)

        public const string ROUND_QUESTIONS = "ROUND_QUESTIONS";//Questions
        public const string ROUND_QUESTIONS_TYPE = "ROUND_QUESTIONS_TYPE";//Type

        public const string ROUND_REVIEWERS = "ROUND_REVIEWERS";//Reviewers
        public const string ROUND_REVIEWERS_CANPROMOTE = "ROUND_REVIEWERS_CANPROMOTE";//Can Promote
        public const string ROUND_REVIEWERS_TITLE = "ROUND_REVIEWERS_TITLE";//Title
    }

    public class VacancyTemplate
    {
        public const string FRM_VACANCYTEMPLATE = "FRM_VACANCYTEMPLATE";//Vacancy Template
        public const string FRM_DIVISION = DivisionConstant.LST_DIV_NAME;
        public const string FRM_POSITION = PositionType.LST_POS_POSNAME;
        public const string FRM_VAC_DETAILS = "FRM_VAC_DETAILS";
        public const string FRM_VAC_JOB_DESCRIPTION = VacancyConstant.FRM_VAC_JOB_DESCRIPTION;
        public const string FRM_VAC_COMPENSATION_INFO = VacancyConstant.FRM_VAC_COMPENSATION_INFO;
        public const string FRM_VAC_ADD_TEMPLATE = "FRM_VAC_ADD_TEMPLATE";
    }

    public class DocumentTypeConstant
    {
        public const string DOCUMENT_NAME = "DOCUMENT_NAME";
        public const string DOCUMENT_TYPE = "DOCUMENT_TYPE";
        public const string DOCUMENT_DESCRIPTION = "DOCUMENT_DESCRIPTION";
        public const string ALLOWED_EXTENSION_TYPES = "ALLOWED_EXTENSION_TYPES";
        public const string ROUND_TYPE = "FRM_APP_ROUND_TYPE";
    }

    public class QueCategory
    {
        public const string FRM_WEIGHT = QuestinoLibraryConstant.FRM_WEIGHT;
        public const string FRM_CATEGORY = "FRM_CATEGORY";
        public const string FRM_CAT_CAT_DETAILS = "FRM_CAT_CAT_DETAILS";

    }

    public class CommonConstant
    {
        public const string IN = "IN";
        public const string JOBS = "Jobs";
        public const string JOB = "Job";
        public const string ADD = "Add";
        public const string BACK = "BACK";
        public const string UPDATE = "Update";
        public const string SAVE = "Save";
        public const string DELETE = "Delete";
        public const string REMOVE = "Remove";
        public const string DETAILS = "Details";
        public const string JOB_TITLE = "Job_Title";
        public const string DESCRIPTION = "Description";
        public const string LOCATION = "Location";
        public const string POSTED_ON = "Posted_On";
        public const string ACTION = "Action";
        public const string LANGUAGE = "Language";
        public const string WELCOME = "Welcome";
        public const string REQUIRE = "Require";
        public const string DOWNLOAD = "Download";
        public const string DEADLINE = "Closing Date";// Changed Deadline to Closing date for CR-19 By Muneendra
        public const string PROGRAM_OFFICE = "Program Office";
        public const string FUND_AMOUNT = "Fund Amount";


        public const string DEFAULT_NAME = "DEFAULT_NAME";
        public const string APPLIED_ON = "APPLIED_ON";
        public const string STATUS = "STATUS";
        public const string UPLOADRESUME = "UPLOADRESUME";
        public const string UPLOADDOCUMENT = "UPLOADDOCUMENT";
        public const string UPLOADDOCUMENTS = "UPLOADDOCUMENTS";
        public const string SELECTRESUME = "SELECTRESUME";
        public const string SIDEBAR_TEXT = "SIDEBAR_TEXT";
        public const string SIDEBAR_TEXT2 = "SIDEBAR_TEXT2";
        public const string CREATE_PROFILE = "CREATE_PROFILE";
        public const string CREATE_PROFILE_OR_LOGIN_HERE = "CREATE_PROFILE_OR_LOGIN_HERE";
        public const string APPLY_JOB = "APPLY_JOB";
        public const string SALARY_RANGE = "SALARY_RANGE";
        public const string VACANCY = "VACANCY";
        public const string POSITION_ID = "POSITION_ID";
        public const string POSITION_TYPE = "POSITION_TYPE";
        public const string START_DATE = "START_DATE";
        public const string JOB_TYPE = "JOB_TYPE";
        public const string EMPLOYEEMENT_TYPE = "FRM_SER_Employement_Type";
        public const string APPLICATION_INCLUDES = "APPLICATION_INCLUDES";
        public const string APPLICATION_SUMMARY = "APPLICATION_SUMMARY";
        public const string REQUIRED_SCREENING_QUESTIONS = "REQUIRED_SCREENING_QUESTIONS";
        public const string ACTION_APPLY = "APPLY";
        public const string ACTION_DRAFT = "ACTION_DRAFT";
        public const string ACTION_SAVE_TO_MYJOBS = "ACTION_SAVE_TO_MYJOBS";
        public const string FRM_PRF_BTN_ADD_EDUCATION = "FRM_PRF_BTN_ADD_EDUCATION";
        public const string FRM_PRF_BTN_ADD_COMPANY = "FRM_PRF_BTN_ADD_COMPANY";
        public const string FRM_PRF_BTN_ADD_LICENCEANDCERTIFICATIONS = "FRM_PRF_BTN_ADD_LICENCEANDCERTIFICATIONS";
        public const string FRM_PRF_BTN_ADD_PUBLICATIONHISTORY = "FRM_PRF_BTN_ADD_PUBLICATIONHISTORY";
        public const string FRM_PRF_BTN_ADD_SPEAKINGEVENTHISTORY = "FRM_PRF_BTN_ADD_SPEAKINGEVENTHISTORY";
        public const string FRM_PRF_BTN_ADD_LANGUAGES = "FRM_PRF_BTN_ADD_LANGUAGES";
        public const string FRM_VAC_PDF = "FRM_VAC_PDF";
        public const string FRM_VAC_PDF_COVER_LETTER = "FRM_VAC_PDF_COVER_LETTER";
        public const string FRM_VAC_PDF_RESUME = "FRM_VAC_PDF_RESUME";

        public const string FRM_PRF_BTN_ADD_ACHIEVEMENTS = "FRM_PRF_BTN_ADD_ACHIEVEMENTS";
        public const string FRM_PRF_BTN_ADD_ASSOCIATIONS = "FRM_PRF_BTN_ADD_ASSOCIATIONS";
        public const string FRM_PRF_BTN_ADD_SKILLSANDQUALIFICATIONS = "FRM_PRF_BTN_ADD_SKILLSANDQUALIFICATIONS";
        public const string FRM_PRF_BTN_ADD_REFERENCES = "FRM_PRF_BTN_ADD_REFERENCES";
        public const string FRM_PRF_BTN_DELETCOMPANY = "FRM_PRF_BTN_DELETCOMPANY";
        public const string FRM_PRF_BTN_EDUCATIONDELETE = "FRM_PRF_BTN_EDUCATIONDELETE";

        public const string FRM_PRF_BTN_DELETE_SKILLSANDQUALIFICATION = "FRM_PRF_BTN_DELETE_SKILLSANDQUALIFICATION";
        public const string FRM_PRF_BTN_DELETE_REFERENCES = "FRM_PRF_BTN_DELETE_REFERENCES";
        public const string FRM_PRF_BTN_DELETE_PUBLICATIONHISTORY = "FRM_PRF_BTN_DELETE_PUBLICATIONHISTORY";
        public const string FRM_PRF_BTN_DELETE_SPEAKINGEVENTHISTORY = "FRM_PRF_BTN_DELETE_SPEAKINGEVENTHISTORY";
        public const string FRM_PRF_BTN_DELETE_LANGUAGES = "FRM_PRF_BTN_DELETE_LANGUAGES";
        public const string FRM_PRF_BTN_DELETE_ACHIEVEMENTS = "FRM_PRF_BTN_DELETE_ACHIEVEMENTS";
        public const string FRM_PRF_BTN_DELETE_ASSOCIATION = "FRM_PRF_BTN_DELETE_ASSOCIATION";

        public const string BTN_LIC_DELETE = "BTN_LIC_DELETE";

        public const string BTN_LIC_ADD = "BTN_LIC_ADD";

        public const string INVALID_DATE = "INVALID_DATE";

        public const string SAVE_SEARCH_QUERY = "SAVE_SEARCH_QUERY";

        public const string SEARCH_QUERY_NAME = "SEARCH_QUERY_NAME";

        public const string SAVE_QUERY_BTN = "SAVE_QUERY_BTN";


        public const string FRM_QUE_CAT_ADD = "FRM_QUE_CAT_ADD";
        public const string FRM_QUE_CAT_SAVE = "FRM_QUE_CAT_SAVE";
        public const string FRM_QUE_CAT_REMOVE = "FRM_QUE_CAT_REMOVE";

        public const string FRM_QUE_ADD = "FRM_QUE_ADD";
        public const string FRM_QUE_SAVE = "FRM_QUE_SAVE";
        public const string FRM_QUE_REMOVE = "FRM_QUE_REMOVE";



        public const string FRM_ANSOPT_ADD = "FRM_ANSOPT_ADD";
        public const string FRM_ANSOPT_SAVE = "FRM_ANSOPT_SAVE";
        public const string FRM_ANSOPT_REMOVE = "FRM_ANSOPT_REMOVE";

        public const string FRM_APP_REV_ADD = "FRM_APP_REV_ADD";
        public const string FRM_APP_REV_SAVE = "FRM_APP_REV_SAVE";
        public const string FRM_APP_REV_REMOVE = "FRM_APP_REV_REMOVE";

        public const string FRM_APP_REV_REVIEWER_ADD = "FRM_APP_REV_REVIEWER_ADD";
        public const string FRM_APP_REV_REVIEWER_SAVE = "FRM_APP_REV_REVIEWER_SAVE";
        public const string FRM_APP_REV_REVIEWER_REMOVE = "FRM_APP_REV_REVIEWER_REMOVE";

        public const string FRM_APP_REV_QUE_ADD = "FRM_APP_REV_QUE_ADD";
        public const string FRM_APP_REV_QUE_SAVE = "FRM_APP_REV_QUE_SAVE";
        public const string FRM_APP_REV_QUE_REMOVE = "FRM_APP_REV_QUE_REMOVE";

        public const string FRM_APP_REV_ROUND = "FRM_APP_REV_ROUND";

        public const string FRM_APP_ROUND_TYPE = "FRM_VACRND_RndType";

        public const string FRM_APP_REV_WEIGHT = "FRM_APP_REV_WEIGHT";

        public const string FRM_SAVE_REVIEWER = "FRM_SAVE_REVIEWER";//Save Reviewers

        public const string FRM_SAVE_VAC_TEMPLATE = "FRM_SAVE_VAC_TEMPLATE";
        public const string FRM_REMOVE_VAC_TEMPLATE = "FRM_REMOVE_VAC_TEMPLATE";
        public const string FRM_VAC_NEW_ROUND = "FRM_VAC_NEW_ROUND";

        public const string FRM_VAC_RESUME = "FRM_VAC_RESUME";
        public const string FRM_VAC_COVERLETTER = "FRM_VAC_COVERLETTER";
        public const string FRM_VAC_FULLPROFILE = "FRM_VAC_FULLPROFILE";

        public const string FRM_CREATED_BY = "FRM_CREATED_BY";
        public const string FRM_CREATED_ON = "FRM_CREATED_ON";
        public const string FRM_UPDATED_BY = "FRM_UPDATED_BY";
        public const string FRM_UPDATED_ON = "FRM_UPDATED_ON";

        public const string DEFAULT_WEIGHT = "DEFAULT_WEIGHT";

        public const string SUBJECT = "SUBJECT";
        public const string EMAIL_DESCRIPTION = "EMAIL_DESCRIPTION";
    }

    public class DropDownConstant
    {
        public const string DRP_SELECT_ONE = "DRP_Select_One";
        public const string DRP_ANY = "DRP_Any";
        public const string DRP_PLEASE_SELECT = "DRP_PLEASE_SELECT";
        public const string DRP_SELECT = "DRP_SELECT";
    }

    public class ModelConstant
    {
        public const string MDL_VACANCY = "MDL_Vacancy";
        public const string MDL_VACANCIES = "MDL_Vacancies";
        public const string MDL_USER_PRIVILEGE = "MDL_User_Privilege";
        public const string MDL_DIVISION = "MDL_Division";
        public const string MDL_DIVISIONS = "MDL_Divisions";
        public const string MDL_POSITION_TYPE = "MDL_Position_Type";
        public const string MDL_JOB_LOCATION = "MDL_Job_Location";
        public const string MDL_DIVISION_POSITION_TYPE = "MDL_Division_Position_type";
        public const string MDL_USER_DIVISION = "MDL_User_Division";
        public const string MDL_USER_POSITION_TYPE = "MDL_User_Position_Type";
        public const string MDL_USER_LOCATION = "MDL_User_Location";
        public const string MDL_MYDOCUMENTS = "MDL_MYDOCUMENTS";
        public const string MDL_MYAPPLICATIONS = "MDL_MYAPPLICATIONS";
        public const string MDL_CHANGE_STATUS = "MDL_CHANGE_STATUS";


    }
    public class SystemEntityConstant
    {
        public const string SE_PORTAL_CONTENT = "SE_Portal_Content";
        public const string SE_VACANCY_TEMPLATE = "SE_Vacancy_Template";
        public const string SE_APPLICATION = "SE_Application";
        public const string SE_SECURITY_ROLE = "SE_Security_Role";
        public const string SE_JOB_LOCATION = "SE_Job_Location";
        public const string SE_RATING = "SE_Rating";
        public const string SE_USER = "SE_User";
        public const string SE_INTERVIEW = "SE_Interview";
        public const string SE_QUESTION = "SE_Question";
        public const string SE_VACANCY = "SE_Vacancy";
        public const string SE_VAC_RND = "SE_VAC_RND";
        public const string SE_CANDIDATE = "SE_Candidate";
        public const string SE_RESUME = "SE_Resume";
        public const string SE_COMPANY = "SE_Company";
        public const string SE_JOB_OFFER = "SE_Job_Offer";
        public const string SE_VACANCY_QUESTION = "SE_Vacancy_Question";
        public const string SE_POSITION_TYPE = "SE_Position_Type";
        public const string SE_DIVISION = "SE_Division";
        public const string SE_REVIEW_PANEL = "SE_Review_Panel";
        public const string SE_SCHEDULE_DATE = "SE_SCHEDULE_DATE";
        public const string SE_START_TIME = "SE_START_TIME";
        public const string SE_END_TIME = "SE_END_TIME";
    }
    public class HomeConstant
    {
        public const string CAREER_HOME = "Career_Home";
        public const string SEARCH = "Search";
        public const string UPLOAD_RESUME = "Upload_Resume";
        public const string FEATURED = "Featured";
        public const string FEATURED_JOBS = "FEATURED_JOBS";
        public const string SEARCH_JOBS = "SEARCH_JOBS";

    }
    public class SearchConstant
    {
        public const string FRM_SER_JOB_LOCATION = "FRM_SER_Job_Location";
        public const string FRM_SER_POSITION_TYPE = "FRM_SER_Position_Type";
        public const string FRM_SER_FULLT_PARTT = "FRM_SER_FullT_PartT";
        public const string FRM_SER_EMPLOYEMENT_TYPE = "FRM_SER_Employement_Type";
        public const string FRM_SER_SKILL_RANGE = "FRM_SER_Skill_Range";
        public const string FRM_SER_SALARY_RANGE = "FRM_SER_Salary_Range";
        public const string FRM_SER_DATE_POSTED = "FRM_SER_Date_Posted";
        public const string FRM_SER_TITLE_DESCRIPTION = "FRM_SER_Title_Description";
        public const string FRM_SER_TITLE_LOCATION_1 = "FRM_SER_Title_Location_1";
        public const string FRM_SER_TITLE_LOCATION_2 = "FRM_SER_Title_Location_2";
        public const string FRM_SER_SEARCH_RESULT = "FRM_SER_Search_Result";
        public const string FRM_SER_SEARCH_LISTING = "FRM_SER_Search_Listing";
        public const string FRM_SER_NOTIFY_ME = "FRM_SER_Notify_Me";
        public const string FRM_SER_SET_DEFAULT = "FRM_SER_Set_Default";
    }
    public class LoginConstant
    {
        public const string LOGIN = "Login";
        public const string SIGNUP = "Signup";
        public const string FRM_EMAIL_ADDRESS = "FRM_Email_Address";
        public const string FRM_PASSWORD = "FRM_Password";
        public const string FRM_REMEMBER_ME = "FRM_Remember_Me";
        public const string FRM_SEND_PASSWORD = "FRM_Send_Password";
        public const string LOGOUT = "Logout";

    }
    public class UploadResumeConstant
    {
        public const string FRM_UPR_TTL_CREATE_PROFILE = "FRM_UPR_TTL_Create_Profile";
        public const string FRM_UPR_PROFILE_NAME = "FRM_UPR_Profile_Name";
        public const string FRM_UPR_FIRST_NAME = "FRM_UPR_First_Name";
        public const string FRM_UPR_LAST_NAME = "FRM_UPR_Last_Name";
        public const string FRM_UPR_USERNAME = "FRM_UPR_Username";
        public const string FRM_UPR_PASSWORD = "FRM_UPR_Password";
        public const string FRM_UPR_CONFIRM_PASSWORD = "FRM_UPR_Confirm_Password";
        public const string FRM_UPR_SELECT_RESUME = "FRM_UPR_Select_Resume";
        public const string FRM_UPR_UPLOAD_RESUME = "FRM_UPR_Upload_Resume";
    }
    public class EmployeeMenuConstant
    {
        public const string MNU_MY_VACANCIES = "MNU_My_Vacancies";
        public const string MNU_MY_CANDIDATES = "MNU_My_Candidates";
        public const string MNU_SEARCH_RESUME = "MNU_Search_Resume";
        public const string MNU_COMPANY_SETUP = "MNU_Company_Setup";
        public const string MNU_MESSAGE = "MNU_Message";
        public const string MNU_INTERVIEW_CALENDER = "MNU_INTERVIEW_CALENDER";
    }
    public class QuestinoLibraryConstant
    {
        public const string FRM_WEIGHT = "FRM_WEIGHT";//Weight
        public const string FRM_QUE = "FRM_QUE";//Question
        public const string FRM_QUE_TYPE = "FRM_QUE_TYPE";//Question Type
        public const string FRM_QUE_DATA_TYPE = "FRM_QUE_DATA_TYPE";//Question Data Type
        public const string FRM_QUE_PRIMARY_DIVISION = "FRM_QUE_PRIMARY_DIVISION";//Primary Division

        public const string YESNO_YES = "YESNO_YES";
        public const string YESNO_NO = "YESNO_NO";
        //CR-9
        public const string FRM_INTEGRATION_KEY = "INTEGRATION_KEY";
    }
    public class AnsOpt
    {
        public const string ANSOPT_ANS = "Answer";//Value;
        public const string ANSOPT_VALUE = "ANSOPT_VALUE";//Value;
    }
    public class VacancyConstant
    {
        public const string LST_VAC_JOB_TITLE = "LST_VAC_Job_Title";
        public const string LST_VAC_LOCATION = "LST_VAC_Location";
        public const string LST_VAC_POSTED_ON = "LST_VAC_Posted_On";
        public const string LST_VAC_ID = "LST_VAC_Id";
        public const string LST_VAC_STATUS = "LST_VAC_Status";
        public const string LST_VAC_APPLICANTS = "LST_VAC_APPLICANTS";
        public const string LST_VAC_VACANCIES = "LST_VAC_Vacancies";
        public const string LST_VAC_OFFERS = "LST_VAC_Offers";
        public const string LST_VAC_PLACEMENT = "LST_VAC_Placement";
        public const string LST_VAC_OWNER = "LST_VAC_OWNER";

        public const string FRM_VAC_JOB_TITLE = "FRM_VAC_Job_title";
        public const string FRM_VAC_POSITION_ID = "FRM_VAC_Position_Id";
        public const string FRM_VAC_VACANCY_STATUS = "FRM_VAC_Vacancy_Status";
        public const string FRM_VAC_STATUS_REASON = "FRM_VAC_STATUS_REASON";
        public const string FRM_VAC_JOB_TYPE = "FRM_VAC_Job_Type";
        public const string FRM_VAC_POSITION_TYPE = "FRM_VAC_Position_Type";
        public const string FRM_VAC_EMPLOYMENT_TYPE = "FRM_VAC_Employment_Type";
        public const string FRM_VAC_DIVISION = "FRM_VAC_Division";
        public const string FRM_VAC_LOCATION = "FRM_VAC_Location";
        public const string FRM_VAC_START_DATE = "FRM_VAC_Start_Date";
        public const string FRM_VAC_END_DATE = "FRM_VAC_End_Date";
        public const string FRM_VAC_TOTAL_POSITIONS = "FRM_VAC_Total_Positions";
        public const string FRM_VAC_REMAING_POSITIONS = "FRM_VAC_Remaing_Positions";
        public const string FRM_VAC_SHOW_ON_WEB = "FRM_VAC_Show_On_Web";
        public const string FRM_VAC_FEATURED_ON_WEB = "FRM_VAC_Featured_On_Web";
        public const string FRM_VAC_POSITION_REQUESTED_BY = "FRM_VAC_Position_Requested_By";
        public const string FRM_VAC_POSITION_OWNER = "FRM_VAC_Position_Owner";

        public const string FRM_VAC_JOB_DESCRIPTION = "FRM_VAC_Job_Description";
        public const string FRM_VAC_DUTIES_AND_RESPONSIBILITY = "FRM_VAC_Duties_And_Responsibility";
        public const string FRM_VAC_SKILL_AND_QUALIFICATION = "FRM_VAC_Skill_And_Qualification";
        public const string FRM_VAC_BENEFITS = "FRM_VAC_Benefits";


        public const string FRM_VAC_COMPENSATION_INFO = "FRM_VAC_Compensation_Info";
        public const string FRM_VAC_ANNUAL_SALARY = "FRM_VAC_Annual_Salary";
        public const string FRM_VAC_SALARY_MIN = "FRM_VAC_Salary_Min";
        public const string FRM_VAC_SALARY_MAX = "FRM_VAC_Salary_Max";
        public const string FRM_VAC_HOURLY_RATE = "FRM_VAC_Hourly_Rate";
        public const string FRM_VAC_HOURLY_MAX = "FRM_VAC_Hourly_Max";
        public const string FRM_VAC_HOURLY_MIN = "FRM_VAC_Hourly_Min";
        public const string FRM_VAC_COMMISSION = "FRM_VAC_Commission";
        public const string FRM_VAC_BONUS_POTENTIALS = "FRM_VAC_Bonus_Potentials";
        public const string FRM_VAC_CLIENT = "FRM_VAC_CLIENT";
        public const string FRM_VAC_HOURS_PER_WEEK = "FRM_VAC_Hrs_Per_Week";
        public const string FRM_VAC_VACANCY_HISTORY = "FRM_VAC_Vacancy_History";
        public const string FRM_VAC_VACANCY_APPLICATIONS = "FRM_VAC_Vacancy_Applications";


        public const string FRM_VAC_CONFIRM_REQUIRED = "FRM_VAC_CONFIRM_REQUIRED";


        //For Vacancy Gear menu Inner
        public const string FRM_VAC_OPEN_VAC = "FRM_VAC_OPEN_VAC";

        public const string FRM_VAC_DRAFT_VAC = "FRM_VAC_DRAFT_VAC";

        public const string FRM_VAC_CLOSE_VAC = "FRM_VAC_CLOSE_VAC";


        public const string FRM_VAC_ARC_VAC = "FRM_VAC_ARC_VAC";

        public const string FRM_VAC_DEL_VAC = "FRM_VAC_DEL_VAC";

        public const string FRM_VAC_CAN_LINK = "FRM_VAC_CAN_LINK";

        public const string FRM_VAC_MAN_LINK = "FRM_VAC_MAN_LINK";

        
        public const string FRM_VAC_ACT_CODE = "FRM_VAC_ACT_CODE";

        public const string FRM_VAC_ANNOUNCE_TYPE = "FRM_VAC_ANNOUNCE_TYPE";

        public const string FRM_VAC_FOA = "FRM_VAC_FOA";

        public const string FRM_VAC_PROGRAM_OFFICER = "FRM_VAC_PROGRAM_OFFICER";

        public const string FRM_VAC_CASH_MATCH_REQ = "FRM_VAC_CASH_MATCH_REQ";

        public const string FRM_VAC_ANNOUNCE_DATE = "FRM_VAC_ANNOUNCE_DATE";

        public const string FRM_VAC_OPEN_DATE = "FRM_VAC_OPEN_DATE";

        public const string FRM_VAC_APP_DUE_DATE = "FRM_VAC_APP_DUE_DATE";

        public const string FRM_VAC_EXP_DATE = "FRM_VAC_EXP_DATE";

        public const string FRM_VAC_FUND_AMOUNT = "FRM_VAC_FUND_AMOUNT";
        //CR-35 MAX AND MIN FUNDING AMMOUNT BY MUNEENDRA START( get and binding the data)
        public const string FRM_VAC_MAX_FUND_AMOUNT = "Max Funding Amount";
        public const string FRM_VAC_MIN_FUND_AMOUNT = "Min Funding Amount";
        public const string FRM_VAC_TOTAL_FUNDED_TODATE = "Total Funded To Date";
        public const string FRM_VAC_TOTAL_NUMBER_AWARDS = "Total Number Of Awards";
        public const string FRM_VAC_REMAINIG_FUNDS = "Remaining Funds";
        //CR-35 END




        //INTERVIEW
        #region INTERVIEW
        public const string FRM_VACRND_ReviewRound = "FRM_VACRND_ReviewRound";
        public const string FRM_VACRND_ReviewRounds = "FRM_VACRND_ReviewRounds";
        public const string FRM_VACRND_RndType = "FRM_VACRND_RndType";
        public const string FRM_VACRND_RndWeight = "FRM_VACRND_RndWeight";
        public const string FRM_VACRND_AssignCandToRev = "FRM_VACRND_AssignCandToRev";
        public const string FRM_VACRND_ReqRev = "FRM_VACRND_ReqRev";
        public const string FRM_VACRND_PromoteCan = "FRM_VACRND_PromoteCan";
        public const string FRM_VACRND_Promothersolscore = "FRM_VACRND_Promothersolscore";
        public const string FRM_VACRND_AssignCanBatches = "FRM_VACRND_AssignCanBatches";


        public const String LST_AC_TITLE_APPLICANT_REVIEW_PROCESS = "LST_AC_TITLE_APPLICANT_REVIEW_PROCESS";
        #endregion

    }
    public class UserPrivilegesConstant
    {
        public const string FRM_UPV_SELECT_USER = "FRM_UPV_Select_User";
        public const string FRM_UPV_SECURITY_ROLES = "FRM_UPV_Security_Roles";
        public const string FRM_UPV_SYSTEM_ENTITY = "FRM_UPV_System_Entity";
        public const string FRM_UPV_PERMISSION_TYPE = "FRM_UPV_Permission_Type";
        public const string FRM_UPV_CREATE = "FRM_UPV_Create";
        public const string FRM_UPV_VIEW = "FRM_UPV_View";
        public const string FRM_UPV_MODIFY = "FRM_UPV_Modify";
        public const string FRM_UPV_DELETE = "FRM_UPV_Delete";
        public const string FRM_UPV_ALL_DIVISION = "FRM_UPV_All_Division";
        public const string FRM_kUPV_SISTER_DIVISION = "FRM_UPV_Sister_Division";
        public const string FRM_UPV_CHILD_DIVISION = "FRM_UPV_Child_Division";
        public const string FRM_UPV_OWN_DIVISION = "FRM_UPV_Own_Division";
    }
    public class ErrorConstant
    {
        public const string FRM_VAL_REQUIRED = "FRM_VAL_Required";
        public const string FRM_VAL_LENGTH = "FRM_VAL_Length";
        public const string FRM_VAL_REGEX = "FRM_VAL_Regex";

    }
    public class DivisionConstant
    {
        public const string LST_DIV_NAME = "LST_DIV_Name";
        public const string LST_DIV_PDIVISION = "LST_DIV_PDivision";
        public const string COMMON = "Common";

        public const string FRM_DIV_DEFAULT_NAME = "FRM_DIV_Default_Name";
        public const string FRM_DIV_PARENT_DIVISION = "FRM_DIV_Parent_Division";
        public const string FRM_DIVISION_REQUIRED_MSG = "FRM_DIVISION_REQUIRED_MSG";

    }
    public class PersonalInfoConstant
    {
        public const string FRM_PRF_TTL_PERSONAL_INFO = "FRM_PRF_TTL_Personal_Info";
        public const string FRM_PRF_FIRST_NAME = "FRM_PRF_First_Name";
        public const string FRM_PRF_MIDDLE_NAME = "FRM_PRF_Middle_Name";
        public const string FRM_PRF_LAST_NAME = "FRM_PRF_Last_Name";
        //public const string FRM_PRF_DOB = "FRM_PRF_DOB";
        public const string FRM_PRF_ADDRESS = "FRM_PRF_Address";
        public const string FRM_PRF_CITY = "FRM_PRF_City";
        public const string FRM_PRF_STATE = "FRM_PRF_State";
        public const string FRM_PRF_ZIP = "FRM_PRF_Zip";
        public const string FRM_PRF_BUSINESS_PHONE_NO = "FRM_PRF_Business_Phone_No";
        public const string FRM_PRF_HOME_PHONE = "FRM_PRF_Home_Phone";
        public const string FRM_PRF_MOBILE_PHONE = "FRM_PRF_Mobile_Phone";
        public const string FRM_PRF_PAGER = "FRM_PRF_Pager";
        public const string FRM_PRF_HOME_EMAIL = "FRM_PRF_Home_Email";
        public const string FRM_PRF_WORK_EMAIL = "FRM_PRF_Work_Email";
        public const string FRM_PRF_TTL_EMERGANCY = "FRM_PRF_TTL_Emergancy";
        public const string FRM_PRF_EMERGANCY_CONTACT1 = "FRM_PRF_Emergancy_Contact1";
        public const string FRM_PRF_EMERGANCY_CONTACT1_PHONE = "FRM_PRF_Emergancy_Contact1_Phone";
        public const string FRM_PRF_EMERGANCY_CONTACT2 = "FRM_PRF_Emergancy_Contact2";
        public const string FRM_PRF_EMERGANCY_CONTACT2_PHONE = "FRM_PRF_Emergancy_Contact2_Phone";
        public const string FRM_PRF_TTL_OTHER_INFORMATION = "FRM_PRF_TTL_Other_Information";
        public const string FRM_PRF_MISDEMEANOR = "FRM_PRF_Misdemeanor";
        public const string FRM_PRF_MISDEMEANOR_EXPLAIN = "FRM_PRF_Misdemeanor_Explain";
        public const string FRM_PRF_MILITARY_SERVICE = "FRM_PRF_Military_Service";
        public const string FRM_PRF_MILITARY_TYPE_DISCHARGE = "FRM_PRF_Military_Type_Discharge";
        public const string FRM_PRF_AFFIX = "FRM_PRF_AFFIX";
        public const string FRM_PRF_FAX = "FRM_PRF_FAX";
        public const string FRM_PRF_WEBSITE = "FRM_PRF_WEBSITE";
        public const string FRM_PRF_POSTALCODE = "FRM_PRF_POSTALCODE";
        public const string FRM_PRF_POSTOFFICEBOXNO = "FRM_PRF_POSTOFFICEBOXNO";
    }

    public class Credential
    {
        public const string FRM_PRF_CREDENTIAL_USERNAME = "FRM_PRF_Credential_UserName";
        public const string FRM_PRF_CREDENTIAL_PASSWORD = "FRM_PRF_Credential_Password";
        public const string FRM_PRF_CREDENTIAL_ISACTIVE = "FRM_PRF_Credential_IsActive";
    }

    public class AvailabilityConstant
    {
        public const string FRM_PRF_TTL_AVAILIBILITY = "FRM_PRF_TTL_Availibility";
        public const string FRM_PRF_DATE_AVAILABILITY = "FRM_PRF_Date_Availability";
        public const string FRM_PRF_TARGET_INCOME = "FRM_PRF_Target_Income";
        public const string FRM_PRF_HOURS_AVAILIBILITY = "FRM_PRF_Hours_Availibility";
        public const string FRM_PRF_HRS_MON = "FRM_PRF_Hrs_Mon";
        public const string FRM_PRF_HRS_TUE = "FRM_PRF_Hrs_Tue";
        public const string FRM_PRF_HRS_WED = "FRM_PRF_Hrs_Wed";
        public const string FRM_PRF_HRS_THU = "FRM_PRF_Hrs_Thu";
        public const string FRM_PRF_HRS_FRI = "FRM_PRF_Hrs_Fri";
        public const string FRM_PRF_HRS_SAT = "FRM_PRF_Hrs_Sat";
        public const string FRM_PRF_HRS_SUN = "FRM_PRF_Hrs_Sun";
        public const string FRM_PRF_EMPLOYMENT_PREFERENCE = "FRM_PRF_Employment_Preference";
        public const string FRM_PRF_WILLING_TO_WORK_OVERTIME = "FRM_PRF_Willing_To_Work_Overtime";
        public const string FRM_PRF_RELATIVES_WORKING_IN_COMPANY = "FRM_PRF_Relatives_Working_In_Company";
        public const string FRM_PRF_RELATIVES_IF_YES = "FRM_PRF_Relatives_If_Yes";
        public const string FRM_PRF_SUBMITTED_APPLICATION_BEFORE = "FRM_PRF_Submitted_Application_Before";
        public const string FRM_PRF_APPLICATION_SUBMISION = "FRM_PRF_Application_Submision";
        public const string FRM_PRF_HEAR_ABOUT_THE_POSITION = "FRM_PRF_Hear_About_The_Position";
        public const string FRM_PRF_REFFERED_BY = "FRM_PRF_Reffered_By";
        public const string FRM_PRF_HOW_OLD = "FRM_PRF_How_Old";
        public const string FRM_PRF_ELIGIBLE_TO_WORK_IN_US = "FRM_PRF_Eligible_To_Work_In_Us";

    }
    public class RecordOfEmployementConstant
    {
        public const string FRM_PRF_TTL_RECORD_OF_EMPLOYMENT = "FRM_PRF_TTL_Record_Of_Employment";
        public const string FRM_PRF_COMPANY_DETAILS = "FRM_PRF_Company_Details";
        public const string FRM_PRF_COMPANY_NAME = "FRM_PRF_Company_Name";
        public const string FRM_PRF_TELEPHONE = "FRM_PRF_Telephone";
        public const string FRM_PRF_SUPERVISOR_NAME = "FRM_PRF_Supervisor_Name";
        public const string FRM_PRF_CITY = "FRM_PRF_City";
        public const string FRM_PRF_ADDRESS = "FRM_PRF_Address";
        public const string FRM_PRF_ZIP = "FRM_PRF_Zip";
        public const string FRM_PRF_STATE = "FRM_PRF_State";
        public const string FRM_PRF_END_DATE = "FRM_PRF_End_Date";
        public const string FRM_PRF_START_DATE = "FRM_PRF_Start_Date";
        public const string FRM_PRF_MOST_RECENT_POSITION = "FRM_PRF_Most_Recent_position";
        public const string FRM_PRF_STARTING_POSITION = "FRM_PRF_Starting_Position";
        public const string FRM_PRF_ENDING_PAY = "FRM_PRF_Ending_Pay";
        public const string FRM_PRF_STARTING_PAY = "FRM_PRF_Starting_Pay";
        public const string FRM_PRF_REASON_FOR_LEAVING = "FRM_PRF_Reason_For_Leaving";
        public const string FRM_PRF_DUTIES_AND_RESPONSIBILITES = "FRM_PRF_Duties_And_Responsibilites";
        public const string FRM_PRF_PROJECT_DETAILS = "FRM_PRF_Project_Details";
        public const string FRM_PRF_PROJECT_NAME = "FRM_PRF_Project_Name";
        public const string FRM_PRF_DESCRIPTION = "FRM_PRF_Description";
        public const string FRM_PRF_TEAM_SIZE = "FRM_PRF_Team_Size";
        public const string FRM_PRF_START_MONTH = "FRM_PRF_START_MONTH";
        public const string FRM_PRF_END_MONTH = "FRM_PRF_END_MONTH";
        public const string FRM_PRF_END_YEAR = "FRM_PRF_END_YEAR";
        public const string FRM_PRF_START_YEAR = "FRM_PRF_START_YEAR";
        public const string FRM_PRF_EXPERIENCE = "FRM_PRF_EXPERIENCE";
        public const string FRM_PRF_JOBCATEGORY = "FRM_PRF_JOBCATEGORY";
        public const string FRM_PRF_MAYWECONTACT = "MAYWECONTACT";

        public const string FRM_PRF_STARTMONTHYEAR = "FRM_PRF_STARTMONTHYEAR";
        public const string FRM_PRF_ENDMONTHYEAR = "FRM_PRF_ENDMONTHYEAR";





    }
    public class EducationConstant
    {
        public const string FRM_PRF_TTL_EDUCATION = "FRM_PRF_TTL_Education";
        public const string FRM_PRF_INSTITUTION_NAME = "FRM_PRF_Institution_Name";
        public const string FRM_PRF_START_DATE_TEXT = "FRM_PRF_Start_Date_Text";
        public const string FRM_PRF_END_DATE_TEXT = "FRM_PRF_End_Date_Text";
        public const string FRM_PRF_START_DATE = "FRM_PRF_Start_Date";
        public const string FRM_PRF_END_DATE = "FRM_PRF_End_Date";

        public const string FRM_PRF_DEGREE_TYPE = "FRM_PRF_Degree_Type";
        public const string FRM_PRF_MAJOR_SUBJECT = "FRM_PRF_Major_Subject";
        public const string FRM_PRF_EDUCITY = "FRM_PRF_EDUCITY";
        public const string FRM_PRF_EDUSTATE = "FRM_PRF_EDUSTATE";
        public const string FRM_PRF_COUNTRY = "FRM_PRF_COUNTRY";
        public const string FRM_PRF_EDUDESCRIPTION = "FRM_PRF_EDUDESCRIPTION";
        public const string FRM_PRF_DEGREEDATE = "FRM_PRF_DEGREEDATE";
        public const string FRM_PRF_MEASUREVALUE = "FRM_PRF_MEASUREVALUE";
        public const string FRM_PRF_MEASURESYSTEM = "FRM_PRF_MEASURESYSTEM";


    }
    public class SkillAndQualificationConstant
    {
        public const string FRM_PRF_TTL_SKILLS_AND_QUALIFICATIONS = "FRM_PRF_TTL_Skills_And_Qualifications";
        public const string FRM_PRF_SKILL_TYPE = "FRM_PRF_Skill_Type";
        public const string FRM_PRF_SKILL_AND_QUALIFICATION = "FRM_PRF_Skill_And_Qualification";
        public const string FRM_PRF_DESCRIPTION = "FRM_PRF_Description";

        public const string FRM_PRF_PROFICIENCY = "FRM_PRF_PROFICIENCY";
        public const string FRM_PRF_LEVEL = "FRM_PRF_LEVEL";
        public const string FRM_PRF_LASTUSED = "FRM_PRF_LASTUSED";
        public const string FRM_PRF_SKILLEXPERIENCE = "FRM_PRF_SKILLEXPERIENCE";


    }
    public class ReferencesConstant
    {
        public const string FRM_PRF_TTL_REFERENCES = "FRM_PRF_TTL_References";
        public const string FRM_PRF_REFERENCE_NAME = "FRM_PRF_Reference_Name";
        public const string FRM_PRF_RELATIONSHIP = "FRM_PRF_RelationShip";
        public const string FRM_PRF_REFERENCE_PHONE = "FRM_PRF_Reference_Phone";
        public const string FRM_PRF_REFERENCE_EMAIL = "FRM_PRF_Reference_Email";
    }

    public class ResumeConstant
    {
        public const string FRM_Res_Upload_Cover = "FRM_Res_Upload_Cover";
        public const string FRM_Res_Upload_Resume = "FRM_Res_Upload_Resume";
        public const string FRM_Res_UseExisting = "FRM_Res_UseExisting";
        public const string FRM_Res_UseExisting_Profile = "FRM_Res_UseExisting_Profile";
        public const string FRM_Res_Apply = "FRM_Res_Apply";
    }

    public class DivisionPositionType
    {
        public const string FRM_DIVPOS_DIVNAME = "FRM_DIVPOS_DIVNAME";
        public const string FRM_DIVPOS_POSNAME = "FRM_DIVPOS_POSNAME";
        public const string LST_DIVPOS_DIVNAME = "LST_DIVPOS_DIVNAME";
        public const string LST_DIVPOS_POSNAME = "LST_DIVPOS_POSNAME";

    }

    public class Company
    {
        public const string FRM_COMPANY_COMNAME = "FRM_COMPANY_COMNAME";
        public const string FRM_COMPANY_PHNO = "FRM_COMPANY_PHNO";
        public const string FRM_COMPANY_ADD = "FRM_COMPANY_ADD";
        public const string FRM_COMPANY_WEBSITE = "FRM_COMPANY_WEBSITE";
        public const string FRM_COMPANY_SUFF = "FRM_COMPANY_SUFF";
        public const string LST_COMPANY_COMNAME = "LST_COMPANY_COMNAME";
        public const string LST_COMPANY_WEBSITE = "LST_COMPANY_WEBSITE";

    }
    public class OnboardManagers
    {
        public const string ONBOARDING_MANAGER = "ONBOARDING_MANAGER";
    }

    public class ClientLabel
    {
        public const string FRM_CLIENT_CLIENTNAME = "FRM_CLIENT_CLIENTNAME";
        public const string FRM_CLIENT_CONTACTPERSON = "FRM_CLIENT_CONTACTPERSON";
        public const string FRM_CLIENT_CONTACTNO = "FRM_CLIENT_CONTACTNO";
        public const string FRM_CLIENT_SUBDOMAIN = "FRM_CLIENT_SUBDOMAIN";
        public const string FRM_CLIENT_WEBSITE = "FRM_CLIENT_WEBSITE";
        public const string FRM_CLIENT_CONNECTIONSTRING = "FRM_CLIENT_CONNECTIONSTRING";
        public const string FRM_CLIENT_DBNAME = "FRM_CLIENT_DBNAME";
        public const string FRM_CLIENT_STARTDATE = "FRM_CLIENT_STARTDATE";
        public const string FRM_CLIENT_ENDDATE = "FRM_CLIENT_ENDDATE";
        public const string FRM_CLIENT_USERLIMIT = "FRM_CLIENT_USERLIMIT";
        public const string FRM_CLIENT_LANGUAGE = "FRM_CLIENT_LANGUAGE";
        public const string FRM_CLIENT = "FRM_Client";
    }

    public class SubscriptionLabel
    {
        public const string FRM_SUBSCRIPTION_PRICE = "FRM_Subscription_Price";
        public const string FRM_SUBSCRIPTION = "FRM_Subscription";
    }

    public class LanguageLabel
    {
        public const string FRM_LANGUAGE = "FRM_Language";
        public const string FRM_LANGUAGE_CULTURE = "FRM_Language_Culture";
        public const string FRM_LANGUAGE_UPLOADFILE = "FRM_Language_UploadFile";
    }

    public class CurrencySign
    {
        public const string FRM_CurrencySign = "FRM_CurrencySign";
    }

    public class UserDivisionPermission
    {
        public const string FRM_USER_DIV_DIVNAME = "FRM_USER_DIV_DIVNAME";
        public const string FRM_USER_DIV_USERNAME = "FRM_USER_DIV_USERNAME";
        public const string LST_USER_DIV_DIVNAME = "LST_USER_DIV_DIVNAME";
        public const string LST_USER_DIV_USERNAME = "LST_USER_DIV_USERNAME";



    }

    public class UserPositiontypePermissionConstant
    {

        public const string FRM_USER_POS_POSNAME = "FRM_USER_POS_POSNAME";

        public const string FRM_USER_POS_USERNAME = "FRM_USER_POS_USERNAME";


        public const string LST_USER_POS_DIVNAME = "LST_USER_POS_POSNAME";

        public const string LST_USER_POS_USERNAME = "LST_USER_POS_USERNAME";
    }


    public class UserLocationPermissionConstant
    {
        public const string FRM_USER_LOC_LOC = "FRM_USER_LOC_LOC";
        public const string FRM_USER_LOC_USERNAME = "FRM_USER_LOC_USERNAME";
        public const string LST_USER_LOC_LOC = "LST_USER_LOC_LOC";

        public const string LST_USER_LOC_USERNAME = "LST_USER_LOC_USERNAME";



    }


    public class JobLocation
    {
        public const string FRM_JOB_DIVNAME = "FRM_JOB_DIVNAME";
        public const string LST_JOB_JOBNAME = "LST_JOB_JOBNAME";
        public const string LST_JOB_DIVNAME = "LST_JOB_DIVNAME";
        public const string FRM_JOB_LOC_MANAGER = "FRM_JOB_LOC_MANAGER";
        public const string FRM_JOB_ONBOARDING_MANAGER = "FRM_JOB_ONBOARDING_MANAGER";


    }

    public class PositionType
    {

        public const string LST_POS_POSNAME = "LST_POS_POSNAME";
        public const string LST_JOB_JOBNAME = "LST_JOB_JOBNAME";
        public const string LST_JOB_DIVNAME = "LST_JOB_DIVNAME";
    }

    public class UserPrivilegesConst
    {
        public const string FRM_USER_PRIV_TTL_SELECTUSER = "FRM_USER_PRIV_TTL_SELECTUSER";
        public const string FRM_USER_PRIV_TTL_SPERINFO = "FRM_USER_PRIV_TTL_SPERINFO";
        public const string FRM_USER_PRIV_TTL_SECROLES = "FRM_USER_PRIV_TTL_SECROLES";

        public const string FRM_USER_PRIV_TTL_SELECTSECROLES = "FRM_USER_PRIV_TTL_SELECTSECROLES";
        public const string FRM_USER_PRIV_TTL_SYSTEMENTITY = "FRM_USER_PRIV_TTL_SYSTEMENTITY";
        public const string FRM_USER_PRIV_TTL_CREATE = "FRM_USER_PRIV_TTL_CREATE";
        public const string FRM_USER_PRIV_TTL_VIEW = "FRM_USER_PRIV_TTL_VIEW";
        public const string FRM_USER_PRIV_TTL_MODIFY = "FRM_USER_PRIV_TTL_MODIFY";
        public const string FRM_USER_PRIV_TTL_DELETE = "FRM_USER_PRIV_TTL_DELETE";

        public const string FRM_USER_PRIV_BUT_ADDUSER = "FRM_USER_PRIV_BUT_ADDUSER";


    }

    public class UserConst
    {

        public const string FRM_USER_SELECTDIVISION = "FRM_USER_SELECTDIVISION";
        public const string FRM_USER_SELECTADHOCDIVISION = "FRM_USER_SELECTADHOCDIVISION";
        public const string FRM_USER_SELECTJOBLOCATION = "FRM_USER_SELECTJOBLOCATION";
        public const string FRM_USER_SELECTPOSITIONTYPE = "FRM_USER_SELECTPOSITIONTYPE";



        public const string FRM_USER_TTL_PERINFO = "FRM_USER_TTL_PERINFO";
        public const string FRM_USER_TTL_CREDENTIAL = "FRM_USER_TTL_CREDENTIAL";
        public const string FRM_USER_TTL_LOGIN_DETAILS = "FRM_USER_TTL_LOGIN_DETAILS";
        public const string FRM_USER_TTL_DIVISION = "FRM_USER_TTL_DIVISION";
        public const string FRM_USER_TTL_SECURITYROLE = "FRM_USER_TTL_SECURITYROLE";

        public const string LST_USER_NAME = "LST_USER_NAME";
        public const string LST_USER_USERNAME = "LST_USER_USERNAME";
        public const string LST_USER_DIVISION = "LST_USER_DIVISION";

    }

    public class ApplicationDetailsConstant
    {

        public const string LST_APP_COVERLETTER = "LST_APP_COVERLETTER";
        public const string LST_APP_RESUME = "LST_APP_RESUME";
        public const string LST_APP_PROFILE = "LST_APP_PROFILE";


    }

    public class DocumentsDetailsConst
    {
        public const string LST_DOC_FILE_NAME = "LST_DOC_FILE_NAME";
        public const string LST_DOC_DOC_TYPE = "LST_DOC_DOC_TYPE";
        public const string LST_DOC_DATE_UPLOADED = "LST_DOC_DATE_UPLOADED";

        public const string LST_DOC_TTL_PREVIEW = "LST_DOC_TTL_PREVIEW";


    }

    public class UploadResumeConst
    {
        public const string FRM_UPRESUME_CREATENEW = "FRM_UPRESUME_CREATENEW";
        public const string FRM_UPRESUME_UPDATEEXISTING = "FRM_UPRESUME_UPDATEEXISTING";
        public const string FRM_UPRESUME_SELECTRESUME = "FRM_UPRESUME_SELECTRESUME";

        public const string FRM_UPRESUME_DTTL_CHANGEPRO = "FRM_UPRESUME_DTTL_CHANGEPRO";
        public const string FRM_SELECT = "FRM_SELECT";



    }

    public class ProfileConstant
    {
        public const string FRM_PRO_PROFILENAME = "FRM_PRO_PROFILENAME";


    }


    public class CandidateMenuConstant
    {//CR-2
        public const string MNU_PEOPLE = "MNU_PEOPLE";
        public const string MNU_ORGANIZATIONS = "MNU_ORGANIZATIONS";
        public const string MNU_MYPROFILES = "MNU_MYPROFILES";
        public const string MNU_MYDOCUMENTS = "MNU_MYDOCUMENTS";
        public const string MNU_MYAPPLICATIONS = "MNU_MYAPPLICATIONS";
        public const string MNU_HOME = "MNU_HOME";
        public const string MNU_MYVACANCIES = "MNU_MYVACANCIES";
        public const string MNU_COMPANYSETUP = "MNU_COMPANYSETUP";
        public const string MNU_SEARCHJOBS = "MNU_SEARCHJOBS";
        public const string MNU_FEATURED_JOBS = "FEATURED_JOBS";
        public const string MNU_SEARCH_JOBS = "SEARCH_JOBS";
    }

    public class CompanySetupMenu
    {
        public const string CSMNU_USERPRIVILEGES = "CSMNU_USERPRIVILEGES";

        public const string CSMNU_COMPANYINFO = "CSMNU_COMPANYINFO";

        public const string CSMNU_DIVISION = "CSMNU_DIVISION";

        public const string CSMNU_POSITIONTYPE = "CSMNU_POSITIONTYPE";

        public const string CSMNU_JOBLOCATION = "CSMNU_JOBLOCATION";

        public const string CSMNU_DIVISION_POSITIONTYPE = "CSMNU_DIVISION_POSITIONTYPE";

        public const string CSMNU_USER_DIVISION = "CSMNU_USER_DIVISION";

        public const string CSMNU_USER_POSITIONTYPE = "CSMNU_USER_POSITIONTYPE";

        public const string CSMNU_USER_LOCATION = "CSMNU_USER_LOCATION";

        public const string CSMNU_USER = "CSMNU_USER";

        public const string CSMNU_CLIENT = "CSMNU_CLIENT";

        public const string CSMNU_DEGREETYPE = "CSMNU_DEGREETYPE";
        public const string CSMNU_SKILLTYPE = "CSMNU_SKILLTYPE";
        public const string CSMNU_VACANCYSTATUS = "CSMNU_VACANCYSTATUS";
        public const string CSMNU_VACANCYQUESTIONTYPE = "CSMNU_VACANCYQUESTIONTYPE";
        public const string CSMNU_RNDTYPE = "CSMNU_RNDTYPE";
        public const string CSMNU_QUECAT = "CSMNU_QUECAT";
        public const string CSMNU_QUE = "CSMNU_QUE";
        public const string CSMNU_STATUSREASON = "CSMNU_STATUSREASON";
        public const string CSMNU_APPBASEDSTATUS = "CSMNU_APPBASEDSTATUS";
        public const string CSMNU_LANGUAGEBLOCKS = "CSMNU_LANGUAGEBLOCKS";

        public const string CSMNU_VACANCYTEMPLATES = "CSMNU_VACANCYTEMPLATES";

        public const string CSMNU_DECLINESTATUS = "CSMNU_DECLINESTATUS";

        public const string CSMNU_OFFERLETTER = "CSMNU_OFFERLETTER";

        public const string CSMNU_OFFERTEMPLATE = "CSMNU_OFFERTEMPLATE";

        public const string CSMNU_EMAILTEMPLATES = "CSMNU_EMAILTEMPLATES";
        public const string CSMNU_REVIEWROUNDS = "CSMNU_REVIEWROUNDS";
        public const string CSMNU_DOCUMENTTYPES = "CSMNU_DOCUMENTTYPES";
        public const string CSMNU_COLOROPTIONS = "CSMNU_COLOROPTIONS";

        public const string CSMNU_SECURITY_TEMPLATES = "CSMNU_SECURITY_TEMPLATES";
    }

    public class DegreeType
    {
        public const string LST_DEREETYPE = "LST_DEREETYPE";
    }

    public class VacancyQuestionType
    {
        public const string LST_VACANCYQUESTIONTYPE = "LST_VACANCYQUESTIONTYPE";
        public const string LST_TYPE = "LST_TYPE";


    }

    public class QueCat
    {
        public const string LST_QUECATNAME = "LST_QUECATNAME";
        public const string LST_ORDER = "LST_ORDER";


    }

    public class RoundType
    {
        public const string ROUND_NAME = "ROUND_NAME";
        public const string LST_ROUNDTYPE = "LST_ROUNDTYPE";
        public const string LST_TYPE = "LST_TYPE";
        public const string LST_SHOWTOCANDIDATE = "FRM_SHOWTOCANDIDATE";
        public const string LST_REVIEWROUNDS = "LST_REVIEWROUNDS";

    }
    public class SkillType
    {
        public const string LST_SKILLTYPE = "LST_SKILLTYPE";

    }

    public class ExecutiveSummaryConstant
    {
        public const string FRM_EXECUTIVESUMMARY = "FRM_EXECUTIVESUMMARY";
        public const string FRM_TITL_EXECUTIVESUMMARY = "FRM_TITL_EXECUTIVESUMMARY";

    }
    public class ObjectiveConstant
    {
        public const string FRM_OBJECTIVEDETAILS = "FRM_OBJECTIVEDETAILS";
        public const string FRM_TITL_OBJECTIVEDETAILS = "FRM_TITL_OBJECTIVEDETAILS";

    }

    public class LicenceAndCertificationsConstant
    {

        public const string FRM_TITL_LICENCEANDCERTIFICATIONS = "FRM_TITL_LICENCEANDCERTIFICATIONS";
        public const string FRM_NAME = "FRM_NAME";


        public const string FRM_ISSUINGAUTHORITY = "FRM_ISSUINGAUTHORITY";

        public const string FRM_DESCRIPTION = "FRM_DESCRIPTION";

        public const string FRM_VALIDFROM = "FRM_VALIDFROM";

        public const string FRM_VALIDTO = "FRM_VALIDTO";





    }

    public class PublicationHistoryConstant
    {
        public const string FRM_PUB_TITLE = "FRM_PUB_TITLE";

        public const string FRM_PUB_TYPE = "FRM_PUB_TYPE";

        public const string FRM_PUB_ROLE = "FRM_PUB_ROLE";

        public const string FRM_PUB_NAME = "FRM_PUB_NAME";

        public const string FRM_PUB_PUBLICATIONDATE = "FRM_PUB_PUBLICATIONDATE";

        public const string FRM_PUB_DESCRIPTION = "FRM_PUB_DESCRIPTION";

        public const string FRM_PUB_COMMENTS = "FRM_PUB_COMMENTS";

        public const string FRM_PUB_TITL_PUBLICATION = "FRM_PUB_TITL_PUBLICATION";



    }
    public class SpeakingEventHistoryConst
    {
        public const string FRM_SPK_TITLE = "FRM_SPK_TITLE";

        public const string FRM_SPK_STARTDATE = "FRM_SPK_STARTDATE";

        public const string FRM_SPK_EVENTNAME = "FRM_SPK_EVENTNAME";

        public const string FRM_SPK_EVENTTYPE = "FRM_SPK_EVENTTYPE";

        public const string FRM_SPK_LOCATION = "FRM_SPK_LOCATION";

        public const string FRM_SPK_LINK = "FRM_SPK_LINK";

        public const string FRM_SPK_TITL_SPEAKINGEVENTHISTORY = "FRM_SPK_TITL_SPEAKINGEVENTHISTORY";

    }

    public class LanguagesConst
    {
        public const string FRM_LAN_TITLE_LANGUAGES = "FRM_LAN_TITLE_LANGUAGES";


        public const string FRM_LAN_LANGUAGECODE = "FRM_LAN_LANGUAGECODE";

        public const string FRM_LAN_READ = "FRM_LAN_READ";

        public const string FRM_LAN_WRITE = "FRM_LAN_WRITE";

        public const string FRM_LAN_SPEAK = "FRM_LAN_SPEAK";

        public const string FRM_LAN_COMMENTS = "FRM_LAN_COMMENTS";


    }

    public class AchievementConst
    {
        public const string FRM_ACH_TITL_ACHIEVEMENT = "FRM_ACH_TITL_ACHIEVEMENT";


        public const string FRM_ACH_DATE = "FRM_ACH_DATE";

        public const string FRM_ACH_DESCRIPTION = "FRM_ACH_DESCRIPTION";

        public const string FRM_ACH_ISSUINGAUTHORITY = "FRM_ACH_ISSUINGAUTHORITY";

    }

    public class AssociationsConst
    {
        public const string FRM_ASC_TITLE_ASSOCIATION = "FRM_ASC_TITLE_ASSOCIATION";

        public const string FRM_ASC_ASSOCIATIONTYPE = "FRM_ASC_ASSOCIATIONTYPE";

        public const string FRM_ASC_LINK = "FRM_ASC_LINK";

        public const string FRM_ASC_NAME = "FRM_ASC_NAME";

        public const string FRM_ASC_STARTDATE = "FRM_ASC_STARTDATE";

        public const string FRM_ASC_ENDDATE = "FRM_ASC_ENDDATE";

        public const string FRM_ASC_ROLE = "FRM_ASC_ROLE";


    }

    public class DateFormatConstant
    {
        public const string US_FORMAT = "MM'/'dd'/'yyyy";
        public const string US_CULTURE_INFO = "en-US";
    }

    public class ImagePaths
    {
        public const string UploadResumeImage = "/Content/images/Upload_Resume_24.png";
        public const string FeaturedVacancyImage = "/Content/images/Featured_on_Web_24";
        public const string HomeImage = "/Content/images/Home_button_24.png";
        public const string SearchImage = "/Content/images/Search_Jobs_24.png";

        public const string MyDocumentsImage = "/Content/images/resume_review_24.png";
        public const string MyApplicationsImage = "/Content/images/Upload_Resume_24.png";


        public const string ProfileImage = "/Content/images/My_Profile_24.png";


        public const string MyCandidates = "/Content/images/Review_Panel_24.png";

        public const string VacancyImage = "/Content/images/Search_By_Position_Type_24.png";

        public const string CompanySetupImage = "/Content/images/Company_Setup_24.png";

        public const string SearchResumeImage = "/Content/images/Search_Jobs_24.png";

        public const string SearchJobImage = "/Content/images/My_Applications_24.png";

        public const string SaveSearchResumeImage = "/Content/images/Saved_Search_24.png";
        public const string AddNewImage = "/Content/images/Add_New_24.png";
        public const string SaveImage = "/Content/images/Save_24.png";

        public const string ApplyImage = "/Content/images/Rating_Reminder_24.png";

    }
    public class ApplicationStatusMenuConstant
    {
        public const string APP_STAT_MNU_VIEW = "APP_STAT_MNU_VIEW"; // View Application

        public const string APP_STAT_MNU_DRAFT = "APP_STAT_MNU_DRAFT";//Save as Draft

        public const string APP_STAT_MNU_SUBMIT = "APP_STAT_MNU_SUBMIT";//Submit Application

        public const string APP_STAT_MNU_WITHDRAW = "APP_STAT_MNU_WITHDRAW";//Withdraw Application


    }

    public class ApplicationStatusOptionsConstant
    {
        public const string APP_STAT_VIEW = "APP_STAT_VIEW";

        public const string APP_STAT_EDIT = "APP_STAT_EDIT";

        public const string APP_STAT_DRAFT = "APP_STAT_DRAFT";//Draft

        public const string APP_STAT_SAVELATER = "APP_STAT_SAVELATER";

        public const string APP_STAT_SUBMIT = "APP_STAT_SUBMIT";//Applied

        public const string APP_STAT_INTERV = "APP_STAT_INTERV";//Interview

    }

    public class TooltipConstant
    {
        public const string TOOLTIP_DRAFT = "TOOLTIP_DRAFT";

        public const string TOOLTIP_APPLY_JOB = "TOOLTIP_APPLY_JOB";

        public const string TOOLTIP_SAVE_TO_MY_JOB = "TOOLTIP_SAVE_TO_MY_JOB";

        public const string TOOLTIP_SHOW_ON_WEB = "TOOLTIP_SHOW_ON_WEB";

        public const string TOOLTIP_SHOW_FEATURE = "TOOLTIP_SHOW_FEATURE";

        public const string TOOLTIP_SHOW_NOT_FEATURE = "TOOLTIP_SHOW_NOT_FEATURE";

    }

    public class NewVacancyOption
    {
        public const string NEWVACANCY_CREATE_NEW = "NEWVACANCY_CREATE_NEW";

        public const string NEWVACANCY_CREATE_FROM_TEMPLATE = "NEWVACANCY_CREATE_FROM_TEMPLATE";

        public const string NEWVACANCY_BTN_NEXT = "NEWVACANCY_BTN_NEXT";

    }

    public class MakeOffer
    {
        public const string FRM_MAKEOFF_CANDIDATE = "FRM_MAKEOFF_CANDIDATE";

        public const string FRM_MAKEOFF_OFFDATE = "FRM_MAKEOFF_OFFDATE";

        public const string FRM_MAKEOFF_VACANCY = "FRM_MAKEOFF_VACANCY";

        public const string FRM_MAKEOFF_OFFSTATUS = "FRM_MAKEOFF_OFFSTATUS";

        public const string FRM_MAKEOFF_NOTE_CAN = "FRM_MAKEOFF_NOTE_CAN";

        public const string FRM_MAKEOFF_PLACEMENT_TYPE = "FRM_MAKEOFF_PLACEMENT_TYPE";

        public const string FRM_MAKEOFF_STARTDATE = "FRM_MAKEOFF_STARTDATE";

        public const string FRM_MAKEOFF_ENDDATE = "FRM_MAKEOFF_ENDDATE";

        public const string FRM_MAKEOFF_JOBTYPE = "FRM_MAKEOFF_JOBTYPE";

        public const string FRM_MAKEOFF_HOURSWEEK = "FRM_MAKEOFF_HOURSWEEK";

        public const string FRM_MAKEOFF_PLACEMENT_LOC = "FRM_MAKEOFF_PLACEMENT_LOC";

        public const string FRM_MAKEOFF_NOTIFICATION_METHOD = "FRM_MAKEOFF_NOTIFICATION_METHOD";

        public const string FRM_MAKEOFF_OFFER_DETAILS = "FRM_MAKEOFF_OFFER_DETAILS";

        public const string FRM_MAKEOFF_SALARY_TYPE = "FRM_MAKEOFF_SALARY_TYPE";

        public const string FRM_MAKEOFF_SALARY_OFFERED = "FRM_MAKEOFF_SALARY_OFFERED";

        public const string FRM_MAKEOFF_CHARGE_RATE = "FRM_MAKEOFF_CHARGE_RATE";

        public const string FRM_MAKEOFF_PAY_RATE = "FRM_MAKEOFF_PAY_RATE";

        public const string FRM_MAKEOFF_RATE_PERIOD = "FRM_MAKEOFF_RATE_PERIOD";

        public const string FRM_MAKEOFF_MARKUP_VALUE = "FRM_MAKEOFF_MARKUP_VALUE";

        public const string FRM_MAKEOFF_COMM_DESC = "FRM_MAKEOFF_COMM_DESC";

        public const string FRM_MAKEOFF_COMM_POTENTIAL = "FRM_MAKEOFF_COMM_POTENTIAL";

        public const string FRM_MAKEOFF_COMM_CAP = "FRM_MAKEOFF_COMM_CAP";

        public const string FRM_MAKEOFF_BONUS_DESC = "FRM_MAKEOFF_BONUS_DESC";

        public const string FRM_MAKEOFF_BONUS_POTENTIAL = "FRM_MAKEOFF_BONUS_POTENTIAL";

        public const string FRM_MAKEOFF_BONUS_CAP = "FRM_MAKEOFF_BONUS_CAP";

        public const string FRM_MAKEOFF_CAN_NOTICE_PER = "FRM_MAKEOFF_CAN_NOTICE_PER";

        public const string FRM_MAKEOFF_CAN_IN = "FRM_MAKEOFF_CAN_IN";

        public const string FRM_MAKEOFF_COM_NOTICE_PER = "FRM_MAKEOFF_COM_NOTICE_PER";

        public const string FRM_MAKEOFF_COM_IN = "FRM_MAKEOFF_COM_IN";

        public const string FRM_MAKEOFF_NOTE_APPLICANT = "FRM_MAKEOFF_NOTE_APPLICANT";

        public const string FRM_MAKEOFF_RESPONSE_APPLICANT = "FRM_MAKEOFF_RESPONSE_APPLICANT";

        public const string FRM_MAKEOFF_PLUS_COMM = "FRM_MAKEOFF_PLUS_COMM";

        public const string FRM_MAKEOFF_PLUS_BON = "FRM_MAKEOFF_PLUS_BON";

        public const string FRM_MAKEOFF_EDITABLE_BY_MANAGER = "FRM_MAKEOFF_EDITABLE_BY_MANAGER";

        public const string FRM_MAKEOFF_EDITABLE_BY_CANDIDATE = "FRM_MAKEOFF_EDITABLE_BY_CANDIDATE";

        public const string FRM_MAKEOFF_PICKLIST_SELECTION_EDITABLE_BY_MANAGER = "FRM_MAKEOFF_PICKLIST_SELECTION_EDITABLE_BY_MANAGER";

        public const string FRM_MAKEOFF_EMAIL_CONTENT_EDITABLE_BY_MANAGER = "FRM_MAKEOFF_EMAIL_CONTENT_EDITABLE_BY_MANAGER";

        public const string FRM_MAKEOFF_TEMPLATE_NAME = "FRM_MAKEOFF_TEMPLATE_NAME";

        public const string FRM_MAKEOFF_POSITION_TYPE = "LST_POS_POSNAME";

        public const string FRM_MAKEOFF_EMAIL_TO_CANDIDATE = "FRM_MAKEOFF_EMAIL_TO_CANDIDATE";

        public const string FRM_MAKEOFF_OFFER_LETTER = "FRM_MAKEOFF_OFFER_LETTER";

        public const string FRM_MAKEOFF_ENABLE_COUNTER_OFFER = "FRM_MAKEOFF_ENABLE_COUNTER_OFFER";

        public const string FRM_MAKEOFF_ENABLE_HOURLY_WAGE = "FRM_MAKEOFF_ENABLE_HOURLY_WAGE";

        public const string FRM_MAKEOFF_RATE = "FRM_MAKEOFF_RATE";

        public const string FRM_MAKEOFF_PER = "FRM_MAKEOFF_PER";

        public const string FRM_MAKEOFF_INCLUDE_IN_OFFER = "FRM_MAKEOFF_INCLUDE_IN_OFFER";
        public const string FRM_MAKEOFF_MAX_VALUE = "FRM_MAKEOFF_MAX_VALUE";
    }

    public class OfferLetter
    {
        public const string FRM_OFFERLETTERNAME = "FRM_OFFERLETTERNAME";

        public const string FRM_OFFERDESCRIPTION = "FRM_OFFERDESCRIPTION";

        public const string FRM_OFFER_POSITIONTYPE = "FRM_OFFER_POSITIONTYPE";


    }
    public class ApplicationConstant
    {
        public const string MANAGER = "MANAGER";

        public const string VACANCY = "VACANCY";

        public const string ROUND = "ROUND";

        public const string TRANSFER_CANDIDATES = "TRANSFER_CANDIDATES";
    }
    public class AppBasedStatusConstant
    {
        public const string FRM_APPBASEDSTATUSNAME = "FRM_APPBASEDSTATUSNAME";
        public const string FRM_APPBASEDSTATUSCATEGORY = "FRM_APPBASEDSTATUSCATEGORY";
        public const string FRM_SHOWTOCANDIDATE = "FRM_SHOWTOCANDIDATE";
        public const string FRM_SHOWTOEMPLOYER = "FRM_SHOWTOEMPLOYER";
        public const string FRM_STATUSTEXT = "FRM_STATUSTEXT";
        public const string FRM_EMAILSUBJECT = "FRM_EMAILSUBJECT";
        public const string FRM_EMAILDESCRIPTION = "FRM_EMAILDESCRIPTION";
        public const string FRM_SAVE = "FRM_SAVE";
        public const string FRM_REMOVEDECLINESTATUS = "FRM_REMOVEDECLINESTATUS";

        public const string FRM_DEFAULTSTATUSSHOWTOCANDIDATE = "FRM_DEFAULTSTATUSSHOWTOCANDIDATE";
        public const string FRM_DEFAULTSTATUSSHOWTOEMPLOYER = "FRM_DEFAULTSTATUSSHOWTOEMPLOYER";
    }

    public class InterviewCalenderConstant
    {
        public const string INT_PENDING = "INT_PENDING";
        public const string INT_COMPLETED = "INT_COMPLETED";
        public const string INT_TERMINATED = "INT_TERMINATED";
        public const string INT_ALLPENDING = "INT_ALLPENDING";
        public const string INT_ALLCOMPLETED = "INT_ALLCOMPLETED";
        public const string INT_ALLTERMINATED = "INT_ALLTERMINATED";
    }

    public class VacancyStatusConstant
    {
        public const string FRM_VACSTATUSNAME = "FRM_VACSTATUSNAME";
        public const string FRM_VACSTATUSCATEGORY = "FRM_VACSTATUSCATEGORY";
        public const string VACSTATUS_ALL = "VACSTATUS_ALL"; //do not change VACSTATUS_
        public const string VACSTATUS_DRAFT = "VACSTATUS_DRAFT";
        public const string VACSTATUS_OPEN = "VACSTATUS_OPEN";
        public const string VACSTATUS_CLOSED = "VACSTATUS_CLOSED";
        public const string VACSTATUS_ARCHIVE = "VACSTATUS_ARCHIVE";

    }
    public class LanguageBlockConstant
    {
        public const string FRM_LANGUAGE_BLOCK = "FRM_LANGUAGE_BLOCK";

        public const string FRM_LANGUAGE_BLOCK_DESCRIPTION = "FRM_LANGUAGE_BLOCK_DESCRIPTION";
    }

    public class OfferTemplateConstant
    {
        public const string BTN_ADD_TEMPLLATE = "BTN_ADD_TEMPLLATE";
        public const string FRM_NEW_OFFER_TEMPLATE = "FRM_NEW_OFFER_TEMPLATE";
    }

    public class CompanyInfoConstant
    {
        public const string FRM_COMPANY_NAME = "FRM_COMPANY_NAME";
        public const string FRM_EMAIL_SEND_NAME = "FRM_EMAIL_SEND_NAME";
        public const string FRM_PORTAL_BAN_TITLE = "FRM_PORTAL_BAN_TITLE";
        public const string FRM_LOGO = "FRM_LOGO";

    }

    public class SecurityTemplateConstant
    {
        public const string FRM_SEC_ROLE_NAME = "FRM_SEC_ROLE_NAME";
        public const string FRM_SEC_EXISTING_SECURITY_ROLES = "FRM_SEC_EXISTING_SECURITY_ROLES";
    }

    #region Error Messages
    public class SuccessMessages
    {
        public const string RECORD_UPDATED_SUCCESSFULLY = "RECORD_UPDATED_SUCCESSFULLY";
        public const string RECORD_ADDED_SUCCESSFULLY = "RECORD_ADDED_SUCCESSFULLY";
        public const string RECORD_DELETED_SUCCESSFULLY = "RECORD_DELETED_SUCCESSFULLY";
    }
    public class ErrorMessages
    {
        public const string NOT_ABLE_TO_UPDATE_RECORD = "NOT_ABLE_TO_UPDATE_RECORD";

        public const string NOT_ABLE_TO_ADD_RECORD = "NOT_ABLE_TO_ADD_RECORD";

        public const string FIRST_CONFIRM_THE_VACANCY = "FIRST_CONFIRM_THE_VACANCY";

        public const string NOT_ABLE_TO_DELETE_RECORD = "NOT_ABLE_TO_DELETE_RECORD";

        public const string MSG_DELETE_ALREADY_UNDERWAY_OR_COMPLETED = "MSG_DELETE_ALREADY_UNDERWAY_OR_COMPLETED";
        public const string YOU_CAN_NOT_DELETE_THIS_INTERVIEW_ITS_ALREADY_BEGIN = "YOU_CAN_NOT_DELETE_THIS_INTERVIEW_ITS_ALREADY_BEGIN";
    }
    #endregion
}