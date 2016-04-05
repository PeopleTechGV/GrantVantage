using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLClient = ATS.BusinessLogic;
using BEClient = ATS.BusinessEntity;
using ATS.WebUi.Areas.Candidate.Models;
using System.IO;
using ATS.WebUi.Models;
using Newtonsoft.Json;
using ATSCommon = ATS.WebUi.Common;
using BECommon = ATS.BusinessEntity.Common;
using System.Web.Script.Serialization;
using RootModels = ATS.WebUi.Models;

using BEApplicationOptionsConstant = ATS.BusinessEntity.Common.ApplicationStatusOptionsConstant;

namespace ATS.WebUi.Areas.Candidate.Controllers
{
    public class MyApplicationsController : ATS.WebUi.Controllers.AreaBaseController
    {
        private Guid _CurrentClientId;
        private Guid _CurrentLanguageId;
        private Guid _CurrentUserId;
        private BLClient.ATSResumeAction objATSResumeAction;

        #region OnAuthorization Method call on every request
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (base.CurrentClient != null && Common.CurrentSession.Instance.VerifiedUser != null)
            {
                _CurrentClientId = base.CurrentClient.ClientId;
                _CurrentLanguageId = base.CurrentClient.CurrentLanguageId;
                _CurrentUserId = WebUi.Common.CurrentSession.Instance.UserId;
                if (ATSCommon.CurrentSession.Instance.VerifiedUser.SecurityRoles.Where(x => x.AtsSecurityRole == BEClient.ATSSecurityRole.Candidate).Count() <= 0)
                {
                    filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_CANDIDATE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_CONTROLLER, new { area = ATSCommon.Constants.AREA_EMPLOYEE }));

                }
            }
        }
        #endregion

        public void NavIndex()
        {
            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = null;
            List<BEClient.BreadCrumb> objBreadCrumblst = new List<BEClient.BreadCrumb>();
            BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
            objBreadCrumb.Action = "Index";
            objBreadCrumb.Controller = "MyApplications";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_CANDIDATE;
            objBreadCrumb.URL = Url.Action("Index", "MyApplications", new { area = ATS.WebUi.Common.Constants.AREA_CANDIDATE });
            objBreadCrumb.ordinal = 1;
            objBreadCrumb.ImagePath = BECommon.ImagePaths.MyApplicationsImage;
            objBreadCrumb.ToolTip = "My Applications";
            objBreadCrumb.WithoutOrdinalURL = Url.Action("Index", "MyApplications", new { area = ATS.WebUi.Common.Constants.AREA_CANDIDATE });
            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);
            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
        }

        public void NavApply()
        {
            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = null;
            List<BEClient.BreadCrumb> objBreadCrumblst = new List<BEClient.BreadCrumb>();
            BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
            objBreadCrumb.Action = "Index";
            objBreadCrumb.Controller = "Home";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_CANDIDATE;
            objBreadCrumb.URL = Url.Action("Index", "Home", new { area = ATS.WebUi.Common.Constants.AREA_CANDIDATE });
            objBreadCrumb.ordinal = 1;
            objBreadCrumb.ImagePath = BECommon.ImagePaths.HomeImage;
            objBreadCrumb.ToolTip = "Home";
            objBreadCrumb.WithoutOrdinalURL = Url.Action("Index", "Home", new { area = ATS.WebUi.Common.Constants.AREA_CANDIDATE });
            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);
            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
        }

        public ActionResult Index(string KeyMsg, string Status = "All")
        {
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
            ViewBag.IsView = true;
            RootModels.BreadScrumbModel<List<BEClient.CandidateApplications>> _objBreadScrumbModel = new RootModels.BreadScrumbModel<List<BEClient.CandidateApplications>>();
            string LstRndTypeId = GetRndTypeIdByCandidateSelfEvalution();

            BLClient.CandidateApplicationsAction _objCandidateApplicationAction = new BLClient.CandidateApplicationsAction(_CurrentClientId);
            _objBreadScrumbModel.obj = _objCandidateApplicationAction.GetAllCandidateApplications(_CurrentUserId, _CurrentLanguageId, LstRndTypeId, Status);

            if (_objBreadScrumbModel.obj.Count != 0 && _objBreadScrumbModel.obj != null)
            {
                List<BEClient.CandidateApplications> objReqDocCountList = new List<BEClient.CandidateApplications>();
                objReqDocCountList = _objCandidateApplicationAction.GetRequiredDocumentsCount(_CurrentUserId);
             
                foreach (var Application in _objBreadScrumbModel.obj)
                {
                    int Answered = 0, Count = 0;
                    int Perc = 100;
                    _objCandidateApplicationAction = new BLClient.CandidateApplicationsAction(_CurrentClientId);
                    int candidateSurveyCnt = _objCandidateApplicationAction.GetCandidateSurveyQueCount(Application.ApplicationId);
                    Application.CanSurveyQueCnt = candidateSurveyCnt;

                    var objReqDocCount = objReqDocCountList.Where(m => m.objVacancy.VacancyId == Application.objVacancy.VacancyId).FirstOrDefault();
                    if (objReqDocCount != null)
                    {
                        Application.ReqDocCount = objReqDocCount.ReqDocCount;
                    }
                  //CR-7
                    BLClient.AppQueAnsAction _AppQueAnsAction = new BLClient.AppQueAnsAction(_CurrentClientId);
                    List<BEClient.AppQueAns> _objAppQueAns = _AppQueAnsAction.GetAppQueByApplicationId(Application.ApplicationId, LstRndTypeId);
                    Application.TotalAppQues = _objAppQueAns.Count;
                    JobApplication<RootModels.BreadScrumbModel<BEClient.CandidateApplications>> _jobAppWithQue = new JobApplication<BreadScrumbModel<BEClient.CandidateApplications>>();
                    if (_objAppQueAns.Count == 0)
                       
                    _jobAppWithQue.ListPickList = GetSubmitPickList(_objAppQueAns.Where(x => x.Question.QueDataType.Equals(1)).ToList());
                    _jobAppWithQue.ListCheckList = GetSubmitCheckList(_objAppQueAns.Where(x => x.Question.QueDataType.Equals(2)).ToList());
                    _jobAppWithQue.ListATSTextArea = GetSubmitTextArea(_objAppQueAns.Where(x => x.Question.QueDataType.Equals(3)).ToList());
                    _jobAppWithQue.ListATSTextBox = GetSubmitTextBox(_objAppQueAns.Where(x => x.Question.QueDataType.Equals(4)).ToList());
                    _jobAppWithQue.ListATSYesNo = GetSubmitYesNo(_objAppQueAns.Where(x => x.Question.QueDataType.Equals(5)).ToList());
                    _jobAppWithQue.ListATSScale = GetSubmitScale(_objAppQueAns.Where(x => x.Question.QueDataType.Equals(6)).ToList());
                    _jobAppWithQue.ATSQueCommon = new RootModels.Question.ATSQuestionCommon();
                    _jobAppWithQue.ATSQueCommon.QueCatList = _objAppQueAns.Select(x => new Tuple<Guid, string>(x.Question.QueCatId, x.Question.QueCatLocalName)).Distinct().ToArray().ToList();
                    
                    foreach (var CurrentCat in _jobAppWithQue.ATSQueCommon.QueCatList)
                    {
                        if (_jobAppWithQue.ListATSScale == null)
                        {
                            Count = Count;

                        }
                        else
                        {
                            Count = Count + _jobAppWithQue.ListATSScale.Count(m => m.QueCatId == CurrentCat.Item1);
                        }
                        if (_jobAppWithQue.ListATSTextArea == null)
                        {
                            Count = Count;

                        }
                        else
                        {
                            Count = Count + _jobAppWithQue.ListATSTextArea.Count(m => m.QueCatId == CurrentCat.Item1);
                        }
                       // Count = _jobAppWithQue.ListATSScale == null ? Count : Count + _jobAppWithQue.ListATSScale.Count(m => m.QueCatId == CurrentCat.Item1);
                       // Count = _jobAppWithQue.ListATSTextArea == null ? Count : Count + _jobAppWithQue.ListATSTextArea.Count(m => m.QueCatId == CurrentCat.Item1);
                        Count = _jobAppWithQue.ListATSTextBox == null ? Count : Count + _jobAppWithQue.ListATSTextBox.Count(m => m.QueCatId == CurrentCat.Item1);
                        Count = _jobAppWithQue.ListATSYesNo == null ? Count : Count + _jobAppWithQue.ListATSYesNo.Count(m => m.QueCatId == CurrentCat.Item1);
                        Count = _jobAppWithQue.ListCheckList == null ? Count : Count + _jobAppWithQue.ListCheckList.Count(m => m.QueCatId == CurrentCat.Item1);
                        Count = _jobAppWithQue.ListPickList == null ? Count : Count + _jobAppWithQue.ListPickList.Count(m => m.QueCatId == CurrentCat.Item1);

                        Answered = _jobAppWithQue.ListATSScale == null ? Answered : Answered + _jobAppWithQue.ListATSScale.Count(m => m.QueCatId == CurrentCat.Item1 && m.Answer != null && m.Answer.ToString() != "0");
                        Answered = _jobAppWithQue.ListATSTextArea == null ? Answered : Answered + _jobAppWithQue.ListATSTextArea.Count(m => m.QueCatId == CurrentCat.Item1 && m.Answer != null && !string.IsNullOrEmpty(m.Answer.ToString()));
                        Answered = _jobAppWithQue.ListATSTextBox == null ? Answered : Answered + _jobAppWithQue.ListATSTextBox.Count(m => m.QueCatId == CurrentCat.Item1 && m.Answer != null && !string.IsNullOrEmpty(m.Answer.ToString()));
                        Answered = _jobAppWithQue.ListATSYesNo == null ? Answered : Answered + _jobAppWithQue.ListATSYesNo.Count(m => m.QueCatId == CurrentCat.Item1 && m.Answer != null && !string.IsNullOrEmpty(m.Answer.ToString()));
                        Answered = _jobAppWithQue.ListPickList == null ? Answered : Answered + _jobAppWithQue.ListPickList.Count(m => m.QueCatId == CurrentCat.Item1 && m.Answer != null && !string.IsNullOrEmpty(m.Answer.ToString()));
                        Answered = _jobAppWithQue.ListCheckList == null ? Answered : Answered + _jobAppWithQue.ListCheckList.Count(m => m.QueCatId == CurrentCat.Item1 && m.SelectedList != null && m.SelectedList.Count() != 0);
                        Answered = _jobAppWithQue.ListCheckList == null ? Answered : Answered + _jobAppWithQue.ListCheckList.Count(m => m.QueCatId == CurrentCat.Item1 && m.AllValues.Count() != 0);

                       
                    }
                    Perc = (Answered * 100) / _objAppQueAns.Count;
                    Application.Questioncounterperc = Perc.ToString();
                   
                }
               
            }

            NavIndex();
            BLClient.VacancyApplicantAction ObjVacancyApplicantAction = new BLClient.VacancyApplicantAction(_CurrentClientId);
            List<BEClient.VacancyApplicant> ListAppStatus = new List<BEClient.VacancyApplicant>();
            ListAppStatus = ObjVacancyApplicantAction.GetApplicationStausListAndCountForCandidate(Common.CurrentSession.Instance.VerifiedUser.UserId);
            ViewBag.ListAppStatus = new SelectList(ListAppStatus, "TotalCount", "ApplicantionStatus");
            BEClient.VacancyApplicant objSeletedStatus = ListAppStatus.Where(x => x.ApplicantionStatus == Status).ToList()[0];
            ViewBag.SelectedStatus = objSeletedStatus.ApplicantionStatus;
            _objBreadScrumbModel.DisplayName = ATSCommon.CommonFunctions.LanguageLabel(ATS.BusinessEntity.Common.ModelConstant.MDL_MYAPPLICATIONS) + " (" + _objBreadScrumbModel.obj.Count() + ")";
            _objBreadScrumbModel.ImagePath = BECommon.ImagePaths.MyApplicationsImage;
            _objBreadScrumbModel.ToolTip = "My Applications";
            return View(_objBreadScrumbModel);
        }

        [HttpPost]
        public ActionResult ApplyJob(ATS.WebUi.Models.BreadScrumbModel<ResumeList> objResumeLst, string VacancyId, string ControllerName)
        {
            try
            {
                BEClient.Application objApplication = new BEClient.Application();
                objApplication.VacancyId = new Guid(VacancyId);
                objApplication.IsDelete = false;
                objApplication.LanguageId = _CurrentLanguageId;
                objApplication.ApplicationStatus = BEClient.Common.ApplicationStatusOptionsConstant.APP_STAT_DRAFT.ToString();
                objApplication.ATSCoverLetterId = objResumeLst.obj.objATSCoverLetter.ATSResumeId;
                objApplication.ATSResumeId = objResumeLst.obj.objATSResume.ATSResumeId;
                BLClient.ApplicationAction objApplicationAction = new BLClient.ApplicationAction(_CurrentClientId);
                Guid ApplicationId = objApplicationAction.InsertApplication(objApplication);

                //Rutul Patel : Get All Vac Rnd and Insert into Promote Candidate
                BLClient.VacancyRoundAction VacRndAction = new BLClient.VacancyRoundAction(_CurrentClientId);
                List<BEClient.VacancyRound> VacRndList = VacRndAction.GetAllRoundByApplicationId(ApplicationId);
                BLClient.PromoteCandidateAction PCAction = new BLClient.PromoteCandidateAction(_CurrentClientId);
                BEClient.PromoteCandidate ObjPC = new BEClient.PromoteCandidate();
                ObjPC.ApplicationId = ApplicationId;
                ObjPC.IsActive = false;
                ObjPC.IsPromoted = false;
                foreach (BEClient.VacancyRound _current in VacRndList)
                {
                    ObjPC.VacRndId = _current.VacancyRoundId;
                    Guid Result = PCAction.InsertPromoteCandidate(ObjPC);
                }

                // Rutul Patel: 28 Oct 2014, If vacancy do not have screen question then application status would be Applied directly.
                string LstRndTypeQTSelf = GetRndTypeIdByCandidateSelfEvalution();
                BLClient.QuestionDetailAction _objQuestionDetailAction = new BLClient.QuestionDetailAction(_CurrentClientId);
                List<BEClient.QuestionDetails> _objQuestionDetails = _objQuestionDetailAction.GetQuestionDetails(ApplicationId, LstRndTypeQTSelf);
                if (_objQuestionDetails.Count == 0)
                {
                    string RndType = GetRndTypeIdByCandidateSelfEvalution();
                    BEClient.VacancyRound ObjVacRnd = VacRndAction.GetVacRndByAppIdAndRndTypeId(ApplicationId, RndType);
                    if (ObjVacRnd != null)
                    {
                        Guid VacRndId = ObjVacRnd.VacancyRoundId;
                        InsertScheduleInt(ApplicationId, VacRndId);
                        UpdatePromoteCandidateForActive(ApplicationId, VacRndId);
                    }
                    else
                    {
                        if (VacRndList.Count > 0)
                        {
                            ActiveNextRound(ApplicationId);
                        }
                    }
                    BLClient.ApplicationAction _ObjApplicationAction = new BLClient.ApplicationAction(_CurrentClientId);
                    bool Result = _ObjApplicationAction.ChangeApplicationStatus(ApplicationId, BECommon.ApplicationStatusOptionsConstant.APP_STAT_SUBMIT);
                }
                return RedirectToAction("Index", "JobApplication", new { ApplicationId = ApplicationId, ControllerName = ControllerName });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult WithdrawApplication(Guid ApplicationId)
        {
            try
            {
                JsonModels actionStatus = null;
                if (ApplicationId != null && ApplicationId != Guid.Empty)
                {
                    BLClient.ApplicationAction _ObjApplicationAction = new BLClient.ApplicationAction(_CurrentClientId);
                    bool result = _ObjApplicationAction.DeleteApplication(ApplicationId);

                    if (result)
                    {
                        actionStatus = base.GetJsonContent(false, string.Empty, "Application Withdrawn Successfully ");
                    }
                    else
                    {
                        actionStatus = base.GetJsonContent(true, string.Empty, "Not Able To Withdraw Application");
                    }
                    string jsonModels = JsonConvert.SerializeObject(actionStatus);
                    string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                    return RedirectToAction("Index", new { KeyMsg = keyMsg });
                }
                else
                {
                    actionStatus = base.GetJsonContent(true, string.Empty, "Not Able To Withdraw Application");
                    string jsonModels = JsonConvert.SerializeObject(actionStatus);
                    string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                    return RedirectToAction("Index", new { KeyMsg = keyMsg });
                }
            }
            catch
            {
                throw;
            }
        }

        public ActionResult ChangeApplicationStatus(Guid ApplicationId, string ApplicationStatus)
        {
            JsonModels actionStatus = null;
            try
            {
                BLClient.ApplicationAction _ObjApplicationAction = new BLClient.ApplicationAction(_CurrentClientId);
                bool Result = _ObjApplicationAction.ChangeApplicationStatus(ApplicationId, ApplicationStatus);

                if (Result)
                {
                    actionStatus = base.GetJsonContent(false, string.Empty, "Application Status Updated Successfully ");
                }
                else
                {
                    actionStatus = base.GetJsonContent(true, string.Empty, "Not Able To Update Application Status ");
                }
                /*Convert to json string*/
                string jsonModels = JsonConvert.SerializeObject(actionStatus);
                string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
                return RedirectToAction("Index", new { KeyMsg = keyMsg });



            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public JsonResult GetAllCandidateOffers(Guid AppId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                List<BEClient.VacancyOffer> ObjVacancyOfferList = new List<BEClient.VacancyOffer>();
                BLClient.VacancyOfferAction VacOfferAction = new BLClient.VacancyOfferAction(_CurrentClientId);
                ObjVacancyOfferList = VacOfferAction.GetVacancyOfferByApplicationId(AppId, _CurrentLanguageId);
                //No need to display Draft offer to Candidate: Gordon 09-04-2015
                ObjVacancyOfferList = ObjVacancyOfferList.Where(x => x.OfferStatusText != BEClient.VacancyOfferStatus.Draft.ToString()).ToList();
                ViewBag.IsCandidate = true;
                Data = base.RenderRazorViewToString("VacancyOffer/_AccVacancyOfferList", ObjVacancyOfferList);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCandidateOffers()
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                Data = base.RenderRazorViewToString("VacancyOffer/_MO", null);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllInterviews(Guid ApplicationId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                //string LstRndTypeId = GetRndTypeIdByCandidateSelfEvalution();
                List<BEClient.ScheduleInt> objScheduleIntLst = new List<BEClient.ScheduleInt>();
                BLClient.ScheduleIntAction objScheduleAction = new BLClient.ScheduleIntAction(_CurrentClientId);
                objScheduleIntLst = objScheduleAction.GetVacRndDetailByApplicationId(ApplicationId, null);

                Data = base.RenderRazorViewToString("Shared/_interview", objScheduleIntLst);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllAppQuestions(Guid ApplicationId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                JobApplication<RootModels.BreadScrumbModel<BEClient.CandidateApplications>> _jobAppWithQue = new JobApplication<BreadScrumbModel<BEClient.CandidateApplications>>();
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
                ViewBag.ApplicationStatus = BEApplicationOptionsConstant.APP_STAT_SUBMIT;
                Data = base.RenderRazorViewToString("Question/_CreateQuestions", _jobAppWithQue);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetApplicationById(Guid ApplicationId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                BLClient.CandidateApplicationsAction objCandidateApplicationAction = new BLClient.CandidateApplicationsAction(_CurrentClientId);
                BEClient.CandidateApplications objCandidateApplication = new BEClient.CandidateApplications();
                objCandidateApplication = objCandidateApplicationAction.GetApplicationDetailByApplication(ApplicationId);
                Data = base.RenderRazorViewToString("Shared/_ApplicationDetails", objCandidateApplication);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        #region UPLOAD DOWNLOAD FUNCTIONS
        public ActionResult UploadCoverLetter(string VacancyId)
        {
            return RedirectToAction("Index", "ApplyVacancy", new { VacancyId = VacancyId });
        }

        [HttpPost]
        public ActionResult UploadNewCoverLetter(string VacancyId)
        {
            try
            {
                string _oldFileName = String.Empty;
                string _newFileName = String.Empty;
                string _serverFilePath = String.Empty;
                Guid _DocumentId = Guid.Empty;

                BEClient.Profile objProfile = new BEClient.Profile();
                objProfile.ClientId = _CurrentClientId;
                objProfile.UserId = _CurrentUserId;

                HttpPostedFileBase coverLetter = Request.Files[0] as HttpPostedFileBase;
                BLClient.DocumentTypeAction objDocTypeAction = new BLClient.DocumentTypeAction(_CurrentClientId);
                BEClient.DocumentType objDocType = objDocTypeAction.GetDocTypeByDocCategory((int)BEClient.DocCategoryType.CoverLetter);
                objProfile.DocumentTypeId = objDocType.DocumentTypeId;
                bool result_CoverLtr = ATS.WebUi.Common.CommonFunctions.UploadResume(objProfile, coverLetter, out _oldFileName, out _newFileName, out _serverFilePath, out _DocumentId, true);
                BEClient.ATSResume _atsResume = ATS.WebUi.Common.CurrentSession.Instance.VerifiedUser.tempResume;
                _atsResume.IsCoverLetter = true;
                if (objATSResumeAction == null)
                    objATSResumeAction = new BLClient.ATSResumeAction(ATS.WebUi.Common.CurrentSession.Instance.VerifiedClient.ClientId);

                _DocumentId = objATSResumeAction.AddATSResume(_atsResume);

                var dict = new Dictionary<string, string> { { "ATSResumeId", _DocumentId.ToString() }, { "Name", _atsResume.UploadedFileName } };
                var serializer = new JavaScriptSerializer();
                JsonModels actionStatus = base.GetJsonContent(false, string.Empty, String.Empty, serializer.Serialize(dict));
                return GetJson(actionStatus);
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                return GetJson(actionStatus);
            }
        }

        [HttpPost]
        public ActionResult UploadResume(string VacancyId)
        {
            try
            {
                string _oldFileName = String.Empty;
                string _newFileName = String.Empty;
                string _serverFilePath = String.Empty;
                Guid _DocumentId = Guid.Empty;

                BEClient.Application objApplication = new BEClient.Application();
                objApplication.VacancyId = new Guid(VacancyId);
                objApplication.IsDelete = false;
                objApplication.LanguageId = _CurrentLanguageId;

                BEClient.Profile objProfile = new BEClient.Profile();
                objProfile.ClientId = _CurrentClientId;
                objProfile.UserId = _CurrentUserId;
                if (Request.Files[0] != null)
                {
                    HttpPostedFileBase resume = Request.Files[0] as HttpPostedFileBase;
                    objProfile.ProfileName = Path.GetFileNameWithoutExtension(resume.FileName);

                    BLClient.DocumentTypeAction objDocTypeAction = new BLClient.DocumentTypeAction(_CurrentClientId);
                    BEClient.DocumentType objDocType = objDocTypeAction.GetDocTypeByDocCategory((int)BEClient.DocCategoryType.Resume);
                    objProfile.DocumentTypeId = objDocType.DocumentTypeId;

                    BEClient.CandidateProfile objCandidate = ATS.WebUi.Common.CommonFunctions.GenerateProfile(objProfile, resume, true);
                    objCandidate.ObjAvailability = new BEClient.Availability();
                    BLClient.ProfileAction objProfileAction = new BLClient.ProfileAction(_CurrentClientId);
                    var dict = new Dictionary<string, string> { { "ATSResumeId", objCandidate.objATSResume.ATSResumeId.ToString() }, { "Name", objCandidate.objProfile.ProfileName } };
                    var serializer = new JavaScriptSerializer();
                    JsonModels actionStatus = base.GetJsonContent(false, string.Empty, String.Empty, serializer.Serialize(dict));
                    return GetJson(actionStatus);
                }
                else
                {
                    throw new Exception("File not uploaded");
                }
            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                return GetJson(actionStatus);
            }
        }

        [HttpPost]
        public ActionResult UploadDocument(Guid DocumentTypeId)
        {
            try
            {
                string _oldFileName = String.Empty;
                string _newFileName = String.Empty;
                string _serverFilePath = String.Empty;
                string _extension = string.Empty;
                Guid _DocumentId = Guid.Empty;
                BEClient.Profile objProfile = new BEClient.Profile();
                objProfile.ClientId = _CurrentClientId;
                objProfile.UserId = _CurrentUserId;
                HttpPostedFileBase Document = Request.Files[0] as HttpPostedFileBase;

                BLClient.DocumentTypeAction objDocTypeAction = new BLClient.DocumentTypeAction(_CurrentClientId);
                BEClient.DocumentType objDocType = objDocTypeAction.GetDocmentTypeById(DocumentTypeId, _CurrentLanguageId);
                objProfile.DocumentTypeId = objDocType.DocumentTypeId;
                BEClient.ATSResume _atsResume = new BEClient.ATSResume();
                if (objDocType.DocCategory == Convert.ToInt32(BEClient.DocCategoryType.Resume))
                {
                    BEClient.CandidateProfile objCandidateProfile = new BEClient.CandidateProfile();
                    objProfile.ProfileName = Path.GetFileNameWithoutExtension(Document.FileName);
                    objCandidateProfile = ATSCommon.CommonFunctions.GenerateProfile(objProfile, Document, true);
                    _atsResume.UploadedFileName = objCandidateProfile.objATSResume.UploadedFileName;
                    _DocumentId = objCandidateProfile.objATSResume.ATSResumeId;
                }
                else
                {
                    bool result_CoverLtr = ATS.WebUi.Common.CommonFunctions.UploadDocument(objProfile, objDocType.ExtensionTypes, Document, out _oldFileName, out _newFileName, out _serverFilePath, out _DocumentId, true, true);
                    _atsResume.UploadedFileName = _oldFileName;
                }

                var dict = new Dictionary<string, string> { { "ATSResumeId", _DocumentId.ToString() }, { "Name", _atsResume.UploadedFileName } };
                var serializer = new JavaScriptSerializer();
                JsonModels actionStatus = base.GetJsonContent(false, string.Empty, String.Empty, serializer.Serialize(dict));
                return GetJson(actionStatus);

            }
            catch (Exception ex)
            {
                JsonModels actionStatus = base.GetJsonContent(true, string.Empty, ex.Message);
                return GetJson(actionStatus);
            }
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
                return RedirectToAction(ATS.WebUi.Common.Constants.STR_LIST_METHOD, new { KeyMsg = keyMsg });
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
        #endregion

        #region COMMON FUNCTIONS
        private void InsertScheduleInt(Guid ApplicationId, Guid VacRndId)
        {
            BLClient.ScheduleIntAction objScheduleIntAction = new BLClient.ScheduleIntAction(_CurrentClientId);
            BEClient.ScheduleInt ScheduleInt = new BEClient.ScheduleInt();
            ScheduleInt.ApplicationId = ApplicationId;
            ScheduleInt.VacRndId = VacRndId;
            Guid ResultSI = objScheduleIntAction.AddSaveScheduleInt(ScheduleInt);


        }

        private void ActiveNextRound(Guid ApplicationId)
        {
            BLClient.PromoteCandidateAction PCAction = new BLClient.PromoteCandidateAction(_CurrentClientId);
            //Get Next Round
            Guid VacRndId = PCAction.GetFirstVacRndIdByApplicationId(ApplicationId);

            //Update Promote Candidate
            BEClient.PromoteCandidate ObjPC = new BEClient.PromoteCandidate();
            ObjPC.ApplicationId = ApplicationId;
            ObjPC.VacRndId = VacRndId;
            ObjPC.VacRndId = VacRndId;
            Boolean ResultPC = PCAction.UpdatePromoteCandidate(ObjPC, null);
        }

        private void UpdatePromoteCandidateForActive(Guid ApplicationId, Guid VacRndId)
        {
            //Update Promote Candidate
            BLClient.PromoteCandidateAction PCAction = new BLClient.PromoteCandidateAction(_CurrentClientId);
            BEClient.PromoteCandidate ObjPC = new BEClient.PromoteCandidate();
            ObjPC.ApplicationId = ApplicationId;
            ObjPC.VacRndId = VacRndId;
            ObjPC.VacRndId = VacRndId;
            Boolean ResultPC = PCAction.UpdatePromoteCandidate(ObjPC, null);
        }

        public string GetRndTypeIdByCandidateSelfEvalution()
        {
            BLClient.RndTypeAction _objRndTypeAction = new BLClient.RndTypeAction(_CurrentClientId);
            List<Guid> lstRndType = _objRndTypeAction.GetRndTypeIdByCandidateSelfEvalution();
            String lstRndTypeStr = String.Join(",", lstRndType.Select(g => g.ToString()).ToArray());
            return lstRndTypeStr;
        }
        public string GetCandidateSurveyRndTypeId()
        {
            BLClient.RndTypeAction _objRndTypeAction = new BLClient.RndTypeAction(_CurrentClientId);
            List<Guid> lstRndType = _objRndTypeAction.GetCandidateSurveyRndTypeId();
            String lstRndTypeStr = String.Join(",", lstRndType.Select(g => g.ToString()).ToArray());
            return lstRndTypeStr;
        }

        //In
        public string GetCandidateSurveyRounds(Guid ApplicationId)
        {
            BLClient.CandidateSurveyAction objCandidateSurveyAction = new BLClient.CandidateSurveyAction(_CurrentClientId);
            List<BEClient.CandidateSurvey> lstCandidateSurvey = new List<BEClient.CandidateSurvey>();
            lstCandidateSurvey = objCandidateSurveyAction.GetCandidateSurveyRounds(ApplicationId);
            String lstRndTypeStr = String.Join(",", lstCandidateSurvey.Select(g => g.RndTypeId.ToString()).ToArray());
            return lstRndTypeStr;
        }
        #endregion

        #region ACCORDION REQUIRED DOCUMENTS
        public JsonResult GetRequiredDocuments(Guid ApplicationId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                BLClient.ApplicationDocumentsAction objAppDocAction = new BLClient.ApplicationDocumentsAction(_CurrentClientId);
                List<BEClient.ApplicationDocuments> objApplicationDocuments = new List<BEClient.ApplicationDocuments>();
                objApplicationDocuments = objAppDocAction.GetRequiredDocumentsByApplicationId(ApplicationId);
                objApplicationDocuments.ToList().ForEach(m => m.ApplicationId = ApplicationId);
                Data = base.RenderRazorViewToString("ApplicationDocuments/_ApplicationDocumentList", objApplicationDocuments);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddUpdateApplicationDocument(BEClient.ApplicationDocuments objApplicationDocuments)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                BLClient.ApplicationDocumentsAction objAppDocAction = new BLClient.ApplicationDocumentsAction(_CurrentClientId);
                if (objApplicationDocuments.ApplicationDocumentId == Guid.Empty)
                {
                    Guid NewApplicationDocumentId = objAppDocAction.AddApplicationDocuments(objApplicationDocuments);
                    objApplicationDocuments.ApplicationDocumentId = NewApplicationDocumentId;
                    Message = objApplicationDocuments.RequiredDocumentName + " Uploaded Successfully";
                }
                else
                {
                    bool Result = objAppDocAction.UpdateApplicationDocuments(objApplicationDocuments);
                    Message = objApplicationDocuments.RequiredDocumentName + " Uploaded Successfully";
                }
                objApplicationDocuments = objAppDocAction.GetApplicationDocumentById(objApplicationDocuments.ApplicationDocumentId);
                objApplicationDocuments.ApplicationId = objApplicationDocuments.ApplicationId;
                Data = base.RenderRazorViewToString("ApplicationDocuments/_ApplicationDocumentSingle", objApplicationDocuments);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data));
        }

        public PartialViewResult GetUploadRequiredDocumentModel(BEClient.ApplicationDocuments objApplicationDocument)
        {
            try
            {
                BLClient.ATSResumeAction objATSResumeAction = new BLClient.ATSResumeAction(_CurrentClientId);
                List<BEClient.ATSResume> objDocumentList = new List<BEClient.ATSResume>();
                objDocumentList = objATSResumeAction.GetUserDocumentsByDocumentTypeId(_CurrentUserId, objApplicationDocument.DocumentTypeId);
                ViewBag.drpDocumentList = new SelectList(objDocumentList, "ATSResumeId", "UploadedFileName");
                return PartialView("ApplicationDocuments/_UploadDocumentModel", objApplicationDocument);
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region BIND APPLICATION QUESTIONS
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
                    if (_current.AppAnswer.Answer.ToString() != string.Empty)
                    {
                        List<BEClient.AnsOpt> objAnsOpt = ObjAnsOptAction.GetAnsListByAppAnsId(_current.AppAnswer.Answer.ToString(), _CurrentLanguageId);
                        _DrpATSPickList.QuestionText = _current.Question.LocalName;
                        _DrpATSPickList.Answer = objAnsOpt[0].LocalName;
                        _DrpATSPickList.QueCatId = _current.Question.QueCatId;
                        _ListATSPickList.Add(_DrpATSPickList);
                    }
                    else
                    {
                        return null;
                    }
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
                    _DrpATSScale.Answer = _current.AppAnswer.Answer.ToString() == "" ? 0 : Convert.ToInt32(_current.AppAnswer.Answer);
                    _ListATSScale.Add(_DrpATSScale);
                }
                return _ListATSScale;
            }
            else
            {
                return null;
            }
        }
        #endregion

        [HttpGet]
        public JsonResult GetAllCandidateAppQuestions(Guid ApplicationId, Guid RndTypeId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            String _rndtypeid = string.Empty;
            try
            {
                JobApplication<RootModels.BreadScrumbModel<BEClient.CandidateApplications>> _jobAppWithQue = new JobApplication<BreadScrumbModel<BEClient.CandidateApplications>>();

                BEClient.CandidateSurvey objCandidateSurvey = new BEClient.CandidateSurvey();
                BLClient.CandidateSurveyAction objCandidateSurveyAction = new BLClient.CandidateSurveyAction(_CurrentClientId);

                if (RndTypeId != Guid.Empty)
                {
                    _rndtypeid = RndTypeId.ToString();
                    BLClient.AppQueAnsAction _AppQueAnsAction = new BLClient.AppQueAnsAction(_CurrentClientId);
                    //         string LstRndTypeId = GetCandidateSurveyRounds(ApplicationId);
                    // string LstRndTypeId = GetCandidateSurveyRndTypeId();
                    List<BEClient.AppQueAns> _objAppQueAns = _AppQueAnsAction.GetAppQueByApplicationId(ApplicationId, _rndtypeid);
                    if (_objAppQueAns.Count > 0)
                    {
                        ViewBag.ScreenQue = 0;

                        _jobAppWithQue.ListPickList = GetSubmitPickList(_objAppQueAns.Where(x => x.Question.QueDataType.Equals(1)).ToList());
                        _jobAppWithQue.ListCheckList = GetSubmitCheckList(_objAppQueAns.Where(x => x.Question.QueDataType.Equals(2)).ToList());
                        _jobAppWithQue.ListATSTextArea = GetSubmitTextArea(_objAppQueAns.Where(x => x.Question.QueDataType.Equals(3)).ToList());
                        _jobAppWithQue.ListATSTextBox = GetSubmitTextBox(_objAppQueAns.Where(x => x.Question.QueDataType.Equals(4)).ToList());
                        _jobAppWithQue.ListATSYesNo = GetSubmitYesNo(_objAppQueAns.Where(x => x.Question.QueDataType.Equals(5)).ToList());
                        _jobAppWithQue.ListATSScale = GetSubmitScale(_objAppQueAns.Where(x => x.Question.QueDataType.Equals(6)).ToList());
                        _jobAppWithQue.ATSQueCommon = new RootModels.Question.ATSQuestionCommon();
                        _jobAppWithQue.ATSQueCommon.QueCatList = _objAppQueAns.Select(x => new Tuple<Guid, string>(x.Question.QueCatId, x.Question.QueCatLocalName)).Distinct().ToArray().ToList();

                        ViewBag.ApplicationStatus = BEApplicationOptionsConstant.APP_STAT_SUBMIT;
                        Data = base.RenderRazorViewToString("Question/_CreateQuestions", _jobAppWithQue);

                    }
                    else
                    {
                        ViewBag.ScreenQue = 0;
                        BLClient.QuestionDetailAction _objQuestionDetailAction = new BLClient.QuestionDetailAction(base.CurrentClient.ClientId);
                        List<BEClient.QuestionDetails> _objQuestionDetails = new List<BEClient.QuestionDetails>();
                        _objQuestionDetails = _objQuestionDetailAction.GetQuestionDetails(ApplicationId, _rndtypeid);
                        _jobAppWithQue.ListPickList = GetATSPickList(_objQuestionDetails.Where(x => x.Question.QueDataType.Equals(1)).ToList());
                        _jobAppWithQue.ListCheckList = GetATSCheckList(_objQuestionDetails.Where(x => x.Question.QueDataType.Equals(2)).ToList());
                        _jobAppWithQue.ListATSTextArea = GetATSTextArea(_objQuestionDetails.Where(x => x.Question.QueDataType.Equals(3)).ToList());
                        _jobAppWithQue.ListATSTextBox = GetATSTextBox(_objQuestionDetails.Where(x => x.Question.QueDataType.Equals(4)).ToList());
                        _jobAppWithQue.ListATSYesNo = GetATSYesNo(_objQuestionDetails.Where(x => x.Question.QueDataType.Equals(5)).ToList());
                        _jobAppWithQue.ListATSScale = GetATSScale(_objQuestionDetails.Where(x => x.Question.QueDataType.Equals(6)).ToList());
                        _jobAppWithQue.ATSQueCommon = new RootModels.Question.ATSQuestionCommon();
                        _jobAppWithQue.ATSQueCommon.QueCatList = _objQuestionDetails.Select(x => new Tuple<Guid, string>(x.Question.QueCatId, x.Question.QueCatLocalName)).Distinct().ToArray().ToList();
                        _jobAppWithQue.mainObject = new BreadScrumbModel<BEClient.CandidateApplications>();
                        _jobAppWithQue.mainObject.obj = new BEClient.CandidateApplications();
                        _jobAppWithQue.mainObject.obj.ApplicationId = ApplicationId;
                        if (_objQuestionDetails != null && _objQuestionDetails.Count() > 0)
                        {
                            _jobAppWithQue.mainObject.obj.VacRndId = _objQuestionDetails.FirstOrDefault().VacRnd.VacancyRoundId;
                        }
                        ViewBag.ApplicationStatus = BEApplicationOptionsConstant.APP_STAT_DRAFT;
                        Data = base.RenderRazorViewToString("_CandidateSurveyQuestions", _jobAppWithQue);
                    }
                }
                else
                {
                    IsError = true;
                    Message = "Error";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllCandidateSurveyRounds(Guid ApplicationId)
        {
            bool IsError = false;
            String Message = String.Empty;
            String Data = String.Empty;
            try
            {
                BLClient.CandidateSurveyAction objCandidateSurveyAction = new BLClient.CandidateSurveyAction(_CurrentClientId);
                List<BEClient.CandidateSurvey> lstCandidateSurvey = new List<BEClient.CandidateSurvey>();
                lstCandidateSurvey = objCandidateSurveyAction.GetCandidateSurveyRounds(ApplicationId);
                foreach (var item in lstCandidateSurvey)
                {
                    BLClient.AppQueAnsAction _AppQueAnsAction = new BLClient.AppQueAnsAction(_CurrentClientId);
                    List<BEClient.AppQueAns> _objAppQueAns = _AppQueAnsAction.GetAppQueByApplicationId(ApplicationId, item.RndTypeId.ToString());
                    if (_objAppQueAns != null && _objAppQueAns.Count() > 0)
                    {
                        item.IsPending = false;
                        item.SubmittedDate = _objAppQueAns[0].AppAnswer.CreatedOn;
                    }
                    else
                    {
                        item.IsPending = true;
                    }
                }
                Data = base.RenderRazorViewToString("Shared/_AccinnerCandidate", lstCandidateSurvey);
            }
            catch
            {
                throw;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }
        #region Bind Question
        private List<ATS.WebUi.Models.Question.ATSPickList> GetATSPickList(List<BEClient.QuestionDetails> objQuestion)
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
                    _DrpATSPickList.QueCatId = _current.Question.QueCatId;
                    SelectList newlist = new SelectList(_objDdlAnsList, "AnsOptId", "DefaultName");
                    _DrpATSPickList.AllValues = newlist;
                    _DrpATSPickList.VacQueId = _current.VacQue.VacQueId;
                    _DrpATSPickList.VacRndId = _current.VacRnd.VacancyRoundId;
                    if (_current.AppAnswer != null)
                        _DrpATSPickList.Answer = _current.AppAnswer.Answer;
                    _ListATSPickList.Add(_DrpATSPickList);
                }
                return _ListATSPickList;
            }
            else
            {
                return null;
            }
        }
        private List<ATS.WebUi.Models.Question.ATSCheckBox> GetATSCheckList(List<BEClient.QuestionDetails> objQuestion)
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
                    _DrpATSCheckBox.QueCatId = _current.Question.QueCatId;
                    _DrpATSCheckBox.AllValues = new SelectList(_objDdlAnsList, "AnsOptId", "DefaultName");
                    _DrpATSCheckBox.VacQueId = _current.VacQue.VacQueId;
                    _DrpATSCheckBox.VacRndId = _current.VacRnd.VacancyRoundId;
                    if (_current.AppAnswer != null)
                    {
                        List<BEClient.AnsOpt> _objselectedList = new List<BEClient.AnsOpt>();
                        int i = 0;
                        foreach (var item in _current.AppAnswer.Answer.ToString().Split(',').ToArray())
                        {
                            if (_objDdlAnsList.Where(x => x.AnsOptId.ToString().Equals(item.ToString())).Count() > 0)
                            {
                                _objselectedList.Add(_objDdlAnsList.Where(x => x.AnsOptId.ToString().Equals(item.ToString())).First());
                            }
                            i++;
                        }
                        _DrpATSCheckBox.SelectedList = new SelectList(_objselectedList, "AnsOptId", "DefaultName");
                    }
                    _ListATSCheckBox.Add(_DrpATSCheckBox);
                }
                return _ListATSCheckBox;
            }
            else
            {
                return null;
            }
        }
        private List<ATS.WebUi.Models.Question.ATSTextArea> GetATSTextArea(List<BEClient.QuestionDetails> objQuestion)
        {
            if (objQuestion != null && objQuestion.Count() > 0)
            {
                List<ATS.WebUi.Models.Question.ATSTextArea> _ListATSTextArea = new List<WebUi.Models.Question.ATSTextArea>();
                foreach (BEClient.QuestionDetails _current in objQuestion)
                {
                    ATS.WebUi.Models.Question.ATSTextArea _DrpATSTextArea = new WebUi.Models.Question.ATSTextArea();
                    _DrpATSTextArea.QuestionText = _current.Question.LocalName;
                    _DrpATSTextArea.QueCatId = _current.Question.QueCatId;
                    _DrpATSTextArea.VacQueId = _current.VacQue.VacQueId;
                    _DrpATSTextArea.VacRndId = _current.VacRnd.VacancyRoundId;
                    if (_current.AppAnswer != null)
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
        private List<ATS.WebUi.Models.Question.ATSTextBox> GetATSTextBox(List<BEClient.QuestionDetails> objQuestion)
        {
            if (objQuestion != null && objQuestion.Count() > 0)
            {
                List<ATS.WebUi.Models.Question.ATSTextBox> _ListATSTextBox = new List<WebUi.Models.Question.ATSTextBox>();
                foreach (BEClient.QuestionDetails _current in objQuestion)
                {
                    ATS.WebUi.Models.Question.ATSTextBox _DrpATSTextBox = new WebUi.Models.Question.ATSTextBox();
                    _DrpATSTextBox.QuestionText = _current.Question.LocalName;
                    _DrpATSTextBox.QueCatId = _current.Question.QueCatId;
                    _DrpATSTextBox.VacQueId = _current.VacQue.VacQueId;
                    _DrpATSTextBox.VacRndId = _current.VacRnd.VacancyRoundId;
                    if (_current.AppAnswer != null)
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
        private List<ATS.WebUi.Models.Question.ATSYesNo> GetATSYesNo(List<BEClient.QuestionDetails> objQuestion)
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
                    _DrpyesNoQue.QueCatId = _current.Question.QueCatId;
                    SelectList newlist = new SelectList(_objDdlAnsList, "AnsOptId", "DefaultName");
                    _DrpyesNoQue.AllValues = newlist;
                    _DrpyesNoQue.VacQueId = _current.VacQue.VacQueId;
                    _DrpyesNoQue.VacRndId = _current.VacRnd.VacancyRoundId;
                    if (_current.AppAnswer != null)
                        _DrpyesNoQue.Answer = _current.AppAnswer.Answer;
                    _ListATSYesNo.Add(_DrpyesNoQue);
                }
                return _ListATSYesNo;
            }
            else
            {
                return null;
            }
        }
        private List<ATS.WebUi.Models.Question.ATSScale> GetATSScale(List<BEClient.QuestionDetails> objQuestion)
        {
            if (objQuestion != null && objQuestion.Count() > 0)
            {
                List<ATS.WebUi.Models.Question.ATSScale> _ListATSTScale = new List<WebUi.Models.Question.ATSScale>();
                foreach (BEClient.QuestionDetails _current in objQuestion)
                {
                    ATS.WebUi.Models.Question.ATSScale _DrpATSScale = new WebUi.Models.Question.ATSScale();
                    _DrpATSScale.QuestionText = _current.Question.LocalName;
                    _DrpATSScale.QueCatId = _current.Question.QueCatId;
                    _DrpATSScale.VacQueId = _current.VacQue.VacQueId;
                    _DrpATSScale.VacRndId = _current.VacRnd.VacancyRoundId;
                    if (_current.AppAnswer != null)
                        _DrpATSScale.Answer = _current.AppAnswer.Answer;
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
    }
}