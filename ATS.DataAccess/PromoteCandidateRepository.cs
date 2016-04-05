using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;

namespace ATS.DataAccess
{
    public class PromoteCandidateRepository : Repository<BEClient.PromoteCandidate>
    {
        public PromoteCandidateRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public Guid InsertPromoteCandidate(BEClient.PromoteCandidate ObjPC)
        {
            try
            {
                Guid reResult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertPromoteCandidate");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ObjPC.ApplicationId);
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, ObjPC.VacRndId);
                Database.AddInParameter(command, "@IsPromoted", DbType.Boolean, ObjPC.IsPromoted);
                Database.AddInParameter(command, "@IsActive", DbType.Boolean, ObjPC.IsActive);
                Database.AddOutParameter(command, "PromoteCandidateId", DbType.Guid, 16);
                var result = base.Add(command, "PromoteCandidateId");
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

        public Boolean UpdatePromoteCandidate(BEClient.PromoteCandidate ObjPC, string status)
        {
            try
            {
                bool reResult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdatePromoteCandidate");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ObjPC.ApplicationId);
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, ObjPC.VacRndId);
                Database.AddInParameter(command, "@Status", DbType.String, status);
                Database.AddInParameter(command, "UpdatedBy", DbType.Guid, CurrentUser.UserId);
                Database.AddInParameter(command, "UpdatedOn", DbType.DateTime, DateTime.UtcNow);
                var result = base.FindScalarValue(command);
                if (result != null)
                {
                    if (result == "101")
                    {
                        throw new Exception("Can't Promote, As Some Required Document(s) are not supplied by Applicant");

                    }
                    else
                    {
                        reResult = true;
                    }

                }
                return reResult;
            }
            catch
            {
                throw;
            }
        }
        public Guid UpdatePromoteCandidateapp(Guid VacRndId, Guid ApplicationId, Guid VacancyId)
        {
            try
            {
                Guid reResult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("UpdatePromoteCandidateApp");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                Database.AddInParameter(command, "@VacancyId", DbType.Guid, VacancyId);
                Database.AddOutParameter(command, "@newVacRndId", DbType.Guid, 16);

                var result = base.SaveWithoutDuplication(command, "newVacRndId");
                if (result != null)
                {
                    Guid.TryParse(result.ToString(), out reResult);
                    if (reResult == Guid.Empty)
                    {
                        throw new Exception("Can't Promote, As Some Required Document(s) are not supplied by Applicant");
                    }
                }
                return reResult;
            }
            catch
            {
                throw;
            }
        }
        public Guid UpdateDemoteandidateapp(Guid VacRndId, Guid ApplicationId, Guid VacancyId)
        {
            try
            {
                Guid reResult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("UpdateDemoteCandidateApp");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                Database.AddInParameter(command, "@VacancyId", DbType.Guid, VacancyId);
                Database.AddOutParameter(command, "newVacRndId", DbType.Guid, 16);

                var result = base.SaveWithoutDuplication(command, "newVacRndId");
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




        public Guid GetFirstVacRndIdByApplicationId(Guid ApplicationId)
        {
            try
            {
                Guid reResult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("GetFirstVacRndIdByApplicationId");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
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
        public Guid GetNextVacRndIdByApplicationId(Guid ApplicationId)
        {
            try
            {
                Guid reResult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("GetNextVacRndIdByApplicationId");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                string Result = base.FindScalarValue(command);
                reResult = new Guid(Result);
                return reResult;
            }
            catch
            {
                throw;
            }
        }


        public bool IsPromoted(Guid VacRndId, Guid ApplicationId)
        {
            try
            {
                bool reResult = false;
                DbCommand command = Database.GetStoredProcCommand("IsPromoted");
                Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
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
        public bool IsPromotedToInterview(Guid ApplicationId, String RndTypeId)
        {
            try
            {
                bool reResult = false;
                DbCommand command = Database.GetStoredProcCommand("IsPromotedToInterview");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                Database.AddInParameter(command, "@RndTypeId", DbType.String, RndTypeId);
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
    }
}
