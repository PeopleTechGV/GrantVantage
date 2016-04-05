using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic
{
    public class SaveResumeSearchAction : ActionBase
    {
        #region private data member
        private DAClient.SaveSearchResumeRepository _SaveSearchResumeRepository;
        private Guid _MyClientId { get; set; }
        #endregion
        #region Constructor

        public SaveResumeSearchAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _SaveSearchResumeRepository = new DAClient.SaveSearchResumeRepository(base.ConnectionString);
            _SaveSearchResumeRepository.CurrentUser = base.CurrentUser;
            _SaveSearchResumeRepository.CurrentClient = base.CurrentClient;

        }
        #endregion


        public Guid AddSaveResumeSearch(BEClient.SaveSearchResume savesearchresume)
        {
            try
            {
                _SaveSearchResumeRepository.BeginTransaction();
                Guid UserId = savesearchresume.UserId;
                if (_SaveSearchResumeRepository.CurrentUser == null)
                {

                    _SaveSearchResumeRepository.CurrentUser = new BEClient.User() { UserId = UserId };
                }
                Guid newSaveSearchResumeId = _SaveSearchResumeRepository.AddSaveResumeSearch(savesearchresume);
                if (!newSaveSearchResumeId.Equals(Guid.Empty))
                {
                    _SaveSearchResumeRepository.CommitTransaction();
                    return newSaveSearchResumeId;
                }
                else
                {

                    _SaveSearchResumeRepository.RollbackTransaction();
                    return newSaveSearchResumeId;
                }
            }
            catch
            {
                _SaveSearchResumeRepository.RollbackTransaction();
                throw;
            }
        }

        public bool IsExistsResumeSearch(string SearchQueryName)
        {
            try
            {
                return _SaveSearchResumeRepository.ExistsSearchQueryName(SearchQueryName);
            }

            catch
            {
                throw;
            }

        }

        public bool UpdateSaveSearchResume(BEClient.SaveSearchResume savesearchresume)
        {
            try
            {
                _SaveSearchResumeRepository.BeginTransaction();
                Guid UserId = savesearchresume.UserId;
                if (_SaveSearchResumeRepository.CurrentUser == null)
                {

                    _SaveSearchResumeRepository.CurrentUser = new BEClient.User() { UserId = UserId };
                }
                bool IssavesearchresumetUpdated = _SaveSearchResumeRepository.UpdateSaveResumeSearch(savesearchresume);
                if (IssavesearchresumetUpdated)
                {
                    _SaveSearchResumeRepository.CommitTransaction();
                    return IssavesearchresumetUpdated;
                }
                else
                {
                    _SaveSearchResumeRepository.RollbackTransaction();
                    return IssavesearchresumetUpdated;
                }
            }
            catch
            {
                _SaveSearchResumeRepository.RollbackTransaction();
                throw;
            }
        }

        public BEClient.SaveSearchResume GetSearchQuery(Guid SearchQueryId)
        {
            try
            {
                return _SaveSearchResumeRepository.GetSearchQuery(SearchQueryId);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.SaveSearchResume GetSaveSearchQueryByUserId(Guid UserId)
        {
            try
            {
                return _SaveSearchResumeRepository.GetSaveSearchQueryByUserId(UserId);
            }
            catch
            {
                throw;
            }
        }

        public bool SetSaveDefaultSearchQuery(BEClient.SaveSearchResume savesearchresume)
        {
            try
            {
                _SaveSearchResumeRepository.BeginTransaction();
                Guid UserId = savesearchresume.UserId;
                if (_SaveSearchResumeRepository.CurrentUser == null)
                {

                    _SaveSearchResumeRepository.CurrentUser = new BEClient.User() { UserId = UserId };
                }
                bool IssavesearchresumetUpdated = _SaveSearchResumeRepository.SetSaveDefaultSearchQuery(savesearchresume);
                if (IssavesearchresumetUpdated)
                {
                    _SaveSearchResumeRepository.CommitTransaction();
                    return IssavesearchresumetUpdated;
                }
                else
                {
                    _SaveSearchResumeRepository.RollbackTransaction();
                    return IssavesearchresumetUpdated;
                }
            }
            catch
            {
                _SaveSearchResumeRepository.RollbackTransaction();
                throw;
            }
        }


        public List<BEClient.SaveSearchResume> GetAllSaveSearchResumeByUser(Guid UserId)
        {
            try
            {
                return _SaveSearchResumeRepository.GetSaveSearchEResumeByUserId(UserId);
            }
            catch
            {
                throw;
            }

        }


        public bool DeleteSaveResumeSearch(BEClient.SaveSearchResume savesearchresume)
        {
            try
            {
                _SaveSearchResumeRepository.BeginTransaction();
                Guid UserId = savesearchresume.UserId;
                if (_SaveSearchResumeRepository.CurrentUser == null)
                {

                    _SaveSearchResumeRepository.CurrentUser = new BEClient.User() { UserId = UserId };
                }
                bool IssavesearchresumetUpdated = _SaveSearchResumeRepository.DeleteSaveResumeSearch(savesearchresume);
                if (IssavesearchresumetUpdated)
                {
                    _SaveSearchResumeRepository.CommitTransaction();
                    return IssavesearchresumetUpdated;
                }
                else
                {
                    _SaveSearchResumeRepository.RollbackTransaction();
                    return IssavesearchresumetUpdated;
                }
            }
            catch
            {
                _SaveSearchResumeRepository.RollbackTransaction();
                throw;
            }
        }


    }
}
