using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity
{
   public class Client
    {
        public Guid ClientId { get; set; }
        public string ClientName { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNo { get; set; }
        public string SubDomain { get; set; }
        public string ConnectionString { get; set; }
    }
}
