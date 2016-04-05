using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic
{
    public class PortalContentAction : ActionBase
    {
         #region private data member
        private DAClient.PortalContentRepository _PortalContentRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor

        public PortalContentAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _PortalContentRepository = new DAClient.PortalContentRepository(base.ConnectionString);
            _PortalContentRepository.CurrentUser = base.CurrentUser;
            _PortalContentRepository.CurrentClient = base.CurrentClient;
        }

        public List<BEClient.PortalContent> GetAllPortalContent()
        {
            try
            {
                return _PortalContentRepository.GetAllPortalContent();
            }
            catch
            {
                throw;
            }
        }
        public BEClient.PortalContent GetRecordByRecordId(Guid recordId)
        {
            try
            {
                return _PortalContentRepository.GetRecordByRecordId(recordId);
            }
            catch
            {

                throw;
            }
        }
        public Guid AddPortalContent(BEClient.PortalContent portalContent)
        {
            try
            {
                return _PortalContentRepository.AddPortalContent(portalContent);
            }
            catch
            {
                
                throw;
            }
        }
        public bool UpdatePortalContent(BEClient.PortalContent portalContent)
        {
            try
            {
                return _PortalContentRepository.UpdatePortalContent(portalContent);
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
