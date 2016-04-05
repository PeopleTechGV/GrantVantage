using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BESrp = ATS.BusinessEntity.SRPEntity;

namespace ATS.BusinessLogic
{
    public class ResumeAction : ActionBase
    {
        public ResumeAction(Guid ClientId, bool CreateSRPObject = false)
            : base(ClientId)
        {
            if (CreateSRPObject)
                base.CreateSRPObject(SRPCommon.CreateSRPEntityObjects.SRPCreateObj<BESrp.SRPResume>(ClientId));
        }

    }
}
