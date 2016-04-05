using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.WebUi.Mailers
{
    public interface IEmailTemplate
    {
        void SendCandidateRegistrationMail(String pTo, String pLink, String pEmail, String pDomainName);
        //void SendEmployeeForgotPasswordMail(String pTo, String pLink, String pUserName);
        //void SendAdminForgotPasswordMail(String pTo, String pLink, String pUserName);

        //void SendEmployerRegistrationMail(String pTo, String pLink, String pEmail);
        //void SendEmployerForgotPasswordMail(String pTo, String pLink, String pUserName);
        //void SendEmployeeReferralMail(String pTo, String pLink, String pUserName, String pMessage);
        //void SendEmployeeReferencesMail(String pTo, String pLink, String pUserName, String pMessage);
        //void SendFeedbackThankYouMail(String pTo);
        //void SendFeedbackToAdminMail(String pFromEmail,String pName, String pMessage);
        //void SendAnonymsEmployeeAlertActiveMail(String pTo, String pLink, String JobAlert);
        //void SendSocialSiteRegistrationMailToAdminOnly(String pTo, String pUserName, Entity.LoginBy LoginBy);

        void SendMessage(String pTo, String pSubject, String mailMessage);
    }
}
