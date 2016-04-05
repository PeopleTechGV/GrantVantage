using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAClient = ATS.DataAccess;
using BEClient = ATS.BusinessEntity;
using BESrp = ATS.BusinessEntity.SRPEntity;
namespace ATS.BusinessLogic
{
    public class CopyTemplateAction : ActionBase
    {
        #region private data member
        private DAClient.VacancyRepository _ObjVacancyRepository;
        private DAClient.VacancyRoundRepository _ObjVacancyRoundRepository;
        private DAClient.VacQueCatRepository _ObjVacQueCatRepository;
        private DAClient.VacancyQuestionRepository _ObjVacancyQuestionRepository;
        private DAClient.ReviewersRepository _ObjReviewersRepository;
        private DAClient.VacancyStatusRepository _ObjVacancyStatusRepository;
        private DAClient.RequiredDocumentRepository _ObjRequiredDocumentRepository;
        private DAClient.AppInstructionDocsRepository _objAppInstructionDocsRepository;

        private DAClient.TVacRepository _ObjTVacRepository;
        private DAClient.TVacancyRoundRepository _ObjTVacancyRound;
        private DAClient.TVacQueCatRepository _ObjTVacQueCatRepository;
        private DAClient.TVacQueRepository _ObjTVacQueRepository;
        private DAClient.TVacReviewMemberRepository _objTVacReviewMemberRepository;
        private DAClient.TRequiredDocumentRepository _ObjTRequiredDocumentRepository;
        private DAClient.TAppInstructionDocsRepository _objTAppInstructionDocsRepository;

        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor

        public CopyTemplateAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;

            _ObjVacancyRepository = new DAClient.VacancyRepository(base.ConnectionString);
            _ObjVacancyRepository.CurrentUser = base.CurrentUser;
            _ObjVacancyRepository.CurrentClient = base.CurrentClient;

            _ObjVacancyStatusRepository = new DAClient.VacancyStatusRepository(base.ConnectionString);
            _ObjVacancyStatusRepository.CurrentUser = base.CurrentUser;
            _ObjVacancyStatusRepository.CurrentClient = base.CurrentClient;


            _ObjVacancyRoundRepository = new DAClient.VacancyRoundRepository(base.ConnectionString);
            _ObjVacancyRoundRepository.CurrentUser = base.CurrentUser;
            _ObjVacancyRoundRepository.CurrentClient = base.CurrentClient;

            _ObjVacQueCatRepository = new DAClient.VacQueCatRepository(base.ConnectionString);
            _ObjVacQueCatRepository.CurrentUser = base.CurrentUser;
            _ObjVacQueCatRepository.CurrentClient = base.CurrentClient;

            _ObjVacancyQuestionRepository = new DAClient.VacancyQuestionRepository(base.ConnectionString);
            _ObjVacancyQuestionRepository.CurrentUser = base.CurrentUser;
            _ObjVacancyQuestionRepository.CurrentClient = base.CurrentClient;

            _ObjReviewersRepository = new DAClient.ReviewersRepository(base.ConnectionString);
            _ObjReviewersRepository.CurrentUser = base.CurrentUser;
            _ObjReviewersRepository.CurrentClient = base.CurrentClient;

            _ObjRequiredDocumentRepository = new DAClient.RequiredDocumentRepository(base.ConnectionString);
            _ObjRequiredDocumentRepository.CurrentUser = base.CurrentUser;
            _ObjRequiredDocumentRepository.CurrentClient = base.CurrentClient;

            _objAppInstructionDocsRepository = new DAClient.AppInstructionDocsRepository(base.ConnectionString);
            _objAppInstructionDocsRepository.CurrentUser = base.CurrentUser;
            _objAppInstructionDocsRepository.CurrentClient = base.CurrentClient;

            _ObjTVacancyRound = new DAClient.TVacancyRoundRepository(base.ConnectionString);
            _ObjTVacRepository = new DAClient.TVacRepository(base.ConnectionString);
            _ObjTVacQueCatRepository = new DAClient.TVacQueCatRepository(base.ConnectionString);
            _ObjTVacQueRepository = new DAClient.TVacQueRepository(base.ConnectionString);
            _objTVacReviewMemberRepository = new DAClient.TVacReviewMemberRepository(base.ConnectionString);
            _ObjTRequiredDocumentRepository = new DAClient.TRequiredDocumentRepository(base.ConnectionString);
            _objTAppInstructionDocsRepository = new DAClient.TAppInstructionDocsRepository(base.ConnectionString);

        }
        #endregion

        public Guid TemplateToMainObject(Guid TVacId, Guid LanguageId)
        {
            bool IsProccessComplete = true;
            try
            {
                _ObjVacancyRoundRepository.BeginTransaction();

                //Continue Transaction for  Vacancy QueCategory
                _ObjVacQueCatRepository.Connection = _ObjVacancyRoundRepository.Connection;
                _ObjVacQueCatRepository.Transaction = _ObjVacancyRoundRepository.Transaction;
                _ObjVacancyRoundRepository.CurrentUser = base.CurrentUser;


                //Continue Transaction for  Vacancy Question
                _ObjVacancyQuestionRepository.Connection = _ObjVacancyRoundRepository.Connection;
                _ObjVacancyQuestionRepository.Transaction = _ObjVacancyRoundRepository.Transaction;
                _ObjVacancyQuestionRepository.CurrentUser = base.CurrentUser;


                //Continue Transaction for  Vacancy Reviewer
                _ObjReviewersRepository.Connection = _ObjVacancyRoundRepository.Connection;
                _ObjReviewersRepository.Transaction = _ObjVacancyRoundRepository.Transaction;
                _ObjReviewersRepository.CurrentUser = base.CurrentUser;

                //Continue Transaction for  Vacancy QueCategory
                _ObjVacancyRepository.Connection = _ObjVacancyRoundRepository.Connection;
                _ObjVacancyRepository.Transaction = _ObjVacancyRoundRepository.Transaction;
                _ObjVacancyRepository.CurrentUser = base.CurrentUser;

                //Continue Transaction for  Vacancy QueCategory
                _ObjRequiredDocumentRepository.Connection = _ObjVacancyRoundRepository.Connection;
                _ObjRequiredDocumentRepository.Transaction = _ObjVacancyRoundRepository.Transaction;
                _ObjRequiredDocumentRepository.CurrentUser = base.CurrentUser;

                //Continue Transaction for  Vacancy QueCategory
                _objTAppInstructionDocsRepository.Connection = _ObjVacancyRoundRepository.Connection;
                _objTAppInstructionDocsRepository.Transaction = _ObjVacancyRoundRepository.Transaction;
                _objTAppInstructionDocsRepository.CurrentUser = base.CurrentUser;

                BEClient.TVac _TVacancy = new BEClient.TVac();
                _TVacancy = _ObjTVacRepository.GetAllTVacDetailsByTVacId(TVacId);

                BEClient.Vacancy Vac = new BEClient.Vacancy();
                Vac = _TVacancy.ObjVacancy;

                List<BEClient.VacancyStatus> VacStatus = new List<BEClient.VacancyStatus>();
                VacStatus = _ObjVacancyStatusRepository.GetAllVacancyStatusByCategoryAndLanguage(LanguageId, "Draft");
                Vac.VacancyStatusId = VacStatus[0].VacancyStatusId;
                Vac.ClientId = CurrentClient.ClientId;
                Vac.PositionID = _ObjVacancyRepository.GetNewPositionId();
                Vac.JobTitle = Vac.JobTitle + " - " + Vac.PositionID;
                Vac.PositionRequestedBy = CurrentUser.UserId;
                Vac.PositionOwner = CurrentUser.UserId;
                Vac.LanguageId = LanguageId;
                Vac.ShowOnWeb = "Draft";
                string fieldValue = string.Empty;
                fieldValue = string.Format("{0} = '{1}' and {2} = {3}", BEClient.Common.VacancyTbl.PositionId, Vac.PositionID, BEClient.Common.CommonTblVal.IsDelete, 0);
                Guid VacancyId = _ObjVacancyRepository.AddGrantVacancy(Vac, fieldValue);
                Vac.VacancyId = VacancyId;

                _ObjVacancyRepository.BeginTransaction();
                Boolean Result = _ObjVacancyRepository.UpdateCompensationInfoByVacancyId(Vac);
                if (Result == true)
                {
                    _ObjVacancyRepository.CommitTransaction();
                }

                else
                {
                    _ObjVacancyRepository.RollbackTransaction();
                }

                _ObjVacancyRepository.BeginTransaction();
                Result = _ObjVacancyRepository.UpdateJobDescriptionByVacancyId(Vac);
                if (Result == true)
                {
                    _ObjVacancyRepository.CommitTransaction();
                }
                else
                {
                    _ObjVacancyRepository.RollbackTransaction();
                }

                List<BEClient.TVacancyRound> _TAllVacancyRound = new List<BEClient.TVacancyRound>();
                _TAllVacancyRound.AddRange(_ObjTVacancyRound.GetAllTVacRndByTVacId(TVacId, LanguageId));
                if (_TAllVacancyRound == null)
                    _TAllVacancyRound = new List<BEClient.TVacancyRound>();

                List<BEClient.TVacQueCat> _TAllVacQueCat = _ObjTVacQueCatRepository.GetTVacQueCatByTVacId(TVacId);
                if (_TAllVacQueCat == null)
                    _TAllVacQueCat = new List<BEClient.TVacQueCat>();

                List<BEClient.TVacQue> _TAllVacQue = _ObjTVacQueRepository.GetTVacQueIdByTVacId(TVacId);
                if (_TAllVacQue == null)
                    _TAllVacQue = new List<BEClient.TVacQue>();

                List<BEClient.TVacReviewMember> _TAllVacRev = _objTVacReviewMemberRepository.GetTVacReviewMemberByTVacId(TVacId);
                if (_TAllVacRev == null)
                    _TAllVacRev = new List<BEClient.TVacReviewMember>();

                List<BEClient.TRequiredDocument> _TAllReqDoc = _ObjTRequiredDocumentRepository.GetTRequiredDocumentByTVacId(TVacId);
                if (_TAllReqDoc == null)
                    _TAllReqDoc = new List<BEClient.TRequiredDocument>();

                List<BEClient.TAppInstructionDocs> _TAllAppInstructionDocs = _objTAppInstructionDocsRepository.GetTAppInstructionDocsByTVacId(TVacId);
                if (_TAllReqDoc == null)
                    _TAllReqDoc = new List<BEClient.TRequiredDocument>();

                int imilli = 1;

                foreach (BEClient.TVacancyRound _currentTVacRnd in _TAllVacancyRound)
                {
                    BEClient.VacancyRound _currentVacRnd = _currentTVacRnd.ObjVacancyRound;
                    _currentVacRnd.VacancyId = VacancyId;
                    try
                    {
                        imilli = imilli + 5;
                        _currentVacRnd.VacancyRoundId = _ObjVacancyRoundRepository.AddVacancyRound(_currentVacRnd, imilli);

                        if (_currentVacRnd.VacancyRoundId != null && !_currentVacRnd.VacancyRoundId.Equals(Guid.Empty))
                        {
                            #region Add Vacacny Question Category
                            //Get Vacancy Category Template from TVacQueCat repository based on _currentVacRnd.TVacRndId
                            List<BEClient.TVacQueCat> _TListVacQueCat = new List<BEClient.TVacQueCat>();
                            if (_TAllVacQueCat.Where(x => x.TVacRndId.ToString().Equals(_currentTVacRnd.TVacRndId.ToString())).Count() > 0)
                            {
                                _TListVacQueCat = _TAllVacQueCat.Where(x => x.TVacRndId.ToString().Equals(_currentTVacRnd.TVacRndId.ToString())).ToList();
                            }

                            foreach (BEClient.TVacQueCat _currentTVacQueCat in _TListVacQueCat)
                            {
                                BEClient.VacQueCat _currentVacQueCat = _currentTVacQueCat.ObjVacQueCat;
                                _currentVacQueCat.VacRndId = _currentVacRnd.VacancyRoundId;
                                try
                                {

                                    imilli = imilli + 5;
                                    _currentVacQueCat.VacQueCatId = _ObjVacQueCatRepository.AddSaveVacQueCat(_currentVacQueCat, imilli);

                                    if (_currentVacQueCat.VacQueCatId != null && !_currentVacQueCat.VacQueCatId.Equals(Guid.Empty))
                                    {
                                        #region Add Vacancy Question
                                        //Get Vacancy Questions Template from TVacQue repository based on _currentTVacQueCat.TVacQueCatId 
                                        List<BEClient.TVacQue> _TListVacQue = new List<BEClient.TVacQue>();
                                        if (_TAllVacQue.Where(x => x.TVacQueCatId.ToString().Equals(_currentTVacQueCat.TVacQueCatId.ToString())).Count() > 0)
                                        {
                                            _TListVacQue = _TAllVacQue.Where(x => x.TVacQueCatId.ToString().Equals(_currentTVacQueCat.TVacQueCatId.ToString()) && x.TVacRndId.ToString().Equals(_currentVacRnd.TVacRndId.ToString())).ToList();
                                        }


                                        foreach (BEClient.TVacQue _currentTVacQue in _TListVacQue)
                                        {
                                            BEClient.VacancyQuestion _currentVacancyQuestion = _currentTVacQue.ObjVacancyQuestion;
                                            _currentVacancyQuestion.VacQueCatId = _currentVacQueCat.VacQueCatId;
                                            _currentVacancyQuestion.RndTypeId = _currentVacRnd.VacancyRoundId;


                                            imilli = imilli + 5;
                                            _currentVacancyQuestion.VacQueId = _ObjVacancyQuestionRepository.InsertVacancyQuestion(_currentVacancyQuestion, imilli);
                                            if (_currentVacancyQuestion.VacQueId != null && !_currentVacancyQuestion.VacQueId.Equals(Guid.Empty))
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                IsProccessComplete = false;
                                                throw new Exception("Vacancy Question not inserted from template");
                                            }
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        IsProccessComplete = false;
                                        throw new Exception("Vacancy Question Category not inserted from template");
                                    }
                                }
                                catch (Exception)
                                {
                                    IsProccessComplete = false;
                                    throw;
                                }

                            }
                            #endregion

                            #region Add Vacancy Reviewer
                            //Get TVacReviewMember based on _currentTVacRnd.TVacRndId
                            List<BEClient.TVacReviewMember> _TListVacReviewMember;
                            _TListVacReviewMember = new List<BEClient.TVacReviewMember>();

                            if (_TAllVacRev.Where(x => x.RndTypeId.ToString().Equals(_currentTVacRnd.TVacRndId.ToString())).Count() > 0)
                            {
                                _TListVacReviewMember = _TAllVacRev.Where(x => x.RndTypeId.ToString().Equals(_currentTVacRnd.TVacRndId.ToString())).ToList();
                            }

                            foreach (BEClient.TVacReviewMember _currentTVacReviewMember in _TListVacReviewMember)
                            {
                                BEClient.Reviewers _currentVacReviewers = _currentTVacReviewMember.ObjVacReviewers;
                                _currentVacReviewers.RndTypeId = _currentVacRnd.VacancyRoundId;
                                try
                                {
                                    imilli = imilli + 5;
                                    _currentVacReviewers.VacancyReviewMemberId = _ObjReviewersRepository.InsertVacReviewMember(_currentVacReviewers, imilli);
                                    if (_currentVacReviewers.VacancyReviewMemberId != null && !_currentVacReviewers.VacancyReviewMemberId.Equals(Guid.Empty))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        IsProccessComplete = false;
                                        throw new Exception("Vacancy Review Member not inserted from template");
                                    }
                                }
                                catch
                                {

                                    throw;
                                }
                            }
                            #endregion

                            #region Add Required Document
                            List<BEClient.TRequiredDocument> _TListRequiredDocument = new List<BEClient.TRequiredDocument>();
                            if (_TAllReqDoc.Where(x => x.TVacRndId.ToString().Equals(_currentTVacRnd.TVacRndId.ToString())).Count() > 0)
                            {
                                _TListRequiredDocument = _TAllReqDoc.Where(x => x.TVacRndId.ToString().Equals(_currentTVacRnd.TVacRndId.ToString())).ToList();
                            }

                            foreach (BEClient.TRequiredDocument _currentTRequiredDocument in _TListRequiredDocument)
                            {
                                BEClient.RequiredDocument _currentRequiredDocument = _currentTRequiredDocument.objRequiredDocument;
                                _currentRequiredDocument.VacRndId = _currentVacRnd.VacancyRoundId;
                                try
                                {
                                    _currentRequiredDocument.RequiredDocumentId = _ObjRequiredDocumentRepository.InsertRequiredDocument(_currentRequiredDocument);
                                    if (_currentRequiredDocument.RequiredDocumentId != null && !_currentRequiredDocument.RequiredDocumentId.Equals(Guid.Empty))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        IsProccessComplete = false;
                                        throw new Exception("Vacancy Review Member not inserted from template");
                                    }
                                }
                                catch
                                {

                                    throw;
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            IsProccessComplete = false;
                            throw new Exception("Vacancy Round not inserted from template");
                        }
                    }
                    catch
                    {
                        IsProccessComplete = false;
                        throw;
                    }
                }

                foreach (BEClient.TAppInstructionDocs _CurrentAIDoc in _TAllAppInstructionDocs)
                {
                    BEClient.AppInstructionDoc objAppInstDoc = new BEClient.AppInstructionDoc();
                    objAppInstDoc.VacancyId = VacancyId;
                    objAppInstDoc.FileName = _CurrentAIDoc.FileName;
                    objAppInstDoc.NewFileName = _CurrentAIDoc.NewFileName;
                    objAppInstDoc.Path = _CurrentAIDoc.Path;
                    Guid NewAppInstDocId = _objAppInstructionDocsRepository.InsertAppInstructionDocs(objAppInstDoc);
                }

                if (IsProccessComplete)
                {
                    _ObjVacancyRoundRepository.CommitTransaction();
                }
                return VacancyId;
            }
            catch
            {
                _ObjVacancyRoundRepository.RollbackTransaction();
                IsProccessComplete = false;
                throw;
            }

        }

    }
}
