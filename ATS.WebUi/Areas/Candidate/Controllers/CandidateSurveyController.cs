using System;
using System.Collections.Generic;
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
using ATS.WebUi.Models;
using System.Text.RegularExpressions;

namespace ATS.WebUi.Areas.Candidate.Controllers
{
    public class CandidateSurveyController : ATS.WebUi.Controllers.AreaBaseController
    {
        //
        // GET: /Candidate/CandidateSurvey/

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
                    filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_CANDIDATE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_CANDIDATE_CONTROLLER, new { area = ATSCommon.Constants.AREA_EMPLOYEE }));
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

        public ActionResult Index(Guid ApplicationId, Guid pVacRndId, string UserId)
        {
            Guid _UserId = Guid.Empty;

            JobApplication<RootModels.BreadScrumbModel<BEClient.CandidateApplications>> _jobAppWithQue = new JobApplication<BreadScrumbModel<BEClient.CandidateApplications>>();
            try
            {
                if (!String.IsNullOrEmpty(UserId))
                {
                    Guid.TryParse(HelperLibrary.Encryption.EncryptionAlgo.DecryptData(UserId), out _UserId);
                    if (_UserId != ATSCommon.CurrentSession.Instance.VerifiedUser.UserId)
                    {
                        return RedirectToAction(ATSCommon.Constants.DEFAULT_REDIRECT_CANDIDATE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_CANDIDATE_CONTROLLER, new { area = ATSCommon.Constants.AREA_CANDIDATE });
                    }
                    if (ApplicationId != Guid.Empty)
                    {
                        BEClient.CandidateSurvey objCandidateSurvey = new BEClient.CandidateSurvey();
                        BLClient.CandidateSurveyAction objCandidateSurveyAction = new BLClient.CandidateSurveyAction(_CurrentClientId);
                        BLClient.AppQueAnsAction _AppQueAnsAction = new BLClient.AppQueAnsAction(_CurrentClientId);
                        List<BEClient.CandidateSurvey> lstCandidateSurvey = new List<BEClient.CandidateSurvey>();
                        lstCandidateSurvey = objCandidateSurveyAction.CheckCandidateSurvey(ApplicationId);


                        //string LstRndTypeId = GetCandidateSurveyRndTypeId();
                        string LstRndTypeId = lstCandidateSurvey.Where(x => x.VacRndId == pVacRndId).Select(x => x.RndTypeId.ToString()).FirstOrDefault();
                        //objCandidateSurvey = objCandidateSurveyAction.CheckCandidateSurvey(ApplicationId);
                        if (!String.IsNullOrEmpty(LstRndTypeId))
                        {
                            List<BEClient.AppQueAns> _objAppQueAns = new List<BEClient.AppQueAns>();
                            _objAppQueAns = _AppQueAnsAction.GetAppQueByApplicationId(ApplicationId, LstRndTypeId);

                            BLClient.QuestionDetailAction _objQuestionDetailAction = new BLClient.QuestionDetailAction(base.CurrentClient.ClientId);
                            List<BEClient.QuestionDetails> _objQuestionDetails = new List<BEClient.QuestionDetails>();

                            if (_objAppQueAns != null && _objAppQueAns.Count() > 0)
                            {
                                _objQuestionDetails = _objQuestionDetailAction.GetQuestionsWithAnswers(ApplicationId, LstRndTypeId);
                                ViewBag.ApplicationStatus = "APP_STAT_SUBMIT";
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

                            }

                            else
                            {
                                _objQuestionDetails = _objQuestionDetailAction.GetQuestionDetails(ApplicationId, LstRndTypeId);

                                if (_objQuestionDetails.Count == 0)
                                {
                                    Guid VacRndId = objCandidateSurvey.VacRndId;
                                    // InsertScheduleInt(ApplicationId, VacRndId);
                                    //UpdatePromoteCandidateForActive(ApplicationId, VacRndId);
                                    _jobAppWithQue.mainObject = new BreadScrumbModel<BEClient.CandidateApplications>();
                                    _jobAppWithQue.mainObject.obj = new BEClient.CandidateApplications();
                                    _jobAppWithQue.mainObject.obj.ApplicationId = ApplicationId;
                                }
                                else
                                {
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

                                }
                                ViewBag.ApplicationStatus = "APP_STAT_DRAFT";
                            }



                        }

                    }
                }
                _jobAppWithQue.mainObject.obj.VacRndId = pVacRndId;
                NavIndex();
                return View(_jobAppWithQue);
            }
            catch { throw; }
        }

        [HttpPost]
        public ActionResult SubmitCandidateSurvey(ATS.WebUi.Areas.Candidate.Models.JobApplication<RootModels.BreadScrumbModel<BEClient.CandidateApplications>> objJobApplication)
        {
            bool IsError = false;
            String Data = string.Empty;
            String Message = String.Empty;
            try
            {
                Guid ApplicationId = objJobApplication.mainObject.obj.ApplicationId;
                //BEClient.Application ObjAppState = GetAppStateByAppId(ApplicationId);
                bool NeedToPromote = true;
                InsertAppRnd(objJobApplication, ApplicationId, NeedToPromote);
                BLClient.ApplicationAction _ObjApplicationAction = new BLClient.ApplicationAction(_CurrentClientId);
                bool Result = _ObjApplicationAction.ChangeApplicationStatus(ApplicationId, BECommon.ApplicationStatusOptionsConstant.APP_STAT_SUBMIT);
                BLClient.VacancyOfferAction ObjVacancyOfferAction = new BLClient.VacancyOfferAction(_CurrentClientId);
                //bool IsSendApplyEmail = ObjVacancyOfferAction.IsSendApplyEmail(ApplicationId);

                //***** Send Apply Email to Candidate ***///////
                SendCandidateSurveyCompletedMail(ApplicationId, objJobApplication.mainObject.obj.VacRndId);
                ////*********** END Send Applly Email *****/////

                //return RedirectToAction("Index", "MyApplications", new { area = ATS.WebUi.Common.Constants.AREA_CANDIDATE, Status = "All" });
            }
            catch (Exception ex)
            {
                IsError = true;
                Message = "Not able to Insert";

            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

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

        public string GetCandidateSurveyRndTypeId()
        {
            BLClient.RndTypeAction _objRndTypeAction = new BLClient.RndTypeAction(_CurrentClientId);
            List<Guid> lstRndType = _objRndTypeAction.GetCandidateSurveyRndTypeId();
            String lstRndTypeStr = String.Join(",", lstRndType.Select(g => g.ToString()).ToArray());
            return lstRndTypeStr;
        }

        public void SendCandidateSurveyCompletedMail(Guid ApplicationId, Guid VacRndId)
        {
            try
            {

                BLClient.EmailTemplatesAction EmailTemplateAction = new BLClient.EmailTemplatesAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                // BEClient.EmailTemplates objEmailTemplate = EmailTemplateAction.GetEmailTemplateByEmailIdentifier(Common.CurrentSession.Instance.VerifiedClient.CurrentLanguageId, BEClient.EmailTemplateIdentifier.Candidate_Survey_Completed.ToString());
                BEClient.EmailTemplates objEmailTemplate = EmailTemplateAction.GetEmailTemplateByEmailIdentifier(Common.CurrentSession.Instance.VerifiedClient.CurrentLanguageId, BEClient.EmailTemplateIdentifier.Candidate_Survey_Completed.ToString());

                BLClient.EmailContentAction objEmailContentAction = new BLClient.EmailContentAction(_CurrentClientId);
                //objEmailTemplate = objEmailContentAction.GetEmailContentByAppIdAndVacRndId(objEmailTemplate, ApplicationId, VacRndId);
                objEmailTemplate = EmailTemplateAction.GetEmailTemplateByEmailIdentifier(_CurrentLanguageId, BEClient.EmailTemplateIdentifier.Candidate_Survey_Completed.ToString());
                BLClient.CandidateEmployeeAction objCandidateEmployeeAction = new BLClient.CandidateEmployeeAction(_CurrentClientId);
                List<BEClient.CandidateEmployee> objCandidateEmployee = new List<BEClient.CandidateEmployee>();
                objCandidateEmployee = objCandidateEmployeeAction.GetReviewerUserNamesByVacRndId(VacRndId, ApplicationId);
                string _EmailDescription = objEmailTemplate.EmailDescription;


                foreach (var item in objCandidateEmployee)
                {
                    string encruserid = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(item.UserId.ToString());
                    string surveyLink = "http://" + Common.ConfigurationMapped.Instance.DomianName;
                    surveyLink = surveyLink + @Url.Action("GetCandidateProfile", "MyCandidates", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, ApplicationId = ApplicationId, UserId = encruserid });
                    string url = "<a href = " + surveyLink + ">Click here</a>";
                    objEmailTemplate.EmailDescription = _EmailDescription;


                    objEmailTemplate.EmailDescription = Regex.Replace(objEmailTemplate.EmailDescription, "##Manager.FirstName##", item.ManagerFirstName, RegexOptions.IgnoreCase);
                    objEmailTemplate.EmailDescription = Regex.Replace(objEmailTemplate.EmailDescription, "##Candidate.FullName##", item.CandidateFirstName + " " + item.CandidateLastName, RegexOptions.IgnoreCase);
                    objEmailTemplate.EmailDescription = Regex.Replace(objEmailTemplate.EmailDescription, "##Vacancy.Round.Score##", item.RndScore.ToString(), RegexOptions.IgnoreCase);
                    objEmailTemplate.EmailDescription = Regex.Replace(objEmailTemplate.EmailDescription, "##Link.CandidateApplication##", url, RegexOptions.IgnoreCase);
                    objEmailTemplate.EmailDescription = Regex.Replace(objEmailTemplate.EmailDescription, "##Offer.CompanyName##", ATSCommon.CurrentSession.Instance.VerifiedClient.Clientname, RegexOptions.IgnoreCase);
                    objEmailTemplate.Subject = Regex.Replace(objEmailTemplate.Subject, "##Candidate.FullName##", item.CandidateFirstName + " " + item.CandidateLastName, RegexOptions.IgnoreCase);
                    Mailers.EmailTemplate mvcMailer = new Mailers.EmailTemplate();
                    mvcMailer.SendMessage(item.Username, objEmailTemplate.Subject, objEmailTemplate.EmailDescription);
                }
            }
            catch
            {
                throw;
            }
        }
        //public string GetReviewerUserNamesByVacRndId(Guid VacRndId)
        //{
        //    BLClient.UserAction _objUserAction = new BLClient.UserAction(_CurrentClientId);
        //    List<BEClient.User> lstRev = _objUserAction.GetReviewerUserNamesByVacRndId(VacRndId);
        //    String lstRevStr = String.Join(",", lstRev.Select(g => g.Username.ToString()).ToArray());
        //    return lstRevStr;
        //}
    }
}
