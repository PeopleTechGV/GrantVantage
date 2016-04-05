using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using BEMaster = ATS.BusinessEntity.Master;

namespace ATS.DataAccess.Master
{
    public class UserMasterRepository : Repository<BEMaster.UserMaster>
    {
        public BEMaster.UserMaster GetUserByUserName(String userName)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetUserByUserName");
                Database.AddInParameter(command, "@UserName", DbType.String, userName);
                BEMaster.UserMaster validUserMaster = base.FindOne(command, new GetAllUserData());
                if (validUserMaster != null)
                {
                    return validUserMaster;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public BEMaster.UserMaster ValidateUser(String userName, String password)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("ValidateUser");
                Database.AddInParameter(command, "@UserName", DbType.String, userName);
                Database.AddInParameter(command, "@Password", DbType.String, password);
                BEMaster.UserMaster validUserMaster = base.FindOne(command, new GetAllUserData());
                if (validUserMaster != null)
                {
                    return validUserMaster;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal class GetAllUserData : IDomainObjectFactory<BEMaster.UserMaster>
        {
            BEMaster.UserMaster IDomainObjectFactory<BEMaster.UserMaster>.Construct(IDataReader reader)
            {
                try
                {
                    BEMaster.UserMaster objUser = new BEMaster.UserMaster();
                    objUser.UserId = HelperMethods.GetGuid(reader, "UserId");
                    objUser.UserName = HelperMethods.GetString(reader, "UserName");
                    objUser.FirstName = HelperMethods.GetString(reader, "FirstName");
                    objUser.LastName = HelperMethods.GetString(reader, "LastName");
                    
                    return objUser;
                }
                catch
                {
                    throw;
                }

            }
        }
    }
}
