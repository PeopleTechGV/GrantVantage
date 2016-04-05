using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using BELabel = ATS.BusinessEntity.Common.VacancyConstant;
using ATS.BusinessEntity.Attributes;
using ATS.BusinessEntity.SolrEntity;
using BEDateFormatConst = ATS.BusinessEntity.Common.DateFormatConstant;
using BECommon = ATS.BusinessEntity.Common;
using BECommonConst = ATS.BusinessEntity.Common.OnboardManagers;
using System.Web.Mvc;

namespace ATS.BusinessEntity
{
    public class Vacancy : ClientBaseEntity, ISettingFields
    {
        public Guid VacancyId { get; set; }

        public string StrVacancyId { get { return VacancyId.ToString(); } set { StrVacancyId = value; } }

        public Guid LanguageId { get; set; }

        [Display(Name = BELabel.FRM_VAC_JOB_TITLE)]
        [ATSRequired(ErrorMessage = "{0}")]
        public string JobTitle { get; set; }

        [Display(Name = BELabel.FRM_VAC_POSITION_ID)]
        public Decimal? PositionID { get; set; }

        public String PositionIDText
        {
            get
            {
                return string.Format("{0}", PositionID);
            }
        }

        [Display(Name = BELabel.FRM_VAC_STATUS_REASON)]
        [ATSRequired(ErrorMessage = "{0}")]
        public Guid VacancyStatusId { get; set; }

        [Display(Name = BELabel.FRM_VAC_JOB_TYPE)]
        [ATSRequired(ErrorMessage = "{0}")]
        public Int32 JobType { get; set; }

        [ATSRequired(ErrorMessage = "{0}")]
        [Display(Name = BELabel.FRM_VAC_DIVISION)]
        public Guid DivisionId { get; set; }

        [ATSRequired(ErrorMessage = "{0}")]
        [Display(Name = BELabel.FRM_VAC_POSITION_TYPE)]
        public Guid PositionTypeId { get; set; }

        [Display(Name = BELabel.FRM_VAC_EMPLOYMENT_TYPE)]
        [ATSRequired(ErrorMessage = "{0}")]
        public Int32 EmploymentType { get; set; }


        [Display(Name = BELabel.FRM_VAC_LOCATION)]
        [ATSRequired(ErrorMessage = "{0}")]
        public Guid Location { get; set; }

        [Display(Name = BELabel.FRM_VAC_START_DATE)]
        public DateTime? StartDate { get; set; }

        [Display(Name = BELabel.FRM_VAC_END_DATE)]
        public DateTime? EndDate { get; set; }

        [Display(Name = BELabel.FRM_VAC_TOTAL_POSITIONS)]
        //        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Total Position(s) is not valid")]
        public Int32? TotalPositions { get; set; }

        [Display(Name = BELabel.FRM_VAC_REMAING_POSITIONS)]

        public Int32 RemainingPositions { get; set; }

        [Display(Name = BELabel.FRM_VAC_VACANCY_STATUS)]
        public string ShowOnWeb { get; set; }

        [Display(Name = BELabel.FRM_VAC_FEATURED_ON_WEB)]
        public Boolean FeaturedOnWeb { get; set; }

        [Display(Name = BELabel.FRM_VAC_POSITION_REQUESTED_BY)]
        [ATSRequired(ErrorMessage = "{0}")]
        public Guid PositionRequestedBy { get; set; }

        [Display(Name = BELabel.FRM_VAC_POSITION_OWNER)]
        [ATSRequired(ErrorMessage = "{0}")]
        public Guid PositionOwner { get; set; }


        [Display(Name = BELabel.FRM_VAC_JOB_DESCRIPTION)]
        public string JobDescription { get; set; }


        public Boolean ShowOnWebJobDescription { get; set; }

        [Display(Name = BELabel.FRM_VAC_DUTIES_AND_RESPONSIBILITY)]
        public string DutiesAndResponsibilities { get; set; }

        public Boolean ShowOnWebDuties { get; set; }


        [Display(Name = BELabel.FRM_VAC_SKILL_AND_QUALIFICATION)]
        public string SkillsAndQualification { get; set; }

        public Boolean ShowOnWebSkills { get; set; }

        [Display(Name = BELabel.FRM_VAC_BENEFITS)]
        public string Benefits { get; set; }

        public Boolean ShowOnWebBenefits { get; set; }

        [Display(Name = BELabel.FRM_VAC_SALARY_MIN)]
        //[ATSRegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "{0}")]
        public Decimal SalaryMin { get; set; }

        [ATSStringLength(13)]
        [ATSRegularExpression(@"[0-9.,]+([0-9]+)*,?", ErrorMessage = "Invalid Salary")]
        public string SalaryMinText
        {
            get
            {
                return BECommon.CommonFunction.AddCommaAfterThreeDigit(SalaryMin);
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    SalaryMin = Convert.ToDecimal(value);
            }
        }

        [Display(Name = BELabel.FRM_VAC_SALARY_MAX)]
        //[ATSRegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "{0}")]
        public Decimal SalaryMax { get; set; }

        [ATSStringLength(13)]
        [ATSRegularExpression(@"[0-9.,]+([0-9]+)*,?", ErrorMessage = "Invalid Salary")]
        public string SalaryMaxText
        {
            get
            {
                return BECommon.CommonFunction.AddCommaAfterThreeDigit(SalaryMax);
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    SalaryMax = Convert.ToDecimal(value);
            }
        }


        [Display(Name = "Posted On")]
        public DateTime PostedOn { get; set; }

        [Display(Name = BELabel.FRM_VAC_SHOW_ON_WEB)]
        public Boolean ShowOnWebSal { get; set; }

        [Display(Name = BELabel.FRM_VAC_HOURLY_MIN)]
        public Decimal HourlyMin { get; set; }

        [ATSStringLength(13)]
        [ATSRegularExpression(@"[0-9.,]+([0-9]+)*,?", ErrorMessage = "Invalid Hourly Min")]
        public string HourlyMinText
        {
            get
            {
                return BECommon.CommonFunction.AddCommaAfterThreeDigit(HourlyMin);
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    HourlyMin = Convert.ToDecimal(value);
            }
        }

        [Display(Name = BELabel.FRM_VAC_HOURLY_MAX)]
        public Decimal HourlyMax { get; set; }


        [ATSStringLength(13)]
        [ATSRegularExpression(@"[0-9.,]+([0-9]+)*,?", ErrorMessage = "Invalid Hourly Max")]
        public string HourlyMaxText
        {
            get
            {
                return BECommon.CommonFunction.AddCommaAfterThreeDigit(HourlyMax);
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    HourlyMax = Convert.ToDecimal(value);
            }
        }


        [Display(Name = BELabel.FRM_VAC_SHOW_ON_WEB)]
        public Boolean ShowonWebHour { get; set; }

        [Display(Name = BELabel.FRM_VAC_COMMISSION)]
        //public Decimal Commission { get; set; }
        public string Commission { get; set; }


        [Display(Name = BELabel.FRM_VAC_SHOW_ON_WEB)]
        public Boolean ShowOnWebCommission { get; set; }

        [Display(Name = "Application Instructions")]// CR-38
        public string ApplicationInstruction { get; set; }

        public List<AppInstructionDoc> objAppInstructionDocList { get; set; }

        [Display(Name = BELabel.FRM_VAC_SHOW_ON_WEB)]
        public Boolean ShowOnWebAppInstruction { get; set; }

        [Display(Name = BELabel.FRM_VAC_SHOW_ON_WEB)]
        public Boolean ShowOnWebAppInstructionDoc { get; set; }

        [Display(Name = BELabel.FRM_VAC_BONUS_POTENTIALS)]
        //public Decimal BonusPotential { get; set; }
        public string BonusPotential { get; set; }

        [Display(Name = BELabel.FRM_VAC_SHOW_ON_WEB)]
        public Boolean ShowOnWebBonus { get; set; }


        public Boolean IsDelete { get; set; }

        public Int32 VacancyState { get; set; }
        [Display(Name = BELabel.FRM_VAC_START_DATE)]
        //[Required(ErrorMessage = "Start Date is Required")]
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

        [Display(Name = BELabel.FRM_VAC_END_DATE)]
        //[Required(ErrorMessage = "End Date Required")]
        public string EndDateText
        {
            get
            {
                if (EndDate != DateTime.MinValue && EndDate != null)
                    return ((DateTime)EndDate).ToString(Common.DateFormatConstant.US_FORMAT);
                else
                    return "";
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    EndDate = Convert.ToDateTime(value, System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat);
            }
        }


        public string PostedOnDateText
        {
            get
            {
                if (PostedOn != DateTime.MinValue)
                    return PostedOn.ToString(Common.DateFormatConstant.US_FORMAT);
                else
                    return "";
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    PostedOn = Convert.ToDateTime(value, System.Globalization.CultureInfo.GetCultureInfo("en-US").DateTimeFormat);
            }
        }

        public override ATSPrivilage Privilage
        {
            get { return ATSPrivilage.Vacancy; }
        }


        public bool ConfirmVacancyDetails { get; set; }

        public bool ConfirmJobDescription { get; set; }

        public bool ConfirmCompensationInfo { get; set; }

        public bool ConfirmApplicationreview { get; set; }

        public bool IsVacancyConfirmed
        {
            get
            {

                if (!ConfirmVacancyDetails || !ConfirmJobDescription || !ConfirmCompensationInfo || !ConfirmApplicationreview)
                { return false; }
                else
                { return true; }
            }
        }

        public string VacancyStatusText { get; set; }

        public string JobTypeText { get; set; }

        [Display(Name = BELabel.FRM_VAC_HOURS_PER_WEEK)]
        public int? HoursPerWeek { get; set; }

        public string EmploymentTypeText { get; set; }

        public string PositionTypeName { get; set; }

        public List<VacancyApplicant> ApplicantVacancyList { get; set; }

        public ATSResume objATSResume { get; set; }

        public string LocationText { get; set; }

        public Int64 PublicCode { get; set; }

        public List<RecordExists> RecordExistsCount { get; set; }

        public List<VacancyRound> VacancyRoundList { get; set; }

        public int RndCount { get; set; }

        public Int32 Applicants { get; set; }

        public List<BindEnumDropDown> VacStatusList { get; set; }

        public List<ApplicationBasedStatus> ApplicationBasedStatusList { get; set; }

        public bool IsDeclineApplication { get; set; }

        public int CntClose { get; set; }
        public int CntOpen { get; set; }
        public int CntArchieve { get; set; }
        public int CntDraft { get; set; }
        public int CntAll { get; set; }

        public bool IsNewUser { get; set; }
        public Guid AppId { get; set; }

        public bool IsSendApplyEmail { get; set; }

        public bool IsSendWithdrawEmail { get; set; }

        public Guid ApplyEmailTemplateId { get; set; }

        public Guid WithdrawEmailTemplateId { get; set; }
        public bool IsSaveHistroy { get; set; }

        [Display(Name = BECommonConst.ONBOARDING_MANAGER)]
        [Required(ErrorMessage = "Onboarding Manager is Required")]
        public Guid OnBoardManagerId { get; set; }

        public Int32 VacRndCount { get; set; }
        [Display(Name = BELabel.LST_VAC_OWNER)]
        public string CreatedByName { get; set; }

        public int DaysOpen { get; set; }




        [Display(Name = BELabel.FRM_VAC_ACT_CODE)]
        public string ActivityCode { get; set; }

        [Display(Name = BELabel.FRM_VAC_ANNOUNCE_TYPE)]
        public string AnnouncementType { get; set; }
        [Display(Name = BELabel.FRM_VAC_FOA)]
        public string FundingOpptyNumber { get; set; }

        [Display(Name = BELabel.FRM_VAC_PROGRAM_OFFICER)]
        public Guid ProgramOfficer { get; set; }

        [Display(Name = BELabel.FRM_VAC_CASH_MATCH_REQ)]
        [ATSRange(0, 1000)]
        [ATSRequired(ErrorMessage = "{0}")]
        public decimal? CashMatchRequirement { get; set; }

        [ATSRequired(ErrorMessage = "{0}")]
        public DateTime AnnouncementDate { get; set; }


        [ATSRequired(ErrorMessage = "{0}")]
        public DateTime OpenDate { get; set; }


        [ATSRequired(ErrorMessage = "{0}")]
        public DateTime ApplicationDueDate { get; set; }


        [ATSRequired(ErrorMessage = "{0}")]
        public DateTime ExpirationDate { get; set; }


        [ATSRequired(ErrorMessage = "{0}")]
        [Display(Name = BELabel.FRM_VAC_ANNOUNCE_DATE)]
        public string AnnouncementDateText
        {
            get
            {
                if (AnnouncementDate != DateTime.MinValue && AnnouncementDate != null)
                    return ((DateTime)AnnouncementDate).ToString(BEDateFormatConst.US_FORMAT);
                else
                    return string.Empty;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    AnnouncementDate = Convert.ToDateTime(value, System.Globalization.CultureInfo.GetCultureInfo(BEDateFormatConst.US_CULTURE_INFO).DateTimeFormat);
            }
        }




        [ATSRequired(ErrorMessage = "{0}")]
        [Display(Name = BELabel.FRM_VAC_OPEN_DATE)]
        public string OpenDateText
        {
            get
            {
                if (OpenDate != DateTime.MinValue && OpenDate != null)
                    return ((DateTime)OpenDate).ToString(BEDateFormatConst.US_FORMAT);
                else
                    return string.Empty;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    OpenDate = Convert.ToDateTime(value, System.Globalization.CultureInfo.GetCultureInfo(BEDateFormatConst.US_CULTURE_INFO).DateTimeFormat);
            }
        }

        [ATSRequired(ErrorMessage = "{0}")]
        [Display(Name = BELabel.FRM_VAC_APP_DUE_DATE)]
        public string ApplicationDueDateText
        {
            get
            {
                if (ApplicationDueDate != DateTime.MinValue && ApplicationDueDate != null)
                    return ((DateTime)ApplicationDueDate).ToString(BEDateFormatConst.US_FORMAT);
                else
                    return string.Empty;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    ApplicationDueDate = Convert.ToDateTime(value, System.Globalization.CultureInfo.GetCultureInfo(BEDateFormatConst.US_CULTURE_INFO).DateTimeFormat);
            }
        }

        [ATSRequired(ErrorMessage = "{0}")]
        [Display(Name = BELabel.FRM_VAC_EXP_DATE)]
        public string ExpirationDateText
        {
            get
            {
                if (ExpirationDate != DateTime.MinValue && ExpirationDate != null)
                    return ((DateTime)ExpirationDate).ToString(BEDateFormatConst.US_FORMAT);
                else
                    return string.Empty;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    ExpirationDate = Convert.ToDateTime(value, System.Globalization.CultureInfo.GetCultureInfo(BEDateFormatConst.US_CULTURE_INFO).DateTimeFormat);
            }
        }

        [Display(Name = BELabel.FRM_VAC_FUND_AMOUNT)]
        public Decimal FundAmount { get; set; }
        //CR-35 MAX AND MIN FUNDING AMMOUNT BY MUNEENDRA START( get and binding the data)
        [Display(Name = BELabel.FRM_VAC_MAX_FUND_AMOUNT)]
        public Decimal MaxFundAmount { get; set; }
        [Display(Name = BELabel.FRM_VAC_MIN_FUND_AMOUNT)]
        public Decimal MinFundAmount { get; set; }
        [Display(Name = BELabel.FRM_VAC_TOTAL_FUNDED_TODATE)]
        public Decimal TotalFundedToDate { get; set; }
        [Display(Name = BELabel.FRM_VAC_TOTAL_NUMBER_AWARDS)]
        public int TotalNumberOfAwards { get; set; }
        [Display(Name = BELabel.FRM_VAC_REMAINIG_FUNDS)]
        public Decimal RemainingFunds { get; set; }
        //CR-35 END

        [ATSRequired(ErrorMessage = "{0}")]
        [ATSRegularExpression(@"[0-9.,]+([0-9]+)*,?", ErrorMessage = "Invalid " + BELabel.FRM_VAC_FUND_AMOUNT)]
        public string FundAmountText
        {
            get
            {
                return BECommon.CommonFunction.AddCommaAfterThreeDigit(FundAmount);
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    FundAmount = Convert.ToDecimal(value);
            }
        }

    }
}
