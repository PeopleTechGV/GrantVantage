using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;
using BESrp = ATS.BusinessEntity.SRPEntity;

namespace ATS.BusinessLogic
{
    public class VacancyQuestionAction : ActionBase
    {
        #region private data member
        private DAClient.VacancyQuestionRepository _VacancyQuestionRepository;
        private DAClient.QuestionRepository _QuestionRepository;

        private Guid _MyClientId { get; set; }
        private Guid _CurrentLanguageId;
        private BESrp.SRPVacancyQuestion _SRPVacancyQuestion { get; set; }


        #endregion

        public VacancyQuestionAction(Guid ClientId, bool CreateSRPObject = false)
            : base(ClientId)
        {
            if (CreateSRPObject)
                base.CreateSRPObject(SRPCommon.CreateSRPEntityObjects.SRPCreateObj<BESrp.SRPVacancyQuestion>(ClientId));

            _MyClientId = ClientId;
            _VacancyQuestionRepository = new DAClient.VacancyQuestionRepository(base.ConnectionString);
            _QuestionRepository = new DAClient.QuestionRepository(base.ConnectionString);

            _QuestionRepository.CurrentUser = _VacancyQuestionRepository.CurrentUser = base.CurrentUser;
            _QuestionRepository.CurrentClient = _VacancyQuestionRepository.CurrentClient = base.CurrentClient;
        }

        public Guid InsertVacancyQuestion(BEClient.VacancyQuestion objVacancyQuestion)
        {
            try
            {
                _VacancyQuestionRepository.BeginTransaction();

                Guid VacancyQuestionId = _VacancyQuestionRepository.InsertVacancyQuestion(objVacancyQuestion);

                _VacancyQuestionRepository.CommitTransaction();

                return VacancyQuestionId;

            }
            catch
            {
                _VacancyQuestionRepository.RollbackTransaction();
                throw;
            }
        }

        public bool Delete(Guid vacQueId)
        {
            try
            {
                bool Result = false;
                _VacancyQuestionRepository.BeginTransaction();
                Result = _VacancyQuestionRepository.Delete(vacQueId);
                if (Result)
                {
                    _VacancyQuestionRepository.CommitTransaction();
                    return Result;
                }
                return Result;
            }
            catch
            {
                _VacancyQuestionRepository.RollbackTransaction();
                throw;
            }
        }

        public bool UpdateVacQueWeightById(BEClient.VacancyQuestion objVacancyQuestion)
        {
            try
            {
                return _VacancyQuestionRepository.UpdateVacQueWeightById(objVacancyQuestion);
            }
            catch
            {
                throw;
            }
        }

        public bool Update(BEClient.VacancyQuestion objVacancyQuestion)
        {
            try
            {
                _VacancyQuestionRepository.BeginTransaction();

                bool recordUpdated = _VacancyQuestionRepository.Update(objVacancyQuestion);

                _VacancyQuestionRepository.CommitTransaction();
                return recordUpdated;
            }
            catch
            {
                _VacancyQuestionRepository.RollbackTransaction();
                throw;
            }
        }

        public List<BEClient.VacancyQuestion> GetAllQueByRoundId(Guid VacancyRoundId, Guid LanguageId)
        {
            try
            {
                List<BEClient.VacancyQuestion> _objVacancyQuestion = _VacancyQuestionRepository.GetAllQuestionByRoundId(VacancyRoundId, LanguageId);
                foreach (BEClient.VacancyQuestion current in _objVacancyQuestion)
                {
                    base.SetRoleRecordWise(current, current.DivisionId);
                }
                return _objVacancyQuestion;
            }
            catch
            {
                throw;
            }
        }

        //Anand:3rd OCT
        public List<BEClient.VacancyQuestion> GetVacQueByVacQueCatId(Guid VacQueCatId, Guid LanguageId)
        {
            try
            {
                List<BEClient.VacancyQuestion> _objVacancyQuestion = _VacancyQuestionRepository.GetVacQueByVacQueCatId(VacQueCatId, LanguageId);
                foreach (BEClient.VacancyQuestion current in _objVacancyQuestion)
                {
                    base.SetRoleRecordWise(current, current.DivisionId);
                }
                return _objVacancyQuestion;
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
                return Convert.ToBoolean(_common.IsRecordExists("VacQue", "VacQueId", recordid));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public BEClient.VacancyQuestion GetrecordByRecorId(Guid recordId)
        {
            try
            {
                BEClient.VacancyQuestion _VacancyQuestion = _VacancyQuestionRepository.GetRecordByRecordId(recordId);
                base.SetRoleRecordWise(_VacancyQuestion, _VacancyQuestion.DivisionId);
                return _VacancyQuestion;
            }
            catch
            {
                throw;
            }
        }

        public int GetCountOfVacQueByVacQueCatId(Guid VacQueCatId)
        {
            try
            {
                return _VacancyQuestionRepository.GetCountOfVacQueByVacQueCatId(VacQueCatId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.Question> GetQueByVacRndAndVacQueCat(Guid VacRndId, Guid VacQueCatId, Guid LanguageId, Guid? QueId = null)
        {
            List<BEClient.Question> questionList = null;
            try
            {
                String divisionList = base.GetAllDivisionsByCurrentUser;
                if (divisionList == "-1")
                {
                    questionList = null;
                }
                else
                {
                    questionList = _QuestionRepository.GetQueByVacRndAndVacQueCat(VacRndId, VacQueCatId, LanguageId, divisionList, QueId);

                }
                return questionList;
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.Question> GetQueByTVacRndAndTVacQueCat(Guid TVacRndId, Guid TVacQueCatId, Guid LanguageId, Guid? QueId = null)
        {
            List<BEClient.Question> questionList = null;
            try
            {
                String divisionList = base.GetAllDivisionsByCurrentUser;
                if (divisionList == "-1")
                {
                    questionList = null;
                }
                else
                {
                    questionList = _QuestionRepository.GetQueByTVacRndAndTVacQueCat(TVacRndId, TVacQueCatId, LanguageId, divisionList, QueId);
                }
                return questionList;
            }
            catch
            {
                throw;
            }
        }
        public bool UpdateVacQueOrder(Guid VacQueCatId, Guid VacQueId, string OrderDir)
        {
            try
            {
                return _VacancyQuestionRepository.UpdateVacQueOrder(VacQueCatId, VacQueId, OrderDir);
            }
            catch
            {
                throw;
            }
        }



    }
}
