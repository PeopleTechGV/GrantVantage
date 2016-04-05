using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity
{
    public class TAppInstructionDocs : BaseEntity
    {
        public Guid TAppInstructionDocId { get; set; }

        public Guid TVacId { get; set; }

        public string FileName { get; set; }

        public string NewFileName { get; set; }

        public string Path { get; set; }
    }
}
