using System.Web.Routing;
using System.Web;
using System.Web.Mvc;
using log4net;
public class SubDomainRoute : RouteBase
{
    public static readonly ILog log = LogManager.GetLogger(
    System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    public override RouteData GetRouteData(HttpContextBase httpContext)
    {
        log.Debug("CALL GetRouteData");
        
        var ns = new string[] { "ATS.WebUi.Controllers" };
        var url = httpContext.Request.Headers["HOST"];
        var index = url.IndexOf(".");
        log.Debug("= > " + index);
        if (index < 0)
        {
            return null;
        }

        var subDomain = url.Substring(0, index);
        log.Debug("subDomain  = > " + subDomain);
        var routeData = new RouteData(this, new MvcRouteHandler());
        routeData.Values.Add("controller", "Home"); //Goes to the User1Controller class
        routeData.Values.Add("action", "Index"); //Goes to the Index action on the User1Controller
        routeData.Values.Add("ClientName", subDomain);
        routeData.DataTokens["namespaces"] = ns;
        log.Debug("return routeData");
        return routeData;
    }

    public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
    {
        //Implement your formating Url formating here
        return null;
    }
}