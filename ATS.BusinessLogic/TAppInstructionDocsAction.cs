using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAClient = ATS.DataAccess;
using BEClient = ATS.BusinessEntity;

namespace ATS.BusinessLogic
{
    public class TAppInstructionDocsAction : ActionBase
    {
        #region private data member
        private DAClient.TAppInstructionDocsRepository _TAppInstructionDocsRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor
        public TAppInstructionDocsAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _TAppInstructionDocsRepository = new DAClient.TAppInstructionDocsRepository(base.ConnectionString);
            _TAppInstructionDocsRepository.CurrentUser = base.CurrentUser;
            _TAppInstructionDocsRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public Guid InsertTAppInstructionDocs(BEClient.TAppInstructionDocs objTAppInstructionDoc)
        {
            try
            {
                return _TAppInstructionDocsRepository.InsertTAppInstructionDocs(objTAppInstructionDoc);
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
                return _TAppInstructionDocsRepository.DeleteTAppInstructionDoc(TAppInstructionDocId);
            }
            catch
            {
                return false;
            }
        }

        public List<BEClient.TAppInstructionDocs> GetTAppInstructionDocsByTVacId(Guid TVacId)
        {
            try
            {
                return _TAppInstructionDocsRepository.GetTAppInstructionDocsByTVacId(TVacId);
            }
            catch
            {
                return null;
            }
        }
    }
}
