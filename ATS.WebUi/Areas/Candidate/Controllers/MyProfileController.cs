using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEClient = ATS.BusinessEntity;
using BLClient = ATS.BusinessLogic;
using ATSCommon = ATS.WebUi.Common;
using System.IO;
using ATS.HelperLibrary;
using ATS.WebUi.Models;
using Newtonsoft.Json;
using BLCommon = ATS.BusinessLogic.Common;
using log4net;
using BECommon = ATS.BusinessEntity.Common;
using RootModels = ATS.WebUi.Models;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ATS.WebUi.Areas.Candidate.Controllers
{
    public class MyProfileController : ATS.WebUi.Controllers.AreaBaseController
    {
        #region Private Members

        private BLClient.ProfileAction _objProfileAction;
        private Guid _CurrentClientId;
        private Guid _CurrentLanguageId;
        private Guid _UserId;
        private static readonly log4net.ILog log = LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region OnAuthorization Method call on every request
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (base.CurrentClient != null && Common.CurrentSession.Instance.VerifiedUser != null)
            {
                _UserId = ATSCommon.CurrentSession.Instance.UserId;
                _CurrentClientId = base.CurrentClient.ClientId;
                _CurrentLanguageId = base.CurrentClient.CurrentLanguageId;
                if (ATSCommon.CurrentSession.Instance.VerifiedUser.SecurityRoles.Where(x => x.AtsSecurityRole == BEClient.ATSSecurityRole.Candidate).Count() <= 0)
                {
                    filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_CANDIDATE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_CONTROLLER, new { area = ATSCommon.Constants.AREA_EMPLOYEE }));

                }
            }
        }
        #endregion

        public void NavGetLatestProfile()
        {
            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = null;
            List<BEClient.BreadCrumb> objBreadCrumblst = new List<BEClient.BreadCrumb>();
            BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
            objBreadCrumb.Action = "GetLatestProfile";
            objBreadCrumb.Controller = "MyProfile";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_CANDIDATE;
            objBreadCrumb.URL = Url.Action("GetLatestProfile", "MyProfile", new { area = ATS.WebUi.Common.Constants.AREA_CANDIDATE });
            objBreadCrumb.ordinal = 1;
            objBreadCrumb.ImagePath = BECommon.ImagePaths.ProfileImage;
            objBreadCrumb.ToolTip = "My profile";
            objBreadCrumb.WithoutOrdinalURL = Url.Action("GetLatestProfile", "MyProfile", new { area = ATS.WebUi.Common.Constants.AREA_CANDIDATE });
            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);

            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
        }
        public void NavGetUpdateProfile()
        {
            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = null;
            List<BEClient.BreadCrumb> objBreadCrumblst = new List<BEClient.BreadCrumb>();
            BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
            objBreadCrumb.Action = "GetLatestProfile";
            objBreadCrumb.Controller = "MyProfile";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_CANDIDATE;
            objBreadCrumb.URL = Url.Action("GetLatestProfile", "MyProfile", new { area = ATS.WebUi.Common.Constants.AREA_CANDIDATE });
            objBreadCrumb.ordinal = 1;
            objBreadCrumb.ImagePath = BECommon.ImagePaths.ProfileImage;
            objBreadCrumb.ToolTip = "My profile";
            objBreadCrumb.WithoutOrdinalURL = Url.Action("GetLatestProfile", "MyProfile", new { area = ATS.WebUi.Common.Constants.AREA_CANDIDATE });
            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);

            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
        }

        public void NavGetUploadProfile()
        {
            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = null;
            List<BEClient.BreadCrumb> objBreadCrumblst = new List<BEClient.BreadCrumb>();
            BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
            objBreadCrumb.Action = "Index";
            objBreadCrumb.Controller = "MyDocuments";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_CANDIDATE;
            objBreadCrumb.URL = Url.Action("Index", "MyDocuments", new { area = ATS.WebUi.Common.Constants.AREA_CANDIDATE });
            objBreadCrumb.ordinal = 1;
            objBreadCrumb.ImagePath = BECommon.ImagePaths.HomeImage;
            objBreadCrumb.ToolTip = "Home";
            objBreadCrumb.WithoutOrdinalURL = Url.Action("index", "Home", new { area = ATS.WebUi.Common.Constants.AREA_CANDIDATE });
            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);

            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
        }

        public ActionResult Index(RootModels.BreadScrumbModel<BEClient.CandidateProfile> Candidateprofile)
        {
            bool isServerError = false;
            if (!ModelState.IsValid)
            {
                bool isbreak = false;

                // do something to display errors. 
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        ViewBag.IsError = true;
                        ViewBag.Message = error.ErrorMessage;
                        isbreak = true;
                        isServerError = true;
                        break;
                    }
                    if (isbreak)
                    {
                        Dropdownlist();
                        break;
                    }
                }
                return View(Candidateprofile);
            }

            BLClient.UserAction objContactAction = new BLClient.UserAction(_CurrentClientId);
            BLClient.ProfileAction objProfileAction = new BLClient.ProfileAction(_CurrentClientId);
            if (Candidateprofile.obj.objProfile.ProfileId == Guid.Empty)
            {
                try
                {
                    Candidateprofile.obj.objProfile.UserId = _UserId;
                    objProfileAction.AddCandidateFullProfile(Candidateprofile.obj);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                try
                {
                    #region Delete Existing Attached file
                    BLClient.ATSResumeAction _objATSResumeAction = new BLClient.ATSResumeAction(_CurrentClientId);
                    BEClient.ATSResume _objATSResume = new BEClient.ATSResume();
                    string Existingfilepath = string.Empty;
                    if (!Candidateprofile.obj.objProfile.ProfileId.Equals(Guid.Empty))
                    {
                        _objATSResume = _objATSResumeAction.GetATSResumeByUserAndProfile(Candidateprofile.obj.objProfile.UserId, Candidateprofile.obj.objProfile.ProfileId);
                        Guid _ATSResumeId = Guid.Empty;
                        //It will delete existing file if profile update
                        if (_objATSResume != null && Common.CurrentSession.Instance.VerifiedUser.tempResume != null)
                        {
                            Existingfilepath = Path.Combine(Common.Constants.STR_RESUME_PATH, _objATSResume.NewFileName);
                            if (!string.IsNullOrEmpty(Existingfilepath))
                                System.IO.File.Delete(System.Web.HttpContext.Current.Server.MapPath(Existingfilepath));

                            //Assign existing id 
                            _ATSResumeId = _objATSResume.ATSResumeId;
                        }
                        //get ATSREsume from Session and make session null
                        if (Common.CurrentSession.Instance.VerifiedUser.tempResume != null)
                        {
                            _objATSResume = Common.CurrentSession.Instance.VerifiedUser.tempResume;
                            Common.CurrentSession.Instance.VerifiedUser.tempResume = null;
                            _objATSResume.ATSResumeId = _ATSResumeId;
                            Candidateprofile.obj.objATSResume = _objATSResume;
                        }
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            ViewBag.IsError = false;
            ViewBag.Message = "Record Save Successfully";
            Dropdownlist();
            return View(Candidateprofile);
        }

        [HttpGet]
        public ActionResult CreateProfile(string ErrMsg = null)
        {
            if (!String.IsNullOrEmpty(ErrMsg))
            {
                ViewBag.IsError = ErrMsg;
            }
            int DocCategory = 1;
            BEClient.DocumentType objDocumentType = new BEClient.DocumentType();
            BLClient.DocumentTypeAction objDocumentAction = new BLClient.DocumentTypeAction(_CurrentClientId);
            ViewBag.HasNoProfile = true;
            RootModels.BreadScrumbModel<BEClient.Profile> profile = new BreadScrumbModel<BEClient.Profile>();
            profile.obj = new BEClient.Profile();

            objDocumentType = objDocumentAction.GetDocTypeByDocCategory(DocCategory);
            if (objDocumentType != null)
            {
                profile.obj.DocumentTypeId = objDocumentType.DocumentTypeId;
                profile.obj.ExtensionTypes = objDocumentType.ExtensionTypes;
                profile.obj.DocCategoryType = objDocumentType.DocCategory;
            }
            profile.ImagePath = BECommon.ImagePaths.ProfileImage;
            profile.DisplayName = "My Profile";

            return View(profile);
        }


        [HttpGet]
        public ActionResult CreateProfileNav(string ErrMsg = null)
        {
            if (!String.IsNullOrEmpty(ErrMsg))
            {
                ViewBag.IsError = ErrMsg;
            }
            int DocCategory = 1;
            BEClient.DocumentType objDocumentType = new BEClient.DocumentType();
            BLClient.DocumentTypeAction objDocumentAction = new BLClient.DocumentTypeAction(_CurrentClientId);
            ViewBag.HasNoProfile = true;
            RootModels.BreadScrumbModel<BEClient.Profile> profile = new BreadScrumbModel<BEClient.Profile>();
            profile.obj = new BEClient.Profile();

            objDocumentType = objDocumentAction.GetDocTypeByDocCategory(DocCategory);
            if (objDocumentType != null)
            {
                profile.obj.DocumentTypeId = objDocumentType.DocumentTypeId;
                profile.obj.ExtensionTypes = objDocumentType.ExtensionTypes;
                profile.obj.DocCategoryType = objDocumentType.DocCategory;
            }
            profile.ImagePath = BECommon.ImagePaths.ProfileImage;
            profile.DisplayName = "My Profile";
            BLClient.ATSResumeAction objATSResumeAction = new BLClient.ATSResumeAction(_CurrentClientId);
            List<BEClient.ATSResume> objATSResumeList = objATSResumeAction.GetAllDocsByUserId(_UserId);
            ViewBag.AtsResumelst = new SelectList(objATSResumeList, "ATSResumeId", "UploadedFileName"); ;
            return View("_CreateProfileNav", profile);
        }




        public ActionResult UpdateResume(RootModels.BreadScrumbModel<BEClient.Profile> profile, HttpPostedFileBase documentFile, bool allowParse = true)
        {
            BEClient.CandidateProfile _objCandidateProfile = new BEClient.CandidateProfile();
            try
            {
                ViewBag.Isparsed = allowParse;
                profile.obj.ClientId = _CurrentClientId;
                profile.obj.UserId = _UserId;
                BLClient.ProfileAction objProfileaction = new BLClient.ProfileAction(_CurrentClientId);
                Guid UserId = objProfileaction.ValidateProfile(profile.obj);
                _objCandidateProfile = ATSCommon.CommonFunctions.GenerateProfile(profile.obj, documentFile, allowParse);
                Dropdownlist();
                return RedirectToAction("IndexProfileChanger", new { ProfileId = _objCandidateProfile.objProfile.ProfileId, Ordinal = 1 });
            }
            catch (Exception ex)
            {
                Dropdownlist();
                ModelState.AddModelError("", ex.Message);
                _objProfileAction = new BLClient.ProfileAction(_CurrentClientId);
                BEClient.CandidateProfile ObjCandidatProfile = _objProfileAction.GetCandidateLastUpdatedProfile(_UserId);
                return RedirectToAction("IndexProfileChanger", new { ProfileId = ObjCandidatProfile.objProfile.ProfileId, message = ex.Message });
            }
        }


        //Udate Profile
        public ActionResult UpdateProfile(RootModels.BreadScrumbModel<BEClient.Profile> profile, HttpPostedFileBase documentFile, bool allowParse = true)
        {
            BEClient.CandidateProfile _objCandidateProfile = new BEClient.CandidateProfile();
            BLClient.ProfileAction objProfileaction = new BLClient.ProfileAction(_CurrentClientId);
            try
            {
                Guid _ProfileId = Guid.Empty;
                if (documentFile != null)
                {
                    if (profile.obj != null)
                    {
                        string filename = documentFile.FileName;
                        Common.CommonFunctions.ValidateDocumentTypeExtensions(filename, profile.obj.ExtensionTypes);
                    }


                    bool reResult = false;
                    ViewBag.Isparsed = allowParse;
                    profile.obj.ClientId = _CurrentClientId;
                    profile.obj.UserId = _UserId;

                    Dropdownlist();
                    reResult = objProfileaction.UpdateProfile(profile.obj);
                    if (reResult)
                    {
                        _objCandidateProfile = ATSCommon.CommonFunctions.GenerateProfile(profile.obj, documentFile, allowParse);
                    }
                    else
                    {
                        throw new Exception("Profile not Updated");
                    }
                    _ProfileId = _objCandidateProfile.objProfile.ProfileId;
                }
                else
                {
                    profile.obj.ClientId = _CurrentClientId;
                    profile.obj.UserId = _UserId;
                    profile.obj.LanguageId = _CurrentLanguageId;
                    bool ReSult = false;
                    ReSult = objProfileaction.UpdateProfile(profile.obj);
                    _ProfileId = profile.obj.ProfileId;
                    if (!ReSult)
                    {
                        throw new Exception("Profile not Updated");
                    }
                }
                return RedirectToAction("IndexProfileChanger", new { ProfileId = _ProfileId, Ordinal = 1 });
            }
            catch (Exception ex)
            {
                Dropdownlist();
                ModelState.AddModelError("", ex.Message);
                _objProfileAction = new BLClient.ProfileAction(_CurrentClientId);
                BEClient.CandidateProfile ObjCandidatProfile = _objProfileAction.GetCandidateLastUpdatedProfile(_UserId);
                return RedirectToAction("IndexProfileChanger", new { ProfileId = ObjCandidatProfile.objProfile.ProfileId, message = ex.Message });
            }
        }


        public ActionResult IndexProfile(RootModels.BreadScrumbModel<BEClient.Profile> profile, HttpPostedFileBase documentFile, bool allowParse, string FromWhere = null)
        {
            BEClient.CandidateProfile _objCandidateProfile = new BEClient.CandidateProfile();
            try
            {
                if (profile.obj != null)
                {
                    string filename = documentFile.FileName;
                    Common.CommonFunctions.ValidateDocumentTypeExtensions(filename, profile.obj.ExtensionTypes);
                }

                if (profile.obj.DocCategoryType == 1)
                {
                    ViewBag.Isparsed = allowParse;

                    profile.obj.ClientId = _CurrentClientId;
                    profile.obj.UserId = _UserId;
                    BLClient.ProfileAction objProfileaction = new BLClient.ProfileAction(_CurrentClientId);
                    Guid UserId = objProfileaction.ValidateProfile(profile.obj);
                    _objCandidateProfile = ATSCommon.CommonFunctions.GenerateProfile(profile.obj, documentFile, allowParse);
                    if (_objCandidateProfile != null)
                    {
                        Dropdownlist(_objCandidateProfile.ObjAvailability);
                    }
                    else
                    {
                        Dropdownlist();
                    }

                    return RedirectToAction("IndexProfileChanger", new { ProfileId = _objCandidateProfile.objProfile.ProfileId, Ordinal = 1 });
                }
                else
                {
                    bool GetCandidateDoc = false;
                    GetCandidateDoc = Common.CommonFunctions.GetCandidateDocument(profile.obj, documentFile, false);
                    return RedirectToAction("Index", "MyDocuments", new { area = "Candidate" });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                _objProfileAction = new BLClient.ProfileAction(_CurrentClientId);
                BEClient.CandidateProfile ObjCandidatProfile = _objProfileAction.GetCandidateLastUpdatedProfile(_UserId);
                if (_objCandidateProfile != null)
                {
                    Dropdownlist(_objCandidateProfile.ObjAvailability);
                }
                else
                {
                    Dropdownlist();
                }
                if (FromWhere == "CreateProfile")
                {
                    return RedirectToAction("CreateProfile", new { ErrMsg = ex.Message });
                }
                else if (FromWhere == "CreateProfilemanually")
                {
                    return RedirectToAction("CreateProfileNav", new { ErrMsg = ex.Message });
                }
                else
                {
                    return RedirectToAction("ProfileMaker", new { Forwhat = "Create", ErrMsg = ex.Message });
                }
            }
        }

        [HttpPost]
        public ActionResult UploadResume(HttpPostedFileBase imgFile, bool IsError, String company)
        {
            try
            {
                if (!IsError)
                {
                    if (imgFile != null && imgFile.ContentLength > 0)
                    {
                        Common.CommonFunctions.ValidateDocumentType(imgFile.FileName);
                        string mypath = Common.Constants.STR_RESUME_PATH + imgFile.FileName;
                        if (!Directory.Exists(Server.MapPath(Common.Constants.STR_RESUME_PATH)))
                        {
                            Directory.CreateDirectory(Server.MapPath(Common.Constants.STR_RESUME_PATH));
                        }
                        var path = Path.Combine(Server.MapPath(Common.Constants.STR_RESUME_PATH), imgFile.FileName);
                        imgFile.SaveAs(path);
                        String _URL = RedirectToResumeParser(ViewBag.ClientName, mypath);
                        return GetJson(base.GetJsonContent(false, _URL));
                    }
                    return GetJson(base.GetJsonContent(true, string.Empty, "Please contact Administrator")); ;
                }
                else
                {
                    return GetJson(base.GetJsonContent(true, string.Empty, "Please contact Administrator")); ;
                }
            }
            catch (Exception ex)
            {
                return GetJson(base.GetJsonContent(true, string.Empty, ex.Message)); ;
            }
        }

        public bool UploadResume(BEClient.Profile profile, HttpPostedFileBase documentFile, out string _oldFileName, out string _newFileName, out string _serverFilePath)
        {
            bool _isFileUploaded = false;
            _oldFileName = String.Empty;
            _newFileName = String.Empty;
            _serverFilePath = String.Empty;
            Guid _profileid = profile.ProfileId;
            try
            {
                if (documentFile != null && documentFile.ContentLength > 0)
                {
                    //Vlidate Document from server side
                    Common.CommonFunctions.ValidateDocumentType(documentFile.FileName);

                    //Get Extension of uploaded File
                    string _extension = Path.GetExtension(documentFile.FileName);
                    _oldFileName = documentFile.FileName;
                    _newFileName = Common.CommonFunctions.GetGuidInStringFormat(Guid.NewGuid()) + _extension;
                    _serverFilePath = Path.Combine(Common.Constants.STR_RESUME_PATH, _newFileName);
                    string _resumePath = Path.Combine(Server.MapPath(Common.Constants.STR_RESUME_PATH), _newFileName);
                    #region Create new directory

                    //Create directory of not exist
                    if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(Common.Constants.STR_RESUME_DIR_PATH)))
                    {
                        Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(Common.Constants.STR_RESUME_DIR_PATH));
                    }

                    #endregion

                    #region for deletion of old file
                    if (!_profileid.Equals(Guid.Empty))
                    {
                        BLClient.ATSResumeAction _objATSResumeAction = new BLClient.ATSResumeAction(_CurrentClientId);
                        BEClient.ATSResume _objATSResume = new BEClient.ATSResume();
                        _objATSResume = _objATSResumeAction.GetATSResumeByUserAndProfile(_UserId, _profileid);
                        if (_objATSResume != null)
                        {
                            try
                            {
                                string Existingfilepath = Path.Combine(Common.Constants.STR_RESUME_PATH, _objATSResume.NewFileName);
                                _objATSResume.UploadedFileName = _oldFileName;
                                _objATSResume.NewFileName = _newFileName;
                                _objATSResume.Path = _serverFilePath;
                                _objATSResumeAction.UpdateATSResume(_objATSResume);

                                System.IO.File.Delete(Server.MapPath(Existingfilepath));

                            }
                            catch
                            {
                                throw new Exception("File not found For updation");
                            }

                        }
                    }

                    #endregion
                    documentFile.SaveAs(_resumePath);

                    if (!System.IO.File.Exists(_resumePath))
                        throw new Exception("File not Upload on Path " + _resumePath);
                    else
                        _isFileUploaded = true;
                }
                ViewBag.Message = "Profile Save Successfully";
            }
            catch
            {
                throw;
            }
            return _isFileUploaded;
        }

        public ActionResult UploadResume()
        {
            return null;
        }

        private String RedirectToResumeParser(string Company, string ResumePath)
        {
            UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);
            return u.Action("Index", "MyProfile", new { company = Company, area = Common.Constants.AREA_CANDIDATE, ResumePath = ResumePath });
        }

        #region Profile Change based on ProfileName DropDown
        public void NavIndexProfileChanger(Guid ProfileId, String DisplayToolTip, String ordinal)
        {
            ATSCommon.CommonFunctions.SetNullNav(this.ControllerContext.RequestContext);
            List<BEClient.BreadCrumb> objBreadCrumblst = ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs;
            if (ordinal != null && !String.IsNullOrEmpty(ordinal.ToString()))
            {
                objBreadCrumblst.RemoveAll(x => x.ordinal >= Convert.ToInt32(ordinal.ToString()));
            }
            BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
            objBreadCrumb.Action = "IndexProfileChanger";
            objBreadCrumb.Controller = "MyProfile";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_CANDIDATE;
            objBreadCrumb.ordinal = objBreadCrumblst.Count() + 1;
            objBreadCrumb.URL = Url.Action("IndexProfileChanger", "MyProfile", new { ProfileId = ProfileId, area = ATS.WebUi.Common.Constants.AREA_CANDIDATE, ordinal = objBreadCrumb.ordinal });

            objBreadCrumb.ImagePath = BECommon.ImagePaths.UploadResumeImage;
            objBreadCrumb.ToolTip = DisplayToolTip;

            objBreadCrumb.WithoutOrdinalURL = Url.Action("IndexProfileChanger", "MyProfile", new { ProfileId = ProfileId, area = ATS.WebUi.Common.Constants.AREA_CANDIDATE });
            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);
            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
        }
        public ActionResult IndexProfileChanger(Guid ProfileId, string message, string ControllerNm, String Ordinal, bool CreateNav = true)
        {
            try
            {
                if (!string.IsNullOrEmpty(message))
                {
                    ViewBag.Message = message;
                    ViewBag.IsError = true;
                }

                //Dropdownlist();
                RootModels.BreadScrumbModel<BEClient.CandidateProfile> objCandidateProfile = new RootModels.BreadScrumbModel<BEClient.CandidateProfile>();
                BLClient.ProfileAction objprofileaction = new BLClient.ProfileAction(_CurrentClientId);


                Guid _ProfileId = ProfileId;

                objCandidateProfile.obj = objprofileaction.GetCanidateFullProfileByUserIdAndProfileId(_UserId, _ProfileId);

                if (objCandidateProfile == null)
                {
                    //return view which will create new profile
                    Dropdownlist();
                }
                else if (CreateNav)
                {
                    NavIndexProfileChanger(ProfileId, objCandidateProfile.obj.objProfile.ProfileName, Ordinal);
                    Dropdownlist(objCandidateProfile.obj.ObjAvailability);
                }
                try
                {
                    ATSCommon.CommonFunctions.SolrEmployeeDeltaImport();
                }
                catch (Exception ex)
                {
                    log.Error("SOLR PROFILE  Full IMPORT (Profile)" + ex.Message);
                }

                //return GetJson(base.GetJsonContent(false, string.Empty, string.Empty, RenderRazorViewToString("Index", objCandidateProfile)));

                //if (ControllerNm.ToLower() == "myapplications")
                //{
                //   objCandidateProfile.
                //}


                //Anand : Under Observation
                Dropdownlist(objCandidateProfile.obj.ObjAvailability);
                return View("Index", objCandidateProfile);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        private void ProfileMakerDropDown()
        {
            try
            {
                BLClient.ProfileAction objprofileAction = new BLClient.ProfileAction(_CurrentClientId);
                BLClient.DocumentTypeAction objdocumenttypeAction = new BLClient.DocumentTypeAction(_CurrentClientId);
                List<BEClient.Profile> lstprofile = new List<BEClient.Profile>();
                lstprofile = objprofileAction.GetProfileByUser(_UserId);
                ViewBag.Profilename = new SelectList(lstprofile, "ProfileId", "ProfileName");
                List<BEClient.DocumentType> lstdocumenttype = new List<BEClient.DocumentType>();
                lstdocumenttype = objdocumenttypeAction.GetAllDocmentType(_CurrentLanguageId);
                ViewBag.DocumentName = new SelectList(lstdocumenttype, "DocumentTypeId", "LocalName");
            }
            catch
            {
                throw;
            }
        }

        private void Dropdownlist(BEClient.Availability objAvailability = null)
        {
            try
            {
                BLClient.ProfileAction objprofileAction = new BLClient.ProfileAction(_CurrentClientId);
                List<BEClient.Profile> lstprofile = new List<BEClient.Profile>();
                lstprofile = objprofileAction.GetProfileByUser(_UserId);
                ViewBag.Profilename = new SelectList(lstprofile, "ProfileId", "ProfileName");
                ViewBag.YesNoDropDownRelativesWorkingInCompany = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownWillingToWorkOverTime = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownSubmittedApplicationBefore = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownEligibleToWorkInUS = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownyearsOld = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownMisdemeanor = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownMilitaryService = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownMayWeContact = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownRead = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownWrite = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownSpeak = Common.CommonFunctions.YesNoDropDownList();

                List<BEClient.DegreeType> lstDegreeType = BLClient.Common.CacheHelper.GetDegreeType(_CurrentClientId, _CurrentLanguageId, CurrentClient.Clientname);
                ViewBag.DegreeTypeName = new SelectList(lstDegreeType, "DegreeTypeId", "LocalName");

                List<BEClient.SkillType> lstSkillType = BLClient.Common.CacheHelper.GetSkillType(_CurrentClientId, _CurrentLanguageId, CurrentClient.Clientname);
                ViewBag.SkillTypeName = new SelectList(lstSkillType, "SkillTypeId", "LocalName");

                var lstMonths = ATS.WebUi.Common.CommonFunctions.BindMonthDropDown();
                var lstEndMonths = ATS.WebUi.Common.CommonFunctions.BindEndMonthDropDown();

                //var lstYears = ATS.WebUi.Common.CommonFunctions.BindYearDropDown();

                ViewBag.StartMonthsList = new SelectList(lstMonths, "Value", "Text");
                //ViewBag.StartYearList = new SelectList(lstYears, "Value", "Text");

                ViewBag.EndMonthsList = new SelectList(lstEndMonths, "Value", "Text");
                //ViewBag.EndYearList = new SelectList(lstYears, "Value", "Text");

                List<BEClient.DrpStringMapping> _EmploymentPreferenceList = new List<BEClient.DrpStringMapping>();
                _EmploymentPreferenceList = BLClient.Common.CacheHelper.GetDropDownStringMapping(_CurrentClientId, _CurrentLanguageId, CurrentClient.Clientname, "JobType");
                ViewBag._EmploymentPreferenceList = new SelectList(_EmploymentPreferenceList, "ValueField", "TextField");

                List<BEClient.DrpStringMapping> _WeekAvailability = new List<BEClient.DrpStringMapping>();
                _WeekAvailability = BLClient.Common.CacheHelper.GetDropDownStringMapping(_CurrentClientId, _CurrentLanguageId, CurrentClient.Clientname, "WeekAvailability");

                if (objAvailability != null && objAvailability.HrsMon != null)
                {
                    ViewBag.MonAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField", objAvailability.HrsMon.Split(','));
                    ViewBag.TueAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField", objAvailability.HrsTue.Split(','));
                    ViewBag.WedAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField", objAvailability.HrsWed.Split(','));
                    ViewBag.ThuAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField", objAvailability.HrsThu.Split(','));
                    ViewBag.FriAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField", objAvailability.HrsFri.Split(','));
                    ViewBag.SatAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField", objAvailability.HrsSat.Split(','));
                    ViewBag.SunAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField", objAvailability.HrsSun.Split(','));
                }
                else
                {
                    ViewBag.MonAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField");
                    ViewBag.TueAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField");
                    ViewBag.WedAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField");
                    ViewBag.ThuAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField");
                    ViewBag.FriAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField");
                    ViewBag.SatAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField");
                    ViewBag.SunAvailability = new MultiSelectList(_WeekAvailability, "ValueField", "TextField");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Get LatestUpdatedProfile
        public ActionResult GetLatestProfile(string SuccessMsg)
        {
            try
            {
                if (SuccessMsg != null)
                {

                    ViewBag.Success = SuccessMsg;
                }
                else if (TempData["SuccessMsg"] != null)
                {
                    ViewBag.Success = TempData["SuccessMsg"].ToString();
                }
                //Dropdownlist();

                _objProfileAction = new BLClient.ProfileAction(_CurrentClientId);

                RootModels.BreadScrumbModel<BEClient.CandidateProfile> ObjCandidatProfile = new BreadScrumbModel<BEClient.CandidateProfile>();

                ObjCandidatProfile.obj = _objProfileAction.GetCandidateLastUpdatedProfile(_UserId);
                if (ObjCandidatProfile.obj != null)
                {
                    Dropdownlist(ObjCandidatProfile.obj.ObjAvailability);
                }
                else
                {
                    Dropdownlist(null);
                }
                NavGetLatestProfile();

                if (ObjCandidatProfile != null && ObjCandidatProfile.obj != null && ObjCandidatProfile.obj.objUserDetails != null)
                    return View("Index", ObjCandidatProfile);
                else
                    return RedirectToAction("CreateProfile");
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region This method will be moved to base controller
        protected string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
        #endregion

        public ViewResult ProfileMaker(String ForWhat, string ErrMsg = null)
        {
            if (!String.IsNullOrEmpty(ErrMsg))
            {
                ViewBag.IsError = ErrMsg;
            }
            ProfileMakerDropDown();
            ViewBag.ForUpdate = true;
            RootModels.BreadScrumbModel<BEClient.Profile> objbreadscrumb = new BreadScrumbModel<BEClient.Profile>();

            if (ForWhat == "Create")
            {
                //NavGetLatestProfile();
                NavGetUploadProfile();
                ViewBag.ForUpdate = false;
                objbreadscrumb.DisplayName = "Create profile";
                objbreadscrumb.ImagePath = BECommon.ImagePaths.AddNewImage;
            }
            else if (ForWhat == "Upload")
            {
                ViewBag.ForUpdate = false;
                NavGetUploadProfile();

                objbreadscrumb.DisplayName = "Upload Resume";
                objbreadscrumb.ImagePath = BECommon.ImagePaths.UploadResumeImage;
            }
            else
            {
                NavGetLatestProfile();
                objbreadscrumb.DisplayName = "Update profile";
                objbreadscrumb.ImagePath = BECommon.ImagePaths.SaveImage;

            }
            return View("_ProfileMaker", objbreadscrumb);
        }

        #region Delete Profile


        //public ActionResult DeleteProfile(Guid ProfileId)
        //{
        //    try
        //    {

        //        string _oldFileName = string.Empty;
        //        string _newFileName = string.Empty;
        //        string _serverFilePath = string.Empty;
        //        BLClient.ATSResumeAction _objATSResumeAction = new BLClient.ATSResumeAction(_CurrentClientId);
        //        BEClient.ATSResume _objATSResume = new BEClient.ATSResume();
        //        _objATSResume = _objATSResumeAction.GetATSResumeByUserAndProfile(_UserId, ProfileId);
        //        if (_objATSResume != null)
        //        {

        //            string Existingfilepath = Path.Combine(Common.Constants.STR_RESUME_PATH, _objATSResume.NewFileName);
        //            System.IO.File.Delete(Server.MapPath(Existingfilepath));

        //        }





        //        bool IsprofileDelted = false;
        //        BLClient.ProfileAction _objProfileAction = new BLClient.ProfileAction(_CurrentClientId);

        //        IsprofileDelted = _objProfileAction.DeleteProfile(ProfileId, _UserId);
        //        if (IsprofileDelted)
        //        {
        //            return RedirectToAction("GetLatestProfile");
        //        }
        //        else
        //        {
        //            throw new Exception("Profile is not deleted");
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);

        //        ///*Convert to json string*/
        //        string jsonModels = JsonConvert.SerializeObject(actionStatus);
        //        string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);

        //        ///*Redirect to List Page*/
        //        return RedirectToAction(ATS.WebUi.Common.Constants.STR_LIST_METHOD, new { KeyMsg = keyMsg });
        //    }

        //}

        public ActionResult DeleteProfile(Guid ProfileId)
        {
            try
            {


                List<BEClient.Application> _objApplicationList = new List<BEClient.Application>();
                BLClient.ApplicationAction _objApplicationAction = new BLClient.ApplicationAction(_CurrentClientId);

                BLClient.ATSResumeAction _objATSResumeAction = new BLClient.ATSResumeAction(_CurrentClientId);
                BEClient.ATSResume _objATSResume = new BEClient.ATSResume();
                _objATSResume = _objATSResumeAction.GetATSResumeByUserAndProfile(_UserId, ProfileId);

                _objApplicationList = _objApplicationAction.GetApplicationByAtsResume(_objATSResume.ATSResumeId);

                if (_objApplicationList.Count() > 0)
                {
                    //ViewBag.IsError = true;
                    ViewBag.Message = "You cannot delete this profile, as it is being used in one or more job applications.";
                    return RedirectToAction("IndexProfileChanger", new { ProfileId = ProfileId, message = ViewBag.Message });
                }
                else
                {

                    string _oldFileName = string.Empty;
                    string _newFileName = string.Empty;
                    string _serverFilePath = string.Empty;
                    //BLClient.ATSResumeAction _objATSResumeAction = new BLClient.ATSResumeAction(_CurrentClientId);
                    //BEClient.ATSResume _objATSResume = new BEClient.ATSResume();
                    _objATSResume = _objATSResumeAction.GetATSResumeByUserAndProfile(_UserId, ProfileId);
                    if (_objATSResume != null)
                    {

                        string Existingfilepath = Path.Combine(Common.Constants.STR_RESUME_PATH, _objATSResume.NewFileName);
                        System.IO.File.Delete(Server.MapPath(Existingfilepath));
                    }

                    bool IsprofileDelted = false;
                    BLClient.ProfileAction _objProfileAction = new BLClient.ProfileAction(_CurrentClientId);

                    IsprofileDelted = _objProfileAction.DeleteProfile(ProfileId, _UserId);
                    if (IsprofileDelted)
                    {
                        return RedirectToAction("GetLatestProfile");
                    }
                    else
                    {
                        throw new Exception("Profile is not deleted");
                    }
                }
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

        #endregion

        #region Set Profile As Default
        public ActionResult SetAsDefault(Guid ProfileId)
        {
            try
            {
                bool IsSetAsDefault = false;
                if (ProfileId != null || ProfileId != Guid.Empty)
                {
                    BLClient.ProfileAction ObjProfileAction = new BLClient.ProfileAction(_CurrentClientId);
                    IsSetAsDefault = ObjProfileAction.SetProfileAsDefault(ProfileId);
                }
                if (IsSetAsDefault)
                {
                    try
                    {
                        ATSCommon.CommonFunctions.SolrEmployeeDeltaImport();
                    }
                    catch (Exception ex)
                    {
                        log.Error("SOLR PROFILE  Full IMPORT (Profile)" + ex.Message);
                    }
                    return RedirectToAction("GetLatestProfile");
                }
                else
                {
                    throw new Exception("Profile is not Set as default");
                }





            }
            catch
            {
                throw;

            }
        }

        #endregion

        [HttpPost]
        public ActionResult UploadProfilePhoto()
        {
            string targetPath = Server.MapPath("~/UploadImages/ProfileImage/");
            string savedImgPath = "/UploadImages/ProfileImage/Candidate.png";
            try
            {
                if (Request.Files[0] != null && Request.Files.Count > 0)
                {
                    HttpPostedFileBase selectedImage = Request.Files[0] as HttpPostedFileBase;
                    string ext = Path.GetExtension(selectedImage.FileName);

                    Stream strm = selectedImage.InputStream;
                    string imageName = ATS.WebUi.Common.CurrentSession.Instance.UserId + ext;
                    targetPath = targetPath + "\\" + imageName;
                    try
                    {
                        GenerateThumbnails(strm, targetPath);
                        savedImgPath = "/UploadImages/ProfileImage/" + ATS.WebUi.Common.CurrentSession.Instance.UserId + ext + "?d=" + DateTime.Now.Ticks;
                        BLClient.UserAction objProfileImage = new BLClient.UserAction(_CurrentClientId);
                        bool returnVal = objProfileImage.UpdateProfileImage(imageName, ATS.WebUi.Common.CurrentSession.Instance.UserId);
                        ATS.WebUi.Common.CurrentSession.Instance.VerifiedUser.ProfileImage = imageName;
                    }
                    catch (Exception ex)
                    {
                        //targetPath = targetPath + "Candidate.png";
                    }
                }
            }
            catch (Exception ex)
            {
                return GetJson(savedImgPath);
            }
            return GetJson(savedImgPath);
        }

        private void GenerateThumbnails(Stream sourcePath, string targetPath)
        {
            using (var image = Image.FromStream(sourcePath))
            {
                var newWidth = 128;// (int)(image.Width * scaleFactor);
                var newHeight = 128;// (int)(image.Height * scaleFactor);
                var thumbnailImg = new Bitmap(newWidth, newHeight);
                var thumbGraph = Graphics.FromImage(thumbnailImg);
                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
                thumbGraph.DrawImage(image, imageRectangle);
                thumbnailImg.Save(targetPath);
            }
        }

        [HttpGet]
        public JsonResult GetDocumentTypeById(Guid DocumenttypeId)
        {
            bool IsError = false;
            string Message = string.Empty;
            string Data = string.Empty;
            try
            {
                BEClient.DocumentType objDocumentType = new BEClient.DocumentType();
                BLClient.DocumentTypeAction objDocumenttypeAction = new BLClient.DocumentTypeAction(_CurrentClientId);
                objDocumentType = objDocumenttypeAction.GetDocmentTypeById(DocumenttypeId, _CurrentLanguageId);

                if (objDocumentType != null)
                {
                    if (objDocumentType.DocCategory == 1)
                    {
                        Data = BEClient.DocCategoryType.Resume.ToString() + '#' + objDocumentType.ExtensionTypes.ToString();
                    }
                    else if (objDocumentType.DocCategory == 2)
                    {
                        Data = BEClient.DocCategoryType.CoverLetter.ToString() + '#' + objDocumentType.ExtensionTypes.ToString();

                    }
                    else
                    {
                        Data = BEClient.DocCategoryType.Others.ToString() + '#' + objDocumentType.ExtensionTypes.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;
            }
            return GetJson(GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUpdateProfile(Guid ProfileId)
        {
            RootModels.BreadScrumbModel<ATS.BusinessEntity.Profile> objProfile = new BreadScrumbModel<BEClient.Profile>();
            try
            {
                BLClient.ProfileAction objProfileAction = new BLClient.ProfileAction(_CurrentClientId);
                objProfile.obj = objProfileAction.GetProfileByProfileId(ProfileId);
                NavGetUpdateProfile();
            }
            catch
            {
                NavGetUpdateProfile();
                throw;
            }
            return View("Shared/_UpdateProfile", objProfile);
        }


        public ActionResult CreateProfileManually()
        {
            try
            {
                BEClient.CandidateProfile objCandidateprofile = new BEClient.CandidateProfile();
                BLClient.UserDetailsAction objUserDetailAction = new BLClient.UserDetailsAction(_CurrentClientId);
                objCandidateprofile.objUserDetails = new BEClient.UserDetails();
                objCandidateprofile.objUserDetails = objUserDetailAction.GetUserDetailsByUserId(ATS.WebUi.Common.CurrentSession.Instance.VerifiedUser.UserId);
                return View("Shared/ManualProfile", objCandidateprofile);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult AddProfileManually(BEClient.UserDetails objUserDetails)
        {
            try
            {
                Guid _newProfileId = Guid.Empty;
                if (objUserDetails != null)
                {
                    BEClient.CandidateProfile objCandidateprofile = new BEClient.CandidateProfile();
                    objCandidateprofile.objProfile = new BEClient.Profile();
                    BLClient.ProfileAction objProfileAction = new BLClient.ProfileAction(_CurrentClientId);
                    objCandidateprofile.objProfile.ProfileName = objUserDetails.ProfileName;
                    objCandidateprofile.objProfile.UserId = ATS.WebUi.Common.CurrentSession.Instance.VerifiedUser.UserId;
                    objCandidateprofile.objProfile.IsManual = true;
                    _newProfileId = objProfileAction.AddProfile(objCandidateprofile.objProfile);
                    if (_newProfileId != Guid.Empty)
                    {
                        Guid _newAtsResume = Guid.Empty;
                        BEClient.ATSResume objAtsResume = new BEClient.ATSResume();
                        BLClient.ATSResumeAction objAtsResumeAction = new BLClient.ATSResumeAction(_CurrentClientId);
                        objAtsResume = Common.CommonFunctions.CreateBlankAtsResumeobject(_newProfileId, objCandidateprofile.objProfile.UserId);
                        _newAtsResume = objAtsResumeAction.AddATSResume(objAtsResume);
                        if (_newAtsResume == Guid.Empty)
                        {
                            throw new Exception("Resume not added");
                        }
                    }
                }
                return RedirectToAction("IndexProfileChanger", new { ProfileId = _newProfileId, Ordinal = 1 });
            }
            catch
            {
                throw;
            }
        }
    }
}