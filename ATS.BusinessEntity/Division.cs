using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using ATS.BusinessEntity.Attributes;
using BECommonConst = ATS.BusinessEntity.Common.CommonConstant;
using BEDivisionConst = ATS.BusinessEntity.Common.DivisionConstant;

namespace ATS.BusinessEntity
{
    public class Division : ClientBaseEntity
    {
        public Guid DivisionId { get; set; }

        public Guid ClientId { get; set; }

        [Display(Name = BEDivisionConst.FRM_DIV_PARENT_DIVISION)]
        public Guid? ParentDivisionId { get; set; }

        [ATSRequired(ErrorMessage = "{0}")]
        [Display(Name = BECommonConst.DEFAULT_NAME)]
        public string DefaultName { get; set; }


        public List<EntityLanguage> DivisionEntityLanguage { get; set; }
        public List<RecordExists> RecordExistsCount { get; set; }
        public bool IsActive { get; set; }

        /// <summary>
        /// Will be used for Get All only
        /// </summary>
        [Required(ErrorMessage = BEDivisionConst.FRM_DIVISION_REQUIRED_MSG)]
        public String DivisionName { get; set; }
        public String DivisionNameTree { get; set; }
        /// <summary>
        /// Will be used for Get All only
        /// </summary>
        public String LocalName { get; set; }
        public String ParentDivisionName { get; set; }
        public override ATSPrivilage Privilage
        {
            get
            {
                return ATSPrivilage.Division;
            }

        }

        public int VacancyCount { get; set; }

        public int JobLocationCount { get; set; }

        public int UsersCount { get; set; }
        public List<Division> Children { get; set; }
        public IList<PositionType> AvailablePositionTypeList { get; set; }
        public IList<PositionType> SelectedPositionTypeList { get; set; }

        public IList<JobLocation> AvailableJobLocationList { get; set; }
        public IList<JobLocation> SelectedJobLocationList { get; set; }
        public PositionTypeMaster objPositionType { get; set; }
        public JobLocationMaster objJobLocation { get; set; }
        
        public String SelectePositionType { get; set; }
        public String SelecteJobLocation { get; set; }

        public int Level { get; set; }

    }
}
