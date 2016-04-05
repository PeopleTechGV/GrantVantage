using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using ATS.WebUi.Helpers;

using BEMaster = ATS.BusinessEntity.Master;
using ATS.WebUi.Models;

namespace ATS.WebUi.Controllers
{
    public class BaseController : Controller
    {
        public bool IsSessionExpired { get; private set; }
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = null;
          
            if (ATS.WebUi.Common.CurrentSession.Instance != null && ATS.WebUi.Common.CurrentSession.Instance.VerifiedClient!=null)
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


        protected JsonModels GetJsonContent(bool IsError, string Url, string Message = "", object data = null)
        {
            return ATS.WebUi.Common.CommonFunctions.GetJsonContentObj(IsSessionExpired, IsError, Url, Message, data);
        }

    }
}