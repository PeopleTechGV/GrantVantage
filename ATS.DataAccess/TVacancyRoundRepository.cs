using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Data.Common;
using System.Data;


namespace ATS.DataAccess
{
    public class TVacancyRoundRepository : Repository<BEClient.TVacancyRound>
    {
        public TVacancyRoundRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }
        public Guid AddTVacancyRound(BEClient.TVacancyRound tvacancyround)
        {
            bool LocalTrasaction = false;
            try
            {
                if (base.Transaction == null)
                {
                    base.BeginTransaction();
                    LocalTrasaction = true;
                }
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertTVacRnd");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@TVacId", System.Data.DbType.Guid, tvacancyround.TVacId);
                Database.AddInParameter(command, "@RndTypeId", DbType.Guid, tvacancyround.RndTypeId);
                Database.AddInParameter(command, "@Weight", DbType.Int32, tvacancyround.RoundWeight);
                Database.AddInParameter(command, "@ReqReviewer", DbType.Int32, tvacancyround.ReqReviewer);
                Database.AddInParameter(command, "@PromoteCandidate", DbType.Boolean, tvacancyround.PromoteCandidate);
                Database.AddInParameter(command, "@ProThresoldScore", DbType.Int32, tvacancyround.PromotionThresoldScore);
                Database.AddInParameter(command, "@AssignCandidateBatches", DbType.Int32, tvacancyround.AssignCandidateBatches);
                Database.AddInParameter(command, "@AssignCandToRev", DbType.Int32, tvacancyround.AssignCandiadteToReviewerId);
                Database.AddInParameter(command, "@OfferTemplateId", DbType.Guid, tvacancyround.OfferTemplateId); ;
                
                Database.AddOutParameter(command, "TVacRndId", DbType.Guid, 16); ;

                var result = base.Add(command, "TVacRndId");

                if (result != null)
                {
                    Guid.TryParse(result.ToString(), out reREsult);
                }
                if (LocalTrasaction)
                    base.CommitTransaction();

                return reREsult;
            }

            catch
            {
                if (LocalTrasaction)
                    this.RollbackTransaction();
                throw;
            }
        }

        public bool UpdateTVacancyRound(BEClient.TVacancyRound tvacancyround)
        {
            bool LocalTrasaction = false;
            try
            {
                if (base.Transaction == null)
                {
                    base.BeginTransaction();
                    LocalTrasaction = true;
                }
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateTVacRnd");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@TVacId", System.Data.DbType.Guid, tvacancyround.TVacId);

                Database.AddInParameter(command, "@RndTypeId", DbType.Guid, tvacancyround.RndTypeId);

                Database.AddInParameter(command, "@Weight", DbType.Int32, tvacancyround.RoundWeight);

                Database.AddInParameter(command, "@ReqReviewer", DbType.Int32, tvacancyround.ReqReviewer);
                Database.AddInParameter(command, "@PromoteCandidate", DbType.Boolean, tvacancyround.PromoteCandidate);
                Database.AddInParameter(command, "@ProThresoldScore", DbType.Int32, tvacancyround.PromotionThresoldScore);

                Database.AddInParameter(command, "@AssignCandidateBatches", DbType.Int32, tvacancyround.AssignCandidateBatches);
                Database.AddInParameter(command, "@AssignCandToRev", DbType.Int32, tvacancyround.AssignCandiadteToReviewerId);
                Database.AddInParameter(command, "@OfferTemplateId", DbType.Guid, tvacancyround.OfferTemplateId); ;

                //Database.AddInParameter(command, "@IsDelete", DbType.Boolean, tvacancyround.IsDelete);


                Database.AddInParameter(command, "@TVacRndId", DbType.Guid, tvacancyround.TVacRndId); ;

                var result = base.Save(command);

                if (result != null)
                {
                    bool.TryParse(result.ToString(), out reREsult);
                }
                if (LocalTrasaction)
                    base.CommitTransaction();

                return reREsult;
            }

            catch
            {
                if (LocalTrasaction)
                    this.RollbackTransaction();
                throw;
            }
        }

        public List<BEClient.TVacancyRound> GetAllTVacancyRound()
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllTVacRnd");

                return base.Find(command, new GetAllTVacancyRoundFactory());
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
                DbCommand command = Database.GetStoredProcCommand("GetTVacRndByTVacId");
                Database.AddInParameter(command, "@TVacId", DbType.Guid, TVacId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);

                return base.FindOne(command, new GetTVacRndByTVacIdFactory());
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.TVacancyRound> GetAllTVacRndByTVacId(Guid TVacId, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("[GetAllTVacRndByTVacId]");
                Database.AddInParameter(command, "@TVacId", DbType.Guid, TVacId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);

                return base.Find(command, new GetAllTVacRndByTVacIdFactory(), false);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.TVacancyRound GetRecordByRecordId(Guid TVacancyRoundId, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetTvacRndById");
                Database.AddInParameter(command, "@TVacRndId", DbType.Guid, TVacancyRoundId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.FindOne(command, new GetAllTVacancyRoundFactory());
            }
            catch
            {
                throw;
            }
        }
        public BEClient.TVacancyRound GetTVacRoundConfigByTVacRoundId(Guid TVacRoundId, Guid LangaugeId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetTRoundConfigByRoundId");
                Database.AddInParameter(command, "@TVacRndId", DbType.Guid, TVacRoundId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LangaugeId);
                return base.FindOne(command, new GetTVacRoundConfigByRoundIdFactory(), false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal class GetTVacRoundConfigByRoundIdFactory : IDomainObjectFactory<BEClient.TVacancyRound>
        {
            public BEClient.TVacancyRound Construct(IDataReader reader)
            {
                BEClient.TVacancyRound objTVacancyRound = new BEClient.TVacancyRound();
                objTVacancyRound.TVacRndId = HelperMethods.GetGuid(reader, "TVacRndId");
                objTVacancyRound.TVacId = HelperMethods.GetGuid(reader, "TVacId");
                objTVacancyRound.RoundWeight = HelperMethods.GetInt32(reader, "Weight");
                objTVacancyRound.RndTypeId = HelperMethods.GetGuid(reader, "RndTypeId");
                objTVacancyRound.RoundType = HelperMethods.GetString(reader, "LocalName");
                objTVacancyRound.PromoteCandidate = HelperMethods.GetBoolean(reader, "PromoteCandidate");
                objTVacancyRound.PromotionThresoldScore = HelperMethods.GetInt32(reader, "ProThresoldScore");
                objTVacancyRound.RoundAttributeNo = HelperMethods.GetInt32(reader, "RoundAttributesNo");
                objTVacancyRound.OfferTemplateId = HelperMethods.GetGuid(reader, "OfferTemplateId");
               
                
                return objTVacancyRound;
            }
        }

        internal class GetAllTVacancyRoundFactory : IDomainObjectFactory<BEClient.TVacancyRound>
        {
            public BEClient.TVacancyRound Construct(IDataReader reader)
            {
                BEClient.TVacancyRound tvacancyround = new BEClient.TVacancyRound();
                tvacancyround.TVacRndId = HelperMethods.GetGuid(reader, "TVacRndId");
                tvacancyround.TVacId = HelperMethods.GetGuid(reader, "TVacId");
                tvacancyround.RndTypeId = HelperMethods.GetGuid(reader, "RndTypeId");
                tvacancyround.RoundWeight = HelperMethods.GetInt32(reader, "Weight");
                tvacancyround.ReqReviewer = HelperMethods.GetInt32(reader, "ReqReviewer");
                tvacancyround.PromoteCandidate = HelperMethods.GetBoolean(reader, "PromoteCandidate");
                tvacancyround.PromotionThresoldScore = HelperMethods.GetInt32(reader, "ProThresoldScore");
                tvacancyround.AssignCandidateBatches = HelperMethods.GetInt32(reader, "AssignCandidateBatches");
                tvacancyround.AssignCandiadteToReviewerId = HelperMethods.GetInt32(reader, "AssignCandToRev");
                tvacancyround.QueCount = HelperMethods.GetInt32(reader, "AssignCandToRev");
                tvacancyround.OfferTemplateId = HelperMethods.GetGuid(reader, "OfferTemplateId");
                
                return tvacancyround;
            }
        }
        internal class GetRecordByRecordIdFactory : IDomainObjectFactory<BEClient.TVacancyRound>
        {
            public BEClient.TVacancyRound Construct(IDataReader reader)
            {
                BEClient.TVacancyRound tvacancyround = new BEClient.TVacancyRound();
                tvacancyround.TVacRndId = HelperMethods.GetGuid(reader, "TVacRndId");
                tvacancyround.TVacId = HelperMethods.GetGuid(reader, "TVacId");
                tvacancyround.RndTypeId = HelperMethods.GetGuid(reader, "RndTypeId");
                tvacancyround.RoundWeight = HelperMethods.GetInt32(reader, "Weight");
                tvacancyround.ReqReviewer = HelperMethods.GetInt32(reader, "ReqReviewer");
                tvacancyround.PromoteCandidate = HelperMethods.GetBoolean(reader, "PromoteCandidate");
                tvacancyround.PromotionThresoldScore = HelperMethods.GetInt32(reader, "ProThresoldScore");
                tvacancyround.AssignCandidateBatches = HelperMethods.GetInt32(reader, "AssignCandidateBatches");
                tvacancyround.AssignCandiadteToReviewerId = HelperMethods.GetInt32(reader, "AssignCandToRev");
                tvacancyround.OfferTemplateId = HelperMethods.GetGuid(reader, "OfferTemplateId");
                
                return tvacancyround;
            }
        }
        internal class GetTVacRndByTVacIdFactory : IDomainObjectFactory<BEClient.TVacancyRound>
        {
            public BEClient.TVacancyRound Construct(IDataReader reader)
            {
                BEClient.TVacancyRound tvacancyround = new BEClient.TVacancyRound();
                tvacancyround.TVacRndId = HelperMethods.GetGuid(reader, "TVacRndId");
                tvacancyround.TVacId = HelperMethods.GetGuid(reader, "TVacId");
                tvacancyround.RoundType = HelperMethods.GetString(reader, "LocalName");
                tvacancyround.RndTypeId = HelperMethods.GetGuid(reader, "RndTypeId");
                tvacancyround.RoundWeight = HelperMethods.GetInt32(reader, "Weight");
                tvacancyround.ReqReviewer = HelperMethods.GetInt32(reader, "ReqReviewer");
                tvacancyround.PromoteCandidate = HelperMethods.GetBoolean(reader, "PromoteCandidate");
                tvacancyround.PromotionThresoldScore = HelperMethods.GetInt32(reader, "ProThresoldScore");
                tvacancyround.AssignCandidateBatches = HelperMethods.GetInt32(reader, "AssignCandidateBatches");
                tvacancyround.AssignCandiadteToReviewerId = HelperMethods.GetInt32(reader, "AssignCandToRev");
                return tvacancyround;
            }
        }
        internal class GetAllTVacRndByTVacIdFactory : IDomainObjectFactory<BEClient.TVacancyRound>
        {
            public BEClient.TVacancyRound Construct(IDataReader reader)
            {
                BEClient.TVacancyRound tvacancyround = new BEClient.TVacancyRound();
                tvacancyround.TVacRndId = HelperMethods.GetGuid(reader, "TVacRndId");
                tvacancyround.TVacId = HelperMethods.GetGuid(reader, "TVacId");
                tvacancyround.RoundType = HelperMethods.GetString(reader, "LocalName");
                tvacancyround.RndTypeId = HelperMethods.GetGuid(reader, "RndTypeId");
                tvacancyround.RoundWeight = HelperMethods.GetInt32(reader, "Weight");
                tvacancyround.ReqReviewer = HelperMethods.GetInt32(reader, "ReqReviewer");
                tvacancyround.PromoteCandidate = HelperMethods.GetBoolean(reader, "PromoteCandidate");
                tvacancyround.PromotionThresoldScore = HelperMethods.GetInt32(reader, "ProThresoldScore");
                tvacancyround.AssignCandidateBatches = HelperMethods.GetInt32(reader, "AssignCandidateBatches");
                tvacancyround.AssignCandiadteToReviewerId = HelperMethods.GetInt32(reader, "AssignCandToRev");
                tvacancyround.RoundAttributeNo = HelperMethods.GetInt32(reader, "RoundAttributesNo");
                tvacancyround.OfferTemplateId = HelperMethods.GetGuid(reader, "OfferTemplateId");
                tvacancyround.RndOrder = HelperMethods.GetInt32(reader, "RndOrder");
               
                return tvacancyround;
            }
        }

        public BEClient.TVacancyRound GetCountOfTRevAndTQue(Guid TRoundId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetCountOfTRevAndTQue");
                Database.AddInParameter(command, "@TRoundId", DbType.Guid, TRoundId);
                return base.FindOne(command, new GetCountOfTRevAndTQueFactory(), false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal class GetCountOfTRevAndTQueFactory : IDomainObjectFactory<BEClient.TVacancyRound>
        {
            public BEClient.TVacancyRound Construct(IDataReader reader)
            {
                BEClient.TVacancyRound objVacancyRound = new BEClient.TVacancyRound();
                objVacancyRound.ReviewersCount = HelperMethods.GetInt32(reader, "CountReviewers");
                objVacancyRound.QueCount = HelperMethods.GetInt32(reader, "CountQue");
                objVacancyRound.RequiredDocumentCount = HelperMethods.GetInt32(reader, "CountRequiredDocument");
                return objVacancyRound;
            }
        }


        public bool DeleteTVacRound(Guid TVacRndId)
        {

            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("DeleteTVacRnd");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@TVacRnd", DbType.String, TVacRndId.ToString());
                var result = base.Save(command, false);

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
        public bool UpdateTVacRndOrder(Guid TVacId, Guid TVacRndId, string OrderDir)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("UpdateTVacRndOrder");
                Database.AddInParameter(command, "@TVacId", DbType.Guid, TVacId);
                Database.AddInParameter(command, "@TVacRndId", DbType.Guid, TVacRndId);
                Database.AddInParameter(command, "@Orderdir", DbType.String, OrderDir);
                return base.Save(command, false);

            }
            catch
            {
                throw;
            }
        }

    }
}
