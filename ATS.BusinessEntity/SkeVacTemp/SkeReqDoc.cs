using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using ATS.BusinessEntity.Attributes;
using BEDocTypeConstant = ATS.BusinessEntity.Common.DocumentTypeConstant;

namespace ATS.BusinessEntity.SkeVacTemp
{
    public class SkeReqDoc : ClientBaseEntity
    {
        public Guid PrimaryKeyId { get; protected set; }

        [Display(Name = BEDocTypeConstant.ROUND_TYPE)]
        public Guid RndTypeId { get; set; }

        [Display(Name = BEDocTypeConstant.DOCUMENT_TYPE)]
        public Guid DocumentTypeId { get; set; }

        public string DocumentTypeName { get; set; }

        public List<Guid> ListDocumentTypeId { get; set; }

        public DocumentType objDocumentType { get; set; }

        public override ATSPrivilage Privilage
        {
            get { return ATSPrivilage.Vacancy; }
        }
    }
}
