using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolrEntity = ATS.BusinessEntity.SolrEntity;
using SolrNet;
using SolrNet.Commands.Parameters;
using SolrNet.DSL;
using System.Collections.Specialized;
using solrFieldPatser = SolrNet.Impl.FieldParsers;
using System.Globalization;
namespace ATS.BusinessLogic.SolrBase
{
    public static class SolrResultMaker
    {
        private const string STR_LOCATION = "Location";
        private const string STR_JOBTYPE = "JobType";
        private const string STR_POSITIONTYPE = "PositionType";
        private const string STR_EMPLOYEMENTTYPE = "EmployementType";
        private const string STR_SKILLSEARCH = "SkillSearch";
        private const string FLD_SCORE = "*,score";
        private const string SORTBY_SCORE = "score desc";


        public static SolrEntity.SolrEmployeeSearchObject GetSearchEmployeeResultFromSolr(ISolrReadOnlyOperations<SolrEntity.SolrEmployeeSearchFields> _solrConnection, ISolrQuery solrQuery, ICollection<ISolrQuery> filterQueries, List<ISolrFacetQuery> facetQuery, SolrEntity.SearchParameter parameters, List<KeyValuePair<string, string>> extraParam = null)
        {
            try
            {
                parameters = new SolrEntity.SearchParameter();
                //parameters.Sort = SORTBY_SCORE;
                // List<string> field = new List<string>() { FLD_SCORE };

                var matchingEmployees = _solrConnection.Query(solrQuery, new QueryOptions
                {
                    FilterQueries = filterQueries,
                    OrderBy = SolrBase.BuildSolrQueryCreater.GetSelectedSort(parameters)
                    // Fields = field 
                });

                var view = new SolrEntity.SolrEmployeeSearchObject
                {
                    SearchData = matchingEmployees,


                    //Facets = matchingEmployees.FacetFields,
                    //DidYouMean = SolrSearchHelper.GetSpellCheckingResult(matchingEmployees),
                };

                return view;
            }
            catch (SolrNet.Exceptions.InvalidFieldException ex)
            {
                return new SolrEntity.SolrEmployeeSearchObject
                {
                    QueryError = true,
                };
            }
            catch (Exception ex)
            {
                return new SolrEntity.SolrEmployeeSearchObject
                {
                    QueryError = true,
                };
            }
        }


        public static SolrEntity.SolrEmployeeSearchObject GetCandidateCountFromQuery(ISolrReadOnlyOperations<SolrEntity.SolrEmployeeSearchFields> _solrConnection, ISolrQuery solrQuery, ICollection<ISolrQuery> filterQueries, List<KeyValuePair<string, string>> extraParam = null)
        {
            try
            {
                var matchingEmployees = _solrConnection.Query(solrQuery, new QueryOptions
                {
                    FilterQueries = filterQueries,
                    Rows = 0,
                    Start = 0,
                    ExtraParams = extraParam,
                });


                var view = new SolrEntity.SolrEmployeeSearchObject
                {
                    TotalCount = matchingEmployees.NumFound
                };

                return view;
            }
            catch (SolrNet.Exceptions.InvalidFieldException ex)
            {
                return new SolrEntity.SolrEmployeeSearchObject
                {
                    QueryError = true,
                };
            }
            catch (Exception ex)
            {
                return new SolrEntity.SolrEmployeeSearchObject
                {
                    QueryError = true,
                };
            }
        }

        /// <summary>
        /// This function will give you object of SolrSearchJobs based on your request
        /// </summary>
        /// <param name="_solrConnection">Solr connection</param>
        /// <param name="solrQuery">General Solr Query</param>
        /// <param name="filterQueries">Filter Solr Query</param>
        /// <param name="facetQuery">Facet query</param>
        /// <param name="parameters">Default param like page,Start...etc</param>
        /// <param name="extraParam">extra filter param</param>
        /// <returns>return object of SolrSearchJobs </returns>
        public static SolrEntity.SolrSearchObject GetSearchJobsResultFromSolr(ISolrReadOnlyOperations<SolrEntity.SolrSearchFields> _solrConnection, ISolrQuery solrQuery, ICollection<ISolrQuery> filterQueries, List<ISolrFacetQuery> facetQuery, SolrEntity.SearchParameter parameters, List<KeyValuePair<string, string>> extraParam = null)
        {
            try
            {
                var matchingEmployees = _solrConnection.Query(solrQuery, new QueryOptions
                {
                    FilterQueries = filterQueries,
                    Rows = parameters.PageSize,
                    Start = (parameters.PageIndex - 1) * parameters.PageSize,
                    ExtraParams = extraParam,
                    OrderBy = SolrBase.BuildSolrQueryCreater.GetSelectedSort(parameters),
                    SpellCheck = new SpellCheckingParameters(),
                    Facet = new FacetParameters
                    {
                        Queries = facetQuery
                    },

                });

                
               
               
                var view = new SolrEntity.SolrSearchObject
                    {
                        SearchData = matchingEmployees,
                        Search = parameters,
                        TotalCount = matchingEmployees.NumFound,
                        //Facets = matchingEmployees.FacetFields,
                        //DidYouMean = SolrSearchHelper.GetSpellCheckingResult(matchingEmployees),
                    };
               
                return view;
               
               


                
            }
            catch (SolrNet.Exceptions.InvalidFieldException ex)
            {
                return new SolrEntity.SolrSearchObject
                {
                    QueryError = true,
                };
            }
            catch (Exception ex)
            {
                return new SolrEntity.SolrSearchObject
                {
                    QueryError = true,
                };
            }
        }
        public static SolrEntity.SolrSearchObject GetJobCountFromQuery(ISolrReadOnlyOperations<SolrEntity.SolrSearchFields> _solrConnection, ISolrQuery solrQuery, ICollection<ISolrQuery> filterQueries, List<KeyValuePair<string, string>> extraParam = null)
        {
            try
            {
                var matchingEmployees = _solrConnection.Query(solrQuery, new QueryOptions
                {
                    FilterQueries = filterQueries,
                    Rows = 0,
                    Start = 0,
                    ExtraParams = extraParam,
                });


                var view = new SolrEntity.SolrSearchObject
                {
                    TotalCount = matchingEmployees.NumFound
                };

                return view;
            }
            catch (SolrNet.Exceptions.InvalidFieldException ex)
            {
                return new SolrEntity.SolrSearchObject
                {
                    QueryError = true,
                };
            }
            catch (Exception)
            {
                return new SolrEntity.SolrSearchObject
                {
                    QueryError = true,
                };
            }
        }
        public static IDictionary<string, StatsResult> GetStatsObjectFromQuery(ISolrReadOnlyOperations<SolrEntity.SolrSearchFields> _solrConnection, ISolrQuery _SolrQuery, StatsParameters _StatsParameters)
        {
            try
            {
                var matchingEmployees = _solrConnection.Query(_SolrQuery, new QueryOptions
                {
                    Stats = _StatsParameters,

                });

                return matchingEmployees.Stats;
            }
            catch (Exception)
            {
                return null;
            }
        }
        //public static BE.SolrSearchJobs SetFacetOrder(BE.SolrSearchJobs view)
        //{

        //    if (view.Facets.Keys.Contains("Experience"))
        //    {
        //        ICollection<KeyValuePair<string, int>> newExperienceCollection = (from row in view.Facets["Experience"]
        //                                                                          join row1 in Common.Functions.ExpFacetMapping
        //                                                                          on row.Key equals row1.Key
        //                                                                          orderby Convert.ToInt32(row.Key) descending
        //                                                                          select new KeyValuePair<string, int>(
        //                                                                               row1.Value,
        //                                                                               row.Value)
        //                                                                          ).ToList();
        //        view.Facets["Experience"] = newExperienceCollection;
        //    }
        //    return view;
        //}

        /// <summary>
        /// Date aand time will be refer in UTC format
        /// </summary>
        /// <param name="pSolrSearchObject"></param>
        /// <param name="_solrConnection"></param>
        /// <param name="currentCulture"></param>
        public static void DefaultSalaryBinder(ref SolrEntity.ICommonSearchParameter pSolrSearchObject, ISolrReadOnlyOperations<SolrEntity.SolrSearchFields> _solrConnection)
        {
            var statsParamsSalary = new StatsParameters();
            statsParamsSalary.AddField("SalaryMax");
            statsParamsSalary.AddField("SalaryMin");

            IDictionary<string, StatsResult> _StatsResult = GetStatsObjectFromQuery(_solrConnection, SolrQuery.All, statsParamsSalary);
            if (_StatsResult.ContainsKey("SalaryMax") && _StatsResult.ContainsKey("SalaryMin"))
            {
                pSolrSearchObject.DefaultMaxSalary = GetZeroRoundOfSalary(_StatsResult["SalaryMax"].Max.ToString(),true);
                pSolrSearchObject.DefaultMinSalary = GetZeroRoundOfSalary(_StatsResult["SalaryMin"].Min.ToString(), false); 
            }
        }
        public static decimal GetZeroRoundOfSalary(String salaryVal, bool IsMax)
        {
            if(salaryVal.Length<=1)
            {
            return Convert.ToDecimal(salaryVal);
            }
            decimal _myNumber = 10;
            int length = salaryVal.Length;

            for (int i = 0; i < length - 2; i++)
            {
                _myNumber = _myNumber * 10;
            }
                int Othernumber = Convert.ToInt32(salaryVal.Substring(0, 1));
                decimal newNumber = _myNumber * Othernumber;
                
            if (IsMax)
            {
                if (Convert.ToDecimal(salaryVal) == newNumber)
                {
                    _myNumber = _myNumber * Othernumber;
                }
                else
                {
                    _myNumber = _myNumber * (Othernumber+1);
                }
            }
            return _myNumber;
        }
        public static void DefaultDateBinder(ref SolrEntity.ICommonSearchParameter pSolrSearchObject, ISolrReadOnlyOperations<SolrEntity.SolrSearchFields> _solrConnection)
        {
            String[] FacetFields = new String[] { "PostedOn" };

            
            IDictionary<string, ICollection<KeyValuePair<string, int>>> _postedSolr = SolrResultMaker.FacetResult(_solrConnection,SolrQuery.All, FacetFields, 1, false, new SortOrder("PostedOn"));

            if (_postedSolr.ContainsKey("PostedOn") && _postedSolr["PostedOn"].Count() > 0)
            {
                pSolrSearchObject.DefaultMaxDate = solrFieldPatser.DateTimeFieldParser.ParseDate(_postedSolr["PostedOn"].Last().Key.ToString());
                pSolrSearchObject.DefaultMinDate = solrFieldPatser.DateTimeFieldParser.ParseDate(_postedSolr["PostedOn"].First().Key.ToString());
                if (pSolrSearchObject.DefaultMaxDate.Equals(pSolrSearchObject.DefaultMinDate))
                {
                    pSolrSearchObject.DefaultMaxDate=pSolrSearchObject.DefaultMaxDate.AddDays(1);
                }
                else
                {
                    pSolrSearchObject.DefaultMaxDate = pSolrSearchObject.DefaultMaxDate.ToLocalTime();
                }
            }
            else
            {
                pSolrSearchObject.DefaultMaxDate = DateTime.Now;
                pSolrSearchObject.DefaultMinDate = DateTime.Now;
            }
        }


        public static void SearchModelBinder(ref SolrEntity.ICommonSearchParameter pSolrSearchObject, NameValueCollection pValueCollection)
        {
            if (pValueCollection.HasKeys())
            {
                if (pValueCollection.GetValues(STR_LOCATION) != null && !String.IsNullOrEmpty(pValueCollection.GetValues(STR_LOCATION).FirstOrDefault()))
                {
                    pSolrSearchObject.Location = pValueCollection.GetValues(STR_LOCATION).FirstOrDefault();
                }
                if (pValueCollection.GetValues(STR_POSITIONTYPE) != null && !String.IsNullOrEmpty(pValueCollection.GetValues(STR_POSITIONTYPE).FirstOrDefault()))
                {
                    pSolrSearchObject.PositionType = pValueCollection.GetValues(STR_POSITIONTYPE).FirstOrDefault();
                }
                if (pValueCollection.GetValues(STR_JOBTYPE) != null && !String.IsNullOrEmpty(pValueCollection.GetValues(STR_JOBTYPE).FirstOrDefault()))
                {
                    pSolrSearchObject.JobType = pValueCollection.GetValues(STR_JOBTYPE).FirstOrDefault();
                }
                if (pValueCollection.GetValues(STR_SKILLSEARCH) != null && !String.IsNullOrEmpty(pValueCollection.GetValues(STR_SKILLSEARCH).FirstOrDefault()))
                {
                    pSolrSearchObject.SkillSearch = pValueCollection.GetValues(STR_SKILLSEARCH).FirstOrDefault();
                }
                if (pValueCollection.GetValues(STR_EMPLOYEMENTTYPE) != null && !String.IsNullOrEmpty(pValueCollection.GetValues(STR_EMPLOYEMENTTYPE).FirstOrDefault()))
                {
                    pSolrSearchObject.JobType = pValueCollection.GetValues(STR_EMPLOYEMENTTYPE).FirstOrDefault();
                }
            }

        }

        public static IDictionary<string, ICollection<KeyValuePair<string, int>>> FacetResult<T>(ISolrReadOnlyOperations<T> _solrConnection, ISolrQuery solrQuery, String[] fields, int? limit, bool sort, SortOrder order)
        {
            List<SolrFacetFieldQuery> solrFacetField = new List<SolrFacetFieldQuery>();
            foreach (String field in fields)
            {
                solrFacetField.Add(new SolrFacetFieldQuery(field));
            }

            var facetResult = _solrConnection.Query(solrQuery, new QueryOptions
            {
                Rows = 0,
                OrderBy = new[] { order },
                Facet = new FacetParameters
                {
                    Queries = solrFacetField.ToArray(),
                    Sort = sort,
                    MinCount = limit
                }
            });
            return facetResult.FacetFields;

        }
        public static List<DateTime> DefaultDateBinder<T>(ISolrReadOnlyOperations<T> _solrConnection, String SolrDateFieldName)
        {
            String[] FacetFields = new String[] { SolrDateFieldName };
            IDictionary<string, ICollection<KeyValuePair<string, int>>> _postedSolr = SolrResultMaker.FacetResult(_solrConnection, SolrQuery.All, FacetFields, null, false, new SortOrder(SolrDateFieldName, Order.DESC));
            List<DateTime> dtRange = new List<DateTime>();
            if (_postedSolr.ContainsKey(SolrDateFieldName) && _postedSolr[SolrDateFieldName].Count() > 0)
            {

                dtRange.Add(solrFieldPatser.DateTimeFieldParser.ParseDate(_postedSolr[SolrDateFieldName].First().Key.ToString()));
                dtRange.Add(solrFieldPatser.DateTimeFieldParser.ParseDate(_postedSolr[SolrDateFieldName].Last().Key.ToString()));
            }
            return dtRange;
        }
        public static IDictionary<string, GroupedResults<T>> GroupResult<T>(ISolrReadOnlyOperations<T> _solrConnection, ISolrQuery solrQuery, String[] fields, int Limit)
        {
            var results = _solrConnection.Query(solrQuery, new QueryOptions
            {
                Grouping = new GroupingParameters()
                {
                    Fields = fields,
                    Format = GroupingFormat.Grouped,
                    Limit = Limit
                }
            });
            IDictionary<string, GroupedResults<T>> myGroupby = results.Grouping;
            return myGroupby;
        }
    }
}
