using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic.Common
{
    public class DrpStringMappingAction : ActionBase
    {
        #region private data member
        private DAClient.Common.DrpStringMappingRepository _DrpStringMappingRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor
        public DrpStringMappingAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _DrpStringMappingRepository = new DAClient.Common.DrpStringMappingRepository(base.ConnectionString);
        }

        #endregion

        #region CRUD
        public List<BEClient.DrpStringMapping> GetAllDropDownValue(Guid languageId, string drpName)
        {
            try
            {
                return _DrpStringMappingRepository.GetAllDropDownValue(languageId,drpName);
            }
            catch
            {
                throw;
            }

        }
        #endregion
    }
}
