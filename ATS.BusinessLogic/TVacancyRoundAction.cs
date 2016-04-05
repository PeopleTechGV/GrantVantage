using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic
{
    public class TVacancyRoundAction : ActionBase
    {
        #region private data member
        private DAClient.TVacancyRoundRepository _TVacancyRoundRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor

        public TVacancyRoundAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _TVacancyRoundRepository = new DAClient.TVacancyRoundRepository(base.ConnectionString);
            _TVacancyRoundRepository.CurrentUser = base.CurrentUser;
            _TVacancyRoundRepository.CurrentClient = base.CurrentClient;

        }
        #endregion


        public List<BEClient.TVacancyRound> GetAllTVacRound()
        {
            try
            {
                return _TVacancyRoundRepository.GetAllTVacancyRound();
            }
            catch
            {
                throw;
            }
        }


        public BEClient.TVacancyRound GetTVacRndByTVacId(Guid TVacId, Guid LanguageId)
        {
            try
            {
                return _TVacancyRoundRepository.GetTVacRndByTVacId(TVacId, LanguageId);
            }
            catch
            {
                throw;
            }
        }


        public List<BEClient.TVacancyRound> GetAllTVacRndByVacId(Guid TVacId, Guid LanguageId)
        {
            try
            {
                return _TVacancyRoundRepository.GetAllTVacRndByTVacId(TVacId, LanguageId);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.TVacancyRound GetrecordByRecordId(Guid TVacancyRoundId, Guid LanguageId)
        {

            try
            {
                return _TVacancyRoundRepository.GetRecordByRecordId(TVacancyRoundId, LanguageId);
            }
            catch
            {
                throw;
            }
        }

        public Guid AddTVacancyRound(BEClient.TVacancyRound tvacancyround)
        {
            Guid tvacroundId = Guid.Empty;
            try
            {
                _TVacancyRoundRepository.BeginTransaction();
                tvacroundId = _TVacancyRoundRepository.AddTVacancyRound(tvacancyround);
                if (!tvacroundId.Equals(Guid.Empty))
                {
                    _TVacancyRoundRepository.CommitTransaction();
                }
                else
                {
                    _TVacancyRoundRepository.RollbackTransaction();
                }
            }
            catch
            {
                _TVacancyRoundRepository.RollbackTransaction();
                throw;
            }
            return tvacroundId;
        }


        public bool UpdateTVacancyRound(BEClient.TVacancyRound tvacancyround)
        {
            bool IsTvacancyroundUpdated = false;
            try
            {
                _TVacancyRoundRepository.BeginTransaction();


                IsTvacancyroundUpdated = _TVacancyRoundRepository.UpdateTVacancyRound(tvacancyround);
                if (IsTvacancyroundUpdated)
                {
                    _TVacancyRoundRepository.CommitTransaction();

                }
                else
                {
                    _TVacancyRoundRepository.RollbackTransaction();

                }
            }
            catch
            {
                _TVacancyRoundRepository.RollbackTransaction();
                throw;
            }
            return IsTvacancyroundUpdated;
        }

        public List<BEClient.TVacancyRound> GetAllTVacRndByTVacId(Guid TVacId, Guid LanguageId)
        {
            try
            {
                return _TVacancyRoundRepository.GetAllTVacRndByTVacId(TVacId, LanguageId);
            }
            catch
            {
                throw;
            }
        }


        public BEClient.TVacancyRound GetTVacRoundConfigByTVacRoundId(Guid TVacRoundId, Guid languageId)
        {
            try
            {
                BEClient.TVacancyRound objVacancyRound = _TVacancyRoundRepository.GetTVacRoundConfigByTVacRoundId(TVacRoundId, languageId);
                return objVacancyRound;
            }
            catch
            {
                throw;
            }
        }

        public BEClient.TVacancyRound GetCountOfTRevAndTQue(Guid TRoundId)
        {
            try
            {
                return _TVacancyRoundRepository.GetCountOfTRevAndTQue(TRoundId);
            }
            catch
            {
                throw;
            }
        }

        public bool Delete(Guid TVacRndId)
        {
            try
            {
                bool Result = false;
                _TVacancyRoundRepository.BeginTransaction();
                Result = _TVacancyRoundRepository.DeleteTVacRound(TVacRndId);
                if (Result)
                {
                    _TVacancyRoundRepository.CommitTransaction();
                    return Result;
                }
                return Result;
            }
            catch
            {
                _TVacancyRoundRepository.RollbackTransaction();
                throw;
            }

        }
        public bool UpdateTVacRndOrder(Guid TVacId, Guid TVacRndId, string OrderDir)
        {
            try
            {
                return _TVacancyRoundRepository.UpdateTVacRndOrder(TVacId, TVacRndId, OrderDir);
            }
            catch
            {
                throw;
            }
        }
    }
}
