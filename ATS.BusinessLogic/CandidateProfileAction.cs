using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAClient = ATS.DataAccess;
using BEClient = ATS.BusinessEntity;

namespace ATS.BusinessLogic
{
    public class CandidateProfileAction : ActionBase
    {
        #region private data member

        private DAClient.AvailabilityRepository _AvailabilityRepository;
        private DAClient.ATSResumeRepository _ATSResumeRepository;

        private DAClient.ExecutiveSummaryRepository _ExecutiveSummaryRepository;
        private DAClient.ObjectiveRepository _ObjectiveRepository;
        private DAClient.EducationHistoryRepository _EducationHistoryRepository;
        private DAClient.LicenceAndCertificationsRepository _LicenceAndCertificationsRepository;
        private DAClient.PublicationHistoryRepository _PublicationHistoryRepository;
        private DAClient.SpeakingEventHistoryRepository _SpeakingEventHistoryRepository;
        private DAClient.LanguagesRepository _LanguagesRepository;
        private DAClient.AchievementRepository _AchievementRepository;
        private DAClient.AssociationsRepository _AssociationsRepository;
        private DAClient.EmploymentHistoryRepository _EmploymentHistoryRepository;
        private DAClient.SkillsRepository _SkillsRepository;
        private DAClient.ReferencesRepository _ReferencesRepository;
        private DAClient.UserDetailsRepository _UserDetailsRepository;

        private Guid _MyClientId { get; set; }

        #endregion

        #region Constructor

        public CandidateProfileAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;

            _AvailabilityRepository = new DAClient.AvailabilityRepository(base.ConnectionString);
            _AvailabilityRepository.CurrentUser = base.CurrentUser;
            _AvailabilityRepository.CurrentClient = base.CurrentClient;

            _ATSResumeRepository = new DAClient.ATSResumeRepository(base.ConnectionString);
            _ATSResumeRepository.CurrentUser = base.CurrentUser;
            _ATSResumeRepository.CurrentClient = base.CurrentClient;


            _ExecutiveSummaryRepository = new DAClient.ExecutiveSummaryRepository(base.ConnectionString);
            _ExecutiveSummaryRepository.CurrentUser = base.CurrentUser;
            _ExecutiveSummaryRepository.CurrentClient = base.CurrentClient;

            _ObjectiveRepository = new DAClient.ObjectiveRepository(base.ConnectionString);
            _ObjectiveRepository.CurrentUser = base.CurrentUser;
            _ObjectiveRepository.CurrentClient = base.CurrentClient;

            _EducationHistoryRepository = new DAClient.EducationHistoryRepository(base.ConnectionString);
            _EducationHistoryRepository.CurrentUser = base.CurrentUser;
            _EducationHistoryRepository.CurrentClient = base.CurrentClient;

            _LicenceAndCertificationsRepository = new DAClient.LicenceAndCertificationsRepository(base.ConnectionString);
            _LicenceAndCertificationsRepository.CurrentUser = base.CurrentUser;
            _LicenceAndCertificationsRepository.CurrentClient = base.CurrentClient;

            _PublicationHistoryRepository = new DAClient.PublicationHistoryRepository(base.ConnectionString);
            _PublicationHistoryRepository.CurrentUser = base.CurrentUser;
            _PublicationHistoryRepository.CurrentClient = base.CurrentClient;

            _SpeakingEventHistoryRepository = new DAClient.SpeakingEventHistoryRepository(base.ConnectionString);
            _SpeakingEventHistoryRepository.CurrentUser = base.CurrentUser;
            _SpeakingEventHistoryRepository.CurrentClient = base.CurrentClient;

            _LanguagesRepository = new DAClient.LanguagesRepository(base.ConnectionString);
            _LanguagesRepository.CurrentUser = base.CurrentUser;
            _LanguagesRepository.CurrentClient = base.CurrentClient;

            _AchievementRepository = new DAClient.AchievementRepository(base.ConnectionString);
            _AchievementRepository.CurrentUser = base.CurrentUser;
            _AchievementRepository.CurrentClient = base.CurrentClient;

            _AssociationsRepository = new DAClient.AssociationsRepository(base.ConnectionString);
            _AssociationsRepository.CurrentUser = base.CurrentUser;
            _AssociationsRepository.CurrentClient = base.CurrentClient;

            _EmploymentHistoryRepository = new DAClient.EmploymentHistoryRepository(base.ConnectionString);
            _EmploymentHistoryRepository.CurrentUser = base.CurrentUser;
            _EmploymentHistoryRepository.CurrentClient = base.CurrentClient;

            _SkillsRepository = new DAClient.SkillsRepository(base.ConnectionString);
            _SkillsRepository.CurrentUser = base.CurrentUser;
            _SkillsRepository.CurrentClient = base.CurrentClient;

            _ReferencesRepository = new DAClient.ReferencesRepository(base.ConnectionString);
            _ReferencesRepository.CurrentUser = base.CurrentUser;
            _ReferencesRepository.CurrentClient = base.CurrentClient;

            _UserDetailsRepository = new DAClient.UserDetailsRepository(base.ConnectionString);
            _UserDetailsRepository.CurrentUser = base.CurrentUser;
            _UserDetailsRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public bool AddCandidateProfile(BEClient.CandidateProfile objCandidateProfile, Guid profileId, Guid userId)
        {
            _AvailabilityRepository.BeginTransaction();
            try
            {

                #region Add Availability

                if (_AvailabilityRepository.CurrentUser == null)
                {
                    _AvailabilityRepository.CurrentUser = new BEClient.User() { UserId = userId };
                }
                objCandidateProfile.ObjAvailability.ProfileId = profileId;
                //ADdd New availability
                Guid availabilityId = _AvailabilityRepository.AddAvailability(objCandidateProfile.ObjAvailability);

                if (availabilityId.Equals(Guid.Empty))
                {
                    throw new Exception("Availability not inserted");
                }


                #endregion
                #region Add AtsResume
                _ATSResumeRepository.Transaction = _AvailabilityRepository.Transaction;
                if (_ATSResumeRepository.CurrentUser == null)
                {

                    _ATSResumeRepository.CurrentUser = new BEClient.User() { UserId = userId };
                }
                Guid Atsresumeid = _ATSResumeRepository.AddATSresume(objCandidateProfile.objATSResume);

                if (Atsresumeid.Equals(Guid.Empty))
                {
                    throw new Exception("AtsResume not inserted");
                    
                }
                #endregion


                #region User Details

                _UserDetailsRepository.Transaction = _AvailabilityRepository.Transaction;
                if (_UserDetailsRepository.CurrentUser == null)
                {

                    _UserDetailsRepository.CurrentUser = new BEClient.User() { UserId = userId };
                }
                //objCandidateProfile.objUserDetails.UserId = userId;
                bool result = _UserDetailsRepository.UpdateUserDetails(objCandidateProfile.objUserDetails);

                if (result.Equals(false))
                {
                    throw new Exception("User Details not inserted");
                    
                }

                #endregion


                #region Add Executive Summary
                _ExecutiveSummaryRepository.Transaction = _AvailabilityRepository.Transaction;
                objCandidateProfile.ObjExecutiveSummary.ProfileId = profileId;
                
                //objCandidateProfile.ObjExecutiveSummary.UserId = userId;
                if (_ExecutiveSummaryRepository.CurrentUser == null)
                {

                    _ExecutiveSummaryRepository.CurrentUser = new BEClient.User() { UserId = userId };
                }
                Guid executiveSummaryId = _ExecutiveSummaryRepository.AddExecutiveSummary(objCandidateProfile.ObjExecutiveSummary);

                if (executiveSummaryId.Equals(Guid.Empty))
                {
                    throw new Exception("Add Executive Summary not inserted");
                }
                #endregion


                #region Add Objective

                _ObjectiveRepository.Transaction = _AvailabilityRepository.Transaction;
                objCandidateProfile.ObjObjective.ProfileId = profileId;
                if (_ObjectiveRepository.CurrentUser == null)
                {

                    _ObjectiveRepository.CurrentUser = new BEClient.User() { UserId = userId };
                }
                //objCandidateProfile.ObjObjective.UserId = userId;
                Guid objectiveId = _ObjectiveRepository.AddObjective(objCandidateProfile.ObjObjective);

                if (objectiveId.Equals(Guid.Empty))
                {
                    throw new Exception("Add Objective not inserted");
                    
                }

                #endregion

                #region Add Language

                _LanguagesRepository.Transaction = _AvailabilityRepository.Transaction;
                if (_LanguagesRepository.CurrentUser == null)
                {

                    _LanguagesRepository.CurrentUser = new BEClient.User() { UserId = userId };
                }
                foreach (var v in objCandidateProfile.ObjContactLanguages)
                {
                    v.ProfileId = profileId;
                    //v.UserId = userId;
                    Guid languageId = _LanguagesRepository.AddLanguages(v);

                    if (languageId.Equals(Guid.Empty))
                    {
                        throw new Exception("Language not inserted");
                    }
                }
                #endregion

                #region Add Speaking History

                _SpeakingEventHistoryRepository.Transaction = _AvailabilityRepository.Transaction;
                if (_SpeakingEventHistoryRepository.CurrentUser == null)
                {

                    _SpeakingEventHistoryRepository.CurrentUser = new BEClient.User() { UserId = userId };
                }
                foreach (var v in objCandidateProfile.ObjContactSpeakingEventHistory)
                {
                    v.ProfileId = profileId;
                    //v.UserId = userId;

                    Guid speakingEventHistoryId = _SpeakingEventHistoryRepository.AddSpeakingEventHistory(v);

                    if (speakingEventHistoryId.Equals(Guid.Empty))
                    {
                        throw new Exception("Speaking History not inserted");
                    }
                }
                #endregion

                #region Add Associations
                _AssociationsRepository.Transaction = _AvailabilityRepository.Transaction;
                if (_AssociationsRepository.CurrentUser == null)
                {

                    _AssociationsRepository.CurrentUser = new BEClient.User() { UserId = userId };
                }

                foreach (var v in objCandidateProfile.ObjContactAssociations)
                {
                    v.ProfileId = profileId;
                    //v.UserId = userId;

                    Guid associationId = _AssociationsRepository.AddAssociations(v);

                    if (associationId.Equals(Guid.Empty))
                    {
                        throw new Exception("Associations not inserted");
                    }
                }

                #endregion

                #region Add Achievement

                _AchievementRepository.Transaction = _AvailabilityRepository.Transaction;
                if (_AchievementRepository.CurrentUser == null)
                {

                    _AchievementRepository.CurrentUser = new BEClient.User() { UserId = userId };
                }
                foreach (var v in objCandidateProfile.ObjContactAchievement)
                {
                    v.ProfileId = profileId;
                    //v.UserId = userId;

                    Guid achievementId = _AchievementRepository.AddAchievements(v);

                    if (achievementId.Equals(Guid.Empty))
                    {
                        throw new Exception("Achievement not inserted");
                    }

                }

                #endregion

                #region Add Employment History
                _EmploymentHistoryRepository.Transaction = _AvailabilityRepository.Transaction;
                if (_EmploymentHistoryRepository.CurrentUser == null)
                {

                    _EmploymentHistoryRepository.CurrentUser = new BEClient.User() { UserId = userId };
                }
                foreach (var v in objCandidateProfile.ObjContactEmployments)
                {
                    v.ProfileId = profileId;
                    //v.UserId = userId;

                    Guid employmenthistoryid = _EmploymentHistoryRepository.AddEmploymentHistory(v);

                    if (employmenthistoryid.Equals(Guid.Empty))
                    {
                        throw new Exception("Employment History not inserted");
                    }
                }

                #endregion

                #region Add Education History
                _EducationHistoryRepository.Transaction = _AvailabilityRepository.Transaction;
                if (_EducationHistoryRepository.CurrentUser == null)
                {

                    _EducationHistoryRepository.CurrentUser = new BEClient.User() { UserId = userId };
                }
                foreach (var v in objCandidateProfile.ObjContactEducations)
                {
                    v.ProfileId = profileId;
                   // v.UserId = userId;

                    Guid educationId = _EducationHistoryRepository.AddEducationHistory(v);

                    if (educationId.Equals(Guid.Empty))
                    {
                        _AvailabilityRepository.RollbackTransaction();
                        return false;
                    }
                }

                #endregion

                #region Add Licence And certifications
                _LicenceAndCertificationsRepository.Transaction = _AvailabilityRepository.Transaction;
                if (_LicenceAndCertificationsRepository.CurrentUser == null)
                {

                    _LicenceAndCertificationsRepository.CurrentUser = new BEClient.User() { UserId = userId };
                }
                foreach (var v in objCandidateProfile.ObjContactLicenceAndCertifications)
                {
                    v.ProfileId = profileId;
                    //v.UserId = userId;

                    Guid licenceAndCertificationId = _LicenceAndCertificationsRepository.AddLicenceAndCertifications(v);

                    if (licenceAndCertificationId.Equals(Guid.Empty))
                    {
                        throw new Exception("Licence And certifications not inserted");
                    }
                }

                #endregion

                #region Add Publication History
                _PublicationHistoryRepository.Transaction = _AvailabilityRepository.Transaction;
                if (_PublicationHistoryRepository.CurrentUser == null)
                {

                    _PublicationHistoryRepository.CurrentUser = new BEClient.User() { UserId = userId };
                }
                foreach (var v in objCandidateProfile.ObjContactPublicationHistory)
                {
                    v.ProfileId = profileId;
                  //  v.UserId = userId;

                    Guid publicationHistoryId = _PublicationHistoryRepository.AddPublicationsHistory(v);

                    if (publicationHistoryId.Equals(Guid.Empty))
                    {
                        throw new Exception("Publication History not inserted");
                    }
                }

                #endregion
                _AvailabilityRepository.CommitTransaction();
                return true;
            }
            catch
            {
                _AvailabilityRepository.RollbackTransaction();
                throw;
            }
        }
    }
}
