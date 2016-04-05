using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity
{
    public interface IListSecurityEntity
    {
        List<ISecurityEntity> ListSecurityEntity { get; set; }
        ATSPrivilage SRPPrivilage { get; }
    }
    public interface ISecurityEntity
    {
        ATSPrivilage Privilage { get; set; }
        ATSSecurityRole SecurityRole { get; set; }
        ATSPermissionType PermissionType { get; set; }
        ATSScope Scope { get; set; }
    }
}
