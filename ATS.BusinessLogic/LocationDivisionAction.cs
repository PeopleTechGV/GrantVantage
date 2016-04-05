using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic
{
    public class LocationDivisionAction : ActionBase
    {
        #region private data member
        private DAClient.LocationDivisionRepository _LocationDivisionRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor

        public LocationDivisionAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _LocationDivisionRepository = new DAClient.LocationDivisionRepository(base.ConnectionString);
            _LocationDivisionRepository.CurrentUser = base.CurrentUser;
            _LocationDivisionRepository.CurrentClient = base.CurrentClient;

        }
        #endregion

        public Guid AddLocationDivision(BEClient.LocationDivision locationdivision)
        {
            try
            {
                return _LocationDivisionRepository.AddLocationDivision(locationdivision);
            }
            catch
            {
                throw;
            }

        }

        public bool DelteLocationDivision(Guid LocationDivisionId)
        {
            try
            {
                return _LocationDivisionRepository.DeleteLocationDivision(LocationDivisionId);
            }
            catch
            {
                throw;
            }
        }

        public bool DeleteLocationDivisionByJobLocationId(Guid JobLocationId)
        {
            try
            {
                return _LocationDivisionRepository.DeleteLocationDivisionByJobLocationId(JobLocationId);
            }
            catch
            {
                throw;
            }
        }
    }
}
