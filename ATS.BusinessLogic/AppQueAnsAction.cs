using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic
{
    public class AppQueAnsAction : ActionBase
    {
        #region private data member
        private DAClient.AppQueAnsRepository _AppQueAnsRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor
        public AppQueAnsAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _AppQueAnsRepository = new DAClient.AppQueAnsRepository(base.ConnectionString);
            _AppQueAnsRepository.CurrentUser = base.CurrentUser;
            _AppQueAnsRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public List<BEClient.AppQueAns> GetAppQueByApplicationId(Guid ApplicationId, string LstRndTypeId)
        {
            try
            {
                return _AppQueAnsRepository.GetAppQueByApplicationId(ApplicationId, LstRndTypeId);
            }
            catch
            {
                throw;
            }
        }

       

        public BEClient.AppQueAns GetAnswerByAppIdAndQueId(Guid ApplicationId, Guid VacQueId, Guid VacRMId)
        {
            try
            {
                return _AppQueAnsRepository.GetAnswerByAppIdAndQueId(ApplicationId, VacQueId, VacRMId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
