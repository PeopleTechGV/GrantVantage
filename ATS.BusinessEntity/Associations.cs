using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEAssociationConst = ATS.BusinessEntity.Common.AssociationsConst;
using BEDateFormatConst = ATS.BusinessEntity.Common.DateFormatConstant;
using System.ComponentModel.DataAnnotations;
using ATS.BusinessEntity.Attributes;

namespace ATS.BusinessEntity
{
    public class Associations:BaseEntity
    {
        public Guid AssociationsId { get; set; }

        public Guid ProfileId { get; set; }

        public Guid UserId { get; set; }

        [Display(Name=BEAssociationConst.FRM_ASC_ASSOCIATIONTYPE)]
        public String AssociationType { get; set; }


       

        [Display(Name = BEAssociationConst.FRM_ASC_NAME)]
        [ATSStringLength(100)]
        [ATSRequired(ErrorMessage = "{0}")]
        public String Name { get; set; }

        [Display(Name = BEAssociationConst.FRM_ASC_STARTDATE)]
        public DateTime StartDate { get; set; }

        [Display(Name = BEAssociationConst.FRM_ASC_ENDDATE)]
        public DateTime EndDate { get; set; }

        [Display(Name = BEAssociationConst.FRM_ASC_ROLE)]
        [ATSStringLength(100)]
        public String Role { get; set; }

        [Display(Name = BEAssociationConst.FRM_ASC_LINK)]
        [ATSStringLength(100)]
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


    }
}
