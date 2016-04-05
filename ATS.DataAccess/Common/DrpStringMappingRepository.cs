using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using ATS.BusinessEntity;

namespace ATS.DataAccess.Common
{
    public class DrpStringMappingRepository : Repository<DrpStringMapping>
    {
        public DrpStringMappingRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }
        public List<DrpStringMapping> GetAllDropDownValue(Guid languageId,string drpName)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllDropDownValue");
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, languageId);
                Database.AddInParameter(command, "@DrpName", DbType.String, drpName);
                return base.Find(command, new GetAllDropDownValueFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }


    internal class GetAllDropDownValueFactory : IDomainObjectFactory<DrpStringMapping>
    {
        public DrpStringMapping Construct(IDataReader reader)
        {
            DrpStringMapping objDrpStringMapping = new DrpStringMapping();

            objDrpStringMapping.TextField = HelperMethods.GetString(reader, "Text");
            objDrpStringMapping.ValueField = HelperMethods.GetString(reader, "Value");

            return objDrpStringMapping;
        }
    }
}
