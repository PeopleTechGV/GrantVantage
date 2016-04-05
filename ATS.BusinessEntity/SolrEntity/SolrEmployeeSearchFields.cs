using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolrNet.Attributes;
using ATS.BusinessEntity.Attributes;

namespace ATS.BusinessEntity.SolrEntity
{
    public class SolrEmployeeSearchFields : ISolrUserDetail, ISolrAchievement, ISolrAssociations, ISolrAvailability, ISolrEducationHistory, ISolrEmploymentHistory
        , ISolrLicenceAndCertifications,  ISolrPublicationHistory, ISolrReference, ISolrSkills, ISolrSpeakingEventHistory
    {
        [SolrUniqueKey("ProfileId")]
        public String StrProfileId { get; set; }
        [SolrField("ClientId")]
        public String StrClientId { get; set; }

        [SolrField("score")]
        public double Score { get; set; }
        #region ISolrUserDetail

        //[SolrUniqueKey("ProfileId")]
        //public String SolrProfileId 
        //{ get; set; }

        [EmployeeSearch("PersonalInfo", "First Name", "FirstName")]
        [SolrField("FirstName")]
        public string FirstName
        { get; set; }
        [SolrField("UserId")]
        public string UserId
        { get; set; }
        [EmployeeSearch("PersonalInfo", "Middle Name", "MiddleName")]
        [SolrField("MiddleName")]
        public string MiddleName
        { get; set; }
        [SolrField("LastName")]
        public string LastName
        { get; set; }
        [SolrField("Affix")]
        public string Affix
        { get; set; }
        [SolrField("Address")]
        public string Address
        { get; set; }        
        [SolrField("City")]
        public string City
        { get; set; }
        [SolrField("State")]
        public string State
        { get; set; }
        [SolrField("Zip")]
        public string Zip
        { get; set; }        
        [SolrField("BusinessPhoneNo")]
        public string BusinessPhoneNo
        { get; set; }
        [SolrField("HomePhone")]
        public string HomePhone
        { get; set; }
        [SolrField("Fax")]
        public string Fax
        { get; set; }
        [SolrField("Pager")]
        public string Pager
        { get; set; }
        [SolrField("WorkEmail")]
        public string WorkEmail
        { get; set; }
        public bool IsBlocked { get; set; }
        
        #endregion

        #region ISolrEmployeeFields
        [EmployeeSearch("Employee", "Description", "ACDescription")]
        [SolrField("ACDescription")]
        public ICollection<string> ACDescription
        { get; set; }
        [SolrField("ACIssuingAuthority")]
        public ICollection<string> ACIssuingAuthority
        { get; set; }
        #endregion

        #region ISolrAssociations
        [SolrField("ASName")]
        public ICollection<string> ASName
        { get; set; }
        [SolrField("ASAssociationType")]
        public ICollection<string> ASAssociationType
        { get; set; }
        #endregion

        #region ISolrAvailability
        public String AVWillingToWorkOverTime { get; set; }
        public String AVRelativesWorkingInCompany { get; set; }
        public String AVRelativesIfYes { get; set; }
        public String AVSubmittedApplicationBefore { get; set; }
        public String AVEligibleToWorkInUS { get; set; }
        #endregion

        #region ISolrEducationHistory
        [SolrField("EHInstitutionName")]
        public ICollection<string> EHInstitutionName { get; set; }
        [SolrField("EHMeasureSystem")]
        public ICollection<string> EHMeasureSystem { get; set; }
        [SolrField("EHDegreeType")]
        public ICollection<string> EHDegreeType { get; set; }
        [SolrField("EHMajorSubject")]
        public ICollection<string> EHMajorSubject { get; set; }
        [SolrField("EHCity")]
        public ICollection<string> EHCity { get; set; }
        [SolrField("EHState")]
        public ICollection<string> EHState { get; set; }
        [SolrField("EHCountry")]
        public ICollection<string> EHCountry { get; set; }
        [SolrField("EHMeasureValue")]
        public ICollection<string> EHMeasureValue { get; set; }
        #endregion

        #region  ISolrEmploymentHistory
        [SolrField("EMHCompanyName")]
        public ICollection<string> EMHCompanyName { get; set; }
        [SolrField("EMHMayWeContact")]
        public ICollection<string> EMHMayWeContact { get; set; }
        [SolrField("EMHSupervisorName")]
        public ICollection<string> EMHSupervisorName { get; set; }
        [SolrField("EMHAddress")]
        public ICollection<string> EMHAddress { get; set; }
        [SolrField("EMHCity")]
        public ICollection<string> EMHCity { get; set; }
        [SolrField("EMHZip")]
        public ICollection<string> EMHZip { get; set; }
        [SolrField("EMHExperience")]
        public ICollection<string> EMHExperience { get; set; }
        [SolrField("EMHDutiesAndResponsibilities ")]
        public ICollection<string> EMHDutiesAndResponsibilities { get; set; }
        #endregion

        //#region ISolrExecutiveSummary

        //string ESExecutiveSummaryDetails { get; set; }
        //#endregion

        #region ISolrLicenceAndCertifications
         [SolrField("LCName")]
        public ICollection<string> LCName { get; set; }
         [SolrField("LCIssuingAuthority")]
        public ICollection<string> LCIssuingAuthority { get; set; }
         [SolrField("LCDescription")]
        public ICollection<string> LCDescription { get; set; }
        #endregion

        //#region ISolrObjective
        //     [SolrField("OBObjectiveDetails")]
        //public string OBObjectiveDetails { get; set; }
        //#endregion

        #region ISolrPublicationHistory
           [SolrField("PHTitle")]
        public ICollection<string> PHTitle { get; set; }
           [SolrField("PHType")]
        public ICollection<string> PHType { get; set; }
           [SolrField("PHName")]
        public ICollection<string> PHName { get; set; }
        #endregion

        #region ISolrReference
         [SolrField("RFReferenceName")]
        public ICollection<string> RFReferenceName { get; set; }
         [SolrField("RFReferenceEmail")]
        public ICollection<string> RFReferenceEmail { get; set; }
         [SolrField("RFReferencePhone")]
        public ICollection<string> RFReferencePhone { get; set; }
        #endregion

        #region ISolrSkills
          [SolrField("SKSkillAndQualification")]
        public ICollection<string> SKSkillAndQualification { get; set; }
          [SolrField("SKDescription")]
        public ICollection<string> SKDescription { get; set; }
          [SolrField("SKSkillType")]
        public ICollection<string> SKSkillType { get; set; }
        #endregion

        #region ISolrSpeakingEventHistory
          [SolrField("SEHTitle")]
        public ICollection<string> SEHTitle { get; set; }
          [SolrField("SEHEventName")]
        public ICollection<string> SEHEventName { get; set; }
          [SolrField("SEHEventType")]
        public ICollection<string> SEHEventType { get; set; }
          [SolrField("SEHLocation")]
        public ICollection<string> SEHLocation { get; set; }
          [SolrField("SEHLink")]
        public ICollection<string> SEHLink { get; set; }
        #endregion
    }
}
