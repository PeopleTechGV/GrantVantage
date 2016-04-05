using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BECommonConst = ATS.BusinessEntity.Common.CommonConstant;
using BERoundType = ATS.BusinessEntity.Common.RoundType;
using ATS.BusinessEntity.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ATS.BusinessEntity
{
    public class RndType : ClientBaseEntity
    {
        public Guid RndTypeId { get; set; }

        [ATSRequired(ErrorMessage = "{0}")]
        [Display(Name = BERoundType.ROUND_NAME)]
        public String DefaultName { get; set; }

        [ATSRequired(ErrorMessage = "{0}")]
        [Display(Name = BERoundType.LST_SHOWTOCANDIDATE)]
        public String ShowToCandidate { get; set; }

        public bool IsActive { get; set; }

        public List<EntityLanguage> RndTypeEntityLanguage { get; set; }

        public override ATSPrivilage Privilage
        {
            get
            {
                return ATSPrivilage.RndType;
            }

        }

        [Display(Name = BECommonConst.FRM_APP_ROUND_TYPE)]
        [ATSRequired(ErrorMessage = "{0}")]
        public Guid RoundAttributesId { get; set; }

        public String RoundAttributesName { get; set; }

        public List<BindEnumDropDown> QuestionTypeList { get; set; }

        public List<BindEnumDropDown> AllQuestionTypeList { get; set; }

        public BindEnumDropDownMaster objBindEnumDropDown { get; set; }

    }
    public class BindEnumDropDownMaster
    {
        public int[] enumId { get; set; }
    }
    public class BindEnumDropDown
    {
        public int Value { get; set; }
        public String Text { get; set; }
    }

}
