using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLMaster = ATS.BusinessLogic.Master;
using BLClient = ATS.BusinessLogic;
using BEMaster = ATS.BusinessEntity.Master;
using BEClient = ATS.BusinessEntity;
using ATSCommon = ATS.WebUi.Common;
using ATSHelper = ATS.HelperLibrary;
using ATS.WebUi.Models;
using Prompt.Shared.Utility.Library;
using ATS.WebUi.Helpers;
using log4net;
using Newtonsoft.Json;

namespace ATS.WebUi.Controllers
{
    public class HomeController : BaseController
    {
        private static readonly log4net.ILog log = LogManager.GetLogger(
  System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ActionResult ChangeLanguage(string clientName, Guid languageId, string returnUrl, string culture)
        {
            if (ATS.WebUi.Common.CurrentSession.Instance == null)
            {
                return RedirectToLoginPage(clientName);
            }

            ATS.WebUi.Common.CurrentSession.Instance.VerifiedClient.CurrentLanguageId = languageId;

            culture = CultureHelper.GetImplementedCulture(culture);
            ATS.WebUi.Common.CurrentSession.Instance.VerifiedClient.CurrentCulture = culture;

            RouteData.Values["culture"] = culture;

            return RedirectToLoginPage(clientName, returnUrl);
        }
        [HttpPost]
        public ActionResult MakeSessionLive()
        {
            JsonModels jsonModel = new JsonModels();
            jsonModel.IsError = false;
            jsonModel.Message = "Reactivate session";
            jsonModel.Data = String.Empty;
            jsonModel.Url = null;
            return GetJson(jsonModel);
        }
        public ActionResult Index(string ClientName, string ReturnUrl, string key, string token)
        {
            if (String.IsNullOrEmpty(ClientName))
            {
                string findClinet = HttpContext.Request.Url.Host.ToLower();
                if (findClinet == "localhost")
                {
                    ClientName = "preaward";//Add default client here
                }
                else if (findClinet.ToLower().Contains("preaward.grantvantage"))
                {
                    ClientName = "preaward";
                }
                else
                {
                    ClientName = "preaward";
                }
            }


            try
            {
                log.Debug(ClientName);
                if (!String.IsNullOrEmpty(ClientName))
                {
                    /*Create client session if not exists*/
                    BEMaster.Client objClient = BusinessLogic.Common.LoginOperation.CreateClientSession(ClientName);

                    if (objClient != null)
                    {
                        log.Debug("objClient != null");
                        ATSCommon.CookieOperation.CookieBasedOnRequest(ATSCommon.Constants.COOKIE_CURRENT_CLIENTNAME, ClientName, ATSCommon.Constants.COOKIE_MIN, Response, Request);
                        new ATSCommon.CurrentSession(objClient);

                    }
                    else
                    {
                        return View();
                    }

                    //////API Login Process
                    if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(token))
                    {
                        try
                        {
                            Guid ssoUserId = Guid.Empty;
                            Guid ssoToken = Guid.Empty;
                            Guid.TryParse(key, out ssoUserId);
                            Guid.TryParse(token, out ssoToken);
                            BLClient.UserAction _objUserAction = new BLClient.UserAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                            BEClient.User objSSOUser = _objUserAction.GetSSOUser(ssoUserId, ssoToken);
                            if (objSSOUser != null)//If User with Tokon found
                            {
                                ATSCommon.CookieOperation.RemoveCookie(ATSCommon.Constants.COOKIE_CURRENT_USER, Response, Request);
                                ATSCommon.CurrentSession.Instance.VerifiedUser = null;
                                BEClient.User objUser = BLClient.Common.LoginOperation.ValidateLogin(objSSOUser.Username, objSSOUser.Password, Common.CurrentSession.Instance.VerifiedClient.ClientId);

                                if (objUser != null)
                                {
                                    new ATSCommon.CurrentSession(objUser);
                                    if (objUser.SecurityRoles != null && objUser.SecurityRoles.Count() <= 0)
                                    {
                                        return RedirectToAction("Index", "Account");
                                    }
                                    else if (objUser.SecurityRoles != null && objUser.SecurityRoles.Where(x => x.AtsSecurityRole == BEClient.ATSSecurityRole.Candidate).Count() > 0)
                                    {
                                        return RedirectToAction("Index", "Account");
                                    }
                                    else
                                    {
                                        return RedirectToAction("Index", "Account");
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        { return RedirectToAction("Index", "Account"); }
                        /*End session Creation*/
                    }
                    //////API Login Process END


                    if (!String.IsNullOrEmpty(ReturnUrl))
                        return Redirect(ReturnUrl);
                    else
                    {
                        log.Debug("RedirectToAction(Index, Account)");
                        return RedirectToAction("Index", "Account");
                    }
                }
                else
                {
                    log.Debug("Methods.Session.Clear()");
                    Methods.Session.Clear();
                    return View();
                }
            }
            catch (Exception ex)
            {

                log.Error(ex.Message);
                return View();
            }
        }
        public ActionResult LogOut()
        {
            try
            {
                BLClient.UserAction _objUserAction = new BLClient.UserAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                bool result = _objUserAction.ResetSSoToken(Common.CurrentSession.Instance.UserId);
            }
            catch (Exception)
            { }

            String clientName = string.Empty;
            if (ATSCommon.CookieOperation.CookieExists(ATSCommon.Constants.COOKIE_CURRENT_CLIENTNAME, Request))
                clientName = ATSCommon.CookieOperation.ReadCookie(ATSCommon.Constants.COOKIE_CURRENT_CLIENTNAME, Request);

            if (ATSCommon.CurrentSession.Instance != null)
            {

                clientName = ATSCommon.CurrentSession.Instance.VerifiedClient.Clientname;
                ATSCommon.CurrentSession.Instance.VerifiedUser = null;
            }

            ATSCommon.CookieOperation.RemoveCookie(ATSCommon.Constants.COOKIE_CURRENT_USER, Response, Request);



            return RedirectToLoginPage(clientName);
        }
        public ActionResult Login()
        {

            LogOnModel objLogon = new LogOnModel();

            if (ATS.WebUi.Common.CookieOperation.CookieExists(ATS.WebUi.Common.Constants.COOKIE_CURRENT_USER_REMEMBER_ME.ToString(), Request))
            {
                objLogon.UserName = ATS.WebUi.Common.CookieOperation.ReadCookie(ATS.WebUi.Common.Constants.COOKIE_CURRENT_USER_REMEMBER_ME.ToString(), Request);
            }

            return View("_LoginForm", objLogon);
        }
        [HttpPost]
        public ActionResult Login(LogOnModel logonModule, string returnUrl, bool IsAnonymous = false)
        {
            try
            {
                //Anand:
                System.Collections.Specialized.NameValueCollection myCol = Request.Params;
                string remember = Request.Params["Rememberme"];
                // It will create session automatically if Client session is expire
                if (ATS.WebUi.Common.CurrentSession.Instance == null || ATS.WebUi.Common.CurrentSession.Instance.VerifiedClient == null)
                {
                    if (ATSCommon.CookieOperation.CookieExists(ATSCommon.Constants.COOKIE_CURRENT_CLIENTNAME, Request))
                    {
                        String clientName = ATSCommon.CookieOperation.ReadCookie(ATSCommon.Constants.COOKIE_CURRENT_CLIENTNAME, Request);
                        BEMaster.Client objClient = BusinessLogic.Common.LoginOperation.CreateClientSession(clientName);
                        if (objClient != null)
                        {
                            new ATSCommon.CurrentSession(objClient);
                        }
                    }
                }

                string password = ATSHelper.Encryption.EncryptionAlgo.EncryptionData(logonModule.Password);
                BEClient.User objUser = BLClient.Common.LoginOperation.ValidateLogin(logonModule.UserName, password, Common.CurrentSession.Instance.VerifiedClient.ClientId);
                //Guid contactid = _webservice.ValidateUser(logonModule.UserName, password);

                //To get Current Skin path.
                ATS.BusinessLogic.CompanyInfoAction objCompanyInfoAction = new ATS.BusinessLogic.CompanyInfoAction(ATS.WebUi.Common.CurrentSession.Instance.VerifiedClient.ClientId);
                ATS.WebUi.Common.CurrentSession.Instance.VerifiedClient.objCompanyInfo = objCompanyInfoAction.GetCompanyInfoDetails();

                if (objUser == null)
                {
                    return GetJson(base.GetJsonContent(true, null, "Invalid Username or Password"));
                }
                else
                {
                    //for Remember me
                    if (remember != null)
                    {
                        ATSCommon.CookieOperation.CookieBasedOnRequest(ATSCommon.Constants.COOKIE_CURRENT_USER_REMEMBER_ME, objUser.Username.ToString(), ATSCommon.Constants.COOKIE_MIN_REMEMBER_ME, Response, Request);
                    }
                    if (!string.IsNullOrEmpty(returnUrl))
                        return GetJson(base.GetJsonContent(false, returnUrl));
                    ATSCommon.CookieOperation.CookieBasedOnRequest(ATSCommon.Constants.COOKIE_CURRENT_USER, objUser.UserId.ToString(), ATSCommon.Constants.COOKIE_MIN, Response, Request);
                    new ATSCommon.CurrentSession(objUser);


                    if (objUser.SecurityRoles != null && objUser.SecurityRoles.Count() <= 0)
                    {
                        return RedirectToLoginPage(ATSCommon.CurrentSession.Instance.VerifiedClient.Clientname);
                    }
                    else if (objUser.SecurityRoles != null && objUser.SecurityRoles.Where(x => x.AtsSecurityRole == BEClient.ATSSecurityRole.Candidate).Count() > 0)
                    {
                        if (IsAnonymous == true && logonModule.VacancyId != null && logonModule.VacancyId != Guid.Empty)
                        {
                            return GetJson(base.GetJsonContent(false, RedirectToStep2(logonModule.VacancyId)));
                        }
                        else if (IsAnonymous == true)
                        {
                            return GetJson(base.GetJsonContent(false, RedirectToUploadResume()));
                        }
                        else
                        {
                            return GetJson(base.GetJsonContent(false, RedirectToCandidateHome()));
                        }
                    }
                    else
                    {
                        return GetJson(base.GetJsonContent(false, RedirectToEmployeeUser()));
                    }
                }
            }
            catch (Exception ex)
            {
                return GetJson(base.GetJsonContent(true, null, ex.Message));
            }
        }

        public ActionResult SignUp()
        {
            return View("_SignupForm", new BEClient.AnonymousUser());
        }
        //Sign Up 
        //[HttpPost, CaptchaMvc.Attributes.CaptchaVerify("Captcha is not valid.")]
        [HttpPost]
        public ActionResult SignUp(SignUpModel signModule)
        {
            //if (!ModelState.IsValid)
            //{
            //    if (CaptchaMvc.HtmlHelpers.CaptchaHelper.IsCaptchaValid(this, "Captcha is not valid.") == false)
            //    {
            //        return GetJson(base.GetJsonContent(true, null, "Captcha is not valid."));
            //    }
            //}
            string password = ATSHelper.Encryption.EncryptionAlgo.EncryptionData(signModule.Password);

            string oResult = string.Empty;

            var contactId = BLClient.Common.LoginOperation.CandidateSignUp(signModule.UserName, password, ATSCommon.CurrentSession.Instance.VerifiedClient.ClientId, out oResult, ATS.WebUi.Common.CurrentSession.Instance.VerifiedClient.CurrentLanguageId, false);
            if (contactId != Guid.Empty)
            {
                string encrContactid = ATSHelper.Encryption.EncryptionAlgo.EncryptionData(Convert.ToString(contactId));
                string expireDateTicks = Convert.ToString(DateTime.UtcNow.AddHours(ATSCommon.Constants.EMAIL_EXPIRY_LINK).Ticks);
                string encrCname = ATSCommon.CurrentSession.Instance.VerifiedClient.Clientname;
                string encrClientid = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(Convert.ToString(ATSCommon.CurrentSession.Instance.VerifiedClient.ClientId));

                var activationlink = Common.CommonFunctions.GenerateWebLink(RedirectToCompleteRegistration(encrCname, encrContactid, expireDateTicks, encrClientid));

                Mailers.EmailTemplate mvcMailer = new Mailers.EmailTemplate();
                mvcMailer.SendCandidateRegistrationMail(signModule.UserName, activationlink, signModule.UserName, ATSCommon.CurrentSession.Instance.VerifiedClient.Clientname);

                return GetJson(base.GetJsonContent(false, RedirectToSignUpSuccess()));
            }
            else
            {
                if (oResult == "105")
                {
                    BEClient.User objuser = new BEClient.User();
                    BLClient.UserAction _objUserAction = new BLClient.UserAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                    if (!String.IsNullOrEmpty(signModule.UserName))
                    {
                        objuser = _objUserAction.GetUserIdByUserName(signModule.UserName);
                        this.SendActivationLink(objuser.UserId);
                        return GetJson(base.GetJsonContent(false, Url.Action("SignUpSuccess", "Home", new { Area = "", @Username = signModule.UserName })));
                    }
                    return GetJson(base.GetJsonContent(false, Url.Action("ActiveUsers", "Home", new { Area = "", username = signModule.UserName }), "User is registered but not activated."));
                }
                else
                {
                    return GetJson(base.GetJsonContent(true, null, "User is already registered."));
                }
            }

        }


        public ActionResult SignUpSuccess()
        {
            return View();
        }

        public ActionResult EmployeeLogin(string PublicCode, string ClientName, int ManagerLink)
        {
            try
            {
                Guid ClientId = Guid.Empty;
                if (ATS.WebUi.Common.CurrentSession.Instance != null && ATS.WebUi.Common.CurrentSession.Instance.VerifiedClient != null && ATS.WebUi.Common.CurrentSession.Instance.VerifiedClient.Clientname.ToLower().Equals(ClientName.ToLower()))
                {
                    ClientId = ATS.WebUi.Common.CurrentSession.Instance.VerifiedClient.ClientId;
                }
                else
                {
                    //Create Client Session
                    BEMaster.Client objClient = BusinessLogic.Common.LoginOperation.CreateClientSession(ClientName);
                    if (objClient != null)
                    {
                        ATSCommon.CookieOperation.CookieBasedOnRequest(ATSCommon.Constants.COOKIE_CURRENT_CLIENTNAME, ClientName, ATSCommon.Constants.COOKIE_MIN, Response, Request);
                        new ATSCommon.CurrentSession(objClient);
                        ClientId = objClient.ClientId;
                    }

                }
                BLClient.VacancyAction vacacnyAction = new BLClient.VacancyAction(ClientId);
                BEClient.Vacancy _myVacancy = vacacnyAction.GetVacancyByPublicCode(Convert.ToInt64(PublicCode));
                TempData["Vacancy"] = _myVacancy.VacancyId;
                //check is manager Link
                //if (ManagerLink)
                if (ManagerLink == (Int32)ATS.BusinessEntity.PublicCodelink.Manager)
                {

                    //Is check current login is Employee
                    if (ATS.WebUi.Common.CurrentSession.Instance != null && ATS.WebUi.Common.CurrentSession.Instance.VerifiedUser != null && ATS.WebUi.Common.CurrentSession.Instance.VerifiedUser.SecurityRoles.Where(x => x.AtsSecurityRole == BEClient.ATSSecurityRole.Candidate).Count() <= 0)
                    {
                        return RedirectToAction("View", "MyVacancy", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, id = _myVacancy.VacancyId });
                    }
                    else
                    {
                        ViewBag.MyVacacnyId = _myVacancy.VacancyId;
                        ATS.WebUi.Models.LogOnModel objLogin = new LogOnModel();
                        return View(objLogin);
                    }
                }
                else
                {
                    if (ATS.WebUi.Common.CurrentSession.Instance != null && ATS.WebUi.Common.CurrentSession.Instance.VerifiedUser != null && ATS.WebUi.Common.CurrentSession.Instance.VerifiedUser.SecurityRoles.Where(x => x.AtsSecurityRole == BEClient.ATSSecurityRole.Candidate).Count() <= 0)
                    {
                        ATSCommon.CookieOperation.RemoveCookie(ATSCommon.Constants.COOKIE_CURRENT_USER, Response, Request);
                        ATS.WebUi.Common.CurrentSession.Instance.CurrentUserLogOut();
                    }
                    //Is check current login is Candidate
                    if (ATS.WebUi.Common.CurrentSession.Instance != null && ATS.WebUi.Common.CurrentSession.Instance.VerifiedUser != null && ATS.WebUi.Common.CurrentSession.Instance.VerifiedUser.DivisionId.Equals(Guid.Empty))
                    {
                        ATS.WebUi.Common.CurrentSession.Instance.VerifiedClient.AbsoluteURI = this.Url.Action("Index", "Search", new { area = ATS.WebUi.Common.Constants.AREA_CANDIDATE }, this.Request.Url.Scheme);
                        //Redirect to Display Job
                        //return RedirectToAction("DisplayJob", "Search", new { area = ATS.WebUi.Common.Constants.AREA_CANDIDATE, VacancyId = _myVacancy.VacancyId });
                        return RedirectToAction("UploadCoverLetter", "MyApplications", new { area = ATS.WebUi.Common.Constants.AREA_CANDIDATE, VacancyId = _myVacancy.VacancyId });
                    }
                    else
                    {
                        ATS.WebUi.Common.CurrentSession.Instance.VerifiedClient.AbsoluteURI = this.Url.Action("Index", "Search", new { area = ATS.WebUi.Common.Constants.AREA_ROOT }, this.Request.Url.Scheme);
                        //return RedirectToAction("DisplayJob", "Search", new { area = ATS.WebUi.Common.Constants.AREA_ROOT, VacancyId = _myVacancy.VacancyId });
                        return RedirectToAction("AnonymousUserProfile", "Account", new { area = ATS.WebUi.Common.Constants.AREA_ROOT, VacancyId = _myVacancy.VacancyId });
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        [HttpPost]
        public ActionResult EmployeeLogin(ATS.WebUi.Models.LogOnModel objLogin)
        {
            try
            {
                TempData["Vacancy"] = TempData["Vacancy"];
                JsonModels actionStatus = null;
                if (objLogin != null)
                {

                    string password = ATSHelper.Encryption.EncryptionAlgo.EncryptionData(objLogin.Password);
                    BEClient.User objUser = BLClient.Common.LoginOperation.ValidateLogin(objLogin.UserName, password, Common.CurrentSession.Instance.VerifiedClient.ClientId);

                    if (objUser != null)
                    {
                        ATSCommon.CurrentSession.Instance.VerifiedUser = objUser;
                        if (ATSCommon.CurrentSession.Instance.VerifiedUser.SecurityRoles.Where(x => x.AtsSecurityRole == BEClient.ATSSecurityRole.Candidate).Count() <= 0)
                        {
                            return RedirectToAction("View", "Myvacancy", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, id = TempData["Vacancy"] });

                        }
                        else
                        {
                            ViewBag.IsError = true;
                            ViewBag.Message = "Invalid Employee";
                            return View(objLogin);
                        }

                    }
                    else
                    {

                        ViewBag.IsError = true;
                        ViewBag.Message = "Invalid User";
                        return View(objLogin);
                    }
                }
                else
                {


                    return RedirectToAction("EmployeeLogin");
                }

            }
            catch
            {
                throw;
            }
        }




        #region Private Data Member
        private String RedirectToSignUpSuccess()
        {
            UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);
            return u.Action("SignUpSuccess", "Home");
        }

        private String RedirectToSuccessfullySentActivation()
        {
            UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);
            return u.Action("SignUpSuccess", "Home");
        }
        private String RedirectToCompleteRegistration(string Company, string ContactId, string encExpires, string encClientId)
        {
            UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);
            return u.Action("CompleteRegistration", "Home", new
            {
                company = Company,
                area = WebUi.Common.Constants.AREA_ROOT,
                id = ContactId,
                expires = encExpires,
                CompanyId = encClientId//,
            });
        }
        private ActionResult RedirectToLoginPage(string ClientName, String ReturnUrl = null)
        {
            return RedirectToAction("Index", "Home", new { ClientName = ClientName, ReturnUrl = ReturnUrl });
        }
        private String RedirectToEmployeeUser()
        {
            UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);
            return u.Action(Common.Constants.DEFAULT_REDIRECT_EMPLOYEE_ACTION, Common.Constants.DEFAULT_REDIRECT_EMPLOYEE_CONTROLLER, new { Area = ATSCommon.Constants.AREA_EMPLOYEE });
        }
        private String RedirectToCandidateHome()
        {
            UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);
            return u.Action("Index", "Home", new { Area = ATSCommon.Constants.AREA_CANDIDATE });
        }
        private String RedirectToUploadResume()
        {
            UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);
            return u.Action("Index", "Home", new { Area = ATSCommon.Constants.AREA_CANDIDATE, ForWhat = "Create" });
        }
        private String RedirectToStep2(Guid VacancyId)
        {
            Session.Remove("LeadStep");
            UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);
            return u.Action("Index", "ApplyVacancy", new { VacancyId = VacancyId, Area = ATSCommon.Constants.AREA_CANDIDATE });
        }
        #endregion


        [HttpPost]
        public ActionResult CompleteRegistration(SignUpModel signModule, string companyName)
        {
            if (signModule != null)
            {
                BLClient.UserDetailsAction _UserDetailsAction = new BLClient.UserDetailsAction(signModule.ClientId);
                BEClient.UserDetails objUserDetails = new BEClient.UserDetails();
                objUserDetails.UserId = signModule.ContactId;
                objUserDetails.ClientId = signModule.ClientId;
                objUserDetails.FirstName = signModule.FirstName;
                objUserDetails.LastName = signModule.LastName;
                Guid newUserDeatilsId = _UserDetailsAction.AddAnonymousUserDetails(objUserDetails);
                ViewBag.Message = "Login to Explore more ";
                if (newUserDeatilsId == Guid.Empty)
                {
                    throw new Exception("user details not added");
                }
                else
                {
                    BLClient.UserAction _UserAction = new BLClient.UserAction(signModule.ClientId);
                    bool isRecordUpdated = _UserAction.CompleteRegistration(new BEClient.User() { UserId = signModule.ContactId }, true);

                    return RedirectToAction("Index", "Account");
                }

            }
            return View(signModule);
        }

        public ActionResult CompleteRegistration()
        {
            LogOut();
            Guid lsUserId = Guid.Empty;
            Guid lsClientId = Guid.Empty;
            DateTime dateExpires = new DateTime();
            bool IsCandidateProfile = false;
            if (string.IsNullOrEmpty(Request.Params["id"]) == false)
            { Guid.TryParse(HelperLibrary.Encryption.EncryptionAlgo.DecryptData(Request.Params["id"]), out lsUserId); }



            if (string.IsNullOrEmpty(Request.Params["expires"]) == false)
            { dateExpires = new DateTime(Convert.ToInt64(Request.Params["expires"]), DateTimeKind.Utc); }

            String clientName = String.Empty;
            if (string.IsNullOrEmpty(Request.Params["Company"]) == false)
            {
                ViewBag.ClientName = clientName = Request.Params["Company"];
            }

            if (string.IsNullOrEmpty(Request.Params["CompanyId"]) == false)
            { Guid.TryParse(HelperLibrary.Encryption.EncryptionAlgo.DecryptData(Request.Params["CompanyId"]), out lsClientId); }

            /*Create client session if not exists*/

            BEMaster.Client objClient = BusinessLogic.Common.LoginOperation.CreateClientSession(clientName);

            if (objClient != null)
            {
                new ATSCommon.CurrentSession(objClient);
            }
            else
            {
                return View();
            }

            /*End session Creation*/

            ViewBag.IsExpired = false;

            if (dateExpires < DateTime.UtcNow)
            {
                ViewBag.IsExpired = true;
                // For Resend activation Link
                ViewBag.UserIdActivation = lsUserId;


            }
            else
            {

                if (lsUserId != Guid.Empty)
                {
                    BEClient.User lsCandidate = new BEClient.User();
                    BEClient.UserDetails lsUserDetails = new BEClient.UserDetails();
                    BLClient.UserDetailsAction _UserDetailsAction = new BLClient.UserDetailsAction(lsClientId);

                    lsCandidate = BLClient.Common.LoginOperation.GetUserById(lsUserId, lsClientId);
                    lsUserDetails = _UserDetailsAction.GetUserDetailsByUserId(lsUserId);




                    if (lsCandidate != null)
                    {

                        ViewBag.UserName = lsCandidate.Username;
                        if (lsUserDetails != null)
                        {
                            ViewBag.IsUserDetails = true;
                            IsCandidateProfile = true;

                        }
                        else
                        {
                            ViewBag.IsUserDetails = false;

                        }

                        if (ViewBag.ClientName == null)
                        {
                            ViewBag.ClientName = lsCandidate.ClientIdName;
                        }

                        ViewBag.ClientId = lsCandidate.ClientId;
                        ViewBag.ContactId = lsCandidate.UserId;



                        if (lsCandidate.IsActive)
                        {
                            ViewBag.Message = "You have been already a registered user. Please login to explore more features.";
                        }
                        else
                        {
                            ViewBag.UserName = lsCandidate.Username;
                            ViewBag.Password = lsCandidate.Password;
                            bool isRecordUpdated = false;

                            BLClient.UserAction _UserAction = new BLClient.UserAction(lsClientId);
                            if (lsUserDetails != null)
                            {
                                isRecordUpdated = _UserAction.CompleteRegistration(new BEClient.User() { UserId = lsUserId }, true);

                                if (isRecordUpdated)
                                {
                                    ViewBag.Message = "User Registered Successfully ,Login to Continue..";
                                }
                                else
                                {
                                    ViewBag.Message = "Failed To Register User ,Contact Admin";
                                }
                            }
                            else
                            {
                                ViewBag.Message = "User Registered Successfully ,Click on Join to Continue..";
                            }
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Oops, we are sorry, your sign up is not complete successfully. Please sign up to activate your account.";
                    }
                    if (IsCandidateProfile && !lsCandidate.IsActive && ViewBag.IsExpired == false)
                    {

                        LogOnModel LoginModuleCandidate = new LogOnModel();
                        LoginModuleCandidate.UserName = ViewBag.UserName;
                        LoginModuleCandidate.Password = ATSHelper.Encryption.EncryptionAlgo.DecryptData(lsCandidate.Password);
                        Login(LoginModuleCandidate, string.Empty);
                        string SuccessMessage = "Thank you. Your account is now active for the email address" + " " + LoginModuleCandidate.UserName;
                        return RedirectToAction("GetLatestProfile", "MyProfile", new { area = Common.Constants.AREA_CANDIDATE, SuccessMsg = SuccessMessage });
                    }
                }
            }
            return View();
        }

        private String RedirectToCreateNewProfile(string Company)
        {
            UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);
            return u.Action("CreateProfile", "MyProfile", new { area = ATS.WebUi.Common.Constants.AREA_CANDIDATE });
        }

        public ActionResult ActiveUsers(String username)
        {

            BEClient.ReActivate objReActivate = new BEClient.ReActivate();
            objReActivate.UserName = username;
            return View(objReActivate);
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(SignUpModel signModule)
        {
            try
            {
                if (ATSCommon.CommonFunctions.ForgotPassword(this.ControllerContext.RequestContext, signModule))
                {
                    return GetJson(base.GetJsonContent(false, null, "Please check your email account and follow the link."));
                }
                else
                {
                    return GetJson(base.GetJsonContent(true, null, "The Email address you entered is not valid. Please try again:"));
                }
            }
            catch (Exception ex)
            {
                return GetJson(base.GetJsonContent(true, null, ex.Message));
            }
        }
        public ActionResult ResetPassword()
        {
            Guid lsContactId = Guid.Empty;
            Guid lsClientId = Guid.Empty;
            DateTime dateExpires = new DateTime();

            if (string.IsNullOrEmpty(Request.Params["id"]) == false)
            { Guid.TryParse(HelperLibrary.Encryption.EncryptionAlgo.DecryptData(Request.Params["id"]), out lsContactId); }

            if (string.IsNullOrEmpty(Request.Params["expires"]) == false)
            { dateExpires = new DateTime(Convert.ToInt64(Request.Params["expires"]), DateTimeKind.Utc); }
            String clientName = string.Empty;
            if (string.IsNullOrEmpty(Request.Params["Company"]) == false)
            { ViewBag.ClientName = clientName = Request.Params["Company"]; }

            if (string.IsNullOrEmpty(Request.Params["CompanyId"]) == false)
            { Guid.TryParse(HelperLibrary.Encryption.EncryptionAlgo.DecryptData(Request.Params["CompanyId"]), out lsClientId); }

            ViewBag.IsExpired = false;

            if (dateExpires < DateTime.UtcNow)
            {
                ViewBag.IsExpired = true;
            }

            if (lsContactId != Guid.Empty)
            {
                BEClient.User lsCandidate = BLClient.Common.LoginOperation.GetUserById(lsContactId, lsClientId);

                if (lsCandidate != null)
                {
                    /*Create client session if not exists*/
                    BEMaster.Client objClient = BusinessLogic.Common.LoginOperation.CreateClientSession(clientName);

                    if (objClient != null)
                    {
                        new ATSCommon.CurrentSession(objClient);
                    }
                    else
                    {
                        return View();
                    }

                    /*End session Creation*/

                    ViewBag.UserName = lsCandidate.Username;
                    if (ViewBag.ClientName == null)
                    {
                        ViewBag.ClientName = lsCandidate.ClientIdName;
                    }

                    ViewBag.ClientId = lsCandidate.ClientId;
                    ViewBag.ContactId = lsCandidate.UserId;
                    ViewBag.UserName = lsCandidate.Username;
                    ViewBag.Password = lsCandidate.Password;
                }
                else { return RedirectToLoginPage(clientName); }
            }


            return View();
        }

        private String RedirectToResetPassword(string Company, string encConactId, string encExpires, string encclientId)
        {
            UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);
            return u.Action("ResetPassword", "Home", new
            {
                company = Company,
                area = WebUi.Common.Constants.AREA_ROOT,
                id = encConactId,
                expires = encExpires,
                CompanyId = encclientId //,
                //cname = Company
            });
        }

        [HttpPost]
        public ActionResult ResetPassword(SignUpModel signModule)
        {

            signModule.ClientId = WebUi.Common.CurrentSession.Instance.VerifiedClient.ClientId;
            BEClient.User objUser = BLClient.Common.LoginOperation.ValidateUserName(signModule.UserName, signModule.ClientId);
            if (objUser != null && objUser.UserId != Guid.Empty)
            {
                BEClient.User objuser = new BEClient.User();
                BLClient.UserAction _objUserAction = new BLClient.UserAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                if (signModule.UserName != null)
                {
                    objuser = _objUserAction.GetUserIdByUserName(signModule.UserName);

                    if (BLClient.Common.LoginOperation.ResetPassword(objuser.UserId, HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(signModule.Password), ATSCommon.CurrentSession.Instance.VerifiedClient.ClientId))
                    {
                        BLClient.Common.LoginOperation.Login(objuser.UserId, ATSCommon.CurrentSession.Instance.VerifiedClient.ClientId);
                        return GetJson(base.GetJsonContent(false, RedirectToAuthenticatedUser()));
                    }
                }
                return View();
            }
            return GetJson(base.GetJsonContent(true, null, "The Email address you entered is not valid"));
        }
        private String RedirectToAuthenticatedUser()
        {
            UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);
            return u.Action("Index", "Account", new { area = WebUi.Common.Constants.AREA_ROOT });
        }

        public void SendActivationLink(Guid UserId)
        {
            if (!UserId.Equals(Guid.Empty))
            {
                BEClient.User objuser = new BEClient.User();
                BLClient.UserAction _objUserAction = new BLClient.UserAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                objuser = _objUserAction.GetRecordById(UserId);
                string encrContactid = ATSHelper.Encryption.EncryptionAlgo.EncryptionData(Convert.ToString(UserId));
                string expireDateTicks = Convert.ToString(DateTime.UtcNow.AddHours(ATSCommon.Constants.EMAIL_EXPIRY_LINK).Ticks);
                string encrCname = ATSCommon.CurrentSession.Instance.VerifiedClient.Clientname;
                string encrClientid = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(Convert.ToString(ATSCommon.CurrentSession.Instance.VerifiedClient.ClientId));

                var activationlink = Common.CommonFunctions.GenerateWebLink(RedirectToCompleteRegistration(encrCname, encrContactid, expireDateTicks, encrClientid));

                Mailers.EmailTemplate mvcMailer = new Mailers.EmailTemplate();
                mvcMailer.SendCandidateRegistrationMail(objuser.Username, activationlink, objuser.Username, ATSCommon.CurrentSession.Instance.VerifiedClient.Clientname);
            }
        }
        public ActionResult ResendActivationLink(Guid UserId)
        {
            SendActivationLink(UserId);
            return RedirectToAction("SignUpSuccess");
        }
        public ActionResult ResendActivationLinkForSignUp(SignUpModel ObjSignUpModel)
        {

            BEClient.User objuser = new BEClient.User();
            BLClient.UserAction _objUserAction = new BLClient.UserAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
            if (ObjSignUpModel.UserName != null)
            {
                objuser = _objUserAction.GetUserIdByUserName(ObjSignUpModel.UserName);

            }
            return this.ResendActivationLink(objuser.UserId);
        }

        [HttpPost]
        public JsonResult FBLogin(string Email, string FName, string LName)
        {
            try
            {
                bool IsError = false;
                BLClient.UserAction _objUserAction = new BLClient.UserAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                BEClient.User objUser = _objUserAction.GetUserByEmailId(Email);
                if (objUser != null)
                {
                    LogOnModel logonModule = new LogOnModel();
                    logonModule.UserName = Email;
                    logonModule.Password = ATSHelper.Encryption.EncryptionAlgo.DecryptData(objUser.Password);
                    Login(logonModule, "", false);
                    //return RedirectToAction("Index", "Home", new { area = Common.Constants.AREA_CANDIDATE });
                    IsError = false;
                }
                else
                {
                    String Message = String.Empty;
                    String Data = String.Empty;
                    //string _Password = DateTime.Now.Ticks.ToString().Substring(0, 8);
                    string _Password = "12345678";
                    try
                    {
                        BEClient.CandidateProfile objCandidateProfile = new BEClient.CandidateProfile();
                        BLClient.UserAction objUserAction = new BLClient.UserAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                        string password = ATSHelper.Encryption.EncryptionAlgo.EncryptionData(_Password);
                        string oResult = string.Empty;
                        Guid newUserId = BLClient.Common.LoginOperation.CandidateSignUp(Email, password, ATSCommon.CurrentSession.Instance.VerifiedClient.ClientId, out oResult, ATSCommon.CurrentSession.Instance.VerifiedClient.CurrentLanguageId, true);
                        if (newUserId != Guid.Empty)
                        {
                            BEClient.AnonymousUser anonymoususer = new BEClient.AnonymousUser();
                            anonymoususer.ObjUser = new BEClient.User();
                            anonymoususer.ObjCandidate = new BEClient.CandidateProfile();
                            anonymoususer.ObjCandidate.objUserDetails = new BEClient.UserDetails();

                            anonymoususer.ObjUser.Username = Email;
                            anonymoususer.ObjCandidate.objUserDetails.FirstName = FName;
                            anonymoususer.ObjCandidate.objUserDetails.LastName = LName;
                            anonymoususer.ObjCandidate.objUserDetails.Password = _Password;
                            anonymoususer.ObjCandidate.objUserDetails.WorkEmail = Email;

                            objUser = BLClient.Common.LoginOperation.ValidateLogin(Email, password, Common.CurrentSession.Instance.VerifiedClient.ClientId);
                            ATSCommon.CookieOperation.CookieBasedOnRequest(ATSCommon.Constants.COOKIE_CURRENT_USER, newUserId.ToString(), ATSCommon.Constants.COOKIE_MIN, Response, Request);
                            objUser.ObjUserDetails = anonymoususer.ObjCandidate.objUserDetails;
                            new ATSCommon.CurrentSession(objUser);
                            BLClient.UserDetailsAction objUserDetailsAction = new BLClient.UserDetailsAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                            //anonymoususer.ObjCandidate.objProfile = new BEClient.Profile();
                            //anonymoususer.ObjCandidate.objProfile.ProfileName = FName + LName + "1";
                            anonymoususer.ObjCandidate.objUserDetails.UserId = newUserId;
                            //anonymoususer.ObjCandidate.objProfile.UserId = newUserId;
                            //anonymoususer.ObjCandidate.objProfile.ClientId = Common.CurrentSession.Instance.VerifiedClient.ClientId;
                            Guid newUserDetailsId = Guid.Empty;
                            newUserDetailsId = objUserDetailsAction.AddAnonymousUserDetails(anonymoususer.ObjCandidate.objUserDetails);

                            return GetJson(base.GetJsonContent(false, RedirectToCandidateHome()));

                        }
                        else
                        {
                            IsError = true;
                            return GetJson(base.GetJsonContent(IsError, null, "User is already registered"));
                        }
                    }
                    catch (Exception ex)
                    {
                        IsError = true;
                        return GetJson(base.GetJsonContent(IsError, null, ex.Message));
                    }
                }

                return GetJson(base.GetJsonContent(IsError, null, ""));
            }
            catch (Exception ex)
            {
                return GetJson(base.GetJsonContent(true, null, ex.Message));
            }
        }

        [HttpPost]
        public JsonResult ChangePassword(ATS.WebUi.Models.SignUpModel objSignupModel)
        {
            String Message = string.Empty;
            String Data = string.Empty;
            bool IsError = false;
            try
            {

            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;

            }

            return GetJson(GetJsonContent(IsError, null, Message, Data));
        }
        [HttpPost]
        public JsonResult ChangeUserPassword(ATS.WebUi.Models.SignUpModel objSignupModel)
        {
            String Message = string.Empty;
            String Data = string.Empty;
            string _Password = string.Empty;
            string _newpassword = string.Empty;
            bool IsError = false;
            try
            {
                if (objSignupModel != null)
                {
                    if (objSignupModel.UserId != null && objSignupModel.UserId != Guid.Empty && objSignupModel.Password != null)
                    {
                        _Password = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(objSignupModel.Password);
                        _newpassword = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(objSignupModel.NewPassword);
                        bool result = false;
                        BLClient.UserAction objUserAction = new BLClient.UserAction(ATS.WebUi.Common.CurrentSession.Instance.VerifiedClient.ClientId);
                        result = objUserAction.ChangeUserPassword(objSignupModel.UserId, _Password, _newpassword);
                        if (result)
                        {
                            IsError = false;
                            Message = "password changed Successfully!";
                        }
                        else
                        {
                            IsError = true;
                            Message = "password not changed";

                        }
                    }
                }
                else
                {
                    IsError = true;
                    Message = "Password not updated";
                }
            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;
            }
            return GetJson(GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }
    }
}
