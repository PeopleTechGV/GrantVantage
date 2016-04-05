using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using ATS.BusinessEntity.Attributes;
using BEClient = ATS.BusinessEntity;
using CompanyInfoConstants = ATS.BusinessEntity.Common.CompanyInfoConstant;
namespace ATS.BusinessEntity
{
    public class CompanyInfo : BaseEntityUpdate
    {
        public Guid CompanyInfoId { get; set; }


        [Display(Name = CompanyInfoConstants.FRM_COMPANY_NAME)]
        [ATSRequired(ErrorMessage = "{0}")]
        public String CompanyName { get; set; }

        public Guid ClientId { get; set; }

        [Display(Name = CompanyInfoConstants.FRM_PORTAL_BAN_TITLE)]
        [ATSRequired(ErrorMessage = "{0}")]
        public String PortalBannerTitle { get; set; }

        [Display(Name = CompanyInfoConstants.FRM_LOGO)]
        //[ATSRequired(ErrorMessage = "{0}")]
        public String Logo { get; set; }


        [Display(Name = CompanyInfoConstants.FRM_EMAIL_SEND_NAME)]
        [ATSRequired(ErrorMessage = "{0}")]
        [ATSRegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "{0}")]
        public String EmailSenderAddress { get; set; }

        public string StylePath { get; set; }
    }
}
