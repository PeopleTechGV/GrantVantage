using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;

namespace ATS.BusinessEntity
{
    public class QuestionDetails
    {
        public VacancyRound VacRnd { get; set; }
        public VacancyQuestion VacQue { get; set; }
        public Question Question { get; set; }
        public AppAnswer AppAnswer { get; set; }
    }
}
