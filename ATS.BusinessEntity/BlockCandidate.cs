using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity
{
    public class BlockCandidate:BaseEntity
    {
        public Guid BlockCandidateId { get; set;}

        public Guid UserId { get; set; }


    }
}
