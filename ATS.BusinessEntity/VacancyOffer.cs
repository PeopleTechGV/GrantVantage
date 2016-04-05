using ATS.BusinessEntity.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEDateFormatConst = ATS.BusinessEntity.Common.DateFormatConstant;
using BEMakeOffer = ATS.BusinessEntity.Common.MakeOffer;
using System.ComponentModel.DataAnnotations;
using BEClient = ATS.BusinessEntity;

namespace ATS.BusinessEntity
{
    public class VacancyOffer : BaseEntity
    {
        public Guid VacancyOfferId { get; set; }

        public Guid ApplicationId { get; set; }

        public Int32 OfferTypeId { get; set; }

        [Display(Name = "Award Type")]
        public string OfferTypeText { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_CANDIDATE)]
        public string CandidateName { get; set; }

        public string CandidateFirstName { get; set; }
        public string CandidateLastName { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_VACANCY)]
        public String JobTitle { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_OFFDATE)]
        public DateTime OfferDate { get; set; }

        public Int32 OfferStatusId { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_OFFSTATUS)]
        public string OfferStatusText { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_ENABLE_COUNTER_OFFER)]
        public bool EnableCounterOffers { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EMAIL_TO_CANDIDATE)]
        public Guid EmailToCandidateId { get; set; }

        public string EmailToCandidateIdList { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EMAIL_CONTENT_EDITABLE_BY_MANAGER)]
        public bool EmailToCandidate_EditContent { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_OFFER_LETTER)]
        public Guid OfferLetterId { get; set; }

        public string OfferLetterIdList { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_PICKLIST_SELECTION_EDITABLE_BY_MANAGER)]
        public bool OfferLetterId_EditContent { get; set; }

        [ATSRequired(ErrorMessage = "{0}")]
        [Display(Name = BEMakeOffer.FRM_MAKEOFF_PLACEMENT_TYPE)]
        public string PlacementType { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_PLACEMENT_TYPE)]
        public string PlacementTypeText { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool PlacementType_EM { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_CANDIDATE)]
        public bool PlacementType_EC { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_STARTDATE)]
        public DateTime StartDate { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_ENDDATE)]
        public DateTime EndDate { get; set; }

        public string StartDateText
        {
            get
            {
                if (StartDate != DateTime.MinValue && StartDate != null)
                    return ((DateTime)StartDate).ToString(BEDateFormatConst.US_FORMAT);
                else
                    return string.Empty;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    StartDate = Convert.ToDateTime(value, System.Globalization.CultureInfo.GetCultureInfo(BEDateFormatConst.US_CULTURE_INFO).DateTimeFormat);
            }
        }

        public string EndDateText
        {
            get
            {
                if (EndDate != DateTime.MinValue && EndDate != null)
                    return ((DateTime)EndDate).ToString(BEDateFormatConst.US_FORMAT);
                else
                    return string.Empty;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    EndDate = Convert.ToDateTime(value, System.Globalization.CultureInfo.GetCultureInfo(BEDateFormatConst.US_CULTURE_INFO).DateTimeFormat);
            }
        }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_JOBTYPE)]
        public Int32 JobType { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_JOBTYPE)]
        public string JobTypeText { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool JobType_EM { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_CANDIDATE)]
        public bool JobType_EC { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_PLACEMENT_LOC)]
        public Guid Location { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_PLACEMENT_LOC)]
        public string LocationText { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_SALARY_TYPE)]
        public Int32 SalaryType { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_SALARY_TYPE)]
        public string SalaryTypeText { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool SalaryType_EM { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_CANDIDATE)]
        public bool SalaryType_EC { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_SALARY_OFFERED)]
        public Decimal SalaryOffered { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_MAX_VALUE)]
        public Decimal SalaryOfferedMax { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool SalaryOffered_EM { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_CANDIDATE)]
        public bool SalaryOffered_EC { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_HOURSWEEK)]
        public Int32 HoursPerWeek { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool HoursPerWeek_EM { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_CANDIDATE)]
        public bool HoursPerWeek_EC { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_ENABLE_HOURLY_WAGE)]
        public Decimal HourlyWage { get; set; }
        [Display(Name = BEMakeOffer.FRM_MAKEOFF_MAX_VALUE)]
        public Decimal HourlyWageMax { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool HourlyWage_EM { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_CANDIDATE)]
        public bool HourlyWage_EC { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_RATE)]
        public Decimal Rate { get; set; }
        [Display(Name = BEMakeOffer.FRM_MAKEOFF_MAX_VALUE)]
        public Decimal RateMax { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool Rate_EM { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_CANDIDATE)]
        public bool Rate_EC { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_PER)]
        public string Per { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool Per_EM { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_CANDIDATE)]
        public bool Per_EC { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_PAY_RATE)]
        public Decimal PayRate { get; set; }
        [Display(Name = BEMakeOffer.FRM_MAKEOFF_MAX_VALUE)]
        public Decimal PayRateMax { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool PayRate_EM { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_CANDIDATE)]
        public bool PayRate_EC { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_RATE_PERIOD)]
        public Int32 RatePeriod { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_RATE_PERIOD)]
        public string RatePeriodText { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool RatePeriod_EM { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_CANDIDATE)]
        public bool RatePeriod_EC { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_PLUS_COMM)]
        public bool PlusCommission { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_COMM_DESC)]
        public string CommissionDescriprion { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool CommissionDescriprion_EM { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_COMM_POTENTIAL)]
        public Decimal AnnualCommissionPotential { get; set; }
        [Display(Name = BEMakeOffer.FRM_MAKEOFF_MAX_VALUE)]
        public Decimal AnnualCommissionPotentialMax { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool AnnualCommissionPotential_EM { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_CANDIDATE)]
        public bool AnnualCommissionPotential_EC { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_COMM_CAP)]
        public Decimal CommissionCap { get; set; }
        [Display(Name = BEMakeOffer.FRM_MAKEOFF_MAX_VALUE)]
        public Decimal CommissionCapMax { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool CommissionCap_EM { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_CANDIDATE)]
        public bool CommissionCap_EC { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_PLUS_BON)]
        public bool PlusBonus { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_BONUS_DESC)]
        public string BonusDescription { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool BonusDescription_EM { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_BONUS_POTENTIAL)]
        public Decimal AnnualBonusPotential { get; set; }
        [Display(Name = BEMakeOffer.FRM_MAKEOFF_MAX_VALUE)]
        public Decimal AnnualBonusPotentialMax { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool AnnualBonusPotential_EM { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_CANDIDATE)]
        public bool AnnualBonusPotential_EC { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_BONUS_CAP)]
        public Decimal BonusCap { get; set; }
        [Display(Name = BEMakeOffer.FRM_MAKEOFF_MAX_VALUE)]
        public Decimal BonusCapMax { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool BonusCap_EM { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_CANDIDATE)]
        public bool BonusCap_EC { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_INCLUDE_IN_OFFER)]
        public bool IncludeCandidateNotice { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_CAN_NOTICE_PER)]
        public Int32 CandidateNoticePeriod { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_CAN_NOTICE_PER)]
        public string CandidateNoticePeriodText { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool CandidateNoticePeriod_EM { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_CANDIDATE)]
        public bool CandidateNoticePeriod_EC { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_CAN_IN)]
        public Int32 CandidateNoticePeriodType { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_CAN_IN)]
        public string CandidateNoticePeriodTypeText { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool CandidateNoticePeriodType_EM { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_CANDIDATE)]
        public bool CandidateNoticePeriodType_EC { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_INCLUDE_IN_OFFER)]
        public bool IncludeCompanyNotice { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_COM_NOTICE_PER)]
        public Int32 CompanyNoticePeriod { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_COM_NOTICE_PER)]
        public string CompanyNoticePeriodText { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool CompanyNoticePeriod_EM { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_CANDIDATE)]
        public bool CompanyNoticePeriod_EC { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_COM_IN)]
        public Int32 CompanyNoticePeriodType { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_COM_IN)]
        public string CompanyNoticePeriodTypeText { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool CompanyNoticePeriodType_EM { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_CANDIDATE)]
        public bool CompanyNoticePeriodType_EC { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_NOTE_APPLICANT)]
        public string NoteToApplicant { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_RESPONSE_APPLICANT)]
        public string ResponseFromApplicant { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EMAIL_TO_CANDIDATE)]
        public Guid EmailTemplateId { get; set; }

        public List<ApplicationHistory> objApplicationHistory { get; set; }

        public DateTime OfferAcceptedDate { get; set; }

        public string PrInt32CandidateName { get; set; }

        public string CandidateConfirmEmail { get; set; }

        public bool IsReviewer { get; set; }
        public bool CanMakeOffer { get; set; }
        public bool CanEditOffer { get; set; }
        public Guid ReviewerId { get; set; }

        public string CandidateCounterReason { get; set; }

        public string CandidateDeclineReason { get; set; }

        public string CandidateEmailId { get; set; }

        public DateTime OfferConfirmedDate { get; set; }

        public bool IsConfirmedByClient { get; set; }

        public Int32 RndNo { get; set; }

        public string OfferLetter { get; set; }

        [Display(Name = ATS.BusinessEntity.Common.CommonConstant.SUBJECT)]
        public string EmailSubject { get; set; }

        [Display(Name = ATS.BusinessEntity.Common.CommonConstant.EMAIL_DESCRIPTION)]
        public string EmailDescription { get; set; }

        public string ManagerName { get; set; }

        public Int32 TotalOffer { get; set; }

        public string PdfHeader { get; set; }

        //To check offer is Initial Offer or New Offer
        //Set to 1 is Offer is New Offer
        public bool IsNewOffer { get; set; }
        [Display(Name = ATS.BusinessEntity.Common.CommonConstant.FRM_CREATED_BY)]
        public string CreatedByName { get; set; }
        [Display(Name = ATS.BusinessEntity.Common.CommonConstant.FRM_UPDATED_BY)]
        public string UpdatedByName { get; set; }

        public Guid OfferTemplateId { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_INCLUDE_IN_OFFER)]
        public bool IncludeAttachments { get; set; }

        public List<BEClient.OfferAttachment> OfferAttachmentList { get; set; }

        [Display(Name = "Award Deadline")]
        public DateTime OfferDeadlineDate
        { get; set; }

        public string OfferDeadlineDateText
        {
            get
            {
                if (OfferDeadlineDate != DateTime.MinValue && OfferDeadlineDate != null)
                    return ((DateTime)OfferDeadlineDate).ToString(BEDateFormatConst.US_FORMAT);
                else
                    return DateTime.Now.AddDays(7).ToString(BEDateFormatConst.US_FORMAT);
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    OfferDeadlineDate = Convert.ToDateTime(value, System.Globalization.CultureInfo.GetCultureInfo(BEDateFormatConst.US_CULTURE_INFO).DateTimeFormat);
            }
        }

        ////Used in GV
        [Display(Name = "Award Amount")]
        public decimal? AwardAmount { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_MAX_VALUE)]
        public decimal? AwardAmountMax { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool AwardAmount_EM { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_CANDIDATE)]
        public bool AwardAmount_EC { get; set; }

        [Display(Name = "Award Issue Date")]
        public DateTime? AwardIssueDate { get; set; }
        public string AwardIssueDateText
        {
            get
            {
                if (AwardIssueDate != DateTime.MinValue && AwardIssueDate != null)
                    return ((DateTime)AwardIssueDate).ToString(BEDateFormatConst.US_FORMAT);
                else
                    return "";
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    AwardIssueDate = Convert.ToDateTime(value, System.Globalization.CultureInfo.GetCultureInfo(BEDateFormatConst.US_CULTURE_INFO).DateTimeFormat);
            }
        }

        [Display(Name = "Cash Match Required")]
        public decimal? CashMatchRequired { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_MAX_VALUE)]
        public decimal? CashMatchRequiredMax { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_MANAGER)]
        public bool CashMatchRequired_EM { get; set; }

        [Display(Name = BEMakeOffer.FRM_MAKEOFF_EDITABLE_BY_CANDIDATE)]
        public bool CashMatchRequired_EC { get; set; }
        ////Used in GV END

    }
}