using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity
{
    public class AppRnd:BaseEntity
    {
        public Guid AppRndId { get; set; }

        public Guid ApplicationId { get; set; }

        public Guid VacRndId { get; set; }
        
    }
}
