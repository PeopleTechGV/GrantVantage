using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEClient = ATS.BusinessEntity;
using BLClient = ATS.BusinessLogic;
using ATSCommon = ATS.WebUi.Common;
using BESrp = ATS.BusinessEntity.SRPEntity;
using ATSModel = ATS.BusinessEntity.UserDivisionSecurityRole;
using ATS.WebUi.Models;
using Newtonsoft.Json;
using BECommon = ATS.BusinessEntity.Common;
using RootModels = ATS.WebUi.Models;
namespace ATS.WebUi.Areas.Employee.Controllers
{
    public class CompanySetupController : ATS.WebUi.Controllers.AreaBaseController
    {
        // GET: /Candidate/CompanySetup/
        #region Private Members
        private BLClient.ProfileAction _objProfileAction;
        private Guid _CurrentClientId;
        private Guid _CurrentLanguageId;
        private Guid _UserId;
        private BESrp.DynamicSRP<ATSModel> _objATSModel;
        BLClient.ATSSecurityRoleAction objATSSecurityRoleAction;
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
                _UserId = ATSCommon.CurrentSession.Instance.UserId;
                _CurrentClientId = base.CurrentClient.ClientId;
                _CurrentLanguageId = base.CurrentClient.CurrentLanguageId;
                objATSSecurityRoleAction = new BLClient.ATSSecurityRoleAction(_CurrentClientId, true);
                if (ATSCommon.CurrentSession.Instance.VerifiedUser.SecurityRoles.Where(x => x.AtsSecurityRole == BEClient.ATSSecurityRole.Candidate).Count() > 0)
                {
                    filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_CANDIDATE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_CANDIDATE_CONTROLLER, new { area = ATSCommon.Constants.AREA_CANDIDATE }));
                }
                _objATSModel = new BESrp.DynamicSRP<ATSModel>();
                _objATSModel.AddBtnText = "Add";
                _objATSModel.EditBtnText = "Edit Privilege";
                _objATSModel.RemoveBtnText = "Remove Privilege";
                _objATSModel.SaveBtnText = "Save";
                _objATSModel.UserPermissionWithScope = objATSSecurityRoleAction.ListUserPermissionWithScope;
            }
        }
        #endregion

        #region Navigation
        private void NavIndex(Guid? userId)
        {
            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = null;
            List<BEClient.BreadCrumb> objBreadCrumblst = new List<BEClient.BreadCrumb>();
            BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
            objBreadCrumb.Action = "Index";
            objBreadCrumb.Controller = "CompanySetup";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_EMPLOYEE;
            objBreadCrumb.ordinal = 1;
            objBreadCrumb.URL = Url.Action("Index", "CompanySetup", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, @userId = userId });
            objBreadCrumb.ImagePath = BECommon.ImagePaths.CompanySetupImage;
            objBreadCrumb.ToolTip = "Company Setup"; 
            objBreadCrumb.WithoutOrdinalURL = Url.Action("Index", "CompanySetup", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });
            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);
            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
        }
        #endregion
        private void Dropdownlist()
        {
            try
            {
                ATSModel objmodel = new ATSModel();
                BLClient.UserAction objUserAction = new BLClient.UserAction(_CurrentClientId, true);
                objmodel.objUserList = objUserAction.GetAllUser();
                BEClient.User objUser = new BEClient.User();
                objUser.ObjUserDetails = new BEClient.UserDetails();
                objUser.ObjUserDetails.FirstName = "-----SELECT-----";
                objmodel.objUserList.Insert(0, objUser);
                _objATSModel.obj = objmodel;
                ViewBag.UserList = new SelectList(_objATSModel.obj.objUserList, "UserId", "ObjUserDetails.FirstName");
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Partial View
        public PartialViewResult AddUserList()
        {
            return PartialView("_UserList", new BESrp.DynamicSRP<ATSModel>());
        }
        #endregion

        public ActionResult Index(string KeyMsg)
        {
            try
            {
                NavIndex(Common.CurrentSession.Instance.UserId);
                return View("CSMenu");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult Index1(string KeyMsg, Guid? userId = null)
        {
            JsonModels resultJsonModel = null;
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
                ATSModel objmodel = new ATSModel();
                //Changed for temp : TODOCHECK
                int securityRoleSqe = 0;
                //int securityRoleSqe = (int)ATSCommon.CurrentSession.Instance.VerifiedUser.SecurityRoles.Select(r => r.AtsSecurityRole).First();
                //if (securityRoleSqe >= (int)BEClient.ATSSecurityRole.SetupManager)
                //{
                //    ViewBag.IsReadOnly = 1;
                //}
                //else
                //{
                //    ViewBag.IsReadOnly = 0;
                //}
                objmodel.AvailableSecurityRoleList = objATSSecurityRoleAction.GetAllATSSecurityRoleByClientAndLanguage(_CurrentLanguageId, securityRoleSqe);
                if (userId == null || userId.Equals(Guid.Empty))
                {
                    objmodel.SelectedSecurityRoleList = new List<BEClient.ATSSecurityRolePrivilages>();
                }
                else
                {
                    objmodel.UserId = (Guid)userId;
                    objmodel.SelectedSecurityRoleList = objATSSecurityRoleAction.GetSecurityRoleByUserAndClient((Guid)userId, _CurrentLanguageId);
                }
                objmodel.objPrevilegList = objATSSecurityRoleAction.GetAllATSPrevilegeByClientAndLanguage(_CurrentLanguageId);
                BLClient.UserDetailsAction objUserDetailsAction = new BLClient.UserDetailsAction(_CurrentClientId);
                objmodel.objUserDetail = objUserDetailsAction.GetUserDetailsByUserId(objmodel.UserId);
                Dropdownlist();
                _objATSModel.obj = objmodel;
                ViewBag.IsHorizontal = 0;
                NavIndex(userId);
                RootModels.BreadScrumbModel<BESrp.DynamicSRP<ATSModel>> _bsModel = new BreadScrumbModel<BESrp.DynamicSRP<ATSModel>>();
                _bsModel.obj = _objATSModel;
                _bsModel.DisplayName = BEClient.Common.CompanySetupMenu.CSMNU_USERPRIVILEGES;
                _bsModel.ToolTip = "Company Setup";
                return View(_bsModel);
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

        public ActionResult CompanySetUpMaster()
        {
            return View("CompanySetUp");
        }

        [HttpPost]
        public ActionResult Save(RootModels.BreadScrumbModel<BESrp.DynamicSRP<ATSModel>> objBreadScrumATSModel, FormCollection frm)
        {
            BESrp.DynamicSRP<ATSModel> objATSModel = objBreadScrumATSModel.obj;
            try
            {
                string keyMsg = string.Empty;
                if (frm.AllKeys.Contains("btn_Save"))
                {
                    List<BEClient.PrivilegeAndPermissionAndScope> objGroupListOfSameRemarkQty = (List<BEClient.PrivilegeAndPermissionAndScope>)Newtonsoft.Json.JsonConvert.DeserializeObject(objATSModel.obj.AdHocPrivilegeList, typeof(List<BEClient.PrivilegeAndPermissionAndScope>));
                    BLClient.UserSecurityRoleAction objUserSecurityRole = new BLClient.UserSecurityRoleAction(_CurrentClientId);
                    Guid insertedId = Guid.Empty;
                    insertedId = objUserSecurityRole.InsertSecurityRoleByUser(objATSModel.obj.objATSSecurityRolePrivilagesMaster.SRP_Ids, objGroupListOfSameRemarkQty, objATSModel.obj.UserId, objATSModel.obj.objUserDetail);
                    JsonModels actionStatus = null;
                    if (insertedId != null && !insertedId.Equals(Guid.Empty))
                    {
                        actionStatus = base.GetJsonContent(false, string.Empty, "Record Added Successfully");
                    }
                    else
                    {
                        actionStatus = base.GetJsonContent(true, string.Empty, "Not Able To Add Record");
                    }
                    /*Convert to json string*/
                    string jsonModels = JsonConvert.SerializeObject(actionStatus);
                    keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                }
                return RedirectToAction("Index", new { userId = objATSModel.obj.UserId, KeyMsg = keyMsg });
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                /*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                /*Redirect to List Page*/
                return RedirectToAction("Index", new { KeyMsg = keyMsg });
            }

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

        public ActionResult AddUser()
        {
            return RedirectToAction("View", "User");
        }
    }
}