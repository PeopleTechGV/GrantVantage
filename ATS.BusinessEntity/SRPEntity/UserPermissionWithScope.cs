using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity.SRPEntity
{
    public class UserPermissionWithScope
    {
        public List<Guid> DivisionId { get; set; }
        public ATSScope Scope { get; set; }
        public List<ATSPermissionType> PermissionType { get; set; }
    }
}
