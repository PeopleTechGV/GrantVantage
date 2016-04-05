using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEClient = ATS.BusinessEntity;
using BEMAster = ATS.BusinessEntity.Master;
using BLClient = ATS.BusinessLogic;
using ATSCommon = ATS.WebUi.Common;
using ATS.WebUi.Models;
using Newtonsoft.Json;
using System.IO;
using BLCommon = ATS.BusinessLogic.Common;
using ATS.WebUi.Controllers;
using BESrp = ATS.BusinessEntity.SRPEntity;
using System.Data.SqlClient;
using log4net;
using BECommon = ATS.BusinessEntity.Common;
using System.Web.Routing;
using RootModels = ATS.WebUi.Models;
using Ionic.Zip;
using System.Web.Mvc.Html;
using DisplayMessageConstant = ATS.BusinessEntity.Common;
using ATS.BusinessLogic;

namespace ATS.WebUi.Areas.Employee.Controllers
{
    public class MyVacancyController : ATS.WebUi.Controllers.AreaBaseController
    {
        const string STR_PDF_EXTENSION = ".pdf";
        //
        // GET: /Employee/MyVacancy/
        #region Private Members
        private BLClient.VacancyAction _objVacancyAction;
        private BLClient.ResumeAction _objResumeAction;
        private BESrp.DynamicSRP<List<BEClient.Vacancy>> _objVacancyList;
        private BESrp.DynamicSRP<BEClient.Vacancy> _objVacancy;
        private BESrp.DynamicSRP<BEClient.Resume> _objResume;
        private BESrp.DynamicSRP<List<BEClient.Resume>> _objResumeList;
        BLCommon.DrpStringMappingAction _DrpdownStringMappingAction;
        private Guid _UserId;
        private Guid _CurrentLanguageId;
        private Guid _CurrentClientId;
        private static readonly log4net.ILog log = LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private BLClient.QueCatAction _QueCatAction;
        #endregion

        #region Redirection Method
        private string STR_VACANCY_LIST_METHOD = "Index";
        private string STR_VACANCY_CREATE_METHOD = "Create";
        private string STR_VACANCY_EDIT_METHOD = "Edit";
        private string STR_VACANCY_VIEW_METHOD = "View";

        #endregion
        #region Initialize Method call on every request
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.ActionName.ToLower() != "printfullprofile")
            {
                base.OnAuthorization(filterContext);
                if (base.CurrentClient != null && ATS.WebUi.Common.CurrentSession.Instance.VerifiedUser != null)
                {

                    _DrpdownStringMappingAction = new BLCommon.DrpStringMappingAction(base.CurrentClient.ClientId);
                    _objVacancyAction = new BLClient.VacancyAction(base.CurrentClient.ClientId, true);
                    _objResumeAction = new BLClient.ResumeAction(base.CurrentClient.ClientId, true);

                    _objResumeList = new BESrp.DynamicSRP<List<BEClient.Resume>>();
                    _objResumeList.UserPermissionWithScope = _objResumeAction.ListUserPermissionWithScope;

                    _CurrentClientId = base.CurrentClient.ClientId;
                    //Create new object for List
                    _UserId = ATSCommon.CurrentSession.Instance.UserId;
                    _CurrentLanguageId = base.CurrentClient.CurrentLanguageId;
                    _objVacancyList = new BESrp.DynamicSRP<List<BEClient.Vacancy>>();
                    _objVacancyList.AddBtnText = "Add Vacancy";
                    _objVacancyList.EditBtnText = "Edit Vacancy";
                    _objVacancyList.RemoveBtnText = "Remove Vacancy";
                    _objVacancyList.DeleteBtnText = "Delete Vacancy";
                    _objVacancyList.SaveBtnText = "Save Vacancy";
                    _objVacancyList.UserPermissionWithScope = _objVacancyAction.ListUserPermissionWithScope;


                    if (ATSCommon.CurrentSession.Instance.VerifiedUser.SecurityRoles.Where(x => x.AtsSecurityRole == BEClient.ATSSecurityRole.Candidate).Count() > 0)
                    {
                        filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_CANDIDATE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_CANDIDATE_CONTROLLER, new { area = ATSCommon.Constants.AREA_CANDIDATE }));

                    }
                    //This function will be used for create new details.It will check two key exists(Action and Contrller)
                    if (filterContext.ActionDescriptor.ActionName == "View" && filterContext.RouteData.Values.Keys.Count() == 2)
                    {
                        if (_objVacancyList.UserPermissionWithScope.Where(x => x.PermissionType.Contains(BEClient.ATSPermissionType.Create)).Count() <= 0)
                        {
                            filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_CONTROLLER, new { @KeyMsg = ATSCommon.CommonFunctions.NonAuthoriseduser() }));
                        }
                    }
                }
            }
        }

        #endregion

        private BLClient.QueCatAction QueCatActionObject(bool force = false)
        {
            if (force)
            {
                _QueCatAction = new BusinessLogic.QueCatAction(_CurrentClientId, true);
            }
            else if (_QueCatAction == null)
            {
                _QueCatAction = new BusinessLogic.QueCatAction(_CurrentClientId, true);

            }
            return _QueCatAction;
        }

        private void CreateObjVacancy(Guid? VacancyId)
        {
            _objVacancy = new BESrp.DynamicSRP<BEClient.Vacancy>();
            _objVacancy.AddBtnText = _objVacancyList.AddBtnText;
            _objVacancy.EditBtnText = _objVacancyList.EditBtnText;
            _objVacancy.RemoveBtnText = _objVacancyList.RemoveBtnText;
            _objVacancy.SaveBtnText = _objVacancyList.SaveBtnText;
            _objVacancy.UserPermissionWithScope = _objVacancyAction.ListUserPermissionWithScope;
            if (VacancyId != null)
                _objVacancy.RemoveRecordURL = RemoveVacancyURL((Guid)VacancyId);
        }

        public ActionResult GetCandidateProfile(Guid ApplicationId, Guid? VacancyId, String ordinal)
        {
            try
            {
                return RedirectToAction("GetCandidateProfile", "MyCandidates", new { ApplicationId = ApplicationId, ordinal = "" });
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        public BESrp.DynamicSRP<BEClient.CandidateProfile> GetFullCandidateProfile(Guid UserId, Guid ProfileId)
        {
            BESrp.DynamicSRP<BEClient.CandidateProfile> ObjCandidatProfile = new BESrp.DynamicSRP<BEClient.CandidateProfile>();
            BLClient.ProfileAction _objProfileAction = new BLClient.ProfileAction(_CurrentClientId);
            BLClient.ApplicationAction _objApplicationAction = new BLClient.ApplicationAction(_CurrentClientId);

            ObjCandidatProfile.obj = new BEClient.CandidateProfile();
            ObjCandidatProfile.obj = _objProfileAction.GetCanidateFullProfileByUserIdAndProfileId(UserId, ProfileId);
            ObjCandidatProfile.obj.ObjApplicationCount = _objApplicationAction.GetCountOfSubmittedApplicationByProfile(ProfileId, _CurrentLanguageId);

            CandidateProfileDropdownlist();
            ViewBag.EmployeeView = true;
            return ObjCandidatProfile;
        }

        [HttpGet]
        public ActionResult CandidateProfilePage(Guid UserId, Guid ProfileId)
        {
            BESrp.DynamicSRP<BEClient.CandidateProfile> ObjCandidatProfile = null;
            ObjCandidatProfile = GetFullCandidateProfile(UserId, ProfileId);
            return View(ObjCandidatProfile);
        }

        public ActionResult DownloadResumeByAtsResumeId(Guid AtsResumeId)
        {
            BLClient.ATSResumeAction AtsResumeAction = new BLClient.ATSResumeAction(_CurrentClientId);
            BEClient.ATSResume ObjAtsResume = AtsResumeAction.GetProfileIdAndUserIdbyAtsResumeId(AtsResumeId);
            string url = GetEmployeeProfileURL(ObjAtsResume.UserId, ObjAtsResume.ProfileId);
            string fileName = Path.Combine(Common.Constants.SERVER_TEMP_DOC_PATH, Common.CommonFunctions.GenerateTempFileName());
            return DownloadResume(url, fileName, ObjAtsResume.UserId);
        }

        public ActionResult DownloadResume(Guid UserId, Guid profileId)
        {
            string url = GetEmployeeProfileURL(UserId, profileId);
            string fileName = Path.Combine(Common.Constants.SERVER_TEMP_DOC_PATH, Common.CommonFunctions.GenerateTempFileName());
            return DownloadResume(url, fileName, UserId);
        }

        public ActionResult PrintFullProfile(string URL, String ClientName, Guid UserId)
        {
            URL = URL + "&ClientName=" + ClientName + "&UserId=" + UserId;
            URL = "http://" + ATS.WebUi.Common.ConfigurationMapped.Instance.DomianName + URL + "&ShowHeader=true";
            return DownloadFullProfilePDF(URL);
        }

        private ActionResult DownloadFullProfilePDF(String pageURL)
        {
            try
            {
                String serverMapPath = "";
                String fileDownloadName = Common.CommonFunctions.GenerateTempFileName();
                fileDownloadName = fileDownloadName + STR_PDF_EXTENSION;
                serverMapPath = Common.CommonFunctions.HtmlToPdf(pageURL, "~/Resume/Temp/", fileDownloadName);
                serverMapPath = HttpContext.Server.MapPath(serverMapPath);
                var fileStream = new FileStream(serverMapPath, FileMode.Open);
                var mimeType = "application/pdf";

                FileStreamResult result = File(fileStream, mimeType, fileDownloadName);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private String GetEmployeeProfileURL(Guid UserId, Guid profileId)
        {
            UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);
            return "http://" + Common.ConfigurationMapped.Instance.DomianName + "/Resume/" + UserId + "/" + profileId + "/" + CurrentClient.Clientname;
        }

        public ActionResult DownloadCandidateResumeAndCoverLetter(Guid AtsResumeId)
        {
            try
            {
                return ATSCommon.CommonFunctions.DownloadAppliedVacancyFiles(AtsResumeId, _CurrentClientId, Server);
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

        private ActionResult DownloadResume(String pageURL, String fileName, Guid? UserId)
        {
            String serverMapPath = HttpContext.Server.MapPath(fileName + STR_PDF_EXTENSION);
            bool isSuccess = Common.CommonFunctions.GeneratePDF(pageURL, serverMapPath);

            var fileStream = new FileStream(serverMapPath, FileMode.Open);
            var mimeType = "application/pdf";
            var fileDownloadName = string.Empty; ;
            String TodayDate = System.DateTime.Now.ToString("MM-dd-yyyy");
            if (UserId != null)
            {
                BEClient.UserDetails objUserDetails = new BEClient.UserDetails();
                BLClient.UserDetailsAction objuserDetailsAction = new BLClient.UserDetailsAction(_CurrentClientId);
                objUserDetails = objuserDetailsAction.GetUserDetailsByUserId((Guid)UserId);
                fileDownloadName = "CandidateProfile_" + objUserDetails.LastName + "_" + objUserDetails.FirstName + "_" + TodayDate + STR_PDF_EXTENSION;
            }
            else
            {
                fileDownloadName = "Resume" + STR_PDF_EXTENSION;
            }
            FileStreamResult result = File(fileStream, mimeType, fileDownloadName);
            // fileStream.Close();
            // DeleteFileFromServer(serverMapPath);
            return result;
        }

        public ActionResult DownloadZipFile(Guid AtsResumeId, Guid AtsCoverLetterId)
        {
            try
            {


                string archiveName = String.Format("ATS-{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                var filesToInclude = new System.Collections.Generic.List<String>();
                string path = Server.MapPath("~/");

                //Get Resume File path
                BEClient.ATSResume objAtsresume = new BEClient.ATSResume();
                BLClient.ATSResumeAction objAtsResumeAction = new BLClient.ATSResumeAction(_CurrentClientId);
                objAtsresume = objAtsResumeAction.GetRecordByRecorId(AtsResumeId);
                if (objAtsresume != null && objAtsresume.IsCoverLetter == false && objAtsresume.NewFileName != string.Empty)
                {
                    if (System.IO.File.Exists(path + "\\Resume\\" + objAtsresume.NewFileName))
                        filesToInclude.Add(path + "\\Resume\\" + objAtsresume.NewFileName);
                }

                //Get CoverLetter File path
                BEClient.ATSResume objAtsCoverLetter = new BEClient.ATSResume();
                BLClient.ATSResumeAction objAtsCoverLetterAction = new BLClient.ATSResumeAction(_CurrentClientId);
                objAtsCoverLetter = objAtsCoverLetterAction.GetRecordByRecorId(AtsCoverLetterId);
                if (objAtsCoverLetter != null && objAtsCoverLetter.IsCoverLetter == true && objAtsCoverLetter.NewFileName != string.Empty)
                {
                    if (System.IO.File.Exists(path + "\\Resume\\" + objAtsCoverLetter.NewFileName))
                        filesToInclude.Add(path + "\\Resume\\" + objAtsCoverLetter.NewFileName);
                }

                // Full Profile File Path
                BLClient.ATSResumeAction AtsResumeAction = new BLClient.ATSResumeAction(_CurrentClientId);
                BEClient.ATSResume ObjAtsResume = AtsResumeAction.GetProfileIdAndUserIdbyAtsResumeId(AtsResumeId);
                string url = GetEmployeeProfileURL(ObjAtsResume.UserId, ObjAtsResume.ProfileId);
                string fileName = Path.Combine(Common.Constants.SERVER_TEMP_DOC_PATH, Common.CommonFunctions.GenerateTempFileName());
                String serverMapPath = HttpContext.Server.MapPath(fileName + STR_PDF_EXTENSION);
                bool isSuccess = Common.CommonFunctions.GeneratePDF(url, serverMapPath);
                if (isSuccess)
                {
                    if (System.IO.File.Exists(serverMapPath))
                        filesToInclude.Add(serverMapPath);

                }

                ZipFile zip = new ZipFile();
                zip.AddFiles(filesToInclude, "ATS");
                return new ZipFileResult(zip, archiveName);

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



        public ActionResult DownloadAllAppDocsZipByApplicationId(Guid ApplicationId, Guid UserId)
        {
            try
            {
                ZipFile zip = new ZipFile("ATS_ZIP");


                string archiveName = String.Format("ATS-{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                var filesToInclude = new System.Collections.Generic.List<String>();
                string path = Server.MapPath("~/");

                ////Get All Uploaded Application Docs
                BLClient.ApplicationDocumentsAction objAppDocAction = new BLClient.ApplicationDocumentsAction(_CurrentClientId);
                List<BEClient.ApplicationDocuments> objAppDocList = objAppDocAction.GetRequiredDocumentsByApplicationId(ApplicationId);
                if (objAppDocList != null && objAppDocList.Count > 0)
                {
                    objAppDocList = objAppDocList.Where(x => x.IsPending == false).ToList();
                }

                if (objAppDocList != null && objAppDocList.Count > 0)
                {
                    int no = 1;
                    foreach (BEClient.ApplicationDocuments objDoc in objAppDocList)
                    {
                        if (System.IO.File.Exists(Server.MapPath(objDoc.Path)))
                        {
                            //filesToInclude.Add(Server.MapPath(objDoc.Path));
                            zip.AddFile(Server.MapPath(objDoc.Path)).FileName = no.ToString() + "_" + objDoc.UploadedFileName;
                            no++;
                        }
                    }
                }

                // Full Profile File Path
                string URL = HttpContext.Request.Url.ToString().Substring(0, HttpContext.Request.Url.ToString().IndexOf("/Employee") + 1);
                string pageURL = URL + "Employee/CandidateDetails?ApplicationId=" + ApplicationId + "&ClientName=" + CurrentClient.Clientname + "&UserId=" + UserId.ToString() + "&ShowHeader=true";
                String serverMapPath = "";
                String fileDownloadName = Common.CommonFunctions.GenerateTempFileName();
                fileDownloadName = fileDownloadName + STR_PDF_EXTENSION;
                serverMapPath = Common.CommonFunctions.HtmlToPdf(pageURL, "~/Resume/Temp/", fileDownloadName);
                serverMapPath = HttpContext.Server.MapPath(serverMapPath);
                if (serverMapPath != "")
                {
                    if (System.IO.File.Exists(serverMapPath))
                    {
                        //filesToInclude.Add(serverMapPath);
                        zip.AddFile(serverMapPath).FileName = "Candidate_Profile.pdf";
                    }
                }

                //ZipFile zip = new ZipFile();
                //zip.AddFiles(filesToInclude, "ATS");
                return new ZipFileResult(zip, archiveName);

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

        private void CandidateProfileDropdownlist()
        {
            try
            {
                //BLClient.ProfileAction objprofileAction = new BLClient.ProfileAction(_CurrentClientId);
                //List<BEClient.Profile> lstprofile = new List<BEClient.Profile>();
                //lstprofile = objprofileAction.GetProfileByUser(_UserId);
                //ViewBag.Profilename = new SelectList(lstprofile, "ProfileId", "ProfileName");
                //ViewBag.YesNoDropDownRelativesWorkingInCompany = Common.CommonFunctions.YesNoDropDownList();
                //ViewBag.YesNoDropDownWillingToWorkOverTime = Common.CommonFunctions.YesNoDropDownList();
                //ViewBag.YesNoDropDownSubmittedApplicationBefore = Common.CommonFunctions.YesNoDropDownList();
                //ViewBag.YesNoDropDownEligibleToWorkInUS = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownyearsOld = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownRelativesWorkingInCompany = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownWillingToWorkOverTime = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownSubmittedApplicationBefore = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownEligibleToWorkInUS = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownMisdemeanor = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownMilitaryService = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownMayWeContact = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownRead = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownWrite = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownSpeak = Common.CommonFunctions.YesNoDropDownList();

                BLClient.DegreeTypeAction objDegreeTypeAction = new BLClient.DegreeTypeAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                List<BEClient.DegreeType> lstDegreeType = objDegreeTypeAction.GetAllDegreeTypeByLanguage(Common.CurrentSession.Instance.VerifiedClient.CurrentLanguageId);
                ViewBag.DegreeTypeName = new SelectList(lstDegreeType, "DegreeTypeId", "LocalName");

                BLClient.SkillTypeAction objSkillTypeAction = new BLClient.SkillTypeAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                List<BEClient.SkillType> lstSkillType = objSkillTypeAction.GetAllSkillTypeByLanguage(Common.CurrentSession.Instance.VerifiedClient.CurrentLanguageId);
                ViewBag.SkillTypeName = new SelectList(lstSkillType, "SkillTypeId", "LocalName");

                var lstMonths = ATS.WebUi.Common.CommonFunctions.BindMonthDropDown();
                var lstEndMonths = ATS.WebUi.Common.CommonFunctions.BindEndMonthDropDown();

                var lstYears = ATS.WebUi.Common.CommonFunctions.BindYearDropDown();
                ViewBag.StartMonthsList = new SelectList(lstMonths, "Value", "Text");
                ViewBag.StartYearList = new SelectList(lstYears, "Value", "Text");
                ViewBag.EndMonthsList = new SelectList(lstEndMonths, "Value", "Text");
                ViewBag.EndYearList = new SelectList(lstYears, "Value", "Text");

                List<BEClient.DrpStringMapping> _EmploymentPreferenceList = new List<BEClient.DrpStringMapping>();
                _EmploymentPreferenceList = BLClient.Common.CacheHelper.GetDropDownStringMapping(_CurrentClientId, _CurrentLanguageId, CurrentClient.Clientname, BEClient.DrpDownFields.JobType.ToString());
                ViewBag._EmploymentPreferenceList = new SelectList(_EmploymentPreferenceList, "ValueField", "TextField");
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region BRead Scrum
        private void NavIndex()
        {
            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = null;
            List<BEClient.BreadCrumb> objBreadCrumblst = new List<BEClient.BreadCrumb>();
            BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
            objBreadCrumb.Action = "Index";
            objBreadCrumb.Controller = "MyVacancy";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_EMPLOYEE;
            objBreadCrumb.ordinal = 1;
            objBreadCrumb.URL = Url.Action("Index", "MyVacancy", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });
            objBreadCrumb.ImagePath = BECommon.ImagePaths.VacancyImage;
            objBreadCrumb.ToolTip = "My Opportunities";
            objBreadCrumb.WithoutOrdinalURL = Url.Action("Index", "MyVacancy", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });

            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);

            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
        }

        private void NavView(Guid? VacancyId, String DisplayToolTip, String ordinal)
        {
            ATSCommon.CommonFunctions.SetNullNav(this.ControllerContext.RequestContext);
            List<BEClient.BreadCrumb> objBreadCrumblst = ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs;
            if (ordinal != null && !String.IsNullOrEmpty(ordinal.ToString()))
            {
                objBreadCrumblst.RemoveAll(x => x.ordinal >= Convert.ToInt32(ordinal.ToString()));
            }
            BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
            objBreadCrumb.Action = "View";
            objBreadCrumb.Controller = "MyVacancy";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_EMPLOYEE;
            objBreadCrumb.ordinal = objBreadCrumblst.Count() + 1;
            objBreadCrumb.URL = Url.Action("View", "MyVacancy", new { id = VacancyId, area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, ordinal = objBreadCrumb.ordinal });
            objBreadCrumb.ImagePath = BECommon.ImagePaths.VacancyImage;
            objBreadCrumb.ToolTip = "Vacancy </br>" + DisplayToolTip;
            objBreadCrumb.WithoutOrdinalURL = Url.Action("View", "MyVacancy", new { id = VacancyId, area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });

            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);

            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;

        }

        private void NavCandidateprofile(Guid ApplicationId, Guid? VacancyId, String ToolTip, String ordinal)
        {
            ATSCommon.CommonFunctions.SetNullNav(this.ControllerContext.RequestContext);
            List<BEClient.BreadCrumb> objBreadCrumblst = ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs;
            if (ordinal != null && !String.IsNullOrEmpty(ordinal.ToString()))
            {
                objBreadCrumblst.RemoveAll(x => x.ordinal >= Convert.ToInt32(ordinal.ToString()));
            }
            BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
            objBreadCrumb.Action = "GetCandidateProfile";
            objBreadCrumb.Controller = "MyVacancy";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_EMPLOYEE;
            objBreadCrumb.ordinal = objBreadCrumblst.Count() + 1;
            objBreadCrumb.URL = Url.Action("GetCandidateProfile", "MyVacancy", new { ApplicationId = ApplicationId, area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, VacancyId = (Guid)VacancyId, ordinal = objBreadCrumb.ordinal });

            objBreadCrumb.ImagePath = BECommon.ImagePaths.ProfileImage;
            objBreadCrumb.ToolTip = ToolTip;
            objBreadCrumb.WithoutOrdinalURL = Url.Action("GetCandidateProfile", "MyVacancy", new { ApplicationId = ApplicationId, area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, VacancyId = (Guid)VacancyId });

            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);
            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;

        }


        private void NavAddVacancyoption(String ToolTip, String ordinal)
        {

            ATSCommon.CommonFunctions.SetNullNav(this.ControllerContext.RequestContext);
            List<BEClient.BreadCrumb> objBreadCrumblst = ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs;
            if (ordinal != null && !String.IsNullOrEmpty(ordinal.ToString()))
            {
                objBreadCrumblst.RemoveAll(x => x.ordinal >= Convert.ToInt32(ordinal.ToString()));
            }
            BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
            objBreadCrumb.Action = "AddVacancyOption";
            objBreadCrumb.Controller = "MyVacancy";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_EMPLOYEE;
            objBreadCrumb.ordinal = objBreadCrumblst.Count() + 1;
            objBreadCrumb.URL = Url.Action("AddVacancyOption", "MyVacancy", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });

            objBreadCrumb.ImagePath = BECommon.ImagePaths.VacancyImage;
            objBreadCrumb.ToolTip = ToolTip;
            objBreadCrumb.WithoutOrdinalURL = Url.Action("AddVacancyOption", "MyVacancy", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });

            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);
            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;


        }
        #endregion

        public ActionResult Index(string KeyMsg, string status = "Open", string search = "")
        {
            ViewBag.SearchText = search;
            ViewBag.SearchTextImg = (search == "" ? "Search" : "Clear");//To change the image in the Search Text box.
            FillVacancyStatusDropDown(BEClient.VacancyStatusDrp.Close.ToString());
            GetAllVacStatus();
            JsonModels resultJsonModel = null;
            if (TempData["DeleteMsg"] != null && !String.IsNullOrEmpty(TempData["DeleteMsg"].ToString()))
            {
                /*Deserialize */
                string deserializeKeyMsg = HelperLibrary.Encryption.EncryptionAlgo.DecryptData(TempData["DeleteMsg"].ToString());

                /*Convert from json to Object*/
                resultJsonModel = JsonConvert.DeserializeObject<JsonModels>(deserializeKeyMsg);
            }
            if (TempData["KeyMsg"] != null && !String.IsNullOrEmpty(TempData["KeyMsg"].ToString()))
            {
                /*Deserialize */
                string deserializeKeyMsg = HelperLibrary.Encryption.EncryptionAlgo.DecryptData(TempData["KeyMsg"].ToString());
                /*Convert from json to Object*/
                resultJsonModel = JsonConvert.DeserializeObject<JsonModels>(deserializeKeyMsg);
            }
            try
            {
                ViewBag.VacCurrStatusCount = 0;
                if (resultJsonModel != null)
                {
                    ViewBag.IsError = resultJsonModel.IsError;
                    ViewBag.Message = resultJsonModel.Message;
                }
                if (status != null)
                {
                    ViewBag.VacStatus = status;
                    if (status == "All")
                    {
                        status = null;
                    }
                }
                ////Feedback on Release - Jan 15 2015.pptx(Slice#25)
                if (status == "Closed")
                    status = "Close";
                _objVacancyList.obj = _objVacancyAction.GetAllVacanciesByDivisionAndLanguage(base.CurrentClient.CurrentLanguageId, status, search);
                _objVacancyList.AddRecordURL = AddVacancyURL();

                ViewBag.PageMode = BEClient.PageMode.View;
                NavIndex();

                RootModels.BreadScrumbModel<BESrp.DynamicSRP<List<BEClient.Vacancy>>> model = new BreadScrumbModel<BESrp.DynamicSRP<List<BEClient.Vacancy>>>();
                model.obj = _objVacancyList;
                model.DisplayName = ATSCommon.CommonFunctions.LanguageLabel(ATS.BusinessEntity.Common.ModelConstant.MDL_VACANCIES);
                model.ImagePath = BECommon.ImagePaths.VacancyImage;
                model.ToolTip = "My Opportunities";
                BEClient.Vacancy newVacCnt = new BEClient.Vacancy();
                newVacCnt = _objVacancyAction.GetVacStatusCnt(_CurrentLanguageId);
                ViewBag.cntOpenStat = newVacCnt.CntOpen;
                ViewBag.cntCloseStat = newVacCnt.CntClose;
                ViewBag.cntArchieveStat = newVacCnt.CntArchieve;
                ViewBag.cntDraftStat = newVacCnt.CntDraft;
                ViewBag.cntAllStat = newVacCnt.CntAll;
                if (status == null || status.ToLower() == "all")
                    ViewBag.VacCurrStatusCount = newVacCnt.CntAll;
                else if (status.ToLower() == "draft")
                    ViewBag.VacCurrStatusCount = newVacCnt.CntDraft;
                else if (status.ToLower() == "open")
                    ViewBag.VacCurrStatusCount = newVacCnt.CntOpen;
                else if (status.ToLower() == "close")
                    ViewBag.VacCurrStatusCount = newVacCnt.CntClose;
                else if (status.ToLower() == "archive")
                    ViewBag.VacCurrStatusCount = newVacCnt.CntArchieve;
                return View(model);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public JsonResult GetVacCnt(string status)
        {
            string Message = string.Empty;
            bool IsError = false;
            string Data = string.Empty;
            try
            {
                ViewBag.VacCurrStatusCount = 0;
                if (status != null)
                {
                    ViewBag.VacStatus = status;
                }
                BEClient.Vacancy newVacCnt = new BEClient.Vacancy();
                newVacCnt = _objVacancyAction.GetVacStatusCnt(_CurrentLanguageId);
                if (newVacCnt != null)
                {

                    ViewBag.cntOpenStat = newVacCnt.CntOpen;
                    ViewBag.cntCloseStat = newVacCnt.CntClose;
                    ViewBag.cntArchieveStat = newVacCnt.CntArchieve;
                    ViewBag.cntDraftStat = newVacCnt.CntDraft;
                    ViewBag.cntAllStat = newVacCnt.CntAll;
                    if (status == null || status.ToLower() == "all")
                        ViewBag.VacCurrStatusCount = newVacCnt.CntAll;
                    else if (status.ToLower() == "draft")
                        ViewBag.VacCurrStatusCount = newVacCnt.CntDraft;
                    else if (status.ToLower() == "open")
                        ViewBag.VacCurrStatusCount = newVacCnt.CntOpen;
                    else if (status.ToLower() == "closed")
                        ViewBag.VacCurrStatusCount = newVacCnt.CntClose;
                    else if (status.ToLower() == "archive")
                        ViewBag.VacCurrStatusCount = newVacCnt.CntArchieve;
                }
                else
                {
                    IsError = true;
                    Message = "Error";
                }
                Data = base.RenderRazorViewToString("Shared/_DrpStatusCnt", null);

            }
            catch
            {
                IsError = true;
                Message = "Error";
                throw;
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data), JsonRequestBehavior.AllowGet);
        }

        #region Private Data Member
        private void DropDownLists(BEClient.ATSPermissionType PermissionType, Guid divisionId, string callMethod, Guid PositionOwner, string category = null)
        {
            try
            {
                //////Bind DropDown For PositionRequestedBy
                BLClient.UserAction _objUserAction = new BLClient.UserAction(base.CurrentClient.ClientId);
                List<BEClient.User> _PositionRequestedBy = new List<BEClient.User>();
                List<BEClient.User> _PositionOwnerList = new List<BEClient.User>();
                BEClient.User currentUser = ATSCommon.CurrentSession.Instance.VerifiedUser;
                _PositionRequestedBy = _objUserAction.GetAllEmployees();
                ViewBag.ContactsName = new SelectList(_PositionRequestedBy, "UserId", "FullName", currentUser.UserId);
                _PositionOwnerList = _objUserAction.GetAllUserByDivisionId(divisionId);
                if (!_PositionOwnerList.Any(r => r.UserId == currentUser.UserId))
                {
                    _PositionOwnerList.Add(currentUser);
                }
                ViewBag.ContactsNameOwner = new SelectList(_PositionOwnerList, "UserId", "FullName");
                ViewBag.YesNoDropDownFeaturedOnWeb = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownShowOnWeb = Common.CommonFunctions.YesNoDropDownList();
                List<BEClient.Division> lstDivision = new List<BEClient.Division>();
                BLClient.DivisionAction objDivisionAction = new BLClient.DivisionAction(base.CurrentClient.ClientId, false);
                lstDivision = _objVacancyAction.GetAllDivisionTreeByScope(base.CurrentClient.CurrentLanguageId, callMethod);
                ViewBag.Division = new SelectList(lstDivision, "DivisionId", "DivisionNameTree");
                List<BEClient.PositionType> _PositionTypeList = new List<BEClient.PositionType>();
                BLClient.PositionTypeAction _PositionTypeAction = new BLClient.PositionTypeAction(base.CurrentClient.ClientId);
                _PositionTypeList = _PositionTypeAction.GetPositionTypeByDivision(_UserId, divisionId, _CurrentLanguageId);
                ViewBag.PositionType = new SelectList(_PositionTypeList, "PositionTypeId", "LocalName");
                if (callMethod == "Create")
                {
                    ViewBag.vacancystatusdrp = Common.CommonFunctions.VacancyStatusDropDownList(BEClient.VacancyStatusDrp.Draft);
                    FillVacancyStatusDropDown(Convert.ToString(BEClient.VacancyStatusDrp.Draft));
                }
                else
                {
                    ViewBag.vacancystatusdrp = Common.CommonFunctions.VacancyStatusDropDownList(null);
                    List<BEClient.VacancyStatus> ObjVacStatus = FillVacancyStatusDropDown(Convert.ToString(BEClient.VacancyStatusDrp.Close));
                    List<BEClient.VacancyStatus> ObjVacClosedStatus = ObjVacStatus.Where(p => p.Category.ToUpper().Contains(BEClient.VacancyStatusDrp.Close.ToString().ToUpper())).ToList();
                    ViewBag.VacancyClosedStatus = new SelectList(ObjVacClosedStatus, "VacancyStatusId", "VacancyStatusTextLocal");
                    FillVacancyStatusDropDown(category);
                }
                FillRemoveVacancyStatusDropDown(BEClient.VacancyStatusCategory.Closed.ToString());
                _DrpdownStringMappingAction = new BLCommon.DrpStringMappingAction(base.CurrentClient.ClientId);
                List<BEClient.DrpStringMapping> _VacancyJobTypeList = new List<BEClient.DrpStringMapping>();
                _VacancyJobTypeList = _DrpdownStringMappingAction.GetAllDropDownValue(base.CurrentClient.CurrentLanguageId, "JobType");
                ViewBag.VacancyJobType = new SelectList(_VacancyJobTypeList, "ValueField", "TextField");
                _DrpdownStringMappingAction = new BLCommon.DrpStringMappingAction(base.CurrentClient.ClientId);
                List<BEClient.DrpStringMapping> _VacancyEmploymentTypeList = new List<BEClient.DrpStringMapping>();
                _VacancyEmploymentTypeList = _DrpdownStringMappingAction.GetAllDropDownValue(base.CurrentClient.CurrentLanguageId, "EmploymentType");
                ViewBag._VacancyEmploymentType = new SelectList(_VacancyEmploymentTypeList, "ValueField", "TextField");
                List<BEClient.JobLocation> _JobLocation = new List<BEClient.JobLocation>();
                List<BEClient.User> LstOnboardingUsers = new List<BEClient.User>();
                if (PermissionType == BEClient.ATSPermissionType.Modify)
                {
                    BLClient.JobLocationAction _objJobLocationAction = new BLClient.JobLocationAction(base.CurrentClient.ClientId);
                    _JobLocation = _objJobLocationAction.GetJobLocationByDivision(_UserId, divisionId, _CurrentLanguageId);

                    BLClient.UserAction _objOnboardUserAction = new BLClient.UserAction(base.CurrentClient.ClientId);
                    LstOnboardingUsers = _objOnboardUserAction.GetAllUOnboardingUser(divisionId);

                }
                ViewBag._JobLocationList = new SelectList(_JobLocation, "JobLocationId", "LocalName");
                ViewBag.OnboardingUsers = new SelectList(LstOnboardingUsers, "UserId", "FullName");

            }
            catch
            {
                throw;
            }
        }

        public JsonResult GetVacancyReasonsBasedOnVacancyStatus(string VacancyStatus)
        {
            try
            {
                List<BEClient.VacancyStatus> _VacancyStatusList = new List<BEClient.VacancyStatus>();
                BLClient.VacancyStatusAction _objVacancyStatusAction = new BLClient.VacancyStatusAction(_CurrentClientId);
                _VacancyStatusList = _objVacancyStatusAction.GetVacancyStatusByCategoryAndlanguage(base.CurrentClient.CurrentLanguageId, VacancyStatus);
                return GetJson(new { VacancyStatusID = _VacancyStatusList.Select(r => r.VacancyStatusId), VacancyStatusName = _VacancyStatusList.Select(p => p.VacancyStatusTextLocal) });
            }
            catch (Exception)
            {

                throw;
            }
        }

        private List<BEClient.VacancyStatus> FillVacancyStatusDropDown(string Category = null)
        {
            List<BEClient.VacancyStatus> _VacancyStatusList = new List<BEClient.VacancyStatus>();
            BLClient.VacancyStatusAction _objVacancyStatusAction = new BLClient.VacancyStatusAction(_CurrentClientId);
            _VacancyStatusList = _objVacancyStatusAction.GetVacancyStatusByCategoryAndlanguage(base.CurrentClient.CurrentLanguageId, Category);
            ViewBag.VacancyStatusList = new SelectList(_VacancyStatusList, "VacancyStatusId", "VacancyStatusTextLocal");
            return _VacancyStatusList;
        }

        private void GetAllVacStatus()
        {
            List<BEClient.VacancyStatus> _FilteredStatus = new List<BEClient.VacancyStatus>();
            List<BEClient.VacancyStatus> _VacancyStatusList = new List<BEClient.VacancyStatus>();
            BLClient.VacancyStatusAction _objVacancyStatusAction = new BLClient.VacancyStatusAction(_CurrentClientId);
            _VacancyStatusList = _objVacancyStatusAction.GetVacancyStatusByCategoryAndlanguage(base.CurrentClient.CurrentLanguageId, null);

            _FilteredStatus = _VacancyStatusList.Where(x => x.Category == Convert.ToString(BEClient.VacancyStatusDrp.Draft)).ToList();
            ViewBag.VacStatusDraft = new SelectList(_FilteredStatus, "VacancyStatusId", "VacancyStatusTextLocal");

            _FilteredStatus = _VacancyStatusList.Where(x => x.Category == Convert.ToString(BEClient.VacancyStatusDrp.Open)).ToList();
            ViewBag.VacStatusOpen = new SelectList(_FilteredStatus, "VacancyStatusId", "VacancyStatusTextLocal");

            _FilteredStatus = _VacancyStatusList.Where(x => x.Category == Convert.ToString(BEClient.VacancyStatusDrp.Close)).ToList();
            ViewBag.VacStatusClose = new SelectList(_FilteredStatus, "VacancyStatusId", "VacancyStatusTextLocal");

            _FilteredStatus = _VacancyStatusList.Where(x => x.Category == Convert.ToString(BEClient.VacancyStatusDrp.Archive)).ToList();
            ViewBag.VacStatusArchive = new SelectList(_FilteredStatus, "VacancyStatusId", "VacancyStatusTextLocal");
        }

        private void FillRemoveVacancyStatusDropDown(string Category)
        {
            List<BEClient.VacancyStatus> _RemoveVacancyStatusList = new List<BEClient.VacancyStatus>();
            BLClient.VacancyStatusAction _objVacancyStatusAction = new BLClient.VacancyStatusAction(_CurrentClientId);
            _RemoveVacancyStatusList = _objVacancyStatusAction.GetVacancyStatusByCategoryAndlanguage(base.CurrentClient.CurrentLanguageId, Category);
            ViewBag.RemoveVacancyStatusList = new SelectList(_RemoveVacancyStatusList, "VacancyStatusId", "VacancyStatusTextLocal");
        }

        #endregion

        public ActionResult ChangeStatus(String id, Guid status, string reason)
        {
            BLClient.VacancyAction vacacnyAction = new BLClient.VacancyAction(base.CurrentClient.ClientId);
            List<Guid> vacancyIds = new List<Guid>();
            foreach (string s in id.Split(new Char[] { ',' }))
            {
                if (!String.IsNullOrEmpty(s))
                    vacancyIds.Add(Guid.Parse(s));
            }
            bool isSuccess = vacacnyAction.UpdateVacancyStatusByVacancy("VacancyStatusId", vacancyIds, status);
            JsonModels actionStatus = null;
            if (isSuccess)
            {
                base.GetJsonContent(false, string.Empty, "Records Updated Successfully");
                try
                {
                    ATSCommon.CommonFunctions.SolrFullImport();
                }
                catch (Exception ex)
                {
                    log.Error("SOLR Employee FULL IMPORT (Create Vacancy)" + ex.Message);
                }
            }
            else
            {
                base.GetJsonContent(true, string.Empty, "Records not Updated");
            }
            /*Convert to json string*/
            string jsonModels = JsonConvert.SerializeObject(actionStatus);
            string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
            /*Redirect to List Page*/
            return RedirectToAction(STR_VACANCY_LIST_METHOD, new { KeyMsg = keyMsg });
        }

        [HttpPost]
        public ActionResult JobLocationAutoComplete(String DivisionId, string term, string displaytop, string columnName)
        {
            if (!string.IsNullOrEmpty(term))
            { term = term + "%"; }
            List<BEClient.JobLocation> _lstJobLocations = null;
            return GetJson(_lstJobLocations, JsonRequestBehavior.AllowGet);
        }

        public ActionResult View(Guid? id, string ordinal, bool ForTemplate = false)
        {
            bool isEdit = false;
            bool IsConsiderToCheck = true;
            BEClient.PageMode objPageMode = BEClient.PageMode.View;
            if (id != null && !id.Equals(Guid.Empty))
            {
                isEdit = true;
            }
            try
            {
                //Create Object of single vacancy
                RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.Vacancy>> _bcModel = null;
                if (isEdit)
                {
                    BLClient.VacancyApplicantAction _ObjVacancyApplicantAction = new BLClient.VacancyApplicantAction(base.CurrentClient.ClientId);
                    CreateObjVacancy((Guid)id);
                    List<BEClient.RecordExists> LstRecordExists = new List<BEClient.RecordExists>();
                    BLClient.RecordExistsAction _objRecordExists = new BLClient.RecordExistsAction(_CurrentClientId);
                    LstRecordExists = _objRecordExists.GetRecordsCountByVacancy((Guid)id);
                    ViewBag.CurrentWebLink = string.Empty;
                    _objVacancy.ActionName = "Edit";
                    _objVacancy.obj = _objVacancyAction.GetVacancyById((Guid)id, base.CurrentClient.CurrentLanguageId);
                    if (_objVacancy.obj == null)
                    {
                        JsonModels actionStatus = ATS.WebUi.Common.CommonFunctions.GetJsonContentObj(false, true, string.Empty, "No Vacancy Found");
                        string jsonModels = JsonConvert.SerializeObject(actionStatus);
                        string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                        TempData["DeleteMsg"] = "No Vacancy Found";
                        return RedirectToAction("Index", "MyVacancy", new { KeyMsg = keyMsg });

                    }
                    _objVacancy.obj.ApplicantVacancyList = _ObjVacancyApplicantAction.GetSubmittedApplicantByVacancyId((Guid)id);

                    _objVacancy.obj.RndCount = _objVacancyAction.GetVacRndCountByVacancy((Guid)id);
                    List<Guid> lstUsers = new List<Guid>();
                    #region security code
                    //foreach (BESrp.UserPermissionWithScope _UserPermissionWithScope in _objResumeList.UserPermissionWithScope)
                    //{
                    //    if (_UserPermissionWithScope.Scope == BEClient.ATSScope.A)
                    //    {
                    //        IsConsiderToCheck = false;
                    //        break;
                    //    }
                    //    else
                    //    {
                    //        switch (_UserPermissionWithScope.Scope)
                    //        {

                    //            case BEClient.ATSScope.S:
                    //                if (_objResumeList.UserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.S).Count() > 0)
                    //                {
                    //                    lstUsers.AddRange(_objResumeList.UserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.S).First().DivisionId);
                    //                }
                    //                continue;
                    //            case BEClient.ATSScope.C:
                    //                if (_objResumeList.UserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.C).Count() > 0)
                    //                {
                    //                    lstUsers.AddRange(_objResumeList.UserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.C).First().DivisionId);
                    //                }
                    //                continue;
                    //            case BEClient.ATSScope.O:
                    //                if (_objResumeList.UserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.O).Count() > 0)
                    //                {
                    //                    lstUsers.AddRange(_objResumeList.UserPermissionWithScope.Where(x => x.Scope == BEClient.ATSScope.O).First().DivisionId);
                    //                }
                    //                continue;
                    //        }
                    //    }
                    //}

                    //if (IsConsiderToCheck == true)
                    //{
                    //    foreach (var v in _objVacancy.obj.ApplicantVacancyList)
                    //    {
                    //        if (!lstUsers.Contains(v.VacancyDivisionId))
                    //        {
                    //            v.IsNotViewable = true;
                    //        }
                    //    }
                    //}
                    #endregion
                    _objVacancy.obj.RecordExistsCount = LstRecordExists;
                    if (_objVacancy.obj == null)
                    {
                        //check is after posted data is different then change posted on
                        _objVacancy.obj = new BEClient.Vacancy();
                        _objVacancy.obj.VacancyId = (Guid)id;
                        _objVacancy.ActionName = "AddByLanguage";

                        ViewBag.Culturename = ATSCommon.CurrentSession.Instance.VerifiedClient.CurrentCulture;
                        ViewBag.CurrentWebLink = this.Url.Action("View", "MyVacancy", new { _objVacancy.obj.VacancyId });
                    }
                    //Which will be used to fill drop down based on rights
                    _objVacancy.UserPermissionWithScope = new List<BESrp.UserPermissionWithScope>();
                    _objVacancy.UserPermissionWithScope.Add(new BESrp.UserPermissionWithScope() { Scope = _objVacancy.obj.Scope, PermissionType = _objVacancy.obj.PermissionType });

                    objPageMode = BEClient.PageMode.Update;
                    if (_objVacancy.obj.PermissionType == null)
                    {
                        return RedirectToAction(ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_CONTROLLER, new { KeyMsg = ATSCommon.CommonFunctions.NonAuthoriseduser() });
                    }
                    if (_objVacancy.obj.PermissionType.Contains(BEClient.ATSPermissionType.Delete))
                    {
                        ViewBag.IsDelete = true;
                    }
                    _objVacancy.RecordPermissionType = _objVacancy.obj.PermissionType;
                    string callMethod = ATS.WebUi.Common.CommonFunctions.GetPageMode(_objVacancy.RecordPermissionType, BEClient.PageMode.Update).ToString();
                    DropDownLists(BEClient.ATSPermissionType.Modify, _objVacancy.obj.DivisionId, callMethod, _objVacancy.obj.PositionOwner, _objVacancy.obj.ShowOnWeb);
                    _bcModel = new BreadScrumbModel<BESrp.DynamicSRP<BEClient.Vacancy>>();
                    _bcModel.obj = _objVacancy;
                    _bcModel.DisplayName = _objVacancy.obj.JobTitle + ", <span style='color: #953634;'>" + _objVacancy.obj.LocationText + "</span>";
                    _bcModel.ImagePath = BECommon.ImagePaths.VacancyImage;
                    _bcModel.ToolTip = _objVacancy.obj.JobTitle + " , " + _objVacancy.obj.LocationText;
                    NavView(id, _bcModel.ToolTip, ordinal);
                    //to be check :Anand
                    GetApplicationStatusMenu((Guid)id, "Active");
                    if (ForTemplate)
                    {
                        ViewBag.IsForTemplate = true;
                    }
                    //Recent Viewed History
                    //Insert current application in Recently View Item 
                    Guid RecentlyViewId = Guid.Empty;
                    BEClient.RecentlyViewed objRecentlView = new BEClient.RecentlyViewed();
                    objRecentlView.Category = BEClient.RecentlyViewedCategory.MyVacancy.ToString();
                    objRecentlView.URL = HttpContext.Request.Url.AbsoluteUri.ToString();
                    objRecentlView.ViewItemId = (Guid)id;
                    RecentlyViewedAction objRecentlyViewedAction = new RecentlyViewedAction(_CurrentClientId);
                    List<BEClient.RecentlyViewed> objRecentViewList = objRecentlyViewedAction.GetRecentlyViewedApplication(objRecentlView);
                    _bcModel.ItemName = _objVacancy.obj.JobTitle;
                    _bcModel.objRecentViewedList = objRecentViewList;
                }
                else
                {
                    CreateObjVacancy(null);
                    _objVacancy.obj = new BEClient.Vacancy();
                    _objVacancy.obj.RecordExistsCount = new List<BEClient.RecordExists>();
                    _objVacancy.obj.VacancyState = 1;
                    _objVacancy.obj.ConfirmVacancyDetails = true;
                    _objVacancy.obj.ShowOnWeb = "";
                    _objVacancy.obj.FeaturedOnWeb = true;
                    _objVacancy.ActionName = "Create";
                    _objVacancy.obj.PositionRequestedBy = Common.CurrentSession.Instance.VerifiedUser.UserId;
                    //Anand for Update Vacancy
                    _objVacancy.obj.ClientId = _CurrentClientId;
                    _objVacancy.obj.LanguageId = _CurrentLanguageId;
                    objPageMode = BEClient.PageMode.Create;
                    _objVacancy.RecordPermissionType = new List<BEClient.ATSPermissionType> { BEClient.ATSPermissionType.Create };
                    string callMethod = ATS.WebUi.Common.CommonFunctions.GetPageMode(_objVacancy.RecordPermissionType, BEClient.PageMode.Create).ToString();
                    DropDownLists(BEClient.ATSPermissionType.Create, _objVacancy.obj.DivisionId, callMethod, _objVacancy.obj.PositionOwner);
                    _bcModel = new BreadScrumbModel<BESrp.DynamicSRP<BEClient.Vacancy>>();
                    _bcModel.obj = _objVacancy;
                    _bcModel.DisplayName = "Add Vacancy";
                    _bcModel.ImagePath = BECommon.ImagePaths.VacancyImage;
                    NavView(id, _bcModel.DisplayName, ordinal);
                }
                ViewBag.PageMode = objPageMode;
                TempData["RecordPermissionType"] = _objVacancy.RecordPermissionType;
                TempData["publishonWeb"] = _objVacancy.obj.ShowOnWeb;
                return View(_bcModel);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string GetApplicationStatusMenu(Guid VacancyId, string Status = "Active")
        {
            BLClient.VacancyApplicantAction _objVacancyApplicantAction = new BLClient.VacancyApplicantAction(_CurrentClientId);
            List<BEClient.VacancyApplicant> objList = _objVacancyApplicantAction.GetApplicationStatusWithCount(VacancyId);
            //TO Remove 'Assigned to me'
            objList = objList.Where(x => x.ApplicantionStatus.ToString() != "Assigned To Me").ToList();
            ViewBag.ApplicationStatusListWithCount = new SelectList(objList, "TotalCount", "ApplicantionStatus");
            BEClient.VacancyApplicant objSeletedStatus = objList.Where(x => x.ApplicantionStatus == Status).ToList()[0];
            ViewBag.SelectedStatus = objSeletedStatus.ApplicantionStatus;
            return base.RenderRazorViewToString("_ApplicationStatusMenu", null);
        }
        public JsonResult GetApplicationStatusCountAndList(Guid VacancyId)
        {
            string Data = "";
            string Message = "";
            bool IsError = false;
            try
            {
                BLClient.VacancyApplicantAction _objVacancyApplicantAction = new BLClient.VacancyApplicantAction(_CurrentClientId);
                List<BEClient.VacancyApplicant> objList = _objVacancyApplicantAction.GetApplicationStatusCountAndList(VacancyId);
                objList = objList.Where(x => x.ApplicantionStatus.ToString() != "Assigned To Me").ToList();
                ViewBag.ApplicationStatusListWithCount = new SelectList(objList, "TotalCount", "ApplicantionStatus");
                Data = base.RenderRazorViewToString("_ApplicantCountWithList", objList);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }
        //Anand 10th march  FeedBack :12:40 10th Mar
        public ActionResult AddVacancyOption()
        {
            BreadScrumbModel<BEClient.Vacancy> _bcModel = new BreadScrumbModel<BEClient.Vacancy>();
            _bcModel.obj = new BEClient.Vacancy();

            _bcModel.DisplayName = "Add Vacancy";
            _bcModel.ImagePath = BECommon.ImagePaths.VacancyImage;
            NavAddVacancyoption(_bcModel.DisplayName, null);
            //Bhavnesh-26-11-2015: Not required to fill dropdown
            ////For Templates
            //BEClient.TVac objTVac = new BEClient.TVac();
            //BLClient.TVacAction objTVacAction = new BLClient.TVacAction(_CurrentClientId, true);
            //List<BEClient.TVac> ObjTVacLst = objTVacAction.GetTVacByPosIdAndDivId(null, _CurrentLanguageId);
            //ViewBag.TVacTemplate = ObjTVacLst;
            //ViewBag.TVacTemplate = new SelectList(ObjTVacLst, "TVacId", "Name");
            return View(_bcModel);
        }

        #region Add Vacancy By Language
        [HttpPost]
        public ActionResult AddByLanguage(BEClient.Vacancy objVacancy)
        {
            try
            {
                objVacancy.ClientId = base.CurrentClient.ClientId;
                objVacancy.LanguageId = base.CurrentClient.CurrentLanguageId;
                objVacancy.PostedOn = System.DateTime.UtcNow;
                Guid newVacancyId = _objVacancyAction.AddVacancyBylanguage(objVacancy);
                JsonModels actionStatus = null;
                if (newVacancyId != null && !newVacancyId.Equals(Guid.Empty))
                {
                    actionStatus = base.GetJsonContent(false, string.Empty, "Record Added Successfully");
                    ATSCommon.CommonFunctions.SolrDeltaImport();
                }
                else
                {
                    actionStatus = base.GetJsonContent(true, string.Empty, "Not Able To Add Record");
                }
                /*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                /*Redirect to List Page*/
                return RedirectToAction(STR_VACANCY_LIST_METHOD, new { KeyMsg = keyMsg });
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);

                /*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);

                /*Redirect to List Page*/
                return RedirectToAction(STR_VACANCY_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }
        #endregion

        #region Add Vacancy
        [HttpPost]

        public ActionResult Create(BESrp.DynamicSRP<BEClient.Vacancy> objBreadScrumbModelVacancy, string btnSubmit)
        {
            BESrp.DynamicSRP<BEClient.Vacancy> objVacancy = objBreadScrumbModelVacancy;

            bool IsServerError = false;
            String ErrorMessage = String.Empty;
            Guid newVacancyId = Guid.Empty;
            string[] Data = new string[2];
            String Data1 = "";
            String Data2 = "";
            try
            {
                ErrorMessage = ServerValidation(ref objVacancy, BEClient.ATSPermissionType.Modify, BEClient.PageMode.Create);


                if (!String.IsNullOrEmpty(ErrorMessage))
                {
                    IsServerError = true;
                }
                if (!IsServerError)
                {

                    if (objVacancy.obj.VacancyId.Equals(Guid.Empty))
                    {
                        objVacancy.obj.ClientId = base.CurrentClient.ClientId;
                        objVacancy.obj.LanguageId = base.CurrentClient.CurrentLanguageId;
                        objVacancy.obj.PostedOn = System.DateTime.Now.Date;
                        objVacancy.obj.PositionID = _objVacancyAction.GetNewPositionId();

                        //Annad:---Start---For Confirmation
                        objVacancy.obj.ConfirmVacancyDetails = true;
                        objVacancy.obj.ConfirmJobDescription = true;
                        objVacancy.obj.ConfirmCompensationInfo = true;
                        objVacancy.obj.ConfirmApplicationreview = true;
                        //---End---
                        TempData["PositionID"] = objVacancy.obj.PositionID;
                        newVacancyId = _objVacancyAction.AddGrantVacancy(objVacancy.obj);
                        if (newVacancyId != null && !newVacancyId.Equals(Guid.Empty))
                        {
                            TempData["publishonWeb"] = objVacancy.obj.ShowOnWeb;
                            CreateObjVacancy(newVacancyId);
                            _objVacancyAction = new BLClient.VacancyAction(_CurrentClientId, true);

                            objVacancy.obj = _objVacancyAction.GetVacancyById(newVacancyId, _CurrentLanguageId);
                            ViewBag.PageMode = BEClient.PageMode.Update;
                            objVacancy.RecordPermissionType = objVacancy.obj.PermissionType;
                            string callMethod = ATS.WebUi.Common.CommonFunctions.GetPageMode(objVacancy.RecordPermissionType, BEClient.PageMode.Update).ToString();
                            DropDownLists(BEClient.ATSPermissionType.Modify, objVacancy.obj.DivisionId, callMethod, objVacancy.obj.PositionOwner);
                            objVacancy.obj.VacancyId = newVacancyId;
                            objVacancy.obj.ApplicantVacancyList = new List<BEClient.VacancyApplicant>();
                            if (objVacancy.obj.PermissionType.Contains(BEClient.ATSPermissionType.Delete))
                            {
                                ViewBag.IsDelete = true;
                            }
                            List<BEClient.RecordExists> LstRecordExists = new List<BEClient.RecordExists>();
                            BLClient.RecordExistsAction _objRecordExists = new BLClient.RecordExistsAction(_CurrentClientId);
                            LstRecordExists = _objRecordExists.GetRecordsCountByVacancy(objVacancy.obj.VacancyId);
                            objVacancy.obj.RecordExistsCount = LstRecordExists;
                            objBreadScrumbModelVacancy = new BESrp.DynamicSRP<BEClient.Vacancy>();
                            objBreadScrumbModelVacancy = objVacancy;
                            Data1 = base.RenderRazorViewToString("Shared/_VacancyDetail", objBreadScrumbModelVacancy);
                            RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.Vacancy>> _bcModel = new BreadScrumbModel<BESrp.DynamicSRP<BEClient.Vacancy>>();
                            _bcModel.obj = objBreadScrumbModelVacancy;


                            Data2 = base.RenderRazorViewToString("Shared/_VacancyGearAction", _bcModel);
                            Data[0] = Data1;
                            Data[1] = Data2;

                            ErrorMessage = "Record Added Successfully";
                            try
                            {
                                ATSCommon.CommonFunctions.SolrFullImport();
                            }
                            catch (Exception ex)
                            {
                                log.Error("SOLR Employee FULL IMPORT (Create Vacancy)" + ex.Message);
                            }

                        }
                        else
                        {
                            ErrorMessage = "Not Able To Add Record";
                            IsServerError = true;

                            return GetJson(base.GetJsonContent(IsServerError, string.Empty, ErrorMessage));
                        }
                    }
                    else
                    {
                        if (btnSubmit == "ConfirmRecord")
                        {
                            objVacancy.obj.IsSaveHistroy = true;
                        }
                        //if (objVacancy.obj.ClientId.Equals(Guid.Empty))
                        //if ((String)TempData["publishonWeb"] != objVacancy.obj.ShowOnWeb)
                        if (objVacancy.obj.ClientId.Equals(Guid.Empty) || objVacancy.obj.PostedOn == DateTime.MinValue || objVacancy.obj.PositionID == null)
                        {

                            objVacancy.obj.ClientId = base.CurrentClient.ClientId;
                            objVacancy.obj.LanguageId = base.CurrentClient.CurrentLanguageId;
                            objVacancy.obj.PostedOn = System.DateTime.Now.Date;
                            objVacancy.obj.PositionID = (decimal?)TempData["PositionID"];
                        }
                        if (TempData["publishonWeb"] != null && (String)TempData["publishonWeb"] != objVacancy.obj.ShowOnWeb)
                        {
                            objVacancy.obj.PostedOn = System.DateTime.Now.Date;
                        }
                        TempData["PositionID"] = objVacancy.obj.PositionID;
                        TempData["publishonWeb"] = objVacancy.obj.ShowOnWeb;
                        bool isRecordUpdated = _objVacancyAction.UpdateGrantVacancy(objVacancy.obj);

                        if (isRecordUpdated)
                        {
                            ViewBag.PageMode = BEClient.PageMode.Update;

                            CreateObjVacancy(objVacancy.obj.VacancyId);

                            _objVacancyAction = new BLClient.VacancyAction(_CurrentClientId, true);

                            objVacancy.obj = _objVacancyAction.GetVacancyById(objVacancy.obj.VacancyId, _CurrentLanguageId);

                            objVacancy.RecordPermissionType = objVacancy.obj.PermissionType;
                            string callMethod = ATS.WebUi.Common.CommonFunctions.GetPageMode(objVacancy.RecordPermissionType, BEClient.PageMode.Update).ToString();

                            DropDownLists(BEClient.ATSPermissionType.Modify, objVacancy.obj.DivisionId, callMethod, objVacancy.obj.PositionOwner);



                            objBreadScrumbModelVacancy = objVacancy;
                            Data1 = base.RenderRazorViewToString("Shared/_VacancyDetail", objBreadScrumbModelVacancy);

                            //******************
                            _objVacancy.obj = objVacancy.obj;

                            _objVacancy.UserPermissionWithScope = new List<BESrp.UserPermissionWithScope>();
                            _objVacancy.UserPermissionWithScope.Add(new BESrp.UserPermissionWithScope() { Scope = _objVacancy.obj.Scope, PermissionType = _objVacancy.obj.PermissionType });
                            BEClient.PageMode objPageMode = BEClient.PageMode.View;
                            objPageMode = BEClient.PageMode.Update;
                            if (_objVacancy.obj.PermissionType == null)
                            {
                                throw new Exception("Non authorized user");
                            }
                            if (_objVacancy.obj.PermissionType.Contains(BEClient.ATSPermissionType.Delete))
                            {
                                ViewBag.IsDelete = true;
                            }
                            _objVacancy.RecordPermissionType = _objVacancy.obj.PermissionType;
                            List<BEClient.RecordExists> LstRecordExists = new List<BEClient.RecordExists>();
                            BLClient.RecordExistsAction _objRecordExists = new BLClient.RecordExistsAction(_CurrentClientId);
                            LstRecordExists = _objRecordExists.GetRecordsCountByVacancy(objVacancy.obj.VacancyId);
                            _objVacancy.obj.RecordExistsCount = LstRecordExists;
                            RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.Vacancy>> _bcModel = new BreadScrumbModel<BESrp.DynamicSRP<BEClient.Vacancy>>();
                            _bcModel.obj = _objVacancy;
                            ViewBag.PageMode = objPageMode;
                            objVacancy.obj.ApplicantVacancyList = new List<BEClient.VacancyApplicant>();
                            BLClient.VacancyApplicantAction _ObjVacancyApplicantAction = new BLClient.VacancyApplicantAction(base.CurrentClient.ClientId);
                            objVacancy.obj.ApplicantVacancyList = _ObjVacancyApplicantAction.GetSubmittedApplicantByVacancyId(objVacancy.obj.VacancyId);
                            Data2 = base.RenderRazorViewToString("Shared/_VacancyGearAction", _bcModel);
                            Data[0] = Data1;
                            Data[1] = Data2;
                            ErrorMessage = "Record Updated Successfully";
                            try
                            {
                                ATSCommon.CommonFunctions.SolrFullImport();
                            }
                            catch (Exception ex)
                            {
                                log.Error("SOLR Employee FULL IMPORT (Create else Vacancy)" + ex.Message);
                            }
                        }
                        else
                        {
                            ErrorMessage = "Not Able To Update Record";
                            IsServerError = true;
                            return GetJson(base.GetJsonContent(IsServerError, string.Empty, ErrorMessage));
                        }
                    }

                    return GetJson(base.GetJsonContent(IsServerError, string.Empty, ErrorMessage, Data));
                }
                else
                {
                    return GetJson(base.GetJsonContent(IsServerError, string.Empty, ErrorMessage));
                }

            }
            catch (Exception ex)
            {
                IsServerError = true;
                ErrorMessage = ex.Message;

            }
            return GetJson(base.GetJsonContent(IsServerError, string.Empty, ErrorMessage, Data));
        }
        #endregion

        #region delete
        //This method will delete record physically from DB
        public ActionResult Delete(Guid id)
        {
            try
            {
                bool Result = _objVacancyAction.Delete(id);

                JsonModels actionStatus = null;
                if (Result)
                {
                    actionStatus = base.GetJsonContent(false, string.Empty, "Record Deleted Successfully");
                }
                else
                {
                    actionStatus = base.GetJsonContent(true, string.Empty, "Not Able To Deleted Record");
                }
                /*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                /*Redirect to List Page*/
                try
                {
                    ATSCommon.CommonFunctions.SolrFullImport();
                }
                catch (Exception ex)
                {
                    log.Error("SOLR Employee FULL IMPORT (Vacancy)" + ex.Message);
                }
                return RedirectToAction(STR_VACANCY_LIST_METHOD, new { KeyMsg = keyMsg });
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);

                ///*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);

                TempData["DeleteMsg"] = keyMsg;
                /*Redirect to List Page*/
                return RedirectToAction(STR_VACANCY_LIST_METHOD, new { KeyMsg = keyMsg });
            }

        }
        #endregion

        #region Edit Vacancy
        [HttpPost]
        public ActionResult Edit(RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.Vacancy>> objBreadScrumbModelVacancy)
        {
            BESrp.DynamicSRP<BEClient.Vacancy> objVacancy = objBreadScrumbModelVacancy.obj;

            bool IsServerError = false;
            String ErrorMessage = String.Empty;
            try
            {

                ErrorMessage = ServerValidation(ref objVacancy, BEClient.ATSPermissionType.Modify, BEClient.PageMode.Update);
                if (!String.IsNullOrEmpty(ErrorMessage))
                {
                    IsServerError = true;
                }
                if (!IsServerError)
                {
                    if ((String)TempData["publishonWeb"] != objVacancy.obj.ShowOnWeb)
                    {
                        objVacancy.obj.PostedOn = System.DateTime.Now.Date;
                    }
                    bool isRecordUpdated = _objVacancyAction.UpdateGrantVacancy(objVacancy.obj);
                    JsonModels actionStatus = null;
                    if (isRecordUpdated)
                    {
                        try
                        {
                            ATSCommon.CommonFunctions.SolrFullImport();
                        }
                        catch (Exception ex)
                        {
                            log.Error("SOLR Employee FULL IMPORT (Vacancy)" + ex.Message);
                        }
                        actionStatus = base.GetJsonContent(!isRecordUpdated, string.Empty, "Record Updated Successfully");
                        ATSCommon.CommonFunctions.SolrFullImport();
                    }
                    else
                    {
                        actionStatus = base.GetJsonContent(!isRecordUpdated, string.Empty, "Not Able To Update Record");
                    }
                    /*Convert to json string*/
                    string jsonModels = JsonConvert.SerializeObject(actionStatus);
                    string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                    /*Redirect to List Page*/
                    return RedirectToAction(STR_VACANCY_LIST_METHOD, new { KeyMsg = keyMsg });
                }
            }
            catch (Exception ex)
            {
                IsServerError = true;
                ErrorMessage = ex.Message;

            }
            //This function will allocate data to _objVacancy object;
            setFormValues(IsServerError, ErrorMessage, objVacancy.obj, BEClient.PageMode.Update);
            objBreadScrumbModelVacancy.obj = objVacancy;
            return View(STR_VACANCY_VIEW_METHOD, objBreadScrumbModelVacancy);
        }
        #endregion

        private void setFormValues(bool IsServerError, string ErrorMessage, BEClient.Vacancy oldVacacnyValue, BEClient.PageMode pageMode)
        {
            //Create Object with role and security _ObjVacancy 
            CreateObjVacancy(oldVacacnyValue.VacancyId);
            //set Record Permission Type from Temp
            _objVacancy.RecordPermissionType = (List<ATS.BusinessEntity.ATSPermissionType>)TempData["RecordPermissionType"];
            //Allocate Form value to ne object
            _objVacancy.obj = oldVacacnyValue;
            DropDownLists(BEClient.ATSPermissionType.Modify, _objVacancy.obj.DivisionId, BEClient.PageMode.Update.ToString(), _objVacancy.obj.PositionOwner);
            ViewBag.PageMode = pageMode;
            TempData["publishonWeb"] = _objVacancy.obj.ShowOnWeb;
            TempData["RecordPermissionType"] = _objVacancy.RecordPermissionType;
            BLClient.VacancyApplicantAction _ObjVacancyApplicantAction = new BLClient.VacancyApplicantAction(base.CurrentClient.ClientId);
            _objVacancy.obj.ApplicantVacancyList = _ObjVacancyApplicantAction.GetSubmittedApplicantByVacancyId(_objVacancy.obj.VacancyId);
            ViewBag.IsError = IsServerError;
            ViewBag.Message = ErrorMessage;
            BLClient.JobLocationAction _objJobLocationAction = new BLClient.JobLocationAction(base.CurrentClient.ClientId);
            BLClient.PositionTypeAction _objPositionTypeAction = new BLClient.PositionTypeAction(base.CurrentClient.ClientId);
            List<BEClient.JobLocation> _JobLocation = new List<BEClient.JobLocation>();
            List<BEClient.PositionType> _Positiontype = new List<BEClient.PositionType>();
            _objVacancyAction.SetRoleRecordWise(_objVacancy.obj, _objVacancy.obj.DivisionId);
            List<BEClient.Division> lstDivision = new List<BEClient.Division>();
            //Allocate current record rights
            _objVacancy.UserPermissionWithScope = new List<BESrp.UserPermissionWithScope>();
            _objVacancy.UserPermissionWithScope.Add(new BESrp.UserPermissionWithScope() { Scope = _objVacancy.obj.Scope, PermissionType = _objVacancy.obj.PermissionType });
            if (!_objVacancy.obj.DivisionId.Equals(Guid.Empty))
            {
                _JobLocation = _objJobLocationAction.GetJobLocationByDivision(_UserId, _objVacancy.obj.DivisionId, _CurrentLanguageId);
                _Positiontype = _objPositionTypeAction.GetPositionTypeByDivision(_UserId, _objVacancy.obj.DivisionId, _CurrentLanguageId);
                lstDivision = _objVacancyAction.GetAllDivisionByScope(base.CurrentClient.CurrentLanguageId, _objVacancy.UserPermissionWithScope, BEClient.ATSPermissionType.Modify);
                ViewBag.PositionType = new SelectList(_Positiontype, "PositionTypeId", "LocalName");
                ViewBag._JobLocationList = new SelectList(_JobLocation, "JobLocationId", "LocalName");
                ViewBag.Division = new SelectList(lstDivision, "DivisionId", "DivisionName");
            }
        }

        #region Remove Vacancy
        //This method will change the delete status of the vacancy
        public ActionResult Remove(Guid id)
        {
            try
            {
                if (id != Guid.Empty)
                {
                    bool isDeleted = _objVacancyAction.DeleteVacancy(id);
                    JsonModels actionStatus = null;
                    if (isDeleted)
                    {
                        actionStatus = base.GetJsonContent(!isDeleted, string.Empty, "Record Deleted Successfully");
                        Common.CommonFunctions.SolrEmployeeFullImport();

                        //TODO : Commented by shanil Solr will take some time for delta import so we have to think about it for clean up DeletedVacancyTable.
                        //_objVacancyAction.DeleteDeletedVacancy(id, _ClientId);
                        try
                        {
                            ATSCommon.CommonFunctions.SolrFullImport();
                        }
                        catch (Exception ex)
                        {
                            log.Error("SOLR Employee FULL IMPORT (Vacancy)" + ex.Message);
                        }
                    }
                    else
                    {
                        actionStatus = base.GetJsonContent(!isDeleted, string.Empty, "No record Found");
                    }
                    /*Convert to json string*/
                    string jsonModels = JsonConvert.SerializeObject(actionStatus);
                    string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                    /*Redirect to List Page*/
                    TempData["DeleteMsg"] = keyMsg;
                    return RedirectToAction(STR_VACANCY_LIST_METHOD, new { KeyMsg = keyMsg });
                }
                else
                {
                    /*Redirect to Add*/
                    return RedirectToAction(STR_VACANCY_CREATE_METHOD);
                }
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);

                /*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                TempData["DeleteMsg"] = keyMsg;
                /*Redirect to List Page*/
                return RedirectToAction(STR_VACANCY_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }
        public ActionResult DeleteMultipleVacancy(String ids)
        {
            try
            {
                string curStatus = "";
                if (HttpContext.Request.UrlReferrer.Query.ToLower().Contains("status"))
                {
                    curStatus = (HttpContext.Request.UrlReferrer.Query.Substring(HttpContext.Request.UrlReferrer.Query.ToLower().IndexOf("status=") + 7).Split('&')[0]);
                }

                bool Result = _objVacancyAction.DeleteMultipleVacancy(ids);
                JsonModels actionStatus = null;
                if (Result)
                {
                    actionStatus = base.GetJsonContent(false, string.Empty, "Record(s) Deleted Successfully");
                }
                else
                {
                    actionStatus = base.GetJsonContent(true, string.Empty, "Not Able To Deleted Record");
                }
                /*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                TempData["DeleteMsg"] = keyMsg;
                /*Redirect to List Page*/
                try
                {
                    ATSCommon.CommonFunctions.SolrFullImport();
                }
                catch (Exception ex)
                {
                    log.Error("SOLR Employee FULL IMPORT (Vacancy)" + ex.Message);
                }
                if (curStatus != string.Empty)
                    return RedirectToAction(STR_VACANCY_LIST_METHOD, new { Status = curStatus, KeyMsg = keyMsg });
                else
                    return RedirectToAction(STR_VACANCY_LIST_METHOD, new { KeyMsg = keyMsg });
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);

                ///*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);

                TempData["DeleteMsg"] = keyMsg;
                /*Redirect to List Page*/
                return RedirectToAction(STR_VACANCY_LIST_METHOD, new { KeyMsg = keyMsg });
            }

        }
        #endregion

        #region Ajax Post Methods
        [HttpPost]
        public ActionResult UpdateShowOnWeb(String pVacancyId, String pFieldValue)
        {
            try
            {
                if (!String.IsNullOrEmpty(pVacancyId))
                {
                    Guid vacancyid = Guid.Empty;
                    Guid.TryParse(pVacancyId, out vacancyid);
                    DateTime pPostedOn = DateTime.Now.Date;
                    bool fieldvalue = false;
                    if (Boolean.TrueString.ToLower() == pFieldValue.ToLower())
                    {
                        fieldvalue = true;
                    }
                    BLClient.VacancyAction _objVacancyAction = new BLClient.VacancyAction(base.CurrentClient.ClientId);
                    var result = _objVacancyAction.UpdateShowOnWeb(vacancyid, fieldvalue, pPostedOn);
                    if (result)
                    {
                        Common.CommonFunctions.SolrFullImport();
                        return GetJson(base.GetJsonContent(false, null, null, pPostedOn.ToString(ATS.WebUi.Common.Constants.VACANCY_DATE_FORMAT)));
                    }
                    else
                        return GetJson(base.GetJsonContent(true, null, "Vacancy Not Updated"));
                }
                else
                {
                    return GetJson(base.GetJsonContent(true, null, "Not valid vacancy"));
                }
            }
            catch (Exception ex)
            {
                return GetJson(base.GetJsonContent(true, null, ex.Message));
            }
        }


        [HttpPost]
        public ActionResult UpdateVacancyStatus(Guid VacancyId, String VacancyStatus)
        {
            String DisplayMessage = string.Empty;
            try
            {
                DateTime pPostedOn = DateTime.Now.Date;
                BLClient.VacancyAction _objVacancyAction = new BLClient.VacancyAction(base.CurrentClient.ClientId);
                var result = _objVacancyAction.UpdateVacancyStatusByVacancyId(VacancyId, VacancyStatus, pPostedOn);
                if (result)
                {
                    Common.CommonFunctions.SolrFullImport();
                    return GetJson(base.GetJsonContent(false, null, null, pPostedOn.ToString(ATS.WebUi.Common.Constants.VACANCY_DATE_FORMAT)));
                }
                else
                {
                    DisplayMessage = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.ErrorMessages.NOT_ABLE_TO_UPDATE_RECORD).ToString();
                    return GetJson(base.GetJsonContent(true, null, DisplayMessage));
                }
            }
            catch (Exception ex)
            {
                return GetJson(base.GetJsonContent(true, null, ex.Message));
            }
        }


        [HttpPost]
        public ActionResult UpdateVacStatus(Guid VacancyId, String VacancyStatus, String VacancyReason)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            String DisplayMessage = string.Empty;
            try
            {

                BEClient.Vacancy objVacancy = new BEClient.Vacancy();
                BLClient.VacancyAction ObjVacancyAction = new BLClient.VacancyAction(_CurrentClientId);
                objVacancy = ObjVacancyAction.GetVacancyById(VacancyId, _CurrentLanguageId);
                if (objVacancy != null && objVacancy.ConfirmVacancyDetails && objVacancy.ConfirmJobDescription && objVacancy.ConfirmCompensationInfo && objVacancy.ConfirmApplicationreview)
                {
                    Guid VacReason = new Guid();
                    if (VacancyStatus == BEClient.VacancyStatusDrp.Close.ToString())
                    {
                        VacReason = new Guid(VacancyReason);
                    }
                    else
                    {
                        BLClient.VacancyStatusAction _objVacancyStatusAction = new BLClient.VacancyStatusAction(_CurrentClientId);
                        VacReason = _objVacancyStatusAction.GetVacancyReasonIdByVacancyStatus(VacancyStatus);
                    }

                    DateTime pPostedOn = DateTime.Now.Date;
                    BLClient.VacancyAction _objVacancyAction = new BLClient.VacancyAction(base.CurrentClient.ClientId);
                    var result = _objVacancyAction.UpdateVacStatusAndVacReasonByVacancyId(VacancyId, VacancyStatus, VacReason, pPostedOn);
                    if (result)
                    {
                        Common.CommonFunctions.SolrFullImport();
                        FillVacancyStatusDropDown(BEClient.VacancyStatusDrp.Close.ToString());
                        BLClient.VacancyAction VacancyAction = new BLClient.VacancyAction(_CurrentClientId, true);
                        BEClient.Vacancy ObjVacancy = new BEClient.Vacancy();
                        ObjVacancy = VacancyAction.GetVacancyById(VacancyId, _CurrentLanguageId);
                        Data = base.RenderRazorViewToString("Shared/_MyVacancyListSingle", ObjVacancy);
                        Message = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.SuccessMessages.RECORD_UPDATED_SUCCESSFULLY).ToString();

                    }
                    else
                    {
                        DisplayMessage = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.ErrorMessages.NOT_ABLE_TO_UPDATE_RECORD).ToString();
                        return GetJson(base.GetJsonContent(true, null, DisplayMessage));
                    }
                }
                else
                {
                    DisplayMessage = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.ErrorMessages.FIRST_CONFIRM_THE_VACANCY).ToString();
                    throw new Exception(DisplayMessage);
                }
            }
            catch (Exception ex)
            {
                return GetJson(base.GetJsonContent(true, null, ex.Message));
            }
            return GetJson(base.GetJsonContent(IsError, null, Message, Data));
            //return RedirectToAction("Index", new { KeyMsg = Message });
        }


        [HttpPost]
        public ActionResult UpdateVacancyVisibility(string pVacancyId, string pFieldName, string pFieldValue)
        {
            try
            {
                if (!String.IsNullOrEmpty(pVacancyId))
                {
                    Guid vacancyid = Guid.Empty;
                    Guid.TryParse(pVacancyId, out vacancyid);

                    string fieldvalue = string.Empty;
                    if (Boolean.TrueString.ToLower() == pFieldValue.ToLower())
                    {
                        fieldvalue = "1";
                    }
                    else
                    { fieldvalue = "0"; }

                    BLClient.VacancyAction _objVacancyAction = new BLClient.VacancyAction(base.CurrentClient.ClientId);
                    var result = _objVacancyAction.UpdateVacancyByfield(pFieldName, vacancyid, fieldvalue);
                    if (result)
                    {
                        Common.CommonFunctions.SolrFullImport();
                        return GetJson(base.GetJsonContent(false, null));
                    }
                    else
                        return GetJson(base.GetJsonContent(true, null, "Vacancy Not Updated"));
                }
                else
                {
                    return GetJson(base.GetJsonContent(true, null, "Not valid vacancy"));
                }
            }
            catch (Exception ex)
            {
                return GetJson(base.GetJsonContent(true, null, ex.Message));
            }

        }
        #endregion
        #region CreateURL
        public string RemoveVacancyURL(Guid vacancyId)
        {
            UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);
            return u.Action("Remove", "MyVacancy", new { id = vacancyId.ToString() });
        }
        public string AddVacancyURL()
        {
            UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);

            //Anand 10th March FeedBack: 12:44 10th March
            return u.Action("AddVacancyOption", "MyVacancy");
        }
        #endregion


        #region

        private String ServerValidationForCom(ref BESrp.DynamicSRP<BEClient.Vacancy> objVacancy, BEClient.ATSPermissionType atsPermissionType, BEClient.PageMode objpageMode)
        {
            String ErrorMessage = String.Empty;
            bool isServerError = false;
            try
            {


                if (objVacancy.obj.SalaryMin > objVacancy.obj.SalaryMax && !isServerError)
                {
                    isServerError = true;
                    ErrorMessage = String.Format("{0} {1}", Resources.Resources.LanguageDisplay(BEClient.Common.VacancyConstant.FRM_VAC_SALARY_MAX), "is less then " + Resources.Resources.LanguageDisplay(BEClient.Common.VacancyConstant.FRM_VAC_SALARY_MIN));
                }

                if (objVacancy.obj.HourlyMin > objVacancy.obj.HourlyMax && !isServerError)
                {
                    isServerError = true;
                    ErrorMessage = String.Format("{0} {1}", Resources.Resources.LanguageDisplay(BEClient.Common.VacancyConstant.FRM_VAC_HOURLY_MAX), "is less then " + Resources.Resources.LanguageDisplay(BEClient.Common.VacancyConstant.FRM_VAC_HOURLY_MIN));
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                isServerError = true;

            }
            return ErrorMessage;
        }

        private String ServerValidation(ref BESrp.DynamicSRP<BEClient.Vacancy> objVacancy, BEClient.ATSPermissionType atsPermissionType, BEClient.PageMode objpageMode)
        {
            String ErrorMessage = String.Empty;
            bool isServerError = false;
            if (!ModelState.IsValid)
            {
                // do something to display errors . 
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        ErrorMessage = error.ErrorMessage;
                        isServerError = true;
                        break;
                    }
                    if (isServerError)
                        break;

                }
            }
            if (objVacancy.obj.SalaryMin > objVacancy.obj.SalaryMax && !isServerError)
            {
                isServerError = true;
                ErrorMessage = String.Format("{0} {1}", Resources.Resources.LanguageDisplay(BEClient.Common.VacancyConstant.FRM_VAC_SALARY_MAX), "is less then " + Resources.Resources.LanguageDisplay(BEClient.Common.VacancyConstant.FRM_VAC_SALARY_MIN));
            }

            if (objVacancy.obj.HourlyMin > objVacancy.obj.HourlyMax && !isServerError)
            {
                isServerError = true;
                ErrorMessage = String.Format("{0} {1}", Resources.Resources.LanguageDisplay(BEClient.Common.VacancyConstant.FRM_VAC_HOURLY_MAX), "is less then " + Resources.Resources.LanguageDisplay(BEClient.Common.VacancyConstant.FRM_VAC_HOURLY_MIN));
            }
            if (objVacancy.obj.StartDate > objVacancy.obj.EndDate && !isServerError)
            {
                isServerError = true;
                ErrorMessage = String.Format("{0}", Resources.Resources.LanguageDisplay(BEClient.Common.CommonConstant.INVALID_DATE));
            }
            if (!isServerError)
            {
                if (objVacancy.obj.JobType == 2 && (objVacancy.obj.HoursPerWeek == null || objVacancy.obj.HoursPerWeek == 0))
                {
                    isServerError = true;
                    ErrorMessage = String.Format("{0} is required.", Resources.Resources.LanguageDisplay(BEClient.Common.VacancyConstant.FRM_VAC_HOURS_PER_WEEK));
                }
            }
            return ErrorMessage;
        }
        #endregion

        public JsonResult GetJobLocationBasedOnUserAndDivision(string divisionId)
        {
            try
            {
                BLClient.JobLocationAction _objJobLocationAction = new BLClient.JobLocationAction(base.CurrentClient.ClientId);
                BLClient.PositionTypeAction _objPositionTypeAction = new BLClient.PositionTypeAction(base.CurrentClient.ClientId);
                BLClient.UserAction _objUserAction = new BLClient.UserAction(base.CurrentClient.ClientId);
                BEClient.User currentUser = ATSCommon.CurrentSession.Instance.VerifiedUser;
                List<BEClient.JobLocation> _JobLocation = new List<BEClient.JobLocation>();
                List<BEClient.PositionType> _Positiontype = new List<BEClient.PositionType>();
                List<BEClient.User> _PositionOwner = new List<BEClient.User>();
                List<BEClient.User> _OnboardingUsers = new List<BEClient.User>();

                if (!divisionId.Equals(string.Empty))
                {
                    _JobLocation = _objJobLocationAction.GetJobLocationByDivision(_UserId, new Guid(divisionId), _CurrentLanguageId);
                    _Positiontype = _objPositionTypeAction.GetPositionTypeByDivision(_UserId, new Guid(divisionId), _CurrentLanguageId);
                    _PositionOwner = _objUserAction.GetAllUserByDivisionId(new Guid(divisionId));
                    if (!_PositionOwner.Any(r => r.UserId == currentUser.UserId))
                    {
                        _PositionOwner.Add(currentUser);
                    }
                    _objUserAction = new BLClient.UserAction(_CurrentClientId);
                    _OnboardingUsers = _objUserAction.GetAllUOnboardingUser(new Guid(divisionId));
                }
                return GetJson(new { JobLocationId = _JobLocation.Select(r => r.JobLocationId), LocalName = _JobLocation.Select(r => r.LocalName), PositypeTypeId = _Positiontype.Select(r => r.PositionTypeId), LocalNamePosition = _Positiontype.Select(p => p.LocalName), UserId = _PositionOwner.Select(r => r.UserId), FullName = _PositionOwner.Select(r => r.FullName), OnboardManagerId = _OnboardingUsers.Select(r => r.UserId), FullNameUsers = _OnboardingUsers.Select(r => r.FullName) });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public JsonResult AddNewRoundConfig(Guid? VacancyId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            BEClient.PageMode objPageMode = BEClient.PageMode.Create;
            Guid _VacancyId = Guid.Empty;
            try
            {
                BEClient.VacancyRound objVacancyRound = NewVacancyRound();
                if (Guid.TryParse(VacancyId.ToString(), out  _VacancyId))
                    objVacancyRound.VacancyId = _VacancyId;

                FillComboRndType(VacancyId);

                if (Enumerable.Count(ViewBag.RoundType) == 0)
                {
                    return base.GetJson(base.GetJsonContent(true, null, "All rounds are already configured.", Data), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ViewBag.PageMode = objPageMode;
                    objVacancyRound.Score = -1;
                    Data = base.RenderRazorViewToString("Shared/RoundConfig/_CreateVacRndAcc", objVacancyRound);
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        public BEClient.VacancyRound NewVacancyRound()
        {
            BEClient.VacancyRound objVacancyRound = new BEClient.VacancyRound();
            objVacancyRound.ReqReviewer = 1;
            objVacancyRound.PromotionThresoldScore = 75;
            objVacancyRound.RoundWeight = 50;
            objVacancyRound.AssignCandidateBatches = 1;

            return objVacancyRound;
        }

        public void FillComboRndType(Guid? VacancyId, int RndAttr = 0)
        {
            try
            {
                string VacId = Convert.ToString(VacancyId);
                Guid reResult = Guid.Empty;
                if (VacId != null)
                {
                    Guid.TryParse(VacId, out reResult);
                }
                List<BEClient.RndType> lstRndType = new List<BEClient.RndType>();
                BLClient.RndTypeAction objRndTypeAction = new BLClient.RndTypeAction(base.CurrentClient.ClientId);
                lstRndType = objRndTypeAction.GetRndTypeForRndConfig(reResult, base.CurrentClient.CurrentLanguageId);
                ViewBag.RoundType = new SelectList(lstRndType, "RndTypeId", "DefaultName");
                var targets = new Dictionary<string, string>();
                targets.Add("0", "Round-Robin fashion");
                ViewBag.AssignCandidatetoReviewers = new SelectList(targets, "Key", "Value");
                ViewBag.YesNoDropDownPromoteCandidate = Common.CommonFunctions.YesNoDropDownList();
                if (RndAttr == (int)ATS.BusinessEntity.RndAttrNo.OfferRound)
                {
                    List<BEClient.OfferTemplates> LstOfferTemplates = BLCommon.CacheHelper.GetAllOffertemplates(_CurrentClientId, VacId.ToString(), null, true);
                    ViewBag.OfferTemplates = new SelectList(LstOfferTemplates, "OfferTemplateId", "OfferTemplateName");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RoundConfigDropDown()
        {
            try
            {
                List<BEClient.RndType> lstRndType = new List<BEClient.RndType>();
                BLClient.RndTypeAction objRndTypeAction = new BLClient.RndTypeAction(base.CurrentClient.ClientId);
                //lstDivision = _objVacancyAction.GetAllDivisionByScope(base.CurrentClient.CurrentLanguageId, _objVacancy.UserPermissionWithScope, PermissionType);
                lstRndType = objRndTypeAction.GetAllRndTypeByLanguage(base.CurrentClient.CurrentLanguageId);
                List<BEClient.RndType> newOrderedList = lstRndType.OrderBy(x => x.DefaultName.ToString()).ToList();
                ViewBag.RoundType = new SelectList(lstRndType, "RndTypeId", "DefaultName");
                var targets = new Dictionary<string, string>();
                targets.Add("0", "Round-Robin fashion");
                ViewBag.AssignCandidatetoReviewers = new SelectList(targets, "Key", "Value");
                ViewBag.YesNoDropDownPromoteCandidate = Common.CommonFunctions.YesNoDropDownList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateRoundConfigDropDown(Guid VacId, Guid VacRndId, int RndAttrNo = 0)
        {
            try
            {
                List<BEClient.RndType> lstRndType = new List<BEClient.RndType>();
                BLClient.RndTypeAction objRndTypeAction = new BLClient.RndTypeAction(base.CurrentClient.ClientId);

                BEClient.RndType objRoundDetails = new BEClient.RndType();
                objRoundDetails = objRndTypeAction.GetRoundDetailsByVacRndId(VacRndId, _CurrentLanguageId);
                lstRndType.Add(objRoundDetails);
                //lstDivision = _objVacancyAction.GetAllDivisionByScope(base.CurrentClient.CurrentLanguageId, _objVacancy.UserPermissionWithScope, PermissionType);
                //lstRndType = objRndTypeAction.GetAllRndTypeByVacancy(VacId, VacRndId, base.CurrentClient.CurrentLanguageId);
                List<BEClient.RndType> newOrderedList = lstRndType.OrderBy(x => x.DefaultName.ToString()).ToList();
                ViewBag.RoundType = new SelectList(lstRndType, "RndTypeId", "DefaultName");
                var targets = new Dictionary<string, string>();
                targets.Add("0", "Round-Robin fashion");
                ViewBag.AssignCandidatetoReviewers = new SelectList(targets, "Key", "Value");
                ViewBag.YesNoDropDownPromoteCandidate = Common.CommonFunctions.YesNoDropDownList();
                if (RndAttrNo == (int)ATS.BusinessEntity.RndAttrNo.OfferRound)
                {
                    List<BEClient.OfferTemplates> LstOfferTemplates = BLCommon.CacheHelper.GetAllOffertemplates(_CurrentClientId, VacId.ToString(), null, true);
                    ViewBag.OfferTemplates = new SelectList(LstOfferTemplates, "OfferTemplateId", "OfferTemplateName");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void GetRndConfigDetail(Guid VacancyRndId, out BEClient.ApplicationReviewProcess objApplicationReviewProcess)
        {
            try
            {
                objApplicationReviewProcess = new BEClient.ApplicationReviewProcess();
                BLClient.VacancyRoundAction objVacancyRoundAction = new BLClient.VacancyRoundAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                objApplicationReviewProcess.objVacancyRound = new BEClient.VacancyRound();
                objApplicationReviewProcess.objVacancyRound = objVacancyRoundAction.GetRoundConfigByRoundId(VacancyRndId, base.CurrentClient.CurrentLanguageId);
                //FillComboRndType(objApplicationReviewProcess.objVacancyRound.VacancyId);
                //RoundConfigDropDown();
                UpdateRoundConfigDropDown(objApplicationReviewProcess.objVacancyRound.VacancyId, VacancyRndId, objApplicationReviewProcess.objVacancyRound.RoundAttributeNo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public JsonResult AddRoundConfig(BEClient.VacancyRound objVacancyRound)
        {
            String Message = String.Empty;
            bool IsError = true;
            String Data = "";
            try
            {
                if (objVacancyRound.RoundWeight == 0)
                    objVacancyRound.RoundWeight = 50;

                if (!String.IsNullOrEmpty(Message))
                {
                    Exception ex = new Exception(Message);
                    throw ex;
                }
                BLClient.VacancyRoundAction objVacancyRoundAction = new BLClient.VacancyRoundAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                Guid NewrecordAdded = Guid.Empty;
                NewrecordAdded = objVacancyRoundAction.AddVacancyRound(objVacancyRound);
                if (!NewrecordAdded.Equals(Guid.Empty))
                {
                    IsError = false;
                    Message = "Add Record Successfully";
                    BEClient.ApplicationReviewProcess objApplicationReviewProcess = new BEClient.ApplicationReviewProcess();

                    //Start CRNOV25 
                    BLClient.VacancyRoundAction objVacancyRndAction = new BLClient.VacancyRoundAction(_CurrentClientId);
                    string RndType = GetRndTypeIdByCandidateSelfEvalution();
                    if (!String.IsNullOrWhiteSpace(RndType))
                    {
                        bool result = objVacancyRndAction.UpdateIsScreening(RndType, NewrecordAdded);
                    }
                    //End CRNOV25
                    GetRndConfigDetail(NewrecordAdded, out objApplicationReviewProcess);
                    //Assign -1 for hide score div from view
                    objApplicationReviewProcess.objVacancyRound.Score = "-1";
                    objApplicationReviewProcess.objVacancyRound = objVacancyRound;                    
                    Data = base.RenderRazorViewToString("Shared/ApplicationReviewProcess", objApplicationReviewProcess);
                }
                else
                {
                    Message = "Not able to add record";
                    return GetJson(base.GetJsonContent(IsError, string.Empty, Message));
                }

            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
        }

        [HttpPost]
        public JsonResult UpdateRoundConfig(BEClient.VacancyRound objVacancyRound)
        {
            String Message = String.Empty;
            bool IsError = true;
            String Data = "";
            try
            {
                if (!String.IsNullOrEmpty(Message))
                {
                    Exception ex = new Exception(Message);
                    throw ex;
                }
                BLClient.VacancyRoundAction objVacancyRoundAction = new BLClient.VacancyRoundAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                Guid NewrecordAdded = Guid.Empty;
                var result = objVacancyRoundAction.Update(objVacancyRound);
                if (result == true)
                {
                    NewrecordAdded = objVacancyRound.VacancyRoundId;
                    IsError = false;
                    Message = "Update Record Successfully";
                    BEClient.ApplicationReviewProcess objApplicationReviewProcess = new BEClient.ApplicationReviewProcess();
                    GetRndConfigDetail(NewrecordAdded, out objApplicationReviewProcess);



                    Data = base.RenderRazorViewToString("Shared/RoundConfig/RoundConfiguration", objApplicationReviewProcess);
                }
                else
                {
                    Message = "Not able to update record";
                    return GetJson(base.GetJsonContent(IsError, string.Empty, Message));
                }

                //if (IsError == false)
                //{
                //    BEClient.ApplicationReviewProcess objApplicationReviewProcess = new BEClient.ApplicationReviewProcess();
                //    objApplicationReviewProcess.objVacancyRound = new BEClient.VacancyRound();
                //    objVacancyRoundAction = new BLClient.VacancyRoundAction(base.CurrentClient.ClientId);
                //    objApplicationReviewProcess.objVacancyRound = objVacancyRoundAction.GetRoundConfigByRoundId(NewrecordAdded, base.CurrentClient.CurrentLanguageId);
                //    RoundConfigDropDown();
                //    Data = base.RenderRazorViewToString("Shared/ApplicationReviewProcess", objApplicationReviewProcess);
                //}
                //else
                //{

                //}
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult AddJobDescription(ATS.BusinessEntity.SRPEntity.DynamicSRP<ATS.BusinessEntity.Vacancy> objBreadScrumVacancy, string btnSubmit)
        {
            ATS.BusinessEntity.SRPEntity.DynamicSRP<ATS.BusinessEntity.Vacancy> objVacancy = objBreadScrumVacancy;
            try
            {
                String Message = String.Empty;
                bool IsError = false;
                String Data = "";
                try
                {
                    if (btnSubmit == "ConfirmRecord")
                    {
                        objVacancy.obj.IsSaveHistroy = true;
                    }
                    bool result = _objVacancyAction.UpdateJobDescriptionByVacancyId(objVacancy.obj);
                    if (result)
                    {
                        Message = "Updated Successfully";
                        CreateObjVacancy(objVacancy.obj.VacancyId);
                        _objVacancyAction = new BLClient.VacancyAction(_CurrentClientId, true);
                        _objVacancy.obj = _objVacancyAction.GetJobDescriptionByVacancyId(objVacancy.obj.VacancyId, _CurrentLanguageId);
                        ViewBag.PageMode = BEClient.PageMode.Update;
                        _objVacancy.RecordPermissionType = _objVacancy.obj.PermissionType;
                        objBreadScrumVacancy = _objVacancy;
                        Data = base.RenderRazorViewToString("Shared/_JobDescription", objBreadScrumVacancy);
                    }
                    else
                    {
                        Message = "Not Able To Add Record";
                        return GetJson(base.GetJsonContent(IsError, string.Empty, Message));
                    }
                }
                catch (Exception ex)
                {
                    Message = ex.Message;
                    IsError = true;
                }
                try
                {
                    ATSCommon.CommonFunctions.SolrFullImport();
                }
                catch (Exception ex)
                {
                    log.Error("SOLR Vacancy Full IMPORT (AddJobDescription)" + ex.Message);
                }
                return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool IsSaveButtonShow(List<BESrp.UserPermissionWithScope> objUserPermissionWithScopelst, Guid divisionId)
        {
            try
            {
                List<Guid> lstUsers = new List<Guid>();
                bool IsConsiderToCheck = true;
                if (objUserPermissionWithScopelst != null)
                {
                    foreach (BESrp.UserPermissionWithScope _UserPermissionWithScope in objUserPermissionWithScopelst)
                    {
                        if (_UserPermissionWithScope.Scope == BEClient.ATSScope.A)
                        {
                            IsConsiderToCheck = false;
                            break;
                        }
                        else
                        {
                            switch (_UserPermissionWithScope.Scope)
                            {
                                case BEClient.ATSScope.S:
                                    if (objUserPermissionWithScopelst.Where(x => x.Scope == BEClient.ATSScope.S).Count() > 0)
                                    {
                                        lstUsers.AddRange(objUserPermissionWithScopelst.Where(x => x.Scope == BEClient.ATSScope.S).First().DivisionId);
                                    }
                                    continue;
                                case BEClient.ATSScope.C:
                                    if (objUserPermissionWithScopelst.Where(x => x.Scope == BEClient.ATSScope.C).Count() > 0)
                                    {
                                        lstUsers.AddRange(objUserPermissionWithScopelst.Where(x => x.Scope == BEClient.ATSScope.C).First().DivisionId);
                                    }
                                    continue;
                                case BEClient.ATSScope.O:
                                    if (objUserPermissionWithScopelst.Where(x => x.Scope == BEClient.ATSScope.O).Count() > 0)
                                    {
                                        lstUsers.AddRange(objUserPermissionWithScopelst.Where(x => x.Scope == BEClient.ATSScope.O).First().DivisionId);
                                    }
                                    continue;
                            }
                        }
                    }

                    if (IsConsiderToCheck == true)
                    {
                        if (lstUsers.Contains(divisionId))
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult AddCompensationInfo(ATS.BusinessEntity.SRPEntity.DynamicSRP<ATS.BusinessEntity.Vacancy> objBreadScrumVacancy, string btnSubmit)
        {
            ATS.BusinessEntity.SRPEntity.DynamicSRP<ATS.BusinessEntity.Vacancy> objVacancy = objBreadScrumVacancy;
            String Data = string.Empty;
            String Message = String.Empty;
            bool IsError = false;
            try
            {
                if (btnSubmit == "ConfirmRecord")
                {
                    objVacancy.obj.IsSaveHistroy = true;
                }

                Message = ServerValidationForCom(ref objVacancy, BEClient.ATSPermissionType.Modify, BEClient.PageMode.Update);
                if (!String.IsNullOrEmpty(Message))
                {
                    IsError = true;
                    return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
                }
                bool result = _objVacancyAction.UpdateCompensationInfoByVacancyId(objVacancy.obj);
                if (result)
                {
                    IsError = false;
                    Message = "Updated Successfully";
                    CreateObjVacancy(objVacancy.obj.VacancyId);
                    ViewBag.PageMode = BEClient.PageMode.Update;
                    _objVacancyAction = new BLClient.VacancyAction(base.CurrentClient.ClientId, true);
                    ViewBag.PageMode = BEClient.PageMode.Update;
                    _objVacancy.obj = _objVacancyAction.GetCompensationInfoByVacancyId(objVacancy.obj.VacancyId, _CurrentLanguageId);
                    _objVacancy.RecordPermissionType = _objVacancy.obj.PermissionType;
                    objBreadScrumVacancy = _objVacancy;

                    BLClient.AppInstructionDocsAction objAppInstDocsAction = new AppInstructionDocsAction(_CurrentClientId);
                    _objVacancy.obj.objAppInstructionDocList = objAppInstDocsAction.GetAppInstructionDocsByVacancyId(objVacancy.obj.VacancyId);

                    Data = base.RenderRazorViewToString("Shared/_CompensationInfo", objBreadScrumVacancy);
                }
                else
                {
                    Message = "Not Able To Add Record";
                    return GetJson(base.GetJsonContent(IsError, string.Empty, Message));
                }
                try
                {
                    ATSCommon.CommonFunctions.SolrFullImport();
                }
                catch (Exception ex)
                {
                    log.Error("SOLR Vacancy Full IMPORT (AddCompensationInfo)" + ex.Message);
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
        }

        [HttpPost]
        public JsonResult GetCompensationInfo(Guid VacancyId, string mode)
        {
            String Message = String.Empty;
            BEClient.PageMode objPageMode = BEClient.PageMode.View;
            bool IsError = false;
            String Data = "";
            try
            {
                if (Enum.IsDefined(typeof(BEClient.PageMode), mode))
                {
                    Enum.TryParse<BEClient.PageMode>(mode, true, out objPageMode);
                    ViewBag.PageMode = objPageMode;
                }

                if (!VacancyId.Equals(Guid.Empty))
                {
                    CreateObjVacancy(VacancyId);
                    _objVacancy.obj = _objVacancyAction.GetCompensationInfoByVacancyId(VacancyId, _CurrentLanguageId);
                    _objVacancy.RecordPermissionType = _objVacancy.obj.PermissionType;

                    BLClient.AppInstructionDocsAction objAppInstDocsAction = new AppInstructionDocsAction(_CurrentClientId);
                    _objVacancy.obj.objAppInstructionDocList = objAppInstDocsAction.GetAppInstructionDocsByVacancyId(VacancyId);
                }
                Data = base.RenderRazorViewToString("Shared/_CompensationInfo", _objVacancy);
                TempData["RecordPermissionType"] = _objVacancy.RecordPermissionType;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
        }

        [HttpPost]
        public JsonResult GetJobDescription(Guid VacancyId, string mode)
        {
            String Message = String.Empty;
            BEClient.PageMode objPageMode = BEClient.PageMode.View;
            bool IsError = false;
            String Data = "";
            try
            {
                if (Enum.IsDefined(typeof(BEClient.PageMode), mode))
                {
                    Enum.TryParse<BEClient.PageMode>(mode, true, out objPageMode);
                    ViewBag.PageMode = objPageMode;
                }

                if (!VacancyId.Equals(Guid.Empty))
                {
                    CreateObjVacancy(VacancyId);
                    _objVacancy.obj = _objVacancyAction.GetJobDescriptionByVacancyId(VacancyId, _CurrentLanguageId);
                    _objVacancy.RecordPermissionType = _objVacancy.obj.PermissionType;
                }
                Data = base.RenderRazorViewToString("Shared/_JobDescription", _objVacancy);
                TempData["RecordPermissionType"] = _objVacancy.RecordPermissionType;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
        }

        [HttpPost]
        public JsonResult GetVacancyDetails(Guid VacancyId, string mode)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = "";
            BEClient.PageMode objPageMode = BEClient.PageMode.View;
            try
            {
                if (Enum.IsDefined(typeof(BEClient.PageMode), mode))
                {
                    Enum.TryParse<BEClient.PageMode>(mode, true, out objPageMode);
                    ViewBag.PageMode = objPageMode;
                }

                if (!VacancyId.Equals(Guid.Empty))
                {
                    CreateObjVacancy(VacancyId);
                    _objVacancy.obj = _objVacancyAction.GetVacancyById(VacancyId, _CurrentLanguageId);
                    _objVacancy.RecordPermissionType = _objVacancy.obj.PermissionType;
                    string callMethod = ATS.WebUi.Common.CommonFunctions.GetPageMode(_objVacancy.RecordPermissionType, BEClient.PageMode.Update).ToString();
                    DropDownLists(BEClient.ATSPermissionType.Modify, _objVacancy.obj.DivisionId, callMethod, _objVacancy.obj.PositionOwner);
                }
                else
                {
                    CreateObjVacancy(null);
                    _objVacancy.obj = new BEClient.Vacancy();
                    _objVacancy.obj.RecordExistsCount = new List<BEClient.RecordExists>();
                    _objVacancy.obj.VacancyState = 1;
                    _objVacancy.obj.ShowOnWeb = "";
                    _objVacancy.obj.FeaturedOnWeb = true;
                    _objVacancy.ActionName = "Create";
                    objPageMode = BEClient.PageMode.Create;
                    _objVacancy.RecordPermissionType = new List<BEClient.ATSPermissionType> { BEClient.ATSPermissionType.Create };
                    string callMethod = ATS.WebUi.Common.CommonFunctions.GetPageMode(_objVacancy.RecordPermissionType, BEClient.PageMode.Create).ToString();
                    DropDownLists(BEClient.ATSPermissionType.Create, _objVacancy.obj.DivisionId, callMethod, _objVacancy.obj.PositionOwner, _objVacancy.obj.ShowOnWeb);
                }

                if (TempData["publishonWeb"] != null)
                    TempData["publishonWeb"] = TempData["publishonWeb"];
                else
                    TempData["publishonWeb"] = _objVacancy.obj.ShowOnWeb;
                if (TempData["RecordPermissionType"] != null)
                    TempData["RecordPermissionType"] = TempData["RecordPermissionType"];
                else
                    TempData["RecordPermissionType"] = _objVacancy.RecordPermissionType;
                Data = base.RenderRazorViewToString("Shared/_VacancyDetail", _objVacancy);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
        }
        //Anand : Method is depreciated - 10th March
        [HttpPost]
        public JsonResult ApplyVacancyTemplate(Guid TVacId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = "";
            try
            {
                BLClient.CopyTemplateAction objCopyTemplateAction = new BLClient.CopyTemplateAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                Guid newVacancyId = objCopyTemplateAction.TemplateToMainObject(TVacId, _CurrentLanguageId);

                try
                {
                    ATSCommon.CommonFunctions.SolrFullImport();
                }
                catch (Exception ex)
                {
                    log.Error("SOLR Employee FULL IMPORT (Create Vacancy)" + ex.Message);
                }

                BESrp.DynamicSRP<BEClient.Vacancy> objBreadScrumbModelVacancy = new BESrp.DynamicSRP<BEClient.Vacancy>();
                BESrp.DynamicSRP<BEClient.Vacancy> objVacancy = new BESrp.DynamicSRP<BEClient.Vacancy>();

                CreateObjVacancy(newVacancyId);
                objVacancy.obj = _objVacancyAction.GetVacancyById(newVacancyId, _CurrentLanguageId);
                ViewBag.PageMode = BEClient.PageMode.Update;
                objVacancy.RecordPermissionType = objVacancy.obj.PermissionType;
                string callMethod = ATS.WebUi.Common.CommonFunctions.GetPageMode(objVacancy.RecordPermissionType, BEClient.PageMode.Update).ToString();
                DropDownLists(BEClient.ATSPermissionType.Modify, objVacancy.obj.DivisionId, callMethod, objVacancy.obj.PositionOwner);
                objVacancy.obj.VacancyId = newVacancyId;
                objVacancy.obj.RndCount = _objVacancyAction.GetVacRndCountByVacancy(newVacancyId);
                objBreadScrumbModelVacancy = new BESrp.DynamicSRP<BEClient.Vacancy>();
                objBreadScrumbModelVacancy = objVacancy;
                Data = base.RenderRazorViewToString("Shared/_VacancyDetail", objBreadScrumbModelVacancy);
            }
            catch (Exception Ex)
            {
                Message = Ex.Message;
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
        }
        //Anand Shah : Method is Depreciated - 10th March  
        [HttpGet]
        public JsonResult GetApplyTemplate(Guid? VacancyId)
        {
            String Message = String.Empty;
            bool IsError = false;
            ViewBag.VacId = VacancyId;
            String Data = String.Empty;
            try
            {
                BEClient.TVac objTVac = new BEClient.TVac();
                BLClient.TVacAction objTVacAction = new BLClient.TVacAction(_CurrentClientId, true);

                List<BEClient.TVac> ObjTVacLst = objTVacAction.GetTVacByPosIdAndDivId(VacancyId, _CurrentLanguageId);
                ViewBag.TVacTemplate = ObjTVacLst;
                ViewBag.TVacTemplate = new SelectList(ObjTVacLst, "TVacId", "Name");
                Data = base.RenderRazorViewToString("Shared/_ApplyVacTemplate", objTVac);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetApplicationProcess(Guid VacancyId, string ApplicationId, string mode, bool FillScore = true)
        {
            try
            {
                String Message = String.Empty;
                bool IsError = false;
                String Data = "";
                Guid _ApplicationId = Guid.Empty;
                Guid.TryParse(ApplicationId, out _ApplicationId);
                Guid _ActiveRoundId = Guid.Empty;
                try
                {
                    RootModels.BreadScrumbModel<ATS.BusinessEntity.SRPEntity.DynamicSRP<ATS.BusinessEntity.Vacancy>> objVacancy = new BreadScrumbModel<BESrp.DynamicSRP<BEClient.Vacancy>>();
                    objVacancy.obj = new BESrp.DynamicSRP<BEClient.Vacancy>();
                    objVacancy.obj.obj = new BEClient.Vacancy();
                    if (!VacancyId.Equals(Guid.Empty))
                    {
                        objVacancy.obj.obj.VacancyId = VacancyId;
                        if (_ApplicationId != Guid.Empty && FillScore)
                        {
                            //Get All Uploaded Docs
                            BEClient.CandidateProfile objCandidateProfile = new BEClient.CandidateProfile();
                            BLClient.ApplicationDocumentsAction objAppDocAction = new BLClient.ApplicationDocumentsAction(_CurrentClientId);
                            List<BEClient.ApplicationDocuments> objAppDocList = objAppDocAction.GetRequiredDocumentsByApplicationId(_ApplicationId);
                            if (objAppDocList != null && objAppDocList.Count > 0)
                            {
                                objAppDocList = objAppDocList.Where(x => x.IsPending == false).ToList();
                                ViewBag.UploadedRequiredDocsList = objAppDocList;
                            }
                            else
                            {
                                BLClient.ATSResumeAction ObjAtsResumeAction = new BLClient.ATSResumeAction(_CurrentClientId);
                                objVacancy.obj.obj.objATSResume = ObjAtsResumeAction.GetATSResumeByVacancyAndLanguage_GearBox(VacancyId, _CurrentLanguageId, _ApplicationId);
                            }
                        }

                        if (mode == BEClient.PageMode.Update.ToString())
                        {
                            CreateObjVacancy(VacancyId);
                            BLClient.VacancyRoundAction objVacancyRoundAction = new BLClient.VacancyRoundAction(_CurrentClientId, true);
                            objVacancy.obj.obj.VacancyRoundList = objVacancyRoundAction.GetAllRound(VacancyId, _CurrentLanguageId, _ApplicationId, FillScore);
                        }
                        else
                        {
                            objVacancy.obj.obj.VacancyRoundList = new List<BEClient.VacancyRound>();
                        }


                        if (_ApplicationId != Guid.Empty)
                        {
                            BLClient.ReviewersAction objReviewerAction = new BLClient.ReviewersAction(_CurrentClientId);
                            for (int i = 0; i < objVacancy.obj.obj.VacancyRoundList.Count(); i++)
                            {
                                objVacancy.obj.obj.VacancyRoundList[i].isReviewer = objReviewerAction.AuthorizeRevmemberForPromoteCandidate(objVacancy.obj.obj.VacancyRoundList[i].VacancyRoundId, Common.CurrentSession.Instance.VerifiedUser.UserId);
                                if (objVacancy.obj.obj.VacancyRoundList[i].IsRndActive)
                                {
                                    _ActiveRoundId = objVacancy.obj.obj.VacancyRoundList[i].VacancyRoundId;
                                }
                            }

                            BLClient.ApplicationBasedStatusAction _objAppBasedStatusAction = new BLClient.ApplicationBasedStatusAction(_CurrentClientId);
                            objVacancy.obj.obj.ApplicationBasedStatusList = _objAppBasedStatusAction.GetAllAppBasedStatus(_CurrentLanguageId);
                            objVacancy.obj.obj.IsDeclineApplication = _objAppBasedStatusAction.IsApplicationDecline(_ApplicationId);

                        }
                    }


                    objVacancy.obj.obj.RndCount = objVacancy.obj.obj.VacancyRoundList.Count();
                    RoundConfigDropDown();
                    objVacancy.obj.obj.AppId = _ApplicationId;

                    BLClient.ScheduleIntAction objScheduleIntAction = new BLClient.ScheduleIntAction(_CurrentClientId);
                    List<BEClient.ScheduleInt> objScheduleIntList = new List<BEClient.ScheduleInt>();
                    objScheduleIntList = objScheduleIntAction.GetBeginInterviewsByAppIdAndUserId(_ApplicationId, _UserId);
                    if (objScheduleIntList.Count > 0)
                    {
                        ViewBag.BeginInterviewList = objScheduleIntAction.GetBeginInterviewsByAppIdAndUserId(_ApplicationId, _UserId).Where(x => x.VacRndId == _ActiveRoundId).ToList();
                    }
                    Data = base.RenderRazorViewToString("Shared/_ApplicationProcess", objVacancy);
                }
                catch (Exception ex)
                {
                    Message = ex.Message;
                    IsError = true;
                }

                return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public JsonResult GetRevAndQueCount(Guid VacancyRoundId)
        {
            try
            {
                String Message = String.Empty;
                bool IsError = false;
                String Data = "";
                try
                {
                    ATS.BusinessEntity.ApplicationReviewProcess objApplicationReviewProcess = new BEClient.ApplicationReviewProcess();
                    objApplicationReviewProcess.objVacancyRound = new BEClient.VacancyRound();
                    if (!VacancyRoundId.Equals(Guid.Empty))
                    {
                        BLClient.VacancyRoundAction objVacancyRoundAction = new BLClient.VacancyRoundAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                        BEClient.VacancyRound vacRound = objVacancyRoundAction.GetRoundConfigByRoundId(VacancyRoundId, _CurrentLanguageId);
                        if (vacRound == null)
                        {
                            return GetJson(base.GetJsonContent(false, string.Empty, "", ""));
                        }
                        objApplicationReviewProcess.objVacancyRound = objVacancyRoundAction.GetCountOfReviewersAndQuestion(VacancyRoundId);
                        objApplicationReviewProcess.objVacancyRound.ReqReviewer = vacRound.ReqReviewer;
                        //this function will be used to get count of reviewers
                        objApplicationReviewProcess.objVacancyRound.VacancyRoundId = VacancyRoundId;
                    }
                    RoundConfigDropDown();
                    Data = base.RenderRazorViewToString("Shared/RoundList/_RoundDetail", objApplicationReviewProcess);
                }
                catch (Exception ex)
                {
                    Message = ex.Message;
                    IsError = true;
                }

                return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public JsonResult GetRoundConfigDetail(Guid VacancyRoundId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = "";
            BEClient.PageMode objPageMode = BEClient.PageMode.Update;
            try
            {
                ATS.BusinessEntity.ApplicationReviewProcess objApplicationReviewProcess = new BEClient.ApplicationReviewProcess();
                objApplicationReviewProcess.objVacancyRound = new BEClient.VacancyRound();
                if (!VacancyRoundId.Equals(Guid.Empty))
                {
                    BLClient.VacancyRoundAction objVacancyRoundAction = new BLClient.VacancyRoundAction(Common.CurrentSession.Instance.VerifiedClient.ClientId, true);
                    objApplicationReviewProcess.objVacancyRound = objVacancyRoundAction.GetRoundConfigByRoundId(VacancyRoundId, _CurrentLanguageId);
                }
                ViewBag.PageMode = objPageMode;
                UpdateRoundConfigDropDown(objApplicationReviewProcess.objVacancyRound.VacancyId, VacancyRoundId, objApplicationReviewProcess.objVacancyRound.RoundAttributeNo);
                Data = base.RenderRazorViewToString("Shared/RoundConfig/RoundConfiguration", objApplicationReviewProcess);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
        }

        [HttpPost]
        public JsonResult RemoveVacRound(Guid VacRoundId)
        {
            string Message = string.Empty;
            bool IsError = false;
            bool result = false;
            try
            {
                if (!VacRoundId.Equals(Guid.Empty))
                {
                    BLClient.VacancyRoundAction ObjRoundAction = new BLClient.VacancyRoundAction(_CurrentClientId);
                    result = ObjRoundAction.Delete(VacRoundId);

                    if (result)
                    {
                        Message = "Record Deleted Successfully";
                    }
                    else
                    {
                        IsError = true;
                        Message = "Not able to delete Data";
                    }
                }
            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message));
        }

        [HttpPost]
        public JsonResult RemoveVacQueCat(Guid VacQueCatId, Guid VacRndId)
        {
            string Message = string.Empty;
            bool IsError = false;
            bool result = false;
            try
            {
                if (!VacQueCatId.Equals(Guid.Empty) && !VacRndId.Equals(Guid.Empty))
                {
                    BLClient.VacQueCatAction ObjVacQueCatAction = new BLClient.VacQueCatAction(_CurrentClientId);
                    result = ObjVacQueCatAction.Delete(VacQueCatId, VacRndId);

                    if (result)
                    {
                        Message = "Record Deleted Successfully";
                    }
                    else
                    {
                        IsError = true;
                        Message = "Not able to delete Data";
                    }
                }
            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message));
        }

        #region Reviewers
        [HttpPost]
        public JsonResult GetReviewersByVacancyRnd(Guid VacancyRndId)
        {
            try
            {
                String Message = String.Empty;
                bool IsError = true;
                String Data = "";
                try
                {
                    ATS.BusinessEntity.ApplicationReviewProcess objApplicationReviewProcess = new BEClient.ApplicationReviewProcess();
                    objApplicationReviewProcess.objReviewerslst = new BESrp.DynamicSRP<List<BEClient.Reviewers>>();
                    if (!VacancyRndId.Equals(Guid.Empty))
                    {
                        IsError = false;
                        BLClient.ReviewersAction objReviewersAction = new BLClient.ReviewersAction(Common.CurrentSession.Instance.VerifiedClient.ClientId, true);
                        objApplicationReviewProcess.objReviewerslst.UserPermissionWithScope = objReviewersAction.ListUserPermissionWithScope;
                        objApplicationReviewProcess.objReviewerslst.obj = objReviewersAction.GetAllReviewersByRoundId(VacancyRndId, _CurrentLanguageId);
                        //sEND REQUEST TO GET COUNT OF REVIEWER ASSIGNED IN ROUND CONFIGURATION
                        BLClient.VacancyRoundAction objVacancyRoundAction = new BLClient.VacancyRoundAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                        objApplicationReviewProcess.objVacancyRound = objVacancyRoundAction.GetRoundConfigByRoundId(VacancyRndId, _CurrentLanguageId);
                        //objApplicationReviewProcess.objVacancyRound.VacancyRoundId = VacancyRoundId;
                        ReviewersDropDown(VacancyRndId, null);
                    }
                    Data = base.RenderRazorViewToString("Shared/Reviewers/_ReviewersDetail", objApplicationReviewProcess);
                }
                catch (Exception ex)
                {
                    Message = ex.Message;
                }
                return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public JsonResult AddVacRevModel(Guid VacRndId)
        {
            String Message = string.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                BEClient.Reviewers ObjVacRev = new BEClient.Reviewers();
                ObjVacRev.RndTypeId = VacRndId;
                ReviewersDropDown(VacRndId, null);
                ViewBag.AllowRemoveBtn = true;
                Data = base.RenderRazorViewToString("Shared/Reviewers/_AddReviewer", ObjVacRev);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RemoveVacRev(string VacRevId)
        {
            try
            {
                string Message = string.Empty;
                bool IsError = false;
                bool result = false;
                if (!String.IsNullOrEmpty(VacRevId))
                {
                    BLClient.ReviewersAction ObjReviewersAction = new BLClient.ReviewersAction(_CurrentClientId);
                    result = ObjReviewersAction.Delete(new Guid(VacRevId));
                    if (result)
                    {
                        Message = "Record Deleted Successfully";
                    }
                    else
                    {
                        IsError = true;
                        Message = "Not able to delete Data";
                    }
                }
                return GetJson(base.GetJsonContent(IsError, string.Empty, Message));
            }
            catch
            {
                throw;
            }
        }

        private void ReviewersDropDown(Guid VacRndId, Guid? UserId)
        {
            try
            {
                BLClient.UserAction objUserAction = new BLClient.UserAction(_CurrentClientId, true);
                List<BEClient.User> objUser = new List<BEClient.User>();
                if (Guid.Empty.Equals(UserId))
                {
                    UserId = null;
                }
                objUser = objUserAction.GetAllReviewer(VacRndId, UserId);
                ViewBag.UserList = new SelectList(objUser, "UserId", "ObjUserDetails.FirstName");
                ViewBag.YesNoDropDownCanPramote = Common.CommonFunctions.YesNoDropDownList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public JsonResult GetUserTitle(string UserId, string VacRndId, string VacRevId, string OldUserId)
        {
            string Data = string.Empty;
            bool IsError = false;
            string Message = string.Empty;
            try
            {
                BEClient.Reviewers objReviewers = new BEClient.Reviewers();
                objReviewers.Weight = 0;
                objReviewers.DivisionLocalName = String.Empty;

                Guid _OldUserId = Guid.Empty;
                Guid.TryParse(OldUserId, out _OldUserId);
                ReviewersDropDown(new Guid(VacRndId), _OldUserId);

                if (!String.IsNullOrEmpty(UserId) && !String.IsNullOrEmpty(OldUserId))
                {
                    BLClient.ReviewersAction objReviewerAction = new BLClient.ReviewersAction(_CurrentClientId);
                    objReviewers = objReviewerAction.GetDivisonByUserId(new Guid(UserId), _CurrentLanguageId);
                    objReviewers.RndTypeId = new Guid(VacRndId);
                    if (VacRevId != "undefined" && new Guid(VacRevId) != Guid.Empty)
                    {
                        objReviewers.VacancyReviewMemberId = new Guid(VacRevId);
                    }
                }
                Data = base.RenderRazorViewToString("Shared/Reviewers/_AddReviewer", objReviewers);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return GetJson(base.GetJsonContent(false, string.Empty, Message, Data), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddUpdateVacRev(BEClient.Reviewers objVacReviewers)
        {
            String Message = String.Empty;
            bool IsError = true;
            String Data = "";
            try
            {
                if (!String.IsNullOrEmpty(Message))
                {
                    Exception ex = new Exception(Message);
                    throw ex;
                }
                BLClient.ReviewersAction ObjReviewersAction = new BLClient.ReviewersAction(_CurrentClientId);
                bool _isRecordExists = ObjReviewersAction.IsRecordExists(objVacReviewers.VacancyReviewMemberId);
                if (_isRecordExists)
                {
                    bool IsrecordUpdated = ObjReviewersAction.Update(objVacReviewers);
                    if (IsrecordUpdated)
                    {
                        ObjReviewersAction = new BLClient.ReviewersAction(_CurrentClientId, true);
                        objVacReviewers = ObjReviewersAction.GetRecordByRecordId(objVacReviewers.VacancyReviewMemberId, _CurrentLanguageId);

                        IsError = false;
                        Message = "Updated Successfully";
                        //data will be pass as empty to identify is update call or inser call
                        Data = base.RenderRazorViewToString("Shared/Reviewers/_ReviewerList", objVacReviewers);
                    }
                }
                else
                {
                    Guid NewrecordAdded = ObjReviewersAction.InsertVacReviewMember(objVacReviewers);
                    if (!NewrecordAdded.Equals(Guid.Empty))
                    {
                        ObjReviewersAction = new BLClient.ReviewersAction(_CurrentClientId, true);
                        BEClient.Reviewers ObjReviewers = new BEClient.Reviewers();
                        objVacReviewers = ObjReviewersAction.GetRecordByRecordId(NewrecordAdded, _CurrentLanguageId);

                        IsError = false;
                        Message = "Record Inserted Successfully";
                        objVacReviewers.VacancyReviewMemberId = NewrecordAdded;
                        //objVacQue.LocalName = 
                        Data = base.RenderRazorViewToString("Shared/Reviewers/_ReviewerList", objVacReviewers);
                    }
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
        }

        [HttpGet]
        public JsonResult GetVacRevById(Guid VacRevId, Guid VacRndId)
        {
            try
            {
                string Data = string.Empty;
                bool IsError = false;
                string Message = string.Empty;
                if (!VacRevId.Equals(Guid.Empty))
                {
                    BLClient.ReviewersAction ObjReviewersAction = new BLClient.ReviewersAction(_CurrentClientId);
                    BEClient.Reviewers ObjReviewers = new BEClient.Reviewers();
                    ObjReviewers = ObjReviewersAction.GetRecordByRecordId(VacRevId, _CurrentLanguageId);
                    ReviewersDropDown(VacRndId, ObjReviewers.UserId);
                    ViewBag.AllowRemoveBtn = false;
                    Data = base.RenderRazorViewToString("Shared/Reviewers/_AddReviewer", ObjReviewers);
                }
                else
                {
                    IsError = true;
                    Message = "Not able to retrieve Data";
                }
                return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region  Vacancy Question
        public JsonResult AddVacQueModel(Guid VacRndId, Guid VacQueCatId)
        {
            string Data = string.Empty;
            bool IsError = false;
            string Message = string.Empty;
            try
            {
                BEClient.VacancyQuestion ObjVacQue = new BEClient.VacancyQuestion();
                ObjVacQue.RndTypeId = VacRndId;
                ObjVacQue.VacQueCatId = VacQueCatId;
                FillDropDown(VacRndId, VacQueCatId, null, Guid.Empty);
                ViewBag.AllowRemoveBtn = true;
                Data = base.RenderRazorViewToString("Shared/Questions/_AddEditVacQue", ObjVacQue);
            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;
                Data = "";
            }

            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data), JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public JsonResult GetARPHint()
        {
            String Data = string.Empty;
            bool IsError = false;
            String Message = string.Empty;
            try
            {
                Data = base.RenderRazorViewToString("Shared/Hint/_ApplicationReviewRound", null);

            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data), JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public JsonResult GetAnswerTooltip(Guid VacQueId)
        {
            String Data = string.Empty;
            bool IsError = false;
            String Message = string.Empty;
            try
            {
                BLClient.AnsOptAction _objDdlAnsOptAction = new BLClient.AnsOptAction(_CurrentClientId);
                List<BEClient.AnsOpt> _objDdlAnsList = _objDdlAnsOptAction.GetAnsListByQueId(VacQueId, _CurrentLanguageId);
                Data = base.RenderRazorViewToString("Shared/Questions/_AnswerToolTip", _objDdlAnsList);

            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;

            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data), JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetQueByVacancyRnd(Guid VacancyRndId)
        {
            try
            {
                String Data = string.Empty;
                bool IsError = false;
                BEClient.ApplicationReviewProcess ObjApplicationReviewProcess = new BEClient.ApplicationReviewProcess();

                BLClient.VacancyQuestionAction ObjVacancyQuestionAction = new BLClient.VacancyQuestionAction(_CurrentClientId, true);
                ObjApplicationReviewProcess.objVacancyQuestionlst = new BESrp.DynamicSRP<List<BEClient.VacancyQuestion>>();
                ObjApplicationReviewProcess.objVacancyQuestionlst.UserPermissionWithScope = ObjVacancyQuestionAction.ListUserPermissionWithScope;

                ObjApplicationReviewProcess.objVacancyQuestionlst.obj = ObjVacancyQuestionAction.GetAllQueByRoundId(VacancyRndId, _CurrentLanguageId);
                List<KeyValuePair<int, string>> objQueDataType = Common.CommonFunctions.QuestionType.ToList();
                ObjApplicationReviewProcess.objVacancyQuestionlst.obj.ForEach(a => a.QueTypeLocalName = objQueDataType.SingleOrDefault(b => b.Key == a.QueType).Value.ToString());

                Data = base.RenderRazorViewToString("Shared/Questions/_QuestionDetail", ObjApplicationReviewProcess);

                return GetJson(base.GetJsonContent(IsError, string.Empty, string.Empty, Data));
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public JsonResult GetQueDetails(Guid? QueId, Guid VacQueCatId, string VacRndId, string VacQueId, string newQueId)
        {
            try
            {
                ModelState.Clear();
                string Data = string.Empty;
                Guid _QueId = Guid.Empty;
                Guid _VacQueId = Guid.Empty;
                Guid _NewQueId = Guid.Empty;
                Guid.TryParse(QueId.ToString(), out _QueId);
                Guid.TryParse(VacQueId.ToString(), out _VacQueId);
                Guid.TryParse(newQueId.ToString(), out _NewQueId);

                FillDropDown(new Guid(VacRndId), VacQueCatId, QueId, _NewQueId);

                if (_NewQueId != Guid.Empty)
                {
                    _QueId = _NewQueId;
                }


                BEClient.VacancyQuestion objVacQue = new BEClient.VacancyQuestion();
                objVacQue.RndTypeId = new Guid(VacRndId);
                objVacQue.QueTypeLocalName = String.Empty;
                if (!Guid.Empty.Equals(_VacQueId))
                {
                    objVacQue.VacQueId = _VacQueId;
                }
                objVacQue.Weight = 0;
                if (!Guid.Empty.Equals(_QueId) && !Guid.Empty.Equals(_NewQueId))
                {

                    BLClient.QuestionAction ObjQueAction = new BLClient.QuestionAction(_CurrentClientId);
                    BEClient.Question ObjQue = ObjQueAction.GetRecordByRecordId(_QueId);

                    objVacQue.QueId = ObjQue.QueId;
                    objVacQue.Weight = ObjQue.Weight;
                    List<KeyValuePair<int, string>> objQueDataType = Common.CommonFunctions.QuestionType.ToList();
                    objVacQue.QueTypeLocalName = objQueDataType.Where(x => x.Key == ObjQue.QueDataType).Select(x => x.Value.ToString()).FirstOrDefault();
                }
                ViewBag.PageMode = "Update";

                Data = base.RenderRazorViewToString("Shared/Questions/_AddEditVacQue", objVacQue);
                //Data = objVacQue.Weight.ToString() + "," + objVacQue.QueTypeLocalName.ToString();
                return GetJson(base.GetJsonContent(false, string.Empty, string.Empty, Data), JsonRequestBehavior.AllowGet);
                //return PartialView("Shared/Questions/_AddEditVacQue", objVacQue);         
            }
            catch
            {
                throw;
            }
        }

        private void FillDropDown(Guid VacRndId, Guid VacQueCatId, Guid? QueId, Guid SelectedQue)
        {
            try
            {
                BLClient.VacancyQuestionAction ObjVacancyQuestionAction = new BLClient.VacancyQuestionAction(_CurrentClientId, true);
                List<BEClient.Division> DivisionLst = new List<BEClient.Division>();
                DivisionLst = ObjVacancyQuestionAction.GetAllDivisionByScope(_CurrentLanguageId, "Create");
                List<BEClient.Question> QueList;
                if (DivisionLst != null)
                {

                    if (QueId != null && QueId.Equals(Guid.Empty))
                        QueId = null;

                    QueList = ObjVacancyQuestionAction.GetQueByVacRndAndVacQueCat(VacRndId, VacQueCatId, _CurrentLanguageId, QueId);
                    List<KeyValuePair<int, string>> objQueDataType = Common.CommonFunctions.QuestionType.ToList();
                    foreach (var curr in QueList)
                    {
                        foreach (var Dcurr in objQueDataType)
                        {
                            if (curr.QueDataType == Dcurr.Key)
                            {
                                curr.LocalNameAndDatatype = curr.LocalName + ' ' + '(' + Dcurr.Value + ')';
                                break;
                            }
                        }
                    }
                }
                else
                {
                    QueList = new List<BEClient.Question>();
                }
                SelectList list;
                if (!SelectedQue.Equals(Guid.Empty))
                    list = new SelectList(QueList, "QueId", "LocalNameAndDatatype", SelectedQue);
                else
                    list = new SelectList(QueList, "QueId", "LocalNameAndDatatype");

                ViewBag.QuestionsList = list;
                ViewBag.QuestionDataTypeList = new SelectList(ATSCommon.CommonFunctions.QuestionType, "Key", "Value");
            }
            catch
            {
                throw;
            }
        }

        //[HttpPost]
        //public JsonResult AddUpdateVacQue(BEClient.VacancyQuestion objVacQue, FormCollection collection)
        //{
        //    String Message = String.Empty;
        //    bool IsError = false;
        //    String Data = "";
        //    BEClient.Question ObjQue = new BEClient.Question();
        //    String _QueTypeLocalName = objVacQue.QueTypeLocalName;

        //    Guid _QueId = Guid.Empty;
        //    if (Request["QueIdData"] != null && Guid.TryParse(Request["QueIdData"].ToString(), out _QueId))
        //    {
        //        objVacQue.QueId = _QueId;
        //    }

        //    //Message = ServerValidation();
        //    try
        //    {
        //        if (!String.IsNullOrEmpty(Message))
        //        {
        //            Exception ex = new Exception(Message);
        //            throw ex;
        //        }
        //        BLClient.QuestionAction ObjQueAction = new BLClient.QuestionAction(_CurrentClientId);
        //        ObjQue = ObjQueAction.GetRecordByRecordId(objVacQue.QueId);
        //        objVacQue.LocalName = ObjQue.EntityLanguageList.Where(x => x.LanguageId == _CurrentLanguageId).Select(x => x.LocalName.ToString()).FirstOrDefault();
        //        BLClient.VacancyQuestionAction objVacQueAction = new BLClient.VacancyQuestionAction(Common.CurrentSession.Instance.VerifiedClient.ClientId, true);

        //        bool _isRecordExists = objVacQueAction.IsRecordExists(objVacQue.VacQueId);
        //        if (_isRecordExists)
        //        {
        //            bool IsrecordUpdated = objVacQueAction.Update(objVacQue);
        //            if (IsrecordUpdated)
        //            {
        //                Message = "Updated Successfully";
        //            }
        //        }
        //        else
        //        {
        //            Guid NewrecordAdded = objVacQueAction.AddVacancyRound(objVacQue);
        //            if (!NewrecordAdded.Equals(Guid.Empty))
        //            {

        //                Message = "Record Inserted Successfully";
        //                objVacQue.VacQueId = NewrecordAdded;
        //            }
        //        }

        //        objVacQueAction = null;
        //        objVacQueAction = new BLClient.VacancyQuestionAction(Common.CurrentSession.Instance.VerifiedClient.ClientId, true);
        //        objVacQue = objVacQueAction.GetrecordByRecorId(objVacQue.VacQueId);
        //        objVacQue.LocalName = ObjQue.EntityLanguageList.Where(x => x.LanguageId == _CurrentLanguageId).Select(x => x.LocalName.ToString()).FirstOrDefault();
        //        objVacQue.QueTypeLocalName = _QueTypeLocalName;

        //        Data = base.RenderRazorViewToString("Shared/Questions/_SingleVacQue", objVacQue);
        //    }
        //    catch (Exception ex)
        //    {

        //        IsError = true;
        //        Message = ex.Message;
        //    }

        //    return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
        //}

        [HttpPost]
        public JsonResult AddUpdateVacQue(BEClient.VacancyQuestion objVacQue)
        {
            string[] Data = new string[2];
            String Data1 = string.Empty;
            String Data2 = string.Empty; // For New Added Question Count
            bool IsError = false;
            String Message = String.Empty;
            try
            {
                String _QueTypeLocalName = objVacQue.QueTypeLocalName;
                BEClient.Question ObjQue = new BEClient.Question();
                BLClient.VacancyQuestionAction objVacQueAction = new BLClient.VacancyQuestionAction(_CurrentClientId);
                BLClient.QuestionAction ObjQueAction = new BLClient.QuestionAction(_CurrentClientId);

                if (objVacQue.ListQueId != null)
                {
                    Guid NewrecordAdded = Guid.Empty;
                    foreach (Guid QueId in objVacQue.ListQueId)
                    {
                        objVacQue.QueId = QueId;
                        NewrecordAdded = objVacQueAction.InsertVacancyQuestion(objVacQue);
                    }
                    if (!NewrecordAdded.Equals(Guid.Empty))
                    {
                        Data2 = Convert.ToString(objVacQue.ListQueId.Count());
                        objVacQueAction = new BLClient.VacancyQuestionAction(Common.CurrentSession.Instance.VerifiedClient.ClientId, true);
                        objVacQue = objVacQueAction.GetrecordByRecorId(NewrecordAdded);

                        Data1 = base.RenderRazorViewToString("Shared/Questions/_SingleVacQue", objVacQue);
                        //Data2 = Convert.ToString(objVacQue.ListQueId.Count());
                        Message = "Record Inserted Successfully";
                        objVacQue.VacQueId = NewrecordAdded;
                    }
                }
                else
                {
                    if (objVacQue.QueId != Guid.Empty)
                    {
                        //bool IsrecordUpdated = objVacQueAction.UpdateVacQueWeightById(objVacQue);
                        bool IsrecordUpdated = objVacQueAction.Update(objVacQue);

                        if (IsrecordUpdated)
                        {
                            Message = "Updated Successfully";
                            objVacQueAction = null;
                            objVacQueAction = new BLClient.VacancyQuestionAction(Common.CurrentSession.Instance.VerifiedClient.ClientId, true);
                            objVacQue = objVacQueAction.GetrecordByRecorId(objVacQue.VacQueId);
                            objVacQue.QueTypeLocalName = _QueTypeLocalName;
                            Data1 = base.RenderRazorViewToString("Shared/Questions/_SingleVacQue", objVacQue);
                            Data2 = "0";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;
            }
            Data[0] = Data1;
            Data[1] = Data2;
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
        }

        [HttpPost]
        public JsonResult RemoveVacQue(string VacQueId)
        {
            try
            {
                string Message = string.Empty;
                bool IsError = false;
                bool result = false;
                if (!String.IsNullOrEmpty(VacQueId))
                {
                    BLClient.VacancyQuestionAction ObjVacQueAction = new BLClient.VacancyQuestionAction(_CurrentClientId);
                    result = ObjVacQueAction.Delete(new Guid(VacQueId));
                    if (result)
                    {
                        Message = "Record Deleted Successfully";
                    }
                    else
                    {
                        IsError = true;
                        Message = "Not able to delete Data";
                    }
                }
                return GetJson(base.GetJsonContent(IsError, string.Empty, Message));
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public JsonResult GetVacQueById(Guid VacQueId, Guid VacRndId, Guid VacQueCatId)
        {
            try
            {
                string Data = string.Empty;
                bool IsError = false;
                string Message = string.Empty;
                if (!VacQueId.Equals(Guid.Empty))
                {
                    BLClient.VacancyQuestionAction ObjVacQueAction = new BLClient.VacancyQuestionAction(_CurrentClientId);
                    BEClient.VacancyQuestion ObjVacQue = new BEClient.VacancyQuestion();
                    ObjVacQue = ObjVacQueAction.GetrecordByRecorId(VacQueId);
                    FillDropDown(VacRndId, VacQueCatId, ObjVacQue.QueId, ObjVacQue.QueId);
                    List<KeyValuePair<int, string>> objQueDataType = Common.CommonFunctions.QuestionType.ToList();
                    ObjVacQue.QueTypeLocalName = objQueDataType.Where(x => x.Key == ObjVacQue.QueType).Select(x => x.Value.ToString()).FirstOrDefault();
                    ViewBag.AllowRemoveBtn = false;
                    ViewBag.PageMode = "Update";
                    Data = base.RenderRazorViewToString("Shared/Questions/_AddEditVacQue", ObjVacQue);
                }
                else
                {
                    IsError = true;
                    Message = "Not able to retrieve Data";
                }

                return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                throw;
            }
        }


        [HttpGet]
        public JsonResult CreateVacRnd()
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                BEClient.VacQueCat ObjVacQueCat = new BEClient.VacQueCat();
                Data = base.RenderRazorViewToString("Shared/RoundConfig/_CreateVacRndAcc", null);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetVacQueCatDetails(Guid QueCatId)
        {
            String Data = string.Empty;
            bool IsError = false;
            String Message = string.Empty;
            try
            {
                BLClient.QueCatAction QueCatAction = new BLClient.QueCatAction(_CurrentClientId);
                int Weight = QueCatAction.GetQueCatDetails(QueCatId);
                Data = Convert.ToString(Weight);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region VacQueCat

        [HttpGet]
        public JsonResult GetQueByVacQueCat(Guid VacQueCatId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {


                BEClient.ApplicationReviewProcess ObjApplicationReviewProcess = new BEClient.ApplicationReviewProcess();

                BLClient.VacancyQuestionAction ObjVacancyQuestionAction = new BLClient.VacancyQuestionAction(_CurrentClientId, true);
                ObjApplicationReviewProcess.objVacancyQuestionlst = new BESrp.DynamicSRP<List<BEClient.VacancyQuestion>>();
                ObjApplicationReviewProcess.objVacancyQuestionlst.UserPermissionWithScope = ObjVacancyQuestionAction.ListUserPermissionWithScope;

                ObjApplicationReviewProcess.objVacancyQuestionlst.obj = ObjVacancyQuestionAction.GetVacQueByVacQueCatId(VacQueCatId, _CurrentLanguageId);
                List<KeyValuePair<int, string>> objQueDataType = Common.CommonFunctions.QuestionType.ToList();
                ObjApplicationReviewProcess.objVacancyQuestionlst.obj.ForEach(a => a.QueTypeLocalName = objQueDataType.SingleOrDefault(b => b.Key == a.QueType).Value.ToString());

                Data = base.RenderRazorViewToString("Shared/Questions/_QuestionDetail", ObjApplicationReviewProcess);


            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, string.Empty, Data), JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddVacQueCatModel(Guid VacancyRndId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                BLClient.VacQueCatAction ObjVacQueCatAction = new BLClient.VacQueCatAction(_CurrentClientId, true);
                BESrp.DynamicSRP<List<BEClient.VacQueCat>> objVacQueCatList = new BESrp.DynamicSRP<List<BEClient.VacQueCat>>();
                objVacQueCatList.UserPermissionWithScope = ObjVacQueCatAction.ListUserPermissionWithScope;
                objVacQueCatList.obj = ObjVacQueCatAction.GetVacQueCatByRoundId(VacancyRndId, _CurrentLanguageId);

                Data = base.RenderRazorViewToString("Shared/VacQueCat/_VacQueCatAcc", objVacQueCatList);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddSaveVacQueCat(BEClient.VacQueCat VacQueCat)
        {
            String Data = string.Empty;
            String Message = String.Empty;
            bool IsError = false;
            try
            {
                BLClient.VacQueCatAction objVacQueCatAction = new BLClient.VacQueCatAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);

                if (VacQueCat.VacQueCatId == Guid.Empty)
                {
                    foreach (Guid QueCatId in VacQueCat.ListQueCatId)
                    {
                        VacQueCat.QueCatId = QueCatId;
                        VacQueCat.VacQueCatId = objVacQueCatAction.AddSaveVacQueCat(VacQueCat);
                    }
                    if (!VacQueCat.VacQueCatId.Equals(Guid.Empty))
                    {
                        //FillDropDownVacQueCat(VacQueCat.VacRndId);
                        BLClient.QueCatAction _objQueCatAction = new BLClient.QueCatAction(_CurrentClientId, true, true);
                        VacQueCat.objQueCat = _objQueCatAction.GetRecordByRecordId(VacQueCat.QueCatId, VacQueCat.DivisionId, _CurrentLanguageId);
                        VacQueCat.PermissionType = VacQueCat.objQueCat.PermissionType;
                        Data = base.RenderRazorViewToString("Shared/VacQueCat/_VacQueCatDetail", VacQueCat);
                        Message = "Record Inserted Successfully";
                    }
                }
                else
                {
                    bool Result = objVacQueCatAction.UpdateVacQueCat(VacQueCat);
                    if (Result == true)
                    {
                        BLClient.QueCatAction _objQueCatAction = new BLClient.QueCatAction(_CurrentClientId, true, true);
                        VacQueCat.objQueCat = _objQueCatAction.GetRecordByRecordId(VacQueCat.QueCatId, VacQueCat.DivisionId, _CurrentLanguageId);
                        VacQueCat.PermissionType = VacQueCat.objQueCat.PermissionType;
                        Data = base.RenderRazorViewToString("Shared/VacQueCat/_VacQueCatDetail", VacQueCat);
                        Message = "Record Inserted Successfully";
                    }
                }
            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
        }

        [HttpPost]
        public JsonResult DeleteVacRevMember(Guid VacReviewMemberId)
        {
            String Message = String.Empty;
            bool IsError = true;
            String Data = "";
            try
            {
                if (!VacReviewMemberId.Equals(Guid.Empty))
                {
                    BLClient.ReviewersAction ReviewersAction = new BLClient.ReviewersAction(_CurrentClientId);
                    bool IsDelete = ReviewersAction.DeleteVacReviewMemberById(VacReviewMemberId);
                    Data = Convert.ToString(IsDelete);
                }
            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
        }
        #endregion

        #region VacQueCat
        [HttpGet]
        public JsonResult GetVacQueCat(Guid VacancyRndId, Guid VacQueCatId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                BEClient.VacQueCat ObjVacQueCat = new BEClient.VacQueCat();
                ViewBag.PageMode = "Update";
                BLClient.VacQueCatAction objVacQueCatAction = new BLClient.VacQueCatAction(_CurrentClientId);
                ObjVacQueCat = objVacQueCatAction.GetVacQueCatById(VacQueCatId);

                BLClient.QueCatAction QueCatAction = new BLClient.QueCatAction(_CurrentClientId);
                List<BEClient.QueCat> QueCatList = QueCatAction.FillQueCat(_CurrentLanguageId);
                ViewBag.AllQueCat = new SelectList(QueCatList, "QueCatId ", "LocalName");

                Data = base.RenderRazorViewToString("Shared/VacQueCat/_AddEditVacQueCat", ObjVacQueCat);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult CreateVacQueCat(Guid VacancyRndId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                BEClient.VacQueCat ObjVacQueCat = new BEClient.VacQueCat();
                ObjVacQueCat.VacRndId = VacancyRndId;

                FillDropDownVacQueCat(VacancyRndId);

                VacancyRoundAction objVacancyRoundAction = new VacancyRoundAction(_CurrentClientId);
                ObjVacQueCat.DivisionId = objVacancyRoundAction.GetDivisionIdByVacRndId(VacancyRndId);
                //Get Vac Division ID, use this while adding data and again get record from DB

                Data = base.RenderRazorViewToString("Shared/VacQueCat/_CreateVacQueCat", ObjVacQueCat);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }
        private void FillDropDownVacQueCat(Guid VacRndId)
        {
            BLClient.QueCatAction QueCatAction = new BLClient.QueCatAction(_CurrentClientId);
            List<BEClient.QueCat> QueCatList = QueCatAction.GetQueByQueCatId(VacRndId, _CurrentLanguageId);
            ViewBag.AllQueCat = new SelectList(QueCatList, "QueCatId ", "LocalName");
        }
        private List<BEClient.QueCat> GetQuestionCategoryList()
        {
            List<BEClient.QueCat> objQueCat = null;
            try
            {
                objQueCat = QueCatActionObject().GetAllQueCatLanguage(_CurrentLanguageId);
            }
            catch
            {
                objQueCat = null;
            }
            return objQueCat;
        }
        #endregion
        // Start CRNOV25
        public string GetRndTypeIdByCandidateSelfEvalution()
        {
            BLClient.RndTypeAction _objRndTypeAction = new BLClient.RndTypeAction(_CurrentClientId);
            List<Guid> lstRndType = _objRndTypeAction.GetRndTypeIdByCandidateSelfEvalution();
            String lstRndTypeStr = String.Join(",", lstRndType.Select(g => g.ToString()).ToArray());
            return lstRndTypeStr;
        }
        //End CRNOV25
        [HttpPost]
        public JsonResult UpdateApplicationBasedStatus(Guid ApplicationId, Guid ApplicationBasedStatusId, Guid? VacancyId)
        {
            bool IsError = false;
            string Message = string.Empty;
            string Data = string.Empty;
            try
            {
                if (ApplicationId != Guid.Empty && ApplicationBasedStatusId != Guid.Empty)
                {
                    BLClient.ApplicationAction _objApplicationAction = new BLClient.ApplicationAction(_CurrentClientId);
                    bool statusUpdated = _objApplicationAction.UpdateApplicationBasedStatus(ApplicationId, ApplicationBasedStatusId);
                    if (statusUpdated)
                    {
                        SendDeclinedMail(ApplicationId, ApplicationBasedStatusId);
                        Message = "Applicant has been Rejected";
                        IsError = false;
                        if (VacancyId != null && VacancyId != Guid.Empty)
                        {
                            Data = GetApplicationStatusMenu((Guid)VacancyId);
                        }
                    }
                    else
                    {
                        Message = "Applicant has not been Rejected!!";
                        IsError = true;
                    }
                }
            }
            catch
            {
                Message = "Applicant has not been Rejected!!";
                IsError = true;
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
        }

        public void SendDeclinedMail(Guid ApplicationId, Guid ApplicationBasedStatusId)
        {
            try
            {
                BLClient.ApplicationBasedStatusAction objABSAction = new BLClient.ApplicationBasedStatusAction(_CurrentClientId);
                BEClient.ApplicationBasedStatus objABS = objABSAction.GetEmailContentByABSId(ApplicationBasedStatusId, ApplicationId);
                if (objABS != null && !string.IsNullOrEmpty(objABS.Subject) && !string.IsNullOrEmpty(objABS.EmailDescription))
                {
                    BLClient.UserDetailsAction UserDetailsAction = new BLClient.UserDetailsAction(_CurrentClientId);
                    BEClient.UserDetails ObjUserDetails = new BEClient.UserDetails();
                    ObjUserDetails = UserDetailsAction.GetUserDetailsByAppId(ApplicationId);

                    objABS.EmailDescription = objABS.EmailDescription.Replace("##CANDIDATE.FIRSTNAME##", ObjUserDetails.FirstName);
                    objABS.EmailDescription = objABS.EmailDescription.Replace("##CANDIDATE.LASTNAME##", ObjUserDetails.LastName);
                    objABS.EmailDescription = objABS.EmailDescription.Replace("##CANDIDATE.FULLNAME##", ObjUserDetails.FirstName + " " + ObjUserDetails.LastName);

                    Mailers.EmailTemplate mvcMailer = new Mailers.EmailTemplate();
                    mvcMailer.SendMessage(ObjUserDetails.UserName, objABS.Subject, objABS.EmailDescription, false, null, true);
                }
            }
            catch (Exception)
            { }
        }

        [HttpPost]
        public JsonResult ReactiveBasedStatus(Guid ApplicationId, Guid? VacancyId)
        {
            bool IsError = false;
            string Message = string.Empty;
            string Data = string.Empty;
            try
            {
                if (ApplicationId != Guid.Empty)
                {
                    BLClient.ApplicationAction _objApplicationAction = new BLClient.ApplicationAction(_CurrentClientId);
                    bool statusUpdated = _objApplicationAction.ReactiveApplication(ApplicationId);
                    if (statusUpdated)
                    {
                        Message = "Applicant has been Reactivated";
                        IsError = false;
                        if (VacancyId != null && VacancyId != Guid.Empty)
                        {
                            Data = GetApplicationStatusMenu((Guid)VacancyId, "Declined");
                        }
                    }
                    else
                    {
                        Message = "Applicant has not been Reactivated!!";
                        IsError = true;
                    }
                }
            }
            catch
            {
                Message = "Applicant has not been Reactivated!!";
                IsError = true;
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
        }

        public JsonResult GetAllApplication(Guid VacancyId, string AppStatus = "Active")
        {
            bool IsError = false;
            string Message = string.Empty;
            string Data = string.Empty;
            try
            {
                BEClient.Vacancy ObjVacancy = new BEClient.Vacancy();
                BLClient.VacancyApplicantAction _ObjVacancyApplicantAction = new BLClient.VacancyApplicantAction(_CurrentClientId);
                //ObjVacancy.ApplicantVacancyList = _ObjVacancyApplicantAction.GetSubmittedApplicantByVacancyId(VacancyId);
                ObjVacancy.ApplicantVacancyList = _ObjVacancyApplicantAction.GetApplicantsByVacIdAndAppStatus(VacancyId, AppStatus);

                if (ObjVacancy.ApplicantVacancyList.Count > 0)
                {
                    BLClient.ApplicationBasedStatusAction _objAppBasedStatusAction = new BLClient.ApplicationBasedStatusAction(_CurrentClientId);
                    List<BEClient.ApplicationBasedStatus> listApplicationBasedStatus = _objAppBasedStatusAction.GetAllAppBasedStatus(_CurrentLanguageId);
                    int TotalRoundsInVacancy = _objVacancyAction.GetVacRndCountByVacancy(VacancyId);
                    foreach (var curr in ObjVacancy.ApplicantVacancyList)
                    {
                        BLClient.ReviewersAction objReviewerAction = new BLClient.ReviewersAction(_CurrentClientId);
                        BLClient.VacancyRoundAction objVacancyRoundAction = new BLClient.VacancyRoundAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                        curr.objVacancyRound = new BEClient.VacancyRound();
                        //curr.objVacancyRoundList = objVacancyRoundAction.GetAllRound(curr.VacancyId, _CurrentLanguageId, curr.ApplicationId, false);
                        //ERRORCHECK
                        curr.objApplicationReviewProcess = new BEClient.ApplicationReviewProcess();
                        curr.objVacancyRound = objVacancyRoundAction.GetActiveRoundForApplication(curr.ApplicationId);
                        if (curr.objVacancyRound != null)
                        {
                            curr.objVacancyRound.isReviewer = objReviewerAction.AuthorizeRevmemberForPromoteCandidate(curr.objVacancyRound.VacancyRoundId, Common.CurrentSession.Instance.VerifiedUser.UserId);
                            curr.objVacancyRound.RndCnt = TotalRoundsInVacancy;// _objVacancyAction.GetVacRndCountByVacancy(VacancyId);
                        }
                        else
                        {
                            curr.objVacancyRound = new BEClient.VacancyRound();
                        }
                        //BLClient.ApplicationBasedStatusAction _objAppBasedStatusAction = new BLClient.ApplicationBasedStatusAction(_CurrentClientId);
                        //curr.objApplicationReviewProcess.ApplicationBasedStatusList = _objAppBasedStatusAction.GetAllAppBasedStatus(_CurrentLanguageId);
                        curr.objApplicationReviewProcess.ApplicationBasedStatusList = listApplicationBasedStatus;
                        ObjVacancy.IsDeclineApplication = _objAppBasedStatusAction.IsApplicationDecline(curr.ApplicationId);
                        curr.objApplicationReviewProcess.IsApplicationDecline = ObjVacancy.IsDeclineApplication;

                        BEClient.CandidateProfile objCandidateProfile = new BEClient.CandidateProfile();
                        BLClient.ApplicationDocumentsAction objAppDocAction = new BLClient.ApplicationDocumentsAction(_CurrentClientId);
                        List<BEClient.ApplicationDocuments> objAppDocList = objAppDocAction.GetRequiredDocumentsByApplicationId(curr.ApplicationId);
                        if (objAppDocList != null && objAppDocList.Count > 0)
                        {
                            objAppDocList = objAppDocList.Where(x => x.IsPending == false).ToList();
                        }
                        curr.objApplicationReviewProcess.ObjApplicationDocuments = objAppDocList;
                    }
                }
                Data = base.RenderRazorViewToString("_ApplicantList", ObjVacancy.ApplicantVacancyList);
            }
            catch
            {
                IsError = true;
                Message = "Error";
            }
            return GetJson(GetJsonContent(IsError, string.Empty, Message, Data));
        }

        public JsonResult GetApplicationByVacIdAndStatus(Guid VacancyId, string VacStatus)
        {
            bool IsError = false;
            string Message = string.Empty;
            string Data = string.Empty;
            try
            {
                BEClient.Vacancy ObjVacancy = new BEClient.Vacancy();
                BLClient.VacancyApplicantAction _ObjVacancyApplicantAction = new BLClient.VacancyApplicantAction(_CurrentClientId);
                ObjVacancy.ApplicantVacancyList = _ObjVacancyApplicantAction.GetApplicantsByVacIdAndAppStatus(VacancyId, VacStatus);
                if (ObjVacancy.ApplicantVacancyList.Count > 0)
                {
                    foreach (var curr in ObjVacancy.ApplicantVacancyList)
                    {
                        BLClient.ReviewersAction objReviewerAction = new BLClient.ReviewersAction(_CurrentClientId);
                        BLClient.VacancyRoundAction objVacancyRoundAction = new BLClient.VacancyRoundAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                        curr.objVacancyRound = new BEClient.VacancyRound();
                        //curr.objVacancyRoundList = objVacancyRoundAction.GetAllRound(curr.VacancyId, _CurrentLanguageId, curr.ApplicationId, false);
                        curr.objApplicationReviewProcess = new BEClient.ApplicationReviewProcess();
                        curr.objVacancyRound = objVacancyRoundAction.GetActiveRoundForApplication(curr.ApplicationId);
                        if (curr.objVacancyRound != null)
                        {
                            curr.objVacancyRound.isReviewer = objReviewerAction.AuthorizeRevmemberForPromoteCandidate(curr.objVacancyRound.VacancyRoundId, Common.CurrentSession.Instance.VerifiedUser.UserId);
                            curr.objVacancyRound.RndCnt = _objVacancyAction.GetVacRndCountByVacancy(VacancyId);
                        }
                        else
                        {
                            curr.objVacancyRound = new BEClient.VacancyRound();
                        } BLClient.ApplicationBasedStatusAction _objAppBasedStatusAction = new BLClient.ApplicationBasedStatusAction(_CurrentClientId);
                        curr.objApplicationReviewProcess.ApplicationBasedStatusList = _objAppBasedStatusAction.GetAllAppBasedStatus(_CurrentLanguageId);
                        ObjVacancy.IsDeclineApplication = _objAppBasedStatusAction.IsApplicationDecline(curr.ApplicationId);
                        curr.objApplicationReviewProcess.IsApplicationDecline = ObjVacancy.IsDeclineApplication;
                    }
                }
                Data = base.RenderRazorViewToString("_ApplicantList", ObjVacancy.ApplicantVacancyList);
            }
            catch
            {
                IsError = true;
                Message = "Error";
            }
            return GetJson(GetJsonContent(IsError, string.Empty, Message, Data));
        }

        [HttpPost]
        public JsonResult GetVacancyListForFilter(string applicationStatus)
        {
            string StatusFilterString = "";
            Guid? UserId = null;
            if (string.IsNullOrEmpty(applicationStatus) || applicationStatus == "Active")
            {
                StatusFilterString += " AND ApplicationStatus Not In ('Draft', 'Declined')";
            }
            else if (applicationStatus == "Assigned To Me")
            {
                StatusFilterString += "AND ApplicationStatus <> 'Draft'";
            }
            else
            {
                StatusFilterString += " AND ApplicationStatus='" + applicationStatus + "'";
            }
            if (!string.IsNullOrEmpty(applicationStatus) && applicationStatus == "Assigned To Me")
            {
                UserId = ATSCommon.CurrentSession.Instance.UserId;
            }
            List<ATS.BusinessEntity.Vacancy> _objVacancyList = _objVacancyAction.GetVacancyListForFilter(_CurrentLanguageId, StatusFilterString, UserId);
            SelectList newlist = new SelectList(_objVacancyList, "VacancyId", "JobTitle");

            return Json(newlist);
            //return base.GetJson(base.GetJsonContent(false, null, "", newlist));
        }

        public ActionResult ApplyNewVacancyTemplate(Guid TVacId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = "";
            try
            {
                BLClient.CopyTemplateAction objCopyTemplateAction = new BLClient.CopyTemplateAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                Guid newVacancyId = objCopyTemplateAction.TemplateToMainObject(TVacId, _CurrentLanguageId);
                BLClient.VacancyHistoryAction objDeleteVacHistroy = new BLClient.VacancyHistoryAction(_CurrentClientId);

                ////Delete Vacancy History if Vacancy is crated using VACANCY TEMPLATE
                objDeleteVacHistroy.DeleteVacancyHistoryByVacancyId(newVacancyId);

                try
                {
                    ATSCommon.CommonFunctions.SolrFullImport();
                }
                catch (Exception ex)
                {
                    log.Error("SOLR Employee FULL IMPORT (Create Vacancy)" + ex.Message);
                }

                BESrp.DynamicSRP<BEClient.Vacancy> objBreadScrumbModelVacancy = new BESrp.DynamicSRP<BEClient.Vacancy>();
                BESrp.DynamicSRP<BEClient.Vacancy> objVacancy = new BESrp.DynamicSRP<BEClient.Vacancy>();

                CreateObjVacancy(newVacancyId);
                objVacancy.obj = _objVacancyAction.GetVacancyById(newVacancyId, _CurrentLanguageId);
                ViewBag.PageMode = BEClient.PageMode.Update;
                objVacancy.RecordPermissionType = objVacancy.obj.PermissionType;
                string callMethod = ATS.WebUi.Common.CommonFunctions.GetPageMode(objVacancy.RecordPermissionType, BEClient.PageMode.Update).ToString();
                DropDownLists(BEClient.ATSPermissionType.Modify, objVacancy.obj.DivisionId, callMethod, objVacancy.obj.PositionOwner);
                objVacancy.obj.VacancyId = newVacancyId;
                objVacancy.obj.RndCount = _objVacancyAction.GetVacRndCountByVacancy(newVacancyId);
                objBreadScrumbModelVacancy = new BESrp.DynamicSRP<BEClient.Vacancy>();
                objBreadScrumbModelVacancy = objVacancy;
                return RedirectToAction(STR_VACANCY_VIEW_METHOD, new { id = newVacancyId, ForTemplate = true });
            }
            catch (Exception Ex)
            {
                throw;
            }
        }

        [HttpGet]
        public JsonResult CheckForRoundNo(Guid RndTypeId, Guid VacId, string VacRndId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            Guid _VacRndId = Guid.Empty;
            Guid.TryParse(VacRndId.ToString(), out _VacRndId);

            try
            {
                BLClient.RoundAttributesAction RndTypeAction = new BLClient.RoundAttributesAction(_CurrentClientId);
                int RndAttrNo = RndTypeAction.GetRoundAttributesNo(RndTypeId);
                ViewBag.YesNoDropDownPromoteCandidate = Common.CommonFunctions.YesNoDropDownList();
                BEClient.ApplicationReviewProcess objApplicationReviewProcess = new BEClient.ApplicationReviewProcess();
                objApplicationReviewProcess.objVacancyRound = new BEClient.VacancyRound();
                objApplicationReviewProcess.objVacancyRound = NewVacancyRound();
                objApplicationReviewProcess.objVacancyRound.RndTypeId = RndTypeId;
                objApplicationReviewProcess.objVacancyRound.VacancyRoundId = _VacRndId;
                objApplicationReviewProcess.objVacancyRound.VacancyId = VacId;
                objApplicationReviewProcess.objVacancyRound.RoundAttributeNo = RndAttrNo;
                if (_VacRndId != Guid.Empty)
                {
                    UpdateRoundConfigDropDown(VacId, _VacRndId, RndAttrNo);
                }
                else
                {
                    FillComboRndType(VacId, RndAttrNo);
                }
                if (_VacRndId != Guid.Empty)
                {
                    Data = base.RenderRazorViewToString("Shared/RoundConfig/RoundConfiguration", objApplicationReviewProcess);
                }
                else
                {
                    Data = base.RenderRazorViewToString("Shared/_RoundConfig", objApplicationReviewProcess.objVacancyRound);
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetMyVacancyGearAction(Guid VacancyId)
        {
            string Message = String.Empty;
            bool IsError = false;
            string Data = String.Empty;
            try
            {
                BLClient.VacancyAction ObjVacancyAction = new BLClient.VacancyAction(_CurrentClientId);

                RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.Vacancy>> _bcModel = new BreadScrumbModel<BESrp.DynamicSRP<BEClient.Vacancy>>();
                CreateObjVacancy(VacancyId);
                _objVacancy.obj = _objVacancyAction.GetVacancyById(VacancyId, base.CurrentClient.CurrentLanguageId);
                _objVacancy.UserPermissionWithScope = new List<BESrp.UserPermissionWithScope>();
                _objVacancy.UserPermissionWithScope.Add(new BESrp.UserPermissionWithScope() { Scope = _objVacancy.obj.Scope, PermissionType = _objVacancy.obj.PermissionType });
                BEClient.PageMode objPageMode = BEClient.PageMode.View;
                objPageMode = BEClient.PageMode.Update;
                if (_objVacancy.obj.PermissionType == null)
                {
                    throw new Exception("Non authorized user");
                }
                if (_objVacancy.obj.PermissionType.Contains(BEClient.ATSPermissionType.Delete))
                {
                    ViewBag.IsDelete = true;
                }
                _objVacancy.RecordPermissionType = _objVacancy.obj.PermissionType;
                string callMethod = ATS.WebUi.Common.CommonFunctions.GetPageMode(_objVacancy.RecordPermissionType, BEClient.PageMode.Update).ToString();
                DropDownLists(BEClient.ATSPermissionType.Modify, _objVacancy.obj.DivisionId, callMethod, _objVacancy.obj.PositionOwner);
                _bcModel.obj = _objVacancy;
                ViewBag.PageMode = objPageMode;
                _bcModel.obj.obj.ApplicantVacancyList = new List<BEClient.VacancyApplicant>();
                Data = base.RenderRazorViewToString("Shared/_VacancyGearAction", _bcModel);
                Message = "Confirmed Successfully!!";
                IsError = false;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ConfirmationRequired(Guid Vacancyid, int index)
        {
            string Message = String.Empty;
            bool IsError = false;
            string Data = String.Empty;
            try
            {
                BLClient.VacancyAction ObjVacancyAction = new BLClient.VacancyAction(_CurrentClientId);
                bool Result = ObjVacancyAction.UpdateConfirmationRequiredByVacancy(Vacancyid, index);
                if (Result)
                {
                    RootModels.BreadScrumbModel<BESrp.DynamicSRP<BEClient.Vacancy>> _bcModel = new BreadScrumbModel<BESrp.DynamicSRP<BEClient.Vacancy>>();

                    CreateObjVacancy(Vacancyid);
                    _objVacancy.obj = _objVacancyAction.GetVacancyById(Vacancyid, base.CurrentClient.CurrentLanguageId);

                    _objVacancy.UserPermissionWithScope = new List<BESrp.UserPermissionWithScope>();
                    _objVacancy.UserPermissionWithScope.Add(new BESrp.UserPermissionWithScope() { Scope = _objVacancy.obj.Scope, PermissionType = _objVacancy.obj.PermissionType });
                    BEClient.PageMode objPageMode = BEClient.PageMode.View;
                    objPageMode = BEClient.PageMode.Update;
                    if (_objVacancy.obj.PermissionType == null)
                    {
                        throw new Exception("Non authorized user");
                    }
                    if (_objVacancy.obj.PermissionType.Contains(BEClient.ATSPermissionType.Delete))
                    {
                        ViewBag.IsDelete = true;
                    }
                    _objVacancy.RecordPermissionType = _objVacancy.obj.PermissionType;
                    string callMethod = ATS.WebUi.Common.CommonFunctions.GetPageMode(_objVacancy.RecordPermissionType, BEClient.PageMode.Update).ToString();
                    DropDownLists(BEClient.ATSPermissionType.Modify, _objVacancy.obj.DivisionId, callMethod, _objVacancy.obj.PositionOwner);
                    _bcModel.obj = _objVacancy;
                    ViewBag.PageMode = objPageMode;
                    _bcModel.obj.obj.ApplicantVacancyList = new List<BEClient.VacancyApplicant>();
                    Data = base.RenderRazorViewToString("Shared/_VacancyGearAction", _bcModel);
                    Message = "Confirmed Successfully!!";
                    IsError = false;
                }
                else
                {
                    Message = "Not Confirmed !!";
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

        [HttpGet]
        public JsonResult GetDeclinedAutoEmail(Guid VacancyId)
        {
            string Message = String.Empty;
            bool IsError = false;
            string Data = String.Empty;
            try
            {
                BLClient.DeclinedAutoEmailAction objAutoEmailAction = new BLClient.DeclinedAutoEmailAction(_CurrentClientId);
                List<BEClient.DeclinedAutoEmail> objAppBasedStatusList = new List<BEClient.DeclinedAutoEmail>();
                objAppBasedStatusList = objAutoEmailAction.FillAppBasedStatusByVacancyId(VacancyId);
                List<BEClient.DeclinedAutoEmail> objAllStatusList = new List<BEClient.DeclinedAutoEmail>();
                List<BEClient.DeclinedAutoEmail> objSelectedStatusList = new List<BEClient.DeclinedAutoEmail>();
                BEClient.DeclinedAutoEmail objAutomatedEmails = new BEClient.DeclinedAutoEmail();
                objAllStatusList = objAppBasedStatusList.Where(m => m.ApplicationBasedStatusName != "SUBMIT-APP-EMAIL").ToList();
                objSelectedStatusList = objAllStatusList.Where(m => m.IsSend == true).ToList();
                objAutomatedEmails.SelectedList = new SelectList(objSelectedStatusList, "ApplicationBasedStatusId", "ApplicationBasedStatusName");
                objAutomatedEmails.AllValues = new SelectList(objAllStatusList, "ApplicationBasedStatusId", "ApplicationBasedStatusName");
                objAutomatedEmails.EmailOnSubmitApp = objAppBasedStatusList.Where(m => m.ApplicationBasedStatusName == "SUBMIT-APP-EMAIL").Select(m => m.IsSend).FirstOrDefault();
                objAutomatedEmails.ApplyEmailTemplateId = objAppBasedStatusList.Where(m => m.ApplicationBasedStatusName == "SUBMIT-APP-EMAIL").Select(m => m.ApplyEmailTemplateId).FirstOrDefault();
                List<BEClient.EmailTemplates> ListEmailTemplates = new List<BEClient.EmailTemplates>();
                BLClient.EmailTemplatesAction EmailTemplateAction = new BLClient.EmailTemplatesAction(_CurrentClientId);
                ListEmailTemplates = EmailTemplateAction.FillEmailTemplatesByCategory(Convert.ToInt32(BEClient.EmailTemplateCateogory.New_Vacancy_Applications), _CurrentLanguageId);
                ViewBag.DRPEmailTemplates = new SelectList(ListEmailTemplates, "EmailTemplateId", "EmailName");

                Data = base.RenderRazorViewToString("Shared/_AutomatedEmail", objAutomatedEmails);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InsertDeclinedAutoEmail(BEClient.DeclinedAutoEmail objAutoEmail)
        {
            string Message = String.Empty;
            bool IsError = false;
            string Data = String.Empty;
            try
            {
                BLClient.VacancyAction objVacancyAction = new BLClient.VacancyAction(_CurrentClientId);
                bool Result = objVacancyAction.UpdateSendApplyEmail(objAutoEmail.VacancyId, objAutoEmail.ApplyEmailTemplateId, objAutoEmail.EmailOnSubmitApp);

                BLClient.DeclinedAutoEmailAction objAutoEmailAction = new BLClient.DeclinedAutoEmailAction(_CurrentClientId);
                Result = objAutoEmailAction.DeleteAllDeclinedAutoEmailByVacancyId(objAutoEmail.VacancyId);
                foreach (var item in objAutoEmail.ListApplicationBasedStatusId)
                {
                    objAutoEmail.ApplicationBasedStatusId = new Guid(item);
                    Guid Id = objAutoEmailAction.AddDeclinedAutoEmail(objAutoEmail);
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data));
        }
        [HttpGet]
        public JsonResult VacRndReorder(Guid VacancyId, Guid VacRndId, string OrderDir)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                BLClient.VacancyRoundAction objVacRndAction = new BLClient.VacancyRoundAction(_CurrentClientId);
                bool result = objVacRndAction.UpdateVacRndOrder(VacancyId, VacRndId, OrderDir);
                if (result)
                {
                    Message = "Order Updates Successfully";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, null), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult VacQueCatReorder(Guid VacRndId, Guid VacQueCatId, string OrderDir)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                BLClient.VacQueCatAction objVacQueCatAction = new BLClient.VacQueCatAction(_CurrentClientId);
                bool result = objVacQueCatAction.UpdateVacQueCatOrder(VacRndId, VacQueCatId, OrderDir);
                if (result)
                {
                    Message = "Order Updates Successfully";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, null), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult VacQueReorder(Guid VacQueCatId, Guid VacQueId, string OrderDir)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                BLClient.VacancyQuestionAction objVacQueAction = new BLClient.VacancyQuestionAction(_CurrentClientId);
                bool result = objVacQueAction.UpdateVacQueOrder(VacQueCatId, VacQueId, OrderDir);
                if (result)
                {
                    Message = "Order Updates Successfully";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, null), JsonRequestBehavior.AllowGet);
        }

        #region REQUIREDDOCUMENTS

        [HttpGet]
        public JsonResult GetRequiredDocuments(Guid VacRndId)
        {
            String Data = String.Empty;
            String Message = String.Empty;
            bool IsError = false;
            try
            {
                BESrp.DynamicSRP<List<BEClient.RequiredDocument>> objRequiredDocumentList = new BESrp.DynamicSRP<List<BEClient.RequiredDocument>>();
                BLClient.RequiredDocumentAction objRequiredDocAction = new BLClient.RequiredDocumentAction(_CurrentClientId, true);
                objRequiredDocumentList.UserPermissionWithScope = objRequiredDocAction.ListUserPermissionWithScope;
                objRequiredDocumentList.obj = objRequiredDocAction.GetRequiredDocumentByVacRndId(VacRndId);
                if (objRequiredDocumentList.obj != null && objRequiredDocumentList.obj.Count > 0)
                {
                    ViewBag.IsAddReqDoc = objRequiredDocumentList.obj[0].PermissionType.Contains(BEClient.ATSPermissionType.Modify);
                }
                else
                {
                    VacancyRoundAction objVacRndAction = new VacancyRoundAction(_CurrentClientId, true);
                    BEClient.Vacancy objVac = new BEClient.Vacancy();
                    objVac.DivisionId = objVacRndAction.GetDivisionIdByVacRndId(VacRndId);
                    ActionBase objAc = new ActionBase(_CurrentClientId);
                    objAc.SetRoleRecordWise(objVac, objVac.DivisionId);
                    int i = objVacRndAction.ListUserPermissionWithScope.Where(x => x.PermissionType.Contains(BEClient.ATSPermissionType.Modify)).ToList().Count();
                    ViewBag.IsAddReqDoc = i >= 1 ? true : false;
                }
                Data = RenderRazorViewToString("Shared/RequiredDocument/_RequiredDocumentList", objRequiredDocumentList.obj);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAddRequiredDocuments(Guid VacRndId)
        {
            String Data = String.Empty;
            String Message = String.Empty;
            bool IsError = false;
            try
            {
                FillRequiredDocuments(VacRndId);
                BEClient.RequiredDocument objRequiredDocument = new BEClient.RequiredDocument();
                objRequiredDocument.VacRndId = VacRndId;
                Data = RenderRazorViewToString("Shared/RequiredDocument/_AddEditRequiredDocument", objRequiredDocument);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        public void FillRequiredDocuments(Guid? VacRndId = null)
        {
            List<BEClient.DocumentType> objDocumentTypeList = new List<BEClient.DocumentType>();
            BLClient.DocumentTypeAction objDocumentTypeAction = new BLClient.DocumentTypeAction(_CurrentClientId);
            objDocumentTypeList = objDocumentTypeAction.FillDocumentType(_CurrentLanguageId, VacRndId);
            ViewBag.drpDocumentTypeList = new SelectList(objDocumentTypeList, "DocumentTypeId", "DocumentName");
        }

        public JsonResult AddSaveRequiredDocument(BEClient.RequiredDocument objRequiredDocuments)
        {
            String Data = String.Empty;
            String Message = String.Empty;
            bool IsError = false;
            try
            {
                BLClient.RequiredDocumentAction objRequiredDocumentAction = new BLClient.RequiredDocumentAction(_CurrentClientId);
                foreach (var DocumentTypeId in objRequiredDocuments.ListDocumentTypeId)
                {
                    objRequiredDocuments.DocumentTypeId = DocumentTypeId;
                    Guid RequiredDocumentId = objRequiredDocumentAction.InsertRequiredDocument(objRequiredDocuments);
                    if (RequiredDocumentId == Guid.Empty)
                    {
                        throw new Exception("Required Document not Added");
                    }
                }
                Message = "Required Document Added Successfully";

            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data));
        }

        public JsonResult DeleteRequiredDocument(Guid RequiredDocumentId)
        {
            String Data = String.Empty;
            String Message = String.Empty;
            bool IsError = false;
            try
            {
                BLClient.RequiredDocumentAction objRequiredDocumentAction = new BLClient.RequiredDocumentAction(_CurrentClientId);
                bool Result = objRequiredDocumentAction.DeleteRequiredDocument(RequiredDocumentId);
                if (Result)
                {
                    Message = "Required Document Deleted Successfully";
                }
                else
                {
                    IsError = true;
                    Message = "Required Document Deleted Failed";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data));
        }

        #endregion

        #region APPLICATION INSTRUCTION DOCUMENTS

        [HttpPost]
        public JsonResult UploadAppInstructionDocument(Guid VacancyId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                int count = Request.Files.Count;
                BLClient.AppInstructionDocsAction objAppInstructionDocsActionAction = new BLClient.AppInstructionDocsAction(_CurrentClientId);
                for (int i = 0; i < count; i++)
                {
                    HttpPostedFileBase resume = Request.Files[i] as HttpPostedFileBase;
                    string _oldFileName = string.Empty;
                    string _newFileName = string.Empty;
                    string _serverFilePath = string.Empty;
                    string _resumePath = ATS.WebUi.Common.CommonFunctions.ValidateOfferAttachments(resume, out _oldFileName, out _newFileName, out _serverFilePath);
                    ATS.WebUi.Common.CommonFunctions.SaveFile(resume, _resumePath);

                    BEClient.AppInstructionDoc objAppInstructionDoc = new BEClient.AppInstructionDoc();
                    objAppInstructionDoc.FileName = _oldFileName;
                    objAppInstructionDoc.NewFileName = _newFileName;
                    objAppInstructionDoc.Path = _serverFilePath;
                    objAppInstructionDoc.VacancyId = VacancyId;
                    Guid Result = objAppInstructionDocsActionAction.InsertAppInstructionDocs(objAppInstructionDoc);
                }
                List<BEClient.AppInstructionDoc> objAppInstructionDocList = new List<BEClient.AppInstructionDoc>();
                objAppInstructionDocList = objAppInstructionDocsActionAction.GetAppInstructionDocsByVacancyId(VacancyId);
                Data = base.RenderRazorViewToString("Shared/_AppInstructionDocList", objAppInstructionDocList);
                Message = "Application Instruction Document(s) Uploaded Successfully";
            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data));
        }

        public JsonResult DeleteAppInstructionDoc(Guid AppInstructionDocId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                BLClient.AppInstructionDocsAction objAppInstDocAction = new AppInstructionDocsAction(_CurrentClientId);
                bool Result = objAppInstDocAction.DeleteAppInstructionDoc(AppInstructionDocId);
                if (Result)
                {
                    Message = "Application Instruction Document Deleted Successfully";
                }
                else
                {
                    IsError = true;
                    Message = "Application Instruction Document Deleted Failed";
                }
            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data));
        }
        #endregion
    }
}
/// <summary>
/// A content result which can accepts a DotNetZip ZipFile object to write to the output stream
/// </summary>
public class ZipFileResult : ActionResult
{

    public ZipFile zip { get; set; }
    public string filename { get; set; }

    public ZipFileResult(ZipFile zip)
    {
        this.zip = zip;
        this.filename = null;
    }
    public ZipFileResult(ZipFile zip, string filename)
    {
        this.zip = zip;
        this.filename = filename;
    }

    public override void ExecuteResult(ControllerContext context)
    {
        var Response = context.HttpContext.Response;
        Response.ContentType = "application/zip";
        Response.AddHeader("Content-Disposition", "attachment;" + (string.IsNullOrEmpty(filename) ? "" : "filename=" + filename));
        zip.Save(Response.OutputStream);
        Response.End();
    }
}




//public ActionResult DownloadZipFileWithApplications(Guid AtsResumeId, Guid AtsCoverLetterId, Guid _ApplicationId)
//        {
//            try
//            {
//                ZipFile zip = new ZipFile("ATS_ZIP");

//                string archiveName = String.Format("ATS-{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
//                //var filesToInclude = new System.Collections.Generic.List<String>();
//                string path = Server.MapPath("~/");

//                //Get Resume File path
//                BEClient.ATSResume objAtsresume = new BEClient.ATSResume();
//                BLClient.ATSResumeAction objAtsResumeAction = new BLClient.ATSResumeAction(_CurrentClientId);
//                objAtsresume = objAtsResumeAction.GetRecordByRecorId(AtsResumeId);
//                if (objAtsresume != null && objAtsresume.IsCoverLetter == false && objAtsresume.NewFileName != string.Empty)
//                {
//                    if (System.IO.File.Exists(path + "\\Resume\\" + objAtsresume.NewFileName))
//                    {   //filesToInclude.Add(path + "\\Resume\\" + objAtsresume.NewFileName);
//                        zip.AddFile(path + "\\Resume\\" + objAtsresume.NewFileName).FileName = objAtsresume.UploadedFileName;
//                    }
//                }

//                //Get CoverLetter File path
//                if (Guid.Empty != AtsCoverLetterId)
//                {
//                    BEClient.ATSResume objAtsCoverLetter = new BEClient.ATSResume();
//                    BLClient.ATSResumeAction objAtsCoverLetterAction = new BLClient.ATSResumeAction(_CurrentClientId);
//                    objAtsCoverLetter = objAtsCoverLetterAction.GetRecordByRecorId(AtsCoverLetterId);
//                    if (objAtsCoverLetter != null && objAtsCoverLetter.IsCoverLetter == true && objAtsCoverLetter.NewFileName != string.Empty)
//                    {
//                        if (System.IO.File.Exists(path + "\\Resume\\" + objAtsCoverLetter.NewFileName))
//                        {
//                            //filesToInclude.Add(path + "\\Resume\\" + objAtsCoverLetter.NewFileName);
//                            zip.AddFile(path + "\\Resume\\" + objAtsCoverLetter.NewFileName).FileName = objAtsCoverLetter.UploadedFileName;
//                        }
//                    }
//                }

//                // Full Profile File Path
//                string URL = HttpContext.Request.Url.ToString().Substring(0, HttpContext.Request.Url.ToString().IndexOf("/Employee") + 1);
//                string pageURL = URL + "Employee/CandidateDetails?ApplicationId=" + _ApplicationId + "&ClientName=" + CurrentClient.Clientname + "&UserId=" + objAtsresume.UserId.ToString() + "&ShowHeader=true";
//                String serverMapPath = "";
//                String fileDownloadName = Common.CommonFunctions.GenerateTempFileName();
//                fileDownloadName = fileDownloadName + STR_PDF_EXTENSION;
//                serverMapPath = Common.CommonFunctions.HtmlToPdf(pageURL, "~/Resume/Temp/", fileDownloadName);
//                serverMapPath = HttpContext.Server.MapPath(serverMapPath);
//                if (serverMapPath != "")
//                {
//                    if (System.IO.File.Exists(serverMapPath))
//                    {
//                        //filesToInclude.Add(serverMapPath);
//                        zip.AddFile(serverMapPath).FileName = "Candidate_Profile.pdf";
//                    }
//                }

//                //ZipFile zip = new ZipFile();
//                //zip.AddFiles(filesToInclude, "ATS");
//                return new ZipFileResult(zip, archiveName);

//            }
//            catch (Exception ex)
//            {
//                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);

//                ///*Convert to json string*/
//                string jsonModels = JsonConvert.SerializeObject(actionStatus);
//                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);

//                /*Redirect to List Page*/
//                return RedirectToAction(ATS.WebUi.Common.Constants.STR_LIST_METHOD, new { KeyMsg = keyMsg });
//            }
//        }