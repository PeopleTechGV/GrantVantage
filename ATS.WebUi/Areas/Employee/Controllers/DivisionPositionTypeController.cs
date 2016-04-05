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

namespace ATS.WebUi.Areas.Employee.Controllers
{
    public class DivisionPositionTypeController : ATS.WebUi.Controllers.AreaBaseController
    {
        
        // GET: /Employee/DivisionPositionType/
        #region Private Members
        private BLClient.DivisionPositionTypeAction _objDivisionPositionTypeAction;
        private string STR_DIVISION_POSITIONTYPE = BEClient.Common.CompanySetupMenu.CSMNU_DIVISION_POSITIONTYPE;
        private BESrp.DynamicSRP<List<BEClient.DivisionPositionType>> _objDivisionPositionTypeList;
        private BESrp.DynamicSRP<BEClient.DivisionPositionType> _objDivisionpositionType;
        private Guid _CurrentClientId;
        private Guid _CurrentLanguageId;
        #endregion

        #region Redirection Method
        private string STR_DIVISIONPOSITIONTYPE_LIST_METHOD = "Index";
        private string STR_DIVISIONPOSITIONTYPE_CREATE_METHOD = "Create";
        private string STR_DIVISIONPOSITIONTYPE_EDIT_METHOD = "Edit";
        private string STR_DIVISIONPOSITIONTYPE_VIEW_METHOD = "View";
        #endregion

        #region Initialize Method call on every request
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (base.CurrentClient != null && Common.CurrentSession.Instance.VerifiedUser != null)
            {
                _CurrentClientId = base.CurrentClient.ClientId;
                _CurrentLanguageId = base.CurrentClient.CurrentLanguageId;
                _objDivisionPositionTypeAction = new BLClient.DivisionPositionTypeAction(base.CurrentClient.ClientId, true);
                _objDivisionPositionTypeList = new BESrp.DynamicSRP<List<BEClient.DivisionPositionType>>();
                _objDivisionPositionTypeList.AddBtnText = "Add";
                _objDivisionPositionTypeList.EditBtnText = "Edit ";
                _objDivisionPositionTypeList.RemoveBtnText = "Remove DivisionPositionType";
                _objDivisionPositionTypeList.SaveBtnText = "Save";
                _objDivisionPositionTypeList.UserPermissionWithScope = _objDivisionPositionTypeAction.ListUserPermissionWithScope;
            }
        }
        #endregion

        private void CreateObjDivisionPositionType(Guid? DivisionPositionTypeId)
        {
            _objDivisionpositionType = new BESrp.DynamicSRP<BEClient.DivisionPositionType>();
            _objDivisionpositionType.AddBtnText = _objDivisionPositionTypeList.AddBtnText;
            _objDivisionpositionType.EditBtnText = _objDivisionPositionTypeList.EditBtnText;
            _objDivisionpositionType.RemoveBtnText = _objDivisionPositionTypeList.RemoveBtnText;
            _objDivisionpositionType.SaveBtnText = _objDivisionPositionTypeList.SaveBtnText;
            _objDivisionpositionType.UserPermissionWithScope = _objDivisionPositionTypeAction.ListUserPermissionWithScope;
        }

        public ActionResult Index(string KeyMsg)
        {
            JsonModels resultJsonModel = null;
            ViewBag.FormName = STR_DIVISION_POSITIONTYPE;
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
                _objDivisionPositionTypeList.obj = _objDivisionPositionTypeAction.GetAllDivisionPositiontype(base.CurrentClient.CurrentLanguageId, _objDivisionPositionTypeList.UserPermissionWithScope, BEClient.ATSPermissionType.View);
                _objDivisionPositionTypeList.AddRecordURL = AddDivisionPositionTypeURL();
                ViewBag.PageMode = BEClient.PageMode.View;
                if (_objDivisionPositionTypeList != null && _objDivisionPositionTypeList.obj.Count() > 0)
                    return View(_objDivisionPositionTypeList);
                else
                    return RedirectToAction(STR_DIVISIONPOSITIONTYPE_VIEW_METHOD);
            }
            catch
            {
                throw;
            }
        }
        #region Edit DivisionPositionType
        [HttpPost]
        public ActionResult Edit(BESrp.DynamicSRP<BEClient.DivisionPositionType> objDivisionPositionType)
        {
            try
            {
                ViewBag.FormName = "My DivisionPositiontype";
                bool isRecordUpdated = _objDivisionPositionTypeAction.UpdateDivisionPositionType(objDivisionPositionType.obj);
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
                /*Redirect to List Page*/
                return RedirectToAction(STR_DIVISIONPOSITIONTYPE_LIST_METHOD, new { KeyMsg = keyMsg });
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                /*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                /*Redirect to List Page*/
                return RedirectToAction(STR_DIVISIONPOSITIONTYPE_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }
        #endregion

        #region Add Division
        [HttpPost]
        public ActionResult Create(BESrp.DynamicSRP<BEClient.DivisionPositionType> objDivisionPositionType)
        {
            try
            {
                objDivisionPositionType.obj.ClientId = _CurrentClientId;
                Guid newDivisionPositionTypeId = _objDivisionPositionTypeAction.AddDivisionPositionType(objDivisionPositionType.obj);
                JsonModels actionStatus = null;
                if (newDivisionPositionTypeId != null && !newDivisionPositionTypeId.Equals(Guid.Empty))
                {
                    actionStatus = base.GetJsonContent(false, string.Empty, "Record Added Successfully");
                }
                else
                {
                    actionStatus = base.GetJsonContent(true, string.Empty, "Not Able To Add Record");
                }
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                return RedirectToAction(STR_DIVISIONPOSITIONTYPE_LIST_METHOD, new { KeyMsg = keyMsg });
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                return RedirectToAction(STR_DIVISIONPOSITIONTYPE_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }
        #endregion

        #region Division By Id
        public ActionResult View(Guid? id)
        {
            ViewBag.FormName = STR_DIVISION_POSITIONTYPE;
            bool isEdit = false;
            BEClient.PageMode objPageMode = BEClient.PageMode.View;
            if (id != null && !id.Equals(Guid.Empty))
            {
                isEdit = true;
            }
            try
            {
                if (isEdit)
                {
                    CreateObjDivisionPositionType((Guid)id);
                    ViewBag.RedirectAction = "Edit";
                    objPageMode = BEClient.PageMode.Update;
                    _objDivisionpositionType.obj = _objDivisionPositionTypeAction.GetDivisionPositionType((Guid)id,base.CurrentClient.CurrentLanguageId);
                }
                else
                {
                    CreateObjDivisionPositionType(null);
                    _objDivisionpositionType.obj = new BEClient.DivisionPositionType();
                    _objDivisionpositionType.obj.ClientId = base.CurrentClient.ClientId;
                    ViewBag.RedirectAction = "Create";
                    objPageMode = BEClient.PageMode.Create;
                    _objDivisionpositionType.obj.PermissionType = new List<BEClient.ATSPermissionType> { BEClient.ATSPermissionType.Create };
                }
                ViewBag.PageMode = objPageMode;
                _objDivisionpositionType.RecordPermissionType = _objDivisionpositionType.obj.PermissionType;
                DropDownList(ATS.WebUi.Common.CommonFunctions.GetPageMode(_objDivisionpositionType.RecordPermissionType, objPageMode).ToString());
                return View(_objDivisionpositionType);
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                return RedirectToAction(STR_DIVISIONPOSITIONTYPE_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }
        #endregion

        #region CreateURL
        public string RemoveDivisionPositionTypeURL(Guid divisionPositionTypeId)
        {
            UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);
            return u.Action("Remove", "DivisionPositionType", new { id = divisionPositionTypeId.ToString() });
        }
        public string AddDivisionPositionTypeURL()
        {
            UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);
            return u.Action("View", "DivisionPositionType");
        }
        #endregion

        #region Bind Dropdown
        public void DropDownList(string callMethod)
        {
            try
            {
                    Guid ClientId = base.CurrentClient.ClientId;
                    List<BEClient.Division> _objDivisionList = new List<BEClient.Division>();
                    _objDivisionList = _objDivisionPositionTypeAction.GetAllDivisionByScope(base.CurrentClient.CurrentLanguageId, callMethod);
                    ViewBag.DivisionLst = new SelectList(_objDivisionList, "DivisionId", "DivisionName");
                    BLClient.PositionTypeAction _objPositionTypeAction = new BLClient.PositionTypeAction(ClientId);
                    List<BEClient.PositionType> _objPositionTypeList = new List<BEClient.PositionType>();
                    _objPositionTypeList = _objPositionTypeAction.GetAllPositionTypeByClientAndLanguage(base.CurrentClient.CurrentLanguageId);
                    ViewBag.PositionTypeLst = new SelectList(_objPositionTypeList, "PositionTypeId", "LocalName");
             }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
