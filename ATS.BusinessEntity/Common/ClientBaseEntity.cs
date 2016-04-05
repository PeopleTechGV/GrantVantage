using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity
{
    public abstract class ClientBaseEntity : BaseEntity
    {
        public abstract ATSPrivilage Privilage { get;  }
        public Guid ClientId {get;set;}
        public ATSScope Scope { get; set; }
        public List<ATSPermissionType> PermissionType { get; set; }
    }
}
