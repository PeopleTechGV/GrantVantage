using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity.Master
{
    public class StoredProcedure
    {
        public String ROUTINE_CATALOG { get; set; }
        public String ROUTINE_TYPE { get; set; }
        public String ROUTINE_NAME { get; set; }
        public String ROUTINE_DEFINITION { get; set; }
    }
}
