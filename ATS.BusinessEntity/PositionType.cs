using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using BECommonConst = ATS.BusinessEntity.Common.CommonConstant;
using ATS.BusinessEntity.Attributes;


namespace ATS.BusinessEntity
{
    public class PositionType : ClientBaseEntity
    {
        public Guid PositionTypeId { get; set; }

        public Guid DivisionId { get; set; }

        public Boolean IsActive { get; set; }

        
        [ATSRequired(ErrorMessage = "{0}")]
        [Display(Name = BECommonConst.DEFAULT_NAME)]
        public String DefaultName { get; set; }
        public List<EntityLanguage> PositionTypeEntityLanguage { get; set; }

        public List<RecordExists> RecordExistsCount { get; set; }
        public override ATSPrivilage Privilage
        {
            get
            {
                return ATSPrivilage.PositionType;
            }

        }
        /// <summary>
        /// Will be used for Get All only
        /// </summary>
        public String LocalName { get; set; }

        /// <summary>
        /// Will be used for Get All only
        /// </summary>
        public String DivisionName { get; set; }

        public List<PositionType> Records { get; set; }

        public int VacancyCount { get; set; }


        public IList<Division> AvailableDivisionList { get; set; }
        public IList<Division> SelectedDivisionList { get; set; }
        public DivisionMaster objDivision { get; set; }

        public string AttachedDivision { get; set; }

        public List<Guid> DivisionList { get; set; }
    }
}
