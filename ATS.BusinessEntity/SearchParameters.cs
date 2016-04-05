using Newtonsoft.Json;
using SolrNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;

namespace ATS.BusinessEntity
{

    [DataContract]
    public class SearchParameters
    {
        [DataMember]
        public List<Inclusions_Exclusions> exclusions { get; set; }
        [DataMember]
        public int exc_count { get;set;}
        [DataMember]
        public List<Inclusions_Exclusions> inclusions { get; set; }
        [DataMember]
        public int inc_count { get; set; }
    }

    [DataContract]
    public class Inclusions_Exclusions
    {
        [DataMember]
        public string type { get; set; }
        [DataMember]
        public string grouping_operator { get; set; }
        [DataMember]
        public string criteria { get; set; }
        [DataMember]
        public string field { get; set; }
        [DataMember]
        public string operators { get; set; }
        public ISolrQuery Query;
        public SolrMultipleCriteriaQuery MultipleQueries;
        [DataMember]
        public int count { get; set; }
        [DataMember]
        public string value { get; set; }
        [DataMember]
        public List<additional_qualifiers> additional_qualifiers { get; set; }
        [DataMember]
        public List<Inclusions_Exclusions> Components { get; set; }
    }

    //[DataContract]
    //public class Components1
    //{
    //    [DataMember]
    //    public string type { get; set; }
    //    [DataMember]
    //    public string criteria { get; set; }
    //    [DataMember]
    //    public string field { get; set; }
    //    [DataMember]
    //    public string operators { get; set; }

    //    public ISolrQuery Query;

    //    public SolrMultipleCriteriaQuery MultipleQueries;
    //    [DataMember]
    //    public int count { get; set; }
    //    [DataMember]
    //    public string value { get; set; }
    //    [DataMember]
    //    public string grouping_operator { get; set; }
    //    [DataMember]
    //    public List<additional_qualifiers> additional_qualifiers { get; set; }
    //    [DataMember]
    //    public List<Components1> Components { get; set; }

    //}

    [DataContract]
    public class additional_qualifiers
    {
        [DataMember]
        public string key { get; set; }
        [DataMember]
        public string operators { get; set; }
        [DataMember]
        public string option { get; set; }
    }

    [DataContract]
    public class RootObject1
    {
        [DataMember]
        public List<SearchParameters> SearchParameters { get; set; }
    }
}
