using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity.SRPEntity
{
    public class SRPATSSecurityRole : IListSecurityEntity
    {
        public List<ATSSecurityRole> ListATSSecurityRole { get; set; }
        public List<ISecurityEntity> ListSecurityEntity { get; set; }
        public ATSPrivilage SRPPrivilage { get { return ATSPrivilage.SecurityRole; } }
    }
}
