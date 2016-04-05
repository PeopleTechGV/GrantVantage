using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEClient = ATS.BusinessEntity;
using BLClient = ATS.BusinessLogic;
using ATSCommon = ATS.WebUi.Common;
using ATS.WebUi.Models;
using Newtonsoft.Json;
using BEMaster = ATS.BusinessEntity.Master;
using BLMaster = ATS.BusinessLogic.Master;
using BECommon = ATS.BusinessEntity.Common;
using BESrp = ATS.BusinessEntity.SRPEntity;
using System.Web.UI.WebControls;
using RootModels = ATS.WebUi.Models;


namespace ATS.WebUi.Areas.Employee.Controllers
{
    public class SkinMasterController : ATS.WebUi.Controllers.AreaBaseController
    {
        #region Private Members
        private Guid _CurrentClientId;
        private Guid _CurrentLanguageId;
        private string STR_FORMNAME = BEClient.Common.CompanySetupMenu.CSMNU_COLOROPTIONS;
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
                if (ATSCommon.CurrentSession.Instance.VerifiedUser.SecurityRoles.Where(x => x.AtsSecurityRole == BEClient.ATSSecurityRole.Candidate).Count() > 0)
                {
                    filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_CANDIDATE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_CANDIDATE_CONTROLLER, new { area = ATSCommon.Constants.AREA_CANDIDATE }));
                }
                //if (Common.CurrentSession.Instance.VerifiedUser.SecurityRoles.Where(x => x.AtsSecurityRole.Equals(BEClient.ATSSecurityRole.SystemAdministrator)).Count() <= 0)
                //{
                //    filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_CONTROLLER, new { @KeyMsg = ATSCommon.CommonFunctions.NonAuthoriseduser() }));
                //}
            }
        }
        #endregion

        public ActionResult Index()
        {
            try
            {
                RootModels.BreadScrumbModel<BESrp.DynamicSRP<List<BEClient.SkinMaster>>> _objBreadScrumbModel = new RootModels.BreadScrumbModel<BESrp.DynamicSRP<List<BEClient.SkinMaster>>>();

                BLClient.SkinMasterAction objSkinAction = new BLClient.SkinMasterAction(CurrentClient.ClientId);
                List<BEClient.SkinMaster> objSkinList = new List<BEClient.SkinMaster>();
                objSkinList = objSkinAction.GetAllSkin();

                BESrp.DynamicSRP<List<BEClient.SkinMaster>> _objSRPSkinMaster = new BESrp.DynamicSRP<List<BEClient.SkinMaster>>(); ;
                _objSRPSkinMaster.obj = objSkinList;

                _objBreadScrumbModel.obj = _objSRPSkinMaster;
                _objBreadScrumbModel.DisplayName = STR_FORMNAME;
                _objBreadScrumbModel.ToolTip = STR_FORMNAME;
                return View(_objBreadScrumbModel);
            }
            catch
            {
                throw;
            }
        }

        public JsonResult UpdateSkin(int SkinId, string StylePath)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                BLClient.SkinMasterAction objSkinAction = new BLClient.SkinMasterAction(CurrentClient.ClientId);
                bool Result = objSkinAction.UpdateSkin(SkinId);
                if (Result)
                {
                    ATS.WebUi.Common.CurrentSession.Instance.VerifiedClient.objCompanyInfo.StylePath = StylePath;
                    Message = "Applied Successfully";
                }
                else
                {
                    IsError = true;
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data));
        }
    }
}