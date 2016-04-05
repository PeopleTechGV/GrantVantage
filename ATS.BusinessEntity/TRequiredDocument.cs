using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATS.BusinessEntity.SkeVacTemp;

namespace ATS.BusinessEntity
{
    public class TRequiredDocument : SkeReqDoc
    {
        public Guid TRequiredDocumentId { get { return PrimaryKeyId; } set { PrimaryKeyId = value; } }

        public Guid TVacRndId { get; set; }

        public RequiredDocument objRequiredDocument { get { return ReturnRequiredDocument(); } }

        private RequiredDocument ReturnRequiredDocument()
        {
            RequiredDocument _RequiredDocument = new RequiredDocument();
            _RequiredDocument.DocumentTypeId = this.DocumentTypeId;
            return _RequiredDocument;
        }
    }
}
