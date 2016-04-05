using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ATS.WebUi.Models;
using System.Reflection;
using System.Web.Mvc;
using BEClient = ATS.BusinessEntity;
using BLClient = ATS.BusinessLogic;
using System.IO;
using System.Net;
using Res = Resources;
using System.Globalization;
using System.Collections.Specialized;
using SolrEntity = ATS.BusinessEntity.SolrEntity;
using System.Web.Routing;
using log4net;
using Newtonsoft.Json;
using SolrNet;
using BEMaster = ATS.BusinessEntity.Master;
using ATSCommon = ATS.WebUi.Common;



using SolrBL = ATS.BusinessLogic.SolrBase;
using BECompanySetupMenuConst = ATS.BusinessEntity.Common.CompanySetupMenu;
using GridMvc.Filtering;
using GridMvc.Sorting;
using System.Security.Policy;
namespace ATS.WebUi.Common
{
    public class CommonFunctions
    {
        const string STR_PDF_EXTENSION = ".pdf";

        private static readonly log4net.ILog log = LogManager.GetLogger(
    System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static Dictionary<int, string> QuestionType { get; set; }
        public static Dictionary<int, string> AllowAnsOptQuestionType { get; set; }
        public static Dictionary<string, string> ApplicationStatus { get; set; }

        public static List<CompanySetup> CompanySetupLst = null;
        static CommonFunctions()
        {
            SetupDefaultQuestionType();
            SetupDefaultApplicationStatus();
        }

        private static void SetupCompanySetupList()
        {
            CompanySetupLst = new List<CompanySetup>();
            
            CompanySetupLst.Add(new CompanySetup
            {
                DisplayName = BECompanySetupMenuConst.CSMNU_COMPANYINFO,
                Action = "Index",
                Controller = "CompanyInfo",
                AllowToAdminOnly = false,
                IsDefault = false,
                ImagePath = "",
                param = new RouteValueDictionary(new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE })
            });
            CompanySetupLst.Add(new CompanySetup
            {
                DisplayName = BECompanySetupMenuConst.CSMNU_DIVISION,
                Action = "Index",
                Controller = "Division",
                AllowToAdminOnly = false,
                IsDefault = false,
                ImagePath = "",
                param = new RouteValueDictionary(new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE })
            });
            //CR-14
            //CompanySetupLst.Add(new CompanySetup
            //{
            //    DisplayName = BECompanySetupMenuConst.CSMNU_DEGREETYPE,
            //    Action = "Index",
            //    Controller = "DegreeType",
            //    AllowToAdminOnly = false,
            //    IsDefault = false,
            //    ImagePath = "",
            //    param = new RouteValueDictionary(new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE })
            //});

            CompanySetupLst.Add(new CompanySetup
            {
                DisplayName = BECompanySetupMenuConst.CSMNU_REVIEWROUNDS,
                Action = "Index",
                Controller = "RndType",
                AllowToAdminOnly = false,
                IsDefault = false,
                ImagePath = "",
                param = new RouteValueDictionary(new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE })
            });

            //CompanySetupLst.Add(new CompanySetup
            //{
            //    DisplayName = "Question Category",
            //    Action = "Index",
            //    Controller = "QueCat",
            //    IsDefault = false,
            //    AllowToAdminOnly = false,
            //    ImagePath = "",
            //    param = new RouteValueDictionary(new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE })
            //});

            CompanySetupLst.Add(new CompanySetup
            {
                DisplayName = BECompanySetupMenuConst.CSMNU_JOBLOCATION,
                Action = "Index",
                Controller = "JobLocation",
                AllowToAdminOnly = false,
                IsDefault = false,
                ImagePath = "",
                param = new RouteValueDictionary(new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE })
            });

            CompanySetupLst.Add(new CompanySetup
            {
                DisplayName = BECompanySetupMenuConst.CSMNU_POSITIONTYPE,
                Action = "Index",
                Controller = "PositionType",
                AllowToAdminOnly = false,
                IsDefault = false,
                ImagePath = "",
                param = new RouteValueDictionary(new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE })
            });

            //CR-14
            //CompanySetupLst.Add(new CompanySetup
            //{
            //    DisplayName = BECompanySetupMenuConst.CSMNU_SKILLTYPE,
            //    Action = "Index",
            //    Controller = "SkillType",
            //    AllowToAdminOnly = false,
            //    IsDefault = false,
            //    ImagePath = "",
            //    param = new RouteValueDictionary(new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE })
            //});

            CompanySetupLst.Add(new CompanySetup
           {
               DisplayName = BECompanySetupMenuConst.CSMNU_QUE,
               Action = "Index",
               Controller = "QuestionLibrary",
               AllowToAdminOnly = false,
               IsDefault = false,
               ImagePath = "",
               param = new RouteValueDictionary(new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE })
           });

            //   CompanySetupLst.Add(new CompanySetup
            //{
            //    DisplayName = BECompanySetupMenuConst.CSMNU_USERPRIVILEGES,
            //    Action = "Index",
            //    Controller = "UserPrivileges",
            //    AllowToAdminOnly = false,
            //    IsDefault = true,
            //    ImagePath = "",
            //    param = new RouteValueDictionary(new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE, userId = ATS.WebUi.Common.CurrentSession.Instance.UserId })
            //});

            CompanySetupLst.Add(new CompanySetup
            {
                DisplayName = BECompanySetupMenuConst.CSMNU_USER,
                Action = "Index",
                Controller = "User",
                AllowToAdminOnly = false,
                IsDefault = false,
                ImagePath = "",
                param = new RouteValueDictionary(new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE })
            });

            CompanySetupLst.Add(new CompanySetup
           {
               DisplayName = BECompanySetupMenuConst.CSMNU_STATUSREASON,
               Action = "Index",
               Controller = "VacancyStatus",
               AllowToAdminOnly = false,
               IsDefault = false,
               ImagePath = "",
               param = new RouteValueDictionary(new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE })
           });
            CompanySetupLst.Add(new CompanySetup
            {
                DisplayName = BECompanySetupMenuConst.CSMNU_VACANCYTEMPLATES,
                Action = "Index",
                Controller = "TemplateVacancy",
                AllowToAdminOnly = false,
                IsDefault = false,
                ImagePath = "",
                param = new RouteValueDictionary(new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE })
            });
            CompanySetupLst.Add(new CompanySetup
            {
                DisplayName = BECompanySetupMenuConst.CSMNU_LANGUAGEBLOCKS,
                Action = "Index",
                Controller = "Languageblocks",
                AllowToAdminOnly = false,
                IsDefault = false,
                ImagePath = "",
                param = new RouteValueDictionary(new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE })
            });
            CompanySetupLst.Add(new CompanySetup
            {
                DisplayName = BECompanySetupMenuConst.CSMNU_DECLINESTATUS,
                Action = "Index",
                Controller = "ApplicationBasedStatus",
                AllowToAdminOnly = false,
                IsDefault = false,
                ImagePath = "",
                param = new RouteValueDictionary(new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE })
            });
            CompanySetupLst.Add(new CompanySetup
            {
                DisplayName = BECompanySetupMenuConst.CSMNU_EMAILTEMPLATES,
                Action = "Index",
                Controller = "EmailTemplates",
                AllowToAdminOnly = false,
                IsDefault = false,
                ImagePath = "",
                param = new RouteValueDictionary(new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE })
            });
            CompanySetupLst.Add(new CompanySetup
            {
                DisplayName = BECompanySetupMenuConst.CSMNU_OFFERLETTER,
                Action = "Index",
                Controller = "OfferLetters",
                AllowToAdminOnly = false,
                IsDefault = false,
                ImagePath = "Offer Letters",
                param = new RouteValueDictionary(new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE })
            });
            CompanySetupLst.Add(new CompanySetup
            {
                DisplayName = BECompanySetupMenuConst.CSMNU_OFFERTEMPLATE,
                Action = "Index",
                Controller = "OfferTemplates",
                AllowToAdminOnly = false,
                IsDefault = false,
                ImagePath = "Offer Templates",
                param = new RouteValueDictionary(new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE })
            });
            CompanySetupLst.Add(new CompanySetup
            {
                DisplayName = BECompanySetupMenuConst.CSMNU_DOCUMENTTYPES,
                Action = "Index",
                Controller = "DocumentType",
                AllowToAdminOnly = false,
                IsDefault = false,
                ImagePath = "Document Types",
                param = new RouteValueDictionary(new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE })
            });
            CompanySetupLst.Add(new CompanySetup
            {
                DisplayName = BECompanySetupMenuConst.CSMNU_COLOROPTIONS,
                Action = "Index",
                Controller = "SkinMaster",
                AllowToAdminOnly = false,
                IsDefault = false,
                ImagePath = "Color Options",
                param = new RouteValueDictionary(new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE })
            });

            CompanySetupLst.Add(new CompanySetup
            {
                DisplayName = BECompanySetupMenuConst.CSMNU_SECURITY_TEMPLATES,
                Action = "Index",
                Controller = "ATSSecurity",
                AllowToAdminOnly = true,
                IsDefault = false,
                ImagePath = "",
                param = new RouteValueDictionary(new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE })
            });
            CompanySetupLst = CompanySetupLst.OrderBy(x => x.DisplayName).ToList();
        }

        public static CompanySetup DefaultCompanySetupItem()
        {
            if (CompanySetupLst == null)
            {
                SetupCompanySetupList();
            }
            return CompanySetupLst.Where(x => x.IsDefault.Equals(true)).ToList().FirstOrDefault();
        }
        public static List<CompanySetup> AdminCompanySetupMenu()
        {
            if (CompanySetupLst == null)
            {
                SetupCompanySetupList();
            }
            return CompanySetupLst;
        }
        public static List<CompanySetup> CompanySetupMenu()
        {
            if (CompanySetupLst == null)
            {
                SetupCompanySetupList();
            }
            return CompanySetupLst.Where(x => x.AllowToAdminOnly.Equals(false)).ToList();
        }

        public static void SetupDefaultQuestionType()
        {
            QuestionType = new Dictionary<int, string>();
            QuestionType.Add(1, "Pick List");
            QuestionType.Add(2, "Check List");
            QuestionType.Add(3, "Large Text");
            QuestionType.Add(4, "Small Text");
            QuestionType.Add(5, "Yes/No");
            QuestionType.Add(6, "Scale 1-100");
            //CR-9
            QuestionType.Add(7, "Date");
            QuestionType.Add(8, "Person");
            QuestionType.Add(9, "Organization");


            //Static condition will be check on SubmitAnsOpt submit 
            //function will be located Areas\Employee\Views\QuestionLibrary\Index.cshtml
            AllowAnsOptQuestionType = new Dictionary<int, string>();
            AllowAnsOptQuestionType.Add(1, "Pick List");
            AllowAnsOptQuestionType.Add(2, "Check List");
            //AllowAnsOptQuestionType.Add(5, "Yes/No");


        }

        public static void SetupDefaultApplicationStatus()
        {
            ApplicationStatus = new Dictionary<string, string>();
            ApplicationStatus.Add("APP_STAT_SUBMIT", "APP_STAT_SUBMIT");
            ApplicationStatus.Add("APP_STAT_VIEW", "APP_STAT_VIEW");
            ApplicationStatus.Add("APP_STAT_EDIT", "APP_STAT_EDIT");
            ApplicationStatus.Add("APP_STAT_DRAFT", "APP_STAT_DRAFT");
            ApplicationStatus.Add("APP_STAT_INTERV", "APP_STAT_INTERV");
            ApplicationStatus.Add("APP_STAT_WITHDRAW", "APP_STAT_WITHDRAW");

        }
        public static string HtmlToPdf(string urls, string pdfOutputLocation, string fileName, bool ShowHeader = true)
        {
            string urlsSeparatedBySpaces = string.Empty;
            string pdfHtmlToPdfExePath = ConfigurationMapped.Instance.WKHtmltopdfPath + "wkhtmltopdf.exe";
            string outputFilenamePrefix = "Temp";
            try
            {

                string outputFolder = pdfOutputLocation;
                string outputFilename = fileName; // outputFilenamePrefix + "_" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss-fff") + ".PDF"; // assemble destination PDF file name
                //string headerHTMlPath = HttpContext.Current.Request.Url.ToString().Substring(0, HttpContext.Current.Request.Url.ToString().IndexOf("/Employee") + 1);
                string headerHTMlPath = "http://" + Common.ConfigurationMapped.Instance.DomianName + "/header.aspx";
                headerHTMlPath = " --lowquality --dpi 100 --quiet -q --header-html " + headerHTMlPath;// +"header.aspx";

                if (!ShowHeader)
                {
                    headerHTMlPath = "";
                }
                var p = new System.Diagnostics.Process()
                {
                    StartInfo =
                    {
                        FileName = pdfHtmlToPdfExePath,
                        //Arguments = ((options == null) ? " --header-html http://localhost:53382/header.html " : String.Join(" ", options)) + " " + urls + " " + outputFilename,
                        Arguments = headerHTMlPath + " " + urls + " " + outputFilename,
                        //Arguments = ((options == null) ? " --header-spacing 0 " : String.Join(" ", options)) + " " + urls + " " + outputFilename,
                        UseShellExecute = false, // needs to be false in order to redirect output
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        RedirectStandardInput = true, // redirect all 3, as it should be all 3 or none
                        WorkingDirectory = HttpContext.Current.Server.MapPath(outputFolder)
                    }
                };

                p.Start();

                // read the output here...
                var output = p.StandardOutput.ReadToEnd();
                var errorOutput = p.StandardError.ReadToEnd();

                // ...then wait n milliseconds for exit (as after exit, it can't read the output)
                p.WaitForExit(60000);

                // read the exit code, close process
                int returnCode = p.ExitCode;
                p.Close();

                // if 0 or 2, it worked so return path of pdf
                if ((returnCode == 0) || (returnCode == 2))
                    return outputFolder + outputFilename;
                else
                    throw new Exception(errorOutput);
            }
            catch (Exception exc)
            {
                throw new Exception("Problem generating PDF from HTML, URLs: " + urlsSeparatedBySpaces + ", outputFilename: " + outputFilenamePrefix, exc);
            }
        }

        public static bool GeneratePDF(String pageURL, String fileName)
        {
            try
            {
                System.Diagnostics.Process si = new System.Diagnostics.Process();
                si.StartInfo.WorkingDirectory = ConfigurationMapped.Instance.WKHtmltopdfPath;
                si.StartInfo.UseShellExecute = false;
                si.StartInfo.FileName = "cmd.exe";
                string arg = "/c wkhtmltopdf.exe " + pageURL + " " + fileName;
                si.StartInfo.Arguments = arg;
                si.StartInfo.CreateNoWindow = true;
                si.StartInfo.RedirectStandardInput = true;
                si.StartInfo.RedirectStandardOutput = true;
                si.StartInfo.RedirectStandardError = true;
                si.Start();
                si.WaitForExit();
                string output = si.StandardOutput.ReadToEnd();
                si.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static string TruncateProfileTitleString(string strTitle)
        {
            if (strTitle.Length > 20)
            {
                return strTitle.Remove(20) + "...";
            }
            else
            {
                return strTitle;
            }
        }

        public static ISolrReadOnlyOperations<SolrEntity.SolrSearchFields> _solrConnection;

        public static bool IsCurrentUserIsSystemAdmin()
        {
            BEClient.User objUser = WebUi.Common.CurrentSession.Instance.VerifiedUser;
            if (objUser.SecurityRoles != null && objUser.SecurityRoles.Where(x => x.AtsSecurityRole == BEClient.ATSSecurityRole.SystemAdministrator).Count() > 0)
                return true;
            else
                return false;
        }
        public static FileContentResult DownloadAppliedVacancyFiles(Guid ATSResumeId, Guid CurrentClientId, HttpServerUtilityBase Server)
        {
            BEClient.ATSResume objAtsresume = new BEClient.ATSResume();
            BLClient.ATSResumeAction objAtsResumeAction = new BLClient.ATSResumeAction(CurrentClientId);
            objAtsresume = objAtsResumeAction.GetRecordByRecorId(ATSResumeId);



            if (objAtsresume == null)
            {
                throw new Exception("File not Found");
            }
            else
            {

                byte[] template_file = System.IO.File.ReadAllBytes(Path.Combine(Server.MapPath(WebUi.Common.Constants.STR_RESUME_PATH), objAtsresume.NewFileName));
                var Resumepath = Path.Combine(Server.MapPath(WebUi.Common.Constants.STR_RESUME_PATH), objAtsresume.NewFileName);
                return new FileContentResult(template_file, "application / octet - stream") { FileDownloadName = objAtsresume.UploadedFileName };
            }
        }
        public static bool ForgotPassword(RequestContext request, SignUpModel signModule)
        {

            try
            {
                signModule.ClientId = WebUi.Common.CurrentSession.Instance.VerifiedClient.ClientId;
                BEClient.User objUser = BLClient.Common.LoginOperation.ValidateUserName(signModule.UserName, signModule.ClientId);
                if (objUser != null && objUser.UserId != Guid.Empty)
                {
                    string encrContactid = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(Convert.ToString(objUser.UserId));
                    string expireDateTicks = Convert.ToString(DateTime.UtcNow.AddHours(24).Ticks);
                    string encrCname = WebUi.Common.CurrentSession.Instance.VerifiedClient.Clientname;
                    string encrClientId = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(Convert.ToString(signModule.ClientId)); ;
                    var activationlink = Common.CommonFunctions.GenerateWebLink(RedirectToResetPassword(request, encrCname, encrContactid, expireDateTicks, encrClientId));

                    Mailers.EmailTemplate mvcMailer = new Mailers.EmailTemplate();
                    mvcMailer.SendCandidateForgotPasswordMail(signModule.UserName, activationlink, signModule.UserName, encrCname);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                throw;
            }
        }
        private static String RedirectToResetPassword(RequestContext request, string Company, string encConactId, string encExpires, string encclientId)
        {
            UrlHelper u = new UrlHelper(request);
            return u.Action("ResetPassword", "Home", new
            {
                company = Company,
                area = WebUi.Common.Constants.AREA_ROOT,
                id = encConactId,
                expires = encExpires,
                CompanyId = encclientId //,
                //cname = Company
            });
        }
        public static JsonModels GetJsonContentObj(bool IsSessionExpire, bool IsError, string Url, string Message = "", object data = null, bool IsDefaultMessage = false)
        {
            JsonModels jsonModel = new JsonModels();

            jsonModel.IsError = IsError;
            jsonModel.IsDefaultMessage = IsDefaultMessage;
            jsonModel.Message = Message;
            jsonModel.SessionTimeOut = IsSessionExpire;
            jsonModel.Data = data;
            if (IsError && String.IsNullOrEmpty(Message))
            {
                jsonModel.Message = "Failed";
            }
            else if (!IsError && String.IsNullOrEmpty(Message))
            {
                jsonModel.Message = "Success";
            }
            jsonModel.Url = Url;

            return jsonModel;

        }
        public static String GetMaxCharacter(String content, int no)
        {
            return content.Substring(0, no);
        }
        public static String LanguageLabel(String label)
        {
            String localName = Res.Resources.LanguageDisplay(label);
            if (String.IsNullOrEmpty(localName))
            {
                localName = label;
            }
            return localName;
        }
        public static SelectList YesNoDropDownList()
        {
            Array Values = System.Enum.GetValues(typeof(BEClient.YesNo));
            List<SelectListItem> lstitems = new List<SelectListItem>();

            foreach (int Value in Values)
            {
                BEClient.YesNo mydata = (BEClient.YesNo)Enum.Parse(typeof(BEClient.YesNo), Enum.GetName(typeof(BEClient.YesNo), Value));
                lstitems.Add(new SelectListItem() { Value = mydata == BEClient.YesNo.Yes ? true.ToString() : false.ToString(), Text = mydata.ToString() });
            }
            SelectList items = new SelectList(lstitems, "Value", "Text");
            return items;
        }


        //CRNOV14
        public static SelectList VacancyStatusDropDownList(BEClient.VacancyStatusDrp? VacStatus)
        {
            //lstVacDrp = lstVacDrp.Where(x => x.Text.Equals(Convert.ToString(VacStatus))).ToList();
            var lstVacDrp = Enum.GetValues(typeof(BEClient.VacancyStatusDrp)).Cast<BEClient.VacancyStatusDrp>().Select(c => new SelectListItem { Value = ((int)c).ToString(), Text = c.ToString() }).ToList();
            SelectList items = new SelectList(lstVacDrp, "Text", "Text", VacStatus);
            return items;

        }

        public static SelectList VacancyStatusDDL(BEClient.VacancyStatusDrp? VacStatus)
        {
            var lstVacDrp = Enum.GetValues(typeof(BEClient.VacancyStatusDrp)).Cast<BEClient.VacancyStatusDrp>().Select(c => new SelectListItem { Value = ((int)c).ToString(), Text = c.ToString() }).ToList();
            lstVacDrp = lstVacDrp.Where(x => x.Text.Equals(Convert.ToString(VacStatus))).ToList();
            SelectList items = new SelectList(lstVacDrp, "Text", "Text", VacStatus);
            return items;
        }

        public static SelectList AppBasedStatusDDL(BEClient.AppBasedStatusDrp? AppStatus)
        {
            var lstAppStatusDrp = Enum.GetValues(typeof(BEClient.AppBasedStatusDrp)).Cast<BEClient.AppBasedStatusDrp>().Select(c => new SelectListItem { Value = ((int)c).ToString(), Text = c.ToString() }).ToList();
            lstAppStatusDrp = lstAppStatusDrp.Where(x => x.Text.Equals(Convert.ToString(AppStatus))).ToList();
            SelectList items = new SelectList(lstAppStatusDrp, "Text", "Text", AppStatus);
            return items;
        }



        public static SelectList VacancyStatusCategoryDropDownList()
        {
            Array Values = System.Enum.GetValues(typeof(BEClient.VacancyStatusCategory));
            List<SelectListItem> lstitems = new List<SelectListItem>();
            foreach (int Value in Values)
            {
                BEClient.VacancyStatusCategory mydata = (BEClient.VacancyStatusCategory)Enum.Parse(typeof(BEClient.VacancyStatusCategory), Enum.GetName(typeof(BEClient.VacancyStatusCategory), Value));
                lstitems.Add(new SelectListItem() { Value = mydata == BEClient.VacancyStatusCategory.Closed ? "Closed" : "Open", Text = mydata.ToString() });
            }
            SelectList items = new SelectList(lstitems, "Value", "Text");
            return items;
        }

        public static SelectList YesNoDropDownListWithDefaultValue()
        {
            Array Values = System.Enum.GetValues(typeof(BEClient.YesNo));
            List<SelectListItem> lstitems = new List<SelectListItem>();

            foreach (int Value in Values)
            {
                BEClient.YesNo mydata = (BEClient.YesNo)Enum.Parse(typeof(BEClient.YesNo), Enum.GetName(typeof(BEClient.YesNo), Value));
                lstitems.Add(new SelectListItem() { Value = mydata == BEClient.YesNo.Yes ? true.ToString() : false.ToString(), Text = mydata.ToString() });
            }
            SelectList items = new SelectList(lstitems, "Value", "Text", true.ToString());
            return items;
        }
        public static List<BEClient.JobLocation> ClientLocationAutoComplete(string term, string displaytop, string columnName, Guid _ClientId, Guid _LanguageId, Guid DivisionId)
        {
            try
            {
                if (String.IsNullOrEmpty(displaytop))
                    displaytop = "20";

                BEClient.SearchModel _searchInfo = new BEClient.SearchModel();
                _searchInfo.PageSize = Convert.ToInt32(displaytop);
                _searchInfo.PageIndex = 1;
                _searchInfo.SearchField = columnName;
                _searchInfo.SearchValue = term;
                _searchInfo.SortBy = columnName;

                List<BEClient.JobLocation> _lstJobLocations = new List<BEClient.JobLocation>();
                BLClient.JobLocationAction _clientJoblocationAction = new BLClient.JobLocationAction(_ClientId);
                var _pagedJobLocations = _clientJoblocationAction.GetClientLocationBySearch(_ClientId, _LanguageId, DivisionId, _searchInfo.SearchValue);

                //lstJobLocations = _pagedJobLocations.Records.GroupBy(c => c.LocalName).Select(g => g.FirstOrDefault()).ToList();

                if (_pagedJobLocations != null)
                {
                    _lstJobLocations = _pagedJobLocations.Records.GroupBy(c => c.LocalName).Select(g => g.FirstOrDefault()).ToList();
                }

                return _lstJobLocations;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static string GenerateWebLink(String pAbsoluteUrl)
        {
            return GetServerUrl() + pAbsoluteUrl.TrimStart(new char[] { '/' });
        }


        internal static string GetServerUrl()
        {
            string originalUrl = HttpContext.Current.Request.Url.OriginalString;
            string absoluteUrl = HttpContext.Current.Request.Url.AbsolutePath;
            string serverUrl = string.Empty;
            if (string.IsNullOrEmpty(absoluteUrl.Replace("/", "")) == false && originalUrl.LastIndexOf(absoluteUrl) > 0)
            {
                serverUrl = originalUrl.Remove(originalUrl.LastIndexOf(absoluteUrl));
            }
            else { serverUrl = originalUrl; }

            if (serverUrl.EndsWith("/") == false) { serverUrl += "/"; }

            return serverUrl;
        }
        public static void SolrFullImport()
        {
            String _SolrURL = ConfigurationMapped.Instance.ATSSolrUrl;
            if (!String.IsNullOrEmpty(_SolrURL))
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(_SolrURL + "/dataimport?command=full-import&optimize=true");
                request.Method = "GET";
                String test = String.Empty;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {

                }
            }

        }
        public static Dictionary<int, string> FullLocationList()
        {
            String[] Fields = new String[] { "FullLocation" };
            IDictionary<string, GroupedResults<ATS.BusinessEntity.SolrEntity.SolrSearchFields>> GroupResult = SolrBL.SolrResultMaker.GroupResult(_solrConnection, SolrQuery.All, Fields, 1);
            int index = 0;

            Dictionary<int, string> FLocationDropDown = new Dictionary<int, string>();
            bool hasData = false;
            foreach (SolrNet.Group<SolrEntity.SolrSearchFields> grp in GroupResult["FullLocation"].Groups)
            {
                FLocationDropDown.Add(index++, grp.GroupValue);
                hasData = true;
            }
            if (!hasData)
            {
                FLocationDropDown.Add(0, "No Data");
            }
            return FLocationDropDown;
        }


        public static void SolrEmployeeFullImport()
        {
            try
            {
                String _SolrURL = ConfigurationMapped.Instance.ATSEmployeeSolrUrl;
                if (!String.IsNullOrEmpty(_SolrURL))
                {
                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(_SolrURL + "/dataimport?command=full-import&optimize=true");
                    request.Method = "GET";
                    String test = String.Empty;
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }
        public static void SolrEmployeeDeltaImport()
        {
            try
            {
                //String _SolrURL = ConfigurationMapped.Instance.ATSEmployeeSolrUrl;
                //if (!String.IsNullOrEmpty(_SolrURL))
                //{
                //    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(_SolrURL + "/dataimport?command=delta-import&optimize=true");
                //    request.Method = "GET";
                //    String test = String.Empty;
                //    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                //    {

                //    }
                //}
                SolrEmployeeFullImport();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
        }
        public static void SolrDeltaImport()
        {
            String _SolrURL = ConfigurationMapped.Instance.ATSSolrUrl;
            if (!String.IsNullOrEmpty(_SolrURL))
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(_SolrURL + "/dataimport?command=delta-import&optimize=true");
                request.Method = "GET";
                String test = String.Empty;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {

                }
            }
        }
        public static string UseGuidAsJavaScriptId(String id)
        {
            if (id != null)
                return id.Replace("]", string.Empty).Replace("[", string.Empty).ToUpper();
            else
                return String.Empty;
        }

        public static bool CandidateHasProfile(Guid UserId, Guid ClientId)
        {
            BEClient.Profile ObjProfile = null;
            try
            {
                BLClient.ProfileAction objprofileaction = new BLClient.ProfileAction(ClientId);
                ObjProfile = objprofileaction.GetLastUpdatedprofileByUserId(UserId);
            }
            catch (Exception)
            {
                throw;
            }
            return ObjProfile != null;

        }

        public static string GetGuidInStringFormat(Guid myGuid)
        {
            return myGuid.ToString().Replace('-', '_').ToUpper();
        }
        public static bool ComparePageMode(List<BEClient.ATSPermissionType> permissionType, BEClient.ATSPermissionType comparetype, BEClient.PageMode objPageMode)
        {
            bool allow = false;
            if (permissionType == null)
                return false;
            switch (comparetype)
            {
                case BEClient.ATSPermissionType.Create:
                    if (permissionType.Contains(comparetype) && objPageMode == BEClient.PageMode.Create)
                        allow = true;
                    break;
                case BEClient.ATSPermissionType.Modify:
                    if (permissionType.Contains(comparetype) && objPageMode == BEClient.PageMode.Update)
                        allow = true;
                    break;
                case BEClient.ATSPermissionType.Delete:
                    if (permissionType.Contains(comparetype) && objPageMode == BEClient.PageMode.Update)
                        allow = true;
                    break;
                case BEClient.ATSPermissionType.View:
                    if (permissionType.Contains(comparetype) && objPageMode == BEClient.PageMode.View)
                        allow = true;
                    break;

            }
            return allow;
        }
        public static BEClient.PageMode GetPageMode(List<BEClient.ATSPermissionType> permissionType, BEClient.PageMode objPageMode)
        {
            BEClient.PageMode _pageMode = BEClient.PageMode.View;
            try
            {
                if (permissionType.Contains(BEClient.ATSPermissionType.Create) && objPageMode == BEClient.PageMode.Create)
                {
                    return BEClient.PageMode.Create;
                }
                else if (permissionType.Contains(BEClient.ATSPermissionType.Modify) && objPageMode == BEClient.PageMode.Update)
                {
                    return BEClient.PageMode.Update;
                }
                //else if (permissionType.Contains(BEClient.ATSPermissionType.View) && objPageMode == BEClient.PageMode.View)
                //{
                //    return BEClient.PageMode.View;
                //}
            }
            catch
            {
                _pageMode = BEClient.PageMode.View;
            }
            return _pageMode;
        }
        #region image type validation
        public static bool ValidateImageType(String FileName)
        {
            try
            {
                switch (Path.GetExtension(FileName).ToLower())
                {
                    case ".jpg":
                    case ".png":
                    case ".gif":
                    case ".jpeg":
                        return true;
                    default:
                        throw new Exception("Invalid Image Type");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool ValidateDocumentType(String FileName)
        {
            try
            {
                switch (Path.GetExtension(FileName).ToLower())
                {
                    case ".doc":
                    case ".docx":
                    case ".pdf":
                        return true;
                    default:
                        throw new Exception("Invalid Document Type");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool ValidateLogo(String FileName)
        {
            try
            {
                switch (Path.GetExtension(FileName).ToLower())
                {
                    case ".jpg":
                    case ".jpeg":
                    case ".gif":
                    case ".png":
                    case ".bmp":
                        return true;
                    default:
                        throw new Exception("Invalid Image Type");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static bool ValidateOfferAttachments(String FileName)
        {
            try
            {
                switch (Path.GetExtension(FileName).ToLower())
                {
                    case ".doc":
                    case ".docx":
                    case ".pdf":
                    case ".csv":
                    case ".xls":
                    case ".xlsx":
                    case ".txt":
                        return true;
                    default:
                        throw new Exception("Invalid Document Type");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion


        public static string ValidateFile(System.Web.HttpPostedFileBase documentFile, out string _oldFileName, out string _newFileName, out string _serverFilePath)
        {
            try
            {
                _oldFileName = String.Empty;
                _newFileName = String.Empty;
                _serverFilePath = String.Empty;

                string _extension = Path.GetExtension(documentFile.FileName);
                _oldFileName = Path.GetFileName(documentFile.FileName);
                _newFileName = Common.CommonFunctions.GetGuidInStringFormat(Guid.NewGuid()) + _extension;
                _serverFilePath = Path.Combine(Common.Constants.STR_RESUME_PATH, _newFileName);
                string _resumePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(Common.Constants.STR_RESUME_PATH), _newFileName);

                return _resumePath;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public static bool UploadDocuments(BEClient.Profile profile, System.Web.HttpPostedFileBase documentFile, out string _oldFileName, out string _newFileName, out string _serverFilePath, out Guid _DocumentId, bool isCoverLetter = false, bool isSaveInDB = true)
        {
            bool _isFileUploaded = false;
            _oldFileName = String.Empty;
            _newFileName = String.Empty;
            _serverFilePath = String.Empty;
            _DocumentId = Guid.Empty;
            Guid _profileid = profile.ProfileId;
            try
            {
                if (documentFile != null && documentFile.ContentLength > 0)
                {
                    string _resumePath = ValidateFile(documentFile, out _oldFileName, out _newFileName, out _serverFilePath);

                    #region Create new directory
                    CreateDirectory();
                    #endregion
                    if (isSaveInDB)
                    {
                        BEClient.ATSResume _objATSResume = new BEClient.ATSResume();
                        _objATSResume.UserId = profile.UserId;
                        _objATSResume.ProfileId = _profileid;
                        _objATSResume.UploadedFileName = _oldFileName;
                        _objATSResume.NewFileName = _newFileName;
                        _objATSResume.Path = _serverFilePath;
                        _objATSResume.DocumentTypeId = profile.DocumentTypeId;
                        _objATSResume.CandidateDescription = profile.CandidateDescription;
                        CurrentSession.Instance.VerifiedUser.tempResume = _objATSResume;
                    }
                    SaveFile(documentFile, _resumePath);
                    if (!System.IO.File.Exists(_resumePath))
                        throw new Exception("File not Upload on Path " + _resumePath);
                    else
                        _isFileUploaded = true;
                }
                //ViewBag.Message = "Profile Save Successfully";
            }
            catch
            {

                throw;
            }
            return _isFileUploaded;
        }


        public static bool UploadResume(BEClient.Profile profile, System.Web.HttpPostedFileBase documentFile, out string _oldFileName, out string _newFileName, out string _serverFilePath, out Guid _DocumentId, bool isCoverLetter = false, bool isSaveInDB = true)
        {
            bool _isFileUploaded = false;
            _oldFileName = String.Empty;
            _newFileName = String.Empty;
            _serverFilePath = String.Empty;
            _DocumentId = Guid.Empty;
            Guid _profileid = profile.ProfileId;
            try
            {
                if (documentFile != null && documentFile.ContentLength > 0)
                {

                    //string _resumePath = ValidateDocument(documentFile, out _oldFileName, out _newFileName, out _serverFilePath);
                    string _resumePath = ValidateDocument(documentFile, out _oldFileName, out _newFileName, out _serverFilePath, profile.ExtensionTypes);

                    #region Create new directory

                    CreateDirectory();

                    #endregion
                    if (isSaveInDB)
                    {
                        BEClient.ATSResume _objATSResume = new BEClient.ATSResume();
                        _objATSResume.UserId = profile.UserId;
                        _objATSResume.ProfileId = _profileid;
                        _objATSResume.UploadedFileName = _oldFileName;
                        _objATSResume.NewFileName = _newFileName;
                        _objATSResume.Path = _serverFilePath;
                        _objATSResume.DocumentTypeId = profile.DocumentTypeId;
                        _objATSResume.CandidateDescription = profile.CandidateDescription;
                        CurrentSession.Instance.VerifiedUser.tempResume = _objATSResume;
                    }
                    SaveFile(documentFile, _resumePath);

                    if (!System.IO.File.Exists(_resumePath))
                        throw new Exception("File not Upload on Path " + _resumePath);
                    else
                        _isFileUploaded = true;
                }
                //ViewBag.Message = "Profile Save Successfully";
            }
            catch
            {

                throw;
            }
            return _isFileUploaded;
        }

        public static bool UploadResume(BEClient.Profile profile, BEClient.User objUser, out string _oldFileName, out string _newFileName, out string _serverFilePath, out Guid _DocumentId, bool isCoverLetter = false, bool isSaveInDB = true)
        {
            bool _isFileUploaded = false;
            _oldFileName = String.Empty;
            _newFileName = String.Empty;
            _serverFilePath = String.Empty;
            _DocumentId = Guid.Empty;
            Guid _profileid = profile.ProfileId;
            try
            {
                if (objUser.DropBoxFileUrl != null)
                {

                    string _resumePath = ValidateDocument(objUser, out _oldFileName, out _newFileName, out _serverFilePath);

                    #region Create new directory

                    CreateDirectory();

                    #endregion
                    if (isSaveInDB)
                    {
                        BEClient.ATSResume _objATSResume = new BEClient.ATSResume();
                        _objATSResume.UserId = profile.UserId;
                        _objATSResume.ProfileId = _profileid;
                        _objATSResume.UploadedFileName = _oldFileName;
                        _objATSResume.NewFileName = _newFileName;
                        _objATSResume.Path = _serverFilePath;

                        CurrentSession.Instance.VerifiedUser.tempResume = _objATSResume;
                    }

                    try
                    {
                        //HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(objUser.DropBoxFileUrl);
                        //request.Method = "GET"; String test = String.Empty;
                        //using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                        //{
                        //    Stream dataStream = response.GetResponseStream();
                        //    StreamReader reader = new StreamReader(dataStream);
                        //    test = reader.ReadToEnd();
                        //    reader.Close();
                        //    dataStream.Close();
                        //}
                        System.Net.WebClient _WebClient = new System.Net.WebClient();
                        _WebClient.DownloadFile(objUser.DropBoxFileUrl, _resumePath);
                    }
                    catch (Exception Ex)
                    {
                        throw new Exception("File not Upload on Path " + _resumePath);
                    }


                    if (!System.IO.File.Exists(_resumePath))
                        throw new Exception("File not Upload on Path " + _resumePath);
                    else
                        _isFileUploaded = true;
                }
                //ViewBag.Message = "Profile Save Successfully";
            }
            catch
            {

                throw;
            }
            return _isFileUploaded;
        }

        public static string ValidateDocument(System.Web.HttpPostedFileBase documentFile, out string _oldFileName, out string _newFileName, out string _serverFilePath,string ext = null)
        {
            try
            {
                _oldFileName = String.Empty;
                _newFileName = String.Empty;
                _serverFilePath = String.Empty;

                //Vlidate Document from server side
                if (ext == null)
                {
                    //Common.CommonFunctions.ValidateDocumentType(documentFile.FileName);
                    throw new Exception( "Please check this function for ext");
                }
                else
                Common.CommonFunctions.ValidateDocumentTypeExtensions(documentFile.FileName,ext);

                //Get Extension of uploaded File
                string _extension = Path.GetExtension(documentFile.FileName);
                _oldFileName = Path.GetFileName(documentFile.FileName);
                _newFileName = Common.CommonFunctions.GetGuidInStringFormat(Guid.NewGuid()) + _extension;
                _serverFilePath = Path.Combine(Common.Constants.STR_RESUME_PATH, _newFileName);
                string _resumePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(Common.Constants.STR_RESUME_PATH), _newFileName);

                return _resumePath;
            }
            catch (Exception)
            {

                throw;
            }
        }



        public static string ValidateOfferAttachments(System.Web.HttpPostedFileBase documentFile, out string _oldFileName, out string _newFileName, out string _serverFilePath)
        {
            try
            {
                _oldFileName = String.Empty;
                _newFileName = String.Empty;
                _serverFilePath = String.Empty;

                //Vlidate Document from server side
                Common.CommonFunctions.ValidateOfferAttachments(documentFile.FileName);

                //Get Extension of uploaded File
                string _extension = Path.GetExtension(documentFile.FileName);
                _oldFileName = Path.GetFileName(documentFile.FileName);
                _newFileName = Common.CommonFunctions.GetGuidInStringFormat(Guid.NewGuid()) + _extension;
                _serverFilePath = Path.Combine(Common.Constants.STR_RESUME_PATH, _newFileName);
                string _resumePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(Common.Constants.STR_RESUME_PATH), _newFileName);

                return _resumePath;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public static string ValidateDocument(BEClient.User objUser, out string _oldFileName, out string _newFileName, out string _serverFilePath)
        {
            try
            {
                _oldFileName = String.Empty;
                _newFileName = String.Empty;
                _serverFilePath = String.Empty;

                //Vlidate Document from server side
                Common.CommonFunctions.ValidateDocumentType(objUser.DropBoxFileName);

                //Get Extension of uploaded File
                string _extension = Path.GetExtension(objUser.DropBoxFileName);
                _oldFileName = objUser.DropBoxFileName;
                _newFileName = Common.CommonFunctions.GetGuidInStringFormat(Guid.NewGuid()) + _extension;
                _serverFilePath = Path.Combine(Common.Constants.STR_RESUME_PATH, _newFileName);
                string _resumePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(Common.Constants.STR_RESUME_PATH), _newFileName);

                return _resumePath;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void CreateDirectory()
        {
            try
            {
                //Create directory of not exist
                if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(Common.Constants.STR_RESUME_DIR_PATH)))
                {
                    Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(Common.Constants.STR_RESUME_DIR_PATH));
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void SaveFile(HttpPostedFileBase documentFile, string filePath)
        {
            try
            {
                documentFile.SaveAs(filePath);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static BEClient.CandidateProfile GetCandidateFullProfile(Guid ApplicationId, Guid ClientId)
        {
            BLClient.ApplicationAction _objApplicationAction = new BLClient.ApplicationAction(ClientId);
            BEClient.Application _objApplication = _objApplicationAction.GetApplicationByApplicationId(ApplicationId);
            BEClient.CandidateProfile ObjCandidatProfile = null;
            if (_objApplication != null)
            {
                BEClient.ATSResume _objATSResume = new BEClient.ATSResume();
                BLClient.ATSResumeAction _objATSResumeAction = new BLClient.ATSResumeAction(ClientId);
                _objATSResume = _objATSResumeAction.GetRecordByRecorId(_objApplication.ATSResumeId);
                ObjCandidatProfile = GetFullCandidateProfile(_objApplication.UserId, _objATSResume.ProfileId, ClientId);
            }
            return ObjCandidatProfile;
        }
        public static BEClient.CandidateProfile GetFullCandidateProfile(Guid UserId, Guid ProfileId, Guid ClientId)
        {
            BEClient.CandidateProfile ObjCandidatProfile = null;
            BLClient.ProfileAction _objProfileAction = new BLClient.ProfileAction(ClientId);
            ObjCandidatProfile = _objProfileAction.GetCanidateFullProfileByUserIdAndProfileId(UserId, ProfileId);
            //CandidateProfileDropdownlist();
            //ViewBag.EmployeeView = true;
            return ObjCandidatProfile;
        }

        public static string NonAuthoriseduser()
        {
            JsonModels actionStatus = null;
            actionStatus = CommonFunctions.GetJsonContentObj(false, true, string.Empty, "You don't have Permission");

            /*Convert to json string*/
            string jsonModels = JsonConvert.SerializeObject(actionStatus);
            string keyMsg = HelperLibrary.Encryption.EncryptionAlgo.EncryptionData(jsonModels);
            /*Redirect to List Page*/
            return keyMsg;

        }
        #region Profile Creation For Anonymous

        public static BEClient.CandidateProfile GenerateProfile(BEClient.Profile profile, HttpPostedFileBase documentFile, bool allowParse)
        {
            BEClient.CandidateProfile _objCandidateProfile = new BEClient.CandidateProfile();
            BLClient.UserDetailsAction objUserDetailsAction = new BLClient.UserDetailsAction(profile.ClientId);
            _objCandidateProfile.objUserDetails = objUserDetailsAction.GetUserDetailsByUserId(profile.UserId);

            try
            {
                string _serverFilePath = string.Empty;
                string _newFileName = string.Empty;
                string _oldFileName = string.Empty;
                Guid _DocumentId = Guid.Empty;
                bool _isFileUploaded = false;
                if (documentFile != null)
                {
                    BLClient.ATSResumeAction _objAtsResumeAction = new BLClient.ATSResumeAction(profile.ClientId);

                    if (!profile.ProfileId.Equals(Guid.Empty))
                    {
                        _isFileUploaded = UploadResume(profile, documentFile, out _oldFileName, out _newFileName, out _serverFilePath, out _DocumentId);
                    }
                    else
                    {
                        _isFileUploaded = UploadResume(profile, documentFile, out _oldFileName, out _newFileName, out _serverFilePath, out _DocumentId, false, false);
                    }
                }

                if (!_isFileUploaded && allowParse)
                {
                    throw new Exception("File Not Uploaded");
                }


                Guid _profileId = Guid.Empty;
                if (!profile.ProfileId.Equals(Guid.Empty))
                {
                    _profileId = profile.ProfileId;
                }
                else
                {
                    Guid.TryParse(profile != null ? profile.ProfileId.ToString() : string.Empty, out _profileId);
                }

                BEClient.ATSResume _objATSResume = null;

                if (_isFileUploaded)
                {
                    //Parse Resume
                    RChilliParserHelper.SovrenParseResume _parseResume = new RChilliParserHelper.SovrenParseResume();
                    _parseResume.ServiceUrl = Common.ConfigurationMapped.Instance.RChilliServiceUrl;
                    _parseResume.ParseResume(System.Web.HttpContext.Current.Server.MapPath(_serverFilePath), Common.ConfigurationMapped.Instance.RChilliUserKey, Common.ConfigurationMapped.Instance.RChilliVersion, Common.ConfigurationMapped.Instance.RChilliSubUserId, false);
                    //End Parsing

                    /*Check is contact Exist or not*/

                    BEClient.UserDetails _objContact = Common.RChilliObjMapping.GetContact(_parseResume, profile.UserId);
                    if (_objCandidateProfile.objUserDetails == null)
                    {
                        _objCandidateProfile.objUserDetails = _objContact;
                    }
                    else
                    {
                        _objCandidateProfile.objUserDetails.FirstName = String.IsNullOrEmpty(_objCandidateProfile.objUserDetails.FirstName) ? _objContact.FirstName : _objCandidateProfile.objUserDetails.FirstName;
                        _objCandidateProfile.objUserDetails.MiddleName = String.IsNullOrEmpty(_objCandidateProfile.objUserDetails.MiddleName) ? _objContact.MiddleName : _objCandidateProfile.objUserDetails.MiddleName;
                        _objCandidateProfile.objUserDetails.LastName = String.IsNullOrEmpty(_objCandidateProfile.objUserDetails.LastName) ? _objContact.LastName : _objCandidateProfile.objUserDetails.LastName;
                        _objCandidateProfile.objUserDetails.Affix = String.IsNullOrEmpty(_objCandidateProfile.objUserDetails.Affix) ? _objContact.Affix : _objCandidateProfile.objUserDetails.Affix;
                        _objCandidateProfile.objUserDetails.Zip = String.IsNullOrEmpty(_objCandidateProfile.objUserDetails.Zip) ? _objContact.Zip : _objCandidateProfile.objUserDetails.Zip;
                        _objCandidateProfile.objUserDetails.Address = String.IsNullOrEmpty(_objCandidateProfile.objUserDetails.Address) ? _objContact.Address : _objCandidateProfile.objUserDetails.Address;
                        _objCandidateProfile.objUserDetails.City = String.IsNullOrEmpty(_objCandidateProfile.objUserDetails.City) ? _objContact.City : _objCandidateProfile.objUserDetails.City;
                        _objCandidateProfile.objUserDetails.State = String.IsNullOrEmpty(_objCandidateProfile.objUserDetails.State) ? _objContact.State : _objCandidateProfile.objUserDetails.State;
                        _objCandidateProfile.objUserDetails.BusinessPhoneNo = String.IsNullOrEmpty(_objCandidateProfile.objUserDetails.BusinessPhoneNo) ? _objContact.MobilePhone : _objCandidateProfile.objUserDetails.BusinessPhoneNo;
                        _objCandidateProfile.objUserDetails.MobilePhone = String.IsNullOrEmpty(_objCandidateProfile.objUserDetails.MobilePhone) ? _objContact.MobilePhone : _objCandidateProfile.objUserDetails.MobilePhone;
                        _objCandidateProfile.objUserDetails.HomePhone = String.IsNullOrEmpty(_objCandidateProfile.objUserDetails.HomePhone) ? _objContact.HomePhone : _objCandidateProfile.objUserDetails.HomePhone;
                        _objCandidateProfile.objUserDetails.WorkEmail = String.IsNullOrEmpty(_objCandidateProfile.objUserDetails.WorkEmail) ? _objContact.WorkEmail : _objCandidateProfile.objUserDetails.WorkEmail;
                        _objCandidateProfile.objUserDetails.HomeEmail = String.IsNullOrEmpty(_objCandidateProfile.objUserDetails.HomeEmail) ? _objContact.HomeEmail : _objCandidateProfile.objUserDetails.HomeEmail;
                        _objCandidateProfile.objUserDetails.Pager = String.IsNullOrEmpty(_objCandidateProfile.objUserDetails.Pager) ? _objContact.Pager : _objCandidateProfile.objUserDetails.Pager;
                        _objCandidateProfile.objUserDetails.Fax = String.IsNullOrEmpty(_objCandidateProfile.objUserDetails.Fax) ? _objContact.Fax : _objCandidateProfile.objUserDetails.Fax;
                        _objCandidateProfile.objUserDetails.Website = String.IsNullOrEmpty(_objCandidateProfile.objUserDetails.Website) ? _objContact.Website : _objCandidateProfile.objUserDetails.Website;

                    }

                    //    /*End Checking*/


                    //Create New Availability for New profile
                    _objCandidateProfile.ObjAvailability = new BEClient.Availability();
                    _objCandidateProfile.ObjAvailability.EligibleToWorkInUS = true;
                    _objCandidateProfile.ObjAvailability.HowOld = true;
                    //Executive Summary
                    //BEClient.ExecutiveSummary ObjExecutiveSummary1 = Common.RChilliObjMapping.GetExecutiveSummary(_parseResume, profile.UserId, _profileId);
                    _objCandidateProfile.ObjExecutiveSummary = new BEClient.ExecutiveSummary();
                    _objCandidateProfile.ObjExecutiveSummary = Common.RChilliObjMapping.GetExecutiveSummary(_parseResume, profile.UserId, _profileId);

                    //Objective
                    //BEClient.Objective objobjective1 = Common.RChilliObjMapping.GetObjective(_parseResume, profile.UserId, _profileId);
                    _objCandidateProfile.ObjObjective = new BEClient.Objective();
                    _objCandidateProfile.ObjObjective = Common.RChilliObjMapping.GetObjective(_parseResume, profile.UserId, _profileId);

                    //Language
                    List<BEClient.Languages> _objLanguages = Common.RChilliObjMapping.GetLanguages(_parseResume, profile.UserId, _profileId);
                    _objCandidateProfile.ObjContactLanguages = _objLanguages;

                    //Speaking Event History
                    List<BEClient.SpeakingEventHistory> _objSpeakingEventHistory = Common.RChilliObjMapping.GetSpeakingEventHistory(_parseResume, profile.UserId, _profileId);
                    _objCandidateProfile.ObjContactSpeakingEventHistory = _objSpeakingEventHistory;

                    List<BEClient.Associations> _objAssociations = Common.RChilliObjMapping.GetAssociations(_parseResume, profile.UserId, _profileId);
                    _objCandidateProfile.ObjContactAssociations = _objAssociations;

                    List<BEClient.Achievement> _objAchievement = Common.RChilliObjMapping.GetAchievement(_parseResume, profile.UserId, _profileId);
                    _objCandidateProfile.ObjContactAchievement = _objAchievement;

                    List<BEClient.EmploymentHistory> _objEmploymentHistory = Common.RChilliObjMapping.GetEmploymentHistory(_parseResume, profile.UserId, _profileId);

                    List<BEClient.EmploymentHistory> _newobjEmploymentHistory = (from v in _objEmploymentHistory
                                                                                 select new BEClient.EmploymentHistory
                                                                                 {
                                                                                     CompanyName = v.CompanyName,
                                                                                     MayWeContact = v.MayWeContact,
                                                                                     SupervisorName = v.SupervisorName,
                                                                                     Telephone = v.Telephone,
                                                                                     Address = v.Address,
                                                                                     City = v.City,
                                                                                     State = v.State,
                                                                                     Zip = v.Zip,
                                                                                     StartigPosition = v.StartigPosition,
                                                                                     MostRecentPosition = v.MostRecentPosition,
                                                                                     StartingPay = v.StartingPay,
                                                                                     EndingPay = v.EndingPay,
                                                                                     JobCategory = v.JobCategory,
                                                                                     Experience = v.Experience,
                                                                                     ReasonForLeaving = v.ReasonForLeaving,
                                                                                     DutiesAndResponsibilities = v.DutiesAndResponsibilities,
                                                                                     StartMonth = v.StartDate.Month,
                                                                                     StartYear = v.StartDate.Year,
                                                                                     EndMonth = v.EndDate.Month,
                                                                                     EndYear = v.EndDate.Year
                                                                                 }).ToList();

                    _objCandidateProfile.ObjContactEmployments = _newobjEmploymentHistory;


                    List<BEClient.EducationHistory> _objEducationHistory = Common.RChilliObjMapping.GetEducationHistory(_parseResume, profile.UserId, _profileId);
                    _objCandidateProfile.ObjContactEducations = _objEducationHistory;

                    List<BEClient.LicenceAndCertifications> _objLicenceAndCertifications = Common.RChilliObjMapping.GetLicenceAndCertifications(_parseResume, profile.UserId, _profileId);
                    _objCandidateProfile.ObjContactLicenceAndCertifications = _objLicenceAndCertifications;

                    List<BEClient.PublicationHistory> _objPublicationHistory = Common.RChilliObjMapping.GetPublicationHistory(_parseResume, profile.UserId, _profileId);
                    _objCandidateProfile.ObjContactPublicationHistory = _objPublicationHistory;



                    if (_profileId.Equals(Guid.Empty))
                    {
                        #region Creat new profile and ATS resume
                        //Create new Profile and Assign value to _profileId 
                        BLClient.ProfileAction _objProfileAction = new BLClient.ProfileAction(profile.ClientId);
                        _profileId = _objProfileAction.AddProfile(profile);
                        _objATSResume = new BEClient.ATSResume();
                        _objATSResume.ProfileId = _profileId;
                        _objATSResume.UserId = profile.UserId;
                        _objATSResume.UploadedFileName = _oldFileName;
                        _objATSResume.NewFileName = _newFileName;
                        _objATSResume.Path = _serverFilePath;
                        _objATSResume.CandidateDescription = profile.CandidateDescription;
                        _objATSResume.DocumentTypeId = profile.DocumentTypeId;
                        _objCandidateProfile.objATSResume = _objATSResume;
                        BLClient.CandidateProfileAction _objCandidateProfileAction = new BLClient.CandidateProfileAction(profile.ClientId);
                        bool result = _objCandidateProfileAction.AddCandidateProfile(_objCandidateProfile, _profileId, profile.UserId);
                    }
                    else
                    {
                        //call Update  method
                        #region Delete Existing Attached file
                        BLClient.ATSResumeAction _objATSResumeAction = new BLClient.ATSResumeAction(ATS.WebUi.Common.CurrentSession.Instance.VerifiedClient.ClientId);
                        BEClient.ATSResume _objATSResumeOld = new BEClient.ATSResume();
                        string Existingfilepath = string.Empty;

                        _objATSResumeOld = _objATSResumeAction.GetATSResumeByUserAndProfile(profile.UserId, _profileId);
                        Guid _ATSResumeId = Guid.Empty;
                        //It will delete existing file if profile update
                        if (_objATSResumeOld != null)
                        {
                            Existingfilepath = Path.Combine(Common.Constants.STR_RESUME_PATH, _objATSResumeOld.NewFileName);
                            if (!string.IsNullOrEmpty(Existingfilepath))
                                System.IO.File.Delete(System.Web.HttpContext.Current.Server.MapPath(Existingfilepath));

                            //Assign existing id 
                            _ATSResumeId = _objATSResumeOld.ATSResumeId;
                        }



                        #endregion

                        BLClient.ProfileAction _objProfileAction = new BLClient.ProfileAction(profile.ClientId);
                        _objCandidateProfile.objProfile = new BEClient.Profile();
                        _objCandidateProfile.objProfile = _objProfileAction.GetProfileByProfileId(_profileId);
                        _objATSResume = new BEClient.ATSResume();
                        _objATSResume.ATSResumeId = _ATSResumeId;

                        _objATSResume.ProfileId = _profileId;
                        _objATSResume.UserId = profile.UserId;
                        _objATSResume.UploadedFileName = _oldFileName;
                        _objATSResume.NewFileName = _newFileName;
                        _objATSResume.Path = _serverFilePath;
                        _objCandidateProfile.objATSResume = _objATSResume;
                        _objProfileAction.UpdateCandidateFullProfile(_objCandidateProfile);


                    }

                        #endregion
                }

                //ADD ATS resume in Model
                BLClient.ProfileAction _ProfileAction = new BLClient.ProfileAction(profile.ClientId);
                _objCandidateProfile = _ProfileAction.GetCanidateFullProfileByUserIdAndProfileId(profile.UserId, _profileId);

                return _objCandidateProfile;
            }
            catch
            {
                throw;
            }

        }

        public static BEClient.CandidateProfile GenerateProfile(BEClient.Profile profile, BEClient.User objUser, bool allowParse)
        {
            BEClient.CandidateProfile _objCandidateProfile = new BEClient.CandidateProfile();
            BLClient.UserDetailsAction objUserDetailsAction = new BLClient.UserDetailsAction(profile.ClientId);
            _objCandidateProfile.objUserDetails = objUserDetailsAction.GetUserDetailsByUserId(profile.UserId);

            try
            {
                string _serverFilePath = string.Empty;
                string _newFileName = string.Empty;
                string _oldFileName = string.Empty;
                Guid _DocumentId = Guid.Empty;
                bool _isFileUploaded = false;
                if (objUser.DropBoxFileUrl != null)
                {
                    BLClient.ATSResumeAction _objAtsResumeAction = new BLClient.ATSResumeAction(profile.ClientId);
                    if (!profile.ProfileId.Equals(Guid.Empty))
                    {
                        _isFileUploaded = UploadResume(profile, objUser, out _oldFileName, out _newFileName, out _serverFilePath, out _DocumentId);
                    }
                    else
                    {
                        _isFileUploaded = UploadResume(profile, objUser, out _oldFileName, out _newFileName, out _serverFilePath, out _DocumentId, false, false);
                    }
                }

                if (!_isFileUploaded && allowParse)
                {
                    throw new Exception("File Not Uploaded");
                }


                Guid _profileId = Guid.Empty;
                if (!profile.ProfileId.Equals(Guid.Empty))
                {
                    _profileId = profile.ProfileId;
                }
                else
                {
                    Guid.TryParse(profile != null ? profile.ProfileId.ToString() : string.Empty, out _profileId);
                }

                BEClient.ATSResume _objATSResume = null;

                if (_isFileUploaded)
                {
                    //Parse Resume
                    RChilliParserHelper.SovrenParseResume _parseResume = new RChilliParserHelper.SovrenParseResume();
                    _parseResume.ServiceUrl = Common.ConfigurationMapped.Instance.RChilliServiceUrl;
                    _parseResume.ParseResume(System.Web.HttpContext.Current.Server.MapPath(_serverFilePath), Common.ConfigurationMapped.Instance.RChilliUserKey, Common.ConfigurationMapped.Instance.RChilliVersion, Common.ConfigurationMapped.Instance.RChilliSubUserId, false);
                    //End Parsing

                    /*Check is contact Exist or not*/

                    BEClient.UserDetails _objContact = Common.RChilliObjMapping.GetContact(_parseResume, profile.UserId);
                    if (_objCandidateProfile.objUserDetails == null)
                    {
                        _objCandidateProfile.objUserDetails = _objContact;
                    }
                    else
                    {
                        _objCandidateProfile.objUserDetails.FirstName = String.IsNullOrEmpty(_objCandidateProfile.objUserDetails.FirstName) ? _objContact.FirstName : _objCandidateProfile.objUserDetails.FirstName;
                        _objCandidateProfile.objUserDetails.MiddleName = String.IsNullOrEmpty(_objCandidateProfile.objUserDetails.MiddleName) ? _objContact.MiddleName : _objCandidateProfile.objUserDetails.MiddleName;
                        _objCandidateProfile.objUserDetails.LastName = String.IsNullOrEmpty(_objCandidateProfile.objUserDetails.LastName) ? _objContact.LastName : _objCandidateProfile.objUserDetails.LastName;
                        _objCandidateProfile.objUserDetails.Affix = String.IsNullOrEmpty(_objCandidateProfile.objUserDetails.Affix) ? _objContact.Affix : _objCandidateProfile.objUserDetails.Affix;
                        _objCandidateProfile.objUserDetails.Zip = String.IsNullOrEmpty(_objCandidateProfile.objUserDetails.Zip) ? _objContact.Zip : _objCandidateProfile.objUserDetails.Zip;
                        _objCandidateProfile.objUserDetails.Address = String.IsNullOrEmpty(_objCandidateProfile.objUserDetails.Address) ? _objContact.Address : _objCandidateProfile.objUserDetails.Address;
                        _objCandidateProfile.objUserDetails.City = String.IsNullOrEmpty(_objCandidateProfile.objUserDetails.City) ? _objContact.City : _objCandidateProfile.objUserDetails.City;
                        _objCandidateProfile.objUserDetails.State = String.IsNullOrEmpty(_objCandidateProfile.objUserDetails.State) ? _objContact.State : _objCandidateProfile.objUserDetails.State;
                        _objCandidateProfile.objUserDetails.BusinessPhoneNo = String.IsNullOrEmpty(_objCandidateProfile.objUserDetails.BusinessPhoneNo) ? _objContact.MobilePhone : _objCandidateProfile.objUserDetails.BusinessPhoneNo;
                        _objCandidateProfile.objUserDetails.MobilePhone = String.IsNullOrEmpty(_objCandidateProfile.objUserDetails.MobilePhone) ? _objContact.MobilePhone : _objCandidateProfile.objUserDetails.MobilePhone;
                        _objCandidateProfile.objUserDetails.HomePhone = String.IsNullOrEmpty(_objCandidateProfile.objUserDetails.HomePhone) ? _objContact.HomePhone : _objCandidateProfile.objUserDetails.HomePhone;
                        _objCandidateProfile.objUserDetails.WorkEmail = String.IsNullOrEmpty(_objCandidateProfile.objUserDetails.WorkEmail) ? _objContact.WorkEmail : _objCandidateProfile.objUserDetails.WorkEmail;
                        _objCandidateProfile.objUserDetails.HomeEmail = String.IsNullOrEmpty(_objCandidateProfile.objUserDetails.HomeEmail) ? _objContact.HomeEmail : _objCandidateProfile.objUserDetails.HomeEmail;
                        _objCandidateProfile.objUserDetails.Pager = String.IsNullOrEmpty(_objCandidateProfile.objUserDetails.Pager) ? _objContact.Pager : _objCandidateProfile.objUserDetails.Pager;
                        _objCandidateProfile.objUserDetails.Fax = String.IsNullOrEmpty(_objCandidateProfile.objUserDetails.Fax) ? _objContact.Fax : _objCandidateProfile.objUserDetails.Fax;
                        _objCandidateProfile.objUserDetails.Website = String.IsNullOrEmpty(_objCandidateProfile.objUserDetails.Website) ? _objContact.Website : _objCandidateProfile.objUserDetails.Website;

                    }

                    //    /*End Checking*/


                    //Create New Availability for New profile
                    _objCandidateProfile.ObjAvailability = new BEClient.Availability();
                    _objCandidateProfile.ObjAvailability.EligibleToWorkInUS = true;
                    _objCandidateProfile.ObjAvailability.HowOld = true;
                    //Executive Summary
                    //BEClient.ExecutiveSummary ObjExecutiveSummary1 = Common.RChilliObjMapping.GetExecutiveSummary(_parseResume, profile.UserId, _profileId);
                    _objCandidateProfile.ObjExecutiveSummary = new BEClient.ExecutiveSummary();
                    _objCandidateProfile.ObjExecutiveSummary = Common.RChilliObjMapping.GetExecutiveSummary(_parseResume, profile.UserId, _profileId);

                    //Objective
                    //BEClient.Objective objobjective1 = Common.RChilliObjMapping.GetObjective(_parseResume, profile.UserId, _profileId);
                    _objCandidateProfile.ObjObjective = new BEClient.Objective();
                    _objCandidateProfile.ObjObjective = Common.RChilliObjMapping.GetObjective(_parseResume, profile.UserId, _profileId);

                    //Language
                    List<BEClient.Languages> _objLanguages = Common.RChilliObjMapping.GetLanguages(_parseResume, profile.UserId, _profileId);
                    _objCandidateProfile.ObjContactLanguages = _objLanguages;

                    //Speaking Event History
                    List<BEClient.SpeakingEventHistory> _objSpeakingEventHistory = Common.RChilliObjMapping.GetSpeakingEventHistory(_parseResume, profile.UserId, _profileId);
                    _objCandidateProfile.ObjContactSpeakingEventHistory = _objSpeakingEventHistory;

                    List<BEClient.Associations> _objAssociations = Common.RChilliObjMapping.GetAssociations(_parseResume, profile.UserId, _profileId);
                    _objCandidateProfile.ObjContactAssociations = _objAssociations;

                    List<BEClient.Achievement> _objAchievement = Common.RChilliObjMapping.GetAchievement(_parseResume, profile.UserId, _profileId);
                    _objCandidateProfile.ObjContactAchievement = _objAchievement;

                    List<BEClient.EmploymentHistory> _objEmploymentHistory = Common.RChilliObjMapping.GetEmploymentHistory(_parseResume, profile.UserId, _profileId);

                    List<BEClient.EmploymentHistory> _newobjEmploymentHistory = (from v in _objEmploymentHistory
                                                                                 select new BEClient.EmploymentHistory
                                                                                 {
                                                                                     CompanyName = v.CompanyName,
                                                                                     MayWeContact = v.MayWeContact,
                                                                                     SupervisorName = v.SupervisorName,
                                                                                     Telephone = v.Telephone,
                                                                                     Address = v.Address,
                                                                                     City = v.City,
                                                                                     State = v.State,
                                                                                     Zip = v.Zip,
                                                                                     StartigPosition = v.StartigPosition,
                                                                                     MostRecentPosition = v.MostRecentPosition,
                                                                                     StartingPay = v.StartingPay,
                                                                                     EndingPay = v.EndingPay,
                                                                                     JobCategory = v.JobCategory,
                                                                                     Experience = v.Experience,
                                                                                     ReasonForLeaving = v.ReasonForLeaving,
                                                                                     DutiesAndResponsibilities = v.DutiesAndResponsibilities,
                                                                                     StartMonth = v.StartDate.Month,
                                                                                     StartYear = v.StartDate.Year,
                                                                                     EndMonth = v.EndDate.Month,
                                                                                     EndYear = v.EndDate.Year
                                                                                 }).ToList();

                    _objCandidateProfile.ObjContactEmployments = _newobjEmploymentHistory;


                    List<BEClient.EducationHistory> _objEducationHistory = Common.RChilliObjMapping.GetEducationHistory(_parseResume, profile.UserId, _profileId);
                    _objCandidateProfile.ObjContactEducations = _objEducationHistory;

                    List<BEClient.LicenceAndCertifications> _objLicenceAndCertifications = Common.RChilliObjMapping.GetLicenceAndCertifications(_parseResume, profile.UserId, _profileId);
                    _objCandidateProfile.ObjContactLicenceAndCertifications = _objLicenceAndCertifications;

                    List<BEClient.PublicationHistory> _objPublicationHistory = Common.RChilliObjMapping.GetPublicationHistory(_parseResume, profile.UserId, _profileId);
                    _objCandidateProfile.ObjContactPublicationHistory = _objPublicationHistory;



                    if (_profileId.Equals(Guid.Empty))
                    {
                        #region Creat new profile and ATS resume
                        //Create new Profile and Assign value to _profileId 
                        BLClient.ProfileAction _objProfileAction = new BLClient.ProfileAction(profile.ClientId);
                        _profileId = _objProfileAction.AddProfile(profile);
                        _objATSResume = new BEClient.ATSResume();
                        _objATSResume.ProfileId = _profileId;
                        _objATSResume.UserId = profile.UserId;
                        _objATSResume.UploadedFileName = _oldFileName;
                        _objATSResume.NewFileName = _newFileName;
                        _objATSResume.Path = _serverFilePath;

                        _objCandidateProfile.objATSResume = _objATSResume;
                        BLClient.CandidateProfileAction _objCandidateProfileAction = new BLClient.CandidateProfileAction(profile.ClientId);
                        bool result = _objCandidateProfileAction.AddCandidateProfile(_objCandidateProfile, _profileId, profile.UserId);
                    }
                    else
                    {
                        //call Update  method
                        #region Delete Existing Attached file
                        BLClient.ATSResumeAction _objATSResumeAction = new BLClient.ATSResumeAction(ATS.WebUi.Common.CurrentSession.Instance.VerifiedClient.ClientId);
                        BEClient.ATSResume _objATSResumeOld = new BEClient.ATSResume();
                        string Existingfilepath = string.Empty;

                        _objATSResumeOld = _objATSResumeAction.GetATSResumeByUserAndProfile(profile.UserId, _profileId);
                        Guid _ATSResumeId = Guid.Empty;
                        //It will delete existing file if profile update
                        if (_objATSResumeOld != null)
                        {
                            Existingfilepath = Path.Combine(Common.Constants.STR_RESUME_PATH, _objATSResumeOld.NewFileName);
                            if (!string.IsNullOrEmpty(Existingfilepath))
                                System.IO.File.Delete(System.Web.HttpContext.Current.Server.MapPath(Existingfilepath));

                            //Assign existing id 
                            _ATSResumeId = _objATSResumeOld.ATSResumeId;
                        }



                        #endregion

                        BLClient.ProfileAction _objProfileAction = new BLClient.ProfileAction(profile.ClientId);
                        _objCandidateProfile.objProfile = new BEClient.Profile();
                        _objCandidateProfile.objProfile = _objProfileAction.GetProfileByProfileId(_profileId);
                        _objATSResume = new BEClient.ATSResume();
                        _objATSResume.ATSResumeId = _ATSResumeId;

                        _objATSResume.ProfileId = _profileId;
                        _objATSResume.UserId = profile.UserId;
                        _objATSResume.UploadedFileName = _oldFileName;
                        _objATSResume.NewFileName = _newFileName;
                        _objATSResume.Path = _serverFilePath;
                        _objCandidateProfile.objATSResume = _objATSResume;
                        _objProfileAction.UpdateCandidateFullProfile(_objCandidateProfile);


                    }

                        #endregion
                }

                //ADD ATS resume in Model
                BLClient.ProfileAction _ProfileAction = new BLClient.ProfileAction(profile.ClientId);
                _objCandidateProfile = _ProfileAction.GetCanidateFullProfileByUserIdAndProfileId(profile.UserId, _profileId);

                return _objCandidateProfile;
            }
            catch
            {
                throw;
            }

        }


        #endregion

        #region Convert UTC to Local datetime
        public static DateTime ConvertUTCToLocalDate(DateTime utcDt)
        {
            // Creates a CultureInfo for German in Germany.
            return utcDt.ToLocalTime();

        }
        #endregion

        #region SOLR Functions
        public static void ForumParamToSearchParam(NameValueCollection formParam, ref SolrEntity.SearchParameter param)
        {
            String SalaryRange = string.Empty;
            String DateRange = string.Empty;
            String[] salaryRange = new String[2];
            String[] dateRange = new String[2];
            if (formParam != null && formParam.Count > 0)
            {
                String Location = String.IsNullOrEmpty(formParam["lstLocation"]) ? null : formParam["lstLocation"];
                String FullTimePartTime = String.IsNullOrEmpty(formParam["lstFtPt"]) ? null : formParam["lstFtPt"];
                String PositionType = String.IsNullOrEmpty(formParam["lstPositionType"]) ? null : formParam["lstPositionType"];
                String EmployeementType = String.IsNullOrEmpty(formParam["lstJobType"]) ? null : formParam["lstJobType"];
                String FreeSearch = String.IsNullOrEmpty(formParam["SkillSearch"]) ? null : formParam["SkillSearch"];

                SalaryRange = formParam["lstSalaryRange"];
                DateRange = formParam["lstDateRange"];

                if (!String.IsNullOrEmpty(SalaryRange))
                {
                    salaryRange = SalaryRange.Split(';');

                    if (salaryRange.Count() > 1 && !String.IsNullOrEmpty(salaryRange[0]) && !String.IsNullOrEmpty(salaryRange[1]))
                    {
                        param.MinSalary = Convert.ToDecimal(salaryRange[0]);
                        param.MaxSalary = Convert.ToDecimal(salaryRange[1]);
                    }
                }
                if (!String.IsNullOrEmpty(DateRange))
                {
                    dateRange = DateRange.Split(';');
                    if (dateRange.Count() > 1 && !String.IsNullOrEmpty(dateRange[0]) && !String.IsNullOrEmpty(dateRange[1]))
                    {
                        try
                        {
                            param.MinDate = Convert.ToDateTime(dateRange[0]);
                            param.MaxDate = Convert.ToDateTime(dateRange[1]);
                        }
                        catch (Exception)
                        {
                            param.MinDate = DateTime.MinValue;
                            param.MaxDate = DateTime.MinValue;
                        }


                        //System.DateTime datetimemin;
                        //System.DateTime datetimemax;
                        //DateTime.TryParseExact(dateRange[0], "MM-DD-YY", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out datetimemin);
                        //if (DateTime.TryParseExact(dateRange[0], "MM-DD-YY", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out datetimemin) && DateTime.TryParseExact(dateRange[1], "MM-DD-YY", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out datetimemax))
                        //{
                        //param.MinDate = Convert.ToDateTime(dateRange[0]);
                        //param.MaxDate = Convert.ToDateTime(dateRange[1]);

                        //}
                    }
                }

                param.FreeSearch = FreeSearch;
                param.Location = Location;
                param.PositionType = PositionType;
                param.FpTime = FullTimePartTime;
                param.JobType = EmployeementType;

            }
        }
        #endregion


        #region Create Temp FileNAme
        public static string GenerateTempFileName()
        {
            return Constants.STR_TEMP + DateTime.Now.Ticks.ToString();
        }

        #endregion
        #region Bind month Dropdown


        public static IEnumerable<SelectListItem> BindMonthDropDown()
        {
            IEnumerable<SelectListItem> Months;

            Months = DateTimeFormatInfo.InvariantInfo.MonthNames.Select((monthName, index) => new SelectListItem
            {
                Value = (index + 1).ToString(),
                Text = monthName
            }).Where(Month => Month.Text != "");
            return Months;
        }
        public static IEnumerable<SelectListItem> BindEndMonthDropDown()
        {
            IEnumerable<SelectListItem> Months;

            Months = DateTimeFormatInfo.InvariantInfo.MonthNames.Select((monthName, index) => new SelectListItem
            {
                Value = (string.IsNullOrEmpty(monthName) ? "13" : (index + 1).ToString()),
                Text = (string.IsNullOrEmpty(monthName) ? "Present" : monthName)
            });
            return Months;
        }

        #endregion


        #region Bind Years Dropdown


        public static IEnumerable<SelectListItem> BindYearDropDown()
        {
            IEnumerable<SelectListItem> Years;

            Years = Enumerable.Range(DateTime.Now.Year - 63, 64).Select(i => new SelectListItem
{
    Value = i.ToString(),
    Text = i.ToString(),

});

            return Years;


        }

        #endregion

        public static ISolrQuery VacancyQuery(String Location, String PositionType, String FpTime, String JobType, String FreeSearch, decimal[] salaryRange, DateTime[] dateRange)
        {
            if (Location == null && PositionType == null && FpTime == null && JobType == null && FreeSearch == null && salaryRange[0] == 0 && salaryRange[1] == 0 && dateRange[0] == System.DateTime.MinValue && dateRange[1] == System.DateTime.MinValue)
            {
                return null;
            }
            else
            {
                return SolrBL.BuildSolrQueryCreater.GenerateCommonQuery(Location, PositionType, FpTime, JobType, FreeSearch, salaryRange, dateRange, FullLocationList());
            }
        }
        public static string ConvertSavedToSearchLocation(string Saved, Dictionary<int, String> list, char SearchSplit, char SavedSplit)
        {
            string search = string.Empty;
            if (!String.IsNullOrEmpty(Saved))
            {
                foreach (string loc in Saved.Split(SavedSplit))
                {
                    if (list.ContainsValue(loc))
                    {
                        search += list.FirstOrDefault(x => x.Value.Equals(loc)).Key;
                        search += SearchSplit;
                    }
                }
                if (search.IndexOf(SearchSplit) > 0)
                {
                    search = search.Substring(0, search.Length - 1);
                }
            }
            return search;

        }
        public static string ConvertSearchToSavedLocation(string search, Dictionary<int, String> list, char SearchSplit, char SavedSplit)
        {
            string saved = string.Empty;
            if (!String.IsNullOrEmpty(search))
            {
                foreach (string key in search.Split(SearchSplit))
                {
                    int index = 0;
                    if (Int32.TryParse(key, out index) && list.ContainsKey(index))
                    {
                        saved += list[index];
                        saved += SavedSplit;
                    }
                }
                if (saved.IndexOf(SavedSplit) > 0)
                    saved = saved.Substring(0, saved.Length - 1);

            }
            return saved;
        }
        public static List<BEClient.EntityLanguage> MultiLanguage(List<BEClient.EntityLanguage> EntityList = null, bool IsEdit = false)
        {


            List<BEClient.EntityLanguage> entitylanguageList = new List<BEClient.EntityLanguage>();
            if (IsEdit)
            {
                foreach (BEMaster.ClientLanguage clientLanguage in ATSCommon.CurrentSession.Instance.VerifiedClient.ClientLanguageList.OrderBy(r => r.objLanguage.LanguageName))
                {
                    if (EntityList.Where(x => x.LanguageId == clientLanguage.LanguageId).Count() <= 0)
                    {
                        BEClient.EntityLanguage entitylanguage = new BEClient.EntityLanguage();

                        entitylanguage.LanguageId = clientLanguage.LanguageId;
                        entitylanguageList.Add(entitylanguage);
                    }
                }
            }
            else
            {
                foreach (BEMaster.ClientLanguage clientLanguage in ATSCommon.CurrentSession.Instance.VerifiedClient.ClientLanguageList.OrderBy(r => r.objLanguage.LanguageName))
                {
                    BEClient.EntityLanguage entitylanguage = new BEClient.EntityLanguage();
                    entitylanguage.LanguageId = clientLanguage.LanguageId;


                    entitylanguageList.Add(entitylanguage);
                }
            }
            return entitylanguageList;

        }
        public static bool AllowAnsOption(int QueDataType)
        {
            bool IsAllow = false;
            if (ATSCommon.CommonFunctions.AllowAnsOptQuestionType.Where(x => x.Key.Equals(QueDataType)).Count() > 0)
            {
                IsAllow = true;
            }
            return IsAllow;

        }
        public static bool CheckDuplicateBreadscrum(BEClient.BreadCrumb currentNavigaion)
        {
            bool IsDuplicate = false;
            if (ATSCommon.CurrentSession.Instance.VerifiedClient != null && ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs != null)
            {
                IsDuplicate = ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Where(x => x.WithoutOrdinalURL.Equals(currentNavigaion.WithoutOrdinalURL)).Count() > 0 ? true : false;

            }
            return IsDuplicate;
        }
        public static bool CheckDuplicateBreadscrumAndDelete(BEClient.BreadCrumb currentNavigaion)
        {
            bool IsDuplicate = false;
            if (ATSCommon.CurrentSession.Instance.VerifiedClient != null && ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs != null)
            {
                IsDuplicate = ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.Where(x => x.WithoutOrdinalURL.Equals(currentNavigaion.WithoutOrdinalURL)).Count() > 0 ? true : false;
                if (IsDuplicate)
                {
                    ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs.RemoveAll(x => x.WithoutOrdinalURL.Equals(currentNavigaion.WithoutOrdinalURL));
                }
            }
            return IsDuplicate;
        }
        public static void SetNullNav(RequestContext request)
        {

            if ((ATSCommon.CurrentSession.Instance.VerifiedClient != null || ATSCommon.CurrentSession.Instance.VerifiedUser != null) && ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs == null)
            {
                BEClient.User objUser = ATSCommon.CurrentSession.Instance.VerifiedUser;
                List<BEClient.BreadCrumb> objBreadCrumblst = new List<BEClient.BreadCrumb>();
                UrlHelper u = new UrlHelper(request);
                if (objUser == null)
                {
                    BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
                    objBreadCrumb.Action = "Index";
                    objBreadCrumb.Controller = "MyVacancy";
                    objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_EMPLOYEE;
                    objBreadCrumb.ordinal = 1;
                    objBreadCrumb.URL = u.Action("Index", "MyVacancy", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });
                    objBreadCrumb.ImagePath = BusinessEntity.Common.ImagePaths.VacancyImage;
                    objBreadCrumb.ToolTip = "My Opportunities";
                    objBreadCrumb.WithoutOrdinalURL = objBreadCrumb.URL;
                    objBreadCrumblst.Add(objBreadCrumb);
                    ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;

                }
                else if (objUser.SecurityRoles != null && objUser.SecurityRoles.Where(x => x.AtsSecurityRole == BEClient.ATSSecurityRole.Candidate).Count() > 0)
                {
                    BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
                    objBreadCrumb.Action = "Index";
                    objBreadCrumb.Controller = "Home";
                    objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_CANDIDATE;
                    objBreadCrumb.ordinal = 1;
                    objBreadCrumb.URL = u.Action("Index", "Home", new { area = ATS.WebUi.Common.Constants.AREA_CANDIDATE });
                    objBreadCrumb.ImagePath = BusinessEntity.Common.ImagePaths.HomeImage;
                    objBreadCrumb.ToolTip = "Home";
                    objBreadCrumb.WithoutOrdinalURL = objBreadCrumb.URL;
                    objBreadCrumblst.Add(objBreadCrumb);
                    ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
                }
                else
                {
                    BEClient.BreadCrumb objBreadCrumb = new BEClient.BreadCrumb();
                    objBreadCrumb.Action = "Index";
                    objBreadCrumb.Controller = "MyVacancy";
                    objBreadCrumb.AreaName = ATS.WebUi.Common.Constants.AREA_EMPLOYEE;
                    objBreadCrumb.ordinal = 1;
                    objBreadCrumb.URL = u.Action("Index", "MyVacancy", new { area = ATS.WebUi.Common.Constants.AREA_EMPLOYEE });
                    objBreadCrumb.ImagePath = BusinessEntity.Common.ImagePaths.VacancyImage;
                    objBreadCrumb.ToolTip = "My Opportunities";
                    objBreadCrumb.WithoutOrdinalURL = objBreadCrumb.URL;
                    objBreadCrumblst.Add(objBreadCrumb);
                    ATSCommon.CurrentSession.Instance.VerifiedClient.BreadCrumbs = objBreadCrumblst;
                }

            }
        }


        public static RouteValueDictionary ToRouteValues(NameValueCollection queryString, String area)
        {
            var routeValues = new RouteValueDictionary();
            routeValues.Add("area", area);

            if (queryString != null && queryString.HasKeys() != false)
            {
                foreach (string key in queryString.AllKeys)
                {
                    if (key != null)
                        routeValues.Add(key, queryString[key]);
                }
            }
            return routeValues;
        }
        #region Grid Common

        public static void FilterGrid(string defaultorderfield, string defaultorder, out string FilterSearchString, out string SortBySearchString)
        {
            GridMvc.QueryStringGridSettingsProvider settings = new GridMvc.QueryStringGridSettingsProvider();
            IGridFilterSettings filter = settings.FilterSettings;
            IGridSortSettings sorting = settings.SortSettings;
            FilterSearchString = string.Empty;
            SortBySearchString = string.Empty;
            String FilterColumnName = string.Empty;
            GridFilterType FilterType;
            String FilterValue = string.Empty;
            String SortColumnName = string.Empty;
            String SortDirection = string.Empty;
            SortColumnName = settings.SortSettings.ColumnName;
            SortDirection = settings.SortSettings.Direction.ToString();
            var filteredColumns = settings.FilterSettings.FilteredColumns.ToList();
            string strFilterSearchString = "";
            for (int i = 0; i < filteredColumns.Count(); i++)
            {
                FilterColumnName = filteredColumns[i].ColumnName;
                FilterType = filteredColumns[i].FilterType;
                FilterValue = filteredColumns[i].FilterValue;
                if (FilterColumnName != null)
                {
                    if (FilterType == GridFilterType.Equals)
                    {
                        strFilterSearchString = "And " + FilterColumnName + " = " + "'" + FilterValue + "'";
                    }
                    if (FilterType == GridFilterType.Contains)
                    {
                        strFilterSearchString = " And " + FilterColumnName + " Like " + "'" + "%" + FilterValue + "%" + "'";
                    }
                    if (FilterType == GridFilterType.StartsWith)
                    {
                        strFilterSearchString = " And " + FilterColumnName + " Like " + "'" + FilterValue + "%" + "'";
                    }
                    if (FilterType == GridFilterType.EndsWidth)
                    {
                        strFilterSearchString = " And " + FilterColumnName + " Like " + "'" + "%" + FilterValue + "'";
                    }
                    if (FilterType == GridFilterType.GreaterThan)//After
                    {
                        if (FilterColumnName == "AppliedOn")
                        {
                            strFilterSearchString = " And " + FilterColumnName + " > " + "'" + FilterValue + " 23:59:59" + "'";
                        }
                        else
                        {
                            strFilterSearchString = " And " + FilterColumnName + " > " + "'" + FilterValue + "'";
                        }
                    }
                    if (FilterType == GridFilterType.LessThan) //Before
                    {
                        strFilterSearchString = " And " + FilterColumnName + " < " + "'" + FilterValue + "'";
                    }
                    if (FilterType == GridFilterType.CustomFilter)
                    {
                        if (FilterColumnName == "ApplicantionStatus")
                        {
                            strFilterSearchString = " And " + "ApplicationStatus" + " in (" + "\'" + (FilterValue.Replace("~", "\',\'")) + "\')";
                        }
                        if (FilterColumnName == "JobTitle")
                        {
                            strFilterSearchString = " And " + "VacancyId" + " in (" + "\'" + (FilterValue.Replace("~", "\',\'")) + "\')";
                        }
                    }
                }
                FilterSearchString += strFilterSearchString;
            }
            if (String.IsNullOrEmpty(SortColumnName))
            {
                SortColumnName = defaultorderfield;
                SortDirection = defaultorder;//"Descending";
            }
            if (SortDirection == "Ascending")
            {
                SortBySearchString = " ORDER BY " + SortColumnName + " Asc";
            }
            if (SortDirection == "Descending")
            {
                SortBySearchString = " ORDER BY " + SortColumnName + " Desc";
            }
        }

        #endregion

        public static bool IsRoundTypeSelfEval(Guid VacRndId)
        {
            BLClient.VacancyRoundAction objVacancyRoundAction = new BLClient.VacancyRoundAction(WebUi.Common.CurrentSession.Instance.VerifiedClient.ClientId);
            return objVacancyRoundAction.IsRndTypeSelfEval(VacRndId);

        }

        public static string GetLangBlockContent(BEClient.LanguageBlock LangBlock)
        {
            BLClient.LanguageBlocksAction ObjLanguageBlock = new BLClient.LanguageBlocksAction(WebUi.Common.CurrentSession.Instance.VerifiedClient.ClientId);
            return ObjLanguageBlock.GetBlockDescriptionByBlockIdentifier(WebUi.Common.CurrentSession.Instance.VerifiedClient.CurrentLanguageId, LangBlock.ToString());
        }

        public static void ActiveNextRound(Guid ApplicationId)
        {
            BLClient.PromoteCandidateAction PCAction = new BLClient.PromoteCandidateAction(WebUi.Common.CurrentSession.Instance.VerifiedClient.ClientId);
            //Get Next Round
            //25 12 2014 CHECK
            Guid VacRndId = PCAction.GetFirstVacRndIdByApplicationId(ApplicationId);

            //Update Promote Candidate
            BEClient.PromoteCandidate ObjPC = new BEClient.PromoteCandidate();
            ObjPC.ApplicationId = ApplicationId;
            ObjPC.VacRndId = VacRndId;

            Boolean ResultPC = PCAction.UpdatePromoteCandidate(ObjPC, null);
        }

        public static int GetAttributesNoByRndTypeId(Guid RndTypeId)
        {
            BLClient.RoundAttributesAction RndAttrAction = new BLClient.RoundAttributesAction(WebUi.Common.CurrentSession.Instance.VerifiedClient.ClientId);
            return RndAttrAction.GetRoundAttributesNo(RndTypeId);
        }

        public static int GetAttributesNoByVacRndId(Guid VacRndId)
        {
            BLClient.RoundAttributesAction RndAttrAction = new BLClient.RoundAttributesAction(WebUi.Common.CurrentSession.Instance.VerifiedClient.ClientId);
            return RndAttrAction.GetRoundAttributesNoByVacRndId(VacRndId);
        }

        public static int GetAttributesNoByTVacRndId(Guid TVacRndId)
        {
            BLClient.RoundAttributesAction RndAttrAction = new BLClient.RoundAttributesAction(WebUi.Common.CurrentSession.Instance.VerifiedClient.ClientId);
            return RndAttrAction.GetRoundAttributesNoByTVacRndId(TVacRndId);
        }

        public static bool GetAnswerStatusByVacRndId(Guid ApplicationId, Guid VacRndId)
        {
            BLClient.AppAnswerAction objAppAnswerAction = new BLClient.AppAnswerAction(WebUi.Common.CurrentSession.Instance.VerifiedClient.ClientId);
            List<BEClient.AppAnswer> objAppAnsStatusList = new List<BEClient.AppAnswer>();
            objAppAnsStatusList = objAppAnswerAction.GetAnswerStatusByVacRndId(ApplicationId, VacRndId);
            if (objAppAnsStatusList == null || objAppAnsStatusList.Count() == 0)
            {
                return false;
            }
            else
            {
                var objPendingList = objAppAnsStatusList.Where(m => m.IsAnsPending == true).ToList();
                return objPendingList.Count > 0 ? true : false;
            }
        }

        public static List<BEClient.DrpStringMapping> FillNumericDropdown(int Start, int End)
        {
            List<BEClient.DrpStringMapping> ListNumericDropDown = new List<BEClient.DrpStringMapping>();
            for (int i = Start; i <= End; i++)
            {
                BEClient.DrpStringMapping ddlNumeric = new BEClient.DrpStringMapping();
                ddlNumeric.ValueField = i.ToString();
                ddlNumeric.TextField = i.ToString();
                ListNumericDropDown.Add(ddlNumeric);
            }
            return ListNumericDropDown;
        }

        //Send Mail on Applying Vacancy for Candidate

        public static void SendMailToCandidate()
        {
            try
            {
                BLClient.EmailTemplatesAction EmailTemplateAction = new BLClient.EmailTemplatesAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                BEClient.EmailTemplates objEmailContent = EmailTemplateAction.GetEmailTemplateByEmailIdentifier(Common.CurrentSession.Instance.VerifiedClient.CurrentLanguageId, BEClient.EmailTemplateIdentifier.Apply_Vacancy.ToString());
                String EmailContent = objEmailContent.EmailDescription;
                EmailContent = EmailContent.Replace("##CANDIDATE.FIRSTNAME##", Common.CurrentSession.Instance.VerifiedUser.ObjUserDetails.FirstName);
                EmailContent = EmailContent.Replace("##CANDIDATE.LASTNAME##", Common.CurrentSession.Instance.VerifiedUser.ObjUserDetails.LastName);
                EmailContent = EmailContent.Replace("##CANDIDATE.FULLNAME##", Common.CurrentSession.Instance.VerifiedUser.FullName);


                Mailers.EmailTemplate mvcMailer = new Mailers.EmailTemplate();
                mvcMailer.SendMessage(Common.CurrentSession.Instance.VerifiedUser.Username, objEmailContent.Subject, EmailContent);
            }
            catch
            {
                throw;
            }
        }


        public static List<BEClient.CSNavigation> GetCSNavigationCount()
        {
            List<BEClient.CSNavigation> objCSNavigationCountList = new List<BEClient.CSNavigation>();
            BLClient.CSNavigationAction objCSNavAction = new BLClient.CSNavigationAction(WebUi.Common.CurrentSession.Instance.VerifiedClient.ClientId);

            //***Get Users Divisions
            BLClient.UserDivisionSecurityRoleAction objUserDivisionSecurityRoleAction = new BLClient.UserDivisionSecurityRoleAction(WebUi.Common.CurrentSession.Instance.VerifiedClient.ClientId, true);
            string usersDivision = objUserDivisionSecurityRoleAction.GetAllDivisionsByCurrentUser;


            objCSNavigationCountList = objCSNavAction.GetCSNavigationCount(usersDivision);
            //objCSNavigationCountList = objCSNavAction.GetCSNavigationCount(WebUi.Common.CurrentSession.Instance.VerifiedUser.UserId);

            return objCSNavigationCountList;
        }
        //csv
        public static string CreateCSVFileForPOB(BEClient.HireCandidates objHireCandidate)
        {
            var _ClientId = Prompt.Shared.Utility.Library.Methods.GetAppSettingValue("OnboardingClientID");
            if (!string.IsNullOrEmpty(_ClientId))
            {
                var fileName = Common.CommonFunctions.GetGuidInStringFormat(objHireCandidate.ApplicationId) + ".csv";
                var filePath = Path.Combine(HttpContext.Current.Server.MapPath(WebUi.Common.Constants.STR_RESUME_PATH + "POB_CSV/") + fileName);
                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Close();
                }
                else
                {
                    string ExistingFilePath = Path.Combine(Common.Constants.STR_RESUME_PATH + "POB_CSV/", fileName);
                    System.IO.File.Delete(System.Web.HttpContext.Current.Server.MapPath(ExistingFilePath));
                    File.Create(filePath).Close();
                }

                var sb1 = "ClientID,FirstName,LastName,MiddleName,PersonalEmail,Division,JobType,StreetAddress,City,State,Zip,HomePhone,JobLocation,EmployementType,PositionType,PositionOwner,PositionID,PayType,HiringDate,OnboardingManagerID,OnboardingManagerFirstName,OnboardingManagerLastName";
                var csv = new System.Text.StringBuilder();
                csv.Append(sb1);
                csv.Append(Environment.NewLine);
                var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21}",
                _ClientId
               , objHireCandidate.FirstName
               , objHireCandidate.LastName
               , objHireCandidate.MiddleName
               , objHireCandidate.PersonalEmail
               , objHireCandidate.Division
               , objHireCandidate.JobType
               , "\"" + objHireCandidate.StreetAddress + "\""
               , objHireCandidate.City
               , objHireCandidate.State
               , objHireCandidate.Zip
               , objHireCandidate.HomePhone
               , objHireCandidate.JobLocation
               , objHireCandidate.EmployementType
               , objHireCandidate.PositionType
               , objHireCandidate.PositionOwner
               , objHireCandidate.PositionID.ToString()
               , objHireCandidate.PayType
               , objHireCandidate.HiringDate.ToString()
               , objHireCandidate.OnboardingManagerID.ToString()
               , objHireCandidate.OnboardingManagerFirstName
                , objHireCandidate.OnboardingManagerLastName);
                csv.Append(newLine);
                File.WriteAllText(filePath, csv.ToString());
                return filePath;
            }
            else
            {
                return "NA";
            }
        }


        public static BEClient.NavCount CandidateNavigationCount()
        {
            try
            {
                Guid UserId = WebUi.Common.CurrentSession.Instance.UserId;
                Guid LanguageId = WebUi.Common.CurrentSession.Instance.VerifiedClient.CurrentLanguageId;
                BLClient.NavCountAction objNavCountAction = new BLClient.NavCountAction(WebUi.Common.CurrentSession.Instance.VerifiedClient.ClientId);
                BEClient.NavCount objNavCount = objNavCountAction.CandidateNavigationCount(UserId, LanguageId);
                return objNavCount;
            }
            catch
            {
                throw;
            }
        }

        public static List<BEClient.RoundAttributes> GetRoundAttributesForQueCat()
        {
            BLClient.RoundAttributesAction ObjRndAttrAction = new BLClient.RoundAttributesAction(WebUi.Common.CurrentSession.Instance.VerifiedClient.ClientId);
            List<BEClient.RoundAttributes> ObjRndAttributes = new List<BEClient.RoundAttributes>();
            ObjRndAttributes = ObjRndAttrAction.FillAllRoundAttributes(WebUi.Common.CurrentSession.Instance.VerifiedClient.CurrentLanguageId);
            var OfferRound = ObjRndAttributes.Where(x => x.RoundAttributesNo == (int)BEClient.RndAttrNo.OfferRound).FirstOrDefault();
            ObjRndAttributes.Remove(OfferRound);
            return ObjRndAttributes;
        }

        public static bool CheckForRequiredDocuments(Guid ApplicationId)
        {
            BLClient.RequiredDocumentAction objRequiredDocAction = new BLClient.RequiredDocumentAction(WebUi.Common.CurrentSession.Instance.VerifiedClient.ClientId);
            bool Result = objRequiredDocAction.CheckForRequiredDocuments(ApplicationId);
            return Result;
        }
        #region new upload document type

        //Anand
        public static bool UploadDocument(BEClient.Profile profile, string ExtensionTypes, System.Web.HttpPostedFileBase documentFile, out string _oldFileName, out string _newFileName, out string _serverFilePath, out Guid _DocumentId, bool isCoverLetter = false, bool isSaveInDB = true)
        {
            bool _isFileUploaded = false;
            _oldFileName = String.Empty;
            _newFileName = String.Empty;
            _serverFilePath = String.Empty;
            _DocumentId = Guid.Empty;
            Guid _profileid = profile.ProfileId;
            try
            {
                if (documentFile != null && documentFile.ContentLength > 0)
                {

                    string _resumePath = ValidateFile(documentFile, out _oldFileName, out _newFileName, out _serverFilePath, ExtensionTypes);

                    #region Create new directory
                    CreateDirectory();
                    #endregion
                    if (isSaveInDB)
                    {
                        BEClient.ATSResume _objATSResume = new BEClient.ATSResume();
                        BLClient.ATSResumeAction objatsresumeAction = new BLClient.ATSResumeAction(ATS.WebUi.Common.CurrentSession.Instance.VerifiedClient.ClientId);
                        _objATSResume.UserId = profile.UserId;
                        _objATSResume.ProfileId = _profileid;
                        _objATSResume.UploadedFileName = _oldFileName;
                        _objATSResume.NewFileName = _newFileName;
                        _objATSResume.Path = _serverFilePath;
                        _objATSResume.IsCoverLetter = isCoverLetter;
                        _objATSResume.DocumentTypeId = profile.DocumentTypeId;
                        _objATSResume.CandidateDescription = profile.CandidateDescription;
                        CurrentSession.Instance.VerifiedUser.tempResume = _objATSResume;
                        Guid AtsResumeid = objatsresumeAction.AddATSResume(_objATSResume);
                        if (AtsResumeid != Guid.Empty)
                        {
                            _DocumentId = AtsResumeid;
                            SaveFile(documentFile, _resumePath);
                        }
                    }


                    if (!System.IO.File.Exists(_resumePath))
                        throw new Exception("File not Upload on Path " + _resumePath);
                    else

                        _isFileUploaded = true;
                }
                //ViewBag.Message = "Profile Save Successfully";
            }
            catch
            {

                throw;
            }
            return _isFileUploaded;
        }
        //Anand
        public static string ValidateFile(System.Web.HttpPostedFileBase documentFile, out string _oldFileName, out string _newFileName, out string _serverFilePath, string ExtensionTypes)
        {
            try
            {
                _oldFileName = String.Empty;
                _newFileName = String.Empty;
                _serverFilePath = String.Empty;

                //Vlidate Document from server side
                Common.CommonFunctions.ValidateDocumentTypeExtensions(documentFile.FileName, ExtensionTypes);

                //Get Extension of uploaded File
                string _extension = Path.GetExtension(documentFile.FileName);
                _oldFileName = Path.GetFileName(documentFile.FileName);
                _newFileName = Common.CommonFunctions.GetGuidInStringFormat(Guid.NewGuid()) + _extension;
                _serverFilePath = Path.Combine(Common.Constants.STR_RESUME_PATH, _newFileName);
                string _resumePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(Common.Constants.STR_RESUME_PATH), _newFileName);

                return _resumePath;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static bool ValidateDocumentTypeExtensions(String FileName, string ExtensionTypes)
        {
            try
            {
                bool exExists = false;
                string _currExt = Path.GetExtension(FileName).ToLower();
                exExists = ExtensionTypes.Contains(_currExt);
                if (!exExists)
                {
                    throw new Exception("Document Invalid !!");
                }
                return exExists;
            }
            catch
            {
                throw;
            }
        }
        public static bool GetCandidateDocument(BEClient.Profile profile, HttpPostedFileBase documentFile, bool allowParse)
        {
            try
            {
                string _serverFilePath = string.Empty;
                string _newFileName = string.Empty;
                string _oldFileName = string.Empty;
                string ExtensionTypes = string.Empty;
                Guid _DocumentId = Guid.Empty;
                bool _isFileUploaded = false;
                bool isCoverletter = false;

                if (profile.DocCategoryType == 2)
                {
                    isCoverletter = true;
                }
                if (documentFile != null)
                {
                    if (profile != null)
                    {
                        ExtensionTypes = profile.ExtensionTypes.ToString();
                        _isFileUploaded = Common.CommonFunctions.UploadDocument(profile, ExtensionTypes, documentFile, out _oldFileName, out _newFileName, out _serverFilePath, out _DocumentId, isCoverletter, true);
                        //if (!_isFileUploaded)
                        //{
                        //    throw new Exception("File Not Uploaded");
                        //}
                        //else
                        //{
                        //    return true;
                        //}

                    }
                }
                return _isFileUploaded;

            }
            catch
            {
                throw;
            }
        }



        #endregion

        //for default profile 

        public static Guid GetDefaultProfileOfUser(Guid UserId)
        {
            try
            {
                Guid _ProfileId = Guid.Empty;
                Guid _AtsResumeId = Guid.Empty;
                BEClient.Profile objProfile = new BEClient.Profile();
                BEClient.ATSResume objATSResume = new BEClient.ATSResume();
                BLClient.ProfileAction _objProfileAction = new BLClient.ProfileAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                BLClient.ATSResumeAction _ATSResumeAction = new BLClient.ATSResumeAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
                if (UserId != Guid.Empty)
                {
                    objProfile = _objProfileAction.GetLastUpdatedProfileByUserId(UserId);
                    if (objProfile != null)
                    {
                        _ProfileId = objProfile.ProfileId;
                        objATSResume = _ATSResumeAction.GetResumeByProfile(_ProfileId);
                        _AtsResumeId = objATSResume.ATSResumeId;
                    }
                    else
                    {
                        #region Creat new profile and ATS resume
                        //Create new Profile and Assign value to _profileId 
                        objProfile = new BEClient.Profile();
                        objProfile.UserId = UserId;
                        objProfile.ClientId = Common.CurrentSession.Instance.VerifiedClient.ClientId;
                        _ProfileId = _objProfileAction.AddProfile(objProfile);
                        objATSResume = CreateBlankAtsResumeobject(_ProfileId, UserId);
                        _AtsResumeId = _ATSResumeAction.AddATSResume(objATSResume);
                        #endregion
                    }
                }
                return _AtsResumeId;
            }
            catch
            {
                throw;
            }
        }
        public static BEClient.ATSResume CreateBlankAtsResumeobject(Guid profileid, Guid userid)
        {
            string _oldFileName = string.Empty;
            string _newFileName = string.Empty;
            string _serverFilePath = string.Empty;
            string _extension = ".doc";
            BEClient.ATSResume objATSResume = new BEClient.ATSResume();
            objATSResume.ProfileId = profileid;
            objATSResume.UserId = userid;
            _oldFileName = "ATS_Blank_Resume.doc";
            _newFileName = Common.CommonFunctions.GetGuidInStringFormat(Guid.NewGuid()) + _extension;
            _serverFilePath = Path.Combine(Common.Constants.STR_RESUME_PATH, _newFileName);
            string _resumePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(Common.Constants.STR_RESUME_PATH), _newFileName);
            objATSResume.NewFileName = _newFileName;
            objATSResume.UploadedFileName = _oldFileName;
            objATSResume.Path = _serverFilePath;
            System.IO.File.Copy(System.Web.HttpContext.Current.Server.MapPath("~/Resume/Sample/ATS_Blank_Resume.doc"), _resumePath);
            return objATSResume;
        }

        public Guid CreateDummyInterview(Guid ApplicationId, Guid VacRndId)
        {
            BLClient.ScheduleIntAction objScheduleIntAction = new BLClient.ScheduleIntAction(Common.CurrentSession.Instance.VerifiedClient.ClientId);
            BEClient.ScheduleInt ScheduleInt = new BEClient.ScheduleInt();
            ScheduleInt.ApplicationId = ApplicationId;
            ScheduleInt.VacRndId = VacRndId;
            return objScheduleIntAction.CreateDummyInterview(ScheduleInt);
        }
    }
}