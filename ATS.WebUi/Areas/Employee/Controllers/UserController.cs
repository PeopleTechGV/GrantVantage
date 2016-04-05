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
using ATS.WebUi.Models;
using Newtonsoft.Json;
using System.IO;
using BESrp = ATS.BusinessEntity.SRPEntity;
using ATSHelper = ATS.HelperLibrary;
using log4net;
using RootModels = ATS.WebUi.Models;
using BECommon = ATS.BusinessEntity.Common;

namespace ATS.WebUi.Areas.Employee.Controllers
{
    public class UserController : ATS.WebUi.Controllers.AreaBaseController
    {
        #region Private Data Member
        private Guid _CurrentClientId;
        private Guid _CurrentLanguageId;
        private Guid _CurrentUserId;
        private BLClient.UserDivisionSecurityRoleAction _objUserDivisionSecurityRoleAction;
        private BESrp.DynamicSRP<List<BEClient.UserDivisionSecurityRole>> _objUserDivisionSecurityRoleList;
        private BESrp.DynamicSRP<BEClient.UserDivisionSecurityRole> _objUserDivisionSecurityRole;
        private string STR_FORMNAME = BEClient.Common.CompanySetupMenu.CSMNU_USER;
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
                _CurrentUserId = ATSCommon.CurrentSession.Instance.UserId;
                _CurrentClientId = base.CurrentClient.ClientId;
                _CurrentLanguageId = base.CurrentClient.CurrentLanguageId;
                _objUserDivisionSecurityRoleAction = new BLClient.UserDivisionSecurityRoleAction(_CurrentClientId, true);
                BEClient.UserDivisionSecurityRole objUserDivisionSecurityRole = new BEClient.UserDivisionSecurityRole();

                //Create new object for List
                _objUserDivisionSecurityRoleList = new BESrp.DynamicSRP<List<BEClient.UserDivisionSecurityRole>>();
                _objUserDivisionSecurityRoleList.AddBtnText = BEClient.Common.CommonConstant.ADD;
                _objUserDivisionSecurityRoleList.EditBtnText = BEClient.Common.CommonConstant.UPDATE;
                _objUserDivisionSecurityRoleList.RemoveBtnText = BEClient.Common.CommonConstant.DELETE;
                _objUserDivisionSecurityRoleList.SaveBtnText = BEClient.Common.CommonConstant.SAVE;
                _objUserDivisionSecurityRoleList.UserPermissionWithScope = _objUserDivisionSecurityRoleAction.ListUserPermissionWithScope;

                //This function will be used for create new details.It will check two key exists(Action and Contrller)
                if (filterContext.ActionDescriptor.ActionName == "View" && filterContext.RouteData.Values.Keys.Count() == 2)
                {
                    if (_objUserDivisionSecurityRoleList.UserPermissionWithScope.Where(x => x.PermissionType.Contains(BEClient.ATSPermissionType.Create)).Count() <= 0)
                    {
                        TempData["KeyMsg"] = ATSCommon.CommonFunctions.NonAuthoriseduser();
                        filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_CONTROLLER, new { @KeyMsg = ATSCommon.CommonFunctions.NonAuthoriseduser() }));
                    }
                }
                if (ATSCommon.CurrentSession.Instance.VerifiedUser.SecurityRoles.Where(x => x.AtsSecurityRole == BEClient.ATSSecurityRole.Candidate).Count() > 0)
                {
                    filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_CANDIDATE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_CANDIDATE_CONTROLLER, new { area = ATSCommon.Constants.AREA_CANDIDATE }));

                }
            }
        }
        #endregion

        #region Create Single object
        private void CreateObjDivision(Guid? userId)
        {
            _objUserDivisionSecurityRole = new BESrp.DynamicSRP<BEClient.UserDivisionSecurityRole>();
            _objUserDivisionSecurityRole.AddBtnText = _objUserDivisionSecurityRoleList.AddBtnText;
            _objUserDivisionSecurityRole.EditBtnText = _objUserDivisionSecurityRoleList.EditBtnText;
            _objUserDivisionSecurityRole.RemoveBtnText = _objUserDivisionSecurityRoleList.RemoveBtnText;
            _objUserDivisionSecurityRole.SaveBtnText = _objUserDivisionSecurityRoleList.SaveBtnText;
            _objUserDivisionSecurityRole.UserPermissionWithScope = _objUserDivisionSecurityRoleAction.ListUserPermissionWithScope;
            if (userId != null)
                _objUserDivisionSecurityRole.RemoveRecordURL = RemoveUserDivisionSecurityRoleURL((Guid)userId);
        }
        #endregion

        #region CreateURL
        public string RemoveUserDivisionSecurityRoleURL(Guid userId)
        {
            UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);
            return u.Action("Remove", "User", new { id = userId.ToString() });
        }
        public string AddUserDivisionSecurityRoleURL()
        {
            UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);
            return u.Action("View", "User");
        }
        #endregion

        public ActionResult Index(string KeyMsg, string ordinal)
        {
            RootModels.BreadScrumbModel<BESrp.DynamicSRP<List<BEClient.UserDivisionSecurityRole>>> _objBreadScrumbUser = new RootModels.BreadScrumbModel<BESrp.DynamicSRP<List<BEClient.UserDivisionSecurityRole>>>();

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
                _objUserDivisionSecurityRoleList.obj = _objUserDivisionSecurityRoleAction.GetAllUsersByDivision(_CurrentLanguageId);
                _objUserDivisionSecurityRoleList.AddRecordURL = AddUserDivisionSecurityRoleURL();
                ViewBag.PageMode = BEClient.PageMode.View;
                //                if (_objDivisionList.obj != null && _objDivisionList.obj.Count() > 0)
                NavIndex(ordinal);

                _objBreadScrumbUser.obj = _objUserDivisionSecurityRoleList;
                _objBreadScrumbUser.DisplayName = STR_FORMNAME;
                _objBreadScrumbUser.ToolTip = STR_FORMNAME;

                return View(_objBreadScrumbUser);
            }
            catch
            {

                throw;
            }
        }

        protected string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        public ActionResult ChangeCredential(Guid UserId, String email)
        {
            string keyMsg = string.Empty;
            string jsonModels = string.Empty;
            JsonModels actionStatus = null;
            SignUpModel signModule = new SignUpModel();
            signModule.UserName = email;
            try
            {
                if (ATSCommon.CommonFunctions.ForgotPassword(this.ControllerContext.RequestContext, signModule))
                {
                    actionStatus = base.GetJsonContent(false, string.Empty, "Password sent on " + email + " successfully");
                }
                else
                {
                    actionStatus = base.GetJsonContent(true, string.Empty, "Password sent to user successfully");
                }
            }
            catch (Exception ex)
            {

                actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
            }
            jsonModels = JsonConvert.SerializeObject(actionStatus);
            keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);

            return RedirectToAction("View", new { id = UserId, KeyMsg = keyMsg });
        }

        public ActionResult View(Guid? id, String KeyMsg, string ordinal)
        {
            RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.UserDivisionSecurityRole>> _objBreadScrumbUsers = new BreadScrumbModel<BESrp.DynamicSRP<BEClient.UserDivisionSecurityRole>>();


            JsonModels resultJsonModel = null;
            BEClient.PageMode objPageMode = BEClient.PageMode.View;
            ViewBag.FormName = STR_FORMNAME;
            if (KeyMsg != null && !String.IsNullOrEmpty(KeyMsg.ToString()))
            {
                /*Deserialize */
                string deserializeKeyMsg = HelperLibrary.Encryption.EncryptionAlgo.DecryptData(KeyMsg.ToString());

                /*Convert from json to Object*/
                resultJsonModel = JsonConvert.DeserializeObject<JsonModels>(deserializeKeyMsg);

                if (resultJsonModel != null)
                {
                    ViewBag.IsError = resultJsonModel.IsError;
                    ViewBag.Message = resultJsonModel.Message;
                }
            }
            ViewBag.IsFromUser = 1;
            ViewBag.FormName = STR_FORMNAME;
            bool isEdit = false;
            if (id != null && !id.Equals(Guid.Empty))
            {
                isEdit = true;
                ViewBag.IsEdit = 1;
            }
            else
            {
                ViewBag.IsEdit = 0;
            }
            try
            {
                _objUserDivisionSecurityRoleList.obj = _objUserDivisionSecurityRoleAction.GetAllUsersByDivision(_CurrentLanguageId);
                //Create action object to fill related Objects
                ViewBag.AvailableRolelist = new SelectList(_objUserDivisionSecurityRoleList.obj, "DivisionId", "DivisionName");

                // for _objUserDivisionSecurityRole.obj.SelectedDivisionList 
                BLClient.DivisionAction objDivisionAction = new BLClient.DivisionAction(_CurrentClientId, true);
                //for _objUserDivisionSecurityRole.obj.SelectedSecurityRoleList 
                BLClient.ATSSecurityRoleAction objATSSecurityRoleAction = new BLClient.ATSSecurityRoleAction(_CurrentClientId, true);

                //END 

                CreateObjDivision(id);

                //Create new objects
                _objUserDivisionSecurityRole.obj = new BEClient.UserDivisionSecurityRole();
                _objUserDivisionSecurityRole.obj.objUser = new BEClient.User();
                _objUserDivisionSecurityRole.obj.objUserDetail = new BEClient.UserDetails();
                _objUserDivisionSecurityRole.obj.SelectedDivisionList = new List<BEClient.Division>();
                _objUserDivisionSecurityRole.obj.SelectedSecurityRoleList = new List<BEClient.ATSSecurityRolePrivilages>();
                //END

                _objUserDivisionSecurityRole.obj.ClientId = _CurrentClientId;

                int securityRoleSqe = (int)ATSCommon.CurrentSession.Instance.VerifiedUser.SecurityRoles.Select(r => r.AtsSecurityRole).First();

                ViewBag.IsHorizontal = 1;

                if (isEdit)
                {
                    List<BEClient.RecordExists> LstRecordExists = new List<BEClient.RecordExists>();
                    BLClient.RecordExistsAction _objRecordExists = new BLClient.RecordExistsAction(_CurrentClientId);
                    LstRecordExists = _objRecordExists.GetRecordsCountByUser((Guid)id);

                    _objUserDivisionSecurityRole.ActionName = "Edit";

                    _objUserDivisionSecurityRole.obj = _objUserDivisionSecurityRoleAction.GetRecordById((Guid)id);

                    _objUserDivisionSecurityRole.obj.RecordExistsCount = LstRecordExists;

                    _objUserDivisionSecurityRole.obj.objUser.Password = ATSHelper.Encryption.EncryptionAlgo.DecryptData(_objUserDivisionSecurityRole.obj.objUser.Password);

                    _objUserDivisionSecurityRole.obj.DivisionId = _objUserDivisionSecurityRole.obj.objUser.DivisionId;
                    string selectedDivision = _objUserDivisionSecurityRole.obj.DivisionId.ToString();
                    ViewBag.DisableDivision = new string[] { selectedDivision };

                    _objUserDivisionSecurityRole.obj.SelectedDivisionList = objDivisionAction.GetDivisionByUserAndClient((Guid)id);

                    _objUserDivisionSecurityRole.obj.SelectedSecurityRoleList = objATSSecurityRoleAction.GetSecurityRoleByUserAndClient((Guid)id, _CurrentLanguageId);

                    BLClient.UserDivisionPermissionAction objUserDivisionPermissionAction = new BLClient.UserDivisionPermissionAction(_CurrentClientId);
                    _objUserDivisionSecurityRole.obj.LstUserDivisionPermission = objUserDivisionPermissionAction.GetAllAdhocDivisionByUser((Guid)id, _CurrentLanguageId);

                    objPageMode = BEClient.PageMode.Update;
                    if (_objUserDivisionSecurityRole.obj.PermissionType == null)
                    {
                        return RedirectToAction(ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_CONTROLLER, new { KeyMsg = ATSCommon.CommonFunctions.NonAuthoriseduser() });
                    }
                    if (_objUserDivisionSecurityRole.obj.PermissionType.Contains(BEClient.ATSPermissionType.Delete))
                    {
                        ViewBag.IsDelete = true;
                    }

                    _objBreadScrumbUsers.obj = _objUserDivisionSecurityRole;

                    _objBreadScrumbUsers.ItemName = _objBreadScrumbUsers.obj.obj.objUserDetail.FirstName + " " + _objBreadScrumbUsers.obj.obj.objUserDetail.LastName;
                    SetBreadScrumb(ref _objBreadScrumbUsers);

                    NavView(id, _objBreadScrumbUsers.ToolTip, ordinal);


                    //Start Privilege

                    //int securityRoleSque = (int)ATSCommon.CurrentSession.Instance.VerifiedUser.SecurityRoles.Select(r => r.AtsSecurityRole).First();
                    //if (securityRoleSque >= (int)BEClient.ATSSecurityRole.SetupManager)
                    //{
                    //    ViewBag.IsReadOnly = 0;
                    //}
                    //else
                    //{
                    //    ViewBag.IsReadOnly = 0;
                    //}
                    _objBreadScrumbUsers.obj.obj.AvailableSecurityRoleList = objATSSecurityRoleAction.GetAllATSSecurityRoleByClientAndLanguage(_CurrentLanguageId, securityRoleSqe);
                    if (id == null || id.Equals(Guid.Empty))
                    {
                        _objBreadScrumbUsers.obj.obj.SelectedSecurityRoleList = new List<BEClient.ATSSecurityRolePrivilages>();
                    }
                    else
                    {
                        _objBreadScrumbUsers.obj.obj.UserId = (Guid)id;
                        _objBreadScrumbUsers.obj.obj.SelectedSecurityRoleList = objATSSecurityRoleAction.GetSecurityRoleByUserAndClient((Guid)id, _CurrentLanguageId);
                    }


                    #region For New ATS Security Role Privileges


                    BLClient.ATSRolePrivilegeAction objRolePrivilegeAction = new BLClient.ATSRolePrivilegeAction(_CurrentClientId);
                    List<Guid> lstAtsSecurityRole = new List<Guid>();
                    String lstRoleStr = String.Join(",", _objBreadScrumbUsers.obj.obj.SelectedSecurityRoleList.Select(g => g.SRP_Id.ToString()).ToArray());
                    _objBreadScrumbUsers.obj.obj.LstATSRolePrivilege = objRolePrivilegeAction.GetAllPrivilegeBySecurityRoles(lstRoleStr, _CurrentClientId);


                    #endregion




                    #region RECENT VIEWED
                    BEClient.RecentlyViewed objRecentlView = new BEClient.RecentlyViewed();
                    BLClient.RecentlyViewedAction objRecentlyViewedAction = new BLClient.RecentlyViewedAction(_CurrentClientId);
                    objRecentlView.Category = BEClient.RecentlyViewedCategory.User.ToString();
                    objRecentlView.ViewItemId = (Guid)id;
                    objRecentlView.URL = HttpContext.Request.Url.ToString();
                    List<BEClient.RecentlyViewed> objList = objRecentlyViewedAction.GetRecentlyViewedApplication(objRecentlView);
                    _objBreadScrumbUsers.objRecentViewedList = new List<BEClient.RecentlyViewed>();
                    _objBreadScrumbUsers.objRecentViewedList = objList;
                    #endregion
                }
                else
                {
                    _objUserDivisionSecurityRole.obj.RecordExistsCount = new List<BEClient.RecordExists>();
                    _objUserDivisionSecurityRole.ActionName = "Create";
                    objPageMode = BEClient.PageMode.Create;
                    _objUserDivisionSecurityRole.obj.PermissionType = new List<BEClient.ATSPermissionType> { BEClient.ATSPermissionType.Create };

                    _objBreadScrumbUsers.ItemName = "ADD USER";
                    SetBreadScrumb(ref _objBreadScrumbUsers);

                    NavView(id, _objBreadScrumbUsers.DisplayName, ordinal);
                }
                ViewBag.PageMode = objPageMode;
                /*fill Drop Down*/

                _objUserDivisionSecurityRole.RecordPermissionType = _objUserDivisionSecurityRole.obj.PermissionType;

                _objUserDivisionSecurityRole.obj.AvailableSecurityRoleList = objATSSecurityRoleAction.GetAllATSSecurityRoleByClientAndLanguage(_CurrentLanguageId, securityRoleSqe);
                _objUserDivisionSecurityRole.obj.AvailableDivisionList = _objUserDivisionSecurityRoleAction.GetAllDivisionByScope(_CurrentLanguageId, ATS.WebUi.Common.CommonFunctions.GetPageMode(_objUserDivisionSecurityRole.RecordPermissionType, objPageMode).ToString());
                var securityroles = (from v in _objUserDivisionSecurityRole.obj.AvailableSecurityRoleList
                                     where v.SqNo < securityRoleSqe
                                     select v.SRP_Id.ToString()).ToList();
                ViewBag.DisableSecurityRoles = securityroles.ToArray();

                List<BEClient.Division> lstDivision = new List<BEClient.Division>();
                _objUserDivisionSecurityRole.obj.objDivisionList = _objUserDivisionSecurityRoleAction.GetAllDivisionByScope(_CurrentLanguageId, ATS.WebUi.Common.CommonFunctions.GetPageMode(_objUserDivisionSecurityRole.RecordPermissionType, objPageMode).ToString());
                //BLClient.DivisionAction _objDivisionAction = new BLClient.DivisionAction(_CurrentClientId, true);
                //_objUserDivisionSecurityRole.obj.objDivisionList = _objDivisionAction.GetAllDivisionsByLanguage(_CurrentLanguageId);
                ViewBag.DivisionList = new SelectList(_objUserDivisionSecurityRole.obj.objDivisionList, "DivisionId", "DivisionName");

                TempData["RecordPermissionType"] = _objUserDivisionSecurityRole.RecordPermissionType;
                TempData["RecordExistCount"] = _objUserDivisionSecurityRole.obj.RecordExistsCount;

                ViewBag.ISFromUSer = true;
                _objBreadScrumbUsers.obj = _objUserDivisionSecurityRole;
                return View(_objBreadScrumbUsers);
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);

                /*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);

                /*Redirect to List Page*/
                return RedirectToAction(ATS.WebUi.Common.Constants.STR_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }

        public void FillDropDownValue(BESrp.DynamicSRP<BEClient.UserDivisionSecurityRole> objATSModel, BEClient.PageMode objPageMode)
        {
            //Create action object to fill related Objects

            // for _objUserDivisionSecurityRole.obj.SelectedDivisionList 
            BLClient.DivisionAction objDivisionAction = new BLClient.DivisionAction(_CurrentClientId, true);

            //for _objUserDivisionSecurityRole.obj.SelectedSecurityRoleList 
            BLClient.ATSSecurityRoleAction objATSSecurityRoleAction = new BLClient.ATSSecurityRoleAction(_CurrentClientId, true);

            //END 

            List<BEClient.Division> lstDivision = new List<BEClient.Division>();
            objATSModel.obj.objDivisionList = _objUserDivisionSecurityRoleAction.GetAllDivisionByScope(_CurrentLanguageId, ATS.WebUi.Common.CommonFunctions.GetPageMode(objATSModel.RecordPermissionType, objPageMode).ToString());
            ViewBag.DivisionList = new SelectList(objATSModel.obj.objDivisionList, "DivisionId", "DivisionName");

            objATSModel.obj.DivisionId = objATSModel.obj.objUser.DivisionId;
            string selectedDivision = objATSModel.obj.DivisionId.ToString();
            ViewBag.DisableDivision = new string[] { selectedDivision };

            List<BEClient.Division> objDivisionList = new List<BEClient.Division>();
            if (objATSModel.obj.objDivision != null)
            {
                foreach (var v in objATSModel.obj.objDivision.DivisionId)
                {
                    BEClient.Division objDivision = new BEClient.Division();
                    objDivision.DivisionId = new Guid(v);
                    objDivisionList.Add(objDivision);
                }
            }

            objATSModel.obj.SelectedDivisionList = objDivisionList;

            List<BEClient.ATSSecurityRolePrivilages> objATSSecurityRolePrivilagesLst = new List<BEClient.ATSSecurityRolePrivilages>();
            if (objATSModel.obj.objATSSecurityRolePrivilagesMaster != null)
            {
                foreach (var v in objATSModel.obj.objATSSecurityRolePrivilagesMaster.SRP_Ids)
                {
                    BEClient.ATSSecurityRolePrivilages objATSSecurityRolePrivilages = new BEClient.ATSSecurityRolePrivilages();
                    objATSSecurityRolePrivilages.SRP_Id = new Guid(v);
                    objATSSecurityRolePrivilagesLst.Add(objATSSecurityRolePrivilages);
                }
            }

            objATSModel.obj.SelectedSecurityRoleList = objATSSecurityRolePrivilagesLst;

            int securityRoleSqe = (int)ATSCommon.CurrentSession.Instance.VerifiedUser.SecurityRoles.Select(r => r.AtsSecurityRole).First();

            objATSModel.obj.AvailableSecurityRoleList = objATSSecurityRoleAction.GetAllATSSecurityRoleByClientAndLanguage(_CurrentLanguageId, securityRoleSqe);

            objATSModel.obj.AvailableDivisionList = _objUserDivisionSecurityRoleAction.GetAllDivisionByScope(_CurrentLanguageId, ATS.WebUi.Common.CommonFunctions.GetPageMode(objATSModel.RecordPermissionType, objPageMode).ToString());

        }

        [HttpPost]
        public ActionResult Create(RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.UserDivisionSecurityRole>> _objBreadScrumbUser)
        {
            string keyMsg = string.Empty;
            string jsonModels = string.Empty;
            JsonModels actionStatus = null;

            if (_objBreadScrumbUser.obj.obj.objATSSecurityRolePrivilagesMaster == null)
            {
                actionStatus = base.GetJsonContent(true, string.Empty, "Please select at least one security role.");
                jsonModels = JsonConvert.SerializeObject(actionStatus);
                keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);

                return RedirectToAction(ATS.WebUi.Common.Constants.STR_VIEW_METHOD, new {KeyMsg = keyMsg });
            }

            BESrp.DynamicSRP<BEClient.UserDivisionSecurityRole> objATSModel = _objBreadScrumbUser.obj;
            ViewBag.FormName = STR_FORMNAME;
            String Message = String.Empty;
            Message = ServerValidation();
            BEClient.PageMode objPageMode = BEClient.PageMode.Create;
            objATSModel.RecordPermissionType = (List<ATS.BusinessEntity.ATSPermissionType>)TempData["RecordPermissionType"];
            objATSModel.obj.RecordExistsCount = (List<ATS.BusinessEntity.RecordExists>)TempData["RecordExistCount"];
            objATSModel.RemoveBtnText = _objUserDivisionSecurityRoleList.RemoveBtnText;
            objATSModel.SaveBtnText = _objUserDivisionSecurityRoleList.SaveBtnText;


            TempData["RecordPermissionType"] = objATSModel.RecordPermissionType;
            TempData["RecordExistCount"] = objATSModel.obj.RecordExistsCount;
            ViewBag.PageMode = objPageMode;

            try
            {
                if (!String.IsNullOrEmpty(Message) && Message != "ProfileName is required")
                {

                    ViewBag.IsError = true;
                    ViewBag.Message = Message;
                    ViewBag.IsEdit = 0;
                    FillDropDownValue(objATSModel, objPageMode);
                    _objBreadScrumbUser.obj = objATSModel;
                    return View(ATS.WebUi.Common.Constants.STR_VIEW_METHOD, _objBreadScrumbUser);
                }
                objATSModel.obj.objUser.Password = ATSHelper.Encryption.EncryptionAlgo.EncryptionData(objATSModel.obj.objUser.Password);

                BLClient.UserDivisionSecurityRoleAction objUserDivisionSecurityRoleAction = new BLClient.UserDivisionSecurityRoleAction(_CurrentClientId);
                Guid userId = Guid.Empty;
                userId = objUserDivisionSecurityRoleAction.InsertUser(objATSModel.obj);

                if (userId != null && !userId.Equals(Guid.Empty))
                {
                    actionStatus = base.GetJsonContent(false, string.Empty, "Record Added Successfully");
                }
                else
                {
                    actionStatus = base.GetJsonContent(true, string.Empty, "Not Able To Add Record");
                }
                /*Convert to json string*/
                jsonModels = JsonConvert.SerializeObject(actionStatus);
                keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                return RedirectToAction("Index", new { KeyMsg = keyMsg });
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                actionStatus = base.GetJsonContent(true, string.Empty, Message);

                /*Convert to json string*/
                jsonModels = JsonConvert.SerializeObject(actionStatus);
                keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                Message = ex.Message;
                ViewBag.IsError = true;
                ViewBag.Message = Message;
                FillDropDownValue(objATSModel, objPageMode);
                ViewBag.IsEdit = 0;

                _objBreadScrumbUser.obj = objATSModel;

                SetBreadScrumb(ref _objBreadScrumbUser);

                return View(ATS.WebUi.Common.Constants.STR_VIEW_METHOD, _objBreadScrumbUser);
                /*Redirect to List Page*/
                //return RedirectToAction(ATS.WebUi.Common.Constants.STR_LIST_METHOD, new { KeyMsg = keyMsg });
            }

        }

        [HttpPost]
        public ActionResult Edit(RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.UserDivisionSecurityRole>> _objBreadScrumbUsers)
        {
            string keyMsg = string.Empty;
            string jsonModels = string.Empty;
            JsonModels actionStatus = null;

            if (_objBreadScrumbUsers.obj.obj.objATSSecurityRolePrivilagesMaster == null)
            {
                actionStatus = base.GetJsonContent(true, string.Empty, "Please select at least one security role.");
                jsonModels = JsonConvert.SerializeObject(actionStatus);
                keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);

                return RedirectToAction(ATS.WebUi.Common.Constants.STR_VIEW_METHOD, new { id = _objBreadScrumbUsers.obj.obj.UserId, KeyMsg = keyMsg });
            }

            BESrp.DynamicSRP<BEClient.UserDivisionSecurityRole> objATSModel = _objBreadScrumbUsers.obj;
            String Message = String.Empty;

            ViewBag.FormName = STR_FORMNAME;
            Message = ServerValidation();
            objATSModel.RecordPermissionType = (List<ATS.BusinessEntity.ATSPermissionType>)TempData["RecordPermissionType"];
            objATSModel.obj.RecordExistsCount = (List<ATS.BusinessEntity.RecordExists>)TempData["RecordExistCount"];
            objATSModel.RemoveBtnText = _objUserDivisionSecurityRoleList.RemoveBtnText;
            objATSModel.SaveBtnText = _objUserDivisionSecurityRoleList.SaveBtnText;
            BEClient.PageMode objPageMode = BEClient.PageMode.Update;

            try
            {
                if (!String.IsNullOrEmpty(Message) && Message != "Password required." && Message != "ProfileName is required")
                {
                    TempData["RecordPermissionType"] = objATSModel.RecordPermissionType;
                    TempData["RecordExistCount"] = objATSModel.obj.RecordExistsCount;
                    ViewBag.PageMode = BEClient.PageMode.Update;
                    ViewBag.IsError = true;
                    ViewBag.Message = Message;
                    ViewBag.IsEdit = 1;
                    FillDropDownValue(objATSModel, objPageMode);

                    return View(ATS.WebUi.Common.Constants.STR_VIEW_METHOD, objATSModel);
                }
                objATSModel.obj.objUser.Password = ATSHelper.Encryption.EncryptionAlgo.EncryptionData(objATSModel.obj.objUser.Password);

                BLClient.UserDivisionSecurityRoleAction objUserDivisionSecurityRoleAction = new BLClient.UserDivisionSecurityRoleAction(_CurrentClientId);
                bool user = false;
                List<BEClient.PrivilegeAndPermissionAndScope> objGroupListOfSameRemarkQty = new List<BEClient.PrivilegeAndPermissionAndScope>();

                user = objUserDivisionSecurityRoleAction.UpdateUser(objATSModel.obj, objGroupListOfSameRemarkQty);
                //user = objUserDivisionSecurityRoleAction.InsertSecurityRoleByUser(objATSModel.obj.objATSSecurityRolePrivilagesMaster.SRP_Ids,objGroupListOfSameRemarkQty,objATSModel.obj.UserId, objATSModel.obj.objUserDetail);

                if (user)
                {
                    try
                    {
                        ATSCommon.CommonFunctions.SolrFullImport();
                    }
                    catch (Exception ex)
                    {
                        log.Error("SOLR Employee FULL IMPORT (Vacancy)" + ex.Message);
                    }
                    actionStatus = base.GetJsonContent(false, string.Empty, "Record Updated Successfully");
                }
                else
                {
                    actionStatus = base.GetJsonContent(true, string.Empty, "Not Able To Update Record");
                }

                /*Convert to json string*/
                jsonModels = JsonConvert.SerializeObject(actionStatus);
                keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                return RedirectToAction("Index", new { KeyMsg = keyMsg });
            }
            catch (Exception ex)
            {
                actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);

                /*Convert to json string*/
                jsonModels = JsonConvert.SerializeObject(actionStatus);
                keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);

                _objBreadScrumbUsers.obj = objATSModel;
                SetBreadScrumb(ref _objBreadScrumbUsers);

                /*Redirect to List Page*/
                return RedirectToAction(ATS.WebUi.Common.Constants.STR_LIST_METHOD, new { KeyMsg = keyMsg });
            }

        }

        #region delete
        public ActionResult Delete(string id)
        {
            try
            {
                BLClient.UserDivisionSecurityRoleAction objUserDivisionSecurityRoleAction = new BLClient.UserDivisionSecurityRoleAction(_CurrentClientId);
                bool Result = objUserDivisionSecurityRoleAction.Delete(id);
                JsonModels actionStatus = null;
                if (Result)
                {
                    actionStatus = base.GetJsonContent(false, string.Empty, "Record Deleted Successfully");
                }
                else
                {
                    actionStatus = base.GetJsonContent(true, string.Empty, "Not Able To Deleted Record");
                }
                try
                {
                    ATSCommon.CommonFunctions.SolrFullImport();
                }
                catch (Exception ex)
                {
                    log.Error("SOLR Employee FULL IMPORT (Vacancy)" + ex.Message);
                }
                /*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                /*Redirect to List Page*/
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                return RedirectToAction(ATS.WebUi.Common.Constants.STR_LIST_METHOD, new { KeyMsg = keyMsg });
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);

                ///*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);

                /*Redirect to List Page*/
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                return RedirectToAction(ATS.WebUi.Common.Constants.STR_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }

        public ActionResult DeleteMultiple(string id)
        {
            string Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                BLClient.UserDivisionSecurityRoleAction objUserDivisionSecurityRoleAction = new BLClient.UserDivisionSecurityRoleAction(_CurrentClientId);
                bool Result = objUserDivisionSecurityRoleAction.Delete(id);
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

        #endregion

        #region Private Member
        private String ServerValidation()
        {
            String ErrorMessage = String.Empty;
            bool isServerError = false;
            if (!ModelState.IsValid)
            {
                // do something to display errors . 
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        ErrorMessage = error.ErrorMessage;
                        isServerError = true;
                        break;
                    }
                    if (isServerError)
                        break;

                }
            }
            return ErrorMessage;
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
            objBreadCrumb.Controller = "User";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_EMPLOYEE;
            objBreadCrumb.ordinal = objBreadCrumblst.Count() + 1;
            objBreadCrumb.URL = Url.Action("Index", "User", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, ordinal = objBreadCrumb.ordinal });
            objBreadCrumb.ImagePath = BECommon.ImagePaths.VacancyImage;
            objBreadCrumb.ToolTip = "Users";
            objBreadCrumb.WithoutOrdinalURL = Url.Action("Index", "User", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });

            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);

            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
        }


        private void NavView(Guid? UserId, String DisplayToolTip, String ordinal)
        {

            ATSCommon.CommonFunctions.SetNullNav(this.ControllerContext.RequestContext);
            List<BEClient.BreadCrumb> objBreadCrumblst = ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs;
            if (ordinal != null && !String.IsNullOrEmpty(ordinal.ToString()))
            {
                objBreadCrumblst.RemoveAll(x => x.ordinal >= Convert.ToInt32(ordinal.ToString()));
            }
            BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
            objBreadCrumb.Action = "View";
            objBreadCrumb.Controller = "User";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_EMPLOYEE;
            objBreadCrumb.ordinal = objBreadCrumblst.Count() + 1;
            objBreadCrumb.URL = Url.Action("View", "UserId", new { id = UserId, area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, ordinal = objBreadCrumb.ordinal });
            objBreadCrumb.ImagePath = BECommon.ImagePaths.VacancyImage;
            objBreadCrumb.ToolTip = "Users </br>" + DisplayToolTip;
            objBreadCrumb.WithoutOrdinalURL = Url.Action("View", "User", new { id = UserId, area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });
            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);

            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;

        }

        private void SetBreadScrumb(ref RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.UserDivisionSecurityRole>> _objBreadScrumbUsers)
        {
            _objBreadScrumbUsers.DisplayName = STR_FORMNAME;
            _objBreadScrumbUsers.ImagePath = BECommon.ImagePaths.VacancyImage;
            _objBreadScrumbUsers.ToolTip = _objBreadScrumbUsers.DisplayName;
        }
        #endregion

        public JsonResult GetAllPrivilegeBySecurityRoleId(string securityRoleId)
        {
            BLClient.ATSSecurityRoleAction objATSSecurityRoleAction = new BLClient.ATSSecurityRoleAction(_CurrentClientId);

            var objPrevilegList = objATSSecurityRoleAction.GetAllPrivilegeBySecurityRoleId(securityRoleId);

            var objPrevilege = objPrevilegList.Select(v => v.SRP_Id).Distinct();

            return base.GetJson(new
            {
                DisableList = objPrevilegList,
                PrivilegeList = objPrevilege
            });
        }
        public JsonResult GetAdHocRoles(Guid UserId)
        {
            try
            {
                List<BEClient.PrivilegeAndPermissionAndScope> objGroupListOfSameRemarkQty = new List<BEClient.PrivilegeAndPermissionAndScope>(); ;
                BLClient.AdHocPrivilegeAction objASD = new BLClient.AdHocPrivilegeAction(_CurrentClientId);
                objGroupListOfSameRemarkQty = objASD.GetAdHocRolesByUserAndClient((Guid)UserId);
                return GetJson(objGroupListOfSameRemarkQty);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public JsonResult GetAllATSPrevilegeByClientAndLanguage()
        {
            try
            {
                BLClient.ATSSecurityRoleAction objATSSecurityRoleAction = new BLClient.ATSSecurityRoleAction(_CurrentClientId);
                List<BEClient.ATSSecurityRolePrivilages> ATSPrevilege = objATSSecurityRoleAction.GetAllATSPrevilegeByClientAndLanguage(_CurrentLanguageId);
                var PrivilegeId = (from v in ATSPrevilege
                                   select new
                                   {
                                       PrivilegeId = v.SRP_Id
                                   }).ToList();
                return base.GetJson(PrivilegeId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult EmployeeChangePassword()
        {
            try
            {
                ATS.WebUi.Models.SignUpModel objSignUpUser = new SignUpModel();
                objSignUpUser.UserId = _CurrentUserId;
                objSignUpUser.UserName = ATS.WebUi.Common.CurrentSession.Instance.UserName;
                return View("ChangePassword", objSignUpUser);
            }
            catch
            {
                throw;
            }
        }
        public List<Guid> GetListFromString(string strList)
        {
            List<Guid> GuidList = new List<Guid>();
            foreach (string id in strList.Split(','))
            {
                GuidList.Add(new Guid(id));
            }
            return GuidList;
        }

        //New DropDown List        
        [HttpGet]
        public JsonResult GetDivisionDropDown(Guid UserId, Guid SelectedDivision)
        {
            bool IsError = false;
            String Message = string.Empty;
            String Data = String.Empty;
            try
            {
                BLClient.DivisionAction objDivisionAction = new BLClient.DivisionAction(_CurrentClientId);
                _objUserDivisionSecurityRole = new BESrp.DynamicSRP<BEClient.UserDivisionSecurityRole>();
                _objUserDivisionSecurityRole.obj = new BEClient.UserDivisionSecurityRole();
                _objUserDivisionSecurityRole.obj.SelectedDivisionList = new List<BEClient.Division>();
                _objUserDivisionSecurityRole.obj.SelectedDivisionList = objDivisionAction.GetDivisionByUserAndClient(UserId);
                List<Guid> selDiv = new List<Guid>();
                //selDiv = 
                _objUserDivisionSecurityRole.obj.strSelectedDivisionList = GetStringFromList(_objUserDivisionSecurityRole.obj.SelectedDivisionList.Select(x => x.DivisionId).ToList());
                DivisionDropDown(SelectedDivision, UserId);
                Data = base.RenderRazorViewToString("Shared/_AdHocDrpDown", _objUserDivisionSecurityRole);
            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;
            }
            return GetJson(GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }
        private void DivisionDropDown(Guid SelectedDivision, Guid UserId)
        {
            try
            {
                BEClient.PageMode objPageMode = BEClient.PageMode.Update;
                _objUserDivisionSecurityRoleList.obj = new List<BEClient.UserDivisionSecurityRole>();
                _objUserDivisionSecurityRoleAction = new BLClient.UserDivisionSecurityRoleAction(_CurrentClientId, true);
                BLClient.ATSSecurityRoleAction objATSSecurityRoleAction = new BLClient.ATSSecurityRoleAction(_CurrentClientId);
                //_objUserDivisionSecurityRoleAction = new BLClient.DivisionAction(_CurrentClientId);
                BLClient.DivisionAction objDivisionAction = new BLClient.DivisionAction(_CurrentClientId);

                //IEqualityComparer<BEClient.Division> compareDivision = null;

                //           _objUserDivisionSecurityRole.obj.SelectedDivisionList = objDivisionAction.GetDivisionByUserAndClient(UserId);
                _objUserDivisionSecurityRoleList.obj = _objUserDivisionSecurityRoleAction.GetAllUsersByDivision(_CurrentLanguageId);
                _objUserDivisionSecurityRole.obj.AvailableDivisionList = _objUserDivisionSecurityRoleAction.GetAllDivisionByScope(_CurrentLanguageId, ATS.WebUi.Common.CommonFunctions.GetPageMode(_objUserDivisionSecurityRole.RecordPermissionType, objPageMode).ToString());
                _objUserDivisionSecurityRole.obj.AvailableDivisionList = _objUserDivisionSecurityRole.obj.AvailableDivisionList.Where(x => x.DivisionId != SelectedDivision).ToList();
                //_objUserDivisionSecurityRole.obj.AvailableDivisionList = _objUserDivisionSecurityRole.obj.AvailableDivisionList.Where(x=> !_objUserDivisionSecurityRole.obj.SelectedDivisionList.Any(y=>y.DivisionId == x.DivisionId)).ToList();
                ViewBag.AvailableDivisionlist = new SelectList(_objUserDivisionSecurityRole.obj.AvailableDivisionList, "DivisionId", "DivisionName");



            }
            catch
            {
                throw;
            }
        }
        private string GetStringFromList(List<Guid> strList)
        {
            string str = string.Join(",", strList);
            return str;
        }

        [HttpGet]
        public JsonResult AddAdhocDivision(Guid UserId, string LstOfDivisionId)
        {
            bool IsError = false;
            string Message = string.Empty;
            string Data = string.Empty;
            try
            {
                BLClient.UserDivisionPermissionAction objUserDivisionPermissionAction = new BLClient.UserDivisionPermissionAction(_CurrentClientId);
                if (!string.IsNullOrEmpty(LstOfDivisionId))
                {
                    bool result = false;
                    List<Guid> divId = GetListFromString(LstOfDivisionId);
                    result = objUserDivisionPermissionAction.DeleteDivisionByUserAndClient(_CurrentClientId, UserId);
                    if (result)
                    {
                        BEClient.UserDivisionPermission objUserDivisionPermission = new BEClient.UserDivisionPermission();
                        objUserDivisionPermission.ClientId = _CurrentClientId;
                        objUserDivisionPermission.UserId = UserId;
                        objUserDivisionPermission.IsDelete = false;

                        foreach (var _curr in divId)
                        {
                            Guid newId = Guid.Empty;
                            objUserDivisionPermission.DivisionId = _curr;
                            objUserDivisionPermissionAction = new BLClient.UserDivisionPermissionAction(_CurrentClientId);
                            newId = objUserDivisionPermissionAction.AddUserDivisionPermission(objUserDivisionPermission);
                            if (newId.Equals(Guid.Empty))
                            {
                                throw new Exception("Not able to Insert Adhoc Division");
                            }
                            else
                            {
                                ViewBag.IsError = false;
                                ViewBag.Message = "Ad hoc division(s) added successfully.";
                            }
                        }
                    }
                }
                objUserDivisionPermissionAction = new BLClient.UserDivisionPermissionAction(_CurrentClientId);
                List<BEClient.UserDivisionPermission> LstUserDivisionPermission = new List<BEClient.UserDivisionPermission>();
                LstUserDivisionPermission = objUserDivisionPermissionAction.GetAllAdhocDivisionByUser(UserId, _CurrentLanguageId);
                Data = base.RenderRazorViewToString("Shared/_AdHocDivisionList", LstUserDivisionPermission);

            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;
            }
            return GetJson(GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DeleteAdHocDivision(Guid UserPermissionId, Guid _UserId)
        {
            bool IsError = false;
            string Message = string.Empty;
            string Data = string.Empty;
            try
            {
                bool Result = false;
                BLClient.UserDivisionPermissionAction ObjUserDivisionPermissionAction = new BLClient.UserDivisionPermissionAction(_CurrentClientId);
                Result = ObjUserDivisionPermissionAction.DeleteAdhocDivisionById(UserPermissionId);
                if (!Result)
                {
                    IsError = true;
                }
                else
                {
                    BLClient.UserDivisionPermissionAction objUserDivisionPermissionAction = new BLClient.UserDivisionPermissionAction(_CurrentClientId);
                    objUserDivisionPermissionAction = new BLClient.UserDivisionPermissionAction(_CurrentClientId);
                    List<BEClient.UserDivisionPermission> LstUserDivisionPermission = new List<BEClient.UserDivisionPermission>();
                    LstUserDivisionPermission = objUserDivisionPermissionAction.GetAllAdhocDivisionByUser(_UserId, _CurrentLanguageId);
                    ViewBag.IsError = false;
                    ViewBag.Message = "Ad hoc division removed successfully.";
                    Data = base.RenderRazorViewToString("Shared/_AdHocDivisionList", LstUserDivisionPermission);
                }

            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;
            }
            return GetJson(GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DeactivateCandidate(string UserName)
        {
            bool IsError = false;
            string Message = string.Empty;
            string Data = string.Empty;
            try
            {
                if (!String.IsNullOrEmpty(UserName))
                {
                    bool Result = false;
                    BLClient.UserAction ObjUserAction = new BLClient.UserAction(_CurrentClientId);
                    Result = ObjUserAction.DeactivateCandidate(UserName);
                    if (!Result)
                    {
                        IsError = true;
                        Message = "Not able to Deactivate Candidate";
                    }
                    else
                    {
                        Message = "Candidate Deactivated.";

                    }
                }
            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;
            }
            return GetJson(GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ValidateUser(string UserName)
        {
            string Message = String.Empty;
            string Data = String.Empty;
            bool IsError = false;
            try
            {
                if (!String.IsNullOrEmpty(UserName))
                {
                    BLClient.UserAction objUserAction = new BLClient.UserAction(_CurrentClientId);
                    bool Result = objUserAction.ValidateNewEmployee(UserName);
                    if (!Result)
                    {
                        IsError = true;
                    }
                }
            }
            catch (Exception ex)
            {
                IsError = true;
                if (ex.Message == "104")
                {
                    Message = "User already Exits";
                }
                else if (ex.Message == "105")
                {
                    Message = "User is registed but not active";
                }
                else
                {
                    Message = ex.Message;
                }
            }
            return GetJson(GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllAtsrolePrivilegsbyRoles(string Roles)
        {

            bool IsError = false;
            String Message = string.Empty;
            String Data = String.Empty;
            try
            {
                if (!String.IsNullOrEmpty(Roles))
                {
                    BLClient.ATSRolePrivilegeAction ObjATSRolePrivilegeAction = new BLClient.ATSRolePrivilegeAction(_CurrentClientId);

                    List<BEClient.ATSRolePrivilege> lstAtsRolePrivilege = ObjATSRolePrivilegeAction.GetAllPrivilegeBySecurityRoles(Roles, _CurrentClientId);

                    Data = base.RenderRazorViewToString("_ATSSecurityRole", lstAtsRolePrivilege);

                }
                ViewBag.ISFromUser = true;
            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;
            }
            return GetJson(GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }
    }
}
