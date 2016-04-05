using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEClient = ATS.BusinessEntity;
using BLClient = ATS.BusinessLogic;
using BEMaster = ATS.BusinessEntity.Master;
using ATSCommon = ATS.WebUi.Common;
using ATS.WebUi.Areas.Employee.Models;
using ATS.WebUi.Models.Question;
using System.Globalization;
using System.Web.Script.Serialization;
using ATS.WebUi.Mailers;
using System.Net;
using System.Text;
using BEOfferStatus = ATS.BusinessEntity.VacancyOfferStatus;
using BEOfferType = ATS.BusinessEntity.VacancyOfferType;
using CommonCtrl = ATS.WebUi.Common.CommonFunctions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc.Html;
using DisplayMessageConstant = ATS.BusinessEntity.Common;
using System.Text.RegularExpressions;
using RootModels = ATS.WebUi.Models;
using ATS.WebUi.Areas.Candidate.Models;
using BEApplicationOptionsConstant = ATS.BusinessEntity.Common.ApplicationStatusOptionsConstant;

namespace ATS.WebUi.Areas.Employee.Controllers
{
    public partial class ApplicationController : ATS.WebUi.Controllers.AreaBaseController
    {
        #region Private Members
        private Guid _CurrentClientId;
        private Guid _CurrentLanguageId;
        private BLClient.ApplicationAction _objApplicationAction;
        private BLClient.InterViewProcessAction _objInterViewProcessAction;
        private BLClient.InterviewProcessQueAction _objInterviewProcessQueAction;
        private BLClient.ReviewersAction _objReviewersAction;
        #endregion

        #region Vacancy History Description Constant

        private const string str_Draft_HistoryDescription = "Award Draft by ###USER###";
        private const string str_DraftRevision_HistoryDescription = "Draft Revision by ###USER###";
        private const string str_OfferMade_HistoryDescription = "###STATUS### by ###USER###";
        private const string str_OfferMadeRevision_HistoryDescription = "Revised the award (###STATUS###) by ###USER###";
        private const string str_CandidateAccepted_HistoryDescription = "###STATUS###";
        private const string str_CandidateDeclined_HistoryDescription = "###STATUS###";
        private const string str_CandidateRetractAcceptance_HistoryDescription = "###STATUS###";
        private const string str_CandidateCountered_HistoryDescription = "###STATUS###";
        private const string str_CompanyCountered_HistoryDescription = "###STATUS### by ###USER###";//When Employer revise the CandidateCounter offer
        private const string str_Retrected_HistoryDescription = "Award ###STATUS### by ###USER###";
        private const string str_CompanyAccepted_HistoryDescription = "Confirm Award Accepted by ###USER###";

        #endregion

        #region OnAuthorization Method call on every request
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.ActionName.ToLower() != "ConfirmOfferPDF".ToLower() &&
                filterContext.ActionDescriptor.ActionName.ToLower() != "GenerateOfferLetterPDF".ToLower())
            {
                base.OnAuthorization(filterContext);
                if (base.CurrentClient != null && Common.CurrentSession.Instance.VerifiedUser != null)
                {
                    _CurrentClientId = base.CurrentClient.ClientId;
                    _CurrentLanguageId = base.CurrentClient.CurrentLanguageId;


                }
            }
        }
        #endregion

        #region Create Action Object
        private BLClient.ReviewersAction ReviewersActionObject
        {
            get
            {
                if (_objReviewersAction == null)
                {
                    _objReviewersAction = new BLClient.ReviewersAction(_CurrentClientId);
                }
                return _objReviewersAction;
            }
        }
        private BLClient.ApplicationAction ApplicationActionObject
        {
            get
            {
                if (_objApplicationAction == null)
                {
                    _objApplicationAction = new BLClient.ApplicationAction(_CurrentClientId);
                }
                return _objApplicationAction;
            }
        }

        private BLClient.InterviewProcessQueAction InterviewProcessQueActionObject
        {
            get
            {
                if (_objInterviewProcessQueAction == null)
                {
                    _objInterviewProcessQueAction = new BLClient.InterviewProcessQueAction(_CurrentClientId);
                }
                return _objInterviewProcessQueAction;
            }
        }

        private BLClient.InterViewProcessAction InterViewProcessActionObject
        {
            get
            {
                if (_objInterViewProcessAction == null)
                {
                    _objInterViewProcessAction = new BLClient.InterViewProcessAction(_CurrentClientId);
                }
                return _objInterViewProcessAction;
            }
        }
        #endregion

        [HttpGet]
        public JsonResult GetReviewers(Guid? VacRndId, Guid? VacQueId, Guid? ApplicationId)
        {

            //Used for Json return
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            Guid _VacRndId = Guid.Empty;
            Guid _VacQueId = Guid.Empty;
            Guid _ApplicationId = Guid.Empty;
            //End Json return property
            try
            {
                List<BEClient.Reviewers> objReviewers = new List<BEClient.Reviewers>();
                if (Guid.TryParse(VacRndId.ToString(), out _VacRndId) && Guid.TryParse(VacQueId.ToString(), out _VacQueId) && Guid.TryParse(ApplicationId.ToString(), out _ApplicationId))
                    objReviewers = ReviewersActionObject.GetAllReviewersByRoundIdAndQuestionId(_VacRndId, _CurrentLanguageId, _VacQueId, _ApplicationId);
                Data = base.RenderRazorViewToString("Profile/_AccReviewers", objReviewers);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetQuestions(Guid? VacRndId, Guid? VacCatId, Guid? AppId)
        {
            //Used for Json return
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            Guid _VacRndId = Guid.Empty;
            Guid _VacCatId = Guid.Empty;
            Guid _AppId = Guid.Empty;

            //End Json return property
            try
            {
                List<BEClient.InterviewProcessQue> objInterviewProcessQue = new List<BEClient.InterviewProcessQue>();
                if (Guid.TryParse(VacRndId.ToString(), out _VacRndId) && Guid.TryParse(VacCatId.ToString(), out _VacCatId) && Guid.TryParse(AppId.ToString(), out _AppId))
                    objInterviewProcessQue = InterviewProcessQueActionObject.GetQueDetailsBtyOrder(_VacRndId, _VacCatId, _AppId, _CurrentLanguageId);

                Data = base.RenderRazorViewToString("Profile/_AccQuestions", objInterviewProcessQue);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }
        #region Ajx Get Methods
        [HttpGet]
        public JsonResult GetInterviewRoundSection(Guid? VacRndId, Guid? AppId)
        {
            //Used for Json return
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            Guid _VacRndId = Guid.Empty;
            //End Json return property
            try
            {
                List<BEClient.InterViewProcess> objInterViewProcess = new List<BEClient.InterViewProcess>();
                if (Guid.TryParse(VacRndId.ToString(), out _VacRndId))
                    objInterViewProcess = InterViewProcessActionObject.GetQueCatDetailsBtyOrder(_VacRndId);

                BLClient.RoundAttributesAction RndAttrAction = new BLClient.RoundAttributesAction(_CurrentClientId);
                int RndAttrNo = RndAttrAction.GetRoundAttributesNoByVacRndId(_VacRndId);
                if (RndAttrNo == Convert.ToInt16(BEClient.RndAttrNo.OfferRound))
                {
                    BLClient.ReviewersAction objReviewerAction = new BLClient.ReviewersAction(_CurrentClientId);

                    ViewBag.IsReviewerOffer = objReviewerAction.IsReviewerForRound((Guid)VacRndId, Common.CurrentSession.Instance.VerifiedUser.UserId);
                    Guid ApplicationId = Guid.Empty;
                    Guid.TryParse(AppId.ToString(), out ApplicationId);
                    List<BEClient.VacancyOffer> ObjVacancyOfferList = new List<BEClient.VacancyOffer>();
                    BLClient.VacancyOfferAction VacOfferAction = new BLClient.VacancyOfferAction(_CurrentClientId);
                    ObjVacancyOfferList = VacOfferAction.GetVacancyOfferByApplicationId(ApplicationId, _CurrentLanguageId);
                    foreach (var item in ObjVacancyOfferList)
                    {
                        item.IsReviewer = ViewBag.IsReviewerOffer;
                    }
                    Data = base.RenderRazorViewToString("VacancyOffer/_AccVacancyOfferList", ObjVacancyOfferList);
                    Message = "MakeOffer";
                }
                else
                {
                    Data = base.RenderRazorViewToString("Profile/_AccInterViewSections", objInterViewProcess);
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        public void FillEmailAndOfferLetterByIds(string EmailTemplateList, string OfferLetterList)
        {
            BLClient.EmailTemplatesAction objEmailTemplateAction = new BLClient.EmailTemplatesAction(_CurrentClientId);
            List<BEClient.EmailTemplates> objEmailTemplateList = new List<BEClient.EmailTemplates>();
            objEmailTemplateList = objEmailTemplateAction.FillEmailTemplatesByIds(EmailTemplateList, _CurrentLanguageId);
            ViewBag.DRPEmailTemplates = new SelectList(objEmailTemplateList, "EmailTemplateId", "EmailName");

            BLClient.OfferLetterAction objOfferLetterAction = new BLClient.OfferLetterAction(_CurrentClientId);
            List<BEClient.OfferLetters> objOfferLetterList = new List<BEClient.OfferLetters>();
            objOfferLetterList = objOfferLetterAction.FillOfferLettersByIds(OfferLetterList, _CurrentLanguageId);
            ViewBag.DRPOfferLetters = new SelectList(objOfferLetterList, "OfferLetterId", "OfferLetterName");
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


            List<BEMaster.ClientLanguage> _lanName = ATSCommon.CurrentSession.Instance.VerifiedClient.ClientLanguageList.Where(x => x.LanguageId == _CurrentLanguageId).ToList();
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
        }

        [HttpGet]
        public JsonResult GetInterviewSchedule(string AppId)
        {
            //Used for Json return
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            //End Json return property
            try
            {
                if (!String.IsNullOrEmpty(AppId))
                {
                    string LstRndTypeId = GetRndTypeIdByCandidateSelfEvalution();
                    BLClient.VacancyRoundAction _objVacRndAction = new BLClient.VacancyRoundAction(_CurrentClientId);
                    List<BEClient.VacancyRound> _objVacRnd = _objVacRndAction.GetRndTypeByApplicationId(new Guid(AppId), LstRndTypeId, _CurrentLanguageId);
                    ViewBag.RoundType = _objVacRnd;
                    ViewBag.RoundType = new SelectList(_objVacRnd, "VacancyRoundId", "RoundType");

                    List<BEClient.UserDetails> _Interviewer = new List<BEClient.UserDetails>();
                    ViewBag.Interviewer = new SelectList(_Interviewer, "UserId", "FirstName");

                    BEClient.ScheduleInt ObjSchd = new BEClient.ScheduleInt();
                    ObjSchd.ApplicationId = new Guid(AppId);
                    ObjSchd.IsBeginInterview = false;


                    Data = base.RenderRazorViewToString("_CRUScheduleInt", ObjSchd);
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetInterviewerByVacRndId(string VacRndId, string AppId)
        {
            try
            {
                BLClient.UserDetailsAction _UserDetailsAction = new BLClient.UserDetailsAction(base.CurrentClient.ClientId);
                List<BEClient.UserDetails> _UserDetails = new List<BEClient.UserDetails>();
                if (!VacRndId.Equals(string.Empty))
                {
                    _UserDetails = _UserDetailsAction.GetInterviewerByVacRndId(new Guid(VacRndId), new Guid(AppId));
                }
                return GetJson(new { UserId = _UserDetails.Select(r => r.UserId), FirstName = _UserDetails.Select(r => r.FirstName) });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// This function will be used under _Profile View which located under \shared\_profilke.cshtml
        /// </summary>
        /// <returns>It return default view of profile</returns>
        [HttpGet]
        public JsonResult GetInterviews(string AppId)
        {
            //Used for Json return
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            //End Json return property
            try
            {
                Guid ApplicationId = new Guid(AppId);
                //                String LstRndTypeId = GetRndTypeIdByCandidateSelfEvalution();
                String LstRndTypeId = GetRestrictedRounds();
                BLClient.ScheduleIntAction _objScheduleIntAction = new BLClient.ScheduleIntAction(_CurrentClientId);
                List<BEClient.ScheduleInt> _objIntScheduleDetails = _objScheduleIntAction.GetVacRndDetailByApplicationId(ApplicationId, LstRndTypeId);
                ////Anand :To check the candidate promoted to Interview
                BLClient.PromoteCandidateAction objPromoteCandidate = new BLClient.PromoteCandidateAction(_CurrentClientId);
                bool ResultPc = objPromoteCandidate.IsPromotedToInterview(ApplicationId, LstRndTypeId);

                BLClient.ApplicationBasedStatusAction _objAppBasedStatusAction = new BLClient.ApplicationBasedStatusAction(_CurrentClientId);
                bool IsDeclined = _objAppBasedStatusAction.IsApplicationDecline(ApplicationId);

                if (ResultPc)
                {
                    ViewBag.IsPromotedInt = true;
                }
                else
                {
                    ViewBag.IsPromotedInt = false;
                }

                if (IsDeclined)
                {
                    ViewBag.IsDeclined = true;
                }
                else
                {
                    ViewBag.IsDeclined = false;
                }

                Data = base.RenderRazorViewToString("_InterviewsAcc", _objIntScheduleDetails);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// This function will be used under _Profile View which located under \shared\_profilke.cshtml
        /// </summary>
        /// <returns>It return default view of profile</returns>
        [HttpGet]
        public JsonResult GetCanInterviewAccformat(Guid ApplicationId)
        {
            //Used for Json return
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            //End Json return property
            try
            {
                BLClient.GetScoreAction _objGetScoreAction = new BLClient.GetScoreAction(_CurrentClientId);
                BEClient.GetScore _objscore = _objGetScoreAction.GetApplicationScore(ApplicationId);

                BLClient.ApplicationBasedStatusAction _objAppBasedStatusAction = new BLClient.ApplicationBasedStatusAction(_CurrentClientId);
                bool IsDeclined = _objAppBasedStatusAction.IsApplicationDecline(ApplicationId);

                if (IsDeclined)
                {
                    ViewBag.IsDeclined = true;
                }
                else
                {
                    ViewBag.IsDeclined = false;
                }
                Data = base.RenderRazorViewToString("_CanInterviewAcc", _objscore);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// This function will be used under _Profile View which located under \shared\_profilke.cshtml
        /// </summary>
        /// <param name="ProfileId">Profile Id of candidate</param>
        /// <returns>return list of application which will be applied by passed profile id, It will return onlu Submitted application </returns>
        [HttpGet]
        public JsonResult GetApplicationList(Guid? ProfileId)
        {
            //Used for Json return
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            //End Json return property
            try
            {
                Guid _ProfileId = Guid.Empty;
                Guid.TryParse(ProfileId.ToString(), out _ProfileId);
                if (!_ProfileId.Equals(Guid.Empty))
                {
                    List<BEClient.Application> _objListApplication = ApplicationActionObject.GetSubmittedApplicationByProfile(_ProfileId, _CurrentLanguageId);
                    Data = base.RenderRazorViewToString("_ApplicationAcc", _objListApplication);
                }
                else
                {
                    throw new Exception("ProfileId is not passed as parameter");
                }

            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        public BEClient.ScheduleInt GetApplicationStatus(Guid ApplicationId)
        {
            BLClient.ScheduleIntAction SIAction = new BLClient.ScheduleIntAction(_CurrentClientId);
            BEClient.ScheduleInt ObjSI = SIAction.GetAppStatusByAppId(ApplicationId, _CurrentLanguageId);
            return ObjSI;
        }

        [HttpPost]
        public JsonResult CRUScheduleInt(BEClient.ScheduleInt ScheduleInt)
        {
            String Message = String.Empty;
            bool IsError = true;
            String Data = "";
            try
            {
                if (!string.IsNullOrEmpty(ScheduleInt.ScheduleDateStr))
                    ScheduleInt.ScheduleDate = Convert.ToDateTime(ScheduleInt.ScheduleDateStr);

                BLClient.ScheduleIntAction objScheduleIntAction = new BLClient.ScheduleIntAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                if (ScheduleInt.ScheduleIntId == Guid.Empty)
                {
                    Guid NewrecordAdded = objScheduleIntAction.AddSaveScheduleInt(ScheduleInt);
                    if (!NewrecordAdded.Equals(Guid.Empty))
                    {
                        IsError = false;
                        Message = "Record Inserted Successfully";
                        ScheduleInt.ScheduleIntId = NewrecordAdded;
                        BLClient.ScheduleIntAction _objScheduleIntAction = new BLClient.ScheduleIntAction(_CurrentClientId);
                        BEClient.ScheduleInt _objIntScheduleDetails = _objScheduleIntAction.GetIntScheduleByScheduleIntId(NewrecordAdded);
                        Data = base.RenderRazorViewToString("_Interview", _objIntScheduleDetails);
                    }
                }
                else
                {
                    Guid ScheduleIntId = ScheduleInt.ScheduleIntId;
                    Boolean Result = objScheduleIntAction.UpdateScheduleInt(ScheduleInt);
                    BEClient.ScheduleInt _objIntScheduleDetails = objScheduleIntAction.GetIntScheduleByScheduleIntId(ScheduleIntId);
                    Data = base.RenderRazorViewToString("_Interview", _objIntScheduleDetails);
                }
            }
            catch (Exception Ex)
            {
                Message = Ex.Message;
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
        }

        [HttpPost]
        public JsonResult UpdateScheduleInt(BEClient.ScheduleInt ScheduleInt)
        {
            String Message = String.Empty;
            bool IsError = true;
            String Data = "";
            try
            {
                BLClient.ScheduleIntAction objScheduleIntAction = new BLClient.ScheduleIntAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                bool isUpdated = objScheduleIntAction.UpdateScheduleInt(ScheduleInt);
                if (isUpdated)
                {
                    IsError = false;
                    Message = "Updated Successfully";

                    BLClient.ScheduleIntAction _objScheduleIntAction = new BLClient.ScheduleIntAction(_CurrentClientId);
                    BEClient.ScheduleInt _objIntScheduleDetails = _objScheduleIntAction.GetIntScheduleByScheduleIntId((Guid)ScheduleInt.ScheduleIntId);
                    Data = base.RenderRazorViewToString("_Interview", _objIntScheduleDetails);
                }
            }
            catch (Exception Ex)
            {
                Message = Ex.Message;
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
        }

        [HttpGet]
        public JsonResult GetScheduleIntById(Guid ScheduleIntId, Guid AppId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                string LstRndTypeId = GetRndTypeIdByCandidateSelfEvalution();
                BLClient.VacancyRoundAction _objVacRndAction = new BLClient.VacancyRoundAction(_CurrentClientId);
                List<BEClient.VacancyRound> _objVacRnd = _objVacRndAction.GetAllRndTypeByApplicationId(AppId, LstRndTypeId, ScheduleIntId, _CurrentLanguageId);
                ViewBag.RoundType = _objVacRnd;
                ViewBag.RoundType = new SelectList(_objVacRnd, "VacancyRoundId", "RoundType");
                BLClient.ScheduleIntAction _objScheduleIntAction = new BLClient.ScheduleIntAction(_CurrentClientId);
                BEClient.ScheduleInt _objIntScheduleDetails = _objScheduleIntAction.GetIntScheduleByScheduleIntId(ScheduleIntId);
                _objIntScheduleDetails.ApplicationId = AppId;
                Data = base.RenderRazorViewToString("_CRUScheduleInt", _objIntScheduleDetails);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetConductScheduleIntById(Guid ScheduleIntId, Guid AppId)
        {
            try
            {
                string LstRndTypeId = GetRndTypeIdByCandidateSelfEvalution();
                BLClient.ScheduleIntAction _objScheduleIntAction = new BLClient.ScheduleIntAction(_CurrentClientId);
                BEClient.ScheduleInt _objIntScheduleDetails = _objScheduleIntAction.GetIntScheduleByScheduleIntId(ScheduleIntId);
                _objIntScheduleDetails.ApplicationId = AppId;
                return View("ConductInterview/_ConductUpdateScheduleInt", _objIntScheduleDetails);
            }
            catch
            {
                throw;
            }
        }

        public JsonResult IsInterviewBegin(Guid ScheduleIntId)
        {
            String Message = string.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                BLClient.ScheduleIntAction ObjScheduleIntAction = new BLClient.ScheduleIntAction(_CurrentClientId);
                bool Result = false;
                Result = ObjScheduleIntAction.IsInterviewBegin(ScheduleIntId);
                if (!Result)
                {
                    IsError = true;
                    Message = "You can not delete this interview, it's already begin.";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data));

        }

        [HttpPost]
        public JsonResult DeleteSchedule(Guid ScheduleIntId)
        {
            String DisplayMessage = string.Empty;
            String Message = string.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                BLClient.ScheduleIntAction ObjScheduleIntAction = new BLClient.ScheduleIntAction(_CurrentClientId);
                bool Result = false;
                Result = ObjScheduleIntAction.DeleteScheduleInt(ScheduleIntId);
                if (Result)
                {
                    IsError = false;
                    Message = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.SuccessMessages.RECORD_DELETED_SUCCESSFULLY).ToString();
                }
                else
                {
                    IsError = true;
                    Message = LanguageLabelExtensions.LanguageLabel(null, DisplayMessageConstant.ErrorMessages.MSG_DELETE_ALREADY_UNDERWAY_OR_COMPLETED).ToString();
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
        public JsonResult GetVacRndDetailByApplicationId(string AppId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                BLClient.ScheduleIntAction _objScheduleIntAction = new BLClient.ScheduleIntAction(_CurrentClientId);
                List<BEClient.ScheduleInt> _objIntScheduleDetails = _objScheduleIntAction.GetVacRndDetailByApplicationId(new Guid(AppId), string.Empty);
                return null;
            }
            catch (Exception ex)
            {

            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetComments(Guid AppId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                BEClient.AppComment objappcomment = new BEClient.AppComment();
                BLClient.AppCommentAction objAppCommnetAction = new BLClient.AppCommentAction(_CurrentClientId);
                objappcomment.AppCommentLst = objAppCommnetAction.GetCommentsByAppId(AppId);
                objappcomment.ApplicationId = AppId;
                Data = base.RenderRazorViewToString("_Comments", objappcomment);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region  Begin interview

        [HttpGet]
        public JsonResult GetInterviewDetails(Guid VacRndId, Guid ScheduleIntId)
        {
            //Used for Json return
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            Guid _VacRndId = Guid.Empty;
            try
            {
                Guid AppRndId = Guid.Empty;

                BLClient.VacancyRoundAction _ObjVacRndAction = new BLClient.VacancyRoundAction(_CurrentClientId);
                BLClient.BeginInerviewAction _BeginInerviewAction = new BLClient.BeginInerviewAction(_CurrentClientId);
                BEClient.VacancyRound ObjVacRnd = new BEClient.VacancyRound();
                BEClient.BeginInterView ObjBeginInterview = new BEClient.BeginInterView();
                bool IsReviewer = _BeginInerviewAction.IsReviewer(VacRndId, ATSCommon.CurrentSession.Instance.VerifiedUser.UserId);

                if (IsReviewer)
                {
                    ObjBeginInterview = _BeginInerviewAction.IsBeginInterviewExists(VacRndId, ATSCommon.CurrentSession.Instance.VerifiedUser.UserId, ScheduleIntId);
                    if (ObjBeginInterview != null)
                    {
                        if (ObjBeginInterview.IsComplete)
                        {
                            ObjBeginInterview.VacRndId = VacRndId;
                            ObjBeginInterview.ScheduleIntId = ScheduleIntId;
                            ObjBeginInterview.IsReviewer = IsReviewer;
                            //for final AppScore
                            BLClient.GetScoreAction ObjScoreAction = new BLClient.GetScoreAction(_CurrentClientId);
                            BEClient.GetScore objScore = new BEClient.GetScore();
                            objScore = ObjScoreAction.GetTotalScoreByScheduleAndVacRnd(ObjBeginInterview.ScheduleIntId, ObjBeginInterview.VacRndId);
                            ObjBeginInterview.TotalScore = objScore.Score;
                            Data = base.RenderRazorViewToString("InterViewQuestions/_BeginInterview", ObjBeginInterview);
                        }
                        else
                        {
                            int Row = 1;
                            BEClient.InterViewProcess ObjIterViewProcess = new BEClient.InterViewProcess();
                            BLClient.InterViewProcessAction ObjInterViewProAction = new BLClient.InterViewProcessAction(_CurrentClientId);
                            ObjIterViewProcess = ObjInterViewProAction.GetQueCatDetailsBtyOrder(VacRndId, Row, ScheduleIntId);
                            if (ObjIterViewProcess == null)
                            {
                                throw new Exception("Question not available");
                            }
                            Data = base.RenderRazorViewToString("InterViewQuestions/_InterViewSections", ObjIterViewProcess);
                        }
                    }
                    else
                    {
                        BEClient.BeginInterView newObjBeginInterview = new BEClient.BeginInterView();
                        newObjBeginInterview.VacRndId = VacRndId;
                        newObjBeginInterview.ScheduleIntId = ScheduleIntId;
                        newObjBeginInterview.IsReviewer = IsReviewer;
                        Data = base.RenderRazorViewToString("InterViewQuestions/_BeginInterview", newObjBeginInterview);
                    }
                }
                else
                {
                    ObjBeginInterview.IsReviewer = IsReviewer;
                    ObjBeginInterview.VacRndId = VacRndId;
                    ObjBeginInterview.ScheduleIntId = ScheduleIntId;
                    Data = base.RenderRazorViewToString("InterViewQuestions/_BeginInterview", ObjBeginInterview);
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddBeginInterview(BEClient.BeginInterView objBeginInt)
        {
            String Data = String.Empty;
            String Message = String.Empty;
            bool IsError = false;
            try
            {
                if (objBeginInt != null)
                {
                    BLClient.BeginInerviewAction ObjBeginIntAction = new BLClient.BeginInerviewAction(_CurrentClientId);
                    objBeginInt.IntStart = DateTime.UtcNow;
                    objBeginInt.UserId = ATSCommon.CurrentSession.Instance.VerifiedUser.UserId;
                    Guid AppRndId = ObjBeginIntAction.AddBeginInterview(objBeginInt);
                    if (!AppRndId.Equals(Guid.Empty))
                    {
                        return RedirectToAction("GetInterviewDetails", new { VacRndId = objBeginInt.VacRndId, ScheduleIntId = objBeginInt.ScheduleIntId });
                    }
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = false;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data));
        }

        [HttpGet]
        public JsonResult GetSectionByRow(Guid VacRndId, int Row, int? QueRow)
        {
            try
            {
                String Data = String.Empty;
                ViewBag.QueRow = QueRow;
                BEClient.InterViewProcess ObjIterViewProcess = new BEClient.InterViewProcess();
                BLClient.InterViewProcessAction ObjInterViewProAction = new BLClient.InterViewProcessAction(_CurrentClientId);
                ObjIterViewProcess = ObjInterViewProAction.GetQueCatDetailsBtyOrder(VacRndId, Row, null);
                Data = base.RenderRazorViewToString("InterViewQuestions/_InterViewSections", ObjIterViewProcess);
                return base.GetJson(base.GetJsonContent(false, null, string.Empty, Data), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult GetQuesByCatAndRow(Guid VacRndId, Guid QueCatId, Guid ScheduleIntId, int Row)
        {
            String Data = String.Empty;
            bool IsError = false;
            String Message = String.Empty;
            Guid _AppId = Guid.Empty;
            try
            {
                InterviewProcessQueModel _ObjInterViewProcessQue = new InterviewProcessQueModel();
                BLClient.InterviewProcessQueAction ObjInterViewProQueAction = new BLClient.InterviewProcessQueAction(_CurrentClientId);
                BLClient.ReviewersAction ObjReviewersAction = new BLClient.ReviewersAction(_CurrentClientId);
                Guid VacRMId = ObjReviewersAction.GetVacReviewerMemberByVacRndAndUser(VacRndId, ATSCommon.CurrentSession.Instance.VerifiedUser.UserId);
                _ObjInterViewProcessQue.ObjInterviewProcessQue = ObjInterViewProQueAction.GetQueDetailsBtyOrder(VacRndId, QueCatId, Row, _CurrentLanguageId);
                _ObjInterViewProcessQue.Questions = new List<ATS.WebUi.Models.Question.ATSQuestionCommon>();
                CreateQuestionWithAns(ref _ObjInterViewProcessQue, VacRMId, ScheduleIntId);
                Data = base.RenderRazorViewToString("InterViewQuestions/_InterViewQuestions", _ObjInterViewProcessQue);
            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCanAnsByVacQueIdAndRMID(Guid ApplicationId, Guid QueId, Guid VacRMId, Guid VacRndId, Guid QueCatId, Guid VacQueId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                BLClient.ScheduleIntAction ScheduleIntAction = new BLClient.ScheduleIntAction(_CurrentClientId);
                Guid ScheduleIntId = ScheduleIntAction.GetSchIdByAppAndVacQueAndRMId(ApplicationId, VacRndId);
                BLClient.BeginInerviewAction BeginInterviewAction = new BLClient.BeginInerviewAction(_CurrentClientId);
                Boolean IsInterviewComplete = BeginInterviewAction.IsInterviewComplete(VacRndId, ATSCommon.CurrentSession.Instance.VerifiedUser.UserId, ScheduleIntId);
                InterviewProcessQueModel _ObjInterViewProcessQue = new InterviewProcessQueModel();
                BLClient.InterviewProcessQueAction ObjInterViewProQueAction = new BLClient.InterviewProcessQueAction(_CurrentClientId);
                List<BEClient.InterviewProcessQue> obj = ObjInterViewProQueAction.GetQueDetailsBtyOrder(VacRndId, QueCatId, ApplicationId, _CurrentLanguageId, QueId);
                if (obj != null)
                    _ObjInterViewProcessQue.ObjInterviewProcessQue = obj.Where(x => x.ObjQue.QueId.ToString().Equals(QueId.ToString())).FirstOrDefault();
                BLClient.AppAnswerAction ObjAppAnswerAction = new BLClient.AppAnswerAction(_CurrentClientId);
                BEClient.AppAnswer objAppAnswer = new BEClient.AppAnswer();
                _ObjInterViewProcessQue.Questions = new List<ATS.WebUi.Models.Question.ATSQuestionCommon>();
                CreateQuestionWithAns(ref _ObjInterViewProcessQue, VacRMId, ScheduleIntId);
                //Anand:To Check whether the Current user is same as Reviewer
                BLClient.ReviewersAction ObjReviewerAction = new BLClient.ReviewersAction(_CurrentClientId);
                Guid VUserId = ObjReviewerAction.GetUserByVacRmId(VacRMId);
                //Anand:To Check Whether user is promoted to Round or not
                BLClient.PromoteCandidateAction objPromoteAction = new BLClient.PromoteCandidateAction(_CurrentClientId);
                bool IsPromoted = objPromoteAction.IsPromoted(VacRndId, ApplicationId);
                if (VUserId == ATSCommon.CurrentSession.Instance.UserId)
                {
                    if (IsPromoted)
                    {
                        ViewBag.IsEditable = true;
                    }
                    else
                    {
                        ViewBag.IsEditable = false;
                    }
                }
                else
                {
                    ViewBag.IsEditable = false;
                }

                if (IsInterviewComplete)
                    ViewBag.IsComplete = true;
                else
                    ViewBag.IsComplete = false;
                Data = base.RenderRazorViewToString("Profile/_CanAnswer", _ObjInterViewProcessQue);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        private void CreateQuestionWithAns(ref InterviewProcessQueModel _ObjInterViewProcessQue, Guid VacRMId, Guid? ScheduleIntId = null)
        {
            InterviewProcessQueModel _CopyObjInterViewProcessQue = _ObjInterViewProcessQue;
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

                    ATS.WebUi.Models.Question.ATSPickList _DrpATSPickList = new WebUi.Models.Question.ATSPickList();
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
                    ATS.WebUi.Models.Question.ATSCheckBox _DrpATSCheckBox = new WebUi.Models.Question.ATSCheckBox();
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
                    ATS.WebUi.Models.Question.ATSTextArea _DrpATSTextArea = new WebUi.Models.Question.ATSTextArea();
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
                    ATS.WebUi.Models.Question.ATSTextBox _DrpATSTextBox = new WebUi.Models.Question.ATSTextBox();
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
                    ATS.WebUi.Models.Question.ATSYesNo _DrpyesNoQue = new WebUi.Models.Question.ATSYesNo();
                    _DrpyesNoQue.QuestionText = _ObjInterViewProcessQue.ObjInterviewProcessQue.ObjQue.LocalName;
                    BLClient.AnsOptAction _objYesNoAnsOptAction = new BLClient.AnsOptAction(_CurrentClientId);
                    List<BEClient.AnsOpt> _objDdlynList = _objYesNoAnsOptAction.GetAnsListByQueId(_ObjInterViewProcessQue.ObjInterviewProcessQue.ObjQue.QueId, _CurrentLanguageId);
                    _DrpyesNoQue.AllValues = new SelectList(_objDdlynList, "AnsOptId", "DefaultName");
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
                    ATS.WebUi.Models.Question.ATSScale _DrpATSScale = new WebUi.Models.Question.ATSScale();
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

        public JsonResult AddEditAnswer(InterviewProcessQueModel objIntProc)
        {
            ATS.WebUi.Models.Question.ATSQuestionCommon objIntProcQue = objIntProc.Questions.FirstOrDefault();
            bool IsError = false;
            string Message = string.Empty;
            string Data = string.Empty;
            if (ServerValidation(out Message))
            {
                IsError = true;
                return GetJson(base.GetJsonContent(IsError, string.Empty, Message, ""));
            }
            try
            {
                Guid newAppAnsId = Guid.Empty;
                if (objIntProcQue != null)
                {
                    BLClient.AppAnswerAction ObjAppAnswerAction = new BLClient.AppAnswerAction(_CurrentClientId);
                    BLClient.ReviewersAction ObjReviewerAction = new BLClient.ReviewersAction(_CurrentClientId);
                    Guid VacRevMemberId = ObjReviewerAction.GetVacReviewerMemberByVacRndAndUser(objIntProcQue.VacRndId, ATSCommon.CurrentSession.Instance.VerifiedUser.UserId);
                    BEClient.AppAnswer ObjAppAnswer = new BEClient.AppAnswer();
                    ObjAppAnswer.VacRMId = VacRevMemberId;
                    ObjAppAnswer.ScheduleIntId = objIntProcQue.ScheduleIntId;
                    ObjAppAnswer.VacQueId = objIntProcQue.VacQueId;
                    ObjAppAnswer.Comment = objIntProcQue.Comment;
                    ObjAppAnswer.Value = objIntProcQue.Value;
                    switch (objIntProcQue.QuestionDataType)
                    {
                        case 1:
                        case 5:
                            string[] AnsOptId = Array.ConvertAll<object, string>((object[])objIntProcQue.Answer, Convert.ToString);
                            ObjAppAnswer.Answer = AnsOptId[0];
                            break;
                        case 2:
                            string Answer = string.Empty;

                            foreach (string item in (string[])objIntProcQue.Answer)
                            {
                                Answer += item + ",";
                            }
                            Answer = Answer.Substring(0, Answer.Length - 1);
                            ObjAppAnswer.Answer = Answer;
                            break;
                        case 3:

                            if (objIntProcQue.Value != null)
                            {
                                foreach (String s in (string[])objIntProcQue.Answer)
                                {
                                    ObjAppAnswer.Answer = s;
                                }
                            }
                            break;
                        case 4:
                            if (objIntProcQue.Value != null)
                            {
                                foreach (String s in (string[])objIntProcQue.Answer)
                                {
                                    ObjAppAnswer.Answer = s;
                                }
                            }
                            break;


                        case 6:
                            if (objIntProcQue.Value != null)
                            {
                                foreach (String s in (string[])objIntProcQue.Answer)
                                {
                                    ObjAppAnswer.Answer = s;
                                }
                            }
                            break;

                    }
                    if (String.IsNullOrEmpty(Convert.ToString(ObjAppAnswer.Answer)))
                    {
                        throw new Exception("Answer is required");
                    }
                    bool _isRecordExists = ObjAppAnswerAction.IsRecordExists(objIntProcQue.AppAnsId);
                    if (_isRecordExists)
                    {
                        ObjAppAnswer.AppAnsId = objIntProcQue.AppAnsId;
                        bool IsUpdated = ObjAppAnswerAction.UpdateAppAns(ObjAppAnswer);

                        if (IsUpdated)
                        {
                            Message = "Answer Updated Successfully";

                        }
                        else
                        {
                            IsError = true;
                            Message = "Answer not Saved";
                        }
                    }
                    else
                    {
                        newAppAnsId = ObjAppAnswerAction.InsertAppAnswer(ObjAppAnswer);
                        if (newAppAnsId != Guid.Empty)
                        {
                            objIntProcQue.AppAnsId = newAppAnsId;
                            Message = "Answer Saved Successfully";
                        }
                        else
                        {
                            IsError = true;
                            Message = "Answer not Saved ";
                        }
                    }
                }
                Data = objIntProcQue.AppAnsId.ToString();
            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
        }
        #endregion

        public string GetRndTypeIdByCandidateSelfEvalution()
        {
            BLClient.RndTypeAction _objRndTypeAction = new BLClient.RndTypeAction(_CurrentClientId);
            List<Guid> lstRndType = _objRndTypeAction.GetRndTypeIdByCandidateSelfEvalution();
            String lstRndTypeStr = String.Join(",", lstRndType.Select(g => g.ToString()).ToArray());
            return lstRndTypeStr;
        }
        public string GetRestrictedRounds()
        {
            BLClient.RndTypeAction _objRndTypeAction = new BLClient.RndTypeAction(_CurrentClientId);
            List<Guid> lstRndType = _objRndTypeAction.GetRestrictedRounds();
            String lstRndTypeStr = String.Join(",", lstRndType.Select(g => g.ToString()).ToArray());
            return lstRndTypeStr;
        }

        #region getscore
        [HttpGet]
        public JsonResult GetValue(string AnsoptId)
        {
            bool IsError = false;
            string Message = string.Empty;
            string Data = string.Empty;
            try
            {
                BLClient.AnsOptAction ObjAnsOptAction = new BLClient.AnsOptAction(_CurrentClientId);
                int value = ObjAnsOptAction.GetValueByAnsopt(AnsoptId);
                Data = value.ToString();

            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;

            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data), JsonRequestBehavior.AllowGet);
        }


        public ActionResult UpdateInterViewStatus(Guid VacRndId, Guid ScheduleIntId, bool IsComplete)
        {

            String Message = String.Empty;
            bool IsError = false;
            bool InterviewComp = false;
            bool result = false;
            int interviewStatus = 0;
            if (IsComplete)
            {
                interviewStatus = Convert.ToInt32(BEClient.InterviewStatus.InterViewComplete);
            }
            else
            {
                interviewStatus = Convert.ToInt32(BEClient.InterviewStatus.InterviewReopened);
            }

            String Data = String.Empty;
            try
            {

                BLClient.BeginInerviewAction ObjBeginInerviewAction = new BLClient.BeginInerviewAction(_CurrentClientId);
                InterviewComp = ObjBeginInerviewAction.UpdateInterviewStatus(VacRndId, ATSCommon.CurrentSession.Instance.VerifiedUser.UserId, IsComplete, ScheduleIntId, interviewStatus);
                if (InterviewComp)
                {
                    return RedirectToAction("GetInterviewDetails", new { VacRndId = VacRndId, ScheduleIntId = ScheduleIntId });
                }
                else
                {
                    IsError = true;
                    Message = "Interview not completed";
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

        private bool ServerValidation(out string ErrorMessage)
        {
            ErrorMessage = String.Empty;
            bool isServerError = false;
            if (!ModelState.IsValid)
            {
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
            return isServerError;
        }

        [HttpGet]
        public JsonResult GetAllScore(Guid AppId, Guid VacRndId, Guid VacQueId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            List<BEClient.GetScore> ObjGetScoreLst = new List<BEClient.GetScore>();
            try
            {
                BLClient.GetScoreAction ObjGetScoreAction = new BLClient.GetScoreAction(_CurrentClientId);
                ObjGetScoreLst = ObjGetScoreAction.GetAllScore(AppId, VacRndId, VacQueId);
            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, base.GetJson(ObjGetScoreLst)), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddUpdateAppComment(BEClient.AppComment objAppComment)
        {
            String Message = string.Empty;
            bool IsError = false;
            String Data = string.Empty;
            try
            {
                Guid AppCommentId = Guid.Empty;
                BLClient.AppCommentAction objAppCommentAction = new BLClient.AppCommentAction(_CurrentClientId);
                AppCommentId = objAppCommentAction.AddAppComment(objAppComment);
                if (AppCommentId != Guid.Empty)
                {
                    IsError = false;
                    Message = "Comment Added Successfully";
                    objAppCommentAction = new BLClient.AppCommentAction(_CurrentClientId);
                    BEClient.AppComment newobjAppComment = new BEClient.AppComment();
                    newobjAppComment = objAppCommentAction.GetAppCommentById(AppCommentId);
                    Data = base.RenderRazorViewToString("_CommentsDetails", newobjAppComment);
                }
                else
                {
                    IsError = true;
                    Message = "Comment Is not Added";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data));
        }

        public string GetCandidateSurveyLink(Guid ApplicationId, Guid VacRndId)
        {
            try
            {
                BLClient.ApplicationAction objApplicationAction = new BLClient.ApplicationAction(_CurrentClientId);
                Guid CandidateUserId = objApplicationAction.GetCandidateUserIdByApplicationId(ApplicationId);
                string encruserid = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(Convert.ToString(CandidateUserId));
                string surveyLink = "http://" + Common.ConfigurationMapped.Instance.DomianName;
                surveyLink = surveyLink + @Url.Action("Index", "CandidateSurvey", new { area = ATS.WebUi.Common.Constants.AREA_CANDIDATE, ApplicationId = ApplicationId, pVacRndId = VacRndId, UserId = encruserid });
                return surveyLink;
            }
            catch
            {
                throw;
            }
        }

        public JsonResult ResendSurveyReminder(Guid ApplicationId, Guid VacRndId)
        {
            String Message = string.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                BEClient.EmailTemplates objEmailTemplate = new BEClient.EmailTemplates();
                BLClient.EmailTemplatesAction objEmailTemplateAction = new BLClient.EmailTemplatesAction(_CurrentClientId);
                objEmailTemplate = objEmailTemplateAction.GetEmailTemplateByEmailIdentifier(_CurrentLanguageId, BEClient.EmailTemplateIdentifier.Candidate_Survey.ToString());

                String EmailDescription = objEmailTemplate.EmailDescription;
                BLClient.EmailContentAction objEmailContentAction = new BLClient.EmailContentAction(_CurrentClientId);
                objEmailTemplate = objEmailContentAction.GetEmailContentByAppIdAndVacRndId(objEmailTemplate, ApplicationId, VacRndId);

                string SurveyLink = GetCandidateSurveyLink(ApplicationId, VacRndId);
                objEmailTemplate.EmailDescription = objEmailTemplate.EmailDescription.Replace("##Link.CandidateSurvey##", "<a href =" + SurveyLink + ">Click Here</a>");

                Mailers.EmailTemplate mvcMailer = new Mailers.EmailTemplate();
                mvcMailer.SendMessage(objEmailTemplate.DestinationAddress, objEmailTemplate.Subject, objEmailTemplate.EmailDescription);
                Message = "Applicant Survey link sent successfully";
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data));
        }

        public JsonResult promoteCandidate(Guid AppId, Guid VacRndId, string Status)
        {
            String Message = string.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                BLClient.PromoteCandidateAction ObjPromoteCandidateAction = new BLClient.PromoteCandidateAction(_CurrentClientId);
                bool Result = false;
                BEClient.PromoteCandidate objPc = new BEClient.PromoteCandidate();
                objPc.ApplicationId = AppId;
                objPc.VacRndId = VacRndId;
                Result = ObjPromoteCandidateAction.UpdatePromoteCandidate(objPc, Status);
                if (!Result)
                {
                    IsError = true;
                    Message = "Applicant was not " + Status;
                }
                else
                {
                    int RoundAttributeNo = ATS.WebUi.Common.CommonFunctions.GetAttributesNoByVacRndId(VacRndId);
                    if (Enum.IsDefined(typeof(BEClient.DummyInterviewRound), RoundAttributeNo))
                    {
                        CommonCtrl objCommonCtrl = new CommonCtrl();
                        objCommonCtrl.CreateDummyInterview(AppId, VacRndId);
                    }

                    if (RoundAttributeNo == (int)BEClient.RndAttrNo.CandidateSurvey)
                    {
                        BLClient.AppAnswerAction objAppAnswerAction = new BLClient.AppAnswerAction(_CurrentClientId);
                        List<BEClient.AppAnswer> objAppAnsStatusList = new List<BEClient.AppAnswer>();
                        objAppAnsStatusList = objAppAnswerAction.GetAnswerStatusByVacRndId(AppId, VacRndId);
                        var objPendingList = objAppAnsStatusList.Where(m => m.IsAnsPending == true).ToList();
                        if (objPendingList.Count > 0)
                        {
                            BEClient.EmailTemplates objEmailTemplate = new BEClient.EmailTemplates();
                            BLClient.EmailTemplatesAction objEmailTemplateAction = new BLClient.EmailTemplatesAction(_CurrentClientId);
                            objEmailTemplate = objEmailTemplateAction.GetEmailTemplateByEmailIdentifier(_CurrentLanguageId, BEClient.EmailTemplateIdentifier.Candidate_Survey.ToString());

                            String EmailDescription = objEmailTemplate.EmailDescription;
                            BLClient.EmailContentAction objEmailContentAction = new BLClient.EmailContentAction(_CurrentClientId);
                            objEmailTemplate = objEmailContentAction.GetEmailContentByAppIdAndVacRndId(objEmailTemplate, AppId, VacRndId);

                            string SurveyLink = GetCandidateSurveyLink(AppId, VacRndId);
                            objEmailTemplate.EmailDescription = Regex.Replace(objEmailTemplate.EmailDescription, "##Link.CandidateSurvey##", "<a href =" + SurveyLink + ">Click Here</a>", RegexOptions.IgnoreCase);

                            Mailers.EmailTemplate mvcMailer = new Mailers.EmailTemplate();
                            mvcMailer.SendMessage(objEmailTemplate.DestinationAddress, objEmailTemplate.Subject, objEmailTemplate.EmailDescription);
                        }
                    }

                    Message = "Applicant " + Status + " " + "Successfully!.";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data));
        }

        public JsonResult promoteCandidateapp(Guid AppId, Guid VacRndId, Guid VacancyId)
        {
            String Message = string.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                BLClient.PromoteCandidateAction ObjPromoteCandidateAction = new BLClient.PromoteCandidateAction(_CurrentClientId);
                Guid Result = Guid.Empty;
                BEClient.PromoteCandidate objPc = new BEClient.PromoteCandidate();
                objPc.ApplicationId = AppId;
                objPc.VacRndId = VacRndId;
                Result = ObjPromoteCandidateAction.UpdatePromoteCandidateapp(VacRndId, AppId, VacancyId);
                Data = GetApplicationStatusMenu(VacancyId);
                if (Result == Guid.Empty)
                {
                    IsError = true;
                    Message = "Applicant was not promoted";
                }
                else
                {
                    VacRndId = Result;
                    int RoundAttributeNo = ATS.WebUi.Common.CommonFunctions.GetAttributesNoByVacRndId(VacRndId);
                    if (Enum.IsDefined(typeof(BEClient.DummyInterviewRound), RoundAttributeNo))
                    {
                        CommonCtrl objCommonCtrl = new CommonCtrl();
                        objCommonCtrl.CreateDummyInterview(AppId, VacRndId);
                    }

                    if (RoundAttributeNo == (int)BEClient.RndAttrNo.CandidateSurvey)
                    {
                        BLClient.AppAnswerAction objAppAnswerAction = new BLClient.AppAnswerAction(_CurrentClientId);
                        List<BEClient.AppAnswer> objAppAnsStatusList = new List<BEClient.AppAnswer>();
                        objAppAnsStatusList = objAppAnswerAction.GetAnswerStatusByVacRndId(AppId, VacRndId);
                        var objPendingList = objAppAnsStatusList.Where(m => m.IsAnsPending == true).ToList();
                        if (objPendingList.Count > 0)
                        {
                            BEClient.EmailTemplates objEmailTemplate = new BEClient.EmailTemplates();
                            BLClient.EmailTemplatesAction objEmailTemplateAction = new BLClient.EmailTemplatesAction(_CurrentClientId);
                            objEmailTemplate = objEmailTemplateAction.GetEmailTemplateByEmailIdentifier(_CurrentLanguageId, BEClient.EmailTemplateIdentifier.Candidate_Survey.ToString());

                            String EmailDescription = objEmailTemplate.EmailDescription;
                            BLClient.EmailContentAction objEmailContentAction = new BLClient.EmailContentAction(_CurrentClientId);
                            objEmailTemplate = objEmailContentAction.GetEmailContentByAppIdAndVacRndId(objEmailTemplate, AppId, VacRndId);

                            string SurveyLink = GetCandidateSurveyLink(AppId, VacRndId);
                            objEmailTemplate.EmailDescription = objEmailTemplate.EmailDescription.Replace("##Link.CandidateSurvey##", "<a href =" + SurveyLink + ">Click Here</a>");

                            Mailers.EmailTemplate mvcMailer = new Mailers.EmailTemplate();
                            mvcMailer.SendMessage(objEmailTemplate.DestinationAddress, objEmailTemplate.Subject, objEmailTemplate.EmailDescription);
                        }
                    }

                    Message = "Applicant Promoted Successfully!.";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data));
        }

        public JsonResult DemoteCandidateapp(Guid AppId, Guid VacRndId, Guid VacancyId)
        {
            String Message = string.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                BLClient.PromoteCandidateAction ObjPromoteCandidateAction = new BLClient.PromoteCandidateAction(_CurrentClientId);
                Guid Result = Guid.Empty;
                BEClient.PromoteCandidate objPc = new BEClient.PromoteCandidate();
                objPc.ApplicationId = AppId;
                objPc.VacRndId = VacRndId;
                Result = ObjPromoteCandidateAction.UpdateDemoteCandidateapp(VacRndId, AppId, VacancyId);
                Data = GetApplicationStatusMenu(VacancyId);
                if (Result == Guid.Empty)
                {
                    IsError = true;
                    Message = "Applicant was not Demoted";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data));
        }

        public string GetApplicationStatusMenu(Guid VacancyId, string Status = "Active")
        {
            BLClient.VacancyApplicantAction _objVacancyApplicantAction = new BLClient.VacancyApplicantAction(_CurrentClientId);
            List<BEClient.VacancyApplicant> objList = _objVacancyApplicantAction.GetApplicationStatusWithCount(VacancyId);
            ViewBag.ApplicationStatusListWithCount = new SelectList(objList, "TotalCount", "ApplicantionStatus");
            BEClient.VacancyApplicant objSeletedStatus = objList.Where(x => x.ApplicantionStatus == Status).ToList()[0];
            ViewBag.SelectedStatus = objSeletedStatus.ApplicantionStatus;
            return base.RenderRazorViewToString("_ApplicationStatusMenu", null);
        }

        [HttpGet]
        public JsonResult GetCandidateHistory(Guid ApplicationId)
        {
            String Message = string.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                BLClient.CandidateHistoryAction ObjCandidateHistory = new BLClient.CandidateHistoryAction(_CurrentClientId);
                List<BEClient.CandidateHistory> ObjCandidateHistoryList = new List<BEClient.CandidateHistory>();
                ObjCandidateHistoryList = ObjCandidateHistory.GetCandidateHistoryByApplicationId(ApplicationId);
                if (ObjCandidateHistoryList.Count > 0)
                {
                    Data = base.RenderRazorViewToString("_CandidateHistory", ObjCandidateHistoryList);
                }
                else
                {
                    Data = base.RenderRazorViewToString("_CandidateHistory", ObjCandidateHistoryList);
                }
            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetOfferRound(Guid AppId, int LastOfferStatusId, Guid VacRndId)
        {
            string Data = string.Empty;
            string Message = string.Empty;
            bool IsError = false;
            try
            {
                BLClient.VacancyOfferAction ObjVacancyOfferAction = null;
                if (AppId != null && AppId != Guid.Empty && VacRndId != null && VacRndId != Guid.Empty)
                {
                    BEClient.VacancyOffer objvacancyOffer = new BEClient.VacancyOffer();
                    BEClient.OfferTemplates objOfferTemplates = new BEClient.OfferTemplates();
                    ObjVacancyOfferAction = new BLClient.VacancyOfferAction(_CurrentClientId);
                    objvacancyOffer = ObjVacancyOfferAction.GetTemplateOfferByVacRndId(VacRndId, AppId, Common.CurrentSession.Instance.UserId);
                    if (objvacancyOffer != null)
                    {
                        objvacancyOffer.IsReviewer = true;
                        objvacancyOffer.ApplicationId = AppId;
                        objvacancyOffer.OfferDate = System.DateTime.Now;
                        ObjVacancyOfferAction = new BLClient.VacancyOfferAction(_CurrentClientId);
                        objvacancyOffer.RndNo = ObjVacancyOfferAction.GetOfferCountByApplicationId(AppId);
                        FillEmailAndOfferLetterByIds(objvacancyOffer.EmailToCandidateIdList, objvacancyOffer.OfferLetterIdList);
                        FillDropdown(AppId);
                        ViewBag.IsCandidate = false;
                        if (LastOfferStatusId == 0)
                        {
                            objvacancyOffer.OfferStatusText = BEClient.VacancyOfferStatus.Draft.ToString();
                            objvacancyOffer.OfferTypeText = BEClient.VacancyOfferType.Draft_Offer.ToString().Replace("_", " ");
                            objvacancyOffer.OfferTypeId = Convert.ToInt32(BEClient.VacancyOfferType.Draft_Offer);
                        }
                        else
                        {
                            objvacancyOffer.IsNewOffer = true;
                            objvacancyOffer.OfferStatusText = BEClient.VacancyOfferStatus.Draft.ToString();
                            objvacancyOffer.OfferTypeText = BEClient.VacancyOfferType.Draft_Offer.ToString().Replace("_", " ");
                            objvacancyOffer.OfferTypeId = Convert.ToInt32(BEClient.VacancyOfferType.New_Offer);
                            objvacancyOffer.OfferStatusId = Convert.ToInt32(BEClient.VacancyOfferStatus.Offer_Made);
                        }

                        BLClient.OfferAttachmentAction objOfferAttachmentAction = new BLClient.OfferAttachmentAction(_CurrentClientId);
                        objvacancyOffer.OfferAttachmentList = objOfferAttachmentAction.GetOfferAttachmentsByOfferTemplateId(objvacancyOffer.OfferTemplateId);
                        Data = base.RenderRazorViewToString("VacancyOffer/_AccVacancyOfferNew", objvacancyOffer);
                    }
                    else
                    {
                        Message = "Award Template is not selected for this round";
                        IsError = true;
                        return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch
            {
                throw;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetNewOfferRound(Guid AppId, int LastOfferStatusId, Guid VacRndId)
        {
            string Data = string.Empty;
            string Message = string.Empty;
            bool IsError = false;
            try
            {
                BLClient.VacancyOfferAction ObjVacancyOfferAction = null;
                if (AppId != null && AppId != Guid.Empty && VacRndId != null && VacRndId != Guid.Empty)
                {
                    BEClient.VacancyOffer objvacancyOffer = new BEClient.VacancyOffer();
                    BEClient.OfferTemplates objOfferTemplates = new BEClient.OfferTemplates();
                    ObjVacancyOfferAction = new BLClient.VacancyOfferAction(_CurrentClientId);
                    objvacancyOffer = ObjVacancyOfferAction.GetTemplateOfferByVacRndId(VacRndId, AppId, Common.CurrentSession.Instance.UserId);
                    objvacancyOffer.IsReviewer = true;
                    objvacancyOffer.ApplicationId = AppId;
                    objvacancyOffer.OfferDate = System.DateTime.Now;
                    ObjVacancyOfferAction = new BLClient.VacancyOfferAction(_CurrentClientId);
                    objvacancyOffer.RndNo = ObjVacancyOfferAction.GetOfferCountByApplicationId(AppId);
                    FillEmailAndOfferLetterByIds(objvacancyOffer.EmailToCandidateIdList, objvacancyOffer.OfferLetterIdList);
                    FillDropdown(AppId);
                    ViewBag.IsCandidate = false;
                    if (LastOfferStatusId == 0)
                    {
                        objvacancyOffer.OfferStatusText = BEClient.VacancyOfferStatus.Draft.ToString();
                        objvacancyOffer.OfferTypeText = BEClient.VacancyOfferType.Draft_Offer.ToString().Replace("_", " ");
                        objvacancyOffer.OfferTypeId = Convert.ToInt32(BEClient.VacancyOfferType.Draft_Offer);
                    }
                    else
                    {
                        objvacancyOffer.IsNewOffer = true;
                        objvacancyOffer.OfferStatusText = BEClient.VacancyOfferStatus.Draft.ToString();
                        objvacancyOffer.OfferTypeText = BEClient.VacancyOfferType.Draft_Offer.ToString().Replace("_", " ");
                        objvacancyOffer.OfferTypeId = Convert.ToInt32(BEClient.VacancyOfferType.New_Offer);
                        objvacancyOffer.OfferStatusId = Convert.ToInt32(BEClient.VacancyOfferStatus.Offer_Made);
                    }
                    Data = base.RenderRazorViewToString("VacancyOffer/_MakeOffer", objvacancyOffer);
                }
            }
            catch
            {
                throw;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetVacancyOfferById(Guid? VacancyOfferId, Guid AppId, bool IsRev = false, bool IsCandidate = false, int lastOfferTypeId = 2, string VacRndId = "")
        {
            string Data = string.Empty;
            string Message = string.Empty;
            bool IsError = false;
            try
            {
                Guid _VacOfferId = Guid.Empty;
                Guid.TryParse(VacancyOfferId.ToString(), out _VacOfferId);
                BEClient.VacancyOffer objvacancyOffer = new BEClient.VacancyOffer();
                BLClient.VacancyOfferAction VacOfferAction = null;
                BLClient.ApplicationHistoryAction objApplicationHistoryAction;
                ViewBag.IsCandidate = IsCandidate == true ? true : false;
                if (_VacOfferId != Guid.Empty)
                {
                    VacOfferAction = new BLClient.VacancyOfferAction(_CurrentClientId);
                    Guid _VacRndId = Guid.Empty;
                    Guid.TryParse(VacRndId, out _VacRndId);
                    objvacancyOffer = VacOfferAction.GetVacancyOfferById(_VacOfferId, _CurrentLanguageId, _VacRndId, Common.CurrentSession.Instance.UserId);
                    if (IsCandidate)
                    {
                        objvacancyOffer.ResponseFromApplicant = "";
                    }
                    FillEmailAndOfferLetterByIds(objvacancyOffer.EmailToCandidateIdList, objvacancyOffer.OfferLetterIdList);
                    FillDropdown(AppId);
                    objvacancyOffer.IsReviewer = IsRev;
                    objApplicationHistoryAction = new BLClient.ApplicationHistoryAction(_CurrentClientId);
                    objvacancyOffer.objApplicationHistory = objApplicationHistoryAction.GetApplicationHistoryByApplicationId(objvacancyOffer.ApplicationId, _CurrentLanguageId);
                    BLClient.OfferAttachmentAction objOfferAttachmentAction = new BLClient.OfferAttachmentAction(_CurrentClientId);
                    objvacancyOffer.OfferAttachmentList = objOfferAttachmentAction.GetOfferAttachmentsByOfferTemplateId(objvacancyOffer.OfferTemplateId);
                    Data = base.RenderRazorViewToString("VacancyOffer/_MakeOffer", objvacancyOffer);
                }
                else
                {
                    VacOfferAction = new BLClient.VacancyOfferAction(_CurrentClientId);
                    objvacancyOffer = VacOfferAction.GetCandidateNameAndJobTitleByApp(AppId);
                    objvacancyOffer.IsReviewer = IsRev;
                    if (lastOfferTypeId == 5)
                    {
                        objvacancyOffer.OfferStatusText = BEClient.VacancyOfferStatus.Draft.ToString();
                        objvacancyOffer.OfferTypeText = BEClient.VacancyOfferType.Draft_Offer.ToString().Replace("_", " ");
                        objvacancyOffer.OfferTypeId = Convert.ToInt32(BEClient.VacancyOfferType.New_Offer);
                    }
                    else
                    {
                        objvacancyOffer.OfferStatusText = BEClient.VacancyOfferStatus.Draft.ToString();
                        objvacancyOffer.OfferTypeText = BEClient.VacancyOfferType.Draft_Offer.ToString().Replace("_", " ");
                        objvacancyOffer.OfferTypeId = Convert.ToInt32(BEClient.VacancyOfferType.Draft_Offer);
                    }
                    objvacancyOffer.OfferDate = System.DateTime.Now;
                    Data = base.RenderRazorViewToString("VacancyOffer/_MakeOffer", objvacancyOffer);
                }
            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.ToString();
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult InsertVacancyOffer(BEClient.VacancyOffer vacancyOffer, string EmployeeAction, string CandidateAction)
        {
            if (EmployeeAction != null && EmployeeAction != string.Empty)
                return EmployerActionsOnOffer(vacancyOffer, EmployeeAction);
            else
                return CandidateActionsOnOffer(vacancyOffer, CandidateAction);
        }

        public JsonResult EmployerActionsOnOffer(BEClient.VacancyOffer objVacancyOffer, string EmployeeAction)
        {
            string Message = string.Empty;
            bool IsError = false;
            string Data = string.Empty;
            try
            {
                if (objVacancyOffer != null)
                {
                    #region Add/Update Offer With Status
                    BEOfferStatus OfferStatus = (BEOfferStatus)objVacancyOffer.OfferStatusId;
                    BEOfferType OfferType = (BEOfferType)objVacancyOffer.OfferTypeId;
                    BLClient.VacancyOfferAction ObjVacancyOfferAction = new BLClient.VacancyOfferAction(_CurrentClientId);
                    Guid VacancyOfferId = Guid.Empty;
                    if (objVacancyOffer.VacancyOfferId == Guid.Empty)
                    {
                        objVacancyOffer.OfferDate = System.DateTime.Today;
                        if (EmployeeAction != null && (EmployeeAction == "Confirm Award"))
                        {
                            if (objVacancyOffer.OfferStatusId == Convert.ToInt32(BEOfferStatus.Candidate_Countered))
                            {
                                objVacancyOffer.OfferStatusId = Convert.ToInt32(BEOfferStatus.Company_Countered);
                                Message = "Company Countered Successfully";
                            }
                            else if (objVacancyOffer.OfferTypeId == Convert.ToInt32(BEOfferType.New_Offer))
                            {
                                objVacancyOffer.OfferStatusId = Convert.ToInt32(BEOfferStatus.Offer_Made);
                                Message = "Award Added Successfully";
                            }
                            else
                            {
                                objVacancyOffer.OfferStatusId = Convert.ToInt32(BEOfferStatus.Offer_Made);
                                objVacancyOffer.OfferTypeId = Convert.ToInt32(BEOfferType.Initial_Offer);
                                Message = "Award Added Successfully";
                            }
                        }
                        else
                        {
                            objVacancyOffer.OfferStatusId = Convert.ToInt32(BEOfferStatus.Draft);
                            objVacancyOffer.OfferTypeId = Convert.ToInt32(BEOfferType.Draft_Offer);
                            Message = "Award Draft Successfully";
                        }
                        objVacancyOffer.ResponseFromApplicant = "";
                        VacancyOfferId = ObjVacancyOfferAction.AddVacancyOffer(objVacancyOffer);
                        if (VacancyOfferId != Guid.Empty)
                        {
                            #region ApplicationHistory
                            if (EmployeeAction != null && EmployeeAction == "Confirm Award")
                            {
                                BLClient.ApplicationHistoryAction objApplicationHistoryAction = new BLClient.ApplicationHistoryAction(_CurrentClientId);
                                BEClient.ApplicationHistory objAppHistory = new BEClient.ApplicationHistory();
                                objAppHistory.VacancyOfferStatusId = Convert.ToInt32(BEOfferStatus.Offer_Made);
                                objAppHistory.Description = str_OfferMade_HistoryDescription;
                                objAppHistory.ApplicationId = objVacancyOffer.ApplicationId;
                                objApplicationHistoryAction.AddApplicationHistory(objAppHistory);
                            }
                            #endregion
                        }
                        else
                        {
                            IsError = true;
                            Message = "Award Not Added";
                        }
                    }
                    else
                    {
                        bool Result = false;
                        bool OfferIsDraft = false;
                        if (EmployeeAction != null && (EmployeeAction == "MakeOffer" || EmployeeAction == "Confirm Award"))
                        {
                            if (objVacancyOffer.OfferStatusId == Convert.ToInt32(BEOfferStatus.Candidate_Countered))
                            {
                                objVacancyOffer.OfferStatusId = Convert.ToInt32(BEOfferStatus.Company_Countered);
                                Message = "Company Countered Successfully";
                            }
                            else
                            {
                                //Create new offer after candidate decline it.
                                if (OfferStatus == BEOfferStatus.Candidate_Declined || OfferStatus == BEOfferStatus.Candidate_Retracted)
                                {
                                    objVacancyOffer.OfferTypeId = Convert.ToInt32(BEOfferType.New_Offer);
                                }
                                else
                                {
                                    if (OfferType != BEOfferType.Initial_Offer && OfferType != BEOfferType.Revised_Offer && OfferType != BEOfferType.New_Offer)
                                    {
                                        objVacancyOffer.OfferTypeId = Convert.ToInt32(BEOfferType.Initial_Offer);
                                        if (OfferType == BEOfferType.Draft_Offer)
                                        { OfferIsDraft = true; }
                                    }
                                    else //If revised the offer
                                    {
                                        objVacancyOffer.OfferTypeId = Convert.ToInt32(BEOfferType.Revised_Offer);
                                    }
                                }
                                objVacancyOffer.OfferStatusId = Convert.ToInt32(BEOfferStatus.Offer_Made);
                                Message = "Award Added Successfully";
                            }
                            objVacancyOffer.OfferDate = DateTime.Today;
                            objVacancyOffer.ResponseFromApplicant = "";
                            //Insert Offer in DB
                            if (!OfferIsDraft)
                            {
                                VacancyOfferId = ObjVacancyOfferAction.AddVacancyOffer(objVacancyOffer);
                            }
                            else
                            {
                                Result = ObjVacancyOfferAction.UpdateVacancyOffer(objVacancyOffer);
                                VacancyOfferId = objVacancyOffer.VacancyOfferId;
                            }
                            objVacancyOffer.VacancyOfferId = VacancyOfferId;
                            Result = true;
                        }
                        else if (EmployeeAction != null && EmployeeAction == "Retract Award")
                        {
                            objVacancyOffer.OfferStatusId = Convert.ToInt32(BEOfferStatus.Retracted);
                            Result = ObjVacancyOfferAction.UpdateVacancyOffer(objVacancyOffer);
                            Message = "Award Retracted Successfully";
                        }
                        else if (EmployeeAction != null && EmployeeAction == "Confirm Acceptance")
                        {
                            objVacancyOffer.OfferStatusId = Convert.ToInt32(BEOfferStatus.Company_Confirmed);
                            objVacancyOffer.IsConfirmedByClient = true;
                            Result = ObjVacancyOfferAction.UpdateVacancyOffer(objVacancyOffer);
                            Message = "Award Accepted Successfully";
                        }
                        else
                        {
                            objVacancyOffer.OfferStatusId = Convert.ToInt32(BEOfferStatus.Draft);
                            Result = ObjVacancyOfferAction.UpdateVacancyOffer(objVacancyOffer);
                            Message = "Award Draft Successfully";
                        }
                        if (Result)
                        {
                            VacancyOfferId = objVacancyOffer.VacancyOfferId;
                            #region ApplicationHistory
                            if (EmployeeAction != null && (EmployeeAction == "Retract Award" || EmployeeAction == "Confirm Award" || EmployeeAction == "Confirm Acceptance"))
                            {
                                BLClient.ApplicationHistoryAction objApplicationHistoryAction = new BLClient.ApplicationHistoryAction(_CurrentClientId);
                                BEClient.ApplicationHistory objAppHistory = new BEClient.ApplicationHistory();
                                if (objVacancyOffer.OfferStatusId == Convert.ToInt32(BEOfferStatus.Company_Countered))
                                {
                                    objAppHistory.VacancyOfferStatusId = Convert.ToInt32(BEOfferStatus.Company_Countered);
                                    objAppHistory.Description = str_CompanyCountered_HistoryDescription;
                                }
                                else if (objVacancyOffer.OfferStatusId == Convert.ToInt32(BEOfferStatus.Retracted))
                                {
                                    objAppHistory.VacancyOfferStatusId = Convert.ToInt32(BEOfferStatus.Retracted);
                                    objAppHistory.Description = str_Retrected_HistoryDescription;
                                }
                                else if (objVacancyOffer.OfferStatusId == Convert.ToInt32(BEOfferStatus.Company_Confirmed))
                                {
                                    objAppHistory.VacancyOfferStatusId = Convert.ToInt32(BEOfferStatus.Company_Confirmed);
                                    objAppHistory.Description = str_CompanyAccepted_HistoryDescription;
                                }
                                else
                                {
                                    objAppHistory.VacancyOfferStatusId = Convert.ToInt32(BEOfferStatus.Offer_Made);
                                    objAppHistory.Description = str_OfferMadeRevision_HistoryDescription;
                                }
                                objAppHistory.ApplicationId = objVacancyOffer.ApplicationId;
                                objApplicationHistoryAction.AddApplicationHistory(objAppHistory);
                            }
                            #endregion
                        }
                        else
                        {
                            IsError = true;
                            Message = "Award Not Updated";
                        }
                        ObjVacancyOfferAction = new BLClient.VacancyOfferAction(_CurrentClientId);
                    }
                    #endregion

                    #region Send Mail & With PDF
                    if (objVacancyOffer.OfferStatusId == (int)BEOfferStatus.Offer_Made || objVacancyOffer.OfferStatusId == (int)BEOfferStatus.Company_Countered || objVacancyOffer.OfferStatusId == (int)BEOfferStatus.Company_Confirmed)
                    {
                        string URL = "http://" + Common.ConfigurationMapped.Instance.DomianName;
                        URL = URL + "/Employee/Application/ConfirmOfferPDF?ClientName=" + CurrentClient.Clientname + "&UserId=" + Common.CurrentSession.Instance.UserId + "&VacancyOfferId=" + VacancyOfferId + "&AppId=" + objVacancyOffer.ApplicationId.ToString();
                        URL = URL + "&ETemplate=" + objVacancyOffer.EmailTemplateId.ToString();
                        if (!System.IO.Directory.Exists(Server.MapPath("~/Resume/OfferPDF")))
                        {
                            System.IO.Directory.CreateDirectory(Server.MapPath("~/Resume/OfferPDF"));
                        }
                        try
                        {
                            if (!string.IsNullOrEmpty(objVacancyOffer.EmailDescription) && !string.IsNullOrEmpty(objVacancyOffer.EmailSubject))
                            {
                                BEClient.VacancyOffer _objVacancyOfferTemp = ObjVacancyOfferAction.GetVacancyOfferByIdForPDF(VacancyOfferId, Guid.Empty, _CurrentLanguageId);
                                objVacancyOffer.JobTitle = _objVacancyOfferTemp.JobTitle;
                                objVacancyOffer.OfferStatusText = _objVacancyOfferTemp.OfferStatusText;
                                objVacancyOffer.OfferTypeText = _objVacancyOfferTemp.OfferTypeText;
                                objVacancyOffer.PlacementTypeText = _objVacancyOfferTemp.PlacementTypeText;
                                objVacancyOffer.JobTypeText = _objVacancyOfferTemp.JobTypeText;
                                objVacancyOffer.LocationText = _objVacancyOfferTemp.LocationText;
                                objVacancyOffer.SalaryTypeText = _objVacancyOfferTemp.SalaryTypeText;
                                objVacancyOffer.RatePeriodText = _objVacancyOfferTemp.RatePeriodText;
                                objVacancyOffer.BonusDescription = _objVacancyOfferTemp.BonusDescription;
                                objVacancyOffer.CommissionDescriprion = _objVacancyOfferTemp.CommissionDescriprion;
                                objVacancyOffer.CandidateNoticePeriodText = _objVacancyOfferTemp.CandidateNoticePeriodText;
                                objVacancyOffer.CandidateNoticePeriodTypeText = _objVacancyOfferTemp.CandidateNoticePeriodTypeText;
                                objVacancyOffer.CompanyNoticePeriodText = _objVacancyOfferTemp.CompanyNoticePeriodText;
                                objVacancyOffer.CompanyNoticePeriodTypeText = _objVacancyOfferTemp.CompanyNoticePeriodTypeText;
                                objVacancyOffer.CandidateName = _objVacancyOfferTemp.CandidateName;
                                objVacancyOffer.CandidateFirstName = _objVacancyOfferTemp.CandidateFirstName;
                                objVacancyOffer.CandidateLastName = _objVacancyOfferTemp.CandidateLastName;
                                objVacancyOffer.CandidateEmailId = _objVacancyOfferTemp.CandidateEmailId;
                                objVacancyOffer.CreatedByName = _objVacancyOfferTemp.CreatedByName;
                                objVacancyOffer.CreatedOn = _objVacancyOfferTemp.CreatedOn;
                                objVacancyOffer.UpdatedByName = _objVacancyOfferTemp.UpdatedByName;
                                objVacancyOffer.UpdatedOn = _objVacancyOfferTemp.UpdatedOn;
                                objVacancyOffer.ManagerName = _objVacancyOfferTemp.ManagerName;
                                string JobType = objVacancyOffer.JobType == 1 ? "Full-Time" : "Part-Time";
                                string strEmailContent = GetEmailContent(objVacancyOffer);
                                objVacancyOffer.EmailDescription = strEmailContent.ToString();
                                string path = "";
                                if (System.IO.File.Exists(Server.MapPath("~/Resume/OfferPDF/pdf-" + VacancyOfferId.ToString() + ".pdf")))
                                {
                                    if (objVacancyOffer.OfferStatusId != (int)BEOfferStatus.Company_Confirmed)
                                        path = Common.CommonFunctions.HtmlToPdf(URL, "~/Resume/OfferPDF/", "final-" + VacancyOfferId.ToString() + ".pdf", false);
                                }
                                else
                                {
                                    path = Common.CommonFunctions.HtmlToPdf(URL, "~/Resume/OfferPDF/", "pdf-" + VacancyOfferId.ToString() + ".pdf", false);
                                }
                                Mailers.EmailTemplate mvcMailer = new Mailers.EmailTemplate();
                                if (path != "" || (objVacancyOffer.OfferStatusId == (int)BEOfferStatus.Company_Confirmed))
                                {
                                    string OfferLetterPath = "";
                                    if (objVacancyOffer.OfferStatusId == Convert.ToInt32(BEOfferStatus.Company_Confirmed))
                                    {
                                        string OfferLetterURL = "http://" + Common.ConfigurationMapped.Instance.DomianName;
                                        OfferLetterURL = OfferLetterURL + "/Employee/Application/GenerateOfferLetterPDF?ClientName=" + CurrentClient.Clientname + "&VacancyOfferId=" + VacancyOfferId;

                                        OfferLetterPath = Common.CommonFunctions.HtmlToPdf(OfferLetterURL, "~/Resume/OfferPDF/", "OfferLetter-" + VacancyOfferId.ToString() + ".pdf", false);
                                        //Offer PDF & Offer Letter PDF Attachment
                                        if (OfferLetterPath != "")
                                        {
                                            //Offer PDF Attachment
                                            var attachmentLink = Server.MapPath(OfferLetterPath);
                                            if (objVacancyOffer.IncludeAttachments && objVacancyOffer.OfferAttachmentList != null)
                                            {
                                                foreach (var item in objVacancyOffer.OfferAttachmentList)
                                                {
                                                    if (item.IsMandatory)
                                                    {
                                                        attachmentLink += "," + Server.MapPath(item.Path);
                                                    }
                                                }
                                            }
                                            SendOfferMail(objVacancyOffer.CandidateEmailId, objVacancyOffer.EmailSubject, objVacancyOffer.EmailDescription, attachmentLink);
                                        }
                                    }
                                    else
                                    {
                                        //Offer PDF Attachment
                                        var attachmentLink = Server.MapPath(path);
                                        if (objVacancyOffer.IncludeAttachments && objVacancyOffer.OfferAttachmentList != null)
                                        {
                                            foreach (var item in objVacancyOffer.OfferAttachmentList)
                                            {
                                                if (item.IsMandatory)
                                                {
                                                    attachmentLink += "," + Server.MapPath(item.Path);
                                                }
                                            }
                                        }
                                        SendOfferMail(objVacancyOffer.CandidateEmailId, objVacancyOffer.EmailSubject, objVacancyOffer.EmailDescription, attachmentLink);
                                    }
                                }
                            }
                        }
                        catch
                        { }
                    }
                    else if (objVacancyOffer.OfferStatusId == (int)BEOfferStatus.Retracted || objVacancyOffer.OfferStatusId == (int)BEOfferStatus.Company_Declined)
                    {
                        try
                        {
                            BLClient.EmailTemplatesAction EmailTemplateAction = new BLClient.EmailTemplatesAction(_CurrentClientId);
                            BEClient.EmailTemplates objEmailTemplates = new BEClient.EmailTemplates();
                            objEmailTemplates = EmailTemplateAction.GetEmailTemplateById(_CurrentLanguageId, objVacancyOffer.EmailTemplateId);
                            if (objEmailTemplates != null)
                            {
                                BEClient.VacancyOffer _objVacancyOfferTemp = ObjVacancyOfferAction.GetVacancyOfferByIdForPDF(VacancyOfferId, Guid.Empty, _CurrentLanguageId);
                                objVacancyOffer.JobTitle = _objVacancyOfferTemp.JobTitle;
                                objVacancyOffer.OfferStatusText = _objVacancyOfferTemp.OfferStatusText;
                                objVacancyOffer.OfferTypeText = _objVacancyOfferTemp.OfferTypeText;
                                objVacancyOffer.PlacementTypeText = _objVacancyOfferTemp.PlacementTypeText;
                                objVacancyOffer.JobTypeText = _objVacancyOfferTemp.JobTypeText;
                                objVacancyOffer.LocationText = _objVacancyOfferTemp.LocationText;
                                objVacancyOffer.SalaryTypeText = _objVacancyOfferTemp.SalaryTypeText;
                                objVacancyOffer.RatePeriodText = _objVacancyOfferTemp.RatePeriodText;
                                objVacancyOffer.CandidateNoticePeriodText = _objVacancyOfferTemp.CandidateNoticePeriodText;
                                objVacancyOffer.CandidateNoticePeriodTypeText = _objVacancyOfferTemp.CandidateNoticePeriodTypeText;
                                objVacancyOffer.CompanyNoticePeriodText = _objVacancyOfferTemp.CompanyNoticePeriodText;
                                objVacancyOffer.CompanyNoticePeriodTypeText = _objVacancyOfferTemp.CompanyNoticePeriodTypeText;
                                objVacancyOffer.CandidateName = _objVacancyOfferTemp.CandidateName;
                                objVacancyOffer.CandidateFirstName = _objVacancyOfferTemp.CandidateFirstName;
                                objVacancyOffer.CandidateLastName = _objVacancyOfferTemp.CandidateLastName;
                                objVacancyOffer.CandidateEmailId = _objVacancyOfferTemp.CandidateEmailId;
                                objVacancyOffer.CreatedByName = _objVacancyOfferTemp.CreatedByName;
                                objVacancyOffer.CreatedOn = _objVacancyOfferTemp.CreatedOn;
                                objVacancyOffer.UpdatedByName = _objVacancyOfferTemp.UpdatedByName;
                                objVacancyOffer.UpdatedOn = _objVacancyOfferTemp.UpdatedOn;
                                objVacancyOffer.ManagerName = _objVacancyOfferTemp.ManagerName;
                                string JobType = objVacancyOffer.JobType == 1 ? "Full-Time" : "Part-Time";
                                string strEmailContent = GetEmailContent(objVacancyOffer);
                                objVacancyOffer.EmailDescription = strEmailContent.ToString();
                                Mailers.EmailTemplate mvcMailer = new Mailers.EmailTemplate();
                                SendOfferMail(objVacancyOffer.CandidateEmailId, objVacancyOffer.EmailSubject, objVacancyOffer.EmailDescription);
                            }
                        }
                        catch
                        {
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data));
        }

        public JsonResult CandidateActionsOnOffer(BEClient.VacancyOffer objVacancyOffer, string CandidateAction)
        {
            string Message = string.Empty;
            bool IsError = false;
            string Data = string.Empty;
            BEClient.VacancyOffer newVacancyOffer = new BEClient.VacancyOffer();
            try
            {
                if (objVacancyOffer != null)
                {
                    BLClient.VacancyOfferAction ObjVacancyOfferAction = new BLClient.VacancyOfferAction(_CurrentClientId);
                    Guid VacancyOfferId = objVacancyOffer.VacancyOfferId;
                    BLClient.ApplicationHistoryAction objApplicationHistoryAction = new BLClient.ApplicationHistoryAction(_CurrentClientId);
                    BEClient.ApplicationHistory objAppHistory = new BEClient.ApplicationHistory();
                    bool result = false;
                    if (objVacancyOffer.VacancyOfferId != Guid.Empty)
                    {
                        //If candidate ACCEPT the offer
                        if (CandidateAction != null && CandidateAction == "Accept Award")
                        {
                            objVacancyOffer.OfferStatusId = Convert.ToInt32(BEClient.VacancyOfferStatus.Candidate_Accepted);
                            result = ObjVacancyOfferAction.AcceptOfferByCandidate(objVacancyOffer);
                            //Set history daetail
                            objAppHistory.VacancyOfferStatusId = objVacancyOffer.OfferStatusId;
                            objAppHistory.Description = str_CandidateAccepted_HistoryDescription;
                            Message = "Award Accepted Successfully";
                        }
                        //If candidate DECLINE the offer
                        else if (CandidateAction != null && (CandidateAction == "Decline Award"))
                        {
                            objVacancyOffer.ResponseFromApplicant = objVacancyOffer.CandidateDeclineReason;
                            objVacancyOffer.OfferStatusId = Convert.ToInt32(BEClient.VacancyOfferStatus.Candidate_Declined);
                            result = ObjVacancyOfferAction.DeclinedOfferByCandidate(objVacancyOffer);
                            //Set history detail
                            objAppHistory.VacancyOfferStatusId = objVacancyOffer.OfferStatusId;
                            objAppHistory.Description = str_CandidateDeclined_HistoryDescription;

                            Message = "Award Declined Successfully";
                        }
                        else if (CandidateAction != null && (CandidateAction == "Retract Acceptance"))
                        {
                            objVacancyOffer.ResponseFromApplicant = objVacancyOffer.CandidateDeclineReason;
                            objVacancyOffer.OfferStatusId = Convert.ToInt32(BEClient.VacancyOfferStatus.Candidate_Retracted);
                            result = ObjVacancyOfferAction.DeclinedOfferByCandidate(objVacancyOffer);
                            //Set history detail
                            objAppHistory.VacancyOfferStatusId = objVacancyOffer.OfferStatusId;
                            objAppHistory.Description = str_CandidateRetractAcceptance_HistoryDescription;

                            Message = "Award Retracted Successfully";
                        }
                        //If candidate COUNTERED(Negotiate) the offer
                        else if (CandidateAction != null && CandidateAction == "Counter Award")
                        {
                            objVacancyOffer.ResponseFromApplicant = objVacancyOffer.CandidateCounterReason;
                            objVacancyOffer.OfferStatusId = Convert.ToInt32(BEClient.VacancyOfferStatus.Candidate_Countered);
                            objVacancyOffer.OfferTypeId = Convert.ToInt32(BEClient.VacancyOfferType.Candidate_Counter);
                            objVacancyOffer.NoteToApplicant = "";
                            VacancyOfferId = ObjVacancyOfferAction.AddVacancyOffer(objVacancyOffer);
                            //Set history detail
                            objAppHistory.VacancyOfferStatusId = objVacancyOffer.OfferStatusId;
                            objAppHistory.Description = str_CandidateCountered_HistoryDescription;
                            Message = "Award Countered Successfully";
                        }

                        if (VacancyOfferId != Guid.Empty || result)
                        {
                            #region ApplicationHistory
                            if (CandidateAction != null && CandidateAction != string.Empty)
                            {
                                objAppHistory.ApplicationId = objVacancyOffer.ApplicationId;
                                objApplicationHistoryAction.AddApplicationHistory(objAppHistory);
                            }
                            #endregion
                        }
                        else
                        {
                            IsError = true;
                            Message = "Award Not Added";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data));
        }
        public String GetEmailContent(ATS.BusinessEntity.VacancyOffer objVacancyOffer)
        {
            String strEmailMsg = objVacancyOffer.EmailDescription;
            strEmailMsg = Regex.Replace(strEmailMsg, "##CANDIDATE.FIRSTNAME##", objVacancyOffer.CandidateFirstName, RegexOptions.IgnoreCase);
            strEmailMsg = Regex.Replace(strEmailMsg, "##CANDIDATE.LASTNAME##", objVacancyOffer.CandidateLastName, RegexOptions.IgnoreCase);
            strEmailMsg = Regex.Replace(strEmailMsg, "##CANDIDATE.FULLNAME##", objVacancyOffer.CandidateName, RegexOptions.IgnoreCase);
            strEmailMsg = Regex.Replace(strEmailMsg, "##VACANCY.JOBTITLE##", objVacancyOffer.JobTitle, RegexOptions.IgnoreCase);
            strEmailMsg = Regex.Replace(strEmailMsg, "##OFFER.VACANCYNAME##", objVacancyOffer.JobTitle, RegexOptions.IgnoreCase);
            if (objVacancyOffer.StartDate > DateTime.MinValue)
                strEmailMsg = Regex.Replace(strEmailMsg, "##VACANCY.STARTDATE##", objVacancyOffer.StartDateText, RegexOptions.IgnoreCase);
            else
                strEmailMsg = Regex.Replace(strEmailMsg, "##VACANCY.STARTDATE##", "NA", RegexOptions.IgnoreCase);
            strEmailMsg = Regex.Replace(strEmailMsg, "##OFFER.OFFERSTATUS##", objVacancyOffer.OfferStatusText, RegexOptions.IgnoreCase);
            strEmailMsg = Regex.Replace(strEmailMsg, "##OFFER.CREATEDBY##", objVacancyOffer.CreatedByName, RegexOptions.IgnoreCase);
            strEmailMsg = Regex.Replace(strEmailMsg, "##OFFER.CREATEDON##", objVacancyOffer.CreatedOn.ToString("MM/dd/yyyy hh:ss tt"), RegexOptions.IgnoreCase);
            if (objVacancyOffer.UpdatedOn > DateTime.MinValue)
            {
                strEmailMsg = Regex.Replace(strEmailMsg, "##OFFER.UPDATEDBY##", objVacancyOffer.UpdatedByName, RegexOptions.IgnoreCase);
                strEmailMsg = Regex.Replace(strEmailMsg, "##OFFER.UPDATEDON##", objVacancyOffer.UpdatedOn.ToString("MM/dd/yyyy hh:ss tt"), RegexOptions.IgnoreCase);
                strEmailMsg = Regex.Replace(strEmailMsg, "##OFFER.MODIFIEDBY##", objVacancyOffer.UpdatedByName, RegexOptions.IgnoreCase);
                strEmailMsg = Regex.Replace(strEmailMsg, "##OFFER.MODIFIEDON##", objVacancyOffer.UpdatedOn.ToString("MM/dd/yyyy hh:ss tt"), RegexOptions.IgnoreCase);
            }
            else
            {
                strEmailMsg = Regex.Replace(strEmailMsg, "##OFFER.UPDATEDBY##", "", RegexOptions.IgnoreCase);
                strEmailMsg = Regex.Replace(strEmailMsg, "##OFFER.UPDATEDON##", "NA", RegexOptions.IgnoreCase);
                strEmailMsg = Regex.Replace(strEmailMsg, "##OFFER.MODIFIEDBY##", "", RegexOptions.IgnoreCase);
                strEmailMsg = Regex.Replace(strEmailMsg, "##OFFER.MODIFIEDON##", "NA", RegexOptions.IgnoreCase);
            }
            strEmailMsg = Regex.Replace(strEmailMsg, "##OFFER.PLACEMENTTYPE##", objVacancyOffer.PlacementTypeText, RegexOptions.IgnoreCase);
            strEmailMsg = Regex.Replace(strEmailMsg, "##OFFER.JOBTYPE##", objVacancyOffer.JobTypeText, RegexOptions.IgnoreCase);
            strEmailMsg = Regex.Replace(strEmailMsg, "##VACANCY.JOBTYPE##", objVacancyOffer.JobTypeText, RegexOptions.IgnoreCase);
            if (objVacancyOffer.StartDate > DateTime.MinValue)
            {
                strEmailMsg = Regex.Replace(strEmailMsg, "##OFFER.STARTDATE##", objVacancyOffer.StartDate.ToString("MM/dd/yyyy"), RegexOptions.IgnoreCase);
            }
            else
            {
                strEmailMsg = Regex.Replace(strEmailMsg, "##OFFER.STARTDATE##", "NA", RegexOptions.IgnoreCase);
            }

            if (objVacancyOffer.PlacementType != Convert.ToInt32(BEClient.PlaceMentType.Permanent).ToString() && objVacancyOffer.EndDate > DateTime.MinValue)
            {
                strEmailMsg = Regex.Replace(strEmailMsg, "##OFFER.ENDDATE##", objVacancyOffer.EndDateText, RegexOptions.IgnoreCase);
                strEmailMsg = Regex.Replace(strEmailMsg, "##VACANCY.ENDDATE##", objVacancyOffer.EndDateText, RegexOptions.IgnoreCase);
            }
            else
            {
                strEmailMsg = Regex.Replace(strEmailMsg, "##OFFER.ENDDATE##", "NA", RegexOptions.IgnoreCase);
                strEmailMsg = Regex.Replace(strEmailMsg, "##VACANCY.ENDDATE##", "NA", RegexOptions.IgnoreCase);
            }

            strEmailMsg = Regex.Replace(strEmailMsg, "##OFFER.PLACEMENTLOCATION##", objVacancyOffer.LocationText, RegexOptions.IgnoreCase);
            strEmailMsg = Regex.Replace(strEmailMsg, "##OFFER.SALARYTYPE##", objVacancyOffer.SalaryTypeText, RegexOptions.IgnoreCase);

            string _salaryOffered = "";
            if (objVacancyOffer.PlacementType == Convert.ToInt32(BEClient.PlaceMentType.Permanent).ToString())
            {
                if (objVacancyOffer.SalaryType == Convert.ToInt32(BEClient.SalaryType.Salary))
                {
                    _salaryOffered = "$" + objVacancyOffer.SalaryOffered.ToString("##,##,###.##");
                }
                else if (objVacancyOffer.SalaryType == Convert.ToInt32(BEClient.SalaryType.Hourly))
                {
                    _salaryOffered = "Hours Per Week: " + objVacancyOffer.HoursPerWeek.ToString();
                    _salaryOffered = "  Hourly Wage: $" + objVacancyOffer.HourlyWage.ToString();
                }
                else if (objVacancyOffer.SalaryType == Convert.ToInt32(BEClient.SalaryType.PieceType))
                {
                    _salaryOffered = "Rate $" + objVacancyOffer.Rate.ToString("##,##,###.##");
                    _salaryOffered = " Per " + objVacancyOffer.Per;
                }
            }
            else
            {
                _salaryOffered = "Pay Rate: $" + objVacancyOffer.PayRate.ToString("##,##,###.##");
                _salaryOffered = "  Rate Period: " + objVacancyOffer.RatePeriod.ToString();
            }
            strEmailMsg = Regex.Replace(strEmailMsg, "##OFFER.SALARYOFFERED##", _salaryOffered, RegexOptions.IgnoreCase);

            strEmailMsg = Regex.Replace(strEmailMsg, "##OFFER.BONUSDESCRIPTION##", objVacancyOffer.BonusDescription.ToString(), RegexOptions.IgnoreCase);
            strEmailMsg = Regex.Replace(strEmailMsg, "##OFFER.BONUSPOTENTIAL##", objVacancyOffer.AnnualBonusPotential.ToString("##,##,###.##"), RegexOptions.IgnoreCase);
            strEmailMsg = Regex.Replace(strEmailMsg, "##OFFER.BONUSCAP##", objVacancyOffer.BonusCap.ToString("##,##,###.##"), RegexOptions.IgnoreCase);
            strEmailMsg = Regex.Replace(strEmailMsg, "##OFFER.COMMISSIONDESCRIPTION##", objVacancyOffer.CommissionDescriprion, RegexOptions.IgnoreCase);
            strEmailMsg = Regex.Replace(strEmailMsg, "##OFFER.COMMISSIONPOTENTIAL##", objVacancyOffer.AnnualCommissionPotential.ToString("##,##,###.##"), RegexOptions.IgnoreCase);
            strEmailMsg = Regex.Replace(strEmailMsg, "##OFFER.COMMISSIONCAP##", objVacancyOffer.CommissionCap.ToString("##,##,###.##"), RegexOptions.IgnoreCase);
            strEmailMsg = Regex.Replace(strEmailMsg, "##OFFER.CANDIDATENOTICEPERIOD##", objVacancyOffer.CandidateNoticePeriodText, RegexOptions.IgnoreCase);
            strEmailMsg = Regex.Replace(strEmailMsg, "##OFFER.CANDIDATENOTICEPERIODIN##", objVacancyOffer.CandidateNoticePeriodTypeText, RegexOptions.IgnoreCase);
            strEmailMsg = Regex.Replace(strEmailMsg, "##OFFER.COMPANYNOTICEPERIOD##", objVacancyOffer.CompanyNoticePeriodText, RegexOptions.IgnoreCase);
            strEmailMsg = Regex.Replace(strEmailMsg, "##OFFER.COMPANYNOTICEPERIODIN##", objVacancyOffer.CompanyNoticePeriodTypeText, RegexOptions.IgnoreCase);
            strEmailMsg = Regex.Replace(strEmailMsg, "##OFFER.COMPANYNAME##", ATS.WebUi.Common.CurrentSession.Instance.VerifiedClient.Clientname, RegexOptions.IgnoreCase);
            strEmailMsg = Regex.Replace(strEmailMsg, "##OFFER.MANAGERNAME##", objVacancyOffer.ManagerName, RegexOptions.IgnoreCase);
            return strEmailMsg;
        }


        //public String GetEmailContent(ATS.BusinessEntity.VacancyOffer objVacancyOffer)
        //{
        //    String strEmailMsg = objVacancyOffer.EmailDescription;
        //    strEmailMsg = Regex.Replace(strEmailMsg, "##Applicant.FIRSTNAME##", objVacancyOffer.CandidateFirstName, RegexOptions.IgnoreCase);
        //    strEmailMsg = Regex.Replace(strEmailMsg, "##Applicant.LASTNAME##", objVacancyOffer.CandidateLastName, RegexOptions.IgnoreCase);
        //    strEmailMsg = Regex.Replace(strEmailMsg, "##Applicant.FULLNAME##", objVacancyOffer.CandidateName, RegexOptions.IgnoreCase);
        //    strEmailMsg = Regex.Replace(strEmailMsg, "##VACANCY.JOBTITLE##", objVacancyOffer.JobTitle, RegexOptions.IgnoreCase);
        //    strEmailMsg = Regex.Replace(strEmailMsg, "##Award”.VACANCYNAME##", objVacancyOffer.JobTitle, RegexOptions.IgnoreCase);
        //    if (objVacancyOffer.StartDate > DateTime.MinValue)
        //        strEmailMsg = Regex.Replace(strEmailMsg, "##VACANCY.STARTDATE##", objVacancyOffer.StartDateText, RegexOptions.IgnoreCase);
        //    else
        //        strEmailMsg = Regex.Replace(strEmailMsg, "##VACANCY.STARTDATE##", "NA", RegexOptions.IgnoreCase);
        //    strEmailMsg = Regex.Replace(strEmailMsg, "##Award.OFFERSTATUS##", objVacancyOffer.OfferStatusText, RegexOptions.IgnoreCase);
        //    strEmailMsg = Regex.Replace(strEmailMsg, "##Award.CREATEDBY##", objVacancyOffer.CreatedByName, RegexOptions.IgnoreCase);
        //    strEmailMsg = Regex.Replace(strEmailMsg, "##Award.CREATEDON##", objVacancyOffer.CreatedOn.ToString("MM/dd/yyyy hh:ss tt"), RegexOptions.IgnoreCase);
        //    if (objVacancyOffer.UpdatedOn > DateTime.MinValue)
        //    {
        //        strEmailMsg = Regex.Replace(strEmailMsg, "##Award.UPDATEDBY##", objVacancyOffer.UpdatedByName, RegexOptions.IgnoreCase);
        //        strEmailMsg = Regex.Replace(strEmailMsg, "##Award.UPDATEDON##", objVacancyOffer.UpdatedOn.ToString("MM/dd/yyyy hh:ss tt"), RegexOptions.IgnoreCase);
        //        strEmailMsg = Regex.Replace(strEmailMsg, "##Award.MODIFIEDBY##", objVacancyOffer.UpdatedByName, RegexOptions.IgnoreCase);
        //        strEmailMsg = Regex.Replace(strEmailMsg, "##Award.MODIFIEDON##", objVacancyOffer.UpdatedOn.ToString("MM/dd/yyyy hh:ss tt"), RegexOptions.IgnoreCase);
        //    }
        //    else
        //    {
        //        strEmailMsg = Regex.Replace(strEmailMsg, "##Award.UPDATEDBY##", "", RegexOptions.IgnoreCase);
        //        strEmailMsg = Regex.Replace(strEmailMsg, "##Award.UPDATEDON##", "NA", RegexOptions.IgnoreCase);
        //        strEmailMsg = Regex.Replace(strEmailMsg, "##Award.MODIFIEDBY##", "", RegexOptions.IgnoreCase);
        //        strEmailMsg = Regex.Replace(strEmailMsg, "##Award.MODIFIEDON##", "NA", RegexOptions.IgnoreCase);
        //    }
        //    strEmailMsg = Regex.Replace(strEmailMsg, "##Award.PLACEMENTTYPE##", objVacancyOffer.PlacementTypeText, RegexOptions.IgnoreCase);
        //    strEmailMsg = Regex.Replace(strEmailMsg, "##Award.JOBTYPE##", objVacancyOffer.JobTypeText, RegexOptions.IgnoreCase);
        //    strEmailMsg = Regex.Replace(strEmailMsg, "##VACANCY.JOBTYPE##", objVacancyOffer.JobTypeText, RegexOptions.IgnoreCase);
        //    if (objVacancyOffer.StartDate > DateTime.MinValue)
        //    {
        //        strEmailMsg = Regex.Replace(strEmailMsg, "##Award.STARTDATE##", objVacancyOffer.StartDate.ToString("MM/dd/yyyy"), RegexOptions.IgnoreCase);
        //    }
        //    else
        //    {
        //        strEmailMsg = Regex.Replace(strEmailMsg, "##Award.STARTDATE##", "NA", RegexOptions.IgnoreCase);
        //    }

        //    if (objVacancyOffer.PlacementType != Convert.ToInt32(BEClient.PlaceMentType.Permanent).ToString() && objVacancyOffer.EndDate > DateTime.MinValue)
        //    {
        //        strEmailMsg = Regex.Replace(strEmailMsg, "##Award.ENDDATE##", objVacancyOffer.EndDateText, RegexOptions.IgnoreCase);
        //        strEmailMsg = Regex.Replace(strEmailMsg, "##VACANCY.ENDDATE##", objVacancyOffer.EndDateText, RegexOptions.IgnoreCase);
        //    }
        //    else
        //    {
        //        strEmailMsg = Regex.Replace(strEmailMsg, "##Award.ENDDATE##", "NA", RegexOptions.IgnoreCase);
        //        strEmailMsg = Regex.Replace(strEmailMsg, "##VACANCY.ENDDATE##", "NA", RegexOptions.IgnoreCase);
        //    }

        //    strEmailMsg = Regex.Replace(strEmailMsg, "##Award.PLACEMENTLOCATION##", objVacancyOffer.LocationText, RegexOptions.IgnoreCase);
        //    strEmailMsg = Regex.Replace(strEmailMsg, "##Award.SALARYTYPE##", objVacancyOffer.SalaryTypeText, RegexOptions.IgnoreCase);

        //    string _salaryOffered = "";
        //    if (objVacancyOffer.PlacementType == Convert.ToInt32(BEClient.PlaceMentType.Permanent).ToString())
        //    {
        //        if (objVacancyOffer.SalaryType == Convert.ToInt32(BEClient.SalaryType.Salary))
        //        {
        //            _salaryOffered = "$" + objVacancyOffer.SalaryOffered.ToString("##,##,###.##");
        //        }
        //        else if (objVacancyOffer.SalaryType == Convert.ToInt32(BEClient.SalaryType.Hourly))
        //        {
        //            _salaryOffered = "Hours Per Week: " + objVacancyOffer.HoursPerWeek.ToString();
        //            _salaryOffered = "  Hourly Wage: $" + objVacancyOffer.HourlyWage.ToString();
        //        }
        //        else if (objVacancyOffer.SalaryType == Convert.ToInt32(BEClient.SalaryType.PieceType))
        //        {
        //            _salaryOffered = "Rate $" + objVacancyOffer.Rate.ToString("##,##,###.##");
        //            _salaryOffered = " Per " + objVacancyOffer.Per;
        //        }
        //    }
        //    else
        //    {
        //        _salaryOffered = "Pay Rate: $" + objVacancyOffer.PayRate.ToString("##,##,###.##");
        //        _salaryOffered = "  Rate Period: " + objVacancyOffer.RatePeriod.ToString();
        //    }
        //    strEmailMsg = Regex.Replace(strEmailMsg, "##Award.SALARYOFFERED##", _salaryOffered, RegexOptions.IgnoreCase);

        //    strEmailMsg = Regex.Replace(strEmailMsg, "##Award.BONUSDESCRIPTION##", objVacancyOffer.BonusDescription.ToString(), RegexOptions.IgnoreCase);
        //    strEmailMsg = Regex.Replace(strEmailMsg, "##Award.BONUSPOTENTIAL##", objVacancyOffer.AnnualBonusPotential.ToString("##,##,###.##"), RegexOptions.IgnoreCase);
        //    strEmailMsg = Regex.Replace(strEmailMsg, "##Award.BONUSCAP##", objVacancyOffer.BonusCap.ToString("##,##,###.##"), RegexOptions.IgnoreCase);
        //    strEmailMsg = Regex.Replace(strEmailMsg, "##Award.COMMISSIONDESCRIPTION##", objVacancyOffer.CommissionDescriprion, RegexOptions.IgnoreCase);
        //    strEmailMsg = Regex.Replace(strEmailMsg, "##Award.COMMISSIONPOTENTIAL##", objVacancyOffer.AnnualCommissionPotential.ToString("##,##,###.##"), RegexOptions.IgnoreCase);
        //    strEmailMsg = Regex.Replace(strEmailMsg, "##Award.COMMISSIONCAP##", objVacancyOffer.CommissionCap.ToString("##,##,###.##"), RegexOptions.IgnoreCase);
        //    strEmailMsg = Regex.Replace(strEmailMsg, "##Award.CANDIDATENOTICEPERIOD##", objVacancyOffer.CandidateNoticePeriodText, RegexOptions.IgnoreCase);
        //    strEmailMsg = Regex.Replace(strEmailMsg, "##Award.CANDIDATENOTICEPERIODIN##", objVacancyOffer.CandidateNoticePeriodTypeText, RegexOptions.IgnoreCase);
        //    strEmailMsg = Regex.Replace(strEmailMsg, "##Award.COMPANYNOTICEPERIOD##", objVacancyOffer.CompanyNoticePeriodText, RegexOptions.IgnoreCase);
        //    strEmailMsg = Regex.Replace(strEmailMsg, "##Award.COMPANYNOTICEPERIODIN##", objVacancyOffer.CompanyNoticePeriodTypeText, RegexOptions.IgnoreCase);
        //    strEmailMsg = Regex.Replace(strEmailMsg, "##Award.COMPANYNAME##", ATS.WebUi.Common.CurrentSession.Instance.VerifiedClient.Clientname, RegexOptions.IgnoreCase);
        //    strEmailMsg = Regex.Replace(strEmailMsg, "##Award.MANAGERNAME##", objVacancyOffer.ManagerName, RegexOptions.IgnoreCase);
        //    return strEmailMsg;
        //}
        public ActionResult GetConfirmation(String model)
        {
            try
            {
                JavaScriptSerializer j = new JavaScriptSerializer();
                object a = j.Deserialize(model, typeof(object));
                BEClient.VacancyOffer obj = (BEClient.VacancyOffer)a;
                return View("VacancyOffer/_ConfirmOffer");
            }
            catch
            {
                throw;
            }
        }

        public ActionResult ConfirmOfferPDF(string ClientName, Guid VacancyOfferId, Guid AppId, Guid ETemplate)
        {
            BEMaster.Client objClient = BusinessLogic.Common.LoginOperation.CreateClientSession(ClientName);
            if (objClient == null)
            {
                return null;
            }
            else
            {
                new ATSCommon.CurrentSession(objClient);
                _CurrentClientId = objClient.ClientId;
                _CurrentLanguageId = objClient.CurrentLanguageId;
                base.CurrentClient = objClient;
            }
            try
            {
                Guid _VacOfferId = Guid.Empty;
                Guid.TryParse(VacancyOfferId.ToString(), out _VacOfferId);
                FillDropdown(AppId);
                BEClient.VacancyOffer objvacancyOffer = new BEClient.VacancyOffer();
                BLClient.VacancyOfferAction VacOfferAction = null;
                if (_VacOfferId != Guid.Empty)
                {
                    VacOfferAction = new BLClient.VacancyOfferAction(_CurrentClientId);
                    objvacancyOffer = VacOfferAction.GetVacancyOfferByIdForPDF(_VacOfferId, ETemplate, _CurrentLanguageId);
                    return View("Offer/_ConfirmOfferPDF", objvacancyOffer);
                }
                return View();
            }
            catch
            {
                throw;
            }
        }

        public ActionResult GenerateOfferLetterPDF(string ClientName, Guid VacancyOfferId)
        {
            BEMaster.Client objClient = BusinessLogic.Common.LoginOperation.CreateClientSession(ClientName);
            if (objClient == null)
            {
                return null;
            }
            else
            {
                new ATSCommon.CurrentSession(objClient);
                _CurrentClientId = objClient.ClientId;
                _CurrentLanguageId = objClient.CurrentLanguageId;
                base.CurrentClient = objClient;
            }
            try
            {
                Guid _VacOfferId = Guid.Empty;
                Guid.TryParse(VacancyOfferId.ToString(), out _VacOfferId);
                BEClient.VacancyOffer objvacancyOffer = new BEClient.VacancyOffer();
                BLClient.VacancyOfferAction VacOfferAction = null;
                if (_VacOfferId != Guid.Empty)
                {
                    VacOfferAction = new BLClient.VacancyOfferAction(_CurrentClientId);
                    objvacancyOffer = VacOfferAction.GenerateOfferLetterContentForPDF(_VacOfferId, _CurrentLanguageId);
                    return View("Offer/_OfferLetterPDF", objvacancyOffer);
                }
                return View();
            }
            catch
            {
                throw;
            }
        }

        public static void CopyPropertyValues(object source, object destination)
        {
            var destProperties = destination.GetType().GetProperties();

            foreach (var sourceProperty in source.GetType().GetProperties())
            {
                foreach (var destProperty in destProperties)
                {
                    if (destProperty.Name == sourceProperty.Name &&
                destProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType))
                    {
                        destProperty.SetValue(destination, sourceProperty.GetValue(
                            source, new object[] { }), new object[] { });
                        break;
                    }
                }
            }
        }

        public Task SendOfferMail(String CandidateEmailId, String EmailSubject, String EmailDescription, string path)
        {
            return Task.Factory.StartNew(() =>
            {
                Mailers.EmailTemplate mvcMailer = new Mailers.EmailTemplate();
                mvcMailer.SendMessage(CandidateEmailId, EmailSubject, EmailDescription, true, path, true);
            });
        }

        public Task SendOfferMail(String CandidateEmailId, String EmailSubject, String EmailDescription)
        {
            return Task.Factory.StartNew(() =>
            {
                Mailers.EmailTemplate mvcMailer = new Mailers.EmailTemplate();
                mvcMailer.SendMessage(CandidateEmailId, EmailSubject, EmailDescription);
            });
        }


        public ActionResult GetAssignedCandidatesByUserIdAndVacRndId(Guid UserId, Guid VacRndId)
        {
            try
            {
                BLClient.ApplicationAction objApplicationAction = new BLClient.ApplicationAction(_CurrentClientId);
                List<BEClient.Application> objApplicationList = objApplicationAction.GetAssignedCandidateByVacRoundId(VacRndId, UserId);
                return View("AssignCandidate/_ViewAssignedCandidates", objApplicationList);
            }
            catch
            {
                throw;
            }
        }

        public ActionResult AssignCandidate()
        {
            try
            {
                return View("AssignCandidate/_AssignCandidate", null);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public JsonResult CreateCsv(Guid AppId)
        {
            string Message = string.Empty;
            bool IsError = false;
            string Data = string.Empty;
            try
            {
                BLClient.HireCandidateAction objHireCandidateAction = new BLClient.HireCandidateAction(_CurrentClientId);
                BEClient.HireCandidates objHirecandidates = new BEClient.HireCandidates();
                objHirecandidates = objHireCandidateAction.GetHiredCandidate(AppId, _CurrentLanguageId);
                if (objHirecandidates != null)
                {
                    objHirecandidates.ApplicationId = AppId;
                    Data = Common.CommonFunctions.CreateCSVFileForPOB(objHirecandidates);
                    Message = "Applicant Hired SuccessFully";
                }
                else
                {
                    IsError = true;
                    Message = "Error Creating File";
                }
            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConductInterview(Guid ScheduleIntId, Guid VacRndId, string ReturnUrl)
        {
            try
            {
                bool InterViewComp = false;
                BEClient.BeginInterView objBeginInt = new BEClient.BeginInterView();
                BEClient.InterViewProcess ObjIterViewProcess = new BEClient.InterViewProcess();
                BLClient.BeginInerviewAction _BeginInerviewAction = new BLClient.BeginInerviewAction(_CurrentClientId);
                objBeginInt = _BeginInerviewAction.IsBeginInterviewExists(VacRndId, ATSCommon.CurrentSession.Instance.VerifiedUser.UserId, ScheduleIntId);
                if (objBeginInt == null)
                {
                    objBeginInt = new BEClient.BeginInterView();
                    objBeginInt.ScheduleIntId = ScheduleIntId;
                    objBeginInt.VacRndId = VacRndId;
                    _BeginInerviewAction = new BLClient.BeginInerviewAction(_CurrentClientId);
                    objBeginInt.IntStart = DateTime.UtcNow;
                    objBeginInt.UserId = ATSCommon.CurrentSession.Instance.VerifiedUser.UserId;
                    Guid AppRndId = _BeginInerviewAction.AddBeginInterview(objBeginInt);
                }
                else
                {
                    if (objBeginInt.IsComplete)
                    {
                        _BeginInerviewAction = new BLClient.BeginInerviewAction(_CurrentClientId);
                        InterViewComp = _BeginInerviewAction.UpdateInterviewStatus(VacRndId, ATSCommon.CurrentSession.Instance.VerifiedUser.UserId, false, ScheduleIntId, Convert.ToInt32(BEClient.InterviewStatus.InterviewReopened));
                    }
                }
                BLClient.VacancyRoundAction _ObjVacRndAction = new BLClient.VacancyRoundAction(_CurrentClientId);
                BLClient.ConductInterviewDetailsAction _ObjConductInterviewDetailsAction = new BLClient.ConductInterviewDetailsAction(_CurrentClientId);
                BEClient.VacancyRound ObjVacRnd = new BEClient.VacancyRound();
                int Row = 1;
                BLClient.InterViewProcessAction ObjInterViewProAction = new BLClient.InterViewProcessAction(_CurrentClientId);
                ObjIterViewProcess = ObjInterViewProAction.GetQueCatDetailsBtyOrder(VacRndId, Row, ScheduleIntId);
                if (ObjIterViewProcess == null)
                {
                    ObjIterViewProcess = new BEClient.InterViewProcess();
                }
                ObjIterViewProcess.ScheduleIntId = ScheduleIntId;
                ObjIterViewProcess.VacRndId = VacRndId;
                ObjIterViewProcess.objConductInterviewDetails = _ObjConductInterviewDetailsAction.GetConductInterviewDetails(ScheduleIntId);
                ObjIterViewProcess.objConductInterviewDetails.ReturnUrl = ReturnUrl;
                return View("ConductInterview/Index", ObjIterViewProcess);
            }
            catch
            {
                throw;
            }
        }
        [HttpPost]
        public PartialViewResult GetConductInterviewQueCat(Guid VacRndId, Guid QueCatId, Guid ScheduleIntId, int Row)
        {
            try
            {
                InterviewProcessQueModel _ObjInterViewProcessQue = new InterviewProcessQueModel();
                BLClient.InterviewProcessQueAction ObjInterViewProQueAction = new BLClient.InterviewProcessQueAction(_CurrentClientId);
                BLClient.ReviewersAction ObjReviewersAction = new BLClient.ReviewersAction(_CurrentClientId);
                Guid VacRMId = ObjReviewersAction.GetVacReviewerMemberByVacRndAndUser(VacRndId, ATSCommon.CurrentSession.Instance.VerifiedUser.UserId);
                _ObjInterViewProcessQue.ObjInterviewProcessQue = ObjInterViewProQueAction.GetQueDetailsBtyOrder(VacRndId, QueCatId, Row, _CurrentLanguageId);
                _ObjInterViewProcessQue.Questions = new List<ATS.WebUi.Models.Question.ATSQuestionCommon>();
                CreateQuestionWithAns(ref _ObjInterViewProcessQue, VacRMId, ScheduleIntId);
                return PartialView("InterViewQuestions/_InterViewQuestions", _ObjInterViewProcessQue);
            }
            catch
            {
                throw;

            }
        }

        [HttpGet]
        public JsonResult GetExpandedViewQueAns(Guid VacRndId, Guid ScheduleIntId)
        {
            string Message = string.Empty;
            bool IsError = false;
            string Data = string.Empty;
            try
            {
                BLClient.ReviewersAction ObjReviewersAction = new BLClient.ReviewersAction(_CurrentClientId);
                Guid VacRMId = ObjReviewersAction.GetVacReviewerMemberByVacRndAndUser(VacRndId, ATSCommon.CurrentSession.Instance.VerifiedUser.UserId);
                BLClient.InterviewProcessQueAction ObjInterViewProQueAction = new BLClient.InterviewProcessQueAction(_CurrentClientId);
                List<InterviewProcessQueModel> _ObjInterViewProcessQueModelList = new List<InterviewProcessQueModel>();
                List<BEClient.InterviewProcessQue> objInterviewProcessQueList = new List<BEClient.InterviewProcessQue>();
                objInterviewProcessQueList = ObjInterViewProQueAction.GetAllQueDetailsByOrder(VacRndId, _CurrentLanguageId);
                foreach (BEClient.InterviewProcessQue CurrentQue in objInterviewProcessQueList)
                {
                    InterviewProcessQueModel _ObjInterViewProcessQueModel = new InterviewProcessQueModel();
                    _ObjInterViewProcessQueModel.ObjInterviewProcessQue = CurrentQue;
                    _ObjInterViewProcessQueModel.Questions = new List<ATS.WebUi.Models.Question.ATSQuestionCommon>();
                    CreateQuestionWithAns(ref _ObjInterViewProcessQueModel, VacRMId, ScheduleIntId);
                    _ObjInterViewProcessQueModelList.Add(_ObjInterViewProcessQueModel);
                }
                Data = base.RenderRazorViewToString("ConductInterview/_ExpandedViewQueList", _ObjInterViewProcessQueModelList);
            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;

            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateConductScheduleInt(BEClient.ScheduleInt ScheduleInt)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = "";
            try
            {
                if (!string.IsNullOrEmpty(ScheduleInt.ScheduleDateStr))
                    ScheduleInt.ScheduleDate = Convert.ToDateTime(ScheduleInt.ScheduleDateStr);
                BLClient.ScheduleIntAction objScheduleIntAction = new BLClient.ScheduleIntAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                Guid ScheduleIntId = ScheduleInt.ScheduleIntId;
                BEClient.InterViewProcess ObjIterViewProcess = new BEClient.InterViewProcess();
                Boolean Result = objScheduleIntAction.UpdateScheduleInt(ScheduleInt);
                if (Result)
                {

                    BLClient.ConductInterviewDetailsAction _ObjConductInterviewDetailsAction = new BLClient.ConductInterviewDetailsAction(_CurrentClientId);
                    ObjIterViewProcess.objConductInterviewDetails = _ObjConductInterviewDetailsAction.GetConductInterviewDetails(ScheduleIntId);
                    ObjIterViewProcess.ScheduleIntId = ScheduleIntId;
                    ObjIterViewProcess.objConductInterviewDetails.ApplicationId = ScheduleInt.ApplicationId;
                }
                Data = base.RenderRazorViewToString("ConductInterview/_ScheduleIntDetails", ObjIterViewProcess);
            }
            catch (Exception Ex)
            {
                Message = Ex.Message;
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
        }

        public ActionResult TerminateConductInterview(Guid VacRndId, Guid ScheduleIntId, Guid ApplicationId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = string.Empty;
            try
            {
                BEClient.InterViewProcess objInterViewProcess = new BEClient.InterViewProcess();
                List<BEClient.ApplicationBasedStatus> objApplicationBasedStatus = new List<BEClient.ApplicationBasedStatus>();
                BLClient.ApplicationBasedStatusAction _objAppBasedStatusAction = new BLClient.ApplicationBasedStatusAction(_CurrentClientId);
                objApplicationBasedStatus = _objAppBasedStatusAction.GetAllAppBasedStatus(_CurrentLanguageId);
                ViewBag.AppStatList = new SelectList(objApplicationBasedStatus, "ApplicationBasedStatusId", "LocalName");
                objInterViewProcess.ScheduleIntId = ScheduleIntId;
                objInterViewProcess.VacRndId = VacRndId;
                objInterViewProcess.ApplicationId = ApplicationId;
                return View("ConductInterview/_TerminateInterview", objInterViewProcess);
            }
            catch
            {
                throw;
            }
        }

        public JsonResult UpdateStatusOfInterview(Guid ScheduleIntId, Guid VacRndId, Int32 InterviewStatus, Guid? ApplicationId, Guid? ApplicationBasedStatusId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = string.Empty;
            try
            {
                bool IsComplete = false;
                bool result = false;
                BLClient.BeginInerviewAction objBeginInterviewAction = new BLClient.BeginInerviewAction(_CurrentClientId);
                if (ApplicationId != null && ApplicationId != Guid.Empty)
                {
                    if (ApplicationBasedStatusId != Guid.Empty)
                    {
                        BLClient.ApplicationAction _objApplicationAction = new BLClient.ApplicationAction(_CurrentClientId);
                        bool statusUpdated = _objApplicationAction.UpdateApplicationBasedStatus((Guid)ApplicationId, (Guid)ApplicationBasedStatusId);
                        if (statusUpdated)
                        {
                            SendDeclinedMail((Guid)ApplicationId, (Guid)ApplicationBasedStatusId);
                            Message = "Applicant has been Rejected";
                            IsError = false;
                        }
                        else
                        {
                            Message = "Applicant has not been Rejected!!";
                            IsError = true;
                        }
                    }
                }
                else
                {
                    if (InterviewStatus == Convert.ToInt32(BEClient.InterviewStatus.InterViewComplete))
                    {
                        IsComplete = true;
                    }
                }
                result = objBeginInterviewAction.UpdateStatusOfInterview(VacRndId, Common.CurrentSession.Instance.VerifiedUser.UserId, IsComplete, ScheduleIntId, InterviewStatus);
            }
            catch
            {
                throw;
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
        [HttpGet]
        public JsonResult GetRequiredDocumentsByApplicationId(Guid ApplicationId, Guid UserId)
        {
            //Used for Json return
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            //End Json return property
            try
            {
                BEClient.CandidateProfile objCandidateProfile = new BEClient.CandidateProfile();
                BLClient.ApplicationDocumentsAction objAppDocAction = new BLClient.ApplicationDocumentsAction(_CurrentClientId);
                List<BEClient.ApplicationDocuments> objAppDocList = objAppDocAction.GetRequiredDocumentsByApplicationId(ApplicationId);
                if (objAppDocList != null && objAppDocList.Count > 0)
                {
                    objAppDocList = objAppDocList.Where(x => x.IsPending == false).ToList();
                }
                objCandidateProfile.ObjApplicationDocuments = objAppDocList;
                objCandidateProfile.objUserDetails = new BEClient.UserDetails();
                objCandidateProfile.objUserDetails.UserId = UserId;
                Data = base.RenderRazorViewToString("_SettingOption", objCandidateProfile);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
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
                ViewBag.IsEmployee = true;
                Data = base.RenderRazorViewToString("ApplicationDocuments/_ApplicationDocumentList", objApplicationDocuments);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
        }

        #region GetApplication Questions

        [HttpGet]
        public JsonResult GetAllAppQuestions(Guid ApplicationId)
        {
            String Message = String.Empty;
            bool IsError = false;
            String Data = String.Empty;
            try
            {
                JobApplication<RootModels.BreadScrumbModel<BEClient.CandidateApplications>> _jobAppWithQue = new JobApplication<RootModels.BreadScrumbModel<BEClient.CandidateApplications>>();
                BLClient.AppQueAnsAction _AppQueAnsAction = new BLClient.AppQueAnsAction(_CurrentClientId);
                string LstRndTypeId = GetRndTypeIdByCandidateSelfEvalution();
                List<BEClient.AppQueAns> _objAppQueAns = _AppQueAnsAction.GetAppQueByApplicationId(ApplicationId, LstRndTypeId);
                if (_objAppQueAns.Count == 0)
                {
                    ViewBag.ScreenQue = 0;
                    Data = base.RenderRazorViewToString("_ApplicationMessage", null);
                }
                else
                {
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
                    ViewBag.AllCategoryNameList = _objAppQueAns.Select(x => x.Question.QueCatLocalName).Distinct().ToList();
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                IsError = true;
            }
            return base.GetJson(base.GetJsonContent(IsError, null, Message, Data), JsonRequestBehavior.AllowGet);
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
                        _DrpATSPickList.QueCatId = _current.Question.QueCatId;
                        _DrpATSPickList.Answer = objAnsOpt[0].LocalName;
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
                    ATS.WebUi.Models.Question.ATSTextArea _DrpATSPickList = new WebUi.Models.Question.ATSTextArea();
                    _DrpATSPickList.QuestionText = _current.Question.LocalName;
                    _DrpATSPickList.QueCatId = _current.Question.QueCatId;
                    _DrpATSPickList.Answer = _current.AppAnswer.Answer;
                    _ListATSTextArea.Add(_DrpATSPickList);
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
                    _DrpATSTextBox.Answer = _current.AppAnswer.Answer;
                    _DrpATSTextBox.QueCatId = _current.Question.QueCatId;
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
        #endregion
    }
}