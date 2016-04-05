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
    public class LanguageBlocksController : ATS.WebUi.Controllers.AreaBaseController
    {
        //
        // GET: /Employee/LanguageBlocks/
        #region Private Members
        private BLClient.LanguageBlocksAction _objLanguageBlocksAction;
        private Guid _CurrentClientId;
        private Guid _CurrentLanguageId;
        private BESrp.DynamicSRP<List<BEClient.LanguageBlocks>> _objLanguageBlockList;

        private BESrp.DynamicSRP<BEClient.LanguageBlocks> _objSRPLanguageBlocks;
        private static readonly log4net.ILog log = LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Redirection Method
        private string STR_LANGUAGEBLOCKS_LIST_METHOD = "Index";
        private string STR_LANGUAGEBLOCKS_EDIT_METHOD = "Edit";
        private string STR_LANGUAGEBLOCKS_VIEW_METHOD = "View";
        private string STR_LANGUAGEBLOCKS_FORMNAME = BEClient.Common.CompanySetupMenu.CSMNU_LANGUAGEBLOCKS;
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
                _objLanguageBlocksAction = new BLClient.LanguageBlocksAction(_CurrentClientId);

                //Create new object for List
                _objLanguageBlockList = new BESrp.DynamicSRP<List<BEClient.LanguageBlocks>>();
                //_objLanguageBlockList.AddBtnText = "Add";
                //_objLanguageBlockList.EditBtnText = "Edit Language Block";
                //_objLanguageBlockList.RemoveBtnText = "Remove Language Block";
                _objLanguageBlockList.SaveBtnText = BEClient.Common.CommonConstant.SAVE;

                if (Common.CurrentSession.Instance.VerifiedUser.SecurityRoles.Where(x => x.AtsSecurityRole.Equals(BEClient.ATSSecurityRole.SystemAdministrator)).Count() <= 0)
                {
                    TempData["KeyMsg"] = ATSCommon.CommonFunctions.NonAuthoriseduser();
                    filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_CONTROLLER, new { @KeyMsg = ATSCommon.CommonFunctions.NonAuthoriseduser() }));
                }
            }
        }
        #endregion

        public ActionResult Index(string KeyMsg, string ordinal)
        {
            RootModels.BreadScrumbModel<BESrp.DynamicSRP<List<BEClient.LanguageBlocks>>> _objBreadScrumbLanguageBlocks = new RootModels.BreadScrumbModel<BESrp.DynamicSRP<List<BEClient.LanguageBlocks>>>();
            JsonModels resultJsonModel = null;

            if (!String.IsNullOrEmpty(KeyMsg))
            {
                /*Deserialize */
                string deserializeKeyMsg = ATSHelper.Encryption.EncryptionAlgo.DecryptData(KeyMsg);

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

                _objLanguageBlockList.obj = _objLanguageBlocksAction.GetAllLanguageBlock(ATSCommon.CurrentSession.Instance.VerifiedClient.CurrentLanguageId);
                ViewBag.PageMode = BEClient.PageMode.View;

                NavIndex(ordinal);

                _objBreadScrumbLanguageBlocks.obj = _objLanguageBlockList;
                _objBreadScrumbLanguageBlocks.DisplayName = STR_LANGUAGEBLOCKS_FORMNAME;
                _objBreadScrumbLanguageBlocks.ToolTip = STR_LANGUAGEBLOCKS_FORMNAME;
                return View(_objBreadScrumbLanguageBlocks);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ActionResult View(Guid? id, string ordinal)
        {
            RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.LanguageBlocks>> _objBreadScrumbLanguageBlocks = new BreadScrumbModel<BESrp.DynamicSRP<BEClient.LanguageBlocks>>();
            bool isEdit = false;
            BEClient.PageMode objPageMode = BEClient.PageMode.View;
            if (id != null && !id.Equals(Guid.Empty))
            {
                isEdit = true;
                ViewBag.IsEdit = 1;
            }
            try
            {
                _objSRPLanguageBlocks = new BESrp.DynamicSRP<BEClient.LanguageBlocks>();
                _objSRPLanguageBlocks.obj = new BEClient.LanguageBlocks();

                if (isEdit)
                {
                    CreateObjLanguageBlock((Guid)id);
                    _objSRPLanguageBlocks.obj = _objLanguageBlocksAction.GetLanguageBlockByRecordId(_CurrentLanguageId, (Guid)id);
                    _objSRPLanguageBlocks.ActionName = "Edit";
                    _objBreadScrumbLanguageBlocks.ItemName = _objSRPLanguageBlocks.obj.LanguageBlockName;
                    _objBreadScrumbLanguageBlocks.obj = _objSRPLanguageBlocks;
                    SetBreadScrumb(ref _objBreadScrumbLanguageBlocks);
                    ViewBag.IsDelete = false;
                    NavView(id, _objBreadScrumbLanguageBlocks.ToolTip, ordinal);

                    #region RECENT VIEWED
                    BEClient.RecentlyViewed objRecentlView = new BEClient.RecentlyViewed();
                    BLClient.RecentlyViewedAction objRecentlyViewedAction = new BLClient.RecentlyViewedAction(_CurrentClientId);
                    objRecentlView.Category = BEClient.RecentlyViewedCategory.LanguageBlock.ToString();
                    objRecentlView.ViewItemId = (Guid)id;
                    objRecentlView.URL = HttpContext.Request.Url.ToString();
                    List<BEClient.RecentlyViewed> objList = objRecentlyViewedAction.GetRecentlyViewedApplication(objRecentlView);
                    _objBreadScrumbLanguageBlocks.objRecentViewedList = new List<BEClient.RecentlyViewed>();
                    _objBreadScrumbLanguageBlocks.objRecentViewedList = objList;
                    #endregion


                }

                _objBreadScrumbLanguageBlocks.obj = _objSRPLanguageBlocks;
                return View(_objBreadScrumbLanguageBlocks);
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);

                ///*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = ATSHelper.Encryption.EncryptionAlgo.EncryptionData(jsonModels);

                ///*Redirect to List Page*/
                return RedirectToAction(STR_LANGUAGEBLOCKS_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }

        [HttpPost]
        public ActionResult Edit(RootModels.BreadScrumbModel<ATS.BusinessEntity.SRPEntity.DynamicSRP<BEClient.LanguageBlocks>> _objBreadScrumbLanguageBlocks)
        {
            ATS.BusinessEntity.SRPEntity.DynamicSRP<BEClient.LanguageBlocks> objLanguageBlock = _objBreadScrumbLanguageBlocks.obj;
            String DisplayMessage = string.Empty;
            try
            {
                
                String Message = String.Empty;
                bool isRecordUpdated = _objLanguageBlocksAction.UpdateLanguageBlock(objLanguageBlock.obj);
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
                return RedirectToAction(STR_LANGUAGEBLOCKS_LIST_METHOD, new { KeyMsg = keyMsg });
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
                _objSRPLanguageBlocks.ActionName = "Edit";
                _objBreadScrumbLanguageBlocks.obj = objLanguageBlock;
                SetBreadScrumb(ref _objBreadScrumbLanguageBlocks);
                return View(ATS.WebUi.Common.Constants.STR_VIEW_METHOD, _objBreadScrumbLanguageBlocks);

            }
        }

        private void CreateObjLanguageBlock(Guid? JobLocationId)
        {
            _objSRPLanguageBlocks = new BESrp.DynamicSRP<BEClient.LanguageBlocks>();
            _objSRPLanguageBlocks.AddBtnText = _objLanguageBlockList.AddBtnText;
            _objSRPLanguageBlocks.EditBtnText = _objLanguageBlockList.EditBtnText;
            _objSRPLanguageBlocks.RemoveBtnText = _objLanguageBlockList.RemoveBtnText;
            _objSRPLanguageBlocks.SaveBtnText = _objLanguageBlockList.SaveBtnText;
        }

        private void SetBreadScrumb(ref RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.LanguageBlocks>> _objBreadScrumbBlocks)
        {
            _objBreadScrumbBlocks.DisplayName = STR_LANGUAGEBLOCKS_FORMNAME;
            _objBreadScrumbBlocks.ImagePath = BECommon.ImagePaths.VacancyImage;
            _objBreadScrumbBlocks.ToolTip = _objBreadScrumbBlocks.DisplayName;
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

        private void NavView(Guid? LanguageBlockId, String DisplayToolTip, String ordinal)
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
            objBreadCrumb.URL = Url.Action("View", "LanguageBlocks", new { id = LanguageBlockId, area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, ordinal = objBreadCrumb.ordinal });
            objBreadCrumb.ImagePath = BECommon.ImagePaths.VacancyImage;
            objBreadCrumb.ToolTip = "Language Blocks </br>" + DisplayToolTip;
            objBreadCrumb.WithoutOrdinalURL = Url.Action("View", "LanguageBlocks", new { id = LanguageBlockId, area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });

            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);

            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;

        }
    }
}
