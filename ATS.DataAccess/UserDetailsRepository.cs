using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Data;
using System.Data.Common;

namespace ATS.DataAccess
{
    public class UserDetailsRepository : Repository<BEClient.UserDetails>
    {
        public UserDetailsRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public BEClient.UserDetails GetUserByUserId(Guid userid)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetUserDetailsByUserId");
                Database.AddInParameter(command, "@UserId", DbType.Guid, userid);
                return base.FindOne(command, new GetUserByUserIdFactory());
            }
            catch
            {
                throw;
            }
        }

      
        public BEClient.UserDetails GetUserDetailsByAppId(Guid ApplicationId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetUserDetailsByAppId");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                return base.FindOne(command, new GetUserDetailsByAppIdFactory());
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateUserDetails(BEClient.UserDetails pUserDetails)
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
                DbCommand command = Database.GetStoredProcCommand("UpdateUserDetails");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@UserId", DbType.Guid, pUserDetails.UserId);
                Database.AddInParameter(command, "@HomeEmail", DbType.String, pUserDetails.HomeEmail);
                Database.AddInParameter(command, "@WorkEmail", DbType.String, pUserDetails.WorkEmail);
                Database.AddInParameter(command, "@Affix", DbType.String, pUserDetails.Affix);
                Database.AddInParameter(command, "@Fax", DbType.String, pUserDetails.Fax);
                Database.AddInParameter(command, "@WebSite", DbType.String, pUserDetails.Website);
                Database.AddInParameter(command, "@PostofficeBox", DbType.String, pUserDetails.PostOfficeBox);
                Database.AddInParameter(command, "@MiddleName", DbType.String, pUserDetails.MiddleName);
                Database.AddInParameter(command, "@FirstName", DbType.String, pUserDetails.FirstName);
                Database.AddInParameter(command, "@LastName", DbType.String, pUserDetails.LastName);
                Database.AddInParameter(command, "@Address", DbType.String, pUserDetails.Address);
                Database.AddInParameter(command, "@City", DbType.String, pUserDetails.City); ;
                Database.AddInParameter(command, "@State", DbType.String, pUserDetails.State);
                Database.AddInParameter(command, "@Zip", DbType.String, pUserDetails.Zip);
                Database.AddInParameter(command, "@BusinessPhoneNo", DbType.String, pUserDetails.BusinessPhoneNo);
                Database.AddInParameter(command, "@HomePhone", DbType.String, pUserDetails.HomePhone);
                Database.AddInParameter(command, "@MobilePhone", DbType.String, pUserDetails.MobilePhone);
                Database.AddInParameter(command, "@Pager", DbType.String, pUserDetails.Pager);
                Database.AddInParameter(command, "@Misdemeanor", DbType.Boolean, pUserDetails.Misdemeanor);
                Database.AddInParameter(command, "@MisdemeanorExplain", DbType.String, pUserDetails.MisdemeanorExplain);
                Database.AddInParameter(command, "@MilitaryService", DbType.Boolean, pUserDetails.MilitaryService);
                Database.AddInParameter(command, "@MilitaryTypeDischarge", DbType.String, pUserDetails.MilitaryTypeDischarge);


                var result = base.Save(command);

                if (result != null)
                {
                    bool.TryParse(result.ToString(), out reREsult);
                }
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

        public bool UpdateEmergencyDetails(BEClient.UserDetails pUserDetails)
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
                DbCommand command = Database.GetStoredProcCommand("UpdateEmergensyContactInfo");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@UserId", DbType.Guid, pUserDetails.UserId);
                Database.AddInParameter(command, "@EmergencyContact1", DbType.String, pUserDetails.EmergencyContact1);
                Database.AddInParameter(command, "@EmergencyContact2", DbType.String, pUserDetails.EmergencyContact2);
                Database.AddInParameter(command, "@EmergencyContact1Phone", DbType.String, pUserDetails.EmergencyContact1Phone);
                Database.AddInParameter(command, "@EmergencyContact2Phone", DbType.String, pUserDetails.EmergencyContact2Phone);
                var result = base.Save(command);
                if (result != null)
                {
                    bool.TryParse(result.ToString(), out reREsult);
                }
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

        public Guid AddUserDetails(BEClient.UserDetails pUserDetails)
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
                DbCommand command = Database.GetStoredProcCommand("InsertUserDetails");

                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@UserId", DbType.Guid, pUserDetails.UserId);
                Database.AddInParameter(command, "@HomeEmail", DbType.String, pUserDetails.HomeEmail);
                Database.AddInParameter(command, "@WorkEmail", DbType.String, pUserDetails.WorkEmail);
                Database.AddInParameter(command, "@Affix", DbType.String, pUserDetails.Affix);
                Database.AddInParameter(command, "@MiddleName", DbType.String, pUserDetails.MiddleName);
                Database.AddInParameter(command, "@Fax", DbType.String, pUserDetails.Fax);
                Database.AddInParameter(command, "@WebSite", DbType.String, pUserDetails.Website);
                Database.AddInParameter(command, "@PostofficeBox", DbType.String, pUserDetails.PostOfficeBox);
                Database.AddInParameter(command, "@FirstName", DbType.String, pUserDetails.FirstName); ;
                Database.AddInParameter(command, "@LastName", DbType.String, pUserDetails.LastName); ;
                Database.AddInParameter(command, "@Address", DbType.String, pUserDetails.Address); ;
                Database.AddInParameter(command, "@City", DbType.String, pUserDetails.City); ;
                Database.AddInParameter(command, "@State", DbType.String, pUserDetails.State); ;
                Database.AddInParameter(command, "@Zip", DbType.String, pUserDetails.Zip); ;
                Database.AddInParameter(command, "@BusinessPhoneNo", DbType.String, pUserDetails.BusinessPhoneNo); ;
                Database.AddInParameter(command, "@HomePhone", DbType.String, pUserDetails.HomePhone);
                Database.AddInParameter(command, "@MobilePhone", DbType.String, pUserDetails.MobilePhone);
                Database.AddInParameter(command, "@Pager", DbType.String, pUserDetails.Pager);
                Database.AddInParameter(command, "@Misdemeanor", DbType.Boolean, pUserDetails.Misdemeanor);
                Database.AddInParameter(command, "@MisdemeanorExplain", DbType.String, pUserDetails.MisdemeanorExplain);
                Database.AddInParameter(command, "@MilitaryService", DbType.Boolean, pUserDetails.MilitaryService);
                Database.AddInParameter(command, "@MilitaryTypeDischarge", DbType.String, pUserDetails.MilitaryTypeDischarge);
                Database.AddInParameter(command, "@EmergencyContact1", DbType.String, pUserDetails.EmergencyContact1);
                Database.AddInParameter(command, "@EmergencyContact2", DbType.String, pUserDetails.EmergencyContact2);
                Database.AddInParameter(command, "@EmergencyContact1Phone", DbType.String, pUserDetails.EmergencyContact1Phone);
                Database.AddInParameter(command, "@EmergencyContact2Phone", DbType.String, pUserDetails.EmergencyContact2Phone);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, pUserDetails.IsDelete);

                Database.AddOutParameter(command, "UserDetailsId", DbType.Guid, 16); ;
                var result = base.Add(command, "UserDetailsId");

                if (result != null)
                {
                    Guid.TryParse(result.ToString(), out reREsult);
                }
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

        public List<BEClient.UserDetails> GetInterviewerByVacRndId(Guid VacRndId, Guid AppId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetInterviewerbyVacRndId");
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, AppId);
                return base.Find(command, new GetInterviewerByVacRndIdFactory());
            }
            catch
            {
                throw;
            }
        }

        internal class GetUserByUserIdFactory : IDomainObjectFactory<BEClient.UserDetails>
        {
            public BEClient.UserDetails Construct(IDataReader reader)
            {
                BEClient.UserDetails objUserDetails = new BEClient.UserDetails();
                objUserDetails.UserId = HelperMethods.GetGuid(reader, "ContactId");
                objUserDetails.HomeEmail = HelperMethods.GetString(reader, "HomeEmail");
                objUserDetails.FirstName = HelperMethods.GetString(reader, "FirstName");
                objUserDetails.LastName = HelperMethods.GetString(reader, "LastName");
                objUserDetails.Address = HelperMethods.GetString(reader, "Address");
                objUserDetails.City = HelperMethods.GetString(reader, "City");
                objUserDetails.State = HelperMethods.GetString(reader, "State");
                objUserDetails.Zip = HelperMethods.GetString(reader, "Zip");
                objUserDetails.BusinessPhoneNo = HelperMethods.GetString(reader, "BusinessPhoneNo");
                objUserDetails.HomePhone = HelperMethods.GetString(reader, "HomePhone");
                objUserDetails.MobilePhone = HelperMethods.GetString(reader, "MobilePhone");
                objUserDetails.Pager = HelperMethods.GetString(reader, "Pager");
                objUserDetails.MiddleName = HelperMethods.GetString(reader, "MiddleName");
                objUserDetails.WorkEmail = HelperMethods.GetString(reader, "WorkEmail");
                objUserDetails.Misdemeanor = HelperMethods.GetBoolean(reader, "Misdemeanor");
                objUserDetails.MisdemeanorExplain = HelperMethods.GetString(reader, "MisdemeanorExplain");
                objUserDetails.MilitaryService = HelperMethods.GetBoolean(reader, "MilitaryService");
                objUserDetails.Affix = HelperMethods.GetString(reader, "Affix");
                objUserDetails.Fax = HelperMethods.GetString(reader, "Fax");
                objUserDetails.Website = HelperMethods.GetString(reader, "WebSite");
                objUserDetails.PostOfficeBox = HelperMethods.GetString(reader, "PostOfficeBox");
                objUserDetails.MilitaryTypeDischarge = HelperMethods.GetString(reader, "MilitaryTypeDischarge");
                objUserDetails.EmergencyContact1 = HelperMethods.GetString(reader, "EmergencyContact1");
                objUserDetails.EmergencyContact2 = HelperMethods.GetString(reader, "EmergencyContact2");
                objUserDetails.EmergencyContact1Phone = HelperMethods.GetString(reader, "EmergencyContact1Phone");
                objUserDetails.EmergencyContact2Phone = HelperMethods.GetString(reader, "EmergencyContact2Phone");
                objUserDetails.ClientId = HelperMethods.GetGuid(reader, "ClientId");
                objUserDetails.UserId = HelperMethods.GetGuid(reader, "UserId");
                objUserDetails.ClientIdName = HelperMethods.GetString(reader, "ClientIdName");
                objUserDetails.UserName = HelperMethods.GetString(reader, "UserName");
                objUserDetails.IsDelete = HelperMethods.GetBoolean(reader, "IsDelete");
                objUserDetails.ProfileImage = HelperMethods.GetString(reader, "ProfileImage");
                return objUserDetails;
            }
        }
    

        internal class GetUserDetailsByAppIdFactory : IDomainObjectFactory<BEClient.UserDetails>
        {
            public BEClient.UserDetails Construct(IDataReader reader)
            {
                BEClient.UserDetails objUserDetails = new BEClient.UserDetails();
                objUserDetails.FirstName = HelperMethods.GetString(reader, "FirstName");
                objUserDetails.LastName = HelperMethods.GetString(reader, "LastName");
                objUserDetails.UserName = HelperMethods.GetString(reader, "Username");
                return objUserDetails;
            }
        }

        internal class GetAllUserFactory : IDomainObjectFactory<BEClient.UserDetails>
        {
            public BEClient.UserDetails Construct(IDataReader reader)
            {
                BEClient.UserDetails objUserDetails = new BEClient.UserDetails();
                objUserDetails.UserId = HelperMethods.GetGuid(reader, "UserId");
                objUserDetails.FirstName = HelperMethods.GetString(reader, "FirstName");
                return objUserDetails;
            }
        }

        internal class GetInterviewerByVacRndIdFactory : IDomainObjectFactory<BEClient.UserDetails>
        {
            public BEClient.UserDetails Construct(IDataReader reader)
            {
                BEClient.UserDetails UserDetails = new BEClient.UserDetails();
                UserDetails.UserId = HelperMethods.GetGuid(reader, "UserId");
                UserDetails.FirstName = HelperMethods.GetString(reader, "InterviewerName");
                return UserDetails;
            }
        }
    }
}