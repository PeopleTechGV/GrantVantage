using ATS.BusinessEntity.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using BECommonConst = ATS.BusinessEntity.Common.MakeOffer;
using BEDateFormatConst = ATS.BusinessEntity.Common.DateFormatConstant;
using BEClient = ATS.BusinessEntity;

namespace ATS.BusinessEntity
{
    public class OfferTemplates : ClientBaseEntity
    {
        public Guid OfferTemplateId { get; set; }

        [ATSRequired(ErrorMessage = "{0}")]
        [Display(Name = BECommonConst.FRM_MAKEOFF_TEMPLATE_NAME)]
        public string OfferTemplateName { get; set; }

        [ATSRequired(ErrorMessage = "{0}")]
        [Display(Name = BECommonConst.FRM_MAKEOFF_POSITION_TYPE)]
        public Guid PositionType { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_ENABLE_COUNTER_OFFER)]
        public bool EnableCounterOffers { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EMAIL_TO_CANDIDATE)]
        public string EmailToCandidateId { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EMAIL_TO_CANDIDATE)]
        public List<Guid> ListEmailToCandidateId { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_PICKLIST_SELECTION_EDITABLE_BY_MANAGER)]
        public bool EmailToCandidate_EditSelection { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EMAIL_CONTENT_EDITABLE_BY_MANAGER)]
        public bool EmailToCandidate_EditContent { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_OFFER_LETTER)]
        public string OfferLetterId { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_OFFER_LETTER)]
        public List<Guid> ListOfferLetterId { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_PICKLIST_SELECTION_EDITABLE_BY_MANAGER)]
        public bool OfferLetterId_EditSelection { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EMAIL_CONTENT_EDITABLE_BY_MANAGER)]
        public bool OfferLetterId_EditContent { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_PLACEMENT_TYPE)]
        public int? PlacementType { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool PlacementType_EM { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_CANDIDATE)]
        public bool PlacementType_EC { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_JOBTYPE)]
        public int? JobType { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool JobType_EM { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_CANDIDATE)]
        public bool JobType_EC { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_SALARY_TYPE)]
        public int? SalaryType { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool SalaryType_EM { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_CANDIDATE)]
        public bool SalaryType_EC { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_SALARY_OFFERED)]
        public decimal? SalaryOffered { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_MAX_VALUE)]
        public decimal? SalaryOfferedMax { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool SalaryOffered_EM { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_CANDIDATE)]
        public bool SalaryOffered_EC { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_HOURSWEEK)]
        public int? HoursPerWeek { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool HoursPerWeek_EM { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_CANDIDATE)]
        public bool HoursPerWeek_EC { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_ENABLE_HOURLY_WAGE)]
        public decimal? HourlyWage { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_MAX_VALUE)]
        public decimal? HourlyWageMax { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool HourlyWage_EM { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_CANDIDATE)]
        public bool HourlyWage_EC { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_RATE)]
        public decimal? Rate { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_MAX_VALUE)]
        public decimal? RateMax { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool Rate_EM { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_CANDIDATE)]
        public bool Rate_EC { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_PER)]
        public string Per { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool Per_EM { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_CANDIDATE)]
        public bool Per_EC { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_CHARGE_RATE)]
        public decimal? ChargeRate { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_MAX_VALUE)]
        public decimal? ChargeRateMax { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool ChargeRate_EM { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_CANDIDATE)]
        public bool ChargeRate_EC { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_PAY_RATE)]
        public decimal? PayRate { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_MAX_VALUE)]
        public decimal? PayRateMax { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool PayRate_EM { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_CANDIDATE)]
        public bool PayRate_EC { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_RATE_PERIOD)]
        public int RatePeriod { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool RatePeriod_EM { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_CANDIDATE)]
        public bool RatePeriod_EC { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_INCLUDE_IN_OFFER)]
        public bool IncludeCommission { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_COMM_DESC)]
        public string CommissionDescriprion { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool CommissionDescriprion_EM { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_COMM_POTENTIAL)]
        public decimal? AnnualCommissionPotential { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_MAX_VALUE)]
        public decimal? AnnualCommissionPotentialMax { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool AnnualCommissionPotential_EM { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_CANDIDATE)]
        public bool AnnualCommissionPotential_EC { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_COMM_CAP)]
        public decimal? CommissionCap { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_MAX_VALUE)]
        public decimal? CommissionCapMax { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool CommissionCap_EM { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_CANDIDATE)]
        public bool CommissionCap_EC { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_INCLUDE_IN_OFFER)]
        public bool IncludeBonus { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_BONUS_DESC)]
        public string BonusDescription { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool BonusDescription_EM { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_BONUS_POTENTIAL)]
        public decimal? AnnualBonusPotential { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_MAX_VALUE)]
        public decimal? AnnualBonusPotentialMax { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool AnnualBonusPotential_EM { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_CANDIDATE)]
        public bool AnnualBonusPotential_EC { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_BONUS_CAP)]
        public decimal? BonusCap { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_MAX_VALUE)]
        public decimal? BonusCapMax { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool BonusCap_EM { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_CANDIDATE)]
        public bool BonusCap_EC { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_INCLUDE_IN_OFFER)]
        public bool IncludeCandidateNotice { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_CAN_NOTICE_PER)]
        public int CandidateNoticePeriod { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool CandidateNoticePeriod_EM { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_CANDIDATE)]
        public bool CandidateNoticePeriod_EC { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_CAN_IN)]
        public int CandidateNoticePeriodType { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool CandidateNoticePeriodType_EM { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_CANDIDATE)]
        public bool CandidateNoticePeriodType_EC { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_INCLUDE_IN_OFFER)]
        public bool IncludeCompanyNotice { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_COM_NOTICE_PER)]
        public int CompanyNoticePeriod { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool CompanyNoticePeriod_EM { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_CANDIDATE)]
        public bool CompanyNoticePeriod_EC { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_CAN_IN)]
        public int CompanyNoticePeriodType { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool CompanyNoticePeriodType_EM { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_CANDIDATE)]
        public bool CompanyNoticePeriodType_EC { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_INCLUDE_IN_OFFER)]
        public bool IncludeAttachments { get; set; }

        public List<BEClient.OfferAttachment> OfferAttachmentList { get; set; }

        ////Used in GV
        [Display(Name = "Award Amount")]
        public decimal? AwardAmount { get; set; }
        
        [Display(Name = BECommonConst.FRM_MAKEOFF_MAX_VALUE)]
        public decimal? AwardAmountMax { get; set; }
        
        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool AwardAmount_EM { get; set; }
        
        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_CANDIDATE)]
        public bool AwardAmount_EC { get; set; }

        [Display(Name = "Award Issue Date")]
        public DateTime AwardIssueDate { get; set; }

        public string AwardIssueDateText
        {
            get
            {
                if (AwardIssueDate != DateTime.MinValue && AwardIssueDate != null)
                    return ((DateTime)AwardIssueDate).ToString(BEDateFormatConst.US_FORMAT);
                else
                    return string.Empty;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    AwardIssueDate = Convert.ToDateTime(value, System.Globalization.CultureInfo.GetCultureInfo(BEDateFormatConst.US_CULTURE_INFO).DateTimeFormat);
            }
        }

        [Display(Name = "Cash Match Required")]
        public decimal? CashMatchRequired { get; set; }

        [Display(Name = BECommonConst.FRM_MAKEOFF_MAX_VALUE)]
        public decimal? CashMatchRequiredMax { get; set; }
        
        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool CashMatchRequired_EM { get; set; }
        
        [Display(Name = BECommonConst.FRM_MAKEOFF_EDITABLE_BY_CANDIDATE)]
        public bool CashMatchRequired_EC { get; set; }
        ////Used in GV END

        public List<Guid> DivisionList { get; set; }

        public override ATSPrivilage Privilage
        {
            get { return ATSPrivilage.OfferTemplates; }
        }
    }
}
