using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;


namespace ATS.DataAccess
{
    public class VacancyOfferRepository : Repository<BEClient.VacancyOffer>
    {
        public VacancyOfferRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }
        public Guid AddVacancyOffer(BEClient.VacancyOffer pVacancyOffer)
        {
            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertVacancyOffer");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, pVacancyOffer.ApplicationId);
                Database.AddInParameter(command, "@OfferDate", DbType.DateTime, pVacancyOffer.OfferDate);
                Database.AddInParameter(command, "@OfferStatusId", DbType.Int32, pVacancyOffer.OfferStatusId);
                Database.AddInParameter(command, "@OfferTypeId", DbType.Int32, pVacancyOffer.OfferTypeId);
                Database.AddInParameter(command, "@EnableCounterOffers", DbType.Boolean, pVacancyOffer.EnableCounterOffers);
                Database.AddInParameter(command, "@EmailToCandidateIdList", DbType.String, pVacancyOffer.EmailToCandidateIdList);
                Database.AddInParameter(command, "@EmailToCandidate_EditContent", DbType.Boolean, pVacancyOffer.EmailToCandidate_EditContent);
                Database.AddInParameter(command, "@OfferLetterIdList", DbType.String, pVacancyOffer.OfferLetterIdList);
                Database.AddInParameter(command, "@OfferLetterId_EditContent", DbType.Boolean, pVacancyOffer.@OfferLetterId_EditContent);
                Database.AddInParameter(command, "@PlacementType", DbType.Int32, pVacancyOffer.PlacementType);
                Database.AddInParameter(command, "@PlacementType_EM", DbType.Boolean, pVacancyOffer.PlacementType_EM);
                Database.AddInParameter(command, "@PlacementType_EC", DbType.Boolean, pVacancyOffer.PlacementType_EC);
                if (pVacancyOffer.StartDate == DateTime.MinValue)
                {
                    Database.AddInParameter(command, "@StartDate", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    Database.AddInParameter(command, "@StartDate", DbType.DateTime, pVacancyOffer.StartDate);
                }
                if (pVacancyOffer.EndDate == DateTime.MinValue)
                {
                    Database.AddInParameter(command, "@EndDate", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    Database.AddInParameter(command, "@EndDate", DbType.DateTime, pVacancyOffer.EndDate);
                }
                Database.AddInParameter(command, "@JobType", DbType.Int32, pVacancyOffer.JobType);
                Database.AddInParameter(command, "@JobType_EM", DbType.Boolean, pVacancyOffer.JobType_EM);
                Database.AddInParameter(command, "@JobType_EC", DbType.Boolean, pVacancyOffer.JobType_EC);
                Database.AddInParameter(command, "@Location", DbType.Guid, pVacancyOffer.Location);
                Database.AddInParameter(command, "@SalaryType", DbType.Int32, pVacancyOffer.SalaryType);
                Database.AddInParameter(command, "@SalaryType_EM", DbType.Boolean, pVacancyOffer.SalaryType_EM);
                Database.AddInParameter(command, "@SalaryType_EC", DbType.Boolean, pVacancyOffer.SalaryType_EC);
                Database.AddInParameter(command, "@SalaryOffered", DbType.Decimal, pVacancyOffer.SalaryOffered);
                Database.AddInParameter(command, "@SalaryOfferedMax", DbType.Decimal, pVacancyOffer.SalaryOfferedMax);
                Database.AddInParameter(command, "@SalaryOffered_EM", DbType.Boolean, pVacancyOffer.SalaryOffered_EM);
                Database.AddInParameter(command, "@SalaryOffered_EC", DbType.Boolean, pVacancyOffer.SalaryOffered_EC);
                Database.AddInParameter(command, "@HoursPerWeek", DbType.Int32, pVacancyOffer.HoursPerWeek);
                Database.AddInParameter(command, "@HoursPerWeek_EM", DbType.Boolean, pVacancyOffer.HoursPerWeek_EM);
                Database.AddInParameter(command, "@HoursPerWeek_EC", DbType.Boolean, pVacancyOffer.HoursPerWeek_EC);
                Database.AddInParameter(command, "@HourlyWage", DbType.Decimal, pVacancyOffer.HourlyWage);
                Database.AddInParameter(command, "@HourlyWageMax", DbType.Decimal, pVacancyOffer.HourlyWageMax);
                Database.AddInParameter(command, "@HourlyWage_EM", DbType.Boolean, pVacancyOffer.HourlyWage_EM);
                Database.AddInParameter(command, "@HourlyWage_EC", DbType.Boolean, pVacancyOffer.HourlyWage_EC);
                Database.AddInParameter(command, "@Rate", DbType.Decimal, pVacancyOffer.Rate);
                Database.AddInParameter(command, "@RateMax", DbType.Decimal, pVacancyOffer.RateMax);
                Database.AddInParameter(command, "@Rate_EM", DbType.Boolean, pVacancyOffer.Rate_EM);
                Database.AddInParameter(command, "@Rate_EC", DbType.Boolean, pVacancyOffer.Rate_EC);
                Database.AddInParameter(command, "@Per", DbType.String, pVacancyOffer.Per);
                Database.AddInParameter(command, "@Per_EM", DbType.Boolean, pVacancyOffer.Per_EM);
                Database.AddInParameter(command, "@Per_EC", DbType.Boolean, pVacancyOffer.Per_EC);
                Database.AddInParameter(command, "@PayRate", DbType.Decimal, pVacancyOffer.PayRate);
                Database.AddInParameter(command, "@PayRateMax", DbType.Decimal, pVacancyOffer.PayRateMax);
                Database.AddInParameter(command, "@PayRate_EM", DbType.Boolean, pVacancyOffer.PayRate_EM);
                Database.AddInParameter(command, "@PayRate_EC", DbType.Boolean, pVacancyOffer.PayRate_EC);
                Database.AddInParameter(command, "@RatePeriod", DbType.Int32, pVacancyOffer.RatePeriod);
                Database.AddInParameter(command, "@RatePeriod_EM", DbType.Boolean, pVacancyOffer.RatePeriod_EM);
                Database.AddInParameter(command, "@RatePeriod_EC", DbType.Boolean, pVacancyOffer.RatePeriod_EC);
                Database.AddInParameter(command, "@PlusCommission", DbType.Boolean, pVacancyOffer.PlusCommission);
                Database.AddInParameter(command, "@CommissionDescriprion", DbType.String, pVacancyOffer.CommissionDescriprion);
                Database.AddInParameter(command, "@CommissionDescriprion_EM", DbType.Boolean, pVacancyOffer.CommissionDescriprion_EM);
                Database.AddInParameter(command, "@AnnualCommissionPotential", DbType.Decimal, pVacancyOffer.AnnualCommissionPotential);
                Database.AddInParameter(command, "@AnnualCommissionPotentialMax", DbType.Decimal, pVacancyOffer.AnnualCommissionPotentialMax);
                Database.AddInParameter(command, "@AnnualCommissionPotential_EM", DbType.Boolean, pVacancyOffer.AnnualCommissionPotential_EM);
                Database.AddInParameter(command, "@AnnualCommissionPotential_EC", DbType.Boolean, pVacancyOffer.AnnualCommissionPotential_EC);
                Database.AddInParameter(command, "@CommissionCap", DbType.Decimal, pVacancyOffer.CommissionCap);
                Database.AddInParameter(command, "@CommissionCapMax", DbType.Decimal, pVacancyOffer.CommissionCapMax);
                Database.AddInParameter(command, "@CommissionCap_EM", DbType.Boolean, pVacancyOffer.CommissionCap_EM);
                Database.AddInParameter(command, "@CommissionCap_EC", DbType.Boolean, pVacancyOffer.CommissionCap_EC);
                //Database.AddInParameter(command, "@AnnualBonusPotential", DbType.Decimal, pVacancyOffer.AnnualBonusPotential);
                Database.AddInParameter(command, "@PlusBonus", DbType.Boolean, pVacancyOffer.PlusBonus);
                Database.AddInParameter(command, "@BonusDescription", DbType.String, pVacancyOffer.BonusDescription);
                Database.AddInParameter(command, "@BonusDescription_EM", DbType.Boolean, pVacancyOffer.BonusDescription_EM);
                Database.AddInParameter(command, "@AnnualBonusPotential", DbType.Decimal, pVacancyOffer.AnnualBonusPotential);
                Database.AddInParameter(command, "@AnnualBonusPotentialMax", DbType.Decimal, pVacancyOffer.AnnualBonusPotentialMax);
                Database.AddInParameter(command, "@AnnualBonusPotential_EM", DbType.Boolean, pVacancyOffer.AnnualBonusPotential_EM);
                Database.AddInParameter(command, "@AnnualBonusPotential_EC", DbType.Boolean, pVacancyOffer.AnnualBonusPotential_EC);
                Database.AddInParameter(command, "@BonusCap", DbType.Decimal, pVacancyOffer.BonusCap);
                Database.AddInParameter(command, "@BonusCapMax", DbType.Decimal, pVacancyOffer.BonusCapMax);
                Database.AddInParameter(command, "@BonusCap_EM", DbType.Boolean, pVacancyOffer.BonusCap_EM);
                Database.AddInParameter(command, "@BonusCap_EC", DbType.Boolean, pVacancyOffer.BonusCap_EC);
                Database.AddInParameter(command, "@IncludeCandidateNotice", DbType.Boolean, pVacancyOffer.IncludeCandidateNotice);
                Database.AddInParameter(command, "@CandidateNoticePeriod", DbType.Int32, pVacancyOffer.CandidateNoticePeriod);
                Database.AddInParameter(command, "@CandidateNoticePeriod_EM", DbType.Boolean, pVacancyOffer.CandidateNoticePeriod_EM);
                Database.AddInParameter(command, "@CandidateNoticePeriod_EC", DbType.Boolean, pVacancyOffer.CandidateNoticePeriod_EC);
                Database.AddInParameter(command, "@CandidateNoticePeriodType", DbType.Int32, pVacancyOffer.CandidateNoticePeriodType);
                Database.AddInParameter(command, "@CandidateNoticePeriodType_EM", DbType.Boolean, pVacancyOffer.CandidateNoticePeriodType_EM);
                Database.AddInParameter(command, "@CandidateNoticePeriodType_EC", DbType.Boolean, pVacancyOffer.CandidateNoticePeriodType_EC);
                Database.AddInParameter(command, "@IncludeCompanyNotice", DbType.Boolean, pVacancyOffer.IncludeCompanyNotice);
                Database.AddInParameter(command, "@CompanyNoticePeriod", DbType.Int32, pVacancyOffer.CompanyNoticePeriod);
                Database.AddInParameter(command, "@CompanyNoticePeriod_EM", DbType.Boolean, pVacancyOffer.CompanyNoticePeriod_EM);
                Database.AddInParameter(command, "@CompanyNoticePeriod_EC", DbType.Boolean, pVacancyOffer.CompanyNoticePeriod_EC);
                Database.AddInParameter(command, "@CompanyNoticePeriodType", DbType.Int32, pVacancyOffer.CompanyNoticePeriodType);
                Database.AddInParameter(command, "@CompanyNoticePeriodType_EM", DbType.Boolean, pVacancyOffer.CompanyNoticePeriodType_EM);
                Database.AddInParameter(command, "@CompanyNoticePeriodType_EC", DbType.Boolean, pVacancyOffer.CompanyNoticePeriodType_EC);
                Database.AddInParameter(command, "@NoteToApplicant", DbType.String, pVacancyOffer.NoteToApplicant);
                Database.AddInParameter(command, "@ResponseFromApplicant", DbType.String, pVacancyOffer.ResponseFromApplicant);
                if (pVacancyOffer.OfferAcceptedDate == DateTime.MinValue)
                {
                    Database.AddInParameter(command, "@OfferAcceptedDate", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    Database.AddInParameter(command, "@OfferAcceptedDate", DbType.DateTime, pVacancyOffer.OfferAcceptedDate);
                }
                Database.AddInParameter(command, "@PrintCandidateName", DbType.String, pVacancyOffer.PrInt32CandidateName);
                Database.AddInParameter(command, "@CandidateConfirmEmail", DbType.String, pVacancyOffer.CandidateConfirmEmail);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, pVacancyOffer.IsDelete);
                Database.AddInParameter(command, "@IncludeAttachments", DbType.Boolean, pVacancyOffer.IncludeAttachments);
                Database.AddInParameter(command, "@OfferTemplateId", DbType.Guid, pVacancyOffer.OfferTemplateId);
                Database.AddInParameter(command, "@OfferDeadlineDate", DbType.DateTime, pVacancyOffer.OfferDeadlineDate);

                ////Used in GV
                Database.AddInParameter(command, "@AwardAmount", DbType.Decimal, pVacancyOffer.AwardAmount);
                Database.AddInParameter(command, "@AwardAmountMax", DbType.Decimal, pVacancyOffer.AwardAmountMax);
                Database.AddInParameter(command, "@AwardAmount_EM", DbType.Boolean, pVacancyOffer.AwardAmount_EM);
                Database.AddInParameter(command, "@AwardAmount_EC", DbType.Boolean, pVacancyOffer.AwardAmount_EC);
                Database.AddInParameter(command, "@AwardIssueDate", DbType.DateTime, pVacancyOffer.AwardIssueDate);
                Database.AddInParameter(command, "@CashMatchRequired", DbType.Decimal, pVacancyOffer.CashMatchRequired);
                Database.AddInParameter(command, "@CashMatchRequiredMax", DbType.Decimal, pVacancyOffer.CashMatchRequiredMax);
                Database.AddInParameter(command, "@CashMatchRequired_EM", DbType.Boolean, pVacancyOffer.CashMatchRequired_EM);
                Database.AddInParameter(command, "@CashMatchRequired_EC", DbType.Boolean, pVacancyOffer.CashMatchRequired_EC);
                


                Database.AddOutParameter(command, "VacancyOfferId", DbType.Guid, 16);
                var result = base.Add(command, "VacancyOfferId");
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

        public bool UpdateVacancyOffer(BEClient.VacancyOffer pVacancyOffer)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("UpdateVacancyOffer");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@VacancyOfferId", DbType.Guid, pVacancyOffer.VacancyOfferId);
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, pVacancyOffer.ApplicationId);
                Database.AddInParameter(command, "@OfferDate ", DbType.DateTime, pVacancyOffer.OfferDate);
                Database.AddInParameter(command, "@OfferStatusId", DbType.Int32, pVacancyOffer.OfferStatusId);
                Database.AddInParameter(command, "@OfferTypeId", DbType.Int32, pVacancyOffer.OfferTypeId);
                Database.AddInParameter(command, "@EnableCounterOffers", DbType.Boolean, pVacancyOffer.EnableCounterOffers);
                Database.AddInParameter(command, "@EmailToCandidateIdList", DbType.String, pVacancyOffer.EmailToCandidateIdList);
                Database.AddInParameter(command, "@EmailToCandidate_EditContent", DbType.Boolean, pVacancyOffer.EmailToCandidate_EditContent);
                Database.AddInParameter(command, "@OfferLetterIdList", DbType.String, pVacancyOffer.OfferLetterIdList);
                Database.AddInParameter(command, "@OfferLetterId_EditContent", DbType.Boolean, pVacancyOffer.OfferLetterId_EditContent);
                Database.AddInParameter(command, "@PlacementType", DbType.Int32, pVacancyOffer.PlacementType);
                Database.AddInParameter(command, "@PlacementType_EM", DbType.Boolean, pVacancyOffer.PlacementType_EM);
                Database.AddInParameter(command, "@PlacementType_EC ", DbType.Boolean, pVacancyOffer.PlacementType_EC);
                if (pVacancyOffer.StartDate == DateTime.MinValue)
                {
                    Database.AddInParameter(command, "@StartDate", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    Database.AddInParameter(command, "@StartDate", DbType.DateTime, pVacancyOffer.StartDate);
                }
                if (pVacancyOffer.EndDate == DateTime.MinValue)
                {
                    Database.AddInParameter(command, "@EndDate", DbType.DateTime, DBNull.Value);
                }
                else
                {
                    Database.AddInParameter(command, "@EndDate", DbType.DateTime, pVacancyOffer.EndDate);
                }
                Database.AddInParameter(command, "@JobType", DbType.Int32, pVacancyOffer.JobType);
                Database.AddInParameter(command, "@JobType_EM ", DbType.Boolean, pVacancyOffer.JobType_EM);
                Database.AddInParameter(command, "@JobType_EC ", DbType.Boolean, pVacancyOffer.JobType_EC);
                Database.AddInParameter(command, "@Location", DbType.Guid, pVacancyOffer.Location);
                Database.AddInParameter(command, "@SalaryType ", DbType.Int32, pVacancyOffer.SalaryType);
                Database.AddInParameter(command, "@SalaryType_EM ", DbType.Boolean, pVacancyOffer.SalaryType_EM);
                Database.AddInParameter(command, "@SalaryType_EC ", DbType.Boolean, pVacancyOffer.SalaryType_EC);
                Database.AddInParameter(command, "@SalaryOffered ", DbType.Decimal, pVacancyOffer.SalaryOffered);
                Database.AddInParameter(command, "@SalaryOffered_EM ", DbType.Boolean, pVacancyOffer.SalaryOffered_EM);
                Database.AddInParameter(command, "@SalaryOffered_EC ", DbType.Boolean, pVacancyOffer.SalaryOffered_EC);
                Database.AddInParameter(command, "@HoursPerWeek ", DbType.Int32, pVacancyOffer.HoursPerWeek);
                Database.AddInParameter(command, "@HoursPerWeek_EM ", DbType.Boolean, pVacancyOffer.HoursPerWeek_EM);
                Database.AddInParameter(command, "@HoursPerWeek_EC ", DbType.Boolean, pVacancyOffer.HoursPerWeek_EC);
                Database.AddInParameter(command, "@HourlyWage ", DbType.Decimal, pVacancyOffer.HourlyWage);
                Database.AddInParameter(command, "@HourlyWage_EM ", DbType.Boolean, pVacancyOffer.HourlyWage_EM);
                Database.AddInParameter(command, "@HourlyWage_EC ", DbType.Boolean, pVacancyOffer.HourlyWage_EC);
                Database.AddInParameter(command, "@Rate ", DbType.Decimal, pVacancyOffer.Rate);
                Database.AddInParameter(command, "@Rate_EM ", DbType.Boolean, pVacancyOffer.Rate_EM);
                Database.AddInParameter(command, "@Rate_EC ", DbType.Boolean, pVacancyOffer.Rate_EC);
                Database.AddInParameter(command, "@Per ", DbType.String, pVacancyOffer.Per);
                Database.AddInParameter(command, "@Per_EM ", DbType.Boolean, pVacancyOffer.Per_EM);
                Database.AddInParameter(command, "@Per_EC ", DbType.Boolean, pVacancyOffer.Per_EC);
                Database.AddInParameter(command, "@PayRate ", DbType.Decimal, pVacancyOffer.PayRate);
                Database.AddInParameter(command, "@PayRate_EM ", DbType.Boolean, pVacancyOffer.PayRate_EM);
                Database.AddInParameter(command, "@PayRate_EC ", DbType.Boolean, pVacancyOffer.PayRate_EC);
                Database.AddInParameter(command, "@RatePeriod ", DbType.Int32, pVacancyOffer.RatePeriod);
                Database.AddInParameter(command, "@RatePeriod_EM ", DbType.Boolean, pVacancyOffer.RatePeriod_EM);
                Database.AddInParameter(command, "@RatePeriod_EC ", DbType.Boolean, pVacancyOffer.RatePeriod_EC);
                Database.AddInParameter(command, "@PlusCommission ", DbType.Boolean, pVacancyOffer.PlusCommission);
                Database.AddInParameter(command, "@CommissionDescriprion ", DbType.String, pVacancyOffer.CommissionDescriprion);
                Database.AddInParameter(command, "@CommissionDescriprion_EM ", DbType.Boolean, pVacancyOffer.CommissionDescriprion_EM);
                Database.AddInParameter(command, "@AnnualCommissionPotential ", DbType.Decimal, pVacancyOffer.AnnualCommissionPotential);
                Database.AddInParameter(command, "@AnnualCommissionPotential_EM ", DbType.Boolean, pVacancyOffer.AnnualCommissionPotential_EM);
                Database.AddInParameter(command, "@AnnualCommissionPotential_EC ", DbType.Boolean, pVacancyOffer.AnnualCommissionPotential_EC);
                Database.AddInParameter(command, "@CommissionCap ", DbType.Decimal, pVacancyOffer.CommissionCap);
                Database.AddInParameter(command, "@CommissionCap_EM ", DbType.Boolean, pVacancyOffer.CommissionCap_EM);
                Database.AddInParameter(command, "@CommissionCap_EC ", DbType.Boolean, pVacancyOffer.CommissionCap_EC);
                Database.AddInParameter(command, "@PlusBonus ", DbType.Boolean, pVacancyOffer.PlusBonus);
                Database.AddInParameter(command, "@BonusDescription ", DbType.String, pVacancyOffer.BonusDescription);
                Database.AddInParameter(command, "@BonusDescription_EM ", DbType.Boolean, pVacancyOffer.BonusDescription_EM);
                Database.AddInParameter(command, "@AnnualBonusPotential ", DbType.Decimal, pVacancyOffer.AnnualBonusPotential);
                Database.AddInParameter(command, "@AnnualBonusPotential_EM ", DbType.Boolean, pVacancyOffer.AnnualBonusPotential_EM);
                Database.AddInParameter(command, "@AnnualBonusPotential_EC ", DbType.Boolean, pVacancyOffer.AnnualBonusPotential_EC);
                Database.AddInParameter(command, "@BonusCap ", DbType.Decimal, pVacancyOffer.BonusCap);
                Database.AddInParameter(command, "@BonusCap_EM ", DbType.Boolean, pVacancyOffer.BonusCap_EM);
                Database.AddInParameter(command, "@BonusCap_EC ", DbType.Boolean, pVacancyOffer.BonusCap_EC);
                Database.AddInParameter(command, "@IncludeCandidateNotice ", DbType.Boolean, pVacancyOffer.IncludeCandidateNotice);
                Database.AddInParameter(command, "@CandidateNoticePeriod ", DbType.Int32, pVacancyOffer.CandidateNoticePeriod);
                Database.AddInParameter(command, "@CandidateNoticePeriod_EM ", DbType.Boolean, pVacancyOffer.CandidateNoticePeriod_EM);
                Database.AddInParameter(command, "@CandidateNoticePeriod_EC ", DbType.Boolean, pVacancyOffer.CandidateNoticePeriod_EC);
                Database.AddInParameter(command, "@CandidateNoticePeriodType ", DbType.Int32, pVacancyOffer.CandidateNoticePeriodType);
                Database.AddInParameter(command, "@CandidateNoticePeriodType_EM ", DbType.Boolean, pVacancyOffer.CandidateNoticePeriodType_EM);
                Database.AddInParameter(command, "@CandidateNoticePeriodType_EC ", DbType.Boolean, pVacancyOffer.CandidateNoticePeriodType_EC);
                Database.AddInParameter(command, "@IncludeCompanyNotice ", DbType.Boolean, pVacancyOffer.IncludeCompanyNotice);
                Database.AddInParameter(command, "@CompanyNoticePeriod ", DbType.Int32, pVacancyOffer.CompanyNoticePeriod);
                Database.AddInParameter(command, "@CompanyNoticePeriod_EM ", DbType.Boolean, pVacancyOffer.CompanyNoticePeriod_EM);
                Database.AddInParameter(command, "@CompanyNoticePeriod_EC ", DbType.Boolean, pVacancyOffer.CompanyNoticePeriod_EC);
                Database.AddInParameter(command, "@CompanyNoticePeriodType ", DbType.Int32, pVacancyOffer.CompanyNoticePeriodType);
                Database.AddInParameter(command, "@CompanyNoticePeriodType_EM ", DbType.Boolean, pVacancyOffer.CompanyNoticePeriodType_EM);
                Database.AddInParameter(command, "@CompanyNoticePeriodType_EC ", DbType.Boolean, pVacancyOffer.CompanyNoticePeriodType_EC);
                Database.AddInParameter(command, "@NoteToApplicant ", DbType.String, pVacancyOffer.NoteToApplicant);
                Database.AddInParameter(command, "@ResponseFromApplicant ", DbType.String, pVacancyOffer.ResponseFromApplicant);
                Database.AddInParameter(command, "@IsConfirmedByClient ", DbType.Boolean, pVacancyOffer.IsConfirmedByClient);
                if (pVacancyOffer.IsConfirmedByClient)
                {
                    Database.AddInParameter(command, "@OfferLetter", DbType.String, pVacancyOffer.OfferLetter);
                }
                else
                {
                    Database.AddInParameter(command, "@OfferLetter", DbType.String, DBNull.Value);
                }
                Database.AddInParameter(command, "@OfferDeadlineDate", DbType.DateTime, pVacancyOffer.OfferDeadlineDate);

                ////Used in GV
                Database.AddInParameter(command, "@AwardAmount", DbType.Decimal, pVacancyOffer.AwardAmount);
                Database.AddInParameter(command, "@AwardAmountMax", DbType.Decimal, pVacancyOffer.AwardAmountMax);
                Database.AddInParameter(command, "@AwardAmount_EM", DbType.Boolean, pVacancyOffer.AwardAmount_EM);
                Database.AddInParameter(command, "@AwardAmount_EC", DbType.Boolean, pVacancyOffer.AwardAmount_EC);
                Database.AddInParameter(command, "@AwardIssueDate", DbType.DateTime, pVacancyOffer.AwardIssueDate);
                Database.AddInParameter(command, "@CashMatchRequired", DbType.Decimal, pVacancyOffer.CashMatchRequired);
                Database.AddInParameter(command, "@CashMatchRequiredMax", DbType.Decimal, pVacancyOffer.CashMatchRequiredMax);
                Database.AddInParameter(command, "@CashMatchRequired_EM", DbType.Boolean, pVacancyOffer.CashMatchRequired_EM);
                Database.AddInParameter(command, "@CashMatchRequired_EC", DbType.Boolean, pVacancyOffer.CashMatchRequired_EC);

                return HelperMethods.GetBoolean(base.Save(command));
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.VacancyOffer> GetVacancyOfferByApplicationId(Guid ApplicationId, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetVacancyOfferByApplicationId");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.Find(command, new GetVacancyOfferByApplicationIdFactory());
            }
            catch
            {
                throw;
            }
        }
        public BEClient.VacancyOffer GetVacancyOfferById(Guid VacancyOfferId, Guid LanguageId, Guid VacRndId, Guid ReviewerId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetVacancyOfferById");
                Database.AddInParameter(command, "@VacancyOfferId", DbType.Guid, VacancyOfferId);
                if (VacRndId == Guid.Empty)
                {
                    Database.AddInParameter(command, "@VacRndId", DbType.Guid, DBNull.Value);
                }
                else
                {
                    Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                }
                Database.AddInParameter(command, "@ReviewerId", DbType.Guid, ReviewerId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.FindOne(command, new GetVacancyOfferByIdFactory());
            }
            catch
            {
                throw;
            }
        }
        public BEClient.VacancyOffer GetCandidateAndJobTitleByApp(Guid ApplicationId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetCandidateAndJobTitleByAppId");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                return base.FindOne(command, new GetCandidateAndJobTitleByAppFactory());
            }
            catch
            {
                throw;
            }
        }

        public int GetOfferCountByApplication(Guid AppId)
        {
            try
            {
                int reReSult = 0;
                DbCommand command = Database.GetStoredProcCommand("GetOfferNumber");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, AppId);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, false);

                var Result = base.FindScalarValue(command);
                if (Result != null)
                {
                    Int32.TryParse(Result.ToString(), out reReSult);
                }
                return reReSult;
            }
            catch
            {
                throw;
            }
        }

        public Guid GetJobLocationByApplication(Guid AppId)
        {
            try
            {
                Guid reReSult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("GetJobLocationByApplication");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, AppId);
                var Result = base.FindScalarValue(command);
                if (Result != null)
                {
                    Guid.TryParse(Result.ToString(), out reReSult);
                }
                return reReSult;
            }
            catch
            {
                throw;
            }
        }
        public bool UpdateAllOfferStatus(Guid ApplicationId, Guid VacancyOfferId, int NewStatusId)
        {
            try
            {
                bool reReSult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateAllOfferStatus");
                Database.AddInParameter(command, "@VacancyOfferId", DbType.Guid, VacancyOfferId);
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                Database.AddInParameter(command, "@OfferStatusId", DbType.Int32, NewStatusId);
                var Result = base.Save(command);
                if (Result != null)
                {
                    bool.TryParse(Result.ToString(), out reReSult);
                }
                return reReSult;
            }
            catch
            {
                throw;
            }
        }

        public bool AcceptOfferByCandidate(BEClient.VacancyOffer pVacancyOffer)
        {

            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("AcceptOfferByCandidate");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@VacancyOfferId", DbType.Guid, pVacancyOffer.VacancyOfferId);
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, pVacancyOffer.ApplicationId);
                Database.AddInParameter(command, "@OfferStatusId", DbType.Int32, pVacancyOffer.OfferStatusId);
                Database.AddInParameter(command, "@OfferTypeId", DbType.Int32, pVacancyOffer.OfferTypeId);
                Database.AddInParameter(command, "@PrintCandidateName", DbType.String, pVacancyOffer.CandidateName);
                Database.AddInParameter(command, "@CandidateConfirmEmail", DbType.String, pVacancyOffer.CandidateEmailId);
                Database.AddInParameter(command, "@ResponseFromApplicant", DbType.String, pVacancyOffer.ResponseFromApplicant);
                Database.AddInParameter(command, "@OfferAcceptedDate", DbType.DateTime, DateTime.UtcNow);
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
        public bool DeclinedOfferByCandidate(BEClient.VacancyOffer pVacancyOffer)
        {

            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("DeclineOfferByCandidate");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@VacancyOfferId", DbType.Guid, pVacancyOffer.VacancyOfferId);
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, pVacancyOffer.ApplicationId);
                Database.AddInParameter(command, "@OfferStatusId", DbType.Int32, pVacancyOffer.OfferStatusId);
                Database.AddInParameter(command, "@OfferTypeId", DbType.Int32, pVacancyOffer.OfferTypeId);
                Database.AddInParameter(command, "@ResponseFromApplicant", DbType.String, pVacancyOffer.ResponseFromApplicant);
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

        public BEClient.VacancyOffer GetTemplateOfferByVacRndId(Guid VacRndId, Guid AppId, Guid ReviewerId)
        {
            try
            {
                try
                {
                    DbCommand command = Database.GetStoredProcCommand("GetOfferTemplateByRoundIdForNewOffer");
                    Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                    Database.AddInParameter(command, "@AppId", DbType.Guid, AppId);
                    Database.AddInParameter(command, "@ReviewerId", DbType.Guid, ReviewerId);
                    return base.FindOne(command, new GetVacancyOfferByVacRndIdFactory(), false);
                }
                catch
                {
                    throw;
                }
            }
            catch
            {
                throw;
            }
        }

        internal class GetCandidateAndJobTitleByAppFactory : IDomainObjectFactory<BEClient.VacancyOffer>
        {
            public BEClient.VacancyOffer Construct(IDataReader reader)
            {
                BEClient.VacancyOffer vacancyoffer = new BEClient.VacancyOffer();
                vacancyoffer.CandidateName = HelperMethods.GetString(reader, "CandidateName");
                vacancyoffer.CandidateEmailId = HelperMethods.GetString(reader, "CandidateEmailId");
                vacancyoffer.JobTitle = HelperMethods.GetString(reader, "JobTitle");
                vacancyoffer.ApplicationId = HelperMethods.GetGuid(reader, "ApplicationId");
                vacancyoffer.Location = HelperMethods.GetGuid(reader, "Location");

                return vacancyoffer;
            }
        }
        internal class GetVacancyOfferByApplicationIdFactory : IDomainObjectFactory<BEClient.VacancyOffer>
        {
            public BEClient.VacancyOffer Construct(IDataReader reader)
            {
                BEClient.VacancyOffer vacancyoffer = new BEClient.VacancyOffer();
                vacancyoffer.VacancyOfferId = HelperMethods.GetGuid(reader, "VacancyOfferId");
                vacancyoffer.OfferDate = HelperMethods.GetDateTime(reader, "OfferDate");
                vacancyoffer.OfferStatusText = HelperMethods.GetString(reader, "OfferStatusText");
                vacancyoffer.OfferTypeText = HelperMethods.GetString(reader, "OfferTypeText");
                vacancyoffer.OfferStatusId = HelperMethods.GetInt32(reader, "OfferStatusId");
                vacancyoffer.OfferAcceptedDate = HelperMethods.GetDateTime(reader, "OfferAcceptedDate");
                vacancyoffer.OfferConfirmedDate = HelperMethods.GetDateTime(reader, "OfferConfirmedDate");
                return vacancyoffer;
            }
        }
        public BEClient.VacancyOffer GetVacancyOfferByIdForPDF(Guid VacancyOfferId, Guid ETemplate, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetVacancyOfferByidPdf");

                Database.AddInParameter(command, "@VacancyOfferId", DbType.Guid, VacancyOfferId);
                if (ETemplate != Guid.Empty)
                    Database.AddInParameter(command, "@EmailTemplateId", DbType.Guid, ETemplate);
                else
                    Database.AddInParameter(command, "@EmailTemplateId", DbType.Guid, DBNull.Value);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.FindOne(command, new GetVacancyOfferByIdForPDFFactory());
            }
            catch
            {
                throw;
            }

        }

        public BEClient.VacancyOffer GenerateOfferLetterContentForPDF(Guid VacancyOfferId, Guid LanguageId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("GetVacancyOfferLetterContentByVacOfferId");
                Database.AddInParameter(command, "@VacancyOfferId", DbType.Guid, VacancyOfferId);
                Database.AddInParameter(command, "@LanguageId", DbType.Guid, LanguageId);
                return base.FindOne(command, new GenerateOfferLetterContentForPDFFactory(), false);
            }
            catch
            {
                throw;
            }

        }


        internal class GenerateOfferLetterContentForPDFFactory : IDomainObjectFactory<BEClient.VacancyOffer>
        {
            public BEClient.VacancyOffer Construct(IDataReader reader)
            {
                BEClient.VacancyOffer objVacancyOffer = new BEClient.VacancyOffer();
                objVacancyOffer.VacancyOfferId = HelperMethods.GetGuid(reader, "VacancyOfferId");
                objVacancyOffer.ApplicationId = HelperMethods.GetGuid(reader, "ApplicationId");
                objVacancyOffer.OfferDate = HelperMethods.GetDateTime(reader, "OfferDate");
                objVacancyOffer.OfferStatusId = HelperMethods.GetInt32(reader, "OfferStatusId");
                objVacancyOffer.OfferStatusText = HelperMethods.GetString(reader, "OfferStatusText");
                objVacancyOffer.OfferTypeId = HelperMethods.GetInt32(reader, "OfferTypeId");
                objVacancyOffer.OfferTypeText = HelperMethods.GetString(reader, "OfferTypeText");
                objVacancyOffer.PlacementType = HelperMethods.GetString(reader, "PlacementType");
                objVacancyOffer.PlacementTypeText = HelperMethods.GetString(reader, "PlacementTypeText");
                objVacancyOffer.StartDate = HelperMethods.GetDateTime(reader, "StartDate");
                objVacancyOffer.EndDate = HelperMethods.GetDateTime(reader, "EndDate");
                objVacancyOffer.JobType = HelperMethods.GetInt32(reader, "JobType");
                objVacancyOffer.JobTypeText = HelperMethods.GetString(reader, "JobTypeText");
                objVacancyOffer.Location = HelperMethods.GetGuid(reader, "Location");
                objVacancyOffer.LocationText = HelperMethods.GetString(reader, "LocationText");
                objVacancyOffer.SalaryType = HelperMethods.GetInt32(reader, "SalaryType");
                objVacancyOffer.SalaryTypeText = HelperMethods.GetString(reader, "SalaryTypeText");
                objVacancyOffer.SalaryOffered = HelperMethods.GetDecimal(reader, "SalaryOffered");
                objVacancyOffer.HoursPerWeek = HelperMethods.GetInt32(reader, "HoursPerWeek");
                objVacancyOffer.HourlyWage = HelperMethods.GetDecimal(reader, "HourlyWage");
                objVacancyOffer.Rate = HelperMethods.GetDecimal(reader, "Rate");
                objVacancyOffer.Per = HelperMethods.GetString(reader, "Per");
                objVacancyOffer.PayRate = HelperMethods.GetDecimal(reader, "PayRate");
                objVacancyOffer.RatePeriod = HelperMethods.GetInt32(reader, "RatePeriod");
                objVacancyOffer.RatePeriodText = HelperMethods.GetString(reader, "RatePeriodText");
                objVacancyOffer.PlusCommission = HelperMethods.GetBoolean(reader, "PlusCommission");
                objVacancyOffer.CommissionDescriprion = HelperMethods.GetString(reader, "CommissionDescriprion");
                objVacancyOffer.AnnualCommissionPotential = HelperMethods.GetDecimal(reader, "AnnualCommissionPotential");
                objVacancyOffer.CommissionCap = HelperMethods.GetDecimal(reader, "CommissionCap");
                objVacancyOffer.PlusBonus = HelperMethods.GetBoolean(reader, "PlusBonus");
                objVacancyOffer.BonusDescription = HelperMethods.GetString(reader, "BonusDescription");
                objVacancyOffer.AnnualBonusPotential = HelperMethods.GetDecimal(reader, "AnnualBonusPotential");
                objVacancyOffer.BonusCap = HelperMethods.GetDecimal(reader, "BonusCap");
                objVacancyOffer.IncludeCandidateNotice = HelperMethods.GetBoolean(reader, "IncludeCandidateNotice");
                objVacancyOffer.CandidateNoticePeriod = HelperMethods.GetInt32(reader, "CandidateNoticePeriod");
                objVacancyOffer.CandidateNoticePeriodText = HelperMethods.GetString(reader, "CandidateNoticePeriodText");
                objVacancyOffer.CandidateNoticePeriodType = HelperMethods.GetInt32(reader, "CandidateNoticePeriodType");
                objVacancyOffer.CandidateNoticePeriodTypeText = HelperMethods.GetString(reader, "CandidateNoticePeriodTypeText");
                objVacancyOffer.IncludeCompanyNotice = HelperMethods.GetBoolean(reader, "IncludeCompanyNotice");
                objVacancyOffer.CompanyNoticePeriod = HelperMethods.GetInt32(reader, "CompanyNoticePeriod");
                objVacancyOffer.CompanyNoticePeriodText = HelperMethods.GetString(reader, "CompanyNoticePeriodText");
                objVacancyOffer.CompanyNoticePeriodType = HelperMethods.GetInt32(reader, "CompanyNoticePeriodType");
                objVacancyOffer.CompanyNoticePeriodTypeText = HelperMethods.GetString(reader, "CompanyNoticePeriodTypeText");
                objVacancyOffer.NoteToApplicant = HelperMethods.GetString(reader, "NoteToApplicant");
                objVacancyOffer.ResponseFromApplicant = HelperMethods.GetString(reader, "ResponseFromApplicant");
                objVacancyOffer.JobTitle = HelperMethods.GetString(reader, "JobTitle");
                objVacancyOffer.CandidateName = HelperMethods.GetString(reader, "CandidateName");
                objVacancyOffer.CandidateFirstName = HelperMethods.GetString(reader, "CandidateFirstName");
                objVacancyOffer.CandidateLastName = HelperMethods.GetString(reader, "CandidateLastName");
                objVacancyOffer.CandidateEmailId = HelperMethods.GetString(reader, "CandidateEmailId");
                objVacancyOffer.CreatedByName = HelperMethods.GetString(reader, "CreatedByName");
                objVacancyOffer.CreatedOn = HelperMethods.GetDateTime(reader, "CreatedOn");
                objVacancyOffer.UpdatedByName = HelperMethods.GetString(reader, "UpdatedByName");
                objVacancyOffer.UpdatedOn = HelperMethods.GetDateTime(reader, "UpdatedOn");
                objVacancyOffer.OfferLetter = HelperMethods.GetString(reader, "OfferLetter");
                objVacancyOffer.ManagerName = HelperMethods.GetString(reader, "ManagerName");
                objVacancyOffer.AwardAmount = HelperMethods.GetDecimal(reader, "AwardAmount");
                objVacancyOffer.AwardIssueDate = HelperMethods.GetDateTime(reader, "AwardIssueDate");
                objVacancyOffer.CashMatchRequired = HelperMethods.GetDecimal(reader, "CashMatchRequired");
                return objVacancyOffer;
            }
        }



        public bool IsSendApplyEmail(Guid ApplicationId)
        {

            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("GetSendApplyEmail");
                Database.AddInParameter(command, "@ApplicationId", DbType.Guid, ApplicationId);
                var result = base.FindScalarValue(command);
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
        internal class GetVacancyOfferByIdFactory : IDomainObjectFactory<BEClient.VacancyOffer>
        {
            public BEClient.VacancyOffer Construct(IDataReader reader)
            {
                BEClient.VacancyOffer vacancyoffer = new BEClient.VacancyOffer();
                vacancyoffer.VacancyOfferId = HelperMethods.GetGuid(reader, "VacancyOfferId");
                vacancyoffer.ApplicationId = HelperMethods.GetGuid(reader, "ApplicationId");
                vacancyoffer.OfferDate = HelperMethods.GetDateTime(reader, "OfferDate");
                vacancyoffer.OfferStatusId = HelperMethods.GetInt32(reader, "OfferStatusId");
                vacancyoffer.OfferTypeId = HelperMethods.GetInt32(reader, "OfferTypeId");
                vacancyoffer.EnableCounterOffers = HelperMethods.GetBoolean(reader, "EnableCounterOffers");
                vacancyoffer.EmailToCandidateIdList = HelperMethods.GetString(reader, "EmailToCandidateIdList");
                vacancyoffer.EmailToCandidate_EditContent = HelperMethods.GetBoolean(reader, "EmailToCandidate_EditContent");
                vacancyoffer.OfferLetterIdList = HelperMethods.GetString(reader, "OfferLetterIdList");
                vacancyoffer.OfferLetterId_EditContent = HelperMethods.GetBoolean(reader, "OfferLetterId_EditContent");
                vacancyoffer.PlacementType = HelperMethods.GetString(reader, "PlacementType");
                vacancyoffer.PlacementType_EM = HelperMethods.GetBoolean(reader, "PlacementType_EM");
                vacancyoffer.PlacementType_EC = HelperMethods.GetBoolean(reader, "PlacementType_EC");
                vacancyoffer.StartDate = HelperMethods.GetDateTime(reader, "StartDate");
                vacancyoffer.EndDate = HelperMethods.GetDateTime(reader, "EndDate");
                vacancyoffer.JobType = HelperMethods.GetInt32(reader, "JobType");
                vacancyoffer.JobType_EM = HelperMethods.GetBoolean(reader, "JobType_EM");
                vacancyoffer.JobType_EC = HelperMethods.GetBoolean(reader, "JobType_EC");
                vacancyoffer.Location = HelperMethods.GetGuid(reader, "Location");
                vacancyoffer.SalaryType = HelperMethods.GetInt32(reader, "SalaryType");
                vacancyoffer.SalaryType_EM = HelperMethods.GetBoolean(reader, "SalaryType_EM");
                vacancyoffer.SalaryType_EC = HelperMethods.GetBoolean(reader, "SalaryType_EC");
                vacancyoffer.SalaryOffered = HelperMethods.GetDecimal(reader, "SalaryOffered");
                vacancyoffer.SalaryOfferedMax = HelperMethods.GetDecimal(reader, "SalaryOfferedMax");
                vacancyoffer.SalaryOffered_EM = HelperMethods.GetBoolean(reader, "SalaryOffered_EM");
                vacancyoffer.SalaryOffered_EC = HelperMethods.GetBoolean(reader, "SalaryOffered_EC");
                vacancyoffer.HoursPerWeek = HelperMethods.GetInt32(reader, "HoursPerWeek");
                vacancyoffer.HoursPerWeek_EM = HelperMethods.GetBoolean(reader, "HoursPerWeek_EM");
                vacancyoffer.HoursPerWeek_EC = HelperMethods.GetBoolean(reader, "HoursPerWeek_EC");
                vacancyoffer.PlusCommission = HelperMethods.GetBoolean(reader, "PlusCommission");
                vacancyoffer.PlusBonus = HelperMethods.GetBoolean(reader, "PlusBonus");
                vacancyoffer.HourlyWage = HelperMethods.GetDecimal(reader, "HourlyWage");
                vacancyoffer.HourlyWageMax = HelperMethods.GetDecimal(reader, "HourlyWageMax");
                vacancyoffer.HourlyWage_EM = HelperMethods.GetBoolean(reader, "HourlyWage_EM");
                vacancyoffer.HourlyWage_EC = HelperMethods.GetBoolean(reader, "HourlyWage_EC");
                vacancyoffer.Rate = HelperMethods.GetDecimal(reader, "Rate");
                vacancyoffer.RateMax = HelperMethods.GetDecimal(reader, "RateMax");
                vacancyoffer.Rate_EM = HelperMethods.GetBoolean(reader, "Rate_EM");
                vacancyoffer.Rate_EC = HelperMethods.GetBoolean(reader, "Rate_EC");
                vacancyoffer.Per = HelperMethods.GetString(reader, "Per");
                vacancyoffer.Per_EM = HelperMethods.GetBoolean(reader, "Per_EM");
                vacancyoffer.Per_EC = HelperMethods.GetBoolean(reader, "Per_EC");
                vacancyoffer.PayRate = HelperMethods.GetDecimal(reader, "PayRate");
                vacancyoffer.PayRateMax = HelperMethods.GetDecimal(reader, "PayRateMax");
                vacancyoffer.PayRate_EM = HelperMethods.GetBoolean(reader, "PayRate_EM");
                vacancyoffer.PayRate_EC = HelperMethods.GetBoolean(reader, "PayRate_EC");
                vacancyoffer.RatePeriod = HelperMethods.GetInt32(reader, "RatePeriod");
                vacancyoffer.RatePeriod_EM = HelperMethods.GetBoolean(reader, "RatePeriod_EM");
                vacancyoffer.RatePeriod_EC = HelperMethods.GetBoolean(reader, "RatePeriod_EC");
                vacancyoffer.PlusCommission = HelperMethods.GetBoolean(reader, "PlusCommission");
                vacancyoffer.CommissionDescriprion = HelperMethods.GetString(reader, "CommissionDescriprion");
                vacancyoffer.CommissionDescriprion_EM = HelperMethods.GetBoolean(reader, "CommissionDescriprion_EM");
                vacancyoffer.AnnualCommissionPotential = HelperMethods.GetDecimal(reader, "AnnualCommissionPotential");
                vacancyoffer.AnnualCommissionPotentialMax = HelperMethods.GetDecimal(reader, "AnnualCommissionPotentialMax");
                vacancyoffer.AnnualCommissionPotential_EM = HelperMethods.GetBoolean(reader, "AnnualCommissionPotential_EM");
                vacancyoffer.AnnualCommissionPotential_EC = HelperMethods.GetBoolean(reader, "AnnualCommissionPotential_EC");
                vacancyoffer.CommissionCap = HelperMethods.GetDecimal(reader, "CommissionCap");
                vacancyoffer.CommissionCapMax = HelperMethods.GetDecimal(reader, "CommissionCapMax");
                vacancyoffer.CommissionCap_EM = HelperMethods.GetBoolean(reader, "CommissionCap_EM");
                vacancyoffer.CommissionCap_EC = HelperMethods.GetBoolean(reader, "CommissionCap_EC");
                vacancyoffer.PlusBonus = HelperMethods.GetBoolean(reader, "PlusBonus");
                vacancyoffer.BonusDescription = HelperMethods.GetString(reader, "BonusDescription");
                vacancyoffer.BonusDescription_EM = HelperMethods.GetBoolean(reader, "BonusDescription_EM");
                vacancyoffer.AnnualBonusPotential = HelperMethods.GetDecimal(reader, "AnnualBonusPotential");
                vacancyoffer.AnnualBonusPotentialMax = HelperMethods.GetDecimal(reader, "AnnualBonusPotentialMax");
                vacancyoffer.AnnualBonusPotential_EM = HelperMethods.GetBoolean(reader, "AnnualBonusPotential_EM");
                vacancyoffer.AnnualBonusPotential_EC = HelperMethods.GetBoolean(reader, "AnnualBonusPotential_EC");
                vacancyoffer.BonusCap = HelperMethods.GetDecimal(reader, "BonusCap");
                vacancyoffer.BonusCapMax = HelperMethods.GetDecimal(reader, "BonusCapMax");
                vacancyoffer.BonusCap_EM = HelperMethods.GetBoolean(reader, "BonusCap_EM");
                vacancyoffer.BonusCap_EC = HelperMethods.GetBoolean(reader, "BonusCap_EC");
                vacancyoffer.IncludeCandidateNotice = HelperMethods.GetBoolean(reader, "IncludeCandidateNotice");
                vacancyoffer.CandidateNoticePeriod = HelperMethods.GetInt32(reader, "CandidateNoticePeriod");
                vacancyoffer.CandidateNoticePeriod_EM = HelperMethods.GetBoolean(reader, "CandidateNoticePeriod_EM");
                vacancyoffer.CandidateNoticePeriod_EC = HelperMethods.GetBoolean(reader, "CandidateNoticePeriod_EC");
                vacancyoffer.CandidateNoticePeriodType = HelperMethods.GetInt32(reader, "CandidateNoticePeriodType");
                vacancyoffer.CandidateNoticePeriodType_EM = HelperMethods.GetBoolean(reader, "CandidateNoticePeriodType_EM");
                vacancyoffer.CandidateNoticePeriodType_EC = HelperMethods.GetBoolean(reader, "CandidateNoticePeriodType_EC");
                vacancyoffer.IncludeCompanyNotice = HelperMethods.GetBoolean(reader, "IncludeCompanyNotice");
                vacancyoffer.CompanyNoticePeriod = HelperMethods.GetInt32(reader, "CompanyNoticePeriod");
                vacancyoffer.CompanyNoticePeriod_EM = HelperMethods.GetBoolean(reader, "CompanyNoticePeriod_EM");
                vacancyoffer.CompanyNoticePeriod_EC = HelperMethods.GetBoolean(reader, "CompanyNoticePeriod_EC");
                vacancyoffer.CompanyNoticePeriodType = HelperMethods.GetInt32(reader, "CompanyNoticePeriodType");
                vacancyoffer.CompanyNoticePeriodType_EM = HelperMethods.GetBoolean(reader, "CompanyNoticePeriodType_EM");
                vacancyoffer.CompanyNoticePeriodType_EC = HelperMethods.GetBoolean(reader, "CompanyNoticePeriodType_EC");
                vacancyoffer.NoteToApplicant = HelperMethods.GetString(reader, "NoteToApplicant");
                vacancyoffer.ResponseFromApplicant = HelperMethods.GetString(reader, "ResponseFromApplicant");
                vacancyoffer.JobTitle = HelperMethods.GetString(reader, "JobTitle");
                vacancyoffer.OfferAcceptedDate = HelperMethods.GetDateTime(reader, "OfferAcceptedDate");
                vacancyoffer.PrInt32CandidateName = HelperMethods.GetString(reader, "PrintCandidateName");
                vacancyoffer.CandidateConfirmEmail = HelperMethods.GetString(reader, "CandidateConfirmEmail");
                vacancyoffer.CandidateName = HelperMethods.GetString(reader, "CandidateName");
                vacancyoffer.OfferStatusText = HelperMethods.GetString(reader, "OfferStatusText");
                vacancyoffer.OfferTypeText = HelperMethods.GetString(reader, "OfferTypeText");
                vacancyoffer.CandidateEmailId = HelperMethods.GetString(reader, "CandidateEmailId");
                vacancyoffer.CanMakeOffer = HelperMethods.GetBoolean(reader, "CanMakeOffer");
                vacancyoffer.CanEditOffer = HelperMethods.GetBoolean(reader, "CanEditOffer");
                vacancyoffer.IncludeAttachments = HelperMethods.GetBoolean(reader, "IncludeAttachment");
                vacancyoffer.OfferTemplateId = HelperMethods.GetGuid(reader, "OfferTemplateId");
                vacancyoffer.CreatedByName = HelperMethods.GetString(reader, "CreatedByName");
                vacancyoffer.CreatedOn = HelperMethods.GetDateTime(reader, "CreatedOn");
                vacancyoffer.UpdatedByName = HelperMethods.GetString(reader, "UpdatedByName");
                vacancyoffer.UpdatedOn = HelperMethods.GetDateTime(reader, "UpdatedOn");
                vacancyoffer.OfferDeadlineDate = HelperMethods.GetDateTime(reader, "OfferDeadlineDate");

                ////Used in GV
                vacancyoffer.AwardAmount = HelperMethods.GetDecimal(reader, "AwardAmount");
                vacancyoffer.AwardAmountMax = HelperMethods.GetDecimal(reader, "AwardAmountMax");
                vacancyoffer.AwardAmount_EM = HelperMethods.GetBoolean(reader, "AwardAmount_EM");
                vacancyoffer.AwardAmount_EC = HelperMethods.GetBoolean(reader, "AwardAmount_EC");
                vacancyoffer.AwardIssueDate = HelperMethods.GetDateTime(reader, "AwardIssueDate");
                vacancyoffer.CashMatchRequired = HelperMethods.GetDecimal(reader, "CashMatchRequired");
                vacancyoffer.CashMatchRequiredMax = HelperMethods.GetDecimal(reader, "CashMatchRequiredMax");
                vacancyoffer.CashMatchRequired_EM = HelperMethods.GetBoolean(reader, "CashMatchRequired_EM");
                vacancyoffer.CashMatchRequired_EC = HelperMethods.GetBoolean(reader, "CashMatchRequired_EC");

                return vacancyoffer;
            }
        }
        internal class GetVacancyOfferByIdForPDFFactory : IDomainObjectFactory<BEClient.VacancyOffer>
        {
            public BEClient.VacancyOffer Construct(IDataReader reader)
            {
                BEClient.VacancyOffer vacancyoffer = new BEClient.VacancyOffer();
                vacancyoffer.VacancyOfferId = HelperMethods.GetGuid(reader, "VacancyOfferId");
                vacancyoffer.ApplicationId = HelperMethods.GetGuid(reader, "ApplicationId");
                vacancyoffer.OfferDate = HelperMethods.GetDateTime(reader, "OfferDate");
                vacancyoffer.OfferStatusId = HelperMethods.GetInt32(reader, "OfferStatusId");
                vacancyoffer.OfferStatusText = HelperMethods.GetString(reader, "OfferStatusText");
                vacancyoffer.OfferTypeId = HelperMethods.GetInt32(reader, "OfferTypeId");
                vacancyoffer.OfferTypeText = HelperMethods.GetString(reader, "OfferTypeText");
                vacancyoffer.PlacementType = HelperMethods.GetString(reader, "PlacementType");
                vacancyoffer.PlacementTypeText = HelperMethods.GetString(reader, "PlacementTypeText");
                vacancyoffer.StartDate = HelperMethods.GetDateTime(reader, "StartDate");
                vacancyoffer.EndDate = HelperMethods.GetDateTime(reader, "EndDate");
                vacancyoffer.JobType = HelperMethods.GetInt32(reader, "JobType");
                vacancyoffer.JobTypeText = HelperMethods.GetString(reader, "JobTypeText");
                vacancyoffer.Location = HelperMethods.GetGuid(reader, "Location");
                vacancyoffer.LocationText = HelperMethods.GetString(reader, "LocationText");
                vacancyoffer.SalaryType = HelperMethods.GetInt32(reader, "SalaryType");
                vacancyoffer.SalaryTypeText = HelperMethods.GetString(reader, "SalaryTypeText");
                vacancyoffer.SalaryOffered = HelperMethods.GetDecimal(reader, "SalaryOffered");
                vacancyoffer.HoursPerWeek = HelperMethods.GetInt32(reader, "HoursPerWeek");
                vacancyoffer.HourlyWage = HelperMethods.GetDecimal(reader, "HourlyWage");
                vacancyoffer.Rate = HelperMethods.GetDecimal(reader, "Rate");
                vacancyoffer.Per = HelperMethods.GetString(reader, "Per");
                vacancyoffer.PayRate = HelperMethods.GetDecimal(reader, "PayRate");
                vacancyoffer.RatePeriod = HelperMethods.GetInt32(reader, "RatePeriod");
                vacancyoffer.RatePeriodText = HelperMethods.GetString(reader, "RatePeriodText");
                vacancyoffer.PlusCommission = HelperMethods.GetBoolean(reader, "PlusCommission");
                vacancyoffer.CommissionDescriprion = HelperMethods.GetString(reader, "CommissionDescriprion");
                vacancyoffer.AnnualCommissionPotential = HelperMethods.GetDecimal(reader, "AnnualCommissionPotential");
                vacancyoffer.CommissionCap = HelperMethods.GetDecimal(reader, "CommissionCap");
                vacancyoffer.PlusBonus = HelperMethods.GetBoolean(reader, "PlusBonus");
                vacancyoffer.BonusDescription = HelperMethods.GetString(reader, "BonusDescription");
                vacancyoffer.AnnualBonusPotential = HelperMethods.GetDecimal(reader, "AnnualBonusPotential");
                vacancyoffer.BonusCap = HelperMethods.GetDecimal(reader, "BonusCap");
                vacancyoffer.IncludeCandidateNotice = HelperMethods.GetBoolean(reader, "IncludeCandidateNotice");
                vacancyoffer.CandidateNoticePeriod = HelperMethods.GetInt32(reader, "CandidateNoticePeriod");
                vacancyoffer.CandidateNoticePeriodText = HelperMethods.GetString(reader, "CandidateNoticePeriodText");
                vacancyoffer.CandidateNoticePeriodType = HelperMethods.GetInt32(reader, "CandidateNoticePeriodType");
                vacancyoffer.CandidateNoticePeriodTypeText = HelperMethods.GetString(reader, "CandidateNoticePeriodTypeText");
                vacancyoffer.IncludeCompanyNotice = HelperMethods.GetBoolean(reader, "IncludeCompanyNotice");
                vacancyoffer.CompanyNoticePeriod = HelperMethods.GetInt32(reader, "CompanyNoticePeriod");
                vacancyoffer.CompanyNoticePeriodText = HelperMethods.GetString(reader, "CompanyNoticePeriodText");
                vacancyoffer.CompanyNoticePeriodType = HelperMethods.GetInt32(reader, "CompanyNoticePeriodType");
                vacancyoffer.CompanyNoticePeriodTypeText = HelperMethods.GetString(reader, "CompanyNoticePeriodTypeText");
                vacancyoffer.NoteToApplicant = HelperMethods.GetString(reader, "NoteToApplicant");
                vacancyoffer.ResponseFromApplicant = HelperMethods.GetString(reader, "ResponseFromApplicant");
                vacancyoffer.JobTitle = HelperMethods.GetString(reader, "JobTitle");
                vacancyoffer.CandidateName = HelperMethods.GetString(reader, "CandidateName");
                vacancyoffer.CandidateFirstName = HelperMethods.GetString(reader, "CandidateFirstName");
                vacancyoffer.CandidateLastName = HelperMethods.GetString(reader, "CandidateLastName");
                vacancyoffer.CandidateEmailId = HelperMethods.GetString(reader, "CandidateEmailId");
                vacancyoffer.CreatedByName = HelperMethods.GetString(reader, "CreatedByName");
                vacancyoffer.CreatedOn = HelperMethods.GetDateTime(reader, "CreatedOn");
                vacancyoffer.UpdatedByName = HelperMethods.GetString(reader, "UpdatedByName");
                vacancyoffer.UpdatedOn = HelperMethods.GetDateTime(reader, "UpdatedOn");
                vacancyoffer.PdfHeader = HelperMethods.GetString(reader, "PdfHeader");
                vacancyoffer.ManagerName = HelperMethods.GetString(reader, "ManagerName");
                return vacancyoffer;
            }
        }

        internal class GetVacancyOfferByVacRndIdFactory : IDomainObjectFactory<BEClient.VacancyOffer>
        {
            public BEClient.VacancyOffer Construct(IDataReader reader)
            {
                BEClient.VacancyOffer objVacancyOffer = new BEClient.VacancyOffer();
                objVacancyOffer.JobTitle = HelperMethods.GetString(reader, "JobTitle");
                objVacancyOffer.Location = new Guid(HelperMethods.GetString(reader, "Location"));
                objVacancyOffer.CandidateName = HelperMethods.GetString(reader, "CandidateName");
                objVacancyOffer.CandidateEmailId = HelperMethods.GetString(reader, "CandidateEmailId");
                objVacancyOffer.EmailToCandidateIdList = HelperMethods.GetString(reader, "EmailToCandidateId");
                objVacancyOffer.EmailToCandidate_EditContent = HelperMethods.GetBoolean(reader, "EmailToCandidate_EditContent");
                objVacancyOffer.OfferLetterIdList = HelperMethods.GetString(reader, "OfferLetterId");
                objVacancyOffer.OfferLetterId_EditContent = HelperMethods.GetBoolean(reader, "OfferLetterId_EditContent");
                objVacancyOffer.EnableCounterOffers = HelperMethods.GetBoolean(reader, "EnableCounterOffers");
                objVacancyOffer.PlacementType = HelperMethods.GetInt32(reader, "PlacementType").ToString();
                objVacancyOffer.PlacementType_EM = HelperMethods.GetBoolean(reader, "PlacementType_EM");
                objVacancyOffer.PlacementType_EC = HelperMethods.GetBoolean(reader, "PlacementType_EC");
                objVacancyOffer.JobType = HelperMethods.GetInt32(reader, "JobType");
                objVacancyOffer.JobType_EM = HelperMethods.GetBoolean(reader, "JobType_EM");
                objVacancyOffer.JobType_EC = HelperMethods.GetBoolean(reader, "JobType_EC");
                objVacancyOffer.SalaryType = HelperMethods.GetInt32(reader, "SalaryType");
                objVacancyOffer.Per = HelperMethods.GetString(reader, "Per");
                objVacancyOffer.SalaryType_EM = HelperMethods.GetBoolean(reader, "SalaryType_EM");
                objVacancyOffer.SalaryType_EC = HelperMethods.GetBoolean(reader, "SalaryType_EC");
                objVacancyOffer.SalaryOffered = HelperMethods.GetDecimal(reader, "SalaryOffered");
                objVacancyOffer.SalaryOfferedMax = HelperMethods.GetDecimal(reader, "SalaryOfferedMax");
                objVacancyOffer.SalaryOffered_EM = HelperMethods.GetBoolean(reader, "SalaryOffered_EM");
                objVacancyOffer.SalaryOffered_EC = HelperMethods.GetBoolean(reader, "SalaryOffered_EC");
                objVacancyOffer.HoursPerWeek = HelperMethods.GetInt32(reader, "HoursPerWeek");
                objVacancyOffer.HoursPerWeek_EM = HelperMethods.GetBoolean(reader, "HoursPerWeek_EM");
                objVacancyOffer.HoursPerWeek_EC = HelperMethods.GetBoolean(reader, "HoursPerWeek_EC");
                objVacancyOffer.HourlyWage = HelperMethods.GetDecimal(reader, "HourlyWage");
                objVacancyOffer.HourlyWageMax = HelperMethods.GetDecimal(reader, "HourlyWageMax");
                objVacancyOffer.HourlyWage_EM = HelperMethods.GetBoolean(reader, "HourlyWage_EM");
                objVacancyOffer.HourlyWage_EC = HelperMethods.GetBoolean(reader, "HourlyWage_EC");
                objVacancyOffer.Rate = HelperMethods.GetDecimal(reader, "Rate");
                objVacancyOffer.RateMax = HelperMethods.GetDecimal(reader, "RateMax");
                objVacancyOffer.Rate_EM = HelperMethods.GetBoolean(reader, "Rate_EM");
                objVacancyOffer.Rate_EC = HelperMethods.GetBoolean(reader, "Rate_EC");
                objVacancyOffer.Per = HelperMethods.GetString(reader, "Per");
                objVacancyOffer.Per_EM = HelperMethods.GetBoolean(reader, "Per_EM");
                objVacancyOffer.Per_EC = HelperMethods.GetBoolean(reader, "Per_EC");
                objVacancyOffer.PayRate = HelperMethods.GetDecimal(reader, "PayRate");
                objVacancyOffer.PayRateMax = HelperMethods.GetDecimal(reader, "PayRateMax");
                objVacancyOffer.PayRate_EM = HelperMethods.GetBoolean(reader, "PayRate_EM");
                objVacancyOffer.PayRate_EC = HelperMethods.GetBoolean(reader, "PayRate_EC");
                objVacancyOffer.RatePeriod = HelperMethods.GetInt32(reader, "RatePeriod");
                objVacancyOffer.RatePeriod_EM = HelperMethods.GetBoolean(reader, "RatePeriod_EM");
                objVacancyOffer.RatePeriod_EC = HelperMethods.GetBoolean(reader, "RatePeriod_EC");
                objVacancyOffer.PlusCommission = HelperMethods.GetBoolean(reader, "IncludeCommission");
                objVacancyOffer.CommissionDescriprion = HelperMethods.GetString(reader, "CommissionDescriprion");
                objVacancyOffer.CommissionDescriprion_EM = HelperMethods.GetBoolean(reader, "CommissionDescriprion_EM");
                objVacancyOffer.AnnualCommissionPotential = HelperMethods.GetDecimal(reader, "AnnualCommissionPotential");
                objVacancyOffer.AnnualCommissionPotentialMax = HelperMethods.GetDecimal(reader, "AnnualCommissionPotentialMax");
                objVacancyOffer.AnnualCommissionPotential_EM = HelperMethods.GetBoolean(reader, "AnnualCommissionPotential_EM");
                objVacancyOffer.AnnualCommissionPotential_EC = HelperMethods.GetBoolean(reader, "AnnualCommissionPotential_EC");
                objVacancyOffer.CommissionCap = HelperMethods.GetDecimal(reader, "CommissionCap");
                objVacancyOffer.CommissionCapMax = HelperMethods.GetDecimal(reader, "CommissionCapMax");
                objVacancyOffer.CommissionCap_EM = HelperMethods.GetBoolean(reader, "CommissionCap_EM");
                objVacancyOffer.CommissionCap_EC = HelperMethods.GetBoolean(reader, "CommissionCap_EC");
                objVacancyOffer.PlusBonus = HelperMethods.GetBoolean(reader, "IncludeBonus");
                objVacancyOffer.BonusDescription = HelperMethods.GetString(reader, "BonusDescription");
                objVacancyOffer.BonusDescription_EM = HelperMethods.GetBoolean(reader, "BonusDescription_EM");
                objVacancyOffer.AnnualBonusPotential = HelperMethods.GetDecimal(reader, "AnnualBonusPotential");
                objVacancyOffer.AnnualBonusPotentialMax = HelperMethods.GetDecimal(reader, "AnnualBonusPotentialMax");
                objVacancyOffer.AnnualBonusPotential_EM = HelperMethods.GetBoolean(reader, "AnnualBonusPotential_EM");
                objVacancyOffer.AnnualBonusPotential_EC = HelperMethods.GetBoolean(reader, "AnnualBonusPotential_EC");
                objVacancyOffer.BonusCap = HelperMethods.GetDecimal(reader, "BonusCap");
                objVacancyOffer.BonusCapMax = HelperMethods.GetDecimal(reader, "BonusCapMax");
                objVacancyOffer.BonusCap_EM = HelperMethods.GetBoolean(reader, "BonusCap_EM");
                objVacancyOffer.BonusCap_EC = HelperMethods.GetBoolean(reader, "BonusCap_EC");
                objVacancyOffer.IncludeCandidateNotice = HelperMethods.GetBoolean(reader, "IncludeCandidateNotice");
                objVacancyOffer.CandidateNoticePeriod = HelperMethods.GetInt32(reader, "CandidateNoticePeriod");
                objVacancyOffer.CandidateNoticePeriod_EM = HelperMethods.GetBoolean(reader, "CandidateNoticePeriod_EM");
                objVacancyOffer.CandidateNoticePeriod_EC = HelperMethods.GetBoolean(reader, "CandidateNoticePeriod_EC");
                objVacancyOffer.CandidateNoticePeriodType = HelperMethods.GetInt32(reader, "CandidateNoticePeriodType");
                objVacancyOffer.CandidateNoticePeriodType_EM = HelperMethods.GetBoolean(reader, "CandidateNoticePeriodType_EM");
                objVacancyOffer.CandidateNoticePeriodType_EC = HelperMethods.GetBoolean(reader, "CandidateNoticePeriodType_EC");
                objVacancyOffer.IncludeCompanyNotice = HelperMethods.GetBoolean(reader, "IncludeCompanyNotice");
                objVacancyOffer.CompanyNoticePeriod = HelperMethods.GetInt32(reader, "CompanyNoticePeriod");
                objVacancyOffer.CompanyNoticePeriod_EM = HelperMethods.GetBoolean(reader, "CompanyNoticePeriod_EM");
                objVacancyOffer.CompanyNoticePeriod_EC = HelperMethods.GetBoolean(reader, "CompanyNoticePeriod_EC");
                objVacancyOffer.CompanyNoticePeriodType = HelperMethods.GetInt32(reader, "CompanyNoticePeriodType");
                objVacancyOffer.CompanyNoticePeriodType_EM = HelperMethods.GetBoolean(reader, "CompanyNoticePeriodType_EM");
                objVacancyOffer.CompanyNoticePeriodType_EC = HelperMethods.GetBoolean(reader, "CompanyNoticePeriodType_EC");
                objVacancyOffer.CanMakeOffer = HelperMethods.GetBoolean(reader, "CanMakeOffer");
                objVacancyOffer.CanEditOffer = HelperMethods.GetBoolean(reader, "CanEditOffer");
                objVacancyOffer.OfferTemplateId = HelperMethods.GetGuid(reader, "OfferTemplateId");
                objVacancyOffer.IncludeAttachments = HelperMethods.GetBoolean(reader, "IncludeAttachment");

                ////Used in GV
                objVacancyOffer.AwardAmount = HelperMethods.GetDecimal(reader, "AwardAmount");
                objVacancyOffer.AwardAmountMax = HelperMethods.GetDecimal(reader, "AwardAmountMax");
                objVacancyOffer.AwardAmount_EM = HelperMethods.GetBoolean(reader, "AwardAmount_EM");
                objVacancyOffer.AwardAmount_EC = HelperMethods.GetBoolean(reader, "AwardAmount_EC");
                objVacancyOffer.AwardIssueDate = HelperMethods.GetDateTime(reader, "AwardIssueDate");
                objVacancyOffer.CashMatchRequired = HelperMethods.GetDecimal(reader, "CashMatchRequired");
                objVacancyOffer.CashMatchRequiredMax = HelperMethods.GetDecimal(reader, "CashMatchRequiredMax");
                objVacancyOffer.CashMatchRequired_EM = HelperMethods.GetBoolean(reader, "CashMatchRequired_EM");
                objVacancyOffer.CashMatchRequired_EC = HelperMethods.GetBoolean(reader, "CashMatchRequired_EC");

                return objVacancyOffer;
            }
        }

    }
}
