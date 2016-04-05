using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BELanguageLabel = ATS.BusinessEntity.Common.LanguageLabel;
using BECommonLabel = ATS.BusinessEntity.Common.CommonConstant;

namespace ATS.BusinessEntity.Master
{
   public class Language
    {
       
       public Guid LanguageId { get; set; }

       [Display(Name = BELanguageLabel.FRM_LANGUAGE)]
       [StringLength(30,MinimumLength=1)]
       [Required(ErrorMessage = BELanguageLabel.FRM_LANGUAGE + " " + BECommonLabel.REQUIRE + ".")]
        public String LanguageName { get; set; }

       [Display(Name = BELanguageLabel.FRM_LANGUAGE_CULTURE)]
        public String LanguageCulture { get; set; }

       [Display(Name = BELanguageLabel.FRM_LANGUAGE_UPLOADFILE)]
       public string UploadFile { get; set; }
    }
}
