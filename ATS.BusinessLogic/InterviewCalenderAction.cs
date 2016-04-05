using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic
{
   public class InterviewCalenderAction:ActionBase
    {
        #region private data member
        private DAClient.InterviewCalenderRepository _InterviewCalenderRepository;
        private Guid _MyClientId { get; set; }
        #endregion

          #region Constructor

        public InterviewCalenderAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _InterviewCalenderRepository = new DAClient.InterviewCalenderRepository(base.ConnectionString);
            _InterviewCalenderRepository.CurrentUser = base.CurrentUser;
            _InterviewCalenderRepository.CurrentClient = base.CurrentClient;

        }
        #endregion

       public List<BEClient.InterviewCalender> GetAllInterviewCalender(Guid LanguageId,string status,string SearchText)
        {
           try
           {
               return _InterviewCalenderRepository.GetAllInterviewCalender(LanguageId, status, SearchText);
           }
           catch
           {
               throw;
           }
        }

       public List<BEClient.InterviewCalender> GetAllInterviewStatusCounts(Guid UserId)
       {
           try
           {
               return _InterviewCalenderRepository.GetAllInterviewStatusCounts(UserId);
           }
           catch
           {
               throw;
           }
       }
    }
}
