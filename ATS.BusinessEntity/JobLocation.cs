using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BECommonConst = ATS.BusinessEntity.Common.CommonConstant;
using BEJobLocationConst = ATS.BusinessEntity.Common.JobLocation;
using ATS.BusinessEntity.Attributes;
using System.ComponentModel.DataAnnotations;
namespace ATS.BusinessEntity
{
    public class JobLocation : ClientBaseEntity
    {
        public Guid JobLocationId { get; set; }

        public Guid LocationDivisionId { get; set; }

        [Display(Name = BEJobLocationConst.FRM_JOB_DIVNAME)]
        public Guid DivisionId { get; set; }

        [Display(Name = BECommonConst.DEFAULT_NAME)]
        [ATSStringLength(100), ATSRequired(ErrorMessage = "{0}")]
        public String DefaultValue { get; set; }
        public List<EntityLanguage> JobLocatoinEntityLanguage { get; set; }
        public override ATSPrivilage Privilage
        {
            get { return ATSPrivilage.JobLocation; }
        }

        public List<RecordExists> RecordExistsCount { get; set; }

        /// <summary>
        /// Will be used for Get All only
        /// </summary>
        public String DivisionName { get; set; }
        /// <summary>
        /// Will be used for Get All only
        /// </summary>
        public String LocalName { get; set; }


        public List<JobLocation> Records { get; set; }
        public int TotalRecords { get; set; }

        public Boolean IsActive { get; set; }

        [Display(Name = BEJobLocationConst.FRM_JOB_ONBOARDING_MANAGER)]
        public Guid OnBoardingManagerId { get; set; }


        [Display(Name = BEJobLocationConst.FRM_JOB_LOC_MANAGER)]
        public Guid LocationManagerId { get; set; }

        public String LocationManagerName { get; set; }

        public String OnBoardingManagerName { get; set; }

        public DivisionMaster objDivision { get; set; }
        public IList<Division> AvailableDivisionList { get; set; }
        public IList<Division> SelectedDivisionList { get; set; }
        public String SelecteDivision { get; set; }

        public String City { get; set; }
        public String State { get; set; }
        public String Country { get; set; }
        public String ZipCode { get; set; }

        public List<Guid> DivisionList { get; set; }
    }
}
