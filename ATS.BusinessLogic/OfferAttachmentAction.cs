using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic
{
    public class OfferAttachmentAction : ActionBase
    {
        #region private data member
        private DAClient.OfferAttachmentRepository _OfferAttachmentRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor
        public OfferAttachmentAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _OfferAttachmentRepository = new DAClient.OfferAttachmentRepository(base.ConnectionString);
            _OfferAttachmentRepository.CurrentUser = base.CurrentUser;
            _OfferAttachmentRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public Guid InsertOfferAttachment(BEClient.OfferAttachment objOfferAttachment)
        {
            try
            {
                return _OfferAttachmentRepository.InsertOfferAttachment(objOfferAttachment);
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
                return _OfferAttachmentRepository.GetOfferAttachmentsByOfferTemplateId(OfferTemplateId);
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
                return _OfferAttachmentRepository.DeleteOfferAttachment(OfferAttachmentId);
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
                return _OfferAttachmentRepository.ChangeAttachmentMandatory(OfferAttachmentId, IsMandatory);
            }
            catch
            {
                throw;
            }
        }
    }
}
