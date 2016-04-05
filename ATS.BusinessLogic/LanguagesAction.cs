using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;
namespace ATS.BusinessLogic
{
    public class LanguagesAction:ActionBase
    {
        #region private data member
        private DAClient.LanguagesRepository _LanguagesRepository;
        private Guid _MyClientId { get; set; }
        #endregion
        #region Constructor

        public LanguagesAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _LanguagesRepository = new DAClient.LanguagesRepository(base.ConnectionString);
            _LanguagesRepository.CurrentUser = base.CurrentUser;
            _LanguagesRepository.CurrentClient = base.CurrentClient;

        }
        #endregion
        public List<BEClient.Languages> GetLanguagesByProfileId(Guid ProfileId)
        {
            try
            {
                return _LanguagesRepository.GetLanguagesByProfileId(ProfileId);
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
                return Convert.ToBoolean(_common.IsRecordExists("Languages", "LanguagesId", recordid));
            }
            catch (Exception)
            {

                throw;
            }
        }


        public Guid AddLanguages(BEClient.Languages languages)
        {
            try
            {
                _LanguagesRepository.BeginTransaction();
                Guid UserId = languages.UserId;
                if (_LanguagesRepository.CurrentUser == null)
                {

                    _LanguagesRepository.CurrentUser = new BEClient.User() { UserId = UserId };
                }
                Guid newlanguagesId = _LanguagesRepository.AddLanguages(languages);
                if (!newlanguagesId.Equals(Guid.Empty))
                {
                    _LanguagesRepository.CommitTransaction();
                    return newlanguagesId;
                }
                else
                {
                    _LanguagesRepository.RollbackTransaction();
                    return newlanguagesId;
                }

            }
            catch
            {
                _LanguagesRepository.RollbackTransaction();
                throw;
            }
        }


        public bool UpdateLanguages(BEClient.Languages languages)
        {
            try
            {
                _LanguagesRepository.BeginTransaction();
                Guid UserId = languages.UserId;
                if (_LanguagesRepository.CurrentUser == null)
                {

                    _LanguagesRepository.CurrentUser = new BEClient.User() { UserId = UserId };
                }
                bool IsLanguagesUpdated = _LanguagesRepository.UpdateLanguages(languages);
                if (IsLanguagesUpdated)
                {
                    _LanguagesRepository.CommitTransaction();
                    return IsLanguagesUpdated;
                }
                else
                {
                    _LanguagesRepository.RollbackTransaction();
                    return IsLanguagesUpdated;
                }
            }
            catch
            {
                _LanguagesRepository.RollbackTransaction();
                throw;
            }
        }

        public bool DeleteLanguages(Guid recordid)
        {
            try
            {
                _LanguagesRepository.BeginTransaction();
                bool isRecordDeleted = _LanguagesRepository.DeleteLanguages (recordid);

                if (isRecordDeleted)
                {
                    _LanguagesRepository.CommitTransaction();
                    return isRecordDeleted;
                }
                else
                {
                    _LanguagesRepository.RollbackTransaction();
                    return isRecordDeleted;
                }

            }
            catch
            {
                _LanguagesRepository.RollbackTransaction();
                throw;
            }
        }



        public bool DeleteAllLanguages(Guid profileid)
        {
            try
            {
                _LanguagesRepository.BeginTransaction();
                bool isRecordDeleted = _LanguagesRepository.DeleteAllLanguages(profileid);

                if (isRecordDeleted)
                {
                    _LanguagesRepository.CommitTransaction();
                    return isRecordDeleted;
                }
                else
                {
                    _LanguagesRepository.RollbackTransaction();
                    return isRecordDeleted;
                }

            }
            catch
            {
                _LanguagesRepository.RollbackTransaction();
                throw;
            }
        }


        
    }
}
