using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;

namespace ATS.DataAccess
{
    public class OfferTemplatesRepository : Repository<BEClient.OfferTemplates>
    {
        public OfferTemplatesRepository(string ConnectionString)
            : base(ConnectionString)
        {

        }

        public Guid AddOfferTemplate(BEClient.OfferTemplates offertemplates)
        {

            try
            {
                Guid reREsult = Guid.Empty;
                DbCommand command = Database.GetStoredProcCommand("InsertOfferTemplates");
                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@OfferTemplateName", DbType.String, offertemplates.OfferTemplateName);
                Database.AddInParameter(command, "@PositionType", DbType.Guid, offertemplates.PositionType);
                Database.AddInParameter(command, "@EnableCounterOffers", DbType.Boolean, offertemplates.EnableCounterOffers);
                Database.AddInParameter(command, "@EmailToCandidateId", DbType.String, offertemplates.EmailToCandidateId);
                Database.AddInParameter(command, "@EmailToCandidate_EditSelection", DbType.Boolean, offertemplates.EmailToCandidate_EditSelection);
                Database.AddInParameter(command, "@EmailToCandidate_EditContent", DbType.Boolean, offertemplates.EmailToCandidate_EditContent);
                Database.AddInParameter(command, "@OfferLetterId", DbType.String, offertemplates.OfferLetterId);
                Database.AddInParameter(command, "@OfferLetterId_EditSelection", DbType.Boolean, offertemplates.OfferLetterId_EditSelection);
                Database.AddInParameter(command, "@OfferLetterId_EditContent", DbType.Boolean, offertemplates.OfferLetterId_EditContent);
                Database.AddInParameter(command, "@PlacementType", DbType.Int32, offertemplates.PlacementType);
                Database.AddInParameter(command, "@PlacementType_EM", DbType.Boolean, offertemplates.PlacementType_EM);
                Database.AddInParameter(command, "@PlacementType_EC", DbType.Boolean, offertemplates.PlacementType_EC);
                Database.AddInParameter(command, "@JobType", DbType.Int32, offertemplates.JobType);
                Database.AddInParameter(command, "@JobType_EM", DbType.Boolean, offertemplates.JobType_EM);
                Database.AddInParameter(command, "@JobType_EC", DbType.Boolean, offertemplates.JobType_EC);
                Database.AddInParameter(command, "@SalaryType", DbType.Int32, offertemplates.SalaryType);
                Database.AddInParameter(command, "@SalaryType_EM", DbType.Boolean, offertemplates.SalaryType_EM);
                Database.AddInParameter(command, "@SalaryType_EC", DbType.Boolean, offertemplates.SalaryType_EC);
                Database.AddInParameter(command, "@SalaryOffered", DbType.Decimal, offertemplates.SalaryOffered);
                Database.AddInParameter(command, "@SalaryOfferedMax", DbType.Decimal, offertemplates.SalaryOfferedMax);
                Database.AddInParameter(command, "@SalaryOffered_EM", DbType.Boolean, offertemplates.SalaryOffered_EM);
                Database.AddInParameter(command, "@SalaryOffered_EC", DbType.Boolean, offertemplates.SalaryOffered_EC);
                Database.AddInParameter(command, "@HoursPerWeek", DbType.Int32, offertemplates.HoursPerWeek);
                Database.AddInParameter(command, "@HoursPerWeek_EM", DbType.Boolean, offertemplates.HoursPerWeek_EM);
                Database.AddInParameter(command, "@HoursPerWeek_EC", DbType.Boolean, offertemplates.HoursPerWeek_EC);
                Database.AddInParameter(command, "@HourlyWage", DbType.Decimal, offertemplates.HourlyWage);
                Database.AddInParameter(command, "@HourlyWageMax", DbType.Decimal, offertemplates.HourlyWageMax);
                Database.AddInParameter(command, "@HourlyWage_EM", DbType.Boolean, offertemplates.HourlyWage_EM);
                Database.AddInParameter(command, "@HourlyWage_EC", DbType.Boolean, offertemplates.HourlyWage_EC);
                Database.AddInParameter(command, "@Rate", DbType.Decimal, offertemplates.Rate);
                Database.AddInParameter(command, "@RateMax", DbType.Decimal, offertemplates.RateMax);
                Database.AddInParameter(command, "@Rate_EM", DbType.Boolean, offertemplates.Rate_EM);
                Database.AddInParameter(command, "@Rate_EC", DbType.Boolean, offertemplates.Rate_EC);
                Database.AddInParameter(command, "@Per", DbType.String, offertemplates.Per);
                Database.AddInParameter(command, "@Per_EM", DbType.Boolean, offertemplates.Per_EM);
                Database.AddInParameter(command, "@Per_EC", DbType.Boolean, offertemplates.Per_EC);
                Database.AddInParameter(command, "@ChargeRate", DbType.Decimal, offertemplates.ChargeRate);
                Database.AddInParameter(command, "@ChargeRateMax", DbType.Decimal, offertemplates.ChargeRateMax);
                Database.AddInParameter(command, "@ChargeRate_EM", DbType.Boolean, offertemplates.ChargeRate_EM);
                Database.AddInParameter(command, "@ChargeRate_EC", DbType.Boolean, offertemplates.ChargeRate_EC);
                Database.AddInParameter(command, "@PayRate", DbType.Decimal, offertemplates.PayRate);
                Database.AddInParameter(command, "@PayRateMax", DbType.Decimal, offertemplates.PayRateMax);
                Database.AddInParameter(command, "@PayRate_EM", DbType.Boolean, offertemplates.PayRate_EM);
                Database.AddInParameter(command, "@PayRate_EC", DbType.Boolean, offertemplates.PayRate_EC);
                Database.AddInParameter(command, "@RatePeriod", DbType.Int32, offertemplates.RatePeriod);
                Database.AddInParameter(command, "@RatePeriod_EM", DbType.Boolean, offertemplates.RatePeriod_EM);
                Database.AddInParameter(command, "@RatePeriod_EC", DbType.Boolean, offertemplates.RatePeriod_EC);
                Database.AddInParameter(command, "@IncludeCommission", DbType.Boolean, offertemplates.IncludeCommission);
                Database.AddInParameter(command, "@CommissionDescriprion", DbType.String, offertemplates.CommissionDescriprion);
                Database.AddInParameter(command, "@CommissionDescriprion_EM", DbType.Boolean, offertemplates.CommissionDescriprion_EM);
                Database.AddInParameter(command, "@AnnualCommissionPotential", DbType.Decimal, offertemplates.AnnualCommissionPotential);
                Database.AddInParameter(command, "@AnnualCommissionPotentialMax", DbType.Decimal, offertemplates.AnnualCommissionPotentialMax);
                Database.AddInParameter(command, "@AnnualCommissionPotential_EM", DbType.Boolean, offertemplates.AnnualCommissionPotential_EM);
                Database.AddInParameter(command, "@AnnualCommissionPotential_EC", DbType.Boolean, offertemplates.AnnualCommissionPotential_EC);
                Database.AddInParameter(command, "@CommissionCap", DbType.Decimal, offertemplates.CommissionCap);
                Database.AddInParameter(command, "@CommissionCapMax", DbType.Decimal, offertemplates.CommissionCapMax);
                Database.AddInParameter(command, "@CommissionCap_EM", DbType.Boolean, offertemplates.CommissionCap_EM);
                Database.AddInParameter(command, "@CommissionCap_EC", DbType.Boolean, offertemplates.CommissionCap_EC);
                Database.AddInParameter(command, "@IncludeBonus", DbType.Boolean, offertemplates.IncludeBonus);
                Database.AddInParameter(command, "@BonusDescription", DbType.String, offertemplates.BonusDescription);
                Database.AddInParameter(command, "@BonusDescription_EM", DbType.Boolean, offertemplates.BonusDescription_EM);
                Database.AddInParameter(command, "@AnnualBonusPotential", DbType.Decimal, offertemplates.AnnualBonusPotential);
                Database.AddInParameter(command, "@AnnualBonusPotentialMax", DbType.Decimal, offertemplates.AnnualBonusPotentialMax);
                Database.AddInParameter(command, "@AnnualBonusPotential_EM", DbType.Boolean, offertemplates.AnnualBonusPotential_EM);
                Database.AddInParameter(command, "@AnnualBonusPotential_EC", DbType.Boolean, offertemplates.AnnualBonusPotential_EC);
                Database.AddInParameter(command, "@BonusCap", DbType.Decimal, offertemplates.BonusCap);
                Database.AddInParameter(command, "@BonusCapMax", DbType.Decimal, offertemplates.BonusCapMax);
                Database.AddInParameter(command, "@BonusCap_EM", DbType.Boolean, offertemplates.BonusCap_EM);
                Database.AddInParameter(command, "@BonusCap_EC", DbType.Boolean, offertemplates.BonusCap_EC);
                Database.AddInParameter(command, "@IncludeCandidateNotice", DbType.Boolean, offertemplates.IncludeCandidateNotice);
                Database.AddInParameter(command, "@CandidateNoticePeriod", DbType.Int32, offertemplates.CandidateNoticePeriod);
                Database.AddInParameter(command, "@CandidateNoticePeriod_EM", DbType.Boolean, offertemplates.CandidateNoticePeriod_EM);
                Database.AddInParameter(command, "@CandidateNoticePeriod_EC", DbType.Boolean, offertemplates.CandidateNoticePeriod_EC);
                Database.AddInParameter(command, "@CandidateNoticePeriodType", DbType.Int32, offertemplates.CandidateNoticePeriodType);
                Database.AddInParameter(command, "@CandidateNoticePeriodType_EM", DbType.Boolean, offertemplates.CandidateNoticePeriodType_EM);
                Database.AddInParameter(command, "@CandidateNoticePeriodType_EC", DbType.Boolean, offertemplates.CandidateNoticePeriodType_EC);
                Database.AddInParameter(command, "@IncludeCompanyNotice", DbType.Boolean, offertemplates.IncludeCompanyNotice);
                Database.AddInParameter(command, "@CompanyNoticePeriod", DbType.Int32, offertemplates.CompanyNoticePeriod);
                Database.AddInParameter(command, "@CompanyNoticePeriod_EM", DbType.Boolean, offertemplates.CompanyNoticePeriod_EM);
                Database.AddInParameter(command, "@CompanyNoticePeriod_EC", DbType.Boolean, offertemplates.CompanyNoticePeriod_EC);
                Database.AddInParameter(command, "@CompanyNoticePeriodType", DbType.Int32, offertemplates.CompanyNoticePeriodType);
                Database.AddInParameter(command, "@CompanyNoticePeriodType_EM", DbType.Boolean, offertemplates.CompanyNoticePeriodType_EM);
                Database.AddInParameter(command, "@CompanyNoticePeriodType_EC", DbType.Boolean, offertemplates.CompanyNoticePeriodType_EC);
                Database.AddInParameter(command, "@IncludeAttachment", DbType.Boolean, offertemplates.IncludeAttachments);
                ////Used in GV
                Database.AddInParameter(command, "@AwardAmount", DbType.Decimal, offertemplates.AwardAmount);
                Database.AddInParameter(command, "@AwardAmountMax", DbType.Decimal, offertemplates.AwardAmountMax);
                Database.AddInParameter(command, "@AwardAmount_EM", DbType.Boolean, offertemplates.AwardAmount_EM);
                Database.AddInParameter(command, "@AwardAmount_EC", DbType.Boolean, offertemplates.AwardAmount_EC);
                Database.AddInParameter(command, "@AwardIssueDate", DbType.DateTime, offertemplates.AwardIssueDate);
                Database.AddInParameter(command, "@CashMatchRequired", DbType.Decimal, offertemplates.CashMatchRequired);
                Database.AddInParameter(command, "@CashMatchRequiredMax", DbType.Decimal, offertemplates.CashMatchRequiredMax);
                Database.AddInParameter(command, "@CashMatchRequired_EM", DbType.Boolean, offertemplates.CashMatchRequired_EM);
                Database.AddInParameter(command, "@CashMatchRequired_EC", DbType.Boolean, offertemplates.CashMatchRequired_EC);

                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, offertemplates.IsDelete);
                Database.AddOutParameter(command, "OfferTemplateId", DbType.Guid, 16);
                var result = base.Add(command, "OfferTemplateId");
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

        public bool UpdateOffertemplate(BEClient.OfferTemplates offertemplates)
        {
            try
            {
                bool reREsult = false;
                DbCommand command = Database.GetStoredProcCommand("UpdateOfferTemplates");

                command.CommandTimeout = 100;
                Database.AddInParameter(command, "@OfferTemplateId", DbType.Guid, offertemplates.OfferTemplateId);
                Database.AddInParameter(command, "@OfferTemplateName", DbType.String, offertemplates.OfferTemplateName);
                Database.AddInParameter(command, "@PositionType", DbType.Guid, offertemplates.PositionType);
                Database.AddInParameter(command, "@EnableCounterOffers", DbType.Boolean, offertemplates.EnableCounterOffers);
                Database.AddInParameter(command, "@EmailToCandidateId", DbType.String, offertemplates.EmailToCandidateId);
                Database.AddInParameter(command, "@EmailToCandidate_EditSelection", DbType.Boolean, offertemplates.EmailToCandidate_EditSelection);
                Database.AddInParameter(command, "@EmailToCandidate_EditContent", DbType.Boolean, offertemplates.EmailToCandidate_EditContent);
                Database.AddInParameter(command, "@OfferLetterId", DbType.String, offertemplates.OfferLetterId);
                Database.AddInParameter(command, "@OfferLetterId_EditSelection", DbType.Boolean, offertemplates.OfferLetterId_EditSelection);
                Database.AddInParameter(command, "@OfferLetterId_EditContent", DbType.Boolean, offertemplates.OfferLetterId_EditContent);
                Database.AddInParameter(command, "@PlacementType", DbType.Int32, offertemplates.PlacementType);
                Database.AddInParameter(command, "@PlacementType_EM", DbType.Boolean, offertemplates.PlacementType_EM);
                Database.AddInParameter(command, "@PlacementType_EC", DbType.Boolean, offertemplates.PlacementType_EC);
                Database.AddInParameter(command, "@JobType", DbType.Int32, offertemplates.JobType);
                Database.AddInParameter(command, "@JobType_EM", DbType.Boolean, offertemplates.JobType_EM);
                Database.AddInParameter(command, "@JobType_EC", DbType.Boolean, offertemplates.JobType_EC);
                Database.AddInParameter(command, "@SalaryType", DbType.Int32, offertemplates.SalaryType);
                Database.AddInParameter(command, "@SalaryType_EM", DbType.Boolean, offertemplates.SalaryType_EM);
                Database.AddInParameter(command, "@SalaryType_EC", DbType.Boolean, offertemplates.SalaryType_EC);
                Database.AddInParameter(command, "@SalaryOffered", DbType.Decimal, offertemplates.SalaryOffered);
                Database.AddInParameter(command, "@SalaryOfferedMax", DbType.Decimal, offertemplates.SalaryOfferedMax);
                Database.AddInParameter(command, "@SalaryOffered_EM", DbType.Boolean, offertemplates.SalaryOffered_EM);
                Database.AddInParameter(command, "@SalaryOffered_EC", DbType.Boolean, offertemplates.SalaryOffered_EC);
                Database.AddInParameter(command, "@HoursPerWeek", DbType.Int32, offertemplates.HoursPerWeek);
                Database.AddInParameter(command, "@HoursPerWeek_EM", DbType.Boolean, offertemplates.HoursPerWeek_EM);
                Database.AddInParameter(command, "@HoursPerWeek_EC", DbType.Boolean, offertemplates.HoursPerWeek_EC);
                Database.AddInParameter(command, "@HourlyWage", DbType.Decimal, offertemplates.HourlyWage);
                Database.AddInParameter(command, "@HourlyWageMax", DbType.Decimal, offertemplates.HourlyWageMax);
                Database.AddInParameter(command, "@HourlyWage_EM", DbType.Boolean, offertemplates.HourlyWage_EM);
                Database.AddInParameter(command, "@HourlyWage_EC", DbType.Boolean, offertemplates.HourlyWage_EC);
                Database.AddInParameter(command, "@Rate", DbType.Decimal, offertemplates.Rate);
                Database.AddInParameter(command, "@RateMax", DbType.Decimal, offertemplates.RateMax);
                Database.AddInParameter(command, "@Rate_EM", DbType.Boolean, offertemplates.Rate_EM);
                Database.AddInParameter(command, "@Rate_EC", DbType.Boolean, offertemplates.Rate_EC);
                Database.AddInParameter(command, "@Per", DbType.String, offertemplates.Per);
                Database.AddInParameter(command, "@Per_EM", DbType.Boolean, offertemplates.Per_EM);
                Database.AddInParameter(command, "@Per_EC", DbType.Boolean, offertemplates.Per_EC);
                Database.AddInParameter(command, "@ChargeRate", DbType.Decimal, offertemplates.ChargeRate);
                Database.AddInParameter(command, "@ChargeRateMax", DbType.Decimal, offertemplates.ChargeRateMax);
                Database.AddInParameter(command, "@ChargeRate_EM", DbType.Boolean, offertemplates.ChargeRate_EM);
                Database.AddInParameter(command, "@ChargeRate_EC", DbType.Boolean, offertemplates.ChargeRate_EC);
                Database.AddInParameter(command, "@PayRate", DbType.Decimal, offertemplates.PayRate);
                Database.AddInParameter(command, "@PayRateMax", DbType.Decimal, offertemplates.PayRateMax);
                Database.AddInParameter(command, "@PayRate_EM", DbType.Boolean, offertemplates.PayRate_EM);
                Database.AddInParameter(command, "@PayRate_EC", DbType.Boolean, offertemplates.PayRate_EC);

                Database.AddInParameter(command, "@RatePeriod", DbType.Int32, offertemplates.RatePeriod);
                Database.AddInParameter(command, "@RatePeriod_EM", DbType.Boolean, offertemplates.RatePeriod_EM);
                Database.AddInParameter(command, "@RatePeriod_EC", DbType.Boolean, offertemplates.RatePeriod_EC);

                Database.AddInParameter(command, "@IncludeCommission", DbType.Boolean, offertemplates.IncludeCommission);
                Database.AddInParameter(command, "@CommissionDescriprion", DbType.String, offertemplates.CommissionDescriprion);
                Database.AddInParameter(command, "@CommissionDescriprion_EM", DbType.Boolean, offertemplates.CommissionDescriprion_EM);
                Database.AddInParameter(command, "@AnnualCommissionPotential", DbType.Decimal, offertemplates.AnnualCommissionPotential);
                Database.AddInParameter(command, "@AnnualCommissionPotentialMax", DbType.Decimal, offertemplates.AnnualCommissionPotentialMax);
                Database.AddInParameter(command, "@AnnualCommissionPotential_EM", DbType.Boolean, offertemplates.AnnualCommissionPotential_EM);
                Database.AddInParameter(command, "@AnnualCommissionPotential_EC", DbType.Boolean, offertemplates.AnnualCommissionPotential_EC);
                Database.AddInParameter(command, "@CommissionCap", DbType.Decimal, offertemplates.CommissionCap);
                Database.AddInParameter(command, "@CommissionCapMax", DbType.Decimal, offertemplates.CommissionCapMax);
                Database.AddInParameter(command, "@CommissionCap_EM", DbType.Boolean, offertemplates.CommissionCap_EM);
                Database.AddInParameter(command, "@CommissionCap_EC", DbType.Boolean, offertemplates.CommissionCap_EC);
                Database.AddInParameter(command, "@IncludeBonus", DbType.Boolean, offertemplates.IncludeBonus);
                Database.AddInParameter(command, "@BonusDescription", DbType.String, offertemplates.BonusDescription);
                Database.AddInParameter(command, "@BonusDescription_EM", DbType.Boolean, offertemplates.BonusDescription_EM);
                Database.AddInParameter(command, "@AnnualBonusPotential", DbType.Decimal, offertemplates.AnnualBonusPotential);
                Database.AddInParameter(command, "@AnnualBonusPotentialMax", DbType.Decimal, offertemplates.AnnualBonusPotentialMax);
                Database.AddInParameter(command, "@AnnualBonusPotential_EM", DbType.Boolean, offertemplates.AnnualBonusPotential_EM);
                Database.AddInParameter(command, "@AnnualBonusPotential_EC", DbType.Boolean, offertemplates.AnnualBonusPotential_EC);
                Database.AddInParameter(command, "@BonusCap", DbType.Decimal, offertemplates.BonusCap);
                Database.AddInParameter(command, "@BonusCapMax", DbType.Decimal, offertemplates.BonusCapMax);
                Database.AddInParameter(command, "@BonusCap_EM", DbType.Boolean, offertemplates.BonusCap_EM);
                Database.AddInParameter(command, "@BonusCap_EC", DbType.Boolean, offertemplates.BonusCap_EC);
                Database.AddInParameter(command, "@IncludeCandidateNotice", DbType.Boolean, offertemplates.IncludeCandidateNotice);
                Database.AddInParameter(command, "@CandidateNoticePeriod", DbType.Int32, offertemplates.CandidateNoticePeriod);
                Database.AddInParameter(command, "@CandidateNoticePeriod_EM", DbType.Boolean, offertemplates.CandidateNoticePeriod_EM);
                Database.AddInParameter(command, "@CandidateNoticePeriod_EC", DbType.Boolean, offertemplates.CandidateNoticePeriod_EC);
                Database.AddInParameter(command, "@CandidateNoticePeriodType", DbType.Int32, offertemplates.CandidateNoticePeriodType);
                Database.AddInParameter(command, "@CandidateNoticePeriodType_EM", DbType.Boolean, offertemplates.CandidateNoticePeriodType_EM);
                Database.AddInParameter(command, "@CandidateNoticePeriodType_EC", DbType.Boolean, offertemplates.CandidateNoticePeriodType_EC);
                Database.AddInParameter(command, "@IncludeCompanyNotice", DbType.Boolean, offertemplates.IncludeCompanyNotice);
                Database.AddInParameter(command, "@CompanyNoticePeriod", DbType.Int32, offertemplates.CompanyNoticePeriod);
                Database.AddInParameter(command, "@CompanyNoticePeriod_EM", DbType.Boolean, offertemplates.CompanyNoticePeriod_EM);
                Database.AddInParameter(command, "@CompanyNoticePeriod_EC", DbType.Boolean, offertemplates.CompanyNoticePeriod_EC);
                Database.AddInParameter(command, "@CompanyNoticePeriodType", DbType.Int32, offertemplates.CompanyNoticePeriodType);
                Database.AddInParameter(command, "@CompanyNoticePeriodType_EM", DbType.Boolean, offertemplates.CompanyNoticePeriodType_EC);
                Database.AddInParameter(command, "@CompanyNoticePeriodType_EC", DbType.Boolean, offertemplates.CompanyNoticePeriodType_EC);

                ////Used in GV
                Database.AddInParameter(command, "@AwardAmount", DbType.Decimal, offertemplates.AwardAmount);
                Database.AddInParameter(command, "@AwardAmountMax", DbType.Decimal, offertemplates.AwardAmountMax);
                Database.AddInParameter(command, "@AwardAmount_EM", DbType.Boolean, offertemplates.AwardAmount_EM);
                Database.AddInParameter(command, "@AwardAmount_EC", DbType.Boolean, offertemplates.AwardAmount_EC);
                Database.AddInParameter(command, "@AwardIssueDate", DbType.DateTime, offertemplates.AwardIssueDate);
                Database.AddInParameter(command, "@CashMatchRequired", DbType.Decimal, offertemplates.CashMatchRequired);
                Database.AddInParameter(command, "@CashMatchRequiredMax", DbType.Decimal, offertemplates.CashMatchRequiredMax);
                Database.AddInParameter(command, "@CashMatchRequired_EM", DbType.Boolean, offertemplates.CashMatchRequired_EM);
                Database.AddInParameter(command, "@CashMatchRequired_EC", DbType.Boolean, offertemplates.CashMatchRequired_EC);


                Database.AddInParameter(command, "@IncludeAttachment", DbType.Boolean, offertemplates.IncludeAttachments);
                Database.AddInParameter(command, "@IsDelete", DbType.Boolean, offertemplates.IsDelete);
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

        public BEClient.OfferTemplates GetOfferTemplateById(Guid offerTemplateId)
        {
            try
            {
                try
                {
                    DbCommand command = Database.GetStoredProcCommand("GetOfferTemplateById");
                    Database.AddInParameter(command, "@OfferTemplateId", DbType.Guid, offerTemplateId);
                    return base.FindOne(command, new GetOfferTemplateByIdFactory());
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

        public BEClient.OfferTemplates GetOfferTemplateByVacRndId(Guid VacRndId)
        {
            try
            {
                try
                {
                    DbCommand command = Database.GetStoredProcCommand("GetOfferTemplateByVAcRndId");
                    Database.AddInParameter(command, "@VacRndId", DbType.Guid, VacRndId);
                    return base.FindOne(command, new GetOfferTemplateByVacRndIdFactory(), false);
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

        public List<BEClient.OfferTemplates> GetAllOfferTemplate(string VacancyId, string TVacId, string UsersDivisionList)
        {
            try
            {
                try
                {
                    DbCommand command = Database.GetStoredProcCommand("GetAllOfferTemplates");
                    if (String.IsNullOrEmpty(VacancyId))
                    {
                        Database.AddInParameter(command, "@VacancyId", DbType.Guid, DBNull.Value);
                    }
                    else
                    {
                        Database.AddInParameter(command, "@VacancyId", DbType.Guid, new Guid(VacancyId));
                    }

                    if (String.IsNullOrEmpty(TVacId))
                    {
                        Database.AddInParameter(command, "@TVacId", DbType.Guid, DBNull.Value);
                    }
                    else
                    {
                        Database.AddInParameter(command, "@TVacId", DbType.Guid, new Guid(TVacId));
                    }
                    if (!String.IsNullOrEmpty(UsersDivisionList))
                    {
                        Database.AddInParameter(command, "@UsersDivisionList", DbType.String, UsersDivisionList);
                    }
                    return base.Find(command, new GetAllOfferTemplateFactory());
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

        public bool DeleteOfferTemplate(Guid offerTemplateId)
        {
            try
            {
                DbCommand command = Database.GetStoredProcCommand("DeleteOfferTemplate");
                Database.AddInParameter(command, "@OfferTemplateId", DbType.Guid, offerTemplateId);
                var Result = base.Remove(command);
                return HelperMethods.GetBoolean(Result);
            }
            catch
            {
                throw;
            }
        }

        internal class GetOfferTemplateByIdFactory : IDomainObjectFactory<BEClient.OfferTemplates>
        {
            public BEClient.OfferTemplates Construct(IDataReader reader)
            {
                BEClient.OfferTemplates offerTemplates = new BEClient.OfferTemplates();
                offerTemplates.OfferTemplateId = HelperMethods.GetGuid(reader, "OfferTemplateId");
                offerTemplates.OfferTemplateName = HelperMethods.GetString(reader, "OfferTemplateName");
                offerTemplates.PositionType = HelperMethods.GetGuid(reader, "PositionType");
                offerTemplates.EnableCounterOffers = HelperMethods.GetBoolean(reader, "EnableCounterOffers");
                offerTemplates.EmailToCandidateId = HelperMethods.GetString(reader, "EmailToCandidateId");
                offerTemplates.EmailToCandidate_EditSelection = HelperMethods.GetBoolean(reader, "EmailToCandidate_EditSelection");
                offerTemplates.EmailToCandidate_EditContent = HelperMethods.GetBoolean(reader, "EmailToCandidate_EditContent");
                offerTemplates.OfferLetterId = HelperMethods.GetString(reader, "OfferLetterId");
                offerTemplates.OfferLetterId_EditSelection = HelperMethods.GetBoolean(reader, "OfferLetterId_EditSelection");
                offerTemplates.OfferLetterId_EditContent = HelperMethods.GetBoolean(reader, "OfferLetterId_EditContent");
                offerTemplates.PlacementType = HelperMethods.GetInt32(reader, "PlacementType");
                offerTemplates.PlacementType_EM = HelperMethods.GetBoolean(reader, "PlacementType_EM");
                offerTemplates.PlacementType_EC = HelperMethods.GetBoolean(reader, "PlacementType_EC");
                offerTemplates.JobType = HelperMethods.GetInt32(reader, "JobType");
                offerTemplates.JobType_EM = HelperMethods.GetBoolean(reader, "JobType_EM");
                offerTemplates.JobType_EC = HelperMethods.GetBoolean(reader, "JobType_EC");
                offerTemplates.SalaryType = HelperMethods.GetInt32(reader, "SalaryType");
                offerTemplates.Per = HelperMethods.GetString(reader, "Per");
                offerTemplates.SalaryType_EM = HelperMethods.GetBoolean(reader, "SalaryType_EM");
                offerTemplates.SalaryType_EC = HelperMethods.GetBoolean(reader, "SalaryType_EC");
                offerTemplates.SalaryOffered = HelperMethods.GetDecimal(reader, "SalaryOffered");
                offerTemplates.SalaryOfferedMax = HelperMethods.GetDecimal(reader, "SalaryOfferedMax");
                offerTemplates.SalaryOffered_EM = HelperMethods.GetBoolean(reader, "SalaryOffered_EM");
                offerTemplates.SalaryOffered_EC = HelperMethods.GetBoolean(reader, "SalaryOffered_EC");
                offerTemplates.HoursPerWeek = HelperMethods.GetInt32(reader, "HoursPerWeek");
                offerTemplates.HoursPerWeek_EM = HelperMethods.GetBoolean(reader, "HoursPerWeek_EM");
                offerTemplates.HoursPerWeek_EC = HelperMethods.GetBoolean(reader, "HoursPerWeek_EC");
                offerTemplates.HourlyWage = HelperMethods.GetDecimal(reader, "HourlyWage");
                offerTemplates.HourlyWageMax = HelperMethods.GetDecimal(reader, "HourlyWageMax");
                offerTemplates.HourlyWage_EM = HelperMethods.GetBoolean(reader, "HourlyWage_EM");
                offerTemplates.HourlyWage_EC = HelperMethods.GetBoolean(reader, "HourlyWage_EC");
                offerTemplates.Rate = HelperMethods.GetDecimal(reader, "Rate");
                offerTemplates.RateMax = HelperMethods.GetDecimal(reader, "RateMax");
                offerTemplates.Rate_EM = HelperMethods.GetBoolean(reader, "Rate_EM");
                offerTemplates.Rate_EC = HelperMethods.GetBoolean(reader, "Rate_EC");
                offerTemplates.Per = HelperMethods.GetString(reader, "Per");
                offerTemplates.Per_EM = HelperMethods.GetBoolean(reader, "Per_EM");
                offerTemplates.Per_EC = HelperMethods.GetBoolean(reader, "Per_EC");
                offerTemplates.ChargeRate = HelperMethods.GetDecimal(reader, "ChargeRate");
                offerTemplates.ChargeRateMax = HelperMethods.GetDecimal(reader, "ChargeRateMax");
                offerTemplates.ChargeRate_EM = HelperMethods.GetBoolean(reader, "ChargeRate_EM");
                offerTemplates.ChargeRate_EC = HelperMethods.GetBoolean(reader, "ChargeRate_EC");
                offerTemplates.PayRate = HelperMethods.GetDecimal(reader, "PayRate");
                offerTemplates.PayRateMax = HelperMethods.GetDecimal(reader, "PayRateMax");
                offerTemplates.PayRate_EM = HelperMethods.GetBoolean(reader, "PayRate_EM");
                offerTemplates.PayRate_EC = HelperMethods.GetBoolean(reader, "PayRate_EC");
                offerTemplates.RatePeriod = HelperMethods.GetInt32(reader, "RatePeriod");
                offerTemplates.RatePeriod_EM = HelperMethods.GetBoolean(reader, "RatePeriod_EM");
                offerTemplates.RatePeriod_EC = HelperMethods.GetBoolean(reader, "RatePeriod_EC");
                offerTemplates.IncludeCommission = HelperMethods.GetBoolean(reader, "IncludeCommission");
                offerTemplates.CommissionDescriprion = HelperMethods.GetString(reader, "CommissionDescriprion");
                offerTemplates.CommissionDescriprion_EM = HelperMethods.GetBoolean(reader, "CommissionDescriprion_EM");
                offerTemplates.AnnualCommissionPotential = HelperMethods.GetDecimal(reader, "AnnualCommissionPotential");
                offerTemplates.AnnualCommissionPotentialMax = HelperMethods.GetDecimal(reader, "AnnualCommissionPotentialMax");
                offerTemplates.AnnualCommissionPotential_EM = HelperMethods.GetBoolean(reader, "AnnualCommissionPotential_EM");
                offerTemplates.AnnualCommissionPotential_EC = HelperMethods.GetBoolean(reader, "AnnualCommissionPotential_EC");
                offerTemplates.CommissionCap = HelperMethods.GetDecimal(reader, "CommissionCap");
                offerTemplates.CommissionCapMax = HelperMethods.GetDecimal(reader, "CommissionCapMax");
                offerTemplates.CommissionCap_EM = HelperMethods.GetBoolean(reader, "CommissionCap_EM");
                offerTemplates.CommissionCap_EC = HelperMethods.GetBoolean(reader, "CommissionCap_EC");
                offerTemplates.IncludeBonus = HelperMethods.GetBoolean(reader, "IncludeBonus");
                offerTemplates.BonusDescription = HelperMethods.GetString(reader, "BonusDescription");
                offerTemplates.BonusDescription_EM = HelperMethods.GetBoolean(reader, "BonusDescription_EM");
                offerTemplates.AnnualBonusPotential = HelperMethods.GetDecimal(reader, "AnnualBonusPotential");
                offerTemplates.AnnualBonusPotentialMax = HelperMethods.GetDecimal(reader, "AnnualBonusPotentialMax");
                offerTemplates.AnnualBonusPotential_EM = HelperMethods.GetBoolean(reader, "AnnualBonusPotential_EM");
                offerTemplates.AnnualBonusPotential_EC = HelperMethods.GetBoolean(reader, "AnnualBonusPotential_EC");
                offerTemplates.BonusCap = HelperMethods.GetDecimal(reader, "BonusCap");
                offerTemplates.BonusCapMax = HelperMethods.GetDecimal(reader, "BonusCapMax");
                offerTemplates.BonusCap_EM = HelperMethods.GetBoolean(reader, "BonusCap_EM");
                offerTemplates.BonusCap_EC = HelperMethods.GetBoolean(reader, "BonusCap_EC");
                offerTemplates.IncludeCandidateNotice = HelperMethods.GetBoolean(reader, "IncludeCandidateNotice");
                offerTemplates.CandidateNoticePeriod = HelperMethods.GetInt32(reader, "CandidateNoticePeriod");
                offerTemplates.CandidateNoticePeriod_EM = HelperMethods.GetBoolean(reader, "CandidateNoticePeriod_EM");
                offerTemplates.CandidateNoticePeriod_EC = HelperMethods.GetBoolean(reader, "CandidateNoticePeriod_EC");
                offerTemplates.CandidateNoticePeriodType = HelperMethods.GetInt32(reader, "CandidateNoticePeriodType");
                offerTemplates.CandidateNoticePeriodType_EM = HelperMethods.GetBoolean(reader, "CandidateNoticePeriodType_EM");
                offerTemplates.CandidateNoticePeriodType_EC = HelperMethods.GetBoolean(reader, "CandidateNoticePeriodType_EC");
                offerTemplates.IncludeCompanyNotice = HelperMethods.GetBoolean(reader, "IncludeCompanyNotice");
                offerTemplates.CompanyNoticePeriod = HelperMethods.GetInt32(reader, "CompanyNoticePeriod");
                offerTemplates.CompanyNoticePeriod_EM = HelperMethods.GetBoolean(reader, "CompanyNoticePeriod_EM");
                offerTemplates.CompanyNoticePeriod_EC = HelperMethods.GetBoolean(reader, "CompanyNoticePeriod_EC");
                offerTemplates.CompanyNoticePeriodType = HelperMethods.GetInt32(reader, "CompanyNoticePeriodType");
                offerTemplates.CompanyNoticePeriodType_EM = HelperMethods.GetBoolean(reader, "CompanyNoticePeriodType_EM");
                offerTemplates.CompanyNoticePeriodType_EC = HelperMethods.GetBoolean(reader, "CompanyNoticePeriodType_EC");
                offerTemplates.IncludeAttachments = HelperMethods.GetBoolean(reader, "IncludeAttachment");

                offerTemplates.AwardAmount = HelperMethods.GetDecimal(reader, "AwardAmount");
                offerTemplates.AwardAmountMax = HelperMethods.GetDecimal(reader, "AwardAmountMax");
                offerTemplates.AwardAmount_EM = HelperMethods.GetBoolean(reader, "AwardAmount_EM");
                offerTemplates.AwardAmount_EC = HelperMethods.GetBoolean(reader, "AwardAmount_EC");
                offerTemplates.AwardIssueDate = HelperMethods.GetDateTime(reader, "AwardIssueDate");
                offerTemplates.CashMatchRequired = HelperMethods.GetDecimal(reader, "CashMatchRequired");
                offerTemplates.CashMatchRequiredMax = HelperMethods.GetDecimal(reader, "CashMatchRequiredMax");
                offerTemplates.CashMatchRequired_EM = HelperMethods.GetBoolean(reader, "CashMatchRequired_EM");
                offerTemplates.CashMatchRequired_EC = HelperMethods.GetBoolean(reader, "CashMatchRequired_EC");
                string strDivisionList = HelperMethods.GetString(reader, "DivisionList");
                if (!string.IsNullOrEmpty(strDivisionList))
                    offerTemplates.DivisionList = strDivisionList.Split(',').Select(x => Guid.Parse(x)).ToList();
                return offerTemplates;
            }
        }

        internal class GetAllOfferTemplateFactory : IDomainObjectFactory<BEClient.OfferTemplates>
        {
            public BEClient.OfferTemplates Construct(IDataReader reader)
            {
                BEClient.OfferTemplates offerTemplates = new BEClient.OfferTemplates();
                offerTemplates.OfferTemplateId = HelperMethods.GetGuid(reader, "OfferTemplateId");
                offerTemplates.OfferTemplateName = HelperMethods.GetString(reader, "OfferTemplateName");
                offerTemplates.PositionType = HelperMethods.GetGuid(reader, "PositionType");
                offerTemplates.EnableCounterOffers = HelperMethods.GetBoolean(reader, "EnableCounterOffers");
                offerTemplates.EmailToCandidateId = HelperMethods.GetString(reader, "EmailToCandidateId");
                offerTemplates.EmailToCandidate_EditSelection = HelperMethods.GetBoolean(reader, "EmailToCandidate_EditSelection");
                offerTemplates.EmailToCandidate_EditContent = HelperMethods.GetBoolean(reader, "EmailToCandidate_EditContent");
                offerTemplates.OfferLetterId = HelperMethods.GetString(reader, "OfferLetterId");
                offerTemplates.OfferLetterId_EditSelection = HelperMethods.GetBoolean(reader, "OfferLetterId_EditSelection");
                offerTemplates.OfferLetterId_EditContent = HelperMethods.GetBoolean(reader, "OfferLetterId_EditContent");
                offerTemplates.PlacementType = HelperMethods.GetInt32(reader, "PlacementType");
                offerTemplates.PlacementType_EM = HelperMethods.GetBoolean(reader, "PlacementType_EM");
                offerTemplates.PlacementType_EC = HelperMethods.GetBoolean(reader, "PlacementType_EC");
                offerTemplates.JobType = HelperMethods.GetInt32(reader, "JobType");
                offerTemplates.JobType_EM = HelperMethods.GetBoolean(reader, "JobType_EM");
                offerTemplates.JobType_EC = HelperMethods.GetBoolean(reader, "JobType_EC");
                offerTemplates.SalaryType = HelperMethods.GetInt32(reader, "SalaryType");
                offerTemplates.Per = HelperMethods.GetString(reader, "Per");
                offerTemplates.SalaryType_EM = HelperMethods.GetBoolean(reader, "SalaryType_EM");
                offerTemplates.SalaryType_EC = HelperMethods.GetBoolean(reader, "SalaryType_EC");
                offerTemplates.SalaryOffered = HelperMethods.GetDecimal(reader, "SalaryOffered");
                offerTemplates.SalaryOffered_EM = HelperMethods.GetBoolean(reader, "SalaryOffered_EM");
                offerTemplates.SalaryOffered_EC = HelperMethods.GetBoolean(reader, "SalaryOffered_EC");
                offerTemplates.HoursPerWeek = HelperMethods.GetInt32(reader, "HoursPerWeek");
                offerTemplates.HoursPerWeek_EM = HelperMethods.GetBoolean(reader, "HoursPerWeek_EM");
                offerTemplates.HoursPerWeek_EC = HelperMethods.GetBoolean(reader, "HoursPerWeek_EC");
                offerTemplates.HourlyWage = HelperMethods.GetDecimal(reader, "HourlyWage");
                offerTemplates.HourlyWage_EM = HelperMethods.GetBoolean(reader, "HourlyWage_EM");
                offerTemplates.HourlyWage_EC = HelperMethods.GetBoolean(reader, "HourlyWage_EC");
                offerTemplates.Rate = HelperMethods.GetDecimal(reader, "Rate");
                offerTemplates.Rate_EM = HelperMethods.GetBoolean(reader, "Rate_EM");
                offerTemplates.Rate_EC = HelperMethods.GetBoolean(reader, "Rate_EC");
                offerTemplates.Per = HelperMethods.GetString(reader, "Per");
                offerTemplates.Per_EM = HelperMethods.GetBoolean(reader, "Per_EM");
                offerTemplates.Per_EC = HelperMethods.GetBoolean(reader, "Per_EC");
                offerTemplates.ChargeRate = HelperMethods.GetDecimal(reader, "ChargeRate");
                offerTemplates.ChargeRate_EM = HelperMethods.GetBoolean(reader, "ChargeRate_EM");
                offerTemplates.ChargeRate_EC = HelperMethods.GetBoolean(reader, "ChargeRate_EC");
                offerTemplates.PayRate = HelperMethods.GetDecimal(reader, "PayRate");
                offerTemplates.PayRate_EM = HelperMethods.GetBoolean(reader, "PayRate_EM");
                offerTemplates.PayRate_EC = HelperMethods.GetBoolean(reader, "PayRate_EC");
                offerTemplates.RatePeriod = HelperMethods.GetInt32(reader, "RatePeriod");
                offerTemplates.RatePeriod_EM = HelperMethods.GetBoolean(reader, "RatePeriod_EM");
                offerTemplates.RatePeriod_EC = HelperMethods.GetBoolean(reader, "RatePeriod_EC");
                offerTemplates.IncludeCommission = HelperMethods.GetBoolean(reader, "IncludeCommission");
                offerTemplates.CommissionDescriprion = HelperMethods.GetString(reader, "CommissionDescriprion");
                offerTemplates.CommissionDescriprion_EM = HelperMethods.GetBoolean(reader, "CommissionDescriprion_EM");
                offerTemplates.AnnualCommissionPotential = HelperMethods.GetDecimal(reader, "AnnualCommissionPotential");
                offerTemplates.AnnualCommissionPotential_EM = HelperMethods.GetBoolean(reader, "AnnualCommissionPotential_EM");
                offerTemplates.AnnualCommissionPotential_EC = HelperMethods.GetBoolean(reader, "AnnualCommissionPotential_EC");
                offerTemplates.CommissionCap = HelperMethods.GetDecimal(reader, "CommissionCap");
                offerTemplates.CommissionCap_EM = HelperMethods.GetBoolean(reader, "CommissionCap_EM");
                offerTemplates.CommissionCap_EC = HelperMethods.GetBoolean(reader, "CommissionCap_EC");
                offerTemplates.IncludeBonus = HelperMethods.GetBoolean(reader, "IncludeBonus");
                offerTemplates.BonusDescription = HelperMethods.GetString(reader, "BonusDescription");
                offerTemplates.BonusDescription_EM = HelperMethods.GetBoolean(reader, "BonusDescription_EM");
                offerTemplates.AnnualBonusPotential = HelperMethods.GetDecimal(reader, "AnnualBonusPotential");
                offerTemplates.AnnualBonusPotential_EM = HelperMethods.GetBoolean(reader, "AnnualBonusPotential_EM");
                offerTemplates.AnnualBonusPotential_EC = HelperMethods.GetBoolean(reader, "AnnualBonusPotential_EC");
                offerTemplates.BonusCap = HelperMethods.GetDecimal(reader, "BonusCap");
                offerTemplates.BonusCap_EM = HelperMethods.GetBoolean(reader, "BonusCap_EM");
                offerTemplates.BonusCap_EC = HelperMethods.GetBoolean(reader, "BonusCap_EC");
                offerTemplates.IncludeCandidateNotice = HelperMethods.GetBoolean(reader, "IncludeCandidateNotice");
                offerTemplates.CandidateNoticePeriod = HelperMethods.GetInt32(reader, "CandidateNoticePeriod");
                offerTemplates.CandidateNoticePeriod_EM = HelperMethods.GetBoolean(reader, "CandidateNoticePeriod_EM");
                offerTemplates.CandidateNoticePeriod_EC = HelperMethods.GetBoolean(reader, "CandidateNoticePeriod_EC");
                offerTemplates.CandidateNoticePeriodType = HelperMethods.GetInt32(reader, "CandidateNoticePeriodType");
                offerTemplates.CandidateNoticePeriodType_EM = HelperMethods.GetBoolean(reader, "CandidateNoticePeriodType_EM");
                offerTemplates.CandidateNoticePeriodType_EC = HelperMethods.GetBoolean(reader, "CandidateNoticePeriodType_EC");
                offerTemplates.IncludeCompanyNotice = HelperMethods.GetBoolean(reader, "IncludeCompanyNotice");
                offerTemplates.CompanyNoticePeriod = HelperMethods.GetInt32(reader, "CompanyNoticePeriod");
                offerTemplates.CompanyNoticePeriod_EM = HelperMethods.GetBoolean(reader, "CompanyNoticePeriod_EM");
                offerTemplates.CompanyNoticePeriod_EC = HelperMethods.GetBoolean(reader, "CompanyNoticePeriod_EC");
                offerTemplates.CompanyNoticePeriodType = HelperMethods.GetInt32(reader, "CompanyNoticePeriodType");
                offerTemplates.CompanyNoticePeriodType_EM = HelperMethods.GetBoolean(reader, "CompanyNoticePeriodType_EM");
                offerTemplates.CompanyNoticePeriodType_EC = HelperMethods.GetBoolean(reader, "CompanyNoticePeriodType_EC");
                string strDivisionList = HelperMethods.GetString(reader, "DivisionList");
                if (!string.IsNullOrEmpty(strDivisionList))
                    offerTemplates.DivisionList = strDivisionList.Split(',').Select(x => Guid.Parse(x)).ToList();
                return offerTemplates;
            }
        }

        internal class GetOfferTemplateByVacRndIdFactory : IDomainObjectFactory<BEClient.OfferTemplates>
        {
            public BEClient.OfferTemplates Construct(IDataReader reader)
            {
                BEClient.OfferTemplates offerTemplates = new BEClient.OfferTemplates();
                offerTemplates.OfferTemplateId = HelperMethods.GetGuid(reader, "OfferTemplateId");
                offerTemplates.OfferTemplateName = HelperMethods.GetString(reader, "OfferTemplateName");
                offerTemplates.PositionType = HelperMethods.GetGuid(reader, "PositionType");
                offerTemplates.EnableCounterOffers = HelperMethods.GetBoolean(reader, "EnableCounterOffers");
                offerTemplates.EmailToCandidateId = HelperMethods.GetString(reader, "EmailToCandidateId");
                offerTemplates.EmailToCandidate_EditSelection = HelperMethods.GetBoolean(reader, "EmailToCandidate_EditSelection");
                offerTemplates.EmailToCandidate_EditContent = HelperMethods.GetBoolean(reader, "EmailToCandidate_EditContent");
                offerTemplates.OfferLetterId = HelperMethods.GetString(reader, "OfferLetterId");
                offerTemplates.OfferLetterId_EditSelection = HelperMethods.GetBoolean(reader, "OfferLetterId_EditSelection");
                offerTemplates.OfferLetterId_EditContent = HelperMethods.GetBoolean(reader, "OfferLetterId_EditContent");
                offerTemplates.PlacementType = HelperMethods.GetInt32(reader, "PlacementType");
                offerTemplates.PlacementType_EM = HelperMethods.GetBoolean(reader, "PlacementType_EM");
                offerTemplates.PlacementType_EC = HelperMethods.GetBoolean(reader, "PlacementType_EC");
                offerTemplates.JobType = HelperMethods.GetInt32(reader, "JobType");
                offerTemplates.JobType_EM = HelperMethods.GetBoolean(reader, "JobType_EM");
                offerTemplates.JobType_EC = HelperMethods.GetBoolean(reader, "JobType_EC");
                offerTemplates.SalaryType = HelperMethods.GetInt32(reader, "SalaryType");
                offerTemplates.Per = HelperMethods.GetString(reader, "Per");
                offerTemplates.SalaryType_EM = HelperMethods.GetBoolean(reader, "SalaryType_EM");
                offerTemplates.SalaryType_EC = HelperMethods.GetBoolean(reader, "SalaryType_EC");
                offerTemplates.SalaryOffered = HelperMethods.GetDecimal(reader, "SalaryOffered");
                offerTemplates.SalaryOffered_EM = HelperMethods.GetBoolean(reader, "SalaryOffered_EM");
                offerTemplates.SalaryOffered_EC = HelperMethods.GetBoolean(reader, "SalaryOffered_EC");
                offerTemplates.HoursPerWeek = HelperMethods.GetInt32(reader, "HoursPerWeek");
                offerTemplates.HoursPerWeek_EM = HelperMethods.GetBoolean(reader, "HoursPerWeek_EM");
                offerTemplates.HoursPerWeek_EC = HelperMethods.GetBoolean(reader, "HoursPerWeek_EC");
                offerTemplates.HourlyWage = HelperMethods.GetDecimal(reader, "HourlyWage");
                offerTemplates.HourlyWage_EM = HelperMethods.GetBoolean(reader, "HourlyWage_EM");
                offerTemplates.HourlyWage_EC = HelperMethods.GetBoolean(reader, "HourlyWage_EC");
                offerTemplates.Rate = HelperMethods.GetDecimal(reader, "Rate");
                offerTemplates.Rate_EM = HelperMethods.GetBoolean(reader, "Rate_EM");
                offerTemplates.Rate_EC = HelperMethods.GetBoolean(reader, "Rate_EC");
                offerTemplates.Per = HelperMethods.GetString(reader, "Per");
                offerTemplates.Per_EM = HelperMethods.GetBoolean(reader, "Per_EM");
                offerTemplates.Per_EC = HelperMethods.GetBoolean(reader, "Per_EC");
                offerTemplates.ChargeRate = HelperMethods.GetDecimal(reader, "ChargeRate");
                offerTemplates.ChargeRate_EM = HelperMethods.GetBoolean(reader, "ChargeRate_EM");
                offerTemplates.ChargeRate_EC = HelperMethods.GetBoolean(reader, "ChargeRate_EC");
                offerTemplates.PayRate = HelperMethods.GetDecimal(reader, "PayRate");
                offerTemplates.PayRate_EM = HelperMethods.GetBoolean(reader, "PayRate_EM");
                offerTemplates.PayRate_EC = HelperMethods.GetBoolean(reader, "PayRate_EC");
                offerTemplates.RatePeriod = HelperMethods.GetInt32(reader, "RatePeriod");
                offerTemplates.RatePeriod_EM = HelperMethods.GetBoolean(reader, "RatePeriod_EM");
                offerTemplates.RatePeriod_EC = HelperMethods.GetBoolean(reader, "RatePeriod_EC");
                offerTemplates.IncludeCommission = HelperMethods.GetBoolean(reader, "IncludeCommission");
                offerTemplates.CommissionDescriprion = HelperMethods.GetString(reader, "CommissionDescriprion");
                offerTemplates.CommissionDescriprion_EM = HelperMethods.GetBoolean(reader, "CommissionDescriprion_EM");
                offerTemplates.AnnualCommissionPotential = HelperMethods.GetDecimal(reader, "AnnualCommissionPotential");
                offerTemplates.AnnualCommissionPotential_EM = HelperMethods.GetBoolean(reader, "AnnualCommissionPotential_EM");
                offerTemplates.AnnualCommissionPotential_EC = HelperMethods.GetBoolean(reader, "AnnualCommissionPotential_EC");
                offerTemplates.CommissionCap = HelperMethods.GetDecimal(reader, "CommissionCap");
                offerTemplates.CommissionCap_EM = HelperMethods.GetBoolean(reader, "CommissionCap_EM");
                offerTemplates.CommissionCap_EC = HelperMethods.GetBoolean(reader, "CommissionCap_EC");
                offerTemplates.IncludeBonus = HelperMethods.GetBoolean(reader, "IncludeBonus");
                offerTemplates.BonusDescription = HelperMethods.GetString(reader, "BonusDescription");
                offerTemplates.BonusDescription_EM = HelperMethods.GetBoolean(reader, "BonusDescription_EM");
                offerTemplates.AnnualBonusPotential = HelperMethods.GetDecimal(reader, "AnnualBonusPotential");
                offerTemplates.AnnualBonusPotential_EM = HelperMethods.GetBoolean(reader, "AnnualBonusPotential_EM");
                offerTemplates.AnnualBonusPotential_EC = HelperMethods.GetBoolean(reader, "AnnualBonusPotential_EC");
                offerTemplates.BonusCap = HelperMethods.GetDecimal(reader, "BonusCap");
                offerTemplates.BonusCap_EM = HelperMethods.GetBoolean(reader, "BonusCap_EM");
                offerTemplates.BonusCap_EC = HelperMethods.GetBoolean(reader, "BonusCap_EC");
                offerTemplates.IncludeCandidateNotice = HelperMethods.GetBoolean(reader, "IncludeCandidateNotice");
                offerTemplates.CandidateNoticePeriod = HelperMethods.GetInt32(reader, "CandidateNoticePeriod");
                offerTemplates.CandidateNoticePeriod_EM = HelperMethods.GetBoolean(reader, "CandidateNoticePeriod_EM");
                offerTemplates.CandidateNoticePeriod_EC = HelperMethods.GetBoolean(reader, "CandidateNoticePeriod_EC");
                offerTemplates.CandidateNoticePeriodType = HelperMethods.GetInt32(reader, "CandidateNoticePeriodType");
                offerTemplates.CandidateNoticePeriodType_EM = HelperMethods.GetBoolean(reader, "CandidateNoticePeriodType_EM");
                offerTemplates.CandidateNoticePeriodType_EC = HelperMethods.GetBoolean(reader, "CandidateNoticePeriodType_EC");
                offerTemplates.IncludeCompanyNotice = HelperMethods.GetBoolean(reader, "IncludeCompanyNotice");
                offerTemplates.CompanyNoticePeriod = HelperMethods.GetInt32(reader, "CompanyNoticePeriod");
                offerTemplates.CompanyNoticePeriod_EM = HelperMethods.GetBoolean(reader, "CompanyNoticePeriod_EM");
                offerTemplates.CompanyNoticePeriod_EC = HelperMethods.GetBoolean(reader, "CompanyNoticePeriod_EC");
                offerTemplates.CompanyNoticePeriodType = HelperMethods.GetInt32(reader, "CompanyNoticePeriodType");
                offerTemplates.CompanyNoticePeriodType_EM = HelperMethods.GetBoolean(reader, "CompanyNoticePeriodType_EM");
                offerTemplates.CompanyNoticePeriodType_EC = HelperMethods.GetBoolean(reader, "CompanyNoticePeriodType_EC");

                offerTemplates.AwardAmount = HelperMethods.GetDecimal(reader, "AwardAmount");
                offerTemplates.AwardAmountMax = HelperMethods.GetDecimal(reader, "AwardAmountMax");
                offerTemplates.AwardAmount_EM = HelperMethods.GetBoolean(reader, "AwardAmount_EM");
                offerTemplates.AwardAmount_EC = HelperMethods.GetBoolean(reader, "AwardAmount_EC");
                offerTemplates.AwardIssueDate = HelperMethods.GetDateTime(reader, "AwardIssueDate");
                offerTemplates.CashMatchRequired = HelperMethods.GetDecimal(reader, "CashMatchRequired");
                offerTemplates.CashMatchRequiredMax = HelperMethods.GetDecimal(reader, "CashMatchRequiredMax");
                offerTemplates.CashMatchRequired_EM = HelperMethods.GetBoolean(reader, "CashMatchRequired_EM");
                offerTemplates.CashMatchRequired_EC = HelperMethods.GetBoolean(reader, "CashMatchRequired_EC");

                return offerTemplates;
            }
        }

    }
}
