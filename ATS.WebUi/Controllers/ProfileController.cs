using ATS.WebUi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEClient = ATS.BusinessEntity;
using BLClient = ATS.BusinessLogic;
using BLCommon = ATS.BusinessLogic.Common;
using ATSCommon = ATS.WebUi.Common;
using log4net;

namespace ATS.WebUi.Controllers
{
    public partial class ProfileController : ATS.WebUi.Controllers.AreaBaseController
    {

        private BLClient.ProfileAction _objProfileAction;
        private Guid _CurrentClientId;
        private Guid _CurrentLanguageId;
        private Guid _UserId;

        private static readonly log4net.ILog log = LogManager.GetLogger(
         System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region OnAuthorization Method call on every request
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (base.CurrentClient != null && Common.CurrentSession.Instance.VerifiedUser != null)
            {
                _UserId = ATSCommon.CurrentSession.Instance.UserId;
                _CurrentClientId = base.CurrentClient.ClientId;
                _CurrentLanguageId = base.CurrentClient.CurrentLanguageId;
            }
        }
        #endregion

        //CR-2

        [HttpPost]
        public JsonResult PersonalInfo(BEClient.UserDetails objUserDetails, string OrgName, string OrgID)
        {
            String Message = String.Empty;
            bool IsError = true;
            String Data = "";

           // Message = ServerValidation();
            try
            {
                if (!String.IsNullOrEmpty(Message))
                {
                    Exception ex = new Exception(Message);
                    throw ex;
                }
                BLClient.UserDetailsAction ObjUserDetailsAction = new BLClient.UserDetailsAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                bool IsUserDetailsUpdated = ObjUserDetailsAction.UpdateUserDetails(objUserDetails);
                //CR-2
                BEClient.Organization objorg=new BEClient.Organization();
                if (OrgID != "")
                {
                    objorg.OrganizationID = int.Parse(OrgID);
                    objorg.OrganizationName = OrgName;
                    BLClient.OrgAction ObjorglAction = new BLClient.OrgAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                    bool IsOrgUpdated = ObjorglAction.UpdateUserorgDetails(objorg);
                }
                else
                {
                    BLClient.Common.LoginOperation.CandidateOrganization(OrgName, objUserDetails.UserId.Value, ATSCommon.CurrentSession.Instance.VerifiedClient.ClientId);
                   // Message = "Updated Successfully";
                }
                if (IsUserDetailsUpdated)
                {
                    IsError = false;
                    Message = "Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            try
            {
                ATSCommon.CommonFunctions.SolrEmployeeDeltaImport();
            }
            catch (Exception ex)
            {
                log.Error("SOLR PROFILE  DELTA IMPORT (AddUpdateReferences)" + ex.Message);
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message));

        }


        //[HttpPost]
        //public JsonResult PersonalInfo(BEClient.UserDetails objUserDetails)
        //{
        //    String Message = String.Empty;
        //    bool IsError = true;
        //    String Data = "";

        //    Message = ServerValidation();
        //    try
        //    {
        //        if (!String.IsNullOrEmpty(Message))
        //        {
        //            Exception ex = new Exception(Message);
        //            throw ex;
        //        }
        //        BLClient.UserDetailsAction ObjUserDetailsAction = new BLClient.UserDetailsAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
        //        bool IsUserDetailsUpdated = ObjUserDetailsAction.UpdateUserDetails(objUserDetails);
        //        if (IsUserDetailsUpdated)
        //        {
        //            IsError = false;
        //            Message = "Updated Successfully";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Message = ex.Message;
        //    }
        //    try
        //    {
        //        ATSCommon.CommonFunctions.SolrEmployeeDeltaImport();
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error("SOLR PROFILE  DELTA IMPORT (AddUpdateReferences)" + ex.Message);
        //    }
        //    return GetJson(base.GetJsonContent(IsError, string.Empty, Message));

        //}

        [HttpPost]
        public JsonResult Availability(BEClient.Availability objAvailability)
        {
            String Message = String.Empty;
            bool IsError = true;
            String Data = "";

            Message = ServerValidation();
            try
            {
                if (!String.IsNullOrEmpty(Message))
                {
                    Exception ex = new Exception(Message);
                    throw ex;
                }
                BLClient.AvailabilityAction ObjAvailabilityAction = new BLClient.AvailabilityAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                bool IsUserDetailsUpdated = ObjAvailabilityAction.UpdateAvailability(objAvailability);
                if (IsUserDetailsUpdated)
                {

                    IsError = false;
                    Message = "Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            try
            {
                ATSCommon.CommonFunctions.SolrEmployeeDeltaImport();
            }
            catch (Exception ex)
            {
                log.Error("SOLR PROFILE  DELTA IMPORT (Availability)" + ex.Message);
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message));


        }


        [HttpPost]
        public JsonResult ExecutiveSummary(BEClient.ExecutiveSummary objExecutiveSummary)
        {
            String Message = String.Empty;
            bool IsError = true;
            String Data = "";
            Message = ServerValidation();
            try
            {
                if (!String.IsNullOrEmpty(Message))
                {
                    Exception ex = new Exception(Message);
                    throw ex;
                }
                BLClient.ExecutiveSummaryAction objExecutiveSummaryAction = new BLClient.ExecutiveSummaryAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                bool IsExecutiveSummaaryUpdated = objExecutiveSummaryAction.UpdateExecutiveSummary(objExecutiveSummary);
                if (IsExecutiveSummaaryUpdated)
                {

                    IsError = false;
                    Message = "Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            try
            {
                ATSCommon.CommonFunctions.SolrEmployeeDeltaImport();
            }
            catch (Exception ex)
            {
                log.Error("SOLR PROFILE  DELTA IMPORT (ExecutiveSummary)" + ex.Message);
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message));
        }
        [HttpPost]
        public JsonResult Objective(BEClient.Objective objobjective)
        {
            String Message = String.Empty;
            bool IsError = true;
            String Data = "";

            Message = ServerValidation();
            try
            {
                if (!String.IsNullOrEmpty(Message))
                {
                    Exception ex = new Exception(Message);
                    throw ex;
                }
                BLClient.ObjectiveAction objObjectiveAction = new BLClient.ObjectiveAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                bool IsObjectiveUpdated = objObjectiveAction.UpdateObjective(objobjective);
                if (IsObjectiveUpdated)
                {

                    IsError = false;
                    Message = "Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            try
            {
                ATSCommon.CommonFunctions.SolrEmployeeDeltaImport();
            }
            catch (Exception ex)
            {
                log.Error("SOLR PROFILE  DELTA IMPORT (Objective)" + ex.Message);
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message));
        }



        #region Achievement

        public PartialViewResult AddAchievementModel(Guid currentProfileId, Guid? userId)
        {

            BEClient.Achievement _objAchievement = new BEClient.Achievement();
            _objAchievement.AchievementId = Guid.NewGuid();
            _objAchievement.ProfileId = currentProfileId;
            _objAchievement.UserId = (Guid)userId;
            return PartialView("Profile/_Achievement", _objAchievement);
        }






        [HttpPost]
        public JsonResult AddUpdateAchievements(BEClient.Achievement objAchievements)
        {
            String Message = String.Empty;
            bool IsError = true;
            String Data = "";

            Message = ServerValidation();
            try
            {
                if (!String.IsNullOrEmpty(Message))
                {
                    Exception ex = new Exception(Message);
                    throw ex;
                }
                BLClient.AchievementAction objAchievementAction = new BLClient.AchievementAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                bool _isRecordExists = objAchievementAction.IsRecordExists(objAchievements.AchievementId);
                if (_isRecordExists)
                {
                    bool IsrecordUpdated = objAchievementAction.UpdateAchievement(objAchievements);
                    if (IsrecordUpdated)
                    {
                        IsError = false;
                        Message = "Updated Successfully";
                        //data will be pass as empty to identify is update call or inser call
                    }
                }
                else
                {
                    Guid NewrecordAdded = objAchievementAction.AddAchievement(objAchievements);
                    if (!NewrecordAdded.Equals(Guid.Empty))
                    {
                        IsError = false;
                        Message = "Record Inserted Successfully";
                        Data = NewrecordAdded.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            try
            {
                ATSCommon.CommonFunctions.SolrEmployeeDeltaImport();
            }
            catch (Exception ex)
            {
                log.Error("SOLR PROFILE  DELTA IMPORT (Achievements)" + ex.Message);
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
        }

        public JsonResult RemoveAchievement(Guid precordId)
        {
            try
            {

                BLClient.AchievementAction objAchievementAction = new BLClient.AchievementAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                bool _isRecordExists = objAchievementAction.IsRecordExists(precordId);
                if (_isRecordExists)
                {
                    bool IsDeleted = objAchievementAction.DeleteAchievements(precordId);
                    if (IsDeleted)
                    {
                        try
                        {
                            ATSCommon.CommonFunctions.SolrEmployeeDeltaImport();
                        }
                        catch (Exception ex)
                        {
                            log.Error("SOLR PROFILE  DELTA IMPORT (RemoveAchievement)" + ex.Message);
                        }
                        return GetJson(base.GetJsonContent(false, string.Empty, " Achievements Deleted Successfully"), JsonRequestBehavior.AllowGet);
                    }
                    else
                    { return GetJson(base.GetJsonContent(true, string.Empty, " Achievements not  Deleted "), JsonRequestBehavior.AllowGet); }



                }
                else
                {
                    return GetJson(base.GetJsonContent(false, string.Empty, "Achievements Deleted Successfully"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return GetJson(base.GetJsonContent(true, string.Empty, ex.Message), JsonRequestBehavior.AllowGet);
            }

        }



        #endregion

        #region Skills And Qualifications


        public PartialViewResult AddSkillsModel(Guid currentProfileId, Guid? userId)
        {

            ViewBag.RedirectAction = "Create";
            FillSkillsAndQualificationDD();
            BEClient.Skills _objSkills = new BEClient.Skills();
            _objSkills.SkillsId = Guid.NewGuid();
            _objSkills.ProfileId = currentProfileId;
            _objSkills.UserId = (Guid)userId;
            return PartialView("Profile/_SkillAndQualification", _objSkills);
        }






        [HttpPost]
        public JsonResult AddUpdateSkills(BEClient.Skills objSkills)
        {
            String Message = String.Empty;
            bool IsError = true;
            String Data = "";
            Message = ServerValidation();
            try
            {
                if (!String.IsNullOrEmpty(Message))
                {
                    Exception ex = new Exception(Message);
                    throw ex;
                }
                BLClient.SkillsAction objSkillsAction = new BLClient.SkillsAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                bool _isRecordExists = objSkillsAction.IsRecordExists(objSkills.SkillsId);
                if (_isRecordExists)
                {
                    bool IsrecordUpdated = objSkillsAction.UpdateSkills(objSkills);
                    if (IsrecordUpdated)
                    {
                        IsError = false;
                        Message = "Updated Successfully";
                        //data will be pass as empty to identify is update call or inser call
                    }
                }
                else
                {
                    Guid NewrecordAdded = objSkillsAction.AddSkills(objSkills);
                    if (!NewrecordAdded.Equals(Guid.Empty))
                    {
                        IsError = false;
                        Message = "Record Inserted Successfully";
                        Data = NewrecordAdded.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            try
            {
                ATSCommon.CommonFunctions.SolrEmployeeDeltaImport();
            }
            catch (Exception ex)
            {
                log.Error("SOLR PROFILE  DELTA IMPORT (AddUpdateSkills)" + ex.Message);
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
        }


        private void FillSkillsAndQualificationDD()
        {
            try
            {

                //BLClient.SkillTypeAction objSkillTypeAction = new BLClient.SkillTypeAction(_CurrentClientId);

                List<BEClient.SkillType> lstSkillType = BLClient.Common.CacheHelper.GetSkillType(_CurrentClientId, _CurrentLanguageId, CurrentClient.Clientname);
                ViewBag.SkillTypeName = new SelectList(lstSkillType, "SkillTypeId", "LocalName");

            }
            catch
            {
                throw;
            }
        }


        public JsonResult RemoveSkills(Guid precordId)
        {
            try
            {

                BLClient.SkillsAction objSkillsAction = new BLClient.SkillsAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                bool _isRecordExists = objSkillsAction.IsRecordExists(precordId);
                if (_isRecordExists)
                {
                    bool IsDeleted = objSkillsAction.DeleteSkills(precordId);
                    if (IsDeleted)
                    {
                        try
                        {
                            ATSCommon.CommonFunctions.SolrEmployeeDeltaImport();
                        }
                        catch (Exception ex)
                        {
                            log.Error("SOLR PROFILE  DELTA IMPORT (RemoveSkills)" + ex.Message);
                        }
                        return GetJson(base.GetJsonContent(false, string.Empty, " Skills Deleted Successfully"), JsonRequestBehavior.AllowGet);
                    }
                    else
                    { return GetJson(base.GetJsonContent(true, string.Empty, " skills not  Deleted "), JsonRequestBehavior.AllowGet); }



                }
                else
                {
                    return GetJson(base.GetJsonContent(false, string.Empty, "skills Deleted Successfully"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return GetJson(base.GetJsonContent(true, string.Empty, ex.Message), JsonRequestBehavior.AllowGet);
            }

        }

        #endregion

        #region Education History

        public PartialViewResult AddEducationHistoryModel(Guid currentProfileId, Guid? userId)
        {
            FillEducationHistoryDD();
            BEClient.EducationHistory _objEducationHistory = new BEClient.EducationHistory();
            _objEducationHistory.EducationId = Guid.NewGuid();
            _objEducationHistory.ProfileId = currentProfileId;
            _objEducationHistory.UserId = (Guid)userId;
            return PartialView("Profile/_Education", _objEducationHistory);
        }

        [HttpPost]
        public JsonResult AddUpdateEducationHistory(BEClient.EducationHistory objEducationHistory)
        {
            String Message = String.Empty;
            bool IsError = true;
            String Data = "";
            Message = ServerValidation();
            if ((objEducationHistory.StartDate > objEducationHistory.EndDate) && String.IsNullOrEmpty(Message))
            {
                Message = String.Format("{0}", Resources.Resources.LanguageDisplay(BEClient.Common.CommonConstant.INVALID_DATE));
            }
            try
            {
                if (!String.IsNullOrEmpty(Message))
                {
                    Exception ex = new Exception(Message);
                    throw ex;
                }
                BLClient.EducationHistoryAction objEducationHistoryAction = new BLClient.EducationHistoryAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                bool _isRecordExists = objEducationHistoryAction.IsRecordExists(objEducationHistory.EducationId);
                if (_isRecordExists)
                {
                    bool IsrecordUpdated = objEducationHistoryAction.UpdateEducationHistory(objEducationHistory);
                    if (IsrecordUpdated)
                    {
                        IsError = false;
                        Message = "Updated Successfully";
                        //data will be pass as empty to identify is update call or inser call
                    }
                }
                else
                {
                    Guid NewrecordAdded = objEducationHistoryAction.AddEducationHistory(objEducationHistory);
                    if (!NewrecordAdded.Equals(Guid.Empty))
                    {
                        IsError = false;
                        Message = "Record Inserted Successfully";
                        Data = NewrecordAdded.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            try
            {
                ATSCommon.CommonFunctions.SolrEmployeeDeltaImport();
            }
            catch (Exception ex)
            {
                log.Error("SOLR PROFILE  DELTA IMPORT (EducationHistory)" + ex.Message);
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
        }

        private void FillEducationHistoryDD()
        {
            try
            {
                List<BEClient.DegreeType> lstDegreeType = BLClient.Common.CacheHelper.GetDegreeType(_CurrentClientId, _CurrentLanguageId, CurrentClient.Clientname);
                ViewBag.DegreeTypeName = new SelectList(lstDegreeType, "DegreeTypeId", "LocalName");
            }
            catch
            {
                throw;
            }
        }



        public JsonResult RemoveEducationHistory(Guid precordId)
        {
            try
            {

                BLClient.EducationHistoryAction objEducationHistoryAction = new BLClient.EducationHistoryAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                bool _isRecordExists = objEducationHistoryAction.IsRecordExists(precordId);
                if (_isRecordExists)
                {
                    bool IsDeleted = objEducationHistoryAction.DeleteEducationHistory(precordId);
                    if (IsDeleted)
                    {
                        try
                        {
                            ATSCommon.CommonFunctions.SolrEmployeeDeltaImport();
                        }
                        catch (Exception ex)
                        {
                            log.Error("SOLR PROFILE  DELTA IMPORT (EducationHistory)" + ex.Message);
                        }
                        return GetJson(base.GetJsonContent(false, string.Empty, " Education History Deleted Successfully"), JsonRequestBehavior.AllowGet);
                    }
                    else
                    { return GetJson(base.GetJsonContent(true, string.Empty, " Education History not  Deleted "), JsonRequestBehavior.AllowGet); }



                }
                else
                {
                    return GetJson(base.GetJsonContent(false, string.Empty, "Education History Deleted Successfully"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return GetJson(base.GetJsonContent(true, string.Empty, ex.Message), JsonRequestBehavior.AllowGet);
            }

        }

        #endregion


        #region Licence And Certifications

        public PartialViewResult AddLicenceAndcertificationModel(Guid currentProfileId, Guid? userId)
        {


            BEClient.LicenceAndCertifications _objLicenceAndCertifications = new BEClient.LicenceAndCertifications();
            _objLicenceAndCertifications.LicenceAndCertificationsId = Guid.NewGuid();
            _objLicenceAndCertifications.ProfileId = currentProfileId;
            _objLicenceAndCertifications.UserId = (Guid)userId;
            return PartialView("Profile/_LicenceAndCertifications", _objLicenceAndCertifications);
        }

        [HttpPost]
        public JsonResult AddUpdateLicenceAndCertifications(BEClient.LicenceAndCertifications objLicenceAndCertifications)
        {
            String Message = String.Empty;
            bool IsError = true;
            String Data = "";
            Message = ServerValidation();
            if ((objLicenceAndCertifications.ValidFrom > objLicenceAndCertifications.ValidTo) && String.IsNullOrEmpty(Message))
            {
                Message = String.Format("{0}", Resources.Resources.LanguageDisplay(BEClient.Common.CommonConstant.INVALID_DATE));
            }
            try
            {
                if (!String.IsNullOrEmpty(Message))
                {
                    Exception ex = new Exception(Message);
                    throw ex;
                }
                BLClient.LicenceAndCertificationsAction objLicenceAndCertificationsAction = new BLClient.LicenceAndCertificationsAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                bool _isRecordExists = objLicenceAndCertificationsAction.IsRecordExists(objLicenceAndCertifications.LicenceAndCertificationsId);
                if (_isRecordExists)
                {
                    bool IsrecordUpdated = objLicenceAndCertificationsAction.UpdateLicenceAndHistory(objLicenceAndCertifications);
                    if (IsrecordUpdated)
                    {
                        IsError = false;
                        Message = "Updated Successfully";
                        //data will be pass as empty to identify is update call or inser call
                    }
                }
                else
                {
                    Guid NewrecordAdded = objLicenceAndCertificationsAction.AddLicenceandHistory(objLicenceAndCertifications);
                    if (!NewrecordAdded.Equals(Guid.Empty))
                    {
                        IsError = false;
                        Message = "Record Inserted Successfully";
                        Data = NewrecordAdded.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            try
            {
                ATSCommon.CommonFunctions.SolrEmployeeDeltaImport();
            }
            catch (Exception ex)
            {
                log.Error("SOLR PROFILE  DELTA IMPORT (LicenceAndCertifications)" + ex.Message);
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
        }



        public JsonResult RemoveLicenceAndCertification(Guid precordId)
        {
            try
            {

                BLClient.LicenceAndCertificationsAction objLicenceAndCertificationsAction = new BLClient.LicenceAndCertificationsAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                bool _isRecordExists = objLicenceAndCertificationsAction.IsRecordExists(precordId);
                if (_isRecordExists)
                {
                    bool IsDeleted = objLicenceAndCertificationsAction.DeleteLicenceAndCertifications(precordId);
                    if (IsDeleted)
                    {
                        try
                        {
                            ATSCommon.CommonFunctions.SolrEmployeeDeltaImport();
                        }
                        catch (Exception ex)
                        {
                            log.Error("SOLR PROFILE  DELTA IMPORT (RemoveLicenceAndCertification)" + ex.Message);
                        }
                        return GetJson(base.GetJsonContent(false, string.Empty, " Education History Deleted Successfully"), JsonRequestBehavior.AllowGet);
                    }
                    else
                    { return GetJson(base.GetJsonContent(true, string.Empty, " Education History not  Deleted "), JsonRequestBehavior.AllowGet); }



                }
                else
                {
                    return GetJson(base.GetJsonContent(false, string.Empty, "Education History Deleted Successfully"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return GetJson(base.GetJsonContent(true, string.Empty, ex.Message), JsonRequestBehavior.AllowGet);
            }

        }


        #endregion

        #region Publication history
        public PartialViewResult AddPublicationHistoryModel(Guid currentProfileId, Guid? userId)
        {


            BEClient.PublicationHistory _objPublicationHistory = new BEClient.PublicationHistory();
            _objPublicationHistory.PublicationHistoryId = Guid.NewGuid();
            _objPublicationHistory.ProfileId = currentProfileId;
            _objPublicationHistory.UserId = (Guid)userId;
            return PartialView("Profile/_PublicationHistory", _objPublicationHistory);
        }



        [HttpPost]
        public JsonResult AddUpdatePublicationHistory(BEClient.PublicationHistory objPublicationHistory)
        {
            String Message = String.Empty;
            bool IsError = true;
            String Data = "";
            Message = ServerValidation();
            try
            {
                if (!String.IsNullOrEmpty(Message))
                {
                    Exception ex = new Exception(Message);
                    throw ex;
                }
                BLClient.PublicationHistoryAction objPublicationHistoryAction = new BLClient.PublicationHistoryAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                bool _isRecordExists = objPublicationHistoryAction.IsRecordExists(objPublicationHistory.PublicationHistoryId);
                if (_isRecordExists)
                {
                    bool IsrecordUpdated = objPublicationHistoryAction.UpdatePublicationHistory(objPublicationHistory);
                    if (IsrecordUpdated)
                    {
                        IsError = false;
                        Message = "Updated Successfully";
                        //data will be pass as empty to identify is update call or inser call
                    }
                }
                else
                {
                    Guid NewrecordAdded = objPublicationHistoryAction.AddPublicationhistory(objPublicationHistory);
                    if (!NewrecordAdded.Equals(Guid.Empty))
                    {
                        IsError = false;
                        Message = "Record Inserted Successfully";
                        Data = NewrecordAdded.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            try
            {
                ATSCommon.CommonFunctions.SolrEmployeeDeltaImport();
            }
            catch (Exception ex)
            {
                log.Error("SOLR PROFILE  DELTA IMPORT (Add Update Publication History)" + ex.Message);
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
        }


        public JsonResult RemovePublicationHistory(Guid precordId)
        {
            try
            {

                BLClient.PublicationHistoryAction objPublicationHistoryAction = new BLClient.PublicationHistoryAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                bool _isRecordExists = objPublicationHistoryAction.IsRecordExists(precordId);
                if (_isRecordExists)
                {
                    bool IsDeleted = objPublicationHistoryAction.DeletePublicationHistory(precordId);
                    if (IsDeleted)
                    {
                        try
                        {
                            ATSCommon.CommonFunctions.SolrEmployeeDeltaImport();
                        }
                        catch (Exception ex)
                        {
                            log.Error("SOLR PROFILE  DELTA IMPORT (RemovePublicationHistory)" + ex.Message);
                        }
                        return GetJson(base.GetJsonContent(false, string.Empty, " Publication History Deleted Successfully"), JsonRequestBehavior.AllowGet);
                    }
                    else
                    { return GetJson(base.GetJsonContent(true, string.Empty, " Publication History not  Deleted "), JsonRequestBehavior.AllowGet); }



                }
                else
                {
                    return GetJson(base.GetJsonContent(false, string.Empty, "Publication History Deleted Successfully"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return GetJson(base.GetJsonContent(true, string.Empty, ex.Message), JsonRequestBehavior.AllowGet);
            }

        }

        #endregion



        #region Speaking Event History


        public PartialViewResult AddSpeakingEventHistoryModel(Guid currentProfileId, Guid? userId)
        {


            BEClient.SpeakingEventHistory _objSpeakingEventHistory = new BEClient.SpeakingEventHistory();
            _objSpeakingEventHistory.SpeakingEventHistoryId = Guid.NewGuid();
            _objSpeakingEventHistory.ProfileId = currentProfileId;
            _objSpeakingEventHistory.UserId = (Guid)userId;
            return PartialView("Profile/_SpeakingEventHistory", _objSpeakingEventHistory);
        }

        [HttpPost]
        public JsonResult AddUpdatSpeakingEventHistory(BEClient.SpeakingEventHistory objSpeakingEventHistory)
        {
            String Message = String.Empty;
            bool IsError = true;
            String Data = "";
            Message = ServerValidation();
            try
            {

                if (!String.IsNullOrEmpty(Message))
                {
                    Exception ex = new Exception(Message);
                    throw ex;
                }
                BLClient.SpeakingEventHistoryAction objSpeakingEventHistoryAction = new BLClient.SpeakingEventHistoryAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                bool _isRecordExists = objSpeakingEventHistoryAction.IsRecordExists(objSpeakingEventHistory.SpeakingEventHistoryId);
                if (_isRecordExists)
                {
                    bool IsrecordUpdated = objSpeakingEventHistoryAction.UpdateSpeakingHistory(objSpeakingEventHistory);
                    if (IsrecordUpdated)
                    {
                        IsError = false;
                        Message = "Updated Successfully";
                        //data will be pass as empty to identify is update call or inser call
                    }
                }
                else
                {
                    Guid NewrecordAdded = objSpeakingEventHistoryAction.AddSpeakingEventHistory(objSpeakingEventHistory);
                    if (!NewrecordAdded.Equals(Guid.Empty))
                    {
                        IsError = false;
                        Message = "Record Inserted Successfully";
                        Data = NewrecordAdded.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            try
            {
                ATSCommon.CommonFunctions.SolrEmployeeDeltaImport();
            }
            catch (Exception ex)
            {
                log.Error("SOLR PROFILE  DELTA IMPORT (SpeakingEventHistory)" + ex.Message);
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
        }



        public JsonResult RemoveSpeakingEventHistory(Guid precordId)
        {
            try
            {

                BLClient.SpeakingEventHistoryAction objSpeakingEventHistoryAction = new BLClient.SpeakingEventHistoryAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                bool _isRecordExists = objSpeakingEventHistoryAction.IsRecordExists(precordId);
                if (_isRecordExists)
                {
                    bool IsDeleted = objSpeakingEventHistoryAction.DeleteSpeakingEventHistory(precordId);
                    if (IsDeleted)
                    {
                        try
                        {
                            ATSCommon.CommonFunctions.SolrEmployeeDeltaImport();
                        }
                        catch (Exception ex)
                        {
                            log.Error("SOLR PROFILE  DELTA IMPORT (SpeakingEventHistory)" + ex.Message);
                        }
                        return GetJson(base.GetJsonContent(false, string.Empty, " Speaking Event History Deleted Successfully"), JsonRequestBehavior.AllowGet);
                    }
                    else
                    { return GetJson(base.GetJsonContent(true, string.Empty, " Speaking Event  History not  Deleted "), JsonRequestBehavior.AllowGet); }



                }
                else
                {
                    return GetJson(base.GetJsonContent(false, string.Empty, "Speaking Event History Deleted Successfully"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return GetJson(base.GetJsonContent(true, string.Empty, ex.Message), JsonRequestBehavior.AllowGet);
            }

        }


        #endregion


        #region Associations
        public PartialViewResult AddAssociationsModel(Guid currentProfileId, Guid? userId)
        {


            BEClient.Associations _objAssociations = new BEClient.Associations();
            _objAssociations.AssociationsId = Guid.NewGuid();
            _objAssociations.ProfileId = currentProfileId;
            _objAssociations.UserId = (Guid)userId;
            return PartialView("Profile/_Associations", _objAssociations);
        }
        [HttpPost]
        public JsonResult AddUpdateAssociations(BEClient.Associations objAssociations)
        {
            String Message = String.Empty;
            bool IsError = true;
            String Data = "";
            Message = ServerValidation();
            if ((objAssociations.StartDate > objAssociations.EndDate) && String.IsNullOrEmpty(Message))
            {
                Message = String.Format("{0}", Resources.Resources.LanguageDisplay(BEClient.Common.CommonConstant.INVALID_DATE));
            }
            try
            {
                if (!String.IsNullOrEmpty(Message))
                {
                    Exception ex = new Exception(Message);
                    throw ex;
                }
                BLClient.AssociationsAction objAssociationsAction = new BLClient.AssociationsAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                bool _isRecordExists = objAssociationsAction.IsRecordExists(objAssociations.AssociationsId);
                if (_isRecordExists)
                {
                    bool IsrecordUpdated = objAssociationsAction.UpdateAssociations(objAssociations);
                    if (IsrecordUpdated)
                    {
                        IsError = false;
                        Message = "Updated Successfully";
                        //data will be pass as empty to identify is update call or inser call
                    }
                }
                else
                {
                    Guid NewrecordAdded = objAssociationsAction.AddAssociations(objAssociations);
                    if (!NewrecordAdded.Equals(Guid.Empty))
                    {
                        IsError = false;
                        Message = "Record Inserted Successfully";
                        Data = NewrecordAdded.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            try
            {
                ATSCommon.CommonFunctions.SolrEmployeeDeltaImport();
            }
            catch (Exception ex)
            {
                log.Error("SOLR PROFILE  DELTA IMPORT (Associations)" + ex.Message);
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
        }



        public JsonResult RemoveAssociations(Guid precordId)
        {
            try
            {

                BLClient.AssociationsAction objAssociationsAction = new BLClient.AssociationsAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                bool _isRecordExists = objAssociationsAction.IsRecordExists(precordId);
                if (_isRecordExists)
                {
                    bool IsDeleted = objAssociationsAction.DeleteAssociation(precordId);
                    if (IsDeleted)
                    {
                        try
                        {
                            ATSCommon.CommonFunctions.SolrEmployeeDeltaImport();
                        }
                        catch (Exception ex)
                        {
                            log.Error("SOLR PROFILE  DELTA IMPORT (RemoveAssociations)" + ex.Message);
                        }
                        return GetJson(base.GetJsonContent(false, string.Empty, " Speaking Event History Deleted Successfully"), JsonRequestBehavior.AllowGet);
                    }
                    else
                    { return GetJson(base.GetJsonContent(true, string.Empty, " Speaking Event  History not  Deleted "), JsonRequestBehavior.AllowGet); }



                }
                else
                {
                    return GetJson(base.GetJsonContent(false, string.Empty, "Speaking Event History Deleted Successfully"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return GetJson(base.GetJsonContent(true, string.Empty, ex.Message), JsonRequestBehavior.AllowGet);
            }

        }

        #endregion

        #region Languages
        public PartialViewResult AddlanguagesModel(Guid currentProfileId, Guid? userId)
        {

            FillLanguagesDD();
            BEClient.Languages _objLanguages = new BEClient.Languages();
            _objLanguages.LanguagesId = Guid.NewGuid();
            _objLanguages.ProfileId = currentProfileId;
            _objLanguages.UserId = (Guid)userId;
            return PartialView("Profile/_Languages", _objLanguages);
        }


        [HttpPost]
        public JsonResult AddUpdateLanguages(BEClient.Languages objLanguages)
        {
            String Message = String.Empty;
            bool IsError = true;
            String Data = "";
            Message = ServerValidation();
            try
            {
                if (!String.IsNullOrEmpty(Message))
                {
                    Exception ex = new Exception(Message);
                    throw ex;
                }
                BLClient.LanguagesAction objLanguagesAction = new BLClient.LanguagesAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                bool _isRecordExists = objLanguagesAction.IsRecordExists(objLanguages.LanguagesId);
                if (_isRecordExists)
                {
                    bool IsrecordUpdated = objLanguagesAction.UpdateLanguages(objLanguages);
                    if (IsrecordUpdated)
                    {
                        IsError = false;
                        Message = "Updated Successfully";
                        //data will be pass as empty to identify is update call or inser call
                    }
                }
                else
                {
                    Guid NewrecordAdded = objLanguagesAction.AddLanguages(objLanguages);
                    if (!NewrecordAdded.Equals(Guid.Empty))
                    {
                        IsError = false;
                        Message = "Record Inserted Successfully";
                        Data = NewrecordAdded.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            try
            {
                ATSCommon.CommonFunctions.SolrEmployeeDeltaImport();
            }
            catch (Exception ex)
            {
                log.Error("SOLR PROFILE  DELTA IMPORT (AddUpdateLanguages)" + ex.Message);
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
        }


        public JsonResult Removelanguages(Guid precordId)
        {
            try
            {

                BLClient.LanguagesAction objLanguagesAction = new BLClient.LanguagesAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                bool _isRecordExists = objLanguagesAction.IsRecordExists(precordId);
                if (_isRecordExists)
                {
                    bool IsDeleted = objLanguagesAction.DeleteLanguages(precordId);
                    if (IsDeleted)
                    {
                        try
                        {
                            ATSCommon.CommonFunctions.SolrEmployeeDeltaImport();
                        }
                        catch (Exception ex)
                        {
                            log.Error("SOLR PROFILE  DELTA IMPORT (Removelanguages)" + ex.Message);
                        }
                        return GetJson(base.GetJsonContent(false, string.Empty, " Languages  Deleted Successfully"), JsonRequestBehavior.AllowGet);
                    }
                    else
                    { return GetJson(base.GetJsonContent(true, string.Empty, " Languages   History not  Deleted "), JsonRequestBehavior.AllowGet); }



                }
                else
                {
                    return GetJson(base.GetJsonContent(false, string.Empty, "Languages  History Deleted Successfully"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return GetJson(base.GetJsonContent(true, string.Empty, ex.Message), JsonRequestBehavior.AllowGet);
            }

        }


        private void FillLanguagesDD()
        {
            try
            {

                ViewBag.YesNoDropDownRead = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownWrite = Common.CommonFunctions.YesNoDropDownList();
                ViewBag.YesNoDropDownSpeak = Common.CommonFunctions.YesNoDropDownList();
            }
            catch
            {
                throw;
            }
        }


        #endregion


        #region References

        public PartialViewResult AddReferencesModel(Guid currentProfileId, Guid? userId)
        {


            BEClient.References _objReferences = new BEClient.References();
            _objReferences.ReferencesId = Guid.NewGuid();
            _objReferences.ProfileId = currentProfileId;
            _objReferences.UserId = (Guid)userId;
            return PartialView("Profile/_References", _objReferences);
        }


        [HttpPost]
        public JsonResult AddUpdateReferences(BEClient.References objReferences)
        {
            String Message = String.Empty;
            bool IsError = true;
            String Data = "";
            Message = ServerValidation();
            try
            {
                if (!String.IsNullOrEmpty(Message))
                {
                    Exception ex = new Exception(Message);
                    throw ex;
                }
                BLClient.ReferencesAction objReferencesAction = new BLClient.ReferencesAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                bool _isRecordExists = objReferencesAction.IsRecordExists(objReferences.ReferencesId);
                if (_isRecordExists)
                {
                    bool IsrecordUpdated = objReferencesAction.UpdateReferences(objReferences);
                    if (IsrecordUpdated)
                    {
                        IsError = false;
                        Message = "Updated Successfully";
                        //data will be pass as empty to identify is update call or inser call
                    }
                }
                else
                {
                    Guid NewrecordAdded = objReferencesAction.AddReferences(objReferences);
                    if (!NewrecordAdded.Equals(Guid.Empty))
                    {
                        IsError = false;
                        Message = "Record Inserted Successfully";
                        Data = NewrecordAdded.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            try
            {
                ATSCommon.CommonFunctions.SolrEmployeeDeltaImport();
            }
            catch (Exception ex)
            {
                log.Error("SOLR PROFILE  DELTA IMPORT (AddUpdateReferences)" + ex.Message);
            }
            return GetJson(base.GetJsonContent(IsError, string.Empty, Message, Data));
        }


        public JsonResult RemoveReferences(Guid precordId)
        {
            try
            {

                BLClient.ReferencesAction objReferencesAction = new BLClient.ReferencesAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                bool _isRecordExists = objReferencesAction.IsRecordExists(precordId);
                if (_isRecordExists)
                {
                    bool IsDeleted = objReferencesAction.DeleteReferences(precordId);
                    if (IsDeleted)
                    {
                        try
                        {
                            ATSCommon.CommonFunctions.SolrEmployeeDeltaImport();
                        }
                        catch (Exception ex)
                        {
                            log.Error("SOLR PROFILE  DELTA IMPORT (RemoveReferences)" + ex.Message);
                        }
                        return GetJson(base.GetJsonContent(false, string.Empty, " References  Deleted Successfully"), JsonRequestBehavior.AllowGet);
                    }
                    else
                    { return GetJson(base.GetJsonContent(true, string.Empty, " References    not  Deleted "), JsonRequestBehavior.AllowGet); }



                }
                else
                {
                    return GetJson(base.GetJsonContent(false, string.Empty, "References   Deleted Successfully"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return GetJson(base.GetJsonContent(true, string.Empty, ex.Message), JsonRequestBehavior.AllowGet);
            }

        }

        #endregion

        #region Emergency Info

        [HttpPost]
        public JsonResult UpdateEmergencyInfo(BEClient.UserDetails objUserDetails)
        {
            try
            {
                BLClient.UserDetailsAction ObjUserDetailsAction = new BLClient.UserDetailsAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                bool IsUserDetailsUpdated = ObjUserDetailsAction.UpdateEmergencyContactDetails(objUserDetails);
                if (IsUserDetailsUpdated)
                {
                    return GetJson(base.GetJsonContent(false, string.Empty, "Emergency Contact Info updated Successfully"));
                }
                else
                {

                    return GetJson(base.GetJsonContent(true, string.Empty, "Emergency Contact Info is not updated"));

                }



            }
            catch (Exception ex)
            {


                return GetJson(base.GetJsonContent(true, string.Empty, ex.Message));

            }


        }

        #endregion


        #region Private Member
        private String ServerValidation()
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
            return ErrorMessage;
        }
        #endregion
    }
}
