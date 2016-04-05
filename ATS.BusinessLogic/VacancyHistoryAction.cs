using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAClient = ATS.DataAccess;
using BEClient = ATS.BusinessEntity;

namespace ATS.BusinessLogic
{
    public class VacancyHistoryAction : ActionBase
    {
        #region private data member
        private DAClient.VacancyHistoryRepository _VacancyHistoryRespository;
        private Guid _MyClientId { get; set; }
        #endregion


        #region Constructor

        public VacancyHistoryAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _VacancyHistoryRespository = new DAClient.VacancyHistoryRepository(base.ConnectionString);
            _VacancyHistoryRespository.CurrentUser = base.CurrentUser;
            _VacancyHistoryRespository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public List<BEClient.VacancyHistory> GetVacancyHistoryByVacancyId(Guid VacancyId)
        {
            try
            {
                return _VacancyHistoryRespository.GetVacancyHistoryByVacancyId(VacancyId);
            }
            catch
            {
                //throw;
                return null;
            }
        }

        public bool DeleteVacancyHistoryByVacancyId(Guid VacancyId)
        {
            try
            {
                return _VacancyHistoryRespository.DeleteVacancyHistoryByVacancyId(VacancyId);
            }
            catch
            {
                //throw;
                return false;
            }
        }
    }
}
