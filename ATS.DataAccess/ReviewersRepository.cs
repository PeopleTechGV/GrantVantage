using ATS.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace ATS.DataAccess
{
    public class ReviewersRepository : Repository<Reviewers>
    {
        public ReviewersRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public Guid InsertVacReviewMember(Reviewers objReviewers, int imilli = 0)
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
                DbCommand command = Database.GetStoredProcCommand("InsertVacReviewMember");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@VacRndId", DbType.Guid, objReviewers.RndTypeId);

                Database.AddInParameter(command, "@UserId", DbType.Guid, objReviewers.UserId);

                Database.AddInParameter(command, "@Weight", DbType.Int32, objReviewers.Weight);

                Database.AddInParameter(command, "@CanPromote", DbType.Boolean, objReviewers.CanPromote);

                Database.AddInParameter(command, "@CanMakeOffers", DbType.Boolean, objReviewers.CanMakeOffers);

                Database.AddInParameter(command, "@CanEditOffers", DbType.Boolean, objReviewers.CanEditOffers);

                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, objReviewers.ApplicationId);

                Database.AddInParameter(command, "@IsAssignedReviewer", DbType.Boolean, objReviewers.IsAssignedReviewer);

                if (objReviewers.RndAttribute == (int)ATS.BusinessEntity.RndAttrNo.ApplicationRound || objReviewers.RndAttribute == (int)ATS.BusinessEntity.RndAttrNo.CandidateSurvey)
                {
                    Database.AddInParameter(command, "@IsScreeningRound", DbType.Boolean, true);
                }

                Database.AddOutParameter(command, "VacReviewMemberId", DbType.Guid, 16);

                var result = base.Add(command, "VacReviewMemberId", true, imilli);
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



        public bool Update(Reviewers objReviewers)
        {
            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateVacReviewMemberByVacReviewMemberId");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@VacReviewMemberId", DbType.Guid, objReviewers.VacancyReviewMemberId);
                Database.AddInParameter(command, "@UserId", DbType.Guid, objReviewers.UserId);
                Database.AddInParameter(command, "@Weight", DbType.Int32, objReviewers.Weight);
                Database.AddInParameter(command, "@CanPromote", DbType.Boolean, objReviewers.CanPromote);
                Database.AddInParameter(command, "@CanMakeOffers", DbType.Boolean, objReviewers.CanMakeOffers);
                Database.AddInParameter(command, "@CanEditOffers", DbType.Boolean, objReviewers.CanEditOffers);

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

        public bool Delete(Guid VacReviewMemberId)
        {
            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("DeleteVacReviewMemberByVacReviewMemberId");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@VacReviewMemberId", DbType.Guid, VacReviewMemberId);
                var result = base.Save(command);
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

        public List<Reviewers> GetAllReviewersByRoundId(Guid RoundId, Guid LanguageId, Guid? ApplicationId = null, Guid? VacQueId = null)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllReviewersByRoundId");

                Database.AddInParameter(command, "@RoundId", DbType.Guid, RoundId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                if (VacQueId != null)
                {
                    Database.AddInParameter(command, "@VacQueId", DbType.Guid, (Guid)VacQueId);
                    Database.AddInParameter(command, "@ApplicationId", DbType.Guid, (Guid)ApplicationId);
                }

                return base.Find(command, new GetAllReviewersByRoundIdFactory(), false);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Reviewers GetDivisonByUserId(Guid UserId, Guid LanguageId)
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

        public Reviewers GetRecordByRecordId(Guid recordIdId, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetVacRevMemberById");

                Database.AddInParameter(command, "@VacReviewMemberId", DbType.Guid, recordIdId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);


                return base.FindOne(command, new GetRecordByrecordIdFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Guid GetVacReviewerMemberByVacRndAndUser(Guid VacRndId, Guid UserId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetVacReviewerMemberByVacRndAndUser");

                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);
                string VacReviewMemberId = FindScalarValue(command);
                return new Guid(VacReviewMemberId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Guid GetUserIdByVacRMId(Guid VacRMId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetUserIdByVacRmId");

                Database.AddInParameter(command, "@VacRMId", DbType.Guid, VacRMId);

                string UserId = FindScalarValue(command);
                if (UserId == string.Empty)
                {
                    return Guid.Empty;
                }
                else
                {
                    return new Guid(UserId);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<Reviewers> GetReviewersByVacRndId(Guid VacRndId)
        {
            List<Reviewers> lstReviewers = new List<Reviewers>();
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetReviewerByVacRndId");
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                return base.Find(command, new GetReviewersByVacRndIdFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteVacReviewMemberById(Guid VacReviewMemberId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("DeleteVacReviewMemberById");
                Database.AddInParameter(command, "@VacReviewMemberId", DbType.Guid, VacReviewMemberId);
                var result = base.FindScalarValue(command);
                return true;
            }
            catch
            {
                return false;
            }
        }

        internal class GetReviewersByVacRndIdFactory : IDomainObjectFactory<Reviewers>
        {
            public Reviewers Construct(IDataReader reader)
            {
                Reviewers ObjReviewers = new Reviewers();
                ObjReviewers.VacancyReviewMemberId = HelperMethods.GetGuid(reader, "VacReviewMemberId");
                return ObjReviewers;
            }
        }
        internal class GetUnAssignedReviewersByVacRndIdFactory : IDomainObjectFactory<Reviewers>
        {
            public Reviewers Construct(IDataReader reader)
            {
                Reviewers ObjReviewers = new Reviewers();
                ObjReviewers.VacancyReviewMemberId = HelperMethods.GetGuid(reader, "VacReviewMemberId");
                return ObjReviewers;
            }
        }


        public bool AuthorizeRevmemberForPromoteCandidate(Guid VacRndId, Guid UserId)
        {
            try
            {
                bool reResult = false;
                DbCommand command = Database.GetStoredProcCommand("AuthorizeRevmemberForPromoteCandidate");
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);
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
        public bool IsReviewerForRound(Guid VacRndId, Guid UserId)
        {
            try
            {
                bool reResult = false;
                DbCommand command = Database.GetStoredProcCommand("IsReviewer");
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);
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

        public List<Reviewers> GetUnAssignedReviewers(Guid VacRndId, Guid ApplicationId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetReviewersForApplication");
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                return base.Find(command, new GetUnassignedReviewersIdFactory(), false);
            }
            catch
            {
                throw;
            }
        }

        public Reviewers GetReviewerDetails(Guid UserId, Guid VacRndId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetReviewerDetails");
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);
                return base.FindOne(command, new GetReviewerDetailsfactory(), false);
            }
            catch
            {
                throw;
            }
        }




        internal class GetAllReviewersByRoundIdFactory : IDomainObjectFactory<Reviewers>
        {
            public Reviewers Construct(IDataReader reader)
            {
                Reviewers objReviewers = new Reviewers();

                objReviewers.UserName = HelperMethods.GetString(reader, "Name");
                objReviewers.Title = HelperMethods.GetString(reader, "LocalName");
                objReviewers.Weight = HelperMethods.GetInt32(reader, "Weight");
                objReviewers.CanPromote = HelperMethods.GetBoolean(reader, "CanPromote");
                objReviewers.RndTypeId = HelperMethods.GetGuid(reader, "VacRndId");
                objReviewers.VacancyReviewMemberId = HelperMethods.GetGuid(reader, "VacReviewMemberId");
                objReviewers.Score = HelperMethods.GetFloat(reader, "Score");
                objReviewers.UserId = HelperMethods.GetGuid(reader, "UserId");
                objReviewers.CanMakeOffers = HelperMethods.GetBoolean(reader, "CanMakeOffers");
                objReviewers.CanEditOffers = HelperMethods.GetBoolean(reader, "CanEditOffers");
                objReviewers.IsAssignedReviewer = HelperMethods.GetBoolean(reader, "IsAssignedReviewer");
                objReviewers.AssignedCandidateCount = HelperMethods.GetInt32(reader, "AssignedCandidateCount");
                objReviewers.DivisionId = HelperMethods.GetGuid(reader, "DivisionId");
                return objReviewers;
            }
        }
        internal class GetDivisionNameByUserIdIdFactory : IDomainObjectFactory<Reviewers>
        {
            public Reviewers Construct(IDataReader reader)
            {
                Reviewers objReviewers = new Reviewers();
                objReviewers.Title = HelperMethods.GetString(reader, "LocalName");
                objReviewers.UserName = HelperMethods.GetString(reader, "UserName");
                return objReviewers;
            }
        }

        internal class GetRecordByrecordIdFactory : IDomainObjectFactory<Reviewers>
        {
            public Reviewers Construct(IDataReader reader)
            {
                Reviewers objReviewers = new Reviewers();

                objReviewers.VacancyReviewMemberId = HelperMethods.GetGuid(reader, "VacReviewMemberId");
                objReviewers.UserId = HelperMethods.GetGuid(reader, "UserId");
                objReviewers.RndTypeId = HelperMethods.GetGuid(reader, "VacRndId");
                objReviewers.Weight = HelperMethods.GetInt32(reader, "Weight");
                objReviewers.CanPromote = HelperMethods.GetBoolean(reader, "CanPromote");
                objReviewers.Title = HelperMethods.GetString(reader, "LocalName");
                objReviewers.UserName = HelperMethods.GetString(reader, "UserName");
                objReviewers.CanMakeOffers = HelperMethods.GetBoolean(reader, "CanMakeOffers");
                objReviewers.CanEditOffers = HelperMethods.GetBoolean(reader, "CanEditOffers");
                return objReviewers;
            }
        }

        internal class GetUnassignedReviewersIdFactory : IDomainObjectFactory<Reviewers>
        {
            public Reviewers Construct(IDataReader reader)
            {
                Reviewers objReviewers = new Reviewers();
                objReviewers.UserId = HelperMethods.GetGuid(reader, "UserId");
                objReviewers.FullName = HelperMethods.GetString(reader, "FullName");
                objReviewers.UserName = HelperMethods.GetString(reader, "Username");
                objReviewers.RndAttribute = HelperMethods.GetInt32(reader, "RndAttribute");
                objReviewers.JobTitle = HelperMethods.GetString(reader, "JobTitle");
                objReviewers.CandidateName = HelperMethods.GetString(reader, "CandidateName");
                objReviewers.Weight = HelperMethods.GetInt32(reader, "Weight");
                objReviewers.IsAssinedReviewerForOldApp = HelperMethods.GetBoolean(reader, "IsAssignedReviewer");
                objReviewers.CanPromote = HelperMethods.GetBoolean(reader, "CanPromote");
                objReviewers.CanMakeOffers = HelperMethods.GetBoolean(reader, "CanMakeOffers");
                objReviewers.CanEditOffers = HelperMethods.GetBoolean(reader, "CanEditOffers");
                objReviewers.VacancyReviewMemberId = HelperMethods.GetGuid(reader, "VacReviewerMemberId");



                return objReviewers;
            }
        }
        internal class GetReviewerDetailsfactory : IDomainObjectFactory<Reviewers>
        {
            public Reviewers Construct(IDataReader reader)
            {
                Reviewers objReviewers = new Reviewers();
                objReviewers.VacancyReviewMemberId = HelperMethods.GetGuid(reader, "VacReviewMemberId");
                objReviewers.Weight = HelperMethods.GetInt32(reader, "Weight");
                objReviewers.IsAssignedReviewer = HelperMethods.GetBoolean(reader, "IsAssignedReviewer");
                objReviewers.RndAttribute = HelperMethods.GetInt32(reader, "RndAttribute");
                return objReviewers;
            }
        }





    }
}
