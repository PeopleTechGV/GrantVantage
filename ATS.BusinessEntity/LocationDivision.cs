using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity
{
    public class LocationDivision : BaseEntity
    {
        public Guid LocationDivisionId { get; set; }

        public Guid JobLocationId { get; set; }

        public Guid DivisionId { get; set; }
    }
}
