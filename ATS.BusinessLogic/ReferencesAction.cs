using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic
{
    public class ReferencesAction:ActionBase
    {
         #region private data member
        private DAClient.ReferencesRepository _ReferencesRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor

        public ReferencesAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _ReferencesRepository = new DAClient.ReferencesRepository(base.ConnectionString);
            _ReferencesRepository.CurrentUser = base.CurrentUser;
            _ReferencesRepository.CurrentClient = base.CurrentClient;

        }
        
        #endregion

        public List<BEClient.References> GetReferencesByProfileId(Guid ProfileId)
        {
            try
            {
                return _ReferencesRepository.GetReferencesProfileId(ProfileId);
            }
            catch
            {
                throw;
            }
        }


        public bool IsRecordExists(Guid recordid)
        {
            try
            {
                DAClient.CommonRepository _common = new DAClient.CommonRepository(base.ConnectionString);
                return Convert.ToBoolean(_common.IsRecordExists("Reference", "ReferenceId", recordid));
            }
            catch (Exception)
            {

                throw;
            }
        }


        #region CRUD

       


        public Guid AddReferences(BEClient.References references)
        {
            try
            {
                _ReferencesRepository.BeginTransaction();
                Guid UserId = references.UserId;
                if (_ReferencesRepository.CurrentUser == null)
                {

                    _ReferencesRepository.CurrentUser = new BEClient.User() { UserId = UserId };
                }
                Guid newreferencesId = _ReferencesRepository.AddReferences(references);
                if (!newreferencesId.Equals(Guid.Empty))
                {
                    _ReferencesRepository.CommitTransaction();
                    return newreferencesId;
                }
                else
                {
                    _ReferencesRepository.RollbackTransaction();
                    return newreferencesId;
                }

            }
            catch
            {
                _ReferencesRepository.RollbackTransaction();
                throw;
            }
        }


        public bool UpdateReferences(BEClient.References references)
        {
            try
            {
                _ReferencesRepository.BeginTransaction();
                Guid UserId = references.UserId;
                if (_ReferencesRepository.CurrentUser == null)
                {

                    _ReferencesRepository.CurrentUser = new BEClient.User() { UserId = UserId };
                }
                bool IsReferencesUpdated = _ReferencesRepository.UpdateReferences(references);
                if (IsReferencesUpdated)
                {
                    _ReferencesRepository.CommitTransaction();
                    return IsReferencesUpdated;
                }
                else
                {
                    _ReferencesRepository.RollbackTransaction();
                    return IsReferencesUpdated;
                }
            }
            catch
            {
                _ReferencesRepository.RollbackTransaction();
                throw;
            }
        }

        public bool DeleteReferences(Guid recordid)
        {
            try
            {
                _ReferencesRepository.BeginTransaction();
                bool isRecordDeleted = _ReferencesRepository.DeleteReferences(recordid);

                if (isRecordDeleted)
                {
                    _ReferencesRepository.CommitTransaction();
                    return isRecordDeleted;
                }
                else
                {
                    _ReferencesRepository.RollbackTransaction();
                    return isRecordDeleted;
                }

            }
            catch
            {
                _ReferencesRepository.RollbackTransaction();
                throw;
            }
        }

        public bool DeleteAllReferences(Guid profileid)
        {
            try
            {
                _ReferencesRepository.BeginTransaction();
                bool isRecordDeleted = _ReferencesRepository.DeleteAllReferences(profileid);

                if (isRecordDeleted)
                {
                    _ReferencesRepository.CommitTransaction();
                    return isRecordDeleted;
                }
                else
                {
                    _ReferencesRepository.RollbackTransaction();
                    return isRecordDeleted;
                }

            }
            catch
            {
                _ReferencesRepository.RollbackTransaction();
                throw;
            }
        }


        #endregion
    }
}
