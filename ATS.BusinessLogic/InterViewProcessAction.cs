using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAClient = ATS.DataAccess;
using BEClient = ATS.BusinessEntity;
namespace ATS.BusinessLogic
{
    public class InterViewProcessAction:ActionBase
    {
        
        #region private data member
        private DAClient.InterViewProcessRepository _InterViewProcessRepository;
        private Guid _MyClientId { get; set; }
        #endregion


        #region Constructor

        public InterViewProcessAction(Guid ClientId, bool CreateSRPObject = false)
            : base(ClientId)
        {
            
            _MyClientId = ClientId;
            _InterViewProcessRepository = new DAClient.InterViewProcessRepository(base.ConnectionString);
            _InterViewProcessRepository.CurrentUser = base.CurrentUser;
            _InterViewProcessRepository.CurrentClient = base.CurrentClient;
        }
        #endregion


        public List<BEClient.InterViewProcess> GetQueCatDetailsBtyOrder(Guid VacRndId)
        {
            try
            {
                return _InterViewProcessRepository.GetQueCatDetailsBtyOrder(VacRndId);
            }
            catch
            {
                throw;
            }
        }
        public BEClient.InterViewProcess GetQueCatDetailsBtyOrder(Guid VacRndId, int Row,Guid? ScheduleIntId )
        {
            try
            {
                return _InterViewProcessRepository.GetQueCatDetailsBtyOrder(VacRndId, Row, ScheduleIntId);
            }
            catch
            {
                throw;
            }
        }
    }
}
