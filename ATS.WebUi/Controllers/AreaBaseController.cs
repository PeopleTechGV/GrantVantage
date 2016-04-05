using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using ATS.WebUi.Helpers;
using ATSCommon = ATS.WebUi.Common;
using BEMaster = ATS.BusinessEntity.Master;
using BEClient = ATS.BusinessEntity;
using System.IO;
using log4net;
using ATS.WebUi.Models;

namespace ATS.WebUi.Controllers
{
    public class AreaBaseController : Controller
    {


        public bool IsSessionExpired { get; private set; }
        public string RedirectionUrl { get; private set; }
        private String _AreaName { get; set; }
        public BEMaster.Client CurrentClient;


        //Trial
        public Guid UserId;

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            IsSessionExpired = false;
            var routeData = filterContext.RequestContext.RouteData;
            if (routeData.DataTokens["area"] != null)
            {
                _AreaName = routeData.DataTokens["area"].ToString();
            }
            Guid ClientId = Guid.Empty;
            Boolean GotoIndexPage = true;
            if (ATS.WebUi.Common.CurrentSession.Instance == null || ATS.WebUi.Common.CurrentSession.Instance.VerifiedClient == null)
            {
                if (ATSCommon.CookieOperation.CookieExists(ATSCommon.Constants.COOKIE_CURRENT_CLIENTNAME, Request))
                {
                    String clientName = ATSCommon.CookieOperation.ReadCookie(ATSCommon.Constants.COOKIE_CURRENT_CLIENTNAME, Request);
                    BEMaster.Client objClient = BusinessLogic.Common.LoginOperation.CreateClientSession(clientName);
                    if (objClient != null)
                    {
                        GotoIndexPage = false;
                        ClientId = objClient.ClientId;
                        new ATSCommon.CurrentSession(objClient);
                        CurrentClient = ATS.WebUi.Common.CurrentSession.Instance.VerifiedClient;
                    }

                }
            }
            else
            {
                GotoIndexPage = false;
                CurrentClient = ATS.WebUi.Common.CurrentSession.Instance.VerifiedClient;
                ClientId = ATS.WebUi.Common.CurrentSession.Instance.VerifiedClient.ClientId;
            }

            if (ATS.WebUi.Common.CurrentSession.Instance == null || ATS.WebUi.Common.CurrentSession.Instance.VerifiedUser == null)
            {
                if (ATSCommon.CookieOperation.CookieExists(ATSCommon.Constants.COOKIE_CURRENT_USER, Request))
                {
                    String loggedinUserId = ATSCommon.CookieOperation.ReadCookie(ATSCommon.Constants.COOKIE_CURRENT_USER, Request);
                    Guid userid = Guid.Empty;
                    Guid.TryParse(loggedinUserId, out userid);
                    BEClient.User _objUser = BusinessLogic.Common.LoginOperation.GetLogedInUserObj(ClientId, userid);
                    if (_objUser != null)
                    {
                        new ATSCommon.CurrentSession(_objUser);

                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(_AreaName) && !ClientId.Equals(Guid.Empty))
                        {
                            CurrentClient = ATS.WebUi.Common.CurrentSession.Instance.VerifiedClient;
                            if (filterContext.HttpContext.Request.IsAjaxRequest())
                            {
                                IsSessionExpired = true;
                                RedirectionUrl = Url.Action("Index", "Home", new { @area = "", @ClientName = CurrentClient.Clientname });
                                filterContext.Result = this.GetJson(this.GetJsonContent(false, string.Empty));
                            }
                            else
                            {
                                filterContext.Result = new RedirectResult(Url.Action("Index", "Home", new { @area = "", @ClientName = CurrentClient.Clientname }));
                            }
                        }
                    }
                }
                else
                {
                    if (!String.IsNullOrEmpty(_AreaName) && !ClientId.Equals(Guid.Empty))
                    {
                        CurrentClient = ATS.WebUi.Common.CurrentSession.Instance.VerifiedClient;
                        if (filterContext.HttpContext.Request.IsAjaxRequest())
                        {
                            IsSessionExpired = true;
                            RedirectionUrl = Url.Action("Index", "Home", new { @area = "", @ClientName = CurrentClient.Clientname });
                            filterContext.Result = this.GetJson(this.GetJsonContent(false, string.Empty));
                        }
                        else
                        {
                            filterContext.Result = new RedirectResult(Url.Action("Index", "Home", new { @area = "", @ClientName = CurrentClient.Clientname }));
                        }
                    }
                }
            }


            if (GotoIndexPage)
            {
                IsSessionExpired = true;
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    RedirectionUrl = Url.Action("Index", "Home", new { @area = "" });
                }
                else
                {
                    filterContext.Result = new RedirectResult(Url.Action("Index", "Home", new { @area = "" }));
                }
            }
            base.OnAuthorization(filterContext);

        }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = null;
            if (ATS.WebUi.Common.CurrentSession.Instance != null)
            {
                cultureName = ATS.WebUi.Common.CurrentSession.Instance.VerifiedClient.CurrentCulture;
            }

            if (string.IsNullOrEmpty(cultureName))
            {
                cultureName = ATS.WebUi.Common.Constants.Default_Language_Culture;
            }
            //// Attempt to read the culture cookie from Request
            //HttpCookie cultureCookie = Request.Cookies["_culture"];
            //if (cultureCookie != null)
            //    cultureName = cultureCookie.Value;
            //else
            //    cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ?
            //            Request.UserLanguages[0] :  // obtain it from HTTP header AcceptLanguages
            //            null;

            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe

            // Modify current thread's cultures           
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            return base.BeginExecuteCore(callback, state);
        }

        protected string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }


        protected JsonResult GetJson(object data)
        {
            if (!Request.AcceptTypes.Contains("application/json"))
            {
                return base.Json(data, "text/plain");
            }
            else
            { return base.Json(data); }

        }

        protected JsonResult GetJson(object data, JsonRequestBehavior requestBehavior)
        {
            if (!Request.AcceptTypes.Contains("application/json"))
            {
                return base.Json(data, "text/plain", requestBehavior);
            }
            else
            { return base.Json(data, requestBehavior); }

        }

        protected JsonModels GetJsonContent(bool IsError, string Url, string Message = "", object data = null,bool IsDefaultMessage = false)
        {
            if (IsSessionExpired)
            {
                Url = RedirectionUrl;
                Message = "Session is expired";
            }

            return ATS.WebUi.Common.CommonFunctions.GetJsonContentObj(IsSessionExpired, IsError, Url, Message, data, IsDefaultMessage);
        }

    }
}
