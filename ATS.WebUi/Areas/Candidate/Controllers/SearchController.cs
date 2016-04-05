using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SolrNet;
using SolrNet.Commands.Parameters;
using SolrNet.DSL;
using SolrEntity = ATS.BusinessEntity.SolrEntity;
using SolrBL = ATS.BusinessLogic.SolrBase;
using BL = ATS.BusinessLogic;
using ATSCommon = ATS.WebUi.Common;
using BE = ATS.BusinessEntity;
using ATS.BusinessLogic.SolrBase;
using BECommon = ATS.BusinessEntity.Common;
using BEClient = ATS.BusinessEntity;
using RootModels = ATS.WebUi.Models;

namespace ATS.WebUi.Areas.Candidate.Controllers
{
    public class SearchController : ATS.WebUi.Controllers.AreaBaseController
    {
        #region Private Members
        private readonly ISolrReadOnlyOperations<SolrEntity.SolrSearchFields> _solrConnection;
        private Guid _CurrentClientId;
        private Guid _CurrentLanguageId;
        private Guid _UserId;
        private static Guid _languageId = Guid.Empty;
        #endregion

        private const char _SavedLocationSplit = ';';
        private const char _SearchLocationSplit = ',';

        #region OnAuthorization Method call on every request
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (base.CurrentClient != null && Common.CurrentSession.Instance.VerifiedUser != null)
            {
                _UserId = ATSCommon.CurrentSession.Instance.UserId;
                _CurrentClientId = base.CurrentClient.ClientId;
                _CurrentLanguageId = base.CurrentClient.CurrentLanguageId;
                if (ATSCommon.CurrentSession.Instance.VerifiedUser.SecurityRoles.Where(x => x.AtsSecurityRole == BE.ATSSecurityRole.Candidate).Count() <= 0)
                {
                    filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_CANDIDATE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_CONTROLLER, new { area = ATSCommon.Constants.AREA_EMPLOYEE }));

                }
            }
        }
        #endregion

        public SearchController()
        {
            _solrConnection = ATS.WebUi.MvcApplication.ATSSolrConnection;
        }

        public void NavIndex()
        {
            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = null;
            List<BEClient.BreadCrumb> objBreadCrumblst = new List<BEClient.BreadCrumb>();
            BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
            objBreadCrumb.Action = "Index";
            objBreadCrumb.Controller = "Search";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_CANDIDATE;
            objBreadCrumb.URL = Url.Action("Index", "Search", new { area = ATS.WebUi.Common.Constants.AREA_CANDIDATE, IsFromBack = true });
            objBreadCrumb.ordinal = 1;
            objBreadCrumb.ImagePath = BECommon.ImagePaths.SearchImage;
            objBreadCrumb.ToolTip = "Search";
            objBreadCrumb.WithoutOrdinalURL = Url.Action("Index", "Search", new { area = ATS.WebUi.Common.Constants.AREA_CANDIDATE, IsFromBack = true });
            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);

            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
        }

        private void NavDisplayJob(Guid VacancyId, String DisplayText, String ordinal)
        {
            ATSCommon.CommonFunctions.SetNullNav(this.ControllerContext.RequestContext);
            List<BE.BreadCrumb> objBreadCrumblst = ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs;
            if (ordinal != null && !String.IsNullOrEmpty(ordinal.ToString()))
            {
                objBreadCrumblst.RemoveAll(x => x.ordinal >= Convert.ToInt32(ordinal.ToString()));
            }
            BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
            objBreadCrumb.Controller = "Search";
            objBreadCrumb.ordinal = objBreadCrumblst.Count() + 1;
            //CR-5
            objBreadCrumb.URL = Url.Action("DisplayOppty", "Search", new { OpptyID = VacancyId, area = ATS.WebUi.Common.Constants.AREA_CANDIDATE, ordinal = objBreadCrumb.ordinal });
          //  objBreadCrumb.URL = Url.Action("DisplayJob", "Search", new { VacancyId = VacancyId, area = ATS.WebUi.Common.Constants.AREA_CANDIDATE, ordinal = objBreadCrumb.ordinal });
            objBreadCrumb.ImagePath = BECommon.ImagePaths.SearchJobImage;
            objBreadCrumb.ToolTip = "Position Details</br>" + DisplayText;
            objBreadCrumb.Action = "DisplayOppty";
          //  objBreadCrumb.Action = "DisplayJob";

            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_CANDIDATE;
            //CR-5
            objBreadCrumb.WithoutOrdinalURL = Url.Action("DisplayOppty", "Search", new { OpptyID = VacancyId, area = ATS.WebUi.Common.Constants.AREA_CANDIDATE });
          //  objBreadCrumb.WithoutOrdinalURL = Url.Action("DisplayJob", "Search", new { VacancyId = VacancyId, area = ATS.WebUi.Common.Constants.AREA_CANDIDATE });
            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);
            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
        }
        public ActionResult DisplayOppty(Guid OpptyID, String ordinal)
        {
            try
            {
                if (TempData["NotificationKeyMsg"] != null)
                {
                    ViewBag.Notification = TempData["NotificationKeyMsg"].ToString();
                }
                BE.Vacancy _objVacancy = new BE.Vacancy();
                BL.VacancyAction _objVacancyAction = new BL.VacancyAction(_CurrentClientId);
                _objVacancy = _objVacancyAction.GetVacancyById(OpptyID, _CurrentLanguageId);

                BL.AppInstructionDocsAction objAppInstructionDocsAction = new BL.AppInstructionDocsAction(_CurrentClientId);
                _objVacancy.objAppInstructionDocList = objAppInstructionDocsAction.GetAppInstructionDocsByVacancyId(OpptyID);

                List<BE.Application> objApplication = AppliedVacancyList(_CurrentClientId, _CurrentLanguageId, _UserId);
                var appliedVacancyList = objApplication.Select(r => r.VacancyId).ToList();
                ViewBag.ApplicationApply = appliedVacancyList;

                if (appliedVacancyList.Contains(OpptyID))
                {
                    BL.ApplicationAction objApplicationAction = new BL.ApplicationAction(_CurrentClientId);

                    BE.Application objApp = objApplicationAction.GetAppState(OpptyID, _UserId);
                    ViewBag.CandidateVacancyStatus = objApp.ApplicationStatus;
                    ViewBag.CandidateApplicationId = objApp.ApplicationId.ToString();
                }

                List<BE.UserVacancy> objUserVacancy = SavedVacancyList(_CurrentClientId, _CurrentLanguageId, _UserId);
                var savedVacancyList = objUserVacancy.Select(r => r.VacancyId).ToList();
                ViewBag.SaveJob = savedVacancyList;

                RootModels.BreadScrumbModel<BE.Vacancy> _objBreadScrumbVacancy = new RootModels.BreadScrumbModel<BE.Vacancy>();

                _objBreadScrumbVacancy.DisplayName = _objVacancy.JobTitle + ",<span style='color: #953634;'>" + " " + _objVacancy.LocationText + "</span>";

                _objBreadScrumbVacancy.ImagePath = BECommon.ImagePaths.SearchJobImage;
                _objBreadScrumbVacancy.ToolTip = _objVacancy.JobTitle + " , " + " " + _objVacancy.LocationText;
                _objBreadScrumbVacancy.obj = _objVacancy;

                NavDisplayJob(OpptyID, _objBreadScrumbVacancy.ToolTip, ordinal);

                #region RECENT VIEWED
                BEClient.RecentlyViewed objRecentlView = new BEClient.RecentlyViewed();
                BL.RecentlyViewedAction objRecentlyViewedAction = new BL.RecentlyViewedAction(_CurrentClientId);
                objRecentlView.Category = BEClient.RecentlyViewedCategory.FeaturedJob.ToString();
                objRecentlView.ViewItemId = OpptyID;
                objRecentlView.URL = HttpContext.Request.Url.ToString();

                List<BEClient.RecentlyViewed> objList = objRecentlyViewedAction.GetRecentlyViewedApplication(objRecentlView);
                _objBreadScrumbVacancy.objRecentViewedList = new List<BEClient.RecentlyViewed>();
                _objBreadScrumbVacancy.objRecentViewedList = objList;
                #endregion

                return View(_objBreadScrumbVacancy);

            }
            catch
            {
                throw;
            }
        }

        //public ActionResult DisplayJob(Guid VacancyId, String ordinal)
        //{
        //    try
        //    {
        //        if (TempData["NotificationKeyMsg"] != null)
        //        {
        //            ViewBag.Notification = TempData["NotificationKeyMsg"].ToString();
        //        }
        //        BE.Vacancy _objVacancy = new BE.Vacancy();
        //        BL.VacancyAction _objVacancyAction = new BL.VacancyAction(_CurrentClientId);
        //        _objVacancy = _objVacancyAction.GetVacancyById(VacancyId, _CurrentLanguageId);

        //        BL.AppInstructionDocsAction objAppInstructionDocsAction = new BL.AppInstructionDocsAction(_CurrentClientId);
        //        _objVacancy.objAppInstructionDocList = objAppInstructionDocsAction.GetAppInstructionDocsByVacancyId(VacancyId);

        //        List<BE.Application> objApplication = AppliedVacancyList(_CurrentClientId, _CurrentLanguageId, _UserId);
        //        var appliedVacancyList = objApplication.Select(r => r.VacancyId).ToList();
        //        ViewBag.ApplicationApply = appliedVacancyList;

        //        if (appliedVacancyList.Contains(VacancyId))
        //        {
        //            BL.ApplicationAction objApplicationAction = new BL.ApplicationAction(_CurrentClientId);

        //            BE.Application objApp = objApplicationAction.GetAppState(VacancyId, _UserId);
        //            ViewBag.CandidateVacancyStatus = objApp.ApplicationStatus;
        //            ViewBag.CandidateApplicationId = objApp.ApplicationId.ToString();
        //        }

        //        List<BE.UserVacancy> objUserVacancy = SavedVacancyList(_CurrentClientId, _CurrentLanguageId, _UserId);
        //        var savedVacancyList = objUserVacancy.Select(r => r.VacancyId).ToList();
        //        ViewBag.SaveJob = savedVacancyList;

        //        RootModels.BreadScrumbModel<BE.Vacancy> _objBreadScrumbVacancy = new RootModels.BreadScrumbModel<BE.Vacancy>();

        //        _objBreadScrumbVacancy.DisplayName = _objVacancy.JobTitle + ",<span style='color: #953634;'>" + " " + _objVacancy.LocationText + "</span>";

        //        _objBreadScrumbVacancy.ImagePath = BECommon.ImagePaths.SearchJobImage;
        //        _objBreadScrumbVacancy.ToolTip = _objVacancy.JobTitle + " , " + " " + _objVacancy.LocationText;
        //        _objBreadScrumbVacancy.obj = _objVacancy;

        //        NavDisplayJob(VacancyId, _objBreadScrumbVacancy.ToolTip, ordinal);

        //        #region RECENT VIEWED
        //        BEClient.RecentlyViewed objRecentlView = new BEClient.RecentlyViewed();
        //        BL.RecentlyViewedAction objRecentlyViewedAction = new BL.RecentlyViewedAction(_CurrentClientId);
        //        objRecentlView.Category = BEClient.RecentlyViewedCategory.FeaturedJob.ToString();
        //        objRecentlView.ViewItemId = VacancyId;
        //        objRecentlView.URL = HttpContext.Request.Url.ToString();

        //        List<BEClient.RecentlyViewed> objList = objRecentlyViewedAction.GetRecentlyViewedApplication(objRecentlView);
        //        _objBreadScrumbVacancy.objRecentViewedList = new List<BEClient.RecentlyViewed>();
        //        _objBreadScrumbVacancy.objRecentViewedList = objList;
        //        #endregion

        //        return View(_objBreadScrumbVacancy);

        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        public ActionResult GetAllVacancyByCurrentClient()
        {

            SolrEntity.SearchParameter _Parameters = new SolrEntity.SearchParameter();
            _Parameters.PageSize = 100;
            _Parameters.Sort = "PostedOn desc";
            SolrQueryByField fieldQuery = new SolrQueryByField("FeaturedOnWeb", true.ToString());


            SolrEntity.SolrSearchObject view = SolrBL.SolrResultMaker.GetSearchJobsResultFromSolr(_solrConnection, fieldQuery, null, null, _Parameters, null);

            List<BE.Application> objApplication = AppliedVacancyList(_CurrentClientId, _CurrentLanguageId, _UserId);
            ViewBag.ApplicationApply = objApplication.Select(r => r.VacancyId).ToList();

            List<BE.UserVacancy> objUserVacancy = SavedVacancyList(_CurrentClientId, _CurrentLanguageId, _UserId);
            ViewBag.SaveJob = objUserVacancy.Select(r => r.VacancyId).ToList();

            //return View("Search/_SearchColumn1", view);
            return View("Search/_AnonymousHomeDefault", view);
        }

        private ISolrQuery BuildQuery(SolrEntity.SearchParameter parameters)
        {
            if (!string.IsNullOrEmpty(parameters.FreeSearch))
                return new SolrQuery(parameters.FreeSearch);
            return SolrQuery.All;
        }

        private ISolrQuery BuildFieldQuery(string fieldName, string fieldValue)
        {
            if (!string.IsNullOrEmpty(fieldValue))
                return new SolrQueryByField(fieldName, fieldValue);
            return SolrQuery.All;
        }

        public ActionResult ReturnIndexView(SolrEntity.SolrSearchObject _obj)
        {
            DropDown();
            return View("Index", _obj);
        }

        public ActionResult Index(bool IsFromBack = false)
        {
            bool isSetViewDate = false;
            String SelectedJobView = String.Empty;
            if (TempData["SelectedJobView"] == null)
            {
                SelectedJobView = Request.Params["SelectedJobView"];
            }
            else
            {
                SelectedJobView = TempData["SelectedJobView"].ToString();
            }
            String SelectedJobType = String.Empty;
            if (TempData["SelectedJobType"] == null)
            {
                SelectedJobType = Request.Params["SelectedJobType"];
                //Display Save Job as Default
                if (SelectedJobType == null)
                    SelectedJobType = "1";//Move To Saved Result 

            }
            else
            {
                SelectedJobType = TempData["SelectedJobType"].ToString();
            }

            ViewBag.SelectedJobView = SelectedJobView;
            SolrEntity.SearchParameter param;
            if (Common.CurrentSession.Instance.VerifiedUser != null && Common.CurrentSession.Instance != null && Common.CurrentSession.Instance.VerifiedUser.searchJob != null && IsFromBack == true)
            {
                param = Common.CurrentSession.Instance.VerifiedUser.searchJob;
            }
            else
            {
                param = new SolrEntity.SearchParameter();
                if (Request.Params != null && Request.Params.Count > 0)
                {
                    //Assign new values to search param 
                    Common.CommonFunctions.ForumParamToSearchParam(Request.Params, ref param);
                }
            }
            SolrEntity.ICommonSearchParameter view = new SolrEntity.SolrSearchObject();
            ISolrQuery GeneralQuery = SolrQuery.All;
            param.MaxDate = param.MaxDate.AddMinutes(1439).AddSeconds(59);

            String SaveDefault = string.Empty;
            String Search = string.Empty;
            Decimal[] salaryRange = new Decimal[2];
            DateTime[] dateRange = new DateTime[2];
            if (Request.Params != null && Request.Params.Count > 0)
            {
                //Assign new values to search param 
                //Common.CommonFunctions.ForumParamToSearchParam(Request.Params, ref param);
                salaryRange = new Decimal[] { param.MinSalary, param.MaxSalary };
                dateRange = new DateTime[] { param.MinDate, param.MaxDate };
                //Create Solr query as per criteria

                if (param.MinDate == DateTime.MinValue && param.MaxDate.Date == DateTime.MinValue.Date)
                {
                    isSetViewDate = true;
                }


                SaveDefault = Request.Params["btn_SaveDefault"];
                Search = Request.Params["btn_Search"];

                BL.SearchAction objSearchAction = new BL.SearchAction(_CurrentClientId);
                BE.Search objSearch = new BE.Search();
                if (SaveDefault != null && _languageId == _CurrentLanguageId)
                {
                    //Mapping
                    //FullLocation -> Job Location
                    //PositionTypeText-> Position Type 
                    //EmploymentTypeText - > Full time Part Time
                    //JobTypeText -> EmployementType
                    SelectedJobType = "0";
                    objSearch.ClientId = _CurrentClientId;
                    objSearch.UserId = _UserId;
                    objSearch.Location = ATSCommon.CommonFunctions.ConvertSearchToSavedLocation(param.Location, ATSCommon.CommonFunctions.FullLocationList(), _SearchLocationSplit, _SavedLocationSplit);
                    objSearch.Position = param.PositionType;
                    objSearch.JobType = param.FpTime; //Full time part time
                    objSearch.EmploymentType = param.JobType;//
                    objSearch.SalMinRange = salaryRange[0];
                    objSearch.SalMaxRange = salaryRange[1];

                    objSearch.DateMinRange = dateRange[0];
                    objSearch.DateMaxRange = dateRange[1];

                    objSearch.KeyWords = param.FreeSearch;


                    //Skip 2 line
                    Guid SearchId = objSearchAction.AddSearch(objSearch, _CurrentLanguageId);
                    GeneralQuery = ATSCommon.CommonFunctions.VacancyQuery(param.Location, param.PositionType, param.FpTime, param.JobType, param.FreeSearch, salaryRange, dateRange);

                }
                else if (Search != null && _languageId == _CurrentLanguageId || IsFromBack == true)
                {
                    //skip
                    SelectedJobType = "0";//Move To Search Result Drop Down\
                    objSearch.ClientId = _CurrentClientId;
                    objSearch.UserId = _UserId;
                    objSearch.Location = param.Location;
                    objSearch.Position = param.PositionType;
                    objSearch.JobType = param.FpTime; //Full time part time
                    objSearch.EmploymentType = param.JobType;//
                    objSearch.SalMinRange = salaryRange[0];
                    objSearch.SalMaxRange = salaryRange[1];

                    objSearch.DateMinRange = dateRange[0];
                    objSearch.DateMaxRange = dateRange[1];

                    objSearch.KeyWords = param.FreeSearch;
                    GeneralQuery = ATSCommon.CommonFunctions.VacancyQuery(param.Location, param.PositionType, param.FpTime, param.JobType, param.FreeSearch, salaryRange, dateRange);
                }
                else
                {

                    if (string.IsNullOrEmpty(Request.Params["JobId"]))
                    {
                        //skip
                        objSearch = objSearchAction.GetDefaultParamByLanguageAndUser(_CurrentLanguageId);
                        if (objSearch != null)
                        {
                            // SelectedJobType = "0";//Move To Search Result Drop Down


                            //Anand temp
                            isSetViewDate = false;

                            salaryRange[0] = param.MinSalary = Convert.ToDecimal(objSearch.SalMinRange.ToString());
                            salaryRange[1] = param.MaxSalary = Convert.ToDecimal(objSearch.SalMaxRange.ToString());

                            dateRange[0] = param.MinDate = objSearch.DateMinRange;
                            dateRange[1] = param.MaxDate = objSearch.DateMaxRange;

                            param.FreeSearch = objSearch.KeyWords;
                            param.Location = ATSCommon.CommonFunctions.ConvertSavedToSearchLocation(objSearch.Location, ATSCommon.CommonFunctions.FullLocationList(), _SearchLocationSplit, _SavedLocationSplit);
                            param.PositionType = objSearch.Position;
                            param.FpTime = objSearch.JobType;
                            param.JobType = objSearch.EmploymentType;

                        }
                        GeneralQuery = ATSCommon.CommonFunctions.VacancyQuery(param.Location, param.PositionType, param.FpTime, param.JobType, param.FreeSearch, salaryRange, dateRange);

                    }
                    else
                    {
                        String JobId = Request.Params["JobId"];
                        String ActionType = Request.Params["hdnActionType"];
                        if (ActionType == "UnSaveJob")
                        {
                            bool Result = UnSavejob(new Guid(JobId));
                        }
                        else
                        {
                            Guid UserVacancyId = Savejob(JobId);
                        }
                        SelectedJobType = "1";//Move To Saved Job drop down
                    }
                }
            }
            DropDown(param);
            _languageId = _CurrentLanguageId;
            param.Sort = "PostedOn desc";


            view = SolrBL.SolrResultMaker.GetSearchJobsResultFromSolr(_solrConnection, GeneralQuery, null, null, param, null);
            ((SolrEntity.SolrSearchObject)view).TotalCount = ((SolrEntity.SolrSearchObject)view).SearchData.Count();

            SolrSearchFieldsAction _SolrSearchFieldsAction = new SolrSearchFieldsAction(_CurrentClientId);
            List<BE.SolrEntity.SolrSearchFields> searchJob = _SolrSearchFieldsAction.GetAlLSaveJobDetailByUserAndLanguage(_UserId, _CurrentLanguageId);
            ((SolrEntity.SolrSearchObject)view).SavedJobCount = searchJob.Count();

            if (SelectedJobType != null && SelectedJobType.Equals("1"))
            {
                ((SolrEntity.SolrSearchObject)view).SearchData = searchJob;
            }

            view.MinSalary = salaryRange[0];
            view.MaxSalary = salaryRange[1];

            view.MinDate = dateRange[0];
            view.MaxDate = dateRange[1];

            //Did Defaul binder of salary and Date(PostedOn)
            SolrBL.SolrResultMaker.DefaultSalaryBinder(ref view, _solrConnection);
            SolrBL.SolrResultMaker.DefaultDateBinder(ref view, _solrConnection);

            /*Convert Min and Max date as local time format*/
            //view.DefaultMinDate = Common.CommonFunctions.ConvertUTCToLocalDate(view.DefaultMinDate);
            //view.DefaultMaxDate = Common.CommonFunctions.ConvertUTCToLocalDate(view.DefaultMaxDate);

            #region dont remove
            //Anand temp under observation Temp commented dont remove --Start----//
            //view.DefaultMinDate = Convert.ToDateTime(view.DefaultMinDate);
            // view.DefaultMaxDate = Convert.ToDateTime(view.DefaultMaxDate);
            //Anand temp under observation Temp commented dont remove --end----//
            #endregion

            if (isSetViewDate)
            {
                if (view.MinDate >= view.DefaultMinDate)
                { view.MinDate = view.DefaultMinDate; }

                if (view.MaxDate <= view.DefaultMaxDate)
                { view.MaxDate = view.DefaultMaxDate; }
            }



            ViewBag.SelectedJobType = SelectedJobType;

            Common.CurrentSession.Instance.VerifiedUser.searchJob = param;

            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = null;

            NavIndex();

            RootModels.BreadScrumbModel<SolrEntity.SolrSearchObject> _ObjBreadScrumbModel = new RootModels.BreadScrumbModel<SolrEntity.SolrSearchObject>();
            _ObjBreadScrumbModel.obj = (SolrEntity.SolrSearchObject)view;

            List<BEClient.Search> lstprofile = new List<BEClient.Search>();
            BEClient.Search objSearchJob = new BEClient.Search();
            objSearchJob.JobTypeId = 0;
            objSearchJob.JobName = ((SolrEntity.SolrSearchObject)view).TotalCount + " Search Results";
            lstprofile.Add(objSearchJob);
            objSearchJob = new BEClient.Search();
            objSearchJob.JobTypeId = 1;
            objSearchJob.JobName = searchJob.Count() + " Saved Jobs";
            lstprofile.Add(objSearchJob);

            ViewBag.JobType = new SelectList(lstprofile, "JobTypeId", "JobName");

            return View(_ObjBreadScrumbModel);

        }

        public Guid Savejob(string JobId)
        {
            try
            {
                BL.UserVacancyAction objUserVacancyAction = new BL.UserVacancyAction(_CurrentClientId);
                BE.UserVacancy objUserVacancy = new BE.UserVacancy();
                objUserVacancy.VacancyId = new Guid(JobId);
                Guid UserVacancyId = objUserVacancyAction.InsertUserVacancy(objUserVacancy, _CurrentLanguageId);
                return UserVacancyId;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool UnSavejob(Guid JobId)
        {
            try
            {
                BL.UserVacancyAction objUserVacancyAction = new BL.UserVacancyAction(_CurrentClientId);
                return objUserVacancyAction.UnSaveJob(JobId, ATS.WebUi.Common.CurrentSession.Instance.UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult SaveAndApplyJobByVacancyId(FormCollection formParam)
        {
            try
            {
                String JobId = Request.Params["Save_JobId"];
                String ActionType = Request.Params["hdnActionType"];
                if (ActionType == "UnSaveJob")
                {
                    bool Result = UnSavejob(new Guid(JobId));
                    TempData["NotificationKeyMsg"] = "Vacancy UnSaved Successfully";
                }
                else
                {
                    Guid UserVacancyId = Savejob(JobId);
                    TempData["NotificationKeyMsg"] = "Vacancy Saved Successfully";
                }
                TempData["SelectedJobType"] = 1;
                TempData["SelectedJobView"] = 0;
                return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DropDown(SolrEntity.SearchParameter param = null)
        {
            //Mapping
            //FullLocation -> Job Location
            //PositionTypeText-> Position Type 
            //EmploymentTypeText - > Full time Part Time
            //JobTypeText -> EmployementType

            String[] Fields = new String[] { "FullPositionTypeText", "EmploymentTypeText", "JobTypeText" };
            IDictionary<string, GroupedResults<ATS.BusinessEntity.SolrEntity.SolrSearchFields>> GroupResult = SolrBL.SolrResultMaker.GroupResult(_solrConnection, SolrQuery.All, Fields, 1);
            bool oldLocationExist = !String.IsNullOrEmpty(param.Location);
            bool oldPositionTypeExist = !String.IsNullOrEmpty(param.PositionType);
            bool oldFptime = !String.IsNullOrEmpty(param.FpTime);
            bool oldEmployementTypeExist = !String.IsNullOrEmpty(param.JobType);
            bool oldFreeSearchExist = !String.IsNullOrEmpty(param.FreeSearch);

            #region Fill Search Data in Location
            if (param != null && oldLocationExist)
            {
                String[] location = param.Location.Split(',');
                ViewBag.drpJobLocation = new MultiSelectList(ATSCommon.CommonFunctions.FullLocationList(), "Key", "Value", location);
            }
            else
            {
                ViewBag.drpJobLocation = new MultiSelectList(ATSCommon.CommonFunctions.FullLocationList(), "Key", "Value");
            }
            #endregion

            #region Fill Search Data in Postion Type
            if (param != null && oldPositionTypeExist)
            {
                String[] positionType = param.PositionType.Split(',');
                if (_languageId == _CurrentLanguageId)
                {
                    String[] SlrPositionType = (GroupResult["FullPositionTypeText"].Groups).Select(r => r.GroupValue).ToArray();
                    foreach (var v in positionType)
                    {
                        if (!SlrPositionType.Contains(v))
                        {
                            Group<SolrEntity.SolrSearchFields> objposition = new Group<SolrEntity.SolrSearchFields>();
                            objposition.GroupValue = v;
                            GroupResult["FullPositionTypeText"].Groups.Add(objposition);
                        }
                    }
                }
                ViewBag.drpPositionType = new MultiSelectList(GroupResult["FullPositionTypeText"].Groups, "GroupValue", "GroupValue", positionType);
            }
            else
            {
                ViewBag.drpPositionType = new MultiSelectList(GroupResult["FullPositionTypeText"].Groups, "GroupValue", "GroupValue");
            }
            #endregion

            #region Fill Search Data in Fulltime parttime
            if (param != null && oldFptime)
            {

                if (_languageId == _CurrentLanguageId)
                {
                    String[] SlrFptime = (GroupResult["EmploymentTypeText"].Groups).Select(r => r.GroupValue).ToArray();

                    if (!SlrFptime.Contains(param.FpTime))
                    {
                        Group<SolrEntity.SolrSearchFields> objposition = new Group<SolrEntity.SolrSearchFields>();
                        objposition.GroupValue = param.FpTime;
                        GroupResult["EmploymentTypeText"].Groups.Add(objposition);
                    }
                }

                ViewBag.drpFtpt = new SelectList(GroupResult["EmploymentTypeText"].Groups, "GroupValue", "GroupValue", param.FpTime);
            }
            else
            {
                ViewBag.drpFtpt = new SelectList(GroupResult["EmploymentTypeText"].Groups, "GroupValue", "GroupValue");
            }
            #endregion

            #region Fill Search Data in employementType
            if (param != null && oldEmployementTypeExist)
            {

                if (_languageId == _CurrentLanguageId)
                {
                    String[] SlremployementType = (GroupResult["JobTypeText"].Groups).Select(r => r.GroupValue).ToArray();

                    if (!SlremployementType.Contains(param.JobType))
                    {
                        Group<SolrEntity.SolrSearchFields> objposition = new Group<SolrEntity.SolrSearchFields>();
                        objposition.GroupValue = param.JobType;
                        GroupResult["JobTypeText"].Groups.Add(objposition);
                    }

                }
                ViewBag.drpJobType = new SelectList(GroupResult["JobTypeText"].Groups, "GroupValue", "GroupValue", param.JobType);
            }
            else
            {
                ViewBag.drpJobType = new SelectList(GroupResult["JobTypeText"].Groups, "GroupValue", "GroupValue");
            }
            #endregion

            #region Fill FreeSearch

            if (param != null && oldFreeSearchExist)
            {
                if (_languageId == _CurrentLanguageId)
                {
                    ViewBag.txtFreeSearch = param.FreeSearch;
                }
                else
                {
                    ViewBag.txtFreeSearch = String.Empty;
                }
            }
            else
                ViewBag.txtFreeSearch = String.Empty;
            #endregion

            List<BE.Application> objApplicationlst = AppliedVacancyList(_CurrentClientId, _CurrentLanguageId, _UserId);
            if (objApplicationlst.Count == 0)
            {
                BE.Application objApplication = new BE.Application();
                objApplication.VacancyId = Guid.Empty;
                objApplicationlst.Add(objApplication);
            }
            ViewBag.ApplicationApply = objApplicationlst.Select(r => r.VacancyId).ToList();

            List<BE.UserVacancy> objUserVacancylst = SavedVacancyList(_CurrentClientId, _CurrentLanguageId, _UserId);
            if (objUserVacancylst.Count == 0)
            {
                BE.UserVacancy objUserVacancy = new BE.UserVacancy();
                objUserVacancy.VacancyId = Guid.Empty;
                objUserVacancylst.Add(objUserVacancy);
            }
            ViewBag.SaveJob = objUserVacancylst.Select(r => r.VacancyId).ToList();
        }

        public List<BE.Application> AppliedVacancyList(Guid clientId, Guid languageId, Guid userId)
        {
            try
            {
                BL.ApplicationAction objApplicationAction = new BL.ApplicationAction(clientId);
                List<BE.Application> objApplication = objApplicationAction.GetAllVacancyApplyByUser(userId, languageId);
                return objApplication;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<BE.UserVacancy> SavedVacancyList(Guid clientId, Guid languageId, Guid userId)
        {
            try
            {
                BL.UserVacancyAction objUserVacancyAction = new BL.UserVacancyAction(clientId);
                List<BE.UserVacancy> objUserVacancy = objUserVacancyAction.GetAllSaveJobByUserAndLanguage(userId, languageId);
                return objUserVacancy;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult SaveJob(FormCollection formParam)
        {
            return RedirectToAction("Index");
        }

        public ActionResult SearchSetting(SolrEntity.ISettingFields objSolrSearchFields, List<Guid> applicationApply, List<Guid> saveJob, string Id, string className, string href, string aTagId, string style, string showSaveJob, string navRightSide = "")
        {
            try
            {
                ViewBag.Id = Id;
                ViewBag.NavClassName = navRightSide;
                ViewBag.className = className;
                ViewBag.href = href;
                ViewBag.aTagId = aTagId;
                ViewBag.style = style;
                ViewBag.ApplicationApply = applicationApply;
                ViewBag.SaveJob = saveJob;
                ViewBag.ShowSaveJob = showSaveJob;

                return PartialView("Search/ShowSearchSetting", objSolrSearchFields);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public ActionResult GetVacancyDetails(Guid VacancyId, bool Ispublic = false)
        {
            try
            {
                String Message = String.Empty;
                bool IsError = false;
                String Data = String.Empty;
                try
                {
                    BEClient.Vacancy _objVacancy = new BEClient.Vacancy();
                    BL.VacancyAction _objVacancyAction = new BL.VacancyAction(_CurrentClientId);
                    _objVacancy = _objVacancyAction.GetVacancyById(VacancyId, _CurrentLanguageId, true);
                    ViewBag.HideSetting = 0;
                    ViewBag.HideSaveJob = 0;
                    ViewBag.VacancyId = VacancyId;
                    RootModels.BreadScrumbModel<BEClient.Vacancy> _objBreadScrumbVacancy = new RootModels.BreadScrumbModel<BEClient.Vacancy>();
                    _objBreadScrumbVacancy.DisplayName = _objVacancy.JobTitle + ", <span style='color: #953634;'>" + _objVacancy.LocationText + "</span>";
                    _objBreadScrumbVacancy.ImagePath = BECommon.ImagePaths.UploadResumeImage;
                    _objBreadScrumbVacancy.ToolTip = "Job Title";
                    _objBreadScrumbVacancy.obj = _objVacancy;
                    Data = base.RenderRazorViewToString("Search/_VacancyDetails", _objBreadScrumbVacancy.obj);
                }
                catch (Exception ex)
                {
                    Message = ex.Message;
                    IsError = true;
                }
                return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                throw;
            }
        }
    }
}