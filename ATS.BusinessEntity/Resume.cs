using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity
{
    public class Resume : ClientBaseEntity
    {
        public override ATSPrivilage Privilage
        {
            get { return ATSPrivilage.Resume; }
        }
    }
}
