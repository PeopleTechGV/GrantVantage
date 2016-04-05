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
   public class ClientLanguageRepository : Repository<BEMaster.ClientLanguage>
    {
       public List<BEMaster.ClientLanguage> GetLanguageByClientId(Guid clientId)
       {
           try
           {
               DbCommand command = Database.GetStoredProcCommand("GetLanguageByClientId");
               Database.AddInParameter(command, "@ClientId", DbType.Guid, clientId);
               return base.Find(command, new GetClientLanguageFactory());
           }
           catch (Exception)
           {
               throw;
           }
       }
    }
   internal class GetClientLanguageFactory : IDomainObjectFactory<BEMaster.ClientLanguage>
   {
       public BEMaster.ClientLanguage Construct(IDataReader reader)
       {
           BEMaster.ClientLanguage objClientLanguage = new BEMaster.ClientLanguage();

           objClientLanguage.LanguageId = HelperMethods.GetGuid(reader, "LanguageId");
           objClientLanguage.IsDefault = HelperMethods.GetBoolean(reader, "IsDefault");
           objClientLanguage.objLanguage = new BEMaster.Language();
           objClientLanguage.objLanguage.LanguageId = HelperMethods.GetGuid(reader, "LanguageId");
           objClientLanguage.objLanguage.LanguageName = HelperMethods.GetString(reader, "LanguageName");
           objClientLanguage.objLanguage.LanguageCulture = HelperMethods.GetString(reader, "LanguageCulture");

           return objClientLanguage;
       }
   }
   internal class GetLanguageByClientIdFactory : IDomainObjectFactory<BEMaster.ClientLanguage>
   {
       public BEMaster.ClientLanguage Construct(IDataReader reader)
       {
           BEMaster.ClientLanguage objClientLanguage = new BEMaster.ClientLanguage();

           objClientLanguage.LanguageId = HelperMethods.GetGuid(reader, "LanguageId");
           objClientLanguage.IsDefault = HelperMethods.GetBoolean(reader, "IsDefault");

           return objClientLanguage;
       }
   }
}
