using ATS.BusinessEntity.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BESrp = ATS.BusinessEntity.SRPEntity;
using BECommonConst = ATS.BusinessEntity.Common.CommonConstant;
using BELanCat = ATS.BusinessEntity.Common.VacancyTemplate;
using System.ComponentModel.DataAnnotations;
using BELabel = ATS.BusinessEntity.Common.VacancyConstant;
using BECommon = ATS.BusinessEntity.Common;
using BEDateFormatConst = ATS.BusinessEntity.Common.DateFormatConstant;

namespace ATS.BusinessEntity
{
    public class TVac : ClientBaseEntity
    {
        [ATSRequired(ErrorMessage = "{0}")]
        public Guid TVacId { get; set; }

        [ATSStringLength(500)]
        [Display(Name = BELanCat.FRM_VACANCYTEMPLATE)]
        [ATSRequired(ErrorMessage = "{0}")]
        public String Name { get; set; }

        public Guid LanguageId { get; set; }

        public Guid UserId { get; set; }

        [Display(Name = BELanCat.FRM_DIVISION)]
        [ATSRequired(ErrorMessage = "{0}")]
        public Guid DivisionId { get; set; }

        [Display(Name = BELanCat.FRM_POSITION)]
        [ATSRequired(ErrorMessage = "{0}")]
        public Guid PositionTypeId { get; set; }

        //*********Chagne request **********

        [Display(Name = BELabel.FRM_VAC_JOB_TYPE)]
        [ATSRequired(ErrorMessage = "{0}")]
        public Int32 JobType { get; set; }

        [Display(Name = BELabel.FRM_VAC_EMPLOYMENT_TYPE)]
        [ATSRequired(ErrorMessage = "{0}")]
        public Int32 EmploymentType { get; set; }


        [Display(Name = BELabel.FRM_VAC_LOCATION)]
        [ATSRequired(ErrorMessage = "{0}")]
        public Guid Location { get; set; }

        [Display(Name = BELabel.FRM_VAC_TOTAL_POSITIONS)]
        //[ATSRequired(ErrorMessage = "{0}")]
        //[RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Total Position(s) is not valid")]
        public Int32? TotalPositions { get; set; }

        [Display(Name = BELabel.FRM_VAC_REMAING_POSITIONS)]
        [ATSRequired(ErrorMessage = "{0}")]
        public Int32 RemainingPositions { get; set; }

        [Display(Name = BELabel.FRM_VAC_FEATURED_ON_WEB)]
        public Boolean FeaturedOnWeb { get; set; }

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

        [Display(Name = BELabel.FRM_VAC_SHOW_ON_WEB)]
        public Boolean ShowOnWebSal { get; set; }

        [Display(Name = BELabel.FRM_VAC_HOURLY_MIN)]
        public Decimal HourlyMin { get; set; }

        [ATSRegularExpression(@"[0-9.,]+([0-9]+)*,?", ErrorMessage = "Invalid HourlyMin")]
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

        [ATSRegularExpression(@"[0-9.,]+([0-9]+)*,?", ErrorMessage = "Invalid HourlyMax")]
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
        public string Commission { get; set; }


        [Display(Name = BELabel.FRM_VAC_SHOW_ON_WEB)]
        public Boolean ShowOnWebCommission { get; set; }

        [Display(Name = BELabel.FRM_VAC_BONUS_POTENTIALS)]
        public string BonusPotential { get; set; }

        [Display(Name = BELabel.FRM_VAC_SHOW_ON_WEB)]
        public Boolean ShowOnWebBonus { get; set; }


        public string JobTypeText { get; set; }

        [Display(Name = BELabel.FRM_VAC_HOURS_PER_WEEK)]
        public int? HoursPerWeek { get; set; }

        public string EmploymentTypeText { get; set; }

        public string PositionTypeName { get; set; }

        //****************
        public List<EntityLanguage> EntityLanguageList { get; set; }

        public override ATSPrivilage Privilage
        {
            get
            {
                return ATSPrivilage.VacancyTemplate;
            }

        }
        public List<TVacancyRound> VacancyRoundList { get; set; }

        public int RndCount { get; set; }


        [ATSRequired(ErrorMessage = "{0}")]
        [Display(Name = BELabel.FRM_VAC_ACT_CODE)]
        public string ActivityCode { get; set; }


        [ATSRequired(ErrorMessage = "{0}")]
        [Display(Name = BELabel.FRM_VAC_ANNOUNCE_TYPE)]
        public string AnnouncementType { get; set; }


        [ATSRequired(ErrorMessage = "{0}")]
        [Display(Name = BELabel.FRM_VAC_FOA)]
        public string FundingOpptyNumber { get; set; }


        [ATSRequired(ErrorMessage = "{0}")]
        [Display(Name = BELabel.FRM_VAC_PROGRAM_OFFICER)]
        public Guid ProgramOfficer { get; set; }



        [ATSRequired(ErrorMessage = "{0}")]
        [ATSRange(0, 1000)]
        [Display(Name = BELabel.FRM_VAC_CASH_MATCH_REQ)]
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

        public Vacancy ObjVacancy { get { return ReturnVacancy(); } }

        [Display(Name = "Application Instruction")]
        public string ApplicationInstruction { get; set; }

        public List<TAppInstructionDocs> objTAppInstructionDocList { get; set; }

        [Display(Name = BELabel.FRM_VAC_SHOW_ON_WEB)]
        public Boolean ShowOnWebAppInstruction { get; set; }

        [Display(Name = BELabel.FRM_VAC_SHOW_ON_WEB)]
        public Boolean ShowOnWebAppInstructionDoc { get; set; }

        private Vacancy ReturnVacancy()
        {
            Vacancy Vac = new Vacancy();
            Vac.LanguageId = this.LanguageId;
            Vac.JobTitle = this.Name;
            Vac.DivisionId = this.DivisionId;
            Vac.PositionTypeId = this.PositionTypeId;
            Vac.JobType = this.JobType;
            Vac.HoursPerWeek = this.HoursPerWeek;
            Vac.EmploymentType = this.EmploymentType;
            Vac.Location = this.Location;
            Vac.TotalPositions = this.TotalPositions;
            Vac.RemainingPositions = this.RemainingPositions;
            Vac.FeaturedOnWeb = this.FeaturedOnWeb;
            Vac.JobDescription = this.JobDescription;
            Vac.ShowOnWebJobDescription = this.ShowOnWebJobDescription;
            Vac.DutiesAndResponsibilities = this.DutiesAndResponsibilities;
            Vac.ShowOnWebDuties = this.ShowOnWebDuties;
            Vac.SkillsAndQualification = this.SkillsAndQualification;
            Vac.ShowOnWebSkills = this.ShowOnWebSkills;
            Vac.Benefits = this.Benefits;
            Vac.ShowOnWebBenefits = this.ShowOnWebBenefits;
            Vac.SalaryMin = this.SalaryMin;
            Vac.SalaryMax = this.SalaryMax;
            Vac.ShowOnWebSal = this.ShowOnWebSal;
            Vac.HourlyMin = this.HourlyMin;
            Vac.HourlyMax = this.HourlyMax;
            Vac.ShowonWebHour = this.ShowonWebHour;
            Vac.Commission = this.Commission;
            Vac.ShowOnWebCommission = this.ShowOnWebCommission;
            Vac.BonusPotential = this.BonusPotential;
            Vac.ShowOnWebBonus = this.ShowOnWebBonus;
            Vac.ActivityCode = this.ActivityCode;
            Vac.AnnouncementType = this.AnnouncementType;
            Vac.FundingOpptyNumber = this.FundingOpptyNumber;
            Vac.ProgramOfficer = this.ProgramOfficer;
            Vac.CashMatchRequirement = this.CashMatchRequirement;
            Vac.AnnouncementDate = this.AnnouncementDate;
            Vac.OpenDate = this.OpenDate;
            Vac.ApplicationDueDate = this.ApplicationDueDate;
            Vac.ExpirationDate = this.ExpirationDate;
            Vac.FundAmount = this.FundAmount;
            Vac.ApplicationInstruction = this.ApplicationInstruction;
            Vac.ShowOnWebAppInstruction = this.ShowOnWebAppInstruction;
            Vac.ShowOnWebAppInstructionDoc = this.ShowOnWebAppInstructionDoc;
            return Vac;
        }
    }
}
