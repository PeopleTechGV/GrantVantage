using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic
{
    public class DocumentTypeAction : ActionBase
    {
        #region private data member
        private DAClient.DocumentTypeRepository _DocumentTypeRepository;
        private Guid _MyClientId { get; set; }
        #endregion
        #region Constructor

        public DocumentTypeAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _DocumentTypeRepository = new DAClient.DocumentTypeRepository(base.ConnectionString);
            _DocumentTypeRepository.CurrentUser = base.CurrentUser;
            _DocumentTypeRepository.CurrentClient = base.CurrentClient;

        }
        #endregion
        public List<BEClient.DocumentType> GetAllDocmentType(Guid LanguageId)
        {
            try
            {
                return _DocumentTypeRepository.GetAllDocmentType(LanguageId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.DocumentType> FillDocumentType(Guid LanguageId, Guid? VacRndId = null)
        {
            try
            {
                return _DocumentTypeRepository.FillDocumentType(LanguageId, VacRndId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.DocumentType> FillTDocumentType(Guid LanguageId, Guid? TVacRndId = null)
        {
            try
            {
                return _DocumentTypeRepository.FillTDocumentType(LanguageId, TVacRndId);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.DocumentType GetDocmentTypeById(Guid DocumentTypeId, Guid LanguageId)
        {
            try
            {
                BEClient.DocumentType objDocumentType = _DocumentTypeRepository.GetDocmentTypeById(DocumentTypeId, LanguageId);
                objDocumentType.DocumentTypeEntityLanguage = new List<BEClient.EntityLanguage>();
                DAClient.EntityLanguageRepository _ObjEntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                objDocumentType.DocumentTypeEntityLanguage = _ObjEntityLanguageRepository.GetEntityLanguageByEntityAndRecordId(objDocumentType.Privilage, objDocumentType.DocumentTypeId);
                return objDocumentType;
            }
            catch
            {
                throw;
            }
        }

        public Guid InsertDocumentType(BEClient.DocumentType objDocumentType)
        {
            try
            {
                _DocumentTypeRepository.BeginTransaction();
                DAClient.EntityLanguageRepository EntityLanguageRepository = new DAClient.EntityLanguageRepository(base.ConnectionString);
                EntityLanguageRepository.Transaction = _DocumentTypeRepository.Transaction;
                EntityLanguageRepository.CurrentUser = _DocumentTypeRepository.CurrentUser;
                EntityLanguageRepository.CurrentClient = _DocumentTypeRepository.CurrentClient;
                Guid DocumentTypeId = _DocumentTypeRepository.InsertDocumentType(objDocumentType);
                foreach (BEClient.EntityLanguage clientLanguage in objDocumentType.DocumentTypeEntityLanguage)
                {
                    clientLanguage.EntityName = objDocumentType.Privilage;
                    clientLanguage.RecordId = DocumentTypeId;
                    EntityLanguageRepository.AddEntityLanguage(clientLanguage);
                }
                _DocumentTypeRepository.CommitTransaction();
                return DocumentTypeId;
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateDocumentType(BEClient.DocumentType objDocumentType)
        {
            try
            {
                return _DocumentTypeRepository.UpdateDocumentType(objDocumentType);
            }
            catch
            {
                throw;
            }
        }

        public bool Delete(string ListDocumentTypeId)
        {
            try
            {
                _DocumentTypeRepository.BeginTransaction();
                bool Result = _DocumentTypeRepository.Delete(ListDocumentTypeId);
                if (Result)
                {
                    _DocumentTypeRepository.CommitTransaction();
                    return Result;
                }
                else
                {
                    _DocumentTypeRepository.RollbackTransaction();
                    return Result;
                }
            }
            catch
            {
                _DocumentTypeRepository.RollbackTransaction();
                throw;

            }
        }

        public BEClient.DocumentType GetDocTypeByDocCategory(Int32 DocCategory)
        {
            try
            {
                return _DocumentTypeRepository.GetDocTypeByDocCategory(DocCategory);
            }
            catch
            {
                throw;
            }
        }

        public string GetDocExtensionByResumeId(Guid ATSResumeId)
        {
            try
            {
                return _DocumentTypeRepository.GetDocExtensionByResumeId(ATSResumeId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.DocumentType> GetRequiredDocumentForScreening(Guid VacancyId)
        {
            try
            {
                return _DocumentTypeRepository.GetRequiredDocumentForScreening(VacancyId);
            }
            catch
            {
                throw;
            }
        }
    }
}
