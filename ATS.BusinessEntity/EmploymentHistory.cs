using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BECommonConst = ATS.BusinessEntity.Common.RecordOfEmployementConstant;
using BEDateFormatConst = ATS.BusinessEntity.Common.DateFormatConstant;
using System.ComponentModel.DataAnnotations;
using ATS.BusinessEntity.Attributes;
using BECommon = ATS.BusinessEntity.Common;


namespace ATS.BusinessEntity
{
    public class EmploymentHistory : BaseEntity
    {
        public Guid EmployementId { get; set; }

        public Guid ProfileId { get; set; }

        public Guid UserId { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_COMPANY_NAME)]
        //[StringLength(50)]
        [ATSStringLength(100), ATSRequired(ErrorMessage = "{0}")]
        public string CompanyName { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_SUPERVISOR_NAME)]
        //[StringLength(50)]
        [ATSStringLength(100)]
        public string SupervisorName { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_TELEPHONE)]
        //[StringLength(11)]
        //[RegularExpression(@"\d{10,11}", ErrorMessage = "Telephone is not valid")]
        //[ATSRegularExpression(@"\d{10,11}", ErrorMessage = "{0}")]
        [ATSStringLength(15)]
            public string Telephone { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_ADDRESS)]
        [ATSStringLength(100)]
        public string Address { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_CITY)]
        [ATSStringLength(100)]
        public string City { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_STATE)]
        [ATSStringLength(100)]
        public string State { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_ZIP)]
        [ATSStringLength(10)]
        //[RegularExpression(@"\d{10,11}", ErrorMessage = "Zip code is invalid")]

        //[ATSRegularExpression(@"\d{6,7}", ErrorMessage = "{0}")]
        public string Zip { get; set; }



        [Display(Name = BECommonConst.FRM_PRF_START_DATE)]
        //[Required(ErrorMessage = "Stasrt Date is Required")]
        public DateTime StartDate { get; set; }

        //For Start Date Conversion
        [Display(Name = BECommonConst.FRM_PRF_START_DATE)]
        //[Required(ErrorMessage = "Start Date is Required")]
        public string StartDateText
        {
            get
            {
                if (StartDate != DateTime.MinValue)
                    return StartDate.ToString(BEDateFormatConst.US_FORMAT);
                else
                    return string.Empty;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    StartDate = Convert.ToDateTime(value, System.Globalization.CultureInfo.GetCultureInfo(BEDateFormatConst.US_CULTURE_INFO).DateTimeFormat);
            }
        }



        [Display(Name = BECommonConst.FRM_PRF_END_DATE)]
        public DateTime EndDate { get; set; }

        //For End Date Conversion
        [Display(Name = BECommonConst.FRM_PRF_END_DATE)]
        //[Required(ErrorMessage = "End Date is Required")]
        public string EndDateText
        {
            get
            {
                if (EndDate != DateTime.MinValue)
                    return EndDate.ToString(BEDateFormatConst.US_FORMAT);
                else
                    return "";
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    EndDate = Convert.ToDateTime(value, System.Globalization.CultureInfo.GetCultureInfo(BEDateFormatConst.US_CULTURE_INFO).DateTimeFormat);
            }
        }

        [Display(Name = BECommonConst.FRM_PRF_STARTING_POSITION)]
        //[StringLength(50)]
        [ATSStringLength(100)]
        public string StartigPosition { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_MOST_RECENT_POSITION)]
        //[Required(ErrorMessage = "Most Recent Position Is Required")]
        //[StringLength(50)]
        [ATSStringLength(100)]
        public string MostRecentPosition { get; set; }
            [ATSRange(0.00, 99999999.99)]
        [Display(Name = BECommonConst.FRM_PRF_STARTING_PAY)]
        public decimal StartingPay
        {
            get;
            set;
        }

        public string StartingPayText
        {
            get
            {
                return BECommon.CommonFunction.AddCommaAfterThreeDigit(StartingPay);
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    StartingPay = Convert.ToDecimal(value);
            }
        }

        [Display(Name = BECommonConst.FRM_PRF_ENDING_PAY)]
        [ATSRange(0.00, 99999999.99)]
        public decimal EndingPay { get; set; }

        public string EndingPayText
        {
            get
            {
                return BECommon.CommonFunction.AddCommaAfterThreeDigit(EndingPay);
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    EndingPay = Convert.ToDecimal(value);
            }
        }

        [Display(Name = BECommonConst.FRM_PRF_REASON_FOR_LEAVING)]
        public string ReasonForLeaving { get; set; }


        [Display(Name = BECommonConst.FRM_PRF_START_MONTH)]
        public int? StartMonth { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_END_MONTH)]
        public int? EndMonth { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_START_YEAR)]
        [Range(1900,2150)]
        public int? StartYear { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_END_YEAR)]
        [Range(1900, 2150)]
        public int? EndYear { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_EXPERIENCE)]
        public int Experience { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_JOBCATEGORY)]
        public string JobCategory { get; set; }



        [Display(Name = BECommonConst.FRM_PRF_DUTIES_AND_RESPONSIBILITES)]
        //[StringLength(50),Required]                       
        public string DutiesAndResponsibilities { get; set; }


        public List<Project> ObjProject { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_MAYWECONTACT)]
        public Boolean MayWeContact { get; set; }

        public int Count { get; set; }
    }
}
