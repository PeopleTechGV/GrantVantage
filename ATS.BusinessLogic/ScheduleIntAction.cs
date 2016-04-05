using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic
{
    public class ScheduleIntAction : ActionBase
    {
        #region private data member
        private DAClient.ScheduleIntRepository _ScheduleIntRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor
        public ScheduleIntAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _ScheduleIntRepository = new DAClient.ScheduleIntRepository(base.ConnectionString);
            _ScheduleIntRepository.CurrentUser = base.CurrentUser;
            _ScheduleIntRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public Guid IsScheduled(BEClient.ScheduleInt ScheduleInt)
        {
            try
            {
                return _ScheduleIntRepository.IsScheduled(ScheduleInt);
            }
            catch
            {
                throw;
            }
        }
        public Guid AddSaveScheduleInt(BEClient.ScheduleInt ScheduleInt, bool IsHistory = true)
        {
            try
            {
                Guid newId = Guid.Empty;
                newId = _ScheduleIntRepository.AddSaveScheduleInt(ScheduleInt);
                if (newId != Guid.Empty && IsHistory)
                {
                    string appendText = ScheduleInt.ScheduleDateStr + " " + ScheduleInt.StartTime;
                    Common.HistoryAction.CreateHistoryObject(ScheduleInt.ApplicationId, this.CurrentUser.UserId, 3, null, _MyClientId, appendText);

                }
                return newId;
            }
            catch
            {
                throw;
            }
        }

        public Guid CreateDummyInterview(BEClient.ScheduleInt ScheduleInt)
        {
            try
            {
                return _ScheduleIntRepository.CreateDummyInterview(ScheduleInt);
            }
            catch
            {
                throw;
            }
        }

        public Guid AddSaveScheduleIntWithoutLogin(BEClient.ScheduleInt ScheduleInt, Guid _UserId)
        {
            try
            {
                if (_ScheduleIntRepository.CurrentUser == null)
                {
                    _ScheduleIntRepository.CurrentUser = new BEClient.User() { UserId = _UserId };

                }

                return _ScheduleIntRepository.AddSaveScheduleInt(ScheduleInt);
            }
            catch
            {
                throw;
            }
        }

        public Boolean UpdateScheduleInt(BEClient.ScheduleInt objScheduleInt)
        {
            try
            {
                bool result = false;
                result = _ScheduleIntRepository.UpdateScheduleInt(objScheduleInt);
                if (result)
                {
                    Int32 strindex = 10;
                    Common.HistoryAction.CreateHistoryObject(objScheduleInt.ApplicationId, this.CurrentUser.UserId, strindex, objScheduleInt.VacRndId, _MyClientId);

                }
                return result;
            }
            catch
            {
                throw;
            }
        }

        public bool IsInterviewBegin(Guid ScheduleIntId)
        {
            try
            {
                return _ScheduleIntRepository.IsInterviewBegin(ScheduleIntId);
            }
            catch
            {
                throw;
            }
        }

        public bool DeleteScheduleInt(Guid ScheduleIntId)
        {
            try
            {
                return _ScheduleIntRepository.DeleteScheduleInt(ScheduleIntId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.ScheduleInt> GetVacRndDetailByApplicationId(Guid ApplicationId, String LstRndTypeId)
        {
            try
            {
                return _ScheduleIntRepository.GetVacRndDetailByApplicationId(ApplicationId, LstRndTypeId);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.ScheduleInt GetIntScheduleByScheduleIntId(Guid ScheduleIntId)
        {
            try
            {
                return _ScheduleIntRepository.GetIntScheduleByScheduleIntId(ScheduleIntId);
            }
            catch
            {
                throw;
            }
        }

        public Guid GetSchIdByAppAndVacQueAndRMId(Guid ApplicationId, Guid VacRndId)
        {
            try
            {
                return _ScheduleIntRepository.GetSchIdByAppAndVacQueAndRMId(ApplicationId, VacRndId);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.ScheduleInt GetAppStatusByAppId(Guid ApplicationId, Guid LanguageId)
        {
            try
            {
                return _ScheduleIntRepository.GetAppStatusByAppId(ApplicationId, LanguageId);
            }
            catch
            {
                throw;
            }
        }
        public BEClient.ScheduleInt GetSchIntByAppAndRndTypeId(Guid ApplicationId, string RndTypeId)
        {
            try
            {
                return _ScheduleIntRepository.GetSchIdByAppAndRndTypeId(ApplicationId, RndTypeId);
            }
            catch
            {
                throw;
            }
        }

        public Guid GetappplicationIdByScheduleIntId(Guid ScheduleIntId)
        {
            try
            {
                return _ScheduleIntRepository.GetApplicationIdByScheduleIntId(ScheduleIntId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.ScheduleInt> GetBeginInterviewsByAppIdAndUserId(Guid ApplicationId, Guid UserId)
        {
            try
            {
                return _ScheduleIntRepository.GetBeginInterviewsByAppIdAndUserId(ApplicationId, UserId);
            }
            catch
            {
                throw;
            }
        }

    }
}
