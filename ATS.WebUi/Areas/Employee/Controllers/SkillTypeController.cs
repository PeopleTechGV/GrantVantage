using ATS.WebUi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLClient = ATS.BusinessLogic;
using BEClient = ATS.BusinessEntity;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Newtonsoft.Json;
using ATSCommon = ATS.WebUi.Common;
using BEMaster = ATS.BusinessEntity.Master;
using BLMaster = ATS.BusinessLogic.Master;
using log4net;
using BECommon = ATS.BusinessEntity.Common;
using BESrp = ATS.BusinessEntity.SRPEntity;
using DisplayMessageConstant = ATS.BusinessEntity.Common;

using RootModels = ATS.WebUi.Models;

namespace ATS.WebUi.Areas.Employee.Controllers
{
    public class SkillTypeController : ATS.WebUi.Controllers.AreaBaseController
    {
        //
        // GET: /Employee/SkillType/

        #region Private Members
        private BLClient.SkillTypeAction _objSkillTypeAction;
        private Guid _CurrentClientId;
        private Guid _CurrentLanguageId;
        private BESrp.DynamicSRP<List<BEClient.SkillType>> _objSkilltypeList;
        private string STR_FORMNAME = BEClient.Common.CompanySetupMenu.CSMNU_SKILLTYPE;
        private BESrp.DynamicSRP<BEClient.SkillType> ObjSkillType;
        private string STR_SKILLTYPE_LIST_METHOD = "Index";
        private string STR_SKILLTYPE_CREATE_METHOD = "Create";
        private string STR_SKILLTYPE_EDIT_METHOD = "Edit";
        private string STR_SKILLTYPE_VIEW_METHOD = "View";
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
                _objSkillTypeAction = new BLClient.SkillTypeAction(_CurrentClientId);
                BESrp.DynamicSRP<BEClient.SkillType> objSkillType = new BESrp.DynamicSRP<BEClient.SkillType>();

                //Create new object for List
                _objSkilltypeList = new BESrp.DynamicSRP<List<BEClient.SkillType>>();
                _objSkilltypeList.AddBtnText = BECommon.CommonConstant.ADD;
                _objSkilltypeList.EditBtnText = BECommon.CommonConstant.UPDATE;
                _objSkilltypeList.RemoveBtnText = BECommon.CommonConstant.DELETE;
                _objSkilltypeList.SaveBtnText = BECommon.CommonConstant.SAVE;

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
        private void CreateObjSkillType()
        {

            ObjSkillType = new BESrp.DynamicSRP<BEClient.SkillType>();
            ObjSkillType.AddBtnText = _objSkilltypeList.AddBtnText;
            ObjSkillType.EditBtnText = _objSkilltypeList.EditBtnText;
            ObjSkillType.RemoveBtnText = _objSkilltypeList.RemoveBtnText;
            ObjSkillType.SaveBtnText = _objSkilltypeList.SaveBtnText;

        }
        private void CreateObjSkillType(BESrp.DynamicSRP<BEClient.SkillType> ObjSkillType)
        {


            ObjSkillType.AddBtnText = _objSkilltypeList.AddBtnText;
            ObjSkillType.EditBtnText = _objSkilltypeList.EditBtnText;
            ObjSkillType.RemoveBtnText = _objSkilltypeList.RemoveBtnText;
            ObjSkillType.SaveBtnText = _objSkilltypeList.SaveBtnText;

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
            objBreadCrumb.Controller = "SkillType";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_EMPLOYEE;
            objBreadCrumb.ordinal = objBreadCrumblst.Count() + 1;
            objBreadCrumb.URL = Url.Action("Index", "SkillType", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, ordinal = objBreadCrumb.ordinal });
            objBreadCrumb.ImagePath = BECommon.ImagePaths.VacancyImage;
            objBreadCrumb.ToolTip = "Skill Types"; 
            objBreadCrumb.WithoutOrdinalURL = Url.Action("Index", "SkillType", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });
            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);
            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
        }

        private void NavView(Guid? SkillTypeId, String DisplayToolTip, String ordinal)
        {
            ATSCommon.CommonFunctions.SetNullNav(this.ControllerContext.RequestContext);
            List<BEClient.BreadCrumb> objBreadCrumblst = ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs;
            if (ordinal != null && !String.IsNullOrEmpty(ordinal.ToString()))
            {
                objBreadCrumblst.RemoveAll(x => x.ordinal >= Convert.ToInt32(ordinal.ToString()));
            }
            BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
            objBreadCrumb.Action = "View";
            objBreadCrumb.Controller = "SkillType";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_EMPLOYEE;
            objBreadCrumb.ordinal = objBreadCrumblst.Count() + 1;
            objBreadCrumb.URL = Url.Action("View", "SkillType", new { id = SkillTypeId, area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, ordinal = objBreadCrumb.ordinal });
            objBreadCrumb.ImagePath = BECommon.ImagePaths.VacancyImage;
            objBreadCrumb.ToolTip = "Skill Type </br>" + DisplayToolTip;
            objBreadCrumb.WithoutOrdinalURL = Url.Action("View", "SkillType", new { id = SkillTypeId, area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });

            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);

            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;

        }
        #endregion

        public ActionResult Index(string KeyMsg, string ordinal)
        {
            RootModels.BreadScrumbModel<BESrp.DynamicSRP<List<BEClient.SkillType>>> _objBreadScrumbModel = new RootModels.BreadScrumbModel<BESrp.DynamicSRP<List<BEClient.SkillType>>>();
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
                _objSkilltypeList.obj = _objSkillTypeAction.GetAllSkillTypeByLanguage(_CurrentLanguageId);
                _objSkilltypeList.AddRecordURL = AddSkillTypeURL();
                NavIndex(ordinal);

                _objBreadScrumbModel.obj = _objSkilltypeList;
                _objBreadScrumbModel.DisplayName = STR_FORMNAME;
                _objBreadScrumbModel.ToolTip = STR_FORMNAME;


                return View(_objBreadScrumbModel);

            }
            catch
            {

                throw;
            }

        }

        #region SkillType By Id
        public ActionResult View(Guid? id, string ordinal)
        {
            RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.SkillType>> _objBreadScrumbSkillType = new BreadScrumbModel<BESrp.DynamicSRP<BEClient.SkillType>>();
            ViewBag.FormName = STR_FORMNAME;
            bool isEdit = false;
            if (id != null && !id.Equals(Guid.Empty))
            {
                isEdit = true;
                ViewBag.IsEdit = 1;
            }
            try
            {
                CreateObjSkillType();
                ObjSkillType.obj = new BEClient.SkillType();
                SetBreadCrumb(ref _objBreadScrumbSkillType);
                ObjSkillType.obj.RecordExistsCount = new List<BEClient.RecordExists>();
                if (isEdit)
                {
                    List<BEClient.RecordExists> LstRecordExists = new List<BEClient.RecordExists>();
                    BLClient.RecordExistsAction _objRecordExists = new BLClient.RecordExistsAction(_CurrentClientId);
                    LstRecordExists = _objRecordExists.GetRecordsCountBySkillType((Guid)id);

                    ViewBag.RedirectAction = STR_SKILLTYPE_EDIT_METHOD;
                    ObjSkillType.obj = _objSkillTypeAction.GetRecordByRecordId((Guid)id);
                    ObjSkillType.obj.RecordExistsCount = LstRecordExists;
                    foreach (BEMaster.ClientLanguage clientLanguage in ATSCommon.CurrentSession.Instance.VerifiedClient.ClientLanguageList.OrderBy(r => r.objLanguage.LanguageName))
                    {
                        if (ObjSkillType.obj.SkillTypeEntityLanguage.Where(x => x.LanguageId == clientLanguage.LanguageId).Count() <= 0)
                        {
                            BEClient.EntityLanguage entitylanguage = new BEClient.EntityLanguage();
                            entitylanguage.LanguageId = clientLanguage.LanguageId;
                            ObjSkillType.obj.SkillTypeEntityLanguage.Add(entitylanguage);
                        }
                    }
                    ViewBag.IsDelete = true;
                    _objBreadScrumbSkillType.ItemName = ObjSkillType.obj.DefaultName;
                    _objBreadScrumbSkillType.obj = ObjSkillType;
                    NavView(id, _objBreadScrumbSkillType.ToolTip, ordinal);

                    #region RECENT VIEWED
                    BEClient.RecentlyViewed objRecentlView = new BEClient.RecentlyViewed();
                    BLClient.RecentlyViewedAction objRecentlyViewedAction = new BLClient.RecentlyViewedAction(_CurrentClientId);
                    objRecentlView.Category = BEClient.RecentlyViewedCategory.SkillType.ToString();
                    objRecentlView.ViewItemId = (Guid)id;
                    objRecentlView.URL = HttpContext.Request.Url.ToString();
                    List<BEClient.RecentlyViewed> objList = objRecentlyViewedAction.GetRecentlyViewedApplication(objRecentlView);
                    _objBreadScrumbSkillType.objRecentViewedList = new List<BEClient.RecentlyViewed>();
                    _objBreadScrumbSkillType.objRecentViewedList = objList;
                    #endregion

                }
                else
                {
                    ObjSkillType.obj.ClientId = _CurrentClientId;
                    ObjSkillType.obj.SkillTypeEntityLanguage = new List<BEClient.EntityLanguage>();
                    foreach (BEMaster.ClientLanguage clientLanguage in ATSCommon.CurrentSession.Instance.VerifiedClient.ClientLanguageList.OrderBy(r => r.objLanguage.LanguageName))
                    {
                        BEClient.EntityLanguage entitylanguage = new BEClient.EntityLanguage();
                        entitylanguage.LanguageId = clientLanguage.LanguageId;
                        ObjSkillType.obj.SkillTypeEntityLanguage.Add(entitylanguage);
                    }
                    _objBreadScrumbSkillType.ItemName = "ADD SKILL";
                    ViewBag.RedirectAction = STR_SKILLTYPE_CREATE_METHOD;
                    NavView(id, _objBreadScrumbSkillType.DisplayName, ordinal);

                }
                _objBreadScrumbSkillType.obj = ObjSkillType;
                return View(_objBreadScrumbSkillType);
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);

                /*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);

                /*Redirect to List Page*/
                return RedirectToAction(STR_SKILLTYPE_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }
        #endregion

        #region Edit SkillType
        [HttpPost]
        public ActionResult Edit(RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.SkillType>> objBreadScrumbSkillType)
        {
            BESrp.DynamicSRP<BEClient.SkillType> objSkillType = objBreadScrumbSkillType.obj;
            CreateObjSkillType(objSkillType);
            String DisplayMessage = string.Empty;
            try
            {
                String Message = String.Empty;
                ViewBag.FormName = STR_FORMNAME;
                bool isRecordUpdated = _objSkillTypeAction.UpdateSkillType(objSkillType.obj);
                JsonModels actionStatus = null;
                if (isRecordUpdated)
                {
                    try
                    {
                        ATSCommon.CommonFunctions.SolrEmployeeFullImport();
                    }
                    catch (Exception ex)
                    {
                        log.Error("SOLR Employee FULL IMPORT (skilltype)" + ex.Message);
                    }
                    DisplayMessage = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.SuccessMessages.RECORD_UPDATED_SUCCESSFULLY).ToString();
                    actionStatus = base.GetJsonContent(!isRecordUpdated, string.Empty, DisplayMessage);
                }
                else
                {
                    DisplayMessage = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.ErrorMessages.NOT_ABLE_TO_UPDATE_RECORD).ToString();
                    actionStatus = base.GetJsonContent(!isRecordUpdated, string.Empty, DisplayMessage);
                }
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                return RedirectToAction(STR_SKILLTYPE_LIST_METHOD, new { KeyMsg = keyMsg });
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                objSkillType.obj.RecordExistsCount = new List<BEClient.RecordExists>();
                BEClient.PageMode objPageMode = BEClient.PageMode.Update;
                SetBreadCrumb(ref objBreadScrumbSkillType);
                ViewBag.PageMode = objPageMode;
                ViewBag.IsError = true;
                ViewBag.Message = ex.Message;
                return View(ATS.WebUi.Common.Constants.STR_VIEW_METHOD, objBreadScrumbSkillType);
            }
        }
        #endregion

        #region Add SkillType
        [HttpPost]
        public ActionResult Create(RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.SkillType>> objBreadScrumbSkillType)
        {
            BESrp.DynamicSRP<BEClient.SkillType> objSkillType = objBreadScrumbSkillType.obj;
            String DisplayMessage = string.Empty;
            try
            {
                objSkillType.obj.ClientId = _CurrentClientId;
                String Message = String.Empty;
                ViewBag.FormName = STR_FORMNAME;
                Guid newSkillTypeId = _objSkillTypeAction.AddSkillType(objSkillType.obj);
                JsonModels actionStatus = null;
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);

                if (newSkillTypeId != null && !newSkillTypeId.Equals(Guid.Empty))
                {
                    DisplayMessage = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.SuccessMessages.RECORD_ADDED_SUCCESSFULLY).ToString();
                    actionStatus = base.GetJsonContent(false, string.Empty, DisplayMessage);
                    BusinessLogic.Common.CacheHelper.GetSkillType(_CurrentClientId, _CurrentLanguageId, CurrentClient.Clientname, true);
                }
                else
                {
                    DisplayMessage = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.ErrorMessages.NOT_ABLE_TO_ADD_RECORD).ToString();
                    actionStatus = base.GetJsonContent(true, string.Empty, DisplayMessage);
                }
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                return RedirectToAction(STR_SKILLTYPE_LIST_METHOD, new { KeyMsg = keyMsg });
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                objSkillType.obj.RecordExistsCount = new List<BEClient.RecordExists>();
                BEClient.PageMode objPageMode = BEClient.PageMode.Create;
                ViewBag.PageMode = objPageMode;
                ViewBag.IsError = true;
                ViewBag.Message = ex.Message;
                CreateObjSkillType(objSkillType);
                objBreadScrumbSkillType.obj = objSkillType;
                SetBreadCrumb(ref objBreadScrumbSkillType);
                return View(ATS.WebUi.Common.Constants.STR_VIEW_METHOD, objBreadScrumbSkillType);
            }
        }
        #endregion

        #region Delete SkillType

        public ActionResult Delete(string id)
        {
            String DisplayMessage = string.Empty;
            try
            {
                bool Result = _objSkillTypeAction.Delete(id);
                JsonModels actionStatus = null;
                if (Result)
                {
                    DisplayMessage = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.SuccessMessages.RECORD_DELETED_SUCCESSFULLY).ToString();
                    actionStatus = base.GetJsonContent(false, string.Empty, DisplayMessage);
                    BusinessLogic.Common.CacheHelper.GetSkillType(_CurrentClientId, _CurrentLanguageId, CurrentClient.Clientname, true);
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
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);

                /*Redirect to List Page*/
                return RedirectToAction(STR_SKILLTYPE_LIST_METHOD, new { KeyMsg = keyMsg });
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);

                /*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);

                /*Redirect to List Page*/
                return RedirectToAction(STR_SKILLTYPE_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }

        public ActionResult DeleteMultiple(string id)
        {
            string Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {

                bool Result = _objSkillTypeAction.Delete(id);
                if (Result)
                {
                    Message = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.SuccessMessages.RECORD_DELETED_SUCCESSFULLY).ToString();
                    BusinessLogic.Common.CacheHelper.GetSkillType(_CurrentClientId, _CurrentLanguageId, CurrentClient.Clientname, true);
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

        public string AddSkillTypeURL()
        {
            UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);
            return u.Action("View", "SkillType");
        }
        #endregion

        private void SetBreadCrumb(ref RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.SkillType>> _objBreadScrumbSkillType)
        {
            _objBreadScrumbSkillType.DisplayName = STR_FORMNAME;
            _objBreadScrumbSkillType.ImagePath = BECommon.ImagePaths.VacancyImage;
            _objBreadScrumbSkillType.ToolTip = _objBreadScrumbSkillType.DisplayName;
        }
    }
}
