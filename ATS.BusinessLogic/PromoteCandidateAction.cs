using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic
{
    public class PromoteCandidateAction : ActionBase
    {
        #region private data member
        private DAClient.PromoteCandidateRepository _PromoteCandidateRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor
        public PromoteCandidateAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _PromoteCandidateRepository = new DAClient.PromoteCandidateRepository(base.ConnectionString);
            _PromoteCandidateRepository.CurrentUser = base.CurrentUser;
            _PromoteCandidateRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public Guid InsertPromoteCandidate(BEClient.PromoteCandidate ObjPC)
        {
            try
            {
                return _PromoteCandidateRepository.InsertPromoteCandidate(ObjPC);
            }
            catch
            {
                throw;
            }
        }
        public Guid InsertPromoteCandidateWithoutLogin(BEClient.PromoteCandidate ObjPC, Guid _UserId)
        {
            try
            {
                if (_PromoteCandidateRepository.CurrentUser == null)
                {
                    _PromoteCandidateRepository.CurrentUser = new BEClient.User() { UserId = _UserId };

                }

                return _PromoteCandidateRepository.InsertPromoteCandidate(ObjPC);
            }
            catch
            {
                throw;
            }
        }

        public Boolean UpdatePromoteCandidate(BEClient.PromoteCandidate ObjPC, string Status = null)
        {
            try
            {
                bool Result = false;
                int strindex = 0;
                Result = _PromoteCandidateRepository.UpdatePromoteCandidate(ObjPC, Status);
                if (Result)
                {
                    if (Status != null)
                    {
                        if (Status == "Promote")
                        {
                            strindex = 1;
                        }
                        else
                        {
                            strindex = 2;
                        }
                        Common.HistoryAction.CreateHistoryObject(ObjPC.ApplicationId, this.CurrentUser.UserId, strindex, ObjPC.VacRndId, _MyClientId);
                    }
                }
                return Result;
            }
            catch
            {
                throw;
            }
        }
        public Guid UpdatePromoteCandidateapp(Guid VacRndId, Guid ApplicationId, Guid VacancyId)
        {
            try
            {
                Guid Result = Guid.Empty;
                Result = _PromoteCandidateRepository.UpdatePromoteCandidateapp(VacRndId, ApplicationId, VacancyId);
                if (Result != Guid.Empty)
                {
                    Common.HistoryAction.CreateHistoryObject(ApplicationId, this.CurrentUser.UserId, 1, Result, _MyClientId);
                }

                return Result;
            }
            catch
            {
                throw;
            }
        }
        public Guid UpdateDemoteCandidateapp(Guid VacRndId, Guid ApplicationId, Guid VacancyId)
        {
            try
            {


                Guid Result = Guid.Empty;
                Result = _PromoteCandidateRepository.UpdateDemoteandidateapp(VacRndId, ApplicationId, VacancyId);
                if (Result != Guid.Empty)
                {
                    Common.HistoryAction.CreateHistoryObject(ApplicationId, this.CurrentUser.UserId, 2, Result, _MyClientId);
                }

                return Result;
            }
            catch
            {
                throw;
            }
        }



        public Boolean UpdatePromoteCandidateWithoutLogin(BEClient.PromoteCandidate ObjPC, Guid _UserId)
        {
            try
            {
                if (_PromoteCandidateRepository.CurrentUser == null)
                {
                    _PromoteCandidateRepository.CurrentUser = new BEClient.User() { UserId = _UserId };

                }

                return _PromoteCandidateRepository.UpdatePromoteCandidate(ObjPC,null);
            }
            catch
            {
                throw;
            }
        }

        public Guid GetFirstVacRndIdByApplicationId(Guid ApplicationId)
        {
            try
            {
                return _PromoteCandidateRepository.GetFirstVacRndIdByApplicationId(ApplicationId);
            }
            catch
            {
                throw;
            }
        }


        public bool IsPromoted(Guid VacRndId, Guid ApplicationId)
        {
            try
            {
                return _PromoteCandidateRepository.IsPromoted(VacRndId, ApplicationId);
            }
            catch
            {
                throw;
            }
        }
        public bool IsPromotedToInterview(Guid ApplicationId, String RndTypeId)
        {
            try
            {
                return _PromoteCandidateRepository.IsPromotedToInterview(ApplicationId, RndTypeId);
            }
            catch
            {
                throw;
            }
        }


        public Guid GetNextVacRndIdByApplicationId(Guid ApplicationId)
        {
            try
            {
                return _PromoteCandidateRepository.GetNextVacRndIdByApplicationId(ApplicationId);
            }
            catch
            {
                throw;
            }
        }
    }
}
