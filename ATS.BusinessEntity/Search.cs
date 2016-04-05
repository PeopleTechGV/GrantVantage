using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity
{
    public class Search:BaseEntity
    {
        public Guid SearchId { get; set; }

        public Guid ClientId { get; set; }
         
        public Guid UserId { get; set; }

        public String Position { get; set; }
              
        public Decimal SalMaxRange { get; set; }

        public Decimal SalMinRange { get; set; }

        public DateTime DateMaxRange { get; set; }

        public DateTime DateMinRange { get; set; }

        public String  Location { get; set; }

        public String KeyWords { get; set; }

        public String JobType { get; set; }

        public String EmploymentType { get; set; }

        public int JobTypeId { get; set; }

        public string JobName { get; set; }
    
    }
}
