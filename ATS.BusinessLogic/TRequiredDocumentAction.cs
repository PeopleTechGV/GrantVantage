using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic
{
    public class TRequiredDocumentAction : ActionBase
    {
        #region private data member
        private DAClient.TRequiredDocumentRepository _TRequiredDocumentRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor
        public TRequiredDocumentAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _TRequiredDocumentRepository = new DAClient.TRequiredDocumentRepository(base.ConnectionString);
            _TRequiredDocumentRepository.CurrentUser = base.CurrentUser;
            _TRequiredDocumentRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public Guid InsertTRequiredDocument(BEClient.TRequiredDocument objTRequiredDocument)
        {
            try
            {
                return _TRequiredDocumentRepository.InsertTRequiredDocument(objTRequiredDocument);
            }

            catch
            {
                throw;
            }
        }

        public bool UpdateTRequiredDocument(BEClient.TRequiredDocument objTRequiredDocument)
        {
            try
            {
                return _TRequiredDocumentRepository.UpdateTRequiredDocument(objTRequiredDocument);
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
                return _TRequiredDocumentRepository.DeleteTRequiredDocument(TRequiredDocumentId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.TRequiredDocument> GetTRequiredDocumentByVacRndId(Guid TVacRndId)
        {
            try
            {
                return _TRequiredDocumentRepository.GetTRequiredDocumentByTVacRndId(TVacRndId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public BEClient.TRequiredDocument GetTRequiredDocumentById(Guid TRequiredDocumentId)
        {
            try
            {
                return _TRequiredDocumentRepository.GetTRequiredDocumentById(TRequiredDocumentId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}