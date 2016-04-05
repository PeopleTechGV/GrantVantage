using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic
{
    public class RecordExistsAction : ActionBase
    {
        #region private data member
        private DAClient.RecordExistsRepository _RecordExistsRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        public RecordExistsAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _RecordExistsRepository = new DAClient.RecordExistsRepository(base.ConnectionString);
            _RecordExistsRepository.CurrentUser = base.CurrentUser;
            _RecordExistsRepository.CurrentClient = base.CurrentClient;

        }

        public List<BEClient.RecordExists> GetRecordsCountByVacancyStatus(Guid VacancyStatusId)
        {
            try
            {
                return _RecordExistsRepository.GetRecordsCountByVacancyStatus(VacancyStatusId);
            }
            catch
            {
                throw;
            }
        }
        public List<BEClient.RecordExists> GetRecordsCountByLocation(Guid JobLocationId)
        {
            try
            {
                return _RecordExistsRepository.GetRecordsCountByLocation(JobLocationId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.RecordExists> GetRecordsCountByPositionType(Guid PositionTypeId)
        {
            try
            {
                return _RecordExistsRepository.GetRecordsCountByPositionType(PositionTypeId);
            }
            catch
            {
                throw;
            }
        }
        public List<BEClient.RecordExists> GetRecordsCountByDegreeType(Guid DegreeTypeId)
        {
            try
            {
                return _RecordExistsRepository.GetRecordsCountByDegreeType(DegreeTypeId);
            }
            catch
            {
                throw;
            }
        }
        public List<BEClient.RecordExists> GetRecordsCountBySkillType(Guid SkillTypeId)
        {
            try
            {
                return _RecordExistsRepository.GetRecordsCountBySkillType(SkillTypeId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.RecordExists> GetRecordsCountByDivision(Guid DivisionId)
        {
            try
            {
                return _RecordExistsRepository.GetRecordsCountByDivision(DivisionId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.RecordExists> GetRecordsCountByUser(Guid UserId)
        {
            try
            {
                return _RecordExistsRepository.GetRecordsCountByUsers(UserId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.RecordExists> GetRecordsCountByVacancy(Guid VacancyId)
        {
            try
            {
                return _RecordExistsRepository.GetRecordsCountByVacancy(VacancyId);
            }
            catch
            {
                throw;
            }
        }
    }
}
