using ATS.BusinessEntity.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using BELanCat = ATS.BusinessEntity.Common.QueCategory;

namespace ATS.BusinessEntity.SkeVacTemp
{
    public class SkeVacQueCat : ClientBaseEntity
    {
        public Guid PrimaryKeyId { get; protected set; }

        [Display(Name = BELanCat.FRM_CATEGORY)]
        public Guid QueCatId { get; set; }

        [Display(Name = BELanCat.FRM_CATEGORY)]
        public List<Guid> ListQueCatId { get; set; }


        [Display(Name = BELanCat.FRM_WEIGHT)]
        //[ATSRegularExpression(@"^\d+$", ErrorMessage = "{0}")]
        [ATSRegularExpression(@"(100)|[0-9]\d?", ErrorMessage = "{0}")]
        public int Weight { get; set; }

        public QueCat objQueCat { get; set; }

        public int TVacQueCount { get; set; }

        public override ATSPrivilage Privilage
        {
            get { return ATSPrivilage.Vacancy; }
        }
    }
}
