using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATSCommon = ATS.WebUi.Common;
using ATS.WebUi.Models;
using BECommon = ATS.BusinessEntity.Common;
using BEClient = ATS.BusinessEntity;
using BLClient = ATS.BusinessLogic;

namespace ATS.WebUi.Areas.Candidate.Controllers
{
    public class HomeController : ATS.WebUi.Controllers.AreaBaseController
    {
        private Guid _CurrentClientId;
        private Guid _CurrentLanguageId;

        #region OnAuthorization Method call on every request
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (base.CurrentClient != null && Common.CurrentSession.Instance.VerifiedUser != null)
            {
                _CurrentClientId = base.CurrentClient.ClientId;
                _CurrentLanguageId = base.CurrentClient.CurrentLanguageId;
                if (ATSCommon.CurrentSession.Instance.VerifiedUser.SecurityRoles.Where(x => x.AtsSecurityRole == BEClient.ATSSecurityRole.Candidate).Count() <= 0)
                {
                    filterContext.Result = new RedirectResult(Url.Action(ATSCommon.Constants.DEFAULT_REDIRECT_CANDIDATE_ACTION, ATSCommon.Constants.DEFAULT_REDIRECT_EMPLOYEE_CONTROLLER, new { area = ATSCommon.Constants.AREA_EMPLOYEE }));

                }
            }
        }
        #endregion

        #region BRead Scrum
        public void NavIndex()
        {
            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = null;
            List<BEClient.BreadCrumb> objBreadCrumblst = new List<BEClient.BreadCrumb>();

            BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
            objBreadCrumb.Action = "Index";
            objBreadCrumb.Controller = "Home";
            objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_CANDIDATE;
            objBreadCrumb.ordinal = 1;
            objBreadCrumb.URL = Url.Action("Index", "Home", new { area = ATS.WebUi.Common.Constants.AREA_CANDIDATE });
            objBreadCrumb.ImagePath = BECommon.ImagePaths.HomeImage;
            objBreadCrumb.ToolTip = "Home";
            objBreadCrumb.WithoutOrdinalURL = Url.Action("Index", "Home", new { area = ATS.WebUi.Common.Constants.AREA_CANDIDATE });
            if (!ATSCommon.CommonFunctions.CheckDuplicateBreadscrum(objBreadCrumb))
                objBreadCrumblst.Add(objBreadCrumb);
            ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
        }
        #endregion

        public ActionResult Index()
        {
            NavIndex();
            BreadScrumbModel<DBNull> _objBcModel = new BreadScrumbModel<DBNull>();
            _objBcModel.DisplayName = BECommon.HomeConstant.FEATURED + " " + BECommon.CommonConstant.JOBS;
            _objBcModel.ImagePath = BECommon.ImagePaths.FeaturedVacancyImage;
            _objBcModel.ToolTip = "Featured Jobs";
            return View(_objBcModel);
        }

        public ActionResult CandidateChangePassword()
        {
            try
            {
                ATS.WebUi.Models.SignUpModel objSignUpUser = new SignUpModel();
                objSignUpUser.UserId = Common.CurrentSession.Instance.VerifiedUser.UserId;
                objSignUpUser.UserName = ATS.WebUi.Common.CurrentSession.Instance.UserName;
                return View("ChangePassword", objSignUpUser);

            }
            catch
            {
                throw;
            }
        }
    }
}