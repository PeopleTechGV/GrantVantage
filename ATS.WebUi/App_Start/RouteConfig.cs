using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace ATS.WebUi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");

            
            //routes.Add(new SubDomainRoute());
            routes.MapRoute(
  name: "Vacancy",
  url: "Vacancy/{PublicCode}/{ClientName}/{ManagerLink}",
  defaults: new { controller = "Home", action = "EmployeeLogin", PublicCode = UrlParameter.Optional, ClientName = UrlParameter.Optional, ManagerLink = UrlParameter.Optional },
   namespaces: new[] { "ATS.WebUi.Controllers" });


            routes.MapRoute(
               name: "Default",
               url: "{ClientName}",
               defaults: new { controller = "Home", action = "Index", ClientName = UrlParameter.Optional },
               namespaces: new[] { "ATS.WebUi.Controllers" });

            routes.MapRoute(
       name: "AdminDefault",
       url: "Admin/Home",
       defaults: new { controller = "Home", action = "Index" },
       namespaces: new[] { "ATS.WebUi.Areas.Admin.Controllers" });

            routes.MapRoute(
                name: "Default_1",
                url: "{controller}/{action}",
                defaults: new { },
                namespaces: new[] { "ATS.WebUi.Controllers" });


            routes.MapRoute(
        name: "Resume",
        url: "Resume/{UserId}/{ProfileId}/{ClientName}",
        defaults: new { controller = "PublicViewProfile", action = "DownloadResume", UserId = UrlParameter.Optional, ProfileId = UrlParameter.Optional, ClientName = UrlParameter.Optional },
         namespaces: new[] { "ATS.WebUi.Controllers" });

            routes.MapRoute(
              name: "CandidateDetails",
              url: "Index/{ApplicationId}",
              defaults: new { controller = "CandidateDetails", action = "Index", ClientName = UrlParameter.Optional, ApplicationId = UrlParameter.Optional },
               namespaces: new[] { "ATS.WebUi.Areas.Employee" });

            routes.MapRoute(
              name: "ConfirmOfferPDF",
              url: "ConfirmOfferPDF/{ApplicationId}",
              defaults: new { controller = "Application", action = "ConfirmOfferPDF", ClientName = UrlParameter.Optional, UserId = UrlParameter.Optional, VacancyOfferId = UrlParameter.Optional, AppId = UrlParameter.Optional },
               namespaces: new[] { "ATS.WebUi.Areas.Employee" });

        }
    }
}