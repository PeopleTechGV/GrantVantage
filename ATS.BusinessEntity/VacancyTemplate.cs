using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity
{
    public class VacancyTemplate
    {
        public TVacReviewMember objTReviewers { get; set; }

        public List<TVacReviewMember> objTReviewerslst { get; set; }

        public ATS.BusinessEntity.SRPEntity.DynamicSRP<List<TVacQue>> objTVacancyQuestionlst { get; set; }

        public TVacancyRound objTVacancyRound { get; set; }

        public Guid TVacId { get; set; }

        public int RndCnt { get; set; }

    }
}
