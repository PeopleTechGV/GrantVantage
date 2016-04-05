using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAClient = ATS.DataAccess;
using BEClient = ATS.BusinessEntity;

namespace ATS.BusinessLogic
{
   public class AssignedCandidatesAction:ActionBase
    {
        #region private data member
       private DAClient.AssignedCandidatesRepository _AssignedCandidatesRepository;
        private Guid _MyClientId { get; set; }

        #endregion

         #region Constructor

        public AssignedCandidatesAction(Guid ClientId)
            : base(ClientId)
        {


            _MyClientId = ClientId;
            _AssignedCandidatesRepository = new DAClient.AssignedCandidatesRepository(base.ConnectionString);
            _AssignedCandidatesRepository.CurrentUser = base.CurrentUser;
            _AssignedCandidatesRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

       public BEClient.AssignedCandidates GetAssignCandidateDetailsForMail(Guid ApplicationId , Guid VacRndId,Guid LanguageId)
        {
            try
            {
                return _AssignedCandidatesRepository.GetAssignCandidateDetailsForMail(ApplicationId, VacRndId, LanguageId);
            }
            catch
            {
                throw;
            }
        }


    }
}
