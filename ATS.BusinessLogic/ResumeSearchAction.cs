using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;


namespace ATS.BusinessLogic
{
    public class ResumeSearchAction : ActionBase
    {
        private DAClient.ResumeSearchRepository _ResumeSearchRepository;

        public ResumeSearchAction(Guid ClientId)
            : base(ClientId)
        {

            _ResumeSearchRepository = new DAClient.ResumeSearchRepository(base.ConnectionString);
            _ResumeSearchRepository.CurrentUser = base.CurrentUser;
            _ResumeSearchRepository.CurrentClient = base.CurrentClient;
        }

        public List<BEClient.ResumeSearch> GetAllSearchResumeModuleAndFields(Guid languageId)
        {
            try
            {
                return _ResumeSearchRepository.GetAllSearchResumeModuleAndFields(languageId);
            }
            catch
            {
                throw;
            }
        }
    }
}
