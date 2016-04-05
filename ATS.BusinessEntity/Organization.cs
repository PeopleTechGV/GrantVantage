using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity
{
   public class Organization
    {
        public int OrganizationID { get; set; }
        public string OrganizationName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }

        public Guid UserId { get; set; }

    }
}
