using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Data.Common;
using System.Data;

namespace ATS.DataAccess
{
    public class ProfileRepository : Repository<BEClient.Profile>
    {
        public ProfileRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }
        public Guid AddProfile(BEClient.Profile profile)
        {
            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertProfile");

                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, base.CurrentClient.CurrentLanguageId);

                Database.AddInParameter(command, "@UserId", DbType.Guid, profile.UserId);

                Database.AddInParameter(command, "@ClientId", DbType.Guid, base.CurrentClient.ClientId);


                Database.AddInParameter(command, "@ProfileName", DbType.String, profile.ProfileName);

                Database.AddInParameter(command, "@Objectives", DbType.String, profile.Objectives);

                Database.AddInParameter(command, "@Hobbies", DbType.String, profile.Hobbies);

                Database.AddInParameter(command, "@Category", DbType.String, profile.Category);

                Database.AddInParameter(command, "@SubCategory", DbType.String, profile.SubCategory);

                Database.AddInParameter(command, "@CurrentSalary", DbType.Decimal, profile.CurrentSalary);

                Database.AddInParameter(command, "@ExpectedSalary", DbType.Decimal, profile.ExpectedSalary);

                Database.AddInParameter(command, "@IsDefault", DbType.Boolean, profile.IsDefault);

                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, profile.IsDelete);

                Database.AddInParameter(command, "@IsManual", DbType.Boolean, profile.IsManual);
               
                Database.AddOutParameter(command, "ProfileId", DbType.Guid, 16); ;

                var result = base.Add(command, "ProfileId");

                if (result != null)
                {
                    Guid.TryParse(result.ToString(), out reREsult);
                }

                return reREsult;
            }

            catch
            {
                throw;
            }
        }

        public BEClient.Profile GetLastUpdatedProfileByUserId(Guid recordId)
        {
            BEClient.Profile reREsult = null;

            try
            {

                DbCommand command = Database.GetStoredProcCommand("GetLastUpdatedProfile");
                Database.AddInParameter(command, "@UserId", DbType.Guid, recordId);
                return base.FindOne(command, new GetLastUpdateProfile());
            }
            catch (Exception)
            {

                throw;
            }
        }


        public bool SetProfileAsDefault(Guid ProfileId)
        {
            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("SetDefaultProfile");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@UserId", DbType.Guid, base.CurrentUser.UserId);

                Database.AddInParameter(command, "@ProfileId", DbType.Guid,ProfileId);

                


                var result = base.Save(command);

                if (result != null)
                {
                    
                 
                    
                        reREsult = true;
                    
                }

                return reREsult;
            }

            catch
            {
                throw;
            }

        }

        public BEClient.Profile GetProfileByProfileId(Guid ProfileId)
        {
            BEClient.Profile reREsult = null;

            try
            {

                DbCommand command = Database.GetStoredProcCommand("GetProfileByProfileId");
                Database.AddInParameter(command, "@ProfileId", DbType.Guid, ProfileId);
                return base.FindOne(command, new GetProfileByProfileIdFactory());
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<BEClient.Profile> GetProfileByUserId(Guid recordId)
        {
            BEClient.Profile reREsult = null;

            try
            {

                DbCommand command = Database.GetStoredProcCommand("GetProfilesByUser");
                Database.AddInParameter(command, "@UserId", DbType.Guid, recordId);
                return base.Find(command, new GetProfileByUser());
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Guid ValidateProfile(BEClient.Profile objProfile)
        {
            try
            {
                Guid reUserId = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("ValidateProfile");

                Database.AddInParameter(command, "@UserId", DbType.Guid, objProfile.UserId);

                Database.AddInParameter(command, "@ProfileName", DbType.String, objProfile.ProfileName);

                Database.AddInParameter(command, "@ClientId", DbType.Guid, objProfile.ClientId);

                Database.AddOutParameter(command, "@IsError", DbType.Int32, 4);

                string[] outParams = new string[] {"@IsError" };

                var result = base.Add(command, outParams, false);

                String errorCode = result[outParams[0]].ToString();
                if (result != null)
                {
                    switch (errorCode)
                    {

                        case "102":
                            { 
                                throw new Exception("Profile Name Already Exists.");
                            }


                    }
                    
                
                }
                return reUserId;

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
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateProfile");

                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@ProfileId", DbType.Guid, profile.ProfileId);
                Database.AddInParameter(command, "@ProfileName", DbType.String, profile.ProfileName);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, false);
                var result = base.Save(command);
                if (result != null)
                {
                    Boolean.TryParse(result.ToString(), out reREsult);
                }
                return reREsult;
            }
            catch
            {
                throw;
            }

        }
        internal class ValidateProfileFactory : IDomainObjectFactory<BEClient.Profile>
        {
            public BEClient.Profile Construct(IDataReader reader)
            {
                BEClient.Profile objprofile = new BEClient.Profile();

                objprofile.ProfileId = HelperMethods.GetGuid(reader, "ProfileId");
                objprofile.UserId = HelperMethods.GetGuid(reader, "UserId");
                objprofile.ProfileName = HelperMethods.GetString(reader, "ProfileName");
              

                return objprofile;
            }
        }

        internal class GetLastUpdateProfile : IDomainObjectFactory<BEClient.Profile>
        {
            public BEClient.Profile Construct(IDataReader reader)
            {
                BEClient.Profile objprofile = new BEClient.Profile();

                objprofile.ProfileId = HelperMethods.GetGuid(reader, "ProfileId");
                objprofile.UserId = HelperMethods.GetGuid(reader, "UserId");
                objprofile.ProfileName = HelperMethods.GetString(reader, "ProfileName");
                objprofile.Objectives = HelperMethods.GetString(reader, "Objectives");
                objprofile.Hobbies = HelperMethods.GetString(reader, "Hobbies");
                objprofile.Category = HelperMethods.GetString(reader, "Category");
                objprofile.SubCategory = HelperMethods.GetString(reader, "SubCategory");
                objprofile.CurrentSalary = HelperMethods.GetDecimal(reader, "CurrentSalary");
                objprofile.ExpectedSalary = HelperMethods.GetDecimal(reader, "ExpectedSalary");

                return objprofile;
            }
        }
      
        internal class GetProfileByProfileIdFactory : IDomainObjectFactory<BEClient.Profile>
        {
            public BEClient.Profile Construct(IDataReader reader)
            {
                BEClient.Profile objprofile = new BEClient.Profile();

                objprofile.ProfileId = HelperMethods.GetGuid(reader, "ProfileId");
                objprofile.LanguageId = HelperMethods.GetGuid(reader, "LanguageId");
                objprofile.UserId = HelperMethods.GetGuid(reader, "UserId");
                objprofile.ProfileName = HelperMethods.GetString(reader, "ProfileName");
                objprofile.Objectives = HelperMethods.GetString(reader, "Objectives");
                objprofile.Hobbies = HelperMethods.GetString(reader, "Hobbies");
                objprofile.Category = HelperMethods.GetString(reader, "Category");
                objprofile.SubCategory = HelperMethods.GetString(reader, "SubCategory");
                objprofile.CurrentSalary = HelperMethods.GetDecimal(reader, "CurrentSalary");
                objprofile.ExpectedSalary = HelperMethods.GetDecimal(reader, "ExpectedSalary");
                objprofile.IsDefault = HelperMethods.GetBoolean(reader, "IsDefault");
                objprofile.ExtensionTypes = HelperMethods.GetString(reader, "ExtensionTypes");

                return objprofile;
            }
        }


        public bool DeleteProfile(Guid profileid, Guid userid)
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
                DbCommand command = Database.GetStoredProcCommand("DeleteProfile");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ProfileId", DbType.Guid, profileid);

                Database.AddInParameter(command, "@UserId", DbType.Guid, userid);

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


        internal class GetProfileByUser : IDomainObjectFactory<BEClient.Profile>
        {
            public BEClient.Profile Construct(IDataReader reader)
            {
                BEClient.Profile objprofile = new BEClient.Profile();

                objprofile.ProfileId = HelperMethods.GetGuid(reader, "ProfileId");
                objprofile.UserId = HelperMethods.GetGuid(reader, "UserId");
                objprofile.ProfileName = HelperMethods.GetString(reader, "ProfileName");
                objprofile.Objectives = HelperMethods.GetString(reader, "Objectives");
                objprofile.Hobbies = HelperMethods.GetString(reader, "Hobbies");
                objprofile.Category = HelperMethods.GetString(reader, "Category");
                objprofile.SubCategory = HelperMethods.GetString(reader, "SubCategory");
                objprofile.CurrentSalary = HelperMethods.GetDecimal(reader, "CurrentSalary");
                objprofile.ExpectedSalary = HelperMethods.GetDecimal(reader, "ExpectedSalary");
                objprofile.IsDefault = HelperMethods.GetBoolean(reader, "IsDefault");

                return objprofile;
            }
        }
    }
}
