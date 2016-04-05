using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAClient = ATS.DataAccess;
using BEClient = ATS.BusinessEntity;

namespace ATS.BusinessLogic
{
    public class RoundAttributesAction : ActionBase
    {
        #region private data member
        private DAClient.RoundAttributesRepository _RoundAttributesRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor
        public RoundAttributesAction(Guid ClientId)
            : base(ClientId)
        {

            _MyClientId = ClientId;
            _RoundAttributesRepository = new DAClient.RoundAttributesRepository(base.ConnectionString);
            _RoundAttributesRepository.CurrentUser = base.CurrentUser;
            _RoundAttributesRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public List<BEClient.RoundAttributes> FillAllRoundAttributes(Guid LanguageId)
        {
            try
            {
                return _RoundAttributesRepository.FillAllRoundAttributes(LanguageId);
            }
            catch
            {
                throw;
            }
        }

        public string GetQuestionTypeByRoundAttributesId(Guid RoundAttributesId)
        {
            try
            {
                return _RoundAttributesRepository.GetQuestionTypeByRoundAttributesId(RoundAttributesId);
            }
            catch
            {
                throw;
            }
        }

        public int GetRoundAttributesNo(Guid RndTypeId)
        {
            try
            {
                return _RoundAttributesRepository.GetRoundAttributesNo(RndTypeId);
            }
            catch
            {
                throw;
            }
        }

        public int GetRoundAttributesNoByVacRndId(Guid VacRndId)
        {
            try
            {
                return _RoundAttributesRepository.GetRoundAttributesNoByVacRndId(VacRndId);
            }
            catch
            {
                throw;
            }
        }

        public int GetRoundAttributesNoByTVacRndId(Guid TVacRndId)
        {
            try
            {
                return _RoundAttributesRepository.GetRoundAttributesNoByTVacRndId(TVacRndId);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.RoundAttributes GetRoundAttributeDetailsById(Guid RoundAttributeId)
        {
            try
            {
                return _RoundAttributesRepository.GetRoundAttributeDetailsById(RoundAttributeId);
            }
            catch
            {
                throw;
            }
        }
    }
}
