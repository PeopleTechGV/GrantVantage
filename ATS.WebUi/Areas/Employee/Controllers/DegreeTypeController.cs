using ATS.WebUi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLClient = ATS.BusinessLogic;
using BEClient = ATS.BusinessEntity;
using System.Web.Mvc;
using Newtonsoft.Json;
using ATSCommon = ATS.WebUi.Common;
using BEMaster = ATS.BusinessEntity.Master;
using BLMaster = ATS.BusinessLogic.Master;
using log4net;
using BECommon = ATS.BusinessEntity.Common;
using BESrp = ATS.BusinessEntity.SRPEntity;
using RootModels = ATS.WebUi.Models;
using System.Web.Mvc.Html;
using DisplayMessageConstant = ATS.BusinessEntity.Common;

namespace ATS.WebUi.Areas.Employee.Controllers
{
    public class DegreeTypeController : ATS.WebUi.Controllers.AreaBaseController
    {
        #region Private Members
        private BLClient.DegreeTypeAction _objDegreeTypeAction;
        private Guid _CurrentClientId;
        private Guid _CurrentLanguageId;
        private BESrp.DynamicSRP<List<BEClient.DegreeType>> _objDegreetypeList;
        private string STR_FORMNAME = BEClient.Common.CompanySetupMenu.CSMNU_DEGREETYPE;
        private BESrp.DynamicSRP<BEClient.DegreeType> ObjDegreeType;
        private string STR_DEGREETYPE_LIST_METHOD = "Index";
        private string STR_DEGREETYPE_CREATE_METHOD = "Create";
        private string STR_DEGREETYPE_EDIT_METHOD = "Edit";
        private string STR_DEGREETYPE_VIEW_METHOD = "View";
        private static readonly log4net.ILog log = LogManager.GetLogger(
  System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region OnAuthorization Method call on every request
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (base.CurrentClient != null && Common.CurrentSession.Instance.VerifiedUser != null)
            {
                _CurrentClientId = base.CurrentClient.ClientId;
                _CurrentLanguageId = base.CurrentClient.CurrentLanguageId;
                _objDegreeTypeAction = new BLClient.DegreeTypeAction(_CurrentClientId);
                ObjDegreeType = new BESrp.DynamicSRP<BEClient.DegreeType>();
                //Create new object for List
                _objDegreetypeList = new BESrp.DynamicSRP<List<BEClient.DegreeType>>();
                _objDegreetypeList.AddBtnText = BECommon.CommonConstant.ADD;
                _objDegreetypeList.EditBtnText = BECommon.CommonConstant.UPDATE;
                _objDegreetypeList.RemoveBtnText = BECommon.CommonConstant.DELETE;
                _objDegreetypeList.SaveBtnText = BECommon.CommonConstant.SAVE;
                if (ATSCommon.CurrentSession.Instance.VerifiedUser.SecurityRoles.Where(x => x.AtsSecurityRole == BEClient.ATSSecurityRole.Candidate).Count() > 0)
                {
                    filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_CANDIDATE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_CANDIDATE_CONTROLLER, new { area = ATSCommon.Constants.AREA_CANDIDATE }));
                }
                if (Common.CurrentSession.Instance.VerifiedUser.ManageCompanySetup == false)
                {
                    TempData["KeyMsg"] = ATSCommon.CommonFunctions.NonAuthoriseduser();
                    filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_CONTROLLER, new { @KeyMsg = ATSCommon.CommonFunctions.NonAuthoriseduser() }));
                }
                //if (Common.CurrentSession.Instance.VerifiedUser.SecurityRoles.Where(x => x.AtsSecurityRole.Equals(BEClient.ATSSecurityRole.SystemAdministrator)).Count() <= 0)
                //{
                //    filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_CONTROLLER, new { @KeyMsg = ATSCommon.CommonFunctions.NonAuthoriseduser() }));
                //}
            }
        }
        #endregion

        #region Create Single object
        private void CreateObjDegreeType()
        {
            ObjDegreeType = new BESrp.DynamicSRP<BEClient.DegreeType>();
            ObjDegreeType.AddBtnText = _objDegreetypeList.AddBtnText;
            ObjDegreeType.EditBtnText = _objDegreetypeList.EditBtnText;
            ObjDegreeType.RemoveBtnText = _objDegreetypeList.RemoveBtnText;
            ObjDegreeType.SaveBtnText = _objDegreetypeList.SaveBtnText;
        }
        private void CreateObjDegreeType(BESrp.DynamicSRP<BEClient.DegreeType> ObjDegreeType)
        {
            ObjDegreeType.AddBtnText = _objDegreetypeList.AddBtnText;
            ObjDegreeType.EditBtnText = _objDegreetypeList.EditBtnText;
            ObjDegreeType.RemoveBtnText = _objDegreetypeList.RemoveBtnText;
            ObjDegreeType.SaveBtnText = _objDegreetypeList.SaveBtnText;
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
            objBreadCrumb.Controller = "DegreeType";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_EMPLOYEE;
            objBreadCrumb.ordinal = objBreadCrumblst.Count() + 1;
            objBreadCrumb.URL = Url.Action("Index", "DegreeType", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, ordinal = objBreadCrumb.ordinal });
            objBreadCrumb.ImagePath = BECommon.ImagePaths.VacancyImage;
            objBreadCrumb.ToolTip = "Degree Type"; 
            objBreadCrumb.WithoutOrdinalURL = Url.Action("Index", "DegreeType", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });
            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);
            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
        }

        private void NavView(Guid? DegreeTypeId, String DisplayToolTip, String ordinal)
        {
            ATSCommon.CommonFunctions.SetNullNav(this.ControllerContext.RequestContext);
            List<BEClient.BreadCrumb> objBreadCrumblst = ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs;
            if (ordinal != null && !String.IsNullOrEmpty(ordinal.ToString()))
            {
                objBreadCrumblst.RemoveAll(x => x.ordinal >= Convert.ToInt32(ordinal.ToString()));
            }
            BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
            objBreadCrumb.Action = "View";
            objBreadCrumb.Controller = "DegreeType";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_EMPLOYEE;
            objBreadCrumb.ordinal = objBreadCrumblst.Count() + 1;
            objBreadCrumb.URL = Url.Action("View", "DegreeType", new { id = DegreeTypeId, area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, ordinal = objBreadCrumb.ordinal });
            objBreadCrumb.ImagePath = BECommon.ImagePaths.VacancyImage;
            objBreadCrumb.ToolTip = "Degee Type </br>" + DisplayToolTip; 
            objBreadCrumb.WithoutOrdinalURL = Url.Action("View", "DegreeType", new { id = DegreeTypeId, area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });
            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);
            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
        }
        #endregion

        public ActionResult Index(string KeyMsg, string ordinal)
        {
            RootModels.BreadScrumbModel<BESrp.DynamicSRP<List<BEClient.DegreeType>>> _objBreadScrumbModel = new RootModels.BreadScrumbModel<BESrp.DynamicSRP<List<BEClient.DegreeType>>>();
            JsonModels resultJsonModel = null;
            ViewBag.FormName = STR_FORMNAME;
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
                _objDegreetypeList.obj = _objDegreeTypeAction.GetAllDegreeTypeByLanguage(_CurrentLanguageId);
                _objDegreetypeList.AddRecordURL = AddDegreeTypeURL();
                NavIndex(ordinal);
                _objBreadScrumbModel.obj = _objDegreetypeList;
                _objBreadScrumbModel.DisplayName = STR_FORMNAME;
                _objBreadScrumbModel.ToolTip = STR_FORMNAME;
                return View(_objBreadScrumbModel);
            }
            catch
            {
                throw;
            }
        }

        #region DegreeType By Id
        public ActionResult View(Guid? id, string ordinal)
        {
            RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.DegreeType>> _objBreadScrumbDegreeType = new BreadScrumbModel<BESrp.DynamicSRP<BEClient.DegreeType>>();
            ViewBag.FormName = STR_FORMNAME;
            bool isEdit = false;
            if (id != null && !id.Equals(Guid.Empty))
            {
                isEdit = true;
                ViewBag.IsEdit = 1;
            }
            try
            {
                CreateObjDegreeType();
                ObjDegreeType.obj = new BEClient.DegreeType();
                if (isEdit)
                {
                    List<BEClient.RecordExists> LstRecordExists = new List<BEClient.RecordExists>();
                    BLClient.RecordExistsAction _objRecordExists = new BLClient.RecordExistsAction(_CurrentClientId);
                    LstRecordExists = _objRecordExists.GetRecordsCountByDegreeType((Guid)id);
                    ViewBag.RedirectAction = STR_DEGREETYPE_EDIT_METHOD;
                    ObjDegreeType.obj = _objDegreeTypeAction.GetRecordByRecordId((Guid)id);
                    ObjDegreeType.obj.RecordExistsCount = LstRecordExists;
                    foreach (BEMaster.ClientLanguage clientLanguage in ATSCommon.CurrentSession.Instance.VerifiedClient.ClientLanguageList.OrderBy(r => r.objLanguage.LanguageName))
                    {
                        if (ObjDegreeType.obj.DegreeTypeEntityLanguage.Where(x => x.LanguageId == clientLanguage.LanguageId).Count() <= 0)
                        {
                            BEClient.EntityLanguage entitylanguage = new BEClient.EntityLanguage();
                            entitylanguage.LanguageId = clientLanguage.LanguageId;
                            ObjDegreeType.obj.DegreeTypeEntityLanguage.Add(entitylanguage);
                        }
                    }
                    ViewBag.IsDelete = true;
                    _objBreadScrumbDegreeType.obj = ObjDegreeType;
                    SetBreadScrum(ref _objBreadScrumbDegreeType);
                    NavView(id, _objBreadScrumbDegreeType.ToolTip, ordinal);
                    _objBreadScrumbDegreeType.ItemName = ObjDegreeType.obj.DefaultName;
                    #region RECENT VIEWED
                    BEClient.RecentlyViewed objRecentlView = new BEClient.RecentlyViewed();
                    BLClient.RecentlyViewedAction objRecentlyViewedAction = new BLClient.RecentlyViewedAction(_CurrentClientId);
                    objRecentlView.Category = BEClient.RecentlyViewedCategory.DegreeType.ToString();
                    objRecentlView.ViewItemId = (Guid)id;
                    objRecentlView.URL = HttpContext.Request.Url.ToString();
                    List<BEClient.RecentlyViewed> objList = objRecentlyViewedAction.GetRecentlyViewedApplication(objRecentlView);
                    _objBreadScrumbDegreeType.objRecentViewedList = new List<BEClient.RecentlyViewed>();
                    _objBreadScrumbDegreeType.objRecentViewedList = objList;
                    #endregion
                }
                else
                {
                    ObjDegreeType.obj.ClientId = _CurrentClientId;
                    ObjDegreeType.obj.DegreeTypeEntityLanguage = new List<BEClient.EntityLanguage>();
                    ObjDegreeType.obj.RecordExistsCount = new List<BEClient.RecordExists>();
                    foreach (BEMaster.ClientLanguage clientLanguage in ATSCommon.CurrentSession.Instance.VerifiedClient.ClientLanguageList.OrderBy(r => r.objLanguage.LanguageName))
                    {
                        BEClient.EntityLanguage entitylanguage = new BEClient.EntityLanguage();
                        entitylanguage.LanguageId = clientLanguage.LanguageId;
                        ObjDegreeType.obj.DegreeTypeEntityLanguage.Add(entitylanguage);
                    }
                    ViewBag.RedirectAction = STR_DEGREETYPE_CREATE_METHOD;
                    SetBreadScrum(ref _objBreadScrumbDegreeType);
                    NavView(id, _objBreadScrumbDegreeType.DisplayName, ordinal);
                    _objBreadScrumbDegreeType.ItemName = "ADD DEGREE TYPE";
                }
                _objBreadScrumbDegreeType.obj = ObjDegreeType;
                return View(_objBreadScrumbDegreeType);
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                return RedirectToAction(STR_DEGREETYPE_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }
        #endregion

        #region Edit DegreeType
        [HttpPost]
        public ActionResult Edit(RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.DegreeType>> objBreadScrumbDegreeType)
        {
            BESrp.DynamicSRP<BEClient.DegreeType> objDegreeType = objBreadScrumbDegreeType.obj;
            CreateObjDegreeType(objDegreeType);
            String DisplayMessage = string.Empty;
            try
            {
                String Message = String.Empty;
                ViewBag.FormName = STR_FORMNAME;
                bool isRecordUpdated = _objDegreeTypeAction.UpdateDegreetype(objDegreeType.obj);
                JsonModels actionStatus = null;
                if (isRecordUpdated)
                {
                    try
                    {
                        ATSCommon.CommonFunctions.SolrEmployeeFullImport();
                    }
                    catch (Exception ex)
                    {
                        log.Error("SOLR Employee FULL IMPORT (DegreeTyr)" + ex.Message);
                    }
                    DisplayMessage = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.SuccessMessages.RECORD_UPDATED_SUCCESSFULLY).ToString();
                    actionStatus = base.GetJsonContent(!isRecordUpdated, string.Empty, DisplayMessage);
                    BusinessLogic.Common.CacheHelper.GetDegreeType(_CurrentClientId, _CurrentLanguageId, CurrentClient.Clientname, true);
                }
                else
                {
                    DisplayMessage = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.ErrorMessages.NOT_ABLE_TO_UPDATE_RECORD).ToString();
                    actionStatus = base.GetJsonContent(!isRecordUpdated, string.Empty, DisplayMessage);
                }
                /*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                /*Redirect to List Page*/
                return RedirectToAction(STR_DEGREETYPE_LIST_METHOD, new { KeyMsg = keyMsg });
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                objDegreeType.obj.RecordExistsCount = new List<BEClient.RecordExists>();
                BEClient.PageMode objPageMode = BEClient.PageMode.Update;
                ViewBag.PageMode = objPageMode;
                ViewBag.IsError = true;
                ViewBag.Message = ex.Message;
                SetBreadScrum(ref objBreadScrumbDegreeType);
                return View(ATS.WebUi.Common.Constants.STR_VIEW_METHOD, objBreadScrumbDegreeType);
            }
        }
        #endregion

        #region Add DegreeType
        [HttpPost]
        public ActionResult Create(RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.DegreeType>> objBreadScrumbDegreeType)
        {
            BESrp.DynamicSRP<BEClient.DegreeType> objDegreeType = objBreadScrumbDegreeType.obj;
            String DisplayMessage = string.Empty;
            try
            {
                objDegreeType.obj.ClientId = _CurrentClientId;
                String Message = String.Empty;
                ViewBag.FormName = STR_FORMNAME;
                Guid newDegreeTypeId = _objDegreeTypeAction.AddDegreeType(objDegreeType.obj);
                JsonModels actionStatus = null;
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                if (newDegreeTypeId != null && !newDegreeTypeId.Equals(Guid.Empty))
                {
                    DisplayMessage = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.SuccessMessages.RECORD_ADDED_SUCCESSFULLY).ToString();
                    actionStatus = base.GetJsonContent(false, string.Empty, DisplayMessage);
                    BusinessLogic.Common.CacheHelper.GetDegreeType(_CurrentClientId, _CurrentLanguageId, CurrentClient.Clientname, true);
                }
                else
                {
                    DisplayMessage = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.ErrorMessages.NOT_ABLE_TO_ADD_RECORD).ToString();
                    actionStatus = base.GetJsonContent(true, string.Empty, DisplayMessage);
                }
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                return RedirectToAction(STR_DEGREETYPE_LIST_METHOD, new { KeyMsg = keyMsg });
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                objDegreeType.obj.RecordExistsCount = new List<BEClient.RecordExists>();
                BEClient.PageMode objPageMode = BEClient.PageMode.Create;
                ViewBag.PageMode = objPageMode;
                ViewBag.IsError = true;
                ViewBag.Message = ex.Message;
                CreateObjDegreeType(objDegreeType);
                objBreadScrumbDegreeType.obj = objDegreeType;
                SetBreadScrum(ref objBreadScrumbDegreeType);
                return View(ATS.WebUi.Common.Constants.STR_VIEW_METHOD, objBreadScrumbDegreeType);
            }
        }
        #endregion

        #region Delete DegreeType
        public ActionResult Delete(string id)
        {
            String DisplayMessage = string.Empty;
            try
            {
                bool Result = _objDegreeTypeAction.Delete(id);
                JsonModels actionStatus = null;
                if (Result)
                {
                    DisplayMessage = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.SuccessMessages.RECORD_DELETED_SUCCESSFULLY).ToString();
                    actionStatus = base.GetJsonContent(false, string.Empty, DisplayMessage);
                    //Update Cache
                    BusinessLogic.Common.CacheHelper.GetDegreeType(_CurrentClientId, _CurrentLanguageId, CurrentClient.Clientname, true);
                }
                else
                {
                    DisplayMessage = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.ErrorMessages.NOT_ABLE_TO_DELETE_RECORD).ToString();
                    actionStatus = base.GetJsonContent(true, string.Empty, DisplayMessage);
                }
                try
                {
                    ATSCommon.CommonFunctions.SolrEmployeeFullImport();
                }
                catch (Exception ex)
                {
                    log.Error("SOLR Employee FULL IMPORT (Employee)" + ex.Message);
                }
                /*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                /*Redirect to List Page*/
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                return RedirectToAction(STR_DEGREETYPE_LIST_METHOD, new { KeyMsg = keyMsg });
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);

                /*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);

                /*Redirect to List Page*/
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                return RedirectToAction(STR_DEGREETYPE_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }
        
        public ActionResult DeleteMultiple(string id)
        {
            string Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                bool Result = _objDegreeTypeAction.Delete(id);
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

        #region CreateURL

        public string AddDegreeTypeURL()
        {
            UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);
            return u.Action(STR_DEGREETYPE_VIEW_METHOD, "DegreeType");
        }
        #endregion

        private void SetBreadScrum(ref RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.DegreeType>> _objBreadScrumbDegreeType)
        {
            _objBreadScrumbDegreeType.DisplayName = STR_FORMNAME;
            _objBreadScrumbDegreeType.ImagePath = BECommon.ImagePaths.VacancyImage;
            _objBreadScrumbDegreeType.ToolTip = _objBreadScrumbDegreeType.DisplayName;
        }
    }
}
