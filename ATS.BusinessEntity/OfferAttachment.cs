using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity
{
    public class OfferAttachment : BaseEntity
    {
        public Guid OfferAttachmentId { get; set; }

        public Guid OfferTemplateId { get; set; }

        public string FileName { get; set; }

        public string NewFileName { get; set; }

        public string Path { get; set; }

        public bool IsMandatory { get; set; }
    }
}
