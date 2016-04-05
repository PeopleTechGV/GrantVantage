using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolrNet;
using SolrNet.Utils;
using BEClient = ATS.BusinessEntity;

using BLMaster = ATS.BusinessLogic.Master;
using BEMaster = ATS.BusinessEntity.Master;

using ATSHelper = ATS.HelperLibrary;
namespace ATS.BusinessLogic.SolrBase
{

    public class BaseSolrConnection : ISolrConnection
    {
        private readonly ISolrConnection connection;
        private readonly String _currentConnection;
        public BaseSolrConnection(ISolrConnection connection, String currentConnection)
        {
            this.connection = connection;
            this._currentConnection = currentConnection;
        }

        public string Post(string relativeUrl, string s)
        {
            //      logger.DebugFormat("POSTing '{0}' to '{1}'", s, relativeUrl);
            return connection.Post(relativeUrl, s);
        }

        public string Get(string relativeUrl, IEnumerable<KeyValuePair<string, string>> parameters)
        {
            var stringParams = string.Join(", ", parameters.Select(p => string.Format("{0}={1}", p.Key, p.Value)).ToArray());
            //        logger.DebugFormat("GETting '{0}' from '{1}'", stringParams, relativeUrl);
            parameters = GetParams(parameters);
            return connection.Get(relativeUrl, parameters);
        }

        //      private static readonly ILog logger = LogManager.GetLogger(typeof (LoggingConnection));

        private IEnumerable<KeyValuePair<string, string>> GetParams(IEnumerable<KeyValuePair<string, string>> parameters)
        {
            BEMaster.Client myClient = null;
            
            Prompt.Shared.Utility.Library.Methods.GetVar<BEMaster.Client>(ATSHelper.Constant.SESSION_LOGGEDIN_CLIENT, ref myClient);

            string stringParams = string.Join(" || ", parameters.Select(p => string.Format("{0}={1}", p.Key, p.Value)).ToArray());
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            if (parameters != null)
            {
                list.AddRange(parameters);
            }
            KeyValuePair<string, string> query = list.Where(en => en.Key == "q").FirstOrDefault();

            String ClientQuery;
            String LanguageQuery;
            
            String _MyLanguageId = myClient.CurrentLanguageId.ToString().ToUpper();
            bool employeeSearch = false;
            if (this._currentConnection.ToLower() == "employee")
            {
                employeeSearch = true;
                
            }
            String _MyClientId = myClient.ClientId.ToString().ToUpper();
            if (query.Value.Contains('{'))
            {
                ClientQuery = "(ClientId:" + _MyClientId + ")";
                if (!employeeSearch)
                LanguageQuery = "(LanguageId:" + _MyLanguageId + ")";
                list.Add(new KeyValuePair<string, string>("fq", query.Value));
                bool result = list.Remove(query);
            }
            else
            {
                bool result = list.Remove(query);
                if (!employeeSearch)
                ClientQuery = "(" + query.Value + (!query.Value.Equals(String.Empty) ? " AND " : "") + " ClientId:" + _MyClientId + " AND LanguageId:" + _MyLanguageId + " )";
                else
                    ClientQuery = "(" + query.Value + (!query.Value.Equals(String.Empty) ? " AND " : "") + " ClientId:" + _MyClientId + " )";
            }

            KeyValuePair<string, string> queryWithCountry = new KeyValuePair<string, string>("q", ClientQuery);
            list.Insert(0, queryWithCountry);
            return list;
        }
        #region ISolrConnection Members


        public string PostStream(string relativeUrl, string contentType, System.IO.Stream content, IEnumerable<KeyValuePair<string, string>> getParameters)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}