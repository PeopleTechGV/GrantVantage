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
using BLClient = ATS.BusinessLogic;
using BEClient = ATS.BusinessEntity;
using System.Reflection;
using ATSCommon = ATS.WebUi.Common;
using BECommon = ATS.BusinessEntity.Common;
using RootModels = ATS.WebUi.Models;
namespace ATS.WebUi.Controllers
{
    public class SearchController : ATS.WebUi.Controllers.AreaBaseController
    {
        private Guid _CurrentClientId;
        private Guid _CurrentLanguageId;
        private readonly ISolrReadOnlyOperations<SolrEntity.SolrSearchFields> _solrConnection;
        private readonly ISolrReadOnlyOperations<SolrEntity.SolrEmployeeSearchFields> _solrEmployeeConnection;

        public SearchController()
        {
            _solrConnection = ATS.WebUi.MvcApplication.ATSSolrConnection;


            //_solrEmployeeConnection = ATS.WebUi.MvcApplication.ATSSolrEmployeeConnection;
            //var view = SolrBL.SolrResultMaker.GetSearchEmployeeResultFromSolr(_solrEmployeeConnection, null, null, null, null, null);
            //var data = view.SearchData;

            //_solrEmployeeConnection = ATS.WebUi.MvcApplication.ATSSolrEmployeeConnection;
            //SolrQuery query = new SolrQuery("EMHDutiesAndResponsibilities:*Dev*");
            //query.Boost(100);
            //SolrQuery query1 = new SolrQuery("LastName:*P*a*");
            //query1.Boost(10);

            //BEClient.SolrEntity.ISolrUserDetail userdetails = data.First();

            //        SolrEntity.SolrEmployeeSearchFields field = new SolrEntity.SolrEmployeeSearchFields();
            //        List<PropertyInfo> result =
            //typeof(SolrEntity.SolrEmployeeSearchFields)
            //.GetProperties()
            //.Where(
            //    p =>
            //        p.GetCustomAttributes(typeof(BEClient.Attributes.EmployeeSearchAttribute), true)
            //        .Any()
            //    )
            //.ToList();


            //var view = SolrBL.SolrResultMaker.GetSearchEmployeeResultFromSolr(_solrEmployeeConnection, query || query1, null, null, null, null);
            //var data = view.SearchData;

        }
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (base.CurrentClient != null)
            {
                _CurrentClientId = base.CurrentClient.ClientId;
                _CurrentLanguageId = base.CurrentClient.CurrentLanguageId;

            }
        }
        public void NavDisplayJob(Guid VacancyId, bool Ispublic, String ToolTip)
        {

            ATSCommon.CommonFunctions.SetNullNav(this.ControllerContext.RequestContext);
            List<BEClient.BreadCrumb> objBreadCrumblst = ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs;
            BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
            objBreadCrumb.Action = "Index";
            objBreadCrumb.Controller = "Search";
            objBreadCrumb.AreaName = "";
            //CR-5
           objBreadCrumb.URL = Url.Action("DisplayOppty", "Search", new { OpptyID = VacancyId, area = "", Ispublic = Ispublic });
           // objBreadCrumb.URL = Url.Action("DisplayJob","Search", new { VacancyId = VacancyId, area = "", Ispublic = Ispublic });
            objBreadCrumb.ordinal = objBreadCrumblst.Count() + 1;
            objBreadCrumb.ImagePath = BECommon.ImagePaths.SearchImage;
            objBreadCrumb.ToolTip = ToolTip;
            objBreadCrumb.URL = Url.Action("DisplayOppty", "Search", new { OpptyID = VacancyId, area = "", Ispublic = Ispublic });
            //CR-5
          //  objBreadCrumb.WithoutOrdinalURL = Url.Action("DisplayJob", "Search", new { VacancyId = VacancyId, area = "", Ispublic = Ispublic });
            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);
            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
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
                    BLClient.VacancyAction _objVacancyAction = new BLClient.VacancyAction(_CurrentClientId);
                    _objVacancy = _objVacancyAction.GetVacancyById(VacancyId, _CurrentLanguageId, true);
                    ViewBag.HideSetting = 0;
                    ViewBag.HideSaveJob = 0;
                    ViewBag.VacancyId = VacancyId;
                    RootModels.BreadScrumbModel<BEClient.Vacancy> _objBreadScrumbVacancy = new RootModels.BreadScrumbModel<BEClient.Vacancy>();
                    _objBreadScrumbVacancy.DisplayName = _objVacancy.JobTitle + ", <span style='color: #953634;'>" + _objVacancy.LocationText + "</span>";
                    _objBreadScrumbVacancy.ImagePath = BECommon.ImagePaths.UploadResumeImage;
                    _objBreadScrumbVacancy.ToolTip = "Job Title";
                    _objBreadScrumbVacancy.obj = _objVacancy;
                    NavDisplayJob(VacancyId, Ispublic, _objVacancy.JobTitle + "," + _objVacancy.LocationText);
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
        //CR-5
        public ActionResult DisplayOppty(Guid OpptyID, bool Ispublic = false)
        {
            try
            {
                BEClient.Vacancy _objVacancy = new BEClient.Vacancy();
                BLClient.VacancyAction _objVacancyAction = new BLClient.VacancyAction(_CurrentClientId);


                _objVacancy = _objVacancyAction.GetVacancyById(OpptyID, _CurrentLanguageId);

                BLClient.AppInstructionDocsAction objAppInstructionDocsAction = new BLClient.AppInstructionDocsAction(_CurrentClientId);
                _objVacancy.objAppInstructionDocList = objAppInstructionDocsAction.GetAppInstructionDocsByVacancyId(OpptyID);

                ViewBag.HideSetting = 0;
                ViewBag.HideSaveJob = 0;
                ViewBag.VacancyId = OpptyID;
                RootModels.BreadScrumbModel<BEClient.Vacancy> _objBreadScrumbVacancy = new RootModels.BreadScrumbModel<BEClient.Vacancy>();
                _objBreadScrumbVacancy.DisplayName = _objVacancy.JobTitle + ", <span style='color: #953634;'>" + _objVacancy.LocationText + "</span>";
                _objBreadScrumbVacancy.ImagePath = BECommon.ImagePaths.UploadResumeImage;
                _objBreadScrumbVacancy.ToolTip = "Job Title";
                _objBreadScrumbVacancy.obj = _objVacancy;
                NavDisplayJob(OpptyID, Ispublic, _objVacancy.JobTitle + "," + _objVacancy.LocationText);
                return View(_objBreadScrumbVacancy);
            }
            catch
            {
                throw;
            }
        }
        //CR-5
        //public ActionResult DisplayJob(Guid VacancyId, bool Ispublic = false)
        //{
        //    try
        //    {
        //        BEClient.Vacancy _objVacancy = new BEClient.Vacancy();
        //        BLClient.VacancyAction _objVacancyAction = new BLClient.VacancyAction(_CurrentClientId);
        //        _objVacancy = _objVacancyAction.GetVacancyById(VacancyId, _CurrentLanguageId);

        //        BLClient.AppInstructionDocsAction objAppInstructionDocsAction = new BLClient.AppInstructionDocsAction(_CurrentClientId);
        //        _objVacancy.objAppInstructionDocList = objAppInstructionDocsAction.GetAppInstructionDocsByVacancyId(VacancyId);

        //        ViewBag.HideSetting = 0;
        //        ViewBag.HideSaveJob = 0;
        //        ViewBag.VacancyId = VacancyId;
        //        RootModels.BreadScrumbModel<BEClient.Vacancy> _objBreadScrumbVacancy = new RootModels.BreadScrumbModel<BEClient.Vacancy>();
        //        _objBreadScrumbVacancy.DisplayName = _objVacancy.JobTitle + ", <span style='color: #953634;'>" + _objVacancy.LocationText + "</span>";
        //        _objBreadScrumbVacancy.ImagePath = BECommon.ImagePaths.UploadResumeImage;
        //        _objBreadScrumbVacancy.ToolTip = "Job Title";
        //        _objBreadScrumbVacancy.obj = _objVacancy;
        //        NavDisplayJob(VacancyId, Ispublic, _objVacancy.JobTitle + "," + _objVacancy.LocationText);
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
            List<BEClient.Application> objApplicationlst = new List<BEClient.Application>();
            BEClient.Application objApplication = new BEClient.Application();
            objApplication.VacancyId = Guid.Empty;
            objApplicationlst.Add(objApplication);
            ViewBag.ApplicationApply = objApplicationlst.Select(r => r.VacancyId).ToList();
            List<BEClient.UserVacancy> objUserVacancylst = new List<BEClient.UserVacancy>();
            BEClient.UserVacancy objUserVacancy = new BEClient.UserVacancy();
            objUserVacancy.VacancyId = Guid.Empty;
            objUserVacancylst.Add(objUserVacancy);
            ViewBag.SaveJob = objUserVacancylst.Select(r => r.VacancyId).ToList();
            return View("Search/_AnonymousHomeDefault", view);
        }
        /// <summary>
        /// Builds the Solr query from the search parameters
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
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

        //
        // GET: /Authenticated/SearchJob/
        //[Attributes.MasterPageAttribute]
        public ActionResult ReturnIndexView(SolrEntity.SolrSearchObject _obj)
        {
            DropDown();
            return View("Index", _obj);
        }

        public void NavIndex()
        {
            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = null;
            List<BEClient.BreadCrumb> objBreadCrumblst = new List<BEClient.BreadCrumb>();
            BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
            objBreadCrumb.Action = "Index";
            objBreadCrumb.Controller = "Search";
            objBreadCrumb.AreaName = "";
            objBreadCrumb.URL = Url.Action("Index", "Search", new { area = "", IsFromBack = true });
            objBreadCrumb.ordinal = 1;
            objBreadCrumb.ImagePath = BECommon.ImagePaths.SearchImage;
            objBreadCrumb.ToolTip = "Search Jobs";
            objBreadCrumb.WithoutOrdinalURL = Url.Action("Index", "Search", new { area = "", IsFromBack = true });
            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);

            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
        }
        public ActionResult Index(bool IsFromBack = false)
        {
            bool isSetViewDate = false;
            TimeSpan t = new TimeSpan(DateTime.Now.Ticks);
            ViewBag.MyTime = t.TotalMilliseconds;
            SolrEntity.SearchParameter param;
            if (Common.CurrentSession.Instance.VerifiedClient != null && Common.CurrentSession.Instance != null && Common.CurrentSession.Instance.VerifiedClient.searchJob != null && IsFromBack == true)
            {
                param = Common.CurrentSession.Instance.VerifiedClient.searchJob;
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
            decimal[] salaryRange = new decimal[] { param.MinSalary, param.MaxSalary };
            DateTime[] dateRange = new DateTime[] { param.MinDate, param.MaxDate };
            //For Contains Search

            if (param.MinDate == DateTime.MinValue && param.MaxDate.Date == DateTime.MinValue.Date)
            {
                isSetViewDate = true;
            }

            //Create Solr query as per criteria
            GeneralQuery = ATSCommon.CommonFunctions.VacancyQuery(param.Location, param.PositionType, param.FpTime, param.JobType, param.FreeSearch, salaryRange, dateRange);



            DropDown(param);
            param.Sort = "PostedOn desc";

            if (GeneralQuery != null)
            {
                view = SolrBL.SolrResultMaker.GetSearchJobsResultFromSolr(_solrConnection, GeneralQuery, null, null, param, null);
            }
            else
            {
                view = new SolrEntity.SolrSearchObject();

            }




            view.MinSalary = Convert.ToDecimal(salaryRange[0]);
            view.MaxSalary = Convert.ToDecimal(salaryRange[1]);

            view.MinDate = Convert.ToDateTime(dateRange[0]);
            view.MaxDate = Convert.ToDateTime(dateRange[1]);

            if (!view.MinSalary.Equals("0") && !view.MinSalary.Equals("0"))
            {
                //Did Defaul binder of salary and Date(PostedOn)
                SolrBL.SolrResultMaker.DefaultSalaryBinder(ref view, _solrConnection);
                SolrBL.SolrResultMaker.DefaultDateBinder(ref view, _solrConnection);
            }

            if (!view.MaxDate.Equals(DateTime.MinValue) && !view.MinDate.Equals(DateTime.MinValue))
            {
                /*Convert Min and Max date as local time format*/
                //view.DefaultMinDate = Common.CommonFunctions.ConvertUTCToLocalDate(view.DefaultMinDate);
                //view.DefaultMaxDate = Common.CommonFunctions.ConvertUTCToLocalDate(view.DefaultMaxDate);
                view.DefaultMinDate = Convert.ToDateTime(view.DefaultMinDate);
                view.DefaultMaxDate = Convert.ToDateTime(view.DefaultMaxDate);

                if (isSetViewDate)
                {
                    if (view.MinDate >= view.DefaultMinDate)
                        view.MinDate = view.DefaultMinDate;

                    if (view.MaxDate <= view.DefaultMaxDate)
                        view.MaxDate = view.DefaultMaxDate;
                }

                if (view.DefaultMaxDate.Date == view.DefaultMinDate.Date)
                {
                    view.MaxDate = view.DefaultMaxDate.AddDays(1);
                    view.DefaultMaxDate = view.DefaultMaxDate.AddDays(1);
                }
            }

            Common.CurrentSession.Instance.VerifiedClient.searchJob = param;

            NavIndex();
            RootModels.BreadScrumbModel<SolrEntity.SolrSearchObject> _ObjBreadScrumbModel = new RootModels.BreadScrumbModel<SolrEntity.SolrSearchObject>();
            _ObjBreadScrumbModel.obj = (SolrEntity.SolrSearchObject)view;
            if (GeneralQuery == null)
            {
                _ObjBreadScrumbModel.DisplayName = null;
            }
            else
            {
                _ObjBreadScrumbModel.DisplayName = ((SolrEntity.SolrSearchObject)_ObjBreadScrumbModel.obj).SearchData.Count() + " Search Results";
            }
            _ObjBreadScrumbModel.ImagePath = BECommon.ImagePaths.SearchImage;
            return View(_ObjBreadScrumbModel);

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

                ViewBag.drpFtpt = new SelectList(GroupResult["EmploymentTypeText"].Groups, "GroupValue", "GroupValue", param.FpTime);
            }
            else
            {
                ViewBag.drpFtpt = new SelectList(GroupResult["EmploymentTypeText"].Groups, "GroupValue", "GroupValue");
            }
            #endregion

            #region Fill Search Data in employement Type Eg. Temp,Parmenant
            if (param != null && oldEmployementTypeExist)
            {

                ViewBag.drpJobType = new SelectList(GroupResult["JobTypeText"].Groups, "GroupValue", "GroupValue", param.JobType);
            }
            else
            {
                ViewBag.drpJobType = new SelectList(GroupResult["JobTypeText"].Groups, "GroupValue", "GroupValue");

            }
            #endregion

            #region Fill FreeSearch

            if (param != null && oldFreeSearchExist)
                ViewBag.txtFreeSearch = param.FreeSearch;
            else
                ViewBag.txtFreeSearch = String.Empty;
            #endregion

            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = null;

            List<BEClient.BreadCrumb> objBreadCrumblst = new List<BEClient.BreadCrumb>();
            BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
            objBreadCrumb.Action = "Index";
            objBreadCrumb.Controller = "Search";
            objBreadCrumb.URL = "Search/Index";
            objBreadCrumb.ordinal = 1;
            objBreadCrumb.ImagePath = BECommon.ImagePaths.SearchImage;
            objBreadCrumb.ToolTip = "Search";
            objBreadCrumblst.Add(objBreadCrumb);

            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
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

    }
}


