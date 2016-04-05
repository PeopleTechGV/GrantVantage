using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using ATS.BusinessEntity.Attributes;
using BECommonConst = ATS.BusinessEntity.Common.CommonConstant;

namespace ATS.BusinessEntity
{
    public class EmailTemplates : ClientBaseEntity
    {
        [Display(Name = "Email Template")]
        public Guid EmailTemplateId { get; set; }

        public String EmailIdentifier { get; set; }

        [ATSStringLength(500)]
        [Display(Name = "Email For")]
        public String EmailName { get; set; }


        [ATSStringLength(500)]
        [Display(Name = "Email Subject")]
        [ATSRequired(ErrorMessage = "{0}")]
        public String Subject { get; set; }

        [Display(Name = "Email Description")]
        [Required(ErrorMessage = "{0}")]
        public String EmailDescription { get; set; }
        public String Description { get; set; }

        public Guid ClientId { get; set; }
        public Guid LanguageId { get; set; }
        public String EmailCategory { get; set; }
        public Int32 CategoryNo { get; set; }
        public String EmailCategoryDisplay
        {
            get
            {
                if (EmailCategory != null)
                    return EmailCategory.Replace("_", " ");
                else
                    return null;
            }
        }
        public List<EmailTemplates> listEmailTemplates { get; set; }

        #region ClientBaseEntity
        public override ATSPrivilage Privilage
        {
            get
            {
                return ATSPrivilage.LanguageBlocks;
            }
        }
        #endregion

        public List<RecordExists> RecordExistsCount { get; set; }
        [Display(Name = "PDF Header")]
        public String PdfHeader { get; set; }

        public string DestinationAddress { get; set; }
    }
}
