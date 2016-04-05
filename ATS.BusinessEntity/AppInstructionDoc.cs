using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity
{
    public class AppInstructionDoc : BaseEntity
    {
        public Guid AppInstructionDocId { get; set; }

        public Guid VacancyId { get; set; }

        public string FileName { get; set; }

        public string NewFileName { get; set; }

        public string Path { get; set; }
    }
}
