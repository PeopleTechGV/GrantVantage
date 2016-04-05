using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAClient = ATS.DataAccess;

using BEClient = ATS.BusinessEntity;

namespace ATS.BusinessLogic
{
    public class ConductInterviewDetailsAction:ActionBase
    {
        #region private data member
        private DAClient.ConductInterviewDetailsRepository _ConductInterviewDetailsRepository;
        private Guid _MyClientId { get; set; }
        #endregion

          #region Constructor

        public ConductInterviewDetailsAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _ConductInterviewDetailsRepository = new DAClient.ConductInterviewDetailsRepository(base.ConnectionString);
            _ConductInterviewDetailsRepository.CurrentUser = base.CurrentUser;
            _ConductInterviewDetailsRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public BEClient.ConductInterviewDetails GetConductInterviewDetails(Guid ScheduleIntId)
        {
            try
            {
                return _ConductInterviewDetailsRepository.GetConductInterviewDetails(ScheduleIntId);
            }
            catch
            {
                throw;
            }
        }


    }
}
