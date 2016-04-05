using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolrNet;
using BE = ATS.BusinessEntity;
using ATSHelper = ATS.HelperLibrary;
using BEMaster = ATS.BusinessEntity.Master;
namespace ATS.BusinessLogic.Common
{
    internal class EmployeeQuery
    {
        internal string FieldName { get; set; }
        internal string FsText { get; set; }
        internal string MidText { get; set; }
        internal string LtText { get; set; }
        internal bool NotQuery { get; set; }
        internal SolrMultipleCriteriaQuery ExistingQueries;
        internal SolrMultipleCriteriaQuery.Operator NewOperation;
    }

    public static class EmployeeSolrQueryOperation
    {

        //private static readonly char[] Punctuation = "+-&|!(){}[]^~*?:\\\"".ToCharArray();
        private static Dictionary<char, string> repl = new Dictionary<char, string>() { { '+', "\\+" },
                                                                                                 { '-', "\\-" },
                                                                                                 { '&', "\\&" },
        { '|', "\\|" },
        { '!', "\\!" },
        { '(', "\\(" },
        { ')', "\\)" },
        { '{', "\\{" },
        { '}', "\\}" },
        { '[', "\\[" },
        { ']', "\\]" },
        { '^', "\\^" },
        { '~', "\\~" },
        { ':', "\\:" },
        { '\\', "\\\\" },
        { '\"', "\\\"" }};
        
        private static Dictionary<BE.QueryOpeation, String> _QueryOperationList;
        public static Dictionary<BE.QueryOpeation, String> QueryOperationMapping()
        {
            if (_QueryOperationList == null)
            {
                _QueryOperationList = new Dictionary<BE.QueryOpeation, string>();
                _QueryOperationList.Add(BE.QueryOpeation.Eq, "Equals");
                _QueryOperationList.Add(BE.QueryOpeation.DNEq, "Does Not Equal");
                _QueryOperationList.Add(BE.QueryOpeation.Con, "Contains");
                _QueryOperationList.Add(BE.QueryOpeation.DNCon, "Does Not Contain");
                _QueryOperationList.Add(BE.QueryOpeation.DNConDa, "Does Not Contain Data");
                _QueryOperationList.Add(BE.QueryOpeation.ConDa, "Contains Data");
                _QueryOperationList.Add(BE.QueryOpeation.BeWith, "Begins With");
                _QueryOperationList.Add(BE.QueryOpeation.DNBeWith, "Does Not Begins With");
                _QueryOperationList.Add(BE.QueryOpeation.EndWith, "Ends With");
                _QueryOperationList.Add(BE.QueryOpeation.DNEndWith, "Does Not End With");
                _QueryOperationList.Add(BE.QueryOpeation.IsLt, "Is Less Than");
                _QueryOperationList.Add(BE.QueryOpeation.IsGt, "Is Greater Than");
                _QueryOperationList.Add(BE.QueryOpeation.ONAfter, "On or After");
                _QueryOperationList.Add(BE.QueryOpeation.ONBefore, "On or Before");
                _QueryOperationList.Add(BE.QueryOpeation.LSTDays, "Last XX Days");
                _QueryOperationList.Add(BE.QueryOpeation.LSTMonths, "Last XX Months");
                _QueryOperationList.Add(BE.QueryOpeation.EQToOrAbove, "Equal to or Above");
            }
            return _QueryOperationList;
        }
        public static string DoRepl(string Inp)
        {
            var tmp = Inp.Select(c =>
            {
                String r;
                if (repl.TryGetValue(c, out r))
                    return r;
                return c.ToString();
            });
            List<string> newstr = tmp.ToList<string>();
            return string.Join("", newstr.ToArray());
            
        }
        public static ISolrQuery BuildEmployeeQuery(String FieldName, String SearchText, String QueryOper)
        {
            String oldSearchText = SearchText;
            SearchText=DoRepl(SearchText);
            BE.QueryOpeation _queryOpp = BE.QueryOpeation.Con;
            if (EmployeeSolrQueryOperation.QueryOperationMapping().Where(x => x.Value.ToLower().Equals(QueryOper.ToLower())).Count() > 0)
            {
                _queryOpp = EmployeeSolrQueryOperation._QueryOperationList.Where(x => x.Value.ToLower().Equals(QueryOper.ToLower())).First().Key;
            }

            EmployeeQuery _ObjEmployeeQuery = new EmployeeQuery();
            BE.YesNo yesNofield = BE.YesNo.Yes;
         
            if (Enum.TryParse<BE.YesNo>(SearchText,true, out yesNofield))
            {
                if(Enum.IsDefined(typeof(BE.YesNo), SearchText))
                    SearchText = ((int)yesNofield).ToString();
               
            }
            _ObjEmployeeQuery.MidText = SearchText;

            _ObjEmployeeQuery.FieldName = FieldName;
            _ObjEmployeeQuery.FsText = String.Empty;
            _ObjEmployeeQuery.LtText = String.Empty;

            switch (_queryOpp)
            {
                case BE.QueryOpeation.Eq:
                    _ObjEmployeeQuery.NotQuery = false;
                    _ObjEmployeeQuery.FieldName = "EQ" + FieldName;
                    _ObjEmployeeQuery.MidText = _ObjEmployeeQuery.MidText.Replace(' ', '*');
                    break;
                case BE.QueryOpeation.DNEq:
                    _ObjEmployeeQuery.NotQuery = true;
                    _ObjEmployeeQuery.FieldName = "EQ" + FieldName;
                    _ObjEmployeeQuery.MidText = _ObjEmployeeQuery.MidText.Replace(' ', '*');
                    break;
                case BE.QueryOpeation.Con:
                    _ObjEmployeeQuery.FieldName = "EQ" + FieldName;
                    _ObjEmployeeQuery.MidText= _ObjEmployeeQuery.MidText.Replace(' ', '*');
                    _ObjEmployeeQuery.FsText = "*";
                    _ObjEmployeeQuery.LtText = "*";
                    _ObjEmployeeQuery.NotQuery = false;
                    break;
                case BE.QueryOpeation.ConDa:
                    _ObjEmployeeQuery.FieldName = "EQ" + FieldName;
                    _ObjEmployeeQuery.MidText="*";
                    _ObjEmployeeQuery.NotQuery = false;
                    break;
                case BE.QueryOpeation.DNCon:
                    _ObjEmployeeQuery.FieldName = "EQ" + FieldName;
                    _ObjEmployeeQuery.MidText = _ObjEmployeeQuery.MidText.Replace(' ', '*');
                    _ObjEmployeeQuery.FsText = "*";
                    _ObjEmployeeQuery.LtText = "*";
                    _ObjEmployeeQuery.NotQuery = true;
                    break;
                case BE.QueryOpeation.DNConDa:
                   _ObjEmployeeQuery.FieldName = "EQ" + FieldName;
                   _ObjEmployeeQuery.MidText = "*";
                    _ObjEmployeeQuery.NotQuery = true;
                    break;
                case BE.QueryOpeation.BeWith:
                    _ObjEmployeeQuery.FieldName = "EQ" + FieldName;
                    _ObjEmployeeQuery.MidText = _ObjEmployeeQuery.MidText.Replace(' ', '*');
                    _ObjEmployeeQuery.LtText = "*";
                    _ObjEmployeeQuery.NotQuery = false;
                    break;
                case BE.QueryOpeation.DNBeWith:
                    _ObjEmployeeQuery.FieldName = "EQ" + FieldName;
                    _ObjEmployeeQuery.MidText = _ObjEmployeeQuery.MidText.Replace(' ', '*');
                    _ObjEmployeeQuery.LtText = "*";
                    _ObjEmployeeQuery.NotQuery = true;
                    break;

                case BE.QueryOpeation.EndWith:
                    _ObjEmployeeQuery.FieldName = "EQ" + FieldName;
                    _ObjEmployeeQuery.MidText = _ObjEmployeeQuery.MidText.Replace(' ', '*');
                    _ObjEmployeeQuery.FsText = "*";
                    _ObjEmployeeQuery.NotQuery = false;
                    break;
                case BE.QueryOpeation.DNEndWith:
                    _ObjEmployeeQuery.FieldName = "EQ" + FieldName;
                    _ObjEmployeeQuery.MidText = _ObjEmployeeQuery.MidText.Replace(' ', '*');
                    _ObjEmployeeQuery.FsText = "*";
                    _ObjEmployeeQuery.NotQuery = true;
                    break;
                case BE.QueryOpeation.IsGt:
                case BE.QueryOpeation.IsLt:
                    return CreateSolrIntRangeQuery(_ObjEmployeeQuery, _queryOpp);
                  
                case BE.QueryOpeation.LSTDays:
                case BE.QueryOpeation.LSTMonths:
                case BE.QueryOpeation.ONBefore:
                case BE.QueryOpeation.ONAfter:
                    return CreateSolrDateRangeQuery(_ObjEmployeeQuery, _queryOpp);
                case BE.QueryOpeation.EQToOrAbove:
                    {
                        _ObjEmployeeQuery.MidText = oldSearchText;
                        return CreateDegreeTypeQuery(_ObjEmployeeQuery);
                    }

            }
            ISolrQuery _newQuery = CreateSolrQuery(_ObjEmployeeQuery);
            return _newQuery;


        }
        private static ISolrQuery CreateDegreeTypeQuery(EmployeeQuery empQuery)
        {
            BEMaster.Client _myClient = null;
            Prompt.Shared.Utility.Library.Methods.GetVar<BEMaster.Client>(ATSHelper.Constant.SESSION_LOGGEDIN_CLIENT, ref _myClient);
            List<BE.DegreeType> _degreeType = null;
            int _degreeTypePrp = 0;
            int _maxdegreeTypePrp = 0;
            if (_myClient != null)
            {
                _degreeType = CacheHelper.GetDegreeType(_myClient.ClientId, _myClient.CurrentLanguageId, _myClient.Clientname);
            }
            if (_degreeType != null)
            {

                //When Degree type deleted then _maxdegreeTypePrp became +1 then max so employee get 0 result
                _maxdegreeTypePrp = _degreeType.Max(x => x.Priority)+1;
                _degreeTypePrp = _maxdegreeTypePrp;
                
                if (_degreeType.Where(x => x.DefaultName.ToLower().Equals(empQuery.MidText.ToLower())).Count()>0)
                { 
                    //it will set min and max details
                _degreeTypePrp = _degreeType.Where(x => x.DefaultName.ToLower().Equals(empQuery.MidText.ToLower())).FirstOrDefault().Priority;
                _maxdegreeTypePrp = _degreeType.Max(x => x.Priority);
                }
                
            }

            ISolrQuery maxPri = new SolrQueryByRange<Int32>(empQuery.FieldName.ToString() + "Min", _degreeTypePrp, _maxdegreeTypePrp);
            ISolrQuery minPri = new SolrQueryByRange<Int32>(empQuery.FieldName.ToString() + "Max", _degreeTypePrp, _maxdegreeTypePrp);
            return new SolrMultipleCriteriaQuery(new List<ISolrQuery> { maxPri, minPri }, "OR");

        }
        private static ISolrQuery CreateSolrIntRangeQuery(EmployeeQuery empQuery, BE.QueryOpeation queryOpp)
        {

            String queryFormat = "{0}Min:[{1} TO {2}] OR {0}Max:[{1} TO {2}]";
            StringBuilder builder = new StringBuilder();
            Decimal searchVal = 0;
            Decimal.TryParse(empQuery.MidText.ToString(), out searchVal);
            if (searchVal >= 0)
            {

                switch (queryOpp)
                {
                    case BE.QueryOpeation.IsGt:
                        builder.Append(String.Format(queryFormat, empQuery.FieldName.ToString(), searchVal+1, "*"));
                        break;
                    case BE.QueryOpeation.IsLt:
                        builder.Append(String.Format(queryFormat, empQuery.FieldName.ToString(), "*", searchVal-1));
                        break;
                }
                return new SolrQuery(builder.ToString());
            }
            return null;
        }
        private static ISolrQuery CreateSolrDateRangeQuery(EmployeeQuery empQuery, BE.QueryOpeation queryOpp)
        {

            SolrNet.SolrQueryByRange<DateTime> maxDate = null;
            SolrNet.SolrQueryByRange<DateTime> minDate = null;
            //String queryFormat = empQuery.FieldName.ToString() + ":[{0} TO {1}]";
            String solrFormat = "yyyy-MM-ddTHH:mm:ssZ";
            StringBuilder builder = new StringBuilder();
            DateTime Startdt = DateTime.MinValue;
            DateTime Enddt = DateTime.MinValue;
            int xxDays = 0;
            int xxMonths = 0;
            String queryFormat = string.Empty;
            switch (queryOpp)
            {
                case BE.QueryOpeation.LSTDays:
                    Enddt = DateTime.Now;
                    if (Int32.TryParse(empQuery.MidText, out  xxDays))
                    {
                        xxDays = xxDays * -1;
                    }

                    //queryFormat = "({0}Min:[{1} TO {2}]) OR ({0}Max:[{1} TO {2}])";
                    Startdt = Enddt.AddDays(xxDays);
                    //builder.Append(String.Format(queryFormat, empQuery.FieldName.ToString(), Startdt.ToString(solrFormat), Enddt.ToString(solrFormat)));
                    maxDate = new SolrQueryByRange<DateTime>(empQuery.FieldName.ToString() + "Max", Startdt.ToUniversalTime(), Enddt.AddDays(1).ToUniversalTime());
                    minDate = new SolrQueryByRange<DateTime>(empQuery.FieldName.ToString() + "Min", Startdt.ToUniversalTime(), Enddt.AddDays(1).ToUniversalTime());
                    break;
                case BE.QueryOpeation.LSTMonths:
                    Enddt = DateTime.Now;
                    Int32.TryParse(empQuery.MidText, out  xxMonths);
                    if (Int32.TryParse(empQuery.MidText, out  xxMonths))
                    {
                        xxMonths = xxMonths * -1;
                    }
                    //queryFormat = "({0}Min:[{1} TO {2}]) OR ({0}Max:[{1} TO {2}])";
                    Startdt = Enddt.AddMonths(xxMonths);
                    //builder.Append(String.Format(queryFormat, empQuery.FieldName.ToString(), Startdt.ToString(solrFormat), Enddt.ToString(solrFormat)));
                    maxDate = new SolrQueryByRange<DateTime>(empQuery.FieldName.ToString() + "Max", Startdt.ToUniversalTime(), Enddt.ToUniversalTime());
                    minDate = new SolrQueryByRange<DateTime>(empQuery.FieldName.ToString() + "Min", Startdt.ToUniversalTime(), Enddt.ToUniversalTime());
                    break;
                case BE.QueryOpeation.ONBefore:
                    //queryFormat = "({0}Min:[{1} TO {2}]) OR ({0}Max:[{1} TO {2}])";
                    DateTime.TryParse(empQuery.MidText, out Enddt);
                    //builder.Append(String.Format(queryFormat, empQuery.FieldName.ToString(), DateTime.Now.AddYears(-100).ToString(solrFormat), Enddt.ToString(solrFormat)));
                    maxDate = new SolrQueryByRange<DateTime>(empQuery.FieldName.ToString() + "Max", DateTime.Now.AddYears(-100).ToUniversalTime(), Enddt.ToUniversalTime());
                    minDate = new SolrQueryByRange<DateTime>(empQuery.FieldName.ToString() + "Min", DateTime.Now.AddYears(-100).ToUniversalTime(), Enddt.ToUniversalTime());
                    break;
                case BE.QueryOpeation.ONAfter:
                    DateTime.TryParse(empQuery.MidText, out Startdt);
                    //queryFormat = "({0}Min:[{1} TO {2}]) OR ({0}Max:[{1} TO {2}])";
                    //builder.Append(String.Format(queryFormat, empQuery.FieldName.ToString(), Startdt.ToString(solrFormat), DateTime.Now.ToString(solrFormat)));
                    maxDate = new SolrQueryByRange<DateTime>(empQuery.FieldName.ToString() + "Max", Startdt.ToUniversalTime(), DateTime.UtcNow);
                    minDate = new SolrQueryByRange<DateTime>(empQuery.FieldName.ToString() + "Min", Startdt.ToUniversalTime(), DateTime.UtcNow);
                    break;
            }
            return new SolrMultipleCriteriaQuery(new List<ISolrQuery> { maxDate, minDate }, "OR");
        }

        private static ISolrQuery CreateSolrRangeQuery(String fieldName, String lessVal, String moreValue)
        {
            SolrQuery rangeQ = new SolrQuery(fieldName + ":" + "[" + lessVal + " TO " + moreValue + "]");
            return rangeQ;
        }
        
        
        private static ISolrQuery CreateSolrQuery(EmployeeQuery empQuery)
        {
            StringBuilder _queryCreater = new StringBuilder();

            if (!empQuery.FsText.Equals(String.Empty))
            {
                _queryCreater.Append(empQuery.FsText);
            }
            if (!empQuery.MidText.Equals(String.Empty))
            {
                _queryCreater.Append(empQuery.MidText);
            }
            if (!empQuery.LtText.Equals(String.Empty))
            {
                _queryCreater.Append(empQuery.LtText);
            }

            //if (empQuery.FsText.Equals(String.Empty) && empQuery.LtText.Equals(String.Empty) || _queryCreater.ToString().Split(' ').Count() > 1)
            //{
            //    //SolrQueryByField fieldQuery = new SolrQueryByField(empQuery.FieldName, _queryCreater.ToString());
            //    SolrQuery fieldQuery = new SolrQuery(empQuery.FieldName + ":\"" + _queryCreater.ToString() + "\"");

            //    if (empQuery.NotQuery)
            //        return (SolrQuery.All && !fieldQuery);
            //    else
            //        return fieldQuery;
            //}
            SolrQuery field = new SolrQuery(empQuery.FieldName + ":" + _queryCreater.ToString());

            //Not query Search
            if (empQuery.NotQuery)
                return (SolrQuery.All && !field);
            else
                return field;
        }
    }

}

