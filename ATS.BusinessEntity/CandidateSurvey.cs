using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity
{
    public class CandidateSurvey
    {
        public Guid VacRndId { get; set; }

        public Guid RndTypeId { get; set; }

        public Int32 RndOrder { get; set; }

        public String LocalName { get; set; }

        public bool IsPending { get; set; }

        public Int32 QueCount { get; set; }
        public DateTime SubmittedDate { get; set; }
    }
}
