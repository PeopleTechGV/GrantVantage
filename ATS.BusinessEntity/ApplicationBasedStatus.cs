using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using ATS.BusinessEntity.Attributes;
using BEConstant = ATS.BusinessEntity.Common.AppBasedStatusConstant;

namespace ATS.BusinessEntity
{
    public class ApplicationBasedStatus : BaseEntity
    {

        public Guid ApplicationBasedStatusId { get; set; }
        [Display(Name = BEConstant.FRM_STATUSTEXT)]
        [ATSRequired(ErrorMessage = "{0}")]
        public string StatusText { get; set; }

        [Display(Name = BEConstant.FRM_APPBASEDSTATUSCATEGORY)]        
        public string Category { get; set; }

        public int Ordinal { get; set; }

        public List<EntityLanguage> ApplicationBasedStatusEntityLanguage { get; set; }


        public ATSPrivilage Privilage
        {
            get
            {
                return ATSPrivilage.ApplicationBasedStatus;
            }

        }

        public string LocalName { get; set; }
        [Display(Name = BEConstant.FRM_SHOWTOCANDIDATE)]
        [ATSRequired(ErrorMessage = "{0}")]
        public string ShowToCandidate { get; set; }

        [ATSStringLength(500)]
        [Display(Name = BEConstant.FRM_EMAILSUBJECT)]        
        [ATSRequired(ErrorMessage = "{0}")]
        public String Subject { get; set; }

        [Display(Name = BEConstant.FRM_EMAILDESCRIPTION)]
        [Required(ErrorMessage = "{0}")]
        public String EmailDescription { get; set; }
    }
}
