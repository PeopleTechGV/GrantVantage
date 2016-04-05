using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;

namespace ATS.DataAccess
{
    public class QuestionDetailRepository : Repository<BEClient.QuestionDetails>
    {
        public QuestionDetailRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }
        public List<BEClient.QuestionDetails> GetQuestionDetails(Guid ApplicationId, string lstRndTypeStr)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetQuestionListByQueType");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                Database.AddInParameter(command, "@lstRndTypeStr", DbType.String, lstRndTypeStr);
                return base.Find(command, new GetQuestionDetailsFactory());

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
                DbCommand command = Database.GetStoredProcCommand("GetScreeningQueByVacancyId");
                Database.AddInParameter(command, "@VacancyId", DbType.Guid, VacancyId);
                return base.Find(command, new GetQuestionDetailsFactory());
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
                int ScreeningCount = 0;
                DbCommand command = Database.GetStoredProcCommand("CheckForScreeningQueByVacIdAndRndType");
                Database.AddInParameter(command, "@VacancyId", DbType.Guid, VacancyId);
                Database.AddInParameter(command, "@lstRndTypeStr", DbType.String, lstRndTypeStr);
                var result = base.FindScalarValue(command);
                if (result != null)
                {
                    int.TryParse(result.ToString(), out ScreeningCount);
                }
                return (ScreeningCount > 0 ? true : false);
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
                DbCommand command = Database.GetStoredProcCommand("GetQueAnsByAppIdAndRndTypeId");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                Database.AddInParameter(command, "@lstRndTypeStr", DbType.String, lstRndTypeStr);
                return base.Find(command, new GetQuestionsAndAnswersFactory());
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
                DbCommand command = Database.GetStoredProcCommand("GetQueAnsByScheduleIntId");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ScheduleIntId);
                Database.AddInParameter(command, "@lstRndTypeStr", DbType.Guid, VacRndId);
                return base.Find(command, new GetQuestionsAndAnswersFactory());
            }
            catch
            {
                throw;
            }
        }

        internal class GetQuestionsAndAnswersFactory : IDomainObjectFactory<BEClient.QuestionDetails>
        {
            public BEClient.QuestionDetails Construct(IDataReader reader)
            {
                BEClient.QuestionDetails questiondetails = new BEClient.QuestionDetails();
                questiondetails.Question = new BEClient.Question();
                questiondetails.VacQue = new BEClient.VacancyQuestion();
                questiondetails.VacRnd = new BEClient.VacancyRound();
                questiondetails.AppAnswer = new BEClient.AppAnswer();

                questiondetails.Question.LocalName = HelperMethods.GetString(reader, "LocalName");
                questiondetails.Question.QueCatId = HelperMethods.GetGuid(reader, "QueCatId");
                questiondetails.Question.QueCatLocalName = HelperMethods.GetString(reader, "QueCatLocalName");
                questiondetails.Question.QueDataType = HelperMethods.GetInt32(reader, "QueDataType");
                questiondetails.Question.QueId = HelperMethods.GetGuid(reader, "QueId");

                questiondetails.VacQue.VacQueId = HelperMethods.GetGuid(reader, "VacQueId");
                questiondetails.VacQue.Weight = HelperMethods.GetInt32(reader, "VacQueWeight");

                questiondetails.VacRnd.VacancyRoundId = HelperMethods.GetGuid(reader, "VacRndId");
                questiondetails.VacRnd.RoundWeight = HelperMethods.GetInt32(reader, "VacRndWeight");

                questiondetails.AppAnswer.Answer = HelperMethods.GetString(reader, "Answer");
                questiondetails.AppAnswer.Value = HelperMethods.GetInt32(reader, "Value");

                return questiondetails;
            }
        }
        internal class GetQuestionDetailsFactory : IDomainObjectFactory<BEClient.QuestionDetails>
        {
            public BEClient.QuestionDetails Construct(IDataReader reader)
            {
                BEClient.QuestionDetails questiondetails = new BEClient.QuestionDetails();
                questiondetails.Question = new BEClient.Question();
                questiondetails.VacQue = new BEClient.VacancyQuestion();
                questiondetails.VacRnd = new BEClient.VacancyRound();

                questiondetails.Question.LocalName = HelperMethods.GetString(reader, "LocalName");
                questiondetails.Question.QueCatId = HelperMethods.GetGuid(reader, "QueCatId");
                questiondetails.Question.QueDataType = HelperMethods.GetInt32(reader, "QueDataType");
                questiondetails.Question.QueId = HelperMethods.GetGuid(reader, "QueId");
                questiondetails.Question.QueCatLocalName = HelperMethods.GetString(reader, "QueCatLocalName");

                questiondetails.VacQue.VacQueId = HelperMethods.GetGuid(reader, "VacQueId");
                questiondetails.VacQue.Weight = HelperMethods.GetInt32(reader, "VacQueWeight");

                questiondetails.VacRnd.VacancyRoundId = HelperMethods.GetGuid(reader, "VacRndId");
                questiondetails.VacRnd.RoundWeight = HelperMethods.GetInt32(reader, "VacRndWeight");
                return questiondetails;
            }
        }
    }
}