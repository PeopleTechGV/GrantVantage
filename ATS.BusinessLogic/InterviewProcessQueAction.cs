using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic
{
    public class InterviewProcessQueAction : ActionBase
    {
        #region private data member
        private DAClient.InterviewProcessQueRepository _InterviewProcessQueRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor

        public InterviewProcessQueAction(Guid ClientId)
            : base(ClientId)
        {

            _MyClientId = ClientId;
            _InterviewProcessQueRepository = new DAClient.InterviewProcessQueRepository(base.ConnectionString);
            _InterviewProcessQueRepository.CurrentUser = base.CurrentUser;
            _InterviewProcessQueRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public List<BEClient.InterviewProcessQue> GetQueDetailsBtyOrder(Guid VacRndId, Guid QueCatId, Guid AppId, Guid LanguageId, Guid? QueId=null)
        {
            try
            {
                return _InterviewProcessQueRepository.GetQueDetailsBtyOrder(VacRndId, QueCatId, AppId, LanguageId, QueId);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.InterviewProcessQue GetQueDetailsBtyOrder(Guid VacRndId, Guid QueCatId, int Row, Guid LanguageId)
        {
            try
            {
                return _InterviewProcessQueRepository.GetQueDetailsBtyOrder(VacRndId, QueCatId, Row, LanguageId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.InterviewProcessQue> GetAllQueDetailsByOrder(Guid VacRndId, Guid LanguageId)
        {
            try
            {
                return _InterviewProcessQueRepository.GetAllQueDetailsByOrder(VacRndId, LanguageId);
            }
            catch
            {
                throw;
            }
        }
    }
}
