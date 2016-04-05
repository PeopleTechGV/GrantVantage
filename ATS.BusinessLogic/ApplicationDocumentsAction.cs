using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAClient = ATS.DataAccess;
using BEClient = ATS.BusinessEntity;

namespace ATS.BusinessLogic
{
    public class ApplicationDocumentsAction : ActionBase
    {
        #region private data member
        private DAClient.ApplicationDocumentsRepository _ApplicationDocumentsRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor

        public ApplicationDocumentsAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _ApplicationDocumentsRepository = new DAClient.ApplicationDocumentsRepository(base.ConnectionString);
            _ApplicationDocumentsRepository.CurrentUser = base.CurrentUser;
            _ApplicationDocumentsRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public List<BEClient.ApplicationDocuments> GetApplicationHistoryByApplicationId(Guid ApplicationId, Guid VacRndId)
        {
            try
            {
                return _ApplicationDocumentsRepository.GetAplicationDocumentsByAppIdVacRndId(ApplicationId, VacRndId);
            }
            catch
            {
                return null;
            }
        }

        public Guid AddApplicationDocuments(BEClient.ApplicationDocuments objApplicationDocuments)
        {
            try
            {
                return _ApplicationDocumentsRepository.AddApplicationDocuments(objApplicationDocuments);
            }
            catch
            {
                return Guid.Empty;
            }
        }

        public bool UpdateApplicationDocuments(BEClient.ApplicationDocuments objAplicationDocument)
        {
            try
            {
                return _ApplicationDocumentsRepository.UpdateApplicationDocuments(objAplicationDocument);
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteAppDocumentByAppId(Guid ApplicationId)
        {
            try
            {
                return _ApplicationDocumentsRepository.DeleteAppDocumentByAppId(ApplicationId);
            }
            catch
            {
                return false;
            }
        }

        public List<BEClient.ApplicationDocuments> GetScreeningDocumentsByVacancyIdAndUserId(Guid VacancyId, Guid UserId)
        {
            try
            {
                return _ApplicationDocumentsRepository.GetScreeningDocumentsByVacancyIdAndUserId(VacancyId, UserId);
            }
            catch
            {
                return null;
            }
        }

        public List<BEClient.ApplicationDocuments> GetRequiredDocumentsByApplicationId(Guid ApplicationId)
        {
            try
            {
                return _ApplicationDocumentsRepository.GetRequiredDocumentsByApplicationId(ApplicationId);
            }
            catch
            {
                return null;
            }
        }

        public BEClient.ApplicationDocuments GetApplicationDocumentById(Guid ApplicationDocumentId)
        {
            try
            {
                return _ApplicationDocumentsRepository.GetApplicationDocumentById(ApplicationDocumentId);
            }
            catch
            {
                throw;
            }
        }
    }
}
