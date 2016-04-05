using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = ATS.BusinessEntity;
using BEMaster = ATS.BusinessEntity.Master;
using BECommon = ATS.BusinessEntity.Common;
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
using ATS.WebUi.Areas.Admin.Models;
using BELabel = ATS.BusinessEntity.Common.ClientLabel;
using BELabelConstant = ATS.BusinessEntity.Common.CommonConstant;

namespace ATS.WebUi.Areas.Admin.Controllers
{
    public class ClientController : AdminBaseController
    {
        //
        // GET: /Employee/Client/
        #region Private members
        private Guid _CurrentUserMasterId;
        private Guid _CurrentLanguageId;
        private BLMaster.ClientMasterAction _objClientMasterAction;
        #endregion

        #region Initialize Method call on every request
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (base.CurrentUserMaster != null)
            {
                _CurrentUserMasterId = ATSCommon.CurrentSession.Instance.VerifiedUserMaster.UserId;
                _CurrentLanguageId = ATSCommon.CurrentSession.Instance.VerifiedUserMaster.CurrentLanguageId;
                _objClientMasterAction = new BLMaster.ClientMasterAction();
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
                List<BEMaster.ClientMaster> objClientMaster = _objClientMasterAction.GetAllClient();

                if (objClientMaster != null && objClientMaster.Count > 0)
                    return View(objClientMaster);
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
                ClientMasterModel objClientMaster = new ClientMasterModel();
                objClientMaster.objClientMaster = new BEMaster.ClientMaster();
                objClientMaster.AvailableLanguage = BLCommon.CacheHelper.GetAllLanguageList();
                string selectedLang = objClientMaster.AvailableLanguage.Where(r => r.LanguageCulture.ToLower().Equals(ATS.WebUi.Common.Constants.Default_Language_Culture.ToLower())).Select(r => r.LanguageId).First().ToString();
                ViewBag.DisableLanguageName = new string[] { selectedLang };
                DropDownLists();
                if (isEdit)
                {
                    //ViewBag.PageName = Resources.Resources.CM_Edit_PN;

                    ViewBag.PageName = String.Format("{0} {1}", ATSCommon.CommonFunctions.LanguageLabel(BELabelConstant.UPDATE),
                    ATSCommon.CommonFunctions.LanguageLabel(BELabel.FRM_CLIENT));
                    ViewBag.RedirectAction = "Edit";
                    objClientMaster.objClientMaster = _objClientMasterAction.GetClientDetailById((Guid)id);
                    List<BEMaster.ClientLanguage> objClientLanguage = new List<BEMaster.ClientLanguage>();
                    BLMaster.ClientLanguageAction objClientLanguageAction = new BLMaster.ClientLanguageAction();
                    objClientLanguage = objClientLanguageAction.GetLanguageByClientId((Guid)id);
                    List<BEMaster.Language> objLang = new List<BEMaster.Language>();
                    foreach (var v in objClientLanguage)
                    {
                        objLang.Add(v.objLanguage);
                    }
                    objClientMaster.SelectedLanguage = objLang;
                }
                else
                {
                    //ViewBag.PageName = Resources.Resources.CM_BN;
                    ViewBag.PageName = String.Format("{0} {1}", ATSCommon.CommonFunctions.LanguageLabel(BELabelConstant.ADD),
                        ATSCommon.CommonFunctions.LanguageLabel(BELabel.FRM_CLIENT));
                    ViewBag.RedirectAction = "Create";

                    //setup a view model
                    List<BEMaster.Language> objLanglist = new List<BEMaster.Language>();
                    BEMaster.Language objLanguage = new BEMaster.Language();
                    objLanguage.LanguageId = new Guid(selectedLang);
                    objLanglist.Add(objLanguage);
                    objClientMaster.SelectedLanguage = objLanglist;//new List<BEMaster.Language>();
                }

                return View(objClientMaster);
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

        #region Private Data Member
        private void DropDownLists()
        {
            try
            {

                //Bind DropDown For PositionType
                List<BEMaster.Subscription> objSubscription = new List<BEMaster.Subscription>();
                BLMaster.SubscriptionAction objSubscriptionAction = new SubscriptionAction();

                objSubscription = objSubscriptionAction.GetAllSubscription();
                ViewBag.UserLimitList1 = new SelectList(objSubscription, "SubscriptionId", "UserLimit");
            }
            catch
            {
                throw;
            }
        }
        #endregion

        //public JsonResult CheckDBNameExists(string dBName)
        //{
        //    try
        //    {
        //        bool result = DBNameExists(dBName);
        //        return GetJson(result);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        public Boolean DBNameExists(string dBName)
        {
            try
            {

                return _objClientMasterAction.CheckDBNameExists(dBName);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region Add Client
        [HttpPost]
        public ActionResult Create(ClientMasterModel objClientMasterModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool result = DBNameExists(objClientMasterModel.objClientMaster.DatabaseName);
                    JsonModels actionStatus = null;
                    if (!result)
                    {
                        Guid newClientId = _objClientMasterAction.Add(objClientMasterModel.objClientMaster, objClientMasterModel.Language.LanguageId);

                        if (newClientId != null && !newClientId.Equals(Guid.Empty))
                        {
                            BLMaster.ClientMasterAction objNew = new ClientMasterAction(newClientId);
                            bool result1 = objNew.AddTables(objClientMasterModel.objClientMaster.DatabaseName);
                            if (result1)
                            {
                                actionStatus = base.GetJsonContent(false, string.Empty, "Record Added Successfully");

                            }
                            else
                            {
                                actionStatus = base.GetJsonContent(true, string.Empty, "Not Able To Add Record");
                            }
                        }
                        else
                        {
                            actionStatus = base.GetJsonContent(true, string.Empty, "Not Able To Add Record");
                        }

                    }
                    else
                    {
                        actionStatus = base.GetJsonContent(true, string.Empty, "Database name already exists");
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

                /*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);

                /*Redirect to List Page*/
                return RedirectToAction(ATS.WebUi.Common.Constants.STR_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }
        #endregion

        #region Edit Client
        [HttpPost]
        public ActionResult Edit(ClientMasterModel objClientMasterModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    JsonModels actionStatus = null;
                    bool result = false;
                    result = _objClientMasterAction.Save(objClientMasterModel.objClientMaster, objClientMasterModel.Language.LanguageId);

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
        #endregion
    }
}
