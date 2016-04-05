using ATS.BusinessEntity.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity
{
    public class EmailContent : BaseEntity
    {
        public string EmailDescription { get; set; }

        public string EmailSubject { get; set; }

        public string DestinationAddress { get; set; }

        public string FullName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string VacancyName { get; set; }

        public string OfferStatus { get; set; }

        public string CreatedBy { get; set; }

        public string CreatedOn { get; set; }

        public string ModifiedBy { get; set; }

        public string ModifiedOn { get; set; }

        public string PlacementType { get; set; }

        public string JobType { get; set; }

        public string StartDate { get; set; }

        public string PlacementLocation { get; set; }

        public string SalaryType { get; set; }

        public string SalaryOffered { get; set; }

        public string BonusDescription { get; set; }

        public string BonusPotential { get; set; }

        public string BonusCap { get; set; }

        public string CommissionDescription { get; set; }

        public string CommissionPotential { get; set; }

        public string CommissionCap { get; set; }

        public string CandidateNoticePeriod { get; set; }

        public string CandidateNoticePeriodIn { get; set; }

        public string CompanyNoticePeriod { get; set; }

        public string CompanyNoticePeriodIn { get; set; }

        public string CompanyName { get; set; }

        public string ManagerName { get; set; }

        public string CandidateSurvey { get; set; }
    }
}
