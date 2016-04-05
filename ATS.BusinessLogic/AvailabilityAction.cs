using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic
{
    public class AvailabilityAction : ActionBase
    {
        #region private data member
        private DAClient.AvailabilityRepository _AvailabilityRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor

        public AvailabilityAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _AvailabilityRepository = new DAClient.AvailabilityRepository(base.ConnectionString);
            _AvailabilityRepository.CurrentUser = base.CurrentUser;
            _AvailabilityRepository.CurrentClient = base.CurrentClient;

        }
        #endregion

        public BEClient.Availability GetAvailabilityByProfileId(Guid ProfileId)
        {
            try
            {
                return _AvailabilityRepository.GetAvailabilityByProfileId(ProfileId);
            }
            catch
            {
                throw;
            }
        }

        #region CRUD
        public Guid AddAvailability(BEClient.Availability availibility)
        {
            try
            {
                _AvailabilityRepository.BeginTransaction();

                Guid UserId = availibility.CreatedBy;
                if (_AvailabilityRepository.CurrentUser == null)
                {

                    _AvailabilityRepository.CurrentUser = new BEClient.User() { UserId = UserId };
                }

                Guid AvailabilityId = _AvailabilityRepository.AddAvailability(availibility);

                _AvailabilityRepository.CommitTransaction();
                return AvailabilityId;



            }
            catch
            {
                _AvailabilityRepository.RollbackTransaction();
                throw;
            }
        }


        public bool UpdateAvailability(BEClient.Availability objavailability)
        {
            try
            {
                _AvailabilityRepository.BeginTransaction();
                Guid UserId = objavailability.CreatedBy;
                if (_AvailabilityRepository.CurrentUser == null)
                {

                    _AvailabilityRepository.CurrentUser = new BEClient.User() { UserId = UserId };
                }
                if (objavailability.ListHrsMon != null)
                    objavailability.HrsMon = string.Join(",", objavailability.ListHrsMon);
                if (objavailability.ListHrsTue != null)
                    objavailability.HrsTue = string.Join(",", objavailability.ListHrsTue);
                if (objavailability.ListHrsWed != null)
                    objavailability.HrsWed = string.Join(",", objavailability.ListHrsWed);
                if (objavailability.ListHrsThu != null)
                    objavailability.HrsThu = string.Join(",", objavailability.ListHrsThu);
                if (objavailability.ListHrsFri != null)
                    objavailability.HrsFri = string.Join(",", objavailability.ListHrsFri);
                if (objavailability.ListHrsSat != null)
                    objavailability.HrsSat = string.Join(",", objavailability.ListHrsSat);
                if (objavailability.ListHrsSun != null)
                    objavailability.HrsSun = string.Join(",", objavailability.ListHrsSun);


                bool IsAvailabilityUpdated = _AvailabilityRepository.UpdateAvailability(objavailability);
                if (IsAvailabilityUpdated)
                {
                    _AvailabilityRepository.CommitTransaction();
                    return IsAvailabilityUpdated;
                }
                else
                {
                    _AvailabilityRepository.RollbackTransaction();
                    return IsAvailabilityUpdated;
                }
            }
            catch
            {
                _AvailabilityRepository.RollbackTransaction();
                throw;
            }
        }

        #endregion
    }
}
