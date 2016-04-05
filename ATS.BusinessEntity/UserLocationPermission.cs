using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEUserLocationPermissionConst = ATS.BusinessEntity.Common.UserLocationPermissionConstant;
using System.ComponentModel.DataAnnotations;

namespace ATS.BusinessEntity
{
    public class UserLocationPermission:ClientBaseEntity
    {
        public Guid UserLocationPermissionId { get; set; }

        [Display(Name=BEUserLocationPermissionConst.FRM_USER_LOC_LOC)]
        public Guid FullLocation { get; set; }

        public Guid ClientId { get; set; }

        [Display(Name=BEUserLocationPermissionConst.FRM_USER_LOC_USERNAME)]
        public Guid UserId { get; set; }

        public string Description { get; set; }

        public string JobLocation { get; set; }

        public string UserFirstName { get; set; }
        
        //for get purpose

        public Guid DivisionId { get; set; }
        
        public override ATSPrivilage Privilage
        {
            get
            {
                return ATSPrivilage.JobLocation;
            }

        }

    }
}
