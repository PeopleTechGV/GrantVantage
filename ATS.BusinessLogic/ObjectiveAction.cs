using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic
{
   public class ObjectiveAction : ActionBase
    {
        #region private data member
       private DAClient.ObjectiveRepository _ObjectiveRepository;
        private Guid _MyClientId { get; set; }
        #endregion


        #region Constructor

        public ObjectiveAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _ObjectiveRepository = new DAClient.ObjectiveRepository(base.ConnectionString);
            _ObjectiveRepository.CurrentUser = base.CurrentUser;
            _ObjectiveRepository.CurrentClient = base.CurrentClient;

        }
        #endregion




        public BEClient.Objective GetObjectiveByProfileId(Guid ProfileId)
        {
            try
            {
                return _ObjectiveRepository.GetObjectiveByProfileId(ProfileId);
            }
            catch
            {
                throw;
            }
        }


        #region CRUD
        public Guid AddObjective(BEClient.Objective objective)
        {
            try
            {
                _ObjectiveRepository.BeginTransaction();
                Guid UserId = objective.CreatedBy;
                if (_ObjectiveRepository.CurrentUser == null)
                {

                    _ObjectiveRepository.CurrentUser = new BEClient.User() { UserId = UserId };
                }

                Guid newObjectiveId = _ObjectiveRepository.AddObjective(objective);

                _ObjectiveRepository.CommitTransaction();
                return newObjectiveId;
            }
            catch
            {
                _ObjectiveRepository.RollbackTransaction();
                throw;
            }
        }


       public bool UpdateObjective(BEClient.Objective objObjective)
        {
           try
           {
               _ObjectiveRepository.BeginTransaction();
               Guid UserId = objObjective.UserId;
               if (_ObjectiveRepository.CurrentUser == null)
               {

                   _ObjectiveRepository.CurrentUser = new BEClient.User() { UserId = UserId };
               }
               bool IsObjectiveUpdated = _ObjectiveRepository.UpdateObjective(objObjective);
               if(IsObjectiveUpdated)
               {
                   _ObjectiveRepository.CommitTransaction();
                   return IsObjectiveUpdated;
               }
               else
               {
                   _ObjectiveRepository.RollbackTransaction();
                   return IsObjectiveUpdated;
               }
           }
           catch
           {
               _ObjectiveRepository.RollbackTransaction();
               throw;
           }

        }
        #endregion
    }
}
