using System;
using System.Collections.Generic;
using BLClient = ATS.BusinessLogic;
using BEClient = ATS.BusinessEntity;
using BEMaster = ATS.BusinessEntity.Master;
using BLMaster = ATS.BusinessLogic.Master;
using ATSCommon = ATS.WebUi.Common;
using BECommon = ATS.BusinessEntity.Common;
using BESrp = ATS.BusinessEntity.SRPEntity;
using RootModels = ATS.WebUi.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATS.WebUi.Models;
using Newtonsoft.Json;
using System.Web.Mvc.Html;
using DisplayMessageConstant = ATS.BusinessEntity.Common;

namespace ATS.WebUi.Areas.Employee.Controllers
{
    public class ApplicationBasedStatusController : ATS.WebUi.Controllers.AreaBaseController
    {
        #region Private Members
        private BLClient.ApplicationBasedStatusAction _objAppBasedStatusAction;
        private Guid _CurrentClientId;
        private Guid _CurrentLanguageId;
        private ATS.BusinessEntity.SRPEntity.DynamicSRP<List<BEClient.ApplicationBasedStatus>> _objAppBasedStatusList;
        private ATS.BusinessEntity.SRPEntity.DynamicSRP<BEClient.ApplicationBasedStatus> _objAppBasedStatus;
        #endregion

        #region Redirection Method
        private string STR_APPBASEDSTATUS_LIST_METHOD = "Index";
        private string STR_APPBASEDSTATUS_VIEW_METHOD = "View";
        private string STR_FORMNAME = BEClient.Common.CompanySetupMenu.CSMNU_DECLINESTATUS;
        #endregion

        #region OnAuthorization Method call on every request
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (base.CurrentClient != null && Common.CurrentSession.Instance.VerifiedUser != null)
            {
                _CurrentClientId = base.CurrentClient.ClientId;
                _CurrentLanguageId = base.CurrentClient.CurrentLanguageId;
                _objAppBasedStatusAction = new BLClient.ApplicationBasedStatusAction(_CurrentClientId);

                //Create new object for List
                _objAppBasedStatusList = new BESrp.DynamicSRP<List<BEClient.ApplicationBasedStatus>>();

                _objAppBasedStatusList.AddBtnText = BEClient.Common.CommonConstant.ADD;
                _objAppBasedStatusList.EditBtnText = BEClient.Common.CommonConstant.UPDATE;
                _objAppBasedStatusList.RemoveBtnText = BEClient.Common.CommonConstant.DELETE;
                _objAppBasedStatusList.SaveBtnText = BEClient.Common.CommonConstant.SAVE;

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
        private void CreateObjVacancyStatus()
        {
            _objAppBasedStatus = new BESrp.DynamicSRP<BEClient.ApplicationBasedStatus>();
            _objAppBasedStatus.AddBtnText = _objAppBasedStatusList.AddBtnText;
            _objAppBasedStatus.EditBtnText = _objAppBasedStatusList.EditBtnText;
            _objAppBasedStatus.RemoveBtnText = _objAppBasedStatusList.RemoveBtnText;
            _objAppBasedStatus.SaveBtnText = _objAppBasedStatusList.SaveBtnText;

        }
        #endregion

        #region Add
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(RootModels.BreadScrumbModel<ATS.BusinessEntity.SRPEntity.DynamicSRP<BEClient.ApplicationBasedStatus>> _objBreadScrumbAppBasedStatus)
        {
            ATS.BusinessEntity.SRPEntity.DynamicSRP<BEClient.ApplicationBasedStatus> objAppStatus = _objBreadScrumbAppBasedStatus.obj;
            try
            {
                String Message = String.Empty;
                ViewBag.FormName = STR_FORMNAME;
                Guid newAppStatusId = _objAppBasedStatusAction.AddAppBasedStatus(objAppStatus.obj);
                JsonModels actionStatus = null;
               
                if (newAppStatusId != null && !newAppStatusId.Equals(Guid.Empty))
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
                return RedirectToAction(STR_APPBASEDSTATUS_LIST_METHOD, new { KeyMsg = keyMsg });
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

                _objBreadScrumbAppBasedStatus.obj = objAppStatus;
                SetBreadScrumb(ref _objBreadScrumbAppBasedStatus);
                FillDropdown();
                return View(ATS.WebUi.Common.Constants.STR_VIEW_METHOD, _objBreadScrumbAppBasedStatus);
            }
        }
        #endregion

        public ActionResult Index(string KeyMsg, string ordinal)
        {
            RootModels.BreadScrumbModel<ATS.BusinessEntity.SRPEntity.DynamicSRP<List<BEClient.ApplicationBasedStatus>>> _objBreadScrumbAppBasedstatus = new RootModels.BreadScrumbModel<BESrp.DynamicSRP<List<BEClient.ApplicationBasedStatus>>>();
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
                _objAppBasedStatusList.obj = _objAppBasedStatusAction.GetAllAppBasedStatus(_CurrentLanguageId);
                _objAppBasedStatusList.AddRecordURL = AddAppBasedStatusURL();
                NavIndex(ordinal);
                _objBreadScrumbAppBasedstatus.obj = _objAppBasedStatusList;
                _objBreadScrumbAppBasedstatus.DisplayName = STR_FORMNAME;
                _objBreadScrumbAppBasedstatus.ToolTip = STR_FORMNAME;
            }
            catch
            {
                throw;
            }
            return View(_objBreadScrumbAppBasedstatus);
        }

        #region View
        public ActionResult View(Guid? id, string ordinal)
        {
            RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.ApplicationBasedStatus>> _objBreadScrumbAppBasedstatus = new BreadScrumbModel<BESrp.DynamicSRP<BEClient.ApplicationBasedStatus>>();
            ViewBag.FormName = STR_FORMNAME;
            bool isEdit = false;
            if (id != null && !id.Equals(Guid.Empty))
            {
                isEdit = true;
                ViewBag.IsEdit = 1;
            }
            try
            {
                SetBreadScrumb(ref _objBreadScrumbAppBasedstatus);
                CreateObjVacancyStatus();
                _objAppBasedStatus.obj = new BEClient.ApplicationBasedStatus();

                if (isEdit)
                {
                    List<BEClient.RecordExists> LstRecordExists = new List<BEClient.RecordExists>();
                    BLClient.RecordExistsAction _objRecordExists = new BLClient.RecordExistsAction(_CurrentClientId);
                    _objAppBasedStatus.obj = _objAppBasedStatusAction.GetrecordByRecordId((Guid)id);
                    
                    foreach (BEMaster.ClientLanguage clientLanguage in ATSCommon.CurrentSession.Instance.VerifiedClient.ClientLanguageList.OrderBy(r => r.objLanguage.LanguageName))
                    {
                        if (_objAppBasedStatus.obj.ApplicationBasedStatusEntityLanguage.Where(x => x.LanguageId == clientLanguage.LanguageId).Count() <= 0)
                        {
                            BEClient.EntityLanguage entitylanguage = new BEClient.EntityLanguage();
                            entitylanguage.LanguageId = clientLanguage.LanguageId;
                            _objAppBasedStatus.obj.ApplicationBasedStatusEntityLanguage.Add(entitylanguage);
                        }
                    }
                    _objAppBasedStatus.ActionName = "Edit";
                    _objBreadScrumbAppBasedstatus.obj = _objAppBasedStatus;
                    _objBreadScrumbAppBasedstatus.ItemName = _objAppBasedStatus.obj.StatusText;
                    ViewBag.IsDelete = true;
                    NavView(id, _objBreadScrumbAppBasedstatus.ToolTip, ordinal);

                    #region RECENT VIEWED
                    BEClient.RecentlyViewed objRecentlView = new BEClient.RecentlyViewed();
                    BLClient.RecentlyViewedAction objRecentlyViewedAction = new BLClient.RecentlyViewedAction(_CurrentClientId);
                    objRecentlView.Category = BEClient.RecentlyViewedCategory.ApplicationBasedStatus.ToString();
                    objRecentlView.ViewItemId = (Guid)id;
                    objRecentlView.URL = HttpContext.Request.Url.ToString();

                    List<BEClient.RecentlyViewed> objList = objRecentlyViewedAction.GetRecentlyViewedApplication(objRecentlView);
                    _objBreadScrumbAppBasedstatus.objRecentViewedList = new List<BEClient.RecentlyViewed>();
                    _objBreadScrumbAppBasedstatus.objRecentViewedList = objList;
                    #endregion
                }
                else
                {
                    _objAppBasedStatus.obj.ApplicationBasedStatusEntityLanguage = new List<BEClient.EntityLanguage>();
                    foreach (BEMaster.ClientLanguage clientLanguage in ATSCommon.CurrentSession.Instance.VerifiedClient.ClientLanguageList.OrderBy(r => r.objLanguage.LanguageName))
                    {
                        BEClient.EntityLanguage entitylanguage = new BEClient.EntityLanguage();
                        entitylanguage.LanguageId = clientLanguage.LanguageId;
                        _objAppBasedStatus.obj.ApplicationBasedStatusEntityLanguage.Add(entitylanguage);
                    }
                    _objAppBasedStatus.ActionName = "Create";
                    SetBreadScrumb(ref _objBreadScrumbAppBasedstatus);
                    _objBreadScrumbAppBasedstatus.ItemName = "ADD APPLICANT DECLINED REASONS";
                    NavView(id, _objBreadScrumbAppBasedstatus.DisplayName, ordinal);
                }
                _objBreadScrumbAppBasedstatus.obj = _objAppBasedStatus;
                FillDropdown();
                return View(_objBreadScrumbAppBasedstatus);
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                FillDropdown();
                return RedirectToAction(STR_APPBASEDSTATUS_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }
        #endregion

        #region Edit Vacancny Status
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(RootModels.BreadScrumbModel<ATS.BusinessEntity.SRPEntity.DynamicSRP<BEClient.ApplicationBasedStatus>> _objBreadScrumbAppBaseStatus)
        {
            ATS.BusinessEntity.SRPEntity.DynamicSRP<BEClient.ApplicationBasedStatus> objAppBasedStatus = _objBreadScrumbAppBaseStatus.obj;
            String DisplayMessage = string.Empty;
            try
            {
                
                String Message = String.Empty;
                ViewBag.FormName = STR_FORMNAME;
                
                bool isRecordUpdated = _objAppBasedStatusAction.UpdateAppBasedStatus(objAppBasedStatus.obj);
                JsonModels actionStatus = null;
                if (isRecordUpdated)
                {
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
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                /*Redirect to List Page*/
                return RedirectToAction(STR_APPBASEDSTATUS_LIST_METHOD, new { KeyMsg = keyMsg });
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);

                /*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                BEClient.PageMode objPageMode = BEClient.PageMode.Update;
                ViewBag.PageMode = objPageMode;
                ViewBag.IsError = true;
                ViewBag.Message = ex.Message;
                FillDropdown();
                _objBreadScrumbAppBaseStatus.obj = objAppBasedStatus;
                SetBreadScrumb(ref _objBreadScrumbAppBaseStatus);
                return View(ATS.WebUi.Common.Constants.STR_VIEW_METHOD, _objBreadScrumbAppBaseStatus);
            }
        }
        #endregion

        #region Delete
        public ActionResult Delete(string id)
        {
            String DisplayMessage = string.Empty;
            try
            {
                bool Result = _objAppBasedStatusAction.Delete(id);
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
                /*Redirect to List Page*/
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                return RedirectToAction(STR_APPBASEDSTATUS_LIST_METHOD, new { KeyMsg = keyMsg });
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);

                /*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);

                /*Redirect to List Page*/
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                return RedirectToAction(STR_APPBASEDSTATUS_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }
        
        public ActionResult DeleteMultiple(string id)
        {
            string Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                bool Result = _objAppBasedStatusAction.Delete(id);
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
                Message = ex.Message;
            }
            return base.GetJson(GetJsonContent(IsError, null, Message, Data));
        }
        #endregion

        #region Dropdown

        private void FillDropdown()
        {
            try
            {
                ViewBag.AppBasedStatusCategory = Common.CommonFunctions.AppBasedStatusDDL(BEClient.AppBasedStatusDrp.Declined);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region NAvigation
        private void SetBreadScrumb(ref RootModels.BreadScrumbModel<ATS.BusinessEntity.SRPEntity.DynamicSRP<BEClient.ApplicationBasedStatus>> _objBreadScrumbAppBasedstatus)
        {
            _objBreadScrumbAppBasedstatus.DisplayName = STR_FORMNAME;
            _objBreadScrumbAppBasedstatus.ImagePath = BECommon.ImagePaths.VacancyImage;
            _objBreadScrumbAppBasedstatus.ToolTip = _objBreadScrumbAppBasedstatus.DisplayName;
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
            objBreadCrumb.Controller = "ApplicationBasedStatus";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_EMPLOYEE;
            objBreadCrumb.ordinal = objBreadCrumblst.Count() + 1;
            objBreadCrumb.URL = Url.Action("Index", "ApplicationBasedStatus", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, ordinal = objBreadCrumb.ordinal });
            objBreadCrumb.ImagePath = BECommon.ImagePaths.VacancyImage;
            objBreadCrumb.ToolTip = "Language Block";
            objBreadCrumb.WithoutOrdinalURL = Url.Action("Index", "ApplicationBasedStatus", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });

            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);

            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
        }


        private void NavView(Guid? AppBasedStatusId, String DisplayToolTip, String ordinal)
        {
            ATSCommon.CommonFunctions.SetNullNav(this.ControllerContext.RequestContext);
            List<BEClient.BreadCrumb> objBreadCrumblst = ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs;
            if (ordinal != null && !String.IsNullOrEmpty(ordinal.ToString()))
            {
                objBreadCrumblst.RemoveAll(x => x.ordinal >= Convert.ToInt32(ordinal.ToString()));
            }
            BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
            objBreadCrumb.Action = "View";
            objBreadCrumb.Controller = "ApplicationBasedStatus";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_EMPLOYEE;
            objBreadCrumb.ordinal = objBreadCrumblst.Count() + 1;
            objBreadCrumb.URL = Url.Action(STR_APPBASEDSTATUS_VIEW_METHOD, "ApplicationBasedStatus", new { id = AppBasedStatusId, area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, ordinal = objBreadCrumb.ordinal });
            objBreadCrumb.ImagePath = BECommon.ImagePaths.VacancyImage;
            objBreadCrumb.ToolTip = "Decline Applicant Reasons</br>" + DisplayToolTip;
            objBreadCrumb.WithoutOrdinalURL = Url.Action("View", "ApplicationBasedStatus", new { id = AppBasedStatusId, area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });

            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);

            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;

        }
        #endregion navigation

        #region CreateURL

        public string AddAppBasedStatusURL()
        {
            UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);
            return u.Action(STR_APPBASEDSTATUS_VIEW_METHOD, "ApplicationBasedStatus");
        }
        #endregion
    }
}
