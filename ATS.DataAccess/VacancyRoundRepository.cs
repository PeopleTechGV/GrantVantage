using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATS.BusinessEntity;
using System.Data.Common;
using System.Data;

namespace ATS.DataAccess
{
    public class VacancyRoundRepository : Repository<VacancyRound>
    {
        public VacancyRoundRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public Guid AddVacancyRound(VacancyRound objVacancyRound, int imilli = 0)
        {
            bool IsInnerTransaction = false;

            try
            {
                if (base.Transaction == null)
                {
                    base.BeginTransaction();
                    IsInnerTransaction = true;
                }


                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertVacRnd");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@VacancyId", DbType.Guid, objVacancyRound.VacancyId);

                Database.AddInParameter(command, "@RndTypeId", DbType.Guid, objVacancyRound.RndTypeId);

                Database.AddInParameter(command, "@Weight", DbType.Int32, objVacancyRound.RoundWeight);

                Database.AddInParameter(command, "@AssignCanToRev", DbType.Int32, objVacancyRound.AssignCandiadteToReviewerId);

                Database.AddInParameter(command, "@ReqReviewer", DbType.Int32, objVacancyRound.ReqReviewer);

                Database.AddInParameter(command, "@PromoteCandidate", DbType.Boolean, objVacancyRound.PromoteCandidate);

                Database.AddInParameter(command, "@ProThresoldScore", DbType.Int32, objVacancyRound.PromotionThresoldScore);

                Database.AddInParameter(command, "@AssignCandidateBatches", DbType.Int32, objVacancyRound.AssignCandidateBatches);

                Database.AddInParameter(command, "@IsScreening", DbType.Boolean, objVacancyRound.IsScreening);

                Database.AddInParameter(command, "@OfferTemplateId", DbType.Guid, objVacancyRound.OfferTemplateId);

                Database.AddOutParameter(command, "VacRndId", DbType.Guid, 16);

                var result = base.Add(command, "VacRndId", true, imilli);
                if (result != null)
                {
                    Guid.TryParse(result.ToString(), out reREsult);
                }
                if (IsInnerTransaction)
                    base.CommitTransaction();

                return reREsult;

            }
            catch
            {
                if (IsInnerTransaction)
                    base.RollbackTransaction();

                throw;
            }
        }

        public bool Update(VacancyRound objVacancyRound)
        {

            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateRoundConfigByRoundId");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@RoundId", DbType.Guid, objVacancyRound.VacancyRoundId);

                Database.AddInParameter(command, "@RoundTypeId", DbType.Guid, objVacancyRound.RndTypeId);

                Database.AddInParameter(command, "@RoundWeight", DbType.Int32, objVacancyRound.RoundWeight);

                Database.AddInParameter(command, "@AssignCanToRev", DbType.Int32, objVacancyRound.AssignCandiadteToReviewerId);

                Database.AddInParameter(command, "@ReqReviewer", DbType.Int32, objVacancyRound.ReqReviewer);

                Database.AddInParameter(command, "@PromoteCandidate", DbType.Boolean, objVacancyRound.PromoteCandidate);

                Database.AddInParameter(command, "@ProThresoldScore", DbType.Int32, objVacancyRound.PromotionThresoldScore);

                Database.AddInParameter(command, "@AssignCandidateBatches", DbType.Int32, objVacancyRound.AssignCandidateBatches);

                Database.AddInParameter(command, "@OfferTemplateId", DbType.Guid, objVacancyRound.OfferTemplateId);


                var result = base.Save(command);

                if (result != null)
                {
                    bool.TryParse(result.ToString(), out reREsult);
                }

                return reREsult;
            }

            catch
            {
                throw;
            }
        }

        public bool DeleteVacRound(Guid VacRndId)
        {

            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("DeleteVacRnd");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@VacRnd", DbType.String, VacRndId.ToString());
                var result = base.Save(command, true);

                if (result != null)
                {
                    reREsult = true;
                }

                return reREsult;
            }

            catch
            {
                throw;
            }
        }

        public VacancyRound GetRoundConfigByRoundId(Guid RoundId, Guid LangaugeId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetRoundConfigByRoundId");
                Database.AddInParameter(command, "@RoundId", DbType.Guid, RoundId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LangaugeId);

                return base.FindOne(command, new GetRoundConfigByRoundIdFactory(), false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<VacancyRound> GetAllRound(Guid VacancyId, Guid LanguageId, bool FillScore, Guid ApplicationId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllRound");

                Database.AddInParameter(command, "@VacancyId", DbType.Guid, VacancyId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                if (FillScore)
                    Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);


                return base.Find(command, new GetAllRoundFactory(FillScore), false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public VacancyRound GetCountOfReviewersAndQuestion(Guid RoundId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetCountOfReviewersAndQuestion");
                Database.AddInParameter(command, "@RoundId", DbType.Guid, RoundId);

                return base.FindOne(command, new GetCountOfReviewersAndQuestionFactory(), false);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Boolean IsRndTypeSelfEval(Guid VacRndId, int RndType)
        {
            try
            {
                bool reResult = false;
                DbCommand command = Database.GetStoredProcCommand("VerifyRndTypeByVacRndId");
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                Database.AddInParameter(command, "@RndType", DbType.Int32, RndType);
                var result = base.FindScalarValue(command);
                if (result != null)
                {
                    Boolean.TryParse(result.ToString(), out reResult);
                }
                return reResult;
            }
            catch
            {
                throw;
            }
        }
        public VacancyRound GetVacRndFromVacIdAndRndTypeId(Guid VacId, Guid RndTypeId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetVacRndByVacIdAndRndTypeId");
                Database.AddInParameter(command, "@VacId", DbType.Guid, VacId);
                Database.AddInParameter(command, "@RndTypeId", DbType.Guid, RndTypeId);

                return base.FindOne(command, new GetVacancyRoundByVacIdAndRndTypeIdFactory(), false);
            }
            catch
            {
                throw;
            }
        }

        public VacancyRound GetVacRndByAppIdAndRndTypeId(Guid ApplicationId, string RndTypeId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetVacRndByAppIdAndRndTypeId");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                Database.AddInParameter(command, "@RndTypeId", DbType.String, RndTypeId);
                return base.FindOne(command, new GetVacRndByAppIdAndRndTypeIdFactory(), false);
            }
            catch
            {
                throw;
            }
        }

        public List<VacancyRound> GetAllRoundByApplicationId(Guid ApplicationId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllRoundByApplicationId");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                return base.Find(command, new GetAllRoundByApplicationIdFactory(), false);
            }
            catch (Exception)
            {
                throw;
            }
        }



        public List<VacancyRound> GetAllVacRndByVacancyIdAndLanguage(Guid VacancyId, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllVacRndByVacancyIdAndLanguage");
                Database.AddInParameter(command, "@VacancyId", DbType.Guid, VacancyId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.Find(command, new GetAllVacRndByVacancyIdAndLanguageFactory(), false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<VacancyRound> GetVacRoundsFromCurrentVacRound(Guid VacRndId,Guid ApplicationId ,Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetVacRoundsFromCurrentVacRound");
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.Find(command, new GetVacRoundsFromCurrentVacRoundFactory(), false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        
            internal class GetVacRoundsFromCurrentVacRoundFactory : IDomainObjectFactory<VacancyRound>
        {
            public VacancyRound Construct(IDataReader reader)
            {
                VacancyRound objVacancyRound = new VacancyRound();
                objVacancyRound.VacancyRoundId = HelperMethods.GetGuid(reader, "VacRndId");
                objVacancyRound.LocalName = HelperMethods.GetString(reader, "LocalName");
                objVacancyRound.CurrentRound = HelperMethods.GetString(reader, "CurrentRound");
                objVacancyRound.CurrentVacRoundId = HelperMethods.GetGuid(reader, "CurrentVacRoundId");
                objVacancyRound.RndOrder = HelperMethods.GetInt32(reader, "RndOrder");
                objVacancyRound.RoundAttributeNo = HelperMethods.GetInt32(reader, "RoundAttributesNo");
                return objVacancyRound;
            }

        }

        internal class GetAllRoundByApplicationIdFactory : IDomainObjectFactory<VacancyRound>
        {
            public VacancyRound Construct(IDataReader reader)
            {
                VacancyRound objVacancyRound = new VacancyRound();
                objVacancyRound.VacancyRoundId = HelperMethods.GetGuid(reader, "VacRndId");
                return objVacancyRound;
            }

        }

        internal class GetAllVacRndByVacancyIdAndLanguageFactory : IDomainObjectFactory<VacancyRound>
        {
            public VacancyRound Construct(IDataReader reader)
            {
                VacancyRound objVacancyRound = new VacancyRound();
                objVacancyRound.VacancyRoundId = HelperMethods.GetGuid(reader, "VacRndId");
                objVacancyRound.LocalName = HelperMethods.GetString(reader, "LocalName");
                
                return objVacancyRound;
            }
        }

        public Boolean UpdateIsScreening(string RndTypeId, Guid VacRndId)
        {
            try
            {
                bool reResult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateVacRndIsScreening");

                Database.AddInParameter(command, "@RndTypeId", DbType.String, RndTypeId);
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                var result = base.Save(command);
                if (result != null)
                {
                    Boolean.TryParse(result.ToString(), out reResult);
                }
                return reResult;
            }
            catch
            {
                throw;
            }
        }


        public VacancyRound GetActiveRoundForApplication(Guid ApplicationId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetActiveRoundForApplication");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);

                return base.FindOne(command, new GetActiveRoundForApplicationFactory(), false);
            }
            catch
            {
                throw;
            }
        }


        public bool UpdateVacRndOrder(Guid VacancyId, Guid VacRndId, string OrderDir)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("UpdateVacRndOrder");
                Database.AddInParameter(command, "@VacancyId", DbType.Guid, VacancyId);
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                Database.AddInParameter(command, "@Orderdir", DbType.String, OrderDir);
                return base.Save(command, false);

            }
            catch
            {
                throw;
            }
        }
        public int GetRoundCountByVancancyId(Guid VacancyId)
        {           
            try
            {
                
                int reREsult = 0;
                DbCommand command = Database.GetStoredProcCommand("GetVacancyRoundCoundByVacancyId");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@VacancyId", DbType.Guid, VacancyId);

                var result = base.FindScalarValue(command);
                if (result != null)
                {
                    Int32.TryParse(result.ToString(), out reREsult);
                }
                
                return reREsult;

            }
            catch
            {               
                throw;
            }
        }
        public Guid GetDivisionIdByVacRndId(Guid VacRndId)
        {
            try
            {
                Guid reResult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("GetDivisionIdByVacRndId");
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                var result = base.FindScalarValue(command);
                if (result != null)
                {
                    Guid.TryParse(result.ToString(), out reResult);
                }
                return reResult;
            }
            catch
            {
                throw;
            }
        }       

        internal class GetActiveRoundForApplicationFactory : IDomainObjectFactory<VacancyRound>
        {
            public VacancyRound Construct(IDataReader reader)
            {
                VacancyRound objVacancyRound = new VacancyRound();

                objVacancyRound.VacancyRoundId = HelperMethods.GetGuid(reader, "VacRndId");
                objVacancyRound.VacancyId = HelperMethods.GetGuid(reader, "VacancyId");
                objVacancyRound.RndOrder = HelperMethods.GetInt32(reader, "RndOrder");
                objVacancyRound.IsRndActive = HelperMethods.GetBoolean(reader, "IsRndActive");
                objVacancyRound.IsPromoted = HelperMethods.GetBoolean(reader, "IsPromoted");
                objVacancyRound.RoundAttributeNo = HelperMethods.GetInt32(reader, "RoundAttributeNo");
                objVacancyRound.OfferStatusId = HelperMethods.GetInt32(reader, "OfferStatusId");

                return objVacancyRound;
            }
        }
        internal class GetRoundConfigByRoundIdFactory : IDomainObjectFactory<VacancyRound>
        {
            public VacancyRound Construct(IDataReader reader)
            {
                VacancyRound objVacancyRound = new VacancyRound();

                objVacancyRound.VacancyRoundId = HelperMethods.GetGuid(reader, "VacRndId");
                objVacancyRound.VacancyId = HelperMethods.GetGuid(reader, "VacancyId");
                objVacancyRound.RoundWeight = HelperMethods.GetInt32(reader, "Weight");
                objVacancyRound.RndTypeId = HelperMethods.GetGuid(reader, "RndTypId");
                objVacancyRound.RoundType = HelperMethods.GetString(reader, "LocalName");
                objVacancyRound.ReqReviewer = HelperMethods.GetInt32(reader, "ReqReviewer");
                objVacancyRound.PromoteCandidate = HelperMethods.GetBoolean(reader, "PromoteCandidate");
                objVacancyRound.PromotionThresoldScore = HelperMethods.GetInt32(reader, "ProThresoldScore");
                objVacancyRound.AssignCandiadteToReviewerId = HelperMethods.GetInt32(reader, "AssignCandToRev");
                objVacancyRound.AssignCandidateBatches = HelperMethods.GetInt32(reader, "AssignCandidateBatches");
                objVacancyRound.OfferTemplateId = HelperMethods.GetGuid(reader, "OfferTemplateId");
                objVacancyRound.RoundAttributeNo = HelperMethods.GetInt32(reader, "RoundAttributeNo");
                objVacancyRound.RndCnt = HelperMethods.GetInt32(reader, "RndCnt");
                objVacancyRound.RndOrder = HelperMethods.GetInt32(reader, "RndOrder");
                objVacancyRound.DivisionId = HelperMethods.GetGuid(reader, "DivisionId");
                
                return objVacancyRound;
            }
        }

        internal class GetAllRoundFactory : IDomainObjectFactory<VacancyRound>
        {
            private bool _FillScore { get; set; }
            public GetAllRoundFactory(bool pFillScore)
            {
                _FillScore = pFillScore;
            }
            public VacancyRound Construct(IDataReader reader)
            {
                VacancyRound objVacancyRound = new VacancyRound();
                objVacancyRound.RoundWeight = HelperMethods.GetInt32(reader, "Weight");
                objVacancyRound.VacancyRoundId = HelperMethods.GetGuid(reader, "VacRndId");
                objVacancyRound.RoundType = HelperMethods.GetString(reader, "LocalName");
                if (_FillScore)
                    objVacancyRound.Score = HelperMethods.GetFloat(reader, "Score");
                else
                    objVacancyRound.Score = -1;
                objVacancyRound.IsRndActive = HelperMethods.GetBoolean(reader, "IsRndActive");
                objVacancyRound.RndOrder = HelperMethods.GetInt32(reader, "RndOrder");
                objVacancyRound.IsPromoted = HelperMethods.GetBoolean(reader, "IsPromoted");
                objVacancyRound.RoundAttributeNo = HelperMethods.GetInt32(reader, "RoundAttributesNo");
                objVacancyRound.OfferStatusId = HelperMethods.GetInt32(reader, "OfferStatusId");
                objVacancyRound.VacancyManagerId = HelperMethods.GetGuid(reader, "VacancyManagerId");
                objVacancyRound.VacancyId = HelperMethods.GetGuid(reader, "VacancyId");
                objVacancyRound.DivisionId = HelperMethods.GetGuid(reader, "DivisionId");

                return objVacancyRound;
            }
        }

        internal class GetCountOfReviewersAndQuestionFactory : IDomainObjectFactory<VacancyRound>
        {
            public VacancyRound Construct(IDataReader reader)
            {
                VacancyRound objVacancyRound = new VacancyRound();

                objVacancyRound.ReviewersCount = HelperMethods.GetInt32(reader, "CountReviewers");
                objVacancyRound.QueCount = HelperMethods.GetInt32(reader, "CountQue");
                objVacancyRound.RequiredDocumentCount = HelperMethods.GetInt32(reader, "CountRequiredDocument");
                return objVacancyRound;
            }
        }

        internal class GetVacRndByAppIdAndRndTypeIdFactory : IDomainObjectFactory<VacancyRound>
        {
            public VacancyRound Construct(IDataReader reader)
            {
                VacancyRound objVacancyRound = new VacancyRound();

                objVacancyRound.VacancyRoundId = HelperMethods.GetGuid(reader, "VacRndId");
                objVacancyRound.VacancyId = HelperMethods.GetGuid(reader, "VacancyId");
                objVacancyRound.RndTypeId = HelperMethods.GetGuid(reader, "RndTypId");
                return objVacancyRound;
            }
        }

        internal class GetVacancyRoundByVacIdAndRndTypeIdFactory : IDomainObjectFactory<VacancyRound>
        {
            public VacancyRound Construct(IDataReader reader)
            {
                VacancyRound objVacancyRound = new VacancyRound();

                objVacancyRound.VacancyRoundId = HelperMethods.GetGuid(reader, "VacRndId");
                objVacancyRound.VacancyId = HelperMethods.GetGuid(reader, "VacancyId");
                objVacancyRound.RoundWeight = HelperMethods.GetInt32(reader, "Weight");
                objVacancyRound.RndTypeId = HelperMethods.GetGuid(reader, "RndTypId");
                objVacancyRound.RoundType = HelperMethods.GetString(reader, "LocalName");
                objVacancyRound.ReqReviewer = HelperMethods.GetInt32(reader, "ReqReviewer");
                objVacancyRound.PromoteCandidate = HelperMethods.GetBoolean(reader, "PromoteCandidate");
                objVacancyRound.PromotionThresoldScore = HelperMethods.GetInt32(reader, "ProThresoldScore");
                objVacancyRound.AssignCandiadteToReviewerId = HelperMethods.GetInt32(reader, "AssignCandToRev");
                objVacancyRound.AssignCandidateBatches = HelperMethods.GetInt32(reader, "AssignCandidateBatches");

                return objVacancyRound;
            }
        }

        public List<VacancyRound> GetRndTypeByApplicationId(Guid ApplicationId, string LstRndTypeId, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetRndTypeByApplicationId");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                Database.AddInParameter(command, "@LstRndTypeId", DbType.String, LstRndTypeId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.Find(command, new GetRndTypeByApplicationIdFactory());
            }
            catch
            {
                throw;
            }
        }

        public List<VacancyRound> GetAllRndTypeByApplicationId(Guid ApplicationId, string LstRndTypeId, Guid ScheduleIntId, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllRndTypeByApplicationId");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                Database.AddInParameter(command, "@LstRndTypeId", DbType.String, LstRndTypeId);
                Database.AddInParameter(command, "@ScheduleIntId", DbType.Guid, ScheduleIntId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.Find(command, new GetRndTypeByApplicationIdFactory());
            }
            catch
            {
                throw;
            }
        }


        internal class GetRndTypeByApplicationIdFactory : IDomainObjectFactory<VacancyRound>
        {
            public VacancyRound Construct(IDataReader reader)
            {
                VacancyRound VacRnd = new VacancyRound();
                VacRnd.VacancyRoundId = HelperMethods.GetGuid(reader, "VacRndId");
                VacRnd.RoundType = HelperMethods.GetString(reader, "DefaultName");
                return VacRnd;
            }
        }

    }
}
