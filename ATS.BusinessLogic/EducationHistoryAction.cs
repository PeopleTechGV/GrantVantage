using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic
{
    public class EducationHistoryAction:ActionBase
    {
         #region private data member
        private DAClient.EducationHistoryRepository _EducationHistoryRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor

        public EducationHistoryAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _EducationHistoryRepository = new DAClient.EducationHistoryRepository(base.ConnectionString);
            _EducationHistoryRepository.CurrentUser = base.CurrentUser;
            _EducationHistoryRepository.CurrentClient = base.CurrentClient;

        }
        #endregion

        public List<BEClient.EducationHistory> GetEducationHistoryByProfileId(Guid ProfileId)
        {
            try
            {
                return _EducationHistoryRepository.GetEducationHistoryByProfileId(ProfileId);
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
                return Convert.ToBoolean(_common.IsRecordExists("EducationHistory", "EducationHistoryId", recordid));
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region CRUD
        public Guid AddEducationHistory(BEClient.EducationHistory pEducationHistory)
        {
            try
            {
                _EducationHistoryRepository.BeginTransaction();
                Guid UserId = pEducationHistory.UserId;
                if (_EducationHistoryRepository.CurrentUser == null)
                {

                    _EducationHistoryRepository.CurrentUser = new BEClient.User() { UserId = UserId };
                }
                Guid newEducationId =  _EducationHistoryRepository.AddEducationHistory(pEducationHistory);
                if(newEducationId.Equals(Guid.Empty))
                {
                    _EducationHistoryRepository.RollbackTransaction();
                    return newEducationId;
                }
                else
                {
                    _EducationHistoryRepository.CommitTransaction();
                    return newEducationId;

                }
                
            }
            catch
            {
                throw;
            }
        }
         public bool UpdateEducationHistory(BEClient.EducationHistory educationhistory)
        {
            try
            {
                _EducationHistoryRepository.BeginTransaction();
                Guid UserId = educationhistory.UserId;
                if (_EducationHistoryRepository.CurrentUser == null)
                {

                    _EducationHistoryRepository.CurrentUser = new BEClient.User() { UserId = UserId };
                }
                bool IsEducationHistoryUpdated = _EducationHistoryRepository.UpdateEducationHistory(educationhistory);
                if (IsEducationHistoryUpdated)
                {
                    _EducationHistoryRepository.CommitTransaction();
                    return IsEducationHistoryUpdated;
                }
                else
                {
                    _EducationHistoryRepository.RollbackTransaction();
                    return IsEducationHistoryUpdated;
                }
            }
            catch
            {
                _EducationHistoryRepository.RollbackTransaction();
                throw;
            }
        }

         public bool DeleteEducationHistory(Guid recordid)
         {
             try
             {
                 _EducationHistoryRepository.BeginTransaction();
                 bool isRecordDeleted = _EducationHistoryRepository.DeleteEducationHistory(recordid);

                 if (isRecordDeleted)
                 {
                     _EducationHistoryRepository.CommitTransaction();
                     return isRecordDeleted;
                 }
                 else
                 {
                     _EducationHistoryRepository.RollbackTransaction();
                     return isRecordDeleted;
                 }

             }
             catch
             {
                 _EducationHistoryRepository.RollbackTransaction();
                 throw;
             }
         }

         public bool DeleteAllEducationHistory(Guid profileid)
         {
             try
             {
                 _EducationHistoryRepository.BeginTransaction();
                 bool isRecordDeleted = _EducationHistoryRepository.DeleteAllEducationHistories(profileid);

                 if (isRecordDeleted)
                 {
                     _EducationHistoryRepository.CommitTransaction();
                     return isRecordDeleted;
                 }
                 else
                 {
                     _EducationHistoryRepository.RollbackTransaction();
                     return isRecordDeleted;
                 }

             }
             catch
             {
                 _EducationHistoryRepository.RollbackTransaction();
                 throw;
             }
         }

    }
        #endregion

    }

