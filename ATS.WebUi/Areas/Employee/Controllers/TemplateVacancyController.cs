using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RootModels = ATS.WebUi.Models;
using BECommon = ATS.BusinessEntity.Common;
using BEClient = ATS.BusinessEntity;
using ATSCommon = ATS.WebUi.Common;
using BESrp = ATS.BusinessEntity.SRPEntity;
using ATS.WebUi.Models;
using BLClient = ATS.BusinessLogic;
using BLCommon = ATS.BusinessLogic.Common;

namespace ATS.WebUi.Areas.Employee.Controllers
{
    public partial class TemplateVacancyController : ATS.WebUi.Controllers.AreaBaseController
    {
        #region Private Member
        private Guid _CurrentClientId;
        private Guid _CurrentLanguageId;
        private Guid _UserId;
        private BLClient.TVacAction _ObjTVacAction;
        private BLClient.TVacancyRoundAction _ObjTVacancyRoundAction;
        private BLClient.TVacQueCatAction _ObjTVacQueCatAction;
        private BLClient.TVacReviewMemberAction _ObjTVacReviewMemberAction;
        private BESrp.DynamicSRP<BEClient.TVac> _objTVac;
        private BLClient.TVacQueAction _ObjTVacQueAction;
        private BLClient.VacancyAction _objVacancyAction;
        BLCommon.DrpStringMappingAction _DrpdownStringMappingAction;

        private BESrp.DynamicSRP<List<BEClient.TVac>> _ObjTvacList;
        #endregion

        #region Get Object from Private
        private BLClient.TVacAction ObjTVacAction
        {
            get
            {
                if (_ObjTVacAction == null)
                {
                    _ObjTVacAction = new BLClient.TVacAction(_CurrentClientId, true);
                }
                return _ObjTVacAction;
            }

        }

        private BLClient.TVacQueCatAction ObjTVacQueCatAction
        {
            get
            {
                if (_ObjTVacQueCatAction == null)
                {
                    _ObjTVacQueCatAction = new BLClient.TVacQueCatAction(_CurrentClientId);
                }
                return _ObjTVacQueCatAction;
            }
        }

        private BLClient.TVacancyRoundAction ObjTVacancyRoundAction
        {
            get
            {
                if (_ObjTVacancyRoundAction == null)
                {
                    _ObjTVacancyRoundAction = new BLClient.TVacancyRoundAction(_CurrentClientId);
                }
                return _ObjTVacancyRoundAction;
            }
        }

        private BLClient.TVacReviewMemberAction ObjTVacReviewMemberAction
        {
            get
            {
                if (_ObjTVacReviewMemberAction == null)
                {
                    _ObjTVacReviewMemberAction = new BLClient.TVacReviewMemberAction(_CurrentClientId);
                }
                return _ObjTVacReviewMemberAction;
            }
        }

        private BLClient.TVacQueAction ObjTVacQueAction
        {
            get
            {
                if (_ObjTVacQueAction == null)
                {
                    _ObjTVacQueAction = new BLClient.TVacQueAction(_CurrentClientId, true);
                }
                return _ObjTVacQueAction;
            }
        }

        #endregion

        #region On Authentication
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (base.CurrentClient != null && Common.CurrentSession.Instance.VerifiedUser != null)
            {
                if (Common.CurrentSession.Instance.VerifiedUser.ManageCompanySetup == false)
                {
                    TempData["KeyMsg"] = ATSCommon.CommonFunctions.NonAuthoriseduser();
                    filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_CONTROLLER, new { @KeyMsg = ATSCommon.CommonFunctions.NonAuthoriseduser() }));
                }
                _CurrentClientId = base.CurrentClient.ClientId;
                _CurrentLanguageId = base.CurrentClient.CurrentLanguageId;
                _UserId = ATSCommon.CurrentSession.Instance.UserId;

                _ObjTvacList = new BESrp.DynamicSRP<List<BEClient.TVac>>();

                _ObjTvacList.UserPermissionWithScope = ObjTVacAction.ListUserPermissionWithScope;

                if (ATSCommon.CurrentSession.Instance.VerifiedUser.SecurityRoles.Where(x => x.AtsSecurityRole == BEClient.ATSSecurityRole.Candidate).Count() > 0)
                {
                    filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_CANDIDATE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_CANDIDATE_CONTROLLER, new { area = ATSCommon.Constants.AREA_CANDIDATE }));
                }
                if (filterContext.ActionDescriptor.ActionName == "View" && filterContext.RouteData.Values.Keys.Count() == 2)
                {
                    if (_ObjTvacList.UserPermissionWithScope.Where(x => x.PermissionType.Contains(BEClient.ATSPermissionType.Create)).Count() <= 0)
                    {
                        TempData["KeyMsg"] = ATSCommon.CommonFunctions.NonAuthoriseduser();
                        filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_CONTROLLER, new { @KeyMsg = ATSCommon.CommonFunctions.NonAuthoriseduser() }));
                    }
                }
            }
        }
        #endregion

        public ActionResult Index(string ordinal)
        {
            bool isEdit = false;
            bool IsConsiderToCheck = true;
            BEClient.PageMode objPageMode = BEClient.PageMode.View;
            RootModels.BreadScrumbModel<BESrp.DynamicSRP<List<BEClient.TVac>>> _objTVac = new BreadScrumbModel<BESrp.DynamicSRP<List<BEClient.TVac>>>();
            _ObjTvacList.obj = GetVacancyTemplateByRights();
            _objTVac.obj = _ObjTvacList;
            _objTVac.DisplayName = BEClient.Common.CompanySetupMenu.CSMNU_VACANCYTEMPLATES;
            _objTVac.ToolTip = "Opportunity Templates";
            _objTVac.obj.UserPermissionWithScope = ObjTVacAction.ListUserPermissionWithScope;
            ViewBag.QuestionPermission = ObjTVacAction.ListUserPermissionWithScope;
            NavIndex(ordinal);
            return View(_objTVac);
        }

        private void DropDownLists(string pVacancyState, BEClient.ATSPermissionType PermissionType, Guid divisionId, string callMethod)
        {
            try
            {

            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public JsonResult GetNewTVac()
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                BESrp.DynamicSRP<BEClient.TVac> _objTVacSrp = new BESrp.DynamicSRP<BEClient.TVac>();
                _objTVacSrp.obj = new BEClient.TVac();
                _objTVacSrp.obj.Name = "New Template";
                BEClient.PageMode objPageMode = BEClient.PageMode.Create;
                ViewBag.PageMode = objPageMode;
                _objTVacSrp.obj.EntityLanguageList = ATSCommon.CommonFunctions.MultiLanguage();
                ViewBag.QuestionPermission = ObjTVacAction.ListUserPermissionWithScope;
                DropDownForTVac(BEClient.ATSPermissionType.Create, "Create", Guid.Empty);
                Data = base.RenderRazorViewToString("Shared/_TVacCreate", _objTVacSrp);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }

            return base.GetJson(base.GetJsonContent(IsError, null, String.Empty, Data), JsonRequestBehavior.AllowGet);
        }
        #region CRUD Operation

        [HttpPost]
        public JsonResult CRUTVac(BESrp.DynamicSRP<BEClient.TVac> objTVac)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            bool _isRecordExists = false;
            try
            {
                BEClient.PageMode ObjPageMode = BEClient.PageMode.View;
                string ErrorMessage = ServerValidation(ref objTVac);
                if (!String.IsNullOrEmpty(ErrorMessage))
                {
                    Exception ex = new Exception(ErrorMessage);
                    throw ex;
                }
                if (objTVac.obj.TVacId != Guid.Empty)
                    _isRecordExists = ObjTVacAction.IsRecordExists(objTVac.obj.TVacId);
                if (_isRecordExists)
                {
                    #region Update Record
                    bool IsrecordUpdated = ObjTVacAction.UpdateGrantTVac(objTVac.obj);
                    if (IsrecordUpdated)
                    {
                        IsError = false;
                        Message = "Updated Successfully";
                    }
                    #endregion
                }
                else
                {
                    #region Add New Record
                    Guid NewrecordAdded = ObjTVacAction.AddGrantTVac(objTVac.obj);
                    if (!NewrecordAdded.Equals(Guid.Empty))
                    {

                        IsError = false;
                        Message = "Record Inserted Successfully";
                        objTVac.obj.TVacId = NewrecordAdded;
                    }
                    #endregion
                }
                if (objTVac != null)
                {
                    ViewBag.QuestionPermission = ObjTVacAction.ListUserPermissionWithScope;
                    ObjPageMode = BEClient.PageMode.Update;
                    Data = base.RenderRazorViewToString("Shared/_TVacAcc", objTVac.obj);
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }

            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data));
        }

        #endregion

        #region BRead Scrum
        private void DropDownForTVac(BEClient.ATSPermissionType PermissionType, string callMethod, Guid DivisionId)
        {
            List<BEClient.Division> lstDivision = new List<BEClient.Division>();
            lstDivision = _ObjTVacAction.GetAllDivisionTreeByScope(base.CurrentClient.CurrentLanguageId, callMethod);
            ViewBag.LstDivision = new SelectList(lstDivision, "DivisionId", "DivisionNameTree");
            List<BEClient.PositionType> _PositionTypeList = new List<BEClient.PositionType>();
            BLClient.PositionTypeAction _PositionTypeAction = new BLClient.PositionTypeAction(base.CurrentClient.ClientId);
            _PositionTypeList = _PositionTypeAction.GetPositionTypeByDivision(_UserId, DivisionId, _CurrentLanguageId);
            ViewBag.LstPositionType = new SelectList(_PositionTypeList, "PositionTypeId", "LocalName");
            _DrpdownStringMappingAction = new BLCommon.DrpStringMappingAction(base.CurrentClient.ClientId);
            List<BEClient.DrpStringMapping> _VacancyJobTypeList = new List<BEClient.DrpStringMapping>();
            _VacancyJobTypeList = _DrpdownStringMappingAction.GetAllDropDownValue(base.CurrentClient.CurrentLanguageId, "JobType");
            ViewBag.VacancyJobType = new SelectList(_VacancyJobTypeList, "ValueField", "TextField");
            _DrpdownStringMappingAction = new BLCommon.DrpStringMappingAction(base.CurrentClient.ClientId);
            List<BEClient.DrpStringMapping> _VacancyEmploymentTypeList = new List<BEClient.DrpStringMapping>();
            _VacancyEmploymentTypeList = _DrpdownStringMappingAction.GetAllDropDownValue(base.CurrentClient.CurrentLanguageId, "EmploymentType");
            ViewBag._VacancyEmploymentType = new SelectList(_VacancyEmploymentTypeList, "ValueField", "TextField");
            List<BEClient.JobLocation> _JobLocation = new List<BEClient.JobLocation>();
            if (PermissionType == BEClient.ATSPermissionType.Modify)
            {
                BLClient.JobLocationAction _objJobLocationAction = new BLClient.JobLocationAction(base.CurrentClient.ClientId);
                _JobLocation = _objJobLocationAction.GetJobLocationByDivision(_UserId, DivisionId, _CurrentLanguageId);
            }
            ViewBag._JobLocationList = new SelectList(_JobLocation, "JobLocationId", "LocalName");
        }
        public JsonResult GetPositionBasedOnDivision(string divisionId)
        {
            try
            {
                BLClient.PositionTypeAction _objPositionTypeAction = new BLClient.PositionTypeAction(base.CurrentClient.ClientId);
                List<BEClient.PositionType> _Positiontype = new List<BEClient.PositionType>();
                if (!String.IsNullOrEmpty(divisionId))
                {
                    _Positiontype = _objPositionTypeAction.GetPositionTypeByDivision(_UserId, new Guid(divisionId), _CurrentLanguageId);
                }
                return GetJson(new { PositypeTypeId = _Positiontype.Select(r => r.PositionTypeId), LocalNamePosition = _Positiontype.Select(p => p.LocalName) });
            }
            catch (Exception)
            {

                throw;
            }
        }


        public JsonResult GetPositionAndLocationByDivisionId(string divisionId)
        {
            BLClient.JobLocationAction _objJobLocationAction = new BLClient.JobLocationAction(base.CurrentClient.ClientId);
            BLClient.PositionTypeAction _objPositionTypeAction = new BLClient.PositionTypeAction(base.CurrentClient.ClientId);
            List<BEClient.JobLocation> _JobLocation = new List<BEClient.JobLocation>();
            List<BEClient.PositionType> _Positiontype = new List<BEClient.PositionType>();
            if (!divisionId.Equals(string.Empty))
            {
                _JobLocation = _objJobLocationAction.GetJobLocationByDivision(_UserId, new Guid(divisionId), _CurrentLanguageId);
                _Positiontype = _objPositionTypeAction.GetPositionTypeByDivision(_UserId, new Guid(divisionId), _CurrentLanguageId);
            }
            return GetJson(new { JobLocationId = _JobLocation.Select(r => r.JobLocationId), LocalName = _JobLocation.Select(r => r.LocalName), PositypeTypeId = _Positiontype.Select(r => r.PositionTypeId), LocalNamePosition = _Positiontype.Select(p => p.LocalName) });
        }

        private List<BEClient.TVac> GetVacancyTemplateByRights()
        {
            List<BEClient.TVac> objTVac = null;
            objTVac = ObjTVacAction.GetAllTVacByLanguage(_CurrentLanguageId);
            return objTVac;
        }
        private void NavIndex(string ordinal)
        {
            ATSCommon.CommonFunctions.SetNullNav(this.ControllerContext.RequestContext);
            List<BEClient.BreadCrumb> objBreadCrumblst = ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs;
            if (ordinal != null && !String.IsNullOrEmpty(ordinal.ToString()))
            {
                objBreadCrumblst.RemoveAll(x => x.ordinal >= Convert.ToInt32(ordinal.ToString()));
            }
            BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
            objBreadCrumb.Action = "Index";
            objBreadCrumb.Controller = "TemplateVacancy";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_EMPLOYEE;
            objBreadCrumb.ordinal = objBreadCrumblst.Count() + 1;
            objBreadCrumb.URL = Url.Action("Index", "TemplateVacancy", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, ordinal = objBreadCrumb.ordinal });
            objBreadCrumb.ImagePath = BECommon.ImagePaths.VacancyImage;
            objBreadCrumb.ToolTip = "Template Vacancy";
            objBreadCrumb.WithoutOrdinalURL = Url.Action("Index", "TemplateVacancy", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });
            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);
            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
        }
        #endregion

        private void GetTVacRndConfigDetail(Guid TVacancyRndId, out BEClient.VacancyTemplate objVacancyTemplate)
        {
            try
            {
                objVacancyTemplate = new BEClient.VacancyTemplate();
                BLClient.TVacancyRoundAction objTVacancyRoundAction = new BLClient.TVacancyRoundAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                objVacancyTemplate.objTVacancyRound = new BEClient.TVacancyRound();
                objVacancyTemplate.objTVacancyRound = objTVacancyRoundAction.GetTVacRoundConfigByTVacRoundId(TVacancyRndId, base.CurrentClient.CurrentLanguageId);
                UpdateRoundConfigDropDown(objVacancyTemplate.TVacId, TVacancyRndId);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        public JsonResult RemoveVacQue(string VacQueId)
        {
            try
            {
                string Message = string.Empty;
                bool IsError = false;
                bool result = false;
                if (!String.IsNullOrEmpty(VacQueId))
                {
                    BLClient.TVacQueAction ObjVacQueAction = new BLClient.TVacQueAction(_CurrentClientId);
                    result = ObjVacQueAction.Delete(new Guid(VacQueId));
                    if (result)
                    {
                        Message = "Record Deleted Successfully";
                    }
                    else
                    {
                        IsError = true;
                        Message = "Not able to delete Data";
                    }
                }
                return GetJson(base.GetJsonContent(IsError, string.Empty, Message));
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        public JsonResult RemoveVacQueCat(Guid VacQueCatId, Guid VacRndId)
        {
            string Message = string.Empty;
            bool IsError = false;
            bool result = false;
            try
            {
                if (!VacQueCatId.Equals(Guid.Empty) && !VacRndId.Equals(Guid.Empty))
                {
                    BLClient.TVacQueCatAction ObjVacQueCatAction = new BLClient.TVacQueCatAction(_CurrentClientId);
                    result = ObjVacQueCatAction.Delete(VacQueCatId, VacRndId);

                    if (result)
                    {
                        Message = "Record Deleted Successfully";
                    }
                    else
                    {
                        IsError = true;
                        Message = "Not able to delete Data";
                    }
                }
            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message));
        }

        [HttpPost]
        public JsonResult RemoveTVacRound(Guid TVacRoundId)
        {
            string Message = string.Empty;
            bool IsError = false;
            bool result = false;
            try
            {
                if (!TVacRoundId.Equals(Guid.Empty))
                {
                    BLClient.TVacancyRoundAction ObjRoundAction = new BLClient.TVacancyRoundAction(_CurrentClientId);
                    result = ObjRoundAction.Delete(TVacRoundId);

                    if (result)
                    {
                        Message = "Record Deleted Successfully";
                    }
                    else
                    {
                        IsError = true;
                        Message = "Not able to delete Data";
                    }
                }
            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message));
        }

        [HttpGet]
        public JsonResult GetTvacByTvacId(Guid TVacId)
        {
            string Message = string.Empty;
            bool IsError = false;
            bool result = false;
            string Data = string.Empty;
            try
            {
                BEClient.PageMode ObjPageMode = BEClient.PageMode.View;
                if (TVacId != Guid.Empty)
                {
                    BESrp.DynamicSRP<BEClient.TVac> objTVac = new BESrp.DynamicSRP<BEClient.TVac>();
                    objTVac.obj = ObjTVacAction.GetrecordByRecordId(TVacId);
                    DropDownForTVac(BEClient.ATSPermissionType.Modify, "Modify", objTVac.obj.DivisionId);
                    ViewBag.PageMode = ObjPageMode;
                    Data = base.RenderRazorViewToString("Shared/_AddEditTVac", objTVac);
                }
            }
            catch
            {
                throw;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, String.Empty, Data), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult AddTVacJobDescription(ATS.BusinessEntity.SRPEntity.DynamicSRP<ATS.BusinessEntity.TVac> objTVAc)
        {
            try
            {
                String Message = String.Empty;
                bool IsError = false;
                String Data = "";
                try
                {
                    bool result = _ObjTVacAction.UpdateJobDescriptionByTVacId(objTVAc.obj);
                    if (result)
                    {
                        Message = "Updated Successfully";
                        CreateObjTVac(objTVAc.obj.TVacId);
                        _ObjTVacAction = new BLClient.TVacAction(_CurrentClientId, true);
                        _objTVac.obj = _ObjTVacAction.GetJobDescriptionByTVacId(objTVAc.obj.TVacId);
                        ViewBag.PageMode = BEClient.PageMode.Update;
                        _objTVac.RecordPermissionType = _objTVac.obj.PermissionType;
                        objTVAc = _objTVac;
                        Data = base.RenderRazorViewToString("Shared/_TJobDescription", objTVAc);
                    }
                    else
                    {
                        Message = "Not Able To Add Record";
                        return GetJson(base.GetJsonContent(IsError, string.Empty, Message));
                    }
                }
                catch (Exception ex)
                {
                    Message = ex.Message;
                    IsError = true;
                }

                return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult AddTVacCompensationInfo(ATS.BusinessEntity.SRPEntity.DynamicSRP<ATS.BusinessEntity.TVac> objBreadScrumVacancy)
        {
            ATS.BusinessEntity.SRPEntity.DynamicSRP<ATS.BusinessEntity.TVac> objTVacancy = objBreadScrumVacancy;
            String Message = String.Empty;
            bool IsError = false;
            String Data = "";
            try
            {
                Message = ServerValidationForCom(ref objTVacancy);
                if (!String.IsNullOrEmpty(Message))
                {
                    Exception ex = new Exception(Message);
                    throw ex;
                }
                bool result = _ObjTVacAction.UpdateCompensationInfoByTVacId(objTVacancy.obj);
                if (result)
                {
                    IsError = false;
                    Message = "Updated Successfully";
                    CreateObjTVac(objTVacancy.obj.TVacId);
                    ViewBag.PageMode = BEClient.PageMode.Update;
                    _ObjTVacAction = new BLClient.TVacAction(_CurrentClientId, true);
                    objTVacancy.obj = _ObjTVacAction.GetCompensationInfoByTVacId(objTVacancy.obj.TVacId);
                    objTVacancy.RecordPermissionType = objTVacancy.obj.PermissionType;
                    BLClient.TAppInstructionDocsAction OBJTAppInstDocsAction = new BLClient.TAppInstructionDocsAction(_CurrentClientId);
                    objTVacancy.obj.objTAppInstructionDocList = OBJTAppInstDocsAction.GetTAppInstructionDocsByTVacId(objTVacancy.obj.TVacId);
                    objBreadScrumVacancy = objTVacancy;
                    Data = base.RenderRazorViewToString("Shared/_TCompensationInfo", objBreadScrumVacancy);
                }
                else
                {
                    Message = "Not Able To Add Record";
                    return GetJson(base.GetJsonContent(IsError, string.Empty, Message));
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
        }

        private void CreateObjTVac(Guid? TVacId)
        {
            _objTVac = new BESrp.DynamicSRP<BEClient.TVac>();
            _objTVac.AddBtnText = _ObjTvacList.AddBtnText;
            _objTVac.EditBtnText = _ObjTvacList.EditBtnText;
            _objTVac.RemoveBtnText = _ObjTvacList.RemoveBtnText;
            _objTVac.SaveBtnText = _ObjTvacList.SaveBtnText;
            _objTVac.UserPermissionWithScope = ObjTVacAction.ListUserPermissionWithScope;
            if (TVacId != null)
                _objTVac.RemoveRecordURL = RemoveVacancyURL((Guid)TVacId);
        }

        public string RemoveVacancyURL(Guid TVacId)
        {
            UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);
            return u.Action("Remove", "RTemplateVacancy", new { id = TVacId.ToString() });
        }

        private String ServerValidationForCom(ref BESrp.DynamicSRP<BEClient.TVac> objVacancy)
        {
            String ErrorMessage = String.Empty;
            bool isServerError = false;
            try
            {
                if (objVacancy.obj.SalaryMin > objVacancy.obj.SalaryMax && !isServerError)
                {
                    isServerError = true;
                    ErrorMessage = String.Format("{0} {1}", Resources.Resources.LanguageDisplay(BEClient.Common.VacancyConstant.FRM_VAC_SALARY_MAX), "is less then " + Resources.Resources.LanguageDisplay(BEClient.Common.VacancyConstant.FRM_VAC_SALARY_MIN));
                }
                if (objVacancy.obj.HourlyMin > objVacancy.obj.HourlyMax && !isServerError)
                {
                    isServerError = true;
                    ErrorMessage = String.Format("{0} {1}", Resources.Resources.LanguageDisplay(BEClient.Common.VacancyConstant.FRM_VAC_HOURLY_MAX), "is less then " + Resources.Resources.LanguageDisplay(BEClient.Common.VacancyConstant.FRM_VAC_HOURLY_MIN));
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                isServerError = true;

            }
            return ErrorMessage;
        }

        [HttpGet]
        public JsonResult GetJobDescriptionByTVacId(Guid TVacId)
        {
            string Message = string.Empty;
            bool IsError = false;
            string Data = string.Empty;
            try
            {
                BEClient.PageMode ObjPageMode = BEClient.PageMode.View;
                if (TVacId != Guid.Empty)
                {
                    BEClient.TVac objTVac = new BEClient.TVac();
                    objTVac = ObjTVacAction.GetJobDescriptionByTVacId(TVacId);
                    ViewBag.PageMode = ObjPageMode;
                    Data = base.RenderRazorViewToString("Shared/_AddEditTVac", objTVac);
                }
            }
            catch
            {
                throw;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, String.Empty, Data), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]

        public JsonResult GetCompensationInfoByTVacId(Guid TVacId)
        {
            string Message = string.Empty;
            bool IsError = false;
            string Data = string.Empty;
            try
            {
                BEClient.PageMode ObjPageMode = BEClient.PageMode.View;
                if (TVacId != Guid.Empty)
                {
                    BEClient.TVac objTVac = new BEClient.TVac();
                    objTVac = ObjTVacAction.GetCompensationInfoByTVacId(TVacId);
                    ViewBag.PageMode = ObjPageMode;
                    Data = base.RenderRazorViewToString("Shared/_AddEditTVac", objTVac);
                }
            }
            catch
            {
                throw;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, String.Empty, Data), JsonRequestBehavior.AllowGet);
        }

        private String ServerValidation(ref BESrp.DynamicSRP<BEClient.TVac> objTVacancy)
        {
            String ErrorMessage = String.Empty;
            bool isServerError = false;
            if (!ModelState.IsValid)
            {
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        ErrorMessage = error.ErrorMessage;
                        isServerError = true;
                        break;
                    }
                    if (isServerError)
                        break;
                }
            }
            if (objTVacancy.obj.SalaryMin > objTVacancy.obj.SalaryMax && !isServerError)
            {
                isServerError = true;
                ErrorMessage = String.Format("{0} {1}", Resources.Resources.LanguageDisplay(BEClient.Common.VacancyConstant.FRM_VAC_SALARY_MAX), "is less then " + Resources.Resources.LanguageDisplay(BEClient.Common.VacancyConstant.FRM_VAC_SALARY_MIN));
            }
            if (objTVacancy.obj.HourlyMin > objTVacancy.obj.HourlyMax && !isServerError)
            {
                isServerError = true;
                ErrorMessage = String.Format("{0} {1}", Resources.Resources.LanguageDisplay(BEClient.Common.VacancyConstant.FRM_VAC_HOURLY_MAX), "is less then " + Resources.Resources.LanguageDisplay(BEClient.Common.VacancyConstant.FRM_VAC_HOURLY_MIN));
            }
            return ErrorMessage;
        }

        public BEClient.TVacancyRound NewVacancyRound()
        {
            BEClient.TVacancyRound objTVacancyRound = new BEClient.TVacancyRound();
            objTVacancyRound.ReqReviewer = 1;
            objTVacancyRound.PromotionThresoldScore = 75;
            objTVacancyRound.RoundWeight = 50;
            objTVacancyRound.AssignCandidateBatches = 1;
            return objTVacancyRound;
        }

        [HttpGet]
        public JsonResult TVacQueReorder(Guid TVacQueCatId, Guid TVacQueId, string OrderDir)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                BLClient.TVacQueAction objTVacQueAction = new BLClient.TVacQueAction(_CurrentClientId);
                bool result = objTVacQueAction.UpdateTVacQueOrder(TVacQueCatId, TVacQueId, OrderDir);
                if (result)
                {
                    Message = "Order Updates Successfully";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, null), JsonRequestBehavior.AllowGet);
        }

        #region RoundConfiguration
        [HttpPost]
        public JsonResult AddEditTVacRnd(BEClient.TVacancyRound objTVacRnd)
        {
            String Data = string.Empty;
            bool IsError = false;
            String Message = string.Empty;
            try
            {
                int RndAttrNo = ATS.WebUi.Common.CommonFunctions.GetAttributesNoByRndTypeId(objTVacRnd.RndTypeId);
                if (RndAttrNo == (Int32)ATS.BusinessEntity.RndAttrNo.OfferRound)
                { objTVacRnd.RoundWeight = 100; }
                if (objTVacRnd.TVacRndId == Guid.Empty)
                {
                    Guid NewrecordAdded = Guid.Empty;
                    NewrecordAdded = ObjTVacancyRoundAction.AddTVacancyRound(objTVacRnd);
                    if (!NewrecordAdded.Equals(Guid.Empty))
                    {
                        BEClient.VacancyTemplate objVacancyTemplate = new BEClient.VacancyTemplate();
                        objTVacRnd.TVacRndId = NewrecordAdded;
                        GetTVacRndConfigDetail(NewrecordAdded, out objVacancyTemplate);
                        Data = base.RenderRazorViewToString("Shared/ApplicationReviewProcess", objVacancyTemplate);
                        Message = "Add Record Successfully";
                    }
                    else
                    {
                        IsError = true;
                        Message = "Not able to add record";
                    }
                }
                else
                {
                    objTVacRnd.AssignCandiadteToReviewerId = 0;
                    UpdateRoundConfigDropDown(objTVacRnd.TVacId, objTVacRnd.TVacRndId);
                    var result = ObjTVacancyRoundAction.UpdateTVacancyRound(objTVacRnd);
                    if (result == true)
                    {
                        BEClient.VacancyTemplate objVacancyTemplate = new BEClient.VacancyTemplate();
                        GetTVacRndConfigDetail(objTVacRnd.TVacRndId, out objVacancyTemplate);
                        Data = base.RenderRazorViewToString("Shared/ApplicationReviewProcess", objVacancyTemplate);
                        Message = "Update Record Successfully";
                    }
                    else
                    {
                        IsError = true;
                        Message = "Not able to update record";
                    }
                }
            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
        }


        [HttpGet]
        public JsonResult CheckForRoundNo(Guid TRndTypeId, Guid TVacId, string TVacRndId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            Guid _TVacRndId = Guid.Empty;
            Guid.TryParse(TVacRndId.ToString(), out _TVacRndId);
            try
            {
                BLClient.RoundAttributesAction RndTypeAction = new BLClient.RoundAttributesAction(_CurrentClientId);
                int RndAttrNo = RndTypeAction.GetRoundAttributesNo(TRndTypeId);
                BEClient.TVacancyRound ObjTVacancyRound = new BEClient.TVacancyRound();
                ObjTVacancyRound = NewTVacancyRound();
                ObjTVacancyRound.RndTypeId = TRndTypeId;
                ObjTVacancyRound.TVacRndId = new Guid(TVacRndId);
                ObjTVacancyRound.TVacId = TVacId;
                ObjTVacancyRound.RoundAttributeNo = RndAttrNo;
                if (_TVacRndId != Guid.Empty)
                {
                    UpdateRoundConfigDropDown(TVacId, _TVacRndId, RndAttrNo);
                }
                else
                {
                    FillComboRndType(TVacId, RndAttrNo);

                }
                Data = base.RenderRazorViewToString("Shared/RoundConfig/_RoundConfig", ObjTVacancyRound);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        public BEClient.TVacancyRound NewTVacancyRound()
        {
            BEClient.TVacancyRound objTVacancyRound = new BEClient.TVacancyRound();
            objTVacancyRound.ReqReviewer = 1;
            objTVacancyRound.PromotionThresoldScore = 75;
            objTVacancyRound.RoundWeight = 50;
            objTVacancyRound.AssignCandidateBatches = 1;
            return objTVacancyRound;
        }

        [HttpGet]
        public JsonResult GetNewTVacRnd(Guid TVacId)
        {
            String Data = string.Empty;
            bool IsError = false;
            String Message = string.Empty;
            BEClient.PageMode objPageMode = BEClient.PageMode.Create;
            try
            {
                if (TVacId != null && TVacId != Guid.Empty)
                {
                    BEClient.TVacancyRound ObjTVacancyRound = NewTVacancyRound();
                    ObjTVacancyRound.TVacId = TVacId;
                    RoundConfigDropDown();
                    ObjTVacancyRound.TVacId = TVacId;
                    ViewBag.PageMode = objPageMode;
                    FillComboRndType(TVacId);
                    Data = base.RenderRazorViewToString("Shared/RoundConfig/_CreateVacRndAcc", ObjTVacancyRound);
                }
                else
                {
                    IsError = true;
                    Message = "Error";
                }
            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetTRoundConfigDetail(Guid TVacancyRndId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = "";
            BEClient.PageMode objPageMode = BEClient.PageMode.Update;
            try
            {
                BEClient.VacancyTemplate objVacancyTemplate = new BEClient.VacancyTemplate();
                objVacancyTemplate.objTVacancyRound = new BEClient.TVacancyRound();
                if (!TVacancyRndId.Equals(Guid.Empty))
                {
                    BLClient.TVacancyRoundAction objTVacancyRoundAction = new BLClient.TVacancyRoundAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                    objVacancyTemplate.objTVacancyRound = objTVacancyRoundAction.GetTVacRoundConfigByTVacRoundId(TVacancyRndId, _CurrentLanguageId);
                }
                ViewBag.PageMode = objPageMode;
                UpdateTVacRoundConfig(objVacancyTemplate.objTVacancyRound.TVacId, TVacancyRndId, objVacancyTemplate.objTVacancyRound.RoundAttributeNo);
                Data = base.RenderRazorViewToString("Shared/RoundConfig/_RoundConfig", objVacancyTemplate.objTVacancyRound);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
        }

        [HttpPost]
        public JsonResult GetRoundConfigDetail(Guid TVacancyRoundId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = "";
            BEClient.PageMode objPageMode = BEClient.PageMode.Update;
            try
            {
                ATS.BusinessEntity.TVacancyRound objVacancyRound = new BEClient.TVacancyRound();
                if (!TVacancyRoundId.Equals(Guid.Empty))
                {
                    objVacancyRound = ObjTVacancyRoundAction.GetrecordByRecordId(TVacancyRoundId, _CurrentLanguageId);
                }
                ViewBag.PageMode = objPageMode;
                RoundConfigDropDown();
                Data = base.RenderRazorViewToString("Shared/RoundConfig/RoundConfiguration", objVacancyRound);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
        }

        public void UpdateRoundConfigDropDown(Guid TVacId, Guid TVacRndId, int RndAttrno = 0)
        {
            try
            {
                List<BEClient.RndType> lstRndType = new List<BEClient.RndType>();
                BLClient.RndTypeAction objRndTypeAction = new BLClient.RndTypeAction(base.CurrentClient.ClientId);
                lstRndType = objRndTypeAction.GetAllRndTypeByTVac(TVacId, TVacRndId, base.CurrentClient.CurrentLanguageId);
                List<BEClient.RndType> newOrderedList = lstRndType.OrderBy(x => x.DefaultName.ToString()).ToList();
                ViewBag.TRoundType = new SelectList(lstRndType, "RndTypeId", "DefaultName");
                var targets = new Dictionary<string, string>();
                targets.Add("0", "Round-Robin fashion");
                ViewBag.AssignCandidatetoReviewers = new SelectList(targets, "Key", "Value");
                ViewBag.YesNoDropDownPromoteCandidate = Common.CommonFunctions.YesNoDropDownList();
                if (RndAttrno == (int)ATS.BusinessEntity.RndAttrNo.OfferRound)
                {
                    List<BEClient.OfferTemplates> LstOfferTemplates = BLCommon.CacheHelper.GetAllOffertemplates(_CurrentClientId, null, TVacId.ToString(), true);
                    ViewBag.OfferTemplates = new SelectList(LstOfferTemplates, "OfferTemplateId", "OfferTemplateName");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public JsonResult UpdateRoundConfig(BEClient.TVacancyRound objTVacancyRound)
        {
            String Message = String.Empty;
            bool IsError = true;
            String Data = "";
            try
            {
                if (!String.IsNullOrEmpty(Message))
                {
                    Exception ex = new Exception(Message);
                    throw ex;
                }

                objTVacancyRound.AssignCandiadteToReviewerId = 0;
                UpdateRoundConfigDropDown(objTVacancyRound.TVacId, objTVacancyRound.TVacRndId);
                var result = ObjTVacancyRoundAction.UpdateTVacancyRound(objTVacancyRound);
                if (result == true)
                {
                    IsError = false;
                    Message = "Update Record Successfully";
                    Data = base.RenderRazorViewToString("Shared/RoundConfig/RoundConfiguration", objTVacancyRound);
                }
                else
                {
                    Message = "Not able to update record";
                    return GetJson(base.GetJsonContent(IsError, string.Empty, Message));
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }

            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
        }

        public void RoundConfigDropDown()
        {
            try
            {
                List<BEClient.RndType> lstRndType = new List<BEClient.RndType>();
                BLClient.RndTypeAction objRndTypeAction = new BLClient.RndTypeAction(base.CurrentClient.ClientId);
                lstRndType = objRndTypeAction.GetAllRndTypeByLanguage(base.CurrentClient.CurrentLanguageId);
                ViewBag.TRoundType = new SelectList(lstRndType, "RndTypeId", "DefaultName");
                var targets = new Dictionary<string, string>();
                targets.Add("0", "Round-Robin fashion");
                ViewBag.AssignCandidatetoReviewers = new SelectList(targets, "Key", "Value");
                ViewBag.YesNoDropDownPromoteCandidate = Common.CommonFunctions.YesNoDropDownList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateTVacRoundConfig(Guid TVacId, Guid TVacRndId, int RndAttrNo = 0)
        {
            try
            {
                List<BEClient.RndType> lstRndType = new List<BEClient.RndType>();
                BLClient.RndTypeAction objRndTypeAction = new BLClient.RndTypeAction(base.CurrentClient.ClientId);
                //lstRndType = objRndTypeAction.GetAllRndTypeByTVac(TVacId, TVacRndId, base.CurrentClient.CurrentLanguageId);

                BEClient.RndType objRoundDetails = new BEClient.RndType();
                objRoundDetails = objRndTypeAction.GetRoundDetailsByTVacRndId(TVacRndId, _CurrentLanguageId);
                lstRndType.Add(objRoundDetails);

                ViewBag.TRoundType = new SelectList(lstRndType, "RndTypeId", "DefaultName");
                var targets = new Dictionary<string, string>();
                targets.Add("0", "Round-Robin fashion");
                ViewBag.AssignCandidatetoReviewers = new SelectList(targets, "Key", "Value");
                ViewBag.YesNoDropDownPromoteCandidate = Common.CommonFunctions.YesNoDropDownList();
                if (RndAttrNo == (int)ATS.BusinessEntity.RndAttrNo.OfferRound)
                {
                    List<BEClient.OfferTemplates> LstOfferTemplates = BLCommon.CacheHelper.GetAllOffertemplates(_CurrentClientId, null, TVacId.ToString(), true);
                    ViewBag.OfferTemplates = new SelectList(LstOfferTemplates, "OfferTemplateId", "OfferTemplateName");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void FillComboRndType(Guid? TVacId, int RndAttrNo = 0)
        {
            try
            {
                string _TVacId = Convert.ToString(TVacId);
                Guid reResult = Guid.Empty;
                if (_TVacId != null)
                {
                    Guid.TryParse(_TVacId, out reResult);
                }
                List<BEClient.RndType> lstRndType = new List<BEClient.RndType>();
                BLClient.RndTypeAction objRndTypeAction = new BLClient.RndTypeAction(base.CurrentClient.ClientId);
                lstRndType = objRndTypeAction.GetRndTypeForTRndConfig(reResult, base.CurrentClient.CurrentLanguageId);
                ViewBag.TRoundType = new SelectList(lstRndType, "RndTypeId", "DefaultName");
                var targets = new Dictionary<string, string>();
                targets.Add("0", "Round-Robin fashion");
                ViewBag.AssignCandidatetoReviewers = new SelectList(targets, "Key", "Value");
                ViewBag.YesNoDropDownPromoteCandidate = Common.CommonFunctions.YesNoDropDownList();
                if (RndAttrNo == (int)ATS.BusinessEntity.RndAttrNo.OfferRound)
                {
                    List<BEClient.OfferTemplates> LstOfferTemplates = BLCommon.CacheHelper.GetAllOffertemplates(_CurrentClientId, null, TVacId.ToString(), true);
                    ViewBag.OfferTemplates = new SelectList(LstOfferTemplates, "OfferTemplateId", "OfferTemplateName");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region REQUIREDDOCUMENTS

        [HttpGet]
        public JsonResult GetRequiredDocuments(Guid TVacRnd)
        {
            String Data = String.Empty;
            String Message = String.Empty;
            bool IsError = false;
            try
            {
                BLClient.TRequiredDocumentAction objTRequiredDocAction = new BLClient.TRequiredDocumentAction(_CurrentClientId);
                List<BEClient.TRequiredDocument> objTRequiredDocumentList = objTRequiredDocAction.GetTRequiredDocumentByVacRndId(TVacRnd);
                Data = RenderRazorViewToString("Shared/RequiredDocument/_TRequiredDocumentList", objTRequiredDocumentList);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAddTRequiredDocuments(Guid TVacRndId)
        {
            String Data = String.Empty;
            String Message = String.Empty;
            bool IsError = false;
            try
            {
                FillRequiredDocuments(TVacRndId);
                BEClient.TRequiredDocument objTRequiredDocument = new BEClient.TRequiredDocument();
                objTRequiredDocument.TVacRndId = TVacRndId;
                Data = RenderRazorViewToString("Shared/RequiredDocument/_AddEditTRequiredDocument", objTRequiredDocument);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        public void FillRequiredDocuments(Guid? VacRndId = null)
        {
            List<BEClient.DocumentType> objDocumentTypeList = new List<BEClient.DocumentType>();
            BLClient.DocumentTypeAction objDocumentTypeAction = new BLClient.DocumentTypeAction(_CurrentClientId);
            objDocumentTypeList = objDocumentTypeAction.FillTDocumentType(_CurrentLanguageId, VacRndId);
            ViewBag.drpDocumentTypeList = new SelectList(objDocumentTypeList, "DocumentTypeId", "DocumentName");
        }

        public JsonResult AddSaveTRequiredDocument(BEClient.TRequiredDocument objTRequiredDocuments)
        {
            String Data = String.Empty;
            String Message = String.Empty;
            bool IsError = false;
            try
            {
                BLClient.TRequiredDocumentAction objTRequiredDocumentAction = new BLClient.TRequiredDocumentAction(_CurrentClientId);
                foreach (var DocumentTypeId in objTRequiredDocuments.ListDocumentTypeId)
                {
                    objTRequiredDocuments.DocumentTypeId = DocumentTypeId;
                    Guid RequiredDocumentId = objTRequiredDocumentAction.InsertTRequiredDocument(objTRequiredDocuments);
                }
                Message = "Record Added successfully";
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data));
        }

        public JsonResult DeleteTRequiredDocument(Guid TRequiredDocumentId)
        {
            String Data = String.Empty;
            String Message = String.Empty;
            bool IsError = false;
            try
            {
                BLClient.TRequiredDocumentAction objTRequiredDocumentAction = new BLClient.TRequiredDocumentAction(_CurrentClientId);
                bool Result = objTRequiredDocumentAction.DeleteTRequiredDocument(TRequiredDocumentId);
                if (Result)
                {
                    Message = "Required Document Deleted Successfully";
                }
                else
                {
                    IsError = true;
                    Message = "Required Document Deleted Failed";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data));
        }

        #endregion

        #region APPLICATION INSTRUCTION DOCUMENTS
        [HttpPost]
        public JsonResult UploadTAppInstructionDocument(Guid TVacId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                int count = Request.Files.Count;
                BLClient.TAppInstructionDocsAction objTAppInstructionDocsActionAction = new BLClient.TAppInstructionDocsAction(_CurrentClientId);
                for (int i = 0; i < count; i++)
                {
                    HttpPostedFileBase resume = Request.Files[i] as HttpPostedFileBase;
                    string _oldFileName = string.Empty;
                    string _newFileName = string.Empty;
                    string _serverFilePath = string.Empty;
                    string _resumePath = ATS.WebUi.Common.CommonFunctions.ValidateOfferAttachments(resume, out _oldFileName, out _newFileName, out _serverFilePath);
                    ATS.WebUi.Common.CommonFunctions.SaveFile(resume, _resumePath);

                    BEClient.TAppInstructionDocs objTAppInstructionDoc = new BEClient.TAppInstructionDocs();
                    objTAppInstructionDoc.FileName = _oldFileName;
                    objTAppInstructionDoc.NewFileName = _newFileName;
                    objTAppInstructionDoc.Path = _serverFilePath;
                    objTAppInstructionDoc.TVacId = TVacId;
                    Guid Result = objTAppInstructionDocsActionAction.InsertTAppInstructionDocs(objTAppInstructionDoc);
                }
                List<BEClient.TAppInstructionDocs> objTAppInstructionDocList = new List<BEClient.TAppInstructionDocs>();
                objTAppInstructionDocList = objTAppInstructionDocsActionAction.GetTAppInstructionDocsByTVacId(TVacId);
                Data = base.RenderRazorViewToString("Shared/_TAppInstructionDocList", objTAppInstructionDocList);
                Message = "Application Instruction Document(s) Uploaded Successfully";
            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data));
        }

        public JsonResult DeleteTAppInstructionDoc(Guid TAppInstructionDocId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                BLClient.TAppInstructionDocsAction objTAppInstDocAction = new BLClient.TAppInstructionDocsAction(_CurrentClientId);
                bool Result = objTAppInstDocAction.DeleteTAppInstructionDoc(TAppInstructionDocId);
                if (Result)
                {
                    Message = "Application Instruction Document Deleted Successfully";
                }
                else
                {
                    IsError = true;
                    Message = "Application Instruction Document Deleted Failed";
                }
            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data));
        }
        #endregion
    }
}
