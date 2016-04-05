using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic
{
    public class ATSResumeAction : ActionBase
    {
        #region private data member
        private DAClient.ATSResumeRepository _ATSResumeRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor

        public ATSResumeAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _ATSResumeRepository = new DAClient.ATSResumeRepository(base.ConnectionString);
            _ATSResumeRepository.CurrentUser = base.CurrentUser;
            _ATSResumeRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public BEClient.ATSResume GetATSResumeByUserAndProfile(Guid UserId, Guid ProfileId)
        {
            try
            {
                return _ATSResumeRepository.GetATSResumeByContactAndProfile(UserId, ProfileId);
            }
            catch
            {
                throw;
            }
        }

        public Guid UpdateATSResume(BEClient.ATSResume pATSResume, bool isCoverLetter = false)
        {
            try
            {
                return _ATSResumeRepository.UpdateATSResume(pATSResume, isCoverLetter);
            }
            catch
            {
                throw;
            }
        }


        public BEClient.ATSResume GetRecordByRecorId(Guid pRecordId)
        {
            try
            {
                return _ATSResumeRepository.GetRecordByRecordId(pRecordId);
            }
            catch
            {
                throw;
            }
        }

        public bool RemoveCoverLetter(Guid ATSResumeId)
        {
            try
            {
                return _ATSResumeRepository.RemoveCoverLetter(ATSResumeId);
            }
            catch
            {
                throw;
            }
        }
        public Guid AddATSResume(BEClient.ATSResume pATSResume)
        {
            try
            {
                _ATSResumeRepository.BeginTransaction();
                Guid UserId = pATSResume.UserId;
                if (_ATSResumeRepository.CurrentUser == null)
                {

                    _ATSResumeRepository.CurrentUser = new BEClient.User() { UserId = UserId };
                }

                Guid ATSResumeId = _ATSResumeRepository.AddATSresume(pATSResume, pATSResume.IsCoverLetter);
                _ATSResumeRepository.CommitTransaction();
                return ATSResumeId;
            }
            catch
            {
                _ATSResumeRepository.RollbackTransaction();
                throw;
            }
        }

        public BEClient.ATSResume GetResumeByProfile(Guid ProfileId)
        {
            try
            {
                return _ATSResumeRepository.GetATSResumeByProfile(ProfileId);
            }
            catch
            {
                throw;
            }
        }


        public BEClient.ATSResume GetATSResumeByVacancyAndLanguage(Guid VacancyId, Guid LanguageId)
        {
            try
            {
                return _ATSResumeRepository.GetATSResumeByVacancyAndLanguage(VacancyId, LanguageId);
            }
            catch
            {
                throw;
            }
        }
        public BEClient.ATSResume GetATSResumeByVacancyAndLanguage_GearBox(Guid VacancyId, Guid LanguageId, Guid ApplicationId)
        {
            try
            {
                return _ATSResumeRepository.GetATSResumeByVacancyAndLanguage_GearBox(VacancyId, LanguageId, ApplicationId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.ATSResume> GetAllProfile(Guid userId)
        {
            try
            {
                return _ATSResumeRepository.GetAllProfile(userId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.ATSResume> GetAllCoverLetter(Guid userId)
        {
            try
            {
                return _ATSResumeRepository.GetAllCoverLetter(userId);
            }
            catch
            {
                throw;
            }
        }


        public List<BEClient.ATSResume> GetAllDocuments(Guid userId)
        {
            try
            {
                return _ATSResumeRepository.GetAllDocuments(userId);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.ATSResume GetProfileIdAndUserIdbyAtsResumeId(Guid AtsResumeId)
        {
            try
            {
                return _ATSResumeRepository.GetProfileIdAndUserIdbyAtsResumeId(AtsResumeId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.ATSResume> GetAllDocsByUserId(Guid UserId)
        {
            try
            {
                return _ATSResumeRepository.GetAllDocsByUserId(UserId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.ATSResume> GetUserDocumentsByDocumentTypeId(Guid UserId, Guid DocumentTypeId)
        {
            try
            {
                return _ATSResumeRepository.GetUserDocumentsByDocumentTypeId(UserId, DocumentTypeId);
            }
            catch
            {
                throw;
            }
        }
    }
}
