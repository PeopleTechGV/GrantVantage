using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLClient = ATS.BusinessLogic;
using BEClient = ATS.BusinessEntity;
using BESrp = ATS.BusinessEntity.SRPEntity;
using log4net;
using BECommon = ATS.BusinessEntity.Common;
using ATSCommon = ATS.WebUi.Common;
using RootModels = ATS.WebUi.Models;
using ATS.WebUi.Models;
using Newtonsoft.Json;
using BEMaster = ATS.BusinessEntity.Master;
using BLMaster = ATS.BusinessLogic.Master;
using System.Web.Mvc.Html;
using DisplayMessageConstant = ATS.BusinessEntity.Common;

namespace ATS.WebUi.Areas.Employee.Controllers
{
    public class ATSSecurityController : ATS.WebUi.Controllers.AreaBaseController
    {
        #region Private Members
        private BLClient.ATSSecurityRolesAction _objATSSecurityRolesAction;
        private Guid _CurrentClientId;
        private Guid _CurrentLanguageId;
        private BESrp.DynamicSRP<List<BEClient.ATSSecurityRoles>> _objAtsSecurityRoleList;
        private string STR_FORMNAME = BEClient.Common.CompanySetupMenu.CSMNU_SECURITY_TEMPLATES;
        private BESrp.DynamicSRP<BEClient.ATSSecurityRoles> ObjATSSecurityRoles;
        private string STR_ATSSECURITY_LIST_METHOD = "Index";
        private string STR_ATSSECURITY_CREATE_METHOD = "Create";
        private string STR_ATSSECURITY_EDIT_METHOD = "Edit";
        private string STR_ATSSECURITY_VIEW_METHOD = "View";
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
                _objATSSecurityRolesAction = new BLClient.ATSSecurityRolesAction(_CurrentClientId);
                ObjATSSecurityRoles = new BESrp.DynamicSRP<BEClient.ATSSecurityRoles>();
                //Create new object for List
                _objAtsSecurityRoleList = new BESrp.DynamicSRP<List<BEClient.ATSSecurityRoles>>();
                _objAtsSecurityRoleList.AddBtnText = BECommon.CommonConstant.ADD;
                _objAtsSecurityRoleList.EditBtnText = BECommon.CommonConstant.UPDATE;
                _objAtsSecurityRoleList.RemoveBtnText = BECommon.CommonConstant.DELETE;
                _objAtsSecurityRoleList.SaveBtnText = BECommon.CommonConstant.SAVE;
                if (ATSCommon.CurrentSession.Instance.VerifiedUser.SecurityRoles.Where(x => x.AtsSecurityRole == BEClient.ATSSecurityRole.Candidate).Count() > 0)
                {
                    filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_CANDIDATE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_CANDIDATE_CONTROLLER, new { area = ATSCommon.Constants.AREA_CANDIDATE }));
                }
                if (Common.CurrentSession.Instance.VerifiedUser.SecurityRoles.Where(x => x.AtsSecurityRole.Equals(BEClient.ATSSecurityRole.SystemAdministrator)).Count() <= 0)
                {
                    TempData["KeyMsg"] = ATSCommon.CommonFunctions.NonAuthoriseduser();
                    filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_CONTROLLER, new { @KeyMsg = ATSCommon.CommonFunctions.NonAuthoriseduser() }));
                }
            }
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
            objBreadCrumb.Controller = "ATSSecurity";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_EMPLOYEE;
            objBreadCrumb.ordinal = objBreadCrumblst.Count() + 1;
            objBreadCrumb.URL = Url.Action("Index", "ATSSecurity", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, ordinal = objBreadCrumb.ordinal });
            objBreadCrumb.ImagePath = BECommon.ImagePaths.VacancyImage;
            objBreadCrumb.ToolTip = "ATS Security";
            objBreadCrumb.WithoutOrdinalURL = Url.Action("Index", "ATSSecurity", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });
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
            objBreadCrumb.Controller = "ATSSecurity";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_EMPLOYEE;
            objBreadCrumb.ordinal = objBreadCrumblst.Count() + 1;
            objBreadCrumb.URL = Url.Action("View", "DegreeType", new { id = DegreeTypeId, area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, ordinal = objBreadCrumb.ordinal });
            objBreadCrumb.ImagePath = BECommon.ImagePaths.VacancyImage;
            objBreadCrumb.ToolTip = "ATS Security </br>" + DisplayToolTip;
            objBreadCrumb.WithoutOrdinalURL = Url.Action("View", "ATSSecurity", new { id = DegreeTypeId, area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });
            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);
            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
        }
        #endregion

        #region Create Single object
        private void CreateObjATSSecurityRoles()
        {
            ObjATSSecurityRoles = new BESrp.DynamicSRP<BEClient.ATSSecurityRoles>();
            ObjATSSecurityRoles.AddBtnText = _objAtsSecurityRoleList.AddBtnText;
            ObjATSSecurityRoles.EditBtnText = _objAtsSecurityRoleList.EditBtnText;
            ObjATSSecurityRoles.RemoveBtnText = _objAtsSecurityRoleList.RemoveBtnText;
            ObjATSSecurityRoles.SaveBtnText = _objAtsSecurityRoleList.SaveBtnText;
        }
        private void CreateObjATSSecurityRoles(BESrp.DynamicSRP<BEClient.ATSSecurityRoles> ObjATSSecurityRoles)
        {
            ObjATSSecurityRoles.AddBtnText = _objAtsSecurityRoleList.AddBtnText;
            ObjATSSecurityRoles.EditBtnText = _objAtsSecurityRoleList.EditBtnText;
            ObjATSSecurityRoles.RemoveBtnText = _objAtsSecurityRoleList.RemoveBtnText;
            ObjATSSecurityRoles.SaveBtnText = _objAtsSecurityRoleList.SaveBtnText;
        }
        #endregion

        public ActionResult Index(string KeyMsg, string ordinal)
        {
            RootModels.BreadScrumbModel<BESrp.DynamicSRP<List<BEClient.ATSSecurityRoles>>> _objBreadScrumbModel = new RootModels.BreadScrumbModel<BESrp.DynamicSRP<List<BEClient.ATSSecurityRoles>>>();
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
                //_objAtsSecurityRoleList = new BESrp.DynamicSRP<List<BEClient.ATSSecurityRoles>>();
                _objAtsSecurityRoleList.obj = new List<BEClient.ATSSecurityRoles>();
                BLClient.ATSSecurityRolesAction objATSSecurityRolesAction = new BLClient.ATSSecurityRolesAction(_CurrentClientId);
                _objAtsSecurityRoleList.obj = objATSSecurityRolesAction.GetAllAtsSecurityRolesByLanguage(_CurrentLanguageId);
                _objAtsSecurityRoleList.AddRecordURL = AddSecurityRoleURL();
                NavIndex(ordinal);
                _objBreadScrumbModel.obj = _objAtsSecurityRoleList;
                _objBreadScrumbModel.DisplayName = STR_FORMNAME;
                _objBreadScrumbModel.ToolTip = STR_FORMNAME;
                return View(_objBreadScrumbModel);
            }
            catch
            {
                throw;
            }
        }

        #region ATSSecurity By Id
        public ActionResult View(Guid? id, string ordinal)
        {
            RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.ATSSecurityRoles>> _objBreadScrumbATSSecurityRole = new BreadScrumbModel<BESrp.DynamicSRP<BEClient.ATSSecurityRoles>>();
            ViewBag.FormName = STR_FORMNAME;
            bool isEdit = false;
            if (id != null && !id.Equals(Guid.Empty))
            {
                isEdit = true;
                ViewBag.IsEdit = 1;
            }
            try
            {
                CreateObjATSSecurityRoles();
                ObjATSSecurityRoles.obj = new BEClient.ATSSecurityRoles();
                if (isEdit)
                {



                    ViewBag.RedirectAction = STR_ATSSECURITY_EDIT_METHOD;
                    ObjATSSecurityRoles.obj = _objATSSecurityRolesAction.GetAtsSecurityRolesById((Guid)id);

                    foreach (BEMaster.ClientLanguage clientLanguage in ATSCommon.CurrentSession.Instance.VerifiedClient.ClientLanguageList.OrderBy(r => r.objLanguage.LanguageName))
                    {
                        if (ObjATSSecurityRoles.obj.AtsSecurityEntityLanguage.Where(x => x.LanguageId == clientLanguage.LanguageId).Count() <= 0)
                        {
                            BEClient.EntityLanguage entitylanguage = new BEClient.EntityLanguage();
                            entitylanguage.LanguageId = clientLanguage.LanguageId;
                            ObjATSSecurityRoles.obj.AtsSecurityEntityLanguage.Add(entitylanguage);
                        }
                    }
                    ViewBag.IsDelete = true;
                    BLClient.ATSRolePrivilegeAction objRolePrivilegeAction = new BLClient.ATSRolePrivilegeAction(_CurrentClientId);
                    ObjATSSecurityRoles.obj.lstATSRolePrivilege = objRolePrivilegeAction.GetSRPByRoleId((Guid)id);
                    _objBreadScrumbATSSecurityRole.obj = ObjATSSecurityRoles;
                    SetBreadScrum(ref _objBreadScrumbATSSecurityRole);
                    NavView(id, _objBreadScrumbATSSecurityRole.ToolTip, ordinal);
                    _objBreadScrumbATSSecurityRole.ItemName = ObjATSSecurityRoles.obj.DefaultName;


                    //#region RECENT VIEWED
                    //BEClient.RecentlyViewed objRecentlView = new BEClient.RecentlyViewed();
                    //BLClient.RecentlyViewedAction objRecentlyViewedAction = new BLClient.RecentlyViewedAction(_CurrentClientId);
                    //objRecentlView.Category = BEClient.RecentlyViewedCategory.DegreeType.ToString();
                    //objRecentlView.ViewItemId = (Guid)id;
                    //objRecentlView.URL = HttpContext.Request.Url.ToString();
                    //List<BEClient.RecentlyViewed> objList = objRecentlyViewedAction.GetRecentlyViewedApplication(objRecentlView);
                    //_objBreadScrumbDegreeType.objRecentViewedList = new List<BEClient.RecentlyViewed>();
                    //_objBreadScrumbDegreeType.objRecentViewedList = objList;
                    //#endregion
                }
                else
                {
                    ObjATSSecurityRoles.obj.ClientId = _CurrentClientId;
                    ObjATSSecurityRoles.obj.AtsSecurityEntityLanguage = new List<BEClient.EntityLanguage>();
                    //ObjATSSecurityRoles.obj.RecordExistsCount = new List<BEClient.RecordExists>();
                    foreach (BEMaster.ClientLanguage clientLanguage in ATSCommon.CurrentSession.Instance.VerifiedClient.ClientLanguageList.OrderBy(r => r.objLanguage.LanguageName))
                    {
                        BEClient.EntityLanguage entitylanguage = new BEClient.EntityLanguage();
                        entitylanguage.LanguageId = clientLanguage.LanguageId;
                        ObjATSSecurityRoles.obj.AtsSecurityEntityLanguage.Add(entitylanguage);
                    }
                    BLClient.ATSRolePrivilegeAction objATSRolePrivilegeAction = new BLClient.ATSRolePrivilegeAction(_CurrentClientId);
                    ObjATSSecurityRoles.obj.lstATSRolePrivilege = objATSRolePrivilegeAction.GetAllRelativePrivilege();
                    ViewBag.RedirectAction = STR_ATSSECURITY_CREATE_METHOD;
                    SetBreadScrum(ref _objBreadScrumbATSSecurityRole);
                    NavView(id, _objBreadScrumbATSSecurityRole.DisplayName, ordinal);
                    _objBreadScrumbATSSecurityRole.ItemName = "ADD SECURITY TEMPLATE";
                }

                BLClient.ATSSecurityRoleAction objAtsRoleAction = new BLClient.ATSSecurityRoleAction(_CurrentClientId);

                List<BEClient.ATSSecurityRolePrivilages> lstAtsRole = new List<BEClient.ATSSecurityRolePrivilages>();
                lstAtsRole = objAtsRoleAction.GetAllATSSecurityRoleByClientAndLanguage(_CurrentLanguageId, 0);
                ViewBag.AvailableRoles = new SelectList(lstAtsRole, "SRP_Id", "RoleLocalName");
                _objBreadScrumbATSSecurityRole.obj = ObjATSSecurityRoles;
                return View(_objBreadScrumbATSSecurityRole);
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                return RedirectToAction(STR_ATSSECURITY_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }
        #endregion

        #region

        public ActionResult Create(RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.ATSSecurityRoles>> objBreadScrumbATSSecurityRoles)
        {
            BESrp.DynamicSRP<BEClient.ATSSecurityRoles> objATSSecurityRole = objBreadScrumbATSSecurityRoles.obj;
            String DisplayMessage = string.Empty;
            try
            {
                objATSSecurityRole.obj.ClientId = _CurrentClientId;
                String Message = String.Empty;
                ViewBag.FormName = STR_FORMNAME;
                Guid newAtsSecurityId = _objATSSecurityRolesAction.AddATSSecurityRoles(objATSSecurityRole.obj);
                JsonModels actionStatus = null;
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                if (newAtsSecurityId != null && !newAtsSecurityId.Equals(Guid.Empty))
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
                return RedirectToAction(STR_ATSSECURITY_LIST_METHOD, new { KeyMsg = keyMsg });
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
                CreateObjATSSecurityRoles(objATSSecurityRole);
                List<BEClient.ATSSecurityRolePrivilages> lstAtsRole = new List<BEClient.ATSSecurityRolePrivilages>();
                BLClient.ATSSecurityRoleAction objAtsRoleAction = new BLClient.ATSSecurityRoleAction(_CurrentClientId);
                lstAtsRole = objAtsRoleAction.GetAllATSSecurityRoleByClientAndLanguage(_CurrentLanguageId, 0);
                ViewBag.AvailableRoles = new SelectList(lstAtsRole, "SRP_Id", "RoleLocalName");
                objBreadScrumbATSSecurityRoles.obj = objATSSecurityRole;
                SetBreadScrum(ref objBreadScrumbATSSecurityRoles);
                return View(ATS.WebUi.Common.Constants.STR_VIEW_METHOD, objBreadScrumbATSSecurityRoles);
            }

        }


        #endregion

        #region Edit ATSSecurityRole
        [HttpPost]
        public ActionResult Edit(RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.ATSSecurityRoles>> objBreadScrumbAtsSecurityRole)
        {
            BESrp.DynamicSRP<BEClient.ATSSecurityRoles> objAtsSecurityRole = objBreadScrumbAtsSecurityRole.obj;
            CreateObjATSSecurityRoles(objAtsSecurityRole);
            String DisplayMessage = string.Empty;
            try
            {
                String Message = String.Empty;
                ViewBag.FormName = STR_FORMNAME;
                bool isRecordUpdated = _objATSSecurityRolesAction.UpdateAtsSecurityRole(objAtsSecurityRole.obj);
                JsonModels actionStatus = null;
                if (isRecordUpdated)
                {

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
                return RedirectToAction(STR_ATSSECURITY_LIST_METHOD, new { KeyMsg = keyMsg });
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);

                BEClient.PageMode objPageMode = BEClient.PageMode.Update;
                ViewBag.PageMode = objPageMode;
                ViewBag.IsError = true;
                ViewBag.Message = ex.Message;
                List<BEClient.ATSSecurityRolePrivilages> lstAtsRole = new List<BEClient.ATSSecurityRolePrivilages>();
                BLClient.ATSSecurityRoleAction objAtsRoleAction = new BLClient.ATSSecurityRoleAction(_CurrentClientId);
                lstAtsRole = objAtsRoleAction.GetAllATSSecurityRoleByClientAndLanguage(_CurrentLanguageId, 0);
                ViewBag.AvailableRoles = new SelectList(lstAtsRole, "SRP_Id", "RoleLocalName");
                SetBreadScrum(ref objBreadScrumbAtsSecurityRole);
                return View(ATS.WebUi.Common.Constants.STR_VIEW_METHOD, objBreadScrumbAtsSecurityRole);
            }
        }
        #endregion

        #region CreateURL

        public string AddSecurityRoleURL()
        {
            UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);
            return u.Action(STR_ATSSECURITY_VIEW_METHOD, "ATSSecurity");
        }
        #endregion

        private void SetBreadScrum(ref RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.ATSSecurityRoles>> _objBreadScrumbAtsSecurityRole)
        {
            _objBreadScrumbAtsSecurityRole.DisplayName = STR_FORMNAME;
            _objBreadScrumbAtsSecurityRole.ImagePath = BECommon.ImagePaths.VacancyImage;
            _objBreadScrumbAtsSecurityRole.ToolTip = _objBreadScrumbAtsSecurityRole.DisplayName;
        }

        [HttpGet]
        public JsonResult GetAllPriveleges(Guid AtsSecurityRoleId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                List<BEClient.ATSRolePrivilege> LstRolePrivileges = new List<BEClient.ATSRolePrivilege>();
                BLClient.ATSRolePrivilegeAction ObjATSRolePrivilegeAction = new BLClient.ATSRolePrivilegeAction(_CurrentClientId);
                LstRolePrivileges = ObjATSRolePrivilegeAction.GetSRPByRoleId(AtsSecurityRoleId);
                Data = base.RenderRazorViewToString("_ATSSecurityRole", LstRolePrivileges);
            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;
            }
            return GetJson(GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        #region Delete
        public ActionResult DeleteMultiple(string id)
        {
            string Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            bool IsDefaultMessage = false;
            try
            {

                bool Result = _objATSSecurityRolesAction.Delete(id);
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
                IsDefaultMessage = true;
                IsError = true;
                Message = ex.Message;
            }
            return base.GetJson(GetJsonContent(IsError, null, Message, Data, IsDefaultMessage));
        }

        public ActionResult Delete(string id)
        {
            String DisplayMessage = string.Empty;
            try
            {
                bool Result = _objATSSecurityRolesAction.Delete(id);
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
                return RedirectToAction(STR_ATSSECURITY_LIST_METHOD, new { KeyMsg = keyMsg });
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);

                /*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);

                /*Redirect to List Page*/
                return RedirectToAction(STR_ATSSECURITY_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }
        #endregion
    }
}
