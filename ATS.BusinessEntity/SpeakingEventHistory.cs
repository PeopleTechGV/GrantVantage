using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BESpeakingHistoryEventConst = ATS.BusinessEntity.Common.SpeakingEventHistoryConst;
using BEDateFormatConst = ATS.BusinessEntity.Common.DateFormatConstant;
using System.ComponentModel.DataAnnotations;
using ATS.BusinessEntity.Attributes;

namespace ATS.BusinessEntity
{
    public class SpeakingEventHistory:BaseEntity
    {
        public Guid SpeakingEventHistoryId { get; set; }

        public Guid UserId { get; set; }

        public Guid ProfileId { get; set; }


        [Display(Name=BESpeakingHistoryEventConst.FRM_SPK_TITLE)]
        [ATSStringLength(256)]
        [ATSRequired(ErrorMessage = "{0}")]
        public String Title { get; set; }

        [Display(Name = BESpeakingHistoryEventConst.FRM_SPK_STARTDATE)]
        public DateTime StartDate { get; set; }

        [Display(Name = BESpeakingHistoryEventConst.FRM_SPK_EVENTNAME)]
        [ATSStringLength(256)]
        public String EventName { get; set; }

        [Display(Name = BESpeakingHistoryEventConst.FRM_SPK_EVENTTYPE)]
        [ATSStringLength(256)]
        public string EventType { get; set; }

        [Display(Name = BESpeakingHistoryEventConst.FRM_SPK_LOCATION)]
        [ATSStringLength(100)]
        public String Location { get; set; }

        [Display(Name = BESpeakingHistoryEventConst.FRM_SPK_LINK)]
        [ATSStringLength(256)]
        public String Link { get; set; }


        public string StartDateText
        {
            get
            {
                if (StartDate != DateTime.MinValue)
                    return StartDate.ToString(BEDateFormatConst.US_FORMAT);
                else
                    return "";
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    StartDate = Convert.ToDateTime(value, System.Globalization.CultureInfo.GetCultureInfo(BEDateFormatConst.US_CULTURE_INFO).DateTimeFormat);
            }
        }




    }
}
