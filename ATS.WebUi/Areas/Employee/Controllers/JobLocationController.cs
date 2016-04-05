using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEMaster = ATS.BusinessEntity.Master;
using BLMaster = ATS.BusinessLogic.Master;
using BECommon = ATS.BusinessEntity.Common;
using BEClient = ATS.BusinessEntity;
using BLClient = ATS.BusinessLogic;
using ATSCommon = ATS.WebUi.Common;
using ATSHelper = ATS.HelperLibrary;
using BESrp = ATS.BusinessEntity.SRPEntity;
using ATS.WebUi.Models;
using Newtonsoft.Json;
using log4net;
using RootModels = ATS.WebUi.Models;
using System.Web.Mvc.Html;
using DisplayMessageConstant = ATS.BusinessEntity.Common;

namespace ATS.WebUi.Areas.Employee.Controllers
{
    public class JobLocationController : ATS.WebUi.Controllers.AreaBaseController
    {
        #region Private members
        private BLClient.JobLocationAction _objJobLocationAction;
        // private List<BEClient.JobLocation> _JobLocationList;
        private Guid _CurrentClientId;
        private Guid _CurrentLanguageId;
        private static readonly log4net.ILog log = LogManager.GetLogger(
     System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        private BESrp.DynamicSRP<List<BEClient.JobLocation>> _JobLocationList;
        private BESrp.DynamicSRP<BEClient.JobLocation> _objJobLocation;

        #endregion

        #region Redirection Method
        private string STR_JOBLOCATION_LIST_METHOD = "Index";
        private string STR_JOBLOCATION_CREATE_METHOD = "Create";
        private string STR_JOBLOCATION_EDIT_METHOD = "Edit";
        private string STR_JOBLOCATION_VIEW_METHOD = "View";
        private string STR_JOBLOCATION_FORMNAME = BEClient.Common.CompanySetupMenu.CSMNU_JOBLOCATION;

        #endregion

        #region OnAuthorization Method call on every request
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (base.CurrentClient != null && Common.CurrentSession.Instance.VerifiedUser != null)
            {
                _CurrentClientId = base.CurrentClient.ClientId;
                _CurrentLanguageId = base.CurrentClient.CurrentLanguageId;
                _objJobLocationAction = new BLClient.JobLocationAction(_CurrentClientId, true);
                ViewBag.FormName = STR_JOBLOCATION_FORMNAME;

                //Create new object for List
                _JobLocationList = new BESrp.DynamicSRP<List<BEClient.JobLocation>>();
                _JobLocationList.AddBtnText = BEClient.Common.CommonConstant.ADD;
                _JobLocationList.EditBtnText = BEClient.Common.CommonConstant.UPDATE;
                _JobLocationList.RemoveBtnText = BEClient.Common.CommonConstant.DELETE;
                _JobLocationList.SaveBtnText = BEClient.Common.CommonConstant.SAVE;
                _JobLocationList.UserPermissionWithScope = _objJobLocationAction.ListUserPermissionWithScope;
                if (ATSCommon.CurrentSession.Instance.VerifiedUser.SecurityRoles.Where(x => x.AtsSecurityRole == BEClient.ATSSecurityRole.Candidate).Count() > 0)
                {
                    filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_CANDIDATE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_CANDIDATE_CONTROLLER, new { area = ATSCommon.Constants.AREA_CANDIDATE }));

                }
                if (filterContext.ActionDescriptor.ActionName == "View" && filterContext.RouteData.Values.Keys.Count() == 2)
                {
                    if (_JobLocationList.UserPermissionWithScope.Where(x => x.PermissionType.Contains(BEClient.ATSPermissionType.Create)).Count() <= 0)
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

        private void CreateObjJobLocation(Guid? JobLocationId)
        {
            _objJobLocation = new BESrp.DynamicSRP<BEClient.JobLocation>();
            _objJobLocation.AddBtnText = _JobLocationList.AddBtnText;
            _objJobLocation.EditBtnText = _JobLocationList.EditBtnText;
            _objJobLocation.RemoveBtnText = _JobLocationList.RemoveBtnText;
            _objJobLocation.SaveBtnText = _JobLocationList.SaveBtnText;
            _objJobLocation.UserPermissionWithScope = _objJobLocationAction.ListUserPermissionWithScope;
        }

        public ActionResult Index(string KeyMsg, string ordinal)
        {
            RootModels.BreadScrumbModel<BESrp.DynamicSRP<List<BEClient.JobLocation>>> _objBreadScrumbJobLocation = new RootModels.BreadScrumbModel<BESrp.DynamicSRP<List<BEClient.JobLocation>>>();
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
                _JobLocationList.AddRecordURL = AddJobLocationURL();
                _JobLocationList.obj = _objJobLocationAction.GetAllJobLocationByLanguageId(ATSCommon.CurrentSession.Instance.VerifiedClient.CurrentLanguageId);
                ViewBag.PageMode = BEClient.PageMode.View;

                NavIndex(ordinal);

                _objBreadScrumbJobLocation.obj = _JobLocationList;
                _objBreadScrumbJobLocation.DisplayName = STR_JOBLOCATION_FORMNAME;
                _objBreadScrumbJobLocation.ToolTip = STR_JOBLOCATION_FORMNAME;
                return View(_objBreadScrumbJobLocation);

            }
            catch
            {

                throw;
            }
        }

        public ActionResult Create(RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.JobLocation>> _objBreadScrumbJobLocation)
        {

            if (_objBreadScrumbJobLocation.obj == null)
            {
                return RedirectToAction("View", "Joblocation");
            }
            BESrp.DynamicSRP<BEClient.JobLocation> jobLocation = _objBreadScrumbJobLocation.obj;
            try
            {

                if (!string.IsNullOrEmpty(_objBreadScrumbJobLocation.obj.obj.SelecteDivision))
                {
                    string[] res = _objBreadScrumbJobLocation.obj.obj.SelecteDivision.Split(new char[] { ';' });
                    _objBreadScrumbJobLocation.obj.obj.objDivision = new BEClient.DivisionMaster();
                    _objBreadScrumbJobLocation.obj.obj.objDivision.DivisionId = res.ToArray();
                }
                ViewBag.FormName = STR_JOBLOCATION_FORMNAME;
                jobLocation.RecordPermissionType = (List<ATS.BusinessEntity.ATSPermissionType>)TempData["RecordPermissionType"];
                jobLocation.obj.RecordExistsCount = (List<ATS.BusinessEntity.RecordExists>)TempData["RecordExistCount"];
                jobLocation.RemoveBtnText = _JobLocationList.RemoveBtnText;
                jobLocation.SaveBtnText = _JobLocationList.SaveBtnText;

                String defaultname = jobLocation.obj.DefaultValue;
                //Change null value with default one
                (from trade in jobLocation.obj.JobLocatoinEntityLanguage
                 where String.IsNullOrEmpty(trade.LocalName)
                 select trade).ToList().ForEach(trade => trade.LocalName = defaultname);

                Guid newId = _objJobLocationAction.AddJobLocation(jobLocation.obj);

                JsonModels actionStatus = null;
                if (newId != null && !newId.Equals(Guid.Empty))
                {
                    actionStatus = base.GetJsonContent(false, string.Empty, "Record Added Successfully");
                }
                else
                {
                    actionStatus = base.GetJsonContent(true, string.Empty, "Not Able To Add Record");
                }
                /*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = ATSHelper.Encryption.EncryptionAlgo.EncryptionData(jsonModels);

                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);

                /*Redirect to List Page*/

                return RedirectToAction(STR_JOBLOCATION_LIST_METHOD, new { KeyMsg = keyMsg });

            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = ATSHelper.Encryption.EncryptionAlgo.EncryptionData(jsonModels);

                TempData["RecordPermissionType"] = jobLocation.RecordPermissionType;
                TempData["RecordExistCount"] = jobLocation.obj.RecordExistsCount;

                BEClient.PageMode objPageMode = BEClient.PageMode.Create;
                ViewBag.PageMode = objPageMode;

                ViewBag.IsError = true;
                ViewBag.Message = ex.Message;
                FillDropDown(ATS.WebUi.Common.CommonFunctions.GetPageMode(jobLocation.RecordPermissionType, objPageMode).ToString(), _objBreadScrumbJobLocation.obj.obj.DivisionId);

                BLClient.DivisionAction ObjDivisionAction = new BLClient.DivisionAction(_CurrentClientId, true);
                _objBreadScrumbJobLocation.obj = jobLocation;
                _objBreadScrumbJobLocation.obj.obj.AvailableDivisionList = ObjDivisionAction.GetAllDivisionByClientAndUsersTree(_CurrentLanguageId);

                SetBreadScrumb(ref _objBreadScrumbJobLocation);
                return View(ATS.WebUi.Common.Constants.STR_VIEW_METHOD, _objBreadScrumbJobLocation);
            }
        }

        public ActionResult Edit(RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.JobLocation>> _objBreadScrumbJobLocation)
        {
            BESrp.DynamicSRP<BEClient.JobLocation> jobLocation = _objBreadScrumbJobLocation.obj;
            try
            {
                if (!string.IsNullOrEmpty(_objBreadScrumbJobLocation.obj.obj.SelecteDivision))
                {
                    string[] res = _objBreadScrumbJobLocation.obj.obj.SelecteDivision.Split(new char[] { ';' });
                    _objBreadScrumbJobLocation.obj.obj.objDivision = new BEClient.DivisionMaster();
                    _objBreadScrumbJobLocation.obj.obj.objDivision.DivisionId = res.ToArray();
                }
                ViewBag.FormName = STR_JOBLOCATION_FORMNAME;
                jobLocation.RecordPermissionType = (List<ATS.BusinessEntity.ATSPermissionType>)TempData["RecordPermissionType"];
                jobLocation.obj.RecordExistsCount = (List<ATS.BusinessEntity.RecordExists>)TempData["RecordExistCount"];
                jobLocation.obj.PermissionType = (List<ATS.BusinessEntity.ATSPermissionType>)TempData["PermissionType"];
                jobLocation.RemoveBtnText = _JobLocationList.RemoveBtnText;
                jobLocation.SaveBtnText = _JobLocationList.SaveBtnText;

                String defaultname = jobLocation.obj.DefaultValue;
                //Change null value with default one
                (from trade in jobLocation.obj.JobLocatoinEntityLanguage
                 where String.IsNullOrEmpty(trade.LocalName)
                 select trade).ToList().ForEach(trade => trade.LocalName = defaultname);

                bool isSaved = _objJobLocationAction.UpdateJobLocation(jobLocation.obj);

                JsonModels actionStatus = null;
                if (isSaved)
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
                    actionStatus = base.GetJsonContent(true, string.Empty, "Not Able To Add Record");
                }
                /*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = ATSHelper.Encryption.EncryptionAlgo.EncryptionData(jsonModels);

                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                /*Redirect to List Page*/
                return RedirectToAction(STR_JOBLOCATION_LIST_METHOD, new { KeyMsg = keyMsg });

            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);

                /*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = ATSHelper.Encryption.EncryptionAlgo.EncryptionData(jsonModels);

                /*Redirect to List Page*/
                //return RedirectToAction(STR_JOBLOCATION_LIST_METHOD, new { KeyMsg = keyMsg });
                TempData["RecordPermissionType"] = jobLocation.RecordPermissionType;
                TempData["RecordExistCount"] = jobLocation.obj.RecordExistsCount;
                TempData["PermissionType"] = jobLocation.obj.PermissionType;
                BEClient.PageMode objPageMode = BEClient.PageMode.Update;
                ViewBag.PageMode = objPageMode;

                ViewBag.IsError = true;
                ViewBag.Message = ex.Message;

                FillDropDown(ATS.WebUi.Common.CommonFunctions.GetPageMode(jobLocation.RecordPermissionType, objPageMode).ToString(), null);
                if (jobLocation.obj.PermissionType.Contains(BEClient.ATSPermissionType.Delete))
                {
                    ViewBag.IsDelete = true;
                }

                _objBreadScrumbJobLocation.obj = jobLocation;
                SetBreadScrumb(ref _objBreadScrumbJobLocation);

                return View(ATS.WebUi.Common.Constants.STR_VIEW_METHOD, _objBreadScrumbJobLocation);
            }
        }

        public ActionResult View(Guid? id, string ordinal)
        {
            RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.JobLocation>> _objBreadScrumbJobLocation = new BreadScrumbModel<BESrp.DynamicSRP<BEClient.JobLocation>>();
            bool isEdit = false;
            BEClient.PageMode objPageMode = BEClient.PageMode.View;
            if (id != null && !id.Equals(Guid.Empty))
            {
                isEdit = true;
                ViewBag.IsEdit = 1;
            }
            try
            {
                _objJobLocation = new BESrp.DynamicSRP<BEClient.JobLocation>();
                _objJobLocation.obj = new BEClient.JobLocation();
                SetBreadScrumb(ref _objBreadScrumbJobLocation);

                if (isEdit)
                {
                    CreateObjJobLocation((Guid)id);
                    List<BEClient.RecordExists> LstRecordExists = new List<BEClient.RecordExists>();
                    BLClient.RecordExistsAction _objRecordExists = new BLClient.RecordExistsAction(_CurrentClientId);
                    LstRecordExists = _objRecordExists.GetRecordsCountByLocation((Guid)id);

                    _objJobLocationAction = new BLClient.JobLocationAction(_CurrentClientId, true);
                    _objJobLocation.obj = _objJobLocationAction.GetRecordByRecordId((Guid)id, _CurrentLanguageId);
                    _objJobLocation.obj.RecordExistsCount = LstRecordExists;
                    foreach (BEMaster.ClientLanguage clientLanguage in ATSCommon.CurrentSession.Instance.VerifiedClient.ClientLanguageList.OrderBy(r => r.objLanguage.LanguageName))
                    {
                        if (_objJobLocation.obj.JobLocatoinEntityLanguage.Where(x => x.LanguageId == clientLanguage.LanguageId).Count() <= 0)
                        {
                            BEClient.EntityLanguage entitylanguage = new BEClient.EntityLanguage();
                            entitylanguage.LanguageId = clientLanguage.LanguageId;
                            _objJobLocation.obj.JobLocatoinEntityLanguage.Add(entitylanguage);
                        }
                    }

                    ViewBag.RedirectAction = "Edit";
                    objPageMode = BEClient.PageMode.Update;
                    //FillDropDown(ATS.WebUi.Common.CommonFunctions.GetPageMode(_objJobLocation.RecordPermissionType, objPageMode).ToString(), _objJobLocation.obj.DivisionId);

                    List<BEClient.User> listOfEmployee = new List<BEClient.User>();
                    BLClient.UserAction objUserAction = new BLClient.UserAction(_CurrentClientId);
                    listOfEmployee = objUserAction.GetAllEmployees();
                    ViewBag.ListOfOnBoardingManager = new SelectList(listOfEmployee, "UserId", "FullName");
                    ViewBag.ListOfLocationManagers = new SelectList(listOfEmployee, "UserId", "FullName");

                    if (_objJobLocation.obj.PermissionType == null)
                    {
                        return RedirectToAction(ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_CONTROLLER, new { KeyMsg = ATSCommon.CommonFunctions.NonAuthoriseduser() });
                    }

                    if (_objJobLocation.obj.PermissionType.Contains(BEClient.ATSPermissionType.Delete))
                    {
                        ViewBag.IsDelete = true;
                    }
                    _objBreadScrumbJobLocation.obj = _objJobLocation;
                    NavView(id, _objBreadScrumbJobLocation.ToolTip, ordinal);
                    //for Selected Division for Location
                    BLClient.DivisionAction objDivisionAction = new BLClient.DivisionAction(_CurrentClientId);
                    _objJobLocation.obj.SelectedDivisionList = objDivisionAction.GetDivisionByJobLocation((Guid)id, _CurrentLanguageId);
                    _objBreadScrumbJobLocation.ItemName = _objJobLocation.obj.DefaultValue;

                    #region RECENT VIEWED
                    BEClient.RecentlyViewed objRecentlView = new BEClient.RecentlyViewed();
                    BLClient.RecentlyViewedAction objRecentlyViewedAction = new BLClient.RecentlyViewedAction(_CurrentClientId);
                    objRecentlView.Category = BEClient.RecentlyViewedCategory.JobLocation.ToString();
                    objRecentlView.ViewItemId = (Guid)id;
                    objRecentlView.URL = HttpContext.Request.Url.ToString();

                    List<BEClient.RecentlyViewed> objList = objRecentlyViewedAction.GetRecentlyViewedApplication(objRecentlView);
                    _objBreadScrumbJobLocation.objRecentViewedList = new List<BEClient.RecentlyViewed>();
                    _objBreadScrumbJobLocation.objRecentViewedList = objList;
                    #endregion
                }
                else
                {
                    CreateObjJobLocation(null);

                    _objJobLocation.obj = new BEClient.JobLocation();
                    _objJobLocation.obj.RecordExistsCount = new List<BEClient.RecordExists>();
                    _objJobLocation.obj.ClientId = _CurrentClientId;
                    _objJobLocation.obj.JobLocatoinEntityLanguage = new List<BEClient.EntityLanguage>();
                    foreach (BEMaster.ClientLanguage clientLanguage in ATSCommon.CurrentSession.Instance.VerifiedClient.ClientLanguageList.OrderBy(r => r.objLanguage.LanguageName))
                    {
                        BEClient.EntityLanguage entitylanguage = new BEClient.EntityLanguage();
                        entitylanguage.LanguageId = clientLanguage.LanguageId;
                        _objJobLocation.obj.JobLocatoinEntityLanguage.Add(entitylanguage);
                    }
                    ViewBag.RedirectAction = "Create";
                    objPageMode = BEClient.PageMode.Create;
                    FillDropDown(ATS.WebUi.Common.CommonFunctions.GetPageMode(_objJobLocation.RecordPermissionType, objPageMode).ToString(), null);

                    _objJobLocation.obj.PermissionType = new List<BEClient.ATSPermissionType> { BEClient.ATSPermissionType.Create };
                    NavView(id, _objBreadScrumbJobLocation.DisplayName, ordinal);
                    _objBreadScrumbJobLocation.ItemName = "ADD JOB LOCATION";
                }
                ViewBag.PageMode = objPageMode;
                _objJobLocation.RecordPermissionType = _objJobLocation.obj.PermissionType;

                TempData["RecordPermissionType"] = _objJobLocation.RecordPermissionType;
                TempData["RecordExistCount"] = _objJobLocation.obj.RecordExistsCount;
                TempData["PermissionType"] = _objJobLocation.obj.PermissionType;

                //For Checkbox Division List

                _objJobLocation.obj.AvailableDivisionList = new List<BEClient.Division>();
                BLClient.DivisionAction ObjDivisionAction = new BLClient.DivisionAction(_CurrentClientId, true);
                _objJobLocation.obj.AvailableDivisionList = ObjDivisionAction.GetAllDivisionByClientAndUsersTree(_CurrentLanguageId);
                //_objJobLocation.obj.AvailableDivisionList = _objJobLocationAction.GetAllDivisionTreeByScope(_CurrentLanguageId, ATS.WebUi.Common.CommonFunctions.GetPageMode(_objJobLocation.RecordPermissionType, objPageMode).ToString(), null);
                // End Check List

                _objBreadScrumbJobLocation.obj = _objJobLocation;
                return View(_objBreadScrumbJobLocation);
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);

                ///*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = ATSHelper.Encryption.EncryptionAlgo.EncryptionData(jsonModels);

                ///*Redirect to List Page*/
                return RedirectToAction(STR_JOBLOCATION_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }

        #region Delete
        public ActionResult Delete(string id)
        {
            try
            {
                bool result = _objJobLocationAction.Delete(id);
                JsonModels actionStatus = null;
                if (result)
                {
                    actionStatus = base.GetJsonContent(false, string.Empty, "Record Deleted Successfully");
                }
                else
                {
                    actionStatus = base.GetJsonContent(true, string.Empty, "Not Able To Delete Record");
                }
                /*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = ATSHelper.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                try
                {
                    ATSCommon.CommonFunctions.SolrFullImport();
                }
                catch (Exception ex)
                {
                    log.Error("SOLR Employee FULL IMPORT (Vacancy)" + ex.Message);
                }
                /*Redirect to List Page*/
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                return RedirectToAction(STR_JOBLOCATION_LIST_METHOD, new { KeyMsg = keyMsg });

            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);

                /*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = ATSHelper.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                /*Redirect to List Page*/
                return RedirectToAction(STR_JOBLOCATION_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }

        public ActionResult DeleteMultiple(string id)
        {
            string Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                bool Result = _objJobLocationAction.Delete(id);
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

        private void FillDropDown(string callMethod, Guid? DivisionId)
        {
            //Fill dropdown of division based on LanguageId
            //ViewBag.ListOfDivision = ATSCommon.DropDownFunction.FillDropDownByEntityAndLanguage(BEClient.ATSPrivilage.Division,);

            List<BEClient.Division> lstDivision = new List<BEClient.Division>();
            BLClient.DivisionAction objDivisionAction = new BLClient.DivisionAction(_CurrentClientId, false);
            lstDivision = _objJobLocationAction.GetAllDivisionByScope(_CurrentLanguageId, callMethod);
            ViewBag.ListOfDivision = new SelectList(lstDivision, "DivisionId", "DivisionName");

            /*Commented by bhavsinh 
             Load all employee for location manager ddl and onboarding manager ddl
             
             */
            //List<BEClient.User> lstLocationManagers = new List<BEClient.User>();
            //BLClient.UserAction objUserAction = new BLClient.UserAction(_CurrentClientId);
            //lstLocationManagers = objUserAction.GetLocationManagers();
            //ViewBag.ListOfLocationManagers = new SelectList(lstLocationManagers, "UserId", "FullName");

            ////List<BEClient.OnBoardManagers> lstOnBoardManagers = new List<BEClient.OnBoardManagers>();
            ////BLClient.ClientEmployeesAction objOnBoardingmanagerAction = new BLClient.ClientEmployeesAction(_CurrentClientId);
            //List<BEClient.User> LstOnboardingUsers = new List<BEClient.User>();
            //if (DivisionId != null && DivisionId != Guid.Empty)
            //{
            //    BLClient.UserAction _objOnboardUserAction = new BLClient.UserAction(base.CurrentClient.ClientId);
            //    LstOnboardingUsers = _objOnboardUserAction.GetAllUOnboardingUser((Guid)DivisionId);
            //}

            //ViewBag.ListOfOnBoardingManager = new SelectList(LstOnboardingUsers, "UserId", "FullName");
            //ViewBag.ListOfLocationManagers = new SelectList(LstOnboardingUsers, "UserId", "FullName");

            List<BEClient.User> listOfEmployee = new List<BEClient.User>();
            BLClient.UserAction objUserAction = new BLClient.UserAction(_CurrentClientId);
            listOfEmployee = objUserAction.GetAllEmployees();

            ViewBag.ListOfOnBoardingManager = new SelectList(listOfEmployee, "UserId", "FullName");
            ViewBag.ListOfLocationManagers = new SelectList(listOfEmployee, "UserId", "FullName");
        }

        public JsonResult GetOnBoardingUsersByDivision(string divisionId)
        {
            try
            {
                BLClient.UserAction _objUserAction = new BLClient.UserAction(base.CurrentClient.ClientId);
                BEClient.User currentUser = ATSCommon.CurrentSession.Instance.VerifiedUser;
                List<BEClient.User> _OnboardingUsers = new List<BEClient.User>();

                if (!divisionId.Equals(string.Empty))
                {
                    _objUserAction = new BLClient.UserAction(_CurrentClientId);
                    _OnboardingUsers = _objUserAction.GetAllUOnboardingUser(new Guid(divisionId));
                }
                return GetJson(new { OnboardManagerId = _OnboardingUsers.Select(r => r.UserId), FullNameUsers = _OnboardingUsers.Select(r => r.FullName) });
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region CreateURL
        public string RemoveJobLocationURL(Guid jobLocationId)
        {
            UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);
            return u.Action("Remove", "JobLocation", new { id = jobLocationId.ToString() });
        }
        public string AddJobLocationURL()
        {
            UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);
            return u.Action("View", "JobLocation");
        }
        #endregion

        #region Navigation Sections
        private void SetBreadScrumb(ref RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.JobLocation>> _objBreadScrumbLocation)
        {
            _objBreadScrumbLocation.DisplayName = STR_JOBLOCATION_FORMNAME;
            _objBreadScrumbLocation.ImagePath = BECommon.ImagePaths.VacancyImage;
            _objBreadScrumbLocation.ToolTip = _objBreadScrumbLocation.DisplayName;
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
            objBreadCrumb.Controller = "JobLocation";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_EMPLOYEE;
            objBreadCrumb.ordinal = objBreadCrumblst.Count() + 1;
            objBreadCrumb.URL = Url.Action("Index", "JobLocation", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, ordinal = objBreadCrumb.ordinal });
            objBreadCrumb.ImagePath = BECommon.ImagePaths.VacancyImage;
            objBreadCrumb.ToolTip = "JobLocation";
            objBreadCrumb.WithoutOrdinalURL = Url.Action("Index", "JobLocation", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });

            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);

            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
        }


        private void NavView(Guid? JobLocationId, String DisplayToolTip, String ordinal)
        {

            ATSCommon.CommonFunctions.SetNullNav(this.ControllerContext.RequestContext);
            List<BEClient.BreadCrumb> objBreadCrumblst = ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs;
            if (ordinal != null && !String.IsNullOrEmpty(ordinal.ToString()))
            {
                objBreadCrumblst.RemoveAll(x => x.ordinal >= Convert.ToInt32(ordinal.ToString()));
            }
            BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
            objBreadCrumb.Action = "View";
            objBreadCrumb.Controller = "JobLocation";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_EMPLOYEE;
            objBreadCrumb.ordinal = objBreadCrumblst.Count() + 1;
            objBreadCrumb.URL = Url.Action("View", "DivisionId", new { id = JobLocationId, area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, ordinal = objBreadCrumb.ordinal });
            objBreadCrumb.ImagePath = BECommon.ImagePaths.VacancyImage;
            objBreadCrumb.ToolTip = "JobLocation </br>" + DisplayToolTip;
            objBreadCrumb.WithoutOrdinalURL = Url.Action("View", "JobLocation", new { id = JobLocationId, area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });

            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);

            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;

        }

        #endregion
    }
}
