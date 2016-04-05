using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATS.BusinessEntity.SkeVacTemp;

namespace ATS.BusinessEntity
{
    public class TVacancyRound : SkeVacRnd
    {
        public Guid TVacRndId { get { return PrimaryKeyId; } set { PrimaryKeyId = value; } }

        public Guid TVacId { get; set; }

        public VacancyRound ObjVacancyRound { get { return ReturnVacancyRound(); } }

        private VacancyRound ReturnVacancyRound()
        {
            VacancyRound _VacRnd = new VacancyRound();
            _VacRnd.RndTypeId = this.RndTypeId;
            _VacRnd.RoundType = this.RoundType;
            _VacRnd.RoundWeight = this.RoundWeight;
            _VacRnd.AssignCandiadteToReviewerId = this.AssignCandiadteToReviewerId;
            _VacRnd.ReqReviewer = this.ReqReviewer;
            _VacRnd.PromoteCandidate = this.PromoteCandidate;
            _VacRnd.PromotionThresoldScore = this.PromotionThresoldScore;
            _VacRnd.AssignCandidateBatches = this.AssignCandidateBatches;
            _VacRnd.QueCount = this.QueCount;
            _VacRnd.TVacRndId = this.PrimaryKeyId;
            _VacRnd.OfferTemplateId = this.OfferTemplateId;
            return _VacRnd;
        }
    }
}
 