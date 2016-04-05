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
using BECommon = ATS.BusinessEntity.Common;
using BESrp = ATS.BusinessEntity.SRPEntity;
using ATS.WebUi.Models;
using Newtonsoft.Json;
using System.IO;
using RootModels = ATS.WebUi.Models;
using System.Web.Mvc.Html;
using DisplayMessageConstant = ATS.BusinessEntity.Common;

namespace ATS.WebUi.Areas.Employee.Controllers
{
    public class VacancyStatusController : ATS.WebUi.Controllers.AreaBaseController
    {
        // GET: /Employee/VacancyStatus/
        #region Private Members
        private BLClient.VacancyStatusAction _objVacancyStatusAction;
        private Guid _CurrentClientId;
        private Guid _CurrentLanguageId;
        private ATS.BusinessEntity.SRPEntity.DynamicSRP<List<BEClient.VacancyStatus>> _objVacancyStatusList;
        private ATS.BusinessEntity.SRPEntity.DynamicSRP<BEClient.VacancyStatus> _objVacancyStatus;
        #endregion

        #region Redirection Method
        private string STR_VACANCYSTATUS_LIST_METHOD = "Index";
        private string STR_VACANCYSTATUS_CREATE_METHOD = "Create";
        private string STR_VACANCYSTATUS_EDIT_METHOD = "Edit";
        private string STR_VACANCYSTATUS_VIEW_METHOD = "View";
        private string STR_FORMNAME = BEClient.Common.CompanySetupMenu.CSMNU_STATUSREASON;
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
                _objVacancyStatusAction = new BLClient.VacancyStatusAction(_CurrentClientId);
                _objVacancyStatusList = new BESrp.DynamicSRP<List<BEClient.VacancyStatus>>();
                _objVacancyStatusList.AddBtnText = BEClient.Common.CommonConstant.ADD;
                _objVacancyStatusList.EditBtnText = BEClient.Common.CommonConstant.UPDATE;
                _objVacancyStatusList.RemoveBtnText = BEClient.Common.CommonConstant.DELETE;
                _objVacancyStatusList.SaveBtnText = BEClient.Common.CommonConstant.SAVE;
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
            _objVacancyStatus = new BESrp.DynamicSRP<BEClient.VacancyStatus>();
            _objVacancyStatus.AddBtnText = _objVacancyStatusList.AddBtnText;
            _objVacancyStatus.EditBtnText = _objVacancyStatusList.EditBtnText;
            _objVacancyStatus.RemoveBtnText = _objVacancyStatusList.RemoveBtnText;
            _objVacancyStatus.SaveBtnText = _objVacancyStatusList.SaveBtnText;
        }
        #endregion

        public ActionResult Index(string KeyMsg, string ordinal)
        {
            RootModels.BreadScrumbModel<ATS.BusinessEntity.SRPEntity.DynamicSRP<List<BEClient.VacancyStatus>>> _objBreadScrumbVacancyStatus = new RootModels.BreadScrumbModel<ATS.BusinessEntity.SRPEntity.DynamicSRP<List<BEClient.VacancyStatus>>>();
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
                _objVacancyStatusList.obj = _objVacancyStatusAction.GetAllVacancyStatusByClientAndLanguage(_CurrentLanguageId);
                _objVacancyStatusList.AddRecordURL = AddVacancyStatusURL();
                NavIndex(ordinal);
                _objBreadScrumbVacancyStatus.obj = _objVacancyStatusList;
                _objBreadScrumbVacancyStatus.DisplayName = STR_FORMNAME;
                _objBreadScrumbVacancyStatus.ToolTip = STR_FORMNAME;
                return View(_objBreadScrumbVacancyStatus);
            }
            catch
            {
                throw;
            }
        }


        #region View
        public ActionResult View(Guid? id, string ordinal)
        {
            RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.VacancyStatus>> _objBreadScrumbVacancyStatus = new BreadScrumbModel<BESrp.DynamicSRP<BEClient.VacancyStatus>>();
            ViewBag.FormName = STR_FORMNAME;
            bool isEdit = false;
            if (id != null && !id.Equals(Guid.Empty))
            {
                isEdit = true;
                ViewBag.IsEdit = 1;
            }
            try
            {
                CreateObjVacancyStatus();
                _objVacancyStatus.obj = new BEClient.VacancyStatus();
                setEditBreadScrum(ref _objBreadScrumbVacancyStatus);
                if (isEdit)
                {
                    List<BEClient.RecordExists> LstRecordExists = new List<BEClient.RecordExists>();
                    BLClient.RecordExistsAction _objRecordExists = new BLClient.RecordExistsAction(_CurrentClientId);
                    LstRecordExists = _objRecordExists.GetRecordsCountByVacancyStatus((Guid)id);
                    _objVacancyStatus.obj = _objVacancyStatusAction.GetRecordByRecordId((Guid)id);
                    _objVacancyStatus.obj.RecordExistsCount = LstRecordExists;
                    foreach (BEMaster.ClientLanguage clientLanguage in ATSCommon.CurrentSession.Instance.VerifiedClient.ClientLanguageList.OrderBy(r => r.objLanguage.LanguageName))
                    {
                        if (_objVacancyStatus.obj.VacancyStatusEntityLanguage.Where(x => x.LanguageId == clientLanguage.LanguageId).Count() <= 0)
                        {
                            BEClient.EntityLanguage entitylanguage = new BEClient.EntityLanguage();
                            entitylanguage.LanguageId = clientLanguage.LanguageId;
                            _objVacancyStatus.obj.VacancyStatusEntityLanguage.Add(entitylanguage);
                        }
                    }
                    ViewBag.IsDelete = true;
                    _objVacancyStatus.ActionName = "Edit";
                    _objBreadScrumbVacancyStatus.ItemName = _objVacancyStatus.obj.VacancyStatusText;
                    _objBreadScrumbVacancyStatus.obj = _objVacancyStatus;
                    NavView(id, _objBreadScrumbVacancyStatus.ToolTip, ordinal);
                    BEClient.VacancyStatusDrp VacStatus = (BEClient.VacancyStatusDrp)Enum.Parse(typeof(BEClient.VacancyStatusDrp), _objVacancyStatus.obj.Category);
                    ViewBag.VacancyStatusCategory = Common.CommonFunctions.VacancyStatusDDL(VacStatus);
                    #region RECENT VIEWED
                    BEClient.RecentlyViewed objRecentlView = new BEClient.RecentlyViewed();
                    BLClient.RecentlyViewedAction objRecentlyViewedAction = new BLClient.RecentlyViewedAction(_CurrentClientId);
                    objRecentlView.Category = BEClient.RecentlyViewedCategory.StatusReason.ToString();
                    objRecentlView.ViewItemId = (Guid)id;
                    objRecentlView.URL = HttpContext.Request.Url.ToString();
                    List<BEClient.RecentlyViewed> objList = objRecentlyViewedAction.GetRecentlyViewedApplication(objRecentlView);
                    _objBreadScrumbVacancyStatus.objRecentViewedList = new List<BEClient.RecentlyViewed>();
                    _objBreadScrumbVacancyStatus.objRecentViewedList = objList;
                    #endregion
                }
                else
                {
                    _objVacancyStatus.obj.ClientId = _CurrentClientId;
                    _objVacancyStatus.obj.VacancyStatusEntityLanguage = new List<BEClient.EntityLanguage>();
                    _objVacancyStatus.obj.RecordExistsCount = new List<BEClient.RecordExists>();
                    foreach (BEMaster.ClientLanguage clientLanguage in ATSCommon.CurrentSession.Instance.VerifiedClient.ClientLanguageList.OrderBy(r => r.objLanguage.LanguageName))
                    {
                        BEClient.EntityLanguage entitylanguage = new BEClient.EntityLanguage();
                        entitylanguage.LanguageId = clientLanguage.LanguageId;
                        _objVacancyStatus.obj.VacancyStatusEntityLanguage.Add(entitylanguage);
                    }
                    _objVacancyStatus.ActionName = "Create";
                    _objBreadScrumbVacancyStatus.ItemName = "ADD STATUS REASON";
                    _objBreadScrumbVacancyStatus.ImagePath = BECommon.ImagePaths.VacancyImage;
                    setAddNewBreadScrum(ref _objBreadScrumbVacancyStatus);
                    NavView(id, _objBreadScrumbVacancyStatus.DisplayName, ordinal);
                    FillDropdown();
                }
                _objBreadScrumbVacancyStatus.obj = _objVacancyStatus;
                return View(_objBreadScrumbVacancyStatus);
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                /*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                /*Redirect to List Page*/
                return RedirectToAction(STR_VACANCYSTATUS_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }
        #endregion

        #region Add
        [HttpPost]
        public ActionResult Create(RootModels.BreadScrumbModel<ATS.BusinessEntity.SRPEntity.DynamicSRP<BEClient.VacancyStatus>> _objBreadScrumbVacancyStatus)
        {
            ATS.BusinessEntity.SRPEntity.DynamicSRP<BEClient.VacancyStatus> objVacancyStatus = _objBreadScrumbVacancyStatus.obj;
            try
            {
                objVacancyStatus.obj.ClientId = _CurrentClientId;
                String Message = String.Empty;
                ViewBag.FormName = STR_FORMNAME;
                Guid newVacancyStatusId = _objVacancyStatusAction.AddVacancyStatus(objVacancyStatus.obj);
                JsonModels actionStatus = null;
                if (newVacancyStatusId != null && !newVacancyStatusId.Equals(Guid.Empty))
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
                return RedirectToAction(STR_VACANCYSTATUS_LIST_METHOD, new { KeyMsg = keyMsg });
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                objVacancyStatus.obj.RecordExistsCount = new List<BEClient.RecordExists>();
                BEClient.PageMode objPageMode = BEClient.PageMode.Create;
                ViewBag.PageMode = objPageMode;
                ViewBag.IsError = true;
                ViewBag.Message = ex.Message;
                ViewBag.VacancyStatusCategory = Common.CommonFunctions.VacancyStatusDDL(null);
                _objBreadScrumbVacancyStatus.obj = objVacancyStatus;
                setAddNewBreadScrum(ref _objBreadScrumbVacancyStatus);
                return View(ATS.WebUi.Common.Constants.STR_VIEW_METHOD, _objBreadScrumbVacancyStatus);
            }
        }
        #endregion

        #region Delete
        public ActionResult Delete(string id)
        {
            try
            {
                bool Result = _objVacancyStatusAction.Delete(id);
                JsonModels actionStatus = null;
                if (Result)
                {
                    actionStatus = base.GetJsonContent(false, string.Empty, "Record Deleted Successfully");
                }
                else
                {
                    actionStatus = base.GetJsonContent(true, string.Empty, "Not Able To Delete Record");
                }
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                return RedirectToAction(STR_VACANCYSTATUS_LIST_METHOD, new { KeyMsg = keyMsg });
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                return RedirectToAction(STR_VACANCYSTATUS_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }

        public ActionResult DeleteMultiple(string id)
        {
            string Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {

                bool Result = _objVacancyStatusAction.Delete(id);
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

        #region Edit Vacancny Status
        [HttpPost]
        public ActionResult Edit(RootModels.BreadScrumbModel<ATS.BusinessEntity.SRPEntity.DynamicSRP<BEClient.VacancyStatus>> _objBreadScrumbVacancyStatus)
        {
            ATS.BusinessEntity.SRPEntity.DynamicSRP<BEClient.VacancyStatus> objVacancyStatus = _objBreadScrumbVacancyStatus.obj;
            try
            {
                String Message = String.Empty;
                ViewBag.FormName = STR_FORMNAME;
                bool isRecordUpdated = _objVacancyStatusAction.UpdateVacancyStatus(objVacancyStatus.obj);
                JsonModels actionStatus = null;
                if (isRecordUpdated)
                {
                    actionStatus = base.GetJsonContent(!isRecordUpdated, string.Empty, "Record Updated Successfully");
                }
                else
                {
                    actionStatus = base.GetJsonContent(!isRecordUpdated, string.Empty, "Not Able To Update Record");
                }
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                return RedirectToAction(STR_VACANCYSTATUS_LIST_METHOD, new { KeyMsg = keyMsg });
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                objVacancyStatus.obj.RecordExistsCount = new List<BEClient.RecordExists>();
                BEClient.PageMode objPageMode = BEClient.PageMode.Update;
                ViewBag.PageMode = objPageMode;
                ViewBag.IsError = true;
                ViewBag.Message = ex.Message;
                _objBreadScrumbVacancyStatus.obj = objVacancyStatus;
                setEditBreadScrum(ref _objBreadScrumbVacancyStatus);
                return View(ATS.WebUi.Common.Constants.STR_VIEW_METHOD, _objBreadScrumbVacancyStatus);
            }
        }
        #endregion

        #region Dropdown
        private void FillDropdown()
        {
            try
            {
                ViewBag.VacancyStatusCategory = Common.CommonFunctions.VacancyStatusDDL(BEClient.VacancyStatusDrp.Close);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region NAvigation
        private void setAddNewBreadScrum(ref RootModels.BreadScrumbModel<ATS.BusinessEntity.SRPEntity.DynamicSRP<BEClient.VacancyStatus>> _objBreadScrumbVacancyStatus)
        {
            _objBreadScrumbVacancyStatus.DisplayName = STR_FORMNAME;
            _objBreadScrumbVacancyStatus.ImagePath = BECommon.ImagePaths.VacancyImage;
            _objBreadScrumbVacancyStatus.ToolTip = _objBreadScrumbVacancyStatus.DisplayName;
        }
        private void setEditBreadScrum(ref RootModels.BreadScrumbModel<ATS.BusinessEntity.SRPEntity.DynamicSRP<BEClient.VacancyStatus>> _objBreadScrumbVacancyStatus)
        {
            _objBreadScrumbVacancyStatus.DisplayName = STR_FORMNAME;
            _objBreadScrumbVacancyStatus.ImagePath = BECommon.ImagePaths.VacancyImage;
            _objBreadScrumbVacancyStatus.ToolTip = _objBreadScrumbVacancyStatus.DisplayName;
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
            objBreadCrumb.Controller = "VacancyStatus";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_EMPLOYEE;
            objBreadCrumb.ordinal = objBreadCrumblst.Count() + 1;
            objBreadCrumb.URL = Url.Action("Index", "VacancyStatus", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, ordinal = objBreadCrumb.ordinal });
            objBreadCrumb.ImagePath = BECommon.ImagePaths.VacancyImage;
            objBreadCrumb.ToolTip = "Vacancy Status"; 
            objBreadCrumb.WithoutOrdinalURL = Url.Action("Index", "VacancyStatus", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });
            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);
            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
        }

        private void NavView(Guid? VacancyStatusId, String DisplayToolTip, String ordinal)
        {
            ATSCommon.CommonFunctions.SetNullNav(this.ControllerContext.RequestContext);
            List<BEClient.BreadCrumb> objBreadCrumblst = ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs;
            if (ordinal != null && !String.IsNullOrEmpty(ordinal.ToString()))
            {
                objBreadCrumblst.RemoveAll(x => x.ordinal >= Convert.ToInt32(ordinal.ToString()));
            }
            BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
            objBreadCrumb.Action = "View";
            objBreadCrumb.Controller = "VacancyStatus";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_EMPLOYEE;
            objBreadCrumb.ordinal = objBreadCrumblst.Count() + 1;
            objBreadCrumb.URL = Url.Action(STR_VACANCYSTATUS_VIEW_METHOD, "VacancyStatus", new { id = VacancyStatusId, area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, ordinal = objBreadCrumb.ordinal });
            objBreadCrumb.ImagePath = BECommon.ImagePaths.VacancyImage;
            objBreadCrumb.ToolTip = "Vacancy Status</br>" + DisplayToolTip; 
            objBreadCrumb.WithoutOrdinalURL = Url.Action("View", "DegreeType", new { id = VacancyStatusId, area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });
            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);
            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
        }
        #endregion navigation
        
        #region CreateURL
        public string AddVacancyStatusURL()
        {
            UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);
            return u.Action(STR_VACANCYSTATUS_VIEW_METHOD, "VacancyStatus");
        }
        #endregion

    }
}
