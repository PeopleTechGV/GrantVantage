using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BECommonConst = ATS.BusinessEntity.Common.SkillAndQualificationConstant;
using System.ComponentModel.DataAnnotations;
using ATS.BusinessEntity.Attributes;
namespace ATS.BusinessEntity
{
    public class Skills:BaseEntity
    {
        public Guid SkillsId { get; set; }

        public Guid ProfileId { get; set; }

        public Guid UserId { get; set; }


         [Display(Name = BECommonConst.FRM_PRF_SKILL_AND_QUALIFICATION)]
         [ATSStringLength(100), ATSRequired( ErrorMessage="{0}")]
         public string SkillAndQualification { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_SKILL_TYPE)]
        //[StringLength(100, ErrorMessage = "Skill And Qualification too long")]
        [ATSRequired(ErrorMessage = "{0}")]
         public Guid SkillType { get; set; }

       [Display(Name = BECommonConst.FRM_PRF_DESCRIPTION)]
       public string Description { get; set; }

       [Display(Name = BECommonConst.FRM_PRF_PROFICIENCY)]      
        public int Proficiency { get; set; }

       [Display(Name = BECommonConst.FRM_PRF_LEVEL)]
        public int Level { get; set; }

 [Display(Name = BECommonConst.FRM_PRF_LASTUSED)]
        public string LastUsed { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_SKILLEXPERIENCE)]
       public int Experience { get; set; }



    }
}
