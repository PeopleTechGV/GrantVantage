using System.Web.Mvc;
using System.Linq;
using System;
using System.Web.Routing;
using ATS.WebUi.Common;
namespace ATS.WebUi.Attributes
{
    public class SessionExistsAttribute : ActionFilterAttribute
    {
        public CurrentSession Session{ get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session != null)
            {

            }
            
        }
 
    }
}