using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using DAClient = ATS.DataAccess;

namespace ATS.BusinessLogic
{
   public class AdHocPrivilegeAction : ActionBase
    {
        #region private data member
       private DAClient.AdHocPrivilegeRepository _AdHocPrivilegeRepository;
        private Guid _MyClientId { get; set; }
        #endregion

         #region Constructor

        public AdHocPrivilegeAction(Guid ClientId)
            : base(ClientId)
        {
            _MyClientId = ClientId;
            _AdHocPrivilegeRepository = new DAClient.AdHocPrivilegeRepository(base.ConnectionString);
            _AdHocPrivilegeRepository.CurrentUser = base.CurrentUser;
            _AdHocPrivilegeRepository.CurrentClient = base.CurrentClient;
        }
        #endregion

        public List<BEClient.PrivilegeAndPermissionAndScope> GetAdHocRolesByUserAndClient(Guid userId)
        {
            try
            {
                List<BEClient.AdHocPrivilege> objAdHocPrivilege = _AdHocPrivilegeRepository.GetAdHocRolesByUserAndClient(_MyClientId, userId);

                List<BEClient.PrivilegeAndPermissionAndScope> objPrivilegeAndPermissionAndScopeList = new List<BEClient.PrivilegeAndPermissionAndScope>();
                
                foreach (var a in objAdHocPrivilege.GroupBy(r => new { r.ATSPrivilegeId }).Select(r => r.FirstOrDefault()))
                {
                    BEClient.PrivilegeAndPermissionAndScope objPrivilegeAndPermissionAndScope = new BEClient.PrivilegeAndPermissionAndScope();
                    objPrivilegeAndPermissionAndScope.PrivilegeId = a.ATSPrivilegeId;
                    List<BEClient.PermissionAndScope> objPermissionAndScopeList = new List<BEClient.PermissionAndScope>();
                    foreach (var i in objAdHocPrivilege.GroupBy(r => new { r.ATSPrivilegeId,r.PermissionType }).Select(r => r.FirstOrDefault()).Where(r => r.ATSPrivilegeId.Equals(a.ATSPrivilegeId)))
                    {
                        BEClient.PermissionAndScope objPermissionAndScope = new BEClient.PermissionAndScope();
                        List<BEClient.ScopeList> objScopeList = new List<BEClient.ScopeList>();
                        
                        var v = objAdHocPrivilege.Where(r => r.ATSPrivilegeId.Equals(a.ATSPrivilegeId) && r.PermissionType.Equals(i.PermissionType)).Select(r => r.Scope);
                        foreach (var j in v)
                        {
                            BEClient.ScopeList objScope = new BEClient.ScopeList();
                            objScope.Scope = j;
                            objScopeList.Add(objScope);
                        }
                        objPermissionAndScope.ScopeList = objScopeList;
                        objPermissionAndScope.Permission = i.PermissionType;
                        objPermissionAndScopeList.Add(objPermissionAndScope);
                        objPrivilegeAndPermissionAndScope.PermissionList = objPermissionAndScopeList;
                    }
                    objPrivilegeAndPermissionAndScopeList.Add(objPrivilegeAndPermissionAndScope);
                }

                return objPrivilegeAndPermissionAndScopeList;
            }
            catch
            {
                throw;
            }
        }
    }
}
