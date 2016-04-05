using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity
{
    public class UserSecurityRole : ClientBaseEntity
    {
        public Guid UserSecurityRoleId { get; set; }
        public Guid UserId { get; set; }
        public ATSSecurityRole AtsSecurityRole { get; set; }
        public Guid AtsSecurityRoleId { get; set; }

        public override ATSPrivilage Privilage
        {
            get { return ATSPrivilage.SecurityRole; }
        }
        public String SecurityRoleName { get; set; }
    }
}
