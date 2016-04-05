using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using ATS.BusinessEntity;
using System.Data;

namespace ATS.DataAccess
{
    public class DivisionLanguageRepository:Repository<DivisionLanguage>
    {
        public DivisionLanguageRepository(string ConnectionString): base(ConnectionString)
        {

        }

        public Guid AddDivisionlanguage(DivisionLanguage pDivisionLanguage)
        {
            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertDivisionLanguage");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@DivisionId", DbType.Guid, pDivisionLanguage.DivisionId);

                Database.AddInParameter(command, "@LanguageId", DbType.Guid, pDivisionLanguage.LanguageId);
                Database.AddInParameter(command, "@DivisionName", DbType.String, pDivisionLanguage.DivisionName);

                Database.AddInParameter(command, "@Description", DbType.String, pDivisionLanguage.Description);

                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, pDivisionLanguage.IsDelete);

                Database.AddOutParameter(command, "DivisionLanguageId", DbType.Guid, 16); ;

                var result = base.Add(command, "DivisionLanguageId");

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

        public bool Update(DivisionLanguage pDivisionLanguage)
        {

            try
            {
                DbCommand command = Database.GetStoredProcCommand("UpdateDivisionLanguage");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@DivisionId", DbType.Guid, pDivisionLanguage.DivisionId);



                Database.AddInParameter(command, "@LanguageId", DbType.Guid, pDivisionLanguage.LanguageId);

                Database.AddInParameter(command, "@DivisionName", DbType.String, pDivisionLanguage.DivisionName);

                Database.AddInParameter(command, "@Description", DbType.String, pDivisionLanguage.Description); 

                return base.Save(command); ;
            }

            catch
            {
                throw;
            }
        }

        public DivisionLanguage GetDivisionLanguageById(Guid DivisionId , Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetDivisionLanguageById");
                
                Database.AddInParameter(command, "@DivisionId", DbType.Guid, DivisionId);

                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.FindOne(command, new GetDivisionLanguageByIdFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal class GetDivisionLanguageByIdFactory : IDomainObjectFactory<DivisionLanguage>
        {
            public DivisionLanguage Construct(IDataReader reader)
            {
                DivisionLanguage objDivisionLanguage = new DivisionLanguage();
                objDivisionLanguage.DivisionLanguageId = HelperMethods.GetGuid(reader, "DivisionLanguageId");
                objDivisionLanguage.DivisionId = HelperMethods.GetGuid(reader, "DivisionId");
                objDivisionLanguage.DivisionName = HelperMethods.GetString(reader, "DivisionName");
                objDivisionLanguage.Description = HelperMethods.GetString(reader, "Description");
                objDivisionLanguage.LanguageId = HelperMethods.GetGuid(reader, "LanguageId");



                return objDivisionLanguage;
            }
        }
    
    }

    
}
