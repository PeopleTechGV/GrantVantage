using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;

namespace ATS.DataAccess
{
    public class OfferAttachmentRepository : Repository<BEClient.OfferAttachment>
    {
        public OfferAttachmentRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public Guid InsertOfferAttachment(BEClient.OfferAttachment objOfferAttachment)
        {
            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertOfferAttachment");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@OfferTemplateId", DbType.Guid, objOfferAttachment.OfferTemplateId);
                Database.AddInParameter(command, "@FileName", DbType.String, objOfferAttachment.FileName);
                Database.AddInParameter(command, "@NewFileName", DbType.String, objOfferAttachment.NewFileName);
                Database.AddInParameter(command, "@Path", DbType.String, objOfferAttachment.Path);
                Database.AddInParameter(command, "@IsMandatory", DbType.Boolean, objOfferAttachment.IsMandatory);
                Database.AddOutParameter(command, "OfferAttachmentId", DbType.Guid, 16);
                var result = base.Add(command, "OfferAttachmentId");
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

        public bool DeleteOfferAttachment(Guid OfferAttachmentId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("DeleteOfferAttachment");
                Database.AddInParameter(command, "@OfferAttachmentId", DbType.Guid, OfferAttachmentId);
                var result = base.Save(command, false);
                return HelperMethods.GetBoolean(result);
            }
            catch
            {
                throw;
            }
        }

        public bool ChangeAttachmentMandatory(Guid OfferAttachmentId, bool IsMandatory)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("ChangeAttachmentMandatory");
                Database.AddInParameter(command, "@OfferAttachmentId", DbType.Guid, OfferAttachmentId);
                Database.AddInParameter(command, "@IsMandatory", DbType.Boolean, IsMandatory);
                var result = base.Save(command, false);
                return HelperMethods.GetBoolean(result);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.OfferAttachment> GetOfferAttachmentsByOfferTemplateId(Guid OfferTemplateId)
        {
            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("GetOfferAttachmentsByOfferTemplateId");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@OfferTemplateId", DbType.Guid, OfferTemplateId);
                return base.Find(command, new GetOfferAttachmentsFactory());
            }
            catch
            {
                throw;
            }
        }

        internal class GetOfferAttachmentsFactory : IDomainObjectFactory<BEClient.OfferAttachment>
        {
            public BEClient.OfferAttachment Construct(IDataReader reader)
            {
                BEClient.OfferAttachment objOfferAttachment = new BEClient.OfferAttachment();
                objOfferAttachment.OfferAttachmentId = HelperMethods.GetGuid(reader, "OfferAttachmentId");
                objOfferAttachment.FileName = HelperMethods.GetString(reader, "FileName");
                objOfferAttachment.IsMandatory = HelperMethods.GetBoolean(reader, "IsMandatory");
                objOfferAttachment.Path = HelperMethods.GetString(reader, "FilePath");
                return objOfferAttachment;
            }
        }
    }
}
