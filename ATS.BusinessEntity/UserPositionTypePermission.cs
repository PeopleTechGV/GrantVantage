using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEUserPositionTYPEConst = ATS.BusinessEntity.Common.UserPositiontypePermissionConstant;
using System.ComponentModel.DataAnnotations;

namespace ATS.BusinessEntity
{
   public class UserPositionTypePermission : ClientBaseEntity
    {
        public Guid UserPositionTypePermissionId { get; set; }

       [Display(Name = BEUserPositionTYPEConst.FRM_USER_POS_POSNAME)] 
       public Guid PositionTypeId { get; set; }

        public Guid ClientId { get; set; }


       [Display(Name = BEUserPositionTYPEConst.FRM_USER_POS_USERNAME)] 
       public Guid UserId { get; set; }

        public string Description { get; set; }


       //Used For Get
        public Guid DivisionId { get; set; }
        //Used For GetAll and GetById
        public string PositionTypeName { get; set; }

        public string UserFirstName { get; set; }

        public override ATSPrivilage Privilage
        {
            get
            {
                return ATSPrivilage.PositionType;
            }

        }
    }
}
