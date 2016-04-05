using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEClient = ATS.BusinessEntity;
using BLClient = ATS.BusinessLogic;
using BEMaster = ATS.BusinessEntity.Master;
using BLMaster = ATS.BusinessLogic.Master;
using ATSCommon = ATS.WebUi.Common;
using ATS.WebUi.Models;
using Newtonsoft.Json;

namespace ATS.WebUi.Areas.Employee.Controllers
{
    public class PortalContentController : ATS.WebUi.Controllers.AreaBaseController
    {
        #region Private members
        private BLClient.PortalContentAction _objPortalContentAction;
        private List<BEClient.PortalContent> _PortalContentList;
        private Guid _CurrentClientId;
        private Guid _CurrentLanguageId;

        #endregion

        #region Redirection Method
        private string STR_PORTALCONTENT_LIST_METHOD = "Index";
        private string STR_PORTALCONTENT_CREATE_METHOD = "Create";
        private string STR_PORTALCONTENT_EDIT_METHOD = "Edit";
        private string STR_PORTALCONTENT_VIEW_METHOD = "View";
        

        #endregion
        #region OnAuthorization Method call on every request
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (base.CurrentClient != null && Common.CurrentSession.Instance.VerifiedUser != null)
            {
                _CurrentClientId = base.CurrentClient.ClientId;
                _CurrentLanguageId = base.CurrentClient.CurrentLanguageId;
                _objPortalContentAction = new BLClient.PortalContentAction(_CurrentClientId);
            }

            
        }
        #endregion

        public ActionResult Index(string KeyMsg)
        {
            ViewBag.FormName = "Portal Content";
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

                _PortalContentList = _objPortalContentAction.GetAllPortalContent();
                if (_PortalContentList != null && _PortalContentList.Count > 0)
                    return View(_PortalContentList);
                else
                    return RedirectToAction(STR_PORTALCONTENT_VIEW_METHOD);
            }
            catch
            {
                throw;
            }
        }
        #region Add Position type
        [HttpPost]
        public ActionResult Create(BEClient.PortalContent objPortalContent)
        {
            try
            {
                objPortalContent.ClientId = _CurrentClientId;


                Guid newPositionTypeIdId = _objPortalContentAction.AddPortalContent(objPortalContent);
                JsonModels actionStatus = null;
                if (newPositionTypeIdId != null && !newPositionTypeIdId.Equals(Guid.Empty))
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
                /*Redirect to List Page*/
                return RedirectToAction(STR_PORTALCONTENT_LIST_METHOD, new { KeyMsg = keyMsg });
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);

                ///*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);

                /*Redirect to List Page*/
                return RedirectToAction(STR_PORTALCONTENT_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }
        #endregion

        #region Edit Position Type
        [HttpPost]
        public ActionResult Edit(BEClient.PortalContent objPortalContent)
        {
            try
            {
                ViewBag.FormName = "Portal Content";
                bool isRecordUpdated = _objPortalContentAction.UpdatePortalContent(objPortalContent);
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
                return RedirectToAction(STR_PORTALCONTENT_LIST_METHOD, new { KeyMsg = keyMsg });
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);

                ///*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);

                ///*Redirect to List Page*/
                return RedirectToAction(STR_PORTALCONTENT_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }
        #endregion

        public ActionResult View(Guid? id)
        {
            ViewBag.FormName = "Portal Content";
            bool isEdit = false;
            if (id != null && !id.Equals(Guid.Empty))
            {
                isEdit = true;
            }
            try
            {
                BEClient.PortalContent ObjPortalContent = new BEClient.PortalContent();
                if (isEdit)
                {
                    ViewBag.RedirectAction = "Edit";
                    ObjPortalContent = _objPortalContentAction.GetRecordByRecordId((Guid)id);
                }
                else
                {
                    ObjPortalContent.ClientId = _CurrentClientId;
                    ObjPortalContent.LanguageId = _CurrentLanguageId;
                    ViewBag.RedirectAction = "Create";
                }


                return View(ObjPortalContent);
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);

                ///*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);

                ///*Redirect to List Page*/
                return RedirectToAction(STR_PORTALCONTENT_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }

    }
}
