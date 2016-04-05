using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAClient = ATS.DataAccess;
using BEClient = ATS.BusinessEntity;
using System.Text.RegularExpressions;

namespace ATS.BusinessLogic
{
    public class EmailContentAction : ActionBase
    {
        #region private data member
        private DAClient.EmailContentRepository _EmailContentRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor
        public EmailContentAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _EmailContentRepository = new DAClient.EmailContentRepository(base.ConnectionString);
            _EmailContentRepository.CurrentUser = base.CurrentUser;
            _EmailContentRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public BEClient.EmailTemplates GetEmailContentByAppIdAndVacRndId(BEClient.EmailTemplates objEmailTemplate, Guid ApplicationId, Guid VacRndId)
        {
            try
            {
                BEClient.EmailContent objEmailContent = new BEClient.EmailContent();
                objEmailContent = _EmailContentRepository.GetEmailContentByAppIdAndVacRndId(ApplicationId, VacRndId);
                objEmailTemplate.EmailDescription = ReplaceEmailContent(objEmailContent, objEmailTemplate.EmailDescription);
                objEmailTemplate.Subject = ReplaceEmailContent(objEmailContent, objEmailTemplate.Subject);
                objEmailTemplate.DestinationAddress = objEmailContent.DestinationAddress;
                return objEmailTemplate;
            }
            catch
            {
                throw;
            }
        }

        public string ReplaceEmailContent(BEClient.EmailContent objMailContent, string Content)
        {
            Content = Regex.Replace(Content, "##Candidate.FullName##", objMailContent.FullName, RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "##Candidate.FirstName##", objMailContent.FirstName, RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "##Candidate.LastName##", objMailContent.LastName, RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "##Offer.VacancyName##", objMailContent.VacancyName, RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "##Offer.OfferStatus##", objMailContent.OfferStatus, RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "##Offer.CreatedBy##", objMailContent.CreatedBy, RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "##Offer.CreatedOn##", objMailContent.CreatedOn, RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "##Offer.ModifiedBy##", objMailContent.ModifiedBy, RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "##Offer.ModifiedOn##", objMailContent.ModifiedOn, RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "##Offer.PlacementType##", objMailContent.PlacementType, RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "##Offer.JobType##", objMailContent.JobType, RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "##Offer.StartDate##", objMailContent.StartDate, RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "##Offer.PlacementLocation##", objMailContent.PlacementLocation, RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "##Offer.SalaryType##", objMailContent.SalaryType, RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "##Offer.SalaryOffered##", objMailContent.SalaryOffered, RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "##Offer.BonusDescription##", objMailContent.BonusDescription, RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "##Offer.BonusPotential##", objMailContent.BonusPotential, RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "##Offer.BonusCap##", objMailContent.BonusCap, RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "##Offer.CommissionDescription##", objMailContent.CommissionDescription, RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "##Offer.CommissionPotential##", objMailContent.CommissionPotential, RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "##Offer.CommissionCap##", objMailContent.CommissionCap, RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "##Offer.CandidateNoticePeriod##", objMailContent.CandidateNoticePeriod, RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "##Offer.CandidateNoticePeriodIn##", objMailContent.CandidateNoticePeriodIn, RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "##Offer.CompanyNoticePeriod##", objMailContent.CompanyNoticePeriod, RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "##Offer.CompanyNoticePeriodIn##", objMailContent.CompanyNoticePeriodIn, RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "##Offer.CompanyName##", CurrentClient.Clientname, RegexOptions.IgnoreCase);
            Content = Regex.Replace(Content, "##Offer.ManagerName##", objMailContent.ManagerName, RegexOptions.IgnoreCase);
            return Content;
        }
    }
}
