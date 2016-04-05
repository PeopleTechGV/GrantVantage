using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity
{
  public  class ConductInterviewDetails
    {
      public String CandidateName { get; set; }

      public String Interviewer { get; set; }

      public String InterviewDate { get; set; }

      public float TotalScore { get; set; }

      public Int32 RoundWeight { get; set; }

      public float RoundScore { get; set; }

      public String RoundName { get; set; }

      public String VacancyName { get; set; }

      public DateTime VacancyPostedDate { get; set; }

      public Guid ApplicationId { get; set; }

      public String ReturnUrl { get; set; }

    }
}
