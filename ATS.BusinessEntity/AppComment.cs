using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEDateFormatConst = ATS.BusinessEntity.Common.DateFormatConstant;

namespace ATS.BusinessEntity
{
  public class AppComment:BaseEntity
    {
      public Guid AppCommentId { get; set; }

      public Guid ApplicationId { get; set; }

      public String Comments { get; set; }
      public List<AppComment> AppCommentLst { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public string FullName 
      {
          get
          {
              return FirstName + " " + LastName;
          }
      }


      public string CreatedOnText
      {
          get
          {
              if (CreatedOn != DateTime.MinValue && CreatedOn != null)
                  return ((DateTime)CreatedOn).ToString(BEDateFormatConst.US_FORMAT);
              else
                  return string.Empty;
          }
          set
          {
              if (!String.IsNullOrEmpty(value))
                  CreatedOn = Convert.ToDateTime(value, System.Globalization.CultureInfo.GetCultureInfo(BEDateFormatConst.US_CULTURE_INFO).DateTimeFormat);
          }
      }
    
  }
}
