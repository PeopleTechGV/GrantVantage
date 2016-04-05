using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAClient = ATS.DataAccess;
using BEClient = ATS.BusinessEntity;

namespace ATS.BusinessLogic
{
    public class SearchAction : ActionBase
    {

        #region private data member
        private DAClient.SearchRepository _SearchRepository;
        private Guid _MyClientId { get; set; }
        #endregion
        #region Constructor

        public SearchAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _SearchRepository = new DAClient.SearchRepository(base.ConnectionString);
            _SearchRepository.CurrentUser = base.CurrentUser;
            _SearchRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public Guid AddSearch(BEClient.Search pSearch, Guid languageId)
        {
            try
            {
                _SearchRepository.BeginTransaction();
                Guid _SearchId = Guid.Empty;
                _SearchId = _SearchRepository.Add(pSearch, languageId);
                if (_SearchId != Guid.Empty)
                {
                    _SearchRepository.CommitTransaction();
                }
                else
                {
                    _SearchRepository.RollbackTransaction();
                }
                return _SearchId;
            }
            catch
            {
                _SearchRepository.RollbackTransaction();
                throw;
            }
        }

        public BEClient.Search GetDefaultParamByLanguageAndUser(Guid languageId)
        {
            try
            {
                return _SearchRepository.GetDefaultParamByLanguageAndUser(languageId, _SearchRepository.CurrentUser.UserId, _MyClientId); ;
            }
            catch
            {
                throw;
            }
        }

    }
}
