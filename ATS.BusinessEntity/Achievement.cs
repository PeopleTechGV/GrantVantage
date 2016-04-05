using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEAchievementConst = ATS.BusinessEntity.Common.AchievementConst;
using BEDateFormatConst = ATS.BusinessEntity.Common.DateFormatConstant;
using System.ComponentModel.DataAnnotations;
using ATS.BusinessEntity.Attributes;
using System.Reflection;

namespace ATS.BusinessEntity
{
    public class Achievement:BaseEntity
    {
        public Guid AchievementId { get; set; }

        public Guid ProfileId { get; set; }

        public Guid UserId { get; set; }

        [Display(Name=BEAchievementConst.FRM_ACH_DATE)]
        public DateTime Date { get; set; }

        [Display(Name = BEAchievementConst.FRM_ACH_DESCRIPTION)]
        public String Description { get; set; }

        [Display(Name = BEAchievementConst.FRM_ACH_ISSUINGAUTHORITY)]
        [ATSStringLength(256)]
        public String IssuingAuthority { get; set; }


        public string DateText
        {
            get
            {
                if (Date != DateTime.MinValue)
                    return Date.ToString(BEDateFormatConst.US_FORMAT);
                else
                    return "";
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    Date = Convert.ToDateTime(value, System.Globalization.CultureInfo.GetCultureInfo(BEDateFormatConst.US_CULTURE_INFO).DateTimeFormat);
            }
        }

    }
}
