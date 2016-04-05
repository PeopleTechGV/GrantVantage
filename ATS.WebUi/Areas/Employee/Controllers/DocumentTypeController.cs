using ATS.WebUi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using BLClient = ATS.BusinessLogic;
using BEClient = ATS.BusinessEntity;
using System.Web.Mvc;
using Newtonsoft.Json;
using ATSCommon = ATS.WebUi.Common;
using log4net;
using BECommon = ATS.BusinessEntity.Common;
using BESrp = ATS.BusinessEntity.SRPEntity;
using BEMaster = ATS.BusinessEntity.Master;
using RootModels = ATS.WebUi.Models;
using System.Web.Mvc.Html;
using DisplayMessageConstant = ATS.BusinessEntity.Common;

namespace ATS.WebUi.Areas.Employee.Controllers
{
    public class DocumentTypeController : ATS.WebUi.Controllers.AreaBaseController
    {
        #region Private Members
        private BLClient.DocumentTypeAction _objDocumentTypeAction;
        private Guid _CurrentClientId;
        private Guid _CurrentLanguageId;
        private BESrp.DynamicSRP<List<BEClient.DocumentType>> _objDocumentTypeList;
        private string STR_FORMNAME = BEClient.Common.CompanySetupMenu.CSMNU_DOCUMENTTYPES;
        private BESrp.DynamicSRP<BEClient.DocumentType> objDocumentType;
        private string STR_DOCUMENTTYPE_LIST_METHOD = "Index";
        private string STR_DOCUMENTTYPE_CREATE_METHOD = "Create";
        private string STR_DOCUMENTTYPE_EDIT_METHOD = "Edit";
        private string STR_DOCUMENTTYPE_VIEW_METHOD = "View";
        private static readonly log4net.ILog log = LogManager.GetLogger(
  System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
                _objDocumentTypeAction = new BLClient.DocumentTypeAction(_CurrentClientId);
                objDocumentType = new BESrp.DynamicSRP<BEClient.DocumentType>();

                //Create new object for List
                _objDocumentTypeList = new BESrp.DynamicSRP<List<BEClient.DocumentType>>();
                _objDocumentTypeList.AddBtnText = BECommon.CommonConstant.ADD;
                _objDocumentTypeList.EditBtnText = BECommon.CommonConstant.UPDATE;
                _objDocumentTypeList.RemoveBtnText = BECommon.CommonConstant.DELETE;
                _objDocumentTypeList.SaveBtnText = BECommon.CommonConstant.SAVE;
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

        #region Create Single object
        private void CreateObjDocumentType()
        {
            objDocumentType = new BESrp.DynamicSRP<BEClient.DocumentType>();
            objDocumentType.AddBtnText = _objDocumentTypeList.AddBtnText;
            objDocumentType.EditBtnText = _objDocumentTypeList.EditBtnText;
            objDocumentType.RemoveBtnText = _objDocumentTypeList.RemoveBtnText;
            objDocumentType.SaveBtnText = _objDocumentTypeList.SaveBtnText;
        }
        private void CreateObjDocumentType(BESrp.DynamicSRP<BEClient.DocumentType> ObjDocumentType)
        {
            ObjDocumentType.AddBtnText = _objDocumentTypeList.AddBtnText;
            ObjDocumentType.EditBtnText = _objDocumentTypeList.EditBtnText;
            ObjDocumentType.RemoveBtnText = _objDocumentTypeList.RemoveBtnText;
            ObjDocumentType.SaveBtnText = _objDocumentTypeList.SaveBtnText;
        }
        #endregion

        #region BReadScrum
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
            objBreadCrumb.Controller = "DocumentType";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_EMPLOYEE;
            objBreadCrumb.ordinal = objBreadCrumblst.Count() + 1;
            objBreadCrumb.URL = Url.Action("Index", "DocumentType", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, ordinal = objBreadCrumb.ordinal });
            objBreadCrumb.ImagePath = BECommon.ImagePaths.VacancyImage;
            objBreadCrumb.ToolTip = "Document Type";
            objBreadCrumb.WithoutOrdinalURL = Url.Action("Index", "DocumentType", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });

            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);

            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
        }

        private void NavView(Guid? DocumentTypeId, String DisplayToolTip, String ordinal)
        {
            ATSCommon.CommonFunctions.SetNullNav(this.ControllerContext.RequestContext);
            List<BEClient.BreadCrumb> objBreadCrumblst = ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs;
            if (ordinal != null && !String.IsNullOrEmpty(ordinal.ToString()))
            {
                objBreadCrumblst.RemoveAll(x => x.ordinal >= Convert.ToInt32(ordinal.ToString()));
            }
            BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
            objBreadCrumb.Action = "View";
            objBreadCrumb.Controller = "DocumentType";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_EMPLOYEE;
            objBreadCrumb.ordinal = objBreadCrumblst.Count() + 1;
            objBreadCrumb.URL = Url.Action("View", "DocumentType", new { id = DocumentTypeId, area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, ordinal = objBreadCrumb.ordinal });
            objBreadCrumb.ImagePath = BECommon.ImagePaths.VacancyImage;
            objBreadCrumb.ToolTip = "Document Type </br>" + DisplayToolTip;
            objBreadCrumb.WithoutOrdinalURL = Url.Action("View", "DocumentType", new { id = DocumentTypeId, area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });

            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);

            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;

        }
        #endregion

        public ActionResult Index(string KeyMsg, string ordinal)
        {
            RootModels.BreadScrumbModel<BESrp.DynamicSRP<List<BEClient.DocumentType>>> _objBreadScrumbModel = new RootModels.BreadScrumbModel<BESrp.DynamicSRP<List<BEClient.DocumentType>>>();
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
                _objDocumentTypeList.obj = _objDocumentTypeAction.GetAllDocmentType(_CurrentLanguageId);
                _objDocumentTypeList.AddRecordURL = AddDocumentTypeURL();
                NavIndex(ordinal);
                _objBreadScrumbModel.obj = _objDocumentTypeList;
                _objBreadScrumbModel.DisplayName = STR_FORMNAME;
                _objBreadScrumbModel.ToolTip = STR_FORMNAME;
                return View(_objBreadScrumbModel);

            }
            catch
            {
                throw;
            }
        }

        public ActionResult View(Guid? id, string ordinal)
        {
            RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.DocumentType>> _objBreadScrumbDocumentType = new BreadScrumbModel<BESrp.DynamicSRP<BEClient.DocumentType>>();
            ViewBag.FormName = STR_FORMNAME;
            bool isEdit = false;
            if (id != null && !id.Equals(Guid.Empty))
            {
                isEdit = true;
                ViewBag.IsEdit = 1;
            }
            try
            {
                CreateObjDocumentType();
                objDocumentType.obj = new BEClient.DocumentType();

                if (isEdit)
                {
                    List<BEClient.RecordExists> LstRecordExists = new List<BEClient.RecordExists>();
                    BLClient.RecordExistsAction _objRecordExists = new BLClient.RecordExistsAction(_CurrentClientId);
                    ViewBag.RedirectAction = STR_DOCUMENTTYPE_EDIT_METHOD;
                    objDocumentType.obj = _objDocumentTypeAction.GetDocmentTypeById((Guid)id, _CurrentLanguageId);
                    foreach (BEMaster.ClientLanguage clientLanguage in ATSCommon.CurrentSession.Instance.VerifiedClient.ClientLanguageList.OrderBy(r => r.objLanguage.LanguageName))
                    {
                        if (objDocumentType.obj.DocumentTypeEntityLanguage.Where(x => x.LanguageId == clientLanguage.LanguageId).Count() <= 0)
                        {
                            BEClient.EntityLanguage entitylanguage = new BEClient.EntityLanguage();
                            entitylanguage.LanguageId = clientLanguage.LanguageId;
                            objDocumentType.obj.DocumentTypeEntityLanguage.Add(entitylanguage);
                        }
                    }
                    ViewBag.IsDelete = true;
                    _objBreadScrumbDocumentType.obj = objDocumentType;
                    SetBreadScrum(ref _objBreadScrumbDocumentType);
                    NavView(id, _objBreadScrumbDocumentType.ToolTip, ordinal);
                    _objBreadScrumbDocumentType.ItemName = objDocumentType.obj.DocumentName;

                    //#region RECENT VIEWED
                    //BEClient.RecentlyViewed objRecentlView = new BEClient.RecentlyViewed();
                    //BLClient.RecentlyViewedAction objRecentlyViewedAction = new BLClient.RecentlyViewedAction(_CurrentClientId);
                    //objRecentlView.Category = BEClient.RecentlyViewedCategory.DocumentType.ToString();
                    //objRecentlView.ViewItemId = (Guid)id;
                    //objRecentlView.URL = HttpContext.Request.Url.ToString();
                    //List<BEClient.RecentlyViewed> objList = objRecentlyViewedAction.GetRecentlyViewedApplication(objRecentlView);
                    //_objBreadScrumbDocumentType.objRecentViewedList = new List<BEClient.RecentlyViewed>();
                    //_objBreadScrumbDocumentType.objRecentViewedList = objList;
                    //#endregion


                }
                else
                {
                    objDocumentType.obj.ClientId = _CurrentClientId;
                    objDocumentType.obj.DocumentTypeEntityLanguage = new List<BEClient.EntityLanguage>();
                    //objDocumentType.obj.RecordExistsCount = new List<BEClient.RecordExists>();
                    foreach (BEMaster.ClientLanguage clientLanguage in ATSCommon.CurrentSession.Instance.VerifiedClient.ClientLanguageList.OrderBy(r => r.objLanguage.LanguageName))
                    {
                        BEClient.EntityLanguage entitylanguage = new BEClient.EntityLanguage();
                        entitylanguage.LanguageId = clientLanguage.LanguageId;
                        objDocumentType.obj.DocumentTypeEntityLanguage.Add(entitylanguage);
                    }
                    ViewBag.RedirectAction = STR_DOCUMENTTYPE_CREATE_METHOD;
                    SetBreadScrum(ref _objBreadScrumbDocumentType);
                    NavView(id, _objBreadScrumbDocumentType.DisplayName, ordinal);
                    _objBreadScrumbDocumentType.ItemName = "ADD DOCUMENT TYPE";
                }
                _objBreadScrumbDocumentType.obj = objDocumentType;
                return View(_objBreadScrumbDocumentType);
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                return RedirectToAction(STR_DOCUMENTTYPE_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }

        private void SetBreadScrum(ref RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.DocumentType>> _objBreadScrumbDocumentType)
        {
            _objBreadScrumbDocumentType.DisplayName = STR_FORMNAME;
            _objBreadScrumbDocumentType.ImagePath = BECommon.ImagePaths.VacancyImage;
            _objBreadScrumbDocumentType.ToolTip = _objBreadScrumbDocumentType.DisplayName;
        }


        #region Add Document Type
        [HttpPost]
        public ActionResult Create(RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.DocumentType>> objBreadScrumbDocumentType)
        {
            BESrp.DynamicSRP<BEClient.DocumentType> objDocumentType = objBreadScrumbDocumentType.obj;
            String DisplayMessage = string.Empty;
            try
            {
                objDocumentType.obj.ClientId = _CurrentClientId;
                String Message = String.Empty;
                ViewBag.FormName = STR_FORMNAME;

                if (objDocumentType.obj.ExtensionTypes == null || objDocumentType.obj.ExtensionTypes == "")
                {
                    BEClient.PageMode objPageMode = BEClient.PageMode.View;
                    ViewBag.PageMode = objPageMode;
                    ViewBag.IsError = true;
                    ViewBag.Message = "Allowed Extension Types Required";
                    CreateObjDocumentType(objDocumentType);
                    objBreadScrumbDocumentType.obj = objDocumentType;
                    SetBreadScrum(ref objBreadScrumbDocumentType);
                    return View(ATS.WebUi.Common.Constants.STR_VIEW_METHOD, objBreadScrumbDocumentType);
                }

                Guid NewDocumentTypeId = _objDocumentTypeAction.InsertDocumentType(objDocumentType.obj);
                JsonModels actionStatus = null;
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                if (NewDocumentTypeId != null && !NewDocumentTypeId.Equals(Guid.Empty))
                {
                    DisplayMessage = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.SuccessMessages.RECORD_ADDED_SUCCESSFULLY).ToString();
                    actionStatus = base.GetJsonContent(false, string.Empty, DisplayMessage);
                    //BusinessLogic.Common.CacheHelper.GetDegreeType(_CurrentClientId, _CurrentLanguageId, CurrentClient.Clientname, true);
                }
                else
                {
                    DisplayMessage = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.ErrorMessages.NOT_ABLE_TO_ADD_RECORD).ToString();
                    actionStatus = base.GetJsonContent(true, string.Empty, DisplayMessage);
                }
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                return RedirectToAction(STR_DOCUMENTTYPE_LIST_METHOD, new { KeyMsg = keyMsg });
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                BEClient.PageMode objPageMode = BEClient.PageMode.Create;
                ViewBag.PageMode = objPageMode;
                ViewBag.IsError = true;
                ViewBag.Message = ex.Message;
                CreateObjDocumentType(objDocumentType);
                objBreadScrumbDocumentType.obj = objDocumentType;
                SetBreadScrum(ref objBreadScrumbDocumentType);
                return View(ATS.WebUi.Common.Constants.STR_VIEW_METHOD, objBreadScrumbDocumentType);
            }
        }
        #endregion


        #region Edit DocumentType
        [HttpPost]
        public ActionResult Edit(RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.DocumentType>> objBreadScrumbDocumentTypeType)
        {
            BESrp.DynamicSRP<BEClient.DocumentType> objDocumentType = objBreadScrumbDocumentTypeType.obj;
            CreateObjDocumentType(objDocumentType);
            String DisplayMessage = string.Empty;
            try
            {
                String Message = String.Empty;
                ViewBag.FormName = STR_FORMNAME;

                if (objDocumentType.obj.ExtensionTypes == null || objDocumentType.obj.ExtensionTypes == "")
                {
                    BEClient.PageMode objPageMode = BEClient.PageMode.Update;
                    ViewBag.PageMode = objPageMode;
                    ViewBag.IsError = true;
                    ViewBag.Message = "Allowed Extension Types Required.";
                    SetBreadScrum(ref objBreadScrumbDocumentTypeType);
                    return View(ATS.WebUi.Common.Constants.STR_VIEW_METHOD, objBreadScrumbDocumentTypeType);
                }
                JsonModels actionStatus = null;
                bool isRecordUpdated = _objDocumentTypeAction.UpdateDocumentType(objDocumentType.obj);

                if (isRecordUpdated)
                {
                    DisplayMessage = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.SuccessMessages.RECORD_UPDATED_SUCCESSFULLY).ToString();
                    actionStatus = base.GetJsonContent(!isRecordUpdated, string.Empty, DisplayMessage);
                    //BusinessLogic.Common.CacheHelper.GetDegreeType(_CurrentClientId, _CurrentLanguageId, CurrentClient.Clientname, true);
                }
                else
                {
                    DisplayMessage = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.ErrorMessages.NOT_ABLE_TO_UPDATE_RECORD).ToString();
                    actionStatus = base.GetJsonContent(!isRecordUpdated, string.Empty, DisplayMessage);
                }
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                return RedirectToAction(STR_DOCUMENTTYPE_LIST_METHOD, new { KeyMsg = keyMsg });
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                //objDocumentType.obj.RecordExistsCount = new List<BEClient.RecordExists>();
                BEClient.PageMode objPageMode = BEClient.PageMode.Update;
                ViewBag.PageMode = objPageMode;
                ViewBag.IsError = true;
                ViewBag.Message = ex.Message;
                SetBreadScrum(ref objBreadScrumbDocumentTypeType);
                return View(ATS.WebUi.Common.Constants.STR_VIEW_METHOD, objBreadScrumbDocumentTypeType);
            }
        }
        #endregion

        public string AddDocumentTypeURL()
        {
            UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);
            return u.Action(STR_DOCUMENTTYPE_VIEW_METHOD, "DocumentType");
        }


        public ActionResult Delete(string id)
        {
            String DisplayMessage = string.Empty;
            try
            {
                bool Result = _objDocumentTypeAction.Delete(id);
                JsonModels actionStatus = null;
                if (Result)
                {
                    DisplayMessage = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.SuccessMessages.RECORD_DELETED_SUCCESSFULLY).ToString();
                    actionStatus = base.GetJsonContent(false, string.Empty, DisplayMessage);
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
                    log.Error("SOLR Employee FULL IMPORT (DocumentType Delete)" + ex.Message);
                }
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                return RedirectToAction(STR_DOCUMENTTYPE_LIST_METHOD, new { KeyMsg = keyMsg });
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                return RedirectToAction(STR_DOCUMENTTYPE_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }

        public ActionResult DeleteMultiple(string id)
        {
            string Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            bool IsDefaultMessage = false;
            try
            {
                bool Result = _objDocumentTypeAction.Delete(id);
                if (Result)
                {
                    Message = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.SuccessMessages.RECORD_DELETED_SUCCESSFULLY).ToString();
                }
                else
                {
                    IsError = true;
                    Message = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.ErrorMessages.NOT_ABLE_TO_DELETE_RECORD).ToString();
                }
                Data = base.RenderRazorViewToString("_NavCompanySetup", null);
            }
            catch (Exception ex)
            {
                IsError = true;
                IsDefaultMessage = true;
                Message = ex.Message;
            }
            return base.GetJson(GetJsonContent(IsError, null, Message, Data, IsDefaultMessage));
        }
    }
}
