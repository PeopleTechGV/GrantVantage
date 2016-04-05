using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity
{
    public class CandidateEmployee
    {
        public Guid UserId { get; set; }

        public Guid ApplicationId { get; set;}

        public String Username { get; set; }

        public String ManagerFirstName { get; set; }

        public String ManagerLastName { get; set; }

        public String CandidateFirstName { get; set; }

        public String CandidateLastName { get; set; }

        public Decimal RndScore { get; set; }
   
    }
}
