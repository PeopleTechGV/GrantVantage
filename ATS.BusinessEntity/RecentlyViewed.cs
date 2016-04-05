using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity
{
   public class RecentlyViewed
    {
       public Guid RecentViewedId { get; set; }

       public Guid ViewItemId { get; set; }

       public string Category { get; set; }

       public string URL { get; set; }

       public Guid UserId { get; set; }

       public DateTime VisitDate { get; set; }

       public bool IsDelete { get; set; }

       public String DisplayText { get; set; }
    }
}
