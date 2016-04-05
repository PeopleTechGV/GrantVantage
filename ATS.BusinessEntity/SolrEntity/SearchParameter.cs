using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BECommon = ATS.BusinessEntity.Common;

namespace ATS.BusinessEntity.SolrEntity
{
    public class SearchParameter : ICommonSearchParameter
    {
        
        public const int DefaultPageSize = 20;
        public SearchParameter()
        {
            Facets = new Dictionary<string, string>();
            PageSize = DefaultPageSize;
            PageIndex = 1;
        }
        public string KeyWords { get; set; }
        public string Location { get; set; }
        public string PositionType { get; set; }

        public string JobType { get; set; }
        public string FpTime { get; set; }
        public string SkillSearch { get; set; }
        public string FreeSearch { get; set; }
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


        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public IDictionary<string, string> Facets { get; set; }
        public string Sort { get; set; }
        public int FirstItemIndex
        {
            get
            {
                return (PageIndex - 1) * PageSize;
            }
        }
        public int LastItemIndex
        {
            get
            {
                return FirstItemIndex + PageSize;
            }
        }

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






    }
}
