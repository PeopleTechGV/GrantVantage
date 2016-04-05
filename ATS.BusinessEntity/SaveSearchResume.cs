using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity.SolrEntity;

namespace ATS.BusinessEntity
{
    public class SaveSearchResume:BaseEntity
    {
        public Guid SaveResumeSearchId { get; set; }

        public Guid UserId { get; set; }

        public String JsonQuery { get; set; }

        public String SearchQueryName { get; set; }

        public bool IsDefault { get; set; }

        public List<BEClient.SolrEmployeeSearchFields> objSolrEmployeeSearchFields = new List<BEClient.SolrEmployeeSearchFields>(); 


    }
}
