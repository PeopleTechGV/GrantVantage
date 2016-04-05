using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEClient = ATS.BusinessEntity;
using BEMAster = ATS.BusinessEntity.Master;
using BLClient = ATS.BusinessLogic;
using ATSCommon = ATS.WebUi.Common;
using ATS.WebUi.Models;
using Newtonsoft.Json;
using System.IO;
using BLCommon = ATS.BusinessLogic.Common;
using ATS.WebUi.Controllers;
using BESrp = ATS.BusinessEntity.SRPEntity;
using BECommon = ATS.BusinessEntity.Common;
using GridMvc.Filtering;
using GridMvc.Sorting;
using System.Collections;
using RootModels = ATS.WebUi.Models;
using System.Web.Mvc.Html;
using ATS.BusinessLogic;
namespace ATS.WebUi.Areas.Employee.Controllers
{
    public class MyCandidatesController : ATS.WebUi.Controllers.AreaBaseController
    {
        #region Private Members
        private BLClient.VacancyAction _objVacancyAction;
        private BLClient.ResumeAction _objResumeAction;
        private BESrp.DynamicSRP<List<BEClient.Vacancy>> _objVacancyList;
        private BESrp.DynamicSRP<BEClient.Vacancy> _objVacancy;
        private BESrp.DynamicSRP<BEClient.Resume> _objResume;
        private BESrp.DynamicSRP<List<BEClient.Resume>> _objResumeList;
        BLCommon.DrpStringMappingAction _DrpdownStringMappingAction;
        private Guid _UserId;
        private Guid _CurrentLanguageId;
        private Guid _CurrentClientId;
        #endregion

        #region Redirection Method
        private string STR_CANDIDATE_LIST_METHOD = "Index";
        private string STR_CANDIDAT_CREATE_METHOD = "Create";
        private string STR_CANDIDAT_EDIT_METHOD = "Edit";
        private string STR_CANDIDAT_VIEW_METHOD = "View";
        #endregion
        #region Initialize Method call on every request
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (base.CurrentClient != null && ATS.WebUi.Common.CurrentSession.Instance.VerifiedUser != null)
            {
                if (ATSCommon.CurrentSession.Instance.VerifiedUser.SecurityRoles.Where(x => x.AtsSecurityRole == BEClient.ATSSecurityRole.Candidate).Count() > 0)
                {
                    filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_CANDIDATE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_CANDIDATE_CONTROLLER, new { area = ATSCommon.Constants.AREA_CANDIDATE }));
                }
                _DrpdownStringMappingAction = new BLCommon.DrpStringMappingAction(base.CurrentClient.ClientId);
                _objVacancyAction = new BLClient.VacancyAction(base.CurrentClient.ClientId, true);
                _objResumeAction = new BLClient.ResumeAction(base.CurrentClient.ClientId, true);
                _CurrentClientId = base.CurrentClient.ClientId;
                _UserId = ATSCommon.CurrentSession.Instance.UserId;
                _CurrentLanguageId = base.CurrentClient.CurrentLanguageId;
                _objResumeList = new BESrp.DynamicSRP<List<BEClient.Resume>>();
                _objResumeList.UserPermissionWithScope = _objResumeAction.ListUserPermissionWithScope;
                _objVacancyList = new BESrp.DynamicSRP<List<BEClient.Vacancy>>();
                _objVacancyList.AddBtnText = "Add Vacancy";
                _objVacancyList.EditBtnText = "Edit Vacancy";
                _objVacancyList.RemoveBtnText = "Remove Vacancy";
                _objVacancyList.SaveBtnText = "Save Vacancy";
                _objVacancyList.UserPermissionWithScope = _objVacancyAction.ListUserPermissionWithScope;
            }
        }

        #endregion
        #region BRead Scrum
        private void NavIndex()
        {
            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = null;
            List<BEClient.BreadCrumb> objBreadCrumblst = new List<BEClient.BreadCrumb>();
            BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
            objBreadCrumb.URL = Url.Action("Index", "MyCandidates", ATSCommon.CommonFunctions.ToRouteValues(Request.QueryString, ATS.WebUi.Common.Constants.AREA_EMPLOYEE));
            objBreadCrumb.Action = "Index";
            objBreadCrumb.Controller = "MyCandidates";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_EMPLOYEE;
            objBreadCrumb.ordinal = 1;
            objBreadCrumb.ImagePath = BECommon.ImagePaths.MyCandidates;
            objBreadCrumb.ToolTip = "My Candidates";
            objBreadCrumb.WithoutOrdinalURL = Url.Action("Index", "MyCandidates", ATSCommon.CommonFunctions.ToRouteValues(Request.QueryString, ATS.WebUi.Common.Constants.AREA_EMPLOYEE));
            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);
            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
        }
        #endregion
        public ActionResult Index(string KeyMsg, string Page = "1", string Search = "", string Status = "Active")
        {
            ViewBag.SearchText = Search;
            ViewBag.SearchTextImg = (Search == "" ? "Search" : "Clear");//To change the image in the Search Text box.
            JsonModels resultJsonModel = null;
            if (!String.IsNullOrEmpty(KeyMsg))
            {
                string deserializeKeyMsg = HelperLibrary.Encryption.EncryptionAlgo.DecryptData(KeyMsg);
                resultJsonModel = JsonConvert.DeserializeObject<JsonModels>(deserializeKeyMsg);
            }
            try
            {
                if (resultJsonModel != null)
                {
                    ViewBag.IsError = resultJsonModel.IsError;
                    ViewBag.Message = resultJsonModel.Message;
                }
                List<BEClient.VacancyApplicant> _ListVacancyApplicant = MyCandidatesPaging(Page, Search, Status);
                RootModels.BreadScrumbModel<List<BEClient.VacancyApplicant>> _objBreadScrumbModel = new BreadScrumbModel<List<BEClient.VacancyApplicant>>();
                BEClient.VacancyApplicant objVacancyApplicant = new BEClient.VacancyApplicant();
                _objBreadScrumbModel.DisplayName = ATSCommon.CommonFunctions.LanguageLabel(ATS.BusinessEntity.Common.EmployeeMenuConstant.MNU_MY_CANDIDATES);
                _objBreadScrumbModel.ImagePath = BECommon.ImagePaths.MyCandidates;
                _objBreadScrumbModel.ToolTip = "My Candidates"; 
                _objBreadScrumbModel.obj = _ListVacancyApplicant;
                BLClient.VacancyApplicantAction _objVacancyApplicantAction = new BLClient.VacancyApplicantAction(_CurrentClientId);
                List<BEClient.VacancyApplicant> objList = _objVacancyApplicantAction.GetApplicationStatusWithCount();
                ViewBag.ApplicationStatusListWithCount = new SelectList(objList, "TotalCount", "ApplicantionStatus");
                BEClient.VacancyApplicant objSeletedStatus = objList.Where(x => x.ApplicantionStatus == Status).ToList()[0];
                ViewBag.SelectedStatus = objSeletedStatus.ApplicantionStatus;
                NavIndex();
                return View(_objBreadScrumbModel);
            }
            catch
            {
                throw;
            }
        }
        public ActionResult AjaxPaging(string Page = "1")
        {
            List<BEClient.VacancyApplicant> _ListVacancyApplicant = MyCandidatesPaging(Page);
            return PartialView("Shared/_CandidateGrid", _ListVacancyApplicant);
        }
        public List<BEClient.VacancyApplicant> MyCandidatesPaging(string Page = "1", string searchText = "", string Status = "Active")
        {
            bool IsConsiderToCheck = true;
            List<Guid> lstUsers = new List<Guid>();
            //foreach (BESrp.UserPermissionWithScope _UserPermissionWithScope in _objResumeList.UserPermissionWithScope)
            //{
            //    if (_UserPermissionWithScope.Scope == BEClient.ATSScope.A)
            //    {
            //        IsConsiderToCheck = false;
            //        break;
            //    }
            //    else
            //    {
            //        switch (_UserPermissionWithScope.Scope)
            //        {

            //            case BEClient.ATSScope.S:
            //                if (_objResumeList.UserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.S).Count() > 0)
            //                {
            //                    lstUsers.AddRange(_objResumeList.UserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.S).First().DivisionId);
            //                }
            //                continue;
            //            case BEClient.ATSScope.C:
            //                if (_objResumeList.UserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.C).Count() > 0)
            //                {
            //                    lstUsers.AddRange(_objResumeList.UserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.C).First().DivisionId);
            //                }
            //                continue;
            //            case BEClient.ATSScope.O:
            //                if (_objResumeList.UserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.O).Count() > 0)
            //                {
            //                    lstUsers.AddRange(_objResumeList.UserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.O).First().DivisionId);
            //                }
            //                continue;
            //        }
            //    }
            //}
            string FilterSearchString = string.Empty;
            string SortBySearchString = string.Empty;
            ATSCommon.CommonFunctions.FilterGrid("AppliedOn", "Descending", out FilterSearchString, out SortBySearchString);
            BLClient.VacancyApplicantAction _ObjVacancyApplicantAction = new BLClient.VacancyApplicantAction(base.CurrentClient.ClientId, true);
            List<BEClient.VacancyApplicant> _ListVacancyApplicant = new List<BEClient.VacancyApplicant>();
            int Paging = 1;
            Int32.TryParse(Page, out Paging);
            Int32 TotalCount = 0;
            _ListVacancyApplicant.AddRange(_ObjVacancyApplicantAction.GetApplicantDetailsByVacancySearch(FilterSearchString, SortBySearchString, Paging, ATSCommon.Constants.PAGGING, out TotalCount, Status, searchText));
            if (IsConsiderToCheck == true)
            {
                //foreach (var v in _ListVacancyApplicant)
                //{
                //    if (!lstUsers.Contains(v.VacancyDivisionId))
                //    {
                //        v.IsNotViewable = true;
                //    }
                //}
            }
            return _ListVacancyApplicant;
        }
        public void NavGetCandidateProfile(Guid ApplicationId, String DisplayToolTip, String ordinal)
        {
            ATSCommon.CommonFunctions.SetNullNav(this.ControllerContext.RequestContext);
            List<BEClient.BreadCrumb> objBreadCrumblst = ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs;
            BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
            objBreadCrumb.Action = "Index";
            objBreadCrumb.Controller = "MyCandidates";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_EMPLOYEE;
            objBreadCrumb.ordinal = objBreadCrumblst.Count() + 1;
            objBreadCrumb.URL = Url.Action("GetCandidateProfile", "MyCandidates", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, ApplicationId = ApplicationId, ordinal = objBreadCrumb.ordinal });
            objBreadCrumb.ImagePath = BECommon.ImagePaths.MyCandidates;
            objBreadCrumb.ToolTip = "Candidates" + DisplayToolTip; 
            objBreadCrumb.WithoutOrdinalURL = Url.Action("GetCandidateProfile", "MyCandidates", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, ApplicationId = ApplicationId });
            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);
            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
        }

        public ActionResult GetCandidateProfile(Guid ApplicationId, String ordinal,String UserId = null)
        {
            try
            {
                if (!String.IsNullOrEmpty(UserId))
                {
                    Guid.TryParse(HelperLibrary.Encryption.EncryptionAlgo.DecryptData(UserId), out _UserId);
                    if (_UserId != ATSCommon.CurrentSession.Instance.VerifiedUser.UserId)
                    {
                        return RedirectToAction(ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_CONTROLLER, new { area = ATSCommon.Constants.AREA_EMPLOYEE });
                    }
                }
                BEClient.CandidateProfile ObjCandidatProfile = null;
                BLClient.ApplicationAction _objApplicationAction = new BLClient.ApplicationAction(_CurrentClientId);
                BEClient.Application _objApplication = _objApplicationAction.GetApplicationByApplicationId(ApplicationId);
                if (_objApplication != null)
                {
                    ObjCandidatProfile = ATSCommon.CommonFunctions.GetCandidateFullProfile(ApplicationId, _CurrentClientId);
                    ObjCandidatProfile.ObjApplicationCount = _objApplicationAction.GetCountOfSubmittedApplicationByProfile(ObjCandidatProfile.objProfile.ProfileId, _CurrentLanguageId);
                    ViewBag.IsFromVacancy = 0;
                    CandidateProfileDropdownlist(ObjCandidatProfile.ObjAvailability);
                    //For DownLodaing purpose
                    ViewBag.CoverLetter = _objApplication.ATSCoverLetterId;
                    ViewBag.Resume = _objApplication.ATSResumeId;
                    ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString();
                    ViewBag.EmployeeView = true;
                    ViewBag.IsFromVacancy = 1;
                    RootModels.BreadScrumbModel<BEClient.CandidateProfile> _objBreadScrumbModel = new BreadScrumbModel<BEClient.CandidateProfile>();
                    _objBreadScrumbModel.DisplayName = ObjCandidatProfile.objUserDetails.FirstName + " " + ObjCandidatProfile.objUserDetails.LastName;
                    if (ObjCandidatProfile.objUserDetails.ProfileImage == "")
                        _objBreadScrumbModel.ImagePath = BECommon.ImagePaths.ProfileImage;
                    else
                        _objBreadScrumbModel.ImagePath = "/UploadImages/ProfileImage/" + ObjCandidatProfile.objUserDetails.ProfileImage;
                    _objBreadScrumbModel.ToolTip = ObjCandidatProfile.objUserDetails.FirstName + " " + ObjCandidatProfile.objUserDetails.LastName;
                    NavGetCandidateProfile(ApplicationId, _objBreadScrumbModel.ToolTip, ordinal);
                    _objBreadScrumbModel.obj = ObjCandidatProfile;
                    ////Get Uploaded Required Documents
                    ApplicationDocumentsAction objAppDocAction = new ApplicationDocumentsAction(_CurrentClientId);
                    List<BEClient.ApplicationDocuments> objAppDocList = objAppDocAction.GetRequiredDocumentsByApplicationId(ApplicationId);
                    _objBreadScrumbModel.obj.ObjApplicationDocuments = objAppDocList.Where(x=> x.IsPending == false).ToList();
                    //Insert current application in Recently View Item 
                    Guid RecentlyViewId = Guid.Empty;
                    BEClient.RecentlyViewed objRecentlyview = new BEClient.RecentlyViewed();
                    objRecentlyview.Category = BEClient.RecentlyViewedCategory.MyCandidate.ToString();
                    objRecentlyview.URL = HttpContext.Request.Url.AbsoluteUri.ToString();
                    objRecentlyview.ViewItemId = ApplicationId;
                    RecentlyViewedAction objRecentlyViewedAction = new RecentlyViewedAction(_CurrentClientId);
                    RecentlyViewId = objRecentlyViewedAction.InsertRecentlyViewed(objRecentlyview);
                    BEClient.RecentlyViewed objRecentlView = new BEClient.RecentlyViewed();
                    objRecentlyViewedAction = new BLClient.RecentlyViewedAction(_CurrentClientId);
                    objRecentlView.Category = BEClient.RecentlyViewedCategory.MyCandidate.ToString();
                    //objRecentlView.ViewItemId = (Guid)ApplicationId;
                    List<BEClient.RecentlyViewed> objRecentViewList = objRecentlyViewedAction.GetRecentlyViewedApplication(objRecentlView);
                    //_objBreadScrumbModel.ItemName = 
                    BEClient.RecentlyViewed objCurItem = objRecentViewList.Where(x => x.ViewItemId == ApplicationId).SingleOrDefault();
                    _objBreadScrumbModel.ItemName = objCurItem.DisplayText;
                    objRecentViewList.Remove(objCurItem);
                    _objBreadScrumbModel.objRecentViewedList = objRecentViewList;
                    return View(_objBreadScrumbModel);
                }
                else
                {
                    return RedirectToAction(STR_CANDIDATE_LIST_METHOD);
                }
            }
            catch (Exception)
            {
                return RedirectToAction(STR_CANDIDATE_LIST_METHOD);
            }
        }

        private void FillVacancyStatusDropDown(string Category)
        {
            List<BEClient.VacancyStatus> _VacancyStatusList = new List<BEClient.VacancyStatus>();
            BLClient.VacancyStatusAction _objVacancyStatusAction = new BLClient.VacancyStatusAction(_CurrentClientId);
            _VacancyStatusList = _objVacancyStatusAction.GetVacancyStatusByCategoryAndlanguage(base.CurrentClient.CurrentLanguageId, Category);
            ViewBag.VacancyStatusList = new SelectList(_VacancyStatusList, "VacancyStatusId", "VacancyStatusTextLocal");
        }
        private void CandidateProfileDropdownlist(BEClient.Availability objAvailability = null)
        {
            try
            {
                ViewBag.YesNoDropDownyearsOld = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownRelativesWorkingInCompany = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownWillingToWorkOverTime = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownSubmittedApplicationBefore = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownEligibleToWorkInUS = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownMisdemeanor = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownMilitaryService = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownMayWeContact = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownRead = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownWrite = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownSpeak = Common.CommonFunctions.YesNoDropDownList();
                BLClient.DegreeTypeAction objDegreeTypeAction = new BLClient.DegreeTypeAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                List<BEClient.DegreeType> lstDegreeType = objDegreeTypeAction.GetAllDegreeTypeByLanguage(Common.CurrentSession.Instance.VerifiedClient.CurrentLanguageId);
                ViewBag.DegreeTypeName = new SelectList(lstDegreeType, "DegreeTypeId", "LocalName");
                BLClient.SkillTypeAction objSkillTypeAction = new BLClient.SkillTypeAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                List<BEClient.SkillType> lstSkillType = objSkillTypeAction.GetAllSkillTypeByLanguage(Common.CurrentSession.Instance.VerifiedClient.CurrentLanguageId);
                ViewBag.SkillTypeName = new SelectList(lstSkillType, "SkillTypeId", "LocalName");
                var lstMonths = ATS.WebUi.Common.CommonFunctions.BindMonthDropDown();
                var lstEndMonths = ATS.WebUi.Common.CommonFunctions.BindEndMonthDropDown();
                var lstYears = ATS.WebUi.Common.CommonFunctions.BindYearDropDown();
                ViewBag.StartMonthsList = new SelectList(lstMonths, "Value", "Text");
                ViewBag.StartYearList = new SelectList(lstYears, "Value", "Text");
                ViewBag.EndMonthsList = new SelectList(lstEndMonths, "Value", "Text");
                ViewBag.EndYearList = new SelectList(lstYears, "Value", "Text");
                List<BEClient.DrpStringMapping> _EmploymentPreferenceList = new List<BEClient.DrpStringMapping>();
                _EmploymentPreferenceList = BLClient.Common.CacheHelper.GetDropDownStringMapping(_CurrentClientId, _CurrentLanguageId, CurrentClient.Clientname, BEClient.DrpDownFields.JobType.ToString());
                ViewBag._EmploymentPreferenceList = new SelectList(_EmploymentPreferenceList, "ValueField", "TextField");
                List<BEClient.DrpStringMapping> _WeekAvailability = new List<BEClient.DrpStringMapping>();
                _WeekAvailability = BLClient.Common.CacheHelper.GetDropDownStringMapping(_CurrentClientId, _CurrentLanguageId, CurrentClient.Clientname, "WeekAvailability");
                if (objAvailability != null && objAvailability.HrsMon != null)
                {
                    ViewBag.MonAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField", objAvailability.HrsMon.Split(','));
                    ViewBag.TueAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField", objAvailability.HrsTue.Split(','));
                    ViewBag.WedAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField", objAvailability.HrsWed.Split(','));
                    ViewBag.ThuAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField", objAvailability.HrsThu.Split(','));
                    ViewBag.FriAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField", objAvailability.HrsFri.Split(','));
                    ViewBag.SatAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField", objAvailability.HrsSat.Split(','));
                    ViewBag.SunAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField", objAvailability.HrsSun.Split(','));
                }
                else
                {
                    ViewBag.MonAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField");
                    ViewBag.TueAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField");
                    ViewBag.WedAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField");
                    ViewBag.ThuAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField");
                    ViewBag.FriAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField");
                    ViewBag.SatAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField");
                    ViewBag.SunAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField");
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public ActionResult DownloadCandidateResumeAndCoverLetter(Guid AtsResumeId)
        {
            try
            {
                return ATSCommon.CommonFunctions.DownloadAppliedVacancyFiles(AtsResumeId, _CurrentClientId, Server);
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                ///*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                /*Redirect to List Page*/
                return RedirectToAction(ATS.WebUi.Common.Constants.STR_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }
        [HttpPost]
        public JsonResult GetBeginInterviews(Guid ApplicationId)
        {
            BLClient.ScheduleIntAction objScheduleIntAction = new BLClient.ScheduleIntAction(_CurrentClientId);
            List<BEClient.ScheduleInt> objScheduleIntList = new List<BEClient.ScheduleInt>();
            objScheduleIntList = objScheduleIntAction.GetBeginInterviewsByAppIdAndUserId(ApplicationId, _UserId);
            if (objScheduleIntList != null && objScheduleIntList.Count > 0)
            {
                foreach (BEClient.ScheduleInt item in objScheduleIntList)
                {
                    if (!string.IsNullOrEmpty(item.InterviewStatusText))
                    {
                        string _InterStat = LanguageLabelExtensions.LanguageLabel(null, item.InterviewStatusText).ToString();
                        item.Round = item.Round + ' ' + '(' + _InterStat + ')';
                    }
                }
            }
            return GetJson(new { ScheduleIntId = objScheduleIntList.Select(r => r.ScheduleIntId), RoundDetail = objScheduleIntList.Select(r => r.Round), VacRndId = objScheduleIntList.Select(r => r.VacRndId) });
        }
    }
}
