using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEMaster = ATS.BusinessEntity.Master;
using BLMaster = ATS.BusinessLogic.Master;
using Prompt.Shared.Utility.Library;
using ATSCommon = ATS.WebUi.Common;
using ATS.WebUi.Models;
using ATSHelper = ATS.HelperLibrary;
using BLClient = ATS.BusinessLogic;
using BLCommon = ATS.BusinessLogic.Common;
using ATS.WebUi.Helpers;

namespace ATS.WebUi.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Admin/Home/
        public ActionResult ChangeLanguage(string userName, Guid languageId, string returnUrl, string culture)
        {
            ATS.WebUi.Common.CurrentSession.Instance.VerifiedUserMaster.CurrentLanguageId = languageId;

            culture = CultureHelper.GetImplementedCulture(culture);
            ATS.WebUi.Common.CurrentSession.Instance.VerifiedUserMaster.CurrentCulture = culture;

            RouteData.Values["culture"] = culture;
            return View();
            //return RedirectToAdmin(userName, returnUrl);
        }

        public ActionResult Index(string UserName,string ReturnUrl)
        {
            if (!String.IsNullOrEmpty(UserName))
            {

                BLMaster.UserMasterAction objUserMasterAction = new BLMaster.UserMasterAction();
                BEMaster.UserMaster objUserMaster = objUserMasterAction.GetUserByUserName(UserName);
               
                if (objUserMaster == null)
                {
                    Methods.Session.Clear();
                    return View();
                }
                if (objUserMaster != null && ATS.WebUi.Common.CurrentSession.Instance == null)
                {
                    new ATSCommon.CurrentSession(objUserMaster);

                }
                if (!String.IsNullOrEmpty(ReturnUrl))
                    return Redirect(ReturnUrl);
                else
                    return GetJson(ATS.WebUi.Common.CommonFunctions.GetJsonContentObj(false,false, RedirectToAdmin()));
                    
            }
            else
            {
                Methods.Session.Clear();
                return View();
            }
        }

        public ActionResult LogOut()
        {
            Methods.Session.Clear();
            return RedirectToAction("Index");
        }

        private String RedirectToAdmin()
        {
            UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);
            return u.Action("Index", "Client");
        }

        [HttpPost]
        public ActionResult Login(LogOnModel logonModule, string returnUrl)
        {
            try
            {

                string password = ATSHelper.Encryption.EncryptionAlgo.EncryptionData(logonModule.Password);
                BEMaster.UserMaster objUserMaster = BLClient.Common.LoginOperation.ValidateUserMaster(logonModule.UserName, password);

                if (objUserMaster == null)
                {
                    return GetJson(ATS.WebUi.Common.CommonFunctions.GetJsonContentObj(false,true, null));
                }
                else
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                        return GetJson(ATS.WebUi.Common.CommonFunctions.GetJsonContentObj(false,false, returnUrl));

                    new ATSCommon.CurrentSession(objUserMaster);

                    return GetJson(ATS.WebUi.Common.CommonFunctions.GetJsonContentObj(false,false, RedirectToAdmin()));

                }
            }
            catch (Exception ex)
            {
                return GetJson(ATS.WebUi.Common.CommonFunctions.GetJsonContentObj(false, true, null, ex.Message));
            }
        }

        public ActionResult Login()
        {
            return View("_LoginForm", new LogOnModel());
        }

        public ActionResult LanguageList()
        {
            try
            {
                List<BEMaster.Language> objLanguage = BLCommon.CacheHelper.GetAllLanguageList();

                return PartialView("_LanguageControl", objLanguage);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public JsonResult GetJson(object data)
        {
            if (!Request.AcceptTypes.Contains("application/json"))
            {
                return base.Json(data, "text/plain");
            }
            else
            { return base.Json(data); }

        }

    }
}
