using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;
using BLClient = ATS.BusinessLogic;
namespace ATS.BusinessLogic
{
    public class ProfileAction : ActionBase
    {
        #region private data member
        private DAClient.ProfileRepository _ProFileRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor

        public ProfileAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _ProFileRepository = new DAClient.ProfileRepository(base.ConnectionString);
            _ProFileRepository.CurrentUser = base.CurrentUser;
            _ProFileRepository.CurrentClient = base.CurrentClient;

        }
        #endregion


        public BEClient.CandidateProfile GetCandidateLastUpdatedProfile(Guid UserId)
        {
            try
            {
                Guid _Profileid = Guid.Empty;
                BEClient.Profile _myProfile = new BEClient.Profile();


                _myProfile = this.GetLastUpdatedProfileByUserId(UserId);

                if (_myProfile != null && !_myProfile.ProfileId.Equals(Guid.Empty))
                    _Profileid = _myProfile.ProfileId;

                //return null when no profile Found
                if (_Profileid.Equals(Guid.Empty))
                    return null;

                BEClient.CandidateProfile ObjCandidatProfile = new BEClient.CandidateProfile();


                /*Get Only contact related information*/
                GetOtherUserDetails(UserId, ref ObjCandidatProfile);

                //Get Only Profile Related information
                GetOtherProfileDetails(_Profileid, ref ObjCandidatProfile);
                //CR-2
                //Getuserorgdetails
                GetUserOrgDetails(UserId, ref ObjCandidatProfile);

                return ObjCandidatProfile;
            }
            catch
            {
                throw;
            }
        }

        public BEClient.Profile GetLastUpdatedprofileByUserId(Guid UserId)
        {
            try
            {
                return _ProFileRepository.GetLastUpdatedProfileByUserId(UserId);
            }
            catch
            {
                throw;
            }
        }

        public Guid ValidateProfile(BEClient.Profile objProfile)
        {
            try
            {

                return _ProFileRepository.ValidateProfile(objProfile);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.Profile GetLastUpdatedProfileByUserId(Guid UserId)
        {
            try
            {
                return _ProFileRepository.GetLastUpdatedProfileByUserId(UserId);
            }
            catch
            {
                throw;
            }
        }
        private void GetOtherUserDetails(Guid UserId, ref BEClient.CandidateProfile ObjCandidatProfile)
        {
            try
            {

                BLClient.UserDetailsAction _UserDetailsAction = new BLClient.UserDetailsAction(_MyClientId);
                ObjCandidatProfile.objUserDetails = _UserDetailsAction.GetUserDetailsByUserId(UserId);
            }
            catch
            {
                throw;
            }

        }

        //CR-2
        private void GetUserOrgDetails(Guid UserId, ref BEClient.CandidateProfile ObjCandidatProfile)
        {
            try
            {

                BLClient.OrgAction _orgDetailsAction = new BLClient.OrgAction(_MyClientId);
                ObjCandidatProfile.objOrgDetails = _orgDetailsAction.GetUserOrgDetailsByUserId(UserId);
            }
            catch
            {
                throw;
            }

        }
        private void GetOtherUserDetailsPDF(Guid UserId, ref BEClient.CandidateProfileForPDF ObjCandidatProfile)
        {
            try
            {

                BLClient.UserDetailsAction _UserDetailsAction = new BLClient.UserDetailsAction(_MyClientId);
                ObjCandidatProfile.objUserDetails = _UserDetailsAction.GetUserDetailsByUserId(UserId);
            }
            catch
            {
                throw;
            }

        }

        public BEClient.CandidateProfile GetCanidateFullProfileByUserIdAndProfileId(Guid userId, Guid ProfileId)
        {
            BEClient.CandidateProfile ObjCandidatProfile = new BEClient.CandidateProfile();
            try
            {
                GetOtherProfileDetails(ProfileId, ref ObjCandidatProfile);
                GetOtherUserDetails(userId, ref ObjCandidatProfile);
                return ObjCandidatProfile;

            }
            catch
            {
                ObjCandidatProfile = null;
                throw;
            }

        }
        public BEClient.CandidateProfileForPDF GetCanidateFullProfileByUserIdAndProfileIdPDF(Guid userId, Guid ProfileId)
        {
            BEClient.CandidateProfileForPDF ObjCandidatProfile = new BEClient.CandidateProfileForPDF();
            try
            {
                GetOtherProfileDetailsPDF(ProfileId, ref ObjCandidatProfile);
                GetOtherUserDetailsPDF(userId, ref ObjCandidatProfile);
                return ObjCandidatProfile;

            }
            catch
            {
                ObjCandidatProfile = null;
                throw;
            }

        }


        public BEClient.Profile GetProfileByProfileId(Guid ProfileId)
        {
            try
            {
                return _ProFileRepository.GetProfileByProfileId(ProfileId);
            }
            catch
            {
                throw;
            }
        }

        private void GetOtherProfileDetails(Guid ProfileId, ref BEClient.CandidateProfile ObjCandidatProfile)
        {
            try
            {


                ObjCandidatProfile.objProfile = this.GetProfileByProfileId(ProfileId);
                //Employment

                ATSResumeAction _ATSResumeAction = new ATSResumeAction(_MyClientId);
                ObjCandidatProfile.objATSResume = _ATSResumeAction.GetResumeByProfile(ProfileId);

                BLClient.EmploymentHistoryAction _EmployMentHistoryAction = new EmploymentHistoryAction(_MyClientId);

                ObjCandidatProfile.ObjContactEmployments = _EmployMentHistoryAction.GetEmploymentHistoryByProfileId(ProfileId);

                BLClient.ProjectAction _ProjectAction = new ProjectAction(_MyClientId);



                //Skills
                BLClient.SkillsAction _SkillsAction = new SkillsAction(_MyClientId);

                ObjCandidatProfile.ObjContactSkills = _SkillsAction.GetSkillsByProfileId(ProfileId);

                //Education
                BLClient.EducationHistoryAction _EducationHistoryAction = new EducationHistoryAction(_MyClientId);
                ObjCandidatProfile.ObjContactEducations = _EducationHistoryAction.GetEducationHistoryByProfileId(ProfileId);

                //reference

                BLClient.ReferencesAction _ReferencesAction = new ReferencesAction(_MyClientId);

                ObjCandidatProfile.ObjContactReferences = _ReferencesAction.GetReferencesByProfileId(ProfileId);

                //Availability
                BLClient.AvailabilityAction _AvailabilityAction = new AvailabilityAction(_MyClientId);

                ObjCandidatProfile.ObjAvailability = _AvailabilityAction.GetAvailabilityByProfileId(ProfileId);
                if (ObjCandidatProfile.ObjAvailability == null)
                {
                    ObjCandidatProfile.ObjAvailability = new BEClient.Availability();
                }
                //Executive Summary
                BLClient.ExecutiveSummaryAction _ExecutiveSummaryAction = new ExecutiveSummaryAction(_MyClientId);

                ObjCandidatProfile.ObjExecutiveSummary = _ExecutiveSummaryAction.GetExecutiveSummaryByProfileId(ProfileId);
                if (ObjCandidatProfile.ObjExecutiveSummary == null)
                {
                    ObjCandidatProfile.ObjExecutiveSummary = new BEClient.ExecutiveSummary();
                }

                //Objective
                BLClient.ObjectiveAction _ObjectiveAction = new ObjectiveAction(_MyClientId);

                ObjCandidatProfile.ObjObjective = _ObjectiveAction.GetObjectiveByProfileId(ProfileId);

                if (ObjCandidatProfile.ObjObjective == null)
                {
                    ObjCandidatProfile.ObjObjective = new BEClient.Objective();
                }

                //Licences And Certifications
                BLClient.LicenceAndCertificationsAction _LicenceAndCertificationsAction = new LicenceAndCertificationsAction(_MyClientId);

                ObjCandidatProfile.ObjContactLicenceAndCertifications = _LicenceAndCertificationsAction.GetLicenceAndCertificationsByProfileId(ProfileId);

                //PublicationHistory
                BLClient.PublicationHistoryAction _PublicationHistoryAction = new PublicationHistoryAction(_MyClientId);

                ObjCandidatProfile.ObjContactPublicationHistory = _PublicationHistoryAction.GePublicationHistoryByProfileId(ProfileId);

                //SpeakingEventHistory
                BLClient.SpeakingEventHistoryAction _SpeakingEventHistoryAction = new SpeakingEventHistoryAction(_MyClientId);

                ObjCandidatProfile.ObjContactSpeakingEventHistory = _SpeakingEventHistoryAction.GetSpeakingEventHistoryByProfileId(ProfileId);

                //Languages
                BLClient.LanguagesAction _LanguagesAction = new LanguagesAction(_MyClientId);

                ObjCandidatProfile.ObjContactLanguages = _LanguagesAction.GetLanguagesByProfileId(ProfileId);

                //Achievement
                BLClient.AchievementAction _AchievementAction = new AchievementAction(_MyClientId);

                ObjCandidatProfile.ObjContactAchievement = _AchievementAction.GetAchievementByProfileId(ProfileId);


                //Associations
                BLClient.AssociationsAction _AssociationsAction = new AssociationsAction(_MyClientId);

                ObjCandidatProfile.ObjContactAssociations = _AssociationsAction.GetAssociationsByProfileId(ProfileId);


            }
            catch
            {
                throw;
            }
        }










        private void GetOtherProfileDetailsPDF(Guid ProfileId, ref BEClient.CandidateProfileForPDF ObjCandidatProfile)
        {
            try
            {


                ObjCandidatProfile.objProfile = this.GetProfileByProfileId(ProfileId);
                //Employment

                ATSResumeAction _ATSResumeAction = new ATSResumeAction(_MyClientId);
                ObjCandidatProfile.objATSResume = _ATSResumeAction.GetResumeByProfile(ProfileId);

                BLClient.EmploymentHistoryAction _EmployMentHistoryAction = new EmploymentHistoryAction(_MyClientId);

                ObjCandidatProfile.ObjContactEmployments = _EmployMentHistoryAction.GetEmploymentHistoryByProfileId(ProfileId);

                BLClient.ProjectAction _ProjectAction = new ProjectAction(_MyClientId);



                //Skills
                BLClient.SkillsAction _SkillsAction = new SkillsAction(_MyClientId);

                ObjCandidatProfile.ObjContactSkills = _SkillsAction.GetSkillsByProfileId(ProfileId);

                //Education
                BLClient.EducationHistoryAction _EducationHistoryAction = new EducationHistoryAction(_MyClientId);
                ObjCandidatProfile.ObjContactEducations = _EducationHistoryAction.GetEducationHistoryByProfileId(ProfileId);

                //reference

                BLClient.ReferencesAction _ReferencesAction = new ReferencesAction(_MyClientId);

                ObjCandidatProfile.ObjContactReferences = _ReferencesAction.GetReferencesByProfileId(ProfileId);

                //Availability
                BLClient.AvailabilityAction _AvailabilityAction = new AvailabilityAction(_MyClientId);

                ObjCandidatProfile.ObjAvailability = _AvailabilityAction.GetAvailabilityByProfileId(ProfileId);

                //Executive Summary
                BLClient.ExecutiveSummaryAction _ExecutiveSummaryAction = new ExecutiveSummaryAction(_MyClientId);

                ObjCandidatProfile.ObjExecutiveSummary = _ExecutiveSummaryAction.GetExecutiveSummaryByProfileId(ProfileId);


                //Objective
                BLClient.ObjectiveAction _ObjectiveAction = new ObjectiveAction(_MyClientId);

                ObjCandidatProfile.ObjObjective = _ObjectiveAction.GetObjectiveByProfileId(ProfileId);

                //Licences And Certifications
                BLClient.LicenceAndCertificationsAction _LicenceAndCertificationsAction = new LicenceAndCertificationsAction(_MyClientId);

                ObjCandidatProfile.ObjContactLicenceAndCertifications = _LicenceAndCertificationsAction.GetLicenceAndCertificationsByProfileId(ProfileId);

                //PublicationHistory
                BLClient.PublicationHistoryAction _PublicationHistoryAction = new PublicationHistoryAction(_MyClientId);

                ObjCandidatProfile.ObjContactPublicationHistory = _PublicationHistoryAction.GePublicationHistoryByProfileId(ProfileId);

                //SpeakingEventHistory
                BLClient.SpeakingEventHistoryAction _SpeakingEventHistoryAction = new SpeakingEventHistoryAction(_MyClientId);

                ObjCandidatProfile.ObjContactSpeakingEventHistory = _SpeakingEventHistoryAction.GetSpeakingEventHistoryByProfileId(ProfileId);

                //Languages
                BLClient.LanguagesAction _LanguagesAction = new LanguagesAction(_MyClientId);

                ObjCandidatProfile.ObjContactLanguages = _LanguagesAction.GetLanguagesByProfileId(ProfileId);

                //Achievement
                BLClient.AchievementAction _AchievementAction = new AchievementAction(_MyClientId);

                ObjCandidatProfile.ObjContactAchievement = _AchievementAction.GetAchievementByProfileId(ProfileId);


                //Associations
                BLClient.AssociationsAction _AssociationsAction = new AssociationsAction(_MyClientId);

                ObjCandidatProfile.ObjContactAssociations = _AssociationsAction.GetAssociationsByProfileId(ProfileId);


            }
            catch
            {
                throw;
            }
        }




        public Guid AddCandidateFullProfile(BEClient.CandidateProfile pCandidate)
        {
            try
            {
                _ProFileRepository.BeginTransaction();
                Guid _UserId = pCandidate.objProfile.UserId;
                if (_ProFileRepository.CurrentUser == null)
                {

                    _ProFileRepository.CurrentUser = new BEClient.User() { UserId = _UserId };
                }
                #region Add Profile
                Guid _ProfileId = this.AddProfile(pCandidate.objProfile);
                if (_ProfileId.Equals(Guid.Empty))
                    throw new Exception("Profile not added in database");

                #endregion

                #region Add Availability

                DAClient.AvailabilityRepository _AvailabilityRpository = new DAClient.AvailabilityRepository(base.ConnectionString);
                if (pCandidate.ObjAvailability == null)
                    pCandidate.ObjAvailability = new BEClient.Availability();

                pCandidate.ObjAvailability.ProfileId = _ProfileId;
                _AvailabilityRpository.Connection = _ProFileRepository.Connection;
                _AvailabilityRpository.Transaction = _ProFileRepository.Transaction;
                _AvailabilityRpository.CurrentUser = base.CurrentUser;
                Guid _AvailabilityId = _AvailabilityRpository.AddAvailability(pCandidate.ObjAvailability);

                if (_AvailabilityId.Equals(Guid.Empty))
                    throw new Exception("Availability not added in database");
                #endregion


                #region Executive Summary


                DAClient.ExecutiveSummaryRepository _ExecutiveSummaryRepository = new DAClient.ExecutiveSummaryRepository(base.ConnectionString);
                if (pCandidate.ObjExecutiveSummary == null)
                    pCandidate.ObjExecutiveSummary = new BEClient.ExecutiveSummary();

                pCandidate.ObjExecutiveSummary.ProfileId = _ProfileId;
                _ExecutiveSummaryRepository.Connection = _ProFileRepository.Connection;
                _ExecutiveSummaryRepository.Transaction = _ProFileRepository.Transaction;
                _ExecutiveSummaryRepository.CurrentUser = base.CurrentUser;

                Guid _ExecutiveSummaryId = _ExecutiveSummaryRepository.AddExecutiveSummary(pCandidate.ObjExecutiveSummary);

                if (_ExecutiveSummaryId.Equals(Guid.Empty))
                    throw new Exception("Executive Summary not added in database");
                #endregion

                #region Objective


                DAClient.ObjectiveRepository _ObjectiveRepository = new DAClient.ObjectiveRepository(base.ConnectionString);
                if (pCandidate.ObjObjective == null)
                    pCandidate.ObjObjective = new BEClient.Objective();

                pCandidate.ObjObjective.ProfileId = _ProfileId;
                _ObjectiveRepository.Connection = _ProFileRepository.Connection;
                _ObjectiveRepository.Transaction = _ProFileRepository.Transaction;
                _ObjectiveRepository.CurrentUser = base.CurrentUser;

                Guid _ObjectiveId = _ObjectiveRepository.AddObjective(pCandidate.ObjObjective);

                if (_ObjectiveId.Equals(Guid.Empty))
                    throw new Exception("Objective not added in database");
                #endregion




                #region Add Employment History
                if (pCandidate.ObjContactEmployments != null)
                {

                    DAClient.EmploymentHistoryRepository _EmploymentHistoryRepository = new DAClient.EmploymentHistoryRepository(this.ConnectionString);
                    foreach (BEClient.EmploymentHistory EmploymentHistory in pCandidate.ObjContactEmployments)
                    {
                        try
                        {
                            _EmploymentHistoryRepository.Transaction = _ProFileRepository.Transaction;
                            _EmploymentHistoryRepository.Connection = _ProFileRepository.Connection;
                            _EmploymentHistoryRepository.CurrentUser = base.CurrentUser;
                            EmploymentHistory.UserId = _UserId;
                            EmploymentHistory.ProfileId = _ProfileId;
                            Guid _EmploymentHistoryId = _EmploymentHistoryRepository.AddEmploymentHistory(EmploymentHistory);

                            if (_EmploymentHistoryId.Equals(Guid.Empty))
                                throw new Exception("Employment History not added in database");
                        }
                        catch
                        {
                            throw;
                        }
                    }
                }

                #endregion


                #region Add Education history
                if (pCandidate.ObjContactEducations != null)
                {
                    DAClient.EducationHistoryRepository _EducationHistoryRepository = new DAClient.EducationHistoryRepository(base.ConnectionString);
                    foreach (BEClient.EducationHistory EducationHistory in pCandidate.ObjContactEducations)
                    {
                        try
                        {
                            _EducationHistoryRepository.Transaction = _ProFileRepository.Transaction;
                            _EducationHistoryRepository.Connection = _ProFileRepository.Connection;
                            _EducationHistoryRepository.CurrentUser = base.CurrentUser;
                            EducationHistory.UserId = _UserId;
                            EducationHistory.ProfileId = _ProfileId;
                            Guid _EducationHistoryId = _EducationHistoryRepository.AddEducationHistory(EducationHistory);
                            if (_EducationHistoryId.Equals(Guid.Empty))
                                throw new Exception("Education History not added in database");
                        }
                        catch
                        {
                            throw;
                        }
                    }
                }



                #endregion

                #region Add Skills
                if (pCandidate.ObjContactSkills != null)
                {
                    DAClient.SkillsRepository _SkillsRepository = new DAClient.SkillsRepository(base.ConnectionString);
                    foreach (BEClient.Skills Skill in pCandidate.ObjContactSkills)
                    {
                        try
                        {
                            _SkillsRepository.Transaction = _ProFileRepository.Transaction;
                            _SkillsRepository.Connection = _ProFileRepository.Connection;
                            _SkillsRepository.CurrentUser = base.CurrentUser;
                            Skill.ProfileId = _ProfileId;
                            Guid _SkillId = _SkillsRepository.AddSkills(Skill);
                            if (_SkillId.Equals(Guid.Empty))
                                throw new Exception("Skill not added in database");
                        }
                        catch
                        {
                            throw;
                        }
                    }
                }

                #endregion

                #region Add reference
                if (pCandidate.ObjContactReferences != null)
                {

                    foreach (BEClient.References Reference in pCandidate.ObjContactReferences)
                    {
                        DAClient.ReferencesRepository _ReferencesRepository = new DAClient.ReferencesRepository(base.ConnectionString);
                        try
                        {
                            _ReferencesRepository.Transaction = _ProFileRepository.Transaction;
                            _ReferencesRepository.Connection = _ProFileRepository.Connection;
                            _ReferencesRepository.CurrentUser = base.CurrentUser;

                            Reference.ProfileId = _ProfileId;
                            Guid _ReferencesId = _ReferencesRepository.AddReferences(Reference);
                            if (_ReferencesId.Equals(Guid.Empty))
                                throw new Exception("Skill not added in database");
                        }
                        catch
                        {
                            throw;
                        }
                    }
                }
                #endregion
                _ProFileRepository.CommitTransaction();
                return _ProfileId;
            }
            catch
            {
                _ProFileRepository.RollbackTransaction();
                throw;
            }

        }


        public void UpdateCandidateFullProfile(BEClient.CandidateProfile pCandidate)
        {
            try
            {
                _ProFileRepository.BeginTransaction();
                Guid UserId = pCandidate.objProfile.UserId;
                Guid _currentProfileId = pCandidate.objProfile.ProfileId;

                #region Update ATSREsume
                try
                {
                    if (pCandidate.objATSResume != null)
                    {
                        DAClient.ATSResumeRepository _ATSResumeRepository = new DAClient.ATSResumeRepository(base.ConnectionString);
                        _ATSResumeRepository.Transaction = _ProFileRepository.Transaction;
                        pCandidate.objATSResume.UserId = UserId;
                        _ATSResumeRepository.CurrentUser = base.CurrentUser;
                        pCandidate.objATSResume.ATSResumeId = _ATSResumeRepository.UpdateATSResume(pCandidate.objATSResume, false);
                    }
                }
                catch
                {
                    throw;
                }
                #endregion

                #region Update UserDetails
                try
                {
                    DAClient.UserDetailsRepository _UserDetailsRepository = new DAClient.UserDetailsRepository(base.ConnectionString);
                    _UserDetailsRepository.Transaction = _ProFileRepository.Transaction;
                    pCandidate.objUserDetails.UserId = UserId;

                    _UserDetailsRepository.CurrentUser = base.CurrentUser;
                    if (base.CurrentUser == null)
                    {
                        _UserDetailsRepository.CurrentUser = new BEClient.User();
                        _UserDetailsRepository.CurrentUser.UserId = UserId;
                    }
                    else
                    {
                        _UserDetailsRepository.CurrentUser = base.CurrentUser;
                    }
                    if (pCandidate.objUserDetails.UserId == Guid.Empty)
                    {

                        Guid NewUserDetailsId = _UserDetailsRepository.AddUserDetails(pCandidate.objUserDetails);
                        if (NewUserDetailsId == Guid.Empty)
                            throw new Exception("User Details not Added");

                    }
                    else
                    {
                        bool UserDetailsUpdated = _UserDetailsRepository.UpdateUserDetails(pCandidate.objUserDetails);
                        if (!UserDetailsUpdated)
                            throw new Exception("User not Updated");
                    }
                }
                catch
                {
                    throw;
                }
                #endregion

                //#region Update Availability
                //try
                //{
                //    DAClient.AvailabilityRepository _AvailabilityRepository = new DAClient.AvailabilityRepository(base.ConnectionString);
                //    _AvailabilityRepository.Transaction = _ProFileRepository.Transaction;
                //    _AvailabilityRepository.CurrentUser = base.CurrentUser;
                //    pCandidate.ObjAvailability.ProfileId = _currentProfileId;
                //    if (base.CurrentUser == null)
                //    {
                //        _AvailabilityRepository.CurrentUser = new BEClient.User();
                //        _AvailabilityRepository.CurrentUser.UserId = UserId;
                //    }
                //    else
                //    {
                //        _AvailabilityRepository.CurrentUser = base.CurrentUser;
                //    }

                //    if (pCandidate.ObjAvailability.AvailibilityId == null || pCandidate.ObjAvailability.AvailibilityId == Guid.Empty)
                //    {

                //        Guid NewAvailabilityid = _AvailabilityRepository.AddAvailability(pCandidate.ObjAvailability);
                //        if (NewAvailabilityid == Guid.Empty)
                //            throw new Exception("Availability not Added");


                //    }
                //    else
                //    {
                //        bool AvailabilityUpdated = _AvailabilityRepository.UpdateAvailability(pCandidate.ObjAvailability);

                //        if (!AvailabilityUpdated)
                //            throw new Exception("Availability not Updated");
                //    }
                //}
                //catch
                //{
                //    throw;
                //}

                //#endregion

                #region Update Executive Summary
                try
                {
                    DAClient.ExecutiveSummaryRepository _ExecutiveSummaryRepository = new DAClient.ExecutiveSummaryRepository(base.ConnectionString);
                    _ExecutiveSummaryRepository.Transaction = _ProFileRepository.Transaction;
                    _ExecutiveSummaryRepository.CurrentUser = base.CurrentUser;
                    pCandidate.ObjExecutiveSummary.ProfileId = _currentProfileId;

                    if (base.CurrentUser == null)
                    {
                        _ExecutiveSummaryRepository.CurrentUser = new BEClient.User();
                        _ExecutiveSummaryRepository.CurrentUser.UserId = UserId;
                        pCandidate.ObjExecutiveSummary.UserId = UserId;
                    }
                    else
                    {
                        _ExecutiveSummaryRepository.CurrentUser = base.CurrentUser;
                        pCandidate.ObjExecutiveSummary.UserId = base.CurrentUser.UserId;
                    }

                    if (pCandidate.ObjExecutiveSummary.ExecutiveSummaryId == null || pCandidate.ObjExecutiveSummary.ExecutiveSummaryId == Guid.Empty)
                    {

                        Guid NewExecutiveSummaryid = _ExecutiveSummaryRepository.AddExecutiveSummary(pCandidate.ObjExecutiveSummary);
                        if (NewExecutiveSummaryid == Guid.Empty)
                            throw new Exception("Executive Summary  not Added");


                    }
                    else
                    {
                        bool ExecutiveSummaryUpdated = _ExecutiveSummaryRepository.UpdateExecutiveSummary(pCandidate.ObjExecutiveSummary);

                        if (!ExecutiveSummaryUpdated)
                            throw new Exception("Executive Summary not Updated");
                    }
                }
                catch
                {
                    throw;
                }

                #endregion

                #region Update Objective Summary
                try
                {
                    DAClient.ObjectiveRepository _ObjectiveRepository = new DAClient.ObjectiveRepository(base.ConnectionString);
                    _ObjectiveRepository.Transaction = _ProFileRepository.Transaction;
                    _ObjectiveRepository.CurrentUser = base.CurrentUser;
                    pCandidate.ObjObjective.ProfileId = _currentProfileId;


                    if (base.CurrentUser == null)
                    {
                        _ObjectiveRepository.CurrentUser = new BEClient.User();
                        _ObjectiveRepository.CurrentUser.UserId = UserId;
                        pCandidate.ObjObjective.UserId = UserId;
                    }
                    else
                    {
                        _ObjectiveRepository.CurrentUser = base.CurrentUser;
                        pCandidate.ObjObjective.UserId = base.CurrentUser.UserId;
                    }

                    if (pCandidate.ObjObjective.ObjectiveId == null || pCandidate.ObjObjective.ObjectiveId == Guid.Empty)
                    {

                        Guid NewObjectiveid = _ObjectiveRepository.AddObjective(pCandidate.ObjObjective);
                        if (NewObjectiveid == Guid.Empty)
                            throw new Exception("Objective not Added");


                    }
                    else
                    {
                        bool ObjectiveUpdated = _ObjectiveRepository.UpdateObjective(pCandidate.ObjObjective);

                        if (!ObjectiveUpdated)
                            throw new Exception("Objective  not Updated");
                    }
                }
                catch
                {
                    throw;
                }

                #endregion

                Guid _currentContactId = pCandidate.objProfile.UserId;

                #region Project Delete
                //Delete All Existing Project with same profile and contactuid
                DAClient.ProjectRepository _ProjectRepository = new DAClient.ProjectRepository(this.ConnectionString);
                _ProjectRepository.Transaction = _ProFileRepository.Transaction;
                _ProjectRepository.CurrentUser = base.CurrentUser;
                if (base.CurrentUser == null)
                {
                    _ProjectRepository.CurrentUser = new BEClient.User();
                    _ProjectRepository.CurrentUser.UserId = UserId;
                }
                else
                {
                    _ProjectRepository.CurrentUser = base.CurrentUser;
                }

                bool _isProjectDelete = _ProjectRepository.DeleteAllProjects(_currentProfileId, _currentContactId);
                if (!_isProjectDelete)
                    throw new Exception("Project not deleted from database");
                #endregion

                #region EmploymentHistory Delete
                //Delete All Existing EmploymentHistory with same profile and contactuid
                DAClient.EmploymentHistoryRepository _EmploymentHistoryRepository = new DAClient.EmploymentHistoryRepository(base.ConnectionString);
                _EmploymentHistoryRepository.Transaction = _ProFileRepository.Transaction;
                _EmploymentHistoryRepository.CurrentUser = base.CurrentUser;


                bool _isEmploymentHistoryDeleted = _EmploymentHistoryRepository.DeleteAllEmploymentHistory(_currentProfileId);
                if (!_isEmploymentHistoryDeleted)
                    throw new Exception("EmploymentHistory not deleted from database");
                #endregion

                #region Adding Project and Employment History
                if (pCandidate.ObjContactEmployments != null)
                {
                    foreach (BEClient.EmploymentHistory EmploymentHistory in pCandidate.ObjContactEmployments)
                    {
                        try
                        {
                            EmploymentHistory.UserId = UserId;
                            EmploymentHistory.ProfileId = _currentProfileId;
                            _EmploymentHistoryRepository.Transaction = _ProFileRepository.Transaction;
                            _EmploymentHistoryRepository.CurrentUser = base.CurrentUser;
                            if (base.CurrentUser == null)
                            {
                                _EmploymentHistoryRepository.CurrentUser = new BEClient.User();
                                _EmploymentHistoryRepository.CurrentUser.UserId = UserId;
                            }
                            else
                            {
                                _EmploymentHistoryRepository.CurrentUser = base.CurrentUser;
                            }


                            Guid _EmploymentHistoryId = _EmploymentHistoryRepository.AddEmploymentHistory(EmploymentHistory);
                            if (_EmploymentHistoryId.Equals(Guid.Empty))
                                throw new Exception("EmploymentHistory not added in database");


                            //if (EmploymentHistory.ObjProject != null)
                            //{
                            //    foreach (BEClient.Project project in EmploymentHistory.ObjProject)
                            //    {
                            //        project.EmploymentHistoryId = _EmploymentHistoryId;
                            //        project.ProfileId = _currentProfileId;
                            //        project.UserId = _currentContactId;

                            //        Guid _currentProjectId = _ProjectRepository.AddProject(project);
                            //        if (_currentProjectId.Equals(Guid.Empty))
                            //            throw new Exception("Project not added in database");
                            //    }
                            //}

                        }
                        catch
                        {
                            throw;
                        }
                    }
                }
                #endregion

                #region Delete  EducationHistory From Database
                DAClient.EducationHistoryRepository _EducationRepository = new DAClient.EducationHistoryRepository(base.ConnectionString);
                _EducationRepository.Transaction = _ProFileRepository.Transaction;

                _EducationRepository.CurrentUser = base.CurrentUser;
                bool _isEducationHistoryDeleted = _EducationRepository.DeleteAllEducationHistories(_currentProfileId);
                if (!_isEducationHistoryDeleted)
                    throw new Exception("EducationHistory not Deleted in database");
                #endregion

                #region Add New EducationHistory in database
                if (pCandidate.ObjContactEducations != null)
                {
                    foreach (BEClient.EducationHistory EducationHistory in pCandidate.ObjContactEducations)
                    {
                        try
                        {
                            EducationHistory.UserId = _currentContactId;
                            EducationHistory.ProfileId = _currentProfileId;
                            _EducationRepository.Transaction = _ProFileRepository.Transaction;
                            _EducationRepository.CurrentUser = base.CurrentUser;

                            if (base.CurrentUser == null)
                            {
                                _EducationRepository.CurrentUser = new BEClient.User();
                                _EducationRepository.CurrentUser.UserId = UserId;
                            }
                            else
                            {
                                _EmploymentHistoryRepository.CurrentUser = base.CurrentUser;
                            }
                            Guid _EducationHistoryId = _EducationRepository.AddEducationHistory(EducationHistory);
                            if (_EducationHistoryId.Equals(Guid.Empty))
                                throw new Exception("EducationHistory not added in database");
                        }
                        catch
                        {
                            throw;
                        }
                    }
                }
                #endregion

                #region Delete  Licence And Certifications From Database
                DAClient.LicenceAndCertificationsRepository _LicenceAndCertificationsRepository = new DAClient.LicenceAndCertificationsRepository(base.ConnectionString);
                _LicenceAndCertificationsRepository.Transaction = _ProFileRepository.Transaction;
                _LicenceAndCertificationsRepository.CurrentUser = base.CurrentUser;
                bool _isLicenceAndCertificationsDeleted = _LicenceAndCertificationsRepository.DeleteAllLicenceAndCertifications(_currentProfileId);
                if (!_isLicenceAndCertificationsDeleted)
                    throw new Exception("Licence and certificate  not Deleted in database");
                #endregion

                #region Add New Licence And Certifications in database
                if (pCandidate.ObjContactLicenceAndCertifications != null)
                {
                    foreach (BEClient.LicenceAndCertifications LicenceAndCertifications in pCandidate.ObjContactLicenceAndCertifications)
                    {
                        try
                        {
                            LicenceAndCertifications.UserId = _currentContactId;
                            LicenceAndCertifications.ProfileId = _currentProfileId;
                            _LicenceAndCertificationsRepository.Transaction = _ProFileRepository.Transaction;
                            _LicenceAndCertificationsRepository.CurrentUser = base.CurrentUser;

                            if (base.CurrentUser == null)
                            {
                                _LicenceAndCertificationsRepository.CurrentUser = new BEClient.User();
                                _LicenceAndCertificationsRepository.CurrentUser.UserId = UserId;
                            }
                            else
                            {
                                _LicenceAndCertificationsRepository.CurrentUser = base.CurrentUser;
                            }
                            Guid _LicenceAndCertificationsId = _LicenceAndCertificationsRepository.AddLicenceAndCertifications(LicenceAndCertifications);
                            if (_LicenceAndCertificationsId.Equals(Guid.Empty))
                                throw new Exception("Licence And Certifications not added in database");
                        }
                        catch
                        {
                            throw;
                        }
                    }
                }
                #endregion

                #region Delete  Publication History From Database
                DAClient.PublicationHistoryRepository _PublicationHistoryRepository = new DAClient.PublicationHistoryRepository(base.ConnectionString);
                _PublicationHistoryRepository.Transaction = _ProFileRepository.Transaction;
                _PublicationHistoryRepository.CurrentUser = base.CurrentUser;
                bool _isPublicationHistoryDeleted = _PublicationHistoryRepository.DeleteAllPublicationHistories(_currentProfileId);
                if (!_isPublicationHistoryDeleted)
                    throw new Exception("Publication not Deleted in database");
                #endregion

                #region Add New Publication History in database
                if (pCandidate.ObjContactPublicationHistory != null)
                {
                    foreach (BEClient.PublicationHistory PublicationHistory in pCandidate.ObjContactPublicationHistory)
                    {
                        try
                        {
                            PublicationHistory.UserId = _currentContactId;
                            PublicationHistory.ProfileId = _currentProfileId;
                            _PublicationHistoryRepository.Transaction = _ProFileRepository.Transaction;
                            _PublicationHistoryRepository.CurrentUser = base.CurrentUser;

                            if (base.CurrentUser == null)
                            {
                                _PublicationHistoryRepository.CurrentUser = new BEClient.User();
                                _PublicationHistoryRepository.CurrentUser.UserId = UserId;
                            }
                            else
                            {
                                _PublicationHistoryRepository.CurrentUser = base.CurrentUser;
                            }
                            Guid _PublicationHistoryId = _PublicationHistoryRepository.AddPublicationsHistory(PublicationHistory);
                            if (_PublicationHistoryId.Equals(Guid.Empty))
                                throw new Exception("Publication HIstory  not added in database");
                        }
                        catch
                        {
                            throw;
                        }
                    }
                }
                #endregion

                #region Delete  Speaking Event History  From Database
                DAClient.SpeakingEventHistoryRepository _SpeakingEventHistoryRepository = new DAClient.SpeakingEventHistoryRepository(base.ConnectionString);
                _SpeakingEventHistoryRepository.Transaction = _ProFileRepository.Transaction;
                _SpeakingEventHistoryRepository.CurrentUser = base.CurrentUser;
                bool _isSpeakingEventHistoryDeleted = _SpeakingEventHistoryRepository.DeleteAllSpeakingEventHistory(_currentProfileId);
                if (!_isSpeakingEventHistoryDeleted)
                    throw new Exception("Speaking Event History not Deleted in database");
                #endregion

                #region Add New Speaking Event History in database
                if (pCandidate.ObjContactSpeakingEventHistory != null)
                {
                    foreach (BEClient.SpeakingEventHistory SpeakingEventHistory in pCandidate.ObjContactSpeakingEventHistory)
                    {
                        try
                        {
                            SpeakingEventHistory.UserId = _currentContactId;
                            SpeakingEventHistory.ProfileId = _currentProfileId;
                            _SpeakingEventHistoryRepository.Transaction = _ProFileRepository.Transaction;
                            _SpeakingEventHistoryRepository.CurrentUser = base.CurrentUser;

                            if (base.CurrentUser == null)
                            {
                                _SpeakingEventHistoryRepository.CurrentUser = new BEClient.User();
                                _SpeakingEventHistoryRepository.CurrentUser.UserId = UserId;
                            }
                            else
                            {
                                _SpeakingEventHistoryRepository.CurrentUser = base.CurrentUser;
                            }
                            Guid _SpeakingEventHistoryId = _SpeakingEventHistoryRepository.AddSpeakingEventHistory(SpeakingEventHistory);
                            if (_SpeakingEventHistoryId.Equals(Guid.Empty))
                                throw new Exception("Speaking Event History  not added in database");
                        }
                        catch
                        {
                            throw;
                        }
                    }
                }
                #endregion

                #region Delete Languages  From Database
                DAClient.LanguagesRepository _LanguagesRepository = new DAClient.LanguagesRepository(base.ConnectionString);
                _LanguagesRepository.Transaction = _ProFileRepository.Transaction;
                _LanguagesRepository.CurrentUser = base.CurrentUser;
                bool _isLanguagesyDeleted = _LanguagesRepository.DeleteAllLanguages(_currentProfileId);
                if (!_isLanguagesyDeleted)
                    throw new Exception("Languages not Deleted in database");
                #endregion

                #region Add New lanfuages in database
                if (pCandidate.ObjContactLanguages != null)
                {
                    foreach (BEClient.Languages Languages in pCandidate.ObjContactLanguages)
                    {
                        try
                        {
                            Languages.UserId = _currentContactId;
                            Languages.ProfileId = _currentProfileId;
                            _LanguagesRepository.Transaction = _ProFileRepository.Transaction;
                            _LanguagesRepository.CurrentUser = base.CurrentUser;

                            if (base.CurrentUser == null)
                            {
                                _LanguagesRepository.CurrentUser = new BEClient.User();
                                _LanguagesRepository.CurrentUser.UserId = UserId;
                            }
                            else
                            {
                                _LanguagesRepository.CurrentUser = base.CurrentUser;
                            }
                            Guid _LanguagesId = _LanguagesRepository.AddLanguages(Languages);
                            if (_LanguagesId.Equals(Guid.Empty))
                                throw new Exception("Languages  not added in database");
                        }
                        catch
                        {
                            throw;
                        }
                    }
                }
                #endregion

                #region Delete Achievements  From Database
                DAClient.AchievementRepository _AchievementRepository = new DAClient.AchievementRepository(base.ConnectionString);
                _AchievementRepository.CurrentUser = base.CurrentUser;
                _AchievementRepository.Transaction = _ProFileRepository.Transaction;
                bool _isAchievementsDeleted = _AchievementRepository.DeleteAllAchievement(_currentProfileId);
                if (!_isAchievementsDeleted)
                    throw new Exception("Achievements not Deleted in database");
                #endregion

                #region Add New Achievements in database
                if (pCandidate.ObjContactAchievement != null)
                {
                    foreach (BEClient.Achievement Achievement in pCandidate.ObjContactAchievement)
                    {
                        try
                        {
                            Achievement.UserId = _currentContactId;
                            Achievement.ProfileId = _currentProfileId;
                            _AchievementRepository.Transaction = _ProFileRepository.Transaction;
                            _AchievementRepository.CurrentUser = base.CurrentUser;

                            if (base.CurrentUser == null)
                            {
                                _AchievementRepository.CurrentUser = new BEClient.User();
                                _AchievementRepository.CurrentUser.UserId = UserId;
                            }
                            else
                            {
                                _AchievementRepository.CurrentUser = base.CurrentUser;
                            }
                            Guid _AchievementId = _AchievementRepository.AddAchievements(Achievement);
                            if (_AchievementId.Equals(Guid.Empty))
                                throw new Exception("Achievement  not added in database");
                        }
                        catch
                        {
                            throw;
                        }
                    }
                }
                #endregion

                #region Delete Associations  From Database
                DAClient.AssociationsRepository _AssociationsRepository = new DAClient.AssociationsRepository(base.ConnectionString);
                _AssociationsRepository.Transaction = _ProFileRepository.Transaction;
                _AssociationsRepository.CurrentUser = base.CurrentUser;
                bool _isAssociationsDeleted = _AssociationsRepository.DeleteAllAssociations(_currentProfileId);
                if (!_isAchievementsDeleted)
                    throw new Exception("Associations not Deleted in database");
                #endregion

                #region Add New Associations in database
                if (pCandidate.ObjContactAssociations != null)
                {
                    foreach (BEClient.Associations Associations in pCandidate.ObjContactAssociations)
                    {
                        try
                        {
                            Associations.UserId = _currentContactId;
                            Associations.ProfileId = _currentProfileId;
                            _AssociationsRepository.Transaction = _ProFileRepository.Transaction;
                            _AssociationsRepository.CurrentUser = base.CurrentUser;

                            if (base.CurrentUser == null)
                            {
                                _AssociationsRepository.CurrentUser = new BEClient.User();
                                _AssociationsRepository.CurrentUser.UserId = UserId;
                            }
                            else
                            {
                                _AssociationsRepository.CurrentUser = base.CurrentUser;
                            }
                            Guid _AssociationId = _AssociationsRepository.AddAssociations(Associations);
                            if (_AssociationId.Equals(Guid.Empty))
                                throw new Exception("Associaton  not added in database");
                        }
                        catch
                        {
                            throw;
                        }
                    }
                }
                #endregion

                #region  Delete Skills from database

                DAClient.SkillsRepository _SkillsRepository = new DAClient.SkillsRepository(base.ConnectionString);
                _SkillsRepository.Transaction = _ProFileRepository.Transaction;
                _SkillsRepository.CurrentUser = base.CurrentUser;

                bool _isSkillsDeleted = _SkillsRepository.DeleteAllSkills(_currentProfileId);
                if (!_isSkillsDeleted)
                    throw new Exception("Skills not deleted in database");

                #endregion

                #region Added new Skills
                if (pCandidate.ObjContactSkills != null)
                {
                    foreach (BEClient.Skills Skills in pCandidate.ObjContactSkills)
                    {
                        try
                        {
                            Skills.UserId = _currentContactId;
                            Skills.ProfileId = _currentProfileId;
                            _SkillsRepository.Transaction = _ProFileRepository.Transaction;
                            _SkillsRepository.CurrentUser = base.CurrentUser;

                            if (base.CurrentUser == null)
                            {
                                _SkillsRepository.CurrentUser = new BEClient.User();
                                _SkillsRepository.CurrentUser.UserId = UserId;
                            }
                            else
                            {
                                _SkillsRepository.CurrentUser = base.CurrentUser;
                            }

                            Guid _skillId = _SkillsRepository.AddSkills(Skills);
                            if (_skillId.Equals(Guid.Empty))
                                throw new Exception("Skills not added in database");
                        }
                        catch
                        {
                            throw;
                        }
                    }
                }
                #endregion

                #region Delete All existing reference based on profile
                DAClient.ReferencesRepository _ReferencesRepository = new DAClient.ReferencesRepository(base.ConnectionString);
                _ReferencesRepository.Transaction = _ProFileRepository.Transaction;
                _ReferencesRepository.CurrentUser = base.CurrentUser;
                bool _isReferencesDeleted = _ReferencesRepository.DeleteAllReferences(_currentProfileId);

                if (!_isReferencesDeleted)
                    throw new Exception("References not deleted from database");
                #endregion

                #region Add new Reference for profile
                if (pCandidate.ObjContactReferences != null)
                {
                    foreach (BEClient.References Reference in pCandidate.ObjContactReferences)
                    {
                        try
                        {
                            Reference.UserId = _currentContactId;
                            Reference.ProfileId = _currentProfileId;
                            _ReferencesRepository.Transaction = _ProFileRepository.Transaction;
                            _ReferencesRepository.CurrentUser = base.CurrentUser;

                            if (base.CurrentUser == null)
                            {
                                _ReferencesRepository.CurrentUser = new BEClient.User();
                                _ReferencesRepository.CurrentUser.UserId = UserId;
                            }
                            else
                            {
                                _ReferencesRepository.CurrentUser = base.CurrentUser;
                            }
                            Guid _ReferencesId = _ReferencesRepository.AddReferences(Reference);
                            if (_ReferencesId.Equals(Guid.Empty))
                                throw new Exception("references not added in database");
                        }
                        catch
                        {
                            throw;
                        }
                    }
                }
                #endregion

                _ProFileRepository.CommitTransaction();

            }
            catch
            {
                _ProFileRepository.RollbackTransaction();
                throw;
            }
        }


        public List<BEClient.Profile> GetProfileByUser(Guid pUserId)
        {
            try
            {
                return _ProFileRepository.GetProfileByUserId(pUserId);
            }
            catch
            {
                throw;
            }
        }

        public bool SetProfileAsDefault(Guid ProfileId)
        {
            try
            {
                _ProFileRepository.BeginTransaction();
                bool IsUpdated = _ProFileRepository.SetProfileAsDefault(ProfileId);
                if (IsUpdated)
                {
                    _ProFileRepository.CommitTransaction();

                }
                else
                {
                    _ProFileRepository.RollbackTransaction();
                }

                return IsUpdated;

            }
            catch
            {
                _ProFileRepository.RollbackTransaction();
                throw;
            }
        }
        #region CRUD

        public Guid AddProfile(BEClient.Profile profile)
        {
            bool isLocalTransaction = false;
            try
            {


                if (_ProFileRepository.Transaction == null)
                {
                    _ProFileRepository.BeginTransaction();
                    isLocalTransaction = true;
                }

                Guid _UserId = profile.UserId;
                if (_ProFileRepository.CurrentUser == null)
                {

                    _ProFileRepository.CurrentUser = new BEClient.User() { UserId = _UserId };
                }
                Guid ProfileId = _ProFileRepository.AddProfile(profile);
                if (isLocalTransaction)
                    _ProFileRepository.CommitTransaction();

                return ProfileId;
            }
            catch
            {
                if (isLocalTransaction)
                    _ProFileRepository.RollbackTransaction();

                throw;
            }
        }


        public bool DeleteProfile(Guid ProfileId, Guid UserId)
        {
            try
            {
                return _ProFileRepository.DeleteProfile(ProfileId, UserId);
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateProfile(BEClient.Profile profile)
        {
            try
            {
                return _ProFileRepository.UpdateProfile(profile);
            }
            catch
            {
                throw;
            }
        }
        #endregion

    }
}
