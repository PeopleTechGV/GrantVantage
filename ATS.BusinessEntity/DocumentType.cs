using ATS.BusinessEntity.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using BEDocTypeConstant = ATS.BusinessEntity.Common.DocumentTypeConstant;

namespace ATS.BusinessEntity
{
    public class DocumentType : ClientBaseEntity
    {
        [Display(Name = BEDocTypeConstant.DOCUMENT_TYPE)]
        public Guid DocumentTypeId { get; set; }

        [Display(Name = BEDocTypeConstant.DOCUMENT_TYPE)]
        [ATSStringLength(100), ATSRequired(ErrorMessage = "{0}")]
        public String DocumentName { get; set; }

        [Display(Name = BEDocTypeConstant.ALLOWED_EXTENSION_TYPES)]
        [ATSRequired(ErrorMessage = "{0}")]
        public String ExtensionTypes { get; set; }

        public String Description { get; set; }

        public Boolean CanDelete { get; set; }

        public List<EntityLanguage> DocumentTypeEntityLanguage { get; set; }
        public override ATSPrivilage Privilage
        {
            get
            {
                return ATSPrivilage.DocumentType;
            }
        }
        public string LocalName { get; set; }


        public Int32 DocCategory { get; set; }

        public List<ATSResume> ATSUserDocumentList { get; set; }
    }
}
