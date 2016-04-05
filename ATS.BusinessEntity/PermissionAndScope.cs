using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity
{
    public class PrivilegeAndPermissionAndScope
    {
        public Guid PrivilegeId { get; set; }
        public List<PermissionAndScope> PermissionList { get; set; }
    }

    public class PermissionAndScope
    {
        public String Permission { get; set; }
        public List<ScopeList> ScopeList { get; set; }
    }

    public class ScopeList
    {
        public String Scope { get; set; }
    }
}
