﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.BusinessEntity.SRPEntity
{
    public class SRPUserDivisionPermission:IListSecurityEntity
    {
        public List<UserDivisionPermission> ListUserDivisionPermission { get; set; }
        public List<ISecurityEntity> ListSecurityEntity { get; set; }
        public ATSPrivilage SRPPrivilage { get { return ATSPrivilage.Division; } }
    }
}
