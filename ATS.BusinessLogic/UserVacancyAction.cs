using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;
using System.Data.Common;
using System.Data;

namespace ATS.BusinessLogic 
{
    public class UserVacancyAction : ActionBase
    {
        #region private data member
        private DAClient.UserVacancyRepository _UserVacancyRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor

        public UserVacancyAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _UserVacancyRepository = new DAClient.UserVacancyRepository(base.ConnectionString);
            _UserVacancyRepository.CurrentUser = base.CurrentUser;
            _UserVacancyRepository.CurrentClient = base.CurrentClient;

        }
        #endregion

        public Guid InsertUserVacancy(BEClient.UserVacancy pUserVacancy,Guid languageId)
        {
            try
            {
                _UserVacancyRepository.BeginTransaction();
                Guid UserVacancyId = Guid.Empty;

                UserVacancyId = _UserVacancyRepository.InsertUserVacancy(pUserVacancy, languageId, _UserVacancyRepository.CurrentUser.UserId, _MyClientId);
                if (UserVacancyId != Guid.Empty)
                {
                    _UserVacancyRepository.CommitTransaction();
                }
                else
                {
                    _UserVacancyRepository.RollbackTransaction();
                }
                return UserVacancyId;
            }
            catch
            {
                _UserVacancyRepository.RollbackTransaction();
                throw;
            }
        }

        public List<BEClient.UserVacancy> GetAllSaveJobByUserAndLanguage(Guid userId,Guid languageId)
        {
            try
            {
                return _UserVacancyRepository.GetAllSaveJobByUserAndLanguage(userId,languageId);
            }
            catch
            {
                throw;
            }
        }

        public bool UnSaveJob(Guid VacancyId, Guid UserId)
        {
            try
            {
                return _UserVacancyRepository.UnSaveJob(VacancyId, UserId);
            }
            catch
            {
                throw;
            }
        }
    }
}
