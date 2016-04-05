using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;
namespace ATS.BusinessLogic
{
    public class AchievementAction : ActionBase
    {
        #region private data member
        private DAClient.AchievementRepository _AchievementRepository;
        private Guid _MyClientId { get; set; }
        #endregion
        #region Constructor

        public AchievementAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _AchievementRepository = new DAClient.AchievementRepository(base.ConnectionString);
            _AchievementRepository.CurrentUser = base.CurrentUser;
            _AchievementRepository.CurrentClient = base.CurrentClient;

        }
        #endregion
        public bool IsRecordExists(Guid recordid)
        {
            try
            {
                DAClient.CommonRepository _common = new DAClient.CommonRepository(base.ConnectionString);
                return Convert.ToBoolean(_common.IsRecordExists("Achievement", "AchievementId", recordid));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<BEClient.Achievement> GetAchievementByProfileId(Guid ProfileId)
        {
            try
            {
                return _AchievementRepository.GetAchievementByProfileId(ProfileId);
            }
            catch
            {
                throw;
            }
        }



        public Guid AddAchievement(BEClient.Achievement achievement)
        {
            try
            {
                _AchievementRepository.BeginTransaction();
                Guid UserId = achievement.UserId;
                if (_AchievementRepository.CurrentUser == null)
                {

                    _AchievementRepository.CurrentUser = new BEClient.User() { UserId = UserId };
                }
                Guid newAchievementId = _AchievementRepository.AddAchievements(achievement);
                if (!newAchievementId.Equals(Guid.Empty))
                {
                    _AchievementRepository.CommitTransaction();
                    return newAchievementId;
                }
                else
                {

                    _AchievementRepository.RollbackTransaction();
                    return newAchievementId;
                }
            }
            catch
            {
                _AchievementRepository.RollbackTransaction();
                throw;
            }
        }

        public bool UpdateAchievement(BEClient.Achievement achievement)
        {
            try
            {
                _AchievementRepository.BeginTransaction();
                Guid UserId = achievement.UserId;
                if (_AchievementRepository.CurrentUser == null)
                {

                    _AchievementRepository.CurrentUser = new BEClient.User() { UserId = UserId };
                }
                bool IsAchievementUpdated = _AchievementRepository.UpdateAchievements(achievement);
                if (IsAchievementUpdated)
                {
                    _AchievementRepository.CommitTransaction();
                    return IsAchievementUpdated;
                }
                else
                {
                    _AchievementRepository.RollbackTransaction();
                    return IsAchievementUpdated;
                }
            }
            catch
            {
                _AchievementRepository.RollbackTransaction();
                throw;
            }
        }


        public bool DeleteAchievements(Guid recordid)
        {
            try
            {
                _AchievementRepository.BeginTransaction();
                bool isRecordDeleted = _AchievementRepository.DeleteAchievements(recordid);
                if (isRecordDeleted)
                {
                    _AchievementRepository.CommitTransaction();
                    return isRecordDeleted;
                }
                else
                {
                    _AchievementRepository.RollbackTransaction();
                    return isRecordDeleted;
                }

            }
            catch
            {
                _AchievementRepository.RollbackTransaction();
                throw;
            }
        }

        public bool DeleteAllAchievements(Guid profileid)
        {
            try
            {
                _AchievementRepository.BeginTransaction();
                bool isRecordDeleted = _AchievementRepository.DeleteAllAchievement(profileid);
                if (isRecordDeleted)
                {
                    _AchievementRepository.CommitTransaction();
                    return isRecordDeleted;
                }
                else
                {
                    _AchievementRepository.RollbackTransaction();
                    return isRecordDeleted;
                }

            }
            catch
            {
                _AchievementRepository.RollbackTransaction();
                throw;
            }
        }

    }
}
