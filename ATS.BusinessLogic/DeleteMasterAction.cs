using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAClient = ATS.DataAccess;
using BEClient = ATS.BusinessEntity;

namespace ATS.BusinessLogic
{
    public class DeleteMasterAction : ActionBase
    {
        #region private data member
        private DAClient.DeleteMasterRepository _DeleteMasterRepository;
        private Guid _MyClientId { get; set; }
        #endregion

         #region Constructor

        public DeleteMasterAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _DeleteMasterRepository = new DAClient.DeleteMasterRepository(base.ConnectionString);
            _DeleteMasterRepository.CurrentUser = base.CurrentUser;
            _DeleteMasterRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public bool DeleteMasters(BEClient.DeleteMaster deletemaster)
        {
            try
            {   
                return _DeleteMasterRepository.Delete(deletemaster);
            }
            catch
            {
                throw;
            }
        }
    }
}
