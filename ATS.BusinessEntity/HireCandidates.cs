using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity
{
    public class HireCandidates
    {
        public Guid ApplicationId { get; set; }
        public Guid CandidateId { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string PersonalEmail { get; set; }

        public string Division { get; set; }

        public string JobType { get; set; }//FT/PT

        public string StreetAddress { get; set; }

        public string City { get; set; }
        
        public string State { get; set; }

        public string Zip { get; set; }

        public string HomePhone { get; set; }

        public string JobLocation { get; set; }

        public string EmployementType { get; set; }

        public string PositionType { get; set; }

        public string PositionOwner { get; set; }
        
        public Decimal PositionID { get; set; }

        public string PayType { get; set; }//Permanent/Contract/Temp

        public string Salary { get; set; }//Annual Salary or Hourly Rate

        public DateTime HiringDate { get; set; }

        public Guid OnboardingManagerID { get; set; }

        public string OnboardingManagerFirstName { get; set; }

        public string OnboardingManagerLastName { get; set; }


    }
}
