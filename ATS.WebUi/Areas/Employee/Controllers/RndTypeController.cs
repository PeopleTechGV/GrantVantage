using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEClient = ATS.BusinessEntity;
using BLClient = ATS.BusinessLogic;
using ATSCommon = ATS.WebUi.Common;
using ATS.WebUi.Models;
using Newtonsoft.Json;
using BEMaster = ATS.BusinessEntity.Master;
using BLMaster = ATS.BusinessLogic.Master;
using BECommon = ATS.BusinessEntity.Common;
using BESrp = ATS.BusinessEntity.SRPEntity;
using System.Web.UI.WebControls;
using RootModels = ATS.WebUi.Models;

namespace ATS.WebUi.Areas.Employee.Controllers
{
    public class RndTypeController : ATS.WebUi.Controllers.AreaBaseController
    {
        // GET: /Employee/RndType/
        #region Private Members
        private BLClient.RndTypeAction _RndTypeAction;
        private Guid _CurrentClientId;
        private Guid _CurrentLanguageId;
        private BESrp.DynamicSRP<List<BEClient.RndType>> _objRndTypeList;
        private string STR_FORMNAME = BEClient.Common.CompanySetupMenu.CSMNU_REVIEWROUNDS;
        private BESrp.DynamicSRP<BEClient.RndType> ObjRndType;
        private string STR_RNDTYPE_LIST_METHOD = "Index";
        private string STR_RNDTYPE_CREATE_METHOD = "Create";
        private string STR_RNDTYPE_EDIT_METHOD = "Edit";
        private string STR_RNDTYPE_VIEW_METHOD = "View";
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
                _RndTypeAction = new BLClient.RndTypeAction(_CurrentClientId);
                ObjRndType = new BESrp.DynamicSRP<BEClient.RndType>();
                _objRndTypeList = new BESrp.DynamicSRP<List<BEClient.RndType>>();
                _objRndTypeList.AddBtnText = BECommon.CommonConstant.ADD;
                _objRndTypeList.EditBtnText = BECommon.CommonConstant.UPDATE;
                _objRndTypeList.RemoveBtnText = BECommon.CommonConstant.DELETE;
                _objRndTypeList.SaveBtnText = BECommon.CommonConstant.SAVE;
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

        #region Create Single object
        private void CreateObjRndType()
        {
            ObjRndType = new BESrp.DynamicSRP<BEClient.RndType>();
            ObjRndType.AddBtnText = _objRndTypeList.AddBtnText;
            ObjRndType.EditBtnText = _objRndTypeList.EditBtnText;
            ObjRndType.RemoveBtnText = _objRndTypeList.RemoveBtnText;
            ObjRndType.SaveBtnText = _objRndTypeList.SaveBtnText;
        }
        private void CreateObjRndType(BESrp.DynamicSRP<BEClient.RndType> ObjRndType)
        {
            ObjRndType.AddBtnText = _objRndTypeList.AddBtnText;
            ObjRndType.EditBtnText = _objRndTypeList.EditBtnText;
            ObjRndType.RemoveBtnText = _objRndTypeList.RemoveBtnText;
            ObjRndType.SaveBtnText = _objRndTypeList.SaveBtnText;
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
            objBreadCrumb.Controller = "RndType";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_EMPLOYEE;
            objBreadCrumb.ordinal = objBreadCrumblst.Count() + 1;
            objBreadCrumb.URL = Url.Action("Index", "RndType", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, ordinal = objBreadCrumb.ordinal });
            objBreadCrumb.ImagePath = BECommon.ImagePaths.VacancyImage;
            objBreadCrumb.ToolTip = "Round Types";
            objBreadCrumb.WithoutOrdinalURL = Url.Action("Index", "RndType", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });
            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);
            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
        }

        private void NavView(Guid? RndTypeId, String DisplayToolTip, String ordinal)
        {
            ATSCommon.CommonFunctions.SetNullNav(this.ControllerContext.RequestContext);
            List<BEClient.BreadCrumb> objBreadCrumblst = ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs;
            if (ordinal != null && !String.IsNullOrEmpty(ordinal.ToString()))
            {
                objBreadCrumblst.RemoveAll(x => x.ordinal >= Convert.ToInt32(ordinal.ToString()));
            }
            BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
            objBreadCrumb.Action = "View";
            objBreadCrumb.Controller = "RndType";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_EMPLOYEE;
            objBreadCrumb.ordinal = objBreadCrumblst.Count() + 1;
            objBreadCrumb.URL = Url.Action("View", "RndType", new { id = RndTypeId, area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, ordinal = objBreadCrumb.ordinal });
            objBreadCrumb.ImagePath = BECommon.ImagePaths.VacancyImage;
            objBreadCrumb.ToolTip = "Review Round </br>" + DisplayToolTip;
            objBreadCrumb.WithoutOrdinalURL = Url.Action("View", "RndType", new { id = RndTypeId, area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });
            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);
            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
        }
        #endregion
        public ActionResult Index(string KeyMsg, string ordinal)
        {
            RootModels.BreadScrumbModel<BESrp.DynamicSRP<List<BEClient.RndType>>> _objBreadScrumbModel = new RootModels.BreadScrumbModel<BESrp.DynamicSRP<List<BEClient.RndType>>>();
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
                _objRndTypeList.obj = _RndTypeAction.GetAllRndTypeByLanguage(_CurrentLanguageId);
                _objRndTypeList.AddRecordURL = AddRndTypeURL();
                NavIndex(ordinal);
                _objBreadScrumbModel.obj = _objRndTypeList;
                _objBreadScrumbModel.DisplayName = STR_FORMNAME;
                _objBreadScrumbModel.ToolTip = STR_FORMNAME;
                return View(_objBreadScrumbModel);
            }
            catch
            {
                throw;
            }
        }

        #region RndType By Id
        public ActionResult View(Guid? id, string ordinal)
        {
            RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.RndType>> _objBreadScrumbRndType = new BreadScrumbModel<BESrp.DynamicSRP<BEClient.RndType>>();
            ViewBag.FormName = STR_FORMNAME;
            bool isEdit = false;
            if (id != null && !id.Equals(Guid.Empty))
            {
                isEdit = true;
                ViewBag.IsEdit = 1;
            }
            try
            {
                CreateObjRndType();
                ObjRndType.obj = new BEClient.RndType();
                SetBreadScrumb(ref _objBreadScrumbRndType);
                if (isEdit)
                {
                    ViewBag.RedirectAction = STR_RNDTYPE_EDIT_METHOD;
                    ObjRndType.obj = _RndTypeAction.GetRecordByRecordId((Guid)id, _CurrentLanguageId);
                    foreach (BEMaster.ClientLanguage clientLanguage in ATSCommon.CurrentSession.Instance.VerifiedClient.ClientLanguageList.OrderBy(r => r.objLanguage.LanguageName))
                    {
                        if (ObjRndType.obj.RndTypeEntityLanguage.Where(x => x.LanguageId == clientLanguage.LanguageId).Count() <= 0)
                        {
                            BEClient.EntityLanguage entitylanguage = new BEClient.EntityLanguage();
                            entitylanguage.LanguageId = clientLanguage.LanguageId;
                            ObjRndType.obj.RndTypeEntityLanguage.Add(entitylanguage);
                        }
                    }
                    _objBreadScrumbRndType.ItemName = ObjRndType.obj.DefaultName;
                    _objBreadScrumbRndType.obj = ObjRndType;
                    ViewBag.IsDelete = true;
                    NavView(id, _objBreadScrumbRndType.ToolTip, ordinal);
                    #region RECENT VIEWED
                    BEClient.RecentlyViewed objRecentlView = new BEClient.RecentlyViewed();
                    BLClient.RecentlyViewedAction objRecentlyViewedAction = new BLClient.RecentlyViewedAction(_CurrentClientId);
                    objRecentlView.Category = BEClient.RecentlyViewedCategory.ReviewRound.ToString();
                    objRecentlView.ViewItemId = (Guid)id;
                    objRecentlView.URL = HttpContext.Request.Url.ToString();
                    List<BEClient.RecentlyViewed> objList = objRecentlyViewedAction.GetRecentlyViewedApplication(objRecentlView);
                    _objBreadScrumbRndType.objRecentViewedList = new List<BEClient.RecentlyViewed>();
                    _objBreadScrumbRndType.objRecentViewedList = objList;
                    #endregion
                }
                else
                {
                    ObjRndType.obj.ClientId = _CurrentClientId;
                    ObjRndType.obj.RndTypeEntityLanguage = new List<BEClient.EntityLanguage>();
                    foreach (BEMaster.ClientLanguage clientLanguage in ATSCommon.CurrentSession.Instance.VerifiedClient.ClientLanguageList.OrderBy(r => r.objLanguage.LanguageName))
                    {
                        BEClient.EntityLanguage entitylanguage = new BEClient.EntityLanguage();
                        entitylanguage.LanguageId = clientLanguage.LanguageId;
                        ObjRndType.obj.RndTypeEntityLanguage.Add(entitylanguage);
                    }
                    ViewBag.RedirectAction = STR_RNDTYPE_CREATE_METHOD;
                    _objBreadScrumbRndType.ItemName = "ADD REVIEW ROUNDS";
                    NavView(id, _objBreadScrumbRndType.DisplayName, ordinal);
                }
                FillDropDown(ref ObjRndType);
                _objBreadScrumbRndType.obj = ObjRndType;
                return View(_objBreadScrumbRndType);
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                return RedirectToAction(STR_RNDTYPE_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }
        #endregion

        #region Add Round Type
        [HttpPost]
        public ActionResult Create(RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.RndType>> objBreadScrumbRndType)
        {
            BESrp.DynamicSRP<BEClient.RndType> ObjRndType = objBreadScrumbRndType.obj;
            CreateObjRndType(ObjRndType);
            try
            {
                ObjRndType.obj.ClientId = _CurrentClientId;
                String Message = String.Empty;
                ViewBag.FormName = STR_FORMNAME;
                Guid newRndTypeId = _RndTypeAction.AddRndType(ObjRndType.obj);
                JsonModels actionStatus = null;
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                if (newRndTypeId != null && !newRndTypeId.Equals(Guid.Empty))
                {
                    actionStatus = base.GetJsonContent(false, string.Empty, "Record Added Successfully");
                }
                else
                {
                    actionStatus = base.GetJsonContent(true, string.Empty, "Not Able To Add Record");
                }
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                return RedirectToAction(STR_RNDTYPE_LIST_METHOD, new { KeyMsg = keyMsg });
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
                FillDropDown(ref ObjRndType);
                objBreadScrumbRndType.obj = ObjRndType;
                SetBreadScrumb(ref objBreadScrumbRndType);
                return View(ATS.WebUi.Common.Constants.STR_VIEW_METHOD, objBreadScrumbRndType);
            }
        }
        #endregion

        #region Edit RndType
        [HttpPost]
        public ActionResult Edit(RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.RndType>> objBreadScrumbRndType)
        {
            BESrp.DynamicSRP<BEClient.RndType> ObjRndType = objBreadScrumbRndType.obj;
            CreateObjRndType(ObjRndType);
            try
            {
                String Message = String.Empty;
                ViewBag.FormName = STR_FORMNAME;
                bool isRecordUpdated = _RndTypeAction.UpdateRndType(ObjRndType.obj);
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
                return RedirectToAction(STR_RNDTYPE_LIST_METHOD, new { KeyMsg = keyMsg });
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                FillDropDown(ref ObjRndType);
                BEClient.PageMode objPageMode = BEClient.PageMode.Update;
                ViewBag.PageMode = objPageMode;
                ViewBag.IsError = true;
                ViewBag.Message = ex.Message;
                SetBreadScrumb(ref objBreadScrumbRndType);
                return View(ATS.WebUi.Common.Constants.STR_VIEW_METHOD, objBreadScrumbRndType);
            }
        }
        #endregion

        #region
        public void FillDropDown(ref BESrp.DynamicSRP<BEClient.RndType> objrndType)
        {
            try
            {
                List<BEClient.BindEnumDropDown> _QuestionTypeList = new List<BEClient.BindEnumDropDown>();
                foreach (BEClient.QuestionType r in Enum.GetValues(typeof(BEClient.QuestionType)))
                {
                    _QuestionTypeList.Add(new BEClient.BindEnumDropDown() { Text = ATSCommon.CommonFunctions.LanguageLabel(r.ToString()), Value = Convert.ToInt32(r) });
                }
                objrndType.obj.AllQuestionTypeList = _QuestionTypeList;

                BLClient.RoundAttributesAction ObjRndAttrAction = new BLClient.RoundAttributesAction(_CurrentClientId);
                List<BEClient.RoundAttributes> ObjRndAttributes = new List<BEClient.RoundAttributes>();
                ObjRndAttributes = ObjRndAttrAction.FillAllRoundAttributes(_CurrentLanguageId);
                ViewBag.RoundAttributes = new SelectList(ObjRndAttributes, "RoundAttributesId", "RoundAttributesName");
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public JsonResult GetQuestionTypeByRoundAttributesId(Guid RoundAttributesId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = "";
            try
            {
                BLClient.RoundAttributesAction objRndAttrAction = new BLClient.RoundAttributesAction(_CurrentClientId);
                string QuestionType = objRndAttrAction.GetQuestionTypeByRoundAttributesId(RoundAttributesId);
                Data = QuestionType;
            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region CreateURL

        public string AddRndTypeURL()
        {
            UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);
            return u.Action(STR_RNDTYPE_VIEW_METHOD, "RndType");
        }
        #endregion

        private void SetBreadScrumb(ref RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.RndType>> _objBreadScrumbRndType)
        {
            _objBreadScrumbRndType.DisplayName = STR_FORMNAME;
            _objBreadScrumbRndType.ImagePath = BECommon.ImagePaths.VacancyImage;
            _objBreadScrumbRndType.ToolTip = _objBreadScrumbRndType.DisplayName;
        }

        public ActionResult Delete(string id)
        {
            try
            {
                bool isRndTypeConfigured = false;
                JsonModels actionStatus = null;
                BLClient.RndTypeAction RndTypeAction = new BLClient.RndTypeAction(_CurrentClientId);
                isRndTypeConfigured = RndTypeAction.IsRndTypeConfigured(id);
                if (!isRndTypeConfigured)
                {
                    bool Result = RndTypeAction.Delete(id);

                    if (Result)
                    {
                        actionStatus = base.GetJsonContent(false, string.Empty, "Record Deleted Successfully");
                        BusinessLogic.Common.CacheHelper.GetDegreeType(_CurrentClientId, _CurrentLanguageId, CurrentClient.Clientname, true);
                    }
                    else
                    {
                        actionStatus = base.GetJsonContent(true, string.Empty, "Not Able To Delete Record");
                    }
                }
                else
                {
                    actionStatus = base.GetJsonContent(true, string.Empty, "Not Able To Delete Record As It has been Configured..");
                }
                ATSCommon.CommonFunctions.SolrEmployeeFullImport();
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                return RedirectToAction(STR_RNDTYPE_LIST_METHOD, new { KeyMsg = keyMsg });
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                return RedirectToAction(STR_RNDTYPE_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }

        public ActionResult DeleteMultiple(string id)
        {
            string Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                bool Result = _RndTypeAction.Delete(id);
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

        [HttpPost]
        public JsonResult GetStatusList(object data)
        {
            GridMvc.QueryStringGridSettingsProvider obj = new GridMvc.QueryStringGridSettingsProvider();
            _objRndTypeList.obj = _RndTypeAction.GetAllRndTypeByLanguage(_CurrentLanguageId);
            SelectList newlist = new SelectList(_objRndTypeList.obj, "RndTypeId", "DefaultName");
            return Json(newlist);
        }
    }
}
