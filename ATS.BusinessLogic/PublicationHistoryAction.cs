using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;


namespace ATS.BusinessLogic
{
    public class PublicationHistoryAction:ActionBase
    {
        #region private data member
        private DAClient.PublicationHistoryRepository _PublicationHistoryRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor

        public PublicationHistoryAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _PublicationHistoryRepository = new DAClient.PublicationHistoryRepository(base.ConnectionString);
            _PublicationHistoryRepository.CurrentUser = base.CurrentUser;
            _PublicationHistoryRepository.CurrentClient = base.CurrentClient;

        }
        #endregion

        public List<BEClient.PublicationHistory> GePublicationHistoryByProfileId(Guid ProfileId)
        {
            try
            {
                return _PublicationHistoryRepository.GetPublicationHistoryByProfileId(ProfileId);
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
                return Convert.ToBoolean(_common.IsRecordExists("PublicationHistory", "PublicationHistoryId", recordid));
            }
            catch (Exception)
            {

                throw;
            }
        }



        public bool DeletePublicationHistory(Guid recordid)
        {
            try
            {
                _PublicationHistoryRepository.BeginTransaction();
                bool isRecordDeleted = _PublicationHistoryRepository.DeletePublicationHistory(recordid);

                if (isRecordDeleted)
                {
                    _PublicationHistoryRepository.CommitTransaction();
                    return isRecordDeleted;
                }
                else
                {
                    _PublicationHistoryRepository.RollbackTransaction();
                    return isRecordDeleted;
                }

            }
            catch
            {
                _PublicationHistoryRepository.RollbackTransaction();
                throw;
            }
        }


        public bool DeleteAllPublicationHistory(Guid profileid)
        {
            try
            {
                _PublicationHistoryRepository.BeginTransaction();
                bool isRecordDeleted = _PublicationHistoryRepository.DeleteAllPublicationHistories(profileid);

                if (isRecordDeleted)
                {
                    _PublicationHistoryRepository.CommitTransaction();
                    return isRecordDeleted;
                }
                else
                {
                    _PublicationHistoryRepository.RollbackTransaction();
                    return isRecordDeleted;
                }

            }
            catch
            {
                _PublicationHistoryRepository.RollbackTransaction();
                throw;
            }
        }
        public Guid AddPublicationhistory(BEClient.PublicationHistory publicationhistory)
        {
            try
            {
                _PublicationHistoryRepository.BeginTransaction();
                Guid UserId = publicationhistory.UserId;
                if (_PublicationHistoryRepository.CurrentUser == null)
                {

                    _PublicationHistoryRepository.CurrentUser = new BEClient.User() { UserId = UserId };
                }
                Guid newpublicationhistoryId = _PublicationHistoryRepository.AddPublicationsHistory(publicationhistory);
                if (!newpublicationhistoryId.Equals(Guid.Empty))
                {
                    _PublicationHistoryRepository.CommitTransaction();
                    return newpublicationhistoryId;
                }
                else
                {
                    _PublicationHistoryRepository.RollbackTransaction();
                    return newpublicationhistoryId;
                }

            }
            catch
            {
                _PublicationHistoryRepository.RollbackTransaction();
                throw;
            }
        }

        public bool UpdatePublicationHistory(BEClient.PublicationHistory publicationhistory)
        {
            try
            {
                _PublicationHistoryRepository.BeginTransaction();
                Guid UserId = publicationhistory.UserId;
                if (_PublicationHistoryRepository.CurrentUser == null)
                {

                    _PublicationHistoryRepository.CurrentUser = new BEClient.User() { UserId = UserId };
                }
                bool IsPublicationHistoryUpdated = _PublicationHistoryRepository.UpdatePublicationsHistory(publicationhistory);
                if (IsPublicationHistoryUpdated)
                {
                    _PublicationHistoryRepository.CommitTransaction();
                    return IsPublicationHistoryUpdated;
                }
                else
                {
                    _PublicationHistoryRepository.RollbackTransaction();
                    return IsPublicationHistoryUpdated;
                }
            }
            catch
            {
                _PublicationHistoryRepository.RollbackTransaction();
                throw;
            }
        }


    }
}
