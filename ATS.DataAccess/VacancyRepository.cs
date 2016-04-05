using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATS.BusinessEntity;
using System.Data.Common;
using System.Data;

namespace ATS.DataAccess
{
    public class VacancyRepository : Repository<Vacancy>
    {


        #region to do later
        //private String _condition = "where CreatedBy =" ;
        //private String _tablename = "Vacancy";
        //private String _countonfield = "VacancyId";

        #endregion

        public VacancyRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public Guid AddVacancy(Vacancy pVacancy, string fieldValue)
        {
            try
            {
                try
                {
                    this.BeginTransaction();

                    Guid reREsult = Guid.Empty;
                    DbCommand command = Database.GetStoredProcCommand("InsertVacancy");

                    command.CommandTimeout = 100;

                    Database.AddInParameter(command, "@ClientId", DbType.Guid, pVacancy.ClientId);

                    Database.AddInParameter(command, "@PositionTypeId", DbType.Guid, pVacancy.PositionTypeId);

                    Database.AddInParameter(command, "@LanguageId", DbType.Guid, pVacancy.LanguageId);

                    Database.AddInParameter(command, "@JobTitle", DbType.String, pVacancy.JobTitle);

                    Database.AddInParameter(command, "@FieldValue", DbType.String, fieldValue);

                    if (pVacancy.PostedOn.Equals(DateTime.MinValue))
                    {
                        pVacancy.PostedOn = DateTime.UtcNow;
                    }

                    Database.AddInParameter(command, "@PostedOn", DbType.DateTime, pVacancy.PostedOn);

                    Database.AddInParameter(command, "@VacancyState", DbType.Int32, pVacancy.VacancyState);

                    Database.AddInParameter(command, "@PositionID", DbType.String, pVacancy.PositionID);

                    Database.AddInParameter(command, "@VacancyStatusId", DbType.Guid, pVacancy.VacancyStatusId);

                    Database.AddInParameter(command, "@JobType", DbType.Int32, pVacancy.JobType);

                    Database.AddInParameter(command, "@HoursPerWeek", DbType.Int32, pVacancy.HoursPerWeek);


                    Database.AddInParameter(command, "@EmploymentType", DbType.Int32, pVacancy.EmploymentType);

                    Database.AddInParameter(command, "@DivisionId", DbType.Guid, pVacancy.DivisionId);

                    Database.AddInParameter(command, "@Location", DbType.Guid, pVacancy.Location);

                    Database.AddInParameter(command, "@StartDate", DbType.DateTime, pVacancy.StartDate);

                    Database.AddInParameter(command, "@EndDate", DbType.DateTime, pVacancy.EndDate);

                    Database.AddInParameter(command, "@TotalPositions", DbType.Int32, pVacancy.TotalPositions);
                    Database.AddInParameter(command, "@RemainingPositions", DbType.Int32, pVacancy.RemainingPositions);

                    Database.AddInParameter(command, "@ShowOnWeb", DbType.String, pVacancy.ShowOnWeb);

                    Database.AddInParameter(command, "@FeaturedOnWeb", DbType.Boolean, pVacancy.FeaturedOnWeb);

                    Database.AddInParameter(command, "@PositionRequestedBy", DbType.Guid, pVacancy.PositionRequestedBy);

                    Database.AddInParameter(command, "@PositionOwner", DbType.Guid, pVacancy.PositionOwner);

                    Database.AddInParameter(command, "@IsDelete", DbType.Boolean, pVacancy.IsDelete);


                    Database.AddInParameter(command, "@ConfirmVacancyDetails", DbType.Boolean, pVacancy.ConfirmVacancyDetails);

                    Database.AddInParameter(command, "@ConfirmJobDescription", DbType.Boolean, pVacancy.ConfirmJobDescription);

                    Database.AddInParameter(command, "@ConfirmCompensationInfo", DbType.Boolean, pVacancy.ConfirmCompensationInfo);

                    Database.AddInParameter(command, "@ConfirmApplicationReview", DbType.Boolean, pVacancy.ConfirmApplicationreview);

                    Database.AddInParameter(command, "@EmployeeId", DbType.Guid, pVacancy.OnBoardManagerId);

                    Database.AddOutParameter(command, "VacancyId", DbType.Guid, 16);

                    Database.AddOutParameter(command, "@IsError", DbType.Int32, 4);

                    string[] outParams = new string[] { "VacancyId", "@IsError" };

                    var result = base.Add(command, outParams);

                    String errorCode = result[outParams[1]].ToString();

                    if (result != null)
                    {
                        switch (errorCode)
                        {
                            case "101":
                                throw new Exception("Position Id already exists.");
                        }
                        Guid.TryParse(result[outParams[0]].ToString(), out reREsult);
                    }
                    this.CommitTransaction();
                    return reREsult;

                }

                catch
                {
                    this.RollbackTransaction();
                    throw;
                }

            }
            catch
            {
                throw;
            }
        }

        public Guid AddGrantVacancy(Vacancy pVacancy, string fieldValue)
        {
            try
            {
                try
                {
                    this.BeginTransaction();
                    Guid reREsult = Guid.Empty;
                    DbCommand command = Database.GetStoredProcCommand("InsertVacancy");
                    command.CommandTimeout = 100;
                    Database.AddInParameter(command, "@ClientId", DbType.Guid, pVacancy.ClientId);
                    Database.AddInParameter(command, "@PositionTypeId", DbType.Guid, pVacancy.PositionTypeId);
                    Database.AddInParameter(command, "@LanguageId", DbType.Guid, pVacancy.LanguageId);
                    Database.AddInParameter(command, "@JobTitle", DbType.String, pVacancy.JobTitle);
                    Database.AddInParameter(command, "@FieldValue", DbType.String, fieldValue);
                    if (pVacancy.PostedOn.Equals(DateTime.MinValue))
                    {
                        pVacancy.PostedOn = DateTime.UtcNow;
                    }
                    Database.AddInParameter(command, "@PostedOn", DbType.DateTime, pVacancy.PostedOn);
                    Database.AddInParameter(command, "@VacancyState", DbType.Int32, pVacancy.VacancyState);
                    Database.AddInParameter(command, "@PositionID", DbType.String, pVacancy.PositionID);
                    Database.AddInParameter(command, "@VacancyStatusId", DbType.Guid, pVacancy.VacancyStatusId);
                    Database.AddInParameter(command, "@JobType", DbType.Int32, pVacancy.JobType);
                    Database.AddInParameter(command, "@HoursPerWeek", DbType.Int32, pVacancy.HoursPerWeek);
                    Database.AddInParameter(command, "@EmploymentType", DbType.Int32, pVacancy.EmploymentType);
                    Database.AddInParameter(command, "@DivisionId", DbType.Guid, pVacancy.DivisionId);
                    Database.AddInParameter(command, "@Location", DbType.Guid, pVacancy.Location);
                    Database.AddInParameter(command, "@StartDate", DbType.DateTime, pVacancy.StartDate);
                    Database.AddInParameter(command, "@EndDate", DbType.DateTime, pVacancy.EndDate);
                    Database.AddInParameter(command, "@TotalPositions", DbType.Int32, pVacancy.TotalPositions);
                    Database.AddInParameter(command, "@RemainingPositions", DbType.Int32, pVacancy.RemainingPositions);
                    Database.AddInParameter(command, "@ShowOnWeb", DbType.String, pVacancy.ShowOnWeb);
                    Database.AddInParameter(command, "@FeaturedOnWeb", DbType.Boolean, pVacancy.FeaturedOnWeb);
                    Database.AddInParameter(command, "@PositionRequestedBy", DbType.Guid, pVacancy.PositionRequestedBy);
                    Database.AddInParameter(command, "@PositionOwner", DbType.Guid, pVacancy.PositionOwner);
                    Database.AddInParameter(command, "@IsDelete", DbType.Boolean, pVacancy.IsDelete);
                    Database.AddInParameter(command, "@ConfirmVacancyDetails", DbType.Boolean, pVacancy.ConfirmVacancyDetails);
                    Database.AddInParameter(command, "@ConfirmJobDescription", DbType.Boolean, pVacancy.ConfirmJobDescription);
                    Database.AddInParameter(command, "@ConfirmCompensationInfo", DbType.Boolean, pVacancy.ConfirmCompensationInfo);
                    Database.AddInParameter(command, "@ConfirmApplicationReview", DbType.Boolean, pVacancy.ConfirmApplicationreview);
                    Database.AddInParameter(command, "@EmployeeId", DbType.Guid, pVacancy.OnBoardManagerId);
                    Database.AddInParameter(command, "@ActivityCode", DbType.String, pVacancy.ActivityCode);
                    Database.AddInParameter(command, "@AnnouncementType", DbType.String, pVacancy.AnnouncementType);
                    Database.AddInParameter(command, "@FundingOpptyNumber", DbType.String, pVacancy.FundingOpptyNumber);

                    if (pVacancy.ProgramOfficer != Guid.Empty)
                        Database.AddInParameter(command, "@ProgramOfficer", DbType.Guid, pVacancy.ProgramOfficer);

                    Database.AddInParameter(command, "@CashMatchRequirement", DbType.Decimal, pVacancy.CashMatchRequirement);

                    if (pVacancy.AnnouncementDate != DateTime.MinValue)
                        Database.AddInParameter(command, "@AnnouncementDate", DbType.DateTime, pVacancy.AnnouncementDate);

                    if (pVacancy.OpenDate != DateTime.MinValue)
                        Database.AddInParameter(command, "@OpenDate", DbType.DateTime, pVacancy.OpenDate);

                    if (pVacancy.ApplicationDueDate != DateTime.MinValue)
                        Database.AddInParameter(command, "@ApplicationDueDate", DbType.DateTime, pVacancy.ApplicationDueDate);

                    if (pVacancy.ExpirationDate != DateTime.MinValue)
                        Database.AddInParameter(command, "@ExpirationDate", DbType.DateTime, pVacancy.ExpirationDate);

                    Database.AddInParameter(command, "@FundAmount", DbType.Decimal, pVacancy.FundAmount);

                    Database.AddOutParameter(command, "VacancyId", DbType.Guid, 16);
                    Database.AddOutParameter(command, "@IsError", DbType.Int32, 4);
                    string[] outParams = new string[] { "VacancyId", "@IsError" };
                    var result = base.Add(command, outParams);
                    String errorCode = result[outParams[1]].ToString();
                    if (result != null)
                    {
                        switch (errorCode)
                        {
                            case "101":
                                throw new Exception("Opportunity Id already exists.");
                        }
                        Guid.TryParse(result[outParams[0]].ToString(), out reREsult);
                    }
                    this.CommitTransaction();
                    return reREsult;
                }
                catch
                {
                    this.RollbackTransaction();
                    throw;
                }
            }
            catch
            {
                throw;
            }
        }


        public bool UpdateGrantVacancy(Vacancy pVacancy, string fieldValue)
        {
            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateVacancy");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@VacancyId", DbType.Guid, pVacancy.VacancyId);

                Database.AddInParameter(command, "@ClientId", DbType.Guid, pVacancy.ClientId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, pVacancy.LanguageId);

                Database.AddInParameter(command, "@PositionTypeId", DbType.Guid, pVacancy.PositionTypeId);


                Database.AddInParameter(command, "@JobTitle", DbType.String, pVacancy.JobTitle);

                //Database.AddInParameter(command, "@CompanyId", DbType.Guid, pVacancy.CompanyId);

                // Database.AddInParameter(command, "@PositionID", DbType.String, pVacancy.PositionID);

                Database.AddInParameter(command, "@VacancyStatusId", DbType.Guid, pVacancy.VacancyStatusId);

                Database.AddInParameter(command, "@JobType", DbType.Int32, pVacancy.JobType);

                Database.AddInParameter(command, "@EmploymentType", DbType.Int32, pVacancy.EmploymentType);

                Database.AddInParameter(command, "@DivisionId", DbType.Guid, pVacancy.DivisionId);

                Database.AddInParameter(command, "@Location", DbType.Guid, pVacancy.Location);


                Database.AddInParameter(command, "@StartDate", DbType.DateTime, pVacancy.StartDate);

                Database.AddInParameter(command, "@EndDate", DbType.DateTime, pVacancy.EndDate);

                Database.AddInParameter(command, "@TotalPositions", DbType.Int32, pVacancy.TotalPositions);
                Database.AddInParameter(command, "@RemainingPositions", DbType.Int32, pVacancy.RemainingPositions);

                Database.AddInParameter(command, "@ShowOnWeb", DbType.String, pVacancy.ShowOnWeb);

                Database.AddInParameter(command, "@FeaturedOnWeb", DbType.Boolean, pVacancy.FeaturedOnWeb);

                Database.AddInParameter(command, "@PositionRequestedBy", DbType.Guid, pVacancy.PositionRequestedBy);

                Database.AddInParameter(command, "@PositionOwner", DbType.Guid, pVacancy.PositionOwner);

                Database.AddInParameter(command, "@EmployeeId", DbType.Guid, pVacancy.OnBoardManagerId);


                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, pVacancy.IsDelete);

                if (pVacancy.PostedOn == System.DateTime.MinValue)
                {
                    Database.AddInParameter(command, "@PostedOn", DbType.DateTime, DBNull.Value);

                }
                else
                {
                    Database.AddInParameter(command, "@PostedOn", DbType.DateTime, pVacancy.PostedOn);


                }
                Database.AddInParameter(command, "@ActivityCode", DbType.String, pVacancy.ActivityCode);
                Database.AddInParameter(command, "@AnnouncementType", DbType.String, pVacancy.AnnouncementType);
                Database.AddInParameter(command, "@FundingOpptyNumber", DbType.String, pVacancy.FundingOpptyNumber);
                Database.AddInParameter(command, "@ProgramOfficer", DbType.Guid, pVacancy.ProgramOfficer);
                Database.AddInParameter(command, "@CashMatchRequirement", DbType.Decimal, pVacancy.CashMatchRequirement);
                Database.AddInParameter(command, "@AnnouncementDate", DbType.DateTime, pVacancy.AnnouncementDate);
                Database.AddInParameter(command, "@OpenDate", DbType.DateTime, pVacancy.OpenDate);
                Database.AddInParameter(command, "@ApplicationDueDate", DbType.DateTime, pVacancy.ApplicationDueDate);
                Database.AddInParameter(command, "@ExpirationDate", DbType.DateTime, pVacancy.ExpirationDate);
                Database.AddInParameter(command, "@FieldValue", DbType.String, fieldValue);
                Database.AddInParameter(command, "@HoursPerWeek", DbType.Int32, pVacancy.HoursPerWeek);
                Database.AddInParameter(command, "@IsSaveHistroy", DbType.Boolean, pVacancy.IsSaveHistroy);
                Database.AddInParameter(command, "@FundAmount", DbType.Decimal, pVacancy.FundAmount);
                //CR-35 MAX AND MIN FUNDING AMMOUNT BY MUNEENDRA START( get and binding the data)
                Database.AddInParameter(command, "@MinFundAmount", DbType.Decimal, pVacancy.MinFundAmount);
                Database.AddInParameter(command, "@MaxFundingAmount", DbType.Decimal, pVacancy.MaxFundAmount);
                Database.AddInParameter(command, "@TotalFundedTodate", DbType.Decimal, pVacancy.TotalFundedToDate);
                //Database.AddInParameter(command, "@TotalNumberofAwards", DbType.Decimal, pVacancy.TotalNumberOfAwards);
                Database.AddInParameter(command, "@TotalNumberofAwards", DbType.Int32, pVacancy.TotalNumberOfAwards);
                Database.AddInParameter(command, "@RemainingFunds", DbType.Decimal, Convert.ToDecimal(pVacancy.FundAmount - pVacancy.TotalFundedToDate));
                //CR-35 END

                Database.AddOutParameter(command, "IsError", DbType.Int32, 4);

                var result = base.SaveWithoutDuplication(command, "IsError");

                if (result != null)
                {
                    switch (result.ToString())
                    {
                        case "101":
                            throw new Exception("Opportunity Id already exists.");
                    }
                    if (result.ToString() == "0")
                    {
                        reREsult = true;
                    }
                }
                return reREsult;
            }
            catch
            {
                throw;
            }
        }

        public Guid AddVacancyBylanguage(Vacancy pVacancy)
        {
            try
            {
                try
                {
                    this.BeginTransaction();

                    Guid reREsult = Guid.Empty;
                    DbCommand command = Database.GetStoredProcCommand("InsertVacancyByLanguage");

                    command.CommandTimeout = 100;
                    Database.AddInParameter(command, "@VacancyId", DbType.Guid, pVacancy.VacancyId);


                    Database.AddInParameter(command, "@ClientId", DbType.Guid, pVacancy.ClientId);

                    Database.AddInParameter(command, "@PositionTypeId", DbType.Guid, pVacancy.PositionTypeId);

                    Database.AddInParameter(command, "@LanguageId", DbType.Guid, pVacancy.LanguageId);

                    Database.AddInParameter(command, "@JobTitle", DbType.String, pVacancy.JobTitle);

                    if (pVacancy.PostedOn.Equals(DateTime.MinValue))
                    {
                        pVacancy.PostedOn = DateTime.UtcNow;
                    }
                    Database.AddInParameter(command, "@PostedOn", DbType.DateTime, pVacancy.PostedOn);

                    Database.AddInParameter(command, "@VacancyState", DbType.Int32, pVacancy.VacancyState);

                    Database.AddInParameter(command, "@PositionID", DbType.String, pVacancy.PositionID);

                    Database.AddInParameter(command, "@VacancyStatusId", DbType.Guid, pVacancy.VacancyStatusId);

                    Database.AddInParameter(command, "@JobType", DbType.Int32, pVacancy.JobType);

                    Database.AddInParameter(command, "@EmploymentType", DbType.Int32, pVacancy.EmploymentType);

                    Database.AddInParameter(command, "@DivisionId", DbType.Guid, pVacancy.DivisionId);

                    Database.AddInParameter(command, "@Location", DbType.String, pVacancy.Location);



                    Database.AddInParameter(command, "@StartDate", DbType.DateTime, pVacancy.StartDate);

                    Database.AddInParameter(command, "@EndDate", DbType.DateTime, pVacancy.EndDate);

                    Database.AddInParameter(command, "@TotalPositions", DbType.Int32, pVacancy.TotalPositions);
                    Database.AddInParameter(command, "@RemainingPositions", DbType.Int32, pVacancy.RemainingPositions);

                    Database.AddInParameter(command, "@ShowOnWeb", DbType.Boolean, pVacancy.ShowOnWeb);

                    Database.AddInParameter(command, "@FeaturedOnWeb", DbType.Boolean, pVacancy.FeaturedOnWeb);

                    Database.AddInParameter(command, "@PositionRequestedBy", DbType.Guid, pVacancy.PositionRequestedBy);

                    Database.AddInParameter(command, "@PositionOwner", DbType.Guid, pVacancy.PositionOwner);

                    Database.AddInParameter(command, "@JobDescription", DbType.String, pVacancy.JobDescription);

                    Database.AddInParameter(command, "@ShowOnWebJobDescription", DbType.Boolean, pVacancy.ShowOnWebJobDescription);

                    Database.AddInParameter(command, "@SkillsAndQualification", DbType.String, pVacancy.SkillsAndQualification);

                    Database.AddInParameter(command, "@ShowOnWebSkills", DbType.Boolean, pVacancy.ShowOnWebSkills);

                    Database.AddInParameter(command, "@DutiesAndResponsibilities", DbType.String, pVacancy.DutiesAndResponsibilities);

                    Database.AddInParameter(command, "@ShowOnWebDuties", DbType.Boolean, pVacancy.ShowOnWebDuties);

                    Database.AddInParameter(command, "@Benefits", DbType.String, pVacancy.Benefits);

                    Database.AddInParameter(command, "@ShowOnWebBenefits", DbType.Boolean, pVacancy.ShowOnWebBenefits);

                    Database.AddInParameter(command, "@SalaryMin", DbType.Decimal, pVacancy.SalaryMin);

                    Database.AddInParameter(command, "@SalaryMax", DbType.Decimal, pVacancy.SalaryMax);

                    Database.AddInParameter(command, "@ShowOnWebSal", DbType.Boolean, pVacancy.ShowOnWebSal);

                    Database.AddInParameter(command, "@HourlyMin", DbType.Decimal, pVacancy.HourlyMin);

                    Database.AddInParameter(command, "@HourlyMax", DbType.Decimal, pVacancy.HourlyMax);

                    Database.AddInParameter(command, "@ShowonWebHour", DbType.Boolean, pVacancy.ShowonWebHour);

                    Database.AddInParameter(command, "@Commission", DbType.String, pVacancy.Commission);

                    Database.AddInParameter(command, "@ShowOnWebCommission", DbType.Boolean, pVacancy.ShowOnWebCommission);

                    Database.AddInParameter(command, "@BonusPotential", DbType.String, pVacancy.BonusPotential);

                    Database.AddInParameter(command, "@ShowOnWebBonus", DbType.Boolean, pVacancy.ShowOnWebCommission);

                    Database.AddInParameter(command, "@IsDelete", DbType.Boolean, pVacancy.IsDelete);
                    Database.AddInParameter(command, "@HoursPerWeek", DbType.Int32, pVacancy.HoursPerWeek);

                    Database.AddOutParameter(command, "VacancyLanguageId", DbType.Guid, 16); ;

                    var result = base.Add(command, "VacancyLanguageId");

                    if (result != null)
                    {
                        Guid.TryParse(result.ToString(), out reREsult);
                    }
                    this.CommitTransaction();
                    return reREsult;

                }

                catch
                {
                    this.RollbackTransaction();
                    throw;
                }

            }
            catch
            {
                throw;
            }
        }

        public bool UpdateVacancy(Vacancy pVacancy, string fieldValue)
        {
            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateVacancy");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@VacancyId", DbType.Guid, pVacancy.VacancyId);

                Database.AddInParameter(command, "@ClientId", DbType.Guid, pVacancy.ClientId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, pVacancy.LanguageId);

                Database.AddInParameter(command, "@PositionTypeId", DbType.Guid, pVacancy.PositionTypeId);


                Database.AddInParameter(command, "@JobTitle", DbType.String, pVacancy.JobTitle);

                //Database.AddInParameter(command, "@CompanyId", DbType.Guid, pVacancy.CompanyId);

                // Database.AddInParameter(command, "@PositionID", DbType.String, pVacancy.PositionID);

                Database.AddInParameter(command, "@VacancyStatusId", DbType.Guid, pVacancy.VacancyStatusId);

                Database.AddInParameter(command, "@JobType", DbType.Int32, pVacancy.JobType);

                Database.AddInParameter(command, "@EmploymentType", DbType.Int32, pVacancy.EmploymentType);

                Database.AddInParameter(command, "@DivisionId", DbType.Guid, pVacancy.DivisionId);

                Database.AddInParameter(command, "@Location", DbType.Guid, pVacancy.Location);


                Database.AddInParameter(command, "@StartDate", DbType.DateTime, pVacancy.StartDate);

                Database.AddInParameter(command, "@EndDate", DbType.DateTime, pVacancy.EndDate);

                Database.AddInParameter(command, "@TotalPositions", DbType.Int32, pVacancy.TotalPositions);
                Database.AddInParameter(command, "@RemainingPositions", DbType.Int32, pVacancy.RemainingPositions);

                Database.AddInParameter(command, "@ShowOnWeb", DbType.String, pVacancy.ShowOnWeb);

                Database.AddInParameter(command, "@FeaturedOnWeb", DbType.Boolean, pVacancy.FeaturedOnWeb);

                Database.AddInParameter(command, "@PositionRequestedBy", DbType.Guid, pVacancy.PositionRequestedBy);

                Database.AddInParameter(command, "@PositionOwner", DbType.Guid, pVacancy.PositionOwner);

                Database.AddInParameter(command, "@EmployeeId", DbType.Guid, pVacancy.OnBoardManagerId);


                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, pVacancy.IsDelete);

                if (pVacancy.PostedOn == System.DateTime.MinValue)
                {
                    Database.AddInParameter(command, "@PostedOn", DbType.DateTime, DBNull.Value);

                }
                else
                {
                    Database.AddInParameter(command, "@PostedOn", DbType.DateTime, pVacancy.PostedOn);


                }
                Database.AddInParameter(command, "@FieldValue", DbType.String, fieldValue);
                Database.AddInParameter(command, "@HoursPerWeek", DbType.Int32, pVacancy.HoursPerWeek);
                Database.AddInParameter(command, "@IsSaveHistroy", DbType.Boolean, pVacancy.IsSaveHistroy);

                Database.AddOutParameter(command, "IsError", DbType.Int32, 4);

                var result = base.SaveWithoutDuplication(command, "IsError");

                if (result != null)
                {
                    switch (result.ToString())
                    {
                        case "101":
                            throw new Exception("Position Id already exists.");
                    }
                    if (result.ToString() == "0")
                    {
                        reREsult = true;
                    }
                }
                return reREsult;
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateJobDescriptionByVacancyId(Vacancy pVacancy)
        {
            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateJobDescriptionByVacancyId");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@VacancyId", DbType.Guid, pVacancy.VacancyId);

                Database.AddInParameter(command, "@JobDescription", DbType.String, pVacancy.JobDescription);

                Database.AddInParameter(command, "@ShowOnWebJobDescription", DbType.Boolean, pVacancy.ShowOnWebJobDescription);

                Database.AddInParameter(command, "@SkillsAndQualification", DbType.String, pVacancy.SkillsAndQualification);

                Database.AddInParameter(command, "@ShowOnWebSkills", DbType.Boolean, pVacancy.ShowOnWebSkills);

                Database.AddInParameter(command, "@DutiesAndResponsibilities", DbType.String, pVacancy.DutiesAndResponsibilities);

                Database.AddInParameter(command, "@ShowOnWebDuties", DbType.Boolean, pVacancy.ShowOnWebDuties);

                Database.AddInParameter(command, "@Benefits", DbType.String, pVacancy.Benefits);

                Database.AddInParameter(command, "@ShowOnWebBenefits", DbType.Boolean, pVacancy.ShowOnWebBenefits);
                Database.AddInParameter(command, "@IsSaveHistroy", DbType.Boolean, pVacancy.IsSaveHistroy);

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

        public bool UpdateCompensationInfoByVacancyId(Vacancy pVacancy)
        {
            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateCompensationInfoByVacancyId");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@VacancyId", DbType.Guid, pVacancy.VacancyId);
                Database.AddInParameter(command, "@SalaryMin", DbType.Decimal, pVacancy.SalaryMin);
                Database.AddInParameter(command, "@SalaryMax", DbType.Decimal, pVacancy.SalaryMax);
                Database.AddInParameter(command, "@ShowOnWebSal", DbType.Boolean, pVacancy.ShowOnWebSal);
                Database.AddInParameter(command, "@HourlyMin", DbType.Decimal, pVacancy.HourlyMin);
                Database.AddInParameter(command, "@HourlyMax", DbType.Decimal, pVacancy.HourlyMax);
                Database.AddInParameter(command, "@ShowonWebHour", DbType.Boolean, pVacancy.ShowonWebHour);
                Database.AddInParameter(command, "@Commission", DbType.String, pVacancy.Commission);
                Database.AddInParameter(command, "@ShowOnWebCommission", DbType.Boolean, pVacancy.ShowOnWebCommission);
                Database.AddInParameter(command, "@BonusPotential", DbType.String, pVacancy.BonusPotential);
                Database.AddInParameter(command, "@ShowOnWebBonus", DbType.Boolean, pVacancy.ShowOnWebBonus);
                Database.AddInParameter(command, "@IsSaveHistroy", DbType.Boolean, pVacancy.IsSaveHistroy);
                Database.AddInParameter(command, "@ApplicationInstruction", DbType.String, pVacancy.ApplicationInstruction);
                Database.AddInParameter(command, "@ShowOnWebAppInstruction", DbType.Boolean, pVacancy.ShowOnWebAppInstruction);
                Database.AddInParameter(command, "@ShowOnWebAppInstructionDoc", DbType.Boolean, pVacancy.ShowOnWebAppInstructionDoc);
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

        public List<Vacancy> GetAllVacancyByClientandLanguage(Guid ClientId, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllVacanciesByClientAndLanguage");
                Database.AddInParameter(command, "@ClientId", DbType.Guid, ClientId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.Find(command, new GetAllVacancyByClientAndLanguageFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Vacancy> GetAllVacanciesByUsersAndLanguage(string divisions, Guid LanguageId, string Status, string search)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllVacanciesByUsersAndLanguage");
                if (!string.IsNullOrEmpty(divisions))
                {
                    Database.AddInParameter(command, "@Users", DbType.String, divisions);
                }

                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                Database.AddInParameter(command, "@Status", DbType.String, Status);
                if (!string.IsNullOrEmpty(search))
                    Database.AddInParameter(command, "@SearchText", DbType.String, search);
                else
                    Database.AddInParameter(command, "@SearchText", DbType.String, DBNull.Value);
                return base.Find(command, new GetAllVacancyByClientAndLanguageFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Vacancy GetVacancyById(Guid pRecordId, Guid ClientId, Guid languageId, bool IsRoundCount)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetVacancyById");
                Database.AddInParameter(command, "@VacancyId", DbType.Guid, pRecordId);
                Database.AddInParameter(command, "@ClientId", DbType.Guid, base.CurrentClient.ClientId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, languageId);

                Database.AddInParameter(command, "@IsRoundCount", DbType.Boolean, IsRoundCount);


                return base.FindOne(command, new GetVacancyByIdFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Vacancy GetJobDescriptionByVacancyId(Guid pRecordId, Guid languageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetJobDescriptionByVacancyId");
                Database.AddInParameter(command, "@VacancyId", DbType.Guid, pRecordId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, languageId);

                return base.FindOne(command, new GetVacancyByIdFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Vacancy GetCompensationInfoByVacancyId(Guid pRecordId, Guid languageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetCompensationInfoByVacancyId");
                Database.AddInParameter(command, "@VacancyId", DbType.Guid, pRecordId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, languageId);
                return base.FindOne(command, new GetVacancyByIdFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Vacancy GetVacancyPublicCode(Int64 PublicCode)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetVacancyByPublicCodeId");
                Database.AddInParameter(command, "@PublicCode", DbType.Int64, PublicCode);
                return base.FindOne(command, new GetVacancyByIdFactory());
            }
            catch (Exception)
            {
                throw;
            }
        }


        public Decimal GetNewPositionId()
        {
            try
            {
                Decimal PosId = 1000001;
                DbCommand command = Database.GetSqlStringCommand("select dbo.fGetNewPosId()");
                command.CommandType = CommandType.Text;
                var result = base.FindScalarValue(command);
                if (result != null)
                {
                    Decimal.TryParse(result.ToString(), out PosId);
                }
                return PosId;
            }
            catch
            {
                throw;
            }
        }

        public Vacancy GetVacStatusCnt(Guid LanguageId, string UsersDivisionIdList)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetStatusWiseVacancyCount");
                if (LanguageId != Guid.Empty)
                    Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                if (!string.IsNullOrEmpty(UsersDivisionIdList))
                    Database.AddInParameter(command, "@UsersDivisionIdList", DbType.String, UsersDivisionIdList);
                return base.FindOne(command, new GetVacancyStatusCountFactory(), false);
            }
            catch
            {
                throw;
            }
        }


        public bool UpdateConfirmation(Guid Vacancyid, int index)
        {
            try
            {
                //index: 1= Vac Details, 2= Job Desc, 3= Compensation Info, 4= Applicant Review Process
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateConfirmationRequiredByVacancy");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@VacancyId", DbType.Guid, Vacancyid);
                Database.AddInParameter(command, "@Index", DbType.Int32, index);
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

        public bool UpdateIsSendApplyEmail(Guid Vacancyid, Guid? ApplyEmailTemplateId, bool value)
        {
            try
            {
                //index: 1= Vac Details, 2= Job Desc, 3= Compensation Info, 4= Applicant Review Process
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateIsSendApplyEmail");
                Database.AddInParameter(command, "@VacancyId", DbType.Guid, Vacancyid);
                Database.AddInParameter(command, "@ApplyEmailTemplateId", DbType.Guid, ApplyEmailTemplateId);

                Database.AddInParameter(command, "@value", DbType.Boolean, value);
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
        public bool UpdateIsSendWithdrawEmail(Guid Vacancyid, bool value)
        {
            try
            {

                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateIsSendWithdrawEmail");
                Database.AddInParameter(command, "@VacancyId", DbType.Guid, Vacancyid);
                Database.AddInParameter(command, "@value", DbType.Boolean, value);
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


        public Guid GetApplyEmailTemplaidId(Guid Vacancyid)
        {
            try
            {

                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("GetApplyEmailTemplateIdByVacancyId");
                Database.AddInParameter(command, "@VacancyId", DbType.Guid, Vacancyid);

                var result = base.FindScalarValue(command);
                if (result != null)
                {
                    Guid.TryParse(result.ToString(), out reREsult);
                }
                return reREsult;
            }
            catch
            {
                throw;
            }
        }

        internal class GetVacancyByIdFactory : IDomainObjectFactory<Vacancy>
        {
            public Vacancy Construct(IDataReader reader)
            {
                Vacancy objVacancy = new Vacancy();

                objVacancy.VacancyId = HelperMethods.GetGuid(reader, "VacancyId");
                objVacancy.ClientId = HelperMethods.GetGuid(reader, "ClientId");
                objVacancy.LanguageId = HelperMethods.GetGuid(reader, "LanguageId");
                objVacancy.PositionTypeId = HelperMethods.GetGuid(reader, "PositionTypeId");
                //objVacancy.CompanyId = HelperMethods.GetGuid(reader, "CompanyId");
                objVacancy.JobTitle = HelperMethods.GetString(reader, "JobTitle");
                objVacancy.PositionID = HelperMethods.GetDecimal(reader, "PositionID");
                objVacancy.VacancyStatusId = HelperMethods.GetGuid(reader, "VacancyStatusId");
                objVacancy.JobType = HelperMethods.GetInt32(reader, "JobType");
                objVacancy.EmploymentType = HelperMethods.GetInt32(reader, "EmploymentType");
                objVacancy.PositionID = HelperMethods.GetDecimal(reader, "PositionID");
                objVacancy.DivisionId = HelperMethods.GetGuid(reader, "DivisionId");
                objVacancy.Location = HelperMethods.GetGuid(reader, "Location");
                objVacancy.LocationText = HelperMethods.GetString(reader, "LocationText");

                objVacancy.StartDate = HelperMethods.GetDateTime(reader, "StartDate");
                objVacancy.EndDate = HelperMethods.GetDateTime(reader, "EndDate");
                objVacancy.PostedOn = HelperMethods.GetDateTime(reader, "PostedOn");

                objVacancy.TotalPositions = HelperMethods.GetInt32(reader, "TotalPositions");
                objVacancy.RemainingPositions = HelperMethods.GetInt32(reader, "RemainingPositions");
                objVacancy.ShowOnWeb = HelperMethods.GetString(reader, "ShowOnWeb");
                objVacancy.FeaturedOnWeb = HelperMethods.GetBoolean(reader, "FeaturedOnWeb");
                objVacancy.PositionRequestedBy = HelperMethods.GetGuid(reader, "PositionRequestedBy");
                objVacancy.PositionOwner = HelperMethods.GetGuid(reader, "PositionOwner");
                objVacancy.JobDescription = HelperMethods.GetString(reader, "JobDescription");
                objVacancy.ShowOnWebJobDescription = HelperMethods.GetBoolean(reader, "ShowOnWebJobDescription");
                objVacancy.DutiesAndResponsibilities = HelperMethods.GetString(reader, "DutiesAndResponsibilities");
                objVacancy.ShowOnWebDuties = HelperMethods.GetBoolean(reader, "ShowOnWebDuties");
                objVacancy.SkillsAndQualification = HelperMethods.GetString(reader, "SkillsAndQualification");
                objVacancy.ShowOnWebSkills = HelperMethods.GetBoolean(reader, "ShowOnWebSkills");
                objVacancy.Benefits = HelperMethods.GetString(reader, "Benefits");
                objVacancy.ShowOnWebBenefits = HelperMethods.GetBoolean(reader, "ShowOnWebBenefits");
                objVacancy.SalaryMin = HelperMethods.GetDecimal(reader, "SalaryMin");
                objVacancy.SalaryMax = HelperMethods.GetDecimal(reader, "SalaryMax");
                objVacancy.ShowOnWebSal = HelperMethods.GetBoolean(reader, "ShowOnWebSal");
                objVacancy.HourlyMin = HelperMethods.GetDecimal(reader, "HourlyMin");
                objVacancy.HourlyMax = HelperMethods.GetDecimal(reader, "HourlyMax");
                objVacancy.ShowonWebHour = HelperMethods.GetBoolean(reader, "ShowonWebHour");

                objVacancy.Commission = HelperMethods.GetString(reader, "Commission");
                objVacancy.ShowOnWebCommission = HelperMethods.GetBoolean(reader, "ShowOnWebCommission");
                objVacancy.BonusPotential = HelperMethods.GetString(reader, "BonusPotential");
                objVacancy.ShowOnWebBonus = HelperMethods.GetBoolean(reader, "ShowOnWebBonus");
                objVacancy.VacancyState = HelperMethods.GetInt32(reader, "VacancyState");
                objVacancy.CreatedBy = HelperMethods.GetGuid(reader, "CreatedBy");
                objVacancy.IsDelete = HelperMethods.GetBoolean(reader, "IsDelete");
                objVacancy.VacancyStatusText = HelperMethods.GetString(reader, "VacancyStatusText");
                objVacancy.JobTypeText = HelperMethods.GetString(reader, "JobTypeText");
                objVacancy.EmploymentTypeText = HelperMethods.GetString(reader, "EmploymentTypeText");
                objVacancy.PositionTypeName = HelperMethods.GetString(reader, "PositionTypeName");
                objVacancy.PublicCode = HelperMethods.GetInt64(reader, "PublicCode");
                objVacancy.HoursPerWeek = HelperMethods.GetInt32(reader, "HoursPerWeek");
                objVacancy.ConfirmJobDescription = HelperMethods.GetBoolean(reader, "ConfirmJobDescription");
                objVacancy.ConfirmVacancyDetails = HelperMethods.GetBoolean(reader, "ConfirmVacancyDetails");
                objVacancy.ConfirmCompensationInfo = HelperMethods.GetBoolean(reader, "ConfirmCompensationInfo");
                objVacancy.ConfirmApplicationreview = HelperMethods.GetBoolean(reader, "ConfirmApplicationReview");
                objVacancy.OnBoardManagerId = HelperMethods.GetGuid(reader, "EmployeeId");
                objVacancy.VacRndCount = HelperMethods.GetInt32(reader, "VacRndCount");
                objVacancy.ActivityCode = HelperMethods.GetString(reader, "ActivityCode");
                objVacancy.AnnouncementType = HelperMethods.GetString(reader, "AnnouncementType");
                objVacancy.FundingOpptyNumber = HelperMethods.GetString(reader, "FundingOpptyNumber");
                objVacancy.ProgramOfficer = HelperMethods.GetGuid(reader, "ProgramOfficer");
                objVacancy.CashMatchRequirement = HelperMethods.GetDecimal(reader, "CashMatchRequirement");
                objVacancy.AnnouncementDate = HelperMethods.GetDateTime(reader, "AnnouncementDate");
                objVacancy.OpenDate = HelperMethods.GetDateTime(reader, "OpenDate");
                objVacancy.ApplicationDueDate = HelperMethods.GetDateTime(reader, "ApplicationDueDate");
                objVacancy.ExpirationDate = HelperMethods.GetDateTime(reader, "ExpirationDate");
                objVacancy.ApplicationInstruction = HelperMethods.GetString(reader, "ApplicationInstruction");
                objVacancy.ShowOnWebAppInstruction = HelperMethods.GetBoolean(reader, "ShowOnWebAppInstruction");
                objVacancy.ShowOnWebAppInstructionDoc = HelperMethods.GetBoolean(reader, "ShowOnWebAppInstructionDoc");
                objVacancy.FundAmount = HelperMethods.GetDecimal(reader, "FundAmount");
                //CR-35 MAX AND MIN FUNDING AMMOUNT BY MUNEENDRA START( get and binding the data)
                objVacancy.MaxFundAmount = HelperMethods.GetDecimal(reader, "MaxFundAmount");
                objVacancy.MinFundAmount = HelperMethods.GetDecimal(reader, "MinFundAmount");
                objVacancy.TotalFundedToDate = HelperMethods.GetDecimal(reader, "TotalFundedTodate");
                objVacancy.TotalNumberOfAwards = HelperMethods.GetInt32(reader, "TotalNumberofAwards");
                objVacancy.RemainingFunds = HelperMethods.GetDecimal(reader, "RemainingFunds");
                return objVacancy;
                //CR-35 END 
            }
        }
        public bool UpdateShowOnWeb(Guid VacancyId, bool FieldValue, DateTime PostedOn)
        {
            bool localTransaction = false;
            try
            {

                if (this.Transaction == null)
                {
                    this.BeginTransaction();
                    localTransaction = true;
                }

                bool reREsult = false;

                DbCommand command = Database.GetStoredProcCommand("UpdateShowOnWeb");

                Database.AddInParameter(command, "@VacancyId", DbType.Guid, VacancyId);
                Database.AddInParameter(command, "@PostedOn", DbType.DateTime, PostedOn);
                Database.AddInParameter(command, "@FieldValue", DbType.Boolean, FieldValue);

                var result = base.Save(command);

                if (result != null)
                {
                    bool.TryParse(result.ToString(), out reREsult);
                }
                if (localTransaction)
                    this.CommitTransaction();

                return reREsult;
            }
            catch
            {
                if (localTransaction)
                    this.RollbackTransaction();
                throw;

            }
        }

        public bool UpdateVacancyStatusByVacancyId(Guid VacancyId, string VacancyStatus, DateTime PostedOn)
        {
            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateVacancyStatusByVacancyId");
                Database.AddInParameter(command, "@VacancyId", DbType.Guid, VacancyId);
                Database.AddInParameter(command, "@PostedOn", DbType.DateTime, PostedOn);
                Database.AddInParameter(command, "@VacancyStatus", DbType.String, VacancyStatus);
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

        public bool UpdateVacStatusAndVacReasonByVacancyId(Guid VacancyId, string VacancyStatus, Guid VacancyReason, DateTime PostedOn)
        {
            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateVacStatusAndVacReasonByVacancyId");
                Database.AddInParameter(command, "@VacancyId", DbType.Guid, VacancyId);
                Database.AddInParameter(command, "@PostedOn", DbType.DateTime, PostedOn);
                Database.AddInParameter(command, "@VacancyStatus", DbType.String, VacancyStatus);
                Database.AddInParameter(command, "@VacancyReason", DbType.Guid, VacancyReason);
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


        public bool UpdateVacancyByField(string Fieldname, Guid VacancyId, string FieldValue)
        {
            bool localTransaction = false;
            try
            {

                if (this.Transaction == null)
                {
                    this.BeginTransaction();
                    localTransaction = true;
                }

                bool reREsult = false;

                DbCommand command = Database.GetStoredProcCommand("UpdateVacancyByField");

                Database.AddInParameter(command, "@VacancyId", DbType.Guid, VacancyId);
                Database.AddInParameter(command, "@FieldName", DbType.String, Fieldname);
                Database.AddInParameter(command, "@FieldValue", DbType.String, FieldValue);

                var result = base.Save(command);

                if (result != null)
                {
                    bool.TryParse(result.ToString(), out reREsult);
                }
                if (localTransaction)
                    this.CommitTransaction();

                return reREsult;
            }
            catch
            {
                if (localTransaction)
                    this.RollbackTransaction();
                throw;

            }
        }

        public bool UpdateVacancyStatusByVacancy(string Fieldname, Guid VacancyId, Guid FieldValue)
        {
            bool localTransaction = false;
            try
            {

                if (this.Transaction == null)
                {
                    this.BeginTransaction();
                    localTransaction = true;
                }

                bool reREsult = false;

                DbCommand command = Database.GetStoredProcCommand("UpdateVacancyStatusByVacancy");

                Database.AddInParameter(command, "@VacancyId", DbType.Guid, VacancyId);
                Database.AddInParameter(command, "@FieldName", DbType.String, Fieldname);
                Database.AddInParameter(command, "@FieldValue", DbType.Guid, FieldValue);
                var result = base.Save(command);

                if (result != null)
                {
                    bool.TryParse(result.ToString(), out reREsult);
                }
                if (localTransaction)
                    this.CommitTransaction();

                return reREsult;
            }
            catch
            {
                if (localTransaction)
                    this.RollbackTransaction();
                throw;

            }
        }

        public bool DeleteVacancy(Guid ClientId, Guid precordId)
        {
            try
            {
                this.BeginTransaction();
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("DeleteVacancy");

                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@VacancyId", DbType.Guid, precordId);
                Database.AddInParameter(command, "@ClientId", DbType.Guid, ClientId);
                var result = base.Save(command);

                if (result != null)
                {
                    bool.TryParse(result.ToString(), out reREsult);
                }
                this.CommitTransaction();

                return reREsult;

            }
            catch
            {
                this.RollbackTransaction();
                throw;
            }

        }
        public bool DeleteMultipleVacancy(Guid ClientId, String ids)
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
                DbCommand command = Database.GetStoredProcCommand("DeleteMultipleVacancy");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@VacancyId", DbType.String, ids);
                Database.AddInParameter(command, "@ClientId", DbType.Guid, ClientId);
                var result = base.Save(command, true);

                if (result != null)
                {
                    Boolean.TryParse(result.ToString(), out reREsult);
                }
                if (LocalTrasaction)
                    this.CommitTransaction();

                return reREsult;
            }

            catch
            {
                if (LocalTrasaction)
                    this.RollbackTransaction();
                throw;
            }
        }
        public bool Delete(Guid recordId)
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
                DbCommand command = Database.GetStoredProcCommand("DeleteEmployeeVacancy");

                command.CommandTimeout = 100;

                Database.AddInParameter(command, "@VacancyId", DbType.Guid, recordId);




                var result = base.Save(command, false);

                if (result != null)
                {
                    Boolean.TryParse(result.ToString(), out reREsult);
                }
                if (LocalTrasaction)
                    this.CommitTransaction();

                return reREsult;
            }

            catch
            {
                if (LocalTrasaction)
                    this.RollbackTransaction();
                throw;
            }
        }

        public int GetVacRndCountByVacancy(Guid VacancyId)
        {
            try
            {
                int cnt = 0;
                DbCommand command = Database.GetStoredProcCommand("VacRndCount");
                Database.AddInParameter(command, "@VacancyId", DbType.Guid, VacancyId);
                var result = base.FindScalarValue(command);
                if (result != null)
                {
                    Int32.TryParse(result.ToString(), out cnt);
                }
                return cnt;
            }
            catch
            {
                throw;
            }
        }

        public Vacancy GetTitleAndLocation(Guid VacancyId, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetJobtitleAndLocationbyVacancy");
                Database.AddInParameter(command, "@VacancyId", DbType.Guid, VacancyId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.FindOne(command, new GetTitleAndLocationFactory());


            }
            catch
            {
                throw;
            }
        }

        public List<Vacancy> GetVacancyListForFilter(Guid LanguageId, string StatusFilterString, Guid? UserId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllVacanciesListForFilter");
                Database.AddInParameter(command, "@StatusFilterString", DbType.String, StatusFilterString);
                if (UserId != null)
                {
                    Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);
                }
                return base.Find(command, new GetVacancyListForFilterFactory(), false);
            }
            catch
            {
                throw;
            }
        }

        public List<Vacancy> GetAllVacanciesByUserId(Guid UserId, Guid ApplicationId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetAllVacanciesByUserId");
                Database.AddInParameter(command, "@UserId", DbType.Guid, UserId);
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                return base.Find(command, new GetVacancyListForFilterFactory(), false);
            }
            catch
            {
                throw;
            }
        }


        #region TO Do later For Count
        //public int GetRecordCount(Guid UserId ,Guid LanguageId)
        //{
        //    try
        //    {
        //        this.BeginTransaction();
        //        int reREsult = 0;
        //        DbCommand command = Database.GetStoredProcCommand("RecordCount");

        //        command.CommandTimeout = 100;
        //        Database.AddInParameter(command, "@condition", DbType.String, _condition);
        //        Database.AddInParameter(command, "@tablename", DbType.String, _tablename);
        //        Database.AddInParameter(command, "@countonfield", DbType.String, _countonfield);
        //        Database.AddInParameter(command, "@languageId", DbType.String, LanguageId);
        //        Database.AddInParameter(command, "@UserId", DbType.String, UserId);


        //        var result = base.FindScalarValue(command);

        //        if (result != null)
        //        {
        //            int.TryParse(result.ToString(), out reREsult);
        //        }
        //        this.CommitTransaction();

        //        return reREsult;


        //    }
        //    catch
        //    {
        //        throw;
        //    }

        //}
        #endregion

        internal class GetAllVacancyByClientAndLanguageFactory : IDomainObjectFactory<Vacancy>
        {
            public Vacancy Construct(IDataReader reader)
            {
                Vacancy objVacancy = new Vacancy();

                objVacancy.VacancyId = HelperMethods.GetGuid(reader, "VacancyId");
                objVacancy.ClientId = HelperMethods.GetGuid(reader, "ClientId");
                objVacancy.LanguageId = HelperMethods.GetGuid(reader, "LanguageId");
                objVacancy.PositionTypeId = HelperMethods.GetGuid(reader, "PositionTypeId");
                objVacancy.PositionTypeName = HelperMethods.GetString(reader, "PositionTypeName");
                objVacancy.JobTitle = HelperMethods.GetString(reader, "JobTitle");
                objVacancy.PositionID = HelperMethods.GetDecimal(reader, "PositionID");
                objVacancy.VacancyStatusId = HelperMethods.GetGuid(reader, "VacancyStatusId");
                objVacancy.JobType = HelperMethods.GetInt32(reader, "JobType");
                objVacancy.EmploymentType = HelperMethods.GetInt32(reader, "EmploymentType");
                objVacancy.PositionID = HelperMethods.GetDecimal(reader, "PositionID");
                objVacancy.DivisionId = HelperMethods.GetGuid(reader, "DivisionId");
                objVacancy.Location = HelperMethods.GetGuid(reader, "Location");
                objVacancy.LocationText = HelperMethods.GetString(reader, "LocationText");
                objVacancy.StartDate = HelperMethods.GetDateTime(reader, "StartDate");
                objVacancy.EndDate = HelperMethods.GetDateTime(reader, "EndDate");
                objVacancy.PostedOn = HelperMethods.GetDateTime(reader, "PostedOn");
                objVacancy.TotalPositions = HelperMethods.GetInt32(reader, "TotalPositions");
                objVacancy.RemainingPositions = HelperMethods.GetInt32(reader, "RemainingPositions");
                objVacancy.ShowOnWeb = HelperMethods.GetString(reader, "ShowOnWeb");
                objVacancy.FeaturedOnWeb = HelperMethods.GetBoolean(reader, "FeaturedOnWeb");
                objVacancy.PositionRequestedBy = HelperMethods.GetGuid(reader, "PositionRequestedBy");
                objVacancy.PositionOwner = HelperMethods.GetGuid(reader, "PositionOwner");
                objVacancy.JobDescription = HelperMethods.GetString(reader, "JobDescription");
                objVacancy.ShowOnWebJobDescription = HelperMethods.GetBoolean(reader, "ShowOnWebJobDescription");
                objVacancy.DutiesAndResponsibilities = HelperMethods.GetString(reader, "DutiesAndResponsibilities");
                objVacancy.ShowOnWebDuties = HelperMethods.GetBoolean(reader, "ShowOnWebDuties");
                objVacancy.SkillsAndQualification = HelperMethods.GetString(reader, "SkillsAndQualification");
                objVacancy.ShowOnWebSkills = HelperMethods.GetBoolean(reader, "ShowOnWebSkills");
                objVacancy.Benefits = HelperMethods.GetString(reader, "Benefits");
                objVacancy.ShowOnWebBenefits = HelperMethods.GetBoolean(reader, "ShowOnWebBenefits");
                objVacancy.SalaryMin = HelperMethods.GetDecimal(reader, "SalaryMin");
                objVacancy.SalaryMax = HelperMethods.GetDecimal(reader, "SalaryMax");
                objVacancy.ShowOnWebSal = HelperMethods.GetBoolean(reader, "ShowOnWebSal");
                objVacancy.HourlyMin = HelperMethods.GetDecimal(reader, "HourlyMin");
                objVacancy.HourlyMax = HelperMethods.GetDecimal(reader, "HourlyMax");
                objVacancy.ShowonWebHour = HelperMethods.GetBoolean(reader, "ShowonWebHour");
                objVacancy.Commission = HelperMethods.GetString(reader, "Commission");
                objVacancy.ShowOnWebCommission = HelperMethods.GetBoolean(reader, "ShowOnWebCommission");
                objVacancy.BonusPotential = HelperMethods.GetString(reader, "BonusPotential");
                objVacancy.ShowOnWebBonus = HelperMethods.GetBoolean(reader, "ShowOnWebBonus");
                objVacancy.VacancyState = HelperMethods.GetInt32(reader, "VacancyState");
                objVacancy.IsDelete = HelperMethods.GetBoolean(reader, "IsDelete");
                objVacancy.CreatedBy = HelperMethods.GetGuid(reader, "CreatedBy");
                objVacancy.CreatedByName = HelperMethods.GetString(reader, "CreatedByName");
                objVacancy.VacancyStatusText = HelperMethods.GetString(reader, "VacancyStatusText");
                objVacancy.JobTypeText = HelperMethods.GetString(reader, "JobTypeText");
                objVacancy.EmploymentTypeText = HelperMethods.GetString(reader, "EmploymentTypeText");
                objVacancy.Applicants = HelperMethods.GetInt32(reader, "Applicants");
                objVacancy.PublicCode = HelperMethods.GetInt64(reader, "PublicCode");
                objVacancy.ConfirmVacancyDetails = HelperMethods.GetBoolean(reader, "ConfirmVacancyDetails");
                objVacancy.ConfirmJobDescription = HelperMethods.GetBoolean(reader, "ConfirmJobDescription");
                objVacancy.ConfirmCompensationInfo = HelperMethods.GetBoolean(reader, "ConfirmCompensationInfo");
                objVacancy.ConfirmApplicationreview = HelperMethods.GetBoolean(reader, "ConfirmApplicationReview");
                objVacancy.DaysOpen = HelperMethods.GetInt32(reader, "DaysOpen");
                return objVacancy;
            }
        }

        internal class GetTitleAndLocationFactory : IDomainObjectFactory<Vacancy>
        {
            public Vacancy Construct(IDataReader reader)
            {
                Vacancy objVacancy = new Vacancy();
                objVacancy.VacancyId = HelperMethods.GetGuid(reader, "VacancyId");
                objVacancy.JobTitle = HelperMethods.GetString(reader, "JobTitle");
                objVacancy.Location = HelperMethods.GetGuid(reader, "Location");
                objVacancy.LocationText = HelperMethods.GetString(reader, "LocationText");
                return objVacancy;
            }
        }

        internal class GetVacancyStatusCountFactory : IDomainObjectFactory<Vacancy>
        {
            public Vacancy Construct(IDataReader reader)
            {
                Vacancy objVacancy = new Vacancy();

                objVacancy.CntArchieve = HelperMethods.GetInt32(reader, "ArchievedVac");
                objVacancy.CntDraft = HelperMethods.GetInt32(reader, "DraftVac");
                objVacancy.CntOpen = HelperMethods.GetInt32(reader, "OpenVac");
                objVacancy.CntClose = HelperMethods.GetInt32(reader, "ClosedVac");
                objVacancy.CntAll = HelperMethods.GetInt32(reader, "AllVac");


                return objVacancy;
            }

        }

        internal class GetVacancyListForFilterFactory : IDomainObjectFactory<Vacancy>
        {
            public Vacancy Construct(IDataReader reader)
            {
                Vacancy objVacancy = new Vacancy();
                objVacancy.VacancyId = HelperMethods.GetGuid(reader, "VacancyId");
                objVacancy.JobTitle = HelperMethods.GetString(reader, "JobTitle");
                return objVacancy;
            }
        }

    }
}
