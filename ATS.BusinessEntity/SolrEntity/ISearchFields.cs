using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity.SolrEntity
{
    public interface IDisplayFields
    {
         string JobTitle { get; set; }
        DateTime PostedOn { get; set; }
        string Location { get; set; }

        string JobDescription { get; set; }
        bool ShowOnWebJobDescription { get; set; }
        string CreatedBy { get; set; }

    }
    public interface ISettingFields
    {
        string StrVacancyId { get; set; }
    }
    public interface ISearchFields
    {
        
        
        
         string ClientId { get; set; }
        
         string PositionName { get; set; }
        
         string PositionID { get; set; }
        
         bool VacancyStatus { get; set; }
        
         string JobType { get; set; }
        
         string EmploymentType { get; set; }
        
         int TotalPositions { get; set; }
        
         string ShowOnWeb { get; set; }
        
         bool FeaturedOnWeb { get; set; }
        
         string DutiesAndResponsibilities { get; set; }

         bool ShowOnWebDuties { get; set; }
        
         string SkillsAndQualification { get; set; }
        
         bool ShowOnWebSkills { get; set; }

         string Benefits { get; set; }
        
         bool ShowOnWebBenefits { get; set; }
        
         bool ShowOnWebSal { get; set; }
        
         double SalaryMin { get; set; }
        
         double SalaryMax { get; set; }
    }
}
