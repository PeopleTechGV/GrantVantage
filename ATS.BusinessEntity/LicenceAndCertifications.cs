using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BELicenceAndCerificationsConst = ATS.BusinessEntity.Common.LicenceAndCertificationsConstant;
using BEDateFormatConst = ATS.BusinessEntity.Common.DateFormatConstant;
using System.ComponentModel.DataAnnotations;
using ATS.BusinessEntity.Attributes;
namespace ATS.BusinessEntity
{
    public class LicenceAndCertifications:BaseEntity
    {
        public Guid LicenceAndCertificationsId { get; set; }

        public Guid ProfileId { get; set; }

        public Guid UserId { get; set; }
        [Display(Name=BELicenceAndCerificationsConst.FRM_NAME)]
        [ATSStringLength(256), ATSRequired(ErrorMessage = "{0}")]
        public String Name { get; set; }


        [Display(Name = BELicenceAndCerificationsConst.FRM_ISSUINGAUTHORITY)]
        [ATSStringLength(256)]
        public String IssuingAuthority { get; set; }

        [Display(Name = BELicenceAndCerificationsConst.FRM_DESCRIPTION)]
        public String Description { get; set; }

        [Display(Name = BELicenceAndCerificationsConst.FRM_VALIDFROM)]
        public DateTime ValidFrom { get; set; }

        [Display(Name = BELicenceAndCerificationsConst.FRM_VALIDTO)]
        public DateTime ValidTo { get; set; }

        public string ValidFromText
        {
            get
            {
                if (ValidFrom != DateTime.MinValue)
                    return ValidFrom.ToString(BEDateFormatConst.US_FORMAT);
                else
                    return "";
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    ValidFrom = Convert.ToDateTime(value, System.Globalization.CultureInfo.GetCultureInfo(BEDateFormatConst.US_CULTURE_INFO).DateTimeFormat);
            }
        }


        public string ValidToText
        {
            get
            {
                if (ValidTo != DateTime.MinValue)
                    return ValidTo.ToString(BEDateFormatConst.US_FORMAT);
                else
                    return "";
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    ValidTo = Convert.ToDateTime(value, System.Globalization.CultureInfo.GetCultureInfo(BEDateFormatConst.US_CULTURE_INFO).DateTimeFormat);
            }
        }




    }
}
