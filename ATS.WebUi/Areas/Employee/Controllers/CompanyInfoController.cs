using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BEClient = ATS.BusinessEntity;
using System.Web.Mvc;
using RootModels = ATS.WebUi.Models;
using BLClient = ATS.BusinessLogic;
using ATSCommon = ATS.WebUi.Common;
using BESrp = ATS.BusinessEntity.SRPEntity;
using ATS.WebUi.Models;
using Newtonsoft.Json;
using System.IO;
namespace ATS.WebUi.Areas.Employee.Controllers
{
    public class CompanyInfoController : ATS.WebUi.Controllers.AreaBaseController
    {
        // GET: /Employee/CompanyInfo/
        #region Private Members
        private BLClient.CompanyInfoAction _objCompanyInfoAction;
        private BESrp.DynamicSRP<BEClient.CompanyInfo> _objComapanyinfo;
        private Guid _CurrentClientId;
        private Guid _CurrentLanguageId;
        #endregion

        #region OnAuthorization Method call on every request
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (base.CurrentClient != null && Common.CurrentSession.Instance.VerifiedUser != null)
            {
                _CurrentClientId = base.CurrentClient.ClientId;
                _CurrentLanguageId = base.CurrentClient.CurrentLanguageId;
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
                RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.CompanyInfo>> _objBreadScrumbModel = new RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.CompanyInfo>>();
                _objBreadScrumbModel.obj = new BESrp.DynamicSRP<BEClient.CompanyInfo>();
                _objCompanyInfoAction = new BLClient.CompanyInfoAction(_CurrentClientId);
                CreateObjComapanyInfo();
                _objBreadScrumbModel.obj = _objComapanyinfo;
                _objBreadScrumbModel.DisplayName = BEClient.Common.CompanySetupMenu.CSMNU_COMPANYINFO;
                _objBreadScrumbModel.ToolTip = BEClient.Common.CompanySetupMenu.CSMNU_COMPANYINFO;
                _objBreadScrumbModel.obj.obj = _objCompanyInfoAction.GetCompanyInfoDetails();
                return View(_objBreadScrumbModel);
            }
            catch
            {
                throw;
            }
        }
        public ActionResult Edit(BESrp.DynamicSRP<ATS.BusinessEntity.CompanyInfo> objCompanyInfo, HttpPostedFileBase imgFile)
        {
            try
            {
                bool Result = false;
                JsonModels actionStatus = null;
                string _serverFilePath = string.Empty;
                string _newFileName = string.Empty;
                string _oldFileName = string.Empty;
                Guid _DocumentId = Guid.Empty;
                bool _isFileUploaded = false;
                if (imgFile != null)
                {
                    _isFileUploaded = UploadLogo(imgFile, out _oldFileName, out _serverFilePath);
                }
                if (_isFileUploaded)
                {
                    objCompanyInfo.obj.Logo = _serverFilePath;
                }
                _objCompanyInfoAction = new BLClient.CompanyInfoAction(_CurrentClientId);
                Result = _objCompanyInfoAction.UpdateCompanyinfo(objCompanyInfo.obj);
                if (Result)
                {
                    ATSCommon.CurrentSession.Instance.VerifiedClient.objCompanyInfo.CompanyName = objCompanyInfo.obj.CompanyName;
                    ATSCommon.CurrentSession.Instance.VerifiedClient.objCompanyInfo.EmailSenderAddress = objCompanyInfo.obj.EmailSenderAddress;
                    ATSCommon.CurrentSession.Instance.VerifiedClient.objCompanyInfo.PortalBannerTitle = objCompanyInfo.obj.PortalBannerTitle;
                    if (objCompanyInfo.obj.Logo != null && objCompanyInfo.obj.Logo != "")
                        ATSCommon.CurrentSession.Instance.VerifiedClient.objCompanyInfo.Logo = objCompanyInfo.obj.Logo;

                    actionStatus = base.GetJsonContent(!Result, string.Empty, "Record Updated Successfully");
                }
                else
                {
                    actionStatus = base.GetJsonContent(!Result, string.Empty, "Not Able To Update Record");
                }
                //}
                //else
                //{
                //    actionStatus = base.GetJsonContent(!Result, string.Empty, "Not Able To Update Record");
                //}
                /*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                /*Redirect to List Page*/
                return RedirectToAction("Index", new { KeyMsg = keyMsg });
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = null;
                actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                return RedirectToAction("Index", new { KeyMsg = keyMsg });
            }

        }
        #region Create Single object
        private void CreateObjComapanyInfo()
        {
            _objComapanyinfo = new BESrp.DynamicSRP<BEClient.CompanyInfo>();
            _objComapanyinfo.AddBtnText =
            _objComapanyinfo.EditBtnText =
            _objComapanyinfo.RemoveBtnText =
            _objComapanyinfo.SaveBtnText = "Save";
        }
        #endregion
        public bool UploadLogo(System.Web.HttpPostedFileBase documentFile, out string _oldFileName, out string _serverFilePath)
        {
            bool _isFileUploaded = false;
            _oldFileName = String.Empty;
            _serverFilePath = String.Empty;
            try
            {
                if (documentFile != null && documentFile.ContentLength > 0)
                {
                    #region Create new directory
                    CreateDirectory();
                    #endregion

                    string _LogoPath = ValidateLogo(documentFile, out _oldFileName, out _serverFilePath);

                    SaveFile(documentFile, _LogoPath);
                    if (!System.IO.File.Exists(_LogoPath))
                        throw new Exception("Logo not Upload on Path " + _LogoPath);
                    else
                        _isFileUploaded = true;
                }
            }
            catch
            {
                throw;
            }
            return _isFileUploaded;
        }
        public string ValidateLogo(System.Web.HttpPostedFileBase documentFile, out string _oldFileName, out string _serverFilePath)
        {
            try
            {
                _oldFileName = String.Empty;
                _serverFilePath = String.Empty;
                Common.CommonFunctions.ValidateLogo(documentFile.FileName);
                string _extension = Path.GetExtension(documentFile.FileName);
                _oldFileName = Path.GetFileName(documentFile.FileName);
                _serverFilePath = Path.Combine(Common.Constants.STR_LOGO_PATH, _oldFileName);
                string _resumePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(Common.Constants.STR_LOGO_PATH), _oldFileName);
                return _resumePath;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void CreateDirectory()
        {
            try
            {
                //Create directory of not exist
                if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(Common.Constants.STR_LOGO_PATH)))
                {
                    Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(Common.Constants.STR_LOGO_PATH));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void SaveFile(HttpPostedFileBase documentFile, string filePath)
        {
            try
            {
                documentFile.SaveAs(filePath);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}