using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;

namespace ATS.BusinessEntity
{
    public class AppQueAns : BaseEntity
    {
        public Question Question { get; set; }
        public AppAnswer AppAnswer { get; set; }
    }
}
