using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using ATS.BusinessEntity.Attributes;
using BEProfileConst = ATS.BusinessEntity.Common.ProfileConstant;
namespace ATS.BusinessEntity
{
   public class Profile:BaseEntity
    {
        public Guid ProfileId { get; set; }
        public Guid LanguageId { get; set; }

        public Guid UserId { get; set; }

        public Guid ClientId { get; set; }

        [ATSRequired(ErrorMessage = "{0}")]
       [Display(Name=BEProfileConst.FRM_PRO_PROFILENAME)]        
       public string ProfileName { get; set; }

        public string Objectives { get; set; }

        public string Hobbies { get; set; }

        public string Category { get; set; }

        public string SubCategory { get; set; }

        public decimal CurrentSalary { get; set; }

        public decimal ExpectedSalary { get; set; }

        public Guid AtsResumeId { get; set; }

        public bool IsDefault { get; set; }

       [Display(Name = "Document Description (optional)")]
        public String CandidateDescription { get; set; }

       [Display(Name = "Document Type")]
        public Guid DocumentTypeId { get; set; }

        public string ExtensionTypes { get; set; }

        public Int32 DocCategoryType { get; set; }

        public List<ATSResume> lsrAtsResume { get; set; }
        public bool IsManual { get; set; }
   }
}
