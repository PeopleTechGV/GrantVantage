using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;
using BESrp = ATS.BusinessEntity.SRPEntity;

namespace ATS.BusinessLogic
{
    public class ReviewersAction : ActionBase
    {

        #region private data member
        private DAClient.ReviewersRepository _ReviewersRepository;
        private Guid _MyClientId { get; set; }
        private Guid _CurrentLanguageId;
        //private BESrp.SRPJobLocation _SRPJobLocation { get; set; }
        //private BESrp.SRPJobLocation SRPJobLocation { get { return SRPJobLocation; } }
        #endregion


        public ReviewersAction(Guid ClientId, bool CreateSRPObject = false)
            : base(ClientId)
        {
            if (CreateSRPObject)
                base.CreateSRPObject(SRPCommon.CreateSRPEntityObjects.SRPCreateObj<BESrp.SRPReviewers>(ClientId));

                _MyClientId = ClientId;
            _ReviewersRepository = new DAClient.ReviewersRepository(base.ConnectionString);
            _ReviewersRepository.CurrentUser = base.CurrentUser;
            _ReviewersRepository.CurrentClient = base.CurrentClient;
        }

        public Guid InsertVacReviewMember(BEClient.Reviewers objReviewers)
        {
            try
            {
                _ReviewersRepository.BeginTransaction();

                Guid ReviewersId = _ReviewersRepository.InsertVacReviewMember(objReviewers);

                _ReviewersRepository.CommitTransaction();

                return ReviewersId;

            }
            catch
            {
                _ReviewersRepository.RollbackTransaction();
                throw;
            }
        }

        public bool Delete(Guid vacReviewMemberId)
        {
            try
            {
                bool Result = false;
                _ReviewersRepository.BeginTransaction();
                Result = _ReviewersRepository.Delete(vacReviewMemberId);
                if (Result)
                {
                    _ReviewersRepository.CommitTransaction();
                    return Result;
                }
                return Result;

            }
            catch
            {
                _ReviewersRepository.RollbackTransaction();
                throw;
            }

        }

        public bool Update(BEClient.Reviewers objReviewers)
        {
            try
            {
                _ReviewersRepository.BeginTransaction();

                bool recordUpdated = _ReviewersRepository.Update(objReviewers);

                _ReviewersRepository.CommitTransaction();
                return recordUpdated;
            }
            catch
            {
                _ReviewersRepository.RollbackTransaction();
                throw;
            }
        }

        public List<BEClient.Reviewers> GetAllReviewersByRoundIdAndQuestionId(Guid RoundId, Guid LanguageId, Guid? VacQueId, Guid ApplicationId)
        {
            try
            {
                return _ReviewersRepository.GetAllReviewersByRoundId(RoundId, LanguageId, ApplicationId, VacQueId);
            }
            catch
            {
                throw;
            }
        }

        public List<BEClient.Reviewers> GetAllReviewersByRoundId(Guid RoundId, Guid LanguageId)
        {
            try
            {
                List<BEClient.Reviewers> _objReviewersList = _ReviewersRepository.GetAllReviewersByRoundId(RoundId, LanguageId);
                foreach (BEClient.Reviewers current in _objReviewersList)
                {
                    base.SetRoleRecordWise(current, current.DivisionId);
                }

                return _objReviewersList;
            }
            catch
            {
                throw;
            }
        }

        public BEClient.Reviewers GetDivisonByUserId(Guid UserId, Guid LanguageId)
        {
            try
            {
                return _ReviewersRepository.GetDivisonByUserId(UserId, LanguageId);
            }
            catch
            {
                throw;
            }
        }
        public BEClient.Reviewers GetRecordByRecordId(Guid recordId, Guid LanguageId)
        {
            try
            {
                BEClient.Reviewers objReviewer = _ReviewersRepository.GetRecordByRecordId(recordId, LanguageId);
                base.SetRoleRecordWise(objReviewer, objReviewer.DivisionId);
                return objReviewer;
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
                return Convert.ToBoolean(_common.IsRecordExists("VacReviewMember", "VacReviewMemberId", recordid));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<BEClient.Reviewers> GetReviewersByVacRndId(Guid VacRndId)
        {
            try
            {
                return _ReviewersRepository.GetReviewersByVacRndId(VacRndId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Guid GetVacReviewerMemberByVacRndAndUser(Guid VacRndId, Guid UserId)
        {
            try
            {
                return _ReviewersRepository.GetVacReviewerMemberByVacRndAndUser(VacRndId, UserId);
            }
            catch
            {
                throw;
            }
        }
        public Guid GetUserByVacRmId(Guid VacRMId)
        {
            try
            {
                return _ReviewersRepository.GetUserIdByVacRMId(VacRMId);
            }
            catch
            {
                throw;
            }
        }
        public bool DeleteVacReviewMemberById(Guid VacReviewMemberId)
        {
            try
            {
                return _ReviewersRepository.DeleteVacReviewMemberById(VacReviewMemberId);
            }
            catch
            {
                throw;
            }
        }

        public bool AuthorizeRevmemberForPromoteCandidate(Guid VacRndId, Guid UserId)
        {
            try
            {
                return _ReviewersRepository.AuthorizeRevmemberForPromoteCandidate(VacRndId, UserId);
            }
            catch
            {
                throw;
            }
        }
        public bool IsReviewerForRound(Guid VacRndId, Guid UserId)
        {
            try
            {
                return _ReviewersRepository.IsReviewerForRound(VacRndId, UserId);
            }
            catch
            {
                throw;
            }
        }
        public List<BEClient.Reviewers> GetUnassignedReviewers(Guid vacRndId, Guid ApplicationId)
        {
            try
            {
                return _ReviewersRepository.GetUnAssignedReviewers(vacRndId, ApplicationId);
            }
            catch
            {
                throw;
            }
        }
        public BEClient.Reviewers GetReviewerDetails(Guid UserId, Guid VacRndId)
        {
            try
            {
                return _ReviewersRepository.GetReviewerDetails(UserId, VacRndId);
            }
            catch
            {
                throw;
            }

        }

    }
}