using System;

namespace ATS.BusinessEntity
{   
    public class ApplicationHistory : BaseEntity
    {
        public Guid ApplicationHistoryId { get; set; }
        public Guid ApplicationId { get; set; }
        public int VacancyOfferStatusId { get; set; }
        public string VacancyOfferStatusName { get; set; }//Status name
        public string Description { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
