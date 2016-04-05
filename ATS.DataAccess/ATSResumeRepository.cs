using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Data.Common;
using System.Data;
namespace ATS.DataAccess
{
    public class ATSResumeRepository : Repository<BEClient.ATSResume>
    {
        public ATSResumeRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }
        public Guid AddATSresume(BEClient.ATSResume pAtsResume, bool isCoverLetter = false)
        {
            bool LocalTrasaction = false;
            try
            {
                if (base.Transaction == null)
                {
                    base.BeginTransaction();
                    LocalTrasaction = true;
                }
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertATSResume");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@UserId", DbType.Guid, base.CurrentUser.UserId);

                Database.AddInParameter(command, "@ProfileId", DbType.Guid, pAtsResume.ProfileId);

                Database.AddInParameter(command, "@Details", DbType.String, pAtsResume.Details);

                Database.AddInParameter(command, "@UploadedFileName", DbType.String, pAtsResume.UploadedFileName);

                Database.AddInParameter(command, "@NewFileName", DbType.String, pAtsResume.NewFileName);

                Database.AddInParameter(command, "@Path", DbType.String, pAtsResume.Path);

                Database.AddInParameter(command, "@TitleName", DbType.String, pAtsResume.TitleName);

                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, pAtsResume.IsDelete);

                Database.AddInParameter(command, "@IsCoverLetter", DbType.Boolean, isCoverLetter);

                Database.AddInParameter(command, "@DocumentTypeId", DbType.Guid, pAtsResume.DocumentTypeId);

                Database.AddInParameter(command, "@CandidateDescription", DbType.String, pAtsResume.CandidateDescription);

                Database.AddOutParameter(command, "ATSResumeId", DbType.Guid, 16);

                var result = base.Add(command, "ATSResumeId");

                if (result != null)
                {
                    Guid.TryParse(result.ToString(), out reREsult);
                }
                if (LocalTrasaction)
                    this.CommitTransaction();

                return reREsult;
            }

            catch
            {
                if (LocalTrasaction)
                    this.RollbackTransaction();
                throw;
            }
        }

        public Guid UpdateATSResume(BEClient.ATSResume atsresume, bool isCoverLetter = false)
        {
            bool LocalTrasaction = false;

            bool LocalUserSet = false;
            try
            {
                if (base.Transaction == null)
                {
                    base.BeginTransaction();
                    LocalTrasaction = true;
                }
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("UpdateATSResume");

                if (base.CurrentUser == null)
                {
                    LocalUserSet = true;

                    base.CurrentUser = new BEClient.User();
                    base.CurrentUser.UserId = atsresume.UserId;
                }
                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ProfileId", System.Data.DbType.Guid, atsresume.ProfileId);

                Database.AddInParameter(command, "@UserId", DbType.Guid, base.CurrentUser.UserId);

                Database.AddInParameter(command, "@Details", DbType.String, atsresume.Details);

                Database.AddInParameter(command, "@UploadedFileName", DbType.String, atsresume.UploadedFileName);

                Database.AddInParameter(command, "@NewFileName", DbType.String, atsresume.NewFileName);

                Database.AddInParameter(command, "@Path", DbType.String, atsresume.Path);

                Database.AddInParameter(command, "@TitleName", DbType.String, atsresume.TitleName);

                Database.AddInParameter(command, "@ATSDocumentId", DbType.Guid, atsresume.ATSResumeId);

                Database.AddOutParameter(command, "ATSResumeId", DbType.Guid, 32);

                Database.AddInParameter(command, "@IsCoverLetter", DbType.Boolean, isCoverLetter);

                var result = base.Add(command, "ATSResumeId");

                if (result != null)
                {
                    Guid.TryParse(result.ToString(), out reREsult);
                }

                if (LocalUserSet)
                    base.CurrentUser = null;

                if (LocalTrasaction)
                    base.CommitTransaction();
                return reREsult;

            }

            catch
            {
                if (LocalTrasaction)
                    this.RollbackTransaction();
                throw;
            }
        }

        public BEClient.ATSResume GetATSResumeByContactAndProfile(Guid UserId, Guid ProfileId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetATSResumeByUserAndProfileId");
                Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);

                Database.AddInParameter(command, "@ProfileId", DbType.Guid, ProfileId);

                return base.FindOne(command, new GetATSResumeByUserAndProfile());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public BEClient.ATSResume GetATSResumeByVacancyAndLanguage(Guid VacancyId, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAtsResumedetailsByVacancyAndLanguage");
                Database.AddInParameter(command, "@VacancyId", DbType.Guid, VacancyId);

                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);

                Database.AddInParameter(command, "@UserId", DbType.Guid, base.CurrentUser.UserId);

                return base.FindOne(command, new GetATSResumeByVacancyAndLanguageFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }
        public BEClient.ATSResume GetATSResumeByVacancyAndLanguage_GearBox(Guid VacancyId, Guid LanguageId, Guid ApplicationId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAtsResumedetailsByVacancyAndLanguage_GearBox");
                Database.AddInParameter(command, "@VacancyId", DbType.Guid, VacancyId);

                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);

                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);

                return base.FindOne(command, new GetATSResumeByVacancyAndLanguageFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public BEClient.ATSResume GetRecordByRecordId(Guid pRecordId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetATSResumeById");
                Database.AddInParameter(command, "@ATSResumeId", DbType.Guid, pRecordId);



                return base.FindOne(command, new GetATSResumeById());
            }
            catch (Exception)
            {
                throw;
            }
        }


        public bool RemoveCoverLetter(Guid AtsResumeId)
        {
            try
            {
                bool LocalTrasaction = false;

                try
                {
                    if (base.Transaction == null)
                    {
                        base.BeginTransaction();
                        LocalTrasaction = true;
                    }
                    bool reREsult = false;
                    //parameterArray
                    DbCommand command = Database.GetStoredProcCommand("DeleteCoverLetter");

                    command.CommandTimeout = 100;

                    Database.AddInParameter(command, "@AtsResumeId", DbType.Guid, AtsResumeId);




                    var result = base.Remove(command);
                    if (result != null)
                    {
                        bool.TryParse(Convert.ToString(result), out reREsult);

                    }
                    if (LocalTrasaction)
                        base.CommitTransaction();

                    return reREsult;
                }

                catch (Exception)
                {
                    if (LocalTrasaction)
                        this.RollbackTransaction();
                    throw;
                }

            }
            catch
            {
                throw;
            }
        }


        public BEClient.ATSResume GetATSResumeByProfile(Guid ProfileId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetATSResumeByProfileId");

                Database.AddInParameter(command, "@ProfileId", DbType.Guid, ProfileId);
                return base.FindOne(command, new GetATSResumeByUserAndProfile());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<BEClient.ATSResume> GetAllProfile(Guid userId)
        {
            DbCommand command = Database.GetStoredProcCommand("GetAllProfile");

            Database.AddInParameter(command, "@UserId", DbType.Guid, userId);
            return base.Find(command, new GetAllProfileFactory());
        }

        public List<BEClient.ATSResume> GetAllCoverLetter(Guid userId)
        {
            DbCommand command = Database.GetStoredProcCommand("GetAllCoverLetter");

            Database.AddInParameter(command, "@UserId", DbType.Guid, userId);
            return base.Find(command, new GetAllCoverLetterFactory());
        }

        public List<BEClient.ATSResume> GetAllDocsByUserId(Guid UserId)
        {
            DbCommand command = Database.GetStoredProcCommand("GetAllDocsByUserId");

            Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);
            return base.Find(command, new GetAllDocsByUserIdFactory());
        }

        public List<BEClient.ATSResume> GetUserDocumentsByDocumentTypeId(Guid UserId, Guid DocumentTypeId)
        {
            DbCommand command = Database.GetStoredProcCommand("GetUserDocumentsByDocumentTypeId");
            Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);
            Database.AddInParameter(command, "@DocumentTypeId", DbType.Guid, DocumentTypeId);
            return base.Find(command, new FillUserDocumentFactory());
        }

        public List<BEClient.ATSResume> GetAllDocuments(Guid userId)
        {
            DbCommand command = Database.GetStoredProcCommand("GetdocumentsByUser");

            Database.AddInParameter(command, "@UserId", DbType.Guid, userId);
            return base.Find(command, new GetAllATSResumeByUser());
        }

        public BEClient.ATSResume GetProfileIdAndUserIdbyAtsResumeId(Guid AtsResumeId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetProfileIdAndUserIdbyAtsResumeId");
                Database.AddInParameter(command, "@AtsResumeId", DbType.Guid, AtsResumeId);
                return base.FindOne(command, new GetProfileIdAndUserIdFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal class GetProfileIdAndUserIdFactory : IDomainObjectFactory<BEClient.ATSResume>
        {
            public BEClient.ATSResume Construct(IDataReader reader)
            {
                BEClient.ATSResume objATSResume = new BEClient.ATSResume();
                objATSResume.ProfileId = HelperMethods.GetGuid(reader, "ProfileId");
                objATSResume.UserId = HelperMethods.GetGuid(reader, "UserId");
                return objATSResume;
            }
        }

        internal class GetATSResumeByUserAndProfile : IDomainObjectFactory<BEClient.ATSResume>
        {
            public BEClient.ATSResume Construct(IDataReader reader)
            {
                BEClient.ATSResume objATSResume = new BEClient.ATSResume();
                objATSResume.ATSResumeId = HelperMethods.GetGuid(reader, "ATSResumeId");

                objATSResume.ProfileId = HelperMethods.GetGuid(reader, "ProfileId");
                objATSResume.UserId = HelperMethods.GetGuid(reader, "UserId");
                objATSResume.Details = HelperMethods.GetString(reader, "Details");
                objATSResume.UploadedFileName = HelperMethods.GetString(reader, "UploadedFileName");

                objATSResume.NewFileName = HelperMethods.GetString(reader, "NewFileName");
                objATSResume.Path = HelperMethods.GetString(reader, "Path");
                objATSResume.TitleName = HelperMethods.GetString(reader, "TitleName");




                return objATSResume;
            }
        }



        internal class GetATSResumeByVacancyAndLanguageFactory : IDomainObjectFactory<BEClient.ATSResume>
        {
            public BEClient.ATSResume Construct(IDataReader reader)
            {
                BEClient.ATSResume objATSResume = new BEClient.ATSResume();
                objATSResume.ATSResumeId = HelperMethods.GetGuid(reader, "ATSResumeId");
                objATSResume.ProfileId = HelperMethods.GetGuid(reader, "ProfileId");
                objATSResume.CoverLetterId = HelperMethods.GetGuid(reader, "CoverLetterId");

                objATSResume.CoverLetterName = HelperMethods.GetString(reader, "CoverLetterName");
                objATSResume.ResumeName = HelperMethods.GetString(reader, "ResumeName");
                objATSResume.ProfileName = HelperMethods.GetString(reader, "ProfileName");
                objATSResume.AppliedOn = HelperMethods.GetDateTime(reader, "AppliedOn");

                return objATSResume;
            }
        }


        internal class GetAllATSResumeByUser : IDomainObjectFactory<BEClient.ATSResume>
        {
            public BEClient.ATSResume Construct(IDataReader reader)
            {
                BEClient.ATSResume objATSResume = new BEClient.ATSResume();
                objATSResume.ATSResumeId = HelperMethods.GetGuid(reader, "ATSResumeId");
                objATSResume.ProfileId = HelperMethods.GetGuid(reader, "ProfileId");
                objATSResume.UserId = HelperMethods.GetGuid(reader, "UserId");
                objATSResume.Details = HelperMethods.GetString(reader, "Details");
                objATSResume.UploadedFileName = HelperMethods.GetString(reader, "UploadedFileName");
                objATSResume.NewFileName = HelperMethods.GetString(reader, "NewFileName");
                objATSResume.Path = HelperMethods.GetString(reader, "Path");
                objATSResume.TitleName = HelperMethods.GetString(reader, "TitleName");
                objATSResume.IsCoverLetter = HelperMethods.GetBoolean(reader, "IsCoverLetter");
                objATSResume.CreatedOn = HelperMethods.GetDateTime(reader, "CreatedOn");
                objATSResume.UpdatedOn = HelperMethods.GetDateTime(reader, "UpdatedOn");
                objATSResume.IsDefaultProfile = HelperMethods.GetBoolean(reader, "IsDefault");
                objATSResume.IsResumeApplied = HelperMethods.GetBoolean(reader, "IsApplied"); 
                objATSResume.DocumentTypeId = HelperMethods.GetGuid(reader, "DocumentTypeId");
                objATSResume.DocumentTypeName = HelperMethods.GetString(reader, "DocumentName"); 
                return objATSResume;
            }
        }
        internal class GetATSResumeById : IDomainObjectFactory<BEClient.ATSResume>
        {
            public BEClient.ATSResume Construct(IDataReader reader)
            {
                BEClient.ATSResume objATSResume = new BEClient.ATSResume();
                objATSResume.ATSResumeId = HelperMethods.GetGuid(reader, "ATSResumeId");

                objATSResume.ProfileId = HelperMethods.GetGuid(reader, "ProfileId");
                objATSResume.UserId = HelperMethods.GetGuid(reader, "UserId");
                objATSResume.Details = HelperMethods.GetString(reader, "Details");
                objATSResume.UploadedFileName = HelperMethods.GetString(reader, "UploadedFileName");

                objATSResume.NewFileName = HelperMethods.GetString(reader, "NewFileName");
                objATSResume.Path = HelperMethods.GetString(reader, "Path");
                objATSResume.TitleName = HelperMethods.GetString(reader, "TitleName");
                objATSResume.IsCoverLetter = HelperMethods.GetBoolean(reader, "IsCoverLetter");
                objATSResume.CreatedOn = HelperMethods.GetDateTime(reader, "CreatedOn");
                objATSResume.IsResumeApplied = HelperMethods.GetBoolean(reader, "IsApplied"); 


                return objATSResume;
            }
        }
        internal class GetAllProfileFactory : IDomainObjectFactory<BEClient.ATSResume>
        {
            public BEClient.ATSResume Construct(IDataReader reader)
            {
                BEClient.ATSResume objATSResume = new BEClient.ATSResume();
                objATSResume.ATSResumeId = HelperMethods.GetGuid(reader, "ATSResumeId");
                objATSResume.ProfileId = HelperMethods.GetGuid(reader, "ProfileId");
                objATSResume.UploadedFileName = HelperMethods.GetString(reader, "ProfileName");
                return objATSResume;
            }
        }

        internal class GetAllCoverLetterFactory : IDomainObjectFactory<BEClient.ATSResume>
        {
            public BEClient.ATSResume Construct(IDataReader reader)
            {
                BEClient.ATSResume objATSResume = new BEClient.ATSResume();
                objATSResume.ATSResumeId = HelperMethods.GetGuid(reader, "ATSResumeId");
                objATSResume.ProfileId = HelperMethods.GetGuid(reader, "ProfileId");
                objATSResume.UploadedFileName = HelperMethods.GetString(reader, "UploadedFileName");
                return objATSResume;
            }
        }

        internal class GetAllDocsByUserIdFactory : IDomainObjectFactory<BEClient.ATSResume>
        {
            public BEClient.ATSResume Construct(IDataReader reader)
            {
                BEClient.ATSResume objATSResume = new BEClient.ATSResume();
                objATSResume.ATSResumeId = HelperMethods.GetGuid(reader, "ATSResumeId");
                objATSResume.ProfileId = HelperMethods.GetGuid(reader, "ProfileId");
                objATSResume.UploadedFileName = HelperMethods.GetString(reader, "UploadedFileName");
                objATSResume.DocumentTypeId = HelperMethods.GetGuid(reader, "DocumentTypeId");
                return objATSResume;
            }
        }

        internal class FillUserDocumentFactory : IDomainObjectFactory<BEClient.ATSResume>
        {
            public BEClient.ATSResume Construct(IDataReader reader)
            {
                BEClient.ATSResume objATSResume = new BEClient.ATSResume();
                objATSResume.ATSResumeId = HelperMethods.GetGuid(reader, "ATSResumeId");
                objATSResume.UploadedFileName = HelperMethods.GetString(reader, "UploadedFileName");
                return objATSResume;
            }
        }
    }
}
