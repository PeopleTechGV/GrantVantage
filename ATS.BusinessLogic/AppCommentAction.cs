using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAClient = ATS.DataAccess;
using BEClient = ATS.BusinessEntity;

namespace ATS.BusinessLogic
{
    public class AppCommentAction : ActionBase
    {

        #region private data member
        private DAClient.AppCommentRepository _AppCommentRepository;
        private Guid _MyClientId { get; set; }

        #endregion

        #region Constructor

        public AppCommentAction(Guid ClientId)
            : base(ClientId)
        {


            _MyClientId = ClientId;
            _AppCommentRepository = new DAClient.AppCommentRepository(base.ConnectionString);
            _AppCommentRepository.CurrentUser = base.CurrentUser;
            _AppCommentRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public Guid AddAppComment(BEClient.AppComment _appcommnet)
        {
            Guid Result = Guid.Empty;
            try
            {
                _AppCommentRepository.BeginTransaction();
                    Result = _AppCommentRepository.AddAppComment(_appcommnet);
                if(Result != Guid.Empty)
                {
                    _AppCommentRepository.CommitTransaction();
                }
                else
                {
                    _AppCommentRepository.RollbackTransaction();
                }
                
            }
            catch
            {
                _AppCommentRepository.RollbackTransaction();
                throw;
            }
            return Result;
        }

        public List<BEClient.AppComment> GetCommentsByAppId(Guid AppId)
        {
            try
            {
                return _AppCommentRepository.GetCommentsByApplicationId(AppId);
            }
            catch
            {
                throw;
            }
        }
        public BEClient.AppComment GetAppCommentById(Guid AppCommentId)
        {
            try
            {
                return _AppCommentRepository.GetCommentByAppCommentId(AppCommentId);
            }
            catch
            {
                throw;
            }
        }
    }
}
