using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;

namespace ATS.DataAccess
{
    public class AppInstructionDocsRepository : Repository<BEClient.AppInstructionDoc>
    {
        public AppInstructionDocsRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public Guid InsertAppInstructionDocs(BEClient.AppInstructionDoc objAppInstructionDoc)
        {
            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertAppInstructionDocs");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@VacancyId", DbType.Guid, objAppInstructionDoc.VacancyId);
                Database.AddInParameter(command, "@FileName", DbType.String, objAppInstructionDoc.FileName);
                Database.AddInParameter(command, "@NewFileName", DbType.String, objAppInstructionDoc.NewFileName);
                Database.AddInParameter(command, "@Path", DbType.String, objAppInstructionDoc.Path);
                Database.AddOutParameter(command, "@AppInstructionDocId", DbType.Guid, 16);
                var result = base.Add(command, "AppInstructionDocId");
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

        public bool DeleteAppInstructionDoc(Guid AppInstructionDocId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("DeleteAppInstructionDoc");
                Database.AddInParameter(command, "@AppInstructionDocId", DbType.Guid, AppInstructionDocId);
                var result = base.Save(command, false);
                return HelperMethods.GetBoolean(result);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.AppInstructionDoc> GetAppInstructionDocsByVacancyId(Guid VacancyId)
        {
            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("GetAppInstructionDocsByVacancyId");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@VacancyId", DbType.Guid, VacancyId);
                return base.Find(command, new GetAppInstructionDocsFactory());
            }
            catch
            {
                throw;
            }
        }

        internal class GetAppInstructionDocsFactory : IDomainObjectFactory<BEClient.AppInstructionDoc>
        {
            public BEClient.AppInstructionDoc Construct(IDataReader reader)
            {
                BEClient.AppInstructionDoc objAppInstructionDoc = new BEClient.AppInstructionDoc();
                objAppInstructionDoc.AppInstructionDocId = HelperMethods.GetGuid(reader, "AppInstructionDocId");
                objAppInstructionDoc.FileName = HelperMethods.GetString(reader, "FileName");
                objAppInstructionDoc.Path = HelperMethods.GetString(reader, "FilePath");
                return objAppInstructionDoc;
            }
        }
    }
}
