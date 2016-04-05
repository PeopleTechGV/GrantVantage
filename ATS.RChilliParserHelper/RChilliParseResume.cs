using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Xml;
using DynamicIOStream.Xml;
namespace ATS.RChilliParserHelper
{
    


    public class SegregateQualification
    {
        string university = "";
        string degree = "";
        string year = "";

        public string University
        {
            get;
            set;
        }
        public string Degree
        {
            get;
            set;
        }
        public string Year
        {
            get;
            set;
        }

    }
    public class Projects
    {
        public string ProjectName
        {
            get;
            set;
        }
        public string UsedSkills
        {
            get;
            set;
        }
        public string TeamSize
        {
            get;
            set;
        }
    }
    public class SkillSet
    {
        public string Skill
        {
            get;
            set;
        }
        public string ExperienceInMonth
        {
            get;
            set;
        }
    }
    public class Skills
    {
        public string BehaviorSkills
        {
            get;
            set;
        }
        public string SoftSkills
        {
            get;
            set;
        }
        public List<SkillSet> OperationalSkills
        {
            get;
            set;
        }
    }

    public class SegregateExperience
    {

        public string Employer
        {
            get;
            set;
        }
        public string JobProfile
        {
            get;
            set;
        }
        public string JobLocation
        {
            get;
            set;
        }
        public string JobPeriod
        {
            get;
            set;
        }
        public string StartDate
        {
            get;
            set;
        }
        public string EndDate
        {
            get;
            set;
        }
        public string JobDescription
        {
            get;
            set;
        }
        public List<Projects> project
        {
            get;
            set;
        }

    }

    public class RChilliParseResume
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



        string resumeFileName = "";
        string parsingDate = "";
        string titleName = "";
        string firstName = "";
        string middlename = "";
        string lastName = "";
        string email = "";
        string phone = "";
        string mobile = "";
        string faxNo = "";
        string licenseNo = "";
        string passportNo = "";
        string visaStatus = "";
        string address = "";
        string city = "";
        string state = "";
        string zipCode = "";
        string category = "";
        string subCategory = "";
        string dateOfBirth = "";        
        string fatherName = "";
        string motherName = "";
        string currentSalary = "";
        string expectedSalary = "";
        string qualification = "";
        string skills = "";
        string languageKnown = "";
        string experience = "";
        string currentEmployer = "";
        string jobProfile = "";
        string workedPeriod = "";
        string gapPeriod = "";
        string numberofJobChanged = "";
        string hobbies = "";
        string objectives = "";
        string achievements = "";
        string references = "";
        string detailResume = "";
        string htmlResume = "";
        string workPeriodInMonth = "";
        string workPeriodInYear = "";
        string workPeriodRange = "";
        List<SegregateQualification> segQualification = new List<SegregateQualification>();
        List<SegregateExperience> segExperience = new List<SegregateExperience>();
        List<Skills> segSkills = new List<Skills>();

        public string ResumeFileName { get { return resumeFileName; } }
        public string ParsingDate { get { return parsingDate; } }
        public string TitleName { get { return titleName; } }
        public string FirstName { get { return firstName; } }
        public string Middlename { get { return middlename; } }
        public string LastName { get { return lastName; } }
        public string Email { get { return email; } }
        public string Phone { get { return phone; } }
        public string Mobile { get { return mobile; } }
        public string FaxNo { get { return faxNo; } }
        public string LicenseNo { get { return licenseNo; } }
        public string PassportNo { get { return passportNo; } }
        public string VisaStatus { get { return visaStatus; } }
        public string Address { get { return address; } }
        public string City { get { return city; } }
        public string State { get { return state; } }
        public string ZipCode { get { return zipCode; } }
        public string Category { get { return category; } }
        public string SubCategory { get { return subCategory; } }
        public string DateOfBirth { get { return dateOfBirth; } }        
        public string FatherName { get { return fatherName; } }
        public string MotherName { get { return motherName; } }
        public string CurrentSalary { get { return currentSalary; } }
        public string ExpectedSalary { get { return expectedSalary; } }
        public string Qualification { get { return qualification; } }
        public string Skills { get { return skills; } }
        public string LanguageKnown { get { return languageKnown; } }
        public string Experience { get { return experience; } }
        public string CurrentEmployer { get { return currentEmployer; } }
        public string JobProfile { get { return jobProfile; } }
        public string WorkedPeriod { get { return workedPeriod; } }
        public string GapPeriod { get { return gapPeriod; } }
        public string Hobbies { get { return hobbies; } }
        public string Objectives { get { return objectives; } }
        public string Achievements { get { return achievements; } }
        public string References { get { return references; } }
        public string DetailResume { get { return detailResume; } }
        public string HtmlResume { get { return htmlResume; } }
        public string WorkPeriodInMonth { get { return workPeriodInMonth; } }
        public string WorkPeriodInYear { get { return workPeriodInYear; } }
        public string WorkPeriodRange { get { return workPeriodRange; } }
        public List<SegregateQualification> SegQualification { get { return segQualification; } }
        public List<SegregateExperience> SegExperience { get { return segExperience; } }
        public List<Skills> SegSkills { get { return segSkills; } }

        //public void ParseResume(string filePath, string userKey, string countryKey, string version, string subUserId)
        public void ParseResume(string filePath, string userKey, string version, string subUserId,bool IsTest=true)
        {
            try
            {
             
                if (!IsTest)
                {
                    FileInfo file = new FileInfo(filePath);
                    byte[] DataFile = converttoBase64(file);
                    //string requestString = soapRequest(Convert.ToBase64String(DataFile), file.Name, userKey, countryKey, version, subUserId);
                    //string outPutXml = CallRchilliApi(requestString, serviceUrl).Replace("\r", Environment.NewLine);
                    //Do when live parser needed
                    string outPutXml = CallJavaApi(Convert.ToBase64String(DataFile), file.Name, userKey, version, subUserId, serviceUrl);
                    byte[] byteArray = Encoding.UTF8.GetBytes(outPutXml);
                    MemoryStream stream = new MemoryStream(byteArray);
                    ReadXml(stream);
                }
                else
                {

                    filePath = Directory.GetParent(filePath).FullName;
                    filePath = filePath + @"\new  2.xml";

                    FileStream fileStream = File.OpenRead(filePath);
                    //create new MemoryStream object
                    MemoryStream memStream = new MemoryStream();
                    memStream.SetLength(fileStream.Length);
                    //read file to MemoryStream
                    fileStream.Read(memStream.GetBuffer(), 0, (int)fileStream.Length);
                    ReadXml(memStream);
                }
            }
            catch (Exception ex)
            {
                errorCode = "5001";
                errorMessage = ex.Message;
            }


        }
        string CallJavaApi(string base64string, string fileName, string userKey, string version, string subUserId, string serviceUrl)
        {

            RChilliParser rchilli = new RChilliParser();
            rchilli.Url = serviceUrl;
            return rchilli.parseResumeBinary(base64string, fileName, userKey, version, subUserId);
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
        string soapRequest(string base64string, string fileName, string userKey, string countryKey, string version, string subUserId)
        {
            StringBuilder strRequest = new StringBuilder();
            strRequest.Append("<?xml version='1.0' encoding='utf-8'?>");
            strRequest.Append("<soap:Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:soap='http://schemas.xmlsoap.org/soap/envelope/'>");
            strRequest.Append("<soap:Body>");
            strRequest.Append("<ParseResumeBinary   xmlns='http://tempuri.org/'>");
            strRequest.Append("<filedata>" + base64string + "</filedata><filetype>" + fileName + "</filetype>");
            strRequest.Append("<key>" + userKey + "</key>");
            strRequest.Append("<version>" + version + "</version>");
            strRequest.Append("<countryKey>" + countryKey + "</countryKey>");
            strRequest.Append("<subUserId>" + subUserId + "</subUserId>");
            strRequest.Append("</ParseResumeBinary>");
            strRequest.Append("</soap:Body>");
            strRequest.Append("</soap:Envelope>");
            return strRequest.ToString();
        }



        string CallRchilliApi(string strRequest, string serviceUrl)
        {
            try
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(strRequest);
                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
                httpRequest.Credentials = CredentialCache.DefaultCredentials;
                httpRequest.Method = "POST";
                httpRequest.ContentType = "text/xml";
                httpRequest.ContentLength = byteArray.Length;
                httpRequest.Timeout = 300000;
                Stream dataStream = httpRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream(), System.Text.Encoding.UTF8);
                string ack = string.Empty;
                using (XmlReader reader = XmlReader.Create(streamReader))
                {
                    while (reader.Read())
                    {

                        if (reader.NodeType == XmlNodeType.Element)
                        {

                            for (int count = 1; count <= reader.Depth; count++)
                            {
                                if (reader.Name == "ParseResumeBinaryResult")
                                {
                                    ack = reader.ReadElementContentAsString();
                                }

                            }

                        }
                    }
                }
                return ack;


            }
            catch (Exception ex)
            {
                error = true;
                errorCode = "5000";
                errorMessage = "Exception occured" + ex.Message;
                return ex.Message + "\n\r" + ex.StackTrace + "\n\r" + ex.Source;

            }
            /////////////////API CALL PROCESS END///////////////////
        }
        void ReadXml(Stream stream)
        {
            dynamic resumeParserData = DynamicXmlStream.Load(stream);
            try
            {
                if (resumeParserData.error.ToString() != "")
                {
                    error = true;
                    errorCode = resumeParserData.error.errorcode;
                    errorMessage = resumeParserData.errormsg;

                }
                return;
            }
            catch
            {

            }
            resumeFileName = resumeParserData.ResumeParserData.ResumeFileName;
            parsingDate = resumeParserData.ResumeParserData.ParsingDate;
            titleName = resumeParserData.ResumeParserData.TitleName;
            firstName = resumeParserData.ResumeParserData.FirstName;
            middlename = resumeParserData.ResumeParserData.Middlename;
            lastName = resumeParserData.ResumeParserData.LastName;
            email = resumeParserData.ResumeParserData.Email;
            phone = resumeParserData.ResumeParserData.Phone;
            mobile = resumeParserData.ResumeParserData.Mobile;
            faxNo = resumeParserData.ResumeParserData.FaxNo;
            licenseNo = resumeParserData.ResumeParserData.LicenseNo;
            passportNo = resumeParserData.ResumeParserData.PassportNo;
            visaStatus = resumeParserData.ResumeParserData.VisaStatus;
            address = resumeParserData.ResumeParserData.Address;
            city = resumeParserData.ResumeParserData.City;
            state = resumeParserData.ResumeParserData.State;
            zipCode = resumeParserData.ResumeParserData.ZipCode;
            category = resumeParserData.ResumeParserData.Category;
            subCategory = resumeParserData.ResumeParserData.SubCategory;
            dateOfBirth = resumeParserData.ResumeParserData.DateOfBirth;            
            fatherName = resumeParserData.ResumeParserData.FatherName;
            motherName = resumeParserData.ResumeParserData.MotherName;                        
            currentSalary = resumeParserData.ResumeParserData.CurrentSalary;
            expectedSalary = resumeParserData.ResumeParserData.ExpectedSalary;
            qualification = resumeParserData.ResumeParserData.Qualification;
            skills = resumeParserData.ResumeParserData.Skills;
            languageKnown = resumeParserData.ResumeParserData.LanguageKnown;
            experience = resumeParserData.ResumeParserData.Experience;
            currentEmployer = resumeParserData.ResumeParserData.CurrentEmployer;
            jobProfile = resumeParserData.ResumeParserData.JobProfile;
            // workedPeriod = resumeParserData.ResumeParserData.WorkedPeriod;
            gapPeriod = resumeParserData.ResumeParserData.GapPeriod;
            hobbies = resumeParserData.ResumeParserData.Hobbies;
            objectives = resumeParserData.ResumeParserData.Objectives;
            achievements = resumeParserData.ResumeParserData.Achievements;
            references = resumeParserData.ResumeParserData.References;
            detailResume = resumeParserData.ResumeParserData.DetailResume;
            //htmlResume = resumeParserData.ResumeParserData.htmlresume;
            //htmlResume = resumeParserData.ResumeParserData

            workedPeriod = resumeParserData.ResumeParserData.WorkedPeriod.TotalExperienceInMonths + " Months";

            foreach (dynamic WorkedPeriod in (resumeParserData.ResumeParserData.WorkedPeriod as DynamicXmlStream).AsDynamicEnumerable())
            {
                workPeriodInMonth = WorkedPeriod.TotalExperienceInMonths;
                workPeriodInYear = WorkedPeriod.TotalExperienceInYear;
                workPeriodRange = WorkedPeriod.TotalExperienceRange;

            }
            try
            {
                foreach (dynamic qualif in (resumeParserData.ResumeParserData.SegregatedQualification.EducationSplit as DynamicXmlStream).AsDynamicEnumerable())
                {
                    segQualification.Add(new SegregateQualification { University = qualif.University, Degree = qualif.Degree, Year = qualif.Year });
                }
            }
            catch
            {
            }
            try
            {
                foreach (dynamic exp in (resumeParserData.ResumeParserData.SegregatedExperience.WorkHistory as DynamicXmlStream).AsDynamicEnumerable())
                {
                    List<Projects> projects = new List<Projects>();
                    foreach (dynamic project in (exp.Projects as DynamicXmlStream).AsDynamicEnumerable())
                    {
                        projects.Add(new Projects { ProjectName = project.ProjectName, TeamSize = project.TeamSize, UsedSkills = project.UsedSkills });
                    }
                    segExperience.Add(new SegregateExperience { Employer = exp.Employer, JobProfile = exp.JobProfile, JobLocation = exp.JobLocation, JobPeriod = exp.JobPeriod, StartDate = exp.StartDate, EndDate = exp.EndDate, JobDescription = exp.JobDescription, project = projects });
                }
            }
            catch
            {
            }
            try
            {
                foreach (dynamic skillKeyword in (resumeParserData.ResumeParserData.skillskeywords as DynamicXmlStream).AsDynamicEnumerable())
                {
                    List<SkillSet> operationalSkills = new List<SkillSet>();
                    foreach (dynamic skill in (skillKeyword.OperationalSkills.SkillSet as DynamicXmlStream).AsDynamicEnumerable())
                    {
                        operationalSkills.Add(new SkillSet { Skill = skill.Skill, ExperienceInMonth = skill.ExperienceInMonths });
                    }
                    segSkills.Add(new Skills { BehaviorSkills = skillKeyword.BehaviorSkills, SoftSkills = skillKeyword.SoftSkills, OperationalSkills = operationalSkills });
                }
            }
            catch { }
        }


    }
}