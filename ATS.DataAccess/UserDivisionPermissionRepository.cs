using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATS.BusinessEntity;
using System.Data.Common;
using ATS.DataAccess;
using System.Data;

namespace ATS.DataAccess
{
    public class UserDivisionPermissionRepository : Repository<UserDivisionPermission>
    {
        public UserDivisionPermissionRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public Guid Add(UserDivisionPermission pUserDivisionPermission)
        {

            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertUserDivisionPermission");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ClientId", DbType.Guid, pUserDivisionPermission.ClientId);

                Database.AddInParameter(command, "@DivisionId", DbType.Guid, pUserDivisionPermission.DivisionId);
                
                Database.AddInParameter(command, "@UserId", DbType.Guid, pUserDivisionPermission.UserId);


                Database.AddInParameter(command, "@Description", DbType.String, pUserDivisionPermission.Description);

                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, pUserDivisionPermission.IsDelete);

                Database.AddOutParameter(command, "UserDivisionPermissionId", DbType.Guid, 16); ;

                var result = base.Add(command, "UserDivisionPermissionId");

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


        public bool Update(UserDivisionPermission pUserDivisionPermission)
        {

            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateUserDivisionPermission");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@UserDivisionPermissionId", DbType.Guid, pUserDivisionPermission.UserDivisionPermissionId);
               
                Database.AddInParameter(command, "@ClientId", DbType.Guid, pUserDivisionPermission.ClientId);

                Database.AddInParameter(command, "@DivisionId", DbType.Guid, pUserDivisionPermission.DivisionId);

                Database.AddInParameter(command, "@UserId", DbType.Guid, pUserDivisionPermission.UserId);
                
                Database.AddInParameter(command, "@Description", DbType.String, pUserDivisionPermission.Description);

                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, pUserDivisionPermission.IsDelete);

                var result = base.Save(command);

                if (result != null)
                {
                    bool.TryParse(result.ToString(), out reREsult);
                }

                return reREsult;
            }

            catch
            {
                throw;
            }
        }

        public bool DeleteDivisionByUserAndClient(Guid clientId, Guid userId)
        {
            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("DeleteDivisionByUserAndClient");

                Database.AddInParameter(command, "@UserId", System.Data.DbType.Guid, userId);

                Database.AddInParameter(command, "@ClientId", DbType.Guid, clientId);

                var result = base.Remove(command);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<UserDivisionPermission> GetAllUserDivisionPermissionByClient(Guid ClientId, Guid LanguageId,String divisions)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllUserDivisionPermissionBy");
                if (!string.IsNullOrEmpty(divisions))
                {
                    Database.AddInParameter(command, "@Users", DbType.String, divisions);
                }
              
                Database.AddInParameter(command, "@ClientId", DbType.Guid, ClientId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);

                return base.Find(command, new GetAllUserDivisionPermissionFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }
        public UserDivisionPermission GetUserDivisionPermissionById(Guid UserDivisionPermissionId,Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetUserDivisionPermissionById");
                Database.AddInParameter(command, "@UserDivisionPermissionId", DbType.Guid, UserDivisionPermissionId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);

                return base.FindOne(command, new GetUserDivisionPermissionByIdFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<UserDivisionPermission> GetAllAdhocDivisionByUser(Guid UserId, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllAdhocDivisionByUser");
                Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);

                return base.Find(command, new GetAllAdhocDivisionByUserFactory(), false);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteAdhocDivisionById(Guid UserDivisionPermissionId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("DeleteAdhocDivisionById");
                Database.AddInParameter(command, "@UserDivisionPerMissionId", DbType.Guid, UserDivisionPermissionId);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, false);
                
                return base.Remove(command);
            }
            catch
            {
                throw;
            }
        }

        internal class GetUserDivisionPermissionByIdFactory : IDomainObjectFactory<UserDivisionPermission>
        {
            public UserDivisionPermission Construct(IDataReader reader)
            {
                UserDivisionPermission objUserDivisionPermission = new UserDivisionPermission();
                objUserDivisionPermission.UserDivisionPermissionId = HelperMethods.GetGuid(reader, "UserDivisionPermissionId");
                
                objUserDivisionPermission.DivisionId = HelperMethods.GetGuid(reader, "DivisionId");
                objUserDivisionPermission.ClientId = HelperMethods.GetGuid(reader, "ClientId");
                objUserDivisionPermission.UserId = HelperMethods.GetGuid(reader, "UserId");
                objUserDivisionPermission.DivisionName = HelperMethods.GetString(reader, "DivisionName");
                objUserDivisionPermission.UserFirstFame = HelperMethods.GetString(reader, "UserFirstName");



                return objUserDivisionPermission;
            }
        }
        internal class GetAllAdhocDivisionByUserFactory : IDomainObjectFactory<UserDivisionPermission>
        {
            public UserDivisionPermission Construct(IDataReader reader)
            {
                UserDivisionPermission objUserDivisionPermission = new UserDivisionPermission();
                objUserDivisionPermission.UserDivisionPermissionId = HelperMethods.GetGuid(reader, "UserDivisionPermissionId");
                objUserDivisionPermission.DivisionId = HelperMethods.GetGuid(reader, "DivisionId");
                objUserDivisionPermission.UserId = HelperMethods.GetGuid(reader, "UserId");
                objUserDivisionPermission.DivisionName = HelperMethods.GetString(reader, "DivisionName");
            



                return objUserDivisionPermission;
            }
        }

        
        
        internal class GetAllUserDivisionPermissionFactory : IDomainObjectFactory<UserDivisionPermission>
        {
            public UserDivisionPermission Construct(IDataReader reader)
            {
                UserDivisionPermission objUserDivisionPermission = new UserDivisionPermission();
                objUserDivisionPermission.UserDivisionPermissionId = HelperMethods.GetGuid(reader, "UserDivisionPermissionId");
                objUserDivisionPermission.DivisionId = HelperMethods.GetGuid(reader, "DivisionId");
                objUserDivisionPermission.ClientId = HelperMethods.GetGuid(reader, "ClientId");
                objUserDivisionPermission.UserId = HelperMethods.GetGuid(reader, "UserId");
                objUserDivisionPermission.DivisionName = HelperMethods.GetString(reader, "DivisionName");
                objUserDivisionPermission.UserFirstFame = HelperMethods.GetString(reader, "UserFirstName");



                return objUserDivisionPermission;
            }
        }

    }
}
