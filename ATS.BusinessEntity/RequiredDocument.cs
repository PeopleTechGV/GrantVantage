using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATS.BusinessEntity.SkeVacTemp;

namespace ATS.BusinessEntity
{
    public class RequiredDocument : SkeReqDoc
    {
        public Guid RequiredDocumentId { get; set; }

        public Guid VacRndId { get; set; }

        public bool IsResume { get; set; }

        public bool IsOptional { get; set; }

        public Guid ATSResumeId { get; set; }

        public List<ATSResume> ATSResumeList { get; set; }

        public override ATSPrivilage Privilage
        {
            get
            {
                return ATSPrivilage.Vacancy;
            }
        }
        //Only used for privileges Authorization
        public Guid DivisionId { get; set; } //This is Vacancy's DivisionId
    }
}
