using System;
using System.Web.Mvc;
using System.Linq;
using System.Web;
using System.IO;
using ATSModel = ATS.WebUi.Models;
using System.Reflection;
using System.Text;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using ATS.WebUi.Models;
using Newtonsoft.Json;
using BEClient = ATS.BusinessEntity;
using RChilliHelper = ATS.RChilliParserHelper;
using System.Globalization;
using ATS.BusinessEntity;

namespace ATS.WebUi.Common
{
    public static class RChilliObjMapping
    {
        private const int SKILL_LIMIT = 10;

        public static BEClient.UserDetails GetContact(RChilliHelper.SovrenParseResume pRChilliParseResume, Guid ContactId)
        {
            try
            {
                BEClient.UserDetails objContact = new BEClient.UserDetails();
                if (!ContactId.Equals(Guid.Empty))
                    objContact.UserId = ContactId;

                #region personal detail
                objContact.FirstName = pRChilliParseResume.objPersonalInformation.GivenName;
                objContact.MiddleName = pRChilliParseResume.objPersonalInformation.MiddleName;
                objContact.LastName = pRChilliParseResume.objPersonalInformation.FamilyName;
                objContact.Affix = pRChilliParseResume.objPersonalInformation.Affix;                
                #endregion

                #region Contact Detail
                if (pRChilliParseResume.objContactMethod.Count > 0)
                {
                    for (int k = 0; k < pRChilliParseResume.objContactMethod.Count; k++)
                    {
                        if (pRChilliParseResume.objContactMethod[k].objPostalAddress != null)
                        {   
                            objContact.Zip = pRChilliParseResume.objContactMethod[k].objPostalAddress.PostalCode;
                            objContact.State = pRChilliParseResume.objContactMethod[k].objPostalAddress.Region;
                            objContact.City = pRChilliParseResume.objContactMethod[k].objPostalAddress.Municipality;
                            if (pRChilliParseResume.objContactMethod[k].objPostalAddress.DeliveryAddresslst!=null && pRChilliParseResume.objContactMethod[k].objPostalAddress.DeliveryAddresslst.Count > 0)
                            { 
                            for (int v = 0; v < pRChilliParseResume.objContactMethod[k].objPostalAddress.DeliveryAddresslst[0].objAddLine.Count; v++)
                            {
                                if (objContact.Address != null)
                                    objContact.Address = objContact.Address + ", " + pRChilliParseResume.objContactMethod[k].objPostalAddress.DeliveryAddresslst[0].objAddLine[v].AddLine;
                                else
                                    objContact.Address = pRChilliParseResume.objContactMethod[k].objPostalAddress.DeliveryAddresslst[0].objAddLine[v].AddLine;
                            }
                        }
                        }
                        else if (pRChilliParseResume.objContactMethod[k].objMobile != null)
                            objContact.MobilePhone = pRChilliParseResume.objContactMethod[k].objMobile.MO_FormattedNumber;
                        else if (pRChilliParseResume.objContactMethod[k].objTelephone != null)
                            objContact.HomePhone = pRChilliParseResume.objContactMethod[k].objTelephone.Tel_FormattedNumber;
                        else if (pRChilliParseResume.objContactMethod[k].objEmail != null && objContact.WorkEmail == null)
                            objContact.WorkEmail = pRChilliParseResume.objContactMethod[k].objEmail.InternetEmailAddress;
                        else if (pRChilliParseResume.objContactMethod[k].objEmail != null && objContact.HomeEmail == null)
                            objContact.HomeEmail = pRChilliParseResume.objContactMethod[k].objEmail.InternetEmailAddress;
                        else if (pRChilliParseResume.objContactMethod[k].objWebAdd != null)
                            objContact.Website = pRChilliParseResume.objContactMethod[k].objWebAdd.InternetWebAddress;
                    }
                }
                #endregion

                return objContact;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static BEClient.ExecutiveSummary GetExecutiveSummary(RChilliHelper.SovrenParseResume pRChilliParseResume, Guid ContactId, Guid ProfileId)
        {
            try
            {
                BEClient.ExecutiveSummary objExecutiveSummary = new ExecutiveSummary();

                if (!ContactId.Equals(Guid.Empty))
                    objExecutiveSummary.UserId = ContactId;

                objExecutiveSummary.ProfileId = ProfileId;

                objExecutiveSummary.ExecutiveSummaryDetails = pRChilliParseResume.objExecutivesummary.ExecutiveSummaryDetail;

                return objExecutiveSummary;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static BEClient.Objective GetObjective(RChilliHelper.SovrenParseResume pRChilliParseResume, Guid ContactId, Guid ProfileId)
        {
            try
            {
                BEClient.Objective objObjective = new Objective();

                if (!ContactId.Equals(Guid.Empty))
                    objObjective.UserId = ContactId;

                objObjective.ProfileId = ProfileId;

                objObjective.ObjectiveDetails = pRChilliParseResume.objobjective.objective;

                return objObjective;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<BEClient.Languages> GetLanguages(RChilliHelper.SovrenParseResume pRChilliParseResume, Guid ContactId, Guid ProfileId)
        {
            try
            {
                List<BEClient.Languages> objLanguageslst = new List<Languages>();
                if (pRChilliParseResume.objLanguages != null)
                {
                    foreach (var v in pRChilliParseResume.objLanguages)
                    {
                        BEClient.Languages objLanguages = new Languages();

                        if (!ContactId.Equals(Guid.Empty))
                            objLanguages.UserId = ContactId;

                        objLanguages.ProfileId = ProfileId;

                        objLanguages.LanguageCode = v.LanguageCode;
                        objLanguages.Read = v.Read.ToLower() == "true" ? true : false;
                        objLanguages.Write = v.Write.ToLower() == "true" ? true : false;
                        objLanguages.Speak = v.Speak.ToLower() == "true" ? true : false;
                        objLanguages.Comments = v.Comments;

                        objLanguageslst.Add(objLanguages);
                    }
                }
                return objLanguageslst;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<BEClient.SpeakingEventHistory> GetSpeakingEventHistory(RChilliHelper.SovrenParseResume pRChilliParseResume, Guid ContactId, Guid ProfileId)
        {
            try
            {
                List<SpeakingEventHistory> objSpeakingEventHistorylst = new List<SpeakingEventHistory>();

                if (pRChilliParseResume.objSpeakingEventsHistory != null)
                {
                    foreach (var v in pRChilliParseResume.objSpeakingEventsHistory)
                    {
                        SpeakingEventHistory objSpeakingEventHistory = new SpeakingEventHistory();

                        if (!ContactId.Equals(Guid.Empty))
                            objSpeakingEventHistory.UserId = ContactId;

                        objSpeakingEventHistory.ProfileId = ProfileId;

                        objSpeakingEventHistory.Title = v.Title;
                        objSpeakingEventHistory.EventName = v.EventName;
                        objSpeakingEventHistory.EventType = v.EventType;
                        objSpeakingEventHistory.Location = v.Location;
                        objSpeakingEventHistory.Link = v.Link;

                        DateTime eventDate;
                        if (DateTime.TryParse(v.StartDate, out eventDate))
                        {
                            objSpeakingEventHistory.StartDate = eventDate;
                        }

                        objSpeakingEventHistorylst.Add(objSpeakingEventHistory);
                    }
                }
                return objSpeakingEventHistorylst;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<Associations> GetAssociations(RChilliHelper.SovrenParseResume pRChilliParseResume, Guid ContactId, Guid ProfileId)
        {
            try
            {
                List<Associations> objAssociationslst = new List<Associations>();
                if (pRChilliParseResume.objAssociation != null)
                {
                    foreach (var v in pRChilliParseResume.objAssociation)
                    {
                        Associations objAssociations = new Associations();

                        if (!ContactId.Equals(Guid.Empty))
                            objAssociations.UserId = ContactId;

                        objAssociations.ProfileId = ProfileId;

                        objAssociations.Name = v.Name;
                        objAssociations.Link = v.Link;
                        objAssociations.AssociationType = v.AssociationType;

                        DateTime startDate;
                        if (DateTime.TryParse(v.StartDate, out startDate))
                        {
                            objAssociations.StartDate = startDate;
                        }

                        DateTime endDate;
                        if (DateTime.TryParse(v.EndDate, out endDate))
                        {
                            objAssociations.EndDate = endDate;
                        }

                        for (int a = 0; a < v.objRoleLst.Count; a++)
                        {
                            if (objAssociations.Role != null)
                                objAssociations.Role = objAssociations.Role + ", " + v.objRoleLst[a].RoleName;
                            else
                                objAssociations.Role = v.objRoleLst[a].RoleName;
                        }

                        objAssociationslst.Add(objAssociations);
                    }
                }

                return objAssociationslst;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<Achievement> GetAchievement(RChilliHelper.SovrenParseResume pRChilliParseResume, Guid ContactId, Guid ProfileId)
        {
            try
            {
                List<Achievement> objAchievementlst = new List<Achievement>();

                if (pRChilliParseResume.objAchievement != null)
                {
                    foreach (var v in pRChilliParseResume.objAchievement)
                    {
                        Achievement objAchievement = new Achievement();

                        if (!ContactId.Equals(Guid.Empty))
                            objAchievement.UserId = ContactId;

                        objAchievement.ProfileId = ProfileId;

                        objAchievement.Description = v.Description;
                        objAchievement.IssuingAuthority = v.IssuingAuthority;

                        DateTime eventDate;
                        if (DateTime.TryParse(v.StringDate, out eventDate))
                        {
                            objAchievement.Date = eventDate;
                        }

                        objAchievementlst.Add(objAchievement);
                    }
                }
                return objAchievementlst;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<EmploymentHistory> GetEmploymentHistory(RChilliHelper.SovrenParseResume pRChilliParseResume, Guid ContactId, Guid ProfileId)
        {
            try
            {
                List<EmploymentHistory> objEmploymentHistorylst = new List<EmploymentHistory>();

                if (pRChilliParseResume.objEmploymentHistory != null)
                {
                    foreach (var v in pRChilliParseResume.objEmploymentHistory)
                    {
                        EmploymentHistory objEmploymentHistory = new EmploymentHistory();

                        if (!ContactId.Equals(Guid.Empty))
                            objEmploymentHistory.UserId = ContactId;

                        objEmploymentHistory.ProfileId = ProfileId;

                        objEmploymentHistory.CompanyName = v.EmployerOrgName;

                        objEmploymentHistory.MostRecentPosition = v.Title;

                        if (v.objLocLst != null)
                        {
                            objEmploymentHistory.City = v.objLocLst[0].Municipality;
                            objEmploymentHistory.State = v.objLocLst[0].Region;
                        }

                        objEmploymentHistory.DutiesAndResponsibilities = v.Description;

                        DateTime startDate;
                        if (DateTime.TryParse(v.StartDate, out startDate))
                        {
                            objEmploymentHistory.StartDate = startDate;
                        }
                        if (v.CurrentEmployer == null)
                        {
                            DateTime endDate;
                            if (DateTime.TryParse(v.EndDate, out endDate))
                            {
                                objEmploymentHistory.EndDate = endDate;
                            }
                        }

                        objEmploymentHistorylst.Add(objEmploymentHistory);
                    }
                }
                return objEmploymentHistorylst;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<EducationHistory> GetEducationHistory(RChilliHelper.SovrenParseResume pRChilliParseResume, Guid ContactId, Guid ProfileId)
        {
            try
            {
                List<EducationHistory> objEducationHistorylst = new List<EducationHistory>();

                if (pRChilliParseResume.objSchoolOrInstitution != null)
                {
                    foreach (var v in pRChilliParseResume.objSchoolOrInstitution)
                    {
                        for (int i = 0; i < v.objDegreeLst.Count; i++)
                        {
                            EducationHistory objEducationHistory = new EducationHistory();

                            if (!ContactId.Equals(Guid.Empty))
                                objEducationHistory.UserId = ContactId;

                            objEducationHistory.ProfileId = ProfileId;

                            objEducationHistory.InstitutionName = v.SchoolName;

                            objEducationHistory.Country = v.CountryCode;

                            objEducationHistory.City = v.Municipality;

                            objEducationHistory.State = v.Region;

                            objEducationHistory.MajorSubject = v.objDegreeLst[i].DegreeMajor;

                            objEducationHistory.MeasureSystem = v.objDegreeLst[i].MeasureSystem;

                            objEducationHistory.MeasureValue = v.objDegreeLst[i].MeasureValue;

                            DateTime degreeDate;
                            if (DateTime.TryParse(v.objDegreeLst[i].DegreeDate, out degreeDate))
                            {
                                objEducationHistory.DegreeDate = degreeDate;
                            }

                            objEducationHistory.Description = v.objDegreeLst[i].Comments;

                            objEducationHistorylst.Add(objEducationHistory);
                        }
                    }
                }

                return objEducationHistorylst;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<LicenceAndCertifications> GetLicenceAndCertifications(RChilliHelper.SovrenParseResume pRChilliParseResume, Guid ContactId, Guid ProfileId)
        {
            try
            {
                List<LicenceAndCertifications> objLicenceAndCertificationslst = new List<LicenceAndCertifications>();

                if (pRChilliParseResume.objLicenseOrCertification != null)
                {
                    foreach (var v in pRChilliParseResume.objLicenseOrCertification)
                    {
                        LicenceAndCertifications objLicenceAndCertifications = new LicenceAndCertifications();

                        if (!ContactId.Equals(Guid.Empty))
                            objLicenceAndCertifications.UserId = ContactId;

                        objLicenceAndCertifications.ProfileId = ProfileId;

                        objLicenceAndCertifications.Name = v.Name;

                        objLicenceAndCertifications.Description = v.Description;

                        objLicenceAndCertifications.IssuingAuthority = v.IssuingAuthority;

                        DateTime validFrom;
                        if (DateTime.TryParse(v.ValidFrom, out validFrom))
                        {
                            objLicenceAndCertifications.ValidFrom = validFrom;
                        }

                        DateTime validTo;
                        if (DateTime.TryParse(v.ValidTo, out validTo))
                        {
                            objLicenceAndCertifications.ValidTo = validTo;
                        }

                        objLicenceAndCertificationslst.Add(objLicenceAndCertifications);
                    }
                }

                return objLicenceAndCertificationslst;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<PublicationHistory> GetPublicationHistory(RChilliHelper.SovrenParseResume pRChilliParseResume, Guid ContactId, Guid ProfileId)
        {
            try
            {
                List<PublicationHistory> objPublicationHistorylst = new List<PublicationHistory>();

                if (pRChilliParseResume.objPublicationHistory.objArticle != null)
                {
                    foreach (var v in pRChilliParseResume.objPublicationHistory.objArticle)
                    {
                        PublicationHistory objPublicationHistory = new PublicationHistory();

                        if (!ContactId.Equals(Guid.Empty))
                            objPublicationHistory.UserId = ContactId;

                        objPublicationHistory.ProfileId = ProfileId;

                        objPublicationHistory.Type = "Artical";

                        objPublicationHistory.Title = v.Title;

                        objPublicationHistory.Name = v.FormattedName;

                        objPublicationHistory.Role = v.Role;

                        DateTime pubDate;
                        if (DateTime.TryParse(v.PublicationDate, out pubDate))
                        {
                            objPublicationHistory.PublicationDate = pubDate;
                        }

                        if (v.JournalOrSerialName != null)
                            objPublicationHistory.Description = objPublicationHistory.Description + "Journal Or SerialName : " + v.JournalOrSerialName + "\r\n";
                        if (v.Volume != null)
                            objPublicationHistory.Description = objPublicationHistory.Description + "Volume : " + v.Volume + "\r\n";
                        if (v.Issue != null)
                            objPublicationHistory.Description = objPublicationHistory.Description + "Issue : " + v.Issue + "\r\n";
                        if (v.PageNumber != null)
                            objPublicationHistory.Description = objPublicationHistory.Description + "Page No. : " + v.PageNumber + "\r\n";
                        if (v.PublicationLanguage != null)
                            objPublicationHistory.Description = objPublicationHistory.Description + "Publication Language : " + v.PublicationLanguage + "\r\n";

                        objPublicationHistorylst.Add(objPublicationHistory);
                    }
                }
                if (pRChilliParseResume.objPublicationHistory.objBook != null)
                {
                    foreach (var v in pRChilliParseResume.objPublicationHistory.objBook)
                    {
                        PublicationHistory objPublicationHistory = new PublicationHistory();

                        if (!ContactId.Equals(Guid.Empty))
                            objPublicationHistory.UserId = ContactId;

                        objPublicationHistory.ProfileId = ProfileId;

                        objPublicationHistory.Type = "Book";

                        objPublicationHistory.Title = v.Title;

                        objPublicationHistory.Name = v.FormattedName;

                        objPublicationHistory.Role = v.Role;

                        DateTime pubDate;
                        if (DateTime.TryParse(v.PublicationDate, out pubDate))
                        {
                            objPublicationHistory.PublicationDate = pubDate;
                        }

                        if (v.Abstract != null)
                            objPublicationHistory.Description = objPublicationHistory.Description + "Abstract : " + v.Abstract + "\r\n";
                        if (v.Edition != null)
                            objPublicationHistory.Description = objPublicationHistory.Description + "Edition : " + v.Edition + "\r\n";
                        if (v.ISBN != null)
                            objPublicationHistory.Description = objPublicationHistory.Description + "ISBN : " + v.ISBN + "\r\n";
                        if (v.PublisherName != null)
                            objPublicationHistory.Description = objPublicationHistory.Description + "Publisher Name : " + v.PublisherName + "\r\n";
                        if (v.PublisherLocation != null)
                            objPublicationHistory.Description = objPublicationHistory.Description + "Publisher Location : " + v.PublisherLocation + "\r\n";
                        if (v.objCopyright != null)
                        {
                            for (int i = 0; i < v.objCopyright.Count; i++)
                            {
                                if (v.objCopyright[i].CopyrightText != null)
                                    objPublicationHistory.Description = objPublicationHistory.Description + "Copyright Text : " + v.objCopyright[i].CopyrightText + "\r\n";
                                if(v.objCopyright[i].objCopyrightDates[0].CopyrightDate != null)
                                    objPublicationHistory.Description = objPublicationHistory.Description + "Copyright Date : " + v.objCopyright[i].objCopyrightDates[0].CopyrightDate + "\r\n";
                            }
                        }

                        objPublicationHistorylst.Add(objPublicationHistory);
                    }
                }
                if (pRChilliParseResume.objPublicationHistory.objConferencePaper != null)
                {
                    foreach (var v in pRChilliParseResume.objPublicationHistory.objConferencePaper)
                    {
                        PublicationHistory objPublicationHistory = new PublicationHistory();

                        if (!ContactId.Equals(Guid.Empty))
                            objPublicationHistory.UserId = ContactId;

                        objPublicationHistory.ProfileId = ProfileId;

                        objPublicationHistory.Type = "Conference Paper";

                        objPublicationHistory.Title = v.Title;

                        objPublicationHistory.Name = v.FormattedName;

                        objPublicationHistory.Role = v.Role;

                        DateTime pubDate;
                        if (DateTime.TryParse(v.ConferenceDate, out pubDate))
                        {
                            objPublicationHistory.PublicationDate = pubDate;
                        }

                        if (v.ConferenceLocation != null)
                            objPublicationHistory.Description = objPublicationHistory.Description + "Conference Location : " + v.ConferenceLocation + "\r\n";
                       
                        objPublicationHistorylst.Add(objPublicationHistory);
                    }
                }
                if (pRChilliParseResume.objPublicationHistory.objOtherPublication != null)
                {
                    foreach (var v in pRChilliParseResume.objPublicationHistory.objOtherPublication)
                    {
                        PublicationHistory objPublicationHistory = new PublicationHistory();

                        if (!ContactId.Equals(Guid.Empty))
                            objPublicationHistory.UserId = ContactId;

                        objPublicationHistory.ProfileId = ProfileId;

                        objPublicationHistory.Type = v.OtherPublicationType;

                        objPublicationHistory.Title = v.Title;

                        objPublicationHistory.Name = v.FormattedName;

                        objPublicationHistory.Role = v.Role;

                        DateTime pubDate;
                        if (DateTime.TryParse(v.PublicationDate, out pubDate))
                        {
                            objPublicationHistory.PublicationDate = pubDate;
                        }

                        objPublicationHistory.Comments = v.Comments;

                        if (v.Link != null)
                            objPublicationHistory.Description = objPublicationHistory.Description + "Link : " + v.Link + "\r\n";
                        if (v.NumberOfPages != null)
                            objPublicationHistory.Description = objPublicationHistory.Description + "Number Of Pages : " + v.NumberOfPages + "\r\n";
                        if (v.PublisherLocation != null)
                            objPublicationHistory.Description = objPublicationHistory.Description + "Publisher Location : " + v.PublisherLocation + "\r\n";
                        
                        objPublicationHistorylst.Add(objPublicationHistory);
                    }
                }
                return objPublicationHistorylst;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}