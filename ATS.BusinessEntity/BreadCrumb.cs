using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
namespace ATS.BusinessEntity
{
    public class BreadCrumb
    {
        public string Controller { get; set; }

        public string Action { get; set; }

        public RouteValueDictionary Param { get; set; }

        public string URL { get; set; }

        public string WithoutOrdinalURL { get; set; }

        public int ordinal { get; set; }

        public string className { get { return ordinal > 1 ? "mid" : ""; } }

        public string ParentKey { get; set; }

        public string SessionName { get; set; }

        public string AreaName { get; set; }

        public string ImagePath { get; set; }

        public string ToolTip { get; set; }
    }
    
}
