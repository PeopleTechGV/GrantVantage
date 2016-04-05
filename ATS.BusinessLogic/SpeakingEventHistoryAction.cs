using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;
namespace ATS.BusinessLogic
{
    public class SpeakingEventHistoryAction:ActionBase
    {
        #region private data member
        private DAClient.SpeakingEventHistoryRepository _SpeakingEventHistoryRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        
         #region Constructor

        public SpeakingEventHistoryAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _SpeakingEventHistoryRepository = new DAClient.SpeakingEventHistoryRepository(base.ConnectionString);
            _SpeakingEventHistoryRepository.CurrentUser = base.CurrentUser;
            _SpeakingEventHistoryRepository.CurrentClient = base.CurrentClient;

        }
        #endregion

        public List<BEClient.SpeakingEventHistory> GetSpeakingEventHistoryByProfileId(Guid ProfileId)
        {
            try
            {
                return _SpeakingEventHistoryRepository.GetSpeakingEventHistoryByProfileId(ProfileId);
            }
            catch
            {
                throw;
            }
        }


        public bool IsRecordExists(Guid recordid)
        {
            try
            {
                DAClient.CommonRepository _common = new DAClient.CommonRepository(base.ConnectionString);
                return Convert.ToBoolean(_common.IsRecordExists("SpeakingEventHistory", "SpeakingEventHistoryId", recordid));
            }
            catch (Exception)
            {

                throw;
            }
        }


        public Guid AddSpeakingEventHistory(BEClient.SpeakingEventHistory speakingeventhistory)
        {
            try
            {
                _SpeakingEventHistoryRepository.BeginTransaction();
                Guid UserId = speakingeventhistory.UserId;
                if (_SpeakingEventHistoryRepository.CurrentUser == null)
                {

                    _SpeakingEventHistoryRepository.CurrentUser = new BEClient.User() { UserId = UserId };
                }
                Guid newSpeakingHistoryId = _SpeakingEventHistoryRepository.AddSpeakingEventHistory(speakingeventhistory);
                if (!newSpeakingHistoryId.Equals(Guid.Empty))
                {
                    _SpeakingEventHistoryRepository.CommitTransaction();
                    return newSpeakingHistoryId;
                }
                else
                {
                    _SpeakingEventHistoryRepository.RollbackTransaction();
                    return newSpeakingHistoryId;
                }

            }
            catch
            {
                _SpeakingEventHistoryRepository.RollbackTransaction();
                throw;
            }
        }


        public bool UpdateSpeakingHistory(BEClient.SpeakingEventHistory speakingeventhistory)
        {
            try
            {

                _SpeakingEventHistoryRepository.BeginTransaction();
                Guid UserId = speakingeventhistory.UserId;
                if (_SpeakingEventHistoryRepository.CurrentUser == null)
                {

                    _SpeakingEventHistoryRepository.CurrentUser = new BEClient.User() { UserId = UserId };
                }
                bool IsSpeakingEventHistoryUpdated = _SpeakingEventHistoryRepository.UpdateSpeakingEventHistory(speakingeventhistory);
                if (IsSpeakingEventHistoryUpdated)
                {
                    _SpeakingEventHistoryRepository.CommitTransaction();
                    return IsSpeakingEventHistoryUpdated;
                }
                else
                {
                    _SpeakingEventHistoryRepository.RollbackTransaction();
                    return IsSpeakingEventHistoryUpdated;
                }
            }
            catch
            {
                _SpeakingEventHistoryRepository.RollbackTransaction();
                throw;
            }
        }

        public bool DeleteSpeakingEventHistory(Guid recordid)
        {
            try
            {
                _SpeakingEventHistoryRepository.BeginTransaction();
                bool isRecordDeleted = _SpeakingEventHistoryRepository.DeleteSpeakingEventHistory(recordid);

                if (isRecordDeleted)
                {
                    _SpeakingEventHistoryRepository.CommitTransaction();
                    return isRecordDeleted;
                }
                else
                {
                    _SpeakingEventHistoryRepository.RollbackTransaction();
                    return isRecordDeleted;
                }

            }
            catch
            {
                _SpeakingEventHistoryRepository.RollbackTransaction();
                throw;
            }
        }

        public bool DeleteAllSpeakingEventHistory(Guid profileid)
        {
            try
            {
                _SpeakingEventHistoryRepository.BeginTransaction();
                bool isRecordDeleted = _SpeakingEventHistoryRepository.DeleteAllSpeakingEventHistory(profileid);

                if (isRecordDeleted)
                {
                    _SpeakingEventHistoryRepository.CommitTransaction();
                    return isRecordDeleted;
                }
                else
                {
                    _SpeakingEventHistoryRepository.RollbackTransaction();
                    return isRecordDeleted;
                }

            }
            catch
            {
                _SpeakingEventHistoryRepository.RollbackTransaction();
                throw;
            }
        }
    }
}
