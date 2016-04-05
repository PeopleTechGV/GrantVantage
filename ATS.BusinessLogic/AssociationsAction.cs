using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic
{
    public class AssociationsAction:ActionBase
    {
          #region private data member
        private DAClient.AssociationsRepository _AssociationsRepository;
        private Guid _MyClientId { get; set; }
        #endregion
        #region Constructor

        public AssociationsAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _AssociationsRepository = new DAClient.AssociationsRepository(base.ConnectionString);
            _AssociationsRepository.CurrentUser = base.CurrentUser;
            _AssociationsRepository.CurrentClient = base.CurrentClient;

        }
        #endregion
        public List<BEClient.Associations> GetAssociationsByProfileId(Guid ProfileId)
        {
            try
            {
                return _AssociationsRepository.GetAssociationsByProfileId(ProfileId);
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
                return Convert.ToBoolean(_common.IsRecordExists("Associations", "AssociationsId", recordid));
            }
            catch (Exception)
            {

                throw;
            }
        }


        public Guid AddAssociations(BEClient.Associations associations)
        {
            try
            {
                _AssociationsRepository.BeginTransaction();
                Guid UserId = associations.UserId;
                if (_AssociationsRepository.CurrentUser == null)
                {

                    _AssociationsRepository.CurrentUser = new BEClient.User() { UserId = UserId };
                }
                Guid newAssociationsId = _AssociationsRepository.AddAssociations(associations);
                if (!newAssociationsId.Equals(Guid.Empty))
                {
                    _AssociationsRepository.CommitTransaction();
                    return newAssociationsId;
                }
                else
                {
                    _AssociationsRepository.RollbackTransaction();
                    return newAssociationsId;
                }

            }
            catch
            {
                _AssociationsRepository.RollbackTransaction();
                throw;
            }
        }



        public bool UpdateAssociations(BEClient.Associations associations)
        {
            try
            {
                _AssociationsRepository.BeginTransaction();
                Guid UserId = associations.UserId;
                if (_AssociationsRepository.CurrentUser == null)
                {

                    _AssociationsRepository.CurrentUser = new BEClient.User() { UserId = UserId };
                }
                bool IsAssociationsUpdated = _AssociationsRepository.UpdateAssociations(associations);
                if (IsAssociationsUpdated)
                {
                    _AssociationsRepository.CommitTransaction();
                    return IsAssociationsUpdated ;
                }
                else
                {
                    _AssociationsRepository.RollbackTransaction();
                    return IsAssociationsUpdated;
                }
            }
            catch
            {
                _AssociationsRepository.RollbackTransaction();
                throw;
            }
        }

        public bool DeleteAssociation(Guid recordid)
        {
            try
            {
                _AssociationsRepository.BeginTransaction();
                bool isRecordDeleted = _AssociationsRepository.DeleteAssociations(recordid);

                if (isRecordDeleted)
                {
                    _AssociationsRepository.CommitTransaction();
                    return isRecordDeleted;
                }
                else
                {
                    _AssociationsRepository.RollbackTransaction();
                    return isRecordDeleted;
                }

            }
            catch
            {
                _AssociationsRepository.RollbackTransaction();
                throw;
            }
        }

        public bool DeleteAllAssociations(Guid profileid)
        {
            try
            {
                _AssociationsRepository.BeginTransaction();
                bool isRecordDeleted = _AssociationsRepository.DeleteAllAssociations(profileid);

                if (isRecordDeleted)
                {
                    _AssociationsRepository.CommitTransaction();
                    return isRecordDeleted;
                }
                else
                {
                    _AssociationsRepository.RollbackTransaction();
                    return isRecordDeleted;
                }

            }
            catch
            {
                _AssociationsRepository.RollbackTransaction();
                throw;
            }
        }


    }
}
