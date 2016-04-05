using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BEClient = ATS.BusinessEntity;
using System.Web.Script.Serialization;
namespace ATS.BusinessEntity
{
    public class ATSSecurityRolePrivilages : BaseEntity, ISecurityEntity 
    {
        public Guid SRP_Id { get; set; }
          [ScriptIgnore]
        public Guid ClientId { get; set; }

        public String RoleLocalName { get; set; }
        public bool IsChecked { get; set; }
        public int SqNo { get; set; }

        #region ISecurityEntity
        public ATSPrivilage Privilage{get;set;}
        public ATSSecurityRole SecurityRole { get; set; }
        public ATSPermissionType PermissionType { get; set; }
        public ATSScope Scope { get; set; }
        #endregion ISecurityEntity
    }
}
