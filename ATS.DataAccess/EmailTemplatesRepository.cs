using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Data.Common;
using System.Data;

namespace ATS.DataAccess
{
    public class EmailTemplatesRepository : Repository<BEClient.EmailTemplates>
    {
        public EmailTemplatesRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public bool UpdateEmailTemplate(BEClient.EmailTemplates objEmailTemplate)
        {
            bool LocalTrasaction = false;
            try
            {
                if (base.Transaction == null)
                {
                    base.BeginTransaction();
                    LocalTrasaction = true;
                }
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateEmailTemplate");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@EmailTemplateId", DbType.Guid, objEmailTemplate.EmailTemplateId);
                Database.AddInParameter(command, "@Subject", DbType.String, objEmailTemplate.Subject);
                Database.AddInParameter(command, "@EmailDescription", DbType.String, objEmailTemplate.EmailDescription);
                Database.AddInParameter(command, "@ClientId", DbType.Guid, objEmailTemplate.ClientId);
                Database.AddInParameter(command, "@PdfHeader", DbType.String, objEmailTemplate.PdfHeader);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, objEmailTemplate.LanguageId);
                var result = base.Save(command, true);

                if (result)
                {
                    bool.TryParse(result.ToString(), out reREsult);
                }
                if (LocalTrasaction)
                    base.CommitTransaction();

                return reREsult;
            }

            catch
            {
                if (LocalTrasaction)
                    this.RollbackTransaction();
                throw;
            }
        }

        public List<BEClient.EmailTemplates> GetAllEmailTemplates(Guid LanguageId, Guid ClientId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllEmailTemplates");

                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                Database.AddInParameter(command, "@ClientId", DbType.Guid, ClientId);

                return base.Find(command, new GetEmailTemplateFactory(), false);

            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.EmailTemplates> FillDropDownEmailTemplates(Guid LanguageId, Guid ClientId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllEmailTemplates");
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                Database.AddInParameter(command, "@ClientId", DbType.Guid, ClientId);
                return base.Find(command, new GetEmailTemplateForDropDownFactory(), false);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.EmailTemplates> FillEmailTemplatesByCategory(Int32 EmailCategory, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetEmailTemplatesByCategory");
                Database.AddInParameter(command, "@EmailCategory", DbType.Int32, EmailCategory);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.Find(command, new FillEmailTemplatesByCategoryFactory(), false);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.EmailTemplates GetEmailTemplateByEmailIdentifier(Guid LanguageId, string EmailIdentifier)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetEmailTemplateByIdentifier");
                Database.AddInParameter(command, "@EmailIdentifier", DbType.String, EmailIdentifier);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.FindOne(command, new GetEmailTemplateFactory(), false);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.EmailTemplates GetEmailTemplateById(Guid LanguageId, Guid EmailTemplateId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetEmailTemplateById");
                Database.AddInParameter(command, "@EmailTemplateId", DbType.Guid, EmailTemplateId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.FindOne(command, new GetEmailTemplateFactory(), false);
            }
            catch
            {
                throw;
            }
        }

        public string GetEmailDescriptionById(Guid EmailTemplateId, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetEmailDescriptionById");
                Database.AddInParameter(command, "@EmailTemplateId", DbType.Guid, EmailTemplateId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.FindScalarValue(command);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.EmailTemplates GetEmailContentById(Guid EmailTemplateId, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetEmailContentById");
                Database.AddInParameter(command, "@EmailTemplateId", DbType.Guid, EmailTemplateId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.FindOne(command, new GetEmailContentFactory(), false);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.EmailTemplates> FillEmailTemplatesByIds(string EmailTemplateList, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("FillEmailTemplatesByIds");
                Database.AddInParameter(command, "@EmailTemplateList", DbType.String, EmailTemplateList);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.Find(command, new FillEmailTemplatesFactory(), false);
            }
            catch
            {
                throw;
            }
        }

        internal class GetEmailContentFactory : IDomainObjectFactory<BEClient.EmailTemplates>
        {
            public BEClient.EmailTemplates Construct(IDataReader reader)
            {
                BEClient.EmailTemplates objLanguageBlock = new BEClient.EmailTemplates();
                objLanguageBlock.Subject = HelperMethods.GetString(reader, "Subject");
                objLanguageBlock.EmailDescription = HelperMethods.GetString(reader, "EmailDescription");
                return objLanguageBlock;
            }
        }

        internal class GetEmailTemplateFactory : IDomainObjectFactory<BEClient.EmailTemplates>
        {
            public BEClient.EmailTemplates Construct(IDataReader reader)
            {
                BEClient.EmailTemplates objLanguageBlock = new BEClient.EmailTemplates();
                objLanguageBlock.EmailTemplateId = HelperMethods.GetGuid(reader, "EmailTemplateId");
                objLanguageBlock.EmailIdentifier = HelperMethods.GetString(reader, "EmailIdentifier");
                objLanguageBlock.EmailName = HelperMethods.GetString(reader, "EmailName");
                objLanguageBlock.Subject = HelperMethods.GetString(reader, "Subject");
                objLanguageBlock.EmailDescription = HelperMethods.GetString(reader, "EmailDescription");
                objLanguageBlock.PdfHeader = HelperMethods.GetString(reader, "PdfHeader");
                objLanguageBlock.EmailCategory = HelperMethods.GetString(reader, "EmailCategory");
                objLanguageBlock.CategoryNo = HelperMethods.GetInt32(reader, "CategoryNo");
                objLanguageBlock.Description = HelperMethods.GetString(reader, "Description");

                return objLanguageBlock;
            }
        }

        internal class GetEmailTemplateForDropDownFactory : IDomainObjectFactory<BEClient.EmailTemplates>
        {
            public BEClient.EmailTemplates Construct(IDataReader reader)
            {
                BEClient.EmailTemplates objLanguageBlock = new BEClient.EmailTemplates();
                objLanguageBlock.EmailName = HelperMethods.GetString(reader, "EmailName");
                objLanguageBlock.EmailIdentifier = HelperMethods.GetString(reader, "EmailIdentifier");
                return objLanguageBlock;
            }
        }

        internal class FillEmailTemplatesByCategoryFactory : IDomainObjectFactory<BEClient.EmailTemplates>
        {
            public BEClient.EmailTemplates Construct(IDataReader reader)
            {
                BEClient.EmailTemplates objLanguageBlock = new BEClient.EmailTemplates();
                objLanguageBlock.EmailTemplateId = HelperMethods.GetGuid(reader, "EmailTemplateId");
                objLanguageBlock.Subject = HelperMethods.GetString(reader, "Subject");
                objLanguageBlock.EmailName = HelperMethods.GetString(reader, "EmailName");
                return objLanguageBlock;
            }
        }

        internal class FillEmailTemplatesFactory : IDomainObjectFactory<BEClient.EmailTemplates>
        {
            public BEClient.EmailTemplates Construct(IDataReader reader)
            {
                BEClient.EmailTemplates objLanguageBlock = new BEClient.EmailTemplates();
                objLanguageBlock.EmailTemplateId = HelperMethods.GetGuid(reader, "EmailTemplateId");
                objLanguageBlock.EmailName = HelperMethods.GetString(reader, "EmailName");
                return objLanguageBlock;
            }
        }
    }
}