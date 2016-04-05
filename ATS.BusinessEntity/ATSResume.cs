using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity
{
    public class ATSResume : BaseEntity
    {
        public Guid ATSResumeId { get; set; }

        public Guid ProfileId { get; set; }

        public Guid CoverLetterId { get; set; }

        public Guid UserId { get; set; }

        public String Details { get; set; }

        public String UploadedFileName { get; set; }

        public String NewFileName { get; set; }

        public String Path { get; set; }

        public String TitleName { get; set; }

        public Boolean IsResume { get; set; }

        public Boolean IsCoverLetter { get; set; }

        public String IsCoverLetterText
        {
            get
            {
                if (IsCoverLetter == true)
                    return IsCoverLetterText = "Cover Letter";
                else
                    return IsCoverLetterText = "Resume";
            }
            set { }
        }

        public DateTime DateUploaded
        {
            get
            {
                if (UpdatedOn.Equals(DateTime.MinValue))
                    return CreatedOn;
                else
                    return UpdatedOn;
            }
        }

        public String ResumeName { get; set; }

        public String CoverLetterName { get; set; }

        public String ProfileName { get; set; }

        public DateTime AppliedOn { get; set; }

        public bool IsDefaultProfile { get; set; }

        public string AppliedOnText
        {
            get
            {
                return ((DateTime)AppliedOn).ToString(Common.DateFormatConstant.US_FORMAT);
            }
        }

        public string DateUploadedText
        {
            get
            {
                return ((DateTime)DateUploaded).ToString(Common.DateFormatConstant.US_FORMAT);
            }
        }

        public bool IsResumeApplied { get; set; } 

        public Guid DocumentTypeId { get; set; }

        public string DocumentTypeName { get; set; }

        public String CandidateDescription { get; set; }

        public Guid VacRndId { get; set; }

        public Guid RequiredDocumentId { get; set; }
       // CR-11
        public bool isdocumnetsubmitted { get; set; }
    }
}
