using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using ATS.WebUi.Helpers;
using ATSCommon = ATS.WebUi.Common;
using BEMaster = ATS.BusinessEntity.Master;
using ATS.WebUi.Models;

namespace ATS.WebUi.Areas.Admin.Controllers
{
    public class AdminBaseController : Controller
    {
        //
        // GET: /Admin/AdminBase/
        public BEMaster.UserMaster CurrentUserMaster;
        public bool IsSessionExpired { get; private set; }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            IsSessionExpired = false;
            if (ATS.WebUi.Common.CurrentSession.Instance == null || ATS.WebUi.Common.CurrentSession.Instance.VerifiedUserMaster == null)
            {
                IsSessionExpired = true;
                filterContext.Result = new RedirectResult(Url.Action("Index", "Home", new { @area = "Admin" }));
            }
            else 
            {
                CurrentUserMaster = ATS.WebUi.Common.CurrentSession.Instance.VerifiedUserMaster;

            }
            base.OnAuthorization(filterContext);

        }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = null;
           
            cultureName = ATS.WebUi.Common.Constants.Default_Language_Culture;

            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe

            // Modify current thread's cultures           
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            return base.BeginExecuteCore(callback, state);
        }
        protected JsonModels GetJsonContent(bool IsError, string Url, string Message = "", object data = null)
        {
            return ATS.WebUi.Common.CommonFunctions.GetJsonContentObj(IsSessionExpired, IsError, Url, Message, data);

        }
    }
}
