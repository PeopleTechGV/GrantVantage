using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEExwcutiveSummaryConst = ATS.BusinessEntity.Common.ExecutiveSummaryConstant;
using System.ComponentModel.DataAnnotations;
using ATS.BusinessEntity.Attributes;


namespace ATS.BusinessEntity
{
    public class ExecutiveSummary : BaseEntity
    {
        public Guid ExecutiveSummaryId { get; set; }
       
        public Guid ProfileId { get; set; }

        public Guid UserId { get; set; }

        [Display(Name= BEExwcutiveSummaryConst.FRM_EXECUTIVESUMMARY)]       
        public String ExecutiveSummaryDetails { get; set; }

    }
}
