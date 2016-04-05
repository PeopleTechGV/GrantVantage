using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEClient = ATS.BusinessEntity;
using BLClient = ATS.BusinessLogic;
using log4net;
using BECommon = ATS.BusinessEntity.Common;
using ATSCommon = ATS.WebUi.Common;
using RootModels = ATS.WebUi.Models;
using ATSHelper = ATS.HelperLibrary;
using Newtonsoft.Json;
using ATS.WebUi.Models;
using BESrp = ATS.BusinessEntity.SRPEntity;

namespace ATS.WebUi.Areas.Employee.Controllers
{
    public class OfferLettersController : ATS.WebUi.Controllers.AreaBaseController
    {
        #region Private Members
        private BLClient.OfferLetterAction _objOfferLetterAction;
        private Guid _CurrentClientId;
        private Guid _CurrentLanguageId;
        private BESrp.DynamicSRP<List<BEClient.OfferLetters>> _objOfferletterList;
        private BESrp.DynamicSRP<BEClient.OfferLetters> _objSRPOfferLetter;

        private static readonly log4net.ILog log = LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Redirection Method
        private string STR_OFFERLETTERS_LIST_METHOD = "Index";

        private string STR_OFFERLETTERS_CREATE_METHOD = "Create";
        private string STR_OFFERLETTERS_EDIT_METHOD = "Edit";
        private string STR_OFFERLETTERS_View_METHOD = "View";
        private string STR_FORMNAME = BECommon.CompanySetupMenu.CSMNU_OFFERLETTER;

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
                _objOfferLetterAction = new BLClient.OfferLetterAction(_CurrentClientId, true);
                _objSRPOfferLetter = new BESrp.DynamicSRP<BEClient.OfferLetters>();
                _objOfferletterList = new BESrp.DynamicSRP<List<BEClient.OfferLetters>>();
                _objOfferletterList.AddBtnText = BEClient.Common.CommonConstant.ADD;
                _objOfferletterList.EditBtnText = BEClient.Common.CommonConstant.UPDATE;
                _objOfferletterList.RemoveBtnText = BEClient.Common.CommonConstant.DELETE;
                _objOfferletterList.SaveBtnText = BEClient.Common.CommonConstant.SAVE;

                _objOfferletterList.UserPermissionWithScope = _objOfferLetterAction.ListUserPermissionWithScope;

                if (filterContext.ActionDescriptor.ActionName.ToLower() != "getofferlettercontent" &&
                    filterContext.ActionDescriptor.ActionName.ToLower() != "getimage")
                {
                    if (filterContext.ActionDescriptor.ActionName == "View" && filterContext.RouteData.Values.Keys.Count() == 2)
                    {
                        if (_objOfferletterList.UserPermissionWithScope.Where(x => x.PermissionType.Contains(BEClient.ATSPermissionType.Create)).Count() <= 0)
                        {
                            TempData["KeyMsg"] = ATSCommon.CommonFunctions.NonAuthoriseduser();
                            filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_CONTROLLER, new { @KeyMsg = ATSCommon.CommonFunctions.NonAuthoriseduser() }));
                        }
                    }
                    //else if (Common.CurrentSession.Instance.VerifiedUser.SecurityRoles.Where(x => x.AtsSecurityRole.Equals(BEClient.ATSSecurityRole.SystemAdministrator)).Count() <= 0)
                    //{
                    //    filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_CONTROLLER, new { @KeyMsg = ATSCommon.CommonFunctions.NonAuthoriseduser() }));
                    //}
                }
            }
        }
        #endregion

        public ActionResult Index(string KeyMsg, string ordinal)
        {
            RootModels.BreadScrumbModel<BESrp.DynamicSRP<List<BEClient.OfferLetters>>> _ObjOfferLetterBreadcrumb = new RootModels.BreadScrumbModel<BESrp.DynamicSRP<List<BEClient.OfferLetters>>>();
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
                BLClient.OfferLetterAction ObjOfferLetterAction = new BLClient.OfferLetterAction(_CurrentClientId, true);
                _objOfferletterList.obj = ObjOfferLetterAction.GetAllOfferLetters();
                _ObjOfferLetterBreadcrumb.obj = _objOfferletterList;
                _ObjOfferLetterBreadcrumb.obj.AddRecordURL = AddOfferLetterURL();
                _ObjOfferLetterBreadcrumb.DisplayName = STR_FORMNAME;
                _ObjOfferLetterBreadcrumb.ToolTip = STR_FORMNAME;
                NavIndex(ordinal);
                KeyMsg = null;

                return View(_ObjOfferLetterBreadcrumb);
            }
            catch
            {
                throw;
            }
        }

        public ActionResult View(Guid? id, string ordinal)
        {
            RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.OfferLetters>> _objBreadScrumbOfferLetters = new BreadScrumbModel<BESrp.DynamicSRP<BEClient.OfferLetters>>();

            bool isEdit = false;
            BEClient.PageMode objPageMode = BEClient.PageMode.View;

            if (id != null && !id.Equals(Guid.Empty))
            {
                isEdit = true;
                ViewBag.IsEdit = 1;
            }
            try
            {
                CreateObjOfferLetter();
                SetBreadScrumb(ref _objBreadScrumbOfferLetters);
                if (isEdit)
                {
                    ViewBag.RedirectAction = STR_OFFERLETTERS_EDIT_METHOD;
                    _objSRPOfferLetter.obj = _objOfferLetterAction.GetOfferLetterById((Guid)id);
                    ViewBag.IsDelete = false;
                    _objSRPOfferLetter.ActionName = "Edit";
                    ViewBag.IsDelete = true;
                    _objBreadScrumbOfferLetters.obj = _objSRPOfferLetter;
                    _objBreadScrumbOfferLetters.ItemName = _objSRPOfferLetter.obj.OfferLetterName;
                    _objBreadScrumbOfferLetters.obj.SaveBtnText = "Save";

                    #region RECENT VIEWED
                    BEClient.RecentlyViewed objRecentlView = new BEClient.RecentlyViewed();
                    BLClient.RecentlyViewedAction objRecentlyViewedAction = new BLClient.RecentlyViewedAction(_CurrentClientId);
                    objRecentlView.Category = BEClient.RecentlyViewedCategory.OfferLetter.ToString();
                    objRecentlView.ViewItemId = (Guid)id;
                    objRecentlView.URL = HttpContext.Request.Url.ToString();

                    List<BEClient.RecentlyViewed> objList = objRecentlyViewedAction.GetRecentlyViewedApplication(objRecentlView);
                    _objBreadScrumbOfferLetters.objRecentViewedList = new List<BEClient.RecentlyViewed>();
                    _objBreadScrumbOfferLetters.objRecentViewedList = objList;
                    #endregion
                }
                else
                {
                    ViewBag.RedirectAction = STR_OFFERLETTERS_CREATE_METHOD;
                    _objBreadScrumbOfferLetters.obj = _objSRPOfferLetter;
                    _objBreadScrumbOfferLetters.obj.obj = new BEClient.OfferLetters();
                    _objBreadScrumbOfferLetters.ItemName = "ADD AWARD LETTER";
                    NavView(id, _objBreadScrumbOfferLetters.DisplayName, ordinal);
                }
                DropDownList();
                return View(_objBreadScrumbOfferLetters);
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);

                ///*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = ATSHelper.Encryption.EncryptionAlgo.EncryptionData(jsonModels);

                ///*Redirect to List Page*/
                return RedirectToAction(STR_OFFERLETTERS_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }

        #region Delete Offer Letter

        public ActionResult Delete(string id)
        {
            try
            {
                bool Result = _objOfferLetterAction.DeleteOfferLeter(id);
                JsonModels actionStatus = null;
                if (Result)
                {
                    actionStatus = base.GetJsonContent(false, string.Empty, "Record Deleted Successfully");
                    //Update Cache
                    BusinessLogic.Common.CacheHelper.GetDegreeType(_CurrentClientId, _CurrentLanguageId, CurrentClient.Clientname, true);
                }
                else
                {
                    actionStatus = base.GetJsonContent(true, string.Empty, "Not Able To Delete Record");
                }

                /*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                /*Redirect to List Page*/
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                return RedirectToAction(STR_OFFERLETTERS_LIST_METHOD, new { KeyMsg = keyMsg });
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);

                /*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);

                /*Redirect to List Page*/
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                return RedirectToAction(STR_OFFERLETTERS_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }

        public ActionResult DeleteMultiple(string id)
        {
            string Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                bool Result = _objOfferLetterAction.DeleteOfferLeter(id);
                if (Result)
                {
                    IsError = false;
                    Message = "Record Deleted Successfully";
                    BusinessLogic.Common.CacheHelper.GetDegreeType(_CurrentClientId, _CurrentLanguageId, CurrentClient.Clientname, true);
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

        #region Add Offerletter
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.OfferLetters>> objBreadScrumbOfferletter)
        {
            BESrp.DynamicSRP<BEClient.OfferLetters> objOfferLetters = objBreadScrumbOfferletter.obj;
            try
            {

                String Message = String.Empty;
                Guid newOfferLetterId = _objOfferLetterAction.InsertOfferletter(objOfferLetters.obj);
                JsonModels actionStatus = null;
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                if (newOfferLetterId != null && !newOfferLetterId.Equals(Guid.Empty))
                {
                    actionStatus = base.GetJsonContent(false, string.Empty, "Record Added Successfully");
                }
                else
                {
                    actionStatus = base.GetJsonContent(true, string.Empty, "Not Able To Add Record");
                }
                /*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);

                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                /*Redirect to List Page*/
                return RedirectToAction(STR_OFFERLETTERS_LIST_METHOD, new { KeyMsg = keyMsg });
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                /*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                BEClient.PageMode objPageMode = BEClient.PageMode.Create;
                ViewBag.PageMode = objPageMode;
                ViewBag.IsError = true;
                ViewBag.Message = ex.Message;
                CreateObjOfferLetter(objBreadScrumbOfferletter.obj);
                SetBreadScrumb(ref objBreadScrumbOfferletter);
                return View(ATS.WebUi.Common.Constants.STR_VIEW_METHOD, objBreadScrumbOfferletter);
            }
        }
        #endregion

        #region Edit Offer Letter
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.OfferLetters>> objBreadScrumbOfferLetter)
        {
            BESrp.DynamicSRP<BEClient.OfferLetters> objOfferLetter = objBreadScrumbOfferLetter.obj;
            CreateObjOfferLetter(objOfferLetter);
            try
            {
                String Message = String.Empty;
                bool isRecordUpdated = _objOfferLetterAction.UpdateOfferletter(objOfferLetter.obj);
                JsonModels actionStatus = null;
                if (isRecordUpdated)
                {
                    actionStatus = base.GetJsonContent(!isRecordUpdated, string.Empty, "Record Updated Successfully");
                }
                else
                {
                    actionStatus = base.GetJsonContent(!isRecordUpdated, string.Empty, "Not Able To Update Record");
                }
                /*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAt(ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Count() - 1);
                /*Redirect to List Page*/

                return RedirectToAction(STR_OFFERLETTERS_LIST_METHOD, new { KeyMsg = keyMsg });
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
                SetBreadScrumb(ref objBreadScrumbOfferLetter);
                return View(ATS.WebUi.Common.Constants.STR_VIEW_METHOD, objBreadScrumbOfferLetter);
            }
        }
        #endregion


        private void CreateObjOfferLetter()
        {
            _objSRPOfferLetter = new BESrp.DynamicSRP<BEClient.OfferLetters>();
            _objSRPOfferLetter.AddBtnText = _objOfferletterList.AddBtnText;
            _objSRPOfferLetter.EditBtnText = _objOfferletterList.EditBtnText;
            _objSRPOfferLetter.RemoveBtnText = _objOfferletterList.RemoveBtnText;
            _objSRPOfferLetter.SaveBtnText = _objOfferletterList.SaveBtnText;
        }
        private void CreateObjOfferLetter(BESrp.DynamicSRP<BEClient.OfferLetters> ObjSRPOfferletter)
        {


            ObjSRPOfferletter.AddBtnText = _objOfferletterList.AddBtnText;
            ObjSRPOfferletter.EditBtnText = _objOfferletterList.EditBtnText;
            ObjSRPOfferletter.RemoveBtnText = _objOfferletterList.RemoveBtnText;
            ObjSRPOfferletter.SaveBtnText = _objOfferletterList.SaveBtnText;

        }
        private void NavView(Guid? OfferLetterId, String DisplayToolTip, String ordinal)
        {
            ATSCommon.CommonFunctions.SetNullNav(this.ControllerContext.RequestContext);
            List<BEClient.BreadCrumb> objBreadCrumblst = ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs;
            if (ordinal != null && !String.IsNullOrEmpty(ordinal.ToString()))
            {
                objBreadCrumblst.RemoveAll(x => x.ordinal >= Convert.ToInt32(ordinal.ToString()));
            }
            BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
            objBreadCrumb.Action = "View";
            objBreadCrumb.Controller = "OfferLetter";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_EMPLOYEE;
            objBreadCrumb.ordinal = objBreadCrumblst.Count() + 1;
            objBreadCrumb.URL = Url.Action("View", "OfferLetters", new { id = OfferLetterId, area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, ordinal = objBreadCrumb.ordinal });
            objBreadCrumb.ImagePath = BECommon.ImagePaths.VacancyImage;
            objBreadCrumb.ToolTip = "Award Letters </br>" + DisplayToolTip;
            objBreadCrumb.WithoutOrdinalURL = Url.Action("View", "OfferLetters", new { id = OfferLetterId, area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });

            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);

            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;

        }
        private void SetBreadScrumb(ref RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.OfferLetters>> _objBreadScrumbOfferLetters)
        {
            _objBreadScrumbOfferLetters.DisplayName = STR_FORMNAME;
            _objBreadScrumbOfferLetters.ImagePath = BECommon.ImagePaths.VacancyImage;
            _objBreadScrumbOfferLetters.ToolTip = _objBreadScrumbOfferLetters.DisplayName;
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
            objBreadCrumb.Controller = "OfferLetters";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_EMPLOYEE;
            objBreadCrumb.ordinal = objBreadCrumblst.Count() + 1;
            objBreadCrumb.URL = Url.Action("Index", "OfferLetters", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });
            objBreadCrumb.ImagePath = BECommon.ImagePaths.VacancyImage;
            objBreadCrumb.ToolTip = "Award Letters";
            objBreadCrumb.WithoutOrdinalURL = Url.Action("Index", "OfferLetters", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });

            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);

            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
        }
        #region CreateURL

        public string AddOfferLetterURL()
        {
            UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);
            return u.Action(STR_OFFERLETTERS_View_METHOD, "OfferLetters");
        }
        #endregion

        [HttpGet]
        public JsonResult GetOfferLetterContent(Guid OfferLetterId)
        {
            String Message = String.Empty;
            bool IsError = false;
            BEClient.OfferLetters objOfferLetter = new BEClient.OfferLetters();
            try
            {
                BLClient.OfferLetterAction objOfferLetterAction = new BLClient.OfferLetterAction(_CurrentClientId);
                objOfferLetter = objOfferLetterAction.GetOfferLetterById(OfferLetterId);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, objOfferLetter), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetImage()
        {
            string path = string.Empty;
            try
            {

                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase file = Request.Files[i];
                    int fileSize = file.ContentLength;
                    string fileName = DateTime.Now.Ticks.ToString() + file.FileName.Substring(file.FileName.IndexOf("."));
                    string mimeType = file.ContentType;
                    System.IO.Stream fileContent = file.InputStream;
                    file.SaveAs(Server.MapPath("~/UploadImages/") + fileName); //File 
                    //path = (System.Web.HttpContext.Current.Server.MapPath("~/UploadImages/") + fileName);
                    path = "http://" + System.Web.HttpContext.Current.Request.Url.Authority + "/UploadImages/" + fileName;
                }
                JsonModels objJson = new JsonModels();
                objJson.Data = path;
                return Json(path);
            }
            catch
            {
                throw;
            }

        }
        public void DropDownList()
        {
            try
            {
                BLClient.PositionTypeAction objpositiontypeAction = new BLClient.PositionTypeAction(_CurrentClientId);
                List<BEClient.PositionType> LstpositionType = new List<BEClient.PositionType>();
                LstpositionType = objpositiontypeAction.GetAllPositionTypeByClientAndLanguage(_CurrentLanguageId);
                ViewBag.LstPositionType = new SelectList(LstpositionType, "PositionTypeId", "LocalName");
            }
            catch
            {
                throw;
            }
        }
    }
}
