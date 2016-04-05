using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using BEClient = ATS.BusinessEntity;
using BESrp = ATS.BusinessEntity.SRPEntity;
using BECommon = ATS.BusinessEntity.Common;
using ATS.BusinessEntity.Attributes;
namespace ATS.WebUi.Models
{


    public class LogOnModel
    {
        [Required(ErrorMessage = "Email Address required.")]
        [Display(Name = BECommon.LoginConstant.FRM_EMAIL_ADDRESS)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password required.")]
        [DataType(DataType.Password)]
        //[ATSStringLength(15, MinimumLength = 8)]
        [Display(Name = BECommon.LoginConstant.FRM_PASSWORD)]
        public string Password { get; set; }

        public Guid VacancyId { get; set; }
    }

    public class SignUpModel
    {
        public Guid ContactId { get; set; }
        public Guid UserId { get; set; }

        public Guid ClientId { get; set; }


        [Display(Name = BECommon.PersonalInfoConstant.FRM_PRF_FIRST_NAME)]
        [ATSRequired(ErrorMessage = "{0}")]
        public string FirstName { get; set; }

        //[Required(ErrorMessage = "Last Name is required.")]
        [Display(Name = BECommon.PersonalInfoConstant.FRM_PRF_LAST_NAME)]
        [ATSRequired(ErrorMessage = "{0}")]
        public string LastName { get; set; }

        [ATSRequired(ErrorMessage = "{0}")]
        [Display(Name = BECommon.LoginConstant.FRM_EMAIL_ADDRESS)]
        [ATSRegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string UserName { get; set; }

        [ATSRequired(ErrorMessage = "{0}")]
        [ATSStringLength(15, MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = BECommon.LoginConstant.FRM_PASSWORD)]

        public string Password { get; set; }

        [Required(ErrorMessage = "Comfirm Password required.")]
        [Compare("Password", ErrorMessage = "Password not matched.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "New Password")]
        [Required(ErrorMessage = "New Password required.")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirm Password required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("NewPassword", ErrorMessage = "Password not matched.")]
        public string ConfirmNewPassword { get; set; }

    }
}
