using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;

namespace ATS.DataAccess
{
    public class TVacReviewMemberRepository : Repository<BEClient.TVacReviewMember>
    {
        public TVacReviewMemberRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }
        public Guid InsertTVacReviewMember(BEClient.TVacReviewMember objTVacReviewMember)
        {
            try
            {
                Guid reResult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertTVacReviewMember");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@TVacId", DbType.Guid, objTVacReviewMember.TVacId);
                Database.AddInParameter(command, "@TVacRndId", DbType.Guid, objTVacReviewMember.RndTypeId);
                Database.AddInParameter(command, "@UserId", DbType.Guid, objTVacReviewMember.UserId);
                Database.AddInParameter(command, "@Weight", DbType.String, objTVacReviewMember.Weight);
                Database.AddInParameter(command, "@CanPromote", DbType.Boolean, objTVacReviewMember.CanPromote);
                Database.AddInParameter(command, "@CanMakeOffers", DbType.Boolean, objTVacReviewMember.CanMakeOffers);
                Database.AddInParameter(command, "@CanEditOffers", DbType.Boolean, objTVacReviewMember.CanEditOffers);
                                
                Database.AddOutParameter(command, "TVacReviewMemberId", DbType.Guid, 32);
                var result = base.Add(command, "TVacReviewMemberId");
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
        public bool UpdateTVacReviewMember(BEClient.TVacReviewMember objTVacReviewMember)
        {
            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateTVacReviewMember");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@TVacReviewMemberId", DbType.Guid  , objTVacReviewMember.TVacReviewMemberId);
                Database.AddInParameter(command, "@TVacId", DbType.Guid, objTVacReviewMember.TVacId);
                Database.AddInParameter(command, "@TVacRndId", DbType.Guid, objTVacReviewMember.RndTypeId);
                Database.AddInParameter(command, "@UserId", DbType.Guid, objTVacReviewMember.UserId);
                Database.AddInParameter(command, "@Weight", DbType.Int32, objTVacReviewMember.Weight);
                Database.AddInParameter(command, "@CanPromote", DbType.Boolean, objTVacReviewMember.CanPromote);
                Database.AddInParameter(command, "@CanMakeOffers", DbType.Boolean, objTVacReviewMember.CanMakeOffers);
                Database.AddInParameter(command, "@CanEditOffers", DbType.Boolean, objTVacReviewMember.CanEditOffers);
                Database.AddInParameter(command, "@IsActive", DbType.Boolean,true);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, objTVacReviewMember.IsDelete);
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

        public BEClient.TVacReviewMember GetTVacReviewMemberById(Guid TVacReviewMemberId,Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetTVacReviewMemberById");
                Database.AddInParameter(command, "@TVacReviewMemberId", DbType.Guid, TVacReviewMemberId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.FindOne(command, new GetTVacReviewMemberByIdFactory());
            }
            catch
            {
                throw;
            }
        }
        public BEClient.TVacReviewMember GetDivisonByUserId(Guid UserId, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetDivisionByUserId");

                Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);

                return base.FindOne(command, new GetDivisionNameByUserIdIdFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<BEClient.TVacReviewMember> GetAllReviewersByTRoundId(Guid TRoundId, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetTAllReviewersByRoundId");

                Database.AddInParameter(command, "@TRoundId", DbType.Guid, TRoundId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);

                return base.Find(command, new GetTVacReviewMemberByIdFactory(), false);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<BEClient.TVacReviewMember> GetTVacReviewMemberByTVacId(Guid TVacId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetTVacReviewMemberByTVacId");
                Database.AddInParameter(command, "@TVacId", DbType.Guid, TVacId);
                return base.Find(command, new GetTVacReviewMemberByIdFactory());   
            }
            catch
            {
                throw;
            }
        }
        internal class GetDivisionNameByUserIdIdFactory : IDomainObjectFactory<BEClient.TVacReviewMember>
        {
            public BEClient.TVacReviewMember Construct(IDataReader reader)
            {
                BEClient.TVacReviewMember objVacTReviewers = new BEClient.TVacReviewMember();
                objVacTReviewers.Title = HelperMethods.GetString(reader, "LocalName");
                objVacTReviewers.UserName = HelperMethods.GetString(reader, "UserName");
                return objVacTReviewers;
            }
        }


        internal class GetTVacReviewMemberByIdFactory : IDomainObjectFactory<BEClient.TVacReviewMember>
        {
            public BEClient.TVacReviewMember Construct(IDataReader reader)
            {
                BEClient.TVacReviewMember TVacReviewMember = new BEClient.TVacReviewMember();

                TVacReviewMember.TVacReviewMemberId = HelperMethods.GetGuid(reader, "TVacReviewMemberId");
                TVacReviewMember.UserId = HelperMethods.GetGuid(reader, "UserId");
                TVacReviewMember.TVacId= HelperMethods.GetGuid(reader, "TVacId");
                TVacReviewMember.RndTypeId = HelperMethods.GetGuid(reader, "TVacRndId");
                TVacReviewMember.Weight = HelperMethods.GetInt32(reader, "Weight");
                TVacReviewMember.CanPromote = HelperMethods.GetBoolean(reader, "CanPromote");
                TVacReviewMember.Title = HelperMethods.GetString(reader, "LocalName");
                TVacReviewMember.UserName = HelperMethods.GetString(reader, "UserName");
                TVacReviewMember.CanMakeOffers = HelperMethods.GetBoolean(reader, "CanMakeOffers");
                TVacReviewMember.CanEditOffers = HelperMethods.GetBoolean(reader, "CanEditOffers");
                return TVacReviewMember;
            }
        }


        public bool Delete(Guid TVacReviewMemberId)
        {
            try
            {
                bool reResult = false;
                DbCommand command = Database.GetStoredProcCommand("DeleteTVacRevByVacRevId");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@TVacReviewMemberId", DbType.Guid, TVacReviewMemberId);
                var result = base.Save(command);
                if (result != null)
                {
                    reResult = true;
                }
                return reResult;
            }
            catch
            {
                throw;
            }
        }
    }
}