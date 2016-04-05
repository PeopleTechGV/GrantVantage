using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic
{
    public class EmploymentHistoryAction:ActionBase
    {
        #region private data member
        private DAClient.EmploymentHistoryRepository _EmploymentHistoryRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor

        public EmploymentHistoryAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _EmploymentHistoryRepository = new DAClient.EmploymentHistoryRepository(base.ConnectionString);
            _EmploymentHistoryRepository.CurrentUser = base.CurrentUser;
            _EmploymentHistoryRepository.CurrentClient = base.CurrentClient;

        }
        #endregion

        public bool IsRecordExists(Guid recordid)
        {
            try
            {
            DAClient.CommonRepository _common = new DAClient.CommonRepository(base.ConnectionString);
            return Convert.ToBoolean(_common.IsRecordExists("EmploymentHistory", "EmployementHistoryId", recordid));
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public List<BEClient.EmploymentHistory> GetEmploymentHistoryByProfileId(Guid ProfileId)
        {
            try
            {
                return _EmploymentHistoryRepository.GetEmploymentHistoryByProfileId(ProfileId);
            }
            catch
            {
                throw; 
            }
        }

        #region CRUD

        public Guid AddEmploymentHistory(BEClient.EmploymentHistory pEmploymentHistory)
        {
            try
            {
                _EmploymentHistoryRepository.BeginTransaction();
                Guid UserId = pEmploymentHistory.UserId;
                if (_EmploymentHistoryRepository.CurrentUser == null)
                {

                    _EmploymentHistoryRepository.CurrentUser = new BEClient.User() { UserId = UserId };
                }
                Guid newEmploymentId = _EmploymentHistoryRepository.AddEmploymentHistory(pEmploymentHistory);
                if(newEmploymentId.Equals(Guid.Empty))
                {
                    _EmploymentHistoryRepository.RollbackTransaction();
                    return newEmploymentId;
                }
                else
                {
                    _EmploymentHistoryRepository.CommitTransaction();
                    return newEmploymentId;
                }
                
            }
            catch
            {
                _EmploymentHistoryRepository.RollbackTransaction();

                throw;
            }
        }
        

        public bool UpdateEmploymentHistory(BEClient.EmploymentHistory employmenthistory)
        {
            try
            {
                _EmploymentHistoryRepository.BeginTransaction();
                Guid UserId = employmenthistory.UserId;
                if (_EmploymentHistoryRepository.CurrentUser == null)
                {

                    _EmploymentHistoryRepository.CurrentUser = new BEClient.User() { UserId = UserId };
                }
                bool IsEmploymentUpdated = _EmploymentHistoryRepository.UpdateEmploymentHistory(employmenthistory);
                if(IsEmploymentUpdated)
                {
                    _EmploymentHistoryRepository.CommitTransaction();
                    return IsEmploymentUpdated;
                }
                else
                {
                    _EmploymentHistoryRepository.RollbackTransaction();
                    return IsEmploymentUpdated;
                }
            }
            catch
            {
                _EmploymentHistoryRepository.RollbackTransaction();
                throw;
            }
        }




        public bool DeleteEmploymentHistory(Guid recordid)
        {
            try
            {
                _EmploymentHistoryRepository.BeginTransaction();
                bool isRecordDeleted = _EmploymentHistoryRepository.DeleteEmploymentHistory(recordid);

                if (isRecordDeleted)
                {
                    _EmploymentHistoryRepository.CommitTransaction();
                    return isRecordDeleted;
                }
                else
                {
                    _EmploymentHistoryRepository.RollbackTransaction();
                    return isRecordDeleted;
                }

            }
            catch
            {
                _EmploymentHistoryRepository.RollbackTransaction();
                throw;
            }
        }


        public bool DeleteAllEmploymentHistory(Guid profileId)
        {
            try
            {
                _EmploymentHistoryRepository.BeginTransaction();
                bool isRecordDeleted = _EmploymentHistoryRepository.DeleteAllEmploymentHistory(profileId);

                if (isRecordDeleted)
                {
                    _EmploymentHistoryRepository.CommitTransaction();
                    return isRecordDeleted;
                }
                else
                {
                    _EmploymentHistoryRepository.RollbackTransaction();
                    return isRecordDeleted;
                }

            }
            catch
            {
                _EmploymentHistoryRepository.RollbackTransaction();
                throw;
            }
        }
        #endregion
    }
}
