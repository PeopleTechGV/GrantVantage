using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BEClient = ATS.BusinessEntity;

namespace ATS.WebUi.Areas.Candidate.Models
{
    public class ResumeList
    {
        public BEClient.ATSResume objATSResume { get; set; }

        public BEClient.ATSResume objATSCoverLetter { get; set; }

        public List<BEClient.ATSResume> ListATSResume { get; set; }

        public Guid VacancyId { get; set; }

        public List<BEClient.DocumentType> DocumentTypeList { get; set; }
    }
}