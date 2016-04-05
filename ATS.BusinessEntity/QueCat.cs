using ATS.BusinessEntity.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using BESrp = ATS.BusinessEntity.SRPEntity;
using BECommonConst = ATS.BusinessEntity.Common.CommonConstant;
using BELanCat = ATS.BusinessEntity.Common.QueCategory;

namespace ATS.BusinessEntity
{
    public class QueCat : ClientBaseEntity
    {
        public Guid QueCatId { get; set; }

        [ATSStringLength(500)]
        [Display(Name = BELanCat.FRM_CATEGORY)]
        [ATSRequired(ErrorMessage = "{0}")]
        public String DefaultName { get; set; }

        [Display(Name = BECommonConst.DEFAULT_WEIGHT)]
        [ATSRequired(ErrorMessage = "{0}")]
        [Range(1, 100, ErrorMessage = "{0}")]
        public int Weight { get; set; }

        public bool IsActive { get; set; }


        public List<EntityLanguage> EntityLanguageList { get; set; }
        public override ATSPrivilage Privilage
        {
            get
            {
                return ATSPrivilage.Question;
            }

        }
        public int Order { get; set; }

        public string LocalName { get; set; }

        public List<Question> QueList { get; set; }

        public List<BESrp.UserPermissionWithScope> UserPermissionWithScope { get; set; }

        public int MaxOrder { get; set; }
        public int MinOrder { get; set; }
        float _score;
        public object Score
        {
            get
            {
                return Common.CommonFunction.GetScoreFormat(this._score);
            }
            set
            {

                float.TryParse(value.ToString(), out _score);
            }
        }
        [Display(Name = "Round Attribute")]
        public Guid RoundAttributeId { get; set; }

        public string RoundAttributeName { get; set; }

        public int RoundAttributeNo { get; set; }
        public int QueCount { get; set; }
    }
}

