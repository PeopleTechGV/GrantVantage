using ATS.BusinessEntity.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity
{
    public class CandidateHistory : BaseEntity
    {
        public Guid CandidateHistoryId { get; set; }

        public Guid ApplicationId { get; set; }

        public string Description { get; set; }
        public Guid CandidateId { get; set; }
        public Guid EmployeerId { get; set; }
        public Guid RoundId { get; set; }

        public string Area { get; set; }

        public string CreatedOnText { get; set; }
    }
}
