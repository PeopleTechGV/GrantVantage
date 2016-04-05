﻿using ATS.WebUi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLClient = ATS.BusinessLogic;
using BEClient = ATS.BusinessEntity;
using System.Web.Mvc;
using Newtonsoft.Json;
using ATSCommon = ATS.WebUi.Common;
using BEMaster = ATS.BusinessEntity.Master;
using BLMaster = ATS.BusinessLogic.Master;
using RootModels = ATS.WebUi.Models;

namespace ATS.WebUi.Areas.Employee.Controllers
{
    public class VacancyHistoryController : ATS.WebUi.Controllers.AreaBaseController
    {
        // GET: /Employee/VacancyHistory/
        #region Private Members
        private Guid _CurrentClientId;
        private Guid _CurrentLanguageId;

        #endregion

        #region OnAuthorization Method call on every request
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (base.CurrentClient != null && Common.CurrentSession.Instance.VerifiedUser != null)
            {
                _CurrentClientId = base.CurrentClient.ClientId;
                _CurrentLanguageId = base.CurrentClient.CurrentLanguageId;
                if (ATSCommon.CurrentSession.Instance.VerifiedUser.SecurityRoles.Where(x => x.AtsSecurityRole == BEClient.ATSSecurityRole.Candidate).Count() > 0)
                {
                    filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_CANDIDATE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_CANDIDATE_CONTROLLER, new { area = ATSCommon.Constants.AREA_CANDIDATE }));
                }
                //if (Common.CurrentSession.Instance.VerifiedUser.SecurityRoles.Where(x => x.AtsSecurityRole.Equals(BEClient.ATSSecurityRole.SystemAdministrator)).Count() <= 0)
                //{
                //    filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_CONTROLLER, new { @KeyMsg = ATSCommon.CommonFunctions.NonAuthoriseduser() }));
                //}
            }
        }
        #endregion

        [HttpGet]
        public JsonResult GetVacancyHistoryByVacancyId(Guid VacancyId)
        {
            string Data = string.Empty;
            bool IsError = false;
            string Message = string.Empty;
            try
            {
                List<BEClient.VacancyHistory> objVacancyHistoryList = new List<BEClient.VacancyHistory>();
                BLClient.VacancyHistoryAction objVacancyHistoryAction = new BLClient.VacancyHistoryAction(_CurrentClientId);
                objVacancyHistoryList = objVacancyHistoryAction.GetVacancyHistoryByVacancyId(VacancyId);
                Data = base.RenderRazorViewToString("_VacancyHistory", objVacancyHistoryList);
            }
            catch (Exception ex)
            {
                IsError = true;
                Message = ex.Message;
            }
            return GetJson(GetJsonContent(IsError, string.Empty, Message, Data), JsonRequestBehavior.AllowGet);
        }
    }
}
