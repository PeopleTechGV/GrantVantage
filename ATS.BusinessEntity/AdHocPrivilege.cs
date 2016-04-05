using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity
{
    public class AdHocPrivilege
    {
        public Guid AdHocPrivilegeId { get; set; }
        public Guid ATSPrivilegeId { get; set; }
        public String PermissionType{ get; set; }
        public String Scope { get; set; }
    }
}
