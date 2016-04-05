using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BELanguagesConst = ATS.BusinessEntity.Common.LanguagesConst;
using System.ComponentModel.DataAnnotations;
using ATS.BusinessEntity.Attributes;

namespace ATS.BusinessEntity
{
    public class Languages:BaseEntity
    {
        public Guid LanguagesId { get; set; }
        
        public Guid ProfileId { get; set; }

        public Guid UserId { get; set; }


        [Display(Name = BELanguagesConst.FRM_LAN_LANGUAGECODE)]
        [ATSStringLength(50)]
        [ATSRequired(ErrorMessage = "{0}")]
        public String LanguageCode { get; set; }

        [Display(Name = BELanguagesConst.FRM_LAN_READ)]
        public Boolean Read { get; set; }

        [Display(Name = BELanguagesConst.FRM_LAN_WRITE)]
        public Boolean Write { get; set; }

        [Display(Name = BELanguagesConst.FRM_LAN_SPEAK)]
        public Boolean Speak { get; set; }

        [Display(Name = BELanguagesConst.FRM_LAN_COMMENTS)]
        public String Comments { get; set; }

    }

}
