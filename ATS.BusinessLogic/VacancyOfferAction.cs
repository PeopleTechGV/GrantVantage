using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAClient = ATS.DataAccess;

using BEClient = ATS.BusinessEntity;

namespace ATS.BusinessLogic
{
    public class VacancyOfferAction : ActionBase
    {
        #region private data member
        private DAClient.VacancyOfferRepository _VacancyOfferRepository;
        private Guid _MyClientId { get; set; }
        #endregion


        #region Constructor

        public VacancyOfferAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _VacancyOfferRepository = new DAClient.VacancyOfferRepository(base.ConnectionString);
            _VacancyOfferRepository.CurrentUser = base.CurrentUser;
            _VacancyOfferRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public Guid AddVacancyOffer(BEClient.VacancyOffer pVacancyOffer)
        {
            try
            {
                return _VacancyOfferRepository.AddVacancyOffer(pVacancyOffer);
            }
            catch
            {
                throw;
            }
        }
        public bool UpdateVacancyOffer(BEClient.VacancyOffer pVacancyOffer)
        {
            try
            {


                return _VacancyOfferRepository.UpdateVacancyOffer(pVacancyOffer);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.VacancyOffer> GetVacancyOfferByApplicationId(Guid ApplicationId, Guid LanguageId)
        {
            try
            {
                return _VacancyOfferRepository.GetVacancyOfferByApplicationId(ApplicationId, LanguageId);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.VacancyOffer GetVacancyOfferById(Guid VacancyOfferId, Guid LanguageId, Guid VacRndId, Guid ReviewerId)
        {
            try
            {
                return _VacancyOfferRepository.GetVacancyOfferById(VacancyOfferId, LanguageId, VacRndId, ReviewerId);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.VacancyOffer GetCandidateNameAndJobTitleByApp(Guid ApplicationId)
        {
            try
            {
                return _VacancyOfferRepository.GetCandidateAndJobTitleByApp(ApplicationId);
            }
            catch
            {
                throw;
            }

        }

        public int GetOfferCountByApplicationId(Guid AppId)
        {
            try
            {
                return _VacancyOfferRepository.GetOfferCountByApplication(AppId);

            }
            catch
            {
                throw;
            }
        }

        public Guid GetJobLocationByApplication(Guid AppId)
        {
            try
            {
                return _VacancyOfferRepository.GetJobLocationByApplication(AppId);
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateAllOfferStatus(Guid ApplicationId, Guid VacancyOfferId, int NewStatusId)
        {
            try
            {
                return _VacancyOfferRepository.UpdateAllOfferStatus(ApplicationId, VacancyOfferId, NewStatusId);
            }
            catch
            {
                throw;
            }
        }
        public BEClient.VacancyOffer GetVacancyOfferByIdForPDF(Guid VacancyOfferId,Guid ETemplate, Guid LanguageId)
        {
            try
            {
                return _VacancyOfferRepository.GetVacancyOfferByIdForPDF(VacancyOfferId, ETemplate, LanguageId);
            }
            catch
            {
                throw;
            }
        }
        public BEClient.VacancyOffer GenerateOfferLetterContentForPDF(Guid VacancyOfferId, Guid LanguageId)
        {
            try
            {
                return _VacancyOfferRepository.GenerateOfferLetterContentForPDF(VacancyOfferId, LanguageId);
            }
            catch
            {
                throw;
            }
        }
        public bool AcceptOfferByCandidate(BEClient.VacancyOffer pVacancyOffer)
        {
            try
            {
                return _VacancyOfferRepository.AcceptOfferByCandidate(pVacancyOffer);
            }
            catch
            {
                throw;
            }
        }
        public bool DeclinedOfferByCandidate(BEClient.VacancyOffer pVacancyOffer)
        {
            try
            {
                return _VacancyOfferRepository.DeclinedOfferByCandidate(pVacancyOffer);
            }
            catch
            {
                throw;
            }
        }

        public bool IsSendApplyEmail(Guid ApplicationId)
        {
            try
            {
                return _VacancyOfferRepository.IsSendApplyEmail(ApplicationId);
            }
            catch
            {
                throw;
            }
        }
        public BEClient.VacancyOffer GetTemplateOfferByVacRndId(Guid VacRndId, Guid AppId, Guid ReviewerId)
        {
            try
            {
                return _VacancyOfferRepository.GetTemplateOfferByVacRndId(VacRndId, AppId, ReviewerId);
            }
            catch
            {
                throw;
            }
        }
    }
}
