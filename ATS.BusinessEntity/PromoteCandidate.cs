using ATS.BusinessEntity.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;


namespace ATS.BusinessEntity
{
    public class PromoteCandidate : BaseEntity
    {
        public Guid PromoteCandidateId { get; set; }

        public Guid ApplicationId { get; set; }

        public Guid VacRndId { get; set; }

        public Boolean IsPromoted { get; set; }

        public Boolean IsActive { get; set; }
    }
}
