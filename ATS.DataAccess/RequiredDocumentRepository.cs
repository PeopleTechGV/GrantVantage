using System;
using System.Collections.Generic;
using System.Linq;
using ATS.BusinessEntity;
using System.Text;
using System.Data.Common;
using System.Data;

namespace ATS.DataAccess
{
    public class RequiredDocumentRepository : Repository<RequiredDocument>
    {
        public RequiredDocumentRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public Guid InsertRequiredDocument(RequiredDocument objRequiredDocument)
        {
            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertRequiredDocument");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, objRequiredDocument.VacRndId);
                Database.AddInParameter(command, "@DocumentTypeId", DbType.Guid, objRequiredDocument.DocumentTypeId);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, false);
                Database.AddOutParameter(command, "@RequiredDocumentId", DbType.Guid, 16);
                var result = base.Add(command, "RequiredDocumentId", true);
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

        public Guid InsertOptionalResume(Guid VacancyId)
        {
            try
            {
                Guid ReResult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertOptionalResume");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@VacancyId", DbType.Guid, VacancyId);
                Database.AddOutParameter(command, "@RequiredDocumentId", DbType.Guid, 16);
                var result = base.Add(command, "RequiredDocumentId", true);
                if (result != null)
                {
                    Guid.TryParse(result.ToString(), out ReResult);
                }
                return ReResult;
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateRequiredDocument(RequiredDocument objRequiredDocument)
        {
            try
            {

                DbCommand command = Database.GetStoredProcCommand("UpdateRequiredDocument");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@RequiredDocumentId", DbType.Guid, objRequiredDocument.RequiredDocumentId);
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, objRequiredDocument.VacRndId);
                Database.AddInParameter(command, "@DocumentTypeId", DbType.Guid, objRequiredDocument.DocumentTypeId);
                var result = base.Save(command);
                return HelperMethods.GetBoolean(result);
            }
            catch
            {
                throw;
            }
        }

        public bool DeleteRequiredDocument(Guid RequiredDocumentId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("DeleteRequiredDocument");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@RequiredDocumentId", DbType.Guid, RequiredDocumentId);
                var result = base.Save(command);
                return HelperMethods.GetBoolean(result);
            }
            catch
            {
                throw;
            }
        }

        public List<RequiredDocument> GetRequiredDocumentByVacRndId(Guid VacRndId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetRequiredDocumentByVacRndId");
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                return base.Find(command, new GetAllRequiredDocumentFactory(), false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public RequiredDocument GetRequiredDocumentById(Guid RequiredDocumentId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetRequiredDocumentById");
                Database.AddInParameter(command, "@RequiredDocumentId", DbType.Guid, RequiredDocumentId);
                return base.FindOne(command, new GetAllRequiredDocumentFactory(), false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<RequiredDocument> GetRequiredDocumentForScreening(Guid VacancyId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetRequiredDocumentForScreening");
                Database.AddInParameter(command, "@VacancyId", DbType.Guid, VacancyId);
                return base.Find(command, new FillDocumentTypeFactory(), false);
            }
            catch
            {
                throw;
            }
        }

        public RequiredDocument GetScreeningDocumentById(Guid RequiredDocumentId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetScreeningDocumentById");
                Database.AddInParameter(command, "@RequiredDocumentId", DbType.Guid, RequiredDocumentId);
                return base.FindOne(command, new FillDocumentTypeFactory(), false);
            }
            catch
            {
                throw;
            }
        }

        public int CheckForRequiredDocuments(Guid ApplicationId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("CheckForRequiredDocuments");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                return HelperMethods.GetInt32(base.FindScalarValue(command));
            }
            catch (Exception)
            {
                throw;
            }
        }


        internal class FillDocumentTypeFactory : IDomainObjectFactory<RequiredDocument>
        {
            public RequiredDocument Construct(IDataReader reader)
            {
                RequiredDocument objRequiredDoc = new RequiredDocument();
                objRequiredDoc.RequiredDocumentId = HelperMethods.GetGuid(reader, "RequiredDocumentId");
                objRequiredDoc.DocumentTypeId = HelperMethods.GetGuid(reader, "DocumentTypeId");
                objRequiredDoc.DocumentTypeName = HelperMethods.GetString(reader, "DocumentName");
                objRequiredDoc.VacRndId = HelperMethods.GetGuid(reader, "VacRndId");
                objRequiredDoc.objDocumentType = new DocumentType();
                objRequiredDoc.objDocumentType.ExtensionTypes = HelperMethods.GetString(reader, "ExtensionTypes");
                objRequiredDoc.objDocumentType.Description = HelperMethods.GetString(reader, "Description");
                objRequiredDoc.IsResume = HelperMethods.GetBoolean(reader, "IsResume");
                objRequiredDoc.IsOptional = HelperMethods.GetBoolean(reader, "IsOptional");
                return objRequiredDoc;
            }
        }

        internal class GetAllRequiredDocumentFactory : IDomainObjectFactory<RequiredDocument>
        {
            public RequiredDocument Construct(IDataReader reader)
            {
                RequiredDocument objRequiredDocument = new RequiredDocument();

                objRequiredDocument.RequiredDocumentId = HelperMethods.GetGuid(reader, "RequiredDocumentId");
                objRequiredDocument.VacRndId = HelperMethods.GetGuid(reader, "VacRndId");
                objRequiredDocument.DocumentTypeId = HelperMethods.GetGuid(reader, "DocumentTypeId");
                objRequiredDocument.DivisionId = HelperMethods.GetGuid(reader, "DivisionId");

                objRequiredDocument.objDocumentType = new DocumentType();

                objRequiredDocument.objDocumentType.DocumentName = HelperMethods.GetString(reader, "DocumentName");
                objRequiredDocument.objDocumentType.Description = HelperMethods.GetString(reader, "Description");
                objRequiredDocument.objDocumentType.ExtensionTypes = HelperMethods.GetString(reader, "ExtensionTypes");
                return objRequiredDocument;
            }
        }
    }
}
