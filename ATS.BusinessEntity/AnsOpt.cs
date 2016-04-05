using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using ATS.BusinessEntity.Attributes;
using BECommonConst = ATS.BusinessEntity.Common.CommonConstant;
using BELabel = ATS.BusinessEntity.Common.AnsOpt;

namespace ATS.BusinessEntity
{
    public class AnsOpt:ClientBaseEntity
    {
        public Guid AnsOptId { get; set; }

        public Guid QueId { get; set; }
        public Guid QueDivisionId { get; set; }
        public int QueDataType { get; set; }
        [ATSStringLength(500)]
        [ATSRequired(ErrorMessage = "{0}")]
        [Display(Name = BELabel.ANSOPT_ANS)]
        public string DefaultName { get; set; }

        [Display(Name = BELabel.ANSOPT_VALUE)]
        [ATSRequired(ErrorMessage = "{0}")]
        [Range(1, 100, ErrorMessage = "{0}")]
        public int Weight { get; set; }

        public bool IsActive { get; set; }

        public int Order { get; set; }

        public override ATSPrivilage Privilage
        {
            get
            {
                return ATSPrivilage.Question;
            }

        }
        public string LocalName { get; set; }

        public List<EntityLanguage> AnsOptEntityLanguageList { get; set; }

        
    }
}
