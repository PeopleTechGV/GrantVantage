using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity.Master
{
    public class Client : MasterBaseEntity
    {

        public Guid ClientId { get; set; }
        public String Clientname { get; set; }
        public String ContactPerson { get; set; }
        public String ContactNo{ get; set; }
        public String SubDomain{ get; set; }
        public String WebSite { get; set; }
        
        public String ConnectionString{ get; set; }
        public Guid CurrentLanguageId { get; set; }
        public String ExcelDownloadLangName { get; set; }
        public String CurrentCulture { get; set; }
        public String CurrencySymbol { get; set; }
        public Int32 ExcelTotalRow { get; set; }
        public String AbsoluteURI { get; set; }
        public List<ClientLanguage> ClientLanguageList{get; set;}
        /// <summary>
        /// it refered for the manager Link for managers
        /// </summary>
        public Guid VacancyId { get; set; }

        public SolrEntity.SearchParameter searchJob { get; set; }

        public List<BreadCrumb> BreadCrumbs { get; set; }
        public CompanyInfo objCompanyInfo { get; set; }
    }
}
