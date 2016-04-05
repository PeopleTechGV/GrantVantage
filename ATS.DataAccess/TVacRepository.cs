using ATS.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;

namespace ATS.DataAccess
{
    public class TVacRepository : Repository<BEClient.TVac>
    {
        public TVacRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public Guid AddTVac(BEClient.TVac tvac)
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
                DbCommand command = Database.GetStoredProcCommand("InsertTvac");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@Name", DbType.String, tvac.Name);
                Database.AddInParameter(command, "@UserId", DbType.Guid, base.CurrentUser.UserId);
                Database.AddInParameter(command, "@DivisionId", DbType.Guid, tvac.DivisionId);
                Database.AddInParameter(command, "@PositionTypeId", DbType.Guid, tvac.PositionTypeId);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, tvac.IsDelete);
                Database.AddInParameter(command, "@JobType", DbType.Int32, tvac.JobType);
                Database.AddInParameter(command, "@HoursPerWeek", DbType.Int32, tvac.HoursPerWeek);
                Database.AddInParameter(command, "@EmploymentType", DbType.Int32, tvac.EmploymentType);
                if (tvac.Location == Guid.Empty)
                {
                    Database.AddInParameter(command, "@Location", DbType.Guid, DBNull.Value);
                }
                else
                {
                    Database.AddInParameter(command, "@Location", DbType.Guid, tvac.Location);
                }
                Database.AddInParameter(command, "@TotalPositions", DbType.Int32, tvac.TotalPositions);
                Database.AddInParameter(command, "@RemainingPositions", DbType.Int32, tvac.RemainingPositions);
                Database.AddInParameter(command, "@FeaturedOnWeb", DbType.Boolean, tvac.FeaturedOnWeb);
                Database.AddOutParameter(command, "TVacId", DbType.Guid, 16);
                var result = base.Add(command, "TVacId");
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

        public Guid AddGrantTVac(BEClient.TVac tvac)
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
                DbCommand command = Database.GetStoredProcCommand("InsertTvac");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@Name", DbType.String, tvac.Name);
                Database.AddInParameter(command, "@UserId", DbType.Guid, base.CurrentUser.UserId);
                Database.AddInParameter(command, "@DivisionId", DbType.Guid, tvac.DivisionId);
                Database.AddInParameter(command, "@PositionTypeId", DbType.Guid, tvac.PositionTypeId);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, tvac.IsDelete);
                Database.AddInParameter(command, "@JobType", DbType.Int32, tvac.JobType);
                Database.AddInParameter(command, "@HoursPerWeek", DbType.Int32, tvac.HoursPerWeek);
                Database.AddInParameter(command, "@EmploymentType", DbType.Int32, tvac.EmploymentType);
                if (tvac.Location == Guid.Empty)
                {
                    Database.AddInParameter(command, "@Location", DbType.Guid, DBNull.Value);
                }
                else
                {
                    Database.AddInParameter(command, "@Location", DbType.Guid, tvac.Location);
                }
                Database.AddInParameter(command, "@TotalPositions", DbType.Int32, tvac.TotalPositions);
                Database.AddInParameter(command, "@RemainingPositions", DbType.Int32, tvac.RemainingPositions);
                Database.AddInParameter(command, "@FeaturedOnWeb", DbType.Boolean, tvac.FeaturedOnWeb);
                Database.AddInParameter(command, "@ActivityCode", DbType.String, tvac.ActivityCode);
                Database.AddInParameter(command, "@AnnouncementType", DbType.String, tvac.AnnouncementType);
                Database.AddInParameter(command, "@FundingOpptyNumber", DbType.String, tvac.FundingOpptyNumber);
                Database.AddInParameter(command, "@ProgramOfficer", DbType.Guid, tvac.ProgramOfficer);
                Database.AddInParameter(command, "@CashMatchRequirement", DbType.Decimal, tvac.CashMatchRequirement);
                Database.AddInParameter(command, "@AnnouncementDate", DbType.DateTime, tvac.AnnouncementDate);
                Database.AddInParameter(command, "@OpenDate", DbType.DateTime, tvac.OpenDate);
                Database.AddInParameter(command, "@ApplicationDueDate", DbType.DateTime, tvac.ApplicationDueDate);
                Database.AddInParameter(command, "@ExpirationDate", DbType.DateTime, tvac.ExpirationDate);
                Database.AddInParameter(command, "@FundAmount", DbType.Decimal, tvac.FundAmount);
                Database.AddOutParameter(command, "TVacId", DbType.Guid, 16);
                var result = base.Add(command, "TVacId");
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

        public bool DeleteTVac(Guid TVacId)
        {
            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("DeleteTVac");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@TVacId", DbType.Guid, TVacId);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, false);

                var result = base.Remove(command);

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

        public bool UpdateTVac(BEClient.TVac tvac)
        {
            try
            {

                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateTVAc");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@TVacId", DbType.Guid, tvac.TVacId);
                Database.AddInParameter(command, "@Name", DbType.String, tvac.Name);

                Database.AddInParameter(command, "@UserId", DbType.Guid, base.CurrentUser.UserId);

                Database.AddInParameter(command, "@DivisionId", DbType.Guid, tvac.DivisionId);

                Database.AddInParameter(command, "@PositionTypeId", DbType.Guid, tvac.PositionTypeId);

                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, tvac.IsDelete);

                //************ Change request **********************

                Database.AddInParameter(command, "@JobType", DbType.Int32, tvac.JobType);
                Database.AddInParameter(command, "@HoursPerWeek", DbType.Int32, tvac.HoursPerWeek);
                Database.AddInParameter(command, "@EmploymentType", DbType.Int32, tvac.EmploymentType);
                Database.AddInParameter(command, "@Location", DbType.Guid, tvac.Location);
                Database.AddInParameter(command, "@TotalPositions", DbType.Int32, tvac.TotalPositions);
                Database.AddInParameter(command, "@RemainingPositions", DbType.Int32, tvac.RemainingPositions);
                Database.AddInParameter(command, "@FeaturedOnWeb", DbType.Boolean, tvac.FeaturedOnWeb);

                //*********************************

                var result = base.Save(command);

                if (result != null)
                {
                    bool.TryParse(result.ToString(), out reREsult);
                }


                return reREsult;
            }

            catch
            {
                this.RollbackTransaction();
                throw;
            }
        }

        public bool UpdateGrantTVac(BEClient.TVac tvac)
        {
            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateTVAc");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@TVacId", DbType.Guid, tvac.TVacId);
                Database.AddInParameter(command, "@Name", DbType.String, tvac.Name);
                Database.AddInParameter(command, "@UserId", DbType.Guid, base.CurrentUser.UserId);
                Database.AddInParameter(command, "@DivisionId", DbType.Guid, tvac.DivisionId);
                Database.AddInParameter(command, "@PositionTypeId", DbType.Guid, tvac.PositionTypeId);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, tvac.IsDelete);
                Database.AddInParameter(command, "@JobType", DbType.Int32, tvac.JobType);
                Database.AddInParameter(command, "@HoursPerWeek", DbType.Int32, tvac.HoursPerWeek);
                Database.AddInParameter(command, "@EmploymentType", DbType.Int32, tvac.EmploymentType);
                Database.AddInParameter(command, "@Location", DbType.Guid, tvac.Location);
                Database.AddInParameter(command, "@TotalPositions", DbType.Int32, tvac.TotalPositions);
                Database.AddInParameter(command, "@RemainingPositions", DbType.Int32, tvac.RemainingPositions);
                Database.AddInParameter(command, "@FeaturedOnWeb", DbType.Boolean, tvac.FeaturedOnWeb);
                Database.AddInParameter(command, "@ActivityCode", DbType.String, tvac.ActivityCode);
                Database.AddInParameter(command, "@AnnouncementType", DbType.String, tvac.AnnouncementType);
                Database.AddInParameter(command, "@FundingOpptyNumber", DbType.String, tvac.FundingOpptyNumber);
                Database.AddInParameter(command, "@ProgramOfficer", DbType.Guid, tvac.ProgramOfficer);
                Database.AddInParameter(command, "@CashMatchRequirement", DbType.Decimal, tvac.CashMatchRequirement);
                Database.AddInParameter(command, "@AnnouncementDate", DbType.DateTime, tvac.AnnouncementDate);
                Database.AddInParameter(command, "@OpenDate", DbType.DateTime, tvac.OpenDate);
                Database.AddInParameter(command, "@ApplicationDueDate", DbType.DateTime, tvac.ApplicationDueDate);
                Database.AddInParameter(command, "@ExpirationDate", DbType.DateTime, tvac.ExpirationDate);
                Database.AddInParameter(command, "@FundAmount", DbType.Decimal, tvac.FundAmount);
                var result = base.Save(command);
                if (result != null)
                {
                    bool.TryParse(result.ToString(), out reREsult);
                }
                return reREsult;
            }
            catch
            {
                this.RollbackTransaction();
                throw;
            }
        }

        public List<BEClient.TVac> GetAllTVac(Guid LanguageId, String DivisionList)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllTVac");
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                if (!String.IsNullOrEmpty(DivisionList))
                    Database.AddInParameter(command, "@DivisionId", DbType.String, DivisionList);

                return base.Find(command, new GetAllTVacFactory());
            }
            catch
            {
                throw;
            }
        }

        public BEClient.TVac GetRecordByrecordId(Guid TVacId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetTVacById");
                Database.AddInParameter(command, "@TVacId", DbType.Guid, TVacId);
                return base.FindOne(command, new GetRecordByRecordIdFactory());
            }
            catch
            {
                throw;
            }
        }

        public BEClient.TVac GetAllTVacDetailsByTVacId(Guid TVacId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllTVacDetailsByTVacId");
                Database.AddInParameter(command, "@TVacId", DbType.Guid, TVacId);
                return base.FindOne(command, new GetAllTVacDetailsByTVacIdFactory());
            }
            catch
            {
                throw;
            }
        }

        internal class GetAllTVacDetailsByTVacIdFactory : IDomainObjectFactory<BEClient.TVac>
        {
            public BEClient.TVac Construct(IDataReader reader)
            {
                BEClient.TVac TVacancy = new BEClient.TVac();
                TVacancy.TVacId = HelperMethods.GetGuid(reader, "VacancyId");
                TVacancy.ClientId = HelperMethods.GetGuid(reader, "ClientId");
                TVacancy.LanguageId = HelperMethods.GetGuid(reader, "LanguageId");
                TVacancy.PositionTypeId = HelperMethods.GetGuid(reader, "PositionTypeId");
                TVacancy.Name = HelperMethods.GetString(reader, "Name");
                TVacancy.JobType = HelperMethods.GetInt32(reader, "JobType");
                TVacancy.HoursPerWeek = HelperMethods.GetInt32(reader, "HoursPerWeek");
                TVacancy.EmploymentType = HelperMethods.GetInt32(reader, "EmploymentType");
                TVacancy.DivisionId = HelperMethods.GetGuid(reader, "DivisionId");
                TVacancy.Location = HelperMethods.GetGuid(reader, "Location");
                TVacancy.TotalPositions = HelperMethods.GetInt32(reader, "TotalPositions");
                TVacancy.RemainingPositions = HelperMethods.GetInt32(reader, "RemainingPositions");
                TVacancy.FeaturedOnWeb = HelperMethods.GetBoolean(reader, "FeaturedOnWeb");
                TVacancy.JobDescription = HelperMethods.GetString(reader, "JobDescription");
                TVacancy.ShowOnWebJobDescription = HelperMethods.GetBoolean(reader, "ShowOnWebJobDescription");
                TVacancy.DutiesAndResponsibilities = HelperMethods.GetString(reader, "DutiesAndresponsibilities");
                TVacancy.ShowOnWebDuties = HelperMethods.GetBoolean(reader, "ShowOnWebDuties");
                TVacancy.SkillsAndQualification = HelperMethods.GetString(reader, "SkillsAndQualification");
                TVacancy.ShowOnWebSkills = HelperMethods.GetBoolean(reader, "ShowOnWebSkills");
                TVacancy.Benefits = HelperMethods.GetString(reader, "Benefits");
                TVacancy.ShowOnWebBenefits = HelperMethods.GetBoolean(reader, "ShowOnWebBenefits");
                TVacancy.SalaryMin = HelperMethods.GetDecimal(reader, "SalaryMin");
                TVacancy.SalaryMax = HelperMethods.GetDecimal(reader, "SalaryMax");
                TVacancy.ShowOnWebSal = HelperMethods.GetBoolean(reader, "ShowOnWebSal");
                TVacancy.HourlyMin = HelperMethods.GetDecimal(reader, "HourlyMin");
                TVacancy.HourlyMax = HelperMethods.GetDecimal(reader, "HourlyMax");
                TVacancy.ShowonWebHour = HelperMethods.GetBoolean(reader, "ShowonWebHour");
                TVacancy.Commission = HelperMethods.GetString(reader, "Commission");
                TVacancy.ShowOnWebCommission = HelperMethods.GetBoolean(reader, "ShowOnWebCommission");
                TVacancy.BonusPotential = HelperMethods.GetString(reader, "BonusPotential");
                TVacancy.ShowOnWebBonus = HelperMethods.GetBoolean(reader, "ShowOnWebBonus");
                TVacancy.CreatedBy = HelperMethods.GetGuid(reader, "CreatedBy");
                TVacancy.IsDelete = HelperMethods.GetBoolean(reader, "IsDelete");
                TVacancy.JobTypeText = HelperMethods.GetString(reader, "JobTypeText");
                TVacancy.EmploymentTypeText = HelperMethods.GetString(reader, "EmploymentTypeText");
                TVacancy.PositionTypeName = HelperMethods.GetString(reader, "PositionTypeName");
                TVacancy.ActivityCode = HelperMethods.GetString(reader, "ActivityCode");
                TVacancy.AnnouncementType = HelperMethods.GetString(reader, "AnnouncementType");
                TVacancy.FundingOpptyNumber = HelperMethods.GetString(reader, "FundingOpptyNumber");
                TVacancy.ProgramOfficer = HelperMethods.GetGuid(reader, "ProgramOfficer");
                TVacancy.CashMatchRequirement = HelperMethods.GetDecimal(reader, "CashMatchRequirement");
                TVacancy.AnnouncementDate = HelperMethods.GetDateTime(reader, "AnnouncementDate");
                TVacancy.OpenDate = HelperMethods.GetDateTime(reader, "OpenDate");
                TVacancy.ApplicationDueDate = HelperMethods.GetDateTime(reader, "ApplicationDueDate");
                TVacancy.ExpirationDate = HelperMethods.GetDateTime(reader, "ExpirationDate");
                TVacancy.FundAmount = HelperMethods.GetDecimal(reader, "FundAmount");
                TVacancy.ApplicationInstruction = HelperMethods.GetString(reader, "ApplicationInstruction");
                TVacancy.ShowOnWebAppInstruction = HelperMethods.GetBoolean(reader, "ShowOnWebAppInstruction");
                TVacancy.ShowOnWebAppInstructionDoc = HelperMethods.GetBoolean(reader, "ShowOnWebAppInstructionDoc");
                return TVacancy;
            }
        }

        public List<BEClient.TVac> GetTVacByPosIdAndDivId(Guid? VacancyId, Guid LanguageId, string UserDivisionIdList)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetTVacByPosIdAndDivId");
                Database.AddInParameter(command, "@VacancyId", DbType.Guid, VacancyId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                if (UserDivisionIdList != string.Empty)
                    Database.AddInParameter(command, "@UserDivisionIdList", DbType.String, UserDivisionIdList);
                else
                    Database.AddInParameter(command, "@UserDivisionIdList", DbType.String, DBNull.Value);
                return base.Find(command, new GetTVacByPosIdAndDivIdFactory());
            }
            catch
            {
                throw;
            }
        }

        public bool IsRecordExists(Guid TVacId)
        {
            try
            {
                bool reResult = false;
                DbCommand command = Database.GetStoredProcCommand("TVac_IsRecordExists");
                Database.AddInParameter(command, "@TVacId", DbType.Guid, TVacId);
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

        internal class GetTVacByPosIdAndDivIdFactory : IDomainObjectFactory<BEClient.TVac>
        {
            public BEClient.TVac Construct(IDataReader reader)
            {
                BEClient.TVac TVac = new BEClient.TVac();
                TVac.TVacId = HelperMethods.GetGuid(reader, "TVacId");
                TVac.Name = HelperMethods.GetString(reader, "Name");
                return TVac;
            }
        }

        internal class GetAllTVacFactory : IDomainObjectFactory<BEClient.TVac>
        {
            public BEClient.TVac Construct(IDataReader reader)
            {
                BEClient.TVac tvac = new BEClient.TVac();
                tvac.TVacId = HelperMethods.GetGuid(reader, "TVacId");
                tvac.Name = HelperMethods.GetString(reader, "Name");
                tvac.UserId = HelperMethods.GetGuid(reader, "UserId");
                tvac.DivisionId = HelperMethods.GetGuid(reader, "DivisionId");
                tvac.PositionTypeId = HelperMethods.GetGuid(reader, "PositionTypeId");
                tvac.RndCount = HelperMethods.GetInt32(reader, "RndCount");
                tvac.JobType = HelperMethods.GetInt32(reader, "JobType");
                tvac.HoursPerWeek = HelperMethods.GetInt32(reader, "HoursPerWeek");
                tvac.EmploymentType = HelperMethods.GetInt32(reader, "EmploymentType");
                tvac.Location = HelperMethods.GetGuid(reader, "Location");
                tvac.TotalPositions = HelperMethods.GetInt32(reader, "TotalPositions");
                tvac.RemainingPositions = HelperMethods.GetInt32(reader, "RemainingPositions");
                tvac.FeaturedOnWeb = HelperMethods.GetBoolean(reader, "FeaturedOnWeb");

                return tvac;
            }
        }

        internal class GetRecordByRecordIdFactory : IDomainObjectFactory<BEClient.TVac>
        {
            public BEClient.TVac Construct(IDataReader reader)
            {
                BEClient.TVac tvac = new BEClient.TVac();
                tvac.TVacId = HelperMethods.GetGuid(reader, "TVacId");
                tvac.Name = HelperMethods.GetString(reader, "Name");
                tvac.UserId = HelperMethods.GetGuid(reader, "UserId");
                tvac.DivisionId = HelperMethods.GetGuid(reader, "DivisionId");
                tvac.PositionTypeId = HelperMethods.GetGuid(reader, "PositionTypeId");
                tvac.JobType = HelperMethods.GetInt32(reader, "JobType");
                tvac.HoursPerWeek = HelperMethods.GetInt32(reader, "HoursPerWeek");
                tvac.EmploymentType = HelperMethods.GetInt32(reader, "EmploymentType");
                tvac.Location = HelperMethods.GetGuid(reader, "Location");
                tvac.TotalPositions = HelperMethods.GetInt32(reader, "TotalPositions");
                tvac.RemainingPositions = HelperMethods.GetInt32(reader, "RemainingPositions");
                tvac.FeaturedOnWeb = HelperMethods.GetBoolean(reader, "FeaturedOnWeb");
                tvac.RndCount = HelperMethods.GetInt32(reader, "RoundCount");
                tvac.ActivityCode = HelperMethods.GetString(reader, "ActivityCode");
                tvac.AnnouncementType = HelperMethods.GetString(reader, "AnnouncementType");
                tvac.FundingOpptyNumber = HelperMethods.GetString(reader, "FundingOpptyNumber");
                tvac.ProgramOfficer = HelperMethods.GetGuid(reader, "ProgramOfficer");
                tvac.CashMatchRequirement = HelperMethods.GetDecimal(reader, "CashMatchRequirement");
                tvac.AnnouncementDate = HelperMethods.GetDateTime(reader, "AnnouncementDate");
                tvac.OpenDate = HelperMethods.GetDateTime(reader, "OpenDate");
                tvac.ApplicationDueDate = HelperMethods.GetDateTime(reader, "ApplicationDueDate");
                tvac.ExpirationDate = HelperMethods.GetDateTime(reader, "ExpirationDate");
                tvac.FundAmount = HelperMethods.GetDecimal(reader, "FundAmount");
                return tvac;
            }
        }

        public bool UpdateJobDescriptionByVacancyId(TVac TVacancy)
        {
            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateJobDescriptionByTVacId");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@TVacId", DbType.Guid, TVacancy.TVacId);

                Database.AddInParameter(command, "@JobDescription", DbType.String, TVacancy.JobDescription);

                Database.AddInParameter(command, "@ShowOnWebJobDescription", DbType.Boolean, TVacancy.ShowOnWebJobDescription);

                Database.AddInParameter(command, "@SkillsAndQualification", DbType.String, TVacancy.SkillsAndQualification);

                Database.AddInParameter(command, "@ShowOnWebSkills", DbType.Boolean, TVacancy.ShowOnWebSkills);

                Database.AddInParameter(command, "@DutiesAndResponsibilities", DbType.String, TVacancy.DutiesAndResponsibilities);

                Database.AddInParameter(command, "@ShowOnWebDuties", DbType.Boolean, TVacancy.ShowOnWebDuties);

                Database.AddInParameter(command, "@Benefits", DbType.String, TVacancy.Benefits);

                Database.AddInParameter(command, "@ShowOnWebBenefits", DbType.Boolean, TVacancy.ShowOnWebBenefits);

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

        public bool UpdateCompensationInfoByVacancyId(TVac TVacancy)
        {
            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateCompensationInfoByTVacId");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@TVacId", DbType.Guid, TVacancy.TVacId);
                Database.AddInParameter(command, "@SalaryMin", DbType.Decimal, TVacancy.SalaryMin);
                Database.AddInParameter(command, "@SalaryMax", DbType.Decimal, TVacancy.SalaryMax);
                Database.AddInParameter(command, "@ShowOnWebSal", DbType.Boolean, TVacancy.ShowOnWebSal);
                Database.AddInParameter(command, "@HourlyMin", DbType.Decimal, TVacancy.HourlyMin);
                Database.AddInParameter(command, "@HourlyMax", DbType.Decimal, TVacancy.HourlyMax);
                Database.AddInParameter(command, "@ShowonWebHour", DbType.Boolean, TVacancy.ShowonWebHour);
                Database.AddInParameter(command, "@Commission", DbType.String, TVacancy.Commission);
                Database.AddInParameter(command, "@ShowOnWebCommission", DbType.Boolean, TVacancy.ShowOnWebCommission);
                Database.AddInParameter(command, "@BonusPotential", DbType.String, TVacancy.BonusPotential);
                Database.AddInParameter(command, "@ShowOnWebBonus", DbType.Boolean, TVacancy.ShowOnWebBonus);
                Database.AddInParameter(command, "@ApplicationInstruction", DbType.String, TVacancy.ApplicationInstruction);
                Database.AddInParameter(command, "@ShowOnWebAppInstruction", DbType.Boolean, TVacancy.ShowOnWebAppInstruction);
                Database.AddInParameter(command, "@ShowOnWebAppInstructionDoc", DbType.Boolean, TVacancy.ShowOnWebAppInstructionDoc);
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

        public TVac GetJobDescriptionByTVacId(Guid pRecordId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetJobDescriptionByTVacId");
                Database.AddInParameter(command, "@TVacId", DbType.Guid, pRecordId);
                //Database.AddInParameter(command, "@LanguageId", DbType.Guid, languageId);

                return base.FindOne(command, new GetTVacJobDescriptionByIdFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public TVac GetCompensationInfoByTVacId(Guid pRecordId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetCompensationInfoByTVacId");
                Database.AddInParameter(command, "@TVacId", DbType.Guid, pRecordId);
                //Database.AddInParameter(command, "@LanguageId", DbType.Guid, languageId);

                return base.FindOne(command, new GetTVacCompensationInfoByIdFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal class GetTVacJobDescriptionByIdFactory : IDomainObjectFactory<TVac>
        {
            public TVac Construct(IDataReader reader)
            {
                TVac objTVacancy = new TVac();

                objTVacancy.TVacId = HelperMethods.GetGuid(reader, "TVacId");
                objTVacancy.JobDescription = HelperMethods.GetString(reader, "JobDescription");
                objTVacancy.ShowOnWebJobDescription = HelperMethods.GetBoolean(reader, "ShowOnWebJobDescription");
                objTVacancy.DutiesAndResponsibilities = HelperMethods.GetString(reader, "DutiesAndResponsibilities");
                objTVacancy.ShowOnWebDuties = HelperMethods.GetBoolean(reader, "ShowOnWebDuties");
                objTVacancy.SkillsAndQualification = HelperMethods.GetString(reader, "SkillsAndQualification");
                objTVacancy.ShowOnWebSkills = HelperMethods.GetBoolean(reader, "ShowOnWebSkills");
                objTVacancy.Benefits = HelperMethods.GetString(reader, "Benefits");
                objTVacancy.ShowOnWebBenefits = HelperMethods.GetBoolean(reader, "ShowOnWebBenefits");
                objTVacancy.DivisionId = HelperMethods.GetGuid(reader, "DivisionId");
                return objTVacancy;
            }
        }

        internal class GetTVacCompensationInfoByIdFactory : IDomainObjectFactory<TVac>
        {
            public TVac Construct(IDataReader reader)
            {
                TVac objTVacancy = new TVac();
                objTVacancy.TVacId = HelperMethods.GetGuid(reader, "TVacId");
                objTVacancy.SalaryMin = HelperMethods.GetDecimal(reader, "SalaryMin");
                objTVacancy.SalaryMax = HelperMethods.GetDecimal(reader, "SalaryMax");
                objTVacancy.ShowOnWebSal = HelperMethods.GetBoolean(reader, "ShowOnWebSal");
                objTVacancy.HourlyMin = HelperMethods.GetDecimal(reader, "HourlyMin");
                objTVacancy.HourlyMax = HelperMethods.GetDecimal(reader, "HourlyMax");
                objTVacancy.ShowonWebHour = HelperMethods.GetBoolean(reader, "ShowonWebHour");
                objTVacancy.Commission = HelperMethods.GetString(reader, "Commission");
                objTVacancy.ShowOnWebCommission = HelperMethods.GetBoolean(reader, "ShowOnWebCommission");
                objTVacancy.BonusPotential = HelperMethods.GetString(reader, "BonusPotential");
                objTVacancy.ShowOnWebBonus = HelperMethods.GetBoolean(reader, "ShowOnWebBonus");
                objTVacancy.DivisionId = HelperMethods.GetGuid(reader, "DivisionId");
                objTVacancy.ApplicationInstruction = HelperMethods.GetString(reader, "ApplicationInstruction");
                objTVacancy.ShowOnWebAppInstruction = HelperMethods.GetBoolean(reader, "ShowOnWebAppInstruction");
                objTVacancy.ShowOnWebAppInstructionDoc = HelperMethods.GetBoolean(reader, "ShowOnWebAppInstructionDoc");
                return objTVacancy;
            }
        }
    }
}