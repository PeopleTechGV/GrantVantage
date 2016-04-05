using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATSCommon = ATS.WebUi.Common;
using BEClient = ATS.BusinessEntity;
using BEMAster = ATS.BusinessEntity.Master;
using BLClient = ATS.BusinessLogic;
using Newtonsoft.Json;
using BLCommon = ATS.BusinessLogic.Common;
using BECommon = ATS.BusinessEntity.Common;
using BESrp = ATS.BusinessEntity.SRPEntity;
using RootModels = ATS.WebUi.Models;
using ATS.WebUi.Models;
using System.Web.Mvc.Html;
using DisplayMessageConstant = ATS.BusinessEntity.Common;

namespace ATS.WebUi.Areas.Employee.Controllers
{
    public class InterviewCalenderController : ATS.WebUi.Controllers.AreaBaseController
    {
        //
        // GET: /Employee/InterviewCalender/

        #region Private Members
        private BLClient.InterviewCalenderAction _objInterviewCalenderAction;
        private BESrp.DynamicSRP<List<BEClient.InterviewCalender>> _objInterviewCalenderList;
        private BESrp.DynamicSRP<BEClient.InterviewCalender> _objInterviewCalender;
        private Guid _UserId;
        private Guid _CurrentLanguageId;
        private Guid _CurrentClientId;
        #endregion

        #region Initialize Method call on every request
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {


            base.OnAuthorization(filterContext);
            if (base.CurrentClient != null && ATS.WebUi.Common.CurrentSession.Instance.VerifiedUser != null)
            {


                _objInterviewCalenderAction = new BLClient.InterviewCalenderAction(base.CurrentClient.ClientId);
                _CurrentClientId = base.CurrentClient.ClientId;
                //Create new object for List
                _UserId = ATSCommon.CurrentSession.Instance.UserId;
                _CurrentLanguageId = base.CurrentClient.CurrentLanguageId;
                _objInterviewCalenderList = new BESrp.DynamicSRP<List<BEClient.InterviewCalender>>();
                _objInterviewCalenderList.AddBtnText = "Add Vacancy";
                _objInterviewCalenderList.EditBtnText = "Edit Vacancy";
                _objInterviewCalenderList.RemoveBtnText = "Remove Vacancy";
                _objInterviewCalenderList.DeleteBtnText = "Delete Vacancy";
                _objInterviewCalenderList.SaveBtnText = "Save Vacancy";
                if (ATSCommon.CurrentSession.Instance.VerifiedUser.SecurityRoles.Where(x => x.AtsSecurityRole == BEClient.ATSSecurityRole.Candidate).Count() > 0)
                {
                    filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_CANDIDATE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_CANDIDATE_CONTROLLER, new { area = ATSCommon.Constants.AREA_CANDIDATE }));

                }


            }
        }


        #endregion
        #region BRead Scrum
        private void NavIndex()
        {
            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = new List<BEClient.BreadCrumb>();
            List<BEClient.BreadCrumb> objBreadCrumblst = new List<BEClient.BreadCrumb>();
            BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
            objBreadCrumb.Action = "Index";
            objBreadCrumb.Controller = "InterviewCalender";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_EMPLOYEE;
            objBreadCrumb.ordinal = 1;
            objBreadCrumb.URL = Url.Action("Index", "InterviewCalender", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });
            objBreadCrumb.ImagePath = BECommon.ImagePaths.VacancyImage;
            objBreadCrumb.ToolTip = "InterviewCalender";
            objBreadCrumb.WithoutOrdinalURL = Url.Action("Index", "InterviewCalender", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });

            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);

            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
        }
        #endregion
        public ActionResult Index(string Status = "Pending", string SearchText = "")
        {
            RootModels.BreadScrumbModel<BESrp.DynamicSRP<List<BEClient.InterviewCalender>>> model = new BreadScrumbModel<BESrp.DynamicSRP<List<BEClient.InterviewCalender>>>();
            ViewBag.SearchText = SearchText;
            ViewBag.SearchTextImg = (SearchText == "" ? "Search" : "Clear");//To change the image in the Search Text box.
            try
            {
                _objInterviewCalenderList.obj = _objInterviewCalenderAction.GetAllInterviewCalender(_CurrentLanguageId, Status, SearchText);

                model.obj = _objInterviewCalenderList;
                ViewBag.VacStatus = Status;
                List<BEClient.InterviewCalender> objcount = new List<BEClient.InterviewCalender>();
                objcount = _objInterviewCalenderAction.GetAllInterviewStatusCounts(ATS.WebUi.Common.CurrentSession.Instance.UserId);
                NavIndex();
                if (objcount != null && objcount.Count() > 0)
                {
                    foreach (BEClient.InterviewCalender _item in objcount)
                    {
                        if (_item.IntStatus == "Begin")
                        {
                            @ViewBag.CntBegin = _item.TotalCounts;
                        }
                        else if (_item.IntStatus == "Complete")
                        {
                            @ViewBag.CntComplete = _item.TotalCounts;
                        }
                        else if (_item.IntStatus == "Terminate")
                        {
                            @ViewBag.CntTerminate = _item.TotalCounts;
                        }
                        else if (_item.IntStatus == "All Begin")
                        {
                            @ViewBag.CntAllBegin = _item.TotalCounts;
                        }
                        else if (_item.IntStatus == "All Complete")
                        {
                            @ViewBag.CntAllComplete = _item.TotalCounts;
                        }
                        else
                        {
                            @ViewBag.CntAllTerminate = _item.TotalCounts;
                        }

                    }
                    if (Status == null || Status.ToLower() == "pending")
                        ViewBag.IntCurrStatusCount = @ViewBag.CntBegin;
                    else if (Status.ToLower() == "completed")
                        ViewBag.IntCurrStatusCount = @ViewBag.CntComplete;
                    else if (Status.ToLower() == "terminated")
                        ViewBag.IntCurrStatusCount = @ViewBag.CntTerminate;
                    else if (Status.ToLower() == "allpending")
                        ViewBag.IntCurrStatusCount = @ViewBag.CntAllBegin;
                    else if (Status.ToLower() == "allcompleted")
                        ViewBag.IntCurrStatusCount = @ViewBag.CntAllComplete;
                    else if (Status.ToLower() == "allterminated")
                        ViewBag.IntCurrStatusCount = @ViewBag.CntAllTerminate;
                }
                return View(model);
            }
            
            catch
            {
                throw;
            }
                
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

    }
}
