using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATS.BusinessEntity.Common;
using System.Web.Mvc;
using ATS.BusinessEntity.Attributes;

namespace ATS.BusinessEntity
{
    public class CandidateApplications 
    {
        public Vacancy objVacancy { get; set; }

        public Guid ApplicationId { get; set; }

        public String ApplicationStatus { get; set; }


        public ATSResume objAtsResume { get; set; }

        public DateTime AppliedOn { get; set; }

        public string AppliedOnText
        {
            get
            {
                if (AppliedOn != DateTime.MinValue)
                    return AppliedOn.ToString("MM'/'dd'/'yyyy");
                else
                    return "";
            }

        }

        public string LocationText { get; set; }
        [ATSStringLength(300)]
        public string ShowToCandidate { get; set; }

        public int TotalOffers { get; set; }

        public int TotalInterviews { get; set; }

        public int TotalAppQues { get; set; }

        public int AppStatusAttrNo { get; set; }

        public int ReqDocCount { get; set; }
        
        public int CanSurveyQueCnt { get; set; }

        public Guid VacRndId { get; set; }
        //CR-7
        public string Questioncounterperc { get; set; }
    }

   
}
