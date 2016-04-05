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


namespace ATS.WebUi.Areas.Employee.Controllers
{
    public class CompanyController : ATS.WebUi.Controllers.AreaBaseController
    {
        // GET: /Employee/Company/
        #region Private Members
        private BLClient.CompanyAction _objCompanyAction;
        private Guid _CurrentClientId;
        private Guid _CurrentLanguageId;
        private List<BEClient.Company> _objCompanyList = new List<BEClient.Company>();
        #endregion

        #region Redirection Method
        private string STR_COMPANY_LIST_METHOD = "Index";
        private string STR_COMPANY_CREATE_METHOD = "Create";
        private string STR_COMPANY_EDIT_METHOD = "Edit";
        private string STR_COMPANY_VIEW_METHOD = "View";
        private string STR_FORMNAME = "Client";
        #endregion
        #region OnAuthorization Method call on every request
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (base.CurrentClient != null && Common.CurrentSession.Instance.VerifiedUser != null)
            {
                _CurrentClientId = base.CurrentClient.ClientId;
                _CurrentLanguageId = base.CurrentClient.CurrentLanguageId;
                _objCompanyAction = new BLClient.CompanyAction(_CurrentClientId);
                BEClient.Company objCompany = new BEClient.Company();
            }
        }
        #endregion
        public ActionResult Index(string KeyMsg)
        {
            ViewBag.FormName = STR_FORMNAME;
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
                _objCompanyList = _objCompanyAction.GetAllCompany();
                return View(_objCompanyList);
            }
            catch
            {
                throw;
            }
        }

        #region Add Company
        [HttpPost]
        public ActionResult Create(BEClient.Company objCompany)
        {
            try
            {
                objCompany.ClientId = _CurrentClientId;
                Guid newUserLocationPermissionId = _objCompanyAction.AddCompany(objCompany);
                JsonModels actionStatus = null;
                if (newUserLocationPermissionId != null && !newUserLocationPermissionId.Equals(Guid.Empty))
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
                return RedirectToAction(STR_COMPANY_LIST_METHOD, new { KeyMsg = keyMsg });
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                /*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                /*Redirect to List Page*/
                return RedirectToAction(STR_COMPANY_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }
#endregion

        public ActionResult Edit(BEClient.Company company)
        {
            try
            {
                bool isSaved = _objCompanyAction.UpdateCompany(company);
                JsonModels actionStatus = null;
                if (isSaved)
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
                return RedirectToAction(STR_COMPANY_LIST_METHOD, new { KeyMsg = keyMsg });
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                /*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                /*Redirect to List Page*/
                return RedirectToAction(STR_COMPANY_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }

        public ActionResult View(Guid? id)
        {
            bool isEdit = false;
            if (id != null && !id.Equals(Guid.Empty))
            {
                isEdit = true;
                ViewBag.IsEdit = 1;
            }
            try
            {
                BEClient.Company ObjCompany = new BEClient.Company();
                if (isEdit)
                {
                    ObjCompany = _objCompanyAction.GetCompanyById((Guid)id);
                     ViewBag.RedirectAction = STR_COMPANY_EDIT_METHOD;
                }
                else
                {
                    ObjCompany.ClientId = _CurrentClientId;
                    ViewBag.RedirectAction = STR_COMPANY_CREATE_METHOD;
                }
                ViewBag.FormName = STR_FORMNAME;
                return View(ObjCompany);
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                ///*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                ///*Redirect to List Page*/
                return RedirectToAction(STR_COMPANY_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }
    }

}
