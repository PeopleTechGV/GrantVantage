using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAClient = ATS.DataAccess;
using BEClient = ATS.BusinessEntity;
using BESrp = ATS.BusinessEntity.SRPEntity;

namespace ATS.BusinessLogic
{
    public class QuestionAction : ActionBase
    {

        #region private data member
        private DAClient.QuestionRepository _QuestionRepository;
        private Guid _MyClientId { get; set; }
        private BESrp.SRPQuestion _SRPQuestion { get; set; }
        #endregion

        private BESrp.SRPQuestion SRPQuestion { get { return SRPQuestion; } }

        #region Constructor

        public QuestionAction(Guid ClientId, bool CreateSRPObject = false)
            : base(ClientId)
        {
            if (CreateSRPObject)
                base.CreateSRPObject(SRPCommon.CreateSRPEntityObjects.SRPCreateObj<BESrp.SRPQuestion>(ClientId));

            _MyClientId = ClientId;
            _QuestionRepository = new DAClient.QuestionRepository(base.ConnectionString);
            _QuestionRepository.CurrentUser = base.CurrentUser;
            _QuestionRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public BEClient.Question GetRecordByRecordId(Guid recordId)
        {
            try
            {
                BEClient.Question objQue = _QuestionRepository.GetRecordByRecordId(recordId);
                objQue.EntityLanguageList = new List<BEClient.EntityLanguage>();

                DAClient.EntityLanguageRepository _ObjEntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                objQue.EntityLanguageList = _ObjEntityLanguageRepository.GetEntityLanguageByEntityAndRecordId(objQue.Privilage, objQue.QueId);
                base.SetRoleRecordWise(objQue, objQue.DivisionId);
                return objQue;
            }
            catch
            {
                throw;
            }
        }
        public List<BEClient.Question> GetAllQueLanguage(Guid LanguageId)
        {
            try
            {
                List<BEClient.Question> _queList = _QuestionRepository.GetAllQue(LanguageId);
                foreach (BEClient.Question _current in _queList)
                    base.SetRoleRecordWise(_current, _current.DivisionId);

                return _queList;
            }
            catch
            {
                throw;
            }
        }
        public bool UpdateQueOrder(Guid QueCatId, Guid QueId, string OrderDir)
        {
            try
            {
                return _QuestionRepository.UpdateQueOrder(QueCatId, QueId, OrderDir);
            }
            catch
            {
                throw;
            }
        }
        public Guid AddQue(BEClient.Question pQue)
        {
            try
            {
                _QuestionRepository.BeginTransaction();
                DAClient.EntityLanguageRepository EntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                EntityLanguageRepository.Transaction = _QuestionRepository.Transaction;
                EntityLanguageRepository.CurrentUser = _QuestionRepository.CurrentUser;
                EntityLanguageRepository.CurrentClient = _QuestionRepository.CurrentClient;
                string fieldValue = string.Empty;
                fieldValue = string.Format("{0} = '{1}' and {2} = {3}", BEClient.Common.CommonTblVal.DefaultName, pQue.LocalName, BEClient.Common.CommonTblVal.IsDelete, 0);
                Guid QueId = _QuestionRepository.AddQue(pQue, fieldValue);
                foreach (BEClient.EntityLanguage clientLanguage in pQue.EntityLanguageList)
                {
                    clientLanguage.EntityName = pQue.Privilage;
                    clientLanguage.RecordId = QueId;
                    EntityLanguageRepository.AddEntityLanguage(clientLanguage);

                }
                DAClient.AnsOptRepository AnsOptRepository = new DAClient.AnsOptRepository(base.ConnectionString);

                AnsOptRepository.Transaction = _QuestionRepository.Transaction;
                AnsOptRepository.CurrentUser = _QuestionRepository.CurrentUser;
                AnsOptRepository.CurrentClient = _QuestionRepository.CurrentClient;


                if (pQue.AnsOptList != null && pQue.AnsOptList.Count() > 0)
                {
                    foreach (BEClient.AnsOpt _current in pQue.AnsOptList)
                    {
                        _current.QueId = QueId;
                        Guid AnsOptId = AnsOptRepository.AddAnsOpt(_current);


                        foreach (BEClient.EntityLanguage clientLanguage in pQue.EntityLanguageList)
                        {
                            clientLanguage.EntityName = _current.Privilage;
                            clientLanguage.RecordId = AnsOptId;
                            EntityLanguageRepository.AddEntityLanguage(clientLanguage);

                        }

                    }
                }

                _QuestionRepository.CommitTransaction();
                return QueId;

            }
            catch
            {
                _QuestionRepository.RollbackTransaction();
                throw;
            }
        }

        public bool UpdateQue(BEClient.Question Que)
        {
            try
            {
                _QuestionRepository.BeginTransaction();

                string fieldValue = string.Empty;
                fieldValue = string.Format("{0} = '{1}' and {2} != '{3}' and {4} = {5}", BEClient.Common.CommonTblVal.DefaultName, Que.LocalName, BEClient.Common.QueTbl.QueId, Que.QueCatId, BEClient.Common.CommonTblVal.IsDelete, 0);

                DAClient.EntityLanguageRepository EntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                EntityLanguageRepository.Transaction = _QuestionRepository.Transaction;
                EntityLanguageRepository.CurrentUser = _QuestionRepository.CurrentUser;
                EntityLanguageRepository.CurrentClient = _QuestionRepository.CurrentClient;

                bool recordUpdated = _QuestionRepository.UpdateQueCat(Que, fieldValue);

                if (recordUpdated)
                {
                    foreach (BEClient.EntityLanguage clientLanguage in Que.EntityLanguageList)
                    {
                        EntityLanguageRepository.UpdateEntityLanguage(new BEClient.EntityLanguage()
                        {
                            EntityName = Que.Privilage,

                            LanguageId = clientLanguage.LanguageId,
                            LocalName = clientLanguage.LocalName,
                            RecordId = Que.QueId
                        }
                        );
                    }
                }

                _QuestionRepository.CommitTransaction();
                return recordUpdated;
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
                return Convert.ToBoolean(_common.IsRecordExists("Que", "QueId", recordid));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<BEClient.Question> GetAllQueByQueCat(Guid QueCatId, Guid LanguageId)
        {
            try
            {
                string ListOfDivision = base.GetAllDivisionsByCurrentUser;
                //List<BEClient.Question> _queList = _QuestionRepository.GetAllQueByQueCatId(QueCatId, LanguageId, ListOfDivision);
                //foreach (BEClient.Question _current in _queList)
                //    base.SetRoleRecordWise(_current, _current.DivisionId);
                List<BEClient.Question> _queList = _QuestionRepository.GetAllQueByQueCatId(QueCatId, LanguageId, ListOfDivision);
                return _queList;

            }
            catch
            {
                throw;
            }
        }

        public bool InlineUpdateQuestion(BEClient.Question objQue, Guid LanguageId)
        {
            try
            {
                _QuestionRepository.BeginTransaction();

                bool Result = _QuestionRepository.InlineUpdateQuestion(objQue, LanguageId);
                if (Result)
                {
                    _QuestionRepository.CommitTransaction();
                }
                else
                {
                    _QuestionRepository.RollbackTransaction();
                }
                return Result;

            }
            catch
            {
                _QuestionRepository.RollbackTransaction();
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

        public List<BEClient.Question> GetAllQueByVacRnd(Guid VacRndId, Guid? QueId)
        {
            try
            {

                return _QuestionRepository.GetAllQueByVacRnd(VacRndId, QueId);
                //foreach (BEClient.Vacancy _Vacancy in vacancyList)
                //{
                //    base.SetRoleRecordWise(_Vacancy, _Vacancy.DivisionId);
                //}
                //return _QuestionRepository.GetAllQueByVacRnd(VacRndId, QueId);
            }
            catch
            {
                throw;
            }
        }


        public int GetNewQueOrder(Guid QueCatId)
        {
            try
            {
                return _QuestionRepository.GetNewQueOrder(QueCatId);
            }
            catch
            {
                throw;
            }
        }
        public bool DeleteQue(Guid QueId)
        {
            try
            {
                return _QuestionRepository.DeleteQue(QueId);
            }
            catch
            {
                throw;
            }
        }

    }
}
