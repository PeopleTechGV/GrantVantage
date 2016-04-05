using System;
using System.Collections.Generic;
using System.Linq;
using ATS.BusinessEntity;
using System.Text;
using System.Data.Common;
using System.Data;

namespace ATS.DataAccess
{
    public class TRequiredDocumentRepository : Repository<TRequiredDocument>
    {
        public TRequiredDocumentRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public Guid InsertTRequiredDocument(TRequiredDocument objTRequiredDocument)
        {
            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertTRequiredDocument");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@TVacRndId", DbType.Guid, objTRequiredDocument.TVacRndId);
                Database.AddInParameter(command, "@DocumentTypeId", DbType.Guid, objTRequiredDocument.DocumentTypeId);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, false);
                Database.AddOutParameter(command, "@TRequiredDocumentId", DbType.Guid, 16);
                var result = base.Add(command, "TRequiredDocumentId", true);
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

        public bool UpdateTRequiredDocument(TRequiredDocument objTRequiredDocument)
        {
            try
            {

                DbCommand command = Database.GetStoredProcCommand("UpdateTRequiredDocument");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@TRequiredDocumentId", DbType.Guid, objTRequiredDocument.TRequiredDocumentId);
                Database.AddInParameter(command, "@TVacRndId", DbType.Guid, objTRequiredDocument.TVacRndId);
                Database.AddInParameter(command, "@DocumentTypeId", DbType.Guid, objTRequiredDocument.DocumentTypeId);
                var result = base.Save(command);
                return HelperMethods.GetBoolean(result);
            }
            catch
            {
                throw;
            }
        }

        public bool DeleteTRequiredDocument(Guid TRequiredDocumentId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("DeleteTRequiredDocument");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@TRequiredDocumentId", DbType.Guid, TRequiredDocumentId);
                var result = base.Save(command);
                return HelperMethods.GetBoolean(result);
            }
            catch
            {
                throw;
            }
        }

        public List<TRequiredDocument> GetTRequiredDocumentByTVacId(Guid TVacId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetTRequiredDocumentByTVacId");
                Database.AddInParameter(command, "@TVacId", DbType.Guid, TVacId);
                return base.Find(command, new GetAllRequiredDocumentFactory(), false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<TRequiredDocument> GetTRequiredDocumentByTVacRndId(Guid TVacRndId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetTRequiredDocumentByTVacRndId");
                Database.AddInParameter(command, "@TVacRndId", DbType.Guid, TVacRndId);
                return base.Find(command, new GetAllRequiredDocumentFactory(), false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TRequiredDocument GetTRequiredDocumentById(Guid TRequiredDocumentId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetTRequiredDocumentById");
                Database.AddInParameter(command, "@TRequiredDocumentId", DbType.Guid, TRequiredDocumentId);
                return base.FindOne(command, new GetAllRequiredDocumentFactory(), false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal class GetAllRequiredDocumentFactory : IDomainObjectFactory<TRequiredDocument>
        {
            public TRequiredDocument Construct(IDataReader reader)
            {
                TRequiredDocument objTRequiredDocument = new TRequiredDocument();

                objTRequiredDocument.TRequiredDocumentId = HelperMethods.GetGuid(reader, "TRequiredDocumentId");
                objTRequiredDocument.TVacRndId = HelperMethods.GetGuid(reader, "TVacRndId");
                objTRequiredDocument.DocumentTypeId = HelperMethods.GetGuid(reader, "DocumentTypeId");
                objTRequiredDocument.objDocumentType = new DocumentType();
                objTRequiredDocument.objDocumentType.DocumentName = HelperMethods.GetString(reader, "DocumentName");
                objTRequiredDocument.objDocumentType.Description = HelperMethods.GetString(reader, "Description");
                objTRequiredDocument.objDocumentType.ExtensionTypes = HelperMethods.GetString(reader, "ExtensionTypes");
                return objTRequiredDocument;
            }
        }
    }
}
