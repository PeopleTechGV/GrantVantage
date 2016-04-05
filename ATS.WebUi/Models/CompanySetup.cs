using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace ATS.WebUi.Models
{
    public class CompanySetup
    {
        public String DisplayName { get; set; }
        public String Action{ get; set; }
        public String Controller{ get; set; }
        public RouteValueDictionary param{get;set;}
        public String ImagePath{ get; set; }
        public bool AllowToAdminOnly { get; set; }
        public bool IsDefault { get; set; }
    }
}