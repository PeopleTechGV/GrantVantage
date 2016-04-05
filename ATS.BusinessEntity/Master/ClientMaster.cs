using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BELabel = ATS.BusinessEntity.Common.ClientLabel;
using BECommonLabel = ATS.BusinessEntity.Common.CommonConstant;
using BECurrencyLabel = ATS.BusinessEntity.Common.CurrencySign;
using BELanguageLabel = ATS.BusinessEntity.Common.LanguageLabel;
using BEDateFormatConst = ATS.BusinessEntity.Common.DateFormatConstant;

namespace ATS.BusinessEntity.Master
{
    public class ClientMaster
    {
        public Guid ClientId { get; set; }

        [Display(Name = BELabel.FRM_CLIENT_CLIENTNAME)]
        [StringLength(50, MinimumLength = 1)]
        [Required(ErrorMessage = BELabel.FRM_CLIENT_CLIENTNAME + " " + BECommonLabel.REQUIRE + ".")]
        public String ClientName { get; set; }

        [Display(Name = BELabel.FRM_CLIENT_CONTACTPERSON)]
        [StringLength(50, MinimumLength = 1)]
        [Required(ErrorMessage = BELabel.FRM_CLIENT_CONTACTPERSON + " " + BECommonLabel.REQUIRE + ".")]
        public String ContactPerson { get; set; }

        [Display(Name = BELabel.FRM_CLIENT_CONTACTNO)]
        [StringLength(50, MinimumLength = 1)]
        [Required(ErrorMessage = BELabel.FRM_CLIENT_CONTACTNO + " " + BECommonLabel.REQUIRE + ".")]
        public String ContactNo { get; set; }

        [Display(Name = BELabel.FRM_CLIENT_SUBDOMAIN)]
        [StringLength(50, MinimumLength = 1)]
        [Required(ErrorMessage = BELabel.FRM_CLIENT_SUBDOMAIN + " " + BECommonLabel.REQUIRE + ".")]
        public String SubDomain { get; set; }

        [Display(Name = BELabel.FRM_CLIENT_CONNECTIONSTRING)]
        [StringLength(256, MinimumLength = 1)]
        [Required(ErrorMessage = BELabel.FRM_CLIENT_CONNECTIONSTRING + " " + BECommonLabel.REQUIRE + ".")]
        public String ConnectionString { get; set; }

        [Display(Name = BELabel.FRM_CLIENT_DBNAME)]
        [StringLength(50, MinimumLength = 1)]
        [Required(ErrorMessage = BELabel.FRM_CLIENT_DBNAME + " " + BECommonLabel.REQUIRE + ".")]
        public String DatabaseName { get; set; }

         [Display(Name = BELabel.FRM_CLIENT_WEBSITE)]
         [Required(ErrorMessage = BELabel.FRM_CLIENT_WEBSITE)]
        public String WebSite { get; set; }

        public List<Language> LanguageList { get; set; }

        [Display(Name = BELabel.FRM_CLIENT_STARTDATE)]
        public DateTime StartDate { get; set; }

        [Display(Name = BELabel.FRM_CLIENT_ENDDATE)]
        public DateTime EndDate { get; set; }

        [Display(Name = BELabel.FRM_CLIENT_USERLIMIT)]
        public String UserLimit { get; set; }

        public List<Subscription> ClientSubscription { get; set; }

        public Guid SubscriptionId { get; set; }

        [Required(ErrorMessage = BELabel.FRM_CLIENT_STARTDATE + " " + BECommonLabel.REQUIRE + ".")]
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

        [Required(ErrorMessage = BELabel.FRM_CLIENT_ENDDATE + " " + BECommonLabel.REQUIRE + ".")]
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

        [Display(Name = BECurrencyLabel.FRM_CurrencySign)]
        [StringLength(7, MinimumLength = 1)]
        [Required(ErrorMessage = BECurrencyLabel.FRM_CurrencySign + " " + BECommonLabel.REQUIRE + ".")]
        public string Currency { get; set; }
    }
}
