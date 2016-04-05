using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Data;
using System.Data.Common;


namespace ATS.DataAccess
{
  public  class OrgDetailsRepository:Repository<BEClient.Organization>
    {
        public OrgDetailsRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public BEClient.Organization GetUserOrgByUserId(Guid userid)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetUserOrgDetailsByUserId");
                Database.AddInParameter(command, "@UserId", DbType.Guid, userid);
                return base.FindOne(command, new GetUserOrgByUserIdFactory());
            }
            catch
            {
                throw;
            }
        }
        //CR-2
        internal class GetUserOrgByUserIdFactory : IDomainObjectFactory<BEClient.Organization>
        {
            public BEClient.Organization Construct(IDataReader reader)
            {
                BEClient.Organization objUseorgrDetails = new BEClient.Organization();
                objUseorgrDetails.OrganizationID = HelperMethods.GetInt32(reader, "OrganizationID");
                objUseorgrDetails.OrganizationName = HelperMethods.GetString(reader, "OrganizationName");
                objUseorgrDetails.CreatedOn = HelperMethods.GetDateTime(reader, "CreatedOn");
                objUseorgrDetails.CreatedBy = HelperMethods.GetString(reader, "CreatedBy");
                objUseorgrDetails.ModifiedOn = HelperMethods.GetDateTime(reader, "ModifiedOn");
                objUseorgrDetails.ModifiedBy = HelperMethods.GetString(reader, "ModifiedBy");

                objUseorgrDetails.UserId = HelperMethods.GetGuid(reader, "UserId");

                return objUseorgrDetails;
            }
        }

        public bool UpdateUserorgDetails(BEClient.Organization pUserDetails)
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
                DbCommand command = Database.GetStoredProcCommand("UpdateUserOrgDetails");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@UserId", DbType.Guid, pUserDetails.UserId);
                Database.AddInParameter(command, "@OrganizationName", DbType.String, pUserDetails.OrganizationName);
                Database.AddInParameter(command, "@OrgID", DbType.Int32, pUserDetails.OrganizationID);
               // Database.AddInParameter(command, "@ModifiedBy", DbType.DateTime, pUserDetails.ModifiedBy);
               // Database.AddInParameter(command, "@OrganizationID", DbType.String, pUserDetails.OrganizationID);
                


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

    }
}
