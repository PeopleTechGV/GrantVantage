using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity
{
    public class ATSRelativePrivilege : BaseEntity
    {

        public String DisplayName { get; set; }

        public Guid ATSRelativePrivilegeId { get; set; }

        public ATSPermissionType PermissionType { get; set; }

        public Guid ATSSecurityRoleId { get; set; }

        public bool ScopeAll { get; set; }
        public bool ScopeOwn { get; set; }
        public bool ScopeChild { get; set; }
        public bool ScopeSister { get; set; }
    }
}
