using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolrNet.Attributes;
using BECommon = ATS.BusinessEntity.Common;

namespace ATS.BusinessEntity.SolrEntity
{
    public class SolrSearchFields : ISearchFields, ISettingFields, IDisplayFields
    {
        /*
        "DivisionText": "सीईओ",
        "FeaturedOnWeb": "true",
        "EmploymentTypeText": "स्थायी",
        "VacancystatusText": false,
        "SalaryMin": 0,
        "JobDescription": "Analyst",
        "TotalPositions": 0,
        "LanguageId": "310613D3-1CD4-4A34-BCF4-36A8697EE242",
        "DutiesAndResponsibilities": "dsa",
        "ShowOnWebDuties": true,
        "PostedOn": "2014-02-11T01:10:12.39Z",
        "ShowOnWebSkills": true,
        "ShowOnWebJobDescription": true,
        "ShowOnWeb": true,
        "ShowOnWebBenefits": false,
        "Location": "Ny",
        "ClientId": "DF9C2730-08DE-467E-BC8D-3D065FE1F855",
        "VacancyId": "C75C67A4-7A23-44DE-8D61-3F82A5CFC899",
        "SalaryMax": 0,
        "SkillsAndQualification": "das",
        "VacancyStateText": "सक्रिय",
        "JobTypeText": "अंशकालिक",
        "JobTitle": "Business Analyst",
        "Benefits": "dsa",
        "ShowOnWebSal": false,
        "PositionID": "ANA123"
         */
        [SolrUniqueKey("VacancyId")]
        public string StrVacancyId { get; set; }
        [SolrField("ClientId")]
        public string ClientId { get; set; }
        [SolrField("PositionName")]
        public string PositionName { get; set; }
        [SolrField("JobTitle")]
        public string JobTitle { get; set; }
        [SolrField("PostedOn")]
        public DateTime PostedOn { get; set; }

        public DateTime LocalPostedOn { get { return PostedOn.ToLocalTime(); } }

        [SolrField("PositionID")]
        public string PositionID { get; set; }
        [SolrField("VacancystatusText")]
        public bool VacancyStatus { get; set; }
        [SolrField("JobTypeText")]
        public string JobType { get; set; }
        [SolrField("EmploymentTypeText")]
        public string EmploymentType { get; set; }
        [SolrField("Location")]
        public string Location { get; set; }


        [SolrField("TotalPositions")]
        public int TotalPositions { get; set; }
        [SolrField("ShowOnWeb")]
        public string ShowOnWeb { get; set; }
        [SolrField("FeaturedOnWeb")]
        public bool FeaturedOnWeb { get; set; }

        [SolrField("JobDescription")]
        public string JobDescription { get; set; }

        [SolrField("ShowOnWebJobDescription")]
        public bool ShowOnWebJobDescription { get; set; }

        [SolrField("DutiesAndResponsibilities")]
        public string DutiesAndResponsibilities { get; set; }

        [SolrField("ShowOnWebDuties")]
        public bool ShowOnWebDuties { get; set; }

        [SolrField("SkillsAndQualification")]
        public string SkillsAndQualification { get; set; }

        [SolrField("ShowOnWebSkills")]
        public bool ShowOnWebSkills { get; set; }

        [SolrField("Benefits")]
        public string Benefits { get; set; }

        [SolrField("ShowOnWebBenefits")]
        public bool ShowOnWebBenefits { get; set; }

        [SolrField("ShowOnWebSal")]
        public bool ShowOnWebSal { get; set; }

        [SolrField("SalaryMin")]
        public double SalaryMin { get; set; }

        public string SalaryMinText
        {
            get
            {
                return BECommon.CommonFunction.AddCommaAfterThreeDigit(SalaryMin);
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    SalaryMin = Convert.ToInt32(value);
            }
        }

        [SolrField("SalaryMax")]
        public double SalaryMax { get; set; }

        public string SalaryMaxText
        {
            get
            {
                return BECommon.CommonFunction.AddCommaAfterThreeDigit(SalaryMax);
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    SalaryMax = Convert.ToInt32(value);
            }
        }

        [SolrField("CreatedBy")]
        public string CreatedBy { get; set; }

        [SolrField("ActivityCode")]
        public string ActivityCode { get; set; }

        [SolrField("AnnouncementType")]
        public string AnnouncementType { get; set; }

        [SolrField("FundingOpptyNumber")]
        public string FundingOpptyNumber { get; set; }

        [SolrField("ProgramOfficer")]
        public Guid ProgramOfficer { get; set; }

        [SolrField("CashMatchRequirement")]
        public int CashMatchRequirement { get; set; }

        [SolrField("AnnouncementDate")]
        public DateTime AnnouncementDate { get; set; }

        [SolrField("OpenDate")]
        public DateTime OpenDate { get; set; }

        [SolrField("ApplicationDueDate")]
        public DateTime ApplicationDueDate { get; set; }

        [SolrField("ExpirationDate")]
        public DateTime ExpirationDate { get; set; }

        [SolrField("FundAmount")]
        public decimal FundAmount { get; set; }

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
