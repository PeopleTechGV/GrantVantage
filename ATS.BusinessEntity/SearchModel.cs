using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity
{
   public class SearchModel
    {
        public string SearchField { get; set; }
        public string SearchValue { get; set; }
        public string SortBy { get; set; }
        public string SortDirection { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
