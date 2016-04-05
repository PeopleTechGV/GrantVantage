using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using System.Configuration;
using ATS.WebUi.App_Start;
using BLMaster = ATS.BusinessLogic.Master;
using BLCommon = ATS.BusinessLogic.Common;
using BEMaster = ATS.BusinessEntity.Master;
using System.Xml;
using System.IO;
using SolrEntity = ATS.BusinessEntity.SolrEntity;
using SolrModel = ATS.WebUi.Models;
using SolrNet;
using ATSCommon = ATS.WebUi.Common;
using ATS.BusinessEntity;
using System.ComponentModel.DataAnnotations;
using ATS.BusinessEntity.Common;

namespace ATS.WebUi
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {

        public static Dictionary<string, string> breadcrum = new Dictionary<string, string>();

        private readonly string _ATSSolrUrl = ATSCommon.ConfigurationMapped.Instance.ATSSolrUrl;
        private readonly string _ATSSolrEmployeeUrl = ATSCommon.ConfigurationMapped.Instance.ATSEmployeeSolrUrl;

        public static ISolrReadOnlyOperations<SolrEntity.SolrSearchFields> ATSSolrConnection
        {
            get;
            internal set;
        }

        public static ISolrReadOnlyOperations<SolrEntity.SolrEmployeeSearchFields> ATSSolrEmployeeConnection
        {
            get;
            internal set;
        }

        protected void Application_Start()
        {

            breadcrum.Add("Account/Index", "~/Content/images/Home_button_24.png");
            breadcrum.Add("Home/Index", "~/Content/images/Home_button_24.png");
            breadcrum.Add("Search/Index", "/Content/images/Search_Jobs_24.png");

            GenerateLngXML();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //BundleTable.EnableOptimizations = true;
            log4net.Config.XmlConfigurator.Configure();
            #region Solr Registration

            ATSSolrConnection = new ATS.BusinessLogic.SolrBase.SolrBaseInstance.Instance<SolrEntity.SolrSearchFields>().ATSSearch(_ATSSolrUrl);
            ATSCommon.CommonFunctions._solrConnection = ATSSolrConnection;


            ModelBinders.Binders[typeof(SolrEntity.SearchParameter)] = new SolrModel.SearchParametersBinder();

            ATSSolrEmployeeConnection = new ATS.BusinessLogic.SolrBase.SolrBaseInstance.Instance<SolrEntity.SolrEmployeeSearchFields>().ATSEmployeeSearch(_ATSSolrEmployeeUrl);



            #endregion

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            BLCommon.CacheHelper.GetAllLanguageList();

            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(ATS.BusinessEntity.Attributes.ATSRequiredAttribute), typeof(RequiredAttributeAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(ATS.BusinessEntity.Attributes.ATSStringLengthAttribute), typeof(StringLengthAttributeAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(ATS.BusinessEntity.Attributes.ATSRegularExpressionAttribute), typeof(RegularExpressionAttributeAdapter));

        }

        protected void GenerateLngXML()
        {
            

            
            try
            {
                //BLMaster.LanguageAction objLanguage = new BLMaster.LanguageAction();
                //List<BEMaster.Language> objLanguageList = objLanguage.GetAllLanguage();

                //foreach (BEMaster.Language objLng in objLanguageList)
                //{
                if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, XMLResources.directory_Name)))
                {
                    Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, XMLResources.directory_Name));
                    GenereateFile();
                }
                else
                {
                    if (!File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, XMLResources.directory_Name, XMLResources.file_Name)))
                    {
                        GenereateFile();
                    }
                    else
                    {
                        File.Delete(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, XMLResources.directory_Name, XMLResources.file_Name));
                        GenereateFile();
                    }
                }
                //}
            }
            catch (Exception)
            {

                throw;
            }

        }

        protected void GenereateFile()
        {
            try
            {
                BLMaster.GenerateLanguageXML objGenerateLanguageXML = new BLMaster.GenerateLanguageXML();
                XmlDocument XD = objGenerateLanguageXML.GeneratelanguageXML();
                XD.Save(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, XMLResources.directory_Name, XMLResources.file_Name));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}