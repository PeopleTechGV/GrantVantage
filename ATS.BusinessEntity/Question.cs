using ATS.BusinessEntity.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using BELabel = ATS.BusinessEntity.Common.VacancyConstant;
using BECommonConst = ATS.BusinessEntity.Common.CommonConstant;
using BEQuestionLibraryLabel = ATS.BusinessEntity.Common.QuestinoLibraryConstant;

namespace ATS.BusinessEntity
{
    public class Question : ClientBaseEntity
    {
        public Guid QueId { get; set; }

        [ATSStringLength(500)]
        [Display(Name = BEQuestionLibraryLabel.FRM_QUE)]
        [ATSRequired(ErrorMessage = "{0}")]
        public string DefaultName { get; set; }

        [Display(Name = BECommonConst.DEFAULT_WEIGHT)]
        [ATSRequired(ErrorMessage = "{0}")]
        [Range(1, 100, ErrorMessage = "{0}")]
        public int Weight { get; set; }

        [Display(Name = BEQuestionLibraryLabel.FRM_QUE_TYPE)]
        [ATSRequired(ErrorMessage = "{0}")]
        public string QueType { get; set; }

        [Display(Name = BEQuestionLibraryLabel.FRM_QUE_DATA_TYPE)]
        [ATSRequired(ErrorMessage = "{0}")]
        public int QueDataType { get; set; }

        [Display(Name = BEQuestionLibraryLabel.YESNO_YES)]
        [Range(1, 100, ErrorMessage = "{0}")]
        public int ValueForYes { get; set; }

        [Display(Name = BEQuestionLibraryLabel.YESNO_NO)]
        [Range(0, 100, ErrorMessage = "{0}")]
        public int ValueForNo { get; set; }

        public Guid QueCatId { get; set; }

        public string QueText { get; set; }

        [Display(Name = BEQuestionLibraryLabel.FRM_QUE_PRIMARY_DIVISION)]
        [ATSRequired(ErrorMessage = "{0}")]
        public Guid DivisionId { get; set; }
        public bool IsActive { get; set; }

        public List<EntityLanguage> EntityLanguageList { get; set; }
        public override ATSPrivilage Privilage
        {
            get
            {
                return ATSPrivilage.Question;
            }

        }
        public string LocalName { get; set; }
        public string QueCatLocalName { get; set; }
        public List<QueCat> QueCatList { get; set; }
        public List<Question> QueList { get; set; }
        public List<AnsOpt> AnsOptList { get; set; }
        public string QueDataTypeText { get; set; }
        public int Order { get; set; }
        public AnsOpt objAnsOpt { get; set; }
        public string LocalNameAndDatatype { get; set; }


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

        public bool IsSelfEval { get; set; }
        //CR-9
         [Display(Name = BEQuestionLibraryLabel.FRM_INTEGRATION_KEY)]
         [ATSRequired(ErrorMessage = "{0}")]
        public string IntegrationKey { get; set; }
    }
}
