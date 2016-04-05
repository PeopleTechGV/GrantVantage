using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using ATS.BusinessEntity.Attributes;
using BEDateFormatConst = ATS.BusinessEntity.Common.DateFormatConstant;
using BECommonConst = ATS.BusinessEntity.Common.EducationConstant;
namespace ATS.BusinessEntity
{
    public class EducationHistory:BaseEntity
    {
        public Guid EducationId { get; set; }

        public Guid ProfileId { get; set; }

        public Guid UserId { get; set; }


        [Display(Name = BECommonConst.FRM_PRF_INSTITUTION_NAME)]
        [ATSStringLength(100), ATSRequired(ErrorMessage = "{0}")]
        public string InstitutionName { get; set; }


        [Display(Name = BECommonConst.FRM_PRF_START_DATE)]
        public DateTime StartDate { get; set; }


        [Display(Name = BECommonConst.FRM_PRF_END_DATE)]
        public DateTime EndDate { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_DEGREE_TYPE)]
        [ATSRequired(ErrorMessage = "{0}")]
        public Guid DegreeType { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_MAJOR_SUBJECT)]
        //[StringLength(50, ErrorMessage = "Major Subject is too long")]
        [ATSStringLength(100)]
        public string MajorSubject { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_START_DATE)]
        //[Required(ErrorMessage = "Start date is Required")]
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
        //[Required(ErrorMessage = "End date is Required")]
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

        public string DegreeDateText
        {
            get
            {
                if (DegreeDate != DateTime.MinValue)
                    return DegreeDate.ToString(BEDateFormatConst.US_FORMAT);
                else
                    return "";
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    DegreeDate = Convert.ToDateTime(value, System.Globalization.CultureInfo.GetCultureInfo(BEDateFormatConst.US_CULTURE_INFO).DateTimeFormat);
            }
        }


        [Display(Name = BECommonConst.FRM_PRF_EDUCITY)]     
        public string City { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_EDUSTATE)]
        public string State { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_COUNTRY)]
        public string Country { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_EDUDESCRIPTION)]
        public string Description { get; set; }


         [Display(Name = BECommonConst.FRM_PRF_DEGREEDATE)]
        public DateTime DegreeDate { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_MEASURESYSTEM)]
        public string MeasureSystem { get; set; }


        [Display(Name = BECommonConst.FRM_PRF_MEASUREVALUE)]
        public string MeasureValue { get; set; }



    }
}
