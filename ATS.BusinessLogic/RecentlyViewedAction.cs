using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic
{
    public class RecentlyViewedAction:ActionBase
    {
        #region private data member
        private DAClient.RecentlyViewedRepository _RecentlyViewedRepository;
        private Guid _MyClientId { get; set; }
        #endregion
        #region Constructor

        public RecentlyViewedAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _RecentlyViewedRepository = new DAClient.RecentlyViewedRepository(base.ConnectionString);
            _RecentlyViewedRepository.CurrentUser = base.CurrentUser;
            _RecentlyViewedRepository.CurrentClient = base.CurrentClient;

        }
        #endregion

        public Guid InsertRecentlyViewed(BEClient.RecentlyViewed recentlyviewed)
        {
            try
            {
                _RecentlyViewedRepository.BeginTransaction();
                Guid newId = _RecentlyViewedRepository.AddRecentlyViewed(recentlyviewed);
                if (!newId.Equals(Guid.Empty))
                {
                    _RecentlyViewedRepository.CommitTransaction();
                    return newId;
                }
                else
                {
                    _RecentlyViewedRepository.RollbackTransaction();
                    return newId;
                }
            }
            catch
            {
                _RecentlyViewedRepository.RollbackTransaction();
                throw;


            }
        }
        public List<BEClient.RecentlyViewed> GetRecentlyViewedApplication(BEClient.RecentlyViewed objRecentlView)
        {
            try
            {
                return _RecentlyViewedRepository.GetRecentlyViewedApplication(objRecentlView);
            }
            catch
            {
                throw;
            }
        }


    }
}
