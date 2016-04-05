using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic
{
    public class TVacQueCatAction : ActionBase
    {


        #region private data member
        private DAClient.TVacQueCatRepository _TVacQueCatRepository;
        private Guid _MyClientId { get; set; }
        #endregion
        #region Constructor

        public TVacQueCatAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _TVacQueCatRepository = new DAClient.TVacQueCatRepository(base.ConnectionString);
            _TVacQueCatRepository.CurrentUser = base.CurrentUser;
            _TVacQueCatRepository.CurrentClient = base.CurrentClient;

        }
        #endregion

        public List<BEClient.TVacQueCat> GetAllTVacQueCat()
        {
            try
            {
                return _TVacQueCatRepository.GetAllTVacQueCat();
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.TVacQueCat> GetTVacQueCatByTVacId(Guid TVacId)
        {
            try
            {
                return _TVacQueCatRepository.GetTVacQueCatByTVacId(TVacId);
            }
            catch
            {
                throw;
            }
        }


        public BEClient.TVacQueCat GetrecordByRecordId(Guid TVacQueCatId, Guid LanguageId)
        {

            try
            {
                return _TVacQueCatRepository.GetRecordByRecordId(TVacQueCatId, LanguageId);
            }
            catch
            {
                throw;
            }
        }


        public Guid AddTVaQueCatd(BEClient.TVacQueCat tvacquecat)
        {
            Guid tvacquecatid = Guid.Empty;

            try
            {
                _TVacQueCatRepository.BeginTransaction();


                tvacquecatid = _TVacQueCatRepository.AddTVacQueCat(tvacquecat);
                if (!tvacquecatid.Equals(Guid.Empty))
                {
                    _TVacQueCatRepository.CommitTransaction();

                }
                else
                {

                    _TVacQueCatRepository.RollbackTransaction();

                }
            }
            catch
            {
                _TVacQueCatRepository.RollbackTransaction();
                throw;
            }
            return tvacquecatid;

        }


        public Boolean UpdateTVacQueCatWeight(BEClient.TVacQueCat TVacQueCat)
        {
            try
            {
                return _TVacQueCatRepository.UpdateTVacQueCatWeight(TVacQueCat);
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateTVacancy(BEClient.TVacQueCat tvacquecat)
        {
            bool IsTvacquecatUpdated = false;
            try
            {
                _TVacQueCatRepository.BeginTransaction();


                IsTvacquecatUpdated = _TVacQueCatRepository.UpdateTVacQueCat(tvacquecat);
                if (IsTvacquecatUpdated)
                {
                    _TVacQueCatRepository.CommitTransaction();

                }
                else
                {
                    _TVacQueCatRepository.RollbackTransaction();

                }
            }
            catch
            {
                _TVacQueCatRepository.RollbackTransaction();
                throw;
            }
            return IsTvacquecatUpdated;
        }
        public List<BEClient.TVacQueCat> GetTVacQueCatByRoundId(Guid TVacRndId, Guid LanguageId)
        {
            try
            {
                return _TVacQueCatRepository.GetTVacQueCatByRoundId(TVacRndId, LanguageId);
            }
            catch
            {
                throw;
            }
        }

        public bool Delete(Guid VacQueCatId, Guid VacRndId)
        {
            bool result = false;
            try
            {
                _TVacQueCatRepository.BeginTransaction();
                result = _TVacQueCatRepository.Delete(VacQueCatId, VacRndId);
                if (result)
                {
                    _TVacQueCatRepository.CommitTransaction();
                }
                else
                {
                    _TVacQueCatRepository.RollbackTransaction();
                }
            }
            catch
            {
                _TVacQueCatRepository.RollbackTransaction();
                throw;
            }
            return result;
        }

        public bool UpdateTVacQueCatOrder(Guid VacRndId, Guid VacQueCatId, string OrderDir)
        {
            try
            {
                return _TVacQueCatRepository.UpdateTVacQueCatOrder(VacRndId,VacQueCatId,OrderDir);
            }
            catch
            {
                throw;
            }
        }

    }
}