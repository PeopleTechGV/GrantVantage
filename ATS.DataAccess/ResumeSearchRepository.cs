using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Data.Common;
using System.Data;

namespace ATS.DataAccess
{
    public class ResumeSearchRepository : Repository<BEClient.ResumeSearch>
    {
        public ResumeSearchRepository(string ConnectionString)
            : base(ConnectionString)
        {
            
        }
       public List<BEClient.ResumeSearch> GetAllSearchResumeModuleAndFields(Guid languageId)
       {
           try
           {
               DbCommand command = Database.GetStoredProcCommand("GetAllSearchResumeModuleAndFields");

               Database.AddInParameter(command, "@LanguageId", DbType.Guid, languageId);

               var result = base.Find(command, new GetAllSearchResumeModuleAndFieldsFactory(), false);

               return result;
           }
           catch
           {
               throw;
           }

       }

       internal class GetAllSearchResumeModuleAndFieldsFactory : IDomainObjectFactory<BEClient.ResumeSearch>
       {
           public BEClient.ResumeSearch Construct(IDataReader reader)
           {
               BEClient.ResumeSearch objResumeSearch = new BEClient.ResumeSearch();

               objResumeSearch.FieldKey = HelperMethods.GetString(reader, "FieldKey");
               objResumeSearch.FieldName = HelperMethods.GetString(reader, "FieldName");
               objResumeSearch.ModuleKey = HelperMethods.GetString(reader, "ModuleKey");
               objResumeSearch.ModuleName = HelperMethods.GetString(reader, "ModuleName");
               objResumeSearch.Icon = HelperMethods.GetString(reader, "Icon");
               objResumeSearch.Type = HelperMethods.GetString(reader, "Type");

               return objResumeSearch;
           }
       }
    }
}
