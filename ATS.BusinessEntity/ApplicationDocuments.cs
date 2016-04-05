using ATS.BusinessEntity.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity
{
    public class ApplicationDocuments : BaseEntity
    {
        public Guid ApplicationDocumentId { get; set; }

        public Guid VacRndId { get; set; }

        public Guid ApplicationId { get; set; }

        public Guid ATSResumeId { get; set; }

        public Guid VacancyId { get; set; }

        public Guid DocumentTypeId { get; set; }

        public String RequiredDocumentName { get; set; }

        public Guid RequiredDocumentId { get; set; }

        public string ExtensionTypes { get; set; }

        public string Path { get; set; }

        public string UploadedFileName { get; set; }

        public bool IsPending { get; set; }

        public bool IsOptional { get; set; }

        public List<RequiredDocument> ListRequiredDocuments { get; set; }

        public List<ATSResume> ATSResumeIdList { get; set; }
    }
}