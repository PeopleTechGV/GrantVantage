using ATS.BusinessEntity.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity.SkeVacTemp
{
    public class SkeVacRM : ClientBaseEntity
    {
        public Guid PrimaryKeyId { get; protected set; }
        public Guid UserId { get; set; }

        public string UserName { get; set; }
        public string FullName { get; set; }


        public string Title { get; set; }

        [Range(1, 100)]
        [ATSRegularExpression(@"^\d+$", ErrorMessage = "{0}")]
        public int Weight { get; set; }

        public bool CanPromote { get; set; }

        //Consider this as VacRndId in AssignCandidate feature
        public Guid RndTypeId { get; set; }

        public string CanPromoteText
        {
            get
            {
                if (CanPromote)
                    return "Yes";
                else
                    return "No";

            }

        }
        public string DivisionLocalName { get; set; }

        public bool CanMakeOffers { get; set; }
        public string CanMakeOffersText
        {
            get
            {
                if (CanMakeOffers != null && (bool)CanMakeOffers)
                    return "Yes";
                else
                    return "No";

            }

        }
        public bool CanEditOffers { get; set; }
        public string CanEditOffersText
        {
            get
            {
                if (CanEditOffers != null && (bool)CanEditOffers)
                    return "Yes";
                else
                    return "No";

            }

        }

        public bool IsAssignedReviewer { get; set; }

        public bool IsAssinedReviewerForOldApp { get; set;}

        public int AssignedCandidateCount { get; set; }

        public int RndAttribute { get; set; }

        public string JobTitle { get; set; }
        public string CandidateName { get; set; }

        public override ATSPrivilage Privilage
        {
            get { return ATSPrivilage.Vacancy; }
        }
    }
}
