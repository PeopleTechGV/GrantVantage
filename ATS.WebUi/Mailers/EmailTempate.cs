using Mvc.Mailer;
using System;
using ATS.WebUi.Common;
using System.Text;

namespace ATS.WebUi.Mailers
{
    public class EmailTemplate : MailerBase, IEmailTemplate
    {
        const string _MasterTemplateName = "EmailTemplates/_Layout";
        const string _CandidateRegistrationTemplateName = "EmailTemplates/EmployeeRegistration";
        const string _CandidateForgotPasswordTemplateName = "EmailTemplates/EmployerForgotPassword";

        const string _CandidateRegistrationSubject = "Welcome to the {0} Job Portal";
        const string _CandidateForgotPasswordSubject = "Forgot Password";
        const string _CandidateReferralSubject = "Join Vacancy Portal";
        const string _CandidateReferencesSubject = "Provide Reference - Vacancy Portal";


        const string _ClientContactRegistrationSubject = "Welcome To Vacancy Portal";
        const string _ClientContactForgotPasswordSubject = "Forgot Password";
        const string _AdminForgotPasswordSubject = "Forgot Password";

        const string _FeedbackThankYouSubject = "Thank you for providing feedback";
        const string _FeedbackTToAdminSubject = "Received Feedback from User";

        const string _AdminNewCandidateRegistrationSubject = "New Job Seeker Registration Notification";
        const string _AdminNewEmployerRegistrationSubject = "New Employer Registration Notification";
        const string _AdminNewAlertCreatedSubject = "New Job Seeker Registration For Alert";

        const string _AnonymsCandidateAlertActiveSubject = "Activate your job alert:";

        public EmailTemplate()
        {
            MasterName = _MasterTemplateName;
        }
        //public virtual void SendFeedbackThankYouMail(String pTo)
        //{
        //    var mailMessage = new MvcMailMessage();
        //    mailMessage.Subject = _FeedbackThankYouSubject;
        //    mailMessage.To.Add(pTo);
        //    PopulateBody(mailMessage, viewName: "FeedbackThankyou");
        //    SendMessage(mailMessage);
        //}
        //public virtual void SendFeedbackToAdminMail(String pFromEmail, String pName, String pMessage)
        //{
        //    var mailMessage = new MvcMailMessage();
        //    mailMessage.Subject = _FeedbackTToAdminSubject;
        //    SetAdminEmailAddress(ref mailMessage);
        //    ViewBag.UserName = pName;
        //    ViewBag.Message = pMessage;
        //    ViewBag.EmailId = pFromEmail;
        //    PopulateBody(mailMessage, viewName: "FeedbackToAdmin");
        //    SendMessage(mailMessage);
        //}

        public virtual void SendCandidateRegistrationMail(String pTo, String pLink, String pUserName, String pDomainName)
        {
            var mailMessage = new MvcMailMessage();
            mailMessage.Subject = String.Format(_CandidateRegistrationSubject, pDomainName);
            mailMessage.To.Add(pTo);
            ViewBag.UserName = pUserName;
            ViewBag.Domain = pDomainName;
            ViewBag.Link = pLink;
            PopulateHtmlBody(mailMessage, _CandidateRegistrationTemplateName, _MasterTemplateName);
            SendMessage(mailMessage);
        }

        public virtual void SendCandidateForgotPasswordMail(String pTo, String pLink, String pUserName, String pDomainName)
        {
            var mailMessage = new MvcMailMessage();
            mailMessage.Subject = _CandidateForgotPasswordSubject;
            mailMessage.To.Add(pTo);
            ViewBag.UserName = pUserName;
            ViewBag.Link = pLink;
            ViewBag.Domain = pDomainName;
            PopulateHtmlBody(mailMessage, _CandidateForgotPasswordTemplateName, _MasterTemplateName);
            SendMessage(mailMessage);
        }

        //public virtual void SendAnonymsEmployeeAlertActiveMail(String pTo, String pLink, String JobAlert)
        //{
        //    var mailMessage = new MvcMailMessage();
        //    mailMessage.Subject = _AnonymsEmployeeAlertActiveSubject + "for " + JobAlert;
        //    mailMessage.To.Add(pTo);
        //    ViewBag.Domain = ConfigurationMapped.Instance.CookieHostingName;
        //    ViewBag.JobAlert = JobAlert;
        //    ViewBag.Link = pLink;
        //    PopulateBody(mailMessage, viewName: "AnonymsEmployeeAlertActive");
        //    SendMessage(mailMessage);

        //    mailMessage = new MvcMailMessage();
        //    mailMessage.Subject = _AdminNewAlertCreatedSubject;
        //    SetAdminEmailAddress(ref mailMessage);
        //    ViewBag.JobAlert = JobAlert;
        //    ViewBag.EmailId = pTo;
        //    PopulateBody(mailMessage, viewName: "AnonymsEmployeeAlertActiveToAdmin");
        //    SendMessage(mailMessage);

        //}

        //public virtual void SendEmployerRegistrationMail(String pTo, String pLink, String pUserName)
        //{
        //    var mailMessage = new MvcMailMessage();
        //    mailMessage.Subject = _EmployerRegistrationSubject;
        //    mailMessage.To.Add(pTo);
        //    ViewBag.UserName = pUserName;
        //    ViewBag.Domain = ConfigurationMapped.Instance.CookieHostingName;
        //    ViewBag.Link = pLink;
        //    PopulateBody(mailMessage, viewName: "EmployerRegistration");
        //    SendMessage(mailMessage);

        //    mailMessage = new MvcMailMessage();
        //    mailMessage.Subject = _AdminNewEmployerRegistrationSubject + " from " + ConfigurationMapped.Instance.CookieHostingName;
        //    SetAdminEmailAddress(ref mailMessage);
        //    ViewBag.UserName = pUserName;
        //    ViewBag.EmailId = pTo;
        //    ViewBag.SiteDomain = ConfigurationMapped.Instance.CookieHostingName;
        //    PopulateBody(mailMessage, viewName: "NewEmployerRegistrationToAdmin");
        //    SendMessage(mailMessage);
        //}
        //public virtual void SendSocialSiteRegistrationMailToAdminOnly(String pTo, String pUserName,BusinessEntity.LoginBy LoginBy)
        //{
        //    String LoginDomain = BusinessLogic.Common.GetWebConfigValue.Instance.DomainRedirectionMapping()[ConfigurationMapped.Instance.CountryCode].ToString();
        //    var mailMessage = new MvcMailMessage();
        //    mailMessage = new MvcMailMessage();
        //    mailMessage.Subject = _AdminNewJobSeekerRegistrationSubject + " from " + LoginBy.ToString() +" "+LoginDomain;
        //    SetAdminEmailAddress(ref mailMessage);

        //    ViewBag.SiteDomain = LoginDomain;
        //    ViewBag.UserName = pUserName;
        //    ViewBag.EmailId = pTo;
        //    ViewBag.LoginBy = LoginBy.ToString();
        //    PopulateBody(mailMessage, viewName: "NewJobSeekerRegistrationToAdmin");
        //    SendMessage(mailMessage);
        //}

        //public virtual void SendEmployerForgotPasswordMail(String pTo, String pLink, String pUserName)
        //{
        //    var mailMessage = new MvcMailMessage();
        //    mailMessage.Subject = _EmployerForgotPasswordSubject;
        //    mailMessage.To.Add(pTo);
        //    ViewBag.UserName = pUserName;
        //    ViewBag.Link = pLink;
        //    PopulateBody(mailMessage, viewName: "EmployerForgotPassword");
        //    SendMessage(mailMessage);
        //}

        //public virtual void SendEmployeeReferralMail(String pTo, String pLink, String pUserName, String pMessage)
        //{
        //    var mailMessage = new MvcMailMessage();
        //    mailMessage.Subject = _EmployeeReferralSubject;
        //    mailMessage.To.Add(pTo);
        //    ViewBag.UserName = pUserName;
        //    ViewBag.Link = pLink;
        //    ViewBag.Message = pMessage;
        //    PopulateBody(mailMessage, viewName: "EmployeeReferrals");
        //    SendMessage(mailMessage);
        //}

        //public virtual void SendEmployeeReferencesMail(String pTo, String pLink, String pUserName, String pMessage)
        //{
        //    var mailMessage = new MvcMailMessage();
        //    mailMessage.Subject = _EmployeeReferencesSubject;
        //    mailMessage.To.Add(pTo);
        //    ViewBag.UserName = pUserName;
        //    ViewBag.Link = pLink;
        //    ViewBag.Message = pMessage;
        //    PopulateBody(mailMessage, viewName: "EmployeeReferences");
        //    SendMessage(mailMessage);
        //}

        //public virtual void SendAdminForgotPasswordMail(String pTo, String pLink, String pUserName)
        //{
        //    var mailMessage = new MvcMailMessage();
        //    mailMessage.Subject = _AdminForgotPasswordSubject;
        //    mailMessage.To.Add(pTo);
        //    ViewBag.UserName = pUserName;
        //    ViewBag.Link = pLink;
        //    PopulateBody(mailMessage, viewName: "AdminForgotPassword");
        //    SendMessage(mailMessage);
        //}

        public void SendMessage(String pTo, String pSubject, String pessage)
        {
            var mailMessage = new MvcMailMessage();
            mailMessage.Subject = pSubject;
            mailMessage.To.Add(pTo);
            mailMessage.Body = pessage;
            mailMessage.IsBodyHtml = true;
            SendMessage(mailMessage);
        }
        public void SendMessage(String pTo, String pSubject, String pessage, bool IsAttachment = false, string AttachmentLink = "", bool IsBodyHtml = false)
        {
            var mailMessage = new MvcMailMessage();
            mailMessage.Subject = pSubject;
            mailMessage.To.Add(pTo);
            if (IsAttachment)
            {
                if (AttachmentLink.Contains(","))
                {
                    string[] attachments = AttachmentLink.Split(',');
                    for (int i = 0; i < attachments.Length; i++)
                    {
                        mailMessage.Attachments.Add(new System.Net.Mail.Attachment(attachments[i]));
                    }
                }
                else
                {
                    mailMessage.Attachments.Add(new System.Net.Mail.Attachment(AttachmentLink));
                }
            }
            mailMessage.Body = pessage;
            mailMessage.IsBodyHtml = IsBodyHtml;
            SendMessage(mailMessage);
        }

        private void SendMessage(MvcMailMessage mailMessage)
        {

            var client = new SmtpClientWrapper();
            client.SendCompleted += (sender, e) =>
            {
                if (e.Error != null || e.Cancelled)
                {

                }

            };
            try
            {
                mailMessage.SendAsync(null, client);
            }
            catch (Exception)
            {
                mailMessage.Send();
            }
        }
        //private void SetAdminEmailAddress(ref MvcMailMessage message)
        //{
        //    foreach (string str in CommonFunctions.AdminEmail.Split(';'))
        //    {
        //        message.To.Add(str);
        //    }
        //}
    }
}