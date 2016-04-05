using System;
using System.Collections.Generic;
using System.Linq;
using BEDateFormatConst = ATS.BusinessEntity.Common.DateFormatConstant;
using System.Text;

namespace ATS.BusinessEntity
{
  public class InterviewCalender
    {
      public Guid ScheduleIntId { get; set; }
      public Guid ApplicationId { get;set; }

      public String InterviewDateTime { get; set; }
      
      public String ApplicantName { get; set; }

      public String JobTitle { get; set; }

      public DateTime AppliedOn { get; set; }

      public String Status { get; set; }

      public float Score { get; set; }

      public string AppliedOnText
      {
          get
          {
              if (AppliedOn != DateTime.MinValue && AppliedOn != null)
                  return ((DateTime)AppliedOn).ToString(BEDateFormatConst.US_FORMAT);
              else
                  return string.Empty;
          }
          set
          {
              if (!String.IsNullOrEmpty(value))
                  AppliedOn = Convert.ToDateTime(value, System.Globalization.CultureInfo.GetCultureInfo(BEDateFormatConst.US_CULTURE_INFO).DateTimeFormat);
          }
      }

      //for getting Counts of interview status
      public int TotalCounts {get;set;}
      public string IntStatus { get; set; }
      
  }
}
