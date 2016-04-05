using GridMvc;
using GridMvc.Filtering;
using GridMvc.Sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATS.WebUi.Common.Grid
{

    public class QueryStringGridSettingsProvider 
    {
        private readonly QueryStringFilterSettings _filterSettings;
        private readonly QueryStringSortSettings _sortSettings;

        public QueryStringGridSettingsProvider()
        {
            _sortSettings = new QueryStringSortSettings();
            //add additional header renderer for filterable columns:
            _filterSettings = new QueryStringFilterSettings();
        }

        #region IGridSettingsProvider Members

        public IGridSortSettings SortSettings
        {
            get { return _sortSettings; }
        }

        public IGridFilterSettings FilterSettings
        {
            get { return _filterSettings; }
        }

       

        #endregion
    }

  

  

}