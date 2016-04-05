using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;

namespace ATS.DataAccess
{
    public class DocumentTypeRepository : Repository<BEClient.DocumentType>
    {
        public DocumentTypeRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public Guid InsertDocumentType(BEClient.DocumentType objDocumentType)
        {
            try
            {
                Guid reResult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertDocumentType");
                Database.AddInParameter(command, "@DocumentName", DbType.String, objDocumentType.DocumentName);
                Database.AddInParameter(command, "@Description", DbType.String, objDocumentType.Description);
                Database.AddInParameter(command, "@ExtensionTypes", DbType.String, objDocumentType.ExtensionTypes);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, false);
                Database.AddInParameter(command, "@CanDelete", DbType.Boolean, true);
                Database.AddOutParameter(command, "@DocumentTypeId", DbType.Guid, 16);
                var result = base.Add(command, "DocumentTypeId");
                if (result != null)
                {
                    Guid.TryParse(result.ToString(), out reResult);
                }
                return reResult;
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateDocumentType(BEClient.DocumentType objDocumentType)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("UpdateDocumentType");
                Database.AddInParameter(command, "@DocumentTypeId", DbType.Guid, objDocumentType.DocumentTypeId);
                Database.AddInParameter(command, "@DocumentName", DbType.String, objDocumentType.DocumentName);
                Database.AddInParameter(command, "@Description", DbType.String, objDocumentType.Description);
                Database.AddInParameter(command, "@ExtensionTypes", DbType.String, objDocumentType.ExtensionTypes);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, false);
                var Result = base.Save(command);
                return HelperMethods.GetBoolean(Result);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.DocumentType> GetAllDocmentType(Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllDocumentType");
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);

                return base.Find(command, new GetAllDocmentTypeFactory());
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.DocumentType> FillDocumentType(Guid LanguageId, Guid? VacRndId = null)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("FillDocumentType");
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.Find(command, new FillDocumentTypeFactory(), false);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.DocumentType> GetRequiredDocumentForScreening(Guid VacancyId)
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

        public List<BEClient.DocumentType> FillTDocumentType(Guid LanguageId, Guid? TVacRndId = null)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("FillTDocumentType");
                Database.AddInParameter(command, "@TVacRndId", DbType.Guid, TVacRndId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.Find(command, new FillDocumentTypeFactory(), false);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.DocumentType GetDocmentTypeById(Guid DocumentTypeId, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetDocumentTypeById");
                Database.AddInParameter(command, "@DocumentTypeId", DbType.Guid, DocumentTypeId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);

                return base.FindOne(command, new GetDocmentTypeByIdFactory());
            }
            catch
            {
                throw;
            }
        }

        public BEClient.DocumentType GetDocTypeByDocCategory(Int32 DocCategory)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetDocTypeByDocCategory");
                Database.AddInParameter(command, "@DocCategory", DbType.Int32, DocCategory);
                return base.FindOne(command, new GetDocTypeByDocCategoryFactory());
            }
            catch
            {
                throw;
            }
        }

        public bool Delete(string ListDocumentTypeId)
        {
            bool LocalTrasaction = false;
            try
            {
                if (base.Transaction == null)
                {
                    base.BeginTransaction();
                    LocalTrasaction = true;
                }
                Boolean reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("DeleteDocumentType");
                Database.AddInParameter(command, "@DocumentTypeId", DbType.String, ListDocumentTypeId);
                Database.AddOutParameter(command, "@IsError", DbType.Int32, 4);
                var result = base.SaveWithoutDuplication(command, "IsError", false);




                if (result != null)
                {
                    Int32 errorCode = Convert.ToInt32(result);
                    if (errorCode == 101)
                    {
                        reREsult = false;

                        throw new Exception("Default document type can't be delete");
                    }
                    else
                    {
                        reREsult = true;
                    }

                }
                if (LocalTrasaction)
                    this.CommitTransaction();

                return reREsult;
            }
            catch
            {
                if (LocalTrasaction)
                    this.RollbackTransaction();
                throw;
            }
        }

        public string GetDocExtensionByResumeId(Guid ATSResumeId)
        {
            DbCommand command = Database.GetStoredProcCommand("GetDocExtensionByResumeId");
            Database.AddInParameter(command, "@ATSResumeId", DbType.Guid, ATSResumeId);
            return base.FindScalarValue(command);
        }

        internal class FillDocumentTypeFactory : IDomainObjectFactory<BEClient.DocumentType>
        {
            public BEClient.DocumentType Construct(IDataReader reader)
            {
                BEClient.DocumentType documenttype = new BEClient.DocumentType();
                documenttype.DocumentTypeId = HelperMethods.GetGuid(reader, "DocumentTypeId");
                documenttype.DocumentName = HelperMethods.GetString(reader, "DocumentName");
                return documenttype;
            }
        }

        internal class GetAllDocmentTypeFactory : IDomainObjectFactory<BEClient.DocumentType>
        {
            public BEClient.DocumentType Construct(IDataReader reader)
            {
                BEClient.DocumentType documenttype = new BEClient.DocumentType();
                documenttype.DocumentTypeId = HelperMethods.GetGuid(reader, "DocumentTypeId");
                documenttype.DocumentName = HelperMethods.GetString(reader, "DocumentName");
                documenttype.Description = HelperMethods.GetString(reader, "Description");
                documenttype.ExtensionTypes = HelperMethods.GetString(reader, "ExtensionTypes");
                documenttype.IsDelete = HelperMethods.GetBoolean(reader, "IsDelete");
                documenttype.CanDelete = HelperMethods.GetBoolean(reader, "CanDelete");
                documenttype.LocalName = HelperMethods.GetString(reader, "LocalName");
                documenttype.DocCategory = HelperMethods.GetInt32(reader, "DocCategory");
                return documenttype;
            }
        }

        internal class GetDocmentTypeByIdFactory : IDomainObjectFactory<BEClient.DocumentType>
        {
            public BEClient.DocumentType Construct(IDataReader reader)
            {
                BEClient.DocumentType documenttype = new BEClient.DocumentType();
                documenttype.DocumentTypeId = HelperMethods.GetGuid(reader, "DocumentTypeId");
                documenttype.DocumentName = HelperMethods.GetString(reader, "DocumentName");
                documenttype.Description = HelperMethods.GetString(reader, "Description");
                documenttype.ExtensionTypes = HelperMethods.GetString(reader, "ExtensionTypes");
                documenttype.IsDelete = HelperMethods.GetBoolean(reader, "IsDelete");
                documenttype.CanDelete = HelperMethods.GetBoolean(reader, "CanDelete");
                documenttype.LocalName = HelperMethods.GetString(reader, "LocalName");
                documenttype.DocCategory = HelperMethods.GetInt32(reader, "DocCategory");
                return documenttype;
            }
        }

        internal class GetDocTypeByDocCategoryFactory : IDomainObjectFactory<BEClient.DocumentType>
        {
            public BEClient.DocumentType Construct(IDataReader reader)
            {
                BEClient.DocumentType documenttype = new BEClient.DocumentType();
                documenttype.DocumentTypeId = HelperMethods.GetGuid(reader, "DocumentTypeId");
                documenttype.ExtensionTypes = HelperMethods.GetString(reader, "ExtensionTypes");
                documenttype.DocCategory = HelperMethods.GetInt32(reader, "DocCategory");

                return documenttype;
            }
        }
    }
}
