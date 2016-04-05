using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;
using BESrp = ATS.BusinessEntity.SRPEntity;

namespace ATS.BusinessLogic
{
    public class LanguageBlocksAction : ActionBase
    {
        #region private data member
        private DAClient.LanguageBlocksRepository _LanguageBlocksRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor
        public LanguageBlocksAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _LanguageBlocksRepository = new DAClient.LanguageBlocksRepository(base.ConnectionString);
            _LanguageBlocksRepository.CurrentUser = base.CurrentUser;
            _LanguageBlocksRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public bool UpdateLanguageBlock(BEClient.LanguageBlocks objLanguageBlock)
        {
            try
            {
                _LanguageBlocksRepository.BeginTransaction();
                bool recordUpdated = _LanguageBlocksRepository.UpdateLanguageBlock(objLanguageBlock);
                _LanguageBlocksRepository.CommitTransaction();
                return recordUpdated;
            }
            catch
            {
                throw;
            }
        }

        public BEClient.LanguageBlocks GetLanguageBlockByRecordId(Guid LanguageId, Guid recordId)
        {
            try
            {
                BEClient.LanguageBlocks objLanguageBlock = _LanguageBlocksRepository.GetLanguageBlockByRecordId(recordId, LanguageId);
                return objLanguageBlock;
            }
            catch
            {
                throw;
            }
        }
        public string GetLanguageBlockByBlockIdentifier(Guid LanguageId, string BlockIdentifier)
        {
            try
            {
                return _LanguageBlocksRepository.GetLanguageBlockByBlockIdentifier(LanguageId, BlockIdentifier);
            }
            catch
            {
                throw;
            }
        }

        public string GetBlockDescriptionByBlockIdentifier(Guid LanguageId, string BlockIdentifier)
        {
            try
            {
                return _LanguageBlocksRepository.GetBlockDescriptionByBlockIdentifier(LanguageId, BlockIdentifier);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.LanguageBlocks> GetAllLanguageBlock(Guid LanguageId)
        {
            try
            {
                return _LanguageBlocksRepository.GetAllLanguageBlock(LanguageId, base.CurrentClient.ClientId);
            }
            catch
            {
                throw;
            }
        }
    }
}
