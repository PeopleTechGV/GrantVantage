using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using BECommon = ATS.BusinessEntity.Common;
using DAClient = ATS.DataAccess;


namespace ATS.BusinessLogic
{
    public class CSNavigationAction : ActionBase
    {
        #region private data member
        private DAClient.CSNavigationRepository _CSNavigationRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor

        public CSNavigationAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _CSNavigationRepository = new DAClient.CSNavigationRepository(base.ConnectionString);
            _CSNavigationRepository.CurrentUser = base.CurrentUser;
            _CSNavigationRepository.CurrentClient = base.CurrentClient;
        }
        #endregion


        public List<BEClient.CSNavigation> GetCSNavigationCount(string UserDivisionList)
        {
            try
            {

                return _CSNavigationRepository.GetCSNavigationCount(UserDivisionList);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
