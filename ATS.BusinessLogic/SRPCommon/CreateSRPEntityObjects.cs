using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BESRP = ATS.BusinessEntity.SRPEntity;
using BEClient = ATS.BusinessEntity;

using BLClient = ATS.BusinessLogic;

namespace ATS.BusinessLogic.SRPCommon
{
    public static class CreateSRPEntityObjects
    {
        public static T SRPCreateObj<T>(Guid ClientId)
        {
            var tmp = Activator.CreateInstance<T>();
            GetSRP((BEClient.IListSecurityEntity)tmp, ClientId);
            return tmp;
        }
        private static void GetSRP(BEClient.IListSecurityEntity securityentity, Guid ClientId)
        {
            BLClient.ATSSecurityRoleAction atsSecurityRoleAction = new ATSSecurityRoleAction(ClientId);
            securityentity.ListSecurityEntity = new List<BEClient.ISecurityEntity>();

            if (atsSecurityRoleAction.CurrentUser.SecurityRoles != null)
            {

                foreach (BEClient.UserSecurityRole _UserSecurityRole in atsSecurityRoleAction.CurrentUser.SecurityRoles)
                {
                    List<BEClient.ATSSecurityRolePrivilages> _objList = atsSecurityRoleAction.GetUserSecurityRole(securityentity.SRPPrivilage, _UserSecurityRole.SecurityRoleName);
                    foreach (BEClient.ATSSecurityRolePrivilages _obj in _objList)
                    {
                        securityentity.ListSecurityEntity.Add(_obj);
                    }
                }
            }
        }

    }
}
