using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DASolr = ATS.DataAccess.SolrAccess;
using BESolr = ATS.BusinessEntity.SolrEntity;

namespace ATS.BusinessLogic.SolrBase
{
    public class SolrSearchFieldsAction : ActionBase
    {
         #region private data member
        private DASolr.SolrSearchFieldsRepository _SolrSearchFieldsRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor

        public SolrSearchFieldsAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _SolrSearchFieldsRepository = new DASolr.SolrSearchFieldsRepository(base.ConnectionString);
            _SolrSearchFieldsRepository.CurrentUser = base.CurrentUser;
            _SolrSearchFieldsRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public List<BESolr.SolrSearchFields> GetAlLSaveJobDetailByUserAndLanguage(Guid userId, Guid languageId)
        {
            try
            {
                return _SolrSearchFieldsRepository.GetAlLSaveJobDetailByUserAndLanguage(userId, languageId);
            }
            catch
            {
                throw;
            }
        }
    }
}
