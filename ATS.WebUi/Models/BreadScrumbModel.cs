using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ATS.BusinessEntity.Common;
using System.Web.Mvc;


namespace ATS.WebUi.Models
{
    public class BreadScrumbModel<T> : IBreadCrumb
    {
        public String DisplayName { get; set; }
        public String ItemName { get; set; }
        public String ImagePath { get; set; }
        public String ToolTip { get; set; } 
        public T obj { get; set; }
        public List<ATS.BusinessEntity.RecentlyViewed> objRecentViewedList { get; set; }
    }
}