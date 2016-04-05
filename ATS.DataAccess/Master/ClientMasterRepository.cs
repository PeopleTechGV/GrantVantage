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
    public class ClientMasterRepository :Repository<BEMaster.ClientMaster>
    {
        public ClientMasterRepository() { }
        public ClientMasterRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public List<BEMaster.ClientMaster> GetAllClient()
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllClient");
                return base.Find(command, new GetAllClientFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public BEMaster.ClientMaster GetClientDetailById(Guid clientId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetClientDetailById");
                Database.AddInParameter(command, "@ClientId", DbType.Guid, clientId);
                return base.FindOne(command, new GetClientDetailByIdFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Guid InsertClientDetail(BEMaster.ClientMaster objClient, string fieldValue)
        {

            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertClientDetail");

                Database.AddInParameter(command, "@ClientName", DbType.String, objClient.ClientName);

                Database.AddInParameter(command, "@ContactPerson", DbType.String, objClient.ContactPerson);

                Database.AddInParameter(command, "@ContactNo", DbType.String, objClient.ContactNo);

                Database.AddInParameter(command, "@SubDomain", DbType.String, objClient.SubDomain);
                Database.AddInParameter(command, "@WebSite", DbType.String, objClient.WebSite);

                Database.AddInParameter(command, "@ConnectionString", DbType.String, objClient.ConnectionString);

                Database.AddInParameter(command, "@DatabaseName", DbType.String, objClient.DatabaseName);

                Database.AddInParameter(command, "@SubscriptionId", DbType.Guid, objClient.SubscriptionId);

                Database.AddInParameter(command, "@CurrencySymbol", DbType.String, objClient.Currency);

                if (objClient.StartDate.Equals(DateTime.MinValue))
                {
                    objClient.StartDate = DateTime.UtcNow;
                }
                Database.AddInParameter(command, "@StartDate", DbType.DateTime, objClient.StartDate);

                if (objClient.EndDate.Equals(DateTime.MinValue))
                {
                    objClient.EndDate = DateTime.UtcNow;
                }
                Database.AddInParameter(command, "@EndDate", DbType.DateTime, objClient.EndDate);

                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, false);

                Database.AddOutParameter(command, "ClientId", DbType.Guid, 32);

                Database.AddInParameter(command, "@FieldValue", DbType.String, fieldValue);

                Database.AddOutParameter(command, "@IsError", DbType.Int32, 4);

                string[] outParams = new string[] { "ClientId", "@IsError" };

                var result = base.Add(command, outParams, false);

                String errorCode = result[outParams[1]].ToString();

                if (result != null)
                {
                    switch (errorCode)
                    {
                        case "101":
                            throw new Exception("Client already exists.");
                    }
                    Guid.TryParse(result[outParams[0]].ToString(), out reREsult);
                }

                return reREsult;
            }

            catch
            {
                throw;
            }
        }

        public Guid InsertClientLanguage(Guid clientId,Guid languageId)
        {

            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertClientLanguage");

                Database.AddInParameter(command, "@ClientId", DbType.Guid, clientId);

                Database.AddInParameter(command, "@LanguageId", DbType.Guid, languageId);

                Database.AddOutParameter(command, "ClientLanguageId", DbType.Guid, 32);

                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, false);

                var result = base.Add(command, "ClientLanguageId", false);

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

        public bool UpdateClientDetail(BEMaster.ClientMaster objClient, string fieldValue)
        {

            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateClientDetail");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ClientName", DbType.String, objClient.ClientName);

                Database.AddInParameter(command, "@ContactPerson", DbType.String, objClient.ContactPerson);

                Database.AddInParameter(command, "@ContactNo", DbType.String, objClient.ContactNo);

                Database.AddInParameter(command, "@SubDomain", DbType.String, objClient.SubDomain);

                Database.AddInParameter(command, "@WebSite", DbType.String, objClient.WebSite);

                Database.AddInParameter(command, "@ConnectionString", DbType.String, objClient.ConnectionString);

                Database.AddInParameter(command, "@DatabaseName", DbType.String, objClient.DatabaseName);

                Database.AddInParameter(command, "@SubscriptionId", DbType.Guid, objClient.SubscriptionId);

                Database.AddInParameter(command, "@CurrencySymbol", DbType.String, objClient.Currency);
                
                Database.AddInParameter(command, "@StartDate", DbType.DateTime, objClient.StartDate);

                Database.AddInParameter(command, "@EndDate", DbType.DateTime, objClient.EndDate);

                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, false);

                Database.AddInParameter(command, "@ClientId", DbType.Guid, objClient.ClientId);

                Database.AddInParameter(command, "@FieldValue", DbType.String, fieldValue);

                Database.AddOutParameter(command, "IsError", DbType.Int32, 4);

                var result = base.SaveWithoutDuplication(command, "IsError", false);

                if (result != null)
                {
                    switch (result.ToString())
                    {
                        case "101":
                            throw new Exception("Client already exists.");
                    }
                    if (result.ToString() == "0")
                    {
                        reREsult = true;
                    }
                }

                return reREsult;
            }

            catch
            {
                throw;
            }
        }

        public bool UpdateClientLanguage(Guid clientId)
        {

            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateClientLanguage");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ClientId", DbType.Guid, clientId);

                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, true);

                var result = base.Save(command, false);

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

        public bool CreateDB(Guid clientId)
        {

            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("CreateDB");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ClientId", DbType.Guid, clientId);

                var result = base.Save(command, false);

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

        public bool DeleteAllClientDetail(Guid clientId)
        {

            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("DeleteAllClientDetail");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@ClientId", DbType.Guid, clientId);

                var result = base.Save(command, false);

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

        public bool CheckDBNameExists(string databaseName)
        {

            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("CheckDBNameExists");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@DatabaseName", DbType.String, databaseName);

                var result = base.FindScalarValue(command);

                if (Convert.ToInt32(result) > 0)
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

        public Boolean AddTables(String script)
        {

            try
            {
                Boolean result = false;
                int result1 = base.Execute(script);
                if (result1 != 0)
                {
                    result = true;
                }
                return result;
            }

            catch
            {
                throw;
            }
        }

    }

    internal class GetAllClientFactory : IDomainObjectFactory<BEMaster.ClientMaster>
    {
        public BEMaster.ClientMaster Construct(IDataReader reader)
        {
            BEMaster.ClientMaster objClientMaster = new BEMaster.ClientMaster();
            objClientMaster.ClientId = HelperMethods.GetGuid(reader, "ClientId");
            objClientMaster.ClientName = HelperMethods.GetString(reader, "ClientName");
            objClientMaster.StartDate = HelperMethods.GetDateTime(reader, "StartDate");
            objClientMaster.EndDate = HelperMethods.GetDateTime(reader, "EndDate");
            objClientMaster.UserLimit = HelperMethods.GetString(reader, "UserLimit");

            return objClientMaster;
        }
    }

    internal class GetClientDetailByIdFactory : IDomainObjectFactory<BEMaster.ClientMaster>
    {
        public BEMaster.ClientMaster Construct(IDataReader reader)
        {
            BEMaster.ClientMaster objClientMaster = new BEMaster.ClientMaster();
            objClientMaster.ClientId = HelperMethods.GetGuid(reader, "ClientId");
            objClientMaster.ClientName = HelperMethods.GetString(reader, "ClientName");
            objClientMaster.ContactPerson = HelperMethods.GetString(reader, "ContactPerson");
            objClientMaster.ContactNo = HelperMethods.GetString(reader, "ContactNo");
            objClientMaster.SubDomain = HelperMethods.GetString(reader, "SubDomain");
            objClientMaster.WebSite = HelperMethods.GetString(reader, "WebSite");
            objClientMaster.ConnectionString = HelperMethods.GetString(reader, "ConnectionString");
            objClientMaster.DatabaseName = HelperMethods.GetString(reader, "DatabaseName");
            objClientMaster.StartDate = HelperMethods.GetDateTime(reader, "StartDate");
            objClientMaster.EndDate = HelperMethods.GetDateTime(reader, "EndDate");
            objClientMaster.SubscriptionId = HelperMethods.GetGuid(reader, "SubscriptionId");
            objClientMaster.Currency = HelperMethods.GetString(reader, "CurrencySymbol");

            return objClientMaster;
        }
    }
}
