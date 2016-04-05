using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolrSearchEntity = ATS.BusinessEntity.SolrEntity;


namespace ATS.BusinessEntity.SolrEntity
{
    public class SolrSearchObject : ICommonSearchParameter
    {
        

        public SearchParameter Search { get; set; }
        public string KeyWords { get; set; }
        public string Location { get; set; }
        public string PositionType { get; set; }
        public string JobType { get; set; }
        public string FpTime { get; set; }
        public string SkillSearch { get; set; }

        private decimal _MinSalary { get; set; }
        private decimal _MaxSalary { get; set; }

        public decimal MinSalary
        {
            get
            {
                return _MinSalary == 0 ? DefaultMinSalary : _MinSalary;
            }
            set { this._MinSalary = value; }

        }

        public decimal MaxSalary
        {
            get
            {
                return _MaxSalary == 0 ? DefaultMaxSalary : _MaxSalary;
            }
            set { this._MaxSalary = value; }

        }


        public decimal DefaultMinSalary { get; set; }
        public decimal DefaultMaxSalary { get; set; }

        public ICollection<SolrSearchEntity.SolrSearchFields> SearchData { get; set; }

        public int TotalCount { get; set; }
        public int SavedJobCount { get; set; }
        public IDictionary<string, ICollection<KeyValuePair<string, int>>> Facets { get; set; }
        public string DidYouMean { get; set; }
        public bool QueryError { get; set; }

        private DateTime _MinDate { get; set; }
        private DateTime _MaxDate { get; set; }

        public DateTime MinDate
        {
            get
            {
                return _MinDate == DateTime.MinValue ? DefaultMinDate : _MinDate;
            }
            set { this._MinDate = value; }
        }

        public DateTime MaxDate
        {
            get
            {
                return _MaxDate == DateTime.MinValue ? DefaultMaxDate : _MaxDate;
            }
            set { this._MaxDate = value; }
        }

        public DateTime DefaultMinDate { get; set; }

        public DateTime DefaultMaxDate { get; set; }

        public SolrSearchObject()
        {
            Search = new SearchParameter();
            Facets = new Dictionary<string, ICollection<KeyValuePair<string, int>>>();
            SearchData = new List<SolrSearchEntity.SolrSearchFields>();

        }




    }


    public class SolrEmployeeSearchObject
    {
        public SolrEmployeeSearchObject()
        {
            SearchData = new List<SolrSearchEntity.SolrEmployeeSearchFields>();
        }
        public int TotalCount { get; set; }
        public bool QueryError { get; set; }
        public ICollection<SolrSearchEntity.SolrEmployeeSearchFields> SearchData { get; set; }
    }
}
