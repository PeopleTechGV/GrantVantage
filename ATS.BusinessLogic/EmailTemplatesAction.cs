using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;
using BESrp = ATS.BusinessEntity.SRPEntity;
namespace ATS.BusinessLogic
{
    public class EmailTemplatesAction : ActionBase
    {
        #region private data member
        private DAClient.EmailTemplatesRepository _EmailTemplatesRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor
        public EmailTemplatesAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _EmailTemplatesRepository = new DAClient.EmailTemplatesRepository(base.ConnectionString);
            _EmailTemplatesRepository.CurrentUser = base.CurrentUser;
            _EmailTemplatesRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public bool UpdateEmailTemplate(BEClient.EmailTemplates objEmailTemplate)
        {
            try
            {
                _EmailTemplatesRepository.BeginTransaction();
                bool recordUpdated = _EmailTemplatesRepository.UpdateEmailTemplate(objEmailTemplate);
                _EmailTemplatesRepository.CommitTransaction();
                return recordUpdated;
            }
            catch
            {
                throw;
            }
        }

        public BEClient.EmailTemplates GetEmailTemplateByEmailIdentifier(Guid LanguageId, string EmailIdentifier)
        {
            try
            {
                return _EmailTemplatesRepository.GetEmailTemplateByEmailIdentifier(LanguageId, EmailIdentifier);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.EmailTemplates GetEmailTemplateById(Guid LanguageId, Guid EmailTemplateId)
        {
            try
            {
                return _EmailTemplatesRepository.GetEmailTemplateById(LanguageId, EmailTemplateId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.EmailTemplates> GetAllEmailTemplates(Guid LanguageId)
        {
            try
            {
                return _EmailTemplatesRepository.GetAllEmailTemplates(LanguageId, base.CurrentClient.ClientId);
            }
            catch
            {
                throw;
            }
        }
        public List<BEClient.EmailTemplates> FillDropDownEmailTemplates(Guid LanguageId)
        {
            try
            {
                return _EmailTemplatesRepository.FillDropDownEmailTemplates(LanguageId, base.CurrentClient.ClientId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.EmailTemplates> FillEmailTemplatesByCategory(Int32 EmailCategory, Guid LanguageId)
        {
            try
            {
                return _EmailTemplatesRepository.FillEmailTemplatesByCategory(EmailCategory, LanguageId);
            }
            catch
            {
                throw;
            }
        }

        public string GetEmailDescriptionById(Guid EmailTemplateId, Guid LanguageId)
        {
            try
            {
                return _EmailTemplatesRepository.GetEmailDescriptionById(EmailTemplateId, LanguageId);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.EmailTemplates GetEmailContentById(Guid EmailTemplateId, Guid LanguageId)
        {
            try
            {
                return _EmailTemplatesRepository.GetEmailContentById(EmailTemplateId, LanguageId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.EmailTemplates> FillEmailTemplatesByIds(string EmailTemplateList, Guid LanguageId)
        {
            try
            {
                return _EmailTemplatesRepository.FillEmailTemplatesByIds(EmailTemplateList, LanguageId);
            }
            catch
            {
                throw;
            }
        }
    }
}
