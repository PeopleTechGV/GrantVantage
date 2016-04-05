using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ATS.BusinessEntity.Attributes;
using BELabel = ATS.BusinessEntity.Common.ClientLabel;
using BESubscription = ATS.BusinessEntity.Common.SubscriptionLabel;
using BECommonLabel = ATS.BusinessEntity.Common.CommonConstant;
using BECommon = ATS.BusinessEntity.Common;

namespace ATS.BusinessEntity.Master
{
    public class Subscription
    {
        public Guid SubscriptionId { get; set; }

        [Display(Name = BELabel.FRM_CLIENT_USERLIMIT)]
        [RegularExpression("^\\d{1,5}\\-\\d{1,5}$",ErrorMessage="Provide User Range")]
        [Required(ErrorMessage = BELabel.FRM_CLIENT_USERLIMIT + " " + BECommonLabel.REQUIRE + ".")]
        public string UserLimit { get; set; }

        [Display(Name = BESubscription.FRM_SUBSCRIPTION_PRICE)]
        //[StringLength(10,ErrorMessage="Out of length")]
        [Range(00,9999999999,ErrorMessage="Invalid amount")]
        [Required(ErrorMessage = BESubscription.FRM_SUBSCRIPTION_PRICE + " " + BECommonLabel.REQUIRE + ".")]
        public int? Price { get; set; }

        public string PriceText
        {
            get
            {
                return BECommon.CommonFunction.AddCommaAfterThreeDigit((int)Price);
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    Price = Convert.ToInt32(value);
            }
        }

    }
}
