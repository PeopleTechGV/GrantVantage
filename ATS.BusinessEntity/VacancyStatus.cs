using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATS.BusinessEntity.Attributes;
using BECommonConst = ATS.BusinessEntity.Common.CommonConstant;
using System.ComponentModel.DataAnnotations;

namespace ATS.BusinessEntity
{
    public class VacancyStatus : BaseEntity
    {
        public Guid VacancyStatusId { get;set;}

        public Guid UserId { get; set; }

        public Guid ClientId { get; set; }

        [ATSRequired(ErrorMessage = "{0}")]
        [Display(Name = BECommonConst.DEFAULT_NAME)]
        public string VacancyStatusText { get; set; }

        [ATSRequired(ErrorMessage = "{0}")]
        public string Category { get; set; }

        public string VacancyStatusTextLocal { get; set; }

        public List<EntityLanguage> VacancyStatusEntityLanguage { get; set; }
        public List<RecordExists> RecordExistsCount { get; set; }
      
        public ATSPrivilage Privilage
        {
            get
            {
                return ATSPrivilage.VacancyStatus;
            }

        }
        public string AddUrl { get; set; }
        public string RemoveUrl { get; set; }
        public string ActionName { get; set; }
    }
}
