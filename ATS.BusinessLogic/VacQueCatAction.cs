using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAClient = ATS.DataAccess;
using BEClient = ATS.BusinessEntity;
using BESrp = ATS.BusinessEntity.SRPEntity;

namespace ATS.BusinessLogic
{
    public class VacQueCatAction : ActionBase
    {
        #region private data member
        private DAClient.VacQueCatRepository _VacQueCatRepository;
        private Guid _MyClientId { get; set; }
        private BESrp.SRPVacQueCat _SRPVacQueCat { get; set; }
        #endregion

        #region Constructor
        public VacQueCatAction(Guid ClientId, bool CreateSRPObject = false) : base(ClientId)        
        {
            if (CreateSRPObject)
                base.CreateSRPObject(SRPCommon.CreateSRPEntityObjects.SRPCreateObj<BESrp.SRPVacQueCat>(ClientId));

            _MyClientId = ClientId;
            _VacQueCatRepository = new DAClient.VacQueCatRepository(base.ConnectionString);
            _VacQueCatRepository.CurrentUser = base.CurrentUser;
            _VacQueCatRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public Guid AddSaveVacQueCat(BEClient.VacQueCat VacQueCat)
        {
            try
            {
                return _VacQueCatRepository.AddSaveVacQueCat(VacQueCat);
            }
            catch
            {
                throw;
            }
        }

        public Boolean UpdateVacQueCat(BEClient.VacQueCat VacQueCat)
        {
            try
            {
                return _VacQueCatRepository.UpdateVacQueCat(VacQueCat);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.VacQueCat GetVacQueCatById(Guid VacQueCatId)
        {
            try
            {
                return _VacQueCatRepository.GetVacQueCatById(VacQueCatId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.VacQueCat> GetVacQueCatByRoundId(Guid VacRndId, Guid LanguageId)
        {
            try
            {
                List<BEClient.VacQueCat> _objVacQueCat = _VacQueCatRepository.GetVacQueCatByRoundId(VacRndId, LanguageId);
                foreach (BEClient.VacQueCat current in _objVacQueCat)
                {
                    base.SetRoleRecordWise(current, current.DivisionId);
                }

                return _objVacQueCat;
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

                _VacQueCatRepository.BeginTransaction();
                result = _VacQueCatRepository.Delete(VacQueCatId, VacRndId);
                if (result)
                {
                    _VacQueCatRepository.CommitTransaction();
                }
                else
                {
                    _VacQueCatRepository.RollbackTransaction();
                }


            }
            catch
            {
                _VacQueCatRepository.RollbackTransaction();
                throw;
            }
            return result;
        }
        public bool UpdateVacQueCatOrder(Guid VacRndId, Guid VacQueCatId, string OrderDir)
        {
            try
            {
                return _VacQueCatRepository.UpdateVacQueCatOrder(VacRndId, VacQueCatId, OrderDir);
            }
            catch
            {
                throw;
            }
        }

    }


}
