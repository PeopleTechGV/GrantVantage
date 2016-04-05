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
using ATS.WebUi.Areas.Employee.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.Web.UI;
using iTextSharp.text.html.simpleparser;
using iTextSharp.tool.xml.pipeline.html;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.parser;
using iTextSharp.tool.xml.pipeline.end;
using System.Text;

namespace ATS.WebUi.Areas.Employee.Controllers
{
    public class CandidateDetailsController : ATS.WebUi.Controllers.AreaBaseController
    {
        #region Private Members
        private BLClient.VacancyAction _objVacancyAction;
        private BLClient.ResumeAction _objResumeAction;
        private BEClient.Vacancy _objVacancy;
        private BEClient.Resume _objResume;
        private BEClient.Resume _objResumeList;
        BLCommon.DrpStringMappingAction _DrpdownStringMappingAction;
        private Guid _UserId;
        private Guid _CurrentLanguageId;
        private Guid _CurrentClientId;
        private static readonly log4net.ILog log = LogManager.GetLogger(
        System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private BLClient.QueCatAction _QueCatAction;
        #endregion
        //
        // GET: /Employee/CandidateDetails/

        #region Initialize Method call on every request
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.ActionName.ToLower() != "index")
            {
                base.OnAuthorization(filterContext);
                if (base.CurrentClient != null && ATS.WebUi.Common.CurrentSession.Instance.VerifiedUser != null)
                {
                    _objResumeAction = new BLClient.ResumeAction(base.CurrentClient.ClientId, true);
                    _CurrentClientId = base.CurrentClient.ClientId;
                    _UserId = ATSCommon.CurrentSession.Instance.UserId;
                    _CurrentLanguageId = base.CurrentClient.CurrentLanguageId;
                }
            }
        }

        #endregion

        public ActionResult Index(Guid ApplicationId, string ClientName, Guid UserId, bool ShowHeader = false)
        {
            BEMAster.Client objClient = BusinessLogic.Common.LoginOperation.CreateClientSession(ClientName);
            if (objClient == null)
            {
                return null;
            }
            else
            {
                new ATSCommon.CurrentSession(objClient);
                _CurrentClientId = objClient.ClientId;
                _CurrentLanguageId = objClient.CurrentLanguageId;
            }
            string Data = string.Empty;
            try
            {
                BLClient.ApplicationAction _objApplicationAction = new BLClient.ApplicationAction(_CurrentClientId);
                BEClient.Application _objApplication = _objApplicationAction.GetApplicationByApplicationId(ApplicationId);
                BEClient.CandidateProfileForPDF ObjCandidatProfile = new BEClient.CandidateProfileForPDF();
                UserId = _objApplication.UserId;

                if (_objApplication != null)
                {
                    BEClient.ATSResume _objATSResume = new BEClient.ATSResume();
                    BLClient.ATSResumeAction _objATSResumeAction = new BLClient.ATSResumeAction(_CurrentClientId);
                    _objATSResume = _objATSResumeAction.GetRecordByRecorId(_objApplication.ATSResumeId);
                    //Get All Application by Current Profile
                    List<BEClient.Application> _objListApplication = _objApplicationAction.GetSubmittedApplicationByProfile(_objATSResume.ProfileId, _CurrentLanguageId);
                    _objListApplication = _objListApplication.Where(x => x.AppState != "Draft").ToList();
                    foreach (BEClient.Application _curr in _objListApplication)
                    {
                        BLClient.GetScoreAction _objGetScoreAction = new BLClient.GetScoreAction(_CurrentClientId);
                        _curr.objGetScore = _objGetScoreAction.GetApplicationScore(_curr.ApplicationId);
                        BLClient.VacancyRoundAction objVacancyRoundAction = new BLClient.VacancyRoundAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                        _curr.objVacRndList = objVacancyRoundAction.GetAllRound(_curr.VacancyId, _CurrentLanguageId, _curr.ApplicationId, false);
                        List<BEClient.ApplicationHistory> objApplicationHistoryList = new List<BEClient.ApplicationHistory>();
                        BLClient.ApplicationHistoryAction objApplicationHistoryAction = new BLClient.ApplicationHistoryAction(_CurrentClientId);
                        objApplicationHistoryList = objApplicationHistoryAction.GetApplicationHistoryByApplicationId(_curr.ApplicationId, _CurrentLanguageId);
                        String LstRndTypeId = GetRndTypeIdByCandidateSelfEvalution();

                        ///**********Application Questions************/
                        //ATS.WebUi.Areas.Candidate.Models.JobApplication<RootModels.BreadScrumbModel<BEClient.CandidateApplications>> _jobAppWithQue = new ATS.WebUi.Areas.Candidate.Models.JobApplication<RootModels.BreadScrumbModel<BEClient.CandidateApplications>>();
                        //BLClient.AppQueAnsAction _AppQueAnsAction = new BLClient.AppQueAnsAction(_CurrentClientId);
                        //List<BEClient.AppQueAns> _objAppQueAns = _AppQueAnsAction.GetAppQueByApplicationId(ApplicationId, LstRndTypeId);
                        //if (_objAppQueAns.Count > 0)
                        //{
                        //    ApplicationController objAppController = new ApplicationController();
                        //    _jobAppWithQue.ListPickList = objAppController.GetSubmitPickList(_objAppQueAns.Where(x => x.Question.QueDataType.Equals(1)).ToList());
                        //    _jobAppWithQue.ListCheckList = objAppController.GetSubmitCheckList(_objAppQueAns.Where(x => x.Question.QueDataType.Equals(2)).ToList());
                        //    _jobAppWithQue.ListATSTextArea = objAppController.GetSubmitTextArea(_objAppQueAns.Where(x => x.Question.QueDataType.Equals(3)).ToList());
                        //    _jobAppWithQue.ListATSTextBox = objAppController.GetSubmitTextBox(_objAppQueAns.Where(x => x.Question.QueDataType.Equals(4)).ToList());
                        //    _jobAppWithQue.ListATSYesNo = objAppController.GetSubmitYesNo(_objAppQueAns.Where(x => x.Question.QueDataType.Equals(5)).ToList());
                        //    _jobAppWithQue.ListATSScale = objAppController.GetSubmitScale(_objAppQueAns.Where(x => x.Question.QueDataType.Equals(6)).ToList());
                        //    _jobAppWithQue.ATSQueCommon = new RootModels.Question.ATSQuestionCommon();
                        //    _jobAppWithQue.ATSQueCommon.QueCatList = _objAppQueAns.Select(x => new Tuple<Guid, string>(x.Question.QueCatId, x.Question.QueCatLocalName)).Distinct().ToArray().ToList();
                        //}
                        //_curr.objApplicationQuestionsList = _jobAppWithQue;

                        /**********Application Questions************/

                        foreach (BEClient.VacancyRound objintpro in _curr.objVacRndList)
                        {
                            BLClient.InterViewProcessAction InterViewProcessActionObject = new BLClient.InterViewProcessAction(_CurrentClientId);
                            if (objintpro.RoundAttributeNo == (int)BEClient.RndAttrNo.OfferRound)
                            {
                                List<BEClient.VacancyOffer> objVacancyOffer = new List<BEClient.VacancyOffer>();
                                BLClient.VacancyOfferAction objVacancyOfferAction = new BLClient.VacancyOfferAction(_CurrentClientId);

                                //Anand : for vacancy Offer in Pdf
                                objintpro.ObjVacancyOffer = objVacancyOfferAction.GetVacancyOfferByApplicationId(_curr.ApplicationId, _CurrentLanguageId);
                                List<BEClient.VacancyOffer> innerList = new List<BEClient.VacancyOffer>();
                                foreach (BEClient.VacancyOffer objinnerOffer in objintpro.ObjVacancyOffer)
                                {
                                    //Removed Draft Offer
                                    if (objinnerOffer.OfferStatusId != Convert.ToInt32(ATS.BusinessEntity.VacancyOfferStatus.Draft))
                                    {
                                        BEClient.VacancyOffer objVacOffer = new BEClient.VacancyOffer();
                                        objVacancyOfferAction = new BLClient.VacancyOfferAction(_CurrentClientId);
                                        objVacOffer = objVacancyOfferAction.GetVacancyOfferByIdForPDF(objinnerOffer.VacancyOfferId, Guid.Empty, _CurrentLanguageId);
                                        objVacOffer.objApplicationHistory = objApplicationHistoryList;
                                        innerList.Add(objVacOffer);
                                    }
                                }
                                objintpro.ObjVacancyOffer = innerList;
                            }
                            objintpro.ObjInterviewProcess = InterViewProcessActionObject.GetQueCatDetailsBtyOrder(objintpro.VacancyRoundId);
                            if (objintpro.ObjInterviewProcess.Count > 0)
                            {
                                foreach (BEClient.InterViewProcess objInterQue in objintpro.ObjInterviewProcess)
                                {
                                    BLClient.InterviewProcessQueAction InterviewProcessQueActionObject = new BLClient.InterviewProcessQueAction(_CurrentClientId);
                                    objInterQue.ObjInterviewProcesssQueList = InterviewProcessQueActionObject.GetQueDetailsBtyOrder(objintpro.VacancyRoundId, objInterQue.ObjQueCat.QueCatId, _curr.ApplicationId, _CurrentLanguageId);
                                    foreach (BEClient.InterviewProcessQue ObjIntQue in objInterQue.ObjInterviewProcesssQueList)
                                    {
                                        BLClient.ReviewersAction ReviewersActionObject = new BLClient.ReviewersAction(_CurrentClientId);
                                        List<BEClient.Reviewers> objReviewers = new List<BEClient.Reviewers>();
                                        ObjIntQue.ObjReviewers = ReviewersActionObject.GetAllReviewersByRoundIdAndQuestionId(objintpro.VacancyRoundId, _CurrentLanguageId, ObjIntQue.VacQueId, _curr.ApplicationId);
                                        foreach (ATS.BusinessEntity.Reviewers ObjReviewers in ObjIntQue.ObjReviewers)
                                        {
                                            BLClient.ScheduleIntAction ScheduleIntAction = new BLClient.ScheduleIntAction(_CurrentClientId);
                                            Guid ScheduleIntId = ScheduleIntAction.GetSchIdByAppAndVacQueAndRMId(_curr.ApplicationId, objintpro.VacancyRoundId);
                                            BLClient.BeginInerviewAction BeginInterviewAction = new BLClient.BeginInerviewAction(_CurrentClientId);
                                            Boolean IsInterviewComplete = BeginInterviewAction.IsInterviewComplete(objintpro.VacancyRoundId, UserId, ScheduleIntId);
                                            BEClient.InterviewProcessQueModel _ObjInterViewProcessQue = new BEClient.InterviewProcessQueModel();
                                            BLClient.InterviewProcessQueAction ObjInterViewProQueAction = new BLClient.InterviewProcessQueAction(_CurrentClientId);
                                            List<BEClient.InterviewProcessQue> obj = ObjInterViewProQueAction.GetQueDetailsBtyOrder(objintpro.VacancyRoundId, ObjIntQue.ObjQue.QueCatId, ApplicationId, _CurrentLanguageId);
                                            if (obj != null)
                                                _ObjInterViewProcessQue.ObjInterviewProcessQue = obj.Where(x => x.ObjQue.QueId.ToString().Equals(ObjIntQue.ObjQue.QueId.ToString())).FirstOrDefault();
                                            BLClient.AppAnswerAction ObjAppAnswerAction = new BLClient.AppAnswerAction(_CurrentClientId);
                                            BEClient.AppAnswer objAppAnswer = new BEClient.AppAnswer();
                                            _ObjInterViewProcessQue.Questions = new List<BEClient.ATSQuestionCommon>();
                                            CreateQuestionWithAns(ref _ObjInterViewProcessQue, ObjReviewers.VacancyReviewMemberId, ScheduleIntId);
                                            ObjReviewers.objInterviewproc = _ObjInterViewProcessQue;
                                        }
                                    }
                                }
                            }
                        }
                       
                        BLClient.ScheduleIntAction _objScheduleIntAction = new BLClient.ScheduleIntAction(_CurrentClientId);
                        List<BEClient.ScheduleInt> _objIntScheduleDetails = _objScheduleIntAction.GetVacRndDetailByApplicationId(_curr.ApplicationId, LstRndTypeId);
                        _curr.ObjScheduleInt = _objIntScheduleDetails;
                        //Application Comments
                        BLClient.AppCommentAction objAppCommnetAction = new BLClient.AppCommentAction(_CurrentClientId);
                        _curr.ObjComment = objAppCommnetAction.GetCommentsByAppId(_curr.ApplicationId);
                        //Candidate Histroy
                        BLClient.CandidateHistoryAction objCandidateHistoryAction = new BLClient.CandidateHistoryAction(_CurrentClientId);
                        _curr.objCandidateHistory = objCandidateHistoryAction.GetCandidateHistoryByApplicationId(_curr.ApplicationId);
                    }
                    ObjCandidatProfile = GetFullCandidateProfile(_objApplication.UserId, _objATSResume.ProfileId, objClient.Clientname);
                    ObjCandidatProfile.ObjApplications = _objListApplication;
                }
                else if (ApplicationId == Guid.Empty)
                {
                    BLClient.ProfileAction _objDefaultProfile = new BLClient.ProfileAction(_CurrentClientId);
                    BEClient.Profile _defualtProfile = _objDefaultProfile.GetLastUpdatedProfileByUserId(UserId);
                    BLClient.ATSResumeAction _objATSResumeAction = new BLClient.ATSResumeAction(_CurrentClientId);
                    ObjCandidatProfile = GetFullCandidateProfile(UserId, _defualtProfile.ProfileId, objClient.Clientname);
                    ObjCandidatProfile.ObjApplications = null;
                }
                ObjCandidatProfile.isShowHeader = ShowHeader;
                return View("_profile", ObjCandidatProfile);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.CandidateProfileForPDF GetFullCandidateProfile(Guid UserId, Guid ProfileId, String ClientName = "")
        {
            BEClient.CandidateProfileForPDF ObjCandidatProfile = new BEClient.CandidateProfileForPDF();
            BLClient.ProfileAction _objProfileAction = new BLClient.ProfileAction(_CurrentClientId);
            BLClient.ApplicationAction _objApplicationAction = new BLClient.ApplicationAction(_CurrentClientId);
            ObjCandidatProfile = new BEClient.CandidateProfileForPDF();
            ObjCandidatProfile = _objProfileAction.GetCanidateFullProfileByUserIdAndProfileIdPDF(UserId, ProfileId);
            ObjCandidatProfile.ObjApplicationCount = _objApplicationAction.GetCountOfSubmittedApplicationByProfile(ProfileId, _CurrentLanguageId);
            CandidateProfileDropdownlist(ClientName);
            ViewBag.EmployeeView = true;
            return ObjCandidatProfile;
        }

        private void CandidateProfileDropdownlist(String ClientName)
        {
            try
            {
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
                _EmploymentPreferenceList = BLClient.Common.CacheHelper.GetDropDownStringMapping(_CurrentClientId, _CurrentLanguageId, ClientName, BEClient.DrpDownFields.JobType.ToString());
                ViewBag._EmploymentPreferenceList = new SelectList(_EmploymentPreferenceList, "ValueField", "TextField");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void CreateQuestionWithAns(ref BEClient.InterviewProcessQueModel _ObjInterViewProcessQue, Guid VacRMId, Guid? ScheduleIntId = null)
        {
            BEClient.InterviewProcessQueModel _CopyObjInterViewProcessQue = _ObjInterViewProcessQue;
            BEClient.AppAnswer objAppAnswer = null;
            BLClient.AppAnswerAction ObjAppAnswerAction = new BLClient.AppAnswerAction(_CurrentClientId);
            Guid _ScheduleIntId = Guid.Empty;
            bool FillAns = false;
            if (Guid.TryParse(ScheduleIntId.ToString(), out _ScheduleIntId) && _ScheduleIntId != Guid.Empty)
            {
                objAppAnswer = ObjAppAnswerAction.GetAppAnswer(_ObjInterViewProcessQue.ObjInterviewProcessQue.VacQueId, VacRMId, _ScheduleIntId);
                if (objAppAnswer != null)
                    FillAns = true;
            }
            switch (_ObjInterViewProcessQue.ObjInterviewProcessQue.ObjQue.QueDataType)
            {
                case 1:
                    BEClient.ATSPickList _DrpATSPickList = new BEClient.ATSPickList();
                    BLClient.AnsOptAction _objDdlAnsOptAction = new BLClient.AnsOptAction(_CurrentClientId);
                    List<BEClient.AnsOpt> _objDdlAnsList = _objDdlAnsOptAction.GetAnsListByQueId(_ObjInterViewProcessQue.ObjInterviewProcessQue.ObjQue.QueId, _CurrentLanguageId);
                    _DrpATSPickList.QuestionText = _ObjInterViewProcessQue.ObjInterviewProcessQue.ObjQue.LocalName;
                    _DrpATSPickList.AllValues = new SelectList(_objDdlAnsList, "AnsOptId", "DefaultName");
                    _DrpATSPickList.VacQueId = _ObjInterViewProcessQue.ObjInterviewProcessQue.VacQueId;
                    _DrpATSPickList.VacRndId = _ObjInterViewProcessQue.ObjInterviewProcessQue.VacRndId;
                    _DrpATSPickList.QuestionDataType = 1;
                    _ObjInterViewProcessQue.Questions.Add(_DrpATSPickList);
                    if (FillAns)
                    {
                        _ObjInterViewProcessQue.Questions.FirstOrDefault().Answer = objAppAnswer.Answer;
                    }
                    break;
                case 2:
                    BEClient.ATSCheckBox _DrpATSCheckBox = new BEClient.ATSCheckBox();
                    BLClient.AnsOptAction _objDdlAnsOpt1Action = new BLClient.AnsOptAction(_CurrentClientId);
                    List<BEClient.AnsOpt> _objDdlAnsList1 = _objDdlAnsOpt1Action.GetAnsListByQueId(_ObjInterViewProcessQue.ObjInterviewProcessQue.ObjQue.QueId, _CurrentLanguageId);
                    _DrpATSCheckBox.QuestionText = _ObjInterViewProcessQue.ObjInterviewProcessQue.ObjQue.LocalName;
                    _DrpATSCheckBox.VacQueId = _ObjInterViewProcessQue.ObjInterviewProcessQue.VacQueId;
                    _DrpATSCheckBox.VacRndId = _ObjInterViewProcessQue.ObjInterviewProcessQue.VacRndId;
                    _DrpATSCheckBox.QuestionDataType = 2;
                    _DrpATSCheckBox.AllValues = new SelectList(_objDdlAnsList1, "AnsOptId", "DefaultName");
                    _ObjInterViewProcessQue.Questions.Add(_DrpATSCheckBox);
                    if (FillAns)
                    {
                        List<BEClient.AnsOpt> _objselectedList = new List<BEClient.AnsOpt>();
                        int i = 0;
                        foreach (var item in objAppAnswer.Answer.ToString().Split(',').ToArray())
                        {
                            if (_objDdlAnsList1.Where(x => x.AnsOptId.ToString().Equals(item.ToString())).Count() > 0)
                            {
                                _objselectedList.Add(_objDdlAnsList1.Where(x => x.AnsOptId.ToString().Equals(item.ToString())).First());
                            }
                            i++;
                        }
                        _DrpATSCheckBox.SelectedList = new SelectList(_objselectedList, "AnsOptId", "DefaultName");
                    }
                    break;
                case 3:
                    BEClient.ATSTextArea _DrpATSTextArea = new BEClient.ATSTextArea();
                    _DrpATSTextArea.QuestionText = _ObjInterViewProcessQue.ObjInterviewProcessQue.ObjQue.LocalName;
                    _DrpATSTextArea.VacQueId = _ObjInterViewProcessQue.ObjInterviewProcessQue.VacQueId;
                    _DrpATSTextArea.VacRndId = _ObjInterViewProcessQue.ObjInterviewProcessQue.VacRndId;
                    _DrpATSTextArea.QuestionDataType = 3;
                    _ObjInterViewProcessQue.Questions.Add(_DrpATSTextArea);
                    if (FillAns)
                    {
                        _ObjInterViewProcessQue.Questions.FirstOrDefault().Answer = objAppAnswer.Answer;
                    }
                    break;
                case 4:
                    BEClient.ATSTextBox _DrpATSTextBox = new BEClient.ATSTextBox();
                    _DrpATSTextBox.QuestionText = _ObjInterViewProcessQue.ObjInterviewProcessQue.ObjQue.LocalName;
                    _DrpATSTextBox.VacQueId = _ObjInterViewProcessQue.ObjInterviewProcessQue.VacQueId;
                    _DrpATSTextBox.VacRndId = _ObjInterViewProcessQue.ObjInterviewProcessQue.VacRndId;
                    _DrpATSTextBox.QuestionDataType = 4;
                    _ObjInterViewProcessQue.Questions.Add(_DrpATSTextBox);
                    if (FillAns)
                    {
                        _ObjInterViewProcessQue.Questions.FirstOrDefault().Answer = objAppAnswer.Answer;
                    }
                    break;
                case 5:
                    BEClient.ATSYesNo _DrpyesNoQue = new BEClient.ATSYesNo();
                    _DrpyesNoQue.QuestionText = _ObjInterViewProcessQue.ObjInterviewProcessQue.ObjQue.LocalName;
                    BLClient.AnsOptAction _objYesNoAnsOptAction = new BLClient.AnsOptAction(_CurrentClientId);
                    List<BEClient.AnsOpt> _objDdlynList = _objYesNoAnsOptAction.GetAnsListByQueId(_ObjInterViewProcessQue.ObjInterviewProcessQue.ObjQue.QueId, _CurrentLanguageId);
                    _DrpyesNoQue.AllValues = new SelectList(_objDdlynList, "AnsOptId", "DefaultName");
                    //TODO:CHECK
                    _DrpyesNoQue.VacQueId = _ObjInterViewProcessQue.ObjInterviewProcessQue.VacQueId;
                    _DrpyesNoQue.VacRndId = _ObjInterViewProcessQue.ObjInterviewProcessQue.VacRndId;
                    _DrpyesNoQue.QuestionDataType = 5;
                    _ObjInterViewProcessQue.Questions.Add(_DrpyesNoQue);
                    if (FillAns)
                    {
                        _ObjInterViewProcessQue.Questions.FirstOrDefault().Answer = objAppAnswer.Answer;
                    }
                    break;
                case 6:
                    BEClient.ATSScale _DrpATSScale = new BEClient.ATSScale();
                    _DrpATSScale.QuestionText = _ObjInterViewProcessQue.ObjInterviewProcessQue.ObjQue.LocalName;
                    _DrpATSScale.VacQueId = _ObjInterViewProcessQue.ObjInterviewProcessQue.VacQueId;
                    _DrpATSScale.VacRndId = _ObjInterViewProcessQue.ObjInterviewProcessQue.VacRndId;
                    _DrpATSScale.QuestionDataType = 6;
                    _ObjInterViewProcessQue.Questions.Add(_DrpATSScale);
                    if (FillAns)
                    {
                        _ObjInterViewProcessQue.Questions.FirstOrDefault().Answer = objAppAnswer.Answer;
                    }
                    break;
            }
            if (FillAns)
            {
                _ObjInterViewProcessQue.Questions.FirstOrDefault().Value = objAppAnswer.Value;
                _ObjInterViewProcessQue.Questions.FirstOrDefault().Comment = objAppAnswer.Comment;
                _ObjInterViewProcessQue.Questions.FirstOrDefault().AppAnsId = objAppAnswer.AppAnsId;
            }
            else
            {
                _ObjInterViewProcessQue.Questions.FirstOrDefault().Value = 0;
            }
            _ObjInterViewProcessQue.Questions.FirstOrDefault().ScheduleIntId = (Guid)ScheduleIntId;
            List<BEClient.BindEnumDropDown> _QuestionTypeList = new List<BEClient.BindEnumDropDown>();
            foreach (BEClient.QuestionType r in Enum.GetValues(typeof(BEClient.QuestionType)))
            {
                _QuestionTypeList.Add(new BEClient.BindEnumDropDown() { Text = ATSCommon.CommonFunctions.LanguageLabel(r.ToString()), Value = Convert.ToInt32(r) });
            }
            int type = Convert.ToInt32(_CopyObjInterViewProcessQue.ObjInterviewProcessQue.ObjQue.QueType);
            _ObjInterViewProcessQue.ObjInterviewProcessQue.ObjQue.QueText = _QuestionTypeList.Where(x => x.Value == type).Select(x => x.Text.ToString()).FirstOrDefault();
            List<KeyValuePair<int, string>> objQueDataType = Common.CommonFunctions.QuestionType.ToList();
            _ObjInterViewProcessQue.ObjInterviewProcessQue.ObjQue.QueDataTypeText = objQueDataType.Where(x => x.Key == _CopyObjInterViewProcessQue.ObjInterviewProcessQue.ObjQue.QueDataType).Select(x => x.Value.ToString()).FirstOrDefault();
        }

        public string GetRndTypeIdByCandidateSelfEvalution()
        {
            BLClient.RndTypeAction _objRndTypeAction = new BLClient.RndTypeAction(_CurrentClientId);
            List<Guid> lstRndType = _objRndTypeAction.GetRndTypeIdByCandidateSelfEvalution();
            String lstRndTypeStr = String.Join(",", lstRndType.Select(g => g.ToString()).ToArray());
            return lstRndTypeStr;
        }

        public FileStreamResult pdf()
        {
            MemoryStream workStream = new MemoryStream();
            Document document = new Document();
            PdfWriter.GetInstance(document, workStream).CloseStream = false;
            document.Open();
            document.Add(new Paragraph("Hello World"));
            document.Add(new Paragraph(DateTime.Now.ToString()));
            document.Close();
            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;
            return new FileStreamResult(workStream, "application/pdf");
        }

        private void RenderHtmlOutput(ControllerContext context, string ViewName)
        {
            IView viewEngineResult = ViewEngines.Engines.FindView(context, ViewName, null).View;
            var viewContext = new ViewContext(context, viewEngineResult, context.Controller.ViewData,
            context.Controller.TempData, context.HttpContext.Response.Output);
            viewEngineResult.Render(viewContext, context.HttpContext.Response.Output);
        }
        public void FillDropdown(Guid ApplicationId)
        {
            List<BEClient.DrpStringMapping> ListNoticePeriod = new List<BEClient.DrpStringMapping>();
            List<BEClient.DrpStringMapping> ListNoticePeriodDaysType = new List<BEClient.DrpStringMapping>();
            List<BEClient.DrpStringMapping> ListNotificationMethod = new List<BEClient.DrpStringMapping>();
            List<BEClient.DrpStringMapping> ListPlacementType = new List<BEClient.DrpStringMapping>();
            List<BEClient.DrpStringMapping> ListSalaryType = new List<BEClient.DrpStringMapping>();
            List<BEClient.DrpStringMapping> ListJobType = new List<BEClient.DrpStringMapping>();
            List<BEClient.JobLocation> ListJoblocation = new List<BEClient.JobLocation>();
            List<BEClient.DrpStringMapping> ListRatePeriod = new List<BEClient.DrpStringMapping>();
            List<BEClient.DrpStringMapping> ListNumericDropDown = new List<BEClient.DrpStringMapping>();
            List<BEClient.EmailTemplates> ListEmailTemplates = new List<BEClient.EmailTemplates>();
            List<BEMAster.ClientLanguage> _lanName = ATSCommon.CurrentSession.Instance.VerifiedClient.ClientLanguageList.Where(x => x.LanguageId == _CurrentLanguageId).ToList();
            string _currentLangName = "English";
            try
            {
                _currentLangName = _lanName[0].objLanguage.LanguageName;
            }
            catch (Exception)
            { }
            ListNoticePeriod = BLClient.Common.CacheHelper.GetDropDownStringMapping(_CurrentClientId, _CurrentLanguageId, base.CurrentClient.Clientname, "NoticePeriod", false, _currentLangName);
            BEClient.DrpStringMapping objtemp = new BEClient.DrpStringMapping();
            ViewBag.DRPNoticePeriod = new SelectList(ListNoticePeriod, "ValueField", "TextField");
            ListNoticePeriodDaysType = BLClient.Common.CacheHelper.GetDropDownStringMapping(_CurrentClientId, _CurrentLanguageId, base.CurrentClient.Clientname, "NoticePeriodDaysType", false, _currentLangName);
            ViewBag.DRPNoticePeriodDaysType = new SelectList(ListNoticePeriodDaysType, "ValueField", "TextField");
            ListNotificationMethod = BLClient.Common.CacheHelper.GetDropDownStringMapping(_CurrentClientId, _CurrentLanguageId, base.CurrentClient.Clientname, "NotificationMethod", false, _currentLangName);
            ViewBag.DRPNotificationMethod = new SelectList(ListNotificationMethod, "ValueField", "TextField");
            ListPlacementType = BLClient.Common.CacheHelper.GetDropDownStringMapping(_CurrentClientId, _CurrentLanguageId, base.CurrentClient.Clientname, "PlacementType", false, _currentLangName);
            ViewBag.DRPPlacementType = new SelectList(ListPlacementType, "ValueField", "TextField");
            ListSalaryType = BLClient.Common.CacheHelper.GetDropDownStringMapping(_CurrentClientId, _CurrentLanguageId, base.CurrentClient.Clientname, "PayType", false, _currentLangName);
            ViewBag.DRPSalaryType = new SelectList(ListSalaryType, "ValueField", "TextField");
            ListJobType = BLClient.Common.CacheHelper.GetDropDownStringMapping(_CurrentClientId, _CurrentLanguageId, base.CurrentClient.Clientname, "JobType", false, _currentLangName);
            ViewBag.DRPJobType = new SelectList(ListJobType, "ValueField", "TextField");
            BLClient.JobLocationAction _objJobLocationAction = new BLClient.JobLocationAction(base.CurrentClient.ClientId);
            ListJoblocation = _objJobLocationAction.FillJobLocationByApplicationId(ApplicationId, _CurrentLanguageId);
            ViewBag.DRPLocationList = new SelectList(ListJoblocation, "JobLocationId", "LocalName");
            ListRatePeriod = BLClient.Common.CacheHelper.GetDropDownStringMapping(_CurrentClientId, _CurrentLanguageId, base.CurrentClient.Clientname, "RatePeriod", false, _currentLangName);
            ViewBag.DRPRatePeriod = new SelectList(ListRatePeriod, "ValueField", "TextField");
            ListNumericDropDown = ATS.WebUi.Common.CommonFunctions.FillNumericDropdown(1, 150);
            ViewBag.DRPWeeklyHours = new SelectList(ListNumericDropDown, "ValueField", "TextField");
            BLClient.EmailTemplatesAction EmailTemplateAction = new BLClient.EmailTemplatesAction(_CurrentClientId);
            ListEmailTemplates = EmailTemplateAction.FillEmailTemplatesByCategory(Convert.ToInt32(BEClient.EmailTemplateCateogory.Offers), _CurrentLanguageId);
            ViewBag.DRPEmailTemplates = new SelectList(ListEmailTemplates, "EmailTemplateId", "Subject");
        }
    }
}
