using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BECommonConst = ATS.BusinessEntity.Common.RecordOfEmployementConstant;
using System.ComponentModel.DataAnnotations;
using ATS.BusinessEntity.Attributes;
namespace ATS.BusinessEntity
{
    public class Project:BaseEntity
    {
        public Guid ProjectId { get; set; }

        public Guid EmploymentHistoryId { get; set; }

        public Guid UserId { get; set; }

        public Guid ProfileId { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_PROJECT_NAME)]
        [ATSStringLength(100), ATSRequired( ErrorMessage="{0}")]
        public string ProjectName { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_TEAM_SIZE)]
        public int TeamSize { get; set; }

        [Display(Name = BECommonConst.FRM_PRF_DESCRIPTION)]
        public string Description { get; set; }
    }
}
