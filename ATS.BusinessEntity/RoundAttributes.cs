using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BECommonConst = ATS.BusinessEntity.Common.CommonConstant;
using ATS.BusinessEntity.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ATS.BusinessEntity
{
    public class RoundAttributes : BaseEntity
    {
        public Guid RoundAttributesId { get; set; }

        public string RoundAttributesName { get; set; }

        public string QuestionType { get; set; }

        public int RoundAttributesNo { get; set; }

        public bool AnsSubmitted { get; set; }
    }
}