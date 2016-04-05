using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using ATS.BusinessEntity.Attributes;
using BECommonConst = ATS.BusinessEntity.Common.CommonConstant;

namespace ATS.BusinessEntity
{
    public class LanguageBlocks : ClientBaseEntity
    {
        public Guid LanguageBlockId { get; set; }
        
        [ATSStringLength(200)]
        [Display(Name = "Language Block Name")]
        [Required(ErrorMessage = "Language block name required.")]
        public String LanguageBlockName { get; set; }

        [Display(Name = "Language Block Description")]
        [Required(ErrorMessage = "Language block description required.")]
        public String LanguageBlockDescription { get; set; }
        public Guid ClientId { get; set; }
        public Guid LanguageId { get; set; }
        public List<LanguageBlocks> listLanguageBlocks { get; set; }

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
    }
}
