using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = ATS.BusinessEntity;
using BEMaster = ATS.BusinessEntity.Master;
using BLMaster = ATS.BusinessLogic.Master;
using BLCommon = ATS.BusinessLogic.Common;
using ATS.WebUi.Utility;
using System.Web.UI.WebControls;
using ATS.WebUi.Models;
using Newtonsoft.Json;
using ATSCommon = ATS.WebUi.Common;
using System.Data;
using System.Xml;
using ATS.BusinessLogic.Master;
using ATS.BusinessEntity;
using System.IO;
using System.Resources;
using html = System.Web.Mvc.Html.LanguageLabelExtensions;
using BELabelConstant = ATS.BusinessEntity.Common.CommonConstant;
using BESubscription = ATS.BusinessEntity.Common.SubscriptionLabel;

namespace ATS.WebUi.Areas.Admin.Controllers
{
    public class SubscriptionController : AdminBaseController
    {
        //
        // GET: /Subscription/

        #region Private members
        private Guid _CurrentUserMasterId;
        private Guid _CurrentLanguageId;
        private BLMaster.SubscriptionAction _objSubscriptionAction;
        #endregion

        #region Initialize Method call on every request
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (base.CurrentUserMaster != null)
            {
                _CurrentUserMasterId = ATSCommon.CurrentSession.Instance.VerifiedUserMaster.UserId;
                _CurrentLanguageId = ATSCommon.CurrentSession.Instance.VerifiedUserMaster.CurrentLanguageId;
                _objSubscriptionAction = new BLMaster.SubscriptionAction();
            }
        }
        #endregion

        public ActionResult Index(string KeyMsg)
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
                List<BEMaster.Subscription> objSubscription = _objSubscriptionAction.GetAllSubscription();

                if (objSubscription != null && objSubscription.Count > 0)
                    return View(objSubscription);
                else
                    return RedirectToAction(ATS.WebUi.Common.Constants.STR_VIEW_METHOD);
            }
            catch
            {
                throw;
            }
        }

        public ActionResult View(Guid? id)
        {
            bool isEdit = false;
            if (id != null && !id.Equals(Guid.Empty))
            {
                isEdit = true;
            }
            try
            {
                BEMaster.Subscription objSubscription = new BEMaster.Subscription();
                if (isEdit)
                {
                    ViewBag.PageName = String.Format("{0} {1}", ATSCommon.CommonFunctions.LanguageLabel(BELabelConstant.UPDATE),
                        ATSCommon.CommonFunctions.LanguageLabel(BESubscription.FRM_SUBSCRIPTION));
                    ViewBag.RedirectAction = "Edit";
                    objSubscription = _objSubscriptionAction.GetSubscriptionById((Guid)id);
                }
                else
                {
                    ViewBag.PageName = String.Format("{0} {1}", ATSCommon.CommonFunctions.LanguageLabel(BELabelConstant.ADD),
                        ATSCommon.CommonFunctions.LanguageLabel(BESubscription.FRM_SUBSCRIPTION));
                    ViewBag.RedirectAction = "Create";
                }

                return View(objSubscription);
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);

                ///*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);

                ///*Redirect to List Page*/
                return RedirectToAction(ATS.WebUi.Common.Constants.STR_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }

        [HttpPost]
        public ActionResult Create(BEMaster.Subscription objSubscription)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    JsonModels actionStatus = null;
                    Guid subscriptionId = Guid.Empty;
                    subscriptionId = _objSubscriptionAction.InsertSubscription(objSubscription);

                    if (subscriptionId != null && !subscriptionId.Equals(Guid.Empty))
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
                    return RedirectToAction(ATS.WebUi.Common.Constants.STR_LIST_METHOD, new { KeyMsg = keyMsg });
                }
                else
                {
                    var error = ModelState.Values.SelectMany(v => v.Errors);
                    return RedirectToAction(ATS.WebUi.Common.Constants.STR_LIST_METHOD, new { KeyMsg = string.Empty });
                }

            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);

                ///*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);

                /*Redirect to List Page*/
                return RedirectToAction(ATS.WebUi.Common.Constants.STR_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }

        [HttpPost]
        public ActionResult Edit(BEMaster.Subscription objSubscription)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    JsonModels actionStatus = null;
                    bool result = false;
                    result = _objSubscriptionAction.UpdateSubscriptionById(objSubscription);

                    if (result)
                    {
                        actionStatus = base.GetJsonContent(false, string.Empty, "Record Updated Successfully");
                    }
                    else
                    {
                        actionStatus = base.GetJsonContent(true, string.Empty, "Not Able To Update Record");
                    }
                    /*Convert to json string*/
                    string jsonModels = JsonConvert.SerializeObject(actionStatus);
                    string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                    /*Redirect to List Page*/
                    return RedirectToAction(ATS.WebUi.Common.Constants.STR_LIST_METHOD, new { KeyMsg = keyMsg });
                }
                else
                {
                    var error = ModelState.Values.SelectMany(v => v.Errors);
                    return RedirectToAction(ATS.WebUi.Common.Constants.STR_LIST_METHOD, new { KeyMsg = string.Empty });
                }

            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);

                ///*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);

                /*Redirect to List Page*/
                return RedirectToAction(ATS.WebUi.Common.Constants.STR_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }
    }
}
