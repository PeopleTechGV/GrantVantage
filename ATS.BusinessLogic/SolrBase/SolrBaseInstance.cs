using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolrNet;
using SolrNet.Impl;
using Microsoft.Practices.ServiceLocation;
using solrEntity = ATS.BusinessEntity.SolrEntity;
namespace ATS.BusinessLogic.SolrBase
{
    public class SolrBaseInstance
    {
        /// <summary>
        /// New instance of Solr
        /// </summary>
        /// <typeparam name="T">Specific type</typeparam>
        public class Instance<T>
        {
            /// <summary>
            /// Start Solr instance for a specific type
            /// </summary>
            public ISolrReadOnlyOperations<solrEntity.SolrSearchFields> ATSSearch(String ATSSolrURL)
            {
                var instances = Startup.Container.GetAllInstances(typeof(ISolrOperations<solrEntity.SolrSearchFields>));
                if (instances.Count() == 0)
                {
                    var ATSConnection = new SolrConnection(ATSSolrURL);
                    var ATSloggingConnection = new BaseSolrConnection(ATSConnection,"job");
                    Startup.Init<solrEntity.SolrSearchFields>(ATSloggingConnection);
                }
                return ServiceLocator.Current.GetInstance<ISolrReadOnlyOperations<solrEntity.SolrSearchFields>>();
            }

            public ISolrReadOnlyOperations<solrEntity.SolrEmployeeSearchFields> ATSEmployeeSearch(String ATSSolrEmployeeURL)
            {
                var instances = Startup.Container.GetAllInstances(typeof(ISolrOperations<solrEntity.SolrEmployeeSearchFields>));
                if (instances.Count() == 0)
                {
                    var ATSConnection = new SolrConnection(ATSSolrEmployeeURL);
                    var ATSloggingConnection = new BaseSolrConnection(ATSConnection, "employee");
                    Startup.Init<solrEntity.SolrEmployeeSearchFields>(ATSloggingConnection);
                }
                return ServiceLocator.Current.GetInstance<ISolrReadOnlyOperations<solrEntity.SolrEmployeeSearchFields>>();
            }
            
        }
        
    }

    
    
}
