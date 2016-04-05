using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEMaster = ATS.BusinessEntity.Master;
using System.Data.Common;
using System.Data;
namespace ATS.DataAccess.Master
{
    public class ClientRepository : Repository<BEMaster.Client>
    {
        public BEMaster.Client GetRecordById(Guid recordId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetClientById");
                Database.AddInParameter(command, "@ClientId", DbType.Guid, recordId);
                BEMaster.Client validClient = base.FindOne(command, new GetAllClientData());
                if (validClient != null)
                {
                    return validClient;
                }
                return null;
            }
            catch
            {
                throw;
            }
        }
        public BEMaster.Client  GetClientByName(String clientName)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetClientByName");
                Database.AddInParameter(command, "@ClientName", DbType.String, clientName);
                BEMaster.Client validClient = base.FindOne(command, new GetAllClientData());
                if (validClient != null)
                {
                    return validClient;
                }
                return null;
            }
            catch
            {
                throw;
            }
        }
    }
    internal class GetAllClientData : IDomainObjectFactory<BEMaster.Client>
    {
        BEMaster.Client IDomainObjectFactory<BEMaster.Client>.Construct(IDataReader reader)
        {
            try
            {
                BEMaster.Client objClient = new BEMaster.Client();
                objClient.ClientId = HelperMethods.GetGuid(reader, "ClientId");
                objClient.Clientname = HelperMethods.GetString(reader, "ClientName");
                objClient.ConnectionString = HelperMethods.GetString(reader, "ConnectionString");
                objClient.CreatedBy = HelperMethods.GetGuid(reader, "CreatedBy");
                objClient.CurrentLanguageId = HelperMethods.GetGuid(reader, "LanguageId");
                objClient.CurrencySymbol = HelperMethods.GetString(reader, "CurrencySymbol");
                objClient.WebSite = HelperMethods.GetString(reader, "WebSite");
            

                return objClient;
            }
            catch
            {
                throw;
            }

        }
    }

}
