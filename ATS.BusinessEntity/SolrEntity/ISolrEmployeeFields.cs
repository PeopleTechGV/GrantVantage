using ATS.BusinessEntity.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity.SolrEntity
{
    public interface ISolrUserDetail
    {
        double Score { get; set; }
        
        string  StrProfileId { get; set; }
        string UserId { get; set; }
        string FirstName { get; set; }
        string MiddleName { get; set; }
        string LastName { get; set; }
        string Affix { get; set; }
        string Address { get; set; }        
        string City { get; set; }
        string State { get; set; }
        string Zip { get; set; }        
        string BusinessPhoneNo { get; set; }
        string HomePhone { get; set; }
        string Fax { get; set; }
        string Pager { get; set; }
        string WorkEmail { get; set; }
        bool IsBlocked { get; set; }
    }

    public interface ISolrAchievement
    {
        ICollection<string> ACDescription { get; set; }
        ICollection<string> ACIssuingAuthority { get; set; }
    }
    public interface ISolrAssociations
    {
        ICollection<string> ASName { get; set; }
        ICollection<string> ASAssociationType { get; set; }
    }

    public interface ISolrAvailability
    {
        String AVWillingToWorkOverTime { get; set; }
        String AVRelativesWorkingInCompany { get; set; }
        String AVRelativesIfYes { get; set; }
        String AVSubmittedApplicationBefore { get; set; }
        String AVEligibleToWorkInUS { get; set; }
    }
    public interface ISolrEducationHistory
    {
        ICollection<string> EHInstitutionName { get; set; }
        ICollection<string> EHMeasureSystem { get; set; }
        ICollection<string> EHDegreeType { get; set; }
        ICollection<string> EHMajorSubject { get; set; }
        ICollection<string> EHCity { get; set; }
        ICollection<string> EHState { get; set; }
        ICollection<string> EHCountry { get; set; }
        ICollection<string> EHMeasureValue { get; set; }
    }
    public interface ISolrEmploymentHistory
    {
        ICollection<string> EMHCompanyName { get; set; }
        ICollection<string> EMHMayWeContact { get; set; }
        ICollection<string> EMHSupervisorName { get; set; }
        ICollection<string> EMHAddress { get; set; }
        ICollection<string> EMHCity { get; set; }
        ICollection<string> EMHZip { get; set; }
        ICollection<string> EMHExperience { get; set; }
        ICollection<string> EMHDutiesAndResponsibilities { get; set; }
    }
    public interface ISolrExecutiveSummary
    {
        string ESExecutiveSummaryDetails { get; set; }
    }
    public interface ISolrLicenceAndCertifications
    {
        ICollection<string> LCName { get; set; }
        ICollection<string> LCIssuingAuthority { get; set; }
        ICollection<string> LCDescription { get; set; }
    }
    public interface ISolrObjective
    {
        string OBObjectiveDetails { get; set; }
    }
    public interface ISolrPublicationHistory
    {
        ICollection<string> PHTitle { get; set; }
        ICollection<string> PHType { get; set; }
        ICollection<string> PHName { get; set; }
    }
    public interface ISolrReference
    {
        ICollection<string> RFReferenceName { get; set; }
        ICollection<string> RFReferenceEmail { get; set; }
        ICollection<string> RFReferencePhone { get; set; }
    }

    public interface ISolrSkills
    {
        ICollection<string> SKSkillAndQualification { get; set; }
        ICollection<string> SKDescription { get; set; }
        ICollection<string> SKSkillType { get; set; }
    }

    public interface ISolrSpeakingEventHistory
    {
        ICollection<string> SEHTitle { get; set; }
        ICollection<string> SEHEventName { get; set; }
        ICollection<string> SEHEventType { get; set; }
        ICollection<string> SEHLocation { get; set; }
        ICollection<string> SEHLink { get; set; }
    }

}
