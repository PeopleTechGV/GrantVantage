using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using BECommonConst = ATS.BusinessEntity.Common.AvailabilityConstant;
using ATS.BusinessEntity.Attributes;
using BECommon = ATS.BusinessEntity.Common;

namespace ATS.BusinessEntity
{
    public class Availability : BaseEntity
    {
        public Availability()
        {
            EligibleToWorkInUS = true;
            HowOld = true;
        }
        public Guid? AvailibilityId { get; set; }

        public Guid ProfileId { get; set; }



        //For Date Availabilty Conversion
        [Display(Name = BECommonConst.FRM_PRF_DATE_AVAILABILITY)]
        [ATSRequired(ErrorMessage = "{0}")]
        public string DateAvailabilityText
        {
            get
            {
                if (DateAvailability != DateTime.MinValue)
                    return DateAvailability.ToString(Common.DateFormatConstant.US_FORMAT);
                else
                    return "";
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    DateAvailability = Convert.ToDateTime(value, System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat);
            }
        }

        [Display(Name = BECommonConst.FRM_PRF_DATE_AVAILABILITY)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        //[Required(ErrorMessage = "Date Availability is a required field")]
        public DateTime DateAvailability { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_TARGET_INCOME)]
        [ATSRequired(ErrorMessage = "{0}")]
        [ATSRange(0.00,99999999.99)]
        public decimal TargetIncome { get; set; }

        public string TargetIncomeText
        {
            get
            {
                return BECommon.CommonFunction.AddCommaAfterThreeDigit(TargetIncome);
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    TargetIncome = Convert.ToDecimal(value);
            }
        }

        [Display(Name = BECommonConst.FRM_PRF_HRS_MON)]
        public string HrsMon { get; set; }
        public string HrsMonName { get; set; }
        public List<string> ListHrsMon { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_HRS_TUE)]
        public string HrsTue { get; set; }
        public string HrsTueName { get; set; }
        public List<string> ListHrsTue { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_HRS_WED)]
        public string HrsWed { get; set; }
        public string HrsWedName { get; set; }
        public List<string> ListHrsWed { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_HRS_THU)]
        public string HrsThu { get; set; }
        public string HrsThuName { get; set; }
        public List<string> ListHrsThu { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_HRS_FRI)]
        public string HrsFri { get; set; }
        public string HrsFriName { get; set; }
        public List<string> ListHrsFri { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_HRS_SAT)]
        public string HrsSat { get; set; }
        public string HrsSatName { get; set; }
        public List<string> ListHrsSat { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_HRS_SUN)]
        public string HrsSun { get; set; }
        public string HrsSunName { get; set; }
        public List<string> ListHrsSun { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_EMPLOYMENT_PREFERENCE)]
        //[StringLength(100, ErrorMessage = "Employment Preference too long")]
        //[ATSStringLength(100)]
        public int EmploymentPreference { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_WILLING_TO_WORK_OVERTIME)]
        //[Required(ErrorMessage = "Willing to work overtime? is a required field")]
        public bool WillingToWorkOverTime { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_RELATIVES_WORKING_IN_COMPANY)]
        //[Required(ErrorMessage = "Do you have any relatives working at this company? is a required field")]
        public bool RelativesWorkingInCompany { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_RELATIVES_IF_YES)]
        //[Required(ErrorMessage = "If yes, who? is a required field")]
        //[StringLength(100, ErrorMessage = "If yes, who? too long")]
        [ATSStringLength(100)]
        public string RelativesIfYes { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_SUBMITTED_APPLICATION_BEFORE)]
        //[Required(ErrorMessage = " Required field")]
        public bool SubmittedApplicationBefore { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_APPLICATION_SUBMISION)]
        //[StringLength(100, ErrorMessage = "too long")]
        [ATSStringLength(256)]
        public string AppicationSubmision { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_HEAR_ABOUT_THE_POSITION)]
        //[StringLength(100, ErrorMessage = "too long")]
        //[Required(ErrorMessage = "required Field")]
        [ATSStringLength(100)]
        public string HearAboutThePosition { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_REFFERED_BY)]
        //[StringLength(100, ErrorMessage = "Referred By too long")]
        [ATSStringLength(100)]
        public string ReferredBy { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_HOW_OLD)]
        //[Required(ErrorMessage = "Are you 18 years or older? is a required field")]
        //[StringLength(3,ErrorMessage="Invalid Age")]
        //[Range(1, 150, ErrorMessage = "Invalid Age")]
        public bool HowOld { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_ELIGIBLE_TO_WORK_IN_US)]
        //[Required(ErrorMessage = "Required Field")]
        public bool EligibleToWorkInUS { get; set; }

        [Display(Name = "Availability Comments")]
        public string AvailabilityComments { get; set; }

    }
}
