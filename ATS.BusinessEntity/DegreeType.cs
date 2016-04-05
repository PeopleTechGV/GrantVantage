using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATS.BusinessEntity.Attributes;
using BECommonConst = ATS.BusinessEntity.Common.CommonConstant;
using System.ComponentModel.DataAnnotations;
using BECommon = ATS.BusinessEntity.Common;
namespace ATS.BusinessEntity
{
    public class DegreeType : ClientBaseEntity
    {
        public Guid DegreeTypeId { get; set; }




        [ATSRequired(ErrorMessage = "{0}")]
        [Display(Name = BECommonConst.DEFAULT_NAME)]
        public String DefaultName { get; set; }
        public int Priority { get; set; }
        public List<EntityLanguage> DegreeTypeEntityLanguage { get; set; }
        public List<RecordExists> RecordExistsCount { get; set; }
        public override ATSPrivilage Privilage
        {
            get
            {
                return ATSPrivilage.DegreeType;
            }

        }

        public string LocalName { get; set; }
        String DisplayName { get; set; }
        String ImagePath { get; set; }
        String ToolTip { get; set; } 
    }
}
