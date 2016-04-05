using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic
{
    public class BlockCandidateAction:ActionBase
    {
        #region private data member
        private DAClient.BlockCandidateRepository _BlockCandidateRepository;
        private Guid _MyClientId { get; set; }
        #endregion
        #region Constructor

        public BlockCandidateAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _BlockCandidateRepository = new DAClient.BlockCandidateRepository(base.ConnectionString);
            _BlockCandidateRepository.CurrentUser = base.CurrentUser;
            _BlockCandidateRepository.CurrentClient = base.CurrentClient;

        }
        #endregion
  
    
    public Guid AddBlockCandidate(BEClient.BlockCandidate blockcandidate)
    {
        try{
            _BlockCandidateRepository.BeginTransaction();
            Guid newBlockcandidateId= _BlockCandidateRepository.AddBlockCandidates(blockcandidate);

            _BlockCandidateRepository.CommitTransaction();
            return newBlockcandidateId;
            
        }
        catch
        {
            _BlockCandidateRepository.RollbackTransaction();
            throw;
        }
    }

    public bool DeleteBlockCandidate(Guid pUserId)
    {
        try
        {
            _BlockCandidateRepository.BeginTransaction();
            bool IsDeletedBlockcandidate = _BlockCandidateRepository.DeleteBlockCandidate(pUserId);

            _BlockCandidateRepository.CommitTransaction();
            return IsDeletedBlockcandidate;

        }
        catch
        {
            _BlockCandidateRepository.RollbackTransaction();
            throw;
        }
    }

        public List<BEClient.BlockCandidate> GetAllBlockCandidate(Guid CreatedById)
    {
            try
            {
                return _BlockCandidateRepository.GetAllBlockByCandidateUserId(CreatedById);

            }
            catch
            {
                throw;

            }
    }

    }
}
