using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATS.BusinessEntity.Attributes;
using BECommonConst = ATS.BusinessEntity.Common.CommonConstant;
using System.ComponentModel.DataAnnotations;

namespace ATS.BusinessEntity
{
    public class SkillType : ClientBaseEntity
    {
        public Guid SkillTypeId { get; set; }

        [ATSRequired(ErrorMessage = "{0}")]
        [Display(Name = BECommonConst.DEFAULT_NAME)]
        public String DefaultName { get; set; }
        public List<EntityLanguage> SkillTypeEntityLanguage { get; set; }
        public List<RecordExists> RecordExistsCount { get; set; }
        public override ATSPrivilage Privilage
        {
            get
            {
                return ATSPrivilage.SkillType;
            }

        }

        public string LocalName { get; set; }
    }
}
