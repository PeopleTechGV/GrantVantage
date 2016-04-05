
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using SolrEntity = ATS.BusinessEntity.SolrEntity;
using ATSCommon = ATS.WebUi.Common;

using ATSHelper = ATS.HelperLibrary;
using System.Web;
using BLSolrBase = ATS.BusinessLogic.SolrBase;
using ATS.WebUi.Helpers;


namespace ATS.WebUi.Models
{
    public class SearchParametersBinder : IModelBinder
    {
        public const int DefaultPageSize = SolrEntity.SearchParameter.DefaultPageSize;

        public IDictionary<string, string> NVToDict(NameValueCollection nv)
        {
            var d = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
            foreach (var k in nv.AllKeys)
                d[k] = nv[k];
            return d;
        }

        private static readonly Regex FacetRegex = new Regex("^f_", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            HttpRequestBase Request = controllerContext.RequestContext.HttpContext.Request;

            if (Request.HttpMethod.ToUpper().ToString() == "POST")
            {
                SolrEntity.ICommonSearchParameter sp = new SolrEntity.SearchParameter();
                BLSolrBase .SolrResultMaker.SearchModelBinder(ref sp, Request.Form);
                return sp;
            }
            else
            {
                var qs = controllerContext.HttpContext.Request.QueryString;
                var qsDict = NVToDict(qs);
                var sp = new SolrEntity.SearchParameter
                {
                    Location = StringHelper.EmptyToNull(qs["Location"]),
                    PageIndex = StringHelper.TryParse(qs["page"], 1),
                    PageSize = StringHelper.TryParse(qs["pageSize"], 5),
                    Sort = StringHelper.EmptyToNull(qs["sort"]),
                    Facets = qsDict.Where(k => FacetRegex.IsMatch(k.Key))
                        .Select(k => k.WithKey(FacetRegex.Replace(k.Key, "")))
                        .ToDictionary()
                };
                return sp;
            }
          
         
        }
    }
}