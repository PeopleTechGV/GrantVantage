using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic
{
    public class TVacReviewMemberAction : ActionBase
    {
        #region private data member
        private DAClient.TVacReviewMemberRepository _TVacReviewMemberRepository;
        private Guid _MyClientId { get; set; }
        #endregion

        #region Constructor
        public TVacReviewMemberAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _TVacReviewMemberRepository = new DAClient.TVacReviewMemberRepository(base.ConnectionString);
            _TVacReviewMemberRepository.CurrentUser = base.CurrentUser;
            _TVacReviewMemberRepository.CurrentClient = base.CurrentClient;

        }
        #endregion
        public Guid InsertTVacReviewMember(BEClient.TVacReviewMember objTVacReviewMember)
        {
            try
            {
                return _TVacReviewMemberRepository.InsertTVacReviewMember(objTVacReviewMember);
            }
            catch
            {
                throw;
            }
        }


        public bool UpdateTVacReviewMember(BEClient.TVacReviewMember objTVacReviewMember)
        {
            try
            {
                return _TVacReviewMemberRepository.UpdateTVacReviewMember(objTVacReviewMember);
            }
            catch
            {
                throw;
            }
        }

        public BEClient.TVacReviewMember GetTVacReviewMemberById(Guid TVacReviewMemberId,Guid LanguageId)
        {
            try
            {
                return _TVacReviewMemberRepository.GetTVacReviewMemberById(TVacReviewMemberId, LanguageId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.TVacReviewMember> GetTVacReviewMemberByTVacId(Guid TVacId)
        {
            try
            {
                return _TVacReviewMemberRepository.GetTVacReviewMemberByTVacId(TVacId);
            }
            catch
            {
                throw;
            }
        }


        public List<BEClient.TVacReviewMember> GetAllReviewersByTRoundId(Guid TRoundId, Guid LanguageId)
        {
            try
            {
                return _TVacReviewMemberRepository.GetAllReviewersByTRoundId(TRoundId, LanguageId);
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
                return Convert.ToBoolean(_common.IsRecordExists("TVacReviewMember", "TVacReviewMemberId", recordid));
            }
            catch (Exception)
            {

                throw;
            }
        }
        public BEClient.TVacReviewMember GetDivisonByUserId(Guid UserId, Guid LanguageId)
        {
            try
            {
                return _TVacReviewMemberRepository.GetDivisonByUserId(UserId, LanguageId);
            }
            catch
            {
                throw;
            }
        }

        public bool Delete(Guid vacReviewMemberId)
        {
            try
            {
                bool Result = false;
                _TVacReviewMemberRepository.BeginTransaction();
                Result = _TVacReviewMemberRepository.Delete(vacReviewMemberId);
                if (Result)
                {
                    _TVacReviewMemberRepository.CommitTransaction();
                    return Result;
                }
                return Result;

            }
            catch
            {
                _TVacReviewMemberRepository.RollbackTransaction();
                throw;
            }

        }
    }
}