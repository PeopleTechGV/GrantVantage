using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEObjectiveConst = ATS.BusinessEntity.Common.ObjectiveConstant;
using System.ComponentModel.DataAnnotations;
using ATS.BusinessEntity.Attributes;

namespace ATS.BusinessEntity
{
    public class Objective:BaseEntity
    {
        public Guid ObjectiveId { get; set; }

        public Guid ProfileId { get; set; }

        public Guid UserId { get; set; }
        [Display(Name=BEObjectiveConst.FRM_OBJECTIVEDETAILS)]
        public String ObjectiveDetails { get; set; }
    }
}
