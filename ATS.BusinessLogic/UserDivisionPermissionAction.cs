using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;
using System.Data.Common;
using System.Data;
using BESrp = ATS.BusinessEntity.SRPEntity;
namespace ATS.BusinessLogic
{
   public class UserDivisionPermissionAction : ActionBase
    {
        #region private data member
        private DAClient.UserDivisionPermissionRepository _UserDivisionPermissionRepository;
        private Guid _MyClientId { get; set; }
        private BESrp.SRPUserDivisionPermission _SRPUserDivisionPermission { get; set; }

        #endregion

         #region Constructor

        public UserDivisionPermissionAction(Guid ClientId, bool CreateSRPObject = false)
            : base(ClientId)
        {
            if (CreateSRPObject)
                base.CreateSRPObject(SRPCommon.CreateSRPEntityObjects.SRPCreateObj<BESrp.SRPDivision>(ClientId));
            _MyClientId = ClientId;
            _UserDivisionPermissionRepository = new DAClient.UserDivisionPermissionRepository(base.ConnectionString);
            _UserDivisionPermissionRepository.CurrentUser = base.CurrentUser;
            _UserDivisionPermissionRepository.CurrentClient = base.CurrentClient;

        }
        #endregion

        public List<BEClient.UserDivisionPermission> GetAllUserDivisionPermission(Guid LanguageId)
        {
            try
            {
                string usersDivision = base.GetAllDivisionsByCurrentUser;
                List<BEClient.UserDivisionPermission> _UserDivisionPermissionList = _UserDivisionPermissionRepository.GetAllUserDivisionPermissionByClient(_MyClientId, LanguageId, usersDivision);

                foreach (BEClient.UserDivisionPermission userdivisionpermission in _UserDivisionPermissionList)
                {
                    base.SetRoleRecordWise(userdivisionpermission, userdivisionpermission.DivisionId);
                }
                return _UserDivisionPermissionList;
            }
            catch
            {
                throw;
            }
        }

        public BEClient.UserDivisionPermission GetUserDivisionPermissionById(Guid UserDivisionPermission,Guid LanguageId)
        {
            try
            {
               BEClient.UserDivisionPermission _UserDivisionPermission =  _UserDivisionPermissionRepository.GetUserDivisionPermissionById(UserDivisionPermission, LanguageId);
               base.SetRoleRecordWise(_UserDivisionPermission, _UserDivisionPermission.DivisionId);
               return _UserDivisionPermission;

            }
            catch
            {
                throw;
            }
        }

       
       #region CRUD 

       public Guid AddUserDivisionPermission(BEClient.UserDivisionPermission pUserDivisionPermission)
       {
           try
           {
               _UserDivisionPermissionRepository.BeginTransaction();
                Guid UserDivisionPermissionId = _UserDivisionPermissionRepository.Add(pUserDivisionPermission);
                _UserDivisionPermissionRepository.CommitTransaction();
                return UserDivisionPermissionId;
           
           
           }
           catch
           {
               _UserDivisionPermissionRepository.RollbackTransaction();
               throw;
           }
       }

       public bool UpdateUserDivisionpermission(BEClient.UserDivisionPermission _pUserDivisionpermission)
       {
           try
           {
               return _UserDivisionPermissionRepository.Update(_pUserDivisionpermission);
           }
           catch
           {
               throw;
           }
       }

       public bool DeleteDivisionByUserAndClient(Guid ClientId,Guid UserId)
       {
           try
           {
               bool result = false;
               _UserDivisionPermissionRepository.BeginTransaction();
               result = _UserDivisionPermissionRepository.DeleteDivisionByUserAndClient(ClientId, UserId);
               if(result)
               {
                   _UserDivisionPermissionRepository.CommitTransaction();

               }
               else
               {
                   _UserDivisionPermissionRepository.RollbackTransaction();
               }

               return result;
           }
           catch
           {
               _UserDivisionPermissionRepository.RollbackTransaction();
               throw;
           }

       }
       public List<BEClient.UserDivisionPermission> GetAllAdhocDivisionByUser(Guid UserId,Guid LanguageId)
       {
           try
           {
               return _UserDivisionPermissionRepository.GetAllAdhocDivisionByUser(UserId, LanguageId);
           }
           catch
           {
               throw;
           }
       }

       public bool DeleteAdhocDivisionById(Guid UserDivisionPermissionId)
       {
           try
           {
               return _UserDivisionPermissionRepository.DeleteAdhocDivisionById(UserDivisionPermissionId);
           }
           catch
           {
               throw;
           }
       }
        #endregion
    }
}
