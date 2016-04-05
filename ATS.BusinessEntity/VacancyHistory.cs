using ATS.BusinessEntity.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity
{
    public class VacancyHistory : BaseEntity
    {
        public long VacancyHistoryId { get; set; }
        public Guid VacancyId { get; set; }
        public string VacancyStatus { get; set; }//Status name
        public Guid VacancyStatusId { get; set; }
        public string Description { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int DaysOpen { get; set; }
    }
}
