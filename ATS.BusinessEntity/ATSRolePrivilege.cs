using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity
{
    public class ATSRolePrivilege : BaseEntity
    {
        public Guid ATSRolePrivilegeId { get; set; }

        public Guid ClientId { get; set; }

        public Guid ATSSecurityRoleId { get; set; }

        public Guid ATSRelativePrivilegeId { get; set; }

        public bool ScopeAll { get; set; }

        public bool ScopeOwn { get; set; }

        public bool ScopeChild { get; set; }

        public bool ScopeSister { get; set; }

        public string DisplayName { get; set; }

        public int DisplayOrder { get; set; }

        public int RelativeDisplayOrder { get; set; }
    }
}
