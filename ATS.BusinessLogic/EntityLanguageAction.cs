using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;
namespace ATS.BusinessLogic
{
    public class EntityLanguageAction : ActionBase
    {
        #region private data member
        private DAClient.EntityLanguageRepository _EntityLanguageRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor

        public EntityLanguageAction(Guid ClientId): base(ClientId)
        {
            _MyClientId = ClientId;
            _EntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
            _EntityLanguageRepository.CurrentUser = base.CurrentUser;
            _EntityLanguageRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        #region CRUD
        public Guid AddEntityLanguage(BEClient.EntityLanguage entityLanguage)
        {
            try
            {
                return _EntityLanguageRepository.AddEntityLanguage(entityLanguage);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public bool UpdateEntityLanguage(BEClient.EntityLanguage entityLanguage)
        {
            try
            {
                return _EntityLanguageRepository.UpdateEntityLanguage(entityLanguage);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<BEClient.EntityLanguage> GetEntityLanguageByEntityAndRecordId(BEClient.ATSPrivilage atsPrivilage, Guid RecordId)
        {
            try
            {
                return _EntityLanguageRepository.GetEntityLanguageByEntityAndRecordId(atsPrivilage, RecordId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<BEClient.EntityLanguage> GetEntityLanguageByEntityAndLanguageId(BEClient.ATSPrivilage atsPrivilage, Guid LanguageId)
        {
            try
            {
                return _EntityLanguageRepository.GetEntityLanguageByEntityAndLanguageId(atsPrivilage, LanguageId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        #endregion
    }
}
