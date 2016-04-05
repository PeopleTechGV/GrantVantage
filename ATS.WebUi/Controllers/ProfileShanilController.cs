using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEClient = ATS.BusinessEntity;
using BLClient = ATS.BusinessLogic;
using BLCommon = ATS.BusinessLogic.Common;
using ATSCommon = ATS.WebUi.Common;

namespace ATS.WebUi.Controllers
{
    public partial class ProfileController : ATS.WebUi.Controllers.AreaBaseController
    {
        #region Job History
        public PartialViewResult AddJobHistoryModel(Guid currentProfileId, Guid? userId)
        {
            FillJobHistoryDD();
            BEClient.EmploymentHistory _objJobHistory = new BEClient.EmploymentHistory();
            _objJobHistory.ProfileId = currentProfileId;
            _objJobHistory.EmployementId = Guid.NewGuid();

            _objJobHistory.UserId = (Guid)userId;
            return PartialView("Profile/_JobHistory", _objJobHistory);
        }
        [HttpPost]
        public JsonResult AddUpdateJobHistory(BEClient.EmploymentHistory objEmploymentHistory)
        {
            String Message = String.Empty;
            bool IsError = true;
            String Data = "";
            Message = ServerValidation();
            if ((objEmploymentHistory.StartYear > objEmploymentHistory.EndYear) && String.IsNullOrEmpty(Message))
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
                BLClient.EmploymentHistoryAction objEmploymentAction = new BLClient.EmploymentHistoryAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                bool _isRecordExists = objEmploymentAction.IsRecordExists(objEmploymentHistory.EmployementId);
                if (_isRecordExists)
                {
                    bool IsrecordUpdated = objEmploymentAction.UpdateEmploymentHistory(objEmploymentHistory);
                    if (IsrecordUpdated)
                    {
                        IsError = false;
                        Message = "Updated Successfully";
                        //data will be pass as empty to identify is update call or inser call
                    }
                }
                else
                {
                    Guid NewrecordAdded = objEmploymentAction.AddEmploymentHistory(objEmploymentHistory);
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

        private void FillJobHistoryDD()
        {
            try
            {
                ViewBag.YesNoDropDownMayWeContact = Common.CommonFunctions.YesNoDropDownList();

                var lstMonths = ATS.WebUi.Common.CommonFunctions.BindMonthDropDown();
                var lstEndMonths = ATS.WebUi.Common.CommonFunctions.BindEndMonthDropDown();

                var lstYears = ATS.WebUi.Common.CommonFunctions.BindYearDropDown();

                ViewBag.StartMonthsList = new SelectList(lstMonths, "Value", "Text");
                ViewBag.StartYearList = new SelectList(lstYears, "Value", "Text");
                ViewBag.EndMonthsList = new SelectList(lstEndMonths, "Value", "Text");
                ViewBag.EndYearList = new SelectList(lstYears, "Value", "Text");
            }
            catch
            {
                throw;
            }
        }

        public JsonResult RemoveJobHistory(Guid precordId)
        {
            try
            {

                BLClient.EmploymentHistoryAction objEmploymentAction = new BLClient.EmploymentHistoryAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                bool _isRecordExists = objEmploymentAction.IsRecordExists(precordId);
                if (_isRecordExists)
                {
                    bool IsDeleted = objEmploymentAction.DeleteEmploymentHistory(precordId);
                    if (IsDeleted)
                    { return GetJson(base.GetJsonContent(false, string.Empty, " Education History Deleted Successfully"), JsonRequestBehavior.AllowGet); }
                    else
                    { return GetJson(base.GetJsonContent(true, string.Empty, " Education History not  Deleted "), JsonRequestBehavior.AllowGet); }

                    return GetJson(base.GetJsonContent(false, string.Empty, "Job History Deleted Successfully"), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return GetJson(base.GetJsonContent(false, string.Empty, "Job History Deleted Successfully"), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return GetJson(base.GetJsonContent(true, string.Empty, ex.Message), JsonRequestBehavior.AllowGet);
            }

        }

        #endregion
    }
}
