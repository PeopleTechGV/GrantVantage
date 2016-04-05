using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BECommonConst = ATS.BusinessEntity.Common.ReferencesConstant;
using System.ComponentModel.DataAnnotations;
using ATS.BusinessEntity.Attributes;
namespace ATS.BusinessEntity
{
   public class References:BaseEntity
    {
        public Guid ReferencesId { get; set; }

        public Guid ProfileId { get; set; }

        public Guid UserId { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_REFERENCE_NAME)]
        //[StringLength(100, ErrorMessage = "Reference Name is too long")]
        //[Required(ErrorMessage = "Reference Name is a required field ")]
        [ATSStringLength(100), ATSRequired(ErrorMessage = "{0}")]
        public string ReferenceName { get; set; }


        [Display(Name = BECommonConst.FRM_PRF_RELATIONSHIP)]
        //[StringLength(50, ErrorMessage = "Relationship is too long")]
        //[Required(ErrorMessage = "Relationship is a required field")]
        [ATSStringLength(100)]
        public string Relationship { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_REFERENCE_PHONE)]
        //[StringLength(11)]
        //[RegularExpression(@"\d{10,11}", ErrorMessage = "Reference Phone is not valid")]

        [ATSStringLength(100)]
        public string ReferencePhone { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_REFERENCE_EMAIL)]
        //[StringLength(100, ErrorMessage = "Reference Email too long")]
        //[RegularExpression(@"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+(?:[A-Z]{2}|com|org|net|edu|gov|mil|biz|info|mobi|name|aero|asia|jobs|museum|co.in|in)\b", ErrorMessage = "Invalid Email")]
        [ATSStringLength(100)]
        [ATSRegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "{0}")]
        public string ReferenceEmail { get; set; }

    }
}
