using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using BESecurityTemplateConst = ATS.BusinessEntity.Common.SecurityTemplateConstant;

namespace ATS.BusinessEntity
{
    public class ATSSecurityRoles : ClientBaseEntity
    {
        public Guid ATSSecurityRoleId { get; set; }

        public Guid ClientId { get; set; }

        [Display(Name = BESecurityTemplateConst.FRM_SEC_ROLE_NAME)]
        public string DefaultName { get; set; }

        public string LocalName { get; set; }

        public Int32 SequenceNo { get; set; }

        public List<EntityLanguage> AtsSecurityEntityLanguage { get; set; }

        [Display(Name = BESecurityTemplateConst.FRM_SEC_EXISTING_SECURITY_ROLES)]
        public Guid ExistingATSSecurityRoleId { get; set; }

        public override ATSPrivilage Privilage
        {
            get
            {
                return ATSPrivilage.SecurityRole;
            }

        }
        public List<ATSSecurityRolePrivilages> lstAtsPrivileges { get; set; }
        public List<ATSRelativePrivilege> lstATSRelativePrivilege { get; set; }
        public List<ATSRolePrivilege> lstATSRolePrivilege { get; set; }
    }
}
