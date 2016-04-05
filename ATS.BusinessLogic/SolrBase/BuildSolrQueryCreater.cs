using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolrNet;
using SolrEntity = ATS.BusinessEntity.SolrEntity;
using System.Text.RegularExpressions;
namespace ATS.BusinessLogic.SolrBase
{
    public static class BuildSolrQueryCreater
    {
        public static Dictionary<int, string> FLocationDropDown { get; set; }

        public static SortOrder[] GetSelectedSort(SolrEntity.SearchParameter parameters)
        {
            if (parameters.Sort != null)
                return (parameters.Sort.Split(',').ToList<string>()).Select<string, SortOrder>(s => SortOrder.Parse(s.Trim())).ToArray();
            else
            {
                return new[] { SortOrder.Parse(parameters.Sort) }.Where(o => o != null).ToArray();
            }
        }
        /// <summary>
        /// It will return the query ISolrQuery. It will generate AND query between all criteria
        /// </summary>
        /// <param name="Location">Search in Location Fields of solr</param>
        /// <param name="PositionType">Search in Location Fields of solr</param>
        /// <param name="EmployeementType">Search in Location Fields of solr</param>
        /// <param name="FreeQuery">Search on multiple fields of solr</param>
        /// <param name="salaryRange">Salaryrange contains two values [0] - MinSalary AND [1] - Max salary it will be in double format</param>
        /// <param name="dateRange">Daterange contains two values [0] - MinDate AND [1] - MaxDate it will be in datetime format</param>
        /// <returns>It will return object of ISolrQuery</returns>
        public static ISolrQuery GenerateCommonQuery(String Location, String PositionType, String Fptime, String EmployeementType, String FreeQuery, decimal[] salaryRange, DateTime[] dateRange, Dictionary<int, String> locaionList)
        {
            //Mapping
            //FullLocation -> Job Location
            //PositionTypeText-> Position Type 
            //Full time Part Time-> EmploymentTypeText 
            //Employement Type-> JobTypeText

            List<ISolrQuery> myQueryList = new List<ISolrQuery>();
            List<ISolrQuery> myLocationQueryList = new List<ISolrQuery>();
            List<ISolrQuery> myPositionTypeQueryList = new List<ISolrQuery>();

            //It will not create any query if salary is same as 0.0
            decimal total = salaryRange[0] + salaryRange[1];
            if (total > 0)
            {
                var minSalary = new SolrQueryByRange<double>("SalaryMin", Convert.ToDouble(salaryRange[0]), Convert.ToDouble(salaryRange[1]));
                var maxSalary = new SolrQueryByRange<double>("SalaryMax", Convert.ToDouble(salaryRange[0]), Convert.ToDouble(salaryRange[1]));
                SolrMultipleCriteriaQuery multiQueries = new SolrMultipleCriteriaQuery(new List<ISolrQuery>() { minSalary, maxSalary }, "OR");
                myQueryList.Add(multiQueries);

            }
            //It will not create any query if date is same as minimum date
            if (dateRange[0] != DateTime.MinValue && dateRange[1] != DateTime.MinValue)
            {
                DateTime minDate = dateRange[0];
                minDate = minDate.ToUniversalTime();
                DateTime maxDate = dateRange[1];
                maxDate = maxDate.ToUniversalTime();
                myQueryList.Add(new SolrQueryByRange<DateTime>("PostedOn", minDate, maxDate));
            }
            if (!String.IsNullOrEmpty(FreeQuery))
            {

                List<SolrQuery> query = new List<SolrQuery>(); ;
                foreach (string frQ in FreeQuery.Split(','))
                {
                    query.Add(new SolrQuery(frQ));
                }
                SolrMultipleCriteriaQuery multiQueries = new SolrMultipleCriteriaQuery(query, "OR");
                myQueryList.Add(multiQueries);
                //String search = Regex.Replace(FreeQuery, "[^a-zA-Z0-9_]+", "*");

            }
            if (Location != null)
            {
                foreach (string loc in Location.Split(','))
                {
                    int x = 0;
                    if (Int32.TryParse(loc, out x) && locaionList.ContainsKey(Convert.ToInt32(x)))
                        myLocationQueryList.Add(new SolrQueryByField("FullLocation", locaionList[Convert.ToInt32(x)]));
                }
            }
            if (PositionType != null)
            {
                foreach (string positionType in PositionType.Split(','))
                    myPositionTypeQueryList.Add(new SolrQueryByField("FullPositionTypeText", positionType));
            }
            //Mapping
            //FullLocation -> Job Location
            //PositionTypeText-> Position Type 
            //EmploymentTypeText - > Full time Part Time
            //JobTypeText -> EmployementType

            myQueryList.Add(new SolrQueryByField("EmploymentTypeText", Fptime));
            myQueryList.Add(new SolrQueryByField("JobTypeText", EmployeementType));



            SolrMultipleCriteriaQuery mixQuery = new SolrMultipleCriteriaQuery(myQueryList, "AND");

            SolrMultipleCriteriaQuery LocationQuery = new SolrMultipleCriteriaQuery(myLocationQueryList);

            SolrMultipleCriteriaQuery PositionTypeQuery = new SolrMultipleCriteriaQuery(myPositionTypeQueryList);
            return mixQuery && LocationQuery && PositionTypeQuery;
        }
    }
}
