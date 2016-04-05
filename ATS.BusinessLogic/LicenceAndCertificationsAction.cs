using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic
{
    public class LicenceAndCertificationsAction:ActionBase
    {
        #region private data member
        private DAClient.LicenceAndCertificationsRepository _LicenceAndCertificationsRepository;
        private Guid _MyClientId { get; set; }
        #endregion

         #region Constructor

        public LicenceAndCertificationsAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _LicenceAndCertificationsRepository = new DAClient.LicenceAndCertificationsRepository(base.ConnectionString);
            _LicenceAndCertificationsRepository.CurrentUser = base.CurrentUser;
            _LicenceAndCertificationsRepository.CurrentClient = base.CurrentClient;

        }
        #endregion
        public List<BEClient.LicenceAndCertifications> GetLicenceAndCertificationsByProfileId(Guid ProfileId)
        {
            try
            {
                return _LicenceAndCertificationsRepository.GetLicenceAndCertificationsByProfileId(ProfileId);
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
                return Convert.ToBoolean(_common.IsRecordExists("LicenceAndCertifications", "LicenceAndCertificationsId", recordid));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Guid AddLicenceandHistory(BEClient.LicenceAndCertifications licenceandhistory)
        {
            try
            {
                _LicenceAndCertificationsRepository.BeginTransaction();
                Guid UserId = licenceandhistory.UserId;
                if (_LicenceAndCertificationsRepository.CurrentUser == null)
                {

                    _LicenceAndCertificationsRepository.CurrentUser = new BEClient.User() { UserId = UserId };
                }
                Guid newlicenceandcertificationsId = _LicenceAndCertificationsRepository.AddLicenceAndCertifications(licenceandhistory);
                if (!newlicenceandcertificationsId.Equals(Guid.Empty))
                {
                  _LicenceAndCertificationsRepository.CommitTransaction();
                 return newlicenceandcertificationsId;
                }                    
                else
                {
                    _LicenceAndCertificationsRepository.RollbackTransaction();
                    return newlicenceandcertificationsId;
                }
                
            }
            catch
            {
                _LicenceAndCertificationsRepository.RollbackTransaction();
                throw;
            }
        }

        public bool UpdateLicenceAndHistory(BEClient.LicenceAndCertifications licenceandhistory)
        {
            try
            {
                _LicenceAndCertificationsRepository .BeginTransaction();
                Guid UserId = licenceandhistory.UserId;
                if (_LicenceAndCertificationsRepository.CurrentUser == null)
                {

                    _LicenceAndCertificationsRepository.CurrentUser = new BEClient.User() { UserId = UserId };
                }
                bool IsLicenceandHistoryUpdated = _LicenceAndCertificationsRepository.UpdateLicenceAndCertifications(licenceandhistory);
                if (IsLicenceandHistoryUpdated)
                {
                    _LicenceAndCertificationsRepository.CommitTransaction();
                    return IsLicenceandHistoryUpdated;
                }
                else
                {
                    _LicenceAndCertificationsRepository.RollbackTransaction();
                    return IsLicenceandHistoryUpdated;
                }
            }
            catch
            {
                _LicenceAndCertificationsRepository.RollbackTransaction();
                throw;
            }
        }


        public bool DeleteLicenceAndCertifications(Guid recordid)
        {
            try
            {
                _LicenceAndCertificationsRepository.BeginTransaction();
                bool isRecordDeleted = _LicenceAndCertificationsRepository.DeleteLicenceAndCertifications(recordid);

                if (isRecordDeleted)
                {
                    _LicenceAndCertificationsRepository.CommitTransaction();
                    return isRecordDeleted;
                }
                else
                {
                    _LicenceAndCertificationsRepository.RollbackTransaction();
                    return isRecordDeleted;
                }

            }
            catch
            {
                _LicenceAndCertificationsRepository.RollbackTransaction();
                throw;
            }
        }


        public bool DeleteAllLicenceAndCertifications(Guid profileid)
        {
            try
            {
                _LicenceAndCertificationsRepository.BeginTransaction();
                bool isRecordDeleted = _LicenceAndCertificationsRepository.DeleteAllLicenceAndCertifications(profileid);

                if (isRecordDeleted)
                {
                    _LicenceAndCertificationsRepository.CommitTransaction();
                    return isRecordDeleted;
                }
                else
                {
                    _LicenceAndCertificationsRepository.RollbackTransaction();
                    return isRecordDeleted;
                }

            }
            catch
            {
                _LicenceAndCertificationsRepository.RollbackTransaction();
                throw;
            }
        }

    }
}
