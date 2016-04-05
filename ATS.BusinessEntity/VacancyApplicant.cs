using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATS.BusinessEntity.Common;
using System.Web.Mvc;

namespace ATS.BusinessEntity
{
    public class VacancyApplicant : IGrid
    {
        public Guid ApplicationId { get; set; }

        public String ApplicantName { get; set; }

        public String ApplicantionStatus { get; set; }

        //CR-34In Manager view “My applicants” page. Renamed Applicant Name to Applicant• Added Requested and Funded Field• Changed Applicant’s Name
        public Decimal RequestedFund { get; set; }
        public Decimal FundedAmount { get; set; }
        //CR-34 END

        public String DisplayStatusName { get; set; }

        public DateTime AppliedOn { get; set; }

        public Guid VacancyId { get; set; }

        public Guid CoverLetterId { get; set; }

        public String JobTitle { get; set; }

        public Guid ResumeId { get; set; }

        public String CoverLetterName { get; set; }

        public String ResumeName { get; set; }

        public String ProfileName { get; set; }

        public String NewFileName { get; set; }

        public String NewCoverLetterName { get; set; }

        public Boolean IsNotViewable { get; set; }

        public Guid VacancyDivisionId { get; set; }

        public int TotalCount { get; set; }

        public float Score { get; set; }

        public ApplicationReviewProcess objApplicationReviewProcess { get; set; }

        public VacancyRound objVacancyRound { get; set; }

        public Int64 PublicCode { get; set; }

        public string VacApplicationLinkList { get; set; }
        public string ProfileImage { get; set; }
    }
}
