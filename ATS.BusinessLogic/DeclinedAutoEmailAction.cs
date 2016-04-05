using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic
{
    public class DeclinedAutoEmailAction : ActionBase
    {
        #region private data member
        private DAClient.DeclinedAutoEmailRepository _DeclinedAutoEmailRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor
        public DeclinedAutoEmailAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _DeclinedAutoEmailRepository = new DAClient.DeclinedAutoEmailRepository(base.ConnectionString);
            _DeclinedAutoEmailRepository.CurrentUser = base.CurrentUser;
            _DeclinedAutoEmailRepository.CurrentClient = base.CurrentClient;

        }
        #endregion

        public Guid AddDeclinedAutoEmail(BEClient.DeclinedAutoEmail objDeclinedAutoEmail)
        {
            try
            {
                return _DeclinedAutoEmailRepository.AddDeclinedAutoEmail(objDeclinedAutoEmail);
            }
            catch
            {
                throw;
            }
        }
        public bool DeleteAllDeclinedAutoEmailByVacancyId(Guid VacancyId)
        {
            try
            {
                return _DeclinedAutoEmailRepository.DeleteAllDeclinedAutoEmailByVacancyId(VacancyId);
            }
            catch
            {
                throw;
            }
        }
        public List<BEClient.DeclinedAutoEmail> FillAppBasedStatusByVacancyId(Guid VacancyId)
        {
            try
            {
                return _DeclinedAutoEmailRepository.FillAppBasedStatusByVacancyId(VacancyId);
            }
            catch
            {
                throw;
            }
        }
    }
}
