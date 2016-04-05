using ATS.BusinessEntity.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using BELabel = ATS.BusinessEntity.Common.VacancyConstant;
namespace ATS.BusinessEntity.SkeVacTemp
{
    public class SkeVacRnd : ClientBaseEntity
    {
        public Guid PrimaryKeyId { get; protected set; }
        [Display(Name = BELabel.FRM_VACRND_ReviewRound)]
        [ATSRequired(ErrorMessage = "{0}")]
        public Guid RndTypeId { get; set; }

        public string RoundType { get; set; }

        [Display(Name = BELabel.FRM_VACRND_RndWeight)]
        [ATSRequired(ErrorMessage = "{0}")]
        [Range(1, 100, ErrorMessage = "{0}")]
        public int RoundWeight { get; set; }

        [Display(Name = BELabel.FRM_VACRND_AssignCandToRev)]
        public int AssignCandiadteToReviewerId { get; set; }

        public string AssignCandiadteToReviewer { get; set; }

        [Display(Name = BELabel.FRM_VACRND_ReqRev)]
        [ATSRequired(ErrorMessage = "{0}")]
        [Range(1, 20, ErrorMessage = "{0}")]
        public int ReqReviewer { get; set; }

        [Display(Name = BELabel.FRM_VACRND_PromoteCan)]
        public bool PromoteCandidate { get; set; }

        [Display(Name = BELabel.FRM_VACRND_Promothersolscore)]
        [Range(1, 100, ErrorMessage = "{0}")]
        public int PromotionThresoldScore { get; set; }

        [Display(Name = BELabel.FRM_VACRND_AssignCanBatches)]
        [Range(1, 20, ErrorMessage = "{0}")]
        public int AssignCandidateBatches { get; set; }

        public int ReviewersCount { get; set; }

        public int QueCount { get; set; }

        public int RequiredDocumentCount { get; set; }
       
        public int RndOrder { get; set; } //Round sequence (Not used this column)
        
        public bool IsRndActive { get; set; } //Is Round Active

        public int RoundAttributeNo { get; set; }

        public Guid OfferTemplateId { get; set; }

        public override ATSPrivilage Privilage
        {
            get { return ATSPrivilage.Vacancy; }
        }
    }
}
