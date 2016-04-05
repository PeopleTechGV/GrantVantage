using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;
namespace ATS.BusinessLogic
{
    public class ExecutiveSummaryAction : ActionBase
    {

        #region private data member
        private DAClient.ExecutiveSummaryRepository _ExecutiveSummaryRepository;
        private Guid _MyClientId { get; set; }
        #endregion


            #region Constructor

        public ExecutiveSummaryAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _ExecutiveSummaryRepository = new DAClient.ExecutiveSummaryRepository(base.ConnectionString);
            _ExecutiveSummaryRepository.CurrentUser = base.CurrentUser;
            _ExecutiveSummaryRepository.CurrentClient = base.CurrentClient;

        }
        #endregion

        public BEClient.ExecutiveSummary GetExecutiveSummaryByProfileId(Guid ProfileId)
        {
            try
            {
                return _ExecutiveSummaryRepository.GetExecutiveSummaryProfileId(ProfileId);
            }
            catch
            {
                throw;
            }
        }
        #region CRUD
        public Guid AddExecutiveSummary(BEClient.ExecutiveSummary executivesummary)
        {
            try
            {
                _ExecutiveSummaryRepository.BeginTransaction();
                Guid UserId = executivesummary.CreatedBy;
                if (_ExecutiveSummaryRepository.CurrentUser == null)
                {

                    _ExecutiveSummaryRepository.CurrentUser = new BEClient.User() { UserId = UserId };
                }

                Guid ExecutiveSummaryId = _ExecutiveSummaryRepository.AddExecutiveSummary(executivesummary);

                _ExecutiveSummaryRepository.CommitTransaction();
                return ExecutiveSummaryId;



            }
            catch
            {
                _ExecutiveSummaryRepository.RollbackTransaction();
                throw;
            }
        }


        public bool UpdateExecutiveSummary(BEClient.ExecutiveSummary objExecutionSummary)
        {

            try
            {
                _ExecutiveSummaryRepository.BeginTransaction();
                Guid UserId = objExecutionSummary.UserId;
                if (_ExecutiveSummaryRepository.CurrentUser == null)
                {

                    _ExecutiveSummaryRepository.CurrentUser = new BEClient.User() { UserId = UserId };
                }
                bool IsExecutionSummaryUpdated = _ExecutiveSummaryRepository.UpdateExecutiveSummary(objExecutionSummary);
                if (IsExecutionSummaryUpdated)
                {
                    _ExecutiveSummaryRepository.CommitTransaction();
                    return IsExecutionSummaryUpdated;
                }
                else
                {
                    _ExecutiveSummaryRepository.RollbackTransaction();
                    return IsExecutionSummaryUpdated;
               }

            }
            catch
            {
                _ExecutiveSummaryRepository.RollbackTransaction();
                throw;

            }
        }
        #endregion
    }
}
