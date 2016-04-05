using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SolrNet;
using SolrEntity = ATS.BusinessEntity.SolrEntity;
using SolrBL = ATS.BusinessLogic.SolrBase;
using BEClient = ATS.BusinessEntity;
using BLClient = ATS.BusinessLogic;

namespace ATS.WebUi.Areas.Employee.Controllers
{
    public class HomeController : ATS.WebUi.Controllers.AreaBaseController
    {
        private readonly ISolrReadOnlyOperations<SolrEntity.SolrSearchFields> _solrConnection;
        public HomeController()
        {
            _solrConnection = ATS.WebUi.MvcApplication.ATSSolrConnection;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAllVacancyByCurrentClient()
        {
            SolrEntity.SearchParameter _Parameters = new SolrEntity.SearchParameter();
            _Parameters.PageSize = 100;
            _Parameters.Sort = "PostedOn desc";
            SolrQueryByField fieldQuery = new SolrQueryByField("FeaturedOnWeb", true.ToString());


            SolrEntity.SolrSearchObject view = SolrBL.SolrResultMaker.GetSearchJobsResultFromSolr(_solrConnection, fieldQuery, null, null, _Parameters, null);
            return View("_TitleDiscription", view);
        }
    }
}
