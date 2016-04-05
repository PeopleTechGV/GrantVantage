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
using System.Text;
using System.Reflection;

namespace ATS.WebUi.Areas.Candidate.Controllers
{
    public class ApplyVacancyController : ATS.WebUi.Controllers.AreaBaseController
    {
        #region DECLARE
        private Guid _CurrentClientId;
        private Guid _CurrentLanguageId;
        private Guid _CurrentUserId;
        //For Recent View --Anand Shah
        private string _url = string.Empty;
        private BLClient.ATSResumeAction objATSResumeAction;
        public HttpPostedFileBase file1 { get; set; }
        #region OnAuthorization Method call on every request
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            _url = filterContext.HttpContext.Request.Url.AbsoluteUri.ToString();
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
        #endregion

        public ActionResult Index(Guid VacancyId, string Status, Boolean IsNewUser = false)
        {
            JobApplication<RootModels.BreadScrumbModel<BEClient.CandidateApplications>> _jobAppWithQue = new JobApplication<BreadScrumbModel<BEClient.CandidateApplications>>();
            Guid ApplicationId = Guid.Empty;
            BEClient.Application ObjAppState = GetAppStateByVacId(VacancyId);

            #region NEW APPLICATION
            if (ObjAppState == null)
            {

                List<BEClient.RequiredDocument> objRequiredDocumentList = GetRequiredDocuments(VacancyId);
                if (objRequiredDocumentList.Count == 0)
                {
                    BLClient.QuestionDetailAction _objQuestionDetailAction = new BLClient.QuestionDetailAction(base.CurrentClient.ClientId);
                    List<BEClient.QuestionDetails> _objQuestionDetails = new List<BEClient.QuestionDetails>();
                    _objQuestionDetails = _objQuestionDetailAction.GetScreeningQueByVacancyId(VacancyId);

                    if (_objQuestionDetails != null && _objQuestionDetails.Count != 0)
                    {
                        ApplicationId = GenerateApplication(VacancyId, BECommon.ApplicationStatusOptionsConstant.APP_STAT_DRAFT, null);
                        _jobAppWithQue.ListPickList = GetATSPickList(_objQuestionDetails.Where(x => x.Question.QueDataType.Equals(1)).ToList());
                        _jobAppWithQue.ListCheckList = GetATSCheckList(_objQuestionDetails.Where(x => x.Question.QueDataType.Equals(2)).ToList());
                        _jobAppWithQue.ListATSTextArea = GetATSTextArea(_objQuestionDetails.Where(x => x.Question.QueDataType.Equals(3)).ToList());
                        _jobAppWithQue.ListATSTextBox = GetATSTextBox(_objQuestionDetails.Where(x => x.Question.QueDataType.Equals(4)).ToList());
                        _jobAppWithQue.ListATSYesNo = GetATSYesNo(_objQuestionDetails.Where(x => x.Question.QueDataType.Equals(5)).ToList());
                        _jobAppWithQue.ListATSScale = GetATSScale(_objQuestionDetails.Where(x => x.Question.QueDataType.Equals(6)).ToList());
                        _jobAppWithQue.ATSQueCommon = new RootModels.Question.ATSQuestionCommon();
                        _jobAppWithQue.ATSQueCommon.QueCatList = _objQuestionDetails.Select(x => new Tuple<Guid, string>(x.Question.QueCatId, x.Question.QueCatLocalName)).Distinct().ToArray().ToList();
                        ViewBag.Step = "Step3";
                        ViewBag.ApplicationStatus = BECommon.ApplicationStatusOptionsConstant.APP_STAT_DRAFT.ToString();
                    }
                    else
                    {
                        ApplicationId = GenerateApplication(VacancyId, BECommon.ApplicationStatusOptionsConstant.APP_STAT_SUBMIT, null);
                        BLClient.ApplicationAction _ObjApplicationAction = new BLClient.ApplicationAction(base.CurrentClient.ClientId);
                        bool Result1 = _ObjApplicationAction.ChangeApplicationStatus(ApplicationId, BECommon.ApplicationStatusOptionsConstant.APP_STAT_SUBMIT);
                        return RedirectToAction("Index", "JobApplication", new { ApplicationId = ApplicationId, ControllerName = "JobApplication" });
                    }
                }
                else
                {
                    ViewBag.ScreeningQue = CheckForScreeningQue(VacancyId);
                    ViewBag.Step = "Step2";
                }
            }
            #endregion


            #region DRAFT APPLICATION
            else if (ObjAppState.ApplicationStatus == ATS.BusinessEntity.Common.ApplicationStatusOptionsConstant.APP_STAT_DRAFT.ToString())
            {
                BLClient.ApplicationAction ObjApplicationAction = new BLClient.ApplicationAction(_CurrentClientId);
                ApplicationId = ObjApplicationAction.GetApplicationIdByVacIdAndUserId(VacancyId, _CurrentUserId, _url);
                BLClient.QuestionDetailAction _objQuestionDetailAction = new BLClient.QuestionDetailAction(base.CurrentClient.ClientId);
                List<BEClient.QuestionDetails> _objQuestionDetails = new List<BEClient.QuestionDetails>();
                string ScreenRound = GetRndTypeIdByCandidateSelfEvalution();
                if (!string.IsNullOrEmpty(ScreenRound))
                {
                    if (ObjAppState.SaveForLater == false)
                        _objQuestionDetails = _objQuestionDetailAction.GetQuestionDetails(ApplicationId, ScreenRound);
                    else
                        _objQuestionDetails = _objQuestionDetailAction.GetQuestionsWithAnswers(ApplicationId, ScreenRound);
                }

                if (_objQuestionDetails != null && _objQuestionDetails.Count != 0)
                {
                    _jobAppWithQue.ListPickList = GetATSPickList(_objQuestionDetails.Where(x => x.Question.QueDataType.Equals(1)).ToList());
                    _jobAppWithQue.ListCheckList = GetATSCheckList(_objQuestionDetails.Where(x => x.Question.QueDataType.Equals(2)).ToList());
                    _jobAppWithQue.ListATSTextArea = GetATSTextArea(_objQuestionDetails.Where(x => x.Question.QueDataType.Equals(3)).ToList());
                    _jobAppWithQue.ListATSTextBox = GetATSTextBox(_objQuestionDetails.Where(x => x.Question.QueDataType.Equals(4)).ToList());
                    _jobAppWithQue.ListATSYesNo = GetATSYesNo(_objQuestionDetails.Where(x => x.Question.QueDataType.Equals(5)).ToList());
                    _jobAppWithQue.ListATSScale = GetATSScale(_objQuestionDetails.Where(x => x.Question.QueDataType.Equals(6)).ToList());
                    _jobAppWithQue.ATSQueCommon = new RootModels.Question.ATSQuestionCommon();
                    _jobAppWithQue.ATSQueCommon.QueCatList = _objQuestionDetails.Select(x => new Tuple<Guid, string>(x.Question.QueCatId, x.Question.QueCatLocalName)).Distinct().ToArray().ToList();
                    ViewBag.Step = "Step3";
                    ViewBag.ApplicationStatus = ObjAppState.ApplicationStatus;
                }
                else
                {
                    ActiveNextRound(ApplicationId);
                    BLClient.ApplicationAction _ObjApplicationAction = new BLClient.ApplicationAction(base.CurrentClient.ClientId);
                    bool Result1 = _ObjApplicationAction.ChangeApplicationStatus(ApplicationId, BECommon.ApplicationStatusOptionsConstant.APP_STAT_SUBMIT);
                    return RedirectToAction("Index", "JobApplication", new { ApplicationId = ApplicationId, ControllerName = "JobApplication" });
                }
            }
            #endregion


            #region SUBMIT APPLICATION
            else if (ObjAppState.ApplicationStatus == ATS.BusinessEntity.Common.ApplicationStatusOptionsConstant.APP_STAT_SUBMIT.ToString())
            {
                BLClient.ApplicationAction ObjApplicationAction = new BLClient.ApplicationAction(_CurrentClientId);
                ApplicationId = ObjApplicationAction.GetApplicationIdByVacIdAndUserId(VacancyId, _CurrentUserId);
                return RedirectToAction("Index", "JobApplication", new { ApplicationId = ApplicationId, ControllerName = "JobApplication" });
            }
            #endregion


            #region PREPARE FINAL VACANCY OBJECT
            if (VacancyId != null)
            {
                BEClient.AnonymousUser objAnonymousUser = new BEClient.AnonymousUser();
                BLClient.VacancyAction ObjVacAction = new BLClient.VacancyAction(CurrentClient.ClientId);
                objAnonymousUser.ObjVacancy = new BEClient.Vacancy();
                objAnonymousUser.ObjVacancy = ObjVacAction.GetVacancyById(VacancyId, CurrentClient.CurrentLanguageId, true);


                objAnonymousUser.ObjVacancy.ShowOnWebBenefits = false;
                objAnonymousUser.ObjVacancy.ShowOnWebDuties = false;
                ////Need to show in GV instance
                //objAnonymousUser.ObjVacancy.ShowOnWebJobDescription = false;
                objAnonymousUser.ObjVacancy.ShowOnWebSkills = false;
                objAnonymousUser.ObjVacancy.IsNewUser = IsNewUser;

                _jobAppWithQue.mainObject = new BreadScrumbModel<BEClient.CandidateApplications>();
                _jobAppWithQue.mainObject.DisplayName = objAnonymousUser.ObjVacancy.JobTitle + ", <span style='color: #953634;'>" + objAnonymousUser.ObjVacancy.LocationText + "</span>";
                _jobAppWithQue.mainObject.ImagePath = BECommon.ImagePaths.ApplyImage;
                _jobAppWithQue.mainObject.obj = new BEClient.CandidateApplications();
                _jobAppWithQue.mainObject.obj.ApplicationId = ApplicationId;
                _jobAppWithQue.mainObject.obj.objVacancy = objAnonymousUser.ObjVacancy;
                ViewBag.VacancyId = VacancyId;
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
                if (!String.IsNullOrEmpty(Status))
                {
                    objRecentlView.Category = BEClient.RecentlyViewedCategory.Application.ToString();
                    objRecentlView.ViewItemId = (Guid)ApplicationId;
                }
                else
                {
                    objRecentlView.Category = BEClient.RecentlyViewedCategory.FeaturedJob.ToString();
                    objRecentlView.ViewItemId = VacancyId;
                }
                objRecentlView.URL = HttpContext.Request.Url.ToString();
                List<BEClient.RecentlyViewed> objList = objRecentlyViewedAction.GetRecentlyViewedApplication(objRecentlView);
                //_jobAppWithQue.mainObject = new BreadScrumbModel<BEClient.CandidateApplications>();
                _jobAppWithQue.mainObject.objRecentViewedList = new List<BEClient.RecentlyViewed>();
                _jobAppWithQue.mainObject.objRecentViewedList = objList;
                #endregion

                return View(_jobAppWithQue);
            }
            else
            {
                return View();
            }
            #endregion
        }

        [HttpPost]
        public JsonResult ApplyJob(BEClient.ApplicationDocuments objApplicationDocuments, string PageMode = "")
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                BEClient.Application ObjAppState = GetAppStateByVacId(objApplicationDocuments.VacancyId);
                Guid ApplicationId = Guid.Empty;
                BEClient.Application objApplication = new BEClient.Application();
                var objResume = objApplicationDocuments.ATSResumeIdList.Where(m => m.IsResume == true).FirstOrDefault();
                Guid ATSResumeId = Guid.Empty;
                if (objResume != null)
                {
                    ATSResumeId = objApplicationDocuments.ATSResumeIdList.Where(m => m.IsResume == true).FirstOrDefault().ATSResumeId;
                }

                if (ObjAppState == null)
                {
                    #region New Application
                    objApplication.ApplicationId = GenerateApplication(objApplicationDocuments.VacancyId, BECommon.ApplicationStatusOptionsConstant.APP_STAT_DRAFT, ATSResumeId);
                    objApplication.ApplicationStatus = BECommon.ApplicationStatusOptionsConstant.APP_STAT_DRAFT;
                    objApplicationDocuments.ApplicationId = objApplication.ApplicationId;
                    foreach (var ATSResume in objApplicationDocuments.ATSResumeIdList)
                    {
                        if (ATSResume.ATSResumeId != Guid.Empty)
                        {
                            objApplicationDocuments.ATSResumeId = ATSResume.ATSResumeId;
                            objApplicationDocuments.VacRndId = ATSResume.VacRndId;
                            objApplicationDocuments.RequiredDocumentId = ATSResume.RequiredDocumentId;
                            BLClient.ApplicationDocumentsAction objAppDocAction = new BLClient.ApplicationDocumentsAction(_CurrentClientId);
                            Guid Appdocid = objAppDocAction.AddApplicationDocuments(objApplicationDocuments);
                        }
                    }

                    Data = RenderView(objApplication);

                    #region Send Successfull Apply Email to Candidate if set in Vacancy
                    try
                    {
                        ObjAppState = GetAppStateByVacId(objApplicationDocuments.VacancyId);

                        if (ObjAppState.ApplicationStatus.ToLower() != BECommon.ApplicationStatusOptionsConstant.APP_STAT_DRAFT.ToLower())
                        {
                            BLClient.VacancyOfferAction ObjVacancyOfferAction = new BLClient.VacancyOfferAction(_CurrentClientId);
                            bool IsSendApplyEmail = ObjVacancyOfferAction.IsSendApplyEmail(objApplication.ApplicationId);
                            if (IsSendApplyEmail)
                            {
                                Common.CommonFunctions.SendMailToCandidate();
                            }
                        }
                    }
                    catch (Exception)
                    { }
                    #endregion

                    #region Check Candidate survey round

                    BLClient.CandidateSurveyAction objCandidateSurveyAction = new BLClient.CandidateSurveyAction(_CurrentClientId);
                    List<BEClient.CandidateSurvey> objCandidateSurvey = new List<BEClient.CandidateSurvey>();
                    objCandidateSurvey = objCandidateSurveyAction.CheckCandidateSurvey(objApplication.ApplicationId);

                    if (objCandidateSurvey != null && objCandidateSurvey.Any(x => x.RndOrder == 1))
                    {
                        BLClient.QuestionDetailAction _objQuestionDetailAction = new BLClient.QuestionDetailAction(base.CurrentClient.ClientId);
                        List<BEClient.QuestionDetails> _objQuestionDetails = new List<BEClient.QuestionDetails>();
                        string RndTypeId = objCandidateSurvey.Where(x => x.RndOrder == 1).Select(x => x.RndTypeId.ToString()).FirstOrDefault();
                        _objQuestionDetails = _objQuestionDetailAction.GetQuestionDetails(objApplication.ApplicationId, RndTypeId);

                        if (_objQuestionDetails != null && _objQuestionDetails.Count() > 0)
                        {
                            SendCandidateSurveyMail(objApplication.ApplicationId, objCandidateSurvey.Where(x => x.RndOrder == 1).Select(x => x.VacRndId).FirstOrDefault());
                        }
                    }
                    #endregion

                    #endregion
                }
                else
                {
                    #region Update Documents for Already Applied Vacancy
                    BLClient.ApplicationAction objAppAction = new BLClient.ApplicationAction(_CurrentClientId);

                    objApplication.ApplicationId = objAppAction.GetApplicationIdByVacIdAndUserId(objApplicationDocuments.VacancyId, _CurrentUserId);
                    objApplication.VacancyId = objApplicationDocuments.VacancyId;
                    objApplication.ApplicationStatus = ObjAppState.ApplicationStatus;
                    objApplication.SaveForLater = ObjAppState.SaveForLater;
                    if (ATSResumeId != Guid.Empty)
                    {
                        bool UpdateResume = objAppAction.UpdateATSResumeId(objApplication.ApplicationId, ATSResumeId);
                    }

                    objApplicationDocuments.ApplicationId = objApplication.ApplicationId;
                    BLClient.ApplicationDocumentsAction objAppDocAction = new BLClient.ApplicationDocumentsAction(_CurrentClientId);
                    bool DeleteDocument = objAppDocAction.DeleteAppDocumentByAppId(objApplication.ApplicationId);
                    if (DeleteDocument)
                    {
                        foreach (var ATSResume in objApplicationDocuments.ATSResumeIdList)
                        {
                            objApplicationDocuments.RequiredDocumentId = ATSResume.RequiredDocumentId;
                            objApplicationDocuments.ATSResumeId = ATSResume.ATSResumeId;
                            objApplicationDocuments.VacRndId = ATSResume.VacRndId;
                            Guid Appdocid = objAppDocAction.AddApplicationDocuments(objApplicationDocuments);
                        }

                        try
                        {
                            if (ObjAppState.ApplicationStatus.ToLower() != BECommon.ApplicationStatusOptionsConstant.APP_STAT_DRAFT.ToLower())
                            {
                                BLClient.VacancyOfferAction ObjVacancyOfferAction = new BLClient.VacancyOfferAction(_CurrentClientId);
                                bool IsSendApplyEmail = ObjVacancyOfferAction.IsSendApplyEmail(ApplicationId);

                                if (IsSendApplyEmail)
                                {
                                    Common.CommonFunctions.SendMailToCandidate();
                                }
                            }
                        }
                        catch (Exception)
                        { }
                    }
                    #endregion

                    Data = RenderView(objApplication);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        public string RenderView(BEClient.Application ObjApplication)
        {
            #region Declare
            string Data = string.Empty;
            Guid VacancyId = ObjApplication.VacancyId;
            BLClient.ApplicationAction objAppAction = new BLClient.ApplicationAction(_CurrentClientId);
            Guid ApplicationId = ObjApplication.ApplicationId;
            string ScreenRound = GetRndTypeIdByCandidateSelfEvalution();
            #endregion

            #region Logic For Render
            if (ObjApplication.ApplicationStatus == BEClient.Common.ApplicationStatusOptionsConstant.APP_STAT_DRAFT)
            {
                if (!string.IsNullOrEmpty(ScreenRound))
                {
                    BLClient.VacancyRoundAction VacRndAction = new BLClient.VacancyRoundAction(_CurrentClientId);
                    BEClient.VacancyRound ObjVacRnd = VacRndAction.GetVacRndByAppIdAndRndTypeId(ApplicationId, ScreenRound);
                    if (ObjVacRnd != null)
                    {
                        BLClient.QuestionDetailAction _objQuestionDetailAction = new BLClient.QuestionDetailAction(base.CurrentClient.ClientId);
                        List<BEClient.QuestionDetails> _objQuestionDetails = new List<BEClient.QuestionDetails>();
                        if (ObjApplication.SaveForLater)
                            _objQuestionDetails = _objQuestionDetailAction.GetQuestionsWithAnswers(ApplicationId, ScreenRound);
                        else
                            _objQuestionDetails = _objQuestionDetailAction.GetQuestionDetails(ApplicationId, ScreenRound);

                        if (_objQuestionDetails.Count == 0)
                        {
                            Guid VacRndId = ObjVacRnd.VacancyRoundId;
                            InsertScheduleInt(ApplicationId, VacRndId);
                            UpdatePromoteCandidateForActive(ApplicationId, VacRndId);
                            ViewBag.ScreenQue = 0;
                            ViewBag.StepNo = "3";
                            BLClient.ApplicationAction _ObjApplicationAction = new BLClient.ApplicationAction(base.CurrentClient.ClientId);
                            bool Result1 = _ObjApplicationAction.ChangeApplicationStatus(ApplicationId, BECommon.ApplicationStatusOptionsConstant.APP_STAT_SUBMIT);
                            Data = base.RenderRazorViewToString("Shared/CAStep4", null);
                        }
                        else
                        {
                            JobApplication<RootModels.BreadScrumbModel<BEClient.CandidateApplications>> _jobAppWithQue = new JobApplication<BreadScrumbModel<BEClient.CandidateApplications>>();
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
                            ViewBag.ScreenQue = 1;
                            ViewBag.ApplicationStatus = "APP_STAT_DRAFT";
                            Data = base.RenderRazorViewToString("Shared/CAStep3", _jobAppWithQue);
                        }
                    }
                    else
                    {
                        ActiveNextRound(ApplicationId);
                        ViewBag.ScreenQue = 0;
                        ViewBag.StepNo = "3";
                        BLClient.ApplicationAction _ObjApplicationAction = new BLClient.ApplicationAction(base.CurrentClient.ClientId);
                        bool Result1 = _ObjApplicationAction.ChangeApplicationStatus(ApplicationId, BECommon.ApplicationStatusOptionsConstant.APP_STAT_SUBMIT);
                        Data = base.RenderRazorViewToString("Shared/CAStep4", null);
                    }
                }
                else
                {
                    ActiveNextRound(ApplicationId);
                    ViewBag.ScreenQue = 0;
                    ViewBag.StepNo = "3";
                    BLClient.ApplicationAction _ObjApplicationAction = new BLClient.ApplicationAction(base.CurrentClient.ClientId);
                    bool Result1 = _ObjApplicationAction.ChangeApplicationStatus(ApplicationId, BECommon.ApplicationStatusOptionsConstant.APP_STAT_SUBMIT);
                    Data = base.RenderRazorViewToString("Shared/CAStep4", null);
                }
            }
            else if (ObjApplication.ApplicationStatus == BEClient.Common.ApplicationStatusOptionsConstant.APP_STAT_SUBMIT)
            {
                JsonResult JSonQueAns = new JsonResult();
                JSonQueAns = GetSubmittedQuestions(VacancyId, "Fwd");
                System.Reflection.PropertyInfo pi = JSonQueAns.Data.GetType().GetProperty("Data");
                Data = (String)(pi.GetValue(JSonQueAns.Data, null));
            }
            #endregion
            return Data;
        }

        #region COMMON FUNCTIONS
        public Guid GenerateApplication(Guid VacancyId, string AppStatus, Guid? ATSResumeId)
        {
            BEClient.Application objApplication = new BEClient.Application();
            objApplication.VacancyId = VacancyId; objApplication.IsDelete = false; objApplication.LanguageId = _CurrentLanguageId;
            objApplication.ApplicationStatus = AppStatus.ToString();
            BLClient.ApplicationAction objApplicationAction = new BLClient.ApplicationAction(base.CurrentClient.ClientId);
            if (ATSResumeId == null || ATSResumeId == Guid.Empty)
            {
                objApplication.ATSResumeId = ATS.WebUi.Common.CommonFunctions.GetDefaultProfileOfUser(_CurrentUserId);
            }
            else
            {
                objApplication.ATSResumeId = (Guid)ATSResumeId;
            }
            objApplication.ApplicationId = objApplicationAction.InsertApplication(objApplication);
            //if (objApplication.ApplicationId != Guid.Empty)
            //{
            //    try
            //    {
            //        if (AppStatus.ToLower() != BECommon.ApplicationStatusOptionsConstant.APP_STAT_DRAFT.ToLower())
            //        {
            //            BLClient.VacancyOfferAction ObjVacancyOfferAction = new BLClient.VacancyOfferAction(_CurrentClientId);
            //            bool IsSendApplyEmail = ObjVacancyOfferAction.IsSendApplyEmail(objApplication.ApplicationId);
            //            if (IsSendApplyEmail)
            //            {
            //                Common.CommonFunctions.SendMailToCandidate();
            //            }
            //        }
            //    }
            //    catch (Exception)
            //    { }
            //}

            #region Promote Candidate Entry
            BLClient.PromoteCandidateAction PCAction = new BLClient.PromoteCandidateAction(base.CurrentClient.ClientId);
            BEClient.PromoteCandidate ObjPC = new BEClient.PromoteCandidate();
            ObjPC.ApplicationId = objApplication.ApplicationId; ObjPC.IsActive = false; ObjPC.IsPromoted = false;
            BLClient.VacancyRoundAction VacRndAction = new BLClient.VacancyRoundAction(base.CurrentClient.ClientId);
            List<BEClient.VacancyRound> VacRndList = VacRndAction.GetAllRoundByApplicationId(objApplication.ApplicationId);
            foreach (BEClient.VacancyRound _current in VacRndList)
            {
                ObjPC.VacRndId = _current.VacancyRoundId;
                Guid ResultPC = PCAction.InsertPromoteCandidate(ObjPC);
            }
            #endregion

            if (AppStatus == BECommon.ApplicationStatusOptionsConstant.APP_STAT_SUBMIT.ToString())
            {
                ActiveNextRound(objApplication.ApplicationId);
            }
            return objApplication.ApplicationId;
        }

        public JsonResult SubmitApp(string SubmitType)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = "";
            string Url = string.Empty;
            try
            {
                if (SubmitType == "SaveForLater")
                {
                    Message = "SaveForLater";
                    Url = RedirectToCandidateHome();
                }
                Data = base.RenderRazorViewToString("Shared/CAStep4", null);
            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;
            }
            return base.GetJson(base.GetJsonContent(IsError, Url, Message, Data), JsonRequestBehavior.AllowGet);
        }



        private String RedirectToCandidateHome()
        {
            UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);
            return u.Action("Index", "Home", new { Area = ATSCommon.Constants.AREA_CANDIDATE });
        }

        public JsonResult GetCompleteProfile()
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = "";
            try
            {
                Data = base.RenderRazorViewToString("Shared/CAStep4", null);
            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        public string GetRndTypeIdByCandidateSelfEvalution()
        {
            BLClient.RndTypeAction _objRndTypeAction = new BLClient.RndTypeAction(base.CurrentClient.ClientId);
            List<Guid> lstRndType = _objRndTypeAction.GetRndTypeIdByCandidateSelfEvalution();
            String lstRndTypeStr = String.Join(",", lstRndType.Select(g => g.ToString()).ToArray());
            return lstRndTypeStr;
        }

        private void InsertScheduleInt(Guid ApplicationId, Guid VacRndId)
        {
            BLClient.ScheduleIntAction objScheduleIntAction = new BLClient.ScheduleIntAction(base.CurrentClient.ClientId);
            BEClient.ScheduleInt ScheduleInt = new BEClient.ScheduleInt();
            ScheduleInt.ApplicationId = ApplicationId;
            ScheduleInt.VacRndId = VacRndId;
            Guid ResultSI = objScheduleIntAction.AddSaveScheduleInt(ScheduleInt, false);
        }

        private void ActiveNextRound(Guid ApplicationId)
        {
            BLClient.PromoteCandidateAction PCAction = new BLClient.PromoteCandidateAction(base.CurrentClient.ClientId);
            //Get Next Round
            Guid VacRndId = PCAction.GetFirstVacRndIdByApplicationId(ApplicationId);
            if (VacRndId != Guid.Empty)
            {
                //Update Promote Candidate
                BEClient.PromoteCandidate ObjPC = new BEClient.PromoteCandidate();
                ObjPC.ApplicationId = ApplicationId;
                ObjPC.VacRndId = VacRndId;
                ObjPC.VacRndId = VacRndId;
                Boolean ResultPC = PCAction.UpdatePromoteCandidate(ObjPC);
            }
        }

        private void UpdatePromoteCandidateForActive(Guid ApplicationId, Guid VacRndId)
        {
            //Update Promote Candidate
            BLClient.PromoteCandidateAction PCAction = new BLClient.PromoteCandidateAction(base.CurrentClient.ClientId);
            BEClient.PromoteCandidate ObjPC = new BEClient.PromoteCandidate();
            ObjPC.ApplicationId = ApplicationId;
            ObjPC.VacRndId = VacRndId;
            ObjPC.VacRndId = VacRndId;
            Boolean ResultPC = PCAction.UpdatePromoteCandidate(ObjPC, null);
        }

        public Boolean CheckForScreeningQue(Guid VacancyId)
        {
            string ScreenRound = GetRndTypeIdByCandidateSelfEvalution();
            if (!string.IsNullOrEmpty(ScreenRound))
            {
                BLClient.QuestionDetailAction _objQuestionDetailAction = new BLClient.QuestionDetailAction(base.CurrentClient.ClientId);
                Boolean TotalScreeningQue = _objQuestionDetailAction.CheckForScreeningQueByVacIdAndRndType(VacancyId, ScreenRound);
                return TotalScreeningQue;
            }
            else
            {
                return false;
            }
        }

        private BEClient.Application GetAppStateByVacId(Guid VacancyId)
        {
            BLClient.ApplicationAction ObjAppAction = new BLClient.ApplicationAction(_CurrentClientId);
            return ObjAppAction.GetAppState(VacancyId, _CurrentUserId);
        }


        #endregion

        [HttpPost]
        public JsonResult GetUploadNewResume(Guid VacancyId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                BLClient.RequiredDocumentAction objRequiredDocAction = new BLClient.RequiredDocumentAction(_CurrentClientId);
                List<BEClient.RequiredDocument> objRequiredDocumentList = objRequiredDocAction.GetRequiredDocumentForScreening(VacancyId);

                BLClient.ATSResumeAction objATSResumeAction = new BLClient.ATSResumeAction(base.CurrentClient.ClientId);
                List<BEClient.ATSResume> objATSResumeList = objATSResumeAction.GetAllDocsByUserId(_CurrentUserId);

                BLClient.ApplicationDocumentsAction objAppDocAction = new BLClient.ApplicationDocumentsAction(_CurrentClientId);
                List<BEClient.ApplicationDocuments> objSelectedAppDocuments = objAppDocAction.GetScreeningDocumentsByVacancyIdAndUserId(VacancyId, _CurrentUserId);

                foreach (var DocumentType in objRequiredDocumentList)
                {
                    DocumentType.ATSResumeList = objATSResumeList.Where(x => x.DocumentTypeId == DocumentType.DocumentTypeId).ToList();
                    if (objSelectedAppDocuments != null && objSelectedAppDocuments.Count != 0)
                    {
                        DocumentType.ATSResumeId = objSelectedAppDocuments.Where(x => x.DocumentTypeId == DocumentType.DocumentTypeId).FirstOrDefault().ATSResumeId;
                    }
                }

                ViewBag.ScreeningQue = CheckForScreeningQue(VacancyId);
                BEClient.ApplicationDocuments objApplicationDocuments = new BEClient.ApplicationDocuments();
                objApplicationDocuments.VacancyId = VacancyId;
                ViewBag.VacancyId = VacancyId;
                objApplicationDocuments.ListRequiredDocuments = objRequiredDocumentList;

                Data = base.RenderRazorViewToString("Shared/_UploadRequiredDocuments", objApplicationDocuments);
            }
            catch (Exception ex)
            {
                return GetJson(base.GetJsonContent(true, null, ex.Message));
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        public List<BEClient.RequiredDocument> GetRequiredDocuments(Guid VacancyId)
        {
            BLClient.RequiredDocumentAction objRequiredDocAction = new BLClient.RequiredDocumentAction(_CurrentClientId);
            List<BEClient.RequiredDocument> objRequiredDocumentList = objRequiredDocAction.GetRequiredDocumentForScreening(VacancyId);
            var objResume = objRequiredDocumentList.Where(m => m.IsResume == true).FirstOrDefault();

            //if (objResume == null)
            //{
            //    objRequiredDocumentList.Add(InsertOptionalResume(VacancyId));
            //}

            BLClient.ATSResumeAction objATSResumeAction = new BLClient.ATSResumeAction(base.CurrentClient.ClientId);
            List<BEClient.ATSResume> objATSResumeList = objATSResumeAction.GetAllDocsByUserId(_CurrentUserId);

            foreach (var DocumentType in objRequiredDocumentList)
            {
                DocumentType.ATSResumeList = objATSResumeList.Where(x => x.DocumentTypeId == DocumentType.DocumentTypeId).ToList();
            }
            return objRequiredDocumentList;
        }

        public BEClient.RequiredDocument InsertOptionalResume(Guid VacancyId)
        {
            BLClient.RequiredDocumentAction objRequiredDocumentAction = new BLClient.RequiredDocumentAction(_CurrentClientId);
            Guid RequiredDocumentId = objRequiredDocumentAction.InsertOptionalResume(VacancyId);
            BLClient.RequiredDocumentAction objRequiredDocAction = new BLClient.RequiredDocumentAction(_CurrentClientId);

            BEClient.RequiredDocument objRequiredDocument = objRequiredDocAction.GetScreeningDocumentById(RequiredDocumentId);
            return objRequiredDocument;
        }

        public bool CheckForApplicationRound(Guid VacancyId)
        {
            BLClient.RndTypeAction objRndTypeAction = new BLClient.RndTypeAction(_CurrentClientId);
            return objRndTypeAction.CheckForApplicationRound(VacancyId);
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
                    SelectList newlist = new SelectList(_objDdlAnsList, "AnsOptId", "DefaultName");
                    _DrpATSPickList.AllValues = newlist;
                    _DrpATSPickList.VacQueId = _current.VacQue.VacQueId;
                    _DrpATSPickList.VacRndId = _current.VacRnd.VacancyRoundId;
                    _DrpATSPickList.QueCatId = _current.Question.QueCatId;
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
                    _DrpATSCheckBox.AllValues = new SelectList(_objDdlAnsList, "AnsOptId", "DefaultName");
                    _DrpATSCheckBox.VacQueId = _current.VacQue.VacQueId;
                    _DrpATSCheckBox.VacRndId = _current.VacRnd.VacancyRoundId;
                    _DrpATSCheckBox.QueCatId = _current.Question.QueCatId;
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
                    _DrpATSTextArea.VacQueId = _current.VacQue.VacQueId;
                    _DrpATSTextArea.VacRndId = _current.VacRnd.VacancyRoundId;
                    _DrpATSTextArea.QueCatId = _current.Question.QueCatId;
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
                    _DrpATSTextBox.VacQueId = _current.VacQue.VacQueId;
                    _DrpATSTextBox.VacRndId = _current.VacRnd.VacancyRoundId;
                    _DrpATSTextBox.QueCatId = _current.Question.QueCatId;
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
                    SelectList newlist = new SelectList(_objDdlAnsList, "AnsOptId", "DefaultName");
                    _DrpyesNoQue.AllValues = newlist;
                    _DrpyesNoQue.VacQueId = _current.VacQue.VacQueId;
                    _DrpyesNoQue.VacRndId = _current.VacRnd.VacancyRoundId;
                    _DrpyesNoQue.QueCatId = _current.Question.QueCatId;
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
                    _DrpATSScale.VacQueId = _current.VacQue.VacQueId;
                    _DrpATSScale.VacRndId = _current.VacRnd.VacancyRoundId;
                    _DrpATSScale.QueCatId = _current.Question.QueCatId;
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
                    if (_current.AppAnswer.Answer == null || _current.AppAnswer.Answer == string.Empty)
                    {
                        _current.AppAnswer.Answer = 0;
                    }
                    _DrpATSScale.Answer = Convert.ToInt32(_current.AppAnswer.Answer);
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

        #region SkipUploadResume
        //public JsonResult GetSkipResume(string VacancyId)
        //{
        //    bool IsError = false;
        //    string Message = string.Empty;
        //    String Data = string.Empty;
        //    try
        //    {
        //        BLClient.ProfileAction ObjProfileAction = new BLClient.ProfileAction(_CurrentClientId);
        //        BEClient.CandidateProfile ObjCandidateProfile = ObjProfileAction.GetCandidateLastUpdatedProfile(_CurrentUserId);
        //        if (ObjCandidateProfile != null)
        //        {
        //            ATS.WebUi.Models.BreadScrumbModel<ResumeList> objResumeLst = new BreadScrumbModel<ResumeList>();
        //            objResumeLst.obj = new ResumeList();
        //            objResumeLst.obj.objATSResume = ObjCandidateProfile.objATSResume;
        //            JsonResult Json1 = new JsonResult();
        //            JsonModels objJson1 = new JsonModels();
        //            Json1 = ApplyJob(objResumeLst, new Guid(VacancyId));
        //            System.Reflection.PropertyInfo pi = Json1.Data.GetType().GetProperty("Data");
        //            Data = (String)(pi.GetValue(Json1.Data, null));
        //        }
        //        else
        //        {
        //            //If No Profile Found  
        //            ObjCandidateProfile = new BEClient.CandidateProfile();
        //            BEClient.Profile objprofile = new BEClient.Profile();
        //            objprofile.ProfileName = "default blank resume";
        //            objprofile.ProfileId = Guid.Empty;

        //            string _serverFilePath = string.Empty;
        //            string _newFileName = string.Empty;
        //            string _oldFileName = string.Empty;
        //            string _extension = string.Empty;
        //            Guid _DocumentId = Guid.Empty;
        //            Guid _profileId = Guid.Empty;
        //            _extension = ".doc";

        //            _oldFileName = "Default_Blank_Resume.doc";
        //            _newFileName = Common.CommonFunctions.GetGuidInStringFormat(Guid.NewGuid()) + _extension;
        //            _serverFilePath = Path.Combine(Common.Constants.STR_RESUME_PATH, _newFileName);
        //            string _resumePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(Common.Constants.STR_RESUME_PATH), _newFileName);
        //            System.IO.File.Copy(Server.MapPath("~/Resume/Sample/Default_Blank_Resume.doc"), _resumePath);

        //            BLClient.ProfileAction _objProfileAction = new BLClient.ProfileAction(_CurrentClientId);
        //            BEClient.ATSResume ObjAtsResume = new BEClient.ATSResume();
        //            objprofile.UserId = _CurrentUserId;
        //            objprofile.IsDefault = true;

        //            _profileId = _objProfileAction.AddProfile(objprofile);
        //            ObjAtsResume = new BEClient.ATSResume();
        //            ObjAtsResume.ProfileId = _profileId;
        //            ObjAtsResume.UserId = objprofile.UserId;
        //            ObjAtsResume.UploadedFileName = _oldFileName;
        //            ObjAtsResume.NewFileName = _newFileName;
        //            ObjAtsResume.Path = _serverFilePath;
        //            ObjCandidateProfile.objATSResume = ObjAtsResume;
        //            ObjCandidateProfile.objProfile = objprofile;

        //            ObjCandidateProfile = InitializeCandidateProfile(ref ObjCandidateProfile);
        //            BLClient.CandidateProfileAction _objCandidateProfileAction = new BLClient.CandidateProfileAction(_CurrentClientId);
        //            bool result = _objCandidateProfileAction.AddCandidateProfile(ObjCandidateProfile, _profileId, objprofile.UserId);
        //            BEClient.ATSResume objAtsresume = new BEClient.ATSResume();
        //            objATSResumeAction = new BLClient.ATSResumeAction(_CurrentClientId);
        //            objAtsresume = objATSResumeAction.GetATSResumeByUserAndProfile(objprofile.UserId, _profileId);

        //            if (result)
        //            {
        //                ATS.WebUi.Models.BreadScrumbModel<ResumeList> objResumeLst = new BreadScrumbModel<ResumeList>();
        //                objResumeLst.obj = new ResumeList();
        //                objResumeLst.obj.objATSResume = objAtsresume;
        //                JsonResult Json1 = new JsonResult();
        //                JsonModels objJson1 = new JsonModels();
        //                Json1 = ApplyJob(objResumeLst, new Guid(VacancyId));
        //                System.Reflection.PropertyInfo pi = Json1.Data.GetType().GetProperty("Data");
        //                Data = (String)(pi.GetValue(Json1.Data, null));
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        IsError = true;
        //        Message = ex.Message;
        //    }
        //    return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        //}

        //private BEClient.CandidateProfile InitializeCandidateProfile(ref BEClient.CandidateProfile ObjCandidateProfile)
        //{
        //    ObjCandidateProfile.ObjAvailability = new BEClient.Availability();
        //    ObjCandidateProfile.ObjContactAchievement = new List<BEClient.Achievement>();
        //    ObjCandidateProfile.ObjContactAssociations = new List<BEClient.Associations>();
        //    ObjCandidateProfile.ObjContactEmployments = new List<BEClient.EmploymentHistory>();
        //    ObjCandidateProfile.ObjContactEducations = new List<BEClient.EducationHistory>();
        //    ObjCandidateProfile.ObjContactLanguages = new List<BEClient.Languages>();
        //    ObjCandidateProfile.ObjContactLicenceAndCertifications = new List<BEClient.LicenceAndCertifications>();
        //    ObjCandidateProfile.ObjContactPublicationHistory = new List<BEClient.PublicationHistory>();
        //    ObjCandidateProfile.ObjContactReferences = new List<BEClient.References>();
        //    ObjCandidateProfile.ObjContactSkills = new List<BEClient.Skills>();
        //    ObjCandidateProfile.ObjContactSpeakingEventHistory = new List<BEClient.SpeakingEventHistory>();
        //    ObjCandidateProfile.ObjExecutiveSummary = new BEClient.ExecutiveSummary();
        //    ObjCandidateProfile.ObjObjective = new BEClient.Objective();
        //    ObjCandidateProfile.objUserDetails = new BEClient.UserDetails();
        //    ObjCandidateProfile.objUserDetails.FirstName = Common.CurrentSession.Instance.VerifiedUser.ObjUserDetails.FirstName;
        //    ObjCandidateProfile.objUserDetails.LastName = Common.CurrentSession.Instance.VerifiedUser.ObjUserDetails.FirstName;
        //    return ObjCandidateProfile;
        //}
        #endregion

        #region Back Functionality
        [HttpGet]
        public JsonResult BackToUploadDocuments(Guid VacancyId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = "";
            try
            {
                BEClient.Vacancy objVacancy = new BEClient.Vacancy();
                BLClient.VacancyAction ObjVacAction = new BLClient.VacancyAction(CurrentClient.ClientId);
                objVacancy = ObjVacAction.GetVacancyById(VacancyId, CurrentClient.CurrentLanguageId);
                objVacancy.ShowOnWebBenefits = false;
                objVacancy.ShowOnWebDuties = false;
                objVacancy.ShowOnWebJobDescription = false;
                objVacancy.ShowOnWebSkills = false;

                BEClient.Application objApplication = new BEClient.Application();
                BLClient.ApplicationAction ApplicationAction = new BLClient.ApplicationAction(_CurrentClientId);
                objApplication = ApplicationAction.GetResumeAndCLByVacIdAndUserId(VacancyId, _CurrentUserId);

                BLClient.ATSResumeAction objATSResumeAction = new BLClient.ATSResumeAction(base.CurrentClient.ClientId);
                List<BEClient.ATSResume> objATSResumeList = objATSResumeAction.GetAllProfile(_CurrentUserId);

                BEClient.ATSResume objATSRes = new BEClient.ATSResume();
                objATSRes.ATSResumeId = Guid.Empty;
                objATSRes.UploadedFileName = "-----Select-----";
                objATSResumeList.Insert(0, objATSRes);
                ViewBag.Resumes = new SelectList(objATSResumeList, "ATSResumeId", "UploadedFileName", objApplication.ATSResumeId);

                List<BEClient.ATSResume> objATSCoverLetterList = objATSResumeAction.GetAllCoverLetter(_CurrentUserId);
                objATSCoverLetterList.Insert(0, objATSRes);
                ViewBag.CoverLetters = new SelectList(objATSCoverLetterList, "ATSResumeId", "UploadedFileName", objApplication.ATSCoverLetterId);
                ViewBag.CAStep2Mode = "Update";
                Data = base.RenderRazorViewToString("Shared/CAStep2", objVacancy);
            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetSubmittedQuestions(Guid VacancyId, string Direction = "Back")
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = "";
            try
            {
                BLClient.ApplicationAction ObjAppAction = new BLClient.ApplicationAction(_CurrentClientId);
                Guid ApplicationId = ObjAppAction.GetApplicationIdByVacIdAndUserId(VacancyId, _CurrentUserId);
                JobApplication<RootModels.BreadScrumbModel<BEClient.CandidateApplications>> _jobAppWithQue = new JobApplication<BreadScrumbModel<BEClient.CandidateApplications>>();
                BLClient.AppQueAnsAction _AppQueAnsAction = new BLClient.AppQueAnsAction(_CurrentClientId);
                string ScreeningRound = GetRndTypeIdByCandidateSelfEvalution();
                List<BEClient.AppQueAns> _objAppQueAns = new List<BEClient.AppQueAns>();

                if (!string.IsNullOrEmpty(ScreeningRound))
                {
                    _objAppQueAns = _AppQueAnsAction.GetAppQueByApplicationId(ApplicationId, ScreeningRound);
                }

                if (_objAppQueAns != null && _objAppQueAns.Count != 0)
                {
                    _jobAppWithQue.ListPickList = GetSubmitPickList(_objAppQueAns.Where(x => x.Question.QueDataType.Equals(1)).ToList());
                    _jobAppWithQue.ListCheckList = GetSubmitCheckList(_objAppQueAns.Where(x => x.Question.QueDataType.Equals(2)).ToList());
                    _jobAppWithQue.ListATSTextArea = GetSubmitTextArea(_objAppQueAns.Where(x => x.Question.QueDataType.Equals(3)).ToList());
                    _jobAppWithQue.ListATSTextBox = GetSubmitTextBox(_objAppQueAns.Where(x => x.Question.QueDataType.Equals(4)).ToList());
                    _jobAppWithQue.ListATSYesNo = GetSubmitYesNo(_objAppQueAns.Where(x => x.Question.QueDataType.Equals(5)).ToList());
                    _jobAppWithQue.ListATSScale = GetSubmitScale(_objAppQueAns.Where(x => x.Question.QueDataType.Equals(6)).ToList());
                    _jobAppWithQue.ATSQueCommon = new RootModels.Question.ATSQuestionCommon();
                    _jobAppWithQue.ATSQueCommon.QueCatList = _objAppQueAns.Select(x => new Tuple<Guid, string>(x.Question.QueCatId, x.Question.QueCatLocalName)).Distinct().ToArray().ToList();
                    _jobAppWithQue.mainObject = new BreadScrumbModel<BEClient.CandidateApplications>();
                    _jobAppWithQue.mainObject.obj = new BEClient.CandidateApplications();
                    _jobAppWithQue.mainObject.obj.ApplicationId = ApplicationId;
                    ViewBag.ApplicationStatus = ATS.BusinessEntity.Common.ApplicationStatusOptionsConstant.APP_STAT_SUBMIT;
                    _jobAppWithQue.mainObject.obj.ApplicationStatus = ATS.BusinessEntity.Common.ApplicationStatusOptionsConstant.APP_STAT_SUBMIT;
                    Data = base.RenderRazorViewToString("Shared/CAStep3", _jobAppWithQue);
                }
                else
                {
                    if (Direction == "Back")
                    {
                        ViewBag.ScreenQue = 0;
                        ViewBag.ScreeningQue = false;
                        JsonResult JsonStep2 = new JsonResult();
                        JsonStep2 = BackToUploadDocuments(VacancyId);
                        System.Reflection.PropertyInfo pi = JsonStep2.Data.GetType().GetProperty("Data");
                        Data = (String)(pi.GetValue(JsonStep2.Data, null));
                    }
                    else
                    {
                        ViewBag.StepNo = "3";
                        Data = base.RenderRazorViewToString("Shared/CAStep4", null);
                    }
                }
            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.ToString();
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }
        #endregion
        public void SendCandidateSurveyMail(Guid ApplicationId, Guid VacRndId)
        {
            try
            {
                string encruserid = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(Convert.ToString(ATSCommon.CurrentSession.Instance.VerifiedUser.UserId));
                string surveyLink = "http://" + Common.ConfigurationMapped.Instance.DomianName;
                surveyLink = surveyLink + @Url.Action("Index", "CandidateSurvey", new { ApplicationId = ApplicationId, pVacRndId = VacRndId, UserId = encruserid });
                BLClient.EmailTemplatesAction EmailTemplateAction = new BLClient.EmailTemplatesAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                BEClient.EmailTemplates objEmailTemplate = EmailTemplateAction.GetEmailTemplateByEmailIdentifier(Common.CurrentSession.Instance.VerifiedClient.CurrentLanguageId, BEClient.EmailTemplateIdentifier.Candidate_Survey.ToString());

                BLClient.EmailContentAction objEmailContentAction = new BLClient.EmailContentAction(_CurrentClientId);
                objEmailTemplate = objEmailContentAction.GetEmailContentByAppIdAndVacRndId(objEmailTemplate, ApplicationId, VacRndId);
                objEmailTemplate.EmailDescription = objEmailTemplate.EmailDescription.Replace("##Link.CandidateSurvey##", "<a href =" + surveyLink + ">Click Here</a>");
                Mailers.EmailTemplate mvcMailer = new Mailers.EmailTemplate();
                mvcMailer.SendMessage(Common.CurrentSession.Instance.VerifiedUser.Username, objEmailTemplate.Subject, objEmailTemplate.EmailDescription);
            }
            catch
            {
                throw;
            }
        }
    }
}