using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEClient = ATS.BusinessEntity;
using ATSHelper = ATS.HelperLibrary;
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
using System.Web.Mvc.Html;
using DisplayMessageConstant = ATS.BusinessEntity.Common;

namespace ATS.WebUi.Areas.Employee.Controllers
{
    public class EmailTemplatesController : ATS.WebUi.Controllers.AreaBaseController
    {
        //
        // GET: /Employee/Emailtemplates/
        #region Private Members
        private BLClient.EmailTemplatesAction _objEmailTemplatesAction;
        private Guid _CurrentClientId;
        private Guid _CurrentLanguageId;
        private BESrp.DynamicSRP<List<BEClient.EmailTemplates>> _objEmailTemplatesList;

        private BESrp.DynamicSRP<BEClient.EmailTemplates> _objSRPEmailTemplates;
        private static readonly log4net.ILog log = LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Redirection Method
        private string STR_EMAILTEMPLATES_LIST_METHOD = "Index";
        private string STR_FORMNAME = BEClient.Common.CompanySetupMenu.CSMNU_EMAILTEMPLATES;
        #endregion

        #region OnAuthorization Method call on every request
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {

            base.OnAuthorization(filterContext);
            if (base.CurrentClient != null && Common.CurrentSession.Instance.VerifiedUser != null)
            {
                _CurrentClientId = base.CurrentClient.ClientId;
                _CurrentLanguageId = base.CurrentClient.CurrentLanguageId;
                _objEmailTemplatesAction = new BLClient.EmailTemplatesAction(_CurrentClientId);

                //Create new object for List
                _objEmailTemplatesList = new BESrp.DynamicSRP<List<BEClient.EmailTemplates>>();
                _objEmailTemplatesList.SaveBtnText = "Save";
                if (filterContext.ActionDescriptor.ActionName.ToLower() != "getemailcontent")
                {
                    if (Common.CurrentSession.Instance.VerifiedUser.SecurityRoles.Where(x => x.AtsSecurityRole.Equals(BEClient.ATSSecurityRole.SystemAdministrator)).Count() <= 0)
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

        public ActionResult Index(string KeyMsg, string ordinal)
        {
            RootModels.BreadScrumbModel<BESrp.DynamicSRP<List<BEClient.EmailTemplates>>> _objBreadScrumbEmailTemplates = new RootModels.BreadScrumbModel<BESrp.DynamicSRP<List<BEClient.EmailTemplates>>>();
            JsonModels resultJsonModel = null;

            if (!String.IsNullOrEmpty(KeyMsg))
            {
                string deserializeKeyMsg = ATSHelper.Encryption.EncryptionAlgo.DecryptData(KeyMsg);
                resultJsonModel = JsonConvert.DeserializeObject<JsonModels>(deserializeKeyMsg);
            }
            try
            {
                if (resultJsonModel != null)
                {
                    ViewBag.IsError = resultJsonModel.IsError;
                    ViewBag.Message = resultJsonModel.Message;
                }
                _objEmailTemplatesList.obj = _objEmailTemplatesAction.GetAllEmailTemplates(ATSCommon.CurrentSession.Instance.VerifiedClient.CurrentLanguageId);
                ViewBag.PageMode = BEClient.PageMode.View;
                NavIndex(ordinal);
                _objBreadScrumbEmailTemplates.obj = _objEmailTemplatesList;
                _objBreadScrumbEmailTemplates.DisplayName = STR_FORMNAME;
                _objBreadScrumbEmailTemplates.ToolTip = "Email Templates";
                return View(_objBreadScrumbEmailTemplates);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult View(Guid? id, string ordinal)
        {
            RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.EmailTemplates>> _objBreadScrumbEmailTemplates = new BreadScrumbModel<BESrp.DynamicSRP<BEClient.EmailTemplates>>();
            bool isEdit = false;
            BEClient.PageMode objPageMode = BEClient.PageMode.View;
            if (id.Equals(Guid.Empty))
            {
                isEdit = true;
                ViewBag.IsEdit = 1;
            }
            try
            {
                _objSRPEmailTemplates = new BESrp.DynamicSRP<BEClient.EmailTemplates>();
                _objSRPEmailTemplates.obj = new BEClient.EmailTemplates();
                CreateObjLanguageBlock();
                _objSRPEmailTemplates.obj = _objEmailTemplatesAction.GetEmailTemplateById(_CurrentLanguageId, (Guid)id);
                ViewBag.IsDelete = false;
                _objSRPEmailTemplates.ActionName = "Edit";
                _objBreadScrumbEmailTemplates.ItemName = _objSRPEmailTemplates.obj.EmailName;
                _objBreadScrumbEmailTemplates.obj = _objSRPEmailTemplates;
                setEditBreadScrum(ref _objBreadScrumbEmailTemplates);
                NavView(_objSRPEmailTemplates.obj.EmailIdentifier, _objBreadScrumbEmailTemplates.ToolTip, ordinal);
                _objBreadScrumbEmailTemplates.obj = _objSRPEmailTemplates;
                _objBreadScrumbEmailTemplates.obj.SaveBtnText = "Save";




                #region RECENT VIEWED
                BEClient.RecentlyViewed objRecentlView = new BEClient.RecentlyViewed();
                BLClient.RecentlyViewedAction objRecentlyViewedAction = new BLClient.RecentlyViewedAction(_CurrentClientId);
                objRecentlView.Category = BEClient.RecentlyViewedCategory.EmailTemplate.ToString();
                objRecentlView.ViewItemId = (Guid)id;
                objRecentlView.URL = HttpContext.Request.Url.ToString();
                List<BEClient.RecentlyViewed> objList = objRecentlyViewedAction.GetRecentlyViewedApplication(objRecentlView);
                _objBreadScrumbEmailTemplates.objRecentViewedList = new List<BEClient.RecentlyViewed>();
                _objBreadScrumbEmailTemplates.objRecentViewedList = objList;
                #endregion

                return View(_objBreadScrumbEmailTemplates);


            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);

                ///*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = ATSHelper.Encryption.EncryptionAlgo.EncryptionData(jsonModels);

                ///*Redirect to List Page*/
                return RedirectToAction(STR_EMAILTEMPLATES_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(RootModels.BreadScrumbModel<ATS.BusinessEntity.SRPEntity.DynamicSRP<BEClient.EmailTemplates>> _objBreadScrumbEmailTemplates)
        {
            ATS.BusinessEntity.SRPEntity.DynamicSRP<BEClient.EmailTemplates> objEmailTemplate = _objBreadScrumbEmailTemplates.obj;
            String DisplayMessage = string.Empty;
            try
            {
                String Message = String.Empty;
                _objBreadScrumbEmailTemplates.obj.obj.ClientId = _CurrentClientId;
                _objBreadScrumbEmailTemplates.obj.obj.LanguageId = _CurrentLanguageId;
                bool isRecordUpdated = _objEmailTemplatesAction.UpdateEmailTemplate(objEmailTemplate.obj);
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
                return RedirectToAction(STR_EMAILTEMPLATES_LIST_METHOD, new { KeyMsg = keyMsg });
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);

                /*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);

                ViewBag.IsError = true;
                ViewBag.Message = ex.Message;
                ViewBag.IsDelete = false;
                _objSRPEmailTemplates.ActionName = "Edit";
                _objBreadScrumbEmailTemplates.obj = objEmailTemplate;
                setEditBreadScrum(ref _objBreadScrumbEmailTemplates);
                return View(ATS.WebUi.Common.Constants.STR_VIEW_METHOD, _objBreadScrumbEmailTemplates);

            }
        }
        private void CreateObjLanguageBlock()
        {
            _objSRPEmailTemplates = new BESrp.DynamicSRP<BEClient.EmailTemplates>();
            _objSRPEmailTemplates.AddBtnText = _objSRPEmailTemplates.AddBtnText;
            _objSRPEmailTemplates.EditBtnText = _objSRPEmailTemplates.EditBtnText;
            _objSRPEmailTemplates.RemoveBtnText = _objSRPEmailTemplates.RemoveBtnText;
            _objSRPEmailTemplates.SaveBtnText = _objSRPEmailTemplates.SaveBtnText;
        }

        private void setEditBreadScrum(ref RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.EmailTemplates>> _objBreadScrumbTemplates)
        {
            _objBreadScrumbTemplates.DisplayName = STR_FORMNAME;
            _objBreadScrumbTemplates.ImagePath = BECommon.ImagePaths.VacancyImage;
            _objBreadScrumbTemplates.ToolTip = _objBreadScrumbTemplates.DisplayName;
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
            objBreadCrumb.Controller = "LanguageBlocks";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_EMPLOYEE;
            objBreadCrumb.ordinal = objBreadCrumblst.Count() + 1;
            objBreadCrumb.URL = Url.Action("Index", "LanguageBlocks", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });
            objBreadCrumb.ImagePath = BECommon.ImagePaths.VacancyImage;
            objBreadCrumb.ToolTip = "Language Blocks";
            objBreadCrumb.WithoutOrdinalURL = Url.Action("Index", "LanguageBlocks", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });

            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);

            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
        }

        private void NavView(string EmailIdentifier, String DisplayToolTip, String ordinal)
        {

            ATSCommon.CommonFunctions.SetNullNav(this.ControllerContext.RequestContext);
            List<BEClient.BreadCrumb> objBreadCrumblst = ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs;
            if (ordinal != null && !String.IsNullOrEmpty(ordinal.ToString()))
            {
                objBreadCrumblst.RemoveAll(x => x.ordinal >= Convert.ToInt32(ordinal.ToString()));
            }
            BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
            objBreadCrumb.Action = "View";
            objBreadCrumb.Controller = "LanguageBlocks";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_EMPLOYEE;
            objBreadCrumb.ordinal = objBreadCrumblst.Count() + 1;
            objBreadCrumb.URL = Url.Action("View", "EmailTemplates", new { id = EmailIdentifier, area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, ordinal = objBreadCrumb.ordinal });
            objBreadCrumb.ImagePath = BECommon.ImagePaths.VacancyImage;
            objBreadCrumb.ToolTip = "Language Blocks </br>" + DisplayToolTip;
            objBreadCrumb.WithoutOrdinalURL = Url.Action("View", "EmailTemplates", new { id = EmailIdentifier, area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });

            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);

            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;

        }

        [HttpGet]
        public JsonResult GetEmailContent(Guid EmailTemplateId)
        {
            String Message = String.Empty;
            bool IsError = false;
            BEClient.EmailTemplates objEmailTemplate = new BEClient.EmailTemplates();
            try
            {
                BLClient.EmailTemplatesAction objEmailTemplateAction = new BLClient.EmailTemplatesAction(_CurrentClientId);
                objEmailTemplate = objEmailTemplateAction.GetEmailContentById(EmailTemplateId, _CurrentLanguageId);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, objEmailTemplate), JsonRequestBehavior.AllowGet);
        }
    }
}
