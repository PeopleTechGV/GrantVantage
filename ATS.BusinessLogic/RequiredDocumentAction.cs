using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;
using BESrp = ATS.BusinessEntity.SRPEntity;

namespace ATS.BusinessLogic
{
    public class RequiredDocumentAction : ActionBase
    {
        #region private data member
        private DAClient.RequiredDocumentRepository _RequiredDocumentRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor
        public RequiredDocumentAction(Guid ClientId, bool CreateSRPObject = false)
            : base(ClientId)
        {
            if (CreateSRPObject)
                base.CreateSRPObject(SRPCommon.CreateSRPEntityObjects.SRPCreateObj<BESrp.SRPReqDoc>(ClientId));

            _MyClientId = ClientId;
            _RequiredDocumentRepository = new DAClient.RequiredDocumentRepository(base.ConnectionString);
            _RequiredDocumentRepository.CurrentUser = base.CurrentUser;
            _RequiredDocumentRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public Guid InsertRequiredDocument(BEClient.RequiredDocument objRequiredDocument)
        {
            try
            {
                return _RequiredDocumentRepository.InsertRequiredDocument(objRequiredDocument);
            }

            catch
            {
                throw;
            }
        }

        public Guid InsertOptionalResume(Guid VacancyId)
        {
            try
            {
                return _RequiredDocumentRepository.InsertOptionalResume(VacancyId);
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateRequiredDocument(BEClient.RequiredDocument objRequiredDocument)
        {
            try
            {
                return _RequiredDocumentRepository.UpdateRequiredDocument(objRequiredDocument);
            }
            catch
            {
                throw;
            }
        }

        public bool DeleteRequiredDocument(Guid RequiredDocumentId)
        {
            try
            {
                return _RequiredDocumentRepository.DeleteRequiredDocument(RequiredDocumentId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.RequiredDocument> GetRequiredDocumentByVacRndId(Guid VacRndId)
        {
            try
            {
                List<BEClient.RequiredDocument> _objRequiredDocumentList = _RequiredDocumentRepository.GetRequiredDocumentByVacRndId(VacRndId);
                foreach (BEClient.RequiredDocument current in _objRequiredDocumentList)
                {
                    base.SetRoleRecordWise(current, current.DivisionId);
                }

                return _objRequiredDocumentList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public BEClient.RequiredDocument GetRequiredDocumentById(Guid RequiredDocumentId)
        {
            try
            {
                return _RequiredDocumentRepository.GetRequiredDocumentById(RequiredDocumentId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<BEClient.RequiredDocument> GetRequiredDocumentForScreening(Guid VacancyId)
        {
            try
            {
                return _RequiredDocumentRepository.GetRequiredDocumentForScreening(VacancyId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public BEClient.RequiredDocument GetScreeningDocumentById(Guid RequiredDocumentId)
        {
            try
            {
                return _RequiredDocumentRepository.GetScreeningDocumentById(RequiredDocumentId);
            }
            catch
            {
                throw;
            }
        }

        public Boolean CheckForRequiredDocuments(Guid ApplicationId)
        {
            try
            {
                int ReqDoc = _RequiredDocumentRepository.CheckForRequiredDocuments(ApplicationId);
                if (ReqDoc == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
