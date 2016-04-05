using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLMaster = ATS.BusinessLogic.Master;
using BEMaster = ATS.BusinessEntity.Master;
using BEClient = ATS.BusinessEntity;
using BLClient = ATS.BusinessLogic;
using BLCommon = ATS.BusinessLogic.Common;
using ATSCommon = ATS.WebUi.Common;
using ATSHelper = ATS.HelperLibrary;
using ATS.HelperLibrary;
using log4net;
using BECommon = ATS.BusinessEntity.Common;
using ATS.WebUi.Models;
using ATS.WebUi.Areas.Candidate.Models;

namespace ATS.WebUi.Controllers
{
    public partial class AccountController : ATS.WebUi.Controllers.AreaBaseController
    {
        public static readonly ILog log = LogManager.GetLogger(
    System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public AccountController()
        {
        }

        private void NavIndex()
        {
            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = null;
            List<BEClient.BreadCrumb> objBreadCrumblst = new List<BEClient.BreadCrumb>();
            BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
            objBreadCrumb.Action = "Index";
            objBreadCrumb.Controller = "Account";
            objBreadCrumb.AreaName = "";
            objBreadCrumb.ordinal = 1;

            objBreadCrumb.URL = Url.Action("Index", "Account", new { area = ATS.WebUi.Common.Constants.AREA_ROOT });
            objBreadCrumb.ImagePath = BECommon.ImagePaths.HomeImage;
            objBreadCrumb.ToolTip = "Home";
            objBreadCrumb.WithoutOrdinalURL = Url.Action("Index", "Account", new { area = ATS.WebUi.Common.Constants.AREA_ROOT });

            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);

            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
        }

        public ActionResult Index()
        {
            log.Debug("Accout => Index Call ");

            if (ATS.WebUi.Common.CurrentSession.Instance != null && ATS.WebUi.Common.CurrentSession.Instance.VerifiedUser != null && ATS.WebUi.Common.CurrentSession.Instance.VerifiedClient != null)
            {
                BEClient.User objUser = ATS.WebUi.Common.CurrentSession.Instance.VerifiedUser;
                log.Debug("Get USer Instance");

                if (objUser != null)
                {
                    if (objUser.SecurityRoles != null && objUser.SecurityRoles.Where(x => x.AtsSecurityRole == BEClient.ATSSecurityRole.Candidate).Count() > 0)
                    {
                        return RedirectToAction("Index", "Home", new { Area = ATSCommon.Constants.AREA_CANDIDATE });
                    }
                    else
                    {
                        return RedirectToAction(Common.Constants.DEFAULT_REDIRECT_EMPLOYEE_ACTION, Common.Constants.DEFAULT_REDIRECT_EMPLOYEE_CONTROLLER, new { Area = ATSCommon.Constants.AREA_EMPLOYEE });
                    }
                }
            }

            if (ATS.WebUi.Common.CurrentSession.Instance != null && ATS.WebUi.Common.CurrentSession.Instance.VerifiedUser == null && ATS.WebUi.Common.CurrentSession.Instance.VerifiedClient != null)
            {
                BLClient.CompanyInfoAction objCompanyInfoAction = new BLClient.CompanyInfoAction(ATS.WebUi.Common.CurrentSession.Instance.VerifiedClient.ClientId);
                ATS.WebUi.Common.CurrentSession.Instance.VerifiedClient.objCompanyInfo = objCompanyInfoAction.GetCompanyInfoDetails();
            }

            BreadScrumbModel<DBNull> objBCModel = new BreadScrumbModel<DBNull>();
            objBCModel.DisplayName = BECommon.HomeConstant.FEATURED + " " + BECommon.CommonConstant.JOBS;
            objBCModel.ImagePath = BECommon.ImagePaths.FeaturedVacancyImage;
            objBCModel.ToolTip = "Featured Jobs";
            NavIndex();
            return View(objBCModel);
        }

        public ActionResult LanguageList()
        {
            try
            {
                List<BEMaster.ClientLanguage> objClientLanguage = new List<BEMaster.ClientLanguage>();
                BLMaster.ClientLanguageAction objClientLanguageAction = new BLMaster.ClientLanguageAction();

                if (ATSCommon.CurrentSession.Instance.VerifiedClient.ClientLanguageList == null)
                {
                    objClientLanguage = objClientLanguageAction.GetLanguageByClientId(base.CurrentClient.ClientId);
                    ATSCommon.CurrentSession.Instance.VerifiedClient.ClientLanguageList = objClientLanguage;
                }
                else
                {
                    objClientLanguage = ATSCommon.CurrentSession.Instance.VerifiedClient.ClientLanguageList;
                }

                return PartialView("_LanguageControl", objClientLanguage);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult AnonymousUserProfile(string oResult, string VacancyId = null)
        {
            Guid VacId = Guid.Empty;
            BEClient.AnonymousUser objAnonymousUser = new BEClient.AnonymousUser();
            BreadScrumbModel<BEClient.AnonymousUser> objbreadscrumb = new BreadScrumbModel<BEClient.AnonymousUser>();

            if (VacancyId != null)
            {
                if (Common.CurrentSession.Instance.VerifiedUser != null)
                {
                    return RedirectToAction("ApplyVacancy", "Candidate", new { VacancyId = VacancyId });
                }

                Guid.TryParse(VacancyId, out VacId);
                BLClient.VacancyAction ObjVacAction = new BLClient.VacancyAction(CurrentClient.ClientId);
                objAnonymousUser.ObjVacancy = new BEClient.Vacancy();
                objAnonymousUser.ObjVacancy = ObjVacAction.GetVacancyById(VacId, CurrentClient.CurrentLanguageId);
                objAnonymousUser.ObjVacancy.ShowOnWebBenefits = false;
                objAnonymousUser.ObjVacancy.ShowOnWebDuties = false;
                objAnonymousUser.ObjVacancy.ShowOnWebJobDescription = false;
                objAnonymousUser.ObjVacancy.ShowOnWebSkills = false;
                objbreadscrumb.ImagePath = BECommon.ImagePaths.ApplyImage;
                objbreadscrumb.DisplayName = objAnonymousUser.ObjVacancy.JobTitle + ", <span style='color: #953634;'>" + objAnonymousUser.ObjVacancy.LocationText + "</span>";

                ViewBag.VacancyId = objAnonymousUser.ObjVacancy.VacancyId;
            }
            else
            {
                objbreadscrumb.ImagePath = BECommon.ImagePaths.UploadResumeImage;
                objbreadscrumb.DisplayName = "Upload Resume";

                //For Upload Resume without Login:Anand 
                ViewBag.VacancyId = Guid.Empty;
            }

            log.Debug("AnonymousUserProfile call");
            objbreadscrumb.obj = objAnonymousUser;
            NavIndex();

            //Error when no login
            return PartialView("_AnonymousUser", objbreadscrumb);
        }

        private String RedirectToCompleteRegistration(string Company, string encConactId, string encExpires, string encClientId)
        {
            log.Debug("RedirectToCompleteRegistration call");
            UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);

            return u.Action("CompleteRegistration", "Home", new
            {
                company = Company,
                area = WebUi.Common.Constants.AREA_ROOT,
                id = encConactId,
                expires = encExpires,
                CompanyId = encClientId//,

            });
        }

        public JsonResult SaveAnonymsUserDetails(BEClient.AnonymousUser anonymoususer, bool allowParse)
        {
            try
            {
                log.Debug("SaveAnonymsUserDetails call");
                BEClient.CandidateProfile objCandidateProfile = new BEClient.CandidateProfile();
                BLClient.UserAction objUserAction = new BLClient.UserAction(base.CurrentClient.ClientId);
                string password = ATSHelper.Encryption.EncryptionAlgo.EncryptionData(anonymoususer.ObjUser.Password);
                string oResult = string.Empty;
                Guid newUserId = BLClient.Common.LoginOperation.CandidateSignUp(anonymoususer.ObjUser.Username, password, ATSCommon.CurrentSession.Instance.VerifiedClient.ClientId, out oResult, ATSCommon.CurrentSession.Instance.VerifiedClient.CurrentLanguageId, true);
                BLClient.Common.LoginOperation.CandidateOrganization(anonymoususer.ObjOrganization.OrganizationName, newUserId, ATSCommon.CurrentSession.Instance.VerifiedClient.ClientId);
                if (newUserId != Guid.Empty)
                {
                    try
                    {
                        String WebLink = string.Empty;
                        BLClient.UserDetailsAction objUserDetailsAction = new BLClient.UserDetailsAction(base.CurrentClient.ClientId);
                        anonymoususer.ObjCandidate.objProfile = new BEClient.Profile();
                        anonymoususer.ObjCandidate.objProfile.ProfileName = anonymoususer.ObjCandidate.objUserDetails.FirstName + anonymoususer.ObjCandidate.objUserDetails.LastName + "1";
                        anonymoususer.ObjCandidate.objUserDetails.UserId = newUserId;
                        anonymoususer.ObjCandidate.objProfile.UserId = newUserId;
                        anonymoususer.ObjCandidate.objProfile.ClientId = base.CurrentClient.ClientId;

                        Guid newUserDetailsId = Guid.Empty;
                        try
                        {
                            newUserDetailsId = objUserDetailsAction.AddAnonymousUserDetails(anonymoususer.ObjCandidate.objUserDetails);
                            log.Debug("newUserDetailsId call" + newUserDetailsId.ToString());
                        }
                        catch
                        {
                            throw;
                        }
                        if (newUserDetailsId != Guid.Empty)
                        {
                            string encrContactid = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(Convert.ToString(newUserId));
                            string expireDateTicks = Convert.ToString(DateTime.UtcNow.AddHours(WebUi.Common.Constants.EMAIL_EXPIRY_LINK).Ticks);
                            string encrCname = Common.CurrentSession.Instance.VerifiedClient.Clientname;
                            string encrClientid = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(Convert.ToString(base.CurrentClient.ClientId));

                            if (anonymoususer.ObjUser.FileUploadType == BEClient.UploadFileType.LocalDevice.ToString() || (anonymoususer.ObjUser.FileUploadType == null && Request.Files[0] != null))
                            {
                                HttpPostedFileBase documentFile = Request.Files[0] as HttpPostedFileBase;
                                objCandidateProfile = ATSCommon.CommonFunctions.GenerateProfile(anonymoususer.ObjCandidate.objProfile, documentFile, allowParse);
                            }
                            else if (anonymoususer.ObjUser.FileUploadType == BEClient.UploadFileType.MSDropBox.ToString())
                            {
                                objCandidateProfile = ATSCommon.CommonFunctions.GenerateProfile(anonymoususer.ObjCandidate.objProfile, anonymoususer.ObjUser, allowParse);
                            }
                            else if (anonymoususer.ObjUser.FileUploadType == BEClient.UploadFileType.GoogleDrive.ToString())
                            {
                                objCandidateProfile = ATSCommon.CommonFunctions.GenerateProfile(anonymoususer.ObjCandidate.objProfile, anonymoususer.ObjUser, allowParse);
                            }
                            else
                            {
                                //No file selected.
                            }

                            log.Debug("Create Session  ");
                            //Create Session 
                            string SessionProfileId = objCandidateProfile.objProfile.ProfileId.ToString();
                            Session[SessionProfileId] = objCandidateProfile;
                            log.Debug("Session created");
                            WebLink = Url.Action("SignUpSuccess", "Home");
                            log.Debug("WebLink " + WebLink);

                            // Anand: for Insertion of Application in draft without login
                            if (anonymoususer.ObjVacancy != null && anonymoususer.ObjVacancy.VacancyId != Guid.Empty)
                            {
                                if (objCandidateProfile != null)
                                {
                                    BEClient.Application ObjApp = new BEClient.Application();
                                    ObjApp.VacancyId = anonymoususer.ObjVacancy.VacancyId;
                                    ObjApp.ATSResumeId = objCandidateProfile.objATSResume.ATSResumeId;
                                    ObjApp.LanguageId = base.CurrentClient.CurrentLanguageId;
                                    Common.CommonFunctions.ApplicationStatus = new Dictionary<string, string>();
                                    ObjApp.ApplicationStatus = BEClient.Common.ApplicationStatusOptionsConstant.APP_STAT_DRAFT.ToString();
                                    ObjApp.ATSCoverLetterId = objCandidateProfile.objATSResume.CoverLetterId;
                                    BLClient.ApplicationAction objApplicationAction = new BLClient.ApplicationAction(base.CurrentClient.ClientId);
                                    Guid ApplicationId = objApplicationAction.InsertApplicationWithoutLogin(ObjApp, newUserId);
                                    
                                    if (ApplicationId == Guid.Empty)
                                    {
                                        throw new Exception("Vacancy Not applied Error");
                                    }

                                    //Rutul Patel : Get All Vac Rnd and Insert into Promote Candidate
                                    BLClient.VacancyRoundAction VacRndAction = new BLClient.VacancyRoundAction(base.CurrentClient.ClientId);
                                    List<BEClient.VacancyRound> VacRndList = VacRndAction.GetAllRoundByApplicationId(ApplicationId);
                                    BLClient.PromoteCandidateAction PCAction = new BLClient.PromoteCandidateAction(base.CurrentClient.ClientId);
                                    BEClient.PromoteCandidate ObjPC = new BEClient.PromoteCandidate();
                                    ObjPC.ApplicationId = ApplicationId;
                                    ObjPC.IsActive = false;
                                    ObjPC.IsPromoted = false;
                                    
                                    foreach (BEClient.VacancyRound _current in VacRndList)
                                    {
                                        ObjPC.VacRndId = _current.VacancyRoundId;
                                        Guid Result = PCAction.InsertPromoteCandidateWithoutLogin(ObjPC, newUserId);
                                    }

                                    string LstRndTypeQTSelf = GetRndTypeIdByCandidateSelfEvalution();
                                    BLClient.QuestionDetailAction _objQuestionDetailAction = new BLClient.QuestionDetailAction(base.CurrentClient.ClientId);
                                    List<BEClient.QuestionDetails> _objQuestionDetails = _objQuestionDetailAction.GetQuestionDetails(ApplicationId, LstRndTypeQTSelf);
                                    
                                    if (_objQuestionDetails.Count == 0)
                                    {
                                        string RndType = GetRndTypeIdByCandidateSelfEvalution();
                                        BEClient.VacancyRound ObjVacRnd = VacRndAction.GetVacRndByAppIdAndRndTypeId(ApplicationId, RndType);
                                    
                                        if (ObjVacRnd != null)
                                        {
                                            Guid VacRndId = ObjVacRnd.VacancyRoundId;
                                            InsertScheduleInt(ApplicationId, VacRndId, newUserId);
                                            UpdatePromoteCandidateForActive(ApplicationId, VacRndId, newUserId);
                                        }
                                        else
                                        {
                                            if (VacRndList.Count > 0)
                                            {
                                                ActiveNextRound(ApplicationId, newUserId);
                                            }
                                        }
                                        
                                        BLClient.ApplicationAction _ObjApplicationAction = new BLClient.ApplicationAction(base.CurrentClient.ClientId);
                                        bool Result = _ObjApplicationAction.ChangeApplicationStatusWithoutLogin(ApplicationId, BECommon.ApplicationStatusOptionsConstant.APP_STAT_SUBMIT, newUserId);
                                    }
                                }
                            }
                        }
                        else
                        {
                            log.Debug("ERROR :: UserDetail not added");
                            throw new Exception("UserDetail not added");
                        }
                        log.Debug("Json return");
                        return GetJson(base.GetJsonContent(false, WebLink, oResult));
                    }
                    catch
                    {
                        throw;
                    }
                }
                else
                {
                    if (oResult == "105")
                    {
                        HomeController homeController = new HomeController();
                        homeController.ControllerContext = this.ControllerContext;
                        BEClient.User objuser = new BEClient.User();
                        BLClient.UserAction _objUserAction = new BLClient.UserAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                        
                        if (!String.IsNullOrEmpty(anonymoususer.ObjUser.Username))
                        {
                            objuser = _objUserAction.GetUserIdByUserName(anonymoususer.ObjUser.Username);
                            homeController.SendActivationLink(objuser.UserId);
                            return GetJson(base.GetJsonContent(false, Url.Action("SignUpSuccess", "Home", new { Area = "", @Username = anonymoususer.ObjUser.Username })));
                        }
                       
                        return GetJson(base.GetJsonContent(false, Url.Action("ActiveUsers", "Home", new { Area = "", username = anonymoususer.ObjUser.Username }), "User is registered but not activated."));
                    }
                    else
                    {
                        return GetJson(base.GetJsonContent(true, null, "User is already registered."));
                    }
                }
            }
            catch (Exception ex)
            {
                log.Debug("SaveAnonymsUserDetails call" + ex.Message);
                return GetJson(base.GetJsonContent(true, null, ex.Message));
            }
        }

        public ActionResult EmployeeVacancyLink(Guid VacancyId)
        {
            try
            {
                if (Common.CurrentSession.Instance.VerifiedUser != null)
                {
                    if (!Common.CurrentSession.Instance.VerifiedUser.DivisionId.Equals(Guid.Empty))
                    {
                        return RedirectToAction("View", "MyVacancy", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, id = VacancyId });
                    }
                    else
                    {
                        ATS.WebUi.Common.CurrentSession.Instance.VerifiedClient.VacancyId = VacancyId;
                        return RedirectToAction("EmployeeLogin", "Home", new { area = ATS.WebUi.Common.Constants.AREA_ROOT });
                    }
                }
                else
                {
                    ATS.WebUi.Common.CurrentSession.Instance.VerifiedClient.VacancyId = VacancyId;
                    return RedirectToAction("EmployeeLogin", "Home", new { area = ATS.WebUi.Common.Constants.AREA_ROOT });
                }
            }
            catch
            {
                throw;
            }
        }

        public string GetRndTypeIdByCandidateSelfEvalution()
        {
            BLClient.RndTypeAction _objRndTypeAction = new BLClient.RndTypeAction(base.CurrentClient.ClientId);
            List<Guid> lstRndType = _objRndTypeAction.GetRndTypeIdByCandidateSelfEvalution();
            String lstRndTypeStr = String.Join(",", lstRndType.Select(g => g.ToString()).ToArray());
            return lstRndTypeStr;
        }

        private void ActiveNextRound(Guid ApplicationId, Guid _UserId)
        {
            BLClient.PromoteCandidateAction PCAction = new BLClient.PromoteCandidateAction(base.CurrentClient.ClientId);
            
            //Get Next Round
            Guid VacRndId = PCAction.GetFirstVacRndIdByApplicationId(ApplicationId);
            
            //Update Promote Candidate
            BEClient.PromoteCandidate ObjPC = new BEClient.PromoteCandidate();
            ObjPC.ApplicationId = ApplicationId;
            ObjPC.VacRndId = VacRndId;
            ObjPC.VacRndId = VacRndId;
            Boolean ResultPC = PCAction.UpdatePromoteCandidateWithoutLogin(ObjPC, _UserId);
        }

        private void UpdatePromoteCandidateForActive(Guid ApplicationId, Guid VacRndId, Guid _UserId)
        {
            //Update Promote Candidate
            BLClient.PromoteCandidateAction PCAction = new BLClient.PromoteCandidateAction(base.CurrentClient.ClientId);
            BEClient.PromoteCandidate ObjPC = new BEClient.PromoteCandidate();
            ObjPC.ApplicationId = ApplicationId;
            ObjPC.VacRndId = VacRndId;
            ObjPC.VacRndId = VacRndId;
            Boolean ResultPC = PCAction.UpdatePromoteCandidateWithoutLogin(ObjPC, _UserId);
        }

        private void InsertScheduleInt(Guid ApplicationId, Guid VacRndId, Guid _UserId)
        {
            BLClient.ScheduleIntAction objScheduleIntAction = new BLClient.ScheduleIntAction(base.CurrentClient.ClientId);
            BEClient.ScheduleInt ScheduleInt = new BEClient.ScheduleInt();
            ScheduleInt.ApplicationId = ApplicationId;
            ScheduleInt.VacRndId = VacRndId;
            Guid ResultSI = objScheduleIntAction.AddSaveScheduleIntWithoutLogin(ScheduleInt, _UserId);
        }

        #region CA Login

        [HttpPost]
        public ActionResult CASaveAnonymsUserDetails(BEClient.AnonymousUser anonymoususer)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                log.Debug("SaveAnonymsUserDetails call");
                BEClient.CandidateProfile objCandidateProfile = new BEClient.CandidateProfile();
                BLClient.UserAction objUserAction = new BLClient.UserAction(base.CurrentClient.ClientId);
                string password = ATSHelper.Encryption.EncryptionAlgo.EncryptionData(anonymoususer.ObjUser.Password);
                string oResult = string.Empty;
                Guid newUserId = BLClient.Common.LoginOperation.CandidateSignUp(anonymoususer.ObjUser.Username, password, ATSCommon.CurrentSession.Instance.VerifiedClient.ClientId, out oResult, ATSCommon.CurrentSession.Instance.VerifiedClient.CurrentLanguageId, true);
                BLClient.Common.LoginOperation.CandidateOrganization(anonymoususer.ObjOrganization.OrganizationName, newUserId, ATSCommon.CurrentSession.Instance.VerifiedClient.ClientId);
                if (newUserId != Guid.Empty)
                {
                    BEClient.User objUser = BLClient.Common.LoginOperation.ValidateLogin(anonymoususer.ObjUser.Username, password, Common.CurrentSession.Instance.VerifiedClient.ClientId);
                    ATSCommon.CookieOperation.CookieBasedOnRequest(ATSCommon.Constants.COOKIE_CURRENT_USER, newUserId.ToString(), ATSCommon.Constants.COOKIE_MIN, Response, Request);
                    objUser.ObjUserDetails = anonymoususer.ObjCandidate.objUserDetails;
                    new ATSCommon.CurrentSession(objUser);
                    BLClient.UserDetailsAction objUserDetailsAction = new BLClient.UserDetailsAction(base.CurrentClient.ClientId);
                    anonymoususer.ObjCandidate.objProfile = new BEClient.Profile();
                    anonymoususer.ObjCandidate.objProfile.ProfileName = anonymoususer.ObjCandidate.objUserDetails.FirstName + anonymoususer.ObjCandidate.objUserDetails.LastName + "1";
                    anonymoususer.ObjCandidate.objUserDetails.UserId = newUserId;
                    anonymoususer.ObjCandidate.objProfile.UserId = newUserId;
                    anonymoususer.ObjCandidate.objProfile.ClientId = base.CurrentClient.ClientId;
                    Guid newUserDetailsId = Guid.Empty;
                    newUserDetailsId = objUserDetailsAction.AddAnonymousUserDetails(anonymoususer.ObjCandidate.objUserDetails);
                    log.Debug("newUserDetailsId call" + newUserDetailsId.ToString());
                    if (anonymoususer.ObjVacancy != null && anonymoususer.ObjVacancy.VacancyId != Guid.Empty)
                    {
                        return GetJson(base.GetJsonContent(IsError, RedirectToStep2(anonymoususer.ObjVacancy.VacancyId)));
                    }
                    else
                    {
                        return GetJson(base.GetJsonContent(false, RedirectToCandidateHome()));
                    }
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
                log.Debug("SaveAnonymsUserDetails call" + ex.Message);
                return GetJson(base.GetJsonContent(IsError, null, ex.Message));
            }
        }

        private String RedirectToStep2(Guid VacancyId)
        {
            Session.Remove("LeadStep");
            UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);
            return u.Action("Index", "ApplyVacancy", new { VacancyId = VacancyId, IsNewUser = true, Area = ATSCommon.Constants.AREA_CANDIDATE });
        }
        private String RedirectToCandidateHome()
        {
            UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);
            return u.Action("Index", "Home", new { Area = ATSCommon.Constants.AREA_CANDIDATE });
        }
        #endregion
    }
}
