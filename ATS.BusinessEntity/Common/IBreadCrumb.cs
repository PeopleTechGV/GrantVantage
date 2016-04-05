using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ATS.BusinessEntity.Common
{
    public interface IBreadCrumb
    {
        String DisplayName { get; set; }
        String ItemName { get; set; }
        String ImagePath { get; set; }
        String ToolTip { get; set; }
        List<ATS.BusinessEntity.RecentlyViewed> objRecentViewedList { get; set; }
    }
}
