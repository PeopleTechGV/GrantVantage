using ATS.WebUi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEClient = ATS.BusinessEntity;
using BLClient = ATS.BusinessLogic;
using ATSCommon = ATS.WebUi.Common;
using BECommon = ATS.BusinessEntity.Common;
using System.Collections;
using ATS.WebUi.Areas.Candidate.Models;
using RootModels = ATS.WebUi.Models;
using System.Text;

namespace ATS.WebUi.Areas.Candidate.Controllers
{
    public class JobApplicationController : ATS.WebUi.Controllers.AreaBaseController
    {
        // GET: /Candidate/JobApplication/
        private Guid _CurrentClientId;

        private Guid _CurrentLanguageId;
        private Guid _UserId;
        private string _url = string.Empty;
        #region OnAuthorization Method call on every request
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            _url = filterContext.HttpContext.Request.Url.AbsoluteUri.ToString();
            if (base.CurrentClient != null && Common.CurrentSession.Instance.VerifiedUser != null)
            {
                _CurrentClientId = base.CurrentClient.ClientId;
                _CurrentLanguageId = base.CurrentClient.CurrentLanguageId;
                _UserId = Common.CurrentSession.Instance.VerifiedUser.UserId;
                if (ATSCommon.CurrentSession.Instance.VerifiedUser.SecurityRoles.Where(x => x.AtsSecurityRole == BEClient.ATSSecurityRole.Candidate).Count() <= 0)
                {
                    filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_CANDIDATE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_CONTROLLER, new { area = ATSCommon.Constants.AREA_EMPLOYEE }));

                }
            }
        }
        #endregion

        private void NavIndex(Guid ApplicationId, String DisplayToolTip, String ordinal)
        {
            ATSCommon.CommonFunctions.SetNullNav(this.ControllerContext.RequestContext);
            List<BEClient.BreadCrumb> objBreadCrumblst = ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs;
            if (ordinal != null && !String.IsNullOrEmpty(ordinal.ToString()))
            {
                objBreadCrumblst.RemoveAll(x => x.ordinal >= Convert.ToInt32(ordinal.ToString()));
            }
            BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
            objBreadCrumb.Action = "Index";
            objBreadCrumb.Controller = "JobApplication";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_CANDIDATE;
            objBreadCrumb.ordinal = objBreadCrumblst.Count() + 1;
            objBreadCrumb.URL = Url.Action("Index", "JobApplication", new { ApplicationId = ApplicationId, area = ATS.WebUi.Common.Constants.AREA_CANDIDATE, ordinal = objBreadCrumb.ordinal });

            objBreadCrumb.ImagePath = BECommon.ImagePaths.UploadResumeImage;
            objBreadCrumb.ToolTip = "Job Applications </br>" + DisplayToolTip;

            objBreadCrumb.WithoutOrdinalURL = Url.Action("Index", "JobApplication", new { ApplicationId = ApplicationId, area = ATS.WebUi.Common.Constants.AREA_CANDIDATE });
            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);
            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
        }

        [HttpPost]
        public ActionResult Index(ATS.WebUi.Areas.Candidate.Models.JobApplication<RootModels.BreadScrumbModel<BEClient.CandidateApplications>> objJobApplication)
        {
            String Message = String.Empty;
            bool IsError = true;
            String Data = "";
            try
            {
                Guid _ApplicationId = objJobApplication.mainObject.obj.ApplicationId;
                if (!String.IsNullOrEmpty(Message))
                {
                    Exception ex = new Exception(Message);
                    throw ex;
                }
                if (objJobApplication != null)
                {

                    InsertAppRnd(objJobApplication, _ApplicationId);
                    JsonModels actionStatus = null;
                    BLClient.ApplicationAction _ObjApplicationAction = new BLClient.ApplicationAction(_CurrentClientId);
                    bool Result = _ObjApplicationAction.ChangeApplicationStatus(_ApplicationId, BECommon.ApplicationStatusOptionsConstant.APP_STAT_SUBMIT);
                    if (Result)
                    {
                        actionStatus = base.GetJsonContent(false, string.Empty, "Application Status Updated Successfully ");
                    }
                    else
                    {
                        actionStatus = base.GetJsonContent(true, string.Empty, "Not Able To Update Application Status ");
                    }
                    string jsonModels = JsonConvert.SerializeObject(actionStatus);
                    string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                    //return RedirectToAction("Index", new { KeyMsg = keyMsg });
                    return RedirectToAction("Index", "JobApplication", new { ApplicationId = _ApplicationId, ControllerName = "JobApplication" });
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return View();
        }

        #region CA Login
        [HttpGet]
        public JsonResult GetVacancyQuestions(BEClient.AnonymousUser AnUser, string VacancyId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                Guid ApplicationId = AnUser.ObjVacancy.VacancyId;
                JobApplication<RootModels.BreadScrumbModel<BEClient.CandidateApplications>> _jobAppWithQue = new JobApplication<BreadScrumbModel<BEClient.CandidateApplications>>();

                ViewBag.ScreenQue = 1;
                ViewBag.ApplicationStatus = "APP_STAT_DRAFT";
                string LstRndTypeQTSelf = GetRndTypeIdByCandidateSelfEvalution();
                BLClient.QuestionDetailAction _objQuestionDetailAction = new BLClient.QuestionDetailAction(_CurrentClientId);
                List<BEClient.QuestionDetails> _objQuestionDetails = _objQuestionDetailAction.GetQuestionDetails(ApplicationId, LstRndTypeQTSelf);
                if (_objQuestionDetails.Count == 0)
                    ViewBag.ScreenQue = 0;
                _jobAppWithQue.ListPickList = GetATSPickList(_objQuestionDetails.Where(x => x.Question.QueDataType.Equals(1)).ToList());
                _jobAppWithQue.ListCheckList = GetATSCheckList(_objQuestionDetails.Where(x => x.Question.QueDataType.Equals(2)).ToList());
                _jobAppWithQue.ListATSTextArea = GetATSTextArea(_objQuestionDetails.Where(x => x.Question.QueDataType.Equals(3)).ToList());
                _jobAppWithQue.ListATSTextBox = GetATSTextBox(_objQuestionDetails.Where(x => x.Question.QueDataType.Equals(4)).ToList());
                _jobAppWithQue.ListATSYesNo = GetATSYesNo(_objQuestionDetails.Where(x => x.Question.QueDataType.Equals(5)).ToList());
                _jobAppWithQue.ListATSScale = GetATSScale(_objQuestionDetails.Where(x => x.Question.QueDataType.Equals(6)).ToList());

                Data = "";
            }
            catch (Exception ex)
            {
                return GetJson(base.GetJsonContent(true, null, ex.Message));
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }
        #endregion

        public ActionResult Index(Guid ApplicationId, string KeyMsg, string ControllerName, String ordinal, string Status)
        {
            try
            {
                Guid _ApplicationId = ApplicationId;
                JsonModels resultJsonModel = null;
                if (!String.IsNullOrEmpty(KeyMsg))
                {
                    string deserializeKeyMsg = HelperLibrary.Encryption.EncryptionAlgo.DecryptData(KeyMsg);
                    resultJsonModel = JsonConvert.DeserializeObject<JsonModels>(deserializeKeyMsg);
                }
                if (resultJsonModel != null)
                {
                    ViewBag.IsError = resultJsonModel.IsError;
                    ViewBag.Message = resultJsonModel.Message;
                }

                RootModels.BreadScrumbModel<BEClient.CandidateApplications> _objBreadScrumbCandidateApplications = new BreadScrumbModel<BEClient.CandidateApplications>();
                BLClient.CandidateApplicationsAction _objCandidateApplicationAction = new BLClient.CandidateApplicationsAction(_CurrentClientId);
                _objBreadScrumbCandidateApplications.obj = _objCandidateApplicationAction.GetApplicationDetailByApplication(ApplicationId, _url);

                _objBreadScrumbCandidateApplications.DisplayName = "Job Application";
                _objBreadScrumbCandidateApplications.ImagePath = BECommon.ImagePaths.UploadResumeImage;
                _objBreadScrumbCandidateApplications.ToolTip = "Job Application";

                if (ApplicationId != null && ApplicationId != Guid.Empty)
                {
                    _objBreadScrumbCandidateApplications.obj.ApplicationId = (Guid)ApplicationId;
                }
                NavIndex(ApplicationId, _objBreadScrumbCandidateApplications.obj.objVacancy.JobTitle + " , " + _objBreadScrumbCandidateApplications.obj.objVacancy.LocationText, ordinal);
                ViewBag.IsView = false;
                JobApplication<RootModels.BreadScrumbModel<BEClient.CandidateApplications>> _jobAppWithQue = new JobApplication<BreadScrumbModel<BEClient.CandidateApplications>>();

                _jobAppWithQue.mainObject = _objBreadScrumbCandidateApplications;

                ViewBag.ScreenQue = 1;
                if (_objBreadScrumbCandidateApplications.obj.ApplicationStatus == "APP_STAT_DRAFT")
                {
                    ViewBag.ApplicationStatus = "APP_STAT_DRAFT";
                    string LstRndTypeQTSelf = GetRndTypeIdByCandidateSelfEvalution();
                    BLClient.QuestionDetailAction _objQuestionDetailAction = new BLClient.QuestionDetailAction(_CurrentClientId);
                    List<BEClient.QuestionDetails> _objQuestionDetails = _objQuestionDetailAction.GetQuestionDetails(ApplicationId, LstRndTypeQTSelf);
                    if (_objQuestionDetails.Count == 0)
                        ViewBag.ScreenQue = 0;
                    _jobAppWithQue.ListPickList = GetATSPickList(_objQuestionDetails.Where(x => x.Question.QueDataType.Equals(1)).ToList());
                    _jobAppWithQue.ListCheckList = GetATSCheckList(_objQuestionDetails.Where(x => x.Question.QueDataType.Equals(2)).ToList());
                    _jobAppWithQue.ListATSTextArea = GetATSTextArea(_objQuestionDetails.Where(x => x.Question.QueDataType.Equals(3)).ToList());
                    _jobAppWithQue.ListATSTextBox = GetATSTextBox(_objQuestionDetails.Where(x => x.Question.QueDataType.Equals(4)).ToList());
                    _jobAppWithQue.ListATSYesNo = GetATSYesNo(_objQuestionDetails.Where(x => x.Question.QueDataType.Equals(5)).ToList());
                    _jobAppWithQue.ListATSScale = GetATSScale(_objQuestionDetails.Where(x => x.Question.QueDataType.Equals(6)).ToList());
                }

                if (_objBreadScrumbCandidateApplications.obj.ApplicationStatus == "APP_STAT_SUBMIT")
                {
                    ViewBag.ApplicationStatus = "APP_STAT_SUBMIT";
                    string LstRndTypeQTSelf = GetRndTypeIdByCandidateSelfEvalution();
                    if (!string.IsNullOrEmpty(LstRndTypeQTSelf))
                    {
                        BLClient.AppQueAnsAction _AppQueAnsAction = new BLClient.AppQueAnsAction(_CurrentClientId);
                        string LstRndTypeId = GetRndTypeIdByCandidateSelfEvalution();
                        List<BEClient.AppQueAns> _objAppQueAns = _AppQueAnsAction.GetAppQueByApplicationId(ApplicationId, LstRndTypeId);
                        if (_objAppQueAns.Count == 0)
                            ViewBag.ScreenQue = 0;
                        _jobAppWithQue.ListPickList = GetSubmitPickList(_objAppQueAns.Where(x => x.Question.QueDataType.Equals(1)).ToList());
                        _jobAppWithQue.ListCheckList = GetSubmitCheckList(_objAppQueAns.Where(x => x.Question.QueDataType.Equals(2)).ToList());
                        _jobAppWithQue.ListATSTextArea = GetSubmitTextArea(_objAppQueAns.Where(x => x.Question.QueDataType.Equals(3)).ToList());
                        _jobAppWithQue.ListATSTextBox = GetSubmitTextBox(_objAppQueAns.Where(x => x.Question.QueDataType.Equals(4)).ToList());
                        _jobAppWithQue.ListATSYesNo = GetSubmitYesNo(_objAppQueAns.Where(x => x.Question.QueDataType.Equals(5)).ToList());
                        _jobAppWithQue.ListATSScale = GetSubmitScale(_objAppQueAns.Where(x => x.Question.QueDataType.Equals(6)).ToList());
                        _jobAppWithQue.ATSQueCommon = new RootModels.Question.ATSQuestionCommon();
                        _jobAppWithQue.ATSQueCommon.QueCatList = _objAppQueAns.Select(x => new Tuple<Guid, string>(x.Question.QueCatId, x.Question.QueCatLocalName)).Distinct().ToArray().ToList();
                        //TO Do:for Auto Promotion in Screening Round
                        //get Schint by Appi and VacrndId
                        BLClient.ScheduleIntAction objScheduleAction = new BLClient.ScheduleIntAction(_CurrentClientId);
                        BEClient.ScheduleInt objSchedleInt = new BEClient.ScheduleInt();
                        objSchedleInt = objScheduleAction.GetSchIntByAppAndRndTypeId(ApplicationId, LstRndTypeId);

                        if (objSchedleInt != null && objSchedleInt.ScheduleIntId != Guid.Empty)
                        {
                            BEClient.GetScore objGetScore = new BEClient.GetScore();
                            BEClient.VacancyRound ObjVacRnd = new BEClient.VacancyRound();
                            BLClient.GetScoreAction _objScoreAction = new BLClient.GetScoreAction(_CurrentClientId);
                            BLClient.VacancyRoundAction _objVacRndAction = new BLClient.VacancyRoundAction(_CurrentClientId);
                            objGetScore = _objScoreAction.GetTotalScoreByScheduleAndVacRnd(objSchedleInt.ScheduleIntId, objSchedleInt.VacRndId);
                            string strscore = string.Format("{0}", objGetScore.Score);
                            double RndScore = Convert.ToDouble(strscore);
                            ObjVacRnd = _objVacRndAction.GetRoundConfigByRoundId(objSchedleInt.VacRndId, _CurrentLanguageId);
                            if (ObjVacRnd.PromoteCandidate && RndScore >= ObjVacRnd.PromotionThresoldScore)
                            {
                                Common.CommonFunctions.ActiveNextRound(ApplicationId);
                            }
                        }
                    }
                }

                if (!String.IsNullOrEmpty(Status))
                {
                    BLClient.VacancyApplicantAction ObjVacancyApplicantAction = new BLClient.VacancyApplicantAction(_CurrentClientId);
                    List<BEClient.VacancyApplicant> ListAppStatus = new List<BEClient.VacancyApplicant>();
                    ListAppStatus = ObjVacancyApplicantAction.GetApplicationStausListAndCountForCandidate(Common.CurrentSession.Instance.VerifiedUser.UserId);
                    ViewBag.ListAppStatus = new SelectList(ListAppStatus, "TotalCount", "ApplicantionStatus");
                    BEClient.VacancyApplicant objSeletedStatus = ListAppStatus.Where(x => x.ApplicantionStatus == Status).ToList()[0];
                    ViewBag.SelectedStatus = objSeletedStatus.ApplicantionStatus;
                    ViewBag.TotalCount = objSeletedStatus.TotalCount;
                }
                #region RECENT VIEWED
                BEClient.RecentlyViewed objRecentlView = new BEClient.RecentlyViewed();
                BLClient.RecentlyViewedAction objRecentlyViewedAction = new BLClient.RecentlyViewedAction(_CurrentClientId);
                objRecentlView.Category = BEClient.RecentlyViewedCategory.Application.ToString();
                objRecentlView.ViewItemId = (Guid)ApplicationId;
                objRecentlView.URL = HttpContext.Request.Url.ToString();

                List<BEClient.RecentlyViewed> objList = objRecentlyViewedAction.GetRecentlyViewedApplication(objRecentlView);
                _objBreadScrumbCandidateApplications.objRecentViewedList = new List<BEClient.RecentlyViewed>();
                _objBreadScrumbCandidateApplications.objRecentViewedList = objList;
                #endregion

                return View(_jobAppWithQue);
            }
            catch
            {
                throw;
            }
        }

        public string GetRndTypeIdByCandidateSelfEvalution()
        {
            BLClient.RndTypeAction _objRndTypeAction = new BLClient.RndTypeAction(_CurrentClientId);
            List<Guid> lstRndType = _objRndTypeAction.GetRndTypeIdByCandidateSelfEvalution();
            String lstRndTypeStr = String.Join(",", lstRndType.Select(g => g.ToString()).ToArray());
            return lstRndTypeStr;
        }

        public ActionResult DownloadResume(Guid ResumeId)
        {
            try
            {
                BEClient.ATSResume objAtsresume = new BEClient.ATSResume();
                BLClient.ATSResumeAction objAtsResumeAction = new BLClient.ATSResumeAction(_CurrentClientId);
                objAtsresume = objAtsResumeAction.GetRecordByRecorId(ResumeId);
                if (objAtsresume == null)
                {
                    throw new Exception("File not Found");
                }
                else
                {
                    byte[] template_file = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(WebUi.Common.Constants.STR_RESUME_PATH), objAtsresume.NewFileName));
                    var Resumepath = Path.Combine(Server.MapPath(WebUi.Common.Constants.STR_RESUME_PATH), objAtsresume.NewFileName);
                    return new FileContentResult(template_file, "application / octet - stream") { FileDownloadName = objAtsresume.UploadedFileName };
                }
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                Guid ApplicationId = Guid.Empty;
                return RedirectToAction(ATS.WebUi.Common.Constants.STR_LIST_METHOD, new { ApplicationId = ApplicationId, KeyMsg = keyMsg });
            }
        }

        public ActionResult DownloadCoverLetter(Guid CoverLetterId)
        {
            try
            {
                BEClient.ATSResume objAtsresume = new BEClient.ATSResume();
                BLClient.ATSResumeAction objAtsResumeAction = new BLClient.ATSResumeAction(_CurrentClientId);
                objAtsresume = objAtsResumeAction.GetRecordByRecorId(CoverLetterId);
                if (objAtsresume == null)
                {
                    throw new Exception("File not Found");
                }
                else
                {
                    byte[] template_file = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(WebUi.Common.Constants.STR_RESUME_PATH), objAtsresume.NewFileName));
                    var Resumepath = Path.Combine(Server.MapPath(WebUi.Common.Constants.STR_RESUME_PATH), objAtsresume.NewFileName);
                    return new FileContentResult(template_file, "application / octet - stream") { FileDownloadName = objAtsresume.UploadedFileName };
                }
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                return RedirectToAction(ATS.WebUi.Common.Constants.STR_LIST_METHOD, new { KeyMsg = keyMsg });
            }
        }

        #region Bind Question
        public List<ATS.WebUi.Models.Question.ATSPickList> GetATSPickList(List<BEClient.QuestionDetails> objQuestion)
        {
            if (objQuestion != null && objQuestion.Count() > 0)
            {
                List<ATS.WebUi.Models.Question.ATSPickList> _ListATSPickList = new List<WebUi.Models.Question.ATSPickList>();
                foreach (BEClient.QuestionDetails _current in objQuestion)
                {
                    ATS.WebUi.Models.Question.ATSPickList _DrpATSPickList = new WebUi.Models.Question.ATSPickList();
                    BLClient.AnsOptAction _objDdlAnsOptAction = new BLClient.AnsOptAction(_CurrentClientId);
                    List<BEClient.AnsOpt> _objDdlAnsList = _objDdlAnsOptAction.GetAnsListByQueId(_current.Question.QueId, _CurrentLanguageId);
                    _DrpATSPickList.QuestionText = _current.Question.LocalName;
                    SelectList newlist = new SelectList(_objDdlAnsList, "AnsOptId", "DefaultName");
                    _DrpATSPickList.AllValues = newlist;
                    _DrpATSPickList.VacQueId = _current.VacQue.VacQueId;
                    _DrpATSPickList.VacRndId = _current.VacRnd.VacancyRoundId;

                    _ListATSPickList.Add(_DrpATSPickList);
                }
                return _ListATSPickList;
            }
            else
            {
                return null;
            }
        }
        public List<ATS.WebUi.Models.Question.ATSCheckBox> GetATSCheckList(List<BEClient.QuestionDetails> objQuestion)
        {
            if (objQuestion != null && objQuestion.Count() > 0)
            {
                List<ATS.WebUi.Models.Question.ATSCheckBox> _ListATSCheckBox = new List<WebUi.Models.Question.ATSCheckBox>();
                foreach (BEClient.QuestionDetails _current in objQuestion)
                {
                    ATS.WebUi.Models.Question.ATSCheckBox _DrpATSCheckBox = new WebUi.Models.Question.ATSCheckBox();
                    BLClient.AnsOptAction _objDdlAnsOptAction = new BLClient.AnsOptAction(_CurrentClientId);
                    List<BEClient.AnsOpt> _objDdlAnsList = _objDdlAnsOptAction.GetAnsListByQueId(_current.Question.QueId, _CurrentLanguageId);
                    _DrpATSCheckBox.QuestionText = _current.Question.LocalName;
                    _DrpATSCheckBox.AllValues = new SelectList(_objDdlAnsList, "AnsOptId", "DefaultName");
                    _DrpATSCheckBox.VacQueId = _current.VacQue.VacQueId;
                    _DrpATSCheckBox.VacRndId = _current.VacRnd.VacancyRoundId;
                    _ListATSCheckBox.Add(_DrpATSCheckBox);
                }
                return _ListATSCheckBox;
            }
            else
            {
                return null;
            }
        }
        public List<ATS.WebUi.Models.Question.ATSTextArea> GetATSTextArea(List<BEClient.QuestionDetails> objQuestion)
        {
            if (objQuestion != null && objQuestion.Count() > 0)
            {
                List<ATS.WebUi.Models.Question.ATSTextArea> _ListATSTextArea = new List<WebUi.Models.Question.ATSTextArea>();
                foreach (BEClient.QuestionDetails _current in objQuestion)
                {
                    ATS.WebUi.Models.Question.ATSTextArea _DrpATSTextArea = new WebUi.Models.Question.ATSTextArea();
                    _DrpATSTextArea.QuestionText = _current.Question.LocalName;
                    _DrpATSTextArea.VacQueId = _current.VacQue.VacQueId;
                    _DrpATSTextArea.VacRndId = _current.VacRnd.VacancyRoundId;
                    _ListATSTextArea.Add(_DrpATSTextArea);
                }
                return _ListATSTextArea;
            }
            else
            {
                return null;
            }
        }
        public List<ATS.WebUi.Models.Question.ATSTextBox> GetATSTextBox(List<BEClient.QuestionDetails> objQuestion)
        {
            if (objQuestion != null && objQuestion.Count() > 0)
            {
                List<ATS.WebUi.Models.Question.ATSTextBox> _ListATSTextBox = new List<WebUi.Models.Question.ATSTextBox>();
                foreach (BEClient.QuestionDetails _current in objQuestion)
                {
                    ATS.WebUi.Models.Question.ATSTextBox _DrpATSTextBox = new WebUi.Models.Question.ATSTextBox();
                    _DrpATSTextBox.QuestionText = _current.Question.LocalName;
                    _DrpATSTextBox.VacQueId = _current.VacQue.VacQueId;
                    _DrpATSTextBox.VacRndId = _current.VacRnd.VacancyRoundId;
                    _ListATSTextBox.Add(_DrpATSTextBox);
                }
                return _ListATSTextBox;
            }
            else
            {
                return null;
            }
        }
        public List<ATS.WebUi.Models.Question.ATSYesNo> GetATSYesNo(List<BEClient.QuestionDetails> objQuestion)
        {
            if (objQuestion != null && objQuestion.Count() > 0)
            {
                List<ATS.WebUi.Models.Question.ATSYesNo> _ListATSYesNo = new List<WebUi.Models.Question.ATSYesNo>();
                foreach (BEClient.QuestionDetails _current in objQuestion)
                {
                    ATS.WebUi.Models.Question.ATSYesNo _DrpyesNoQue = new WebUi.Models.Question.ATSYesNo();
                    BLClient.AnsOptAction _objDdlAnsOptAction = new BLClient.AnsOptAction(_CurrentClientId);
                    List<BEClient.AnsOpt> _objDdlAnsList = _objDdlAnsOptAction.GetAnsListByQueId(_current.Question.QueId, _CurrentLanguageId);
                    _DrpyesNoQue.QuestionText = _current.Question.LocalName;
                    SelectList newlist = new SelectList(_objDdlAnsList, "AnsOptId", "DefaultName");
                    _DrpyesNoQue.AllValues = newlist;
                    _DrpyesNoQue.VacQueId = _current.VacQue.VacQueId;
                    _DrpyesNoQue.VacRndId = _current.VacRnd.VacancyRoundId;
                    _ListATSYesNo.Add(_DrpyesNoQue);
                }
                return _ListATSYesNo;
            }
            else
            {
                return null;
            }
        }
        public List<ATS.WebUi.Models.Question.ATSScale> GetATSScale(List<BEClient.QuestionDetails> objQuestion)
        {
            if (objQuestion != null && objQuestion.Count() > 0)
            {
                List<ATS.WebUi.Models.Question.ATSScale> _ListATSTScale = new List<WebUi.Models.Question.ATSScale>();
                foreach (BEClient.QuestionDetails _current in objQuestion)
                {
                    ATS.WebUi.Models.Question.ATSScale _DrpATSScale = new WebUi.Models.Question.ATSScale();
                    _DrpATSScale.QuestionText = _current.Question.LocalName;
                    _DrpATSScale.VacQueId = _current.VacQue.VacQueId;
                    _DrpATSScale.VacRndId = _current.VacRnd.VacancyRoundId;
                    _ListATSTScale.Add(_DrpATSScale);
                }
                return _ListATSTScale;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Bind Question Answer
        private List<ATS.WebUi.Models.Question.ATSPickList> GetSubmitPickList(List<BEClient.AppQueAns> objQuestion)
        {
            if (objQuestion != null && objQuestion.Count() > 0)
            {
                List<ATS.WebUi.Models.Question.ATSPickList> _ListATSPickList = new List<WebUi.Models.Question.ATSPickList>();
                foreach (BEClient.AppQueAns _current in objQuestion)
                {
                    ATS.WebUi.Models.Question.ATSPickList _DrpATSPickList = new WebUi.Models.Question.ATSPickList();
                    BLClient.AnsOptAction ObjAnsOptAction = new BLClient.AnsOptAction(_CurrentClientId);
                    //TODO:CHECK
                    List<BEClient.AnsOpt> objAnsOpt = ObjAnsOptAction.GetAnsListByAppAnsId(_current.AppAnswer.Answer.ToString(), _CurrentLanguageId);
                    _DrpATSPickList.QuestionText = _current.Question.LocalName;
                    _DrpATSPickList.QueCatId = _current.Question.QueCatId;
                    _DrpATSPickList.Answer = objAnsOpt[0].LocalName;
                    _ListATSPickList.Add(_DrpATSPickList);
                }
                return _ListATSPickList;
            }
            else
            {
                return null;
            }
        }
        private List<ATS.WebUi.Models.Question.ATSCheckBox> GetSubmitCheckList(List<BEClient.AppQueAns> objQuestion)
        {
            if (objQuestion != null && objQuestion.Count() > 0)
            {
                List<ATS.WebUi.Models.Question.ATSCheckBox> _ListATSCheckList = new List<WebUi.Models.Question.ATSCheckBox>();
                foreach (BEClient.AppQueAns _current in objQuestion)
                {
                    ATS.WebUi.Models.Question.ATSCheckBox _DrpATSCheckList = new WebUi.Models.Question.ATSCheckBox();
                    BLClient.AnsOptAction ObjAnsOptAction = new BLClient.AnsOptAction(_CurrentClientId);

                    //TODO:CHECK
                    List<BEClient.AnsOpt> objAnsOptList = ObjAnsOptAction.GetAnsListByAppAnsId(_current.AppAnswer.Answer.ToString(), _CurrentLanguageId);
                    _DrpATSCheckList.QuestionText = _current.Question.LocalName;
                    _DrpATSCheckList.QueCatId = _current.Question.QueCatId;
                    _DrpATSCheckList.AllValues = new SelectList(objAnsOptList, "LocalName", "LocalName");
                    _ListATSCheckList.Add(_DrpATSCheckList);
                }
                return _ListATSCheckList;
            }
            else
            {
                return null;
            }
        }
        private List<ATS.WebUi.Models.Question.ATSTextArea> GetSubmitTextArea(List<BEClient.AppQueAns> objQuestion)
        {
            if (objQuestion != null && objQuestion.Count() > 0)
            {
                List<ATS.WebUi.Models.Question.ATSTextArea> _ListATSTextArea = new List<WebUi.Models.Question.ATSTextArea>();
                foreach (BEClient.AppQueAns _current in objQuestion)
                {
                    //TODO:CHECK
                    ATS.WebUi.Models.Question.ATSTextArea _DrpATSTextArea = new WebUi.Models.Question.ATSTextArea();
                    _DrpATSTextArea.QuestionText = _current.Question.LocalName;
                    _DrpATSTextArea.QueCatId = _current.Question.QueCatId;
                    _DrpATSTextArea.Answer = _current.AppAnswer.Answer;
                    _ListATSTextArea.Add(_DrpATSTextArea);
                }
                return _ListATSTextArea;
            }
            else
            {
                return null;
            }
        }
        private List<ATS.WebUi.Models.Question.ATSTextBox> GetSubmitTextBox(List<BEClient.AppQueAns> objQuestion)
        {
            if (objQuestion != null && objQuestion.Count() > 0)
            {
                List<ATS.WebUi.Models.Question.ATSTextBox> _ListATSTextBox = new List<WebUi.Models.Question.ATSTextBox>();
                foreach (BEClient.AppQueAns _current in objQuestion)
                {
                    //TODO:CHECK
                    ATS.WebUi.Models.Question.ATSTextBox _DrpATSTextBox = new WebUi.Models.Question.ATSTextBox();
                    _DrpATSTextBox.QuestionText = _current.Question.LocalName;
                    _DrpATSTextBox.QueCatId = _current.Question.QueCatId;
                    _DrpATSTextBox.Answer = _current.AppAnswer.Answer;
                    _ListATSTextBox.Add(_DrpATSTextBox);
                }
                return _ListATSTextBox;
            }
            else
            {
                return null;
            }
        }
        private List<ATS.WebUi.Models.Question.ATSYesNo> GetSubmitYesNo(List<BEClient.AppQueAns> objQuestion)
        {
            if (objQuestion != null && objQuestion.Count() > 0)
            {
                List<ATS.WebUi.Models.Question.ATSYesNo> _ListATSYesNo = new List<WebUi.Models.Question.ATSYesNo>();
                foreach (BEClient.AppQueAns _current in objQuestion)
                {
                    ATS.WebUi.Models.Question.ATSYesNo _DrpATSYesNo = new WebUi.Models.Question.ATSYesNo();
                    BLClient.AnsOptAction ObjAnsOptAction = new BLClient.AnsOptAction(_CurrentClientId);

                    //TODO:CHECK
                    List<BEClient.AnsOpt> objAnsOpt = ObjAnsOptAction.GetAnsListByAppAnsId(_current.AppAnswer.Answer.ToString(), _CurrentLanguageId);
                    _DrpATSYesNo.QuestionText = _current.Question.LocalName;
                    _DrpATSYesNo.QueCatId = _current.Question.QueCatId;
                    _DrpATSYesNo.Answer = objAnsOpt[0].LocalName;
                    _ListATSYesNo.Add(_DrpATSYesNo);
                }
                return _ListATSYesNo;
            }
            else
            {
                return null;
            }
        }
        private List<ATS.WebUi.Models.Question.ATSScale> GetSubmitScale(List<BEClient.AppQueAns> objQuestion)
        {
            if (objQuestion != null && objQuestion.Count() > 0)
            {
                List<ATS.WebUi.Models.Question.ATSScale> _ListATSScale = new List<WebUi.Models.Question.ATSScale>();
                foreach (BEClient.AppQueAns _current in objQuestion)
                {
                    ATS.WebUi.Models.Question.ATSScale _DrpATSScale = new WebUi.Models.Question.ATSScale();
                    _DrpATSScale.QuestionText = _current.Question.LocalName;
                    _DrpATSScale.QueCatId = _current.Question.QueCatId;
                    _DrpATSScale.Answer = _current.AppAnswer.Answer == "" ? 0 : Convert.ToInt32(_current.AppAnswer.Answer);
                    _ListATSScale.Add(_DrpATSScale);
                }
                return _ListATSScale;
            }
            else
            {
                return null;
            }
        }

        #endregion Bind Question Answer

        #region Insert Application Round
        public void InsertAppRnd(WebUi.Areas.Candidate.Models.IQuestions objanswer, Guid ApplicationId, bool NeedToPromote = true)
        {
            try
            {
                InsertAppAnwers(objanswer, ApplicationId, NeedToPromote);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Insert Application Answers
        private Guid InsertIfScheduleNot(Guid ApplicationId, Guid VacRndId, bool NeedToPromote = true)
        {
            //Update Promote Candidate
            BLClient.PromoteCandidateAction PCAction = new BLClient.PromoteCandidateAction(_CurrentClientId);
            BEClient.PromoteCandidate ObjPC = new BEClient.PromoteCandidate();
            ObjPC.ApplicationId = ApplicationId;
            ObjPC.VacRndId = VacRndId;
            ObjPC.VacRndId = VacRndId;
            if (NeedToPromote)//If save for Later then no need to promote in any round.
            {
                Boolean Result = PCAction.UpdatePromoteCandidate(ObjPC, null);
            }

            //Check is Schedule exists by vacid ,vacRndId,ApplicationId
            //if  not then add New
            //If added get existing Schedulingid
            BLClient.ScheduleIntAction objScheduleIntAction = new BLClient.ScheduleIntAction(_CurrentClientId);
            BEClient.ScheduleInt ScheduleInt = new BEClient.ScheduleInt();
            ScheduleInt.ApplicationId = ApplicationId;
            ScheduleInt.VacRndId = VacRndId;

            Guid _ScheduleId = Guid.Empty;
            _ScheduleId = objScheduleIntAction.IsScheduled(ScheduleInt);
            if (_ScheduleId.Equals(Guid.Empty))
            {
                _ScheduleId = objScheduleIntAction.AddSaveScheduleInt(ScheduleInt, false);
            }
            return _ScheduleId;
        }

        public void InsertAppAnwers(WebUi.Areas.Candidate.Models.IQuestions objanswer, Guid ApplicationId, bool NeedToPromote = true)
        {
            BLClient.ReviewersAction _objReviewerAction = new BLClient.ReviewersAction(_CurrentClientId);
            BLClient.AnsOptAction _objAnsOptAction = new BLClient.AnsOptAction(_CurrentClientId);
            List<BEClient.Reviewers> _objReviewers = null;
            try
            {
                Guid _ScheduleIntId = Guid.Empty;
                BLClient.AppAnswerAction objAppAnswerAction = new BLClient.AppAnswerAction(_CurrentClientId);
                if (objanswer != null)
                {
                    BEClient.AppAnswer objAppAnswer = new BEClient.AppAnswer();
                    objAppAnswer.Comment = null;
                    if (objanswer.ListCheckList != null)
                    {
                        foreach (ATS.WebUi.Models.Question.ATSCheckBox _current in objanswer.ListCheckList)
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(_current.Answer)))
                            {
                                objAppAnswer.VacQueId = _current.VacQueId;
                                string Answer = string.Empty;
                                foreach (string item in (string[])_current.Answer)
                                {
                                    Answer += item + ",";
                                }
                                Answer = Answer.Substring(0, Answer.Length - 1);
                                objAppAnswer.Value = _objAnsOptAction.GetValueByAnsopt(Answer);
                                objAppAnswer.Answer = Answer;
                                if (_ScheduleIntId == Guid.Empty)
                                    _ScheduleIntId = InsertIfScheduleNot(ApplicationId, _current.VacRndId, NeedToPromote);
                                objAppAnswer.ScheduleIntId = _ScheduleIntId;
                                _objReviewers = _objReviewerAction.GetReviewersByVacRndId(_current.VacRndId);
                                if (_objReviewers.Count == 0)
                                {
                                    _objReviewers.Add(new BEClient.Reviewers());
                                    _objReviewers[0].TVacReviewMemberId = Guid.Empty;
                                }
                                foreach (BEClient.Reviewers rvwr in _objReviewers)
                                {
                                    objAppAnswer.VacRMId = rvwr.VacancyReviewMemberId;
                                    Guid Result = objAppAnswerAction.InsertAppAnswer(objAppAnswer);
                                }
                            }
                        }
                    }
                    if (objanswer.ListPickList != null)
                    {
                        foreach (ATS.WebUi.Models.Question.ATSPickList _current in objanswer.ListPickList)
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(_current.Answer)))
                            {
                                string[] AnsOptId = Array.ConvertAll<object, string>((object[])_current.Answer, Convert.ToString);
                                objAppAnswer.VacQueId = _current.VacQueId;
                                objAppAnswer.Answer = AnsOptId[0];
                                objAppAnswer.Value = _objAnsOptAction.GetValueByAnsopt(objAppAnswer.Answer.ToString());
                                if (_ScheduleIntId == Guid.Empty)
                                    _ScheduleIntId = InsertIfScheduleNot(ApplicationId, _current.VacRndId, NeedToPromote);
                                objAppAnswer.ScheduleIntId = _ScheduleIntId;
                                _objReviewers = _objReviewerAction.GetReviewersByVacRndId(_current.VacRndId);
                                if (_objReviewers.Count == 0)
                                {
                                    _objReviewers.Add(new BEClient.Reviewers());
                                    _objReviewers[0].TVacReviewMemberId = Guid.Empty;
                                }
                                foreach (BEClient.Reviewers rvwr in _objReviewers)
                                {
                                    objAppAnswer.VacRMId = rvwr.VacancyReviewMemberId;
                                    Guid Result = objAppAnswerAction.InsertAppAnswer(objAppAnswer);
                                }
                            }
                        }
                    }
                    if (objanswer.ListATSYesNo != null)
                    {
                        foreach (ATS.WebUi.Models.Question.ATSYesNo _current in objanswer.ListATSYesNo)
                        {
                            string[] AnsOptId = Array.ConvertAll<object, string>((object[])_current.Answer, Convert.ToString);
                            if (!string.IsNullOrEmpty(Convert.ToString(AnsOptId[0])))
                            {
                                objAppAnswer.VacQueId = _current.VacQueId;
                                objAppAnswer.Answer = AnsOptId[0];
                                objAppAnswer.Value = _objAnsOptAction.GetValueByAnsopt(objAppAnswer.Answer.ToString());
                                if (_ScheduleIntId == Guid.Empty)
                                    _ScheduleIntId = InsertIfScheduleNot(ApplicationId, _current.VacRndId, NeedToPromote);
                                objAppAnswer.ScheduleIntId = _ScheduleIntId;
                                _objReviewers = _objReviewerAction.GetReviewersByVacRndId(_current.VacRndId);
                                if (_objReviewers.Count == 0)
                                {
                                    _objReviewers.Add(new BEClient.Reviewers());
                                    _objReviewers[0].TVacReviewMemberId = Guid.Empty;
                                }
                                foreach (BEClient.Reviewers rvwr in _objReviewers)
                                {
                                    objAppAnswer.VacRMId = rvwr.VacancyReviewMemberId;
                                    Guid Result = objAppAnswerAction.InsertAppAnswer(objAppAnswer);
                                }
                            }
                        }
                    }
                    if (objanswer.ListATSTextBox != null)
                    {
                        foreach (ATS.WebUi.Models.Question.ATSTextBox _current in objanswer.ListATSTextBox)
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(_current.Answer)))
                            {
                                objAppAnswer.VacQueId = _current.VacQueId;
                                foreach (String s in (string[])_current.Answer)
                                {
                                    objAppAnswer.Answer = s;
                                }
                                objAppAnswer.Value = null;
                                if (_ScheduleIntId == Guid.Empty)
                                    _ScheduleIntId = InsertIfScheduleNot(ApplicationId, _current.VacRndId, NeedToPromote);
                                objAppAnswer.ScheduleIntId = _ScheduleIntId;
                                _objReviewers = _objReviewerAction.GetReviewersByVacRndId(_current.VacRndId);
                                if (_objReviewers.Count == 0)
                                {
                                    _objReviewers.Add(new BEClient.Reviewers());
                                    _objReviewers[0].TVacReviewMemberId = Guid.Empty;
                                }
                                foreach (BEClient.Reviewers rvwr in _objReviewers)
                                {
                                    objAppAnswer.VacRMId = rvwr.VacancyReviewMemberId;
                                    Guid Result = objAppAnswerAction.InsertAppAnswer(objAppAnswer);
                                }
                            }
                        }
                    }
                    if (objanswer.ListATSTextArea != null)
                    {
                        foreach (ATS.WebUi.Models.Question.ATSTextArea _current in objanswer.ListATSTextArea)
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(_current.Answer)))
                            {
                                objAppAnswer.VacQueId = _current.VacQueId;
                                foreach (String s in (string[])_current.Answer)
                                {
                                    objAppAnswer.Answer = s;
                                }
                                if (_ScheduleIntId == Guid.Empty)
                                    _ScheduleIntId = InsertIfScheduleNot(ApplicationId, _current.VacRndId, NeedToPromote);
                                objAppAnswer.ScheduleIntId = _ScheduleIntId;
                                objAppAnswer.Value = null;
                                _objReviewers = _objReviewerAction.GetReviewersByVacRndId(_current.VacRndId);
                                if (_objReviewers.Count == 0)
                                {
                                    _objReviewers.Add(new BEClient.Reviewers());
                                    _objReviewers[0].TVacReviewMemberId = Guid.Empty;
                                    objAppAnswer.Value = 100;
                                }
                                foreach (BEClient.Reviewers rvwr in _objReviewers)
                                {
                                    objAppAnswer.VacRMId = rvwr.VacancyReviewMemberId;
                                    Guid Result = objAppAnswerAction.InsertAppAnswer(objAppAnswer);
                                }
                            }
                        }
                    }
                    if (objanswer.ListATSScale != null)
                    {
                        foreach (ATS.WebUi.Models.Question.ATSScale _current in objanswer.ListATSScale)
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(_current.Answer)))
                            {
                                objAppAnswer.VacQueId = _current.VacQueId;

                                foreach (String s in (string[])_current.Answer)
                                {
                                    objAppAnswer.Answer = s;
                                }
                                if (_ScheduleIntId == Guid.Empty)
                                    _ScheduleIntId = InsertIfScheduleNot(ApplicationId, _current.VacRndId, NeedToPromote);
                                objAppAnswer.ScheduleIntId = _ScheduleIntId;
                                objAppAnswer.Value = null;
                                _objReviewers = _objReviewerAction.GetReviewersByVacRndId(_current.VacRndId);
                                if (_objReviewers.Count == 0)
                                {
                                    _objReviewers.Add(new BEClient.Reviewers());
                                    _objReviewers[0].TVacReviewMemberId = Guid.Empty;
                                    objAppAnswer.Value = Convert.ToInt32(objAppAnswer.Answer);
                                }
                                foreach (BEClient.Reviewers rvwr in _objReviewers)
                                {
                                    objAppAnswer.VacRMId = rvwr.VacancyReviewMemberId;
                                    Guid Result = objAppAnswerAction.InsertAppAnswer(objAppAnswer);
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        [HttpPost]
        public ActionResult SubmitApplication(ATS.WebUi.Areas.Candidate.Models.JobApplication<RootModels.BreadScrumbModel<BEClient.CandidateApplications>> objJobApplication, string submitButton)
        {
            try
            {
                Guid ApplicationId = objJobApplication.mainObject.obj.ApplicationId;
                BEClient.Application ObjAppState = GetAppStateByAppId(ApplicationId);



                if (ObjAppState.SaveForLater == true)
                {
                    string ScreeningRound = GetRndTypeIdByCandidateSelfEvalution();
                    BLClient.AppAnswerAction ObjAppAnswer = new BLClient.AppAnswerAction(_CurrentClientId);
                    bool Result = ObjAppAnswer.RemoveOldAnswers(objJobApplication.mainObject.obj.ApplicationId, ScreeningRound);
                }
                bool NeedToPromote = (submitButton == "SaveForLater" ? false : true);

                InsertAppRnd(objJobApplication, ApplicationId, NeedToPromote);
                BLClient.ApplicationAction _ObjApplicationAction = new BLClient.ApplicationAction(_CurrentClientId);

                if (submitButton != "SaveForLater")
                {
                    bool Result = _ObjApplicationAction.ChangeApplicationStatus(ApplicationId, BECommon.ApplicationStatusOptionsConstant.APP_STAT_SUBMIT);
                    bool Result1 = _ObjApplicationAction.UpdateSaveForLater(ApplicationId, false);
                    try
                    {
                        //To check whether vacancy has checked on send apply email or not -- Anand --Start ---//
                        BLClient.VacancyOfferAction ObjVacancyOfferAction = new BLClient.VacancyOfferAction(_CurrentClientId);
                        bool IsSendApplyEmail = ObjVacancyOfferAction.IsSendApplyEmail(ApplicationId);
                        // End //
                        if (IsSendApplyEmail)
                        {
                            //***** Send Apply Email to Candidate ***///////
                            Common.CommonFunctions.SendMailToCandidate();
                            ////*********** END Send Applly Email *****/////
                        }
                    }
                    catch (Exception)
                    { }
                    return RedirectToAction("SubmitApp", "ApplyVacancy", new { area = ATS.WebUi.Common.Constants.AREA_CANDIDATE, SubmitType = "SubmitApp" });
                }
                else
                {
                    bool Result = _ObjApplicationAction.UpdateSaveForLater(ApplicationId, true);
                    return RedirectToAction("SubmitApp", "ApplyVacancy", new { area = ATS.WebUi.Common.Constants.AREA_CANDIDATE, SubmitType = "SaveForLater" });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private BEClient.Application GetAppStateByAppId(Guid ApplicationId)
        {
            BLClient.ApplicationAction ObjAppAction = new BLClient.ApplicationAction(_CurrentClientId);
            return ObjAppAction.GetAppState(ApplicationId);
        }
    }
}