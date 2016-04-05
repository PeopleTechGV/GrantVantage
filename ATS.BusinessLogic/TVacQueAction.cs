using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;
using BESrp = ATS.BusinessEntity.SRPEntity;

namespace ATS.BusinessLogic
{
    public class TVacQueAction : ActionBase
    {
        #region private data member
        private DAClient.TVacQueRepository _TVacQueRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor
        public TVacQueAction(Guid ClientId, bool CreateSRPObject = false)
            : base(ClientId)
        {
            if (CreateSRPObject)
                base.CreateSRPObject(SRPCommon.CreateSRPEntityObjects.SRPCreateObj<BESrp.SRPTVacQue>(ClientId));

            _MyClientId = ClientId;
            _TVacQueRepository = new DAClient.TVacQueRepository(base.ConnectionString);
            _TVacQueRepository.CurrentUser = base.CurrentUser;
            _TVacQueRepository.CurrentClient = base.CurrentClient;

        }
        #endregion

        public Guid InsertTVacQue(BEClient.TVacQue objTVacQue)
        {
            return _TVacQueRepository.InsertTVacQue(objTVacQue);
        }

        public bool UpdateTVacQueWeightById(BEClient.TVacQue TVacQue)
        {
            try
            {
                return _TVacQueRepository.UpdateTVacQueWeightById(TVacQue);
            }
            catch
            {
                throw;
            }
        }

        public bool IsRecordExists(Guid recordid)
        {
            try
            {
                DAClient.CommonRepository _common = new DAClient.CommonRepository(base.ConnectionString);
                return Convert.ToBoolean(_common.IsRecordExists("TVacQue", "TVacQueId", recordid));
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Update(BEClient.TVacQue objTVacQue)
        {
            try
            {
                return _TVacQueRepository.Update(objTVacQue);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.TVacQue GetRecordByrecordId(Guid TVacQueId)
        {
            try
            {
                BEClient.TVacQue _objTVacQue = new BEClient.TVacQue();
                _objTVacQue = _TVacQueRepository.GetTVacQueById(TVacQueId);

                base.SetRoleRecordWise(_objTVacQue, _objTVacQue.DivisionId);
                return _objTVacQue;
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.TVacQue> GetTVacQueByTVacQueCatId(Guid TVacQueCatId, Guid LanguageId)
        {
            try
            {
                List<BEClient.TVacQue> _objTVacQue = new List<BEClient.TVacQue>();
                _objTVacQue = _TVacQueRepository.GetTVacQueByTVacQueCatId(TVacQueCatId, LanguageId);

                foreach (BEClient.TVacQue current in _objTVacQue)
                {
                    base.SetRoleRecordWise(current, current.DivisionId);
                }
                return _objTVacQue;
            }
            catch
            {
                throw;
            }
        }

        public bool Delete(Guid TVacQueId)
        {
            try
            {
                bool Result = false;
                _TVacQueRepository.BeginTransaction();
                Result = _TVacQueRepository.Delete(TVacQueId);
                if (Result)
                {
                    _TVacQueRepository.CommitTransaction();
                    return Result;
                }
                return Result;
            }
            catch
            {
                _TVacQueRepository.RollbackTransaction();
                throw;
            }

        }
        public bool UpdateTVacQueOrder(Guid TVacQueCatId, Guid TVacQueId, string OrderDir)
        {
            try
            {
                return _TVacQueRepository.UpdateTVacQueOrder(TVacQueCatId, TVacQueId, OrderDir);
            }
            catch
            {
                throw;
            }
        }

    }
}
