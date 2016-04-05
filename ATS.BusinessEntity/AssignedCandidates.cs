using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
namespace ATS.BusinessEntity
{
    public class AssignedCandidates
    {
           
        public Guid ApplicationId { get; set; }
        public Guid VacRndId { get; set; }
        public Int32 RndAttribute { get; set; }
        public string Jobtitle { get; set; }
        public string CandidateName { get; set; }
        public string RndName { get; set; }
        public List<BEClient.Reviewers> objReviewers { get; set; }
        public List<BEClient.VacancyRound> objVacancyRounds { get; set; }
        public Guid CurrentVacRoundId { get; set; }
        
        public String CurrentName { get; set; }

        public string RoundsToAssign { get; set; }
        public string RoundsName { get; set; }
    
    }
}
