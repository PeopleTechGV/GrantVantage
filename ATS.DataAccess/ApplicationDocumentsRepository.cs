using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;


namespace ATS.DataAccess
{
    public class ApplicationDocumentsRepository : Repository<BEClient.ApplicationDocuments>
    {
        public ApplicationDocumentsRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public Guid AddApplicationDocuments(BEClient.ApplicationDocuments objAplicationDocument)
        {
            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertAplicationDocument");

                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, objAplicationDocument.ApplicationId);
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, objAplicationDocument.VacRndId);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, objAplicationDocument.IsDelete);
                Database.AddInParameter(command, "@ATSResumeId", DbType.Guid, objAplicationDocument.ATSResumeId);
                Database.AddInParameter(command, "@RequiredDocumentId", DbType.Guid, objAplicationDocument.RequiredDocumentId);
                Database.AddOutParameter(command, "ApplicationDocumentId", DbType.Guid, 16);
                var result = base.Add(command, "ApplicationDocumentId");
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

        public bool UpdateApplicationDocuments(BEClient.ApplicationDocuments objAplicationDocument)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("UpdateAplicationDocument");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@ApplicationDocumentId", DbType.Guid, objAplicationDocument.ApplicationDocumentId);
                Database.AddInParameter(command, "@ATSResumeId", DbType.Guid, objAplicationDocument.ATSResumeId);
                var result = base.Save(command);
                return HelperMethods.GetBoolean(result);
            }
            catch
            {
                throw;
            }
        }

        public bool DeleteAppDocumentByAppId(Guid ApplicationId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("DeleteAppDocumentByAppId");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                Boolean Result = base.Remove(command);
                return HelperMethods.GetBoolean(Result);
            }
            catch
            {
                throw;
            }
        }


        public List<BEClient.ApplicationDocuments> GetAplicationDocumentsByAppIdVacRndId(Guid ApplicationId, Guid VacRndId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAplicationDocumentsByAppIdVacRndId");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);

                return base.Find(command, new GetApplicationDocumentsByApp_VacRndIdFactory());
            }
            catch
            {
                throw;
            }
        }


        public List<BEClient.ApplicationDocuments> GetScreeningDocumentsByVacancyIdAndUserId(Guid VacancyId, Guid UserId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetScreeningDocumentsByVacancyIdAndUserId");
                Database.AddInParameter(command, "@VacancyId", DbType.Guid, VacancyId);
                Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);
                return base.Find(command, new GetScreeningDocumentsFactory());
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.ApplicationDocuments> GetRequiredDocumentsByApplicationId(Guid ApplicationId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetRequiredDocumentsByApplicationId");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                return base.Find(command, new GetRequiredDocumentsFactory(), false);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.ApplicationDocuments GetApplicationDocumentById(Guid ApplicationDocumentId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetApplicationDocumentById");
                Database.AddInParameter(command, "@ApplicationDocumentId", DbType.Guid, ApplicationDocumentId);
                return base.FindOne(command, new GetRequiredDocumentsFactory(), false);
            }
            catch
            {
                throw;
            }
        }

        internal class GetRequiredDocumentsFactory : IDomainObjectFactory<BEClient.ApplicationDocuments>
        {
            public BEClient.ApplicationDocuments Construct(IDataReader reader)
            {
                BEClient.ApplicationDocuments objAppDocument = new BEClient.ApplicationDocuments();
                objAppDocument.ApplicationDocumentId = HelperMethods.GetGuid(reader, "ApplicationDocumentId");
                objAppDocument.ATSResumeId = HelperMethods.GetGuid(reader, "ATSResumeId");
                objAppDocument.DocumentTypeId = HelperMethods.GetGuid(reader, "DocumentTypeId");
                objAppDocument.RequiredDocumentId = HelperMethods.GetGuid(reader, "RequiredDocumentId");
                objAppDocument.RequiredDocumentName = HelperMethods.GetString(reader, "RequiredDocumentName");
                objAppDocument.VacRndId = HelperMethods.GetGuid(reader, "VacRndId");
                objAppDocument.Path = HelperMethods.GetString(reader, "Path");
                objAppDocument.UploadedFileName = HelperMethods.GetString(reader, "UploadedFileName");
                objAppDocument.IsPending = HelperMethods.GetBoolean(reader, "IsPending");
                objAppDocument.ExtensionTypes = HelperMethods.GetString(reader, "ExtensionTypes");
                objAppDocument.IsOptional = HelperMethods.GetBoolean(reader, "IsOptional");
                return objAppDocument;
            }
        }

        internal class GetScreeningDocumentsFactory : IDomainObjectFactory<BEClient.ApplicationDocuments>
        {
            public BEClient.ApplicationDocuments Construct(IDataReader reader)
            {
                BEClient.ApplicationDocuments objAppDocument = new BEClient.ApplicationDocuments();
                objAppDocument.ATSResumeId = HelperMethods.GetGuid(reader, "ATSResumeId");
                objAppDocument.DocumentTypeId = HelperMethods.GetGuid(reader, "DocumentTypeId");
                return objAppDocument;
            }
        }


        internal class GetApplicationDocumentsByApp_VacRndIdFactory : IDomainObjectFactory<BEClient.ApplicationDocuments>
        {
            public BEClient.ApplicationDocuments Construct(IDataReader reader)
            {
                BEClient.ApplicationDocuments objAppDocument = new BEClient.ApplicationDocuments();
                objAppDocument.ApplicationDocumentId = HelperMethods.GetGuid(reader, "ApplicationDocumentId");
                objAppDocument.ApplicationId = HelperMethods.GetGuid(reader, "ApplicationId");
                objAppDocument.VacRndId = HelperMethods.GetGuid(reader, "VacRndId");
                objAppDocument.ATSResumeId = HelperMethods.GetGuid(reader, "RequiredDocumentId");
                objAppDocument.RequiredDocumentName = HelperMethods.GetString(reader, "RequiredDocumentName");

                return objAppDocument;
            }
        }

    }
}
