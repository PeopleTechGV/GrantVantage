using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEClient = ATS.BusinessEntity;
using BLClient = ATS.BusinessLogic;
using BEMaster = ATS.BusinessEntity.Master;
using BLMaster = ATS.BusinessLogic.Master;
using ATSCommon = ATS.WebUi.Common;
using ATS.WebUi.Models;
using Newtonsoft.Json;
using BESrp = ATS.BusinessEntity.SRPEntity;
using BECommon = ATS.BusinessEntity.Common;
using log4net;
using RootModels = ATS.WebUi.Models;
using System.Web.Mvc.Html;
using DisplayMessageConstant = ATS.BusinessEntity.Common;
namespace ATS.WebUi.Areas.Employee.Controllers
{
    public class PositionTypeController : ATS.WebUi.Controllers.AreaBaseController
    {
        //
        // GET: /Admin/PositionType/


        #region Private Members
        private BLClient.PositionTypeAction _objPositionTypeAction;
        private Guid _CurrentClientId;
        private Guid _CurrentLanguageId;
        private string STR_FORMNAME = BEClient.Common.CompanySetupMenu.CSMNU_POSITIONTYPE;
        private BESrp.DynamicSRP<List<BEClient.PositionType>> _objPositionTypeList;
        private BESrp.DynamicSRP<BEClient.PositionType> _objPositionType;
        private static readonly log4net.ILog log = LogManager.GetLogger(
       System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region Redirection Method
        private string STR_POSITIONTYPE_LIST_METHOD = "Index";
        private string STR_POSITIONTYPECREATE_METHOD = "Create";
        private string STR_POSITIONTYPEEDIT_METHOD = "Edit";
        private string STR_POSITIONTYPEVIEW_METHOD = "View";

        #endregion
        #region OnAuthorization Method call on every request
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
                _objPositionTypeAction = new BLClient.PositionTypeAction(_CurrentClientId, true);

                //Create new object for List
                _objPositionTypeList = new BESrp.DynamicSRP<List<BEClient.PositionType>>();
                _objPositionTypeList.AddBtnText = BECommon.CommonConstant.ADD;
                _objPositionTypeList.EditBtnText = BECommon.CommonConstant.UPDATE;
                _objPositionTypeList.RemoveBtnText = BECommon.CommonConstant.DELETE;
                _objPositionTypeList.SaveBtnText = BECommon.CommonConstant.SAVE;
                _objPositionTypeList.UserPermissionWithScope = _objPositionTypeAction.ListUserPermissionWithScope;
                ViewBag.FormName = STR_FORMNAME;
                if (ATSCommon.CurrentSession.Instance.VerifiedUser.SecurityRoles.Where(x => x.AtsSecurityRole == BEClient.ATSSecurityRole.Candidate).Count() > 0)
                {
                    filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_CANDIDATE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_CANDIDATE_CONTROLLER, new { area = ATSCommon.Constants.AREA_CANDIDATE }));

                }
                //if (Common.CurrentSession.Instance.VerifiedUser.SecurityRoles.Where(x => x.AtsSecurityRole.Equals(BEClient.ATSSecurityRole.SystemAdministrator)).Count() <= 0)
                //{
                //    filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_CONTROLLER, new { @KeyMsg = ATSCommon.CommonFunctions.NonAuthoriseduser() }));
                //}
            }
        }
        #endregion

        #region Create Signle Pbject
        private void CreateObjDivision(Guid? DivisionId)
        {
            _objPositionType = new BESrp.DynamicSRP<BEClient.PositionType>();
            _objPositionType.AddBtnText = _objPositionTypeList.AddBtnText;
            _objPositionType.EditBtnText = _objPositionTypeList.EditBtnText;
            _objPositionType.RemoveBtnText = _objPositionTypeList.RemoveBtnText;
            _objPositionType.SaveBtnText = _objPositionTypeList.SaveBtnText;
            _objPositionType.UserPermissionWithScope = _objPositionTypeAction.ListUserPermissionWithScope;

            if (DivisionId != null)
                _objPositionType.RemoveRecordURL = RemovePositionTypeURL((Guid)DivisionId);
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
            objBreadCrumb.Controller = "PositionType";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_EMPLOYEE;
            objBreadCrumb.ordinal = objBreadCrumblst.Count() + 1;
            objBreadCrumb.URL = Url.Action("Index", "PositionType", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, ordinal = objBreadCrumb.ordinal });
            objBreadCrumb.ImagePath = BECommon.ImagePaths.VacancyImage;
            objBreadCrumb.ToolTip = "PositionType";
            objBreadCrumb.WithoutOrdinalURL = Url.Action("Index", "PositionType", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });

            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);

            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
        }

        private void NavView(Guid? PositionTypeId, String DisplayToolTip, String ordinal)
        {

            ATSCommon.CommonFunctions.SetNullNav(this.ControllerContext.RequestContext);
            List<BEClient.BreadCrumb> objBreadCrumblst = ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs;
            if (ordinal != null && !String.IsNullOrEmpty(ordinal.ToString()))
            {
                objBreadCrumblst.RemoveAll(x => x.ordinal >= Convert.ToInt32(ordinal.ToString()));
            }
            BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
            objBreadCrumb.Action = "View";
            objBreadCrumb.Controller = "PositionType";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_EMPLOYEE;
            objBreadCrumb.ordinal = objBreadCrumblst.Count() + 1;
            objBreadCrumb.URL = Url.Action("View", "PositionType", new { id = PositionTypeId, area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, ordinal = objBreadCrumb.ordinal });
            objBreadCrumb.ImagePath = BECommon.ImagePaths.VacancyImage;
            objBreadCrumb.ToolTip = "PositionType </br>" + DisplayToolTip;
            objBreadCrumb.WithoutOrdinalURL = Url.Action("View", "PositionType", new { id = PositionTypeId, area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });
            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);
            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
        }
        #endregion
        public ActionResult Index(string KeyMsg, string ordinal)
        {
            RootModels.BreadScrumbModel<BESrp.DynamicSRP<List<BEClient.PositionType>>> _objBreadScrumbModel = new RootModels.BreadScrumbModel<BESrp.DynamicSRP<List<BEClient.PositionType>>>();
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
                _objPositionTypeList.obj = _objPositionTypeAction.GetAllPositionTypeByUsersAndLanguage(_CurrentLanguageId);
                _objPositionTypeList.AddRecordURL = AddPositionTypeURL();
                ViewBag.PageMode = BEClient.PageMode.View;
                NavIndex(ordinal);
                _objBreadScrumbModel.obj = _objPositionTypeList;
                _objBreadScrumbModel.DisplayName = STR_FORMNAME;
                _objBreadScrumbModel.ToolTip = STR_FORMNAME;
                return View(_objBreadScrumbModel);
            }
            catch
            {
                throw;
            }
        }

        #region Add Position type
        [HttpPost]
        public ActionResult Create(RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.PositionType>> _objBreadScrumbPositionType)
        {
            BESrp.DynamicSRP<BEClient.PositionType> objPositionType = _objBreadScrumbPositionType.obj;

            String DisplayMessage = string.Empty;
            try
            {
                objPositionType.obj.ClientId = _CurrentClientId;
                String Message = String.Empty;
                ViewBag.FormName = STR_FORMNAME;
                objPositionType.RecordPermissionType = (List<ATS.BusinessEntity.ATSPermissionType>)TempData["RecordPermissionType"];
                objPositionType.obj.RecordExistsCount = (List<ATS.BusinessEntity.RecordExists>)TempData["RecordExistCount"];
                objPositionType.RemoveBtnText = _objPositionTypeList.RemoveBtnText;
                objPositionType.SaveBtnText = _objPositionTypeList.SaveBtnText;
                Guid newPositionTypeIdId = _objPositionTypeAction.AddPositiontype(objPositionType.obj);
                JsonModels actionStatus = null;
                if (newPositionTypeIdId != null && !newPositionTypeIdId.Equals(Guid.Empty))
                {
                    DisplayMessage = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.SuccessMessages.RECORD_ADDED_SUCCESSFULLY).ToString();
                    actionStatus = base.GetJsonContent(false, string.Empty, DisplayMessage);
                }
                else
                {
                    DisplayMessage = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.ErrorMessages.NOT_ABLE_TO_ADD_RECORD).ToString();
                    actionStatus = base.GetJsonContent(true, string.Empty, DisplayMessage);
                }
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                return RedirectToAction(STR_POSITIONTYPE_LIST_METHOD, new { KeyMsg = keyMsg });
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                BLClient.DivisionAction ObjDivisionAction = new BLClient.DivisionAction(_CurrentClientId);
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                TempData["RecordPermissionType"] = objPositionType.RecordPermissionType;
                TempData["RecordExistCount"] = objPositionType.obj.RecordExistsCount;
                BEClient.PageMode objPageMode = BEClient.PageMode.Create;
                ViewBag.PageMode = objPageMode;
                ViewBag.IsError = true;
                ViewBag.Message = ex.Message;
                _objBreadScrumbPositionType.obj = objPositionType;
                _objBreadScrumbPositionType.obj.obj.AvailableDivisionList = ObjDivisionAction.GetAllDivisionByClientAndUsersTree(_CurrentLanguageId);
                SetBreadScrumb(ref _objBreadScrumbPositionType);
                return View(ATS.WebUi.Common.Constants.STR_VIEW_METHOD, _objBreadScrumbPositionType);
            }
        }
        #endregion

        #region delete
        public ActionResult Delete(string id)
        {
            String DisplayMessage = string.Empty;

            try
            {
                bool Result = _objPositionTypeAction.Delete(id);
                JsonModels actionStatus = null;
                if (Result)
                {
                    DisplayMessage = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.SuccessMessages.RECORD_DELETED_SUCCESSFULLY).ToString();
                    actionStatus = base.GetJsonContent(false, string.Empty, DisplayMessage);
                }
                else
                {
                    DisplayMessage = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.ErrorMessages.NOT_ABLE_TO_DELETE_RECORD).ToString();

                    actionStatus = base.GetJsonContent(true, string.Empty, DisplayMessage);
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
                /*Redirect to List Page*/
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                return RedirectToAction(STR_POSITIONTYPE_LIST_METHOD, new { KeyMsg = keyMsg });
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);

                ///*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                /*Redirect to List Page*/
                return RedirectToAction(STR_POSITIONTYPE_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }

        public ActionResult DeleteMultiple(string id)
        {
            string Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {

                bool Result = _objPositionTypeAction.Delete(id);
                if (Result)
                {
                    Message = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.SuccessMessages.RECORD_DELETED_SUCCESSFULLY).ToString();
                }
                else
                {
                    IsError = true;
                    Message = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.ErrorMessages.NOT_ABLE_TO_DELETE_RECORD).ToString();

                }
                try
                {
                    ATSCommon.CommonFunctions.SolrEmployeeFullImport();
                }
                catch (Exception ex)
                {
                    log.Error("SOLR Employee FULL IMPORT (Employee)" + ex.Message);
                }

                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                Data = base.RenderRazorViewToString("_NavCompanySetup", null);
            }
            catch (Exception ex)
            {
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                IsError = true;
                Message = ex.Message;
                Data = "";
            }
            return base.GetJson(GetJsonContent(IsError, null, Message, Data));

        }
        #endregion

        #region Edit Position Type
        [HttpPost]
        public ActionResult Edit(RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.PositionType>> _objBreadScrumbPositionType)
        {
            BESrp.DynamicSRP<BEClient.PositionType> objPositionType = _objBreadScrumbPositionType.obj;
            String DisplayMessage = string.Empty;

            try
            {
                String Message = String.Empty;
                ViewBag.FormName = STR_FORMNAME;
                objPositionType.RecordPermissionType = (List<ATS.BusinessEntity.ATSPermissionType>)TempData["RecordPermissionType"];
                objPositionType.obj.RecordExistsCount = (List<ATS.BusinessEntity.RecordExists>)TempData["RecordExistCount"];
                objPositionType.obj.PermissionType = (List<ATS.BusinessEntity.ATSPermissionType>)TempData["PermissionType"];
                objPositionType.RemoveBtnText = _objPositionTypeList.RemoveBtnText;
                objPositionType.SaveBtnText = _objPositionTypeList.SaveBtnText;
                bool isRecordUpdated = _objPositionTypeAction.UpdatePositionType(objPositionType.obj);
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
                    DisplayMessage = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.SuccessMessages.RECORD_UPDATED_SUCCESSFULLY).ToString();

                    actionStatus = base.GetJsonContent(!isRecordUpdated, string.Empty, DisplayMessage);
                }
                else
                {
                    DisplayMessage = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.ErrorMessages.NOT_ABLE_TO_UPDATE_RECORD).ToString();

                    actionStatus = base.GetJsonContent(!isRecordUpdated, string.Empty, DisplayMessage);
                }
                /*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                /*Redirect to List Page*/
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                return RedirectToAction(STR_POSITIONTYPE_LIST_METHOD, new { KeyMsg = keyMsg });
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);

                ///*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                TempData["RecordPermissionType"] = objPositionType.RecordPermissionType;
                TempData["RecordExistCount"] = objPositionType.obj.RecordExistsCount;
                TempData["PermissionType"] = objPositionType.obj.PermissionType;
                BEClient.PageMode objPageMode = BEClient.PageMode.Update;
                ViewBag.PageMode = objPageMode;
                ViewBag.IsError = true;
                ViewBag.Message = ex.Message;
                SetBreadScrumb(ref _objBreadScrumbPositionType);
                if (objPositionType.obj.PermissionType.Contains(BEClient.ATSPermissionType.Delete))
                {
                    ViewBag.IsDelete = true;
                }
                return View(ATS.WebUi.Common.Constants.STR_VIEW_METHOD, objPositionType);
            }
        }
        #endregion

        //public ActionResult View(Guid? id, string ordinal)
        //{
        //    bool isEdit = false;
        //    BEClient.PageMode objPageMode = BEClient.PageMode.View;
        //    if (id != null && !id.Equals(Guid.Empty))
        //    {
        //        isEdit = true;
        //        ViewBag.IsEdit = 1;
        //    }
        //    try
        //    {
        //        RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.PositionType>> _objBreadScrumbPositiontype = new BreadScrumbModel<BESrp.DynamicSRP<BEClient.PositionType>>();
        //        _objPositionType = new BESrp.DynamicSRP<BEClient.PositionType>();
        //        _objPositionType.obj = new BEClient.PositionType();
        //        SetBreadScrumb(ref  _objBreadScrumbPositiontype);
        //        BLClient.DivisionAction ObjDivisionAction = new BLClient.DivisionAction(_CurrentClientId);
        //        if (isEdit)
        //        {
        //            CreateObjDivision((Guid)id);
        //            List<BEClient.RecordExists> LstRecordExists = new List<BEClient.RecordExists>();
        //            BLClient.RecordExistsAction _objRecordExists = new BLClient.RecordExistsAction(_CurrentClientId);
        //            LstRecordExists = _objRecordExists.GetRecordsCountByPositionType((Guid)id);

        //            _objPositionType.ActionName = "Edit";
        //            _objPositionType.obj = _objPositionTypeAction.GetRecordByRecordId((Guid)id);
        //            _objPositionType.obj.RecordExistsCount = LstRecordExists;
        //            _objPositionType.obj.SelectedDivisionList = ObjDivisionAction.GetSelectedDivisionByPositionType(_objPositionType.obj.PositionTypeId); 
        //            BEClient.PositionType _objPositiontypeCount = new BEClient.PositionType();
        //            _objPositiontypeCount = _objPositionTypeAction.GetVacancyRecordCount((Guid)id, _CurrentLanguageId, Common.CurrentSession.Instance.UserId, "PositionType");
        //            _objPositionType.obj.VacancyCount = _objPositiontypeCount.VacancyCount;
        //            foreach (BEMaster.ClientLanguage clientLanguage in ATSCommon.CurrentSession.Instance.VerifiedClient.ClientLanguageList.OrderBy(r => r.objLanguage.LanguageName))
        //            {
        //                if (_objPositionType.obj.PositionTypeEntityLanguage.Where(x => x.LanguageId == clientLanguage.LanguageId).Count() <= 0)
        //                {
        //                    BEClient.EntityLanguage entitylanguage = new BEClient.EntityLanguage();
        //                    entitylanguage.LanguageId = clientLanguage.LanguageId;
        //                    entitylanguage.LocalName = clientLanguage.LocalName;
        //                    _objPositionType.obj.PositionTypeEntityLanguage.Add(entitylanguage);
        //                }
        //            }
        //            objPageMode = BEClient.PageMode.Update;
        //            if (_objPositionType.obj.PermissionType.Contains(BEClient.ATSPermissionType.Delete))
        //            {
        //                ViewBag.IsDelete = true;
        //            }
        //            _objBreadScrumbPositiontype.obj = _objPositionType;
        //            _objBreadScrumbPositiontype.ItemName = _objPositionType.obj.DefaultName;
        //            NavView(id, _objBreadScrumbPositiontype.ToolTip, ordinal);

        //            #region RECENT VIEWED
        //            BEClient.RecentlyViewed objRecentlView = new BEClient.RecentlyViewed();
        //            BLClient.RecentlyViewedAction objRecentlyViewedAction = new BLClient.RecentlyViewedAction(_CurrentClientId);
        //            objRecentlView.Category = BEClient.RecentlyViewedCategory.PositionType.ToString();
        //            objRecentlView.ViewItemId = (Guid)id;
        //            objRecentlView.URL = HttpContext.Request.Url.ToString();

        //            List<BEClient.RecentlyViewed> objList = objRecentlyViewedAction.GetRecentlyViewedApplication(objRecentlView);
        //            _objBreadScrumbPositiontype.objRecentViewedList = new List<BEClient.RecentlyViewed>();
        //            _objBreadScrumbPositiontype.objRecentViewedList = objList;
        //            #endregion

        //        }
        //        else
        //        {
        //            CreateObjDivision(id);
        //            _objPositionType.obj = new BEClient.PositionType();
        //            _objPositionType.obj.RecordExistsCount = new List<BEClient.RecordExists>();
        //            _objPositionType.obj.ClientId = _CurrentClientId;
        //            _objPositionType.obj.PositionTypeEntityLanguage = new List<BEClient.EntityLanguage>();
        //            foreach (BEMaster.ClientLanguage clientLanguage in ATSCommon.CurrentSession.Instance.VerifiedClient.ClientLanguageList.OrderBy(r => r.objLanguage.LanguageName))
        //            {
        //                BEClient.EntityLanguage entitylanguage = new BEClient.EntityLanguage();
        //                entitylanguage.LanguageId = clientLanguage.LanguageId;
        //                entitylanguage.LocalName = clientLanguage.LocalName;
        //                _objPositionType.obj.PositionTypeEntityLanguage.Add(entitylanguage);
        //            }
        //            _objPositionType.ActionName = "Create";
        //            objPageMode = BEClient.PageMode.Create;
        //            _objPositionType.obj.PermissionType = new List<BEClient.ATSPermissionType> { BEClient.ATSPermissionType.Create };
        //            _objBreadScrumbPositiontype.ItemName = "ADD PROGRAM TYPE";
        //            NavView(id, _objBreadScrumbPositiontype.DisplayName, ordinal);
        //        }
        //        _objPositionType.obj.AvailableDivisionList = ObjDivisionAction.GetAllDivisionByClientAndUsersTree(_CurrentLanguageId);
        //        ViewBag.PageMode = objPageMode;
        //        _objPositionType.RecordPermissionType = _objPositionType.obj.PermissionType;
        //        TempData["RecordPermissionType"] = _objPositionType.RecordPermissionType;
        //        TempData["RecordExistCount"] = _objPositionType.obj.RecordExistsCount;
        //        TempData["PermissionType"] = _objPositionType.obj.PermissionType;
        //        _objBreadScrumbPositiontype.obj = _objPositionType;
        //        return View(_objBreadScrumbPositiontype);
        //    }
        //    catch (Exception ex)
        //    {
        //        JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
        //        string jsonModels = JsonConvert.SerializeObject(actionStatus);
        //        string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
        //        return RedirectToAction(STR_POSITIONTYPE_LIST_METHOD, new { KeyMsg = keyMsg });
        //    }
        //}

        public ActionResult View(Guid? id, string ordinal)
        {
            bool isEdit = false;
            BEClient.PageMode objPageMode = BEClient.PageMode.View;
            if (id != null && !id.Equals(Guid.Empty))
            {
                isEdit = true;
                ViewBag.IsEdit = 1;
            }
            try
            {
                RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.PositionType>> _objBreadScrumbPositiontype = new BreadScrumbModel<BESrp.DynamicSRP<BEClient.PositionType>>();
                _objPositionType = new BESrp.DynamicSRP<BEClient.PositionType>();
                _objPositionType.obj = new BEClient.PositionType();
                SetBreadScrumb(ref  _objBreadScrumbPositiontype);
                BLClient.DivisionAction ObjDivisionAction = new BLClient.DivisionAction(_CurrentClientId, true);
                if (isEdit)
                {
                    CreateObjDivision((Guid)id);
                    List<BEClient.RecordExists> LstRecordExists = new List<BEClient.RecordExists>();
                    BLClient.RecordExistsAction _objRecordExists = new BLClient.RecordExistsAction(_CurrentClientId);
                    LstRecordExists = _objRecordExists.GetRecordsCountByPositionType((Guid)id);
                    
                    _objPositionType.ActionName = "Edit";
                    _objPositionType.obj = _objPositionTypeAction.GetRecordByRecordId((Guid)id, _CurrentLanguageId);

                    _objPositionType.obj.RecordExistsCount = LstRecordExists;
                    BEClient.PositionType _objPositiontypeCount = new BEClient.PositionType();
                    _objPositiontypeCount = _objPositionTypeAction.GetVacancyRecordCount((Guid)id, _CurrentLanguageId, Common.CurrentSession.Instance.UserId, "PositionType");
                    _objPositionType.obj.VacancyCount = _objPositiontypeCount.VacancyCount;
                    foreach (BEMaster.ClientLanguage clientLanguage in ATSCommon.CurrentSession.Instance.VerifiedClient.ClientLanguageList.OrderBy(r => r.objLanguage.LanguageName))
                    {
                        if (_objPositionType.obj.PositionTypeEntityLanguage.Where(x => x.LanguageId == clientLanguage.LanguageId).Count() <= 0)
                        {
                            BEClient.EntityLanguage entitylanguage = new BEClient.EntityLanguage();
                            entitylanguage.LanguageId = clientLanguage.LanguageId;
                            entitylanguage.LocalName = clientLanguage.LocalName;
                            _objPositionType.obj.PositionTypeEntityLanguage.Add(entitylanguage);
                        }
                    }
                    objPageMode = BEClient.PageMode.Update;
                    if (_objPositionType.obj.PermissionType.Contains(BEClient.ATSPermissionType.Delete))
                    {
                        ViewBag.IsDelete = true;
                    }
                    _objBreadScrumbPositiontype.obj = _objPositionType;
                    _objBreadScrumbPositiontype.ItemName = _objPositionType.obj.DefaultName;
                    NavView(id, _objBreadScrumbPositiontype.ToolTip, ordinal);

                    #region RECENT VIEWED
                    BEClient.RecentlyViewed objRecentlView = new BEClient.RecentlyViewed();
                    BLClient.RecentlyViewedAction objRecentlyViewedAction = new BLClient.RecentlyViewedAction(_CurrentClientId);
                    objRecentlView.Category = BEClient.RecentlyViewedCategory.PositionType.ToString();
                    objRecentlView.ViewItemId = (Guid)id;
                    objRecentlView.URL = HttpContext.Request.Url.ToString();

                    List<BEClient.RecentlyViewed> objList = objRecentlyViewedAction.GetRecentlyViewedApplication(objRecentlView);
                    _objBreadScrumbPositiontype.objRecentViewedList = new List<BEClient.RecentlyViewed>();
                    _objBreadScrumbPositiontype.objRecentViewedList = objList;
                    #endregion

                }
                else
                {
                    CreateObjDivision(id);
                    _objPositionType.obj = new BEClient.PositionType();
                    _objPositionType.obj.RecordExistsCount = new List<BEClient.RecordExists>();
                    _objPositionType.obj.ClientId = _CurrentClientId;
                    _objPositionType.obj.PositionTypeEntityLanguage = new List<BEClient.EntityLanguage>();
                    foreach (BEMaster.ClientLanguage clientLanguage in ATSCommon.CurrentSession.Instance.VerifiedClient.ClientLanguageList.OrderBy(r => r.objLanguage.LanguageName))
                    {
                        BEClient.EntityLanguage entitylanguage = new BEClient.EntityLanguage();
                        entitylanguage.LanguageId = clientLanguage.LanguageId;
                        entitylanguage.LocalName = clientLanguage.LocalName;
                        _objPositionType.obj.PositionTypeEntityLanguage.Add(entitylanguage);
                    }
                    _objPositionType.ActionName = "Create";
                    objPageMode = BEClient.PageMode.Create;
                    _objPositionType.obj.PermissionType = new List<BEClient.ATSPermissionType> { BEClient.ATSPermissionType.Create };
                    _objBreadScrumbPositiontype.ItemName = "ADD POSITION TYPE";
                    NavView(id, _objBreadScrumbPositiontype.DisplayName, ordinal);

                    _objPositionType.obj.AvailableDivisionList = ObjDivisionAction.GetAllDivisionByClientAndUsersTree(_CurrentLanguageId);
                    _objPositionType.obj.SelectedDivisionList = ObjDivisionAction.GetSelectedDivisionByPositionType(_objPositionType.obj.PositionTypeId);
                }

                ViewBag.PageMode = objPageMode;
                _objPositionType.RecordPermissionType = _objPositionType.obj.PermissionType;
                TempData["RecordPermissionType"] = _objPositionType.RecordPermissionType;
                TempData["RecordExistCount"] = _objPositionType.obj.RecordExistsCount;
                TempData["PermissionType"] = _objPositionType.obj.PermissionType;
                _objBreadScrumbPositiontype.obj = _objPositionType;
                return View(_objBreadScrumbPositiontype);
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                return RedirectToAction(STR_POSITIONTYPE_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }


        #region CreateURL
        public string RemovePositionTypeURL(Guid PositionTypeId)
        {
            UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);
            return u.Action("Remove", "PositionType", new { id = PositionTypeId.ToString() });
        }
        public string AddPositionTypeURL()
        {
            UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);
            return u.Action("View", "PositionType");
        }
        #endregion

        private void SetBreadScrumb(ref RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.PositionType>> _objBreadScrumbPositionType)
        {
            _objBreadScrumbPositionType.DisplayName = STR_FORMNAME;
            _objBreadScrumbPositionType.ImagePath = BECommon.ImagePaths.VacancyImage;
            _objBreadScrumbPositionType.ToolTip = _objBreadScrumbPositionType.DisplayName;
        }
    }
}
