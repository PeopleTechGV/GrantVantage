using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic
{
    public class QuestionDetailAction : ActionBase
    {
        #region private data member
        private DAClient.QuestionDetailRepository _QuestionDetailRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor

        public QuestionDetailAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _QuestionDetailRepository = new DAClient.QuestionDetailRepository(base.ConnectionString);
            _QuestionDetailRepository.CurrentUser = base.CurrentUser;
            _QuestionDetailRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public List<BEClient.QuestionDetails> GetQuestionDetails(Guid ApplicationId, string lstRndTypeStr)
        {
            try
            {
                return _QuestionDetailRepository.GetQuestionDetails(ApplicationId, lstRndTypeStr);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.QuestionDetails> GetScreeningQueByVacancyId(Guid VacancyId)
        {
            try
            {
                return _QuestionDetailRepository.GetScreeningQueByVacancyId(VacancyId);
            }
            catch
            {
                throw;
            }
        }
        


        public Boolean CheckForScreeningQueByVacIdAndRndType(Guid VacancyId, string lstRndTypeStr)
        {
            try
            {
                return _QuestionDetailRepository.CheckForScreeningQueByVacIdAndRndType(VacancyId, lstRndTypeStr);
            }
            catch
            {
                throw;
            }
        }


        public List<BEClient.QuestionDetails> GetQuestionsWithAnswers(Guid ApplicationId, string lstRndTypeStr)
        {
            try
            {
                return _QuestionDetailRepository.GetQuestionsWithAnswers(ApplicationId, lstRndTypeStr);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.QuestionDetails> GetQueAnsByScheduleIntId(Guid ScheduleIntId, Guid VacRndId)
        {
            try
            {
                return _QuestionDetailRepository.GetQueAnsByScheduleIntId(ScheduleIntId, VacRndId);
            }
            catch
            {
                throw;
            }
        }
    }
}
