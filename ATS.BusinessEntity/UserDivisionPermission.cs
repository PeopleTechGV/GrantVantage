using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEUserDivisionConst = ATS.BusinessEntity.Common.UserDivisionPermission;
using System.ComponentModel.DataAnnotations;

namespace ATS.BusinessEntity
{
    public class UserDivisionPermission : ClientBaseEntity
    {
        public Guid UserDivisionPermissionId { get; set; }

        [Display(Name = BEUserDivisionConst.FRM_USER_DIV_DIVNAME)]
        public Guid DivisionId { get; set; }

        public Guid ClientId { get; set; }

        [Display(Name = BEUserDivisionConst.FRM_USER_DIV_USERNAME)]
        public Guid UserId { get; set; }

        public string Description { get; set; }

        
        //Used For GetAll and GetById
        [Display(Name = BEUserDivisionConst.FRM_USER_DIV_DIVNAME)]
        public string DivisionName { get; set; }

        [Display(Name = BEUserDivisionConst.FRM_USER_DIV_USERNAME)]
        public string UserFirstFame { get; set; }
        
        public override ATSPrivilage Privilage
        {
            get
            {
                return ATSPrivilage.Division;
            }

        }


    }
}
