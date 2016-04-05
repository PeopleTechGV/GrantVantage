using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BECommonConst = ATS.BusinessEntity.Common.PersonalInfoConstant;
using ATS.BusinessEntity.Attributes;

namespace ATS.BusinessEntity
{
   
    public class UserDetails:ClientBaseEntity
    {
        
        public Guid? UserId { get; set; }
        
        [ATSRequired]
        public string ProfileName { get; set; }
 
        public string UserName { get; set; }

        public string Password { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_AFFIX)]
        public string Affix { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_FAX)]
        public string Fax { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_WEBSITE)]
        public string Website { get; set; }


        [Display(Name = BECommonConst.FRM_PRF_POSTALCODE)]
        public string PostalCode { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_POSTOFFICEBOXNO)]
        public string PostOfficeBox { get; set; }
         

        [Display(Name = BECommonConst.FRM_PRF_HOME_EMAIL)]
        [ATSStringLength(50)]
        [ATSRegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "{0}")]
        public string HomeEmail { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_FIRST_NAME)]
        [ATSStringLength(50),  ATSRequired(ErrorMessage = "{0}")]
        public string FirstName { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_MIDDLE_NAME)]
        [ATSStringLength(50)]
        public string MiddleName { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_LAST_NAME)]
        [ATSStringLength(50),  ATSRequired(ErrorMessage = "{0}")]
        public string LastName { get; set; }


        [Display(Name = BECommonConst.FRM_PRF_ADDRESS)]
        [ATSStringLength(200), ATSRequired(ErrorMessage = "{0}")]
        public string Address { get; set; }


        [Display(Name = BECommonConst.FRM_PRF_CITY)]
        [ATSStringLength(50), ATSRequired(ErrorMessage = "{0}")]
        public string City { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_STATE)]
        //[StringLength(50), Required(ErrorMessage = "State is a required field")]
        public string State { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_ZIP)]
        //[StringLength(11), Required(ErrorMessage = "Zip is a required field")]
        //[RegularExpression(@"^[0-9]+$", ErrorMessage = "Zip code is invalid")]
        [ATSStringLength(15)]
        public string Zip { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_BUSINESS_PHONE_NO)]
        //[StringLength(11)]
        //[RegularExpression(@"\d{10,11}", ErrorMessage = "Business Phone No is not valid")]
        [ATSStringLength(15)]
        public string BusinessPhoneNo { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_HOME_PHONE)]
        //[StringLength(11)]
        //[RegularExpression(@"\d{10,11}", ErrorMessage = "Home Phone is not valid")]
        [ATSStringLength(15)]
        public string HomePhone { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_MOBILE_PHONE)]
        //[StringLength(11)]
        //[RegularExpression(@"\d{10,11}", ErrorMessage = "Mobile Phone is not valid")]
        [ATSStringLength(15)]
        public string MobilePhone { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_PAGER)]
        //[StringLength(20)]
        //[RegularExpression(@"\d{10,11}", ErrorMessage = "pager  is not valid")]
        [ATSStringLength(15)]
        public string Pager { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_WORK_EMAIL)]
        [ATSStringLength(50)]
        [ATSRegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "{0}")]
        public string WorkEmail { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_MISDEMEANOR)]
        //[Required(ErrorMessage = "Is a required Field")]
        public Boolean Misdemeanor { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_MISDEMEANOR_EXPLAIN)]
        //[StringLength(256, ErrorMessage = "Too Long")]
        [ATSStringLength(100)]
        public string MisdemeanorExplain { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_MILITARY_SERVICE)]
        //[Required(ErrorMessage = "Is a required Field")]
        public Boolean MilitaryService { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_MILITARY_TYPE_DISCHARGE)]
        //[StringLength(256, ErrorMessage = "Too Long")]
      
        public string MilitaryTypeDischarge { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_EMERGANCY_CONTACT1)]
        //[StringLength(50, ErrorMessage = "Contact 1 Too Long")]
        [ATSStringLength(15)]
        public string EmergencyContact1 { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_EMERGANCY_CONTACT2)]
        //[StringLength(50, ErrorMessage = "Contact 1 Too Long")]
        [ATSStringLength(15)]
        public string EmergencyContact2 { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_EMERGANCY_CONTACT1_PHONE)]
        //[RegularExpression(@"\d{10,11}", ErrorMessage = "Alternate/Emergency Contact 1 Phone is not valid")]
        [ATSStringLength(15)]
        public string EmergencyContact1Phone { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_EMERGANCY_CONTACT2_PHONE)]
        //[RegularExpression(@"\d{10,11}", ErrorMessage = "Alternate/Emegency Contact 2 Phone is not valid")]
        [ATSStringLength(15)]
        public string EmergencyContact2Phone { get; set; }

        //[Display(Name = "Client Id")]
        public Guid ClientId { get; set; }

        //[Display(Name = "Client Name")]
        public string ClientIdName { get; set; }

        public string ProfileImage { get; set; }

        public override ATSPrivilage Privilage
        {
            get { return ATSPrivilage.User; }
        }
        //CR-2
        //public int OrganizationID { get; set; }
    }

    
}
