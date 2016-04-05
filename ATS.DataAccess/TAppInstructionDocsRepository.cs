using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;

namespace ATS.DataAccess
{
    public class TAppInstructionDocsRepository : Repository<BEClient.TAppInstructionDocs>
    {
        public TAppInstructionDocsRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public Guid InsertTAppInstructionDocs(BEClient.TAppInstructionDocs objTAppInstructionDoc)
        {
            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertTAppInstructionDocs");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@TVacId", DbType.Guid, objTAppInstructionDoc.TVacId);
                Database.AddInParameter(command, "@FileName", DbType.String, objTAppInstructionDoc.FileName);
                Database.AddInParameter(command, "@NewFileName", DbType.String, objTAppInstructionDoc.NewFileName);
                Database.AddInParameter(command, "@Path", DbType.String, objTAppInstructionDoc.Path);
                Database.AddOutParameter(command, "@TAppInstructionDocId", DbType.Guid, 16);
                var result = base.Add(command, "TAppInstructionDocId");
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

        public bool DeleteTAppInstructionDoc(Guid TAppInstructionDocId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("DeleteTAppInstructionDoc");
                Database.AddInParameter(command, "@TAppInstructionDocId", DbType.Guid, TAppInstructionDocId);
                var result = base.Save(command, false);
                return HelperMethods.GetBoolean(result);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.TAppInstructionDocs> GetTAppInstructionDocsByTVacId(Guid TVacId)
        {
            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("GetTAppInstructionDocsByTVacId");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@TVacId", DbType.Guid, TVacId);
                return base.Find(command, new GetTAppInstructionDocsFactory());
            }
            catch
            {
                throw;
            }
        }

        internal class GetTAppInstructionDocsFactory : IDomainObjectFactory<BEClient.TAppInstructionDocs>
        {
            public BEClient.TAppInstructionDocs Construct(IDataReader reader)
            {
                BEClient.TAppInstructionDocs objTAppInstructionDoc = new BEClient.TAppInstructionDocs();
                objTAppInstructionDoc.TAppInstructionDocId = HelperMethods.GetGuid(reader, "TAppInstructionDocId");
                objTAppInstructionDoc.FileName = HelperMethods.GetString(reader, "FileName");
                objTAppInstructionDoc.NewFileName = HelperMethods.GetString(reader, "NewFileName");
                objTAppInstructionDoc.Path = HelperMethods.GetString(reader, "FilePath");
                return objTAppInstructionDoc;
            }
        }
    }
}