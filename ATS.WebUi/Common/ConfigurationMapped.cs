using System;
using BE = ATS.BusinessEntity;
using BL = ATS.BusinessLogic;
using System.Web.Mvc;
using System.Linq;
using System.Web;
using System.IO;
using System.Reflection;
using System.Text;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

using System.Text.RegularExpressions;
using System.Configuration;


namespace ATS.WebUi.Common
{
    public class ConfigurationMapped
    {
        private const string KEY_ATS_JOB_SOLR_URL = "ATSJobSolrUrl";
        private const string KEY_ATS_EMPLOYEE_SOLR_URL = "ATSEmployeeSolrUrl";
        private const string KEY_RChilli_SERVICEURL = "ServiceUrl";
        private const string KEY_RChilli_USERKEY = "UserKey";
        private const string KEY_RChilli_SUBUSERID = "SubUserId";
        private const string KEY_RChilli_VERSION = "Version";

        private const string KEY_WKHTMLTOPDF = "WKHtmltopdfPath";
        private const string KEY_DOMAINNAME = "DomainName";
        private const string KEY_RESUMEURL= "ResumeURL";

        #region Private DataMember
        static ConfigurationMapped m_Instance = null;
        Configuration m_Configuration { get; set; }
        #endregion

        #region Singleton
        public static ConfigurationMapped Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new ConfigurationMapped();

                }
                return m_Instance;
            }
            set
            {
                m_Instance = value;
            }
        }
        #endregion

        ConfigurationMapped()
        {
            ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
            configMap.ExeConfigFilename = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;//System.Reflection.Assembly.GetExecutingAssembly().Location + ".config";
            m_Configuration = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
        }
        public string DomianName
        {
            get
            {
                return Convert.ToString(GetConfigurationValue(KEY_DOMAINNAME, string.Empty));
            }
        }
        public string WKHtmltopdfPath
        {
            get
            {
                return Convert.ToString(GetConfigurationValue(KEY_WKHTMLTOPDF, string.Empty));
            }
        }
        public string ATSSolrUrl
        {
            get
            {
                return Convert.ToString(GetConfigurationValue(KEY_ATS_JOB_SOLR_URL, string.Empty));
            }
        }
        public string ATSEmployeeSolrUrl
        {
            get
            {
                return Convert.ToString(GetConfigurationValue(KEY_ATS_EMPLOYEE_SOLR_URL, string.Empty));
            }
        }
        #region Get RChilli value from Webconfig
        public string RChilliServiceUrl
        {
            get
            {
                return Convert.ToString(GetConfigurationValue(KEY_RChilli_SERVICEURL, string.Empty));
            }
        }
        public string RChilliUserKey
        {
            get
            {
                return Convert.ToString(GetConfigurationValue(KEY_RChilli_USERKEY, string.Empty));
            }
        }
        public string RChilliVersion
        {
            get
            {
                return Convert.ToString(GetConfigurationValue(KEY_RChilli_VERSION, string.Empty));
            }
        }
        public string RChilliSubUserId
        {
            get
            {
                return Convert.ToString(GetConfigurationValue(KEY_RChilli_SUBUSERID, string.Empty));
            }
        }
        #endregion

        #region Get ResumeURL
        public string ResumeURL
        {
            get
            {
                return Convert.ToString(GetConfigurationValue(KEY_RESUMEURL, string.Empty));
            }
        }
        #endregion

        #region private methods
        private string GetConfigurationValue(string key, string defaultvalue)
        {
            if (m_Configuration.AppSettings.Settings[key] != null && !string.IsNullOrEmpty(m_Configuration.AppSettings.Settings[key].Value))
                return m_Configuration.AppSettings.Settings[key].Value;
            else
                return defaultvalue;
        }
        #endregion
    }
}