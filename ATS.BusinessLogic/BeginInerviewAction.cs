using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAClient = ATS.DataAccess;
using BEClient = ATS.BusinessEntity;

namespace ATS.BusinessLogic
{
    public class BeginInerviewAction : ActionBase
    {
        #region private data member
        private DAClient.BeginInterviewRepository _BeginInterviewRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor

        public BeginInerviewAction(Guid ClientId)
            : base(ClientId)
        {


            _MyClientId = ClientId;
            _BeginInterviewRepository = new DAClient.BeginInterviewRepository(base.ConnectionString);
            _BeginInterviewRepository.CurrentUser = base.CurrentUser;
            _BeginInterviewRepository.CurrentClient = base.CurrentClient;
        }
        #endregion



        public Guid AddBeginInterview(BEClient.BeginInterView BeginInterView)
        {
            try
            {
                _BeginInterviewRepository.BeginTransaction();
                //DAClient.AppRndRepository _AppRndRepository = new DAClient.AppRndRepository(base.ConnectionString);
                //_AppRndRepository.Transaction = _BeginInterviewRepository.Transaction;
                //_AppRndRepository.CurrentUser = _BeginInterviewRepository.CurrentUser;
                //_AppRndRepository.CurrentClient = _BeginInterviewRepository.CurrentClient;
                Guid BeginInterViewId = _BeginInterviewRepository.InsertBeginInterview(BeginInterView);

                if (BeginInterViewId.Equals(Guid.Empty))
                {
                    _BeginInterviewRepository.RollbackTransaction();
                    throw new Exception("Begin Interview not Inserted");
                }
                else
                {

                    _BeginInterviewRepository.CommitTransaction();


                }

                return BeginInterViewId;
            }
            catch
            {
                _BeginInterviewRepository.RollbackTransaction();
                throw;
            }
        }

        public bool IsReviewer(Guid VacRndId, Guid UserId)
        {
            try
            {
                return _BeginInterviewRepository.IsReviewer(VacRndId, UserId);
            }
            catch
            {
                throw;
            }

        }

        public bool IsInterviewComplete(Guid VacRndId, Guid UserId, Guid ScheduleIntId)
        {
            try
            {
                return _BeginInterviewRepository.IsInterviewComplete(VacRndId, UserId, ScheduleIntId);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.BeginInterView IsBeginInterviewExists(Guid VacRndId, Guid UserId, Guid ScheduleIntId)
        {

            try
            {
                return _BeginInterviewRepository.IsBeginInterviewExists(VacRndId, UserId, ScheduleIntId);
            }
            catch
            {
                throw;
            }

        }

        public bool UpdateInterviewStatus(Guid VacRndId, Guid UserId, bool IsComplete, Guid ScheduleIntId,Int32? intStatus)
        {
            try
            {
                _BeginInterviewRepository.BeginTransaction();
                bool result = _BeginInterviewRepository.UpdateInterviewStatus(VacRndId, UserId, IsComplete, ScheduleIntId,intStatus);
                if (result)
                {
                    Guid ApplicationId = Guid.Empty;
                    ScheduleIntAction ObjScheduleIntAction = new ScheduleIntAction(_MyClientId);
                    ApplicationId = ObjScheduleIntAction.GetappplicationIdByScheduleIntId(ScheduleIntId);
                    if (ApplicationId != null && ApplicationId != Guid.Empty)
                    {
                        int DescriptionText = 0;
                        if (IsComplete)
                        {
                            DescriptionText = 7;
                        }
                        else
                        {
                            DescriptionText = 8;
                        }
                        Common.HistoryAction.CreateHistoryObject(ApplicationId, this.CurrentUser.UserId, DescriptionText, VacRndId, _MyClientId);

                    }
                    _BeginInterviewRepository.CommitTransaction();


                }
                else
                {
                    _BeginInterviewRepository.RollbackTransaction();
                }
                return result;
            }
            catch
            {
                _BeginInterviewRepository.RollbackTransaction();
                throw;

            }
        }


        public bool UpdateStatusOfInterview(Guid VacRndId, Guid UserId, bool IsComplete, Guid ScheduleIntId, Int32 InterviewStatus)
        {
            try
            {
                _BeginInterviewRepository.BeginTransaction();
                bool result = _BeginInterviewRepository.UpdateStatusOfInterview(VacRndId, UserId, IsComplete, ScheduleIntId,InterviewStatus);
                if (result)
                {
                    _BeginInterviewRepository.CommitTransaction();
                }
                else
                {
                    _BeginInterviewRepository.RollbackTransaction();
                }
                return result;
            }
            catch
            {
                _BeginInterviewRepository.RollbackTransaction();
                throw;
            }
        }





    }
}
