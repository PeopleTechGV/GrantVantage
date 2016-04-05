using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATS.BusinessEntity;

namespace ATS.BusinessEntity
{
    public class ApplicationReviewProcess
    {
        public Reviewers objReviewers { get; set; }

        public ATS.BusinessEntity.SRPEntity.DynamicSRP<List<Reviewers>> objReviewerslst { get; set; }

        public ATS.BusinessEntity.SRPEntity.DynamicSRP<List<VacancyQuestion>> objVacancyQuestionlst { get; set; }

        public VacancyRound objVacancyRound { get; set; }

        public Guid VacancyId { get; set; }

        public List<ApplicationBasedStatus> ApplicationBasedStatusList { get; set; }

        public bool IsApplicationDecline { get; set; }

        public Guid ATSResumeId { get; set; }

        public String ATSResumeNewName { get; set; }

        public Guid ATSCoverLetterId { get; set; }

        public String ATSCoverLetterNewName { get; set; }

        public Guid AppId { get; set; }
        public List<ApplicationDocuments> ObjApplicationDocuments { get; set; }
    }
}
