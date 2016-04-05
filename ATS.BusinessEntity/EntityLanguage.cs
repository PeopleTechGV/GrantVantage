using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using ATS.BusinessEntity.Attributes;

namespace ATS.BusinessEntity
{
    public class EntityLanguage:BaseEntity
    {
        public Guid EntityLanguageId { get; set; }
        public ATSPrivilage EntityName { get; set; }
        public Guid RecordId{ get; set; }
        public Guid LanguageId { get; set; }

      [ATSRequired( ErrorMessage="Local Name Required")]
        public String LocalName { get; set; }
      [ATSRequired(ErrorMessage = "Status For Candidate Required")]
        public String ShowToCandidate { get; set; }
    }
}
