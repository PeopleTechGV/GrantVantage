using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEClient = ATS.BusinessEntity;

using BEMaster = ATS.BusinessEntity.Master;
using BLMaster = ATS.BusinessLogic.Master;
using BLClient = ATS.BusinessLogic;
using ATSCommon = ATS.WebUi.Common;
using ATS.WebUi.Models;
using Newtonsoft.Json;
using System.IO;
using BESrp = ATS.BusinessEntity.SRPEntity;
using log4net;
using BECommon = ATS.BusinessEntity.Common;
using RootModels = ATS.WebUi.Models;
using ATS.BusinessLogic;
using System.Web.Mvc.Html;
using DisplayMessageConstant = ATS.BusinessEntity.Common;

namespace ATS.WebUi.Areas.Employee.Controllers
{
    public class DivisionController : ATS.WebUi.Controllers.AreaBaseController
    {
        #region Private Members
        private BLClient.DivisionAction _objDivisionAction;
        private Guid _CurrentClientId;
        private Guid _CurrentLanguageId;
        private BESrp.DynamicSRP<List<BEClient.Division>> _objDivisionList;

        private BESrp.DynamicSRP<BEClient.Division> _objDivision;
        private static readonly log4net.ILog log = LogManager.GetLogger(
     System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Redirection Method
        private string STR_DIVISION_LIST_METHOD = "Index";
        private string STR_FORMNAME = BEClient.Common.CompanySetupMenu.CSMNU_DIVISION;

        #endregion

        #region OnAuthorization Method call on every request
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (base.CurrentClient != null && Common.CurrentSession.Instance.VerifiedUser != null)
            {
                _CurrentClientId = base.CurrentClient.ClientId;
                _CurrentLanguageId = base.CurrentClient.CurrentLanguageId;
                _objDivisionAction = new BLClient.DivisionAction(_CurrentClientId, true);
                BEClient.Division objDivision = new BEClient.Division();

                //Create new object for List
                _objDivisionList = new BESrp.DynamicSRP<List<BEClient.Division>>();
                _objDivisionList.AddBtnText = BEClient.Common.CommonConstant.ADD;
                _objDivisionList.EditBtnText = BEClient.Common.CommonConstant.UPDATE;
                _objDivisionList.RemoveBtnText = BEClient.Common.CommonConstant.DELETE;
                _objDivisionList.SaveBtnText = BEClient.Common.CommonConstant.SAVE;
                _objDivisionList.UserPermissionWithScope = _objDivisionAction.ListUserPermissionWithScope;
                if (ATSCommon.CurrentSession.Instance.VerifiedUser.SecurityRoles.Where(x => x.AtsSecurityRole == BEClient.ATSSecurityRole.Candidate).Count() > 0)
                {
                    filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_CANDIDATE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_CANDIDATE_CONTROLLER, new { area = ATSCommon.Constants.AREA_CANDIDATE }));
                }

                //This function will be used for create new details.It will check two key exists(Action and Contrller)
                if (filterContext.ActionDescriptor.ActionName == "View" && filterContext.RouteData.Values.Keys.Count() == 2)
                {
                    if (_objDivisionList.UserPermissionWithScope.Where(x => x.PermissionType.Contains(BEClient.ATSPermissionType.Create)).Count() <= 0)
                    {
                        TempData["KeyMsg"] = ATSCommon.CommonFunctions.NonAuthoriseduser();
                        filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_CONTROLLER, new { @KeyMsg = ATSCommon.CommonFunctions.NonAuthoriseduser() }));
                    }
                }
                if (Common.CurrentSession.Instance.VerifiedUser.ManageCompanySetup == false)
                {
                    TempData["KeyMsg"] = ATSCommon.CommonFunctions.NonAuthoriseduser();
                    filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_CONTROLLER, new { @KeyMsg = ATSCommon.CommonFunctions.NonAuthoriseduser() }));
                }
            }
        }
        #endregion

        #region Create Single object
        private void CreateObjDivision(Guid? DivisionId)
        {
            _objDivision = new BESrp.DynamicSRP<BEClient.Division>();
            _objDivision.AddBtnText = _objDivisionList.AddBtnText;
            _objDivision.EditBtnText = _objDivisionList.EditBtnText;
            _objDivision.RemoveBtnText = _objDivisionList.RemoveBtnText;
            _objDivision.SaveBtnText = _objDivisionList.SaveBtnText;
            _objDivision.UserPermissionWithScope = _objDivisionAction.ListUserPermissionWithScope;
            if (DivisionId != null)
                _objDivision.RemoveRecordURL = RemoveDivisionURL((Guid)DivisionId);
        }
        #endregion

        #region BRead Scrum
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
            objBreadCrumb.Controller = "Division";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_EMPLOYEE;
            objBreadCrumb.ordinal = objBreadCrumblst.Count() + 1;
            objBreadCrumb.URL = Url.Action("Index", "Division", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, ordinal = objBreadCrumb.ordinal });
            objBreadCrumb.ImagePath = BECommon.ImagePaths.VacancyImage;
            objBreadCrumb.ToolTip = "Division";
            objBreadCrumb.WithoutOrdinalURL = Url.Action("Index", "Division", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });

            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);

            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
        }


        private void NavView(Guid? DivisionId, String DisplayToolTip, String ordinal)
        {

            ATSCommon.CommonFunctions.SetNullNav(this.ControllerContext.RequestContext);
            List<BEClient.BreadCrumb> objBreadCrumblst = ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs;
            if (ordinal != null && !String.IsNullOrEmpty(ordinal.ToString()))
            {
                objBreadCrumblst.RemoveAll(x => x.ordinal >= Convert.ToInt32(ordinal.ToString()));
            }
            BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
            objBreadCrumb.Action = "View";
            objBreadCrumb.Controller = "Division";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_EMPLOYEE;
            objBreadCrumb.ordinal = objBreadCrumblst.Count() + 1;
            objBreadCrumb.URL = Url.Action("View", "DivisionId", new { id = DivisionId, area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, ordinal = objBreadCrumb.ordinal });
            objBreadCrumb.ImagePath = BECommon.ImagePaths.VacancyImage;
            objBreadCrumb.ToolTip = "Division </br>" + DisplayToolTip;
            objBreadCrumb.WithoutOrdinalURL = Url.Action("View", "Division", new { id = DivisionId, area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });

            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);

            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;

        }
        #endregion

        public ActionResult Index(string KeyMsg, string ordinal)
        {
            RootModels.BreadScrumbModel<BESrp.DynamicSRP<List<BEClient.Division>>> _objBreadScrumbModel = new RootModels.BreadScrumbModel<BESrp.DynamicSRP<List<BEClient.Division>>>();
            JsonModels resultJsonModel = null;
            ViewBag.FormName = STR_FORMNAME;
            if (!String.IsNullOrEmpty(KeyMsg))
            {
                /*Deserialize */
                string deserializeKeyMsg = HelperLibrary.Encryption.EncryptionAlgo.DecryptData(KeyMsg);
                /*Convert from json to Object*/
                resultJsonModel = JsonConvert.DeserializeObject<JsonModels>(deserializeKeyMsg);
            }
            try
            {
                if (resultJsonModel != null)
                {
                    ViewBag.IsError = resultJsonModel.IsError;
                    ViewBag.Message = resultJsonModel.Message;
                }
                _objDivisionList.obj = _objDivisionAction.GetAllDivisionByClientAndLanguage(_CurrentLanguageId);
                _objDivisionList.AddRecordURL = AddDivisionURL();
                ViewBag.PageMode = BEClient.PageMode.View;
                NavIndex(ordinal);
                _objBreadScrumbModel.obj = _objDivisionList;
                _objBreadScrumbModel.DisplayName = STR_FORMNAME;
                _objBreadScrumbModel.ToolTip = STR_FORMNAME;
                return View(_objBreadScrumbModel);
            }
            catch
            {
                throw;
            }
        }

        #region Division By Id
        public ActionResult View(Guid? id, string ordinal)
        {
            RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.Division>> _objBreadScrumbDivision = new BreadScrumbModel<BESrp.DynamicSRP<BEClient.Division>>();
            ViewBag.FormName = STR_FORMNAME;
            BEClient.PageMode objPageMode = BEClient.PageMode.View;
            bool isEdit = false;
            if (id != null && !id.Equals(Guid.Empty))
            {
                isEdit = true;
                ViewBag.IsEdit = 1;
            }
            try
            {
                CreateObjDivision(id);
                _objDivision.obj = new BEClient.Division();

                //BLClient.PositionTypeAction objPositionTypeAction = new BLClient.PositionTypeAction(_CurrentClientId, true);
                //_objDivision.obj.SelectedPositionTypeList = new List<BEClient.PositionType>();
                if (isEdit)
                {
                    List<BEClient.RecordExists> LstRecordExists = new List<BEClient.RecordExists>();
                    BLClient.RecordExistsAction _objRecordExists = new BLClient.RecordExistsAction(_CurrentClientId);
                    LstRecordExists = _objRecordExists.GetRecordsCountByDivision((Guid)id);
                    CheckIsOwnDivision((Guid)id);
                    _objDivision.ActionName = "Edit";
                    _objDivision.obj = _objDivisionAction.GetRecordByRecordId((Guid)id);
                    _objDivision.obj.RecordExistsCount = LstRecordExists;
                    //_objDivision.obj.SelectedPositionTypeList = objPositionTypeAction.GetSelectedPositionTypeByDivisionId(_objDivision.obj.DivisionId);
                    foreach (BEMaster.ClientLanguage clientLanguage in ATSCommon.CurrentSession.Instance.VerifiedClient.ClientLanguageList.OrderBy(r => r.objLanguage.LanguageName))
                    {
                        if (_objDivision.obj.DivisionEntityLanguage.Where(x => x.LanguageId == clientLanguage.LanguageId).Count() <= 0)
                        {
                            BEClient.EntityLanguage entitylanguage = new BEClient.EntityLanguage();
                            entitylanguage.LanguageId = clientLanguage.LanguageId;
                            _objDivision.obj.DivisionEntityLanguage.Add(entitylanguage);
                        }
                    }
                    objPageMode = BEClient.PageMode.Update;
                    if (_objDivision.obj.PermissionType == null)
                    {
                        return RedirectToAction(ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_CONTROLLER, new { KeyMsg = ATSCommon.CommonFunctions.NonAuthoriseduser() });
                    }
                    if (_objDivision.obj.PermissionType.Contains(BEClient.ATSPermissionType.Delete))
                    {
                        ViewBag.IsDelete = true;
                    }
                    _objDivision.RecordPermissionType = _objDivision.obj.PermissionType;
                    DropDownList(_objDivision.obj.ParentDivisionId, _CurrentLanguageId, (Guid)id, ATS.WebUi.Common.CommonFunctions.GetPageMode(_objDivision.RecordPermissionType, objPageMode).ToString());
                    _objBreadScrumbDivision.obj = _objDivision;
                    SetBreadScrum(ref _objBreadScrumbDivision);
                    NavView(id, _objBreadScrumbDivision.ToolTip, ordinal);
                    _objBreadScrumbDivision.ItemName = _objBreadScrumbDivision.obj.obj.DefaultName;
                    #region RECENT VIEWED
                    BEClient.RecentlyViewed objRecentlView = new BEClient.RecentlyViewed();
                    RecentlyViewedAction objRecentlyViewedAction = new RecentlyViewedAction(_CurrentClientId);
                    objRecentlView.Category = BEClient.RecentlyViewedCategory.Division.ToString();
                    objRecentlView.ViewItemId = (Guid)id;
                    objRecentlView.URL = HttpContext.Request.Url.ToString();
                    List<BEClient.RecentlyViewed> objList = objRecentlyViewedAction.GetRecentlyViewedApplication(objRecentlView);
                    _objBreadScrumbDivision.objRecentViewedList = new List<BEClient.RecentlyViewed>();
                    _objBreadScrumbDivision.objRecentViewedList = objList;//.obj.ConvertAll(x => new BECommon.RecentViewedList
                    #endregion
                }
                else
                {
                    _objDivision.obj.ClientId = _CurrentClientId;
                    _objDivision.obj.DivisionEntityLanguage = new List<BEClient.EntityLanguage>();
                    _objDivision.obj.RecordExistsCount = new List<BEClient.RecordExists>();
                    foreach (BEMaster.ClientLanguage clientLanguage in ATSCommon.CurrentSession.Instance.VerifiedClient.ClientLanguageList.OrderBy(r => r.objLanguage.LanguageName))
                    {
                        BEClient.EntityLanguage entitylanguage = new BEClient.EntityLanguage();
                        entitylanguage.LanguageId = clientLanguage.LanguageId;
                        _objDivision.obj.DivisionEntityLanguage.Add(entitylanguage);
                    }
                    _objDivision.ActionName = "Create";
                    objPageMode = BEClient.PageMode.Create;
                    _objDivision.obj.PermissionType = new List<BEClient.ATSPermissionType> { BEClient.ATSPermissionType.Create };
                    _objDivision.RecordPermissionType = _objDivision.obj.PermissionType;
                    DropDownList(Guid.Empty, _CurrentLanguageId, Guid.Empty, ATS.WebUi.Common.CommonFunctions.GetPageMode(_objDivision.RecordPermissionType, objPageMode).ToString());
                    SetBreadScrum(ref _objBreadScrumbDivision);
                    NavView(id, _objBreadScrumbDivision.DisplayName, ordinal);
                    _objBreadScrumbDivision.ItemName = "ADD DIVISION";
                }
                //_objDivision.obj.AvailablePositionTypeList = objPositionTypeAction.GetAllPostionsWithDivisionList(_CurrentLanguageId);
                ViewBag.PageMode = objPageMode;
                TempData["RecordPermissionType"] = _objDivision.RecordPermissionType;
                TempData["RecordExistCount"] = _objDivision.obj.RecordExistsCount;
                TempData["PermissionType"] = _objDivision.obj.PermissionType;
                _objBreadScrumbDivision.obj = _objDivision;
                return View(_objBreadScrumbDivision);
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                /*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                /*Redirect to List Page*/
                return RedirectToAction(STR_DIVISION_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }
        #endregion

        #region Edit Division
        [HttpPost]
        public ActionResult Edit(RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.Division>> _objBreadScrumbDivision)
        {
            BESrp.DynamicSRP<BEClient.Division> objDivision = _objBreadScrumbDivision.obj;
            try
            {
                if (!string.IsNullOrEmpty(_objBreadScrumbDivision.obj.obj.SelectePositionType))
                {
                    string[] res = _objBreadScrumbDivision.obj.obj.SelectePositionType.Split(new char[] { ';' });
                    _objBreadScrumbDivision.obj.obj.objPositionType = new BEClient.PositionTypeMaster();
                    _objBreadScrumbDivision.obj.obj.objPositionType.PositionTypeId = res.ToArray();
                }
                if (!string.IsNullOrEmpty(_objBreadScrumbDivision.obj.obj.SelecteJobLocation))
                {
                    string[] res = _objBreadScrumbDivision.obj.obj.SelecteJobLocation.Split(new char[] { ';' });
                    _objBreadScrumbDivision.obj.obj.objJobLocation = new BEClient.JobLocationMaster();
                    _objBreadScrumbDivision.obj.obj.objJobLocation.JobLocationId = res.ToArray();
                }
                String Message = String.Empty;
                ViewBag.FormName = STR_FORMNAME;
                CheckIsOwnDivision(objDivision.obj.DivisionId);
                objDivision.RecordPermissionType = (List<ATS.BusinessEntity.ATSPermissionType>)TempData["RecordPermissionType"];
                objDivision.obj.RecordExistsCount = (List<ATS.BusinessEntity.RecordExists>)TempData["RecordExistCount"];
                objDivision.obj.PermissionType = (List<ATS.BusinessEntity.ATSPermissionType>)TempData["PermissionType"];
                objDivision.RemoveBtnText = _objDivisionList.RemoveBtnText;
                objDivision.SaveBtnText = _objDivisionList.SaveBtnText;
                bool isRecordUpdated = _objDivisionAction.UpdateDivision(objDivision.obj);
                JsonModels actionStatus = null;
                if (isRecordUpdated)
                {
                    try
                    {
                        ATSCommon.CommonFunctions.SolrFullImport();
                    }
                    catch (Exception ex)
                    {
                        log.Error("SOLR Employee FULL IMPORT (Vacancy)" + ex.Message);
                    }
                    actionStatus = base.GetJsonContent(!isRecordUpdated, string.Empty, "Record Updated Successfully");
                }
                else
                {
                    actionStatus = base.GetJsonContent(!isRecordUpdated, string.Empty, "Not Able To Update Record");
                }
                /*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                /*Redirect to List Page*/
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                return RedirectToAction(STR_DIVISION_LIST_METHOD, new { KeyMsg = keyMsg });
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                TempData["RecordPermissionType"] = objDivision.RecordPermissionType;
                TempData["RecordExistCount"] = objDivision.obj.RecordExistsCount;
                TempData["PermissionType"] = objDivision.obj.PermissionType;
                BEClient.PageMode objPageMode = BEClient.PageMode.Update;
                ViewBag.PageMode = objPageMode;
                ViewBag.IsError = true;
                ViewBag.Message = ex.Message;
                objDivision.obj.SelectedPositionTypeList = new List<BEClient.PositionType>();
                BLClient.DivisionAction objDivisionAction = new BLClient.DivisionAction(_CurrentClientId, true);
                BLClient.PositionTypeAction objPositionTypeAction = new BLClient.PositionTypeAction(_CurrentClientId, true);
                objDivision.obj.AvailablePositionTypeList = objDivisionAction.GetAllPositionTypeByScope(_CurrentLanguageId, ATS.WebUi.Common.CommonFunctions.GetPageMode(objDivision.RecordPermissionType, objPageMode).ToString());
                DropDownList(objDivision.obj.ParentDivisionId, _CurrentLanguageId, Guid.Empty, ATS.WebUi.Common.CommonFunctions.GetPageMode(objDivision.RecordPermissionType, objPageMode).ToString());
                objDivision.obj.SelectedPositionTypeList = objPositionTypeAction.GetSelectedPositionTypeByDivisionId(objDivision.obj.DivisionId);
                if (objDivision.obj.PermissionType.Contains(BEClient.ATSPermissionType.Delete))
                {
                    ViewBag.IsDelete = true;
                }
                _objBreadScrumbDivision.obj = objDivision;
                SetBreadScrum(ref _objBreadScrumbDivision);
                return View(ATS.WebUi.Common.Constants.STR_VIEW_METHOD, _objBreadScrumbDivision);
            }
        }
        #endregion

        #region Bind Dropdown
        public void DropDownList(Guid? parentDivisionId, Guid LanguageId, Guid currentDivisionId, string callMethod)
        {
            try
            {
                Guid ClientId = _CurrentClientId;
                List<BEClient.Division> _objDivisionList = new List<BEClient.Division>();
                _objDivisionList = _objDivisionAction.GetAllDivisionByScope(LanguageId, callMethod, parentDivisionId);
                if (currentDivisionId != Guid.Empty)
                {
                    foreach (var v in _objDivisionList)
                    {
                        if (v.DivisionId == currentDivisionId)
                        {
                            _objDivisionList.Remove(v);
                            break;
                        }
                    }
                }
                var AllList = new SelectList(_objDivisionList, "DivisionId", "DivisionName");
                if (parentDivisionId != null)
                {
                    if (AllList.Where(x => x.Value.ToUpper() == parentDivisionId.ToString().ToUpper()).Count() > 0)
                    {
                        AllList.Where(x => x.Value.ToUpper() == parentDivisionId.ToString().ToUpper()).First().Selected = true;
                    }
                }
                ViewBag.ParentDivisionList = AllList;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Add Division
        [HttpPost]
        public ActionResult Create(RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.Division>> _objBreadScrumbDivision)
        {
            BESrp.DynamicSRP<BEClient.Division> objDivision = _objBreadScrumbDivision.obj;
            try
            {
                if (!string.IsNullOrEmpty(_objBreadScrumbDivision.obj.obj.SelectePositionType))
                {
                    string[] res = _objBreadScrumbDivision.obj.obj.SelectePositionType.Split(new char[] { ';' });
                    _objBreadScrumbDivision.obj.obj.objPositionType = new BEClient.PositionTypeMaster();
                    _objBreadScrumbDivision.obj.obj.objPositionType.PositionTypeId = res.ToArray();
                }
                objDivision.obj.ClientId = _CurrentClientId;
                String Message = String.Empty;
                ViewBag.FormName = STR_FORMNAME;
                objDivision.RecordPermissionType = (List<ATS.BusinessEntity.ATSPermissionType>)TempData["RecordPermissionType"];
                objDivision.obj.RecordExistsCount = (List<ATS.BusinessEntity.RecordExists>)TempData["RecordExistCount"];
                objDivision.RemoveBtnText = _objDivisionList.RemoveBtnText;
                objDivision.SaveBtnText = _objDivisionList.SaveBtnText;
                Guid newDivisionId = _objDivisionAction.AddDivision(objDivision.obj);
                JsonModels actionStatus = null;
                if (newDivisionId != null && !newDivisionId.Equals(Guid.Empty))
                {
                    actionStatus = base.GetJsonContent(false, string.Empty, "Record Added Successfully");
                }
                else
                {
                    actionStatus = base.GetJsonContent(true, string.Empty, "Not Able To Add Record");
                }
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                return RedirectToAction("View", "Division", new { id = newDivisionId });
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                TempData["RecordPermissionType"] = objDivision.RecordPermissionType;
                TempData["RecordExistCount"] = objDivision.obj.RecordExistsCount;
                BEClient.PageMode objPageMode = BEClient.PageMode.Create;
                ViewBag.PageMode = objPageMode;
                ViewBag.IsError = true;
                ViewBag.Message = ex.Message;
                objDivision.obj.SelectedPositionTypeList = new List<BEClient.PositionType>();
                BLClient.DivisionAction objDivisionAction = new BLClient.DivisionAction(_CurrentClientId, true);
                BLClient.PositionTypeAction objPositionTypeAction = new BLClient.PositionTypeAction(_CurrentClientId, true);
                objDivision.obj.AvailablePositionTypeList = objDivisionAction.GetAllPositionTypeByScope(_CurrentLanguageId, ATS.WebUi.Common.CommonFunctions.GetPageMode(objDivision.RecordPermissionType, objPageMode).ToString());
                DropDownList(objDivision.obj.ParentDivisionId, _CurrentLanguageId, Guid.Empty, ATS.WebUi.Common.CommonFunctions.GetPageMode(objDivision.RecordPermissionType, objPageMode).ToString());
                objDivision.obj.SelectedPositionTypeList = objPositionTypeAction.GetSelectedPositionTypeByDivisionId(objDivision.obj.DivisionId);
                _objBreadScrumbDivision.obj = objDivision;
                _objBreadScrumbDivision.ItemName = "ADD DIVISION";
                SetBreadScrum(ref _objBreadScrumbDivision);
                return View(ATS.WebUi.Common.Constants.STR_VIEW_METHOD, _objBreadScrumbDivision);
            }
        }
        #endregion

        private void SetBreadScrum(ref RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.Division>> _objBreadScrumbDivision)
        {
            _objBreadScrumbDivision.DisplayName = STR_FORMNAME;
            _objBreadScrumbDivision.ImagePath = BECommon.ImagePaths.VacancyImage;
            _objBreadScrumbDivision.ToolTip = _objBreadScrumbDivision.DisplayName;
        }

        #region Delete Division
        public ActionResult Delete(string id)
        {
            try
            {
                bool Result = _objDivisionAction.Delete(id);
                JsonModels actionStatus = null;
                if (Result)
                {
                    actionStatus = base.GetJsonContent(false, string.Empty, "Record Deleted Successfully");
                }
                else
                {
                    actionStatus = base.GetJsonContent(true, string.Empty, "Not Able To Delete Record");
                }
                /*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                try
                {
                    ATSCommon.CommonFunctions.SolrFullImport();
                }
                catch (Exception ex)
                {
                    log.Error("SOLR Employee FULL IMPORT (Vacancy)" + ex.Message);
                }
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                /*Redirect to List Page*/
                return RedirectToAction(STR_DIVISION_LIST_METHOD, new { KeyMsg = keyMsg });
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                /*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                /*Redirect to List Page*/
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                return RedirectToAction(STR_DIVISION_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }

        public ActionResult DeleteMultiple(string id)
        {
            string Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                bool Result = _objDivisionAction.Delete(id);
                if (Result)
                {
                    Message = "Record Deleted Successfully";
                }
                else
                {
                    IsError = true;
                    Message = "Not Able To Delete Record";
                }
                Data = base.RenderRazorViewToString("_NavCompanySetup", null);
            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;
            }
            return base.GetJson(GetJsonContent(IsError, null, Message, Data));
        }
        #endregion

        private void CheckIsOwnDivision(Guid recordId)
        {
            if (ATS.WebUi.Common.CurrentSession.Instance.VerifiedUser.AddHocDivision.Where(x => x.DivisionId.ToString().Equals(recordId.ToString())).Count() > 0)
                ViewBag.IsMyDivision = true;
            else
                ViewBag.IsMyDivision = false;
        }

        #region CreateURL
        public string RemoveDivisionURL(Guid divisionId)
        {
            UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);
            return u.Action("Remove", "Division", new { id = divisionId.ToString() });
        }
        public string AddDivisionURL()
        {
            UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);
            return u.Action("View", "Division");
        }
        #endregion
        [HttpPost]
        public JsonResult GetPositionTypeForDivision(Guid Divisionid)
        {
            bool IsError = false;
            String Message = string.Empty;
            String Data = string.Empty;
            try
            {
                RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.Division>> _objBreadScrumbDivision = new BreadScrumbModel<BESrp.DynamicSRP<BEClient.Division>>();
                BLClient.PositionTypeAction objPositionTypeAction = new PositionTypeAction(_CurrentClientId, true);
                _objBreadScrumbDivision.obj = new BESrp.DynamicSRP<BEClient.Division>();
                _objBreadScrumbDivision.obj.obj = new BEClient.Division();
                if (Divisionid != Guid.Empty)
                {
                    _objBreadScrumbDivision.obj.obj.SelectedPositionTypeList = new List<BEClient.PositionType>();
                    _objBreadScrumbDivision.obj.obj.SelectedPositionTypeList = objPositionTypeAction.GetSelectedPositionTypeByDivisionId(Divisionid);
                    _objBreadScrumbDivision.obj.obj.AvailablePositionTypeList = objPositionTypeAction.GetAllPostionsWithDivisionList(_CurrentLanguageId);
                }
                else
                {
                    List<BEClient.PositionType> lstPositionType = new List<BEClient.PositionType>();
                    lstPositionType = objPositionTypeAction.GetAllPositionTypeByClientAndLanguage(_CurrentLanguageId);
                    _objBreadScrumbDivision.obj.obj.AvailablePositionTypeList = lstPositionType;
                }
                _objBreadScrumbDivision.obj.UserPermissionWithScope = objPositionTypeAction.ListUserPermissionWithScope;
                Data = base.RenderRazorViewToString("Shared/_PositionType", _objBreadScrumbDivision);
            }
            catch
            {
                throw;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data));
        }

        public JsonResult GetPositionTypeModel(Guid DivisionId)
        {
            bool IsError = false;
            String Message = string.Empty;
            String Data = string.Empty;
            try
            {
                BEClient.PositionType objPositionType = new BEClient.PositionType();
                objPositionType.DivisionId = DivisionId;
                objPositionType.PositionTypeEntityLanguage = new List<BEClient.EntityLanguage>();
                foreach (BEMaster.ClientLanguage clientLanguage in ATSCommon.CurrentSession.Instance.VerifiedClient.ClientLanguageList.OrderBy(r => r.objLanguage.LanguageName))
                {
                    if (objPositionType.PositionTypeEntityLanguage.Where(x => x.LanguageId == clientLanguage.LanguageId).Count() <= 0)
                    {
                        BEClient.EntityLanguage entitylanguage = new BEClient.EntityLanguage();
                        entitylanguage.LanguageId = clientLanguage.LanguageId;
                        entitylanguage.LocalName = clientLanguage.LocalName;
                        objPositionType.PositionTypeEntityLanguage.Add(entitylanguage);
                    }
                }
                Data = base.RenderRazorViewToString("Shared/_AddEditPositionType", objPositionType);
            }
            catch
            {
                throw;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data));
        }

        public JsonResult InsertPositionType(BEClient.PositionType positiontype)
        {
            bool IsError = false;
            String Message = string.Empty;
            String Data = string.Empty;
            try
            {
                if (positiontype != null)
                {
                    BLClient.PositionTypeAction _objPositionTypeAction = new PositionTypeAction(_CurrentClientId);
                    positiontype.objDivision = new BEClient.DivisionMaster();
                    positiontype.objDivision.DivisionId = new string[] { positiontype.DivisionId.ToString() };
                    positiontype.ClientId = _CurrentClientId;
                    Guid newPositionTypeIdId = _objPositionTypeAction.AddPositiontype(positiontype);
                    if (newPositionTypeIdId != null && !newPositionTypeIdId.Equals(Guid.Empty))
                    {
                        Message = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.SuccessMessages.RECORD_ADDED_SUCCESSFULLY).ToString();
                    }
                    else
                    {
                        Message = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.ErrorMessages.NOT_ABLE_TO_ADD_RECORD).ToString();
                        IsError = true;
                    }
                }

            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data));
        }

        public JsonResult InsertJobLocation(BEClient.JobLocation jobLocation)
        {
            bool IsError = false;
            String Message = string.Empty;
            String Data = string.Empty;
            try
            {
                if (jobLocation != null)
                {
                    BLClient.JobLocationAction _objJobLocationAction = new JobLocationAction(_CurrentClientId);
                    Guid newJobLocationId = _objJobLocationAction.AddJobLocation(jobLocation);
                    if (newJobLocationId != null && !newJobLocationId.Equals(Guid.Empty))
                    {
                        Message = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.SuccessMessages.RECORD_ADDED_SUCCESSFULLY).ToString();
                    }
                    else
                    {
                        Message = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.ErrorMessages.NOT_ABLE_TO_ADD_RECORD).ToString();
                        IsError = true;
                    }
                }
            }
            catch
            {
                Message = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.ErrorMessages.NOT_ABLE_TO_ADD_RECORD).ToString();
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data));
        }

        public JsonResult GetJobLocationByDivision(Guid DivisionId)
        {
            bool IsError = false;
            String Message = string.Empty;
            String Data = string.Empty;
            try
            {
                RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.Division>> _objBreadScrumbDivision = new BreadScrumbModel<BESrp.DynamicSRP<BEClient.Division>>();
                BLClient.JobLocationAction objJobLocationAction = new JobLocationAction(_CurrentClientId, true);
                _objBreadScrumbDivision.obj = new BESrp.DynamicSRP<BEClient.Division>();
                _objBreadScrumbDivision.obj.obj = new BEClient.Division();
                if (DivisionId != Guid.Empty)
                {
                    //TWICE : DO NOT NEED
                    //List<BEClient.JobLocation> lstjobLocation = new List<BEClient.JobLocation>();
                    //lstjobLocation = objJobLocationAction.GetAllJobLocationSByDivisionId(DivisionId, _CurrentLanguageId);

                    _objBreadScrumbDivision.obj.obj.SelectedJobLocationList = new List<BEClient.JobLocation>();
                    _objBreadScrumbDivision.obj.obj.SelectedJobLocationList = objJobLocationAction.GetAllJobLocationSByDivisionId(DivisionId, _CurrentLanguageId);
                    _objBreadScrumbDivision.obj.obj.AvailableJobLocationList = new List<BEClient.JobLocation>();
                    _objBreadScrumbDivision.obj.obj.AvailableJobLocationList = objJobLocationAction.GetAllJobLocationByLanguageId(_CurrentLanguageId);
                    _objBreadScrumbDivision.obj.obj.AvailableJobLocationList = _objBreadScrumbDivision.obj.obj.AvailableJobLocationList.Where(l1 => !_objBreadScrumbDivision.obj.obj.SelectedJobLocationList.Any(l2 => l2.JobLocationId == l1.JobLocationId)).ToList();

                    _objBreadScrumbDivision.obj.UserPermissionWithScope = objJobLocationAction.ListUserPermissionWithScope;
                    Data = base.RenderRazorViewToString("Shared/_JobLocation", _objBreadScrumbDivision);
                }
            }
            catch
            {
                throw;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data));
        }

        public JsonResult GetJobLocationModel(Guid DivisionId)
        {
            bool IsError = false;
            String Message = string.Empty;
            String Data = string.Empty;
            string callMethod = "Create";
            try
            {
                BEClient.JobLocation objJobLocation = new BEClient.JobLocation();
                objJobLocation.DivisionId = DivisionId;
                objJobLocation.JobLocatoinEntityLanguage = new List<BEClient.EntityLanguage>();
                foreach (BEMaster.ClientLanguage clientLanguage in ATSCommon.CurrentSession.Instance.VerifiedClient.ClientLanguageList.OrderBy(r => r.objLanguage.LanguageName))
                {
                    if (objJobLocation.JobLocatoinEntityLanguage.Where(x => x.LanguageId == clientLanguage.LanguageId).Count() <= 0)
                    {
                        BEClient.EntityLanguage entitylanguage = new BEClient.EntityLanguage();
                        entitylanguage.LanguageId = clientLanguage.LanguageId;
                        entitylanguage.LocalName = clientLanguage.LocalName;
                        objJobLocation.JobLocatoinEntityLanguage.Add(entitylanguage);
                    }
                }
                FillJobLocationDropDown(DivisionId, callMethod);
                Data = base.RenderRazorViewToString("Shared/_AddEditJobLocation", objJobLocation);
            }
            catch
            {
                throw;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data));
        }

        private void FillJobLocationDropDown(Guid divisionId, string callMethod)
        {
            BLClient.JobLocationAction _objJobLocationAction = new JobLocationAction(_CurrentClientId);
            List<BEClient.Division> lstDivision = new List<BEClient.Division>();
            BLClient.DivisionAction objDivisionAction = new BLClient.DivisionAction(_CurrentClientId, false);
            lstDivision = objDivisionAction.GetAllDivisionsByLanguage(_CurrentLanguageId);
            ViewBag.ListOfDivision = new SelectList(lstDivision, "DivisionId", "DivisionName");
            /*
            List<BEClient.User> lstLocationManagers = new List<BEClient.User>();
            BLClient.UserAction objUserAction = new BLClient.UserAction(_CurrentClientId);
            lstLocationManagers = objUserAction.GetLocationManagers();
            ViewBag.ListOfLocationManagers = new SelectList(lstLocationManagers, "UserId", "FullName");
            List<BEClient.OnBoardManagers> lstOnBoardManagers = new List<BEClient.OnBoardManagers>();
            BLClient.UserAction _objOnboardUserAction = new BLClient.UserAction(base.CurrentClient.ClientId);
            List<BEClient.User> LstOnboardingUsers = new List<BEClient.User>();
            LstOnboardingUsers = _objOnboardUserAction.GetAllUOnboardingUser(divisionId);
            ViewBag.ListOfOnBoardingManager = new SelectList(LstOnboardingUsers, "UserId", "FullName");
            */

            List<BEClient.User> listOfEmployee = new List<BEClient.User>();
            BLClient.UserAction objUserAction = new BLClient.UserAction(_CurrentClientId);
            listOfEmployee = objUserAction.GetAllEmployees();

            ViewBag.ListOfOnBoardingManager = new SelectList(listOfEmployee, "UserId", "FullName");
            ViewBag.ListOfLocationManagers = new SelectList(listOfEmployee, "UserId", "FullName");
        }



        public JsonResult DeleteLocationDivision(Guid LocationDivisionId)
        {
            bool IsError = false;
            String Message = string.Empty;
            String Data = string.Empty;
            try
            {
                bool result = false;
                BLClient.LocationDivisionAction objLocationDivisionAction = new LocationDivisionAction(_CurrentClientId);
                result = objLocationDivisionAction.DelteLocationDivision(LocationDivisionId);

                if (result)
                {
                    Message = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.SuccessMessages.RECORD_DELETED_SUCCESSFULLY).ToString();
                }
                else
                {
                    Message = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.ErrorMessages.NOT_ABLE_TO_DELETE_RECORD).ToString();
                    IsError = true;
                }
            }
            catch
            {
                IsError = true;
                Message = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.ErrorMessages.NOT_ABLE_TO_DELETE_RECORD).ToString();
            }
            return GetJson(GetJsonContent(IsError, null, Message, Data));
        }
    }
}
