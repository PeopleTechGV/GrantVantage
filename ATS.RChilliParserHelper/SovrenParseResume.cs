using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Xml;
using DynamicIOStream.Xml;
using System.Xml.XPath;
using System.Dynamic;
using Microsoft.CSharp.RuntimeBinder;
using ATS.RChilliParserHelper.ResumeService;
using System.Xml.Linq;

namespace ATS.RChilliParserHelper
{
    //public class SegregateQualification
    //{
    //    string university = "";
    //    string degree = "";
    //    string year = "";

    //    public string University
    //    {
    //        get;
    //        set;
    //    }
    //    public string Degree
    //    {
    //        get;
    //        set;
    //    }
    //    public string Year
    //    {
    //        get;
    //        set;
    //    }

    //}
    //public class Projects
    //{
    //    public string ProjectName
    //    {
    //        get;
    //        set;
    //    }
    //    public string UsedSkills
    //    {
    //        get;
    //        set;
    //    }
    //    public string TeamSize
    //    {
    //        get;
    //        set;
    //    }
    //}
    //public class SkillSet
    //{
    //    public string Skill
    //    {
    //        get;
    //        set;
    //    }
    //    public string ExperienceInMonth
    //    {
    //        get;
    //        set;
    //    }
    //}
    //public class Skills
    //{
    //    public string BehaviorSkills
    //    {
    //        get;
    //        set;
    //    }
    //    public string SoftSkills
    //    {
    //        get;
    //        set;
    //    }
    //    public List<SkillSet> OperationalSkills
    //    {
    //        get;
    //        set;
    //    }
    //}

    //public class SegregateExperience
    //{

    //    public string Employer
    //    {
    //        get;
    //        set;
    //    }
    //    public string JobProfile
    //    {
    //        get;
    //        set;
    //    }
    //    public string JobLocation
    //    {
    //        get;
    //        set;
    //    }
    //    public string JobPeriod
    //    {
    //        get;
    //        set;
    //    }
    //    public string StartDate
    //    {
    //        get;
    //        set;
    //    }
    //    public string EndDate
    //    {
    //        get;
    //        set;
    //    }
    //    public string JobDescription
    //    {
    //        get;
    //        set;
    //    }
    //    public List<Projects> project
    //    {
    //        get;
    //        set;
    //    }

    //}

    public class SovrenParseResume
    {
        string serviceUrl = "";
        bool error = false;
        string errorCode = "";
        string errorMessage = "";
        public string ServiceUrl
        {
            set { serviceUrl = value; }
        }
        public bool Error
        {
            get { return error; }
        }
        public string ErrorCode
        {
            get { return errorCode; }
        }
        public string ErrorMessage
        {
            get { return errorMessage; }
        }

        #region Properties
        #region Personal Info

        PersonalInfo objPersonalInfo = new PersonalInfo();
        public PersonalInfo objPersonalInformation { get { return objPersonalInfo; } }
        #endregion

        #region Contact Method

        List<ContactMethod> objContactMethodLst = new List<ContactMethod>();
        public List<ContactMethod> objContactMethod { get { return objContactMethodLst; } }
        #endregion

        #region Executive Summary

        ExecutiveSummary objExecutiveSummary = new ExecutiveSummary();
        public ExecutiveSummary objExecutivesummary { get { return objExecutiveSummary; } }
        #endregion

        #region Objective

        Objective objObjective = new Objective();
        public Objective objobjective { get { return objObjective; } }
        #endregion

        #region Langauge

        List<Language> objLanguageLst = new List<Language>();
        public List<Language> objLanguages { get { return objLanguageLst; } }
        #endregion

        #region EmploymentHistory

        List<EmploymentHistory> objEmploymentHistoryLst = new List<EmploymentHistory>();
        public List<EmploymentHistory> objEmploymentHistory { get { return objEmploymentHistoryLst; } }
        #endregion

        #region Education History

        List<SchoolOrInstitution> objSchoolOrInstitutionLst = new List<SchoolOrInstitution>();
        public List<SchoolOrInstitution> objSchoolOrInstitution { get { return objSchoolOrInstitutionLst; } }
        #endregion

        #region LicenseOrCertification

        List<LicenseOrCertification> objLicenseOrCertificationLst = new List<LicenseOrCertification>();
        public List<LicenseOrCertification> objLicenseOrCertification { get { return objLicenseOrCertificationLst; } }
        #endregion

        #region Association
        List<Association> objAssociationLst = new List<Association>();
        public List<Association> objAssociation { get { return objAssociationLst; } }
        #endregion

        #region Achievement

        List<Achievement> objAchievementLst = new List<Achievement>();
        public List<Achievement> objAchievement { get { return objAchievementLst; } }

        #endregion

        #region SpeakingEventsHistory

        List<SpeakingEventsHistory> objSpeakingEventsHistoryLst = new List<SpeakingEventsHistory>();
        public List<SpeakingEventsHistory> objSpeakingEventsHistory { get { return objSpeakingEventsHistoryLst; } }

        #endregion

        #region PublicationHistory

        PublicationHistory objPublicationhistory = new PublicationHistory();
        public PublicationHistory objPublicationHistory { get { return objPublicationhistory; } }

        #endregion

        #region Qualifications

        Qualifications objQualification = new Qualifications();
        public Qualifications objQualifications { get { return objQualification; } }

        #endregion

        #endregion

        public void ParseResume(string filePath, string userKey, string version, string subUserId, bool IsTest = false)
        {
            try
            {
                //IsTest = true;
                if (!IsTest)
                {
                    FileInfo file = new FileInfo(filePath);
                    byte[] fileBytes = converttoBase64(file);

                    ResumeServiceSoapClient client = new ResumeServiceSoapClient();
                    ParseResumeRequest request = new ParseResumeRequest
                    {
                        // Required parameters
                        AccountId = "28201059",
                        ServiceKey = "YFJMq/A/fdFST5zb4pGYWLhujFgK7k45Fad7pAB5",
                        FileBytes = fileBytes,

                        // Optional parameters
                        //Configuration = "", // Paste string from Parser Config String Builder.xls spreadsheet
                        //OutputHtml = true, // Convert to HTML
                        //OutputRtf = true, // Convert to RTF
                        //OutputWordXml = true, // Convert to WordXml
                        //RevisionDate = "2011-05-15", // Parse assuming a historical date for "current"
                    };

                    // Perform the parse. The first request will be slow due to WCF initializing
                    // the connection and SOAP/XML serialization, but subsequent calls will be fast.
                    ParseResumeResponse response = client.ParseResume(request);
                    ReadXml(response.Xml);
                }
                else
                {

                    filePath = Directory.GetParent(filePath).FullName;
                    //filePath = filePath + @"\Resume-Viral Shah - .Net Developer - 2+ Years Exp.doc_Sovren.xml";
                    filePath = filePath + @"\arpan.xml";
                    //filePath = filePath + @"\Resume-SteveCooley.pdf_Sovren.xml";
                    String xmlText = File.ReadAllText(filePath);
                    ReadXml(xmlText);
                }
            }
            catch (Exception ex)
            {
                errorCode = "5001";
                errorMessage = ex.Message;
            }


        }

        public byte[] converttoBase64(FileInfo fno)
        {

            try
            {
                Int64 numofbyte = fno.Length;
                FileStream fs = new FileStream(fno.FullName, FileMode.Open);
                BinaryReader br = new BinaryReader(fs);
                byte[] DataFile = br.ReadBytes(Convert.ToInt32(numofbyte));
                fs.Close();
                fs.Dispose();
                return DataFile;
            }
            catch (Exception ex)
            {
                error = true;
                errorCode = "5000";
                errorMessage = "Exception occured" + ex.Message;
            }
            byte[] error1 = new byte[1];
            error1[0] = (byte)' ';
            return error1;
        }

        //public string ReturnNodeValue(XmlNodeList objXMLNodeList, string nodeValue, bool isAttributeValue = false)
        //{
        //    try
        //    {
        //        string result = string.Empty;

        //        if (!isAttributeValue)
        //        {
        //            XmlNode objXMLnode = objXMLNodeList[0].ChildNodes[0];
        //            if (objXMLnode != null)
        //            {
        //                result = objXMLnode.Value;
        //            }
        //        }
        //        //else
        //        //{
        //        //    if (objXMLNodeList.Attributes.GetNamedItem(nodeValue) != null)
        //        //    {
        //        //        result = objXMLNodeList.Attributes.GetNamedItem(nodeValue).Value;
        //        //    }
        //        //}

        //        return result;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        void ReadXml(string XMLFile)
        {
            XmlDocument objXMLDoc = new XmlDocument();
            objXMLDoc.LoadXml(XMLFile);

            #region Personal Info
            //Start Personal Info
            if (objXMLDoc.GetElementsByTagName("FormattedName").Count > 0)
                objPersonalInfo.FormattedName = objXMLDoc.GetElementsByTagName("FormattedName")[0].ChildNodes[0].Value;
            if (objXMLDoc.GetElementsByTagName("GivenName").Count > 0)
                objPersonalInfo.GivenName = objXMLDoc.GetElementsByTagName("GivenName")[0].ChildNodes[0].Value;
            if (objXMLDoc.GetElementsByTagName("MiddleName").Count > 0)
                objPersonalInfo.MiddleName = objXMLDoc.GetElementsByTagName("MiddleName")[0].ChildNodes[0].Value;
            if (objXMLDoc.GetElementsByTagName("FamilyName").Count > 0)
                objPersonalInfo.FamilyName = objXMLDoc.GetElementsByTagName("FamilyName")[0].ChildNodes[0].Value;
            if (objXMLDoc.GetElementsByTagName("Affix").Count > 0)
                objPersonalInfo.Affix = objXMLDoc.GetElementsByTagName("Affix")[0].ChildNodes[0].Value;
            if (objXMLDoc.GetElementsByTagName("Date").Count > 0)
            {
                objPersonalInfo.DateType = objXMLDoc.GetElementsByTagName("Date")[0].Attributes["type"].Value;
                objPersonalInfo.AnyDate = objXMLDoc.GetElementsByTagName("Date")[0].ChildNodes[0].Value;
            }

            
            //End Personal Info
            #endregion

            #region Contact Info
            //Start Contact Info

            if (objXMLDoc.GetElementsByTagName("ContactMethod").Count > 0)
            {
                for (int i = 0; i < objXMLDoc.GetElementsByTagName("ContactMethod").Count; i++)
                {
                    ContactMethod objContactMethod = new ContactMethod();
                    for (int v = 0; v < objXMLDoc.GetElementsByTagName("ContactMethod")[i].ChildNodes.Count; v++)
                    {
                        if (objXMLDoc.GetElementsByTagName("ContactMethod")[i].ChildNodes[v].Name == "WhenAvailable")
                            objContactMethod.WhenAvailable = objXMLDoc.GetElementsByTagName("ContactMethod")[i].ChildNodes[v].InnerText;
                        else if (objXMLDoc.GetElementsByTagName("ContactMethod")[i].ChildNodes[v].Name == "Use")
                            objContactMethod.Use = objXMLDoc.GetElementsByTagName("ContactMethod")[i].ChildNodes[v].InnerText;
                        else if (objXMLDoc.GetElementsByTagName("ContactMethod")[i].ChildNodes[v].Name == "Location")
                            objContactMethod.Location = objXMLDoc.GetElementsByTagName("ContactMethod")[i].ChildNodes[v].InnerText;
                        else if (objXMLDoc.GetElementsByTagName("ContactMethod")[i].ChildNodes[v].Name == "Mobile")
                        {
                            objContactMethod.objMobile = new Mobile();
                            objContactMethod.objMobile.MO_FormattedNumber = objXMLDoc.GetElementsByTagName("ContactMethod")[i].ChildNodes[v].InnerText;
                        }
                        else if (objXMLDoc.GetElementsByTagName("ContactMethod")[i].ChildNodes[v].Name == "Telephone")
                        {
                            objContactMethod.objTelephone = new Telephone();
                            objContactMethod.objTelephone.Tel_FormattedNumber = objXMLDoc.GetElementsByTagName("ContactMethod")[i].ChildNodes[v].InnerText;
                        }
                        else if (objXMLDoc.GetElementsByTagName("ContactMethod")[i].ChildNodes[v].Name == "InternetEmailAddress")
                        {
                            objContactMethod.objEmail = new Email();
                            objContactMethod.objEmail.InternetEmailAddress = objXMLDoc.GetElementsByTagName("ContactMethod")[i].ChildNodes[v].InnerText;
                        }
                        else if (objXMLDoc.GetElementsByTagName("ContactMethod")[i].ChildNodes[v].Name == "InternetWebAddress")
                        {
                            objContactMethod.objWebAdd = new WebAdd();
                            objContactMethod.objWebAdd.InternetWebAddress = objXMLDoc.GetElementsByTagName("ContactMethod")[i].ChildNodes[v].InnerText;
                        }
                        else if (objXMLDoc.GetElementsByTagName("ContactMethod")[i].ChildNodes[v].Name == "PostalAddress")
                        {
                            objContactMethod.objPostalAddress = new PostalAddress();
                            bool IsAddTypeRemain = true;
                            if (objXMLDoc.GetElementsByTagName("ContactMethod")[i].ChildNodes[v].Attributes["type"] != null && IsAddTypeRemain == true)
                            {
                                objContactMethod.objPostalAddress.AddressType = objXMLDoc.GetElementsByTagName("ContactMethod")[i].ChildNodes[v].Attributes["type"].Value;
                                IsAddTypeRemain = false;
                            }
                            for (var k = 0; k < objXMLDoc.GetElementsByTagName("ContactMethod")[i].ChildNodes[v].ChildNodes.Count; k++)
                            {
                                if (objXMLDoc.GetElementsByTagName("ContactMethod")[i].ChildNodes[v].ChildNodes[k].Name == "CountryCode")
                                    objContactMethod.objPostalAddress.CountryCode = objXMLDoc.GetElementsByTagName("ContactMethod")[i].ChildNodes[v].ChildNodes[k].InnerText;
                                else if (objXMLDoc.GetElementsByTagName("ContactMethod")[i].ChildNodes[v].ChildNodes[k].Name == "PostalCode")
                                    objContactMethod.objPostalAddress.PostalCode = objXMLDoc.GetElementsByTagName("ContactMethod")[i].ChildNodes[v].ChildNodes[k].InnerText;
                                else if (objXMLDoc.GetElementsByTagName("ContactMethod")[i].ChildNodes[v].ChildNodes[k].Name == "Region")
                                    objContactMethod.objPostalAddress.Region = objXMLDoc.GetElementsByTagName("ContactMethod")[i].ChildNodes[v].ChildNodes[k].InnerText;
                                else if (objXMLDoc.GetElementsByTagName("ContactMethod")[i].ChildNodes[v].ChildNodes[k].Name == "Municipality")
                                    objContactMethod.objPostalAddress.Municipality = objXMLDoc.GetElementsByTagName("ContactMethod")[i].ChildNodes[v].ChildNodes[k].InnerText;
                                else if (objXMLDoc.GetElementsByTagName("ContactMethod")[i].ChildNodes[v].ChildNodes[k].Name == "DeliveryAddress")
                                {
                                    List<DeliveryAddress> objDeliveryAddlst = new List<DeliveryAddress>();
                                    List<AddressLine> objAddLinelst = new List<AddressLine>();
                                    DeliveryAddress objDelAdd = new DeliveryAddress();
                                    for (var j = 0; j < objXMLDoc.GetElementsByTagName("ContactMethod")[i].ChildNodes[v].ChildNodes[k].ChildNodes.Count; j++)
                                    {
                                        if (objXMLDoc.GetElementsByTagName("ContactMethod")[i].ChildNodes[v].ChildNodes[k].ChildNodes[j].Name == "BuildingNumber")
                                            objDelAdd.BuildingNumber = objXMLDoc.GetElementsByTagName("ContactMethod")[i].ChildNodes[v].ChildNodes[k].ChildNodes[j].InnerText;
                                        else if (objXMLDoc.GetElementsByTagName("ContactMethod")[i].ChildNodes[v].ChildNodes[k].ChildNodes[j].Name == "PostOfficeBox")
                                            objDelAdd.PostOfficeBox = objXMLDoc.GetElementsByTagName("ContactMethod")[i].ChildNodes[v].ChildNodes[k].ChildNodes[j].InnerText;
                                        else if (objXMLDoc.GetElementsByTagName("ContactMethod")[i].ChildNodes[v].ChildNodes[k].ChildNodes[j].Name == "StreetName")
                                            objDelAdd.StreetName = objXMLDoc.GetElementsByTagName("ContactMethod")[i].ChildNodes[v].ChildNodes[k].ChildNodes[j].InnerText;
                                        else if (objXMLDoc.GetElementsByTagName("ContactMethod")[i].ChildNodes[v].ChildNodes[k].ChildNodes[j].Name == "Unit")
                                            objDelAdd.Unit = objXMLDoc.GetElementsByTagName("ContactMethod")[i].ChildNodes[v].ChildNodes[k].ChildNodes[j].InnerText;
                                        else if (objXMLDoc.GetElementsByTagName("ContactMethod")[i].ChildNodes[v].ChildNodes[k].ChildNodes[j].Name == "AddressLine")
                                        {

                                            AddressLine objAddLine = new AddressLine();

                                            objAddLine.AddLine = objXMLDoc.GetElementsByTagName("ContactMethod")[i].ChildNodes[v].ChildNodes[k].ChildNodes[j].InnerText;

                                            objAddLinelst.Add(objAddLine);

                                        }
                                    }
                                    objDelAdd.objAddLine = objAddLinelst;
                                    objDeliveryAddlst.Add(objDelAdd);
                                    objContactMethod.objPostalAddress.DeliveryAddresslst = objDeliveryAddlst;
                                }
                            }
                        }
                    }
                    objContactMethodLst.Add(objContactMethod);
                }
            }

            //End Contact Info
            #endregion

            #region Executive Summary
            //Start Executive Summary

            if (objXMLDoc.GetElementsByTagName("ExecutiveSummary").Count > 0)
                objExecutiveSummary.ExecutiveSummaryDetail = objXMLDoc.GetElementsByTagName("ExecutiveSummary")[0].ChildNodes[0].Value;

            //End Executive Summary
            #endregion

            #region Objective
            //Start Objective

            if (objXMLDoc.GetElementsByTagName("Objective").Count > 0)
                objObjective.objective = objXMLDoc.GetElementsByTagName("Objective")[0].ChildNodes[0].Value;

            // End Objective
            #endregion

            #region Language
            //Language Start
            if (objXMLDoc.GetElementsByTagName("Languages").Count > 0)
            {
                for (int i = 0; i < objXMLDoc.GetElementsByTagName("Language").Count; i++)
                {
                    Language objLanguage = new Language();
                    for (int v = 0; v < objXMLDoc.GetElementsByTagName("Language")[i].ChildNodes.Count; v++)
                    {
                        if (objXMLDoc.GetElementsByTagName("Language")[i].ChildNodes[v].Name == "LanguageCode")
                            objLanguage.LanguageCode = objXMLDoc.GetElementsByTagName("Language")[i].ChildNodes[v].InnerText;
                        else if (objXMLDoc.GetElementsByTagName("Language")[i].ChildNodes[v].Name == "Read")
                            objLanguage.Read = objXMLDoc.GetElementsByTagName("Language")[i].ChildNodes[v].InnerText;
                        else if (objXMLDoc.GetElementsByTagName("Language")[i].ChildNodes[v].Name == "Speak")
                            objLanguage.Speak = objXMLDoc.GetElementsByTagName("Language")[i].ChildNodes[v].InnerText;
                        else if (objXMLDoc.GetElementsByTagName("Language")[i].ChildNodes[v].Name == "Write")
                            objLanguage.Write = objXMLDoc.GetElementsByTagName("Language")[i].ChildNodes[v].InnerText;
                        else if (objXMLDoc.GetElementsByTagName("Language")[i].ChildNodes[v].Name == "Comments")
                            objLanguage.Comments = objXMLDoc.GetElementsByTagName("Language")[i].ChildNodes[v].InnerText;

                    }
                    objLanguageLst.Add(objLanguage);
                }
            }

            //End Language

            #endregion

            #region EmploymentHistory

            if (objXMLDoc.GetElementsByTagName("EmploymentHistory").Count > 0)
            {
                for (int v = 0; v < objXMLDoc.GetElementsByTagName("EmploymentHistory").Count; v++)
                {

                    for (int k = 0; k < objXMLDoc.GetElementsByTagName("EmploymentHistory")[v].ChildNodes.Count; k++)
                    {
                        EmploymentHistory objEmpHistory = new EmploymentHistory();
                        bool IspositionTypeRemain = true;
                        bool IscurrentEmployerRemain = true;

                        for (int a = 0; a < objXMLDoc.GetElementsByTagName("EmploymentHistory")[v].ChildNodes[k].ChildNodes.Count; a++)
                        {
                            if (objXMLDoc.GetElementsByTagName("EmploymentHistory")[v].ChildNodes[k].ChildNodes[a].Name == "EmployerOrgName")
                                objEmpHistory.EmployerOrgName = objXMLDoc.GetElementsByTagName("EmploymentHistory")[v].ChildNodes[k].ChildNodes[a].InnerText;
                            else if (objXMLDoc.GetElementsByTagName("EmploymentHistory")[v].ChildNodes[k].ChildNodes[a].Name == "PositionHistory")
                            {
                                if (objXMLDoc.GetElementsByTagName("EmploymentHistory")[v].ChildNodes[k].ChildNodes[a].Attributes["positionType"] != null && IspositionTypeRemain == true)
                                {
                                    objEmpHistory.PositionType = objXMLDoc.GetElementsByTagName("EmploymentHistory")[v].ChildNodes[k].ChildNodes[a].Attributes["positionType"].Value;
                                    IspositionTypeRemain = false;
                                }
                                if (objXMLDoc.GetElementsByTagName("EmploymentHistory")[v].ChildNodes[k].ChildNodes[a].Attributes["currentEmployer"] != null && IscurrentEmployerRemain == true)
                                {
                                    objEmpHistory.CurrentEmployer = objXMLDoc.GetElementsByTagName("EmploymentHistory")[v].ChildNodes[k].ChildNodes[a].Attributes["currentEmployer"].Value;
                                    IscurrentEmployerRemain = false;
                                }
                                for (int b = 0; b < objXMLDoc.GetElementsByTagName("EmploymentHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes.Count; b++)
                                {
                                    if (objXMLDoc.GetElementsByTagName("EmploymentHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].Name == "Title")
                                        objEmpHistory.Title = objXMLDoc.GetElementsByTagName("EmploymentHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].InnerText;
                                    else if (objXMLDoc.GetElementsByTagName("EmploymentHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].Name == "OrgName")
                                        objEmpHistory.OrganizationName = objXMLDoc.GetElementsByTagName("EmploymentHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].ChildNodes[0].InnerText;
                                    else if (objXMLDoc.GetElementsByTagName("EmploymentHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].Name == "Description")
                                        objEmpHistory.Description = objXMLDoc.GetElementsByTagName("EmploymentHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].InnerText;
                                    else if (objXMLDoc.GetElementsByTagName("EmploymentHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].Name == "StartDate")
                                        objEmpHistory.StartDate = objXMLDoc.GetElementsByTagName("EmploymentHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].ChildNodes[0].InnerText;
                                    else if (objXMLDoc.GetElementsByTagName("EmploymentHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].Name == "EndDate")
                                        objEmpHistory.EndDate = objXMLDoc.GetElementsByTagName("EmploymentHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].ChildNodes[0].InnerText;
                                    else if (objXMLDoc.GetElementsByTagName("EmploymentHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].Name == "OrgInfo")
                                    {
                                        List<Location> objLocationlst = new List<Location>();
                                        for (int n = 0; n < objXMLDoc.GetElementsByTagName("EmploymentHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].ChildNodes.Count; n++)
                                        {
                                            Location objLoc = new Location();
                                            if (objXMLDoc.GetElementsByTagName("EmploymentHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].ChildNodes[n].Attributes["type"] != null)
                                                objLoc.AddressType = objXMLDoc.GetElementsByTagName("EmploymentHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].ChildNodes[n].Attributes["type"].Value;
                                            for (int z = 0; z < objXMLDoc.GetElementsByTagName("EmploymentHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].ChildNodes[n].ChildNodes.Count; z++)
                                            {
                                                if (objXMLDoc.GetElementsByTagName("EmploymentHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].ChildNodes[n].ChildNodes[z].Name == "CountryCode")
                                                    objLoc.CountryCode = objXMLDoc.GetElementsByTagName("EmploymentHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].ChildNodes[n].ChildNodes[z].InnerText;
                                                else if (objXMLDoc.GetElementsByTagName("EmploymentHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].ChildNodes[n].ChildNodes[z].Name == "Region")
                                                    objLoc.Region = objXMLDoc.GetElementsByTagName("EmploymentHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].ChildNodes[n].ChildNodes[z].InnerText;
                                                else if (objXMLDoc.GetElementsByTagName("EmploymentHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].ChildNodes[n].ChildNodes[z].Name == "Municipality")
                                                    objLoc.Municipality = objXMLDoc.GetElementsByTagName("EmploymentHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].ChildNodes[n].ChildNodes[z].InnerText;
                                                else if (objXMLDoc.GetElementsByTagName("EmploymentHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].ChildNodes[n].ChildNodes[z].Name == "PostalCode")
                                                    objLoc.PostalCode = objXMLDoc.GetElementsByTagName("EmploymentHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].ChildNodes[n].ChildNodes[z].InnerText;
                                            }
                                            objLocationlst.Add(objLoc);
                                        }
                                        objEmpHistory.objLocLst = objLocationlst;
                                    }
                                }
                            }
                        }
                        objEmploymentHistoryLst.Add(objEmpHistory);
                    }
                }
            }

            #endregion

            #region EducationHistory

            if (objXMLDoc.GetElementsByTagName("EducationHistory").Count > 0)
            {
                for (int v = 0; v < objXMLDoc.GetElementsByTagName("EducationHistory").Count; v++)
                {

                    for (int k = 0; k < objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes.Count; k++)
                    {
                        SchoolOrInstitution objSchoolOrInstitution = new SchoolOrInstitution();
                        bool IsschoolTypeRemain = true;
                        bool IsPostalAddressTypeRemain = true;
                        if (objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].Attributes["schoolType"] != null && IsschoolTypeRemain == true)
                        {
                            objSchoolOrInstitution.SchoolType = objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].Attributes["schoolType"].Value;
                            IsschoolTypeRemain = false;
                        }
                        for (int a = 0; a < objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes.Count; a++)
                        {
                            List<Degree> objDegreelst = new List<Degree>();
                            if (objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].Name == "School")
                                objSchoolOrInstitution.SchoolName = objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[0].InnerText;
                            else if (objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].Name == "PostalAddress")
                            {
                                if (objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].Attributes["type"] != null && IsPostalAddressTypeRemain == true)
                                {
                                    objSchoolOrInstitution.AddressType = objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].Attributes["type"].Value;
                                    IsPostalAddressTypeRemain = false;
                                }
                                for (int n = 0; n < objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes.Count; n++)
                                {
                                    if (objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[n].Name == "CountryCode")
                                        objSchoolOrInstitution.CountryCode = objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[n].InnerText;
                                    else if (objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[n].Name == "Region")
                                        objSchoolOrInstitution.Region = objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[n].InnerText;
                                    else if (objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[n].Name == "Municipality")
                                        objSchoolOrInstitution.Municipality = objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[n].InnerText;
                                    else if (objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[n].Name == "PostalCode")
                                        objSchoolOrInstitution.PostalCode = objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[n].InnerText;
                                }
                            }
                            else if (objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].Name == "Degree")
                            {
                                bool IsDegreeTypeRemain = true;

                                Degree objDegree = new Degree();
                                if (objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].Attributes["degreeType"] != null && IsDegreeTypeRemain == true)
                                {
                                    objDegree.DegreeType = objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].Attributes["degreeType"].Value;
                                    IsDegreeTypeRemain = false;
                                }

                                for (int b = 0; b < objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes.Count; b++)
                                {
                                    if (objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].Name == "DegreeName")
                                        objDegree.DegreeName = objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].InnerText;
                                    else if (objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].Name == "DegreeMajor")
                                        objDegree.DegreeMajor = objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].ChildNodes[0].InnerText;
                                    else if (objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].Name == "DegreeMinor")
                                        objDegree.DegreeMinor = objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].ChildNodes[0].InnerText;
                                    else if (objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].Name == "DegreeDate")
                                        objDegree.DegreeDate = objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].ChildNodes[0].InnerText;
                                    else if (objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].Name == "DatesOfAttendance")
                                    {
                                        for (int s = 0; s < objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].ChildNodes.Count; s++)
                                        {
                                            if (objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].ChildNodes[s].Name == "StartDate")
                                                objDegree.DatesOfAttendance_StartDate = objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].ChildNodes[s].ChildNodes[0].InnerText;
                                            else if (objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].ChildNodes[s].Name == "EndDate")
                                                objDegree.DatesOfAttendance_EndDate = objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].ChildNodes[s].ChildNodes[0].InnerText;
                                        }
                                    }
                                    else if (objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].Name == "DegreeMeasure")
                                    {
                                        for (int s = 0; s < objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].ChildNodes[0].ChildNodes.Count; s++)
                                        {
                                            if (objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].ChildNodes[0].ChildNodes[s].Name == "MeasureSystem")
                                                objDegree.MeasureSystem = objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].ChildNodes[0].ChildNodes[s].InnerText;
                                            else if (objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].ChildNodes[0].ChildNodes[s].Name == "MeasureValue")
                                                objDegree.MeasureValue = objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].ChildNodes[0].ChildNodes[s].ChildNodes[0].InnerText;
                                            else if (objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].ChildNodes[0].ChildNodes[s].Name == "LowestPossibleValue")
                                                objDegree.LowestPossibleValue = objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].ChildNodes[0].ChildNodes[s].ChildNodes[0].InnerText;
                                            else if (objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].ChildNodes[0].ChildNodes[s].Name == "HighestPossibleValue")
                                                objDegree.HighestPossibleValue = objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].ChildNodes[0].ChildNodes[s].ChildNodes[0].InnerText;
                                        }
                                    }
                                    else if (objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].Name == "Comments")
                                        objDegree.Comments = objXMLDoc.GetElementsByTagName("EducationHistory")[v].ChildNodes[k].ChildNodes[a].ChildNodes[b].InnerText;
                                }
                                objDegreelst.Add(objDegree);
                                objSchoolOrInstitution.objDegreeLst = objDegreelst;
                            }
                        }
                        objSchoolOrInstitutionLst.Add(objSchoolOrInstitution);
                    }
                }
            }

            #endregion

            #region LicenseOrCertification
            if (objXMLDoc.GetElementsByTagName("LicensesAndCertifications").Count > 0)
            {
                for (int i = 0; i < objXMLDoc.GetElementsByTagName("LicenseOrCertification").Count; i++)
                {
                    LicenseOrCertification objLicenseOrCertification = new LicenseOrCertification();
                    for (int v = 0; v < objXMLDoc.GetElementsByTagName("LicenseOrCertification")[i].ChildNodes.Count; v++)
                    {
                        if (objXMLDoc.GetElementsByTagName("LicenseOrCertification")[i].ChildNodes[v].Name == "Name")
                            objLicenseOrCertification.Name = objXMLDoc.GetElementsByTagName("LicenseOrCertification")[i].ChildNodes[v].InnerText;
                        else if (objXMLDoc.GetElementsByTagName("LicenseOrCertification")[i].ChildNodes[v].Name == "IssuingAuthority")
                            objLicenseOrCertification.IssuingAuthority = objXMLDoc.GetElementsByTagName("LicenseOrCertification")[i].ChildNodes[v].InnerText;
                        else if (objXMLDoc.GetElementsByTagName("LicenseOrCertification")[i].ChildNodes[v].Name == "Description")
                            objLicenseOrCertification.Description = objXMLDoc.GetElementsByTagName("LicenseOrCertification")[i].ChildNodes[v].InnerText;
                        else if (objXMLDoc.GetElementsByTagName("LicenseOrCertification")[i].ChildNodes[v].Name == "EffectiveDate")
                        {
                            for (int b = 0; b < objXMLDoc.GetElementsByTagName("LicenseOrCertification")[i].ChildNodes[v].ChildNodes.Count; b++)
                            {
                                if (objXMLDoc.GetElementsByTagName("LicenseOrCertification")[i].ChildNodes[v].ChildNodes[b].Name == "ValidFrom")
                                    objLicenseOrCertification.ValidFrom = objXMLDoc.GetElementsByTagName("LicenseOrCertification")[i].ChildNodes[v].ChildNodes[b].ChildNodes[0].InnerText;
                                else if (objXMLDoc.GetElementsByTagName("LicenseOrCertification")[i].ChildNodes[v].ChildNodes[b].Name == "ValidTo")
                                    objLicenseOrCertification.ValidTo = objXMLDoc.GetElementsByTagName("LicenseOrCertification")[i].ChildNodes[v].ChildNodes[b].ChildNodes[0].InnerText;
                                else if (objXMLDoc.GetElementsByTagName("LicenseOrCertification")[i].ChildNodes[v].ChildNodes[b].Name == "FirstIssuedDate")
                                    objLicenseOrCertification.FirstIssuedDate = objXMLDoc.GetElementsByTagName("LicenseOrCertification")[i].ChildNodes[v].ChildNodes[b].ChildNodes[0].InnerText;
                            }
                        }
                    }
                    objLicenseOrCertificationLst.Add(objLicenseOrCertification);
                }
            }
            #endregion

            #region Associations

            if (objXMLDoc.GetElementsByTagName("Associations").Count > 0)
            {
                for (int i = 0; i < objXMLDoc.GetElementsByTagName("Association").Count; i++)
                {
                    Association objAssociation = new Association();
                    List<Role> objRolelst = new List<Role>();
                    if (objXMLDoc.GetElementsByTagName("Association")[i].Attributes["type"] != null)
                    {
                        objAssociation.AssociationType = objXMLDoc.GetElementsByTagName("EducationHistory")[i].Attributes["type"].Value;
                    }
                    for (int v = 0; v < objXMLDoc.GetElementsByTagName("Association")[i].ChildNodes.Count; v++)
                    {
                        if (objXMLDoc.GetElementsByTagName("Association")[i].ChildNodes[v].Name == "Name")
                            objAssociation.Name = objXMLDoc.GetElementsByTagName("Association")[i].ChildNodes[v].InnerText;
                        else if (objXMLDoc.GetElementsByTagName("Association")[i].ChildNodes[v].Name == "Link")
                            objAssociation.Link = objXMLDoc.GetElementsByTagName("Association")[i].ChildNodes[v].InnerText;
                        else if (objXMLDoc.GetElementsByTagName("Association")[i].ChildNodes[v].Name == "StartDate")
                            objAssociation.StartDate = objXMLDoc.GetElementsByTagName("Association")[i].ChildNodes[v].ChildNodes[0].InnerText;
                        else if (objXMLDoc.GetElementsByTagName("Association")[i].ChildNodes[v].Name == "EndDate")
                            objAssociation.EndDate = objXMLDoc.GetElementsByTagName("Association")[i].ChildNodes[v].ChildNodes[0].InnerText;
                        else if (objXMLDoc.GetElementsByTagName("Association")[i].ChildNodes[v].Name == "Role")
                        {
                            Role objRole = new Role();
                            for (int b = 0; b < objXMLDoc.GetElementsByTagName("Association")[i].ChildNodes[v].ChildNodes.Count; b++)
                            {
                                if (objXMLDoc.GetElementsByTagName("Association")[i].ChildNodes[v].ChildNodes[b].Name == "Name")
                                    objRole.RoleName = objXMLDoc.GetElementsByTagName("Association")[i].ChildNodes[v].ChildNodes[b].InnerText;
                                else if (objXMLDoc.GetElementsByTagName("Association")[i].ChildNodes[v].ChildNodes[b].Name == "Deliverable")
                                    objRole.Deliverable = objXMLDoc.GetElementsByTagName("Association")[i].ChildNodes[v].ChildNodes[b].InnerText;
                            }
                            objRolelst.Add(objRole);
                        }
                    }
                    objAssociation.objRoleLst = objRolelst;
                    objAssociationLst.Add(objAssociation);
                }
            }

            #endregion

            #region Achievements

            if (objXMLDoc.GetElementsByTagName("Achievements").Count > 0)
            {
                for (int i = 0; i < objXMLDoc.GetElementsByTagName("Achievement").Count; i++)
                {
                    Achievement objAchievement = new Achievement();
                    for (int v = 0; v < objXMLDoc.GetElementsByTagName("Achievement")[i].ChildNodes.Count; v++)
                    {
                        if (objXMLDoc.GetElementsByTagName("Achievement")[i].ChildNodes[v].Name == "Date")
                            objAchievement.StringDate = objXMLDoc.GetElementsByTagName("Achievement")[i].ChildNodes[v].ChildNodes[0].InnerText;
                        else if (objXMLDoc.GetElementsByTagName("Achievement")[i].ChildNodes[v].Name == "Description")
                            objAchievement.Description = objXMLDoc.GetElementsByTagName("Achievement")[i].ChildNodes[v].InnerText;
                        else if (objXMLDoc.GetElementsByTagName("Achievement")[i].ChildNodes[v].Name == "IssuingAuthority")
                            objAchievement.IssuingAuthority = objXMLDoc.GetElementsByTagName("Achievement")[i].ChildNodes[v].InnerText;
                    }
                    objAchievementLst.Add(objAchievement);
                }
            }

            #endregion

            #region SpeakingEventsHistory

            if (objXMLDoc.GetElementsByTagName("SpeakingEventsHistory").Count > 0)
            {
                for (int i = 0; i < objXMLDoc.GetElementsByTagName("SpeakingEvent").Count; i++)
                {
                    SpeakingEventsHistory objSpeakingEventsHistory = new SpeakingEventsHistory();

                    if (objXMLDoc.GetElementsByTagName("SpeakingEvent")[i].Attributes["type"] != null)
                    {
                        objSpeakingEventsHistory.SpeakingEventtype = objXMLDoc.GetElementsByTagName("SpeakingEvent")[i].Attributes["type"].Value;
                    }

                    for (int v = 0; v < objXMLDoc.GetElementsByTagName("SpeakingEvent")[i].ChildNodes.Count; v++)
                    {
                        if (objXMLDoc.GetElementsByTagName("SpeakingEvent")[i].ChildNodes[v].Name == "StartDate")
                            objSpeakingEventsHistory.StartDate = objXMLDoc.GetElementsByTagName("SpeakingEvent")[i].ChildNodes[v].ChildNodes[0].InnerText;
                        else if (objXMLDoc.GetElementsByTagName("SpeakingEvent")[i].ChildNodes[v].Name == "EventName")
                            objSpeakingEventsHistory.EventName = objXMLDoc.GetElementsByTagName("SpeakingEvent")[i].ChildNodes[v].InnerText;
                        else if (objXMLDoc.GetElementsByTagName("SpeakingEvent")[i].ChildNodes[v].Name == "EventType")
                            objSpeakingEventsHistory.EventType = objXMLDoc.GetElementsByTagName("SpeakingEvent")[i].ChildNodes[v].InnerText;
                        else if (objXMLDoc.GetElementsByTagName("SpeakingEvent")[i].ChildNodes[v].Name == "Title")
                            objSpeakingEventsHistory.Title = objXMLDoc.GetElementsByTagName("SpeakingEvent")[i].ChildNodes[v].InnerText;
                        else if (objXMLDoc.GetElementsByTagName("SpeakingEvent")[i].ChildNodes[v].Name == "Location")
                            objSpeakingEventsHistory.Location = objXMLDoc.GetElementsByTagName("SpeakingEvent")[i].ChildNodes[v].InnerText;
                        else if (objXMLDoc.GetElementsByTagName("SpeakingEvent")[i].ChildNodes[v].Name == "Link")
                            objSpeakingEventsHistory.Link = objXMLDoc.GetElementsByTagName("SpeakingEvent")[i].ChildNodes[v].InnerText;

                    }
                    objSpeakingEventsHistoryLst.Add(objSpeakingEventsHistory);
                }
            }

            #endregion

            #region Publication History

            if (objXMLDoc.GetElementsByTagName("PublicationHistory").Count > 0)
            {
                List<Article> objArticallst = new List<Article>();
                List<Book> objBooklst = new List<Book>();
                List<ConferencePaper> objConferencePaperlst = new List<ConferencePaper>();
                List<OtherPublication> objOtherPublicationlst = new List<OtherPublication>();

                for (int i = 0; i < objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes.Count; i++)
                {
                    bool IsRoleRemain = true;
                    if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].Name == "Article")
                    {
                        Article objArtical = new Article();
                        for (int v = 0; v < objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes.Count; v++)
                        {
                            if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].Name == "Title")
                                objArtical.Title = objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].InnerText;
                            else if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].Name == "Name")
                            {

                                if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].Attributes["role"] != null && IsRoleRemain == true)
                                {
                                    objArtical.Role = objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].Attributes["role"].Value;
                                    IsRoleRemain = false;
                                }
                                if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].ChildNodes[0].Name == "FormattedName")
                                    objArtical.FormattedName = objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].ChildNodes[0].InnerText;
                            }
                            else if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].Name == "PublicationDate")
                                objArtical.PublicationDate = objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].ChildNodes[0].InnerText;
                            else if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].Name == "JournalOrSerialName")
                                objArtical.JournalOrSerialName = objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].InnerText;
                            else if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].Name == "Volume")
                                objArtical.Volume = objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].InnerText;
                            else if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].Name == "Issue")
                                objArtical.Issue = objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].InnerText;
                            else if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].Name == "PageNumber")
                                objArtical.PageNumber = objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].InnerText;
                            else if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].Name == "PublicationLanguage")
                                objArtical.PublicationLanguage = objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].InnerText;
                        }
                        objArticallst.Add(objArtical);
                    }
                    else if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].Name == "Book")
                    {
                        Book objBook = new Book();
                        List<Copyright> objCopyrightlst = new List<Copyright>();
                        for (int v = 0; v < objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes.Count; v++)
                        {
                            if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].Name == "Title")
                                objBook.Title = objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].InnerText;
                            else if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].Name == "Name")
                            {
                                if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].Attributes["role"] != null && IsRoleRemain == true)
                                {
                                    objBook.Role = objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].Attributes["role"].Value;
                                    IsRoleRemain = false;
                                }
                                if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].ChildNodes[0].Name == "FormattedName")
                                    objBook.FormattedName = objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].ChildNodes[0].InnerText;
                            }
                            else if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].Name == "PublicationDate")
                                objBook.PublicationDate = objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].ChildNodes[0].InnerText;
                            else if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].Name == "Abstract")
                                objBook.Abstract = objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].InnerText;
                            else if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].Name == "Edition")
                                objBook.Edition = objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].InnerText;
                            else if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].Name == "ISBN")
                                objBook.ISBN = objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].InnerText;
                            else if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].Name == "PublisherName")
                                objBook.PublisherName = objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].InnerText;
                            else if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].Name == "PublisherLocation")
                                objBook.PublisherLocation = objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].InnerText;
                            else if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].Name == "Copyright")
                            {
                                Copyright objCopyright = new Copyright();
                                for (int m = 0; m < objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].ChildNodes.Count; m++)
                                {
                                    if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].ChildNodes[m].Name == "CopyrightText")
                                        objCopyright.CopyrightText = objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].ChildNodes[m].InnerText;
                                    else if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].ChildNodes[m].Name == "CopyrightDates")
                                    {
                                        List<CopyrightDates> objCopyrightDateslst = new List<CopyrightDates>();
                                        for (int n = 0; n < objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].ChildNodes[m].ChildNodes.Count; n++)
                                        {
                                            CopyrightDates objCopyrightDates = new CopyrightDates();

                                            objCopyrightDates.CopyrightDate = objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].ChildNodes[m].ChildNodes[n].InnerText;

                                            objCopyrightDateslst.Add(objCopyrightDates);
                                        }
                                        objCopyright.objCopyrightDates = objCopyrightDateslst;
                                    }
                                }
                                objCopyrightlst.Add(objCopyright);
                            }
                        }
                        objBook.objCopyright = objCopyrightlst;
                        objBooklst.Add(objBook);
                    }
                    else if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].Name == "ConferencePaper")
                    {
                        ConferencePaper objConferencePaper = new ConferencePaper();
                        for (int v = 0; v < objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes.Count; v++)
                        {
                            if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].Name == "Title")
                                objConferencePaper.Title = objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].InnerText;
                            else if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].Name == "Name")
                            {
                                if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].Attributes["role"] != null && IsRoleRemain == true)
                                {
                                    objConferencePaper.Role = objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].Attributes["role"].Value;
                                    IsRoleRemain = false;
                                }
                                if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].ChildNodes[0].Name == "FormattedName")
                                    objConferencePaper.FormattedName = objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].ChildNodes[0].InnerText;
                            }
                            else if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].Name == "PublicationDate")
                                objConferencePaper.ConferenceDate = objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].ChildNodes[0].InnerText;
                            else if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].Name == "EventName")
                                objConferencePaper.EventName = objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].InnerText;
                            else if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].Name == "ConferenceLocation")
                                objConferencePaper.ConferenceLocation = objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].InnerText;
                        }
                        objConferencePaperlst.Add(objConferencePaper);
                    }
                    else if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].Name == "OtherPublication")
                    {
                        OtherPublication objOtherPublication = new OtherPublication();
                        if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].Attributes["type"] != null)
                        {
                            objOtherPublication.OtherPublicationType = objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].Attributes["type"].Value;
                        }
                        for (int v = 0; v < objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes.Count; v++)
                        {
                            if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].Name == "Title")
                                objOtherPublication.Title = objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].InnerText;
                            else if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].Name == "Name")
                            {
                                if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].Attributes["role"] != null && IsRoleRemain == true)
                                {
                                    objOtherPublication.Role = objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].Attributes["role"].Value;
                                    IsRoleRemain = false;
                                }
                                if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].ChildNodes[0].Name == "FormattedName")
                                    objOtherPublication.FormattedName = objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].ChildNodes[0].InnerText;
                            }
                            else if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].Name == "PublicationDate")
                                objOtherPublication.PublicationDate = objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].ChildNodes[0].InnerText;
                            else if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].Name == "Comments")
                                objOtherPublication.Comments = objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].InnerText;
                            else if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].Name == "NumberOfPages")
                                objOtherPublication.NumberOfPages = objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].InnerText;
                            else if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].Name == "PublisherLocation")
                                objOtherPublication.PublisherLocation = objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].InnerText;
                            else if (objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].Name == "Link")
                                objOtherPublication.Link = objXMLDoc.GetElementsByTagName("PublicationHistory")[0].ChildNodes[i].ChildNodes[v].InnerText;
                        }
                        objOtherPublicationlst.Add(objOtherPublication);
                    }
                }

                objPublicationhistory.objArticle = objArticallst;
                objPublicationhistory.objBook = objBooklst;
                objPublicationhistory.objConferencePaper = objConferencePaperlst;
                objPublicationhistory.objOtherPublication = objOtherPublicationlst;
            }

            #endregion

            #region Qualifications

            if (objXMLDoc.GetElementsByTagName("Qualifications").Count > 0)
            {
                List<Competency> objCompetencylst = new List<Competency>();
                for (int i = 0; i < objXMLDoc.GetElementsByTagName("Qualifications")[0].ChildNodes.Count; i++)
                {
                    if (objXMLDoc.GetElementsByTagName("Qualifications")[0].ChildNodes[i].Name == "QualificationSummary")
                        objQualification.QualificationSummary = objXMLDoc.GetElementsByTagName("Qualifications")[0].ChildNodes[i].InnerText;
                    else if (objXMLDoc.GetElementsByTagName("Qualifications")[0].ChildNodes[i].Name == "Competency")
                    {
                        Competency objCompetency = new Competency();
                        if (objXMLDoc.GetElementsByTagName("Qualifications")[0].ChildNodes[i].Attributes["name"] != null)
                            objCompetency.Name = objXMLDoc.GetElementsByTagName("Qualifications")[0].ChildNodes[i].Attributes["name"].Value;
                        for (int m = 0; m < objXMLDoc.GetElementsByTagName("Qualifications")[0].ChildNodes[i].ChildNodes.Count; m++)
                        {
                            if (objXMLDoc.GetElementsByTagName("Qualifications")[0].ChildNodes[i].ChildNodes[m].Name == "CompetencyEvidence")
                                objCompetency.TotalMonths = objXMLDoc.GetElementsByTagName("Qualifications")[0].ChildNodes[i].ChildNodes[m].ChildNodes[0].InnerText;
                        }
                        objCompetencylst.Add(objCompetency);
                    }
                }
                objQualification.objCompetency = objCompetencylst;
            }

            #endregion
        }
    }

    #region Classes

    #region Contact Info
    //Start Contact Info

    public class PersonalInfo
    {
        public string FormattedName { get; set; }
        public string GivenName { get; set; }
        public string MiddleName { get; set; }
        public string FamilyName { get; set; }
        public string Affix { get; set; }        
        public string DateType { get; set; }
        public string AnyDate { get; set; }        
    }

    public class Telephone
    {
        public string Tel_FormattedNumber
        {
            get;
            set;
        }
    }

    public class Mobile
    {
        public string MO_FormattedNumber
        {
            get;
            set;
        }
    }

    public class Email
    {
        public string InternetEmailAddress
        {
            get;
            set;
        }
    }

    public class ContactMethod
    {
        public string WhenAvailable { get; set; }

        public string Use { get; set; }

        public string Location { get; set; }

        public PostalAddress objPostalAddress { get; set; }

        public Email objEmail { get; set; }

        public Mobile objMobile { get; set; }

        public Telephone objTelephone { get; set; }

        public WebAdd objWebAdd { get; set; }

    }

    public class PostalAddress : Location
    {
        //public string AddressType
        //{
        //    get;
        //    set;
        //}
        //public string Region
        //{
        //    get;
        //    set;
        //}
        //public string PostalCode
        //{
        //    get;
        //    set;
        //}
        //public string CountryCode
        //{
        //    get;
        //    set;
        //}
        //public string Municipality
        //{
        //    get;
        //    set;
        //}
        public List<DeliveryAddress> DeliveryAddresslst { get; set; }
    }

    public class DeliveryAddress
    {
        public List<AddressLine> objAddLine { get; set; }
        public string StreetName { get; set; }
        public string BuildingNumber { get; set; }
        public string Unit { get; set; }
        public string PostOfficeBox { get; set; }
    }

    public class AddressLine
    {
        public string AddLine { get; set; }
    }

    public class Fax
    {
        public string fax_InternationalCountryCode { get; set; }
        public string fax_AreaCityCode { get; set; }
        public string fax_SubscriberNumber { get; set; }
    }

    public class Pager
    {
        public string pager_InternationalCountryCode { get; set; }
        public string pager_AreaCityCode { get; set; }
        public string pager_SubscriberNumber { get; set; }
        public string pager_Extension { get; set; }
    }

    public class WebAdd
    {
        public string InternetWebAddress { get; set; }
    }

    //End Contact Info
    #endregion

    #region Location

    public class Location
    {
        public string CountryCode { get; set; }

        public string Region { get; set; }

        public string Municipality { get; set; }

        public string PostalCode { get; set; }

        public string AddressType
        {
            get;
            set;
        }
    }

    #endregion

    #region Executive Summary
    //Executive Summary

    public class ExecutiveSummary
    {
        public string ExecutiveSummaryDetail { get; set; }
    }

    //End Executive Summary
    #endregion

    #region Objective
    //Start Objective

    public class Objective
    {
        public string objective { get; set; }
    }

    //End Objective
    #endregion

    #region Language
    //Start Language

    public class Language
    {
        public string LanguageCode { get; set; }
        public string Read { get; set; }
        public string Write { get; set; }
        public string Speak { get; set; }
        public string Comments { get; set; }
    }

    //End Language
    #endregion

    #region Employment History
    //Start Employment History

    public class EmploymentHistory
    {
        public string EmployerOrgName { get; set; }

        public string PositionType { get; set; }

        public string CurrentEmployer { get; set; }

        public string Title { get; set; }

        public string OrganizationName { get; set; }

        public string Description { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public List<Location> objLocLst { get; set; }
    }

    //End EmploymentHistory
    #endregion

    #region Education History

    public class SchoolOrInstitution : Location
    {
        public string SchoolType { get; set; }

        public string SchoolName { get; set; }

        public List<Degree> objDegreeLst { get; set; }
    }

    public class Degree
    {
        public string DegreeType { get; set; }

        public string DegreeName { get; set; }

        public string DegreeDate { get; set; }

        public string DegreeMajor { get; set; }

        public string DegreeMinor { get; set; }

        public string MeasureSystem { get; set; }

        public string MeasureValue { get; set; }

        public string DatesOfAttendance_StartDate { get; set; }

        public string DatesOfAttendance_EndDate { get; set; }

        public string LowestPossibleValue { get; set; }

        public string HighestPossibleValue { get; set; }

        public string Comments { get; set; }
    }

    #endregion

    #region LicenseOrCertification

    public class LicenseOrCertification
    {
        public string Name { get; set; }

        public string IssuingAuthority { get; set; }

        public string Description { get; set; }

        public string ValidFrom { get; set; }

        public string ValidTo { get; set; }

        public string FirstIssuedDate { get; set; }
    }

    #endregion

    #region Association

    public class Association
    {
        public string AssociationType { get; set; }

        public string Name { get; set; }

        public string Link { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public List<Role> objRoleLst { get; set; }

    }

    public class Role
    {
        public string RoleName { get; set; }

        public string Deliverable { get; set; }
    }
    #endregion

    #region Achievement

    public class Achievement
    {
        public string StringDate { get; set; }

        public string Description { get; set; }

        public string IssuingAuthority { get; set; }

    }

    #endregion

    #region SpeakingEventsHistory

    public class SpeakingEventsHistory
    {
        public string SpeakingEventtype { get; set; }

        public string StartDate { get; set; }

        public string EventName { get; set; }

        public string EventType { get; set; }

        public string Title { get; set; }

        public string Location { get; set; }

        public string Link { get; set; }

    }

    #endregion

    #region Publication History

    public class PublicationHistory
    {
        public List<Article> objArticle { get; set; }

        public List<Book> objBook { get; set; }

        public List<ConferencePaper> objConferencePaper { get; set; }

        public List<OtherPublication> objOtherPublication { get; set; }
    }

    public class Article : Publication
    {
        public string JournalOrSerialName { get; set; }

        public string Volume { get; set; }

        public string Issue { get; set; }

        public string PageNumber { get; set; }

        public string PublicationLanguage { get; set; }
    }

    public class Book : Publication
    {
        public List<Copyright> objCopyright { get; set; }

        public string Abstract { get; set; }

        public string Edition { get; set; }

        public string ISBN { get; set; }

        public string PublisherName { get; set; }

        public string PublisherLocation { get; set; }
    }

    public class ConferencePaper : Publication
    {
        public string EventName { get; set; }

        public string ConferenceDate { get; set; }

        public string ConferenceLocation { get; set; }
    }

    public class OtherPublication : Publication
    {
        public string OtherPublicationType { get; set; }

        public string Link { get; set; }

        public string Comments { get; set; }

        public string NumberOfPages { get; set; }

        public string PublisherLocation { get; set; }
    }

    public class Publication
    {
        public string Title { get; set; }

        public string FormattedName { get; set; }

        public string Role { get; set; }

        public string PublicationDate { get; set; }
    }

    public class Copyright
    {
        public string CopyrightText { get; set; }

        public List<CopyrightDates> objCopyrightDates { get; set; }
    }

    public class CopyrightDates
    {
        public string CopyrightDate { get; set; }
    }
    #endregion

    #region Qualifications

    public class Qualifications
    {
        public string QualificationSummary { get; set; }

        public List<Competency> objCompetency { get; set; }
    }

    public class Competency
    {
        public string Name { get; set; }

        public string TotalMonths { get; set; }
    }
    #endregion

    #endregion
}
