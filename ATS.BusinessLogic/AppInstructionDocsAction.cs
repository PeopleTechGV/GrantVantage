using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAClient = ATS.DataAccess;
using BEClient = ATS.BusinessEntity;

namespace ATS.BusinessLogic
{
    public class AppInstructionDocsAction : ActionBase
    {
        #region private data member
        private DAClient.AppInstructionDocsRepository _AppInstructionDocsRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor
        public AppInstructionDocsAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _AppInstructionDocsRepository = new DAClient.AppInstructionDocsRepository(base.ConnectionString);
            _AppInstructionDocsRepository.CurrentUser = base.CurrentUser;
            _AppInstructionDocsRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public Guid InsertAppInstructionDocs(BEClient.AppInstructionDoc objAppInstructionDoc)
        {
            try
            {
                return _AppInstructionDocsRepository.InsertAppInstructionDocs(objAppInstructionDoc);
            }
            catch
            {
                throw;
            }
        }

        public bool DeleteAppInstructionDoc(Guid AppInstructionDocId)
        {
            try
            {
                return _AppInstructionDocsRepository.DeleteAppInstructionDoc(AppInstructionDocId);
            }
            catch
            {
                return false;
            }
        }

        public List<BEClient.AppInstructionDoc> GetAppInstructionDocsByVacancyId(Guid VacancyId)
        {
            try
            {
                return _AppInstructionDocsRepository.GetAppInstructionDocsByVacancyId(VacancyId);
            }
            catch
            {
                return null;
            }
        }
    }
}
