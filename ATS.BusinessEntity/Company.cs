using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BECommonConst = ATS.BusinessEntity.Common.Company;
using ATS.BusinessEntity.Attributes;


namespace ATS.BusinessEntity
{
    public class Company : BaseEntity
    {
        public Guid CompanyId { get; set; }

        public Guid ClientId { get; set; }

        [ATSStringLength(100), ATSRequired(ErrorMessage = "{0}")]
        [Display(Name = BECommonConst.FRM_COMPANY_COMNAME)]
        public String CompanyName { get; set; }
        
        [ATSRequired(ErrorMessage = "{0}")]
        //[ATSRegularExpression(@"\d{10,11}", ErrorMessage = "{0}")]
        [Display(Name = BECommonConst.FRM_COMPANY_PHNO)]
        public String PhoneNo { get; set; }


        [ATSRequired(ErrorMessage = "{0}")]
        [Display(Name = BECommonConst.FRM_COMPANY_ADD)]
        public String Address { get; set; }

        [ATSStringLength(100), ATSRequired(ErrorMessage = "{0}")]
        [Display(Name = BECommonConst.FRM_COMPANY_SUFF)]
        public String SuffixTitle { get; set; }
 
        [ATSStringLength(100), ATSRequired(ErrorMessage = "{0}")]
        [ATSRegularExpression(@"^(http:\/\/www\.|https:\/\/www\.|http:\/\/|https:\/\/)[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?$", ErrorMessage = "{0}")]
        [Display(Name = BECommonConst.FRM_COMPANY_WEBSITE)]
        public String WebSite { get; set; }

        public Boolean IsActive { get; set; }

       
    }
}
